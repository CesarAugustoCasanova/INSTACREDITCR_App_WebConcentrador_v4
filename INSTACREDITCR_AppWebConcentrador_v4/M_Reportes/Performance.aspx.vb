Imports WorkSheetHelper
Imports System.Threading
Imports Telerik.Web.Spreadsheet
Imports Telerik.Web.UI
Imports System.Data
Imports System.Data.SqlClient
Imports Db

Partial Class Performance
    Inherits System.Web.UI.Page

    Private Sub Performance_Load(sender As Object, e As EventArgs) Handles Me.PreRender
        If Not IsPostBack Then
            crearSummary()
            crearProducto()
            crearAgencias()
            crearAgencias1()
            crearAgencias2()
            crearPlazas()
            crearGestionAgencias()
            crearGestionPlaza()
            For Each hoja As Worksheet In ssPerformance.Sheets
                LockSheet(hoja)
            Next
        End If
    End Sub

    Private Sub crearSummary()
        Dim hoja As New Performance_Summary2()
        ssPerformance.Sheets.Add(hoja.crearSummary())

    End Sub

    Private Sub crearProducto()
        Dim hoja As New Performance_Producto()
        ssPerformance.Sheets.Add(hoja.crearProducto())
    End Sub

    Private Sub crearAgencias()
        Dim hoja As New Performance_Agencias()
        ssPerformance.Sheets.Add(hoja.crearSheet())
    End Sub

    Private Sub crearAgencias1()
        Dim hoja As New Performance_Agencias1()
        ssPerformance.Sheets.Add(hoja.crearSheet())
    End Sub

    Private Sub crearAgencias2()
        Dim hoja As New Performance_Agencias2()
        ssPerformance.Sheets.Add(hoja.crearSheet())
    End Sub

    Private Sub crearPlazas()
        Dim hoja As New Performance_Plazas()
        ssPerformance.Sheets.Add(hoja.crearSheet())
    End Sub

    Private Sub crearGestionAgencias()
        Dim hoja As New Performance_GestionAgencias()
        ssPerformance.Sheets.Add(hoja.crearSheet())
    End Sub

    Private Sub crearGestionPlaza()
        Dim hoja As New Performance_GestionPlaza()
        ssPerformance.Sheets.Add(hoja.crearSheet())
    End Sub
End Class

Public Class Performance_Summary2

    Private sheet As Worksheet
    Private anios As Integer
    Private anio_base As Integer

    Private row_base As Integer = 1

    Private column_base As String = "B"
    Private column_half As String

    Private total_cols As Integer
    Private half_cols As Integer

    Private colors() As String = {"rgb(223,223,223)", "rgb(255,242,204)", "rgb(226,239,218)", "rgb(51,153,255)"}

    Public Sub New()
        anio_base = 2020
        anios = (anio_base - Now.Year) + 1
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
        Try
            crearSummaryHeader("PLAZAS")
            createSummaryBody(8)
            sheet.AddRow(New Row())

            crearSummaryHeader("AGENCIAS")
            createSummaryBody(9)
            sheet.AddRow(New Row())

            crearSummaryHeader("OTRO CANAL")
            createSummaryBody(10)
            sheet.AddRow(New Row())

            crearSummaryHeader("TOTAL")
            createSummaryBody(11)
            sheet.AddRow(New Row())

            'giveStyleToSummarySheet()
            'fillSummaryTotalTable()

            createGreenTable()
            sheet.AddRow(New Row())

            createBlueTable()
            sheet.AddRow(New Row())


        Catch ex As Exception

        End Try
        Return sheet
    End Function

    Private Sub crearSummaryHeader(Titulo As String)
        Dim row1 As Row = crearRow(anios * 4 + 1)
        print(row1.Cells(0), "PERIODO", colors(0))
        print(row1.Cells(1), Titulo, colors(0))

        sheet.AddRow(row1)
        sheet.AddMergedCells(getRange(sheet.Rows.Count - 1, 1, sheet.Rows.Count - 1, anios * 4))
        sheet.AddMergedCells(getRange(sheet.Rows.Count - 1, 0, sheet.Rows.Count + 2, 0))

        row1 = crearRow(anios * 4 + 1)

        print(row1.Cells(1), "ASIGNCACION", colors(0))
        print(row1.Cells(anios * 2 + 1), "RECUPERACION", colors(1))

        sheet.AddRow(row1)
        sheet.AddMergedCells(getRange(sheet.Rows.Count - 1, 1, sheet.Rows.Count - 1, anios * 2))
        sheet.AddMergedCells(getRange(sheet.Rows.Count - 1, anios * 2 + 1, sheet.Rows.Count - 1, anios * 4))

        row1 = crearRow(anios * 4 + 1)

        Dim offset_anio As Integer = 0
        For i As Integer = 0 To (anios * 4) - 1 Step 2
            Dim auxColor As String = IIf((i + 1) > (anios * 2), colors(1), colors(0))
            print(row1.Cells(i + 1), anio_base + (offset_anio Mod anios), auxColor)
            sheet.AddMergedCells(getRange(sheet.Rows.Count, i + 1, sheet.Rows.Count, i + 2))

            offset_anio += 1
        Next
        sheet.AddRow(row1)

        row1 = crearRow(anios * 4 + 1)
        For i As Integer = 0 To (anios * 4) - 1
            Dim auxColor As String = IIf((i + 1) > (anios * 2), colors(1), colors(0))
            print(row1.Cells(i + 1), IIf(i Mod 2 = 0, "CUENTAS", "MONTO"), auxColor)
            row1.Cells(i + 1).Bold = True
            offset_anio += 1
        Next
        sheet.AddRow(row1)

    End Sub

    Private Sub createSummaryBody(bandera As Integer)
        Dim meses() As String = {"ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE", "TOTAL"}
        'FALTA HACER ITERACION POR AÑO
        Dim data As DataTable = SP.Performance(bandera, 2020, 0)
        Dim firstRow As Integer = sheet.Rows.Count - 1

        Dim row1 As Row = crearRow(anios * 4 + 1)
        For indexRow = 0 To data.Rows.Count - 1
            row1 = crearRow(anios * 4 + 1)
            Dim mes As String
            mes = meses(data.Rows(indexRow).Item("mes") - 1)
            print(row1.Cells(0), mes)
            row1.Cells(0).Bold = True

            For indexCelda As Integer = 1 To anios * 2
                Try
                    print(row1.Cells(indexCelda), IIf(meses(data.Rows(indexRow).Item("mes")) = "TOTAL", "", data.Rows(indexRow).Item(indexCelda)))
                Catch
                    print(row1.Cells(indexCelda), "")
                End Try
            Next

            For indexCelda As Integer = anios * 2 + 1 To anios * 4
                Try
                    print(row1.Cells(indexCelda), IIf(meses(data.Rows(indexRow).Item("mes")) = "TOTAL", "", data.Rows(indexRow).Item(indexCelda)), "rgb(255,242,204)")
                Catch
                    print(row1.Cells(indexCelda), "", "rgb(255,242,204)")
                End Try
            Next
            sheet.AddRow(row1)
        Next

        Dim border As New BorderStyle With {.Color = "black", .Size = 1}
        'Calculamos totales
        row1 = crearRow(anios * 4 + 1)
        row1.Cells(0).Value = "TOTAL"
        row1.Cells(0).BorderBottom = border
        row1.Cells(0).BorderLeft = border
        row1.Cells(0).BorderRight = border
        row1.Cells(0).BorderTop = border
        For indexCell = 1 To row1.Cells.Count - 2
            Dim celda As Cell = row1.Cells(indexCell)
            celda.Formula = "=SUM(" & getRange(firstRow, indexCell, sheet.Rows.Count - 1, indexCell) & ")"
            celda.Bold = True
            celda.Background = IIf((row1.Cells.Count - 2) / 2 > indexCell - 1, colors(0), colors(1))
            row1.Cells(0).BorderBottom = border
            row1.Cells(0).BorderLeft = border
            row1.Cells(0).BorderRight = border
            row1.Cells(0).BorderTop = border
        Next
        sheet.AddRow(row1)

    End Sub

    Private Sub createGreenTable()
        Dim verde = "rgb(226,239,218)"
        Dim columns_per_year As Integer = 4
        row_base = sheet.Rows.Count
        'Indicamos cuantas rows vamos a agregar y el tamaño en celdas de cada row
        For i = 0 To 16
            sheet.AddRow(crearRow((anios * columns_per_year) + 1))
        Next

        column_base = "A"


        Dim auxRow As Row = sheet.Rows(row_base)
        print(auxRow.Cells(0), "PERIODO", verde)
        sheet.AddMergedCells(column_base & (row_base + 1) & ":" & column_base & (row_base + 2))

        column_base = moveColumn(column_base, 1)
        Dim column_destino As String = ""
        For i = 0 To anios - 1
            print(auxRow.Cells((i * columns_per_year) + 1), "INGRESOS RECONOCIDOS " & (anio_base + i), verde)
            column_destino = moveColumn(column_base, columns_per_year - 1)
            sheet.AddMergedCells(column_base & (row_base + 1) & ":" & column_destino & (row_base + 1))
            column_base = moveColumn(column_destino, 1)
        Next

        auxRow = sheet.Rows(row_base + 1)
        For i = 1 To (anios * columns_per_year)
            Dim titulo As String = ""
            Select Case ((i - 1) Mod columns_per_year)
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

        'Creacion del body.
        Dim meses() As String = {"ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE", "TOTAL"}
        row_base += 2
        For Each mes As String In meses
            auxRow = sheet.Rows(row_base)
            row_base += 1
            print(auxRow.Cells(0), mes)
            auxRow.Cells(0).Bold = True

            For indexCelda As Integer = 1 To auxRow.Cells.Count - 2
                print(auxRow.Cells(indexCelda), 0, IIf(mes = "TOTAL", verde, Nothing))
            Next
        Next
    End Sub

    Private Sub createBlueTable()
        Dim azul = "rgb(51,153,255)"
        Dim columns_per_year As Integer = 5
        'Indicamos cuantas rows vamos a agregar y el tamaño en celdas de cada row
        row_base = sheet.Rows.Count
        For i = 0 To 16
            sheet.AddRow(crearRow((anios * columns_per_year) + 1))
        Next

        column_base = "A"


        Dim auxRow As Row = sheet.Rows(row_base)
        print(auxRow.Cells(0), "PERIODO", azul)
        sheet.AddMergedCells(column_base & (row_base + 1) & ":" & column_base & (row_base + 2))

        column_base = moveColumn(column_base, 1)
        Dim column_destino As String = ""
        For i = 0 To anios - 1
            print(auxRow.Cells((i * columns_per_year) + 1), "INGRESOS RECONOCIDOS " & (anio_base + i), azul)
            column_destino = moveColumn(column_base, columns_per_year - 1)
            sheet.AddMergedCells(column_base & (row_base + 1) & ":" & column_destino & (row_base + 1))
            column_base = moveColumn(column_destino, 1)
        Next


        auxRow = sheet.Rows(row_base + 1)
        For i = 1 To (anios * columns_per_year)
            Dim titulo As String = ""
            Select Case ((i - 1) Mod columns_per_year)
                Case 0
                    titulo = "PLAZAS"
                Case 1
                    titulo = "AGENCIAS"
                Case 2
                    titulo = "CALLCENTER"
                Case 3
                    titulo = "SIN ASIGNACION"
                Case 4
                    titulo = "TOTAL"
            End Select


            print(auxRow.Cells(i), titulo, azul)
        Next

        'Creacion del body.
        Dim meses() As String = {"ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE", "TOTAL"}
        row_base += 2
        For Each mes As String In meses

            auxRow = sheet.Rows(row_base)
            row_base += 1
            print(auxRow.Cells(0), mes)
            auxRow.Cells(0).Bold = True

            For indexCelda As Integer = 1 To auxRow.Cells.Count - 2
                print(auxRow.Cells(indexCelda), 0, IIf(mes = "TOTAL", azul, Nothing))
            Next
        Next
    End Sub
