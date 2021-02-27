Imports System.Data
Imports Microsoft.VisualBasic
Imports Telerik.Web.Spreadsheet

Public Class WorkSheetHelper

    Public Const FormatPercentage As String = "#0.0#%"
    Public Const FormatDecimal As String = "###,###,###,##0.00"
    Public Const FormatInteger As String = "###,###,###,###"
    Public Const FormatMoney As String = "$#,##0.00;[Red]$#,##0.00"
    Public Const FormatClean As String = ""

    Public Shared Function BorderSlim(Optional color As String = "black") As BorderStyle
        Return New BorderStyle With {.Color = color, .Size = 1}
    End Function
    Public Shared Function BorderNormal(Optional color As String = "black") As BorderStyle
        Return New BorderStyle With {.Color = color, .Size = 2}
    End Function
    Public Shared Function BorderHeavy(Optional color As String = "black") As BorderStyle
        Return New BorderStyle With {.Color = color, .Size = 3}
    End Function

    Public Shared Function generateWorkSheet(title As String, rows_number As Integer, cols_number As Integer) As Worksheet
        Dim sheet As New Worksheet With {.Name = title}
        For row_number = 0 To rows_number
            Dim auxRow As New Row
            For col_number = 0 To cols_number
                auxRow.AddCell(New Cell())
            Next
            sheet.AddRow(auxRow)
        Next
        Return sheet
    End Function



    Public Shared Sub print(ByRef celda As Cell, Optional value As Object = Nothing, Optional color As String = "white")
        celda.Value = value
        celda.Background = color
        Dim bordeNegro As New BorderStyle With {
            .Color = "black"
        }
        celda.BorderBottom = bordeNegro
        celda.BorderLeft = bordeNegro
        celda.BorderRight = bordeNegro
        celda.BorderTop = bordeNegro
        celda.TextAlign = "center"
        celda.VerticalAlign = "center"
    End Sub

    Public Shared Function crearRow(num_cells As Integer) As Row
        Dim row1 As New Row

        For i = 0 To num_cells
            Dim celda As New Cell()

            row1.AddCell(celda)
        Next
        Return row1
    End Function

    ''' <summary>
    ''' Calcula un rango de columnas en la misma fila
    ''' </summary>
    ''' <param name="fila">Fila en la que se requere el rango</param>
    ''' <param name="desde">Columna de inicio en formato de letra</param>
    ''' <param name="hasta">El numero de posiciones que se va a abarcar el rango</param>
    ''' <returns></returns>
    Public Shared Function getColumnsRange(fila As Integer, desde As String, hasta As Integer) As String
        Dim empiezaEn As String = desde & fila
        Dim terminaEn As String = moveColumn(desde, hasta) & fila
        Dim resultado As String = (empiezaEn & ":" & terminaEn).Replace(" ", "")
        Return resultado
    End Function

    ''' <summary>
    ''' Calcula y regresa el rango establecido en formato de letra
    ''' </summary>
    ''' <param name="row1">Fila base 0</param>
    ''' <param name="col1">Columna base 0</param>
    ''' <param name="row2">Fila base 0</param>
    ''' <param name="col2">Columna base 0</param>
    ''' <returns></returns>
    Public Shared Function getRange(row1 As Integer, col1 As Integer, row2 As Integer, col2 As Integer) As String
        Dim celda1 As String = moveColumn("A", col1) & (row1 + 1)
        Dim celda2 As String = moveColumn("A", col2) & (row2 + 1)
        Return celda1 & ":" & celda2
    End Function


    ''' <summary>
    ''' Calcula la posicion de la siguiente columna basado en la columna que se le proporciona y el numero de columnas que se desplazará
    ''' </summary>
    ''' <param name="column">Letra de la columna. Soporta hasta 3 letras (de A hasta ZZZ)</param>
    ''' <param name="num_cols">Numero de espacios a desplazar</param>
    ''' <returns></returns>
    Public Shared Function moveColumn(column As String, num_cols As Integer) As String
        Dim col_1 As Char = "", col_2 As Char = "", col_3 As Char = ""

        'Se decompone la columna en 3

        Try
            If column.Length = 3 Then
                col_1 = column.Chars(0)
                col_2 = column.Chars(1)
                col_3 = column.Chars(2)
            ElseIf column.Length = 2 Then
                col_1 = ""
                col_2 = column.Chars(0)
                col_3 = column.Chars(1)
            ElseIf column.Length = 1 Then
                col_1 = ""
                col_2 = ""
                col_3 = column.Chars(0)
            End If
        Catch ex As Exception

        End Try


        For i = 1 To num_cols
            If col_3 = "Z" Then
                col_3 = "A"
                If col_2 = "Z" Then
                    col_2 = "A"
                    If col_1 = "Z" Then
                        Throw New Exception("Solo es posible llegar hasta la columna 'ZZZ'")
                    Else
                        col_1 = IIf(col_1 = vbNullChar, "A", Chr(Asc(col_1) + 1))
                    End If
                Else
                    col_2 = IIf(col_2 = vbNullChar, "A", Chr(Asc(col_2) + 1))
                End If
            Else
                col_3 = Chr(Asc(col_3) + 1)
            End If
        Next

        Dim isnull = Function(c As Char) As String
                         Return IIf(c = vbNullChar, "", c)
                     End Function

        Return isnull(col_1) & isnull(col_2) & isnull(col_3)

    End Function

    Public Shared Function IndexesToExcelFormat(rowIndex As Integer, cellIndex As Integer) As String
        Dim baseCol As String = "A"
        baseCol = moveColumn(baseCol, cellIndex)
        Return baseCol & (rowIndex + 1)
    End Function
    ''' <summary>
    ''' Bloquea todas las celdas de una hoja para que no puedan hacer modificaciones
    ''' </summary>
    ''' <param name="sheet">Hoja para bloquear</param>
    Public Shared Sub LockSheet(ByRef sheet As Worksheet)
        Try
            For Each auxrow As Row In sheet.Rows
                Try
                    For Each celda As Cell In auxrow.Cells
                        celda.Enable = False
                    Next
                Catch ex As Exception

                End Try
            Next
        Catch ex As Exception

        End Try
    End Sub

    Public Class GroupedSheetGenerator

        Private ColsToAnalize As List(Of Integer)

        Private colsToSum As List(Of Integer)
        Private colsToCount As List(Of Integer)
        Private colsToGroup As List(Of Integer)
        Private formatos As Hashtable
        Private LastGroupValues As Hashtable

        Private dt As DataTable

        Private colors() As String = {"#ffa41b", "#000839", "#005082", "#00a8cc"}

        Public Sub New(dt As DataTable, colsConfig() As ColumnConfig)
            Me.dt = dt
            ColsToAnalize = New List(Of Integer)
            colsToSum = New List(Of Integer)
            colsToCount = New List(Of Integer)
            colsToGroup = New List(Of Integer)

            LastGroupValues = New Hashtable
            formatos = New Hashtable

            For Each colconf As ColumnConfig In colsConfig
                Dim _numCol As Integer = colconf.NumCol
                If Not ColsToAnalize.Contains(_numCol) Then
                    ColsToAnalize.Add(_numCol)
                    formatos.Add(_numCol, colconf.Format)
                    Select Case colconf.Action
                        Case ColumnConfig.ColumnAction.Group
                            colsToGroup.Add(_numCol)
                            LastGroupValues.Add(_numCol, "")
                        Case ColumnConfig.ColumnAction.Sum
                            colsToSum.Add(_numCol)
                        Case ColumnConfig.ColumnAction.Count
                            colsToCount.Add(_numCol)
                    End Select
                End If
            Next
            Dim maximo = Aggregate cols In ColsToAnalize Into _max = Max(cols)
            If maximo > dt.Columns.Count - 1 Then
                Throw New Exception("No se pueden analizar las " & maximo + 1 & " columnas. El DataTable solo tiene " & dt.Columns.Count & " columnas.")
            End If
        End Sub


        Public Function GenerateRows() As List(Of Row)
            Dim allRows As New List(Of Row)

            Dim firstRow As DataRow = dt(0)
            Dim GroupChanged As New Hashtable
            Dim keygrupos() = colsToGroup.Cast(Of Integer)().ToArray()
            Array.Sort(keygrupos)
            Array.Reverse(keygrupos)

            For Each col As Integer In keygrupos
                LastGroupValues(col) = firstRow(col).ToString
                GroupChanged.Add(col, False)
            Next
            Dim totalCols As Integer = dt(0).ItemArray.Length - 1

            For rowNumber = 0 To dt.Rows.Count - 1
                Try

                    Dim dtRow As DataRow = dt.Rows(rowNumber)
                    Dim newRow As New Row
                    For actualCol As Integer = 0 To totalCols
                        Dim valor As String = dtRow(actualCol).ToString
                        If ColsToAnalize.Contains(actualCol) Then
                            If colsToSum.Contains(actualCol) Then
                                Dim sValor As Double = 0
                                Try
                                    sValor = Double.Parse(valor)
                                Catch ex As Exception

                                End Try
                                newRow.AddCell(New Cell With {.Value = sValor, .Format = formatos(actualCol)})
                            ElseIf colsToCount.Contains(actualCol) Then
                                Dim cValor As Double = 0
                                Try
                                    cValor = Double.Parse(valor)
                                Catch ex As Exception

                                End Try
                                newRow.AddCell(New Cell With {.Value = cValor, .Format = formatos(actualCol)})
                            ElseIf colsToGroup.Contains(actualCol) Then
                                GroupChanged(actualCol) = IIf(LastGroupValues(actualCol) = valor, False, True)
                                LastGroupValues(actualCol) = valor
                                newRow.AddCell(New Cell With {.Value = valor, .Format = formatos(actualCol)})
                            Else
                                If IsNumeric(valor) Then
                                    Dim cValor As Double = 0
                                    Try
                                        cValor = Double.Parse(valor)
                                        newRow.AddCell(New Cell With {.Value = cValor, .Format = formatos(actualCol)})
                                    Catch ex As Exception
                                        newRow.AddCell(New Cell With {.Value = valor, .Format = formatos(actualCol)})
                                    End Try
                                Else
                                    newRow.AddCell(New Cell With {.Value = valor, .Format = formatos(actualCol)})
                                End If
                            End If

                        Else
                            If IsNumeric(valor) Then
                                Dim cValor As Double = 0
                                Try
                                    cValor = Double.Parse(valor)
                                    newRow.AddCell(New Cell With {.Value = cValor})
                                Catch ex As Exception
                                    newRow.AddCell(New Cell With {.Value = valor})
                                End Try
                            Else
                                newRow.AddCell(New Cell With {.Value = valor})
                            End If
                        End If
                    Next

                    For Each col As Integer In keygrupos
                        If GroupChanged(col) Then
                            allRows.Add(BuildTotalRow(col, dt.Rows(rowNumber - 1)))
                        End If
                    Next

                    allRows.Add(newRow)

                Catch ex As Exception
                    Dim msg As String = ex.Message
                End Try
            Next

            Dim LastRow As DataRow = dt.Rows(dt.Rows.Count - 1)
            For Each col As Integer In keygrupos
                allRows.Add(BuildTotalRow(col, LastRow))
            Next
            Try
                allRows.Add(BuildGeneralTotalRow)
            Catch ex As Exception

            End Try
            Return allRows
        End Function

        Private Function BuildTotalRow(col As Integer, lastRow As DataRow) As Row
            Dim auxData = From auxRows In dt
            Dim totalCols As Integer = lastRow.ItemArray.Length - 1
            For i = 0 To col
                If colsToGroup.Contains(i) Then
                    Dim colToFilter = i
                    auxData = From auxRows In auxData Where auxRows(colToFilter).ToString = lastRow(colToFilter).ToString
                End If
            Next
            Dim newRow As Row = crearRow(totalCols)
            Dim color As String = PickColor(col)
            For cellNumber = 0 To newRow.Cells.Count - 1
                Dim celda As Cell = newRow.Cells(cellNumber)
                Dim auxCol As Integer = cellNumber 'Para los filtros
                celda.Bold = True
                celda.Background = color
                celda.Color = "white"
                If colsToSum.Contains(cellNumber) Then
                    Dim valor = Aggregate auxRow In auxData Into Sum(Double.Parse(auxRow(auxCol).ToString))
                    celda.Value = valor

                    celda.Format = formatos(cellNumber)
                ElseIf colsToCount.Contains(cellNumber) Then
                    Dim valor = Aggregate auxRow In auxData Into Count(Double.Parse(auxRow(auxCol).ToString) > 0)
                    celda.Value = colsToCount(auxCol)
                    celda.Format = formatos(auxCol)
                ElseIf col = cellNumber Then
                    celda.Value = "TOTAL " & lastRow(auxCol).ToString
                End If
            Next
            Return newRow
        End Function
        Private Function BuildGeneralTotalRow() As Row
            Dim auxData = From auxRows In dt
            Dim totalCols As Integer = dt(0).ItemArray.Length - 1

            Dim newRow As Row = crearRow(totalCols)
            Dim color As String = "#a4d7e1"
            For cellNumber = 0 To newRow.Cells.Count - 1
                Dim celda As Cell = newRow.Cells(cellNumber)
                Dim auxCol As Integer = cellNumber 'Para los filtros
                celda.Bold = True
                celda.Background = color
                celda.Color = "white"
                If colsToSum.Contains(cellNumber) Then
                    Dim valor = Aggregate auxRow In auxData Into Sum(Double.Parse(auxRow(auxCol).ToString))
                    celda.Value = valor

                    celda.Format = formatos(cellNumber)
                ElseIf colsToCount.Contains(cellNumber) Then
                    Dim valor = Aggregate auxRow In auxData Into Count(Double.Parse(auxRow(auxCol).ToString) > 0)
                    celda.Value = colsToCount(auxCol)
                    celda.Format = formatos(auxCol)
                ElseIf 0 = cellNumber Then
                    celda.Value = "TOTAL"
                End If
            Next
            Return newRow
        End Function

        Private Function PickColor(_numCol As Integer) As String
            Dim totalColors As Integer = colors.Length - 1
            Dim index As Integer = _numCol Mod totalColors
            Return colors(index)
        End Function
    End Class

    Public Class ColumnConfig
        Public Enum ColumnAction
            None
            Count
            Sum
            Group
        End Enum

        Private _numCol As Integer
        Private _action As ColumnAction
        Private _format As String

        Public Property NumCol As Integer
            Get
                Return _numCol
            End Get
            Set(value As Integer)
                _numCol = value
            End Set
        End Property

        Public Property Action As ColumnAction
            Get
                Return _action
            End Get
            Set(value As ColumnAction)
                _action = value
            End Set
        End Property

        Public Property Format As String
            Get
                Return _format
            End Get
            Set(value As String)
                _format = value
            End Set
        End Property

        Public Sub New(_numCol As Integer, _action As ColumnAction, _format As String)
            Me.NumCol = _numCol
            Me.Action = _action
            Me.Format = _format
        End Sub
    End Class
End Class
