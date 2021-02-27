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

Public Class AvisosPDFReglas
    Private FONTSIZE As String = ".6em"
    '1em = 16px
    Private rutaDestino As String
    '  Private creditoID As String
    ' Private avisoID As String
    ''' <summary>
    ''' Inicializa las variables necesarias para generar un pdf para un credito en especial
    ''' </summary>
    ''' <param name="creditoID">Crédito al que se le generará el PDF</param>
    Public Sub New(creditoID As String)
        'Me.creditoID = creditoID
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

    ''' <summary>
    ''' Inicializa las variables necesarias para generar un pdf para un credito en especial
    ''' </summary>
    ''' <param name="creditoID">Crédito al que se le generará el PDF</param>
    Public Sub New(creditoID As String, AvisoID As String)
        'Me.creditoID = creditoID
        'Me.avisoID = AvisoID
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


    Function CreatePDF(ByVal DtaTable As DataTable, ByVal Plantilla As String, ByVal UsuarioA As String, ByVal FechaSim As String, ByVal FechaVig As String) As String
        Dim V_Error As String = "OK"
        Dim avisoID As String
        Dim creditoID As String
        Dim usrdestino As String
        Dim participante As String
        Dim folio As String
        Dim direccion As String
        Dim rutaPDF As String = rutaDestino + "Avisos_descargar\AvisosReglas\" & UsuarioA & ".pdf"
        Try


            Dim file As FileInfo = New FileInfo(rutaPDF)
            Dim pdfDoc As PdfDocument = New PdfDocument(New PdfWriter(rutaPDF))
            Dim Document As Document = New Document(pdfDoc, PageSize.LEGAL)

            For Each rowx As DataRow In DtaTable.Rows
                avisoID = Plantilla
                creditoID = rowx("CREDITO")
                usrdestino = UsuarioA
                participante = rowx("Cliente_Id")
                folio = "Aviso_" & rowx("CREDITO") & "_" & Now.Month & Now.Day & Now.Second
                direccion = rowx("DIRECCION_ID")
                If FechaSim.ToString <> "" Then
                    Dim objeto As Class_P_Saldos = Consultasaldoscredito(rowx("CREDITO"), rowx("cliente"), FechaSim)
                    If objeto.Estatus = "Error" Then
                        If objeto.Informacion(0).mensajeSafi.ToString() = "No es posible conectar con el servidor remoto" Then
                            V_Error = objeto.Informacion(0).mensajeSafi.ToString & " Genere Avisos Sin Simulacion"
                        Else
                            V_Error = objeto.Informacion(0).mensajeSafi.ToString
                        End If
                        Exit For
                    End If
                    Dim Honorarios As DataTable = Consulta("select nvl(CAT_PE_PUESTACORRIENMTE,0) CAT_PE_PUESTACORRIENMTE,nvl(CAT_PE_LIQUIDACIONES,0) CAT_PE_LIQUIDACIONES from cat_logins join cat_perfiles on CAT_PE_ID=cat_lo_perfil join pr_mc_gral on pr_mc_uasignado=cat_lo_usuario where pr_mc_Credito='" & rowx("CREDITO") & "'", "Queri")
                    Dim SdoLiquidarHonorarios As Double = Val(objeto.Informacion(0).baseHonorariosT) * Val(Val(Honorarios.Rows(0).Item("CAT_PE_LIQUIDACIONES")) / 100)
                    Dim SdoCorrienteHonorarios As Double = Val(objeto.Informacion(0).baseHonorariosP) * Val(Val(Honorarios.Rows(0).Item("CAT_PE_PUESTACORRIENMTE")) / 100)

                    Ejecuta("update pr_bienestar set PR_BI_PUESTAALCORRIENTE=to_number('" & objeto.Informacion(0).saldoPCorriente & "','999999999.9999999'), PR_BI_PUESTAALCORRIENTEHONO=to_number('" & SdoCorrienteHonorarios & "','999999999.9999999'), PR_BI_LIQUIDACION=to_number('" & objeto.Informacion(0).saldoLiquidar & "','999999999.9999999'), PR_BI_LIQUIDACIONHONO=to_number('" & SdoLiquidarHonorarios & "','999999999.9999999'), PR_BI_DIASMORAA=to_number('" & objeto.Informacion(0).diasMora & "','999999999'), PR_BI_DTESIMULACION=to_date('" & FechaSim & "','yyyy-mm-dd') where PR_BI_CREDITOID='" & rowx("credito") & "'")
                End If
                If UsuarioA <> "Impresion" Then
                    Ejecuta("update hist_direcciones_demo set hist_d_usuario_asignado='" & UsuarioA & "',hist_d_dte_vigencia=to_date('" & FechaVig.ToString.Substring(0, 10) & "','DD/MM/YYYY') where HIST_D_DIRECCIONID='" & rowx("Direccion_ID") & "'  AND  HIST_D_TIPODIRECCIONID='" & rowx("TipoDireccion") & "' AND HIST_D_CREDITO='" & rowx("credito") & "'")
                    Ejecuta("update pr_mc_gral set pr_mc_uasignadov='" & UsuarioA & "' where pr_mc_credito='" & rowx("credito") & "'")
                End If

                Dim drow As DataRow = guardargestion(creditoID, usrdestino, participante, FechaVig, folio, direccion, avisoID)

                Dim talon As New StringBuilder
                Dim estilosCelda As String = "width:33%;padding: 0px " & FONTSIZE & " 0px 0px;"
                Dim row As String = "<tr>" &
                "<td style=""" & estilosCelda & """><b>{0}:</b> {1}</td>" &
                "<td style=""" & estilosCelda & """><b>{2}:</b> {3}</td>" &
                "<td style=""" & estilosCelda & """><b>{4}:</b> {5}</td>" &
                "</tr>"
                Dim row2 As String = "<tr>" &
            "<td style=""" & estilosCelda & """><b>{0}:</b> {1}</td>" &
            "<td style=""colspan:2""><b>{2}:</b> {3}</td>" &
            "</tr>"
                Dim estilosTabla As String = "width:100%;height: 4.3cm;border-collapse:collapse;border-style:none none dashed none;text-align:justify;"
                talon.AppendFormat("<table style=""{0}"">", estilosTabla)

                talon.AppendFormat(row, "CLIENTE", drow.Item("ID"), "SALDO VENCIDO", to_money(drow.Item("SALDO_VENCIDO")), "NÚMERO DE EMPLEADO", drow.Item("NOEMPLEADO"))
                talon.AppendFormat(row, "NOMBRE", drow.Item("NOMBRE"), "DÍAS MORA", drow.Item("MORA"), "FECHA IMPRESION", Now.ToShortDateString)
                talon.AppendFormat(row, "ROL", drow.Item("ROL"), "FACTURAS", drow.Item("FACTURAS"), "NOMBRE USUARIO", drow.Item("USUARIO"))
                talon.AppendFormat(row2, "# CRÉDITO", drow.Item("ACUERDO"), "FRECUENCIA DE FACTURA", drow.Item("FRECUENCIA"))
                talon.AppendFormat(row, "DOMICILIO", drow.Item("DOMICILIO"), "MONTO PRÓXIMA FACTURA", to_money(drow.Item("MPROXPAGO")), "ÚLTIMA GESTION", drow.Item("FULTIMAGESTION"))
                talon.AppendFormat(row, "TITULAR", drow.Item("TITULAR"), "FECHA PROXIMA FACTURA", drow.Item("FPROXPAGO").ToString.Split(" ")(0), "PAGOS VENCIDOS", drow.Item("ATRASO").ToString)
                talon.AppendFormat(row2, "ÚLTIMO PAGO", drow.Item("ultimopago"), "VISITADOR", drow.Item("VISITADOR"))
                talon.AppendFormat("<tr><td colspan=""3""><b>{0}</b> {1} </br></br></td></tr>", "", "")
                talon.Append("</table></br>")




                Dim html As String = traePlantilla(avisoID)
                Dim etiquetas() As String = getEtiquetas(html)
                For Each etiqueta As String In etiquetas
                    Dim valor As String
                    Try
                        valor = getValorEtiqueta(etiqueta, direccion, creditoID, rowx("Cliente_Id"))
                        Dim h = valor
                    Catch ex As Exception
                        valor = " "
                    End Try
                    html = html.Replace("<<" & etiqueta & ">>", valor)
                Next
                putHTML(Document, "<div style=""max-width:19cm;height: 33cm;""> <div style=""font-size:" & FONTSIZE & "; width:100%"">" & talon.ToString & html & "</div></div>")
                pdfDoc.AddNewPage()
            Next
            pdfDoc.Close()
            Return V_Error
        Catch ex As Exception
            Return V_Error
        End Try
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

    Private Function traePlantilla(avisoID As String) As String
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_ADD_CAT_PLANTILLAS_AVISOS"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_CAT_PAV_Id", SqlDbType.Decimal).Value = avisoID
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = 8

        Dim DtsPDF_Fichas As DataTable = Consulta_Procedure(SSCommand, "PDF")
        Return DtsPDF_Fichas(0)(0).ToString.Replace("&gt;&gt;", ">>").Replace("&lt;&lt;", "<<")
    End Function

    Private Function getValorEtiqueta(etiqueta As String, idDomicilio As Integer, creditoID As String, clienteID As String) As String
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_ADD_CAT_PLANTILLAS_AVISOS"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_CAT_PAV_nombre", SqlDbType.NVarChar).Value = etiqueta
        SSCommand.Parameters.Add("@V_CAT_PAV_producto", SqlDbType.NVarChar).Value = creditoID
        SSCommand.Parameters.Add("@V_CAT_PAV_Id", SqlDbType.Decimal).Value = idDomicilio
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

    Private Function guardargestion(ByVal credito As String, ByVal usrdestino As String, ByVal participante As String, ByVal dtelimite As String, ByVal folio As String, ByVal direccion As String, ByVal avisoID As String) As DataRow
        Dim rows As DataRow = Nothing
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_PDF_AVISOS"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_credito", SqlDbType.NVarChar).Value = credito
        SSCommand.Parameters.Add("@v_usuario", SqlDbType.NVarChar).Value = usrdestino
        SSCommand.Parameters.Add("@v_resultado", SqlDbType.NVarChar).Value = "Aviso" & avisoID
        SSCommand.Parameters.Add("@v_dtecontacto", SqlDbType.NVarChar).Value = dtelimite
        SSCommand.Parameters.Add("@v_callid", SqlDbType.NVarChar).Value = folio
        SSCommand.Parameters.Add("@v_participante", SqlDbType.NVarChar).Value = participante
        SSCommand.Parameters.Add("@v_direccion", SqlDbType.Decimal).Value = direccion


        Dim DtsPDF_Fichas As DataTable = Consulta_Procedure(SSCommand, "PDF")
        rows = DtsPDF_Fichas.Rows(0)
        Return rows

    End Function
End Class