End Class

Public Class Performance_Producto
    Private sheet As Worksheet
    Private colors() As String = {"#FCE4D6", "#F2F2F2", "#92D050"}

    Private anio_base As Integer = 2020

    Private indexTotalRow As Integer

    Public Sub New()
        sheet = New Worksheet With {.Name = "Producto"}
        indexTotalRow = 12
    End Sub

    Public Function crearProducto() As Worksheet
        sheet.ShowGridLines = False
        Try
            createHorizontalFreezedTable()
            Dim mesActual = Now.Month - 1
            Dim anioActual = Now.Year

            For anio = anio_base To anioActual
                For mes = 0 To mesActual
                    createMonthTable(mes, anio)
                Next
            Next


        Catch ex As Exception

        End Try
        Return sheet
    End Function

    Private Sub createHorizontalFreezedTable()
        For i = 0 To 12
            Dim rrow As New Row
            rrow.AddCell(New Cell)
            sheet.AddRow(rrow)
        Next

        Dim put = Sub(row As Integer, bold As Boolean, color As String, texto As String)
                      Dim bs As New BorderStyle With {.Color = "black", .Size = 1}

                      sheet.Rows(row).Cells(0).BorderBottom = bs
                      sheet.Rows(row).Cells(0).BorderRight = bs
                      sheet.Rows(row).Cells(0).BorderLeft = bs
                      sheet.Rows(row).Cells(0).BorderTop = bs

                      sheet.Rows(row).Cells(0).Bold = bold
                      sheet.Rows(row).Cells(0).Background = color
                      sheet.Rows(row).Cells(0).Value = texto

                      sheet.Rows(row).Cells(0).TextAlign = "center"
                      sheet.Rows(row).Cells(0).VerticalAlign = "center"
                  End Sub


        put(3, True, colors(0), "PORTAFOLIO")
        put(4, False, "white", "TDC - VIGENTE (BF2)")
        put(5, False, "white", "TDC -  CASTIGADO (BF7)")
        put(6, True, "white", "INSTITUCIONAL - VIGENTE ")
        put(7, False, "white", " - INSTALADO (NOMINA)")
        put(8, False, "white", " - NO INSTALADO (BANCOS)")
        put(9, True, "white", "INSTITUCIONAL - CASTIGADO")
        put(10, False, "white", " - INSTALADO (NOMINA)")
        put(11, False, "white", " - CASTIGADO NO INSTALADO (BANCOS)")

        sheet.FrozenColumns = 1
    End Sub

    Private Sub createMonthTable(mes As Integer, anio As Integer)
        Dim firsColumn As Integer = createAgenciasTable(mes, anio)
        createPlazasTable(mes, anio)
        createOtroCanalTable(mes, anio)
        createTotalTable()
        Dim lastColumn As Integer = createAnalisisTable()

        sheet.AddMergedCells(getRange(0, firsColumn, 0, lastColumn))
    End Sub

    ''' <summary>
    ''' Crea el inicio del reporte de cada mes
    ''' </summary>
    ''' <param name="mes">mes del reporte base 0</param>
    ''' <param name="anio">año del reporte</param>
    ''' <returns>el indice de la columna donde empieza la tabla</returns>
    Private Function createAgenciasTable(mes As Integer, anio As Integer) As Integer
        Dim mesDesc() As String = {"ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE"}

        For Each row As Row In sheet.Rows
            row.AddCell(New Cell()) 'Espacio en blanco
            row.AddCell(New Cell()) 'cuentas
            row.AddCell(New Cell()) 'Sdo deudor
            row.AddCell(New Cell()) 'cuentas
            row.AddCell(New Cell()) 'Monto recuperado
        Next

        Dim lastIndexCell As Integer = sheet.Rows(0).Cells.Count - 1

        sheet.Rows(0).Cells(lastIndexCell - 3).Value = mesDesc(mes) & " " & anio
        formatCell(0, lastIndexCell - 3, colors(0))

        sheet.Rows(1).Cells(lastIndexCell - 3).Value = "AGENCIAS"
        formatCell(1, lastIndexCell - 3, colors(0))
        sheet.AddMergedCells(getRange(1, lastIndexCell - 3, 1, lastIndexCell))


        sheet.Rows(2).Cells(lastIndexCell - 3).Value = "ASIGNACION"
        formatCell(2, lastIndexCell - 3, colors(0))
        sheet.AddMergedCells(getRange(2, lastIndexCell - 3, 2, lastIndexCell - 2))
        sheet.Rows(2).Cells(lastIndexCell - 1).Value = "RECUPERACION"
        formatCell(2, lastIndexCell - 1, colors(0))
        sheet.AddMergedCells(getRange(2, lastIndexCell - 1, 2, lastIndexCell))

        sheet.Rows(3).Cells(lastIndexCell - 3).Value = "CUENTAS"
        formatCell(3, lastIndexCell - 3, colors(0))
        sheet.Rows(3).Cells(lastIndexCell - 2).Value = "SALDO DEUDOR"
        formatCell(3, lastIndexCell - 2, colors(0))
        sheet.Rows(3).Cells(lastIndexCell - 1).Value = "CUENTAS"
        formatCell(3, lastIndexCell - 1, colors(0))
        sheet.Rows(3).Cells(lastIndexCell - 0).Value = "MONTO RECUPERADO"
        formatCell(3, lastIndexCell - 0, colors(0))

        Dim tab As DataTable = SP.Performance(2, anio, mes)
        Dim idx As Int16 = 0

        For indexRow = 4 To 11 'Aqui se llenan los valores de agencias
            For colIndex = lastIndexCell - 3 To lastIndexCell
                Try

                    sheet.Rows(indexRow).Cells(colIndex).Value = tab.Rows(indexRow - 4).Item(idx)

                Catch
                    sheet.Rows(indexRow).Cells(colIndex).Value = 0
                End Try
                idx = idx + 1
                formatCell(indexRow, colIndex)
            Next
        Next

        Dim col As Integer = lastIndexCell - 3
        sheet.Rows(12).Cells(col).Formula = "SUM(" & IndexesToExcelFormat(4, col) & ":" & IndexesToExcelFormat(11, col) & ")"
        formatCell(12, col)

        col += 1
        sheet.Rows(12).Cells(col).Formula = "SUM(" & IndexesToExcelFormat(4, col) & ":" & IndexesToExcelFormat(11, col) & ")"
        formatCell(12, col)

        col += 1
        sheet.Rows(12).Cells(col).Formula = "SUM(" & IndexesToExcelFormat(4, col) & ":" & IndexesToExcelFormat(11, col) & ")"
        formatCell(12, col)

        col += 1
        sheet.Rows(12).Cells(col).Formula = "SUM(" & IndexesToExcelFormat(4, col) & ":" & IndexesToExcelFormat(11, col) & ")"
        formatCell(12, col)

        Return lastIndexCell - 3
    End Function

    Private Sub createPlazasTable(mes As Integer, anio As Integer)
        For Each row As Row In sheet.Rows
            row.AddCell(New Cell()) 'Espacio en blanco
            row.AddCell(New Cell()) 'cuentas
            row.AddCell(New Cell()) 'Sdo deudor
            row.AddCell(New Cell()) 'cuentas
            row.AddCell(New Cell()) 'Monto recuperado
        Next

        Dim lastIndexCell As Integer = sheet.Rows(0).Cells.Count - 1

        sheet.Rows(1).Cells(lastIndexCell - 3).Value = "PLAZAS"
        formatCell(1, lastIndexCell - 3, colors(0))
        sheet.AddMergedCells(getRange(1, lastIndexCell - 3, 1, lastIndexCell))


        sheet.Rows(2).Cells(lastIndexCell - 3).Value = "ASIGNACION"
        formatCell(2, lastIndexCell - 3, colors(0))
        sheet.AddMergedCells(getRange(2, lastIndexCell - 3, 2, lastIndexCell - 2))
        sheet.Rows(2).Cells(lastIndexCell - 1).Value = "RECUPERACION"
        formatCell(2, lastIndexCell - 1, colors(0))
        sheet.AddMergedCells(getRange(2, lastIndexCell - 1, 2, lastIndexCell))

        sheet.Rows(3).Cells(lastIndexCell - 3).Value = "CUENTAS"
        formatCell(3, lastIndexCell - 3, colors(0))
        sheet.Rows(3).Cells(lastIndexCell - 2).Value = "SALDO DEUDOR"
        formatCell(3, lastIndexCell - 2, colors(0))
        sheet.Rows(3).Cells(lastIndexCell - 1).Value = "CUENTAS"
        formatCell(3, lastIndexCell - 1, colors(0))
        sheet.Rows(3).Cells(lastIndexCell - 0).Value = "MONTO RECUPERADO"
        formatCell(3, lastIndexCell - 0, colors(0))


        Dim tab As DataTable = SP.Performance(21, anio, mes)
        Dim idx As Int16 = 0

        For indexRow = 4 To 11 'Se llena lo de plazas
            For colIndex = lastIndexCell - 3 To lastIndexCell
                Try

                    sheet.Rows(indexRow).Cells(colIndex).Value = tab.Rows(indexRow - 4).Item(idx)

                Catch
                    sheet.Rows(indexRow).Cells(colIndex).Value = 0
                End Try
                idx = idx + 1
                formatCell(indexRow, colIndex)
            Next
        Next

        Dim col As Integer = lastIndexCell - 3
        sheet.Rows(12).Cells(col).Formula = "SUM(" & IndexesToExcelFormat(4, col) & ":" & IndexesToExcelFormat(11, col) & ")"
        formatCell(12, col)

        col += 1
        sheet.Rows(12).Cells(col).Formula = "SUM(" & IndexesToExcelFormat(4, col) & ":" & IndexesToExcelFormat(11, col) & ")"
        formatCell(12, col)

        col += 1
        sheet.Rows(12).Cells(col).Formula = "SUM(" & IndexesToExcelFormat(4, col) & ":" & IndexesToExcelFormat(11, col) & ")"
        formatCell(12, col)

        col += 1
        sheet.Rows(12).Cells(col).Formula = "SUM(" & IndexesToExcelFormat(4, col) & ":" & IndexesToExcelFormat(11, col) & ")"
        formatCell(12, col)
    End Sub

    Private Sub createOtroCanalTable(mes As Integer, anio As Integer)
        For Each row As Row In sheet.Rows
            row.AddCell(New Cell()) 'Espacio en blanco
            row.AddCell(New Cell()) 'cuentas
            row.AddCell(New Cell()) 'Sdo deudor
            row.AddCell(New Cell()) 'cuentas
            row.AddCell(New Cell()) 'Monto recuperado
        Next

        Dim lastIndexCell As Integer = sheet.Rows(0).Cells.Count - 1

        sheet.Rows(1).Cells(lastIndexCell - 3).Value = "OTRO CANAL"
        formatCell(1, lastIndexCell - 3, colors(0))
        sheet.AddMergedCells(getRange(1, lastIndexCell - 3, 1, lastIndexCell))


        sheet.Rows(2).Cells(lastIndexCell - 3).Value = "ASIGNACION"
        formatCell(2, lastIndexCell - 3, colors(0))
        sheet.AddMergedCells(getRange(2, lastIndexCell - 3, 2, lastIndexCell - 2))
        sheet.Rows(2).Cells(lastIndexCell - 1).Value = "RECUPERACION"
        formatCell(2, lastIndexCell - 1, colors(0))
        sheet.AddMergedCells(getRange(2, lastIndexCell - 1, 2, lastIndexCell))

        sheet.Rows(3).Cells(lastIndexCell - 3).Value = "CUENTAS"
        formatCell(3, lastIndexCell - 3, colors(0))
        sheet.Rows(3).Cells(lastIndexCell - 2).Value = "SALDO DEUDOR"
        formatCell(3, lastIndexCell - 2, colors(0))
        sheet.Rows(3).Cells(lastIndexCell - 1).Value = "CUENTAS"
        formatCell(3, lastIndexCell - 1, colors(0))
        sheet.Rows(3).Cells(lastIndexCell - 0).Value = "MONTO RECUPERADO"
        formatCell(3, lastIndexCell - 0, colors(0))

        Dim tab As DataTable = SP.Performance(22, anio, mes)
        Dim idx As Int16 = 0



        For indexRow = 4 To 11 'llena otro canal
            For colIndex = lastIndexCell - 3 To lastIndexCell
                Try

                    sheet.Rows(indexRow).Cells(colIndex).Value = tab.Rows(indexRow - 4).Item(idx)

                Catch
                    sheet.Rows(indexRow).Cells(colIndex).Value = 0
                End Try
                idx = idx + 1
                formatCell(indexRow, colIndex)
            Next
        Next

        Dim col As Integer = lastIndexCell - 3
        sheet.Rows(12).Cells(col).Formula = "SUM(" & IndexesToExcelFormat(4, col) & ":" & IndexesToExcelFormat(11, col) & ")"
        formatCell(12, col)

        col += 1
        sheet.Rows(12).Cells(col).Formula = "SUM(" & IndexesToExcelFormat(4, col) & ":" & IndexesToExcelFormat(11, col) & ")"
        formatCell(12, col)

        col += 1
        sheet.Rows(12).Cells(col).Formula = "SUM(" & IndexesToExcelFormat(4, col) & ":" & IndexesToExcelFormat(11, col) & ")"
        formatCell(12, col)

        col += 1
        sheet.Rows(12).Cells(col).Formula = "SUM(" & IndexesToExcelFormat(4, col) & ":" & IndexesToExcelFormat(11, col) & ")"
        formatCell(12, col)
    End Sub

    Private Sub createTotalTable()
        For Each row As Row In sheet.Rows
            row.AddCell(New Cell()) 'Espacio en blanco
            row.AddCell(New Cell()) 'cuentas
            row.AddCell(New Cell()) 'Sdo deudor
            row.AddCell(New Cell()) 'cuentas
            row.AddCell(New Cell()) 'Monto recuperado
        Next

        Dim lastIndexCell As Integer = sheet.Rows(0).Cells.Count - 1

        sheet.Rows(1).Cells(lastIndexCell - 3).Value = "TOTAL"
        formatCell(1, lastIndexCell - 3, colors(0))
        sheet.AddMergedCells(getRange(1, lastIndexCell - 3, 1, lastIndexCell))


        sheet.Rows(2).Cells(lastIndexCell - 3).Value = "ASIGNACION"
        formatCell(2, lastIndexCell - 3, colors(0))
        sheet.AddMergedCells(getRange(2, lastIndexCell - 3, 2, lastIndexCell - 2))
        sheet.Rows(2).Cells(lastIndexCell - 1).Value = "RECUPERACION"
        formatCell(2, lastIndexCell - 1, colors(0))
        sheet.AddMergedCells(getRange(2, lastIndexCell - 1, 2, lastIndexCell))

        sheet.Rows(3).Cells(lastIndexCell - 3).Value = "CUENTAS"
        formatCell(3, lastIndexCell - 3, colors(0))
        sheet.Rows(3).Cells(lastIndexCell - 2).Value = "SALDO DEUDOR"
        formatCell(3, lastIndexCell - 2, colors(0))
        sheet.Rows(3).Cells(lastIndexCell - 1).Value = "CUENTAS"
        formatCell(3, lastIndexCell - 1, colors(0))
        sheet.Rows(3).Cells(lastIndexCell - 0).Value = "MONTO RECUPERADO"
        formatCell(3, lastIndexCell - 0, colors(0))

        For indexRow = 4 To 11
            For colIndex = lastIndexCell - 3 To lastIndexCell
                sheet.Rows(indexRow).Cells(colIndex).Formula = "=" & IndexesToExcelFormat(indexRow, colIndex - 5) & "+" & IndexesToExcelFormat(indexRow, colIndex - 10) & "+" & IndexesToExcelFormat(indexRow, colIndex - 15)
                formatCell(indexRow, colIndex)
            Next
        Next

        Dim col As Integer = lastIndexCell - 3
        sheet.Rows(12).Cells(col).Formula = "SUM(" & IndexesToExcelFormat(4, col) & ":" & IndexesToExcelFormat(11, col) & ")"
        formatCell(12, col)

        col += 1
        sheet.Rows(12).Cells(col).Formula = "SUM(" & IndexesToExcelFormat(4, col) & ":" & IndexesToExcelFormat(11, col) & ")"
        formatCell(12, col)

        col += 1
        sheet.Rows(12).Cells(col).Formula = "SUM(" & IndexesToExcelFormat(4, col) & ":" & IndexesToExcelFormat(11, col) & ")"
        formatCell(12, col)

        col += 1
        sheet.Rows(12).Cells(col).Formula = "SUM(" & IndexesToExcelFormat(4, col) & ":" & IndexesToExcelFormat(11, col) & ")"
        formatCell(12, col, "#D9E1F2")
    End Sub

    Private Function createAnalisisTable() As Integer
        For Each row As Row In sheet.Rows
            row.AddCell(New Cell()) 'Espacio en blanco
            row.AddCell(New Cell()) 'cuentas
            row.AddCell(New Cell()) 'Sdo deudor
            row.AddCell(New Cell()) 'cuentas
            row.AddCell(New Cell()) 'Monto recuperado
        Next

        Dim lastIndexCell As Integer = sheet.Rows(0).Cells.Count - 1

        sheet.Rows(1).Cells(lastIndexCell - 3).Value = "ANALISIS"
        formatCell(1, lastIndexCell - 3, colors(0))
        sheet.AddMergedCells(getRange(1, lastIndexCell - 3, 1, lastIndexCell))


        sheet.Rows(2).Cells(lastIndexCell - 3).Value = "ASIGNACION"
        formatCell(2, lastIndexCell - 3, colors(0))
        sheet.AddMergedCells(getRange(2, lastIndexCell - 3, 2, lastIndexCell - 2))
        sheet.Rows(2).Cells(lastIndexCell - 1).Value = "RECUPERACION"
        formatCell(2, lastIndexCell - 1, colors(0))
        sheet.AddMergedCells(getRange(2, lastIndexCell - 1, 2, lastIndexCell))

        sheet.Rows(3).Cells(lastIndexCell - 3).Value = "CUENTAS"
        formatCell(3, lastIndexCell - 3, colors(0))
        sheet.Rows(3).Cells(lastIndexCell - 2).Value = "SALDO DEUDOR"
        formatCell(3, lastIndexCell - 2, colors(0))
        sheet.Rows(3).Cells(lastIndexCell - 1).Value = "CUENTAS"
        formatCell(3, lastIndexCell - 1, colors(0))
        sheet.Rows(3).Cells(lastIndexCell - 0).Value = "MONTO RECUPERADO"
        formatCell(3, lastIndexCell - 0, colors(0))

        For indexRow = 4 To 11 'llena analisis ¿esta no la mando?
            For colIndex = lastIndexCell - 3 To lastIndexCell
                sheet.Rows(indexRow).Cells(colIndex).Value = indexRow + colIndex
                formatCell(indexRow, colIndex)
            Next
        Next

        Dim col As Integer = lastIndexCell - 3
        sheet.Rows(12).Cells(col).Formula = "SUM(" & IndexesToExcelFormat(4, col) & ":" & IndexesToExcelFormat(11, col) & ")"
        formatCell(12, col)

        col += 1
        sheet.Rows(12).Cells(col).Formula = "SUM(" & IndexesToExcelFormat(4, col) & ":" & IndexesToExcelFormat(11, col) & ")"
        formatCell(12, col)

        col += 1
        sheet.Rows(12).Cells(col).Formula = "SUM(" & IndexesToExcelFormat(4, col) & ":" & IndexesToExcelFormat(11, col) & ")"
        formatCell(12, col)

        col += 1
        sheet.Rows(12).Cells(col).Formula = "SUM(" & IndexesToExcelFormat(4, col) & ":" & IndexesToExcelFormat(11, col) & ")"
        formatCell(12, col)

        Return lastIndexCell
    End Function

    Private Sub formatCell(indexRow As Integer, indexCol As Integer, Optional color As String = "white")
        Dim bs As New BorderStyle With {.Color = "black", .Size = 1}
        sheet.Rows(indexRow).Cells(indexCol).BorderBottom = bs
        sheet.Rows(indexRow).Cells(indexCol).BorderLeft = bs
        sheet.Rows(indexRow).Cells(indexCol).BorderRight = bs
        sheet.Rows(indexRow).Cells(indexCol).BorderTop = bs
        sheet.Rows(indexRow).Cells(indexCol).Background = color
    End Sub
