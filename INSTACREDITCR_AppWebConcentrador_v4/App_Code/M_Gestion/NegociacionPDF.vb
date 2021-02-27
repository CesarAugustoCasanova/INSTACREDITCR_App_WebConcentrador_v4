Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports iText.Kernel.Pdf
Imports iText.Layout
Imports iText.Layout.Element
Imports iText
Imports iText.Forms
Imports iText.Forms.Fields
Imports iText.Kernel.Geom
Imports iText.Kernel.Pdf.Canvas
Imports iText.Kernel.Font
Imports iText.IO.Font.Constants
Public Class NegociacionPDF
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

    Public Sub CreateFichaNegociacion(ByVal interes_bala As String, ByVal estimacion As String, ByVal valdeuda As String, ByVal inmora As String, ByVal valneto As String, ByVal intnor As String, Optional bandera As Integer = -1)
        Dim dataCred As Data.DataTable = Class_Fichas.LlenarPDF2(creditoID, bandera)
        If dataCred IsNot Nothing Then
            If dataCred.Rows.Count > 0 Then
                Dim ro As Data.DataRow = dataCred.Rows(0)
                Dim rutaPDF As String = rutaDestino + "Ficha_Negociacion_Dos_" + creditoID + ".pdf"
                Dim rutaPDF2 As String = rutaDestino + "PlantillaNegociacionDos.pdf"
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
                putText(canvas, ro.Item("HIST_FI_FOLIOFICHA"), 585, pageSize.GetHeight() - 75, 10) 'FOLIO
                putText(canvas, ro.Item("HIST_FI_DESPACHONOMBRE"), 500, pageSize.GetHeight() - 89, 9) 'DESPACHO
                putText(canvas, "Pendiente", 550, pageSize.GetHeight() - 110, 8) 'ESTATUS
                putText(canvas, ro.Item("HIST_FI_CREDITOID"), 265, pageSize.GetHeight() - 125, 8) 'Numero de credito
                putText(canvas, ro.Item("HIST_FI_CLIENTEID"), 265, pageSize.GetHeight() - 142, 8) 'NO CLIENTE
                putText(canvas, Today.ToShortDateString, 550, pageSize.GetHeight() - 142, 10) 'fecha
                putText(canvas, ro.Item("PR_BI_NOMBRECOMPLETO"), 400, pageSize.GetHeight() - 157, 10) 'Nombre del cliente
                putText(canvas, ro.Item("PR_BI_ESTATUSCREDITO"), 265, pageSize.GetHeight() - 170, 8) 'Situacion del credito
                putText(canvas, "03", 190, pageSize.GetHeight() - 184, 8) 'Tipo de ficha
                putText(canvas, "T", 190, pageSize.GetHeight() - 194, 8) 'Tipo de pago

                putText(canvas, FormatCurrency(ro.Item("HIST_FI_CAPITAL"), 2), 243, pageSize.GetHeight() - 225) 'importe capital importe
                ' putText(canvas, FormatCurrency(ro.Item("HIST_FI_CAPITAL") * 0.16, 2), 300, pageSize.GetHeight() - 222) 'importe capital iva
                ' putText(canvas, FormatCurrency(ro.Item("HIST_FI_CAPITAL") + (ro.Item("HIST_FI_CAPITAL") * 0.16), 2), 410, pageSize.GetHeight() - 222) 'importe capital total

                putText(canvas, FormatCurrency(0, 2), 243, pageSize.GetHeight() - 242) 'interes de blance importe
                'putText(canvas, FormatCurrency(0 * 0.16, 2), 300, pageSize.GetHeight() - 233) 'interes de balance iva
                'putText(canvas, FormatCurrency(0 + (interes_bala * 0.16), 2), 410, pageSize.GetHeight() - 233) 'interes de blance total

                putText(canvas, ro.Item("HIST_FI_FECHAPAGO"), 520, pageSize.GetHeight() - 227) 'fecha de pago

                putText(canvas, FormatCurrency(estimacion, 2), 243, pageSize.GetHeight() - 257) 'estimacion importe
                'putText(canvas, FormatCurrency(estimacion * 0.16, 2), 300, pageSize.GetHeight() - 246) 'estimacion iva
                'putText(canvas, FormatCurrency(estimacion + (estimacion * 0.16), 2), 410, pageSize.GetHeight() - 246) 'estimacion total

                putText(canvas, ro.Item("HIST_FI_USUARIOENVIONOMBRE"), 450, pageSize.GetHeight() - 242) 'Nombre del noegociador

                putText(canvas, FormatCurrency(0, 2), 243, pageSize.GetHeight() - 272) 'gastos de cobranza importe
                'putText(canvas, FormatCurrency(0 * 0.16, 2), 300, pageSize.GetHeight() - 257) 'gastos de cobranza iva
                'putText(canvas, FormatCurrency(0 + (0 * 0.16), 2), 410, pageSize.GetHeight() - 257) 'gastos de cobranza total

                putText(canvas, FormatCurrency("0", 2), 243, pageSize.GetHeight() - 286) 'Honorarios importe
                'putText(canvas, FormatCurrency("0", 2), 300, pageSize.GetHeight() - 272) 'honorarios iva
                'putText(canvas, FormatCurrency("0", 2), 410, pageSize.GetHeight() - 272) 'honorarios total

                putText(canvas, FormatCurrency("0", 2), 243, pageSize.GetHeight() - 300) 'intereses normales importe
                ' putText(canvas, FormatCurrency("0", 2), 300, pageSize.GetHeight() - 286) 'intereses normales iva
                'putText(canvas, FormatCurrency("0", 2), 410, pageSize.GetHeight() - 286) 'intereses normales total

                putText(canvas, FormatCurrency(0, 2), 243, pageSize.GetHeight() - 312) 'intereses moratorios importe
                ' putText(canvas, FormatCurrency(0, 2), 300, pageSize.GetHeight() - 300) 'intereses moratorios iva
                ' putText(canvas, FormatCurrency(0, 2), 410, pageSize.GetHeight() - 300) 'intereses moratorios total

                putText(canvas, " ", 530, pageSize.GetHeight() - 295) 'Nombre y firma de quien pago el adeudo

                putText(canvas, FormatCurrency("0", 2), 243, pageSize.GetHeight() - 326) 'Comision por impago importe
                '  putText(canvas, FormatCurrency("0", 2), 300, pageSize.GetHeight() - 312) 'Comision por impago iva
                ' putText(canvas, FormatCurrency("0", 2), 410, pageSize.GetHeight() - 312) 'Comision por impago total

                putText(canvas, FormatCurrency(valdeuda, 2), 243, pageSize.GetHeight() - 340) 'Valor de la deuda importe
                'putText(canvas, FormatCurrency(valdeuda * 0.16, 2), 300, pageSize.GetHeight() - 324) 'Valor de la deuda iva
                ' putText(canvas, FormatCurrency(valdeuda + (valdeuda * 0.16), 2), 410, pageSize.GetHeight() - 324) 'Valor de la deuda total

                putText(canvas, FormatCurrency(ro.Item("HIST_FI_VALORAPLICAR"), 2), 243, pageSize.GetHeight() - 356) 'Valor de bien adjudicado importe
                '  putText(canvas, FormatCurrency(ro.Item("HIST_FI_VALORAPLICAR") * 0.16, 2), 300, pageSize.GetHeight() - 336) 'Valor de bien adjudicado iva
                '  putText(canvas, FormatCurrency(ro.Item("HIST_FI_VALORAPLICAR") + (ro.Item("HIST_FI_VALORAPLICAR") * 0.16), 2), 410, pageSize.GetHeight() - 336) 'valor de bien adjudicado total

                putText(canvas, FormatCurrency(valneto, 2), 243, pageSize.GetHeight() - 367) 'Valor neto del activo importe
                '  putText(canvas, FormatCurrency(valneto * 0.16, 2), 300, pageSize.GetHeight() - 348) 'Valor neto del activo iva
                ' putText(canvas, FormatCurrency(valneto + (valneto * 0.16), 2), 410, pageSize.GetHeight() - 348) 'valor neto del activo total

                putText(canvas, FormatCurrency(0, 2), 243, pageSize.GetHeight() - 380) 'importe del capital condonado importe
                ' putText(canvas, FormatCurrency(0, 2), 300, pageSize.GetHeight() - 362) 'importe del capital condonado iva
                ' putText(canvas, FormatCurrency(0, 2), 410, pageSize.GetHeight() - 362) 'importe del capital condonado total

                putText(canvas, FormatCurrency(interes_bala, 2), 243, pageSize.GetHeight() - 393) 'interes de balance condonado importe
                ' putText(canvas, FormatCurrency(interes_bala * 0.16, 2), 300, pageSize.GetHeight() - 376) 'interes de balance condonado iva
                ' putText(canvas, FormatCurrency(interes_bala + (interes_bala * 0.16), 2), 410, pageSize.GetHeight() - 376) 'interes de balance condonado total

                putText(canvas, FormatCurrency(intnor, 2), 243, pageSize.GetHeight() - 405) 'intereses normales condonado importe
                ' putText(canvas, FormatCurrency(intnor * 0.16, 2), 300, pageSize.GetHeight() - 388) 'intereses normales condonado iva
                '  putText(canvas, FormatCurrency(intnor + (intnor * 0.16), 2), 410, pageSize.GetHeight() - 388) 'intereses normales condonado total

                putText(canvas, FormatCurrency(inmora, 2), 243, pageSize.GetHeight() - 417) 'interes moratorio condonado importe
                ' putText(canvas, FormatCurrency(inmora * 0.16, 2), 300, pageSize.GetHeight() - 404) 'interes moratorio condonado iva
                ' putText(canvas, FormatCurrency(inmora + (inmora * 16), 2), 410, pageSize.GetHeight() - 404) 'interes moratorio condonado total

                putText(canvas, FormatCurrency(0, 2), 243, pageSize.GetHeight() - 428) 'comision por impago condonado importe
                ' putText(canvas, FormatCurrency("0", 2), 300, pageSize.GetHeight() - 416) 'comision por impago condonado iva
                '  putText(canvas, FormatCurrency("0", 2), 410, pageSize.GetHeight() - 416) 'comision por impago condonado total

                putText(canvas, FormatCurrency(Double.Parse(ro.Item("HIST_FI_CAPITAL")) + Double.Parse(estimacion) + Double.Parse(valdeuda) + Double.Parse(valneto) + Double.Parse(interes_bala) + Double.Parse(intnor) + Double.Parse(inmora), 2), 243, pageSize.GetHeight() - 440) 'TOTALES importe
                '  putText(canvas, FormatCurrency(Double.Parse((ro.Item("HIST_FI_CAPITAL") * 0.16)) + (Double.Parse(estimacion * 0.16)) + (Double.Parse(valdeuda * 0.16)) + (Double.Parse(valneto * 0.16)) + (Double.Parse(interes_bala * 0.16)) + (Double.Parse(intnor * 0.16)) + (Double.Parse(inmora * 0.16)), 2), 300, pageSize.GetHeight() - 426) 'TOTALES iva
                ' putText(canvas, FormatCurrency(Double.Parse((ro.Item("HIST_FI_CAPITAL") + (ro.Item("HIST_FI_CAPITAL") * 0.16)) + (estimacion + (estimacion * 0.16)) + (valdeuda + (valdeuda * 0.16)) + (valneto + (valneto * 0.16)) + (interes_bala + (interes_bala * 0.16)) + (intnor + (intnor * 0.16)) + (inmora + (inmora * 0.16))), 2), 410, pageSize.GetHeight() - 426) 'TOTALES total

                putText(canvas, FormatCurrency((Double.Parse(estimacion) + (Double.Parse(estimacion) * 0.16)) + (Double.Parse(valdeuda) + (Double.Parse(valdeuda) * 0.16)) + (Double.Parse(valneto) + (Double.Parse(valneto) * 0.16)) + (Double.Parse(interes_bala) + (Double.Parse(interes_bala) * 0.16)) + (Double.Parse(intnor) + (Double.Parse(intnor) * 0.16)) + (Double.Parse(inmora) + (Double.Parse(inmora) * 0.16)), 2), 243, pageSize.GetHeight() - 467) 'Totales condonados
                putText(canvas, FormatCurrency("0", 2), 243, pageSize.GetHeight() - 483) 'Base de honorarios
                putText(canvas, "0%", 243, pageSize.GetHeight() - 497) '% honorarios cobrado
                putText(canvas, FormatCurrency(valneto, 2), 243, pageSize.GetHeight() - 510) 'Total

                Dim arrayComment = ro.Item("HIST_FI_OBSERVACIONES")
                Dim contador = 0
                Dim renglon As String = ""
                Dim numRenglon = 0
                Dim separa = 335
                For Each item As String In arrayComment
                    If contador = 20 Then
                        putText(canvas, renglon, 400, pageSize.GetHeight() - separa, 10) 'Observaciones
                        contador = 0
                        renglon = ""
                        separa = separa + 11
                    End If
                    renglon = renglon & " " & item
                    contador = contador + 1
                Next
                renglon = ""
                separa = separa + 11
                putText(canvas, renglon, 365, pageSize.GetHeight() - separa) 'Observaciones
                '   //Close document
                pdfDoc.Close()
            End If
        End If
    End Sub

    Private Sub putText(ByRef canvas As PdfCanvas, texto As String, x As Integer, y As Integer, Optional textSize As Integer = 7, Optional font As String = "Helvetica")
        canvas.BeginText().SetFontAndSize(PdfFontFactory.CreateFont(font), textSize).MoveText(x, y).ShowText(texto).EndText()
    End Sub
End Class
