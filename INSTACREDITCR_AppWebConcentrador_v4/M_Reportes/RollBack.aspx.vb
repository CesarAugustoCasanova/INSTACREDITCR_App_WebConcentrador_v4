Imports Funciones

Imports System.Data
Imports System.Web.Services
Imports Telerik.Web.Spreadsheet
Imports Telerik.Web.UI
Imports WorkSheetHelper


Partial Class RollBack
    Inherits System.Web.UI.Page

    Private Sub RollBack_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then

            RPFechaReporte.MaxDate = Today
    
        End If

    End Sub

    Private Sub LlenarSS()
        Try
            Dim sheets As New List(Of Worksheet)

            sheets.Add(New RollBackGenerator(hoja:=0, subproducto:=rcbSubProd.SelectedValue, Fecha:=CType(RPFechaReporte.SelectedDate, DateTime).ToShortDateString).generate())

            sheets.Add(New RollBackGenerator(hoja:=1, subproducto:=rcbSubProd.SelectedValue, Fecha:=CType(RPFechaReporte.SelectedDate, DateTime).ToShortDateString).generate())

            Dim ds As DataTable = SP.RP_ROLLBACK(3, subproducto:=rcbSubProd.SelectedValue, v_fecha:=CType(RPFechaReporte.SelectedDate, DateTime).ToShortDateString)

            sheets.Add(New RollBackGenerator(hoja:=2, data:=ds).generate())

            ds = SP.RP_ROLLBACK(4, subproducto:=rcbSubProd.SelectedValue, v_fecha:=CType(RPFechaReporte.SelectedDate, DateTime).ToShortDateString)
            sheets.Add(New RollBackGenerator(hoja:=3, data:=ds).generate())

            ssRollBack.Visible = True
            For Each sheet As Worksheet In sheets
                ssRollBack.Sheets.Add(sheet)
            Next

        Catch ex As Exception
            ssRollBack.Visible = False
            Dim gg As String = ex.Message
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "script", "console.error('" & gg & "');", True)
            Funciones.showModal(notificacion1, "deny", "Error", "Error de servidor. Intente de nuevo.")
        End Try

    End Sub

    Private Sub rcbSubProd_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbSubProd.SelectedIndexChanged
        'LlenarSS()
    End Sub

    Private Sub btnGenerarReporte_Click(sender As Object, e As EventArgs) Handles btnGenerarReporte.Click
        LlenarSS()
    End Sub

End Class