End Class

Public Class Performance_Agencias
    Private sheet As Worksheet
    Private colors() As String = {"#FCE4D6", "#F2F2F2", "#92D050"}

    Private anio_base As Integer = 2020

    Private hashTable As Hashtable

    Private indexTotalRow As Integer
    Public Sub New()
        sheet = New Worksheet With {.Name = "Agencias"}
    End Sub

    Public Function crearSheet() As Worksheet
        sheet.ShowGridLines = False
        Try
            buildHorizontalHeader()
            Dim anioActual = Now.Year

            For anio = 2020 To anioActual
                Dim mesActual = IIf(anio = anioActual, Now.Month - 1, 11)
                For mes = 0 To mesActual
                    printMonthTable(mes, anio)
                Next
            Next


        Catch ex As Exception

        End Try
        Return sheet
    End Function

    Private Sub buildHorizontalHeader()
        Dim rrow As New Row

        rrow.AddCell(New Cell With {.Bold = True, .FontSize = 18, .Value = "AGENCIAS"})

        sheet.AddRow(rrow)

        rrow = New Row
        rrow.AddCell(New Cell With {.Background = colors(0), .BorderBottom = New BorderStyle() With {.Color = "black", .Size = 1}})
        sheet.AddRow(rrow)

        rrow = New Row
        rrow.AddCell(New Cell With {.Value = "AGENCIA", .Background = colors(0), .BorderBottom = New BorderStyle() With {.Color = "black", .Size = 1}, .VerticalAlign = "center", .TextAlign = "center"})
        sheet.AddRow(rrow)

        rrow = New Row
        rrow.AddCell(New Cell With {.Value = "", .Background = colors(0), .BorderBottom = New BorderStyle() With {.Color = "black", .Size = 1}})
        sheet.AddRow(rrow)

        sheet.AddMergedCells("A3:A4")

        Dim createRow = Function(text As String) As Integer
                            rrow = New Row
                            rrow.AddCell(New Cell With {.Value = text, .BorderBottom = New BorderStyle() With {.Color = "black", .Size = 1}})
                            sheet.AddRow(rrow)
                            Return sheet.Rows.Count - 1
                        End Function

        'Traer informacion con las agencias
        Dim dataAgencias As DataTable = SP.Performance(12, 0)

        'Ya que tenemos esa informacion, creamos un hashtable donde la key es la agencia y el valor es el index de la row
        hashTable = New Hashtable()
        For Each agencia As DataRow In dataAgencias.Rows
            Dim txtAgencia As String = agencia(0).ToString
            hashTable.Add(txtAgencia, createRow(txtAgencia))
        Next


        rrow = New Row
        rrow.AddCell(New Cell With {.Value = "Total", .Background = colors(0), .BorderBottom = New BorderStyle() With {.Color = "black", .Size = 1}})
        sheet.AddRow(rrow)

        indexTotalRow = sheet.Rows.Count - 1

        rrow = New Row
        rrow.AddCell(New Cell With {.Value = ""})
        sheet.AddRow(rrow)

        rrow = New Row
        rrow.AddCell(New Cell With {.Value = ""})
        sheet.AddRow(rrow)

        sheet.Rows(0).Height = 50
        sheet.FrozenColumns = 1
    End Sub

    Public Sub printMonthTable(mes As Integer, anio As Integer)
        Dim baseRow As Integer = 2
        Dim mesDesc() As String = {"ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE"}

        For Each row As Row In sheet.Rows
            row.AddCell(New Cell())
            row.AddCell(New Cell())
            row.AddCell(New Cell())
            row.AddCell(New Cell())
            row.AddCell(New Cell())
            row.AddCell(New Cell())
        Next
        Dim formatCell = Sub(ByRef celda As Cell)
                             Dim bs As New BorderStyle With {.Color = "black", .Size = 1}
                             celda.BorderBottom = bs
                             celda.BorderLeft = bs
                             celda.BorderRight = bs
                             celda.BorderTop = bs
                             celda.Background = colors(0)
                         End Sub
        Dim lastCellIndex As Integer = sheet.Rows(1).Cells().Count - 1

        '>>>>>>>>>>>>>>>>>> IMPRESION DE ENCABEZADO <<<<<<<<<<<<<<<<<<<<<<<<<
        sheet.Rows(1).Cells(lastCellIndex - 4).Value = mesDesc(mes) & " " & anio
        formatCell(sheet.Rows(1).Cells(lastCellIndex - 4))
        sheet.AddMergedCells(IndexesToExcelFormat(1, lastCellIndex - 4) & ":" & IndexesToExcelFormat(1, lastCellIndex - 0))

        sheet.Rows(2).Cells(lastCellIndex - 4).Value = "ASIGNACION"
        formatCell(sheet.Rows(2).Cells(lastCellIndex - 4))
        sheet.AddMergedCells(IndexesToExcelFormat(2, lastCellIndex - 4) & ":" & IndexesToExcelFormat(2, lastCellIndex - 3))
        sheet.Rows(2).Cells(lastCellIndex - 2).Value = "RECUPERACION"
        formatCell(sheet.Rows(2).Cells(lastCellIndex - 2))
        sheet.AddMergedCells(IndexesToExcelFormat(2, lastCellIndex - 2) & ":" & IndexesToExcelFormat(2, lastCellIndex - 0))

        sheet.Rows(3).Cells(lastCellIndex - 4).Value = "CUENTAS"
        formatCell(sheet.Rows(3).Cells(lastCellIndex - 4))
        sheet.Rows(3).Cells(lastCellIndex - 3).Value = "SALDO DEUDOR"
        formatCell(sheet.Rows(3).Cells(lastCellIndex - 3))
        sheet.Rows(3).Cells(lastCellIndex - 2).Value = "CUENTAS"
        formatCell(sheet.Rows(3).Cells(lastCellIndex - 2))
        sheet.Rows(3).Cells(lastCellIndex - 1).Value = "MONTO RECUPERADO"
        formatCell(sheet.Rows(3).Cells(lastCellIndex - 1))
        sheet.AddMergedCells(IndexesToExcelFormat(3, lastCellIndex - 1) & ":" & IndexesToExcelFormat(3, lastCellIndex - 0))
        '>>>>>>>>>>>>>>>>>> FIN IMPRESION DE ENCABEZADO <<<<<<<<<<<<<<<<<<<<<<<<<

        Dim printValue = Sub(row As Integer, col As Integer, value As Integer)
                             Dim bs As New BorderStyle With {.Color = "black", .Size = 1}
                             sheet.Rows(row).Cells(col).BorderBottom = bs
                             sheet.Rows(row).Cells(col).BorderLeft = bs
                             sheet.Rows(row).Cells(col).BorderRight = bs
                             sheet.Rows(row).Cells(col).BorderTop = bs
                             sheet.Rows(row).Cells(col).Value = value
                         End Sub
        Dim prinFormula = Sub(row As Integer, col As Integer, value As String)
                              Dim bs As New BorderStyle With {.Color = "black", .Size = 1}
                              sheet.Rows(row).Cells(col).BorderBottom = bs
                              sheet.Rows(row).Cells(col).BorderLeft = bs
                              sheet.Rows(row).Cells(col).BorderRight = bs
                              sheet.Rows(row).Cells(col).BorderTop = bs
                              sheet.Rows(row).Cells(col).Formula = value
                          End Sub


        '>>>>>>>>>>>>>>>>>> IMPRESION DE DATOS <<<<<<<<<<<<<<<<<<<<<<<<<

        Dim tab As DataTable = SP.Performance(12, anio, mes + 1)
        For Each fila As DataRow In tab.Rows
            Dim rownumber As Integer = hashTable(fila(1).ToString)
            printValue(rownumber, lastCellIndex - 4, Double.Parse(fila(2).ToString))
            sheet.Rows(rownumber).Cells(lastCellIndex - 4).Format = "###,###,###,###"

            printValue(rownumber, lastCellIndex - 3, Double.Parse(fila(3).ToString))
            sheet.Rows(rownumber).Cells(lastCellIndex - 3).Format = "$#,##0.00;[Red]$#,##0.00"

            printValue(rownumber, lastCellIndex - 2, Double.Parse(fila(4).ToString))
            sheet.Rows(rownumber).Cells(lastCellIndex - 2).Format = "###,###,###,###"

            printValue(rownumber, lastCellIndex - 1, Double.Parse(fila(5).ToString))
            sheet.Rows(rownumber).Cells(lastCellIndex - 1).Format = "$#,##0.00;[Red]$#,##0.00"

            prinFormula(rownumber, lastCellIndex - 0, "=" & IndexesToExcelFormat(rownumber, lastCellIndex - 1) & "/" & IndexesToExcelFormat(indexTotalRow, lastCellIndex - 1))
            sheet.Rows(rownumber).Cells(lastCellIndex - 0).Format = "00.00%"
        Next

        Dim colIndex As Integer = lastCellIndex - 4
        formatCell(sheet.Rows(indexTotalRow).Cells(colIndex))

        colIndex += 1
        formatCell(sheet.Rows(indexTotalRow).Cells(colIndex))

        colIndex += 1
        sheet.Rows(indexTotalRow).Cells(colIndex).Formula = "SUM(" & IndexesToExcelFormat(4, colIndex) & ":" & IndexesToExcelFormat(indexTotalRow - 1, colIndex) & ")"
        formatCell(sheet.Rows(indexTotalRow).Cells(colIndex))

        colIndex += 1
        sheet.Rows(indexTotalRow).Cells(colIndex).Formula = "SUM(" & IndexesToExcelFormat(4, colIndex) & ":" & IndexesToExcelFormat(indexTotalRow - 1, colIndex) & ")"
        formatCell(sheet.Rows(indexTotalRow).Cells(colIndex))

        colIndex += 1
        sheet.Rows(indexTotalRow).Cells(colIndex).Formula = "SUM(" & IndexesToExcelFormat(4, colIndex) & ":" & IndexesToExcelFormat(indexTotalRow - 1, colIndex) & ")"
        formatCell(sheet.Rows(indexTotalRow).Cells(colIndex))
        sheet.Rows(indexTotalRow).Cells(colIndex).Format = "00.00%"

        '>>>>>>>>>>>>>>>>>> FIN IMPRESION DE DATOS <<<<<<<<<<<<<<<<<<<<<<<<<

    End Sub
