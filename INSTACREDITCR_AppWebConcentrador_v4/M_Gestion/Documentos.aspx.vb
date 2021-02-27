Imports System
Imports System.Web
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports System.Net
Imports Db
Imports Funciones
Imports Class_Documentos
Imports Conexiones
Imports Telerik.Web.UI
Imports RestSharp
Imports iText.Kernel.Pdf
Imports iText.Kernel.Geom
Imports iText.Layout
Imports iText.Layout.Element


Partial Class Documentos
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Usuario") IsNot Nothing Then
            Try
                If Not IsPostBack Then
                    GridArchivos.Rebind()
                    GridConvenios.Rebind()
                    PnlCargar.DataBind()
                End If
            Catch ex As Exception
                EnviarCorreo("Gestion", "Documentos.aspx", "Page_Load", ex, "", "", tmpUSUARIO("CAT_LO_USUARIO"))
            End Try
        End If
    End Sub

    Protected Sub RadAsyncUpload1_FileUploaded(sender As Object, e As FileUploadedEventArgs) Handles RadAsyncUpload1.FileUploaded
        Dim ruta As String = StrRuta() & "docs"
        SubExisteRuta(ruta)
        Dim Tamano As Integer = e.File.ContentLength
        Dim nom As String = e.File.FileName.Substring(e.File.FileName.LastIndexOf("\") + 1)
        Dim extension As String = nom.Substring(nom.LastIndexOf("."))
        If Len(txtDescripcion.Text) > 5 Then
            If Tamano < 4000000 Then
                Try
                    Dim ARCHIVO As String = ruta & "\" & nom
                    e.File.SaveAs(ARCHIVO)

                    Dim fs As Stream = File.OpenRead(ARCHIVO)
                    Dim tempBuff(fs.Length) As Byte
                    fs.Read(tempBuff, 0, fs.Length)
                    fs.Close()

                    Dim cmd As New SqlCommand()
                    cmd.CommandText = "SP_ADD_ARCHIVO"
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add("@V_HIST_DO_DOCUMENTO", SqlDbType.Binary).Value = tempBuff
                    cmd.Parameters.Add("@V_HIST_DO_CREDITO", SqlDbType.NVarChar).Value = tmpCredito("PR_MC_CREDITO")
                    'cmd.Parameters.Add("@V_HIST_DO_PRODUCTO", SqlDbType.NVarChar).Value = tmpCredito("PR_MC_PRODUCTO")
                    cmd.Parameters.Add("@V_HIST_DO_USUARIO", SqlDbType.NVarChar).Value = tmpUSUARIO("CAT_LO_USUARIO")
                    cmd.Parameters.Add("@V_HIST_DO_NOMBRE_DOCUMENTO", SqlDbType.NVarChar).Value = nom
                    cmd.Parameters.Add("@V_HIST_DESC_DOCUMENTO", SqlDbType.NVarChar).Value = txtDescripcion.Text
                    cmd.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = 1
                    Consulta_Procedure(cmd, "prueba")
                    Dim script As String = "ClearUploads();"
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ClearUploads", script, True)
                    txtDescripcion.Text = ""
                    showModal(Notificacion, "ok", "Elemento Cargado", "Elemento " & nom & " cargado con éxito.")
                Catch ex As Exception
                    showModal(Notificacion, "warning", "Aviso", ex.ToString)
                End Try
                GridArchivos.Rebind()
                GridConvenios.Rebind()
            Else
                showModal(Notificacion, "warning", "Aviso", "El Archivo Supera El Tamaño Permitido De 4 Megabytes,Intente De Nuevo")
            End If
        Else
            showModal(Notificacion, "warning", "Aviso", "Capture un comentario válido")
        End If

    End Sub

    Public Sub EliminarRes(gvRow As GridDataItem)
        Try
            Dim SSCommandT As New SqlCommand
            SSCommandT.CommandText = "SP_HISTORICO_ARCHIVO"
            SSCommandT.CommandType = CommandType.StoredProcedure
            SSCommandT.Parameters.Add("@V_HIST_DO_CREDITO", SqlDbType.NVarChar).Value = tmpCredito("PR_MC_CREDITO")
            SSCommandT.Parameters.Add("@V_HIST_DO_USUARIO", SqlDbType.NVarChar).Value = gvRow.Cells(4).Text
            SSCommandT.Parameters.Add("@V_HIST_DO_NOMBRE_DOCUMENTO", SqlDbType.NVarChar).Value = gvRow.Cells(5).Text
            SSCommandT.Parameters.Add("@V_HIST_DO_FECHA", SqlDbType.NVarChar).Value = gvRow.Cells(6).Text
            SSCommandT.Parameters.Add("@V_HIST_DESC_DOCUMENTO", SqlDbType.NVarChar).Value = gvRow.Cells(7).Text
            SSCommandT.Parameters.Add("@v_bandera", SqlDbType.Decimal).Value = 2
            Dim oDataset1 As DataTable = Consulta_Procedure(SSCommandT, "documento")
            Funciones.showModal(Notificacion, "ok", "Elemento Eliminado", "Elemento " & gvRow.Cells(5).Text & " eliminado con éxito")
        Catch ex As Exception
        End Try
        GridArchivos.Rebind()
        GridConvenios.Rebind()
    End Sub

    Public Sub DescargarRes(row As GridDataItem)
        Dim SSCommandT As New SqlCommand
        SSCommandT.CommandText = "SP_HISTORICO_ARCHIVO"
        SSCommandT.CommandType = CommandType.StoredProcedure
        SSCommandT.Parameters.Add("@V_HIST_DO_CREDITO", SqlDbType.NVarChar).Value = tmpCredito("PR_MC_CREDITO")
        SSCommandT.Parameters.Add("@V_HIST_DO_USUARIO", SqlDbType.NVarChar).Value = row.Cells(4).Text
        SSCommandT.Parameters.Add("@V_HIST_DO_NOMBRE_DOCUMENTO", SqlDbType.NVarChar).Value = HttpUtility.HtmlDecode(row.Cells(5).Text)
        SSCommandT.Parameters.Add("@V_HIST_DO_FECHA", SqlDbType.NVarChar).Value = row.Cells(6).Text
        SSCommandT.Parameters.Add("@V_HIST_DESC_DOCUMENTO", SqlDbType.NVarChar).Value = row.Cells(7).Text
        SSCommandT.Parameters.Add("@v_bandera", SqlDbType.Decimal).Value = 1
        Dim oDataset1 As DataTable = Consulta_Procedure(SSCommandT, "documento")
        If oDataset1.Rows.Count > 0 Then

            Dim ruta_arch As String = HttpContext.Current.Server.MapPath("~/M_Gestion/GestionDocs/").ToString

            Dim ruta_url As String = HttpContext.Current.Request.Url.AbsoluteUri.Replace("Documentos.aspx", "")

            Dim carga_archivo As String = ruta_arch & oDataset1.Rows(0)("HIST_DO_NOMBRE_DOCUMENTO")
            Dim tmp_archivo As String

            tmp_archivo = ruta_url & "GestionDocs/" & oDataset1.Rows(0)("HIST_DO_NOMBRE_DOCUMENTO")

            'Dim carga_archivo As String = "C:\Cargas\Donde\Salida\" & oDataset1.Rows(0)("HIST_DO_NOMBRE_DOCUMENTO")
            'Dim tmp_archivo As String

            'If DesEncriptarCadena(StrConexion(3)) = "MCCOLLECT" Then
            '    tmp_archivo = "https://pruebasmc.com.mx/GestionV4Imagenes/" & oDataset1.Rows(0)("HIST_DO_NOMBRE_DOCUMENTO")
            'Else
            '    tmp_archivo = "https://dev.mcnoc.mx/ASFGestionDocs/" & oDataset1.Rows(0)("HIST_DO_NOMBRE_DOCUMENTO")
            'End If

            'tmp_archivo = tmp_archivo.Replace(" ", "")
            'carga_archivo = carga_archivo.Replace(" ", "")

            If File.Exists(carga_archivo) Then
                Kill(carga_archivo)
            End If

            If File.Exists(carga_archivo) Then
                Kill(carga_archivo)
            End If

            Dim PDF As Byte() = DirectCast(oDataset1.Rows(0)("HIST_DO_DOCUMENTO"), Byte())

            Dim oFileStream As FileStream
            oFileStream = New FileStream(carga_archivo, FileMode.CreateNew, FileAccess.Write)
            oFileStream.Write(PDF, 0, PDF.Length)
            oFileStream.Close()

            If File.Exists(carga_archivo) Then
                Dim vtn As String = "window.open('" & tmp_archivo & "','M2','scrollbars=yes,resizable=yes','height=300', 'width=300')"
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup2", vtn, True)
            End If

        End If
    End Sub
    Protected Sub RadGrid1_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs)
        If e.CommandName = "download_file" Then
            DescargarRes(TryCast(e.Item, GridDataItem))

        ElseIf e.CommandName = "delete_file" Then
            EliminarRes(TryCast(e.Item, GridDataItem))
        End If

    End Sub

    Protected Sub RadGrid2_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs)
        Try
            If e.CommandName = "download_file" Then
                DescargarRes(TryCast(e.Item, GridDataItem))

            ElseIf e.CommandName = "delete_file" Then
                EliminarRes(TryCast(e.Item, GridDataItem))
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Property tmpUSUARIO As IDictionary
        Get
            Return CType(Session("USUARIO"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("USUARIO") = value
        End Set
    End Property

    Public Property tmpCredito As IDictionary
        Get
            Return CType(Session("Credito"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("Credito") = value
        End Set
    End Property
    Public Property tmpPermisos As IDictionary
        Get
            Return CType(Session("Permisos"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("Permisos") = value
        End Set
    End Property
    Protected Sub GridArchivos_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs)
        Try
            GridArchivos.DataSource = Class_Documentos.LlenaGridDocumentos(tmpCredito("PR_MC_CREDITO"))
        Catch ex As Exception
            GridArchivos.DataSource = Nothing
        End Try
    End Sub

    Protected Sub GridConvenios_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs)
        Try
            GridConvenios.DataSource = Class_Documentos.LlenaGridConvenios(tmpCredito("PR_MC_CREDITO"))
        Catch ex As Exception
            GridConvenios.DataSource = Nothing
        End Try
    End Sub

    Public Sub btnPopUp_Click(sender As Object, e As EventArgs) Handles BtnPopUp.Click
        Try

            Dim link As String = ConstruirLink()
            Dim client = New RestClient(link)
            client.Timeout = -1
            Dim request = New RestRequest(Method.[GET])
            Dim response As IRestResponse = client.Execute(request)

            If response.StatusCode = 200 Then
                Dim Funcion As String = "OpenWindows('" & link & "','directories=no, location=no, menubar=no, scrollbars=yes, statusbar=no, tittlebar=no, resizable=yes');"
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "SIFEL", Funcion, True)
            Else
                showModal(Notificacion, "warning", "Aviso", "Conexion fallida con el web service")
            End If
        Catch ax As WebException
            Dim abc = ax.Message
            showModal(Notificacion, "warning", "Aviso", "WebException" + abc)
        Catch ex As Exception

            Dim abc = ex.Message
            showModal(Notificacion, "warning", "Aviso", "Exception" + abc)
        End Try
    End Sub

    Public Shared Function ConstruirLink() As String
        Dim servidor As String = "http://172.19.217.1/Public/stCRF.aspx?"
        Dim vCompanyId As String = ""
        Dim vUserId As String = "PUBLIC"
        Dim iCreditId As String = "454106"
        Dim fToDate As String = "80265"
        Dim strlink As String = (servidor & "vCompanyId=" & vCompanyId & "&vUserId=" & vUserId & "&iCreditId=" & iCreditId & "&fToDate=" & fToDate)
        Return strlink
    End Function


End Class
