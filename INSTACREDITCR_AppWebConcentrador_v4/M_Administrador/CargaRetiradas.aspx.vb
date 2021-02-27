Imports System.Data.SqlClient
Imports System.Data
Imports System.Diagnostics
Imports System.IO
Imports Db
Imports Funciones
Imports Telerik.Web.UI

Partial Class CargaRetiradas
    Inherits System.Web.UI.Page

    Dim ERRORS As String = "100"
    Dim SKIP As String = "1"
    Dim ctlCarga As String
    Dim logCarga As String
    Dim badCarga As String
    Dim Ruta As String = StrRuta() & "Retiradas\"

    Protected Sub BtnCargar_Click(sender As Object, e As EventArgs) Handles BtnCargar.Click
        Try
            Dim Directory As New IO.DirectoryInfo(Ruta)
            Dim allFiles As IO.FileInfo() = Directory.GetFiles.Where(Function(fi) fi.Extension = ".csv" OrElse fi.Extension = ".txt").ToArray
            If allFiles.Length = 0 Then
                aviso("Archivo No Seleccionado o No Valido ")
            ElseIf DdlTipo.SelectedValue = "Seleccione" Then
                aviso("Seleccione un Tipo ")
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
                        Dim TABLA As String = "TMP_RETIRAR_" & (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_AGENCIA
                        ctlCarga = Ruta & "CTL_ASIG_" & TABLA & "_" & Now.ToShortDateString.Replace("/", "") & "" & ".ctl"
                        logCarga = Ruta & "LOG_ASIG" & TABLA & "_" & Now.ToShortDateString.Replace("/", "") & "" & ".log"
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
                        sw.WriteLine("FIELDS TERMINATED BY ',' optionally enclosed by '""'")
                        sw.WriteLine("TRAILING NULLCOLS")
                        sw.WriteLine("(")
                        If DdlTipo.SelectedValue = "AZUL" Then
                            'CUENTA	FECHA_RETIRO	FECHA_ASIGNACION	MOTIVO_RETIRO	AGENCIA	LINEAS	BUCKET_AGENCIA
                            sw.WriteLine("TMP_PR_MC_CREDITO,")
                            sw.WriteLine("TMP_DTE_RETIRO,")
                            sw.WriteLine("TMP_DTE_ASIGNACION,")
                            sw.WriteLine("TMP_MOTIVO_RETIRO,")
                            sw.WriteLine("TMP_AGENCIA,")
                            sw.WriteLine("TMP_LINEAS,")
                            sw.WriteLine("TMP_BUCKET_AGENCIA")
                        ElseIf DdlTipo.SelectedValue = "NARANJA" Then
                            'CUENTA  FECHA_RETIRO	FECHA_ASIGNACION 	MOTIVO_RETIRO	SEGMENTO	AGENCIA	LINEAS	CAPA
                            sw.WriteLine("TMP_PR_MC_CREDITO,")
                            sw.WriteLine("TMP_DTE_RETIRO,")
                            sw.WriteLine("TMP_DTE_ASIGNACION,")
                            sw.WriteLine("TMP_MOTIVO_RETIRO,")
                            sw.WriteLine("TMP_SEGMENTO,")
                            sw.WriteLine("TMP_AGENCIA,")
                            sw.WriteLine("TMP_LINEAS,")
                            sw.WriteLine("TMP_CAPA")
                        ElseIf DdlTipo.SelectedValue = "ROJO" Then
                            'CUENTA	FECHA_RETIRO	FECHA_ASIGNACION	MOTIVO_RETIRO	AGENCIA	LINEAS	BK
                            sw.WriteLine("TMP_PR_MC_CREDITO,")
                            sw.WriteLine("TMP_DTE_RETIRO,")
                            sw.WriteLine("TMP_DTE_ASIGNACION,")
                            sw.WriteLine("TMP_MOTIVO_RETIRO,")
                            sw.WriteLine("TMP_AGENCIA,")
                            sw.WriteLine("TMP_LINEAS,")
                            sw.WriteLine("TMP_BUCKET_AGENCIA")
                        End If
                        sw.WriteLine(")")
                        sw.Close()
                        fs.Close()
                        Dim SSCommand As New SqlCommand("SP_CARGA_TMP_ACTSALDOS")
                        SSCommand.CommandType = CommandType.StoredProcedure
                        SSCommand.Parameters.Add("@V_ARCHIVO", SqlDbType.NVarChar).Value = FileCarga
                        SSCommand.Parameters.Add("@V_ERROR", SqlDbType.NVarChar).Value = badCarga
                        SSCommand.Parameters.Add("@V_TABLA", SqlDbType.NVarChar).Value = TABLA
                        Ejecuta_Procedure(SSCommand)
                        Dim num As Integer = 0
                        If (num <> 0) Then
                            GvCargaRetiradas.DataSource = Nothing
                            GvCargaRetiradas.DataBind()

                            If (File.Exists(badCarga)) Then
                                Dim fic As New IO.StreamReader(badCarga, System.Text.Encoding.UTF8)
                                Dim linea As String = fic.ReadLine
                                fic.Close()
                                LblMensaje.Text = "Error en la cadena siguiente " & linea
                            Else
                                LblMensaje.Text = "Error  " & num
                            End If
                        Else
                            LblMensaje.Text = ""
                            Dim SSCommand2 As New SqlCommand("SP_CARGAR_RETIRADAS")
                            SSCommand2.CommandType = CommandType.StoredProcedure
                            SSCommand2.Parameters.Add("@V_AGENCIA", SqlDbType.NVarChar).Value = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_AGENCIA
                            SSCommand2.Parameters.Add("@V_NUM_AGENCIA", SqlDbType.NVarChar).Value = (CType(Session("USUARIOADMIN"), USUARIO)).cat_Lo_Num_Agencia
                            SSCommand2.Parameters.Add("@V_Usuario", SqlDbType.NVarChar).Value = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
                            Dim DtsCarga As DataTable = Consulta_Procedure(SSCommand2, "SP_CARGAR_RETIRADAS")
                            GvCargaRetiradas.DataSource = DtsCarga
                            GvCargaRetiradas.DataBind()
                            LblMensaje.Text = "Fin De La Carga"

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
            aviso(ex.Message)
            SendMail("BtnCargar_Click", ex, "", "", HidenUrs.Value)
        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("USUARIOADMIN") IsNot Nothing Then
            HidenUrs.Value = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
            If CType(Session("USUARIOADMIN"), USUARIO).CAT_LO_MADMINISTRADOR.ToString.Substring(18, 1) = 0 Then
                OffLine(HidenUrs.Value)
                Session.Clear()
                Session.Abandon()
                Response.Redirect("~/SesionExpirada.aspx")
            Else

            End If
        Else
            OffLine(HidenUrs.Value)
            AUDITORIA(HidenUrs.Value, "Administrador", "CargaRetiradas", " ", "Page_Load", "", Request.ServerVariables("REMOTE_ADDR"), Request.ServerVariables("REMOTE_HOST"))
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/SesionExpirada.aspx")
        End If
    End Sub


    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Administrador", "CargaRetiradas.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub

    'Protected Sub AsyncFileUpload1_UploadedComplete(ByVal sender As Object, ByVal e As AjaxControlToolkit.AsyncFileUploadEventArgs) Handles AsyncFileUpload1.UploadedComplete
    '    Try
    '        SubExisteRuta(Ruta)
    '        For Each s In System.IO.Directory.GetFiles(Ruta)
    '            System.IO.File.Delete(s)
    '        Next
    '        Dim nom As String = e.FileName.Substring(e.FileName.LastIndexOf("\") + 1)
    '        Dim ruta_archivo As String = Ruta + nom
    '        Me.AsyncFileUpload1.SaveAs(ruta_archivo)
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

    Private Sub DdlTipo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlTipo.SelectedIndexChanged
        If DdlTipo.SelectedValue = "NARANJA" Then
            lbllayout.Text = "CUENTA, FECHA_RETIRO, FECHA_ASIGNACION, MOTIVO_RETIRO, SEGMENTO, AGENCIA, LINEAS,CAPA"
        ElseIf DdlTipo.SelectedValue = "AZUL" Then
            lbllayout.Text = "CUENTA, FECHA_RETIRO, FECHA_ASIGNACION, MOTIVO_RETIRO, AGENCIA, LINEAS, BUCKET_AGENCIA"
        ElseIf DdlTipo.SelectedValue = "ROJO" Then
            lbllayout.Text = "CUENTA, FECHA_RETIRO, FECHA_ASIGNACION, MOTIVO_RETIRO, AGENCIA, LINEAS, BK"
        Else
            lbllayout.Text = ""
        End If
    End Sub
    Protected Sub aviso(ByVal MSJ As String)
        RadAviso.RadAlert(MSJ, 400, 150, "Aviso", Nothing)
    End Sub
End Class