End Class

Public Class Performance_Agencias1
    Private sheet As Worksheet
    Public Sub New()
    End Sub

    Public Function crearSheet() As Worksheet
        sheet = New Worksheet With {
            .Name = "Agencias1",
            .ShowGridLines = False
        }
        Try
            crearHeader()
            createBody()

        Catch ex As Exception

        End Try
        Return sheet
    End Function

    Private Sub crearHeader()
        Dim auxRow As New Row

        Dim printf = Sub(value As String)
                         Dim borderStyle As New BorderStyle With {
                         .Color = "black", .Size = 1
                         }
                         Dim newCell As New Cell With {
                         .Value = value,
                         .VerticalAlign = "center",
                         .TextAlign = "center",
                         .Bold = True,
                         .BorderBottom = borderStyle,
                         .BorderTop = borderStyle,
                         .BorderLeft = borderStyle,
                         .BorderRight = borderStyle
                         }
                         auxRow.AddCell(newCell)
                     End Sub

        printf("AGENCIA")
        printf("TDC - VIGENTE (BF2)")
        printf("TDC -  CASTIGADO (BF7)")
        printf("INSTITUCIONAL - VIGENTE TOTAL")
        printf("INSTITUCIONAL -  VIGENTE INSTALADO (NOMINA)")
        printf("INSTITUCIONAL -  VIGENTE NO INSTALADO (BANCOS)")
        printf("INSTITUCIONAL - CASTIGADO TOTAL")
        printf("INSTITUCIONAL - CASTIGADO INSTALADO (NOMINA)")
        printf("INSTITUCIONAL - CASTIGADO NO INSTALADO (BANCOS)")
        printf("TOTAL")

        sheet.AddRow(auxRow)

        sheet.FrozenRows = 1
    End Sub

    Private Sub createBody()
        Dim auxRow As New Row

        Dim printf = Sub(value As String, bold As Boolean)
                         Dim borderStyle As New BorderStyle With {
                         .Color = "black", .Size = 1
                         }
                         Dim newCell As New Cell With {
                         .Value = value,
                         .VerticalAlign = "center",
                         .TextAlign = "center",
                         .Bold = bold,
                         .BorderBottom = borderStyle,
                         .BorderTop = borderStyle,
                         .BorderLeft = borderStyle,
                         .BorderRight = borderStyle
                         }
                         auxRow.AddCell(newCell)
                     End Sub
        Dim printformula = Sub(value As String, bold As Boolean)
                               Dim borderStyle As New BorderStyle With {
                         .Color = "black", .Size = 1
                         }
                               Dim newCell As New Cell With {
                         .Formula = value,
                         .VerticalAlign = "center",
                         .TextAlign = "center",
                         .Bold = bold,
                         .BorderBottom = borderStyle,
                         .BorderTop = borderStyle,
                         .BorderLeft = borderStyle,
                         .BorderRight = borderStyle
                         }
                               auxRow.AddCell(newCell)
                           End Sub
        Dim printporcentaje = Sub(value As String)
                                  Dim borderStyle As New BorderStyle With {
                         .Color = "black", .Size = 1
                         }
                                  Dim newCell As New Cell With {
                         .Formula = value,
                         .VerticalAlign = "center",
                         .TextAlign = "center",
                         .Bold = True,
                         .BorderBottom = borderStyle,
                         .BorderTop = borderStyle,
                         .BorderLeft = borderStyle,
                         .BorderRight = borderStyle,
                         .Format = "##0.0#%"
                         }
                                  auxRow.AddCell(newCell)
                              End Sub
        'Impresion de datos
        Dim tab As DataTable = SP.Performance(5, 0, 0)

        For i = 0 To tab.Rows.Count - 1
            auxRow = New Row
            Dim indexActualRow As Integer = sheet.Rows.Count
            printf(tab.Rows(i).Item("AGENCIA"), True)
            printf(tab.Rows(i).Item("BF2"), False)
            printf(tab.Rows(i).Item("BF7"), False)
            printf(tab.Rows(i).Item("INST_VIG"), False)
            printf(tab.Rows(i).Item("VIG_NOM"), False)
            printf(tab.Rows(i).Item("VIG_BAN"), False)
            printf(tab.Rows(i).Item("INST_CAS"), False)
            printf(tab.Rows(i).Item("CAS_NOM"), False)
            printf(tab.Rows(i).Item("CAS_BAN"), False)
            printf(tab.Rows(i).Item("TOTAL"), True)

            sheet.AddRow(auxRow)
        Next


        Dim indexTotalRow As Integer = sheet.Rows.Count - 1
        auxRow = New Row
        printf("TOTAL", True)
        printformula("=sum(" & getRange(1, 1, indexTotalRow, 1) & ")", True)
        printformula("=sum(" & getRange(1, 2, indexTotalRow, 2) & ")", True)
        printformula("=sum(" & getRange(1, 3, indexTotalRow, 3) & ")", True)
        printformula("=sum(" & getRange(1, 4, indexTotalRow, 4) & ")", True)
        printformula("=sum(" & getRange(1, 5, indexTotalRow, 5) & ")", True)
        printformula("=sum(" & getRange(1, 6, indexTotalRow, 6) & ")", True)
        printformula("=sum(" & getRange(1, 7, indexTotalRow, 7) & ")", True)
        printformula("=sum(" & getRange(1, 8, indexTotalRow, 8) & ")", True)
        printformula("=sum(" & getRange(1, 9, indexTotalRow, 9) & ")", True)
        sheet.AddRow(auxRow)

        indexTotalRow += 1
        Dim totalCell As String = "J" & indexTotalRow

        auxRow = New Row
        printf(" ", True)
        printporcentaje("=B" & indexTotalRow & "/" & totalCell)
        printporcentaje("=C" & indexTotalRow & "/" & totalCell)
        printporcentaje("=D" & indexTotalRow & "/" & totalCell)
        printporcentaje("=E" & indexTotalRow & "/" & totalCell)
        printporcentaje("=F" & indexTotalRow & "/" & totalCell)
        printporcentaje("=G" & indexTotalRow & "/" & totalCell)
        printporcentaje("=H" & indexTotalRow & "/" & totalCell)
        printporcentaje("=I" & indexTotalRow & "/" & totalCell)
        printporcentaje("=J" & indexTotalRow & "/" & totalCell)

        sheet.AddRow(auxRow)

        auxRow = New Row
        sheet.AddRow(auxRow)

        totalCell = "J" & (indexTotalRow + 1)

        Dim auxIndex As Integer = sheet.Rows.Count + 1

        'MOVIMIENTO
        auxRow = New Row
        printf("MOVIMIENTO", True)
        printf("BANCOS", True)
        printformula(String.Format("=B{0}+C{0}+F{0}+I{0}", indexTotalRow + 1), False)
        printf(" ", False)
        printporcentaje("=C" & auxIndex & "/" & totalCell)
        sheet.AddRow(auxRow)

        auxIndex += 1
        auxRow = New Row
        printf(" ", True)
        printf("PORTAFOLIO", True)
        printformula(String.Format("=E{0}+H{0}", indexTotalRow + 1), False)
        printf(" ", False)
        printporcentaje("=C" & auxIndex & "/" & totalCell)
        sheet.AddRow(auxRow)

        sheet.AddRow(New Row)

        'PRODUCTO
        Dim tabpro As DataTable = SP.Performance(6, 0, 0)

        auxIndex += 1
        auxRow = New Row
        printf("PRODUCTO", True)
        printf("TDC", True)
        printf(tabpro.Rows(0).Item("TDC"), False)
        printf(" ", False)
        printf(tabpro.Rows(0).Item("TDC_porc"), False)
        sheet.AddRow(auxRow)

        auxIndex += 1
        auxRow = New Row
        printf(" ", True)
        printf("INSTITUCIONAL", True)
        printf(tabpro.Rows(0).Item("INST"), False)
        printf(" ", False)
        printf(tabpro.Rows(0).Item("INST_porc"), False)
        sheet.AddRow(auxRow)

        sheet.AddRow(New Row)

        'PORTAFOLIO
        Dim tabpor As DataTable = SP.Performance(7, 0, 0)
        auxIndex += 1
        auxRow = New Row
        printf("PORTAFOLIO", True)
        printf("VIGENTE", True)
        printf(tabpor.Rows(0).Item("VIGENTE"), False)
        printf(" ", False)
        printf(tabpor.Rows(0).Item("VIGENTE_porc"), False)
        sheet.AddRow(auxRow)

        auxIndex += 1
        auxRow = New Row
        printf(" ", True)
        printf("VENCIDO", True)
        printf(tabpor.Rows(0).Item("VENCIDO"), False)
        printf(" ", False)
        printf(tabpor.Rows(0).Item("VENCIDO_porc"), False)
        sheet.AddRow(auxRow)
    End Sub