Class RollBackGenerator
    Private data As DataTable
    Private sheet As Worksheet
    Private hoja As Integer
    Private subproducto As String
    Private Fecha As String


    Private colors() As String = {"#C5D9F1", "#EBF1DE", "#F2DCDB", "#E4DFEC", "#FFFFCC", "#FDE9D9", "#DAEEF3", "#DDD9C4", "white", "white", "white"}

    ''' <summary>
    ''' Genera una hoja de excel con la informacion que se proporciona y se le da formato dependiendo de su s.
    ''' </summary>
    ''' <param name="hoja">Base 0. El index de hoja que se va a llenar </param>
    Public Sub New(hoja As Integer, Optional data As DataTable = Nothing, Optional subproducto As String = "", Optional Fecha As String = "")
        Me.data = data
        Me.hoja = hoja
        Me.subproducto = subproducto
        Me.Fecha = Fecha
    End Sub

    Public Function generate() As Worksheet
        Select Case hoja
            Case 0
                ejecutivo("x Ejecutivo cta")
            Case 1
                ejecutivo("x Ejecutivo saldo")
            Case 2
                ctasMigraronBucket()
            Case 3
                Pagos()
        End Select
        Return sheet
    End Function

    Private Function getStartDate() As String
        Dim fecha As Date = Now

        While fecha.AddDays(-1).Day <> 12
            fecha = fecha.AddDays(-1)
        End While

        Return fecha.ToLongDateString
    End Function

    Private Sub generateEjecutivoHeader()
        print(sheet.Rows(0).Cells(0), "Roll Back- Buckets - (New Portfolio)")
        sheet.Rows(0).Cells(0).Bold = True
        sheet.AddMergedCells("A1:M1")

        sheet.AddRow(crearRow(21))
        sheet.AddRow(crearRow(21))
        print(sheet.Rows(2).Cells(0), "Start date")
        sheet.AddMergedCells("A3:C3")
        print(sheet.Rows(2).Cells(3), "Date of the Report")
        sheet.AddMergedCells("D3:G3")
        print(sheet.Rows(2).Cells(7), "Date of payments referred")
        sheet.AddMergedCells("H3:M3")

        sheet.AddRow(crearRow(21))
        print(sheet.Rows(3).Cells(0), getStartDate())
        sheet.AddMergedCells("A4:C4")
        print(sheet.Rows(3).Cells(3), Now.ToLongDateString)
        sheet.AddMergedCells("D4:G4")
        print(sheet.Rows(3).Cells(7), getStartDate() & " To " & Now.AddDays(-2).ToLongDateString)
        sheet.AddMergedCells("H4:M4")

        sheet.AddRow(crearRow(21))
        sheet.AddRow(crearRow(21))
        print(sheet.Rows(5).Cells(3), "Bucket 0")
        print(sheet.Rows(5).Cells(4), "Bucket 1", "rgb(0,112,192)")
        print(sheet.Rows(5).Cells(5), "Bucket 2", "rgb(0,176,80)")
        print(sheet.Rows(5).Cells(6), "Bucket 3", "rgb(255,0,0)")
        print(sheet.Rows(5).Cells(7), "Bucket 4", "rgb(112,48,160)")
        print(sheet.Rows(5).Cells(8), "Bucket 5", "rgb(255,255,0)")
        print(sheet.Rows(5).Cells(9), "Bucket 6", "rgb(226,107,10)")
        print(sheet.Rows(5).Cells(10), "Bucket 7", "rgb(49,134,155)")
        print(sheet.Rows(5).Cells(11), "Bucket 8", "rgb(196,189,151)")
        print(sheet.Rows(5).Cells(12), "Total")
    End Sub

    Private Sub ejecutivoTotal()
        Dim ds As DataTable = SP.RP_ROLLBACK(1, subproducto:=subproducto, tipo:=IIf(hoja = 0, "cuentas", "saldos"), v_fecha:=Fecha)
        For Each dr As DataRow In ds.Rows
            Dim row As New Row
            row.AddCell(New Cell() With {.Value = dr("titulo").ToString})
            row.AddCell(New Cell() With {.Value = ""})
            row.AddCell(New Cell() With {.Value = ""})
            row.AddCell(New Cell() With {.Value = ""})
            row.AddCell(New Cell() With {.Value = StringToDecimal(dr("BUCKET_1").ToString), .Background = colors(0), .Format = GetFormatString(dr("FBUCKET_1").ToString)})
            row.AddCell(New Cell() With {.Value = StringToDecimal(dr("BUCKET_2").ToString), .Background = colors(1), .Format = GetFormatString(dr("FBUCKET_2").ToString)})
            row.AddCell(New Cell() With {.Value = StringToDecimal(dr("BUCKET_3").ToString), .Background = colors(2), .Format = GetFormatString(dr("FBUCKET_3").ToString)})
            row.AddCell(New Cell() With {.Value = StringToDecimal(dr("BUCKET_4").ToString), .Background = colors(3), .Format = GetFormatString(dr("FBUCKET_4").ToString)})
            row.AddCell(New Cell() With {.Value = StringToDecimal(dr("BUCKET_5").ToString), .Background = colors(4), .Format = GetFormatString(dr("FBUCKET_5").ToString)})
            row.AddCell(New Cell() With {.Value = StringToDecimal(dr("BUCKET_6").ToString), .Background = colors(5), .Format = GetFormatString(dr("FBUCKET_6").ToString)})
            row.AddCell(New Cell() With {.Value = StringToDecimal(dr("BUCKET_7").ToString), .Background = colors(6), .Format = GetFormatString(dr("FBUCKET_7").ToString)})
            row.AddCell(New Cell() With {.Value = StringToDecimal(dr("BUCKET_8").ToString), .Background = colors(7), .Format = GetFormatString(dr("FBUCKET_8").ToString)})
            row.AddCell(New Cell() With {.Value = StringToDecimal(dr("Total").ToString), .Format = GetFormatString(dr("FTOTAL").ToString)})
            sheet.AddRow(row)
        Next
        sheet.AddRow(New Row)
    End Sub

    Private Sub ejecutivoBody()
        Dim getColor = Function(bucket As Integer, columna As Integer) As String
                           Dim color As String
                           If bucket > columna Then
                               color = colors(bucket - 1)
                           Else
                               color = colors(columna)
                           End If
                           Return color
                       End Function
        Dim getBold = Function(text As String) As Boolean
                          Return text.Contains("RollBk") Or text.Contains("Number Accounts")
                      End Function
        For bucket = 1 To 8
            Dim ds As DataTable = SP.RP_ROLLBACK(2, subproducto:=subproducto, tipo:=IIf(hoja = 0, "cuentas", "saldos"), v_bucket:=bucket, v_fecha:=Fecha)
            For Each dr As DataRow In ds.Rows
                Dim row As New Row
                row.AddCell(New Cell() With {.Value = dr("TITULO").ToString, .Background = getColor(bucket, 0)})
                row.AddCell(New Cell() With {.Value = StringToDecimal(dr("ESPACIO1").ToString), .Bold = getBold(dr("TITULO").ToString), .Background = getColor(bucket, 0), .Format = GetFormatString(dr("FESPACIO1").ToString)})
                row.AddCell(New Cell() With {.Value = StringToDecimal(dr("ESPACIO2").ToString), .Bold = getBold(dr("TITULO").ToString), .Background = getColor(bucket, 0), .Format = GetFormatString(dr("FESPACIO2").ToString)})
                row.AddCell(New Cell() With {.Value = StringToDecimal(dr("BUCKET_0").ToString), .Bold = getBold(dr("TITULO").ToString), .Background = getColor(bucket, 0), .Format = GetFormatString(dr("FBUCKET_0").ToString)})
                row.AddCell(New Cell() With {.Value = StringToDecimal(dr("BUCKET_1").ToString), .Bold = getBold(dr("TITULO").ToString), .Background = getColor(bucket, 0), .Format = GetFormatString(dr("FBUCKET_1").ToString)})
                row.AddCell(New Cell() With {.Value = StringToDecimal(dr("BUCKET_2").ToString), .Bold = getBold(dr("TITULO").ToString), .Background = getColor(bucket, 1), .Format = GetFormatString(dr("FBUCKET_2").ToString)})
                row.AddCell(New Cell() With {.Value = StringToDecimal(dr("BUCKET_3").ToString), .Bold = getBold(dr("TITULO").ToString), .Background = getColor(bucket, 2), .Format = GetFormatString(dr("FBUCKET_3").ToString)})
                row.AddCell(New Cell() With {.Value = StringToDecimal(dr("BUCKET_4").ToString), .Bold = getBold(dr("TITULO").ToString), .Background = getColor(bucket, 3), .Format = GetFormatString(dr("FBUCKET_4").ToString)})
                row.AddCell(New Cell() With {.Value = StringToDecimal(dr("BUCKET_5").ToString), .Bold = getBold(dr("TITULO").ToString), .Background = getColor(bucket, 4), .Format = GetFormatString(dr("FBUCKET_5").ToString)})
                row.AddCell(New Cell() With {.Value = StringToDecimal(dr("BUCKET_6").ToString), .Bold = getBold(dr("TITULO").ToString), .Background = getColor(bucket, 5), .Format = GetFormatString(dr("FBUCKET_6").ToString)})
                row.AddCell(New Cell() With {.Value = StringToDecimal(dr("BUCKET_7").ToString), .Bold = getBold(dr("TITULO").ToString), .Background = getColor(bucket, 6), .Format = GetFormatString(dr("FBUCKET_7").ToString)})
                row.AddCell(New Cell() With {.Value = StringToDecimal(dr("BUCKET_8").ToString), .Bold = getBold(dr("TITULO").ToString), .Background = getColor(bucket, 7), .Format = GetFormatString(dr("FBUCKET_8").ToString)})
                row.AddCell(New Cell() With {.Value = StringToDecimal(dr("TOTAL").ToString), .Bold = getBold(dr("TITULO").ToString), .Format = GetFormatString(dr("FTOTAL").ToString)})
                sheet.AddRow(row)
            Next
            sheet.AddRow(New Row)
        Next
    End Sub

    Private Function StringToDecimal(numero As String) As Decimal
        Dim res As Decimal = 0
        If numero <> "" Then
            res = Decimal.Parse(numero)
        End If
        Return res
    End Function

    Private Function GetFormatString(formato As String) As String

        Dim auxFormato As String
        If formato = "P" Then
            auxFormato = "##0.0#%"
        Else
            auxFormato = IIf(hoja = 0, "###,###,###", "$###,###,###,##0.00")
        End If

        Return auxFormato
    End Function

    Private Sub ejecutivo(title As String)
        data = New DataTable()
        sheet = generateWorkSheet(title, 1, 21)
        generateEjecutivoHeader()

        ejecutivoTotal()
        ejecutivoBody()


    End Sub
    Private Sub ctasMigraronBucket()
        sheet = generateWorkSheet("Ctas pago migraron bucket", data.Rows.Count + 2, 16)
        Dim rowHeader As Row = sheet.Rows(0)

        rowHeader.Cells(0).Value = "Grupo de la Cuenta"
        rowHeader.Cells(1).Value = "Número de Cuenta"
        rowHeader.Cells(2).Value = "Monto vencido"
        rowHeader.Cells(3).Value = "Saldo deudor"
        rowHeader.Cells(4).Value = "Monto del próximo payoff."
        rowHeader.Cells(5).Value = "No de Pagos"
        rowHeader.Cells(6).Value = "Monto"
        rowHeader.Cells(7).Value = "Diferencia"
        rowHeader.Cells(8).Value = "Número de días de mora"
        rowHeader.Cells(9).Value = "Bucket Inicial"
        rowHeader.Cells(10).Value = "Bucket Actual"
        rowHeader.Cells(11).Value = "Subproducto"
        rowHeader.Cells(12).Value = "Gestor"
        rowHeader.Cells(13).Value = "Agencia actual"
        rowHeader.Cells(14).Value = "Fecha de referencia a la agenc"
        rowHeader.Cells(15).Value = "Status"

        For rowIndex As Integer = 0 To data.Rows.Count - 1
            For colIndex As Integer = 0 To data.Columns.Count - 1
                sheet.Rows(rowIndex + 1).Cells(colIndex).Value = data.Rows(rowIndex)(colIndex).ToString
            Next
        Next
    End Sub

    Private Sub Pagos()
        sheet = generateWorkSheet("Pagos", data.Rows.Count + 2, 6)

        Dim rowHeader As Row = sheet.Rows(0)

        rowHeader.Cells(0).Value = "Cuenta"
        rowHeader.Cells(1).Value = "Monto de Transacción/Pago"
        rowHeader.Cells(2).Value = "Fecha de Transacción/Pago"
        rowHeader.Cells(3).Value = "Fecha de Registro de la Transacción/Pago"
        rowHeader.Cells(4).Value = "Descripción de trandefs usada"

        For rowIndex As Integer = 0 To data.Rows.Count - 1
            For colIndex As Integer = 0 To data.Columns.Count - 1
                sheet.Rows(rowIndex + 1).Cells(colIndex).Value = data.Rows(rowIndex)(colIndex).ToString
            Next
        Next
    End Sub
End Class
