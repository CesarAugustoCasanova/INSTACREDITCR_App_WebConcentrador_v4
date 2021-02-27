Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports Db
Imports Funciones
Imports System.Diagnostics
Imports Telerik.Web.UI

Partial Class Cartera
    Inherits System.Web.UI.Page

    Dim ERRORS As String = "100"
    Dim SKIP As String = "1"
    Dim ctlCarga As String
    Dim logCarga As String
    Dim badCarga As String
    Dim Ruta As String = StrRuta() & "Cartera\"

    Protected Sub BtnCargar_Click(sender As Object, e As EventArgs) Handles BtnCargar.Click

        Try
                Dim Directory As New IO.DirectoryInfo(Ruta)
                Dim allFiles As IO.FileInfo() = Directory.GetFiles.Where(Function(fi) fi.Extension = ".csv" OrElse fi.Extension = ".txt").ToArray
                If allFiles.Length = 0 Then
                    aviso("Archivo No Seleccionado o No Valido ")
                ElseIf DdlDelimitador.SelectedValue = "Seleccione" Then
                aviso("Seleccione Un Delimitador")
            ElseIf DdlTipo.SelectedValue = "Seleccione" Then
                aviso("Seleccione Un Tipo")
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
                            Dim TABLA As String = "TMP_CASHBACK"
                            ctlCarga = Ruta & "CTL_CASH_" & Now.ToShortDateString.Replace("/", "") & "" & ".ctl"
                            logCarga = Ruta & "LOG_CASH_" & Now.ToShortDateString.Replace("/", "") & "" & ".log"
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
                            sw.WriteLine("TMP_CB_CREDITO,")
                            sw.WriteLine("TMP_CB_TEXTO")
                            sw.WriteLine(")")
                            sw.Close()
                            fs.Close()
                            ' Ejecuta("truncate table " & TABLA & "")
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
                                LblMensaje.Text = "Error Num" & num
                            End If
                            Else
                                LblMensaje.Text = ""
                                Dim SSCommand2 As New SqlCommand("SP_CARGAR_CASHBACK")
                                SSCommand2.CommandType = CommandType.StoredProcedure
                                SSCommand2.Parameters.Add("@V_Usuario", SqlDbType.NVarChar).Value = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
                                SSCommand2.Parameters.Add("@V_Tipo", SqlDbType.NVarChar).Value = DdlTipo.SelectedValue
                                Dim DtsCarga As DataTable = Consulta_Procedure(SSCommand2, "SP_CARGAR_CASHBACK")
                                GvCargaRojos.DataSource = DtsCarga
                                GvCargaRojos.DataBind()
                                LblMensaje.Text = "Fin De La Carga"
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
                SendMail("BtnCargar_Click", ex, "", "", HidenUrs.Value)
            End Try

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        BtnCargar.Attributes.Add("onclick", "this.disabled=true;" + Page.ClientScript.GetPostBackEventReference(BtnCargar, "").ToString())

        If Session("USUARIOADMIN") IsNot Nothing Then
            HidenUrs.Value = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
            'If CType(Session("USUARIOADMIN"), USUARIO).CAT_LO_MADMINISTRADOR.ToString.Substring(19, 1) = 0 Then
            '    OffLine(HidenUrs.Value)
            '    Session.Clear()
            '    Session.Abandon()
            '    Response.Redirect("~/SesionExpirada.aspx")
            'Else
            Try
                    If Not IsPostBack Then
                        LLENAR_DROP(16, DdlDelimitador, "Delimitador", "Delimitador")
                    End If
                Catch ex As Exception
                    SendMail("Page_Load", ex, "", "", HidenUrs.Value)
                End Try
                'End If
                Else
            OffLine(HidenUrs.Value)
            AUDITORIA(HidenUrs.Value, "Administrador", "Cartera", " ", "Page_Load", "", Request.ServerVariables("REMOTE_ADDR"), Request.ServerVariables("REMOTE_HOST"))
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/SesionExpirada.aspx")
        End If
    End Sub

    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Administrador", "Cartera.aspx", evento, ex, Cuenta, Captura, usr)
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
        'Try
        SubExisteRuta(Ruta)
            For Each s In System.IO.Directory.GetFiles(Ruta)
                System.IO.File.Delete(s)
            Next
            Dim nom As String = e.File.FileName
            Dim ruta_archivo As String = Ruta + nom
            e.File.SaveAs(ruta_archivo)
        'Catch ex As Exception
        'End Try
    End Sub
    Protected Sub aviso(ByVal MSJ As String)
        RadAviso.RadAlert(MSJ, 400, 150, "Aviso", Nothing)
    End Sub
End Class