End Class

Public Class Performance_Agencias2
    Private sheet As Worksheet
    Private anioInicial As Integer = 2020
    Public Sub New()
        sheet = New Worksheet With {.Name = "Agencias2"}
    End Sub

    Public Function crearSheet() As Worksheet
        Try
            createHeader()
            createBody()

        Catch ex As Exception

        End Try
        Return sheet
    End Function


    Private Sub createHeader()
        Dim mesDesc() As String = {"ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE"}
        Dim rowFecha As New Row
        Dim rowTitulos As New Row
        Dim rowDescr As New Row

        sheet.AddRow(rowFecha)
        sheet.AddRow(rowTitulos)
        sheet.AddRow(rowDescr)

        rowFecha.AddCell(New Cell())
        rowFecha.AddCell(New Cell())

        rowTitulos.AddCell(New Cell())
        rowTitulos.AddCell(New Cell())

        rowDescr.AddCell(New Cell() With {.Value = "AGENCIA"})
        rowDescr.AddCell(New Cell() With {.Value = "PORTAFOLIO"})

        For anio = anioInicial To Now.Year
            'Si se está analizando el año actual, hacemos el recorrido de los meses hasta el mes actual.
            'Si se trata de un año anterior al actual, hacemos el recorrido de los 12 meses
            Dim totalMonths As Integer = IIf(anio = Now.Year, Now.Month - 1, 11)
            For mes = 0 To totalMonths
                Dim firstCell As Integer, lastCell As Integer
                rowFecha.AddCell(New Cell() With {.Value = mesDesc(mes) & " - " & anio})
                rowFecha.AddCell(New Cell())
                rowFecha.AddCell(New Cell())
                rowFecha.AddCell(New Cell())
                lastCell = rowFecha.Cells.Count - 1
                firstCell = lastCell - 3
                sheet.AddMergedCells(getRange(0, firstCell, 0, lastCell))

                rowTitulos.AddCell(New Cell() With {.Value = "ASIGNACIÓN"})
                rowTitulos.AddCell(New Cell())
                lastCell = rowTitulos.Cells.Count - 1
                firstCell = lastCell - 1
                sheet.AddMergedCells(getRange(1, firstCell, 1, lastCell))
                rowTitulos.AddCell(New Cell() With {.Value = "RECUPERACIÓN"})
                rowTitulos.AddCell(New Cell())
                lastCell = rowTitulos.Cells.Count - 1
                firstCell = lastCell - 1
                sheet.AddMergedCells(getRange(1, firstCell, 1, lastCell))

                rowDescr.AddCell(New Cell() With {.Value = "# Cuentas"})
                rowDescr.AddCell(New Cell() With {.Value = "Sdo Deudor"})
                rowDescr.AddCell(New Cell() With {.Value = "# Cuentas"})
                rowDescr.AddCell(New Cell() With {.Value = "Monto recuperacion"})
            Next
        Next
        sheet.FrozenColumns = 2
        sheet.FrozenRows = 3
    End Sub
    Private Sub createBody()
        'Estimado programador, si deseas modificar esta parte del código, te recomiendo leer este pequeño
        '   resumen de cómo funciona este metodo.


        'Creamos un hashtable de hashtables de rows
        '
        'El primer nivel de hash está conformado por las agencias (Keys) y hashTables (values).
        '   Esto para llevar un mejor control del worksheet
        '
        'Los segundos hashtables tendran los nombres de los productos (Keys) y rows (values).
        '
        'De esta manera logramos algo como lo siguiente:
        '|->Agencia 1
        '|       |-> Producto 1 -> row del producto 
        '|       |-> Producto 2 -> row del producto
        '|       .
        '|       .
        '|       |-> Producto n -> row del producto
        '|
        '|->Agencia 2
        '|       |-> Producto 1 -> row del producto 
        '|       |-> Producto 2 -> row del producto
        '|       .
        '|       .
        '|       |-> Producto n -> row del producto
        '.
        '.
        '|->Agencia n
        '        |-> Producto 1 -> row del producto 
        '        |-> Producto 2 -> row del producto
        '        .
        '        .
        '        |-> Producto n -> row del producto
        '
        '
        'Ya que tenemos esta estructuta, podemos llenar los rows con la informacion
        '   de cada año y de cada mes para despues imprimirla secuencialmente en la worksheet
        '
        'Espero que te sirva. Suerte!


        'Obetenemos todas las agencias involucradas
        Dim Agencias As DataTable = SP.Performance(0, 0)

        'Procedemos a la creacion del primer hastable de hashtables
        Dim hashAgencias As New Hashtable
        Dim Productos() As String = {"TDC - VIGENTE (BF2)", "TDC -  CASTIGADO (BF7)", "INSTITUCIONAL - VIGENTE TOTAL",
                "INSTITUCIONAL -  VIGENTE INSTALADO (NOMINA)", "INSTITUCIONAL -  VIGENTE NO INSTALADO (BANCOS)",
                "INSTITUCIONAL - CASTIGADO TOTAL", "INSTITUCIONAL - CASTIGADO INSTALADO (NOMINA)", "INSTITUCIONAL - CASTIGADO NO INSTALADO (BANCOS)", "TOTAL"
            }
        For Each agencia As DataRow In Agencias.Rows
            'Creacion del hash de rows

            Dim hashRows As New Hashtable

            For Each producto As String In Productos
                Dim newRow As New Row
                newRow.AddCell(New Cell() With {.Value = agencia(0).ToString})
                newRow.AddCell(New Cell() With {.Value = producto})

                hashRows.Add(producto, newRow)
            Next

            hashAgencias.Add(agencia(0).ToString, hashRows)
        Next

        'Ya que tenemos todo el hash de hashes armado,
        '   podemos proceder a llenarlo con info de mes y año

        For anio = anioInicial To Now.Year
            Dim auxMes As Integer = IIf(anio = Now.Year, Now.Month, 12)
            For mes = 1 To auxMes
                Dim mesData As DataTable = SP.Performance(0, anio, mes)

                'Procesamos cada fila para darle formato correcto
                For Each fila As DataRow In mesData.Rows
                    Try

                        Dim agencia = fila(0).ToString
                        Dim producto = fila(1).ToString

                        Dim rowActual As Row = CType(CType(hashAgencias.Item(agencia), Hashtable).Item(producto), Row)

                        rowActual.AddCell(New Cell With {.Value = Double.Parse(fila(2).ToString), .Format = WorkSheetHelper.FormatInteger})
                        rowActual.AddCell(New Cell With {.Value = Double.Parse(fila(3).ToString), .Format = WorkSheetHelper.FormatMoney})
                        rowActual.AddCell(New Cell With {.Value = Double.Parse(fila(4).ToString), .Format = WorkSheetHelper.FormatInteger})
                        rowActual.AddCell(New Cell With {.Value = Double.Parse(fila(5).ToString), .Format = WorkSheetHelper.FormatMoney})

                        CType(hashAgencias.Item(agencia), Hashtable).Item(producto) = rowActual
                    Catch ex As Exception
                        Dim lol As String = ex.Message
                    End Try
                Next
            Next
        Next

        Dim keys = hashAgencias.Keys.Cast(Of String)().ToArray()

        Array.Sort(keys)
        'Una vez que tenemos todas las filas, Procedemos a agregarlas al excel ordenadamente
        For Each key As String In keys
            For Each producto As String In Productos
                sheet.AddRow(CType(CType(hashAgencias(key), Hashtable)(producto), Row))
            Next
        Next

    End Sub
End Class

