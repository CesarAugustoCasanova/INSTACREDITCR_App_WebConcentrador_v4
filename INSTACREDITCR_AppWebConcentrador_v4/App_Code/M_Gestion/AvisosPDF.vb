Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports iText.Kernel.Pdf
Imports iText.Layout
Imports iText.Layout.Element
Imports iText.Test.Attributes
Imports iText
Imports iText.Forms
Imports iText.Forms.Fields
Imports iText.Kernel.Geom
Imports iText.Kernel.Pdf.Canvas
Imports iText.Kernel.Font
Imports iText.IO.Font.Constants
Imports System.Data.SqlClient
Imports System.Data
Imports Funciones
Imports Db
Imports RestSharp
Imports Newtonsoft.Json
Imports AvisosPDFTalon

Public Class AvisosPDF
    Private FONTSIZE As String = ".6em"
    '1em = 16px
    Private rutaDestino As String
    Private creditoID As String
    Private clienteID As String
    Private Plantilla As String
    Private numeroid As String
    Private rol As String
    Private tipo_reg As String
    Private direccionid As String

    ''' <summary>
    ''' Inicializa las variables necesarias para generar un pdf para un credito en especial
    ''' </summary>
    Public Sub New(creditoID As String, numeroid As String, direccionid As String, PlantillaID As String)
        Me.creditoID = creditoID
        Me.clienteID = numeroid
        Me.Plantilla = PlantillaID
        Me.numeroid = numeroid
        Me.direccionid = direccionid
        Try
            rutaDestino = "E:\Cargas\BIENESTAR\Avisos\"
            Dim file As FileInfo = New FileInfo(rutaDestino)
            If Not file.Directory.Exists Then
                file.Directory.Create()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Function CreatePDF(ByVal UsuarioA As String, ByVal FechaVig As String, folio As String, simular As Boolean, FechaSim As String) As String
        Dim V_Error As String = "OK"

        Dim usrdestino As String
        'Dim rutaPDF As String = rutaDestino + "Avisos_descargar\AvisosReglas\" & UsuarioA & ".pdf"
        'Dim rutaPDF As String = rutaDestino + "Avisos_descargar\" & Now.ToShortDateString.Replace("/", "_").Replace(" ", "") & "\" & folio & ".pdf"
        Dim rutaPDF As String = rutaDestino + "Avisos_descargar\" & Now.ToShortDateString.Replace("/", "_").Replace(" ", "") & "\" & folio & ".pdf"
        Dim file As FileInfo = New FileInfo(rutaPDF)

        If Not file.Directory.Exists Then
            file.Directory.Create()
        End If

        If file.Exists Then
            rutaPDF = rutaDestino + "Avisos_descargar\" & Now.ToShortDateString.Replace("/", "_").Replace(" ", "") & "\" & folio & "_" & Now.ToShortTimeString.Replace(":", "_").Replace(" ", "") & ".pdf"
            file = New FileInfo(rutaPDF)
        End If

        Dim pdfDoc As PdfDocument = New PdfDocument(New PdfWriter(rutaPDF))
        Dim Document As Document = New Document(pdfDoc, PageSize.LEGAL)
        usrdestino = UsuarioA
        folio = "Aviso_" & creditoID & "_" & Now.Month & Now.Day & Now.Second

        If simular Then
            Dim objeto As Class_P_Saldos = Consultasaldoscredito(creditoID, numeroid, FechaSim.ToString.Substring(0, 10))

            If objeto.Estatus = "Error" Then
                Throw New Exception(objeto.Informacion(0).mensajeSafi.ToString)

            End If


            Dim Honorarios As DataTable = Consulta("select nvl(CAT_PE_PUESTACORRIENMTE,0) CAT_PE_PUESTACORRIENMTE,nvl(CAT_PE_LIQUIDACIONES,0) CAT_PE_LIQUIDACIONES from cat_logins join cat_perfiles on CAT_PE_ID=cat_lo_perfil join pr_mc_gral on pr_mc_uasignado=cat_lo_usuario where pr_mc_Credito='" & creditoID & "'", "Queri")

            Dim SdoLiquidarHonorarios As Double = Val(objeto.Informacion(0).baseHonorariosT) * Val(Val(Honorarios.Rows(0).Item("CAT_PE_LIQUIDACIONES")) / 100)
            Dim SdoCorrienteHonorarios As Double = Val(objeto.Informacion(0).baseHonorariosP) * Val(Val(Honorarios.Rows(0).Item("CAT_PE_PUESTACORRIENMTE")) / 100)



            Ejecuta("update pr_bienestar set PR_BI_PUESTAALCORRIENTE=to_number('" & objeto.Informacion(0).saldoPCorriente & "','999999999.9999999'), PR_BI_PUESTAALCORRIENTEHONO=to_number('" & SdoCorrienteHonorarios & "','999999999.9999999'), PR_BI_LIQUIDACION=to_number('" & objeto.Informacion(0).saldoLiquidar & "','999999999.9999999'), PR_BI_LIQUIDACIONHONO=to_number('" & SdoLiquidarHonorarios & "','999999999.9999999'), PR_BI_DIASMORAA=to_number('" & objeto.Informacion(0).diasMora & "','999999999'), PR_BI_DTESIMULACION=to_date('" & FechaSim & "','yyyy-mm-dd') where PR_BI_CREDITOID='" & creditoID & "'")
        End If

        Dim drow As DataRow = guardargestion(usrdestino, FechaVig, folio)
        rol = getRol(drow("ROL").ToString)
        tipo_reg = drow("tipo_reg").ToString

        Ejecuta("update hist_direcciones_demo set hist_d_usuario_asignado='" & UsuarioA & "',hist_d_dte_vigencia=to_date('" & FechaVig.ToString.Substring(0, 10) & "','DD/MM/YYYY') where HIST_D_DIRECCIONID='" & direccionid & "' and  HIST_D_NUMEROID='" & numeroid & "'  and HIST_D_TIPOREGISTRO='" & tipo_reg & "' and HIST_D_ROL='" & rol & "'")
        Ejecuta("update pr_mc_gral set pr_mc_uasignadov='" & UsuarioA & "' where pr_mc_credito='" & creditoID & "'")

        Dim talon As String = AvisosPDFTalon.generarTalon(drow)

        Dim html As String = traePlantilla()
        Dim etiquetas() As String = getEtiquetas(html)
        For Each etiqueta As String In etiquetas
            Dim valor As String
            Try
                valor = getValorEtiqueta(etiqueta)
                Dim h = valor
            Catch ex As Exception
                valor = " "
            End Try
            html = html.Replace("<<" & etiqueta & ">>", valor)
        Next
        putHTML(Document, "<div style=""max-width:19cm;height: 33cm;font-size:" & FONTSIZE & "; width:100%"">" & talon & html & "</div>")

        pdfDoc.Close()
        Return V_Error

    End Function
    Public Shared Function Consultasaldoscredito(ByVal V_creditoID As String, ByVal V_cliente As String, ByVal V_fecha As String) As Class_P_Saldos

        Dim Class_P_Saldos As Class_P_Saldos
        Dim v_endpoint As String = "https://pruebasmc.com.mx/WsMovilBienestar/api/"
        Dim v_metodo As String = "SafiproyeccionSaldos"

        Dim data As String = "{" & vbCrLf &
                  "  ""creditoID"": """ & V_creditoID & """," & vbCrLf &
                  "  ""cliente"": """ & V_cliente & """," & vbCrLf &
                  "  ""fecha"": """ & V_fecha & """" & vbCrLf &
                              "}" & vbCrLf

        Dim v_credential As String = "dXN1YXJpb1BydWViYVdTOjEyMw=="
        Dim client = New RestClient(v_endpoint & v_metodo)
        Dim request = New RestRequest(Method.POST)
        request.AddHeader("cache-control", "no-cache")
        request.AddHeader("Content-Type", "application/json")
        request.AddParameter("undefined", data, ParameterType.RequestBody)

        Dim response As IRestResponse = client.Execute(request)
        Dim invDom As String = response.Content

        If response.StatusCode.ToString = "OK" Then
            Class_P_Saldos = JsonConvert.DeserializeObject(Of Class_P_Saldos)(response.Content)
        End If

        Return Class_P_Saldos

    End Function

    Private Function traePlantilla() As String
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_ADD_CAT_PLANTILLAS_AVISOS"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_CAT_PAV_Id", SqlDbType.Decimal).Value = Plantilla
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = 8

        Dim DtsPDF_Fichas As DataTable = Consulta_Procedure(SSCommand, "PDF")
        Return DtsPDF_Fichas(0)(0).ToString.Replace("&gt;&gt;", ">>").Replace("&lt;&lt;", "<<")
    End Function

    Private Function getValorEtiqueta(etiqueta As String) As String


        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_ADD_CAT_PLANTILLAS_AVISOS"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_CAT_PAV_nombre", SqlDbType.NVarChar).Value = etiqueta
        SSCommand.Parameters.Add("@V_CAT_PAV_producto", SqlDbType.NVarChar).Value = creditoID
        SSCommand.Parameters.Add("@V_CAT_PAV_Id", SqlDbType.Decimal).Value = direccionid
        SSCommand.Parameters.Add("@V_CLIENTE", SqlDbType.Decimal).Value = clienteID
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = 9

        Return Consulta_Procedure(SSCommand, "PDF")(0)(0).ToString
    End Function

    Private Sub putText(ByRef canvas As PdfCanvas, texto As String, x As Integer, y As Integer, Optional textSize As Integer = 7, Optional font As String = "Helvetica")
        canvas.BeginText().SetFontAndSize(PdfFontFactory.CreateFont(font), textSize).MoveText(x, y).ShowText(texto).EndText()
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

    Private Function getEtiquetas(data As String) As String()
        Dim etiquetas As String = getAllEtiquetas(data)
        Return EliminarEtiquetasRepetidas(etiquetas).Split(",")
    End Function

    Private Function getAllEtiquetas(data As String) As String
        Dim etiquetas As String = ""
        Dim chars() As Char = data.ToCharArray
        Dim etiquetaComienza As Integer = 0
        Dim etiquetaTermina As Integer = 0
        For Each caracter As Char In chars
            Select Case caracter
                Case "<"
                    etiquetaComienza += 1
                    If etiquetaComienza > 2 Then
                        etiquetaComienza -= 1
                    End If
                Case ">"
                    etiquetaTermina += 1
                    If etiquetaTermina = 2 Then
                        etiquetaTermina = 0
                        etiquetaComienza = 0
                        etiquetas &= ","
                    End If
                Case Else
                    If etiquetaTermina <> 2 Then
                        etiquetaTermina = 0
                    End If

                    If etiquetaComienza <> 2 Then
                        etiquetaComienza = 0
                    Else
                        etiquetas &= caracter
                    End If
            End Select
        Next
        Try
            etiquetas = etiquetas.Substring(0, etiquetas.Length - 1)
        Catch ex As Exception
        End Try
        Return etiquetas
    End Function

    Private Function EliminarEtiquetasRepetidas(data As String) As String
        Dim hash As New Hashtable
        Dim etiquetas As String = ""
        For Each etiqueta As String In data.Split(",")
            If Not hash.ContainsKey(etiqueta) Then
                hash.Add(etiqueta, "gg")
                etiquetas &= etiqueta & ","
            End If
        Next
        Try
            etiquetas = etiquetas.Substring(0, etiquetas.Length - 1)
        Catch ex As Exception
        End Try
        Return etiquetas
    End Function

    Private Function guardargestion(ByVal usrdestino As String, ByVal dtelimite As String, ByVal folio As String) As DataRow
        Dim row As DataRow = Nothing
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_PDF_AVISOS"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_credito", SqlDbType.NVarChar).Value = creditoID
        SSCommand.Parameters.Add("@v_usuario", SqlDbType.NVarChar).Value = usrdestino
        SSCommand.Parameters.Add("@v_resultado", SqlDbType.NVarChar).Value = "Aviso" & Plantilla
        SSCommand.Parameters.Add("@v_dtecontacto", SqlDbType.NVarChar).Value = dtelimite
        SSCommand.Parameters.Add("@v_callid", SqlDbType.NVarChar).Value = folio
        SSCommand.Parameters.Add("@v_participante", SqlDbType.NVarChar).Value = numeroid
        SSCommand.Parameters.Add("@v_direccion", SqlDbType.Decimal).Value = direccionid


        Dim DtsPDF_Fichas As DataTable = Consulta_Procedure(SSCommand, "PDF")
        row = DtsPDF_Fichas.Rows(0)
        Return row

    End Function

    Private Function getRol(rol As String) As String
        Select Case rol.ToUpper
            Case "TITULAR"
                Return "T"
            Case "AVAL"
                Return "A"
            Case "CODEUDOR"
                Return "C"
            Case "GARANTE"
                Return "G"
            Case Else
                Return rol
        End Select
    End Function
End Class
