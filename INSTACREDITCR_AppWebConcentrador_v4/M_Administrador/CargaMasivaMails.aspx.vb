Imports System.Data.SqlClient
Imports System.Data
Imports System.Diagnostics
Imports System.IO
Imports Conexiones
Imports Db
Imports Funciones
Imports System.Net.Mail
Imports System.Net.Mail.SmtpFailedRecipientsException
Imports System.ComponentModel
Imports Telerik.Web.UI

Partial Class CargaMasivaMails
    Inherits System.Web.UI.Page

    Dim ERRORS As String = "100"
    Dim SKIP As String = "1"
    Dim ctlCarga As String
    Dim logCarga As String
    Dim badCarga As String
    Dim Ruta As String = StrRuta() & "Correos\"

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim Usr As String = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO

        Catch ex As Exception
            OffLine(HidenUrs.Value)
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/SesionExpirada.aspx")
        End Try
        Try
            If Not IsPostBack Then

                Dim Facultad As String = CType(Session("USUARIOADMIN"), USUARIO).CAT_LO_MADMINISTRADOR.ToString.Substring(25, 1)
                If Facultad = "0" Then
                    OffLine(HidenUrs.Value)
                    Session.Clear()
                    Session.Abandon()
                    Response.Redirect("~/SesionExpirada.aspx")
                End If

                HidenUrs.Value = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
                LLENAR_DROP(16, DdlDelimitador, "Delimitador", "Delimitador")
                LLENAR_DROP(26, DdlCorreoSalida, "CAT_CONF_USER", "CAT_CONF_USER")
                Dim SSCommand As New SqlCommand("SP_ADD_CAT_PLANTILLAS_CORREO")
                SSCommand.CommandType = CommandType.StoredProcedure
                SSCommand.Parameters.Add("@V_Cat_Pc_Id", SqlDbType.NVarChar).Value = 0
                SSCommand.Parameters.Add("@V_Cat_Pc_Nombre", SqlDbType.NVarChar).Value = ""
                SSCommand.Parameters.Add("@V_CAT_PC_CONFIGURACION", SqlDbType.NVarChar).Value = DBNull.Value
                SSCommand.Parameters.Add("@V_CAT_PC_PRODUCTO", SqlDbType.NVarChar).Value = ""
                SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 4
                Dim DtsCorreo As DataTable = Consulta_Procedure(SSCommand, "Correo")
                If DtsCorreo.Rows.Count > 0 Then
                    DdlPlantilla.DataTextField = "Nombre"
                    DdlPlantilla.DataValueField = "Nombre"
                    DdlPlantilla.DataSource = DtsCorreo
                    DdlPlantilla.DataBind()
                    DdlPlantilla.Items.Add("Seleccione")
                    DdlPlantilla.SelectedValue = "Seleccione"
                End If
            End If
        Catch ex As Exception
            'SendMail("Page_Load", ex, "", "", HidenUrs.Value)
        End Try
    End Sub

    Dim CorreoPr As DataTable
    Dim NOENVIADOS As Integer
    Dim Credito As String
    Dim Usuario As String
    Dim Comentario As String
    Dim CORREO As String

    Protected Sub BtnCargar_Click(sender As Object, e As EventArgs) Handles BtnCargar.Click
        'Try
        If DdlPlantilla.SelectedValue = "Seleccione" Then
            aviso("Seleccione Una Plantilla Para Enviar Los Mails"
            )
        ElseIf DdlDelimitador.SelectedValue = "Seleccione" Then
            aviso("Seleccione Un Delimitador"
            )
        ElseIf RadAsyncUpload1.UploadedFiles.Count = 0 Then
            aviso("Seleccione Un Archivo"
            )
        Else
            Dim Directory As New IO.DirectoryInfo(Ruta)
            Dim allFiles As IO.FileInfo() = Directory.GetFiles.Where(Function(fi) fi.Extension.ToUpper = ".CSV" OrElse fi.Extension.ToUpper = ".TXT").ToArray
            If allFiles.Length = 0 Then
                aviso("Archivo No Seleccionado o No Valido "
                )
            Else
                Dim singleFile As IO.FileInfo
                Dim value As Long
                Dim FileCarga As String = ""

                For Each singleFile In allFiles
                    value = singleFile.Length
                    FileCarga = singleFile.Name
                    If value = 0 Then
                        My.Computer.FileSystem.DeleteFile(Ruta & FileCarga, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently, FileIO.UICancelOption.DoNothing)
                    Else
                        Dim TABLA As String = "TMP_CORREOS"

                        ctlCarga = Ruta & "CTL_CORREO_" & TABLA & "_" & Now.ToShortDateString.Replace("/", "") & "" & ".ctl"
                        logCarga = Ruta & "LOG_CORREO" & TABLA & "_" & Now.ToShortDateString.Replace("/", "") & "" & ".log"
                        badCarga = "C:\Users\ccasanova\Documents\clientes\TUIIO\errorescargas.csv"

                        If File.Exists(ctlCarga) Then
                            Kill(ctlCarga)
                        End If
                        If File.Exists(logCarga) Then
                            Kill(logCarga)
                        End If
                        If File.Exists(badCarga) Then
                            Kill(badCarga)
                        End If

                        Dim fs As Stream
                        fs = New System.IO.FileStream(ctlCarga, IO.FileMode.OpenOrCreate)
                        Dim sw As New System.IO.StreamWriter(fs)
                        sw.AutoFlush = True
                        sw.WriteLine("load data")
                        sw.WriteLine("infile '" & Ruta & "" & FileCarga & "'")
                        sw.WriteLine("into table " & TABLA & "")
                        sw.WriteLine("FIELDS TERMINATED BY '" & DdlDelimitador.SelectedValue & "' optionally enclosed by '""'")
                        sw.WriteLine("TRAILING NULLCOLS")
                        sw.WriteLine("(")
                        sw.WriteLine("TMP_PR_MC_CREDITO CHAR(25),")
                        sw.WriteLine("TMP_CORREO CHAR(50)")
                        sw.WriteLine(")")
                        sw.Close()
                        fs.Close()
                        '  Ejecuta("truncate table " & TABLA)
                        Dim SSCommand As New SqlCommand("SP_CARGA_TMP_ACTSALDOS")
                        SSCommand.CommandType = CommandType.StoredProcedure
                        SSCommand.Parameters.Add("@V_ARCHIVO", SqlDbType.NVarChar).Value = FileCarga
                        SSCommand.Parameters.Add("@V_ERROR", SqlDbType.NVarChar).Value = badCarga
                        SSCommand.Parameters.Add("@V_TABLA", SqlDbType.NVarChar).Value = TABLA
                        Ejecuta_Procedure(SSCommand)
                        Dim num As Integer = 0
                        If (num <> 0) Then
                            If (File.Exists(badCarga)) Then
                                Dim fic As New IO.StreamReader(badCarga, System.Text.Encoding.UTF8)
                                Dim linea As String = fic.ReadLine
                                fic.Close()
                                LblMensaje.Text = "Error en la cadena siguiente " & linea
                                LnkLog.Visible = True
                                LnkBad.Visible = True
                            End If
                        Else


                            'LblMensaje.Text = ""
                            'LnkLog.Visible = False
                            'LnkBad.Visible = False

                            '----CArgando correos de destino en el datatable
                            Dim SSCommand2 As New SqlCommand("SP_CARGAR_CORREOS")
                            SSCommand2.CommandType = CommandType.StoredProcedure
                            SSCommand2.Parameters.Add("@V_USUARIO", SqlDbType.NVarChar).Value = FileCarga
                            SSCommand2.Parameters.Add("@V_AGENCIA", SqlDbType.NVarChar).Value = badCarga
                            '''''''''''''''''''''''''''''''''''''''''''''checar agencia
                            Dim DtsCarga As DataTable = Consulta_Procedure(SSCommand2, "SP_CARGAR_CORREOS")
                            If DtsCarga.Rows.Count > 0 Then

                                '----Obteniendo correos de Salida

                                Dim SSCommand3 As New SqlCommand("SP_CARGAR_CORREOS")
                                SSCommand3.CommandType = CommandType.StoredProcedure
                                SSCommand3.Parameters.Add("@V_BANDERA", SqlDbType.NVarChar).Value = 26
                                SSCommand3.Parameters.Add("@V_Valor", SqlDbType.NVarChar).Value = DdlCorreoSalida.SelectedValue
                                Dim DtsMail As DataTable = Consulta_Procedure(SSCommand3, "Correo")

                                '----Obteniendo Plantilla de Correo

                                Dim SSCommand4 As New SqlCommand("SP_ADD_CAT_PLANTILLAS_CORREO")
                                SSCommand4.CommandType = CommandType.StoredProcedure
                                SSCommand4.Parameters.Add("@V_Cat_Pc_Id", SqlDbType.NVarChar).Value = 0
                                SSCommand4.Parameters.Add("@V_Cat_Pc_Nombre", SqlDbType.NVarChar).Value = DdlPlantilla.SelectedValue
                                SSCommand4.Parameters.Add("@V_CAT_PC_CONFIGURACION", SqlDbType.NVarChar).Value = DBNull.Value
                                SSCommand4.Parameters.Add("@V_CAT_PC_PRODUCTO", SqlDbType.NVarChar).Value = ""
                                SSCommand4.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 6
                                Dim dtsdesc As DataTable = Consulta_Procedure(SSCommand4, "mail")

                                '----Obtiene las etiquetas que se usaran para remplazar las etiquetas
                                Dim SSCommand5 As New SqlCommand("SP_CATALOGOS")
                                SSCommand5.CommandType = CommandType.StoredProcedure
                                SSCommand5.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 27
                                Dim dtsReplace As DataTable = Consulta_Procedure(SSCommand5, "remplazar")

                                Dim ENVIADOS As Integer = 0
                                'Dim NOENVIADOS As Integer = 0
                                For A As Integer = 0 To DtsCarga.Rows.Count - 1

                                    Dim msg As New System.Net.Mail.MailMessage()
                                    msg.[To].Add(DtsCarga.Rows(A)("CORREO"))
                                    'msg.[To].Add("adalberto.hernandez@mccollect.com.mx")
                                    'msg.[To].Add("blinares@mastercollection.com.mx")
                                    'msg.[To].Add("rcoyote@mastercollection.com.mx")
                                    msg.From = New MailAddress(DtsMail.Rows(0).Item("CAT_CONF_USER"), DtsMail.Rows(0).Item("CAT_CONF_RESPONSABLE"), System.Text.Encoding.UTF8)
                                    msg.Subject = DdlPlantilla.SelectedValue
                                    msg.SubjectEncoding = System.Text.Encoding.UTF8

                                    'Dim Querydesc As String
                                    'Querydesc = "SELECT CATCVALOR FROM CAT_CORREO where CATCDESC ='" & DdlPlantilla.SelectedValue & "'"



                                    Dim StrRemplazar As String = dtsdesc.Rows(0)("Cat_Pc_Configuracion").ToString.Replace("&lt;", "<").Replace("&gt;", ">")


                                    Dim SSCommand6 As New SqlCommand()

                                    If DtsCarga.Rows.Count > 0 Then '---Si existen correos se procede a sustituir las etiquetas
                                        For c As Integer = 0 To dtsReplace.Rows.Count - 1
                                            SSCommand6.CommandText = "SP_ADD_CAT_PLANTILLAS_CORREO" '---Obtiene el valor que se va a utilizar en la sustitucion de las etiquetas
                                            SSCommand6.CommandType = CommandType.StoredProcedure
                                            SSCommand6.Parameters.Add("@V_Cat_Pc_Id", SqlDbType.Decimal).Value = 0
                                            SSCommand6.Parameters.Add("@V_Cat_Pc_Nombre", SqlDbType.Decimal).Value = dtsReplace.Rows(c)("CAT_EC_CAMPOREAL")
                                            SSCommand6.Parameters.Add("@V_CAT_PC_CONFIGURACION", SqlDbType.Decimal).Value = DBNull.Value
                                            SSCommand6.Parameters.Add("@V_CAT_PC_PRODUCTO", SqlDbType.Decimal).Value = DtsCarga.Rows(A)("CREDITO")
                                            SSCommand6.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = 7
                                            Dim DtsBuscaValor As DataTable = Consulta_Procedure(SSCommand6, "BuscaValor")
                                            StrRemplazar = StrRemplazar.Replace(dtsReplace.Rows(c)("CAT_EC_DESCRIPCION"), DtsBuscaValor.Rows(0)("VALOR"))
                                            DtsBuscaValor.Clear()
                                            SSCommand6.Parameters.Clear()
                                        Next
                                    End If

                                    SSCommand6.CommandText = "SP_ADD_CAT_PLANTILLAS_CORREO" '---Obtiene el valor que se va a utilizar en la sustitucion de las etiquetas
                                    SSCommand6.CommandType = CommandType.StoredProcedure
                                    SSCommand6.Parameters.Add("@V_Cat_Pc_Id", SqlDbType.Decimal).Value = 0
                                    SSCommand6.Parameters.Add("@V_Cat_Pc_Nombre", SqlDbType.Decimal).Value = "PR_CD_NOMBRE"
                                    SSCommand6.Parameters.Add("@V_CAT_PC_CONFIGURACION", SqlDbType.Decimal).Value = DBNull.Value
                                    SSCommand6.Parameters.Add("@V_CAT_PC_PRODUCTO", SqlDbType.Decimal).Value = DtsCarga.Rows(A)("CREDITO")
                                    SSCommand6.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = 7
                                    Dim DtsBuscaValo As DataTable = Consulta_Procedure(SSCommand6, "BuscaValor")

                                    Dim StrEncabezado As String = "<table align=""right"">" &
                                        "<tr align="" right"">" &
                                            "<td style="" text-align Right();font-weight: bold"">" &
                                                "aliviocapital" &
                                            "</td>" &
                                        "</tr>" &
                                    "</table>" &
                                    "<table>" &
                                        "<tr align=""left"">" &
                                            "<td>" &
                                                "<img src=cid:LogoAlivio>" &
                                            "</td>" &
                                        "</tr>" &
                                        "<tr>" &
                                            "<td>" &
                                                "SR.(A): " & DtsBuscaValo.Rows(0)("NOMBRE") &
                                            "</td>" &
                                        "</tr>" &
                                        "<tr>" &
                                            "<td>" &
                                                "Calle: " & DtsBuscaValo.Rows(0)("CALLE") &
                                            "</td>" &
                                        "</tr>" &
                                        "<tr>" &
                                            "<td>" &
                                                "Colonia: " & DtsBuscaValo.Rows(0)("COLONIA") &
                                            "</td>" &
                                        "</tr>" &
                                        "<tr>" &
                                            "<td>" &
                                                "Ciudad: " & DtsBuscaValo.Rows(0)("ESTADO") &
                                            "</td>" &
                                        "</tr>" &
                                        "<tr>" &
                                            "<td>" &
                                                "Delegación o Municipio: " & DtsBuscaValo.Rows(0)("DELEGACION") &
                                            "</td>" &
                                        "</tr>" &
                                        "<tr>" &
                                            "<td>" &
                                            "Código Postal: " & DtsBuscaValo.Rows(0)("CP") &
                                            "</td>" &
                                        "</tr>" &
                                        "<tr>" &
                                            "<td>" &
                                            "Estado : " & DtsBuscaValo.Rows(0)("ESTADO") &
                                            "</td>" &
                                        "</tr>" &
                                "</table>" &
                                "<table align=""right"">" &
                                    "<tr>" &
                                        "<td style="" font-weight:bold;text-align: Right"" >" &
                                            "Numero de Cuenta:" & DtsCarga.Rows(A)("CREDITO") &
                                        "</td>" &
                                    "</tr>" &
                                    "<tr><td>&nbsp;</td></tr>" &
                                    "<tr><td>&nbsp;</td></tr>" &
                                "</table> <p><br><br><br><br></p>"



                                    Dim htmlview = AlternateView.CreateAlternateViewFromString(StrEncabezado + StrRemplazar, Nothing, "text/html")
                                    Dim StrRutaLogo As String = Server.MapPath(".") & "\Imagenes\ImgLogo_Cl.png"

                                    Dim LRLogoMail As New LinkedResource(StrRutaLogo)

                                    LRLogoMail.ContentId = "LogoAlivio"
                                    htmlview.LinkedResources.Add(LRLogoMail)


                                    'msg.Body = " < HTML >< body > " & StrRemplazar & "</body></HTML>"
                                    msg.AlternateViews.Add(htmlview)
                                    msg.BodyEncoding = System.Text.Encoding.UTF8
                                    msg.IsBodyHtml = True
                                    Dim client As New SmtpClient()
                                    client.Credentials = New System.Net.NetworkCredential(DtsMail.Rows(0).Item("CAT_CONF_USER").ToString, DtsMail.Rows(0).Item("CAT_CONF_PWD").ToString)
                                    client.Port = Val(DtsMail.Rows(0).Item("CAT_CONF_PUERTO").ToString) ' 587
                                    client.Host = DtsMail.Rows(0).Item("CAT_CONF_HOST").ToString

                                    If DtsMail.Rows(0).Item("CAT_CONF_SSL").ToString = 1 Then
                                        client.EnableSsl = True
                                    Else
                                        client.EnableSsl = False
                                    End If
                                    'Dim userState As Object = msg
                                    CorreoPr = DtsCarga

                                    AddHandler client.SendCompleted, AddressOf SmtpClient_OnCompleted

                                    Try
                                        'client.Send(msg)
                                        client.SendAsync(msg, msg)

                                        ENVIADOS = ENVIADOS + 1

                                        Credito = DtsCarga.Rows(A)("CREDITO")
                                        Usuario = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
                                        Comentario = DdlPlantilla.SelectedValue
                                        CORREO = DtsCarga.Rows(A)("CORREO")


                                        Dim SSCommand7 As New SqlCommand("SP_HIST_GEST_MAIL_SMS")
                                        SSCommand7.CommandType = CommandType.StoredProcedure
                                        SSCommand7.Parameters.Add("@V_Hist_Ge_Credito", SqlDbType.Decimal).Value = Credito
                                        SSCommand7.Parameters.Add("@V_Hist_Ge_Usuario", SqlDbType.Decimal).Value = Usuario
                                        SSCommand7.Parameters.Add("@V_Codaccion", SqlDbType.Decimal).Value = "MT"
                                        SSCommand7.Parameters.Add("@V_Hist_Ge_Resultado", SqlDbType.Decimal).Value = "MEDIO ALTERNO"
                                        SSCommand7.Parameters.Add("@V_Codresult", SqlDbType.Decimal).Value = "MT"
                                        SSCommand7.Parameters.Add("@V_HIST_GE_INOUTBOUND", SqlDbType.Decimal).Value = "1"
                                        SSCommand7.Parameters.Add("@V_Hist_Ge_Comentario", SqlDbType.Decimal).Value = Comentario
                                        SSCommand7.Parameters.Add("@V_Hist_Ge_Telefono", SqlDbType.Decimal).Value = CORREO
                                        Dim DtsGestion As DataTable = Consulta_Procedure(SSCommand7, "Gestion")
                                        RemoveHandler client.SendCompleted, AddressOf SmtpClient_OnCompleted
                                    Catch ex As System.Net.Mail.SmtpException
                                        RemoveHandler client.SendCompleted, AddressOf SmtpClient_OnCompleted
                                    End Try
                                Next
                                LblMensaje.Text = "Correos enviados: " & ENVIADOS & " para detalles por favor consulte el historico de gestiones masivas." '& DtsCarga.Rows.Count.ToString & " cargados"
                            Else
                                aviso("Los Creditos A Los Que Intenta Enviar Correos No Los Tiene Asignados O No Existen."
                                )

                            End If
                        End If
                        SubExisteRuta(Ruta & "Historico")
                        If (File.Exists(Ruta & FileCarga)) Then
                            FileCopy(Ruta & FileCarga, Ruta & "Historico/" & singleFile.Name.ToString.Substring(0, Len(singleFile.Name.ToString) - 4) & "_" & DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") & singleFile.Extension)
                            Kill(Ruta & FileCarga)
                        End If

                        If (File.Exists(logCarga)) Then
                            FileCopy(logCarga, Ruta & "Historico/" & singleFile.Name.ToString.Substring(0, Len(singleFile.Name.ToString) - 4) & "_" & DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") & ".log")
                        End If

                        If (File.Exists(badCarga)) Then
                            FileCopy(badCarga, Ruta & "Historico/" & singleFile.Name.ToString.Substring(0, Len(singleFile.Name.ToString) - 4) & "_" & DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") & ".bad")
                        End If
                    End If
                Next
            End If
        End If


        'Catch ex As Exception
        '    SendMail("BtnCargar_Click", ex, "", "", HidenUrs.Value)
        'End Try
    End Sub

    Public Sub SmtpClient_OnCompleted(ByVal sender As Object, ByVal e As AsyncCompletedEventArgs)
        'Get the Original MailMessage object
        Dim mail As MailMessage = CType(e.UserState, MailMessage)
        'Dim DTS As DataSet = CType(e.UserState, DataSet)
        'Dim credito As String = CorreoPr.Rows(0)("CREDITO")
        'write out the subject
        Dim subject As String = mail.To.ToString 'mail.Subject
        'Dim Credito As String =
        'If e.Cancelled Then
        '    Console.WriteLine("Send canceled for mail with subject [{0}].", subject)
        'End If
        If Not (e.Error Is Nothing) Then
            'LblMensaje2.Text = " Correos no enviados: " '& NOENVIADOS
            'Console.WriteLine("Error {1} occurred when sending mail [{0}] ", subject, e.Error.ToString())
            NOENVIADOS = NOENVIADOS + 1
            Dim SSCommand8 As New SqlCommand("SP_HIST_GEST_MAIL_SMS")
            SSCommand8.CommandType = CommandType.StoredProcedure
            SSCommand8.Parameters.Add("@V_Hist_Ge_Credito", SqlDbType.Decimal).Value = Credito
            SSCommand8.Parameters.Add("@V_Hist_Ge_Usuario", SqlDbType.Decimal).Value = Usuario
            SSCommand8.Parameters.Add("@V_Codaccion", SqlDbType.Decimal).Value = "MT"
            SSCommand8.Parameters.Add("@V_Hist_Ge_Resultado", SqlDbType.Decimal).Value = "MEDIO ALTERNO"
            SSCommand8.Parameters.Add("@V_Codresult", SqlDbType.Decimal).Value = "MT"
            SSCommand8.Parameters.Add("@V_HIST_GE_INOUTBOUND", SqlDbType.Decimal).Value = "0"
            SSCommand8.Parameters.Add("@V_Hist_Ge_Comentario", SqlDbType.Decimal).Value = Comentario
            SSCommand8.Parameters.Add("@V_Hist_Ge_Telefono", SqlDbType.Decimal).Value = CORREO
            Dim DtsGestion As DataTable = Consulta_Procedure(SSCommand8, "Gestion")

        Else
            'Console.WriteLine("Message [{0}] sent.", subject)
        End If
    End Sub 'SmtpClient_OnCompleted

    'Protected Sub AsyncFileUpload1_UploadedComplete(ByVal sender As Object, ByVal e As AjaxControlToolkit.AsyncFileUploadEventArgs) Handles AsyncFileUpload1.UploadedComplete
    '    Try
    '        SubExisteRuta(Ruta)
    '        For Each s In System.IO.Directory.GetFiles(Ruta)
    '            System.IO.File.Delete(s)
    '        Next
    '        Dim nom As String = e.FileName.Substring(e.FileName.LastIndexOf("\") + 1)
    '        Dim ruta_archivo As String = Ruta + nom
    '        AsyncFileUpload1.SaveAs(ruta_archivo)
    '        Dim archivo As String = AsyncFileUpload1.FileName.ToString
    '    Catch ex As Exception
    '    End Try
    'End Sub
    'Protected Sub FileUploadComplete(ByVal sender As Object, ByVal e As EventArgs)

    'AsyncFileUpload1.SaveAs(Server.MapPath("Uploads/") + filename)

    '    Try
    '        SubExisteRuta(Ruta)
    '        For Each s In System.IO.Directory.GetFiles(Ruta)
    '            System.IO.File.Delete(s)
    '        Next
    '        Dim filename As String = Path.GetFileName(AsyncFileUpload1.FileName)
    '        'Dim nom As String = e.FileName.Substring(e.FileName.LastIndexOf("\") + 1)
    '        Dim ruta_archivo As String = Ruta + filename
    '        'AsyncFileUpload1.SaveAs(ruta_archivo)
    '        AsyncFileUpload1.SaveAs(ruta_archivo)
    '        Dim archivo As String = AsyncFileUpload1.FileName.ToString
    '    Catch ex As Exception
    '    End Try
    'End Sub
    Private Sub RadAsyncUpload1_FileUploaded(sender As Object, e As FileUploadedEventArgs) Handles RadAsyncUpload1.FileUploaded
        Try
            SubExisteRuta(Ruta)
            For Each s In System.IO.Directory.GetFiles(Ruta)
                System.IO.File.Delete(s)
            Next
            Dim nom As String = e.File.FileName
            Dim ruta_archivo As String = Ruta + nom
            e.File.SaveAs(ruta_archivo)
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub aviso(ByVal MSJ As String)
        RadAviso.RadAlert(MSJ, 400, 150, "Aviso", Nothing)
    End Sub

End Class