Public Class Performance_Plazas
    Private sheet As Worksheet
    Private anioInicial As Integer = 2020
    Private blue As String = "#D9E1F2"
    Private orange As String = "#FCE4D6"
    Public Sub New()
        sheet = New Worksheet With {.Name = "Plazas"}
        sheet.ShowGridLines = False
    End Sub

    Public Function crearSheet() As Worksheet
        Try
            createHeader()
            createBody()
            bordearCeldas()
        Catch ex As Exception

        End Try
        Return sheet
    End Function

    Private Sub createHeader()
        Dim mesDesc() As String = {"ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE"}
        Dim rowFecha As New Row
        Dim rowTitulos As New Row
        Dim rowDescr As New Row


        sheet.AddRow(rowFecha)
        sheet.AddRow(rowTitulos)
        sheet.AddRow(rowDescr)

        rowFecha.AddCell(New Cell())
        rowFecha.AddCell(New Cell())

        rowTitulos.AddCell(New Cell())
        rowTitulos.AddCell(New Cell())

        rowDescr.AddCell(New Cell() With {.Background = orange})
        rowDescr.AddCell(New Cell() With {.Background = orange})

        For anio = anioInicial To Now.Year
            Dim firstCell As Integer, lastCell As Integer
            'Si se está analizando el año actual, hacemos el recorrido de los meses hasta el mes actual.
            'Si se trata de un año anterior al actual, hacemos el recorrido de los 12 meses
            Dim totalMonths As Integer = IIf(anio = Now.Year, Now.Month - 1, 11)
            For mes = 0 To totalMonths
                rowFecha.AddCell(New Cell() With {.Value = mesDesc(mes) & " - " & anio, .Background = blue, .Bold = True})
                rowFecha.AddCell(New Cell() With {.Background = blue})
                rowFecha.AddCell(New Cell() With {.Background = blue})
                rowFecha.AddCell(New Cell() With {.Background = blue})
                rowFecha.AddCell(New Cell() With {.Background = blue})
                rowFecha.AddCell(New Cell() With {.Background = blue})
                rowFecha.AddCell(New Cell() With {.Background = blue})
                lastCell = rowFecha.Cells.Count - 1
                firstCell = lastCell - 6
                sheet.AddMergedCells(getRange(0, firstCell, 0, lastCell))

                rowTitulos.AddCell(New Cell() With {.Value = "ASIGNACIÓN", .Background = blue, .Bold = True})
                rowTitulos.AddCell(New Cell() With {.Background = blue})
                lastCell = rowTitulos.Cells.Count - 1
                firstCell = lastCell - 1
                sheet.AddMergedCells(getRange(1, firstCell, 1, lastCell))
                rowTitulos.AddCell(New Cell() With {.Value = "RECUPERACIÓN", .Background = blue, .Bold = True})
                rowTitulos.AddCell(New Cell() With {.Background = blue})
                rowTitulos.AddCell(New Cell() With {.Background = blue})
                lastCell = rowTitulos.Cells.Count - 1
                firstCell = lastCell - 2
                sheet.AddMergedCells(getRange(1, firstCell, 1, lastCell))
                rowTitulos.AddCell(New Cell() With {.Value = "LIQUIDACIÓN", .Background = blue, .Bold = True})
                rowTitulos.AddCell(New Cell() With {.Background = blue})
                lastCell = rowTitulos.Cells.Count - 1
                firstCell = lastCell - 1
                sheet.AddMergedCells(getRange(1, firstCell, 1, lastCell))

                rowDescr.AddCell(New Cell() With {.Value = "CUENTAS", .Background = blue, .Bold = True})
                rowDescr.AddCell(New Cell() With {.Value = "MONTOS", .Background = blue, .Bold = True})
                rowDescr.AddCell(New Cell() With {.Value = "CUENTAS", .Background = blue, .Bold = True})
                rowDescr.AddCell(New Cell() With {.Value = "MONTOS", .Background = blue, .Bold = True})
                rowDescr.AddCell(New Cell())
                lastCell = rowDescr.Cells.Count - 1
                firstCell = lastCell - 1
                sheet.AddMergedCells(getRange(2, firstCell, 2, lastCell))
                rowDescr.AddCell(New Cell() With {.Value = "%CTAS", .Background = blue, .Bold = True})
                rowDescr.AddCell(New Cell() With {.Value = "MONTO", .Background = blue, .Bold = True})

                rowFecha.AddCell(New Cell())
                rowTitulos.AddCell(New Cell())
                rowDescr.AddCell(New Cell())

            Next
            rowFecha.AddCell(New Cell() With {.Value = " " & anio, .Background = orange, .Bold = True})
            rowFecha.AddCell(New Cell() With {.Background = orange})
            rowFecha.AddCell(New Cell() With {.Background = orange})
            rowFecha.AddCell(New Cell() With {.Background = orange})
            rowFecha.AddCell(New Cell() With {.Background = orange})
            rowFecha.AddCell(New Cell() With {.Background = orange})
            rowFecha.AddCell(New Cell() With {.Background = orange})
            lastCell = rowFecha.Cells.Count - 1
            firstCell = lastCell - 6
            sheet.AddMergedCells(getRange(0, firstCell, 0, lastCell))

            rowTitulos.AddCell(New Cell() With {.Value = "ASIGNACIÓN", .Background = orange, .Bold = True})
            rowTitulos.AddCell(New Cell() With {.Background = orange})
            lastCell = rowTitulos.Cells.Count - 1
            firstCell = lastCell - 1
            sheet.AddMergedCells(getRange(1, firstCell, 1, lastCell))
            rowTitulos.AddCell(New Cell() With {.Value = "RECUPERACIÓN", .Background = orange, .Bold = True})
            rowTitulos.AddCell(New Cell() With {.Background = orange})
            rowTitulos.AddCell(New Cell() With {.Background = orange})
            lastCell = rowTitulos.Cells.Count - 1
            firstCell = lastCell - 2
            sheet.AddMergedCells(getRange(1, firstCell, 1, lastCell))
            rowTitulos.AddCell(New Cell() With {.Value = "LIQUIDACIÓN", .Background = orange, .Bold = True})
            rowTitulos.AddCell(New Cell() With {.Background = orange})
            lastCell = rowTitulos.Cells.Count - 1
            firstCell = lastCell - 1
            sheet.AddMergedCells(getRange(1, firstCell, 1, lastCell))

            rowDescr.AddCell(New Cell() With {.Value = "CUENTAS", .Background = orange, .Bold = True})
            rowDescr.AddCell(New Cell() With {.Value = "MONTOS", .Background = orange, .Bold = True})
            rowDescr.AddCell(New Cell() With {.Value = "CUENTAS", .Background = orange, .Bold = True})
            rowDescr.AddCell(New Cell() With {.Value = "MONTOS", .Background = orange, .Bold = True})
            rowDescr.AddCell(New Cell() With {.Background = orange})
            lastCell = rowDescr.Cells.Count - 1
            firstCell = lastCell - 1
            sheet.AddMergedCells(getRange(2, firstCell, 2, lastCell))
            rowDescr.AddCell(New Cell() With {.Value = "%CTAS", .Background = orange, .Bold = True})
            rowDescr.AddCell(New Cell() With {.Value = "MONTO", .Background = orange, .Bold = True})

            rowFecha.AddCell(New Cell())
            rowTitulos.AddCell(New Cell())
            rowDescr.AddCell(New Cell())
        Next
        sheet.FrozenColumns = 2
        sheet.FrozenRows = 3
    End Sub

    Private Sub createBody()
        Dim dataPlazas As DataTable = SP.Performance(1, 0)
        Dim rowsPlazas As New Hashtable
        Dim firstRow As Integer = sheet.Rows.Count 'Primera fila base 0
        Dim lastRow As Integer = firstRow + dataPlazas.Rows.Count - 1 'Ultima fila base 0
        Dim rowTotales As New Row
        Dim plazaIndex As Integer = 1

        rowTotales.AddCell(New Cell With {.Value = " ", .Background = orange})
        rowTotales.AddCell(New Cell With {.Value = " ", .Background = orange})

        For Each auxRow As DataRow In dataPlazas.Rows
            Dim newRow As New Row
            newRow.AddCell(New Cell With {.Value = plazaIndex, .Bold = True})
            newRow.AddCell(New Cell With {.Value = auxRow(0).ToString, .Bold = True})
            rowsPlazas.Add(auxRow(0).ToString, newRow)
            plazaIndex += 1
        Next

        'Para cada Año 
        For anio = anioInicial To Now.Year
            Dim totalMonths As Integer = IIf(anio = Now.Year, Now.Month, 12)
            'Para cada mes
            For mes = 1 To totalMonths

                'Sacamos la informacion de cada año - mes
                Dim info As DataTable = SP.Performance(1, anio, mes)

                'Para cada row de la informacion 
                For Each plaza_info As DataRow In info.Rows
                    Dim plaza = plaza_info(0).ToString

                    Dim rowPlaza As Row = CType(rowsPlazas(plaza), Row)

                    'Para cada celda de la fila añadimos la informacion a cada fila
                    rowPlaza.AddCell(New Cell With {.Value = Double.Parse(plaza_info(1).ToString), .Format = WorkSheetHelper.FormatInteger})
                    rowPlaza.AddCell(New Cell With {.Value = Double.Parse(plaza_info(2).ToString), .Format = WorkSheetHelper.FormatMoney})
                    rowPlaza.AddCell(New Cell With {.Value = Double.Parse(plaza_info(3).ToString), .Format = WorkSheetHelper.FormatInteger})
                    rowPlaza.AddCell(New Cell With {.Value = Double.Parse(plaza_info(4).ToString), .Format = WorkSheetHelper.FormatMoney})
                    rowPlaza.AddCell(New Cell With {.Value = Double.Parse(plaza_info(5).ToString), .Format = WorkSheetHelper.FormatPercentage})
                    rowPlaza.AddCell(New Cell With {.Value = Double.Parse(plaza_info(6).ToString), .Format = WorkSheetHelper.FormatPercentage})
                    rowPlaza.AddCell(New Cell With {.Value = Double.Parse(plaza_info(7).ToString), .Format = WorkSheetHelper.FormatPercentage})

                    rowPlaza.AddCell(New Cell)


                    rowsPlazas(plaza) = rowPlaza
                Next
                Dim TotalColumnStarts As Integer = (mes - 1) * 8 + 2
                rowTotales.AddCell(New Cell With {.Formula = "=SUM(" & getRange(firstRow, TotalColumnStarts + 0, lastRow, TotalColumnStarts + 0) & ")", .Format = WorkSheetHelper.FormatInteger, .Background = blue, .Bold = True})
                rowTotales.AddCell(New Cell With {.Formula = "=SUM(" & getRange(firstRow, TotalColumnStarts + 1, lastRow, TotalColumnStarts + 1) & ")", .Format = WorkSheetHelper.FormatMoney, .Background = blue, .Bold = True})
                rowTotales.AddCell(New Cell With {.Formula = "=SUM(" & getRange(firstRow, TotalColumnStarts + 2, lastRow, TotalColumnStarts + 2) & ")", .Format = WorkSheetHelper.FormatInteger, .Background = blue, .Bold = True})
                rowTotales.AddCell(New Cell With {.Formula = "=SUM(" & getRange(firstRow, TotalColumnStarts + 3, lastRow, TotalColumnStarts + 3) & ")", .Format = WorkSheetHelper.FormatMoney, .Background = blue, .Bold = True})
                rowTotales.AddCell(New Cell With {.Formula = "=SUM(" & getRange(firstRow, TotalColumnStarts + 4, lastRow, TotalColumnStarts + 4) & ")", .Format = WorkSheetHelper.FormatPercentage, .Background = blue, .Bold = True})
                rowTotales.AddCell(New Cell With {.Formula = "=0", .Format = WorkSheetHelper.FormatPercentage, .Background = blue, .Bold = True})
                rowTotales.AddCell(New Cell With {.Formula = "=0", .Format = WorkSheetHelper.FormatPercentage, .Background = blue, .Bold = True})

                rowTotales.AddCell(New Cell)

            Next
        Next
        Dim keyPlazas() = rowsPlazas.Keys.Cast(Of String)().ToArray()
        Array.Sort(keyPlazas)
        'Añadimos a la hoja todas las filas generdas
        For Each key As String In keyPlazas
            sheet.AddRow(CType(rowsPlazas(key), Row))
        Next
        sheet.AddRow(rowTotales)

    End Sub

    Private Sub bordearCeldas()
        For Each auxRow As Row In sheet.Rows
            Try
                For Each celda As Cell In auxRow.Cells

                    celda.BorderBottom = BorderSlim()
                    celda.BorderLeft = BorderSlim()
                    celda.BorderRight = BorderSlim()
                    celda.BorderTop = BorderSlim()
                Next
            Catch ex As Exception

            End Try
        Next
    End Sub
End Class

