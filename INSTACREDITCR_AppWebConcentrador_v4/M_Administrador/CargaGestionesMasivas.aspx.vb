Imports System.Data.SqlClient
Imports System.Data
Imports System.Diagnostics
Imports System.IO
Imports Db
Imports Funciones
Imports Telerik.Web.UI

Partial Class CargaGestionesMasivas
    Inherits System.Web.UI.Page

    Dim ERRORS As String = "100"
    Dim SKIP As String = "1"
    Dim ctlCarga As String
    Dim logCarga As String
    Dim badCarga As String
    Dim TABLA As String
    Dim Ruta As String = StrRuta() & "Masivas\"

    Protected Sub BtnCargar_Click(sender As Object, e As EventArgs) Handles BtnCargar.Click
        Try
            Dim Directory As New IO.DirectoryInfo(Ruta)
            Dim allFiles As IO.FileInfo() = Directory.GetFiles.Where(Function(fi) fi.Extension = ".csv" OrElse fi.Extension = ".txt").ToArray
            If allFiles.Length = 0 Then
                aviso("Archivo No Seleccionado o No Valido ")
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
                        If DdlTipo.SelectedValue = "Gestiones" Then
                            TABLA = "TMP_MASIVAS_" & (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_AGENCIA
                            ctlCarga = Ruta & "CTL_GESTIONES_" & TABLA & "_" & Now.ToShortDateString.Replace("/", "") & "" & ".ctl"
                            logCarga = Ruta & "LOG_GESTIONES" & TABLA & "_" & Now.ToShortDateString.Replace("/", "") & "" & ".log"
                            badCarga = "C:\Users\ccasanova\Documents\clientes\TUIIO\errorescargas.csv"
                        Else
                            TABLA = "TMP_HIST_VISITAS_" & (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_AGENCIA
                            ctlCarga = Ruta & "CTL_VISITAS_" & TABLA & "_" & Now.ToShortDateString.Replace("/", "") & "" & ".ctl"
                            logCarga = Ruta & "LOG_VISITAS_" & TABLA & "_" & Now.ToShortDateString.Replace("/", "") & "" & ".log"
                            badCarga = "C:\Users\ccasanova\Documents\clientes\TUIIO\errorescargas.csv"
                        End If


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
                        If DdlTipo.SelectedValue = "Gestiones" Then
                            sw.WriteLine("TMP_HIST_GE_CREDITO char(25),")
                            sw.WriteLine("TMP_HIST_GE_USUARIO char(25),")
                            sw.WriteLine("TMP_HIST_GE_CODIGO char(4),")
                            sw.WriteLine("TMP_HIST_GE_COMENTARIO char(1090),")
                            sw.WriteLine("TMP_HIST_GE_DTEACTIVIDAD char(25)")
                        Else
                            sw.WriteLine("TMP_HIST_VI_CREDITO CHAR(25),")
                            sw.WriteLine("TMP_HIST_VI_VISITADOR CHAR(25),")
                            sw.WriteLine("TMP_HIST_VI_CAPTURISTA CHAR(25),")
                            sw.WriteLine("TMP_HIST_VI_DTEVISITA CHAR(19),")
                            sw.WriteLine("TMP_HIST_VI_DTECAPTURA CHAR(19),")
                            sw.WriteLine("TMP_HIST_VI_CODIGO CHAR(12),")
                            sw.WriteLine("TMP_HIST_VI_COMENTARIO CHAR(500),")
                            sw.WriteLine("TMP_HIST_VI_PARENTESCO CHAR(50),")
                            sw.WriteLine("TMP_HIST_VI_NOMBREC CHAR(50),")
                            sw.WriteLine("TMP_HIST_VI_TIPODOMICILIO CHAR(20),")
                            sw.WriteLine("TMP_HIST_VI_NIVELSOCIO CHAR(20),")
                            sw.WriteLine("TMP_HIST_VI_NIVELES CHAR(2),")
                            sw.WriteLine("TMP_HIST_VI_CARACTERISTICAS CHAR(20),")
                            sw.WriteLine("TMP_HIST_VI_COLORF CHAR(50),")
                            sw.WriteLine("TMP_HIST_VI_COLORP CHAR(50),")
                            sw.WriteLine("TMP_HIST_VI_HCONTACTO CHAR(11),")
                            sw.WriteLine("TMP_HIST_VI_DCONTACTO CHAR(7),")
                            sw.WriteLine("TMP_HIST_VI_REFERENCIA CHAR(500),")
                            sw.WriteLine("TMP_HIST_VI_ENTRECALLE1 CHAR(50),")
                            sw.WriteLine("TMP_HIST_VI_ENTRECALLE2 CHAR(50)")
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
                            If (File.Exists(badCarga)) Then
                                Dim fic As New IO.StreamReader(badCarga, System.Text.Encoding.UTF8)
                                Dim linea As String = fic.ReadLine
                                fic.Close()
                                LblMensaje.Text = "Error En La Cadena Siguiente " & linea
                            End If
                        Else
                            LblMensaje.Text = ""
                            Dim SSCommand2 As New SqlCommand()
                            Dim DtsCarga As DataTable
                            If DdlTipo.SelectedValue = "Gestiones" Then
                                SSCommand2.CommandText = "SP_CARGAR_GESTIONES_MASIVAS"
                                SSCommand2.CommandType = CommandType.StoredProcedure
                                SSCommand2.Parameters.Add("@V_AGENCIA", SqlDbType.NVarChar).Value = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_AGENCIA
                                SSCommand2.Parameters.Add("@V_NOAGENCIA", SqlDbType.NVarChar).Value = (CType(Session("USUARIOADMIN"), USUARIO)).cat_Lo_Num_Agencia
                                SSCommand2.Parameters.Add("@V_USUARIO", SqlDbType.NVarChar).Value = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO

                                DtsCarga = Consulta_Procedure(SSCommand2, "SP_CARGAR_GESTIONES_MASIVAS")
                            Else
                                SSCommand2.CommandText = "SP_CARGAR_VISITAS_MASIVAS"
                                SSCommand2.CommandType = CommandType.StoredProcedure
                                SSCommand2.Parameters.Add("@V_AGENCIA", SqlDbType.NVarChar).Value = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_AGENCIA
                                SSCommand2.Parameters.Add("@V_NOAGENCIA", SqlDbType.NVarChar).Value = (CType(Session("USUARIOADMIN"), USUARIO)).cat_Lo_Num_Agencia
                                SSCommand2.Parameters.Add("@V_USUARIO", SqlDbType.NVarChar).Value = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
                                DtsCarga = Consulta_Procedure(SSCommand2, "SP_CARGAR_VISITAS_MASIVAS")
                            End If
                            GvCargaAsignacion.DataSource = DtsCarga
                            GvCargaAsignacion.DataBind()
                            LblMensaje.Text = "Carga Completada"


                        End If
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
                Next
            End If
        Catch ex As Exception
            aviso(ex.Message)
        End Try
        'Catch ex As Exception
        '    SendMail("BtnCargar_Click", ex, "", "", HidenUrs.Value)
        'End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("USUARIOADMIN") IsNot Nothing Then
            HidenUrs.Value = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
            If Not IsPostBack Then
                HidenUrs.Value = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
                'If CType(Session("USUARIOADMIN"), USUARIO).CAT_LO_MADMINISTRADOR.ToString.Substring(19, 1) = 0 Then
                '    OffLine(HidenUrs.Value)
                '    Session.Clear()
                '    Session.Abandon()
                '    Response.Redirect("~/SesionExpirada.aspx")
                'Else
                Try
                        LLENAR_DROP(16, DdlDelimitador, "Delimitador", "Delimitador")
                    Catch ex As Exception
                        SendMail("Page_Load", ex, "", "", HidenUrs.Value)
                    End Try
                    'End If
                End If
        Else
            OffLine(HidenUrs.Value)
            AUDITORIA(HidenUrs.Value, "Administrador", "CargaMasivas", " ", "Page_Load", "", Request.ServerVariables("REMOTE_ADDR"), Request.ServerVariables("REMOTE_HOST"))
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/SesionExpirada.aspx")
        End If
    End Sub

    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Administrador", "CargaGestionesMasivas.aspx", evento, ex, Cuenta, Captura, usr)
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
    Protected Sub DdlTipo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlTipo.SelectedIndexChanged
        PnlGestiones.Visible = False
        PnlVisitas.Visible = False

        If DdlTipo.SelectedValue = "Gestiones" Then
            PnlGestiones.Visible = True

        ElseIf DdlTipo.SelectedValue = "Visitas" Then
            PnlVisitas.Visible = True

        End If
    End Sub
    Protected Sub aviso(ByVal MSJ As String)
        RadAviso.RadAlert(MSJ, 400, 150, "Aviso", Nothing)
    End Sub
End Class
