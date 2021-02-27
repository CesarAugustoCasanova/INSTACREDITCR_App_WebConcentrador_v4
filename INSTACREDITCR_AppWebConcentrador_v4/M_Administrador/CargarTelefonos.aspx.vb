Imports System.Data.SqlClient
Imports System.Data
Imports System.Diagnostics
Imports System.IO
Imports Db
Imports Funciones
Imports System.Net.Mail
Imports Microsoft.Exchange.WebServices.Data
Imports Microsoft.Exchange.WebServices.Autodiscover
Imports Telerik.Web.UI

Partial Class CargarTelefonos
    Inherits System.Web.UI.Page

    Dim TABLA As String = "TMP_TELEFONOS"
    Dim ERRORS As String = "1"
    Dim SKIP As String = "1"
    Dim ctlCarga As String
    Dim logCarga As String
    Dim badCarga As String
    Dim Ruta As String = Db.StrRuta() & "TELEFONOS\"


    Protected Sub BtnCargar_Click(sender As Object, e As EventArgs) Handles BtnCargar.Click
        Try
            Db.SubExisteRuta(Ruta)
            Dim V_CuantosTels As String = "1"
            Dim Directory As New IO.DirectoryInfo(Ruta)
            Dim allFiles As IO.FileInfo() = Directory.GetFiles.Where(Function(fi) fi.Extension = ".csv" OrElse fi.Extension = ".txt").ToArray
            If allFiles.Length = 0 Then
                aviso("Archivo No Seleccionado o No Valido ")
            ElseIf DdlTipo.SelectedValue = "Seleccione" Then
                aviso("Selecciona Un Tipo")
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
                        'TABLA = TABLA & "_" & DdlProducto.SelectedValue
                        ctlCarga = Ruta & "CTL_" & TABLA & "_" & Now.ToShortDateString.Replace("/", "") & "" & ".ctl"
                        logCarga = Ruta & "LOG_" & TABLA & "_" & Now.ToShortDateString.Replace("/", "") & "" & ".log"
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

                        If DdlTipo.SelectedValue = "Quejas" Then
                            sw.WriteLine("TMP_CUENTA CHAR(25),")
                            sw.WriteLine("TMP_TELEFONO CHAR(20),")
                            sw.WriteLine("TMP_AGENCIA CHAR(20)")
                            V_CuantosTels = "1"
                        ElseIf DdlTipo.SelectedValue = "Adicionales" Then

                            sw.WriteLine("TMP_CUENTA CHAR(25),")
                            sw.WriteLine("TMP_TELEFONO CHAR(20),")
                            sw.WriteLine("TMP_TIPO_TELEFONO CHAR(20),")
                            sw.WriteLine("TMP_AGENCIA CHAR(20)")
                            V_CuantosTels = "1"
                        ElseIf DdlTipo.SelectedValue = "AOC 4 Colums" Then

                            sw.WriteLine("TMP_CUENTA CHAR(25),")
                            sw.WriteLine("TMP_TELEFONO CHAR(20),")
                            sw.WriteLine("null1 filler,")
                            sw.WriteLine("TMP_AGENCIA CHAR(20)")
                            V_CuantosTels = "1"

                        ElseIf DdlTipo.SelectedValue = "BTTC 7 NARANJA" Then
                            sw.WriteLine("TMP_CUENTA CHAR(25),")
                            sw.WriteLine("null1 filler,")
                            sw.WriteLine("null2 filler,")
                            sw.WriteLine("TMP_TELEFONO CHAR(20),")
                            sw.WriteLine("TMP_TELEFONO2 CHAR(20),")
                            sw.WriteLine("TMP_TELEFONO3 CHAR(20),")
                            sw.WriteLine("TMP_AGENCIA CHAR(20)")
                            V_CuantosTels = "3"
                        ElseIf DdlTipo.SelectedValue = "BTTC 8 AZUL" Then
                            sw.WriteLine("TMP_CUENTA CHAR(25),")
                            sw.WriteLine("null1 filler,")
                            sw.WriteLine("null2 filler,")
                            sw.WriteLine("null3 filler,")
                            sw.WriteLine("TMP_TELEFONO CHAR(20),")
                            sw.WriteLine("null4 filler,")
                            sw.WriteLine("null5 filler,")
                            sw.WriteLine("TMP_AGENCIA CHAR(20)")
                            V_CuantosTels = "1"
                        ElseIf DdlTipo.SelectedValue = "BTTC 8 ROJO" Then
                            sw.WriteLine("TMP_CUENTA CHAR(25),")
                            sw.WriteLine("null1 filler,")
                            sw.WriteLine("null2 filler,")
                            sw.WriteLine("null3 filler,")
                            sw.WriteLine("TMP_TELEFONO CHAR(20),")
                            sw.WriteLine("TMP_TELEFONO2 CHAR(20),")
                            sw.WriteLine("null4 filler,")
                            sw.WriteLine("TMP_AGENCIA CHAR(20)")
                            V_CuantosTels = "2"
                        ElseIf DdlTipo.SelectedValue = "NARANJA CON LEGADOS (5 Columns)" Then
                            sw.WriteLine("TMP_CUENTA CHAR(25),")
                            sw.WriteLine("TMP_TELEFONO CHAR(20),")
                            sw.WriteLine("bucket filler,")
                            sw.WriteLine("campana filler,")
                            sw.WriteLine("fecja_gestion filler")
                            V_CuantosTels = "1"
                        End If


                        sw.WriteLine(")")
                        sw.Close()
                        fs.Close()
                        Ejecuta("TRUNCATE TABLE " & TABLA)

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
                            Else
                                LblMensaje.Text = "Codigo de Error " & num
                            End If
                        Else
                            LblMensaje.Text = ""
                            Dim DtsCarga As DataTable
                            Dim SSCommand2 As New SqlCommand("SP_CARGA_TELS")
                            SSCommand2.CommandType = CommandType.StoredProcedure
                            SSCommand2.Parameters.Add("@V_ARCHIVO", SqlDbType.NVarChar).Value = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_AGENCIA
                            SSCommand2.Parameters.Add("@V_ERROR", SqlDbType.NVarChar).Value = (CType(Session("USUARIOADMIN"), USUARIO)).cat_Lo_Num_Agencia
                            SSCommand2.Parameters.Add("@V_TABLA", SqlDbType.NVarChar).Value = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
                            SSCommand2.Parameters.Add("@V_ERROR", SqlDbType.NVarChar).Value = V_CuantosTels
                            SSCommand2.Parameters.Add("@V_TABLA", SqlDbType.NVarChar).Value = DdlTipo.SelectedValue

                            DtsCarga = Consulta_Procedure(SSCommand2, "SP_CARGA_TELS")

                            GvCargaAsignacion.DataSource = DtsCarga
                            GvCargaAsignacion.DataBind()
                            LblMensaje.Text = "Carga Completada"

                            SubExisteRuta(Ruta & "Historico")
                            FileCopy(Ruta & FileCarga, Ruta & "Historico/" & singleFile.Name.ToString.Substring(0, Len(singleFile.Name.ToString) - 4) & "_" & DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") & singleFile.Extension)
                            Kill(Ruta & FileCarga)

                            If (File.Exists(logCarga)) Then
                                FileCopy(logCarga, Ruta & "Historico/" & singleFile.Name.ToString.Substring(0, Len(singleFile.Name.ToString) - 4) & "_" & DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") & ".log")
                            End If

                            If (File.Exists(badCarga)) Then
                                FileCopy(badCarga, Ruta & "Historico/" & singleFile.Name.ToString.Substring(0, Len(singleFile.Name.ToString) - 4) & "_" & DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") & ".bad")
                            End If
                        End If


                    End If
                Next
            End If
            DdlTipo.SelectedValue = "Seleccione"
        Catch ex As Exception
            aviso(ex.Message)
            GvCargaAsignacion.DataSource = Nothing
            GvCargaAsignacion.DataBind()
        End Try

        'Catch ex As Exception
        '    SendMail("BtnCargar_Click", ex, "", "", HidenUrs.Value)
        'End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim Usr As String = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
        Catch ex As Exception
            OffLine(HidenUrs.Value)
            AUDITORIA(HidenUrs.Value, "Administrador", "CargaCartera", " ", "Page_Load", "", Request.ServerVariables("REMOTE_ADDR"), Request.ServerVariables("REMOTE_HOST"))
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/SesionExpirada.aspx")
        End Try
        Try
            If Not IsPostBack Then
                HidenUrs.Value = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
                If CType(Session("USUARIOADMIN"), USUARIO).CAT_LO_MADMINISTRADOR.ToString.Substring(23, 1) = 0 Then
                    OffLine(HidenUrs.Value)
                    Session.Clear()
                    Session.Abandon()
                    Response.Redirect("~/SesionExpirada.aspx")
                End If
                LLENAR_DROP(16, DdlDelimitador, "Delimitador", "Delimitador")
                'LLENAR_DROP(9, DdlProducto, "Productos", "Productos")
            End If
        Catch ex As Exception
            SendMail("Page_Load", ex, "", "", HidenUrs.Value)
        End Try
    End Sub

    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Administrador", "CargaTelefonos.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub
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
    'Protected Sub AsyncFileUpload1_UploadedComplete(ByVal sender As Object, ByVal e As AjaxControlToolkit.AsyncFileUploadEventArgs) Handles AsyncFileUpload1.UploadedComplete
    '    GvCargaAsignacion.DataSource = Nothing
    '    GvCargaAsignacion.DataBind()

    '    Try
    '        SubExisteRuta(Ruta)
    '        For Each s In System.IO.Directory.GetFiles(Ruta)
    '            System.IO.File.Delete(s)
    '        Next
    '        Dim nom As String = e.FileName.Substring(e.FileName.LastIndexOf("\") + 1)
    '        Dim ruta_archivo As String = Ruta + nom
    '        Me.AsyncFileUpload1.SaveAs(ruta_archivo)
    '        LblLayOut.Text = ""
    '        GvCargaAsignacion.DataSource = Nothing
    '        GvCargaAsignacion.DataBind()

    '    Catch ex As Exception
    '    End Try
    'End Sub

    Sub Send_MAil(ByRef Subject As String, ByVal Titulo As String, ByVal V_Msg As DataSet)

        Dim v_Html As String = "<table>" &
                       "<tr>" &
                       "<td colspan=""2"" style=""font-weight: bold; text-align: center;background-color: #3366CC; color: #FFFFFF;"">ATT - Carga de Cartera</td>" &
                       "</tr>" &
                       "<tr>" &
                       "<td>Créditos cargados</td>" &
                       "<td>" & V_Msg.Tables(0).Rows(0)(0).ToString & "</td>" &
                       "</tr>" &
                       "<tr>" &
                       "<td>Creditos actualizados</td>" &
                       "<td>" & V_Msg.Tables(0).Rows(0)(1).ToString & "</td>" &
                       "</tr>" &
                       "<tr>" &
                       "<td>Creditos no existentes</td>" &
                       "<td>" & V_Msg.Tables(0).Rows(0)(2).ToString & "</td>" &
                       "</tr>" &
                       "</table>"
        Dim htmlView = AlternateView.CreateAlternateViewFromString(v_Html, Nothing, "text/html")

        Dim msg As New System.Net.Mail.MailMessage()
        'msg.[To].Add("uriel.lino@finem.com.mx")
        'msg.[To].Add("victor.ramirez@finem.com.mx")
        'msg.[To].Add("gabriel.juarez@finem.com.mx")
        'msg.[To].Add("soporte@mccollect.com.mx")
        msg.From = New MailAddress("soporte@mccollect.com.mx", "Soporte MC Collect", System.Text.Encoding.UTF8)
        msg.Subject = Subject
        msg.SubjectEncoding = System.Text.Encoding.UTF8
        msg.AlternateViews.Add(htmlView)
        msg.BodyEncoding = System.Text.Encoding.UTF8
        msg.IsBodyHtml = True
        Dim client As New SmtpClient()
        client.Credentials = New System.Net.NetworkCredential("soporte@mccollect.com.mx", "Adalesperra2")
        client.Port = "587"
        client.Host = "smtp.gmail.com"
        client.EnableSsl = True
        Try
            client.Send(msg)
        Catch ex As System.Net.Mail.SmtpException
            'Ejecuta("insert into hist_errores (ERRORS) values('" & ex.ToString & "')")
        End Try
    End Sub



    'Private Sub BtnDetalleCartera_Click(sender As Object, e As EventArgs) Handles BtnDetalleCartera.Click
    '    If BtnDetalleCartera.Text = "Ver Detalle" Then
    '        PnlDetalleCartera.Visible = True
    '        BtnDetalleCartera.Text = "Ocultar Detalle"
    '        GvDetalleCartera.DataSource = Class_VariosQueries.VARIOS_QUERIES((CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO, "5")
    '        GvDetalleCartera.DataBind()
    '    Else
    '        PnlDetalleCartera.Visible = False
    '        BtnDetalleCartera.Text = "Ver Detalle"
    '        GvDetalleCartera.DataSource = Nothing
    '        GvDetalleCartera.DataBind()
    '    End If
    'End Sub


    Private Sub DdlTipo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlTipo.SelectedIndexChanged
        If DdlTipo.SelectedValue = "Adicionales" Then
            LblLayOut.Text = "CUENTA, Telefono, Tipo telefono, Agencia"
        ElseIf DdlTipo.SelectedValue = "Quejas" Then
            LblLayOut.Text = "CUENTA, Telefono, Agencia"

        ElseIf DdlTipo.SelectedValue = "AOC 4 Colums" Then
            LblLayOut.Text = "CUENTA, Telefono, ID, Agencia"

        ElseIf DdlTipo.SelectedValue = "BTTC 7 NARANJA" Then
            LblLayOut.Text = "CUENTA,Hora red, Capa, Tel 1, Tel 2, Tel 3, Agencia"
        ElseIf DdlTipo.SelectedValue = "BTTC 8 AZUL" Then
            LblLayOut.Text = "CUENTALlamadas, Escuchadas, No Contacto, Tel Contacto, Hora Ord, BTTC, Agencia"
        ElseIf DdlTipo.SelectedValue = "BTTC 8 ROJO" Then
            LblLayOut.Text = "CUENTA,Llamadas, Escuchadas, No Contacto, Tel 1, Tel 2, BTTC, Agencia"
        ElseIf DdlTipo.SelectedValue = "NARANJA CON LEGADOS (5 Columns)" Then
            LblLayOut.Text = "CUENTA, DN_ENVIO, BUCKET, CAMPAÑA, FECHA_GESTION"
        End If
    End Sub
    Protected Sub aviso(ByVal MSJ As String)
        RadAviso.RadAlert(MSJ, 400, 150, "Aviso", Nothing)
    End Sub
End Class