Public Class Performance_GestionAgencias
    Private border As New BorderStyle With {.Color = "#0070C0", .Size = 1}
    Private sheet As Worksheet
    Public Sub New()
        sheet = New Worksheet With {.Name = "GestionAgencias"}
    End Sub

    Public Function crearSheet() As Worksheet
        Try
            createHeader()
            createBody()


        Catch ex As Exception

        End Try
        Return sheet
    End Function

    Private Sub createBody()
        Dim toNumber = Function(cadena As String) As Double
                           Dim numero As Double = 0
                           Try
                               numero = Double.Parse(cadena)
                           Catch ex As Exception

                           End Try
                           Return numero
                       End Function

        Dim printTotalRow = Sub(agencia As String, totales() As Integer, IsTotalGeneral As Boolean)
                                Dim bg As String = IIf(IsTotalGeneral, "#DDEBF7", "#ACB9CA")
                                Dim TotalRow As Integer = sheet.Rows.Count + 1
                                Dim newRow As New Row
                                Dim printCell = Sub(value As String, isFormula As Boolean)
                                                    Dim _cell As New Cell With {
                                                         .Background = bg,
                                                         .BorderBottom = border,
                                                         .BorderLeft = border,
                                                         .BorderRight = border,
                                                         .BorderTop = border,
                                                         .Bold = True
                                                         }
                                                    If isFormula Then
                                                        _cell.Formula = value
                                                    Else
                                                        _cell.Value = value
                                                    End If
                                                    newRow.AddCell(_cell)
                                                End Sub
                                printCell("TOTAL " & IIf(IsTotalGeneral, "GENERAL", agencia), False)
                                printCell(" ", False)
                                printCell(totales(0), False)
                                printCell(totales(1), False)
                                printCell("=C" & TotalRow & "/" & "G" & TotalRow, True)
                                printCell("=D" & TotalRow & "/" & "G" & TotalRow, True)
                                printCell(totales(2), False)
                                printCell(totales(3), False)
                                printCell(totales(4), False)
                                printCell(totales(5), False)
                                printCell("=H" & TotalRow & "/" & "N" & TotalRow, True)
                                printCell("=0", True)
                                printCell("=I" & TotalRow & "/" & "N" & TotalRow, True)
                                printCell(totales(6), False)
                                printCell(totales(7), False)
                                printCell(totales(8), False)
                                printCell("=O" & TotalRow & "/" & "S" & TotalRow, True)
                                printCell("=P" & TotalRow & "/" & "S" & TotalRow, True)
                                printCell(totales(9), False)
                                printCell(totales(10), False)
                                sheet.AddRow(newRow)
                            End Sub
        Dim data As DataTable = SP.Performance(3)
        sheet.Columns = New List(Of Column)
        sheet.Columns.Add(New Column() With {.Width = 200})
        sheet.Columns.Add(New Column() With {.Width = 200})
        Dim totalAgencia() As Integer = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
        Dim totalGeneral() As Integer = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
        Dim plazaAnterior As String = data.Rows(0)(0).ToString
        For Each auxRow As DataRow In data.Rows
            totalGeneral(0) += toNumber(auxRow("Cuentas_Contacto_BF2").ToString)
            totalGeneral(1) += toNumber(auxRow("Cuentas_NO_Contacto_BF2").ToString)
            totalGeneral(2) += toNumber(auxRow("total_BF2").ToString)
            totalGeneral(3) += toNumber(auxRow("Cuentas_Contacto_BF7").ToString)
            totalGeneral(4) += toNumber(auxRow("Estrategia").ToString)
            totalGeneral(5) += toNumber(auxRow("Cuentas_NO_Contacto_BF7").ToString)
            totalGeneral(6) += toNumber(auxRow("total_BF7").ToString)
            totalGeneral(7) += toNumber(auxRow("Cuentas_Contacto_INST").ToString)
            totalGeneral(8) += toNumber(auxRow("Cuentas_NO_Contacto_INST").ToString)
            totalGeneral(9) += toNumber(auxRow("total_INST").ToString)
            totalGeneral(10) += toNumber(auxRow("TOTAL_GEN").ToString)

            If plazaAnterior = auxRow(0).ToString Then
                totalAgencia(0) += toNumber(auxRow("Cuentas_Contacto_BF2").ToString)
                totalAgencia(1) += toNumber(auxRow("Cuentas_NO_Contacto_BF2").ToString)
                totalAgencia(2) += toNumber(auxRow("total_BF2").ToString)
                totalAgencia(3) += toNumber(auxRow("Cuentas_Contacto_BF7").ToString)
                totalGeneral(4) += toNumber(auxRow("Estrategia").ToString)
                totalAgencia(5) += toNumber(auxRow("Cuentas_NO_Contacto_BF7").ToString)
                totalAgencia(6) += toNumber(auxRow("total_BF7").ToString)
                totalAgencia(7) += toNumber(auxRow("Cuentas_Contacto_INST").ToString)
                totalAgencia(8) += toNumber(auxRow("Cuentas_NO_Contacto_INST").ToString)
                totalAgencia(9) += toNumber(auxRow("total_INST").ToString)
                totalAgencia(10) += toNumber(auxRow("TOTAL_GEN").ToString)
            Else
                printTotalRow(plazaAnterior, totalAgencia, False)
                plazaAnterior = auxRow(0).ToString
                totalAgencia(0) = toNumber(auxRow("Cuentas_Contacto_BF2").ToString)
                totalAgencia(1) = toNumber(auxRow("Cuentas_NO_Contacto_BF2").ToString)
                totalAgencia(2) = toNumber(auxRow("total_BF2").ToString)
                totalAgencia(3) = toNumber(auxRow("Cuentas_Contacto_BF7").ToString)
                totalGeneral(4) = toNumber(auxRow("Estrategia").ToString)
                totalAgencia(5) = toNumber(auxRow("Cuentas_NO_Contacto_BF7").ToString)
                totalAgencia(6) = toNumber(auxRow("total_BF7").ToString)
                totalAgencia(7) = toNumber(auxRow("Cuentas_Contacto_INST").ToString)
                totalAgencia(8) = toNumber(auxRow("Cuentas_NO_Contacto_INST").ToString)
                totalAgencia(9) = toNumber(auxRow("total_INST").ToString)
                totalAgencia(10) = toNumber(auxRow("TOTAL_GEN").ToString)
            End If



            Dim _Row As New Row
            For Each auxCell In auxRow.ItemArray
                _Row.AddCell(New Cell() With {.Value = IIf(auxCell.ToString = "", 0, auxCell)})
            Next
            sheet.AddRow(_Row)
        Next
        printTotalRow(plazaAnterior, totalAgencia, False)
        printTotalRow("", totalGeneral, True)
    End Sub

    Private Sub createHeader()
        Dim mesDesc() As String = {"ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE"}
        Dim firstCell As Integer, lastCell As Integer

        Dim auxRow As New Row
        auxRow.AddCell(New Cell() With {.Value = mesDesc(Now.Month - 1) & " - " & Now.Year})
        sheet.AddRow(auxRow)

        auxRow = New Row
        auxRow.AddCell(New Cell() With {.Value = "GESTION / DICTAMEN AGENCIAS"})
        sheet.AddRow(auxRow)

        auxRow = New Row
        auxRow.AddCell(New Cell())
        auxRow.AddCell(New Cell())
        auxRow.AddCell(New Cell() With {.Value = "TDC- VIGENTE (BF2)", .Background = "#DDEBF7", .BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border, .Bold = True})
        auxRow.AddCell(New Cell() With {.BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border})
        auxRow.AddCell(New Cell() With {.BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border})
        auxRow.AddCell(New Cell() With {.BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border})
        auxRow.AddCell(New Cell() With {.BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border})
        lastCell = auxRow.Cells.Count - 1
        firstCell = lastCell - 4
        sheet.AddMergedCells(getRange(2, firstCell, 2, lastCell))
        auxRow.AddCell(New Cell() With {.Value = "TDC - CASTIGADO (BF7)", .Background = "#DDEBF7", .BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border, .Bold = True})
        auxRow.AddCell(New Cell() With {.BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border})
        auxRow.AddCell(New Cell() With {.BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border})
        auxRow.AddCell(New Cell() With {.BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border})
        auxRow.AddCell(New Cell() With {.BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border})
        auxRow.AddCell(New Cell() With {.BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border})
        auxRow.AddCell(New Cell() With {.BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border})
        lastCell = auxRow.Cells.Count - 1
        firstCell = lastCell - 6
        sheet.AddMergedCells(getRange(2, firstCell, 2, lastCell))
        auxRow.AddCell(New Cell() With {.Value = "INST", .Background = "#DDEBF7", .BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border, .Bold = True})
        auxRow.AddCell(New Cell() With {.BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border})
        auxRow.AddCell(New Cell() With {.BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border})
        auxRow.AddCell(New Cell() With {.BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border})
        auxRow.AddCell(New Cell() With {.BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border})
        lastCell = auxRow.Cells.Count - 1
        firstCell = lastCell - 4
        sheet.AddMergedCells(getRange(2, firstCell, 2, lastCell))
        auxRow.AddCell(New Cell())

        sheet.AddRow(auxRow)

        auxRow = New Row
        auxRow.AddCell(New Cell() With {.Value = "PLAZA", .Background = "#5B9BD5", .Color = "white", .BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border, .Bold = True})
        auxRow.AddCell(New Cell() With {.Value = "CR=", .Background = "#5B9BD5", .Color = "white", .BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border, .Bold = True})

        auxRow.AddCell(New Cell() With {.Value = "CONTACTADA", .Background = "#5B9BD5", .Color = "white", .BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border, .Bold = True})
        auxRow.AddCell(New Cell() With {.Value = "NO CONTACTADA", .Background = "#5B9BD5", .Color = "white", .BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border, .Bold = True})
        auxRow.AddCell(New Cell() With {.Value = "% CONT", .Background = "#5B9BD5", .Color = "white", .BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border, .Bold = True})
        auxRow.AddCell(New Cell() With {.Value = "% NO CONTAC", .Background = "#5B9BD5", .Color = "white", .BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border, .Bold = True})
        auxRow.AddCell(New Cell() With {.Value = "Total BF2", .Background = "#5B9BD5", .Color = "white", .BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border, .Bold = True})

        auxRow.AddCell(New Cell() With {.Value = "CONTACTADA", .Background = "#5B9BD5", .Color = "white", .BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border, .Bold = True})
        auxRow.AddCell(New Cell() With {.Value = "ESTRATEGIA - STAND BEY", .Background = "#5B9BD5", .Color = "white", .BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border, .Bold = True})
        auxRow.AddCell(New Cell() With {.Value = "NO CONTACTADA", .Background = "#5B9BD5", .Color = "white", .BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border, .Bold = True})
        auxRow.AddCell(New Cell() With {.Value = "% CONT", .Background = "#5B9BD5", .Color = "white", .BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border, .Bold = True})
        auxRow.AddCell(New Cell() With {.Value = "% ST BEY", .Background = "#5B9BD5", .Color = "white", .BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border, .Bold = True})
        auxRow.AddCell(New Cell() With {.Value = "% NO CONTAC", .Background = "#5B9BD5", .Color = "white", .BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border, .Bold = True})
        auxRow.AddCell(New Cell() With {.Value = "Total BF7", .Background = "#5B9BD5", .Color = "white", .BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border, .Bold = True})

        auxRow.AddCell(New Cell() With {.Value = "CONTACTADA", .Background = "#5B9BD5", .Color = "white", .BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border, .Bold = True})
        auxRow.AddCell(New Cell() With {.Value = "NO CONTACTADA", .Background = "#5B9BD5", .Color = "white", .BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border, .Bold = True})
        auxRow.AddCell(New Cell() With {.Value = "% CONT", .Background = "#5B9BD5", .Color = "white", .BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border, .Bold = True})
        auxRow.AddCell(New Cell() With {.Value = "% NO CONTAC", .Background = "#5B9BD5", .Color = "white", .BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border, .Bold = True})
        auxRow.AddCell(New Cell() With {.Value = "Total INST", .Background = "#5B9BD5", .Color = "white", .BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border, .Bold = True})

        auxRow.AddCell(New Cell() With {.Value = "TOTAL GENERAL", .Background = "#5B9BD5", .Color = "white", .BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border, .Bold = True})
        sheet.AddRow(auxRow)

    End Sub
