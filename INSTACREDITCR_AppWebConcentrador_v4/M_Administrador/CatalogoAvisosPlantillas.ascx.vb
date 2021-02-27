Imports System.Data

Imports System.IO
Imports System.Web.Services
Imports Conexiones
Imports Db
Imports System.Net
Imports Funciones
Imports iText.Kernel.Geom
Imports iText.Kernel.Pdf
Imports iText.Layout
Imports iText.Layout.Element
Imports System.Data.SqlClient

Partial Class M_Administrador_CatalogoAvisosPlantillas
    Inherits System.Web.UI.UserControl
    Private _dataItem As Object = Nothing
    Public Property DataItem() As Object
        Get
            Return Me._dataItem
        End Get
        Set(ByVal value As Object)
            Me._dataItem = value
        End Set
    End Property

    Private Sub M_Administrador_gridPlantillasCorreo_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("InitInsert") Then
            Session.Remove("InitInsert")
            initEtiquetas()
        End If

        If Session("Edit") Then
            Session.Remove("Edit")
            initEtiquetas()
            txtID.Text = DataItem("ID")

            Dim oraCommand As New SqlCommand
            oraCommand.CommandText = "SP_ADD_CAT_PLANTILLAS_AVISOS"
            oraCommand.CommandType = CommandType.StoredProcedure
            oraCommand.Parameters.Add("V_CAT_PAV_Id", SqlDbType.Int).Value = Val(txtID.Text)
            oraCommand.Parameters.Add("V_CAT_PAV_Nombre", SqlDbType.VarChar).Value = ""
            ' oraCommand.Parameters.Add("V_CAT_PAV_CONFIGURACION", SqlDbType.Clob).Value = DBNull.Value
            oraCommand.Parameters.Add("V_CAT_PAV_PRODUCTO", SqlDbType.VarChar).Value = ""
            oraCommand.Parameters.Add("V_Bandera", SqlDbType.Int).Value = 5
            'oraCommand.Parameters.Add("CV_1", SqlDbType.Cursor).Direction = ParameterDirection.Output
            Dim DtsAviso As DataTable = Consulta_Procedure(oraCommand, "Aviso")
            DdlInstancia.SelectedText = DtsAviso.Rows(0)("Instancia")
            'DdlReferencia.SelectedText = DtsAviso.Rows(0)("Referencia")
            DdlRolParticipante.SelectedText = DtsAviso.Rows(0)("Rol")
            txtNombre.Text = DtsAviso.Rows(0)("Nombre")
            'Session("datoseditor") = DtsAviso.Rows(0)("Configuracion")
            editor.Content = DtsAviso.Rows(0)("Configuracion")
        End If
    End Sub

    Private Sub initEtiquetas()
        Dim oraCommanAgencias As New SqlCommand
        oraCommanAgencias.CommandText = "SP_ADD_CAT_ETIQUETAS_AVISOS"
        oraCommanAgencias.CommandType = CommandType.StoredProcedure
        oraCommanAgencias.Parameters.Add("V_Bandera", SqlDbType.VarChar).Value = 0
        'oraCommanAgencias.Parameters.Add("CV_1", SqlDbType.Cursor).Direction = ParameterDirection.Output
        Dim DtsVarios As DataTable = Consulta_Procedure(oraCommanAgencias, "SP_ADD_CAT_ETIQUETAS_CORREO")
        comboEtiquetas.DataTextField = "Etiqueta"
        comboEtiquetas.DataValueField = "CAMPOREAL"
        comboEtiquetas.DataSource = DtsVarios
        comboEtiquetas.DataBind()
    End Sub
    Private FONTSIZE As String = ".6em"

    Protected Sub ExportToXcel_SomeReport(fileName As String)

        Dim file As FileInfo = New FileInfo("E:\CREDIFIEL\BIENESTAR\PDFTemp\")
        If Not file.Directory.Exists Then
            file.Directory.Create()
        End If


        fileName = String.Format(fileName, DateTime.Now.ToString("MMddyyyy_hhmmss"))
        Dim pdfDoc As PdfDocument = New PdfDocument(New PdfWriter("E:\Cargas\CREDIFIEL\PDFTemp\archivo.pdf"))
        Dim Document As iText.Layout.Document = New iText.Layout.Document(pdfDoc, PageSize.LEGAL)
        Dim html As String = editor.Content

        putHTML(Document, "<div style=""max-width:19cm;height: 33cm;font-size:.6em; width:100%""><table cellspacing='0' cellpadding='0' style='border: none;' width='100%' border ='0'>" +
           "<tr>" +
           "<td><b><font size='1'>ID:</font></b></td>" +
           "<td><b><font size='1'>SALDO VENCIDO:</font></b></td>" +
           "<td><b><font size='1'>NÚMERO DE EMPLEADO:</font></b></td>" +
           "</tr>" +
           "<tr>" +
           "<td><b><font size='1'>NOMBRE:</font></b></td>" +
           "<td><b><font size='1'>DÍAS MORA:</font></b></td>" +
           "<td><b><font size='1'>FECHA DE IMPRESIÓN:</font></b></td>" +
           "</tr>" +
           "<tr>" +
           "<td><b><font size='1'>ROL:</font></b></td>" +
           "<td><b><font size='1'>FACTURAS:</font></b></td>" +
           "<td><b><font size='1'>NOMBRE USUARIO:</font></b></td>" +
           "</tr>" +
           "<tr>" +
           "<td><b><font size='1'>ACUERDO:</font></b></td>" +
           "<td><b><font size='1'>FRECUENCIA DE FACTURA:</font></b></td>" +
           "<td><b><font size='1'>JEFE DE AREA:</font></b></td>" +
           "</tr>" +
           "<tr>" +
           "<td><b><font size='1'>DOMICILIO:</font></b></td>" +
           "<td><b><font size='1'>MONTO PRÓXIMA FACTURA:</font></b></td>" +
           "<td><b><font size='1'>ÚLTIMA GESTIÓN:</font></b></td>" +
           "</tr>" +
           "<tr>" +
           "<td><b>&nbsp;</b></td>" +
           "<td><b><font size='1'>FECHA PRÓXIMA FACTURA:</font></b></td>" +
           "<td><b>&nbsp;</b></td>" +
           "</tr>" +
           "<tr>" +
           "<td><b><font size='1'>TITULAR:</font></b></td>" +
           "<td><b>&nbsp;</b></td>" +
           "<td><b>&nbsp;</b></td>" +
           "</tr>" +
           "<tr>" +
           "<td><b><font size='1'>OBSERVACIONES:</font></b></td>" +
           "<td><b>&nbsp;</b></td>" +
           "<td><b>&nbsp;</b></td>" +
           "</tr>" +
           "</table><br/>" & html & "</div>")
        pdfDoc.Close()
        DynaGenExcelFile(fileName, pdfDoc)

    End Sub
    Private Sub putHTML(ByRef document As Document, html As String)
        Try
            Dim elements As IList(Of IElement) = iText.Html2pdf.HtmlConverter.ConvertToElements(html)

            For Each element As IElement In elements
                document.Add(TryCast(element, IBlockElement))
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub DynaGenExcelFile(fileName As String, xlsx As PdfDocument)

        Try
            Me.Response.ClearContent()
            Me.Response.ClearHeaders()
            Me.Response.Buffer = True
            Me.Response.Clear()
            Me.Response.AddHeader("content-disposition", "attachment; filename=" + "DemoPreliminarPDF" + ".pdf")
            Me.Response.BinaryWrite(IO.File.ReadAllBytes("E:\Cargas\CREDIFIEL\PDFTemp\archivo.pdf"))
            Me.Response.Flush()



        Catch ex As Exception
            Dim esss As String = ex.Message
        End Try




        'Catch ex As System.Threading.ThreadAbortException
        'End Try
    End Sub

    Public Sub exportpdf_clic(sender As Object, e As EventArgs)

        Try
            ExportToXcel_SomeReport("archivo")
            Kill("E:\Cargas\CREDIFIEL\PDFTemp\archivo.pdf")
        Catch ex As Exception
            Dim s As String = ex.Message
        End Try
    End Sub
End Class
