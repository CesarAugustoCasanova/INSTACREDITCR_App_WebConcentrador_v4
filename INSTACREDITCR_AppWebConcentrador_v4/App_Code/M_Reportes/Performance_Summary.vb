Imports Microsoft.VisualBasic
Imports Telerik.Web.Spreadsheet
Imports WorkSheetHelper
Public Class Performance_Summary

    Private sheet As Worksheet
    Private anios As Integer

    Private row_base As Integer = 1

    Private column_base As String = "B"
    Private column_half As String

    Private total_cols As Integer
    Private half_cols As Integer
    Public Sub New()
        anios = 4
        total_cols = anios * 4
        half_cols = (total_cols - 1) / 2
        column_base = "B"
        row_base = 1
        column_half = moveColumn(column_base, half_cols)
    End Sub

    Public Function crearSummary() As Worksheet
        sheet = New Worksheet With {
            .Name = "Summary",
            .ShowGridLines = False
        }

        sheet.AddRows(crearSummaryHeader("PLAZAS"))
        sheet.AddRows(createSummaryBody())
        sheet.AddRow(New Row())

        sheet.AddRows(crearSummaryHeader("AGENCIAS"))
        sheet.AddRows(createSummaryBody())
        sheet.AddRow(New Row())

        sheet.AddRows(crearSummaryHeader("OTRO CANAL"))
        sheet.AddRows(createSummaryBody())
        sheet.AddRow(New Row())

        sheet.AddRows(crearSummaryHeader("TOTAL"))
        sheet.AddRows(createSummaryBody())
        sheet.AddRow(New Row())

        giveStyleToSummarySheet()
        fillSummaryTotalTable()
        createGreenTable()
        Return sheet
    End Function

    Private Function crearSummaryHeader(Titulo As String) As List(Of Row)
        Dim rows As New List(Of Row)
        Dim row1 As Row = crearRow(anios * 4 + 1)
        print(row1.Cells(0), "PERIODO", "rgb(223,223,223)")
        print(row1.Cells(1), Titulo, "rgb(223,223,223)")

        rows.Add(row1)

        row1 = crearRow(anios * 4 + 1)

        print(row1.Cells(1), "ASIGNCACION", "rgb(223,223,223)")
        print(row1.Cells(anios * 2 + 1), "RECUPERACION", "rgb(223,223,223)")

        rows.Add(row1)

        row1 = crearRow(anios * 4 + 1)

        Dim offset_anio As Integer = 0
        For i As Integer = 0 To (anios * 4) - 1 Step 2
            print(row1.Cells(i + 1), 2017 + (offset_anio Mod anios), "rgb(223,223,223)")
            offset_anio += 1
        Next
        rows.Add(row1)

        row1 = crearRow(anios * 4 + 1)
        For i As Integer = 0 To (anios * 4) - 1
            print(row1.Cells(i + 1), IIf(i Mod 2 = 0, "CUENTAS", "MONTO"), "rgb(223,223,223)")
            row1.Cells(i + 1).Bold = True
            offset_anio += 1
        Next
        rows.Add(row1)

        Return rows
    End Function

    Private Function createSummaryBody() As List(Of Row)
        Dim rows As New List(Of Row)
        Dim meses() As String = {"ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE", "TOTAL"}
        For Each mes As String In meses
            Dim row1 As Row = crearRow(anios * 4 + 1)
            print(row1.Cells(0), mes)
            row1.Cells(0).Bold = True

            For indexCelda As Integer = 1 To anios * 2
                print(row1.Cells(indexCelda), IIf(mes = "TOTAL", "", 0))
            Next

            For indexCelda As Integer = anios * 2 + 1 To anios * 4
                print(row1.Cells(indexCelda), IIf(mes = "TOTAL", "", 0), "rgb(255,242,204)")
            Next
            rows.Add(row1)
        Next
        Return rows
    End Function

    Private Sub fillSummaryTotalTable()
        Dim auxCol As String = "B"
        For indexRow As Integer = 59 To 71
            Dim auxRow As Row = sheet.Rows(indexRow - 1)

            auxCol = "B"
            Try
                For index As Integer = 1 To auxRow.Cells.Count - 2
                    Dim formula As String = "=SUM(" &
            auxCol & indexRow - 18 & "," &
            auxCol & indexRow - (18 * 2) & "," &
            auxCol & indexRow - (18 * 3) & ")"
                    auxRow.Cells(index).Formula = formula
                    auxCol = moveColumn(auxCol, 1)
                Next
            Catch ex As Exception

            End Try
        Next
    End Sub

    Private Sub giveStyleToSummarySheet()
        'Plazas
        row_base = 1
        column_base = "B"
        sheet.AddMergedCells("A" & row_base & ":A" & row_base + 3)
        sheet.AddMergedCells(getColumnsRange(row_base, "B", total_cols - 1))
        sheet.AddMergedCells(getColumnsRange(row_base + 1, "B", half_cols - 1))
        sheet.AddMergedCells(getColumnsRange(row_base + 1, column_half, half_cols - 1))
        For i = 1 To anios * 2
            sheet.AddMergedCells(getColumnsRange(row_base + 2, column_base, 1))
            column_base = moveColumn(column_base, 2)
        Next
        Dim colAux As String = column_half
        For index = half_cols + 1 To total_cols
            sheet.Rows(row_base).Cells(index).Background = "rgb(255,242,204)"
            sheet.Rows(row_base + 1).Cells(index).Background = "rgb(255,242,204)"
            sheet.Rows(row_base + 2).Cells(index).Background = "rgb(255,242,204)"

            sheet.Rows(row_base + 15).Cells(index).Formula = "=SUM(" & colAux & (row_base + 15) & ":" & colAux & (row_base + 4) & ")"
            colAux = moveColumn(colAux, 1)
        Next

        'Agencias
        row_base = 19
        column_base = "B"
        colAux = column_half
        sheet.AddMergedCells("A" & row_base & ":A" & row_base + 3)
        sheet.AddMergedCells(getColumnsRange(row_base, "B", total_cols - 1))
        sheet.AddMergedCells(getColumnsRange(row_base + 1, "B", half_cols - 1))
        sheet.AddMergedCells(getColumnsRange(row_base + 1, column_half, half_cols - 1))
        For i = 1 To anios * 2
            sheet.AddMergedCells(getColumnsRange(row_base + 2, column_base, 1))
            column_base = moveColumn(column_base, 2)
        Next
        For index = half_cols + 1 To total_cols
            sheet.Rows(row_base).Cells(index).Background = "rgb(255,242,204)"
            sheet.Rows(row_base + 1).Cells(index).Background = "rgb(255,242,204)"
            sheet.Rows(row_base + 2).Cells(index).Background = "rgb(255,242,204)"

            sheet.Rows(row_base + 15).Cells(index).Formula = "=SUM(" & colAux & (row_base + 15) & ":" & colAux & (row_base + 4) & ")"
            colAux = moveColumn(colAux, 1)
        Next

        'OTRO CANAL
        row_base = 37
        column_base = "B"
        colAux = column_half
        sheet.AddMergedCells("A" & row_base & ":A" & row_base + 3)
        sheet.AddMergedCells(getColumnsRange(row_base, "B", total_cols - 1))
        sheet.AddMergedCells(getColumnsRange(row_base + 1, "B", half_cols - 1))
        sheet.AddMergedCells(getColumnsRange(row_base + 1, column_half, half_cols - 1))
        For i = 1 To anios * 2
            sheet.AddMergedCells(getColumnsRange(row_base + 2, column_base, 1))
            column_base = moveColumn(column_base, 2)
        Next
        For index = half_cols + 1 To total_cols
            sheet.Rows(row_base).Cells(index).Background = "rgb(255,242,204)"
            sheet.Rows(row_base + 1).Cells(index).Background = "rgb(255,242,204)"
            sheet.Rows(row_base + 2).Cells(index).Background = "rgb(255,242,204)"

            sheet.Rows(row_base + 15).Cells(index).Formula = "=SUM(" & colAux & (row_base + 15) & ":" & colAux & (row_base + 4) & ")"
            colAux = moveColumn(colAux, 1)
        Next

        'TOTAL
        row_base = 55
        column_base = "B"
        sheet.AddMergedCells("A" & row_base & ":A" & row_base + 3)
        sheet.AddMergedCells(getColumnsRange(row_base, "B", total_cols - 1))
        sheet.AddMergedCells(getColumnsRange(row_base + 1, "B", half_cols - 1))
        sheet.AddMergedCells(getColumnsRange(row_base + 1, column_half, half_cols - 1))
        For i = 1 To anios * 2
            sheet.AddMergedCells(getColumnsRange(row_base + 2, column_base, 1))
            column_base = moveColumn(column_base, 2)
        Next
        For index = half_cols + 1 To total_cols
            sheet.Rows(row_base).Cells(index).Background = "rgb(255,242,204)"
            sheet.Rows(row_base + 1).Cells(index).Background = "rgb(255,242,204)"
            sheet.Rows(row_base + 2).Cells(index).Background = "rgb(255,242,204)"
        Next
    End Sub

    Private Sub createGreenTable()
        Dim verde = "rgb(226,239,218)"
        For i = 0 To 16
            sheet.AddRow(crearRow((anios * 4) + 1))
        Next

        row_base = 73

        Dim auxRow As Row = sheet.Rows(row_base)
        print(auxRow.Cells(0), "PERIODO", verde)
        For i = 0 To anios - 1
            print(auxRow.Cells((i * 4) + 1), "INGRESOS RECONOCIDOS " & (2017 + i), verde)

        Next
        auxRow = sheet.Rows(row_base + 1)
        For i = 1 To (anios * 4)
            Dim titulo As String
            Select Case (i Mod 4)
                Case 0
                    titulo = "INSTITUCIONAL"
                Case 1
                    titulo = "TDC"
                Case 2
                    titulo = "TOTAL"
                Case 3
                    titulo = "DIFERENCIA"
            End Select


            print(auxRow.Cells(i), titulo, verde)
        Next

        Dim meses() As String = {"ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE", "TOTAL"}
        row_base += 2
        For Each mes As String In meses
            auxRow = sheet.Rows(row_base)
            row_base += 1
            print(auxRow.Cells(0), mes)
            auxRow.Cells(0).Bold = True

            For indexCelda As Integer = 0 To auxRow.Cells.Count - 1
                print(auxRow.Cells(indexCelda), 0, IIf(mes = "TOTAL", verde, Nothing))
            Next
        Next


    End Sub

End Class