End Class

Public Class Performance_GestionPlaza
    Private sheet As Worksheet
    Private border As New BorderStyle With {.Color = "#0070C0", .Size = 1}

    Public Sub New()
        sheet = New Worksheet With {.Name = "GestionPlaza"}
    End Sub

    Public Function crearSheet() As Worksheet
        Try
            createHeader()
            createBody()


        Catch ex As Exception

        End Try
        Return sheet
    End Function

    Private Sub createHeader()
        Dim mesDesc() As String = {"ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE"}
        Dim firstCell As Integer, lastCell As Integer

        Dim auxRow As New Row
        auxRow.AddCell(New Cell() With {.Value = mesDesc(Now.Month - 1) & " - " & Now.Year})
        sheet.AddRow(auxRow)

        auxRow = New Row
        auxRow.AddCell(New Cell() With {.Value = "GESTION / DICTAMEN PLAZAS"})
        sheet.AddRow(auxRow)

        auxRow = New Row
        auxRow.AddCell(New Cell())
        auxRow.AddCell(New Cell())
        auxRow.AddCell(New Cell() With {.Value = "TDC- VIGENTE (BF2)", .Background = "#DDEBF7", .BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border, .Bold = True})
        auxRow.AddCell(New Cell() With {.BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border})
        auxRow.AddCell(New Cell() With {.BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border})
        auxRow.AddCell(New Cell() With {.BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border})
        auxRow.AddCell(New Cell() With {.BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border})
        lastCell = auxRow.Cells.Count - 1
        firstCell = lastCell - 4
        sheet.AddMergedCells(getRange(2, firstCell, 2, lastCell))
        auxRow.AddCell(New Cell() With {.Value = "TDC - CASTIGADO (BF7)", .Background = "#DDEBF7", .BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border, .Bold = True})
        auxRow.AddCell(New Cell() With {.BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border})
        auxRow.AddCell(New Cell() With {.BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border})
        auxRow.AddCell(New Cell() With {.BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border})
        auxRow.AddCell(New Cell() With {.BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border})
        lastCell = auxRow.Cells.Count - 1
        firstCell = lastCell - 4
        sheet.AddMergedCells(getRange(2, firstCell, 2, lastCell))
        auxRow.AddCell(New Cell() With {.Value = "INST", .Background = "#DDEBF7", .BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border, .Bold = True})
        auxRow.AddCell(New Cell() With {.BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border})
        auxRow.AddCell(New Cell() With {.BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border})
        auxRow.AddCell(New Cell() With {.BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border})
        auxRow.AddCell(New Cell() With {.BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border})
        lastCell = auxRow.Cells.Count - 1
        firstCell = lastCell - 4
        sheet.AddMergedCells(getRange(2, firstCell, 2, lastCell))
        auxRow.AddCell(New Cell())

        sheet.AddRow(auxRow)

        auxRow = New Row
        auxRow.AddCell(New Cell() With {.Value = "PLAZA", .Background = "#5B9BD5", .Color = "white", .BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border, .Bold = True})
        auxRow.AddCell(New Cell() With {.Value = "CR=", .Background = "#5B9BD5", .Color = "white", .BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border, .Bold = True})

        auxRow.AddCell(New Cell() With {.Value = "CONTACTADA", .Background = "#5B9BD5", .Color = "white", .BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border, .Bold = True})
        auxRow.AddCell(New Cell() With {.Value = "NO CONTACTADA", .Background = "#5B9BD5", .Color = "white", .BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border, .Bold = True})
        auxRow.AddCell(New Cell() With {.Value = "% CONT", .Background = "#5B9BD5", .Color = "white", .BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border, .Bold = True})
        auxRow.AddCell(New Cell() With {.Value = "% NO CONTAC", .Background = "#5B9BD5", .Color = "white", .BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border, .Bold = True})
        auxRow.AddCell(New Cell() With {.Value = "Total BF2", .Background = "#5B9BD5", .Color = "white", .BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border, .Bold = True})

        auxRow.AddCell(New Cell() With {.Value = "CONTACTADA", .Background = "#5B9BD5", .Color = "white", .BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border, .Bold = True})
        auxRow.AddCell(New Cell() With {.Value = "NO CONTACTADA", .Background = "#5B9BD5", .Color = "white", .BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border, .Bold = True})
        auxRow.AddCell(New Cell() With {.Value = "% CONT", .Background = "#5B9BD5", .Color = "white", .BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border, .Bold = True})
        auxRow.AddCell(New Cell() With {.Value = "% NO CONTAC", .Background = "#5B9BD5", .Color = "white", .BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border, .Bold = True})
        auxRow.AddCell(New Cell() With {.Value = "Total BF7", .Background = "#5B9BD5", .Color = "white", .BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border, .Bold = True})

        auxRow.AddCell(New Cell() With {.Value = "CONTACTADA", .Background = "#5B9BD5", .Color = "white", .BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border, .Bold = True})
        auxRow.AddCell(New Cell() With {.Value = "NO CONTACTADA", .Background = "#5B9BD5", .Color = "white", .BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border, .Bold = True})
        auxRow.AddCell(New Cell() With {.Value = "% CONT", .Background = "#5B9BD5", .Color = "white", .BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border, .Bold = True})
        auxRow.AddCell(New Cell() With {.Value = "% NO CONTAC", .Background = "#5B9BD5", .Color = "white", .BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border, .Bold = True})
        auxRow.AddCell(New Cell() With {.Value = "Total INST", .Background = "#5B9BD5", .Color = "white", .BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border, .Bold = True})

        auxRow.AddCell(New Cell() With {.Value = "TOTAL GENERAL", .Background = "#5B9BD5", .Color = "white", .BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border, .Bold = True})
        sheet.AddRow(auxRow)

    End Sub
    Private Sub createBody()
        Dim toNumber = Function(cadena As String) As Double
                           Dim numero As Double = 0
                           Try
                               numero = Double.Parse(cadena)
                           Catch ex As Exception

                           End Try
                           Return numero
                       End Function

        Dim printTotalRow = Sub(plaza As String, totales() As Integer, IsTotalGeneral As Boolean)
                                Dim bg As String = IIf(IsTotalGeneral, "#DDEBF7", "#ACB9CA")
                                Dim TotalRow As Integer = sheet.Rows.Count + 1
                                Dim newRow As New Row
                                Dim printCell = Sub(value As String, isFormula As Boolean)
                                                    Dim _cell As New Cell With {
                                                         .Background = bg,
                                                         .BorderBottom = border,
                                                         .BorderLeft = border,
                                                         .BorderRight = border,
                                                         .BorderTop = border,
                                                         .Bold = True
                                                         }
                                                    If isFormula Then
                                                        _cell.Formula = value
                                                    Else
                                                        _cell.Value = value
                                                    End If
                                                    newRow.AddCell(_cell)
                                                End Sub
                                printCell("TOTAL " & IIf(IsTotalGeneral, "GENERAL", plaza), False)
                                printCell(" ", False)
                                printCell(totales(0), False)
                                printCell(totales(1), False)
                                printCell("=C" & TotalRow & "/" & "G" & TotalRow, True)
                                printCell("=D" & TotalRow & "/" & "G" & TotalRow, True)
                                printCell(totales(2), False)
                                printCell(totales(3), False)
                                printCell(totales(4), False)
                                printCell("=H" & TotalRow & "/" & "L" & TotalRow, True)
                                printCell("=I" & TotalRow & "/" & "L" & TotalRow, True)
                                printCell(totales(5), False)
                                printCell(totales(6), False)
                                printCell(totales(7), False)
                                printCell("=M" & TotalRow & "/" & "Q" & TotalRow, True)
                                printCell("=N" & TotalRow & "/" & "Q" & TotalRow, True)
                                printCell(totales(8), False)
                                printCell(totales(9), False)
                                sheet.AddRow(newRow)
                            End Sub
        Dim data As DataTable = SP.Performance(4)
        sheet.Columns = New List(Of Column)
        sheet.Columns.Add(New Column() With {.Width = 200})
        sheet.Columns.Add(New Column() With {.Width = 200})
        Dim totalPlaza() As Integer = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
        Dim totalGeneral() As Integer = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
        Dim plazaAnterior As String = data.Rows(0)(0).ToString
        For Each auxRow As DataRow In data.Rows
            totalGeneral(0) += toNumber(auxRow("Cuentas_Contacto_BF2").ToString)
            totalGeneral(1) += toNumber(auxRow("Cuentas_NO_Contacto_BF2").ToString)
            totalGeneral(2) += toNumber(auxRow("total_BF2").ToString)
            totalGeneral(3) += toNumber(auxRow("Cuentas_Contacto_BF7").ToString)
            totalGeneral(4) += toNumber(auxRow("Cuentas_NO_Contacto_BF7").ToString)
            totalGeneral(5) += toNumber(auxRow("total_BF7").ToString)
            totalGeneral(6) += toNumber(auxRow("Cuentas_Contacto_INST").ToString)
            totalGeneral(7) += toNumber(auxRow("Cuentas_NO_Contacto_INST").ToString)
            totalGeneral(8) += toNumber(auxRow("total_INST").ToString)
            totalGeneral(9) += toNumber(auxRow("TOTAL_GEN").ToString)

            If plazaAnterior = auxRow(0).ToString Then
                totalPlaza(0) += toNumber(auxRow("Cuentas_Contacto_BF2").ToString)
                totalPlaza(1) += toNumber(auxRow("Cuentas_NO_Contacto_BF2").ToString)
                totalPlaza(2) += toNumber(auxRow("total_BF2").ToString)
                totalPlaza(3) += toNumber(auxRow("Cuentas_Contacto_BF7").ToString)
                totalPlaza(4) += toNumber(auxRow("Cuentas_NO_Contacto_BF7").ToString)
                totalPlaza(5) += toNumber(auxRow("total_BF7").ToString)
                totalPlaza(6) += toNumber(auxRow("Cuentas_Contacto_INST").ToString)
                totalPlaza(7) += toNumber(auxRow("Cuentas_NO_Contacto_INST").ToString)
                totalPlaza(8) += toNumber(auxRow("total_INST").ToString)
                totalPlaza(9) += toNumber(auxRow("TOTAL_GEN").ToString)
            Else
                printTotalRow(plazaAnterior, totalPlaza, False)
                plazaAnterior = auxRow(0).ToString
                totalPlaza(0) = toNumber(auxRow("Cuentas_Contacto_BF2").ToString)
                totalPlaza(1) = toNumber(auxRow("Cuentas_NO_Contacto_BF2").ToString)
                totalPlaza(2) = toNumber(auxRow("total_BF2").ToString)
                totalPlaza(3) = toNumber(auxRow("Cuentas_Contacto_BF7").ToString)
                totalPlaza(4) = toNumber(auxRow("Cuentas_NO_Contacto_BF7").ToString)
                totalPlaza(5) = toNumber(auxRow("total_BF7").ToString)
                totalPlaza(6) = toNumber(auxRow("Cuentas_Contacto_INST").ToString)
                totalPlaza(7) = toNumber(auxRow("Cuentas_NO_Contacto_INST").ToString)
                totalPlaza(8) = toNumber(auxRow("total_INST").ToString)
                totalPlaza(9) = toNumber(auxRow("TOTAL_GEN").ToString)
            End If



            Dim _Row As New Row
            For Each auxCell In auxRow.ItemArray
                _Row.AddCell(New Cell() With {.Value = IIf(auxCell.ToString = "", 0, auxCell)})
            Next
            sheet.AddRow(_Row)
        Next
        printTotalRow(plazaAnterior, totalPlaza, False)
        printTotalRow("", totalGeneral, True)
    End Sub
End Class
