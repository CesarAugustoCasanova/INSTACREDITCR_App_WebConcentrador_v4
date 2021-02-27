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

Partial Class CargaActSaldos
    Inherits System.Web.UI.Page
    Dim TABLA As String = "TMP_ACT_SALDOS"
    Dim ERRORS As String = "1"
    Dim SKIP As String = "1"
    Dim ctlCarga As String
    Dim logCarga As String
    Dim badCarga As String
    Dim Ruta As String = Db.StrRuta() & "ACT_SALDOS\"


    Protected Sub BtnCargar_Click(sender As Object, e As EventArgs) Handles BtnCargar.Click
        Try
            Db.SubExisteRuta(Ruta)

            Dim Directory As New IO.DirectoryInfo(Ruta)
            Dim allFiles As IO.FileInfo() = Directory.GetFiles.Where(Function(fi) fi.Extension = ".csv" OrElse fi.Extension = ".txt").ToArray
            If allFiles.Length = 0 Then
                aviso("Archivo No Seleccionado o No Valido "
                )
            ElseIf DdlProducto.SelectedValue = "Seleccione" Then
                aviso("Seleccione un Tipo "
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
                        TABLA = TABLA
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

                        Dim SSCommandT As New SqlCommand("SP_VALIDA_TABLA")
                        SSCommandT.CommandType = CommandType.StoredProcedure
                        SSCommandT.Parameters.Add("@V_TABLA", SqlDbType.NVarChar).Value = FileCarga
                        SSCommandT.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 4
                        SSCommandT.Parameters.Add("@V_Principal", SqlDbType.NVarChar).Value = 0
                        Ejecuta_Procedure(SSCommandT)

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
                        sw.WriteLine("TMP_CREDITO,")
                        sw.WriteLine("TMP_AGENCIA,")
                        sw.WriteLine("TMP_CARTERA,")
                        sw.WriteLine("TMP_SALDO_INICIAL_CALCULO_FC,")
                        sw.WriteLine("TMP_TOTAL_HOY,")
                        sw.WriteLine("TMP_PAGO_CLIENTE,")
                        sw.WriteLine("TMP_BONIFICACION_TOTAL,")
                        sw.WriteLine("TMP_PORCENTAJE,")
                        sw.WriteLine("TMP_SALDO_SERV,")
                        sw.WriteLine("TMP_SALDO_CURRENT,")
                        If DdlProducto.SelectedValue = "ROJO" Then
                            sw.WriteLine("TMP_DTE_VENC_FAC,")
                        End If
                        sw.WriteLine("TMP_SALDO_VENCIDO")
                        sw.WriteLine(")")
                        sw.Close()
                        fs.Close()
                        ' Ejecuta("TRUNCATE TABLE " & TABLA)

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
                            Dim SSCommand2 As New SqlCommand("SP_INGRESO")
                            SSCommand2.CommandType = CommandType.StoredProcedure
                            SSCommand2.Parameters.Add("@V_USUARIO", SqlDbType.NVarChar).Value = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
                            SSCommand2.Parameters.Add("@V_PRODUCTO", SqlDbType.NVarChar).Value = DdlProducto.SelectedValue

                            DtsCarga = Consulta_Procedure(SSCommand, "SP_CARGA_ACT_SALDOS")

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
        Catch ex As Exception
            aviso(ex.Message
            )
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
        EnviarCorreo("Administrador", "CargaActSaldos.aspx", evento, ex, Cuenta, Captura, usr)
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

    Protected Sub aviso(ByVal MSJ As String)
        RadAviso.RadAlert(MSJ, 400, 150, "Aviso", Nothing)
    End Sub

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

    Private Sub DdlProducto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlProducto.SelectedIndexChanged
        If DdlProducto.SelectedValue = "NARANJA" Then
            LblLayOut.Text = ""
        ElseIf DdlProducto.SelectedValue = "AZUL" Then
            LblLayOut.Text = "CUENTA, AGENCIA, CARTERA, SALDO INICIAL CALCULO FC, TOTAL_HOY, PAGO_CLIENTE, BONIFICACION_TOTAL, %, SALDO_SERV, SALDO_CURRENT, FECHA_VENC_FAC, SALDO_VENCIDO"
        ElseIf DdlProducto.SelectedValue = "ROJO" Then
            LblLayOut.Text = ""
        Else
            LblLayOut.Text = ""
        End If
    End Sub

End Class

