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
Imports Db

Public Class GestionPDF
    Private rutaDestino As String
    Private creditoID As String
    ''' <summary>
    ''' Inicializa las variables necesarias para generar un pdf para un credito en especial
    ''' </summary>
    ''' <param name="creditoID">Crédito al que se le generará el PDF</param>
    Public Sub New(creditoID As String)
        Me.creditoID = creditoID
        Try
            rutaDestino = "E:\Cargas\GestionV4\"
            Dim file As FileInfo = New FileInfo(rutaDestino)
            If Not file.Directory.Exists Then
                file.Directory.Create()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Property tmpUSUARIO As IDictionary
        Get
            Return CType(HttpContext.Current.Session("USUARIO"), IDictionary)
        End Get
        Set(value As IDictionary)
            HttpContext.Current.Session("USUARIO") = value
        End Set
    End Property

    Public Property tmpCredito As IDictionary
        Get
            Return CType(HttpContext.Current.Session("Credito"), IDictionary)
        End Get
        Set(value As IDictionary)
            HttpContext.Current.Session("Credito") = value
        End Set
    End Property
    Public Sub CreateFichaNegociacion(ByVal folioFicha As String, ByVal tipoFicha As String, ByVal usuarioEnvioID As String, ByVal usuarioEnvioNombre As String, ByVal usuarioIP As String, ByVal clienteID As String, ByVal creditoID As String, ByVal tipoPago As String, ByVal totalPagar As String, ByVal fechaPago As String, ByVal observaciones As String, ByVal valorNetoActivo As String, ByVal capital As String, ByVal interes As String, ByVal ivaInteres As String, ByVal moratorio As String, ByVal ivaMoratorio As String, ByVal comisiones As String, ByVal ivaComisiones As String, ByVal cC_comisiones As String, ByVal cC_moratorios As String, ByVal cC_interes As String, ByVal cC_capital As String, ByVal gastosCobranza As String, ByVal ivaGastosCob As String, ByVal honorario As String, ByVal despachoID As String, ByVal despachoNombre As String, ByVal tipoDespacho As String, ByVal abogadoID As String, ByVal abogadoNombre As String, ByVal supervisorID As String, ByVal supervisorNombre As String, ByVal folioDacion As String, ByVal descripcion As String, ByVal valorComercial As String, ByVal valorAplicar As String, ByVal numCuenta As String, ByVal monto As String, ByVal CONDOCOMIS As String, ByVal CONDOMORA As String, ByVal CONDOINT As String, ByVal CONDOCAP As String, ByVal MTOCTASAHO As String)
        Dim dataCred As Data.DataTable = Class_Fichas.LlenarPDF(Class_CapturaVisitas.VariosQ(tmpCredito("PR_MC_CREDITO"), tmpCredito("CAT_LO_USUARIO"), 18, "CREDIFIEL"), 0)
        'Dim dataCred As Data.DataSet = HttpContext.Current.Session("RESULTADOFICHA")

        'dim SSCommand as new sqlcommand
        'SSCommand.CommandText = "SP_TRAE_CUENTAS_HABERES"
        'SSCommand.CommandType = CommandType.StoredProcedure
        'SSCommand.Parameters.Add("@CREDITO", SqlDbType.NVarChar).Value = creditoID
        '
        'Dim dtcuentas As DataSet = Consulta_Procedure(SSCommand, "ELEMENTOS")

        If dataCred IsNot Nothing Then
            If dataCred.Rows.Count > 0 Then
                Dim rutaPDF As String = rutaDestino + "Ficha_Negociacion_" + creditoID + ".pdf"
                'Dim rutaPDF2 As String = rutaDestino + "plantillaNegociacion.pdf"
                Dim rutaPDF2 As String = rutaDestino + "Nueva_plantilla_fichas.pdf"
                Dim file As FileInfo = New FileInfo(rutaPDF)

                If file.Exists Then
                    file.Delete()
                End If

                Dim pdfDoc As PdfDocument = New PdfDocument(New PdfReader(rutaPDF2), New PdfWriter(rutaPDF))
                Dim document As Document = New Document(pdfDoc)

                Dim firstpage As PdfPage = pdfDoc.GetPage(1)
                Dim pageSize As Rectangle = firstpage.GetPageSize
                Dim canvas As New PdfCanvas(firstpage)

                canvas.BeginText().SetFontAndSize(PdfFontFactory.CreateFont(StandardFonts.HELVETICA), 7).MoveText(pageSize.GetWidth() / 2 - 24, pageSize.GetHeight() - 10).ShowText(" ").EndText()
                putText(canvas, dataCred.Rows(0).Item("HIST_FI_FOLIOFICHA"), 680, pageSize.GetHeight() - 80, 10) 'FOLIO
                putText(canvas, dataCred.Rows(0).Item("HIST_FI_DESPACHONOMBRE"), 550, pageSize.GetHeight() - 95, 9) 'DESPACHO
                putText(canvas, dataCred.Rows(0).Item("HIST_FI_CREDITOID"), 322, pageSize.GetHeight() - 140, 8) 'Numero de credito
                putText(canvas, dataCred.Rows(0).Item("HIST_FI_CLIENTEID"), 330, pageSize.GetHeight() - 154, 8) 'NO CLIENTE
                putText(canvas, dataCred.Rows(0).Item("HIST_FI_FECHAPAGO"), 530, pageSize.GetHeight() - 154, 8) 'fecha
                putText(canvas, tmpCredito("PR_BI_NOMBRECOMPLETO"), 380, pageSize.GetHeight() - 168, 8) 'Nombre del cliente
                putText(canvas, dataCred.Rows(0).Item("HIST_FI_TIPOFICHA"), 260, pageSize.GetHeight() - 185, 8) 'Tipo de ficha
                putText(canvas, dataCred.Rows(0).Item("HIST_FI_TIPOPAGO"), 260, pageSize.GetHeight() - 200, 8) 'Tipo de pago
                'putText(canvas, FormatCurrency(dataCred.Rows(0).Item("V_VARIABLE1"), 2), 260, pageSize.GetHeight() - 163) 'capital importe
                'putText(canvas, FormatCurrency(dataCred.Rows(0).Item("V_VARIABLE2"), 2), 380, pageSize.GetHeight() - 163) 'capital iva
                putText(canvas, FormatCurrency(Val(dataCred.Rows(0).Item("V_VARIABLE3")) + Val(dataCred.Rows(0).Item("V_VARIABLE6")) + Val(dataCred.Rows(0).Item("V_VARIABLE9")) + Val(dataCred.Rows(0).Item("V_VARIABLE12")), 2), 390, pageSize.GetHeight() - 231) 'capital total
                'putText(canvas, FormatCurrency(dataCred.Rows(0).Item("V_VARIABLE4"), 2), 260, pageSize.GetHeight() - 173) 'interes normal importe
                'putText(canvas, FormatCurrency(dataCred.Rows(0).Item("V_VARIABLE5"), 2), 380, pageSize.GetHeight() - 173) 'interes normal iva
                'putText(canvas, FormatCurrency(dataCred.Rows(0).Item("V_VARIABLE6"), 2), 490, pageSize.GetHeight() - 173) 'interes normal total
                putText(canvas, dataCred.Rows(0).Item("HIST_FI_FECHAPAGO"), 690, pageSize.GetHeight() - 247, 8) 'fecha de pago
                'putText(canvas, FormatCurrency(dataCred.Rows(0).Item("V_VARIABLE7"), 2), 260, pageSize.GetHeight() - 184) 'Interes moratorio importe
                'putText(canvas, FormatCurrency(dataCred.Rows(0).Item("V_VARIABLE8"), 2), 380, pageSize.GetHeight() - 184) 'Interes moratorio iva
                'putText(canvas, FormatCurrency(dataCred.Rows(0).Item("V_VARIABLE9"), 2), 490, pageSize.GetHeight() - 184) 'Interes moratorio total
                putText(canvas, dataCred.Rows(0).Item("HIST_FI_USUARIOENVIONOMBRE"), 600, pageSize.GetHeight() - 265, 8) 'Nombre del noegociador
                'putText(canvas, FormatCurrency(dataCred.Rows(0).Item("V_VARIABLE10"), 2), 260, pageSize.GetHeight() - 195) 'comision impago importe
                'putText(canvas, FormatCurrency(dataCred.Rows(0).Item("V_VARIABLE11"), 2), 380, pageSize.GetHeight() - 195) 'comision impago iva
                'putText(canvas, FormatCurrency(dataCred.Rows(0).Item("V_VARIABLE12"), 2), 490, pageSize.GetHeight() - 195) 'comision impago total
                'putText(canvas, FormatCurrency(dataCred.Rows(0).Item("V_VARIABLE13"), 2), 260, pageSize.GetHeight() - 208) 'gasto de cobranza importe
                'putText(canvas, FormatCurrency(dataCred.Rows(0).Item("V_VARIABLE14"), 2), 380, pageSize.GetHeight() - 208) 'gasto de cobranza iva
                putText(canvas, FormatCurrency(dataCred.Rows(0).Item("V_VARIABLE15"), 2), 390, pageSize.GetHeight() - 246) 'gasto de cobranza total
                'putText(canvas, FormatCurrency(dataCred.Rows(0).Item("V_VARIABLE16"), 2), 260, pageSize.GetHeight() - 218) 'honorarios importe
                'putText(canvas, FormatCurrency(dataCred.Rows(0).Item("V_VARIABLE17"), 2), 380, pageSize.GetHeight() - 218) 'honorarios iva
                putText(canvas, FormatCurrency(dataCred.Rows(0).Item("V_VARIABLE18"), 2), 390, pageSize.GetHeight() - 261) 'honorarios total
                'putText(canvas, FormatCurrency(dataCred.Rows(0).Item("V_VARIABLE19"), 2), 260, pageSize.GetHeight() - 231, 10) 'totales importe
                'putText(canvas, FormatCurrency(dataCred.Rows(0).Item("V_VARIABLE20"), 2), 380, pageSize.GetHeight() - 231, 10) 'totales iva
                putText(canvas, FormatCurrency(dataCred.Rows(0).Item("V_VARIABLE21"), 2), 390, pageSize.GetHeight() - 276, 10) 'totales total
                putText(canvas, "", 630, pageSize.GetHeight() - 225) 'Nombre y firma de quien pago el adeudo
                putText(canvas, FormatCurrency(dataCred.Rows(0).Item("V_VARIABLE22"), 2), 390, pageSize.GetHeight() - 306) 'Capital condonacion importe
                'putText(canvas, FormatCurrency(dataCred.Rows(0).Item("V_VARIABLE23"), 2), 380, pageSize.GetHeight() - 242) 'Capital condonacion iva
                'putText(canvas, FormatCurrency(dataCred.Rows(0).Item("V_VARIABLE24"), 2), 490, pageSize.GetHeight() - 242) 'Capital condonacion total
                putText(canvas, FormatCurrency(dataCred.Rows(0).Item("V_VARIABLE25"), 2), 390, pageSize.GetHeight() - 320) 'Interes normal condonado importe
                'putText(canvas, FormatCurrency(dataCred.Rows(0).Item("V_VARIABLE26"), 2), 380, pageSize.GetHeight() - 252) 'Interes normal condonado iva
                'putText(canvas, FormatCurrency(dataCred.Rows(0).Item("V_VARIABLE27"), 2), 490, pageSize.GetHeight() - 252) 'Interes normal condonado total
                putText(canvas, FormatCurrency(dataCred.Rows(0).Item("V_VARIABLE28"), 2), 390, pageSize.GetHeight() - 334) 'interes moratorio condonado importe
                'putText(canvas, FormatCurrency(dataCred.Rows(0).Item("V_VARIABLE29"), 2), 380, pageSize.GetHeight() - 264) 'interes moratorio condonado iva
                'putText(canvas, FormatCurrency(dataCred.Rows(0).Item("V_VARIABLE30"), 2), 490, pageSize.GetHeight() - 264) 'interes moratorio condonado total
                'If dataCred.Rows(0).Item("V_COMENTARIO") <> " " And dataCred.Rows(0).Item("V_COMENTARIO") <> "" Then
                If observaciones <> " " And observaciones <> "" Then
                    Dim arrayComment = observaciones.ToUpper.Split(" ")
                    Dim contador = 0
                    Dim renglon As String = ""
                    Dim numRenglon = 0
                    Dim separa = 360
                    For Each item As String In arrayComment
                        If item.Length >= 40 Then
                            putText(canvas, renglon, 522, pageSize.GetHeight() - separa) 'Observaciones
                            contador = 0
                            renglon = ""
                            separa = separa + 7
                            renglon = renglon & " " & item
                        Else
                            contador = contador + item.Length
                            If contador >= 40 Then
                                putText(canvas, renglon, 522, pageSize.GetHeight() - separa) 'Observaciones
                                contador = 0
                                renglon = ""
                                separa = separa + 7
                            End If
                            renglon = renglon & " " & item
                        End If


                    Next
                    'renglon = ""
                    separa = separa + 7
                    putText(canvas, renglon, 522, pageSize.GetHeight() - separa) 'Observaciones
                End If
                putText(canvas, FormatCurrency(dataCred.Rows(0).Item("V_VARIABLE31"), 2), 390, pageSize.GetHeight() - 348) 'Comision por impago condonada importe
                'putText(canvas, FormatCurrency(dataCred.Rows(0).Item("V_VARIABLE32"), 2), 380, pageSize.GetHeight() - 272) 'Comision por impago condonada iva
                'putText(canvas, FormatCurrency(dataCred.Rows(0).Item("V_VARIABLE33"), 2), 490, pageSize.GetHeight() - 272) 'Comision por impago condonada total
                putText(canvas, FormatCurrency(dataCred.Rows(0).Item("V_VARIABLE34"), 2), 390, pageSize.GetHeight() - 362, 10) 'totales importe
                'putText(canvas, FormatCurrency(dataCred.Rows(0).Item("V_VARIABLE35"), 2), 380, pageSize.GetHeight() - 285, 10) 'totales iva
                'putText(canvas, FormatCurrency(dataCred.Rows(0).Item("V_VARIABLE36"), 2), 490, pageSize.GetHeight() - 285, 10) 'totales total
                'putText(canvas, "CUENTA_EJE", 260, pageSize.GetHeight() - 308) 'Cuenta eje
                putText(canvas, MTOCTASAHO, 260, pageSize.GetHeight() - 393) 'cUENTA DE HABERES
                putText(canvas, dataCred.Rows(0).Item("PORCCAP"), 490, pageSize.GetHeight() - 393) '% Capital condonado
                'putText(canvas, "CUENTA_AHORRO", 260, pageSize.GetHeight() - 319) 'Cuenta ahorro
                putText(canvas, dataCred.Rows(0).Item("PORCINT"), 490, pageSize.GetHeight() - 407) '% Interes normal condonado
                'putText(canvas, "CUENTA_RECIP", 260, pageSize.GetHeight() - 330) 'Cuenta reciprocidad
                putText(canvas, dataCred.Rows(0).Item("PORCMORA"), 490, pageSize.GetHeight() - 421) '% Interes moratorio condonado
                putText(canvas, dataCred.Rows(0).Item("PORCCOMIS"), 490, pageSize.GetHeight() - 435) '% Comision de impago condonado
                'putText(canvas, FormatCurrency(dataCred.Rows(0).Item("V_VARIABLE36"), 2), 460, pageSize.GetHeight() - 449, 10) 'Totales condonados
                putText(canvas, FormatCurrency(dataCred.Rows(0).Item("V_BASEHONORARIO"), 2), 460, pageSize.GetHeight() - 449) 'Base de honorarios
                putText(canvas, dataCred.Rows(0).Item("HIST_FI_PORCHONORARIO"), 490, pageSize.GetHeight() - 463) '% Honorarios cobrado
                putText(canvas, FormatCurrency(dataCred.Rows(0).Item("HIST_FI_TOTALPAGAR"), 2), 460, pageSize.GetHeight() - 477) 'Total a depositar
                putText(canvas, tmpCredito("PR_BI_DIASATRASO"), 630, pageSize.GetHeight() - 477) 'Dias mora
                '   //Close document
                pdfDoc.Close()
            End If
        End If
    End Sub

    Private Sub putText(ByRef canvas As PdfCanvas, texto As String, x As Integer, y As Integer, Optional textSize As Integer = 7, Optional font As String = "Helvetica")
        canvas.BeginText().SetFontAndSize(PdfFontFactory.CreateFont(font), textSize).MoveText(x, y).ShowText(texto).EndText()
    End Sub

    Public Sub CreateFichaSimulacion(ByVal folioFicha As String, ByVal tipoFicha As String, ByVal usuarioEnvioNombre As String, ByVal usuarioIP As String, ByVal clienteID As String, ByVal creditoID As String, ByVal tipoPago As String, ByVal totalPagar As String, ByVal fechaPago As String, ByVal observaciones As String, ByVal valorNetoActivo As String, ByVal capital As String, ByVal interes As String, ByVal ivaInteres As String, ByVal moratorio As String, ByVal ivaMoratorio As String, ByVal comisiones As String, ByVal ivaComisiones As String, ByVal cC_comisiones As String, ByVal cC_moratorios As String, ByVal cC_interes As String, ByVal cC_capital As String, ByVal gastosCobranza As String, ByVal ivaGastosCob As String, ByVal honorario As String, ByVal despachoID As String, ByVal despachoNombre As String, ByVal tipoDespacho As String, ByVal abogadoID As String, ByVal abogadoNombre As String, ByVal supervisorID As String, ByVal supervisorNombre As String, ByVal folioDacion As String, ByVal descripcion As String, ByVal valorComercial As String, ByVal valorAplicar As String, ByVal numCuenta As String, ByVal monto As String, ByVal CONDOCOMIS As String, ByVal CONDOMORA As String, ByVal CONDOINT As String, ByVal CONDOCAP As String, ByVal MTOCTASAHO As String, ByVal totcondos As String, ByVal basehono As String, ByVal porchono As String, ByVal totdepo As String, ByVal diasmorasim As String)

        Dim rutaPDF As String = rutaDestino + "Ficha_Simulacion_" + creditoID + ".pdf"
        'Dim rutaPDF2 As String = rutaDestino + "plantillaNegociacion.pdf"
        Dim rutaPDF2 As String = rutaDestino + "Ficha_Simulacion.pdf"
        Dim file As FileInfo = New FileInfo(rutaPDF)

        If file.Exists Then
            file.Delete()
        End If

        Dim pdfDoc As PdfDocument = New PdfDocument(New PdfReader(rutaPDF2), New PdfWriter(rutaPDF))
        Dim document As Document = New Document(pdfDoc)

        Dim firstpage As PdfPage = pdfDoc.GetPage(1)
        Dim pageSize As Rectangle = firstpage.GetPageSize
        Dim canvas As New PdfCanvas(firstpage)

        canvas.BeginText().SetFontAndSize(PdfFontFactory.CreateFont(StandardFonts.HELVETICA), 7).MoveText(pageSize.GetWidth() / 2 - 24, pageSize.GetHeight() - 10).ShowText(" ").EndText()
        putText(canvas, despachoNombre, 550, pageSize.GetHeight() - 95, 9) 'DESPACHO
        putText(canvas, creditoID, 322, pageSize.GetHeight() - 140, 8) 'Numero de credito
        putText(canvas, clienteID, 330, pageSize.GetHeight() - 154, 8) 'NO CLIENTE
        putText(canvas, fechaPago, 530, pageSize.GetHeight() - 154, 8) 'fecha
        putText(canvas, HttpUtility.HtmlDecode(tmpCredito("PR_BI_NOMBRECOMPLETO")), 380, pageSize.GetHeight() - 168, 8) 'Nombre del cliente
        putText(canvas, tipoFicha, 260, pageSize.GetHeight() - 185, 8) 'Tipo de ficha
        putText(canvas, tipoPago, 260, pageSize.GetHeight() - 200, 8) 'Tipo de pago
        putText(canvas, FormatCurrency(Val(capital) + Val(interes) + Val(ivaInteres) + Val(moratorio) + Val(ivaMoratorio) + Val(comisiones) + Val(ivaComisiones), 2), 390, pageSize.GetHeight() - 231) 'capital total        
        putText(canvas, fechaPago, 690, pageSize.GetHeight() - 247, 8) 'fecha de pago        
        putText(canvas, HttpUtility.HtmlDecode(usuarioEnvioNombre), 600, pageSize.GetHeight() - 265, 8) 'Nombre del noegociador        
        putText(canvas, FormatCurrency(gastosCobranza, 2), 390, pageSize.GetHeight() - 246) 'gasto de cobranza total        
        putText(canvas, FormatCurrency(honorario, 2), 390, pageSize.GetHeight() - 261) 'honorarios total
        putText(canvas, FormatCurrency(totalPagar, 2), 390, pageSize.GetHeight() - 276, 10) 'totales total
        putText(canvas, "", 630, pageSize.GetHeight() - 225) 'Nombre y firma de quien pago el adeudo
        putText(canvas, FormatCurrency(cC_capital, 2), 390, pageSize.GetHeight() - 306) 'Capital condonacion importe        
        putText(canvas, FormatCurrency(cC_interes, 2), 390, pageSize.GetHeight() - 320) 'Interes normal condonado importe        
        putText(canvas, FormatCurrency(cC_moratorios, 2), 390, pageSize.GetHeight() - 334) 'interes moratorio condonado importe        
        If observaciones <> " " And observaciones <> "" Then
            Dim arrayComment = observaciones.ToString.Split(" ")
            Dim contador = 0
            Dim renglon As String = ""
            Dim numRenglon = 0
            Dim separa = 360
            For Each item As String In arrayComment
                If contador = 7 Then
                    putText(canvas, renglon, 522, pageSize.GetHeight() - separa) 'Observaciones
                    contador = 0
                    renglon = ""
                    separa = separa + 7
                End If
                renglon = renglon & " " & item
                contador = contador + 1
            Next
            'renglon = ""
            separa = separa + 7
            putText(canvas, renglon, 522, pageSize.GetHeight() - separa) 'Observaciones
        End If
        putText(canvas, FormatCurrency(cC_comisiones, 2), 390, pageSize.GetHeight() - 348) 'Comision por impago condonada importe
        'putText(canvas, FormatCurrency(dataCred.Rows(0).Item("V_VARIABLE32"), 2), 380, pageSize.GetHeight() - 272) 'Comision por impago condonada iva
        'putText(canvas, FormatCurrency(dataCred.Rows(0).Item("V_VARIABLE33"), 2), 490, pageSize.GetHeight() - 272) 'Comision por impago condonada total
        putText(canvas, FormatCurrency(totcondos, 2), 390, pageSize.GetHeight() - 362, 10) 'totales condonados
        'putText(canvas, FormatCurrency(dataCred.Rows(0).Item("V_VARIABLE35"), 2), 380, pageSize.GetHeight() - 285, 10) 'totales iva
        'putText(canvas, FormatCurrency(dataCred.Rows(0).Item("V_VARIABLE36"), 2), 490, pageSize.GetHeight() - 285, 10) 'totales total
        'putText(canvas, "CUENTA_EJE", 260, pageSize.GetHeight() - 308) 'Cuenta eje
        putText(canvas, MTOCTASAHO, 260, pageSize.GetHeight() - 393) 'cUENTA DE HABERES
        putText(canvas, CONDOCAP, 490, pageSize.GetHeight() - 393) '% Capital condonado
        'putText(canvas, "CUENTA_AHORRO", 260, pageSize.GetHeight() - 319) 'Cuenta ahorro
        putText(canvas, CONDOINT, 490, pageSize.GetHeight() - 407) '% Interes normal condonado
        'putText(canvas, "CUENTA_RECIP", 260, pageSize.GetHeight() - 330) 'Cuenta reciprocidad
        putText(canvas, CONDOMORA, 490, pageSize.GetHeight() - 421) '% Interes moratorio condonado
        putText(canvas, CONDOCOMIS, 490, pageSize.GetHeight() - 435) '% Comision de impago condonado
        'putText(canvas, FormatCurrency(totcondos, 2), 460, pageSize.GetHeight() - 449, 10) 'Totales condonados
        putText(canvas, FormatCurrency(basehono, 2), 460, pageSize.GetHeight() - 449) 'Base de honorarios
        putText(canvas, porchono, 490, pageSize.GetHeight() - 463) '% Honorarios cobrado
        putText(canvas, FormatCurrency(totdepo, 2), 460, pageSize.GetHeight() - 477) 'Total a depositar
        putText(canvas, diasmorasim, 630, pageSize.GetHeight() - 477) 'Dias mora
        '   //Close document
        pdfDoc.Close()
    End Sub
End Class
