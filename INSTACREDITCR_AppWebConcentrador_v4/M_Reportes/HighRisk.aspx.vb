Imports Funciones

Imports System.Data
Imports System.Web.Services
Imports Telerik.Web.UI
Imports Telerik.Web.Spreadsheet
Imports WorkSheetHelper
Partial Class HighRisk
    Inherits System.Web.UI.Page

    Private Sub btnGenerarReporte_Click(sender As Object, e As EventArgs) Handles btnGenerarReporte.Click
        ssHighRisk.Visible = True

        Dim inicio As DateTime = DateTime.Now

        ssHighRisk.Sheets.Add(New _HighRisk(tipo:=_HighRisk.HR_TDC, fecha:=CType(RPFechaReporte.SelectedDate, DateTime).ToShortDateString).createSheet)
        ssHighRisk.Sheets.Add(New _HighRisk(tipo:=_HighRisk.HR_MIPAGUITOS, fecha:=CType(RPFechaReporte.SelectedDate, DateTime).ToShortDateString).createSheet)
        ssHighRisk.Sheets.Add(New _HighRisk(tipo:=_HighRisk.FOTO, fecha:=CType(RPFechaReporte.SelectedDate, DateTime).ToShortDateString).createSheet)
        ssHighRisk.Sheets.Add(New _HighRisk(tipo:=_HighRisk.CTAS_PAGO, fecha:=CType(RPFechaReporte.SelectedDate, DateTime).ToShortDateString).createSheet)
        ssHighRisk.Sheets.Add(New _HighRisk(tipo:=_HighRisk.PAGOS, fecha:=CType(RPFechaReporte.SelectedDate, DateTime).ToShortDateString).createSheet)

        Dim fin As DateTime = DateTime.Now

        Dim totalMin As String
        Dim total As TimeSpan = New TimeSpan(fin.Ticks - inicio.Ticks)
        totalMin = total.Hours.ToString("00") & ":" & total.Minutes.ToString("00") & ":" & total.Seconds.ToString("00") & "." & total.Milliseconds

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "script", "console.log('Tiempo total: " & totalMin & "');", True)
    End Sub

    Private Sub HighRisk_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub
End Class

Class _HighRisk
    Private tipo As String = ""
    Private fecha As String = ""
    Private sheet As Worksheet
    Public Const HR_TDC As String = "0"
    Public Const HR_MIPAGUITOS As String = "1"
    Public Const FOTO As String = "2"
    Public Const CTAS_PAGO As String = "3"
    Public Const PAGOS As String = "4"

    Public Sub New(tipo As String, fecha As String)
        Me.tipo = tipo
        Me.fecha = fecha
        Dim nombreHoja As String = ""

        Select Case tipo
            Case HR_TDC
                nombreHoja = "HR TDC"
            Case HR_MIPAGUITOS
                nombreHoja = "HR MI PAGUITOS"
            Case FOTO
                nombreHoja = "FOTO " '& returnToDay(6).ToShortDateString
            Case CTAS_PAGO
                nombreHoja = "CTAS PAGO MIGRARON BUCKET C"
            Case PAGOS
                nombreHoja = "PAGOS"
        End Select

        sheet = New Worksheet() With {.Name = nombreHoja}
        sheet.ShowGridLines = False
    End Sub

    Public Function createSheet() As Worksheet
        Select Case tipo
            Case HR_TDC
                _HR()
            Case HR_MIPAGUITOS
                _HR()
            Case FOTO
                _Foto()
            Case CTAS_PAGO
                _CTAS()
            Case PAGOS
                _PAGOS()
        End Select
        Return sheet
    End Function
    Private Function returnToDay(dayMonth As Integer) As Date
        Dim fecha = Now
        While fecha.Day <> dayMonth
            fecha = fecha.AddDays(-1)
        End While
        Return fecha
    End Function
    Private Sub _HR()
        sheet.Columns = New List(Of Column)
        sheet.Columns.Add(New Column With {.Width = 200})
        Dim createHeader = Sub()
                               Dim border As New BorderStyle With {.Color = "black", .Size = 2}
                               Dim auxRow As New Row
                               auxRow.AddCell(New Cell() With {.Value = "Roll Back- High Risk", .Bold = True, .TextAlign = "center"})
                               sheet.AddRow(auxRow)
                               sheet.AddMergedCells(getRange(0, 0, 0, 8))

                               auxRow = New Row
                               auxRow.AddCell(New Cell() With {.Value = "Start Date", .Bold = True, .TextAlign = "center", .BorderTop = border})
                               auxRow.AddCell(New Cell() With {.BorderTop = border})
                               auxRow.AddCell(New Cell() With {.BorderTop = border})
                               auxRow.AddCell(New Cell() With {.Value = "Date of the Report", .Bold = True, .TextAlign = "center", .BorderTop = border})
                               auxRow.AddCell(New Cell() With {.BorderTop = border})
                               auxRow.AddCell(New Cell() With {.BorderTop = border})
                               auxRow.AddCell(New Cell() With {.Value = "Date of payments referred", .Bold = True, .TextAlign = "center", .BorderTop = border})
                               auxRow.AddCell(New Cell() With {.BorderTop = border})
                               auxRow.AddCell(New Cell() With {.BorderTop = border})
                               sheet.AddRow(auxRow)
                               sheet.AddMergedCells(getRange(1, 0, 1, 2))
                               sheet.AddMergedCells(getRange(1, 3, 1, 5))
                               sheet.AddMergedCells(getRange(1, 6, 1, 8))

                               Dim fechaInicio = returnToDay(6)
                               auxRow = New Row
                               auxRow.AddCell(New Cell() With {.Value = fechaInicio.ToShortDateString, .TextAlign = "center", .BorderBottom = border})
                               auxRow.AddCell(New Cell() With {.BorderBottom = border})
                               auxRow.AddCell(New Cell() With {.BorderBottom = border})
                               auxRow.AddCell(New Cell() With {.Value = Now.ToShortDateString, .TextAlign = "center", .BorderBottom = border})
                               auxRow.AddCell(New Cell() With {.BorderBottom = border})
                               auxRow.AddCell(New Cell() With {.BorderBottom = border})
                               auxRow.AddCell(New Cell() With {.Value = fechaInicio.ToLongDateString.Split(",")(1) & " to" & fechaInicio.AddDays(6).ToLongDateString.Split(",")(1), .TextAlign = "center", .BorderBottom = border})
                               auxRow.AddCell(New Cell() With {.BorderBottom = border})
                               auxRow.AddCell(New Cell() With {.BorderBottom = border})
                               sheet.AddRow(auxRow)
                               sheet.AddMergedCells(getRange(2, 0, 2, 2))
                               sheet.AddMergedCells(getRange(2, 3, 2, 5))
                               sheet.AddMergedCells(getRange(2, 6, 2, 8))

                               auxRow = New Row
                               sheet.AddRow(auxRow)

                               auxRow = New Row
                               auxRow.AddCell(New Cell())
                               auxRow.AddCell(New Cell())
                               auxRow.AddCell(New Cell())
                               auxRow.AddCell(New Cell())
                               auxRow.AddCell(New Cell())
                               auxRow.AddCell(New Cell() With {.Value = "#", .Bold = True, .TextAlign = "center"})
                               auxRow.AddCell(New Cell() With {.Value = "$", .Bold = True, .TextAlign = "center"})
                               auxRow.AddCell(New Cell() With {.Value = "#", .Bold = True, .TextAlign = "center"})
                               auxRow.AddCell(New Cell() With {.Value = "$", .Bold = True, .TextAlign = "center"})
                               sheet.AddRow(auxRow)

                               Dim slimBorder As BorderStyle = New BorderStyle With {.Color = "black", .Size = 1}
                               auxRow = New Row
                               auxRow.AddCell(New Cell())
                               auxRow.AddCell(New Cell())
                               auxRow.AddCell(New Cell())
                               auxRow.AddCell(New Cell())
                               auxRow.AddCell(New Cell())
                               auxRow.AddCell(New Cell() With {.Value = "BUCKET C", .Bold = True, .TextAlign = "center", .BorderTop = slimBorder, .BorderBottom = slimBorder})
                               auxRow.AddCell(New Cell() With {.Value = "BUCKET C", .Bold = True, .TextAlign = "center", .BorderTop = slimBorder, .BorderBottom = slimBorder})
                               auxRow.AddCell(New Cell() With {.Value = "BUCKET 0", .Bold = True, .TextAlign = "center", .BorderTop = slimBorder, .BorderBottom = slimBorder, .Background = "#0070C0"})
                               auxRow.AddCell(New Cell() With {.Value = "BUCKET 0", .Bold = True, .TextAlign = "center", .BorderTop = slimBorder, .BorderBottom = slimBorder, .Background = "#00B050"})
                               sheet.AddRow(auxRow)
                           End Sub

        Dim createTotalHeader = Sub()
                                    Dim v_subproducto As Integer = IIf(tipo = HR_TDC, 0, 1)
                                    Dim dataHeader As DataTable = SP.RP_HIGH_RISK(1, v_subproducto, v_fecha:=fecha)

                                    For Each rowHeader As DataRow In dataHeader.Rows
                                        Dim newRow As New Row
                                        If rowHeader(1).ToString.Contains("%") Then
                                            newRow.AddCell(New Cell With {.Value = rowHeader(1).ToString})
                                            newRow.AddCell(New Cell)
                                            newRow.AddCell(New Cell)
                                            newRow.AddCell(New Cell)
                                            newRow.AddCell(New Cell)
                                            newRow.AddCell(New Cell)
                                            newRow.AddCell(New Cell)
                                            newRow.AddCell(New Cell With {.Value = Double.Parse(rowHeader(2).ToString), .Format = WorkSheetHelper.FormatPercentage})
                                            newRow.AddCell(New Cell With {.Value = Double.Parse(rowHeader(3).ToString), .Format = WorkSheetHelper.FormatPercentage})
                                        ElseIf rowHeader(1).ToString.Contains("TOTAL") Then
                                            newRow.AddCell(New Cell With {.Value = rowHeader(1).ToString, .Bold = True, .BorderBottom = WorkSheetHelper.BorderNormal})
                                            newRow.AddCell(New Cell With {.Value = "#", .Bold = True, .BorderBottom = WorkSheetHelper.BorderNormal})
                                            newRow.AddCell(New Cell With {.Value = "$", .Bold = True, .BorderBottom = WorkSheetHelper.BorderNormal})
                                            newRow.AddCell(New Cell With {.BorderBottom = WorkSheetHelper.BorderNormal})
                                            newRow.AddCell(New Cell With {.BorderBottom = WorkSheetHelper.BorderNormal})
                                            newRow.AddCell(New Cell With {.BorderBottom = WorkSheetHelper.BorderNormal})
                                            newRow.AddCell(New Cell With {.BorderBottom = WorkSheetHelper.BorderNormal})
                                            newRow.AddCell(New Cell With {.Value = Double.Parse(rowHeader(2).ToString), .Format = WorkSheetHelper.FormatInteger, .Bold = True, .BorderBottom = WorkSheetHelper.BorderNormal})
                                            newRow.AddCell(New Cell With {.Value = Double.Parse(rowHeader(3).ToString), .Format = WorkSheetHelper.FormatMoney, .Bold = True, .BorderBottom = WorkSheetHelper.BorderNormal})
                                        Else
                                            newRow.AddCell(New Cell With {.Value = rowHeader(1).ToString})
                                            newRow.AddCell(New Cell)
                                            newRow.AddCell(New Cell)
                                            newRow.AddCell(New Cell)
                                            newRow.AddCell(New Cell)
                                            newRow.AddCell(New Cell)
                                            newRow.AddCell(New Cell)
                                            newRow.AddCell(New Cell With {.Value = Double.Parse(rowHeader(2).ToString), .Format = WorkSheetHelper.FormatInteger})
                                            newRow.AddCell(New Cell With {.Value = Double.Parse(rowHeader(3).ToString), .Format = WorkSheetHelper.FormatMoney})
                                        End If
                                        sheet.AddRow(newRow)
                                    Next
                                    Dim _Row As New Row
                                    _Row.AddCell(New Cell With {.Value = " "})
                                    _Row.AddCell(New Cell With {.Value = " "})
                                    _Row.AddCell(New Cell With {.Value = " "})
                                    _Row.AddCell(New Cell With {.Value = "% Of # accounts"})
                                    sheet.AddRow(_Row)
                                End Sub
        Dim createBody = Sub()
                             Dim v_subproducto As Integer = IIf(tipo = HR_TDC, 0, 1)
                             Dim dataHeader As DataTable = SP.RP_HIGH_RISK(2, v_subproducto, v_fecha:=fecha)
                             Dim filasBold() As Integer = {2, 4, 6, 8}

                             For Each rowHeader As DataRow In dataHeader.Rows
                                 Dim hidden As Boolean = Not filasBold.Contains(rowHeader("orden").ToString)
                                 Dim bold As Boolean = filasBold.Contains(rowHeader("orden").ToString)

                                 Dim newRow As New Row With {.Hidden = hidden}

                                 newRow.AddCell(New Cell With {.Value = rowHeader(1).ToString, .Bold = bold})
                                 For index = 2 To rowHeader.ItemArray.Length - 1 Step 2
                                     Dim formato As String = ""
                                     Dim valor As Double = 0
                                     Try
                                         valor = Double.Parse(rowHeader(index).ToString)
                                     Catch ex As Exception
                                         valor = 0
                                     End Try
                                     Select Case rowHeader(index + 1).ToString
                                         Case "P"
                                             formato = WorkSheetHelper.FormatPercentage
                                         Case "N"
                                             formato = WorkSheetHelper.FormatInteger
                                         Case "S"
                                             formato = WorkSheetHelper.FormatMoney
                                     End Select
                                     newRow.AddCell(New Cell With {.Value = valor, .Bold = bold, .Format = formato})
                                 Next
                                 sheet.AddRow(newRow)
                             Next
                         End Sub


        createHeader()
        createTotalHeader()
        createBody()
    End Sub

    Private Sub _Foto()
        Dim titulos() As String = {"Grupo de la Cuenta", "Número de Cuenta", "Monto vencido",
            "Saldo deudor", "Monto del próximo payoff.", "Número de días de mora Local",
            "Bucket", "Subproducto", "Gestor",
            "Agencia actual", "Fecha de referencia a la agenc"}
        sheet.Columns = New List(Of Column)
        Dim auxRow As New Row
        For Each titulo As String In titulos
            auxRow.AddCell(New Cell() With {.Value = titulo, .Background = "#0070C0", .Color = "white", .Bold = True, .TextAlign = "center"})
            sheet.Columns.Add(New Column With {.Width = 100})
        Next
        sheet.AddRow(auxRow)

        Dim border As New BorderStyle With {.Color = "Black", .Size = 1}

        Dim data As DataTable = SP.RP_HIGH_RISK(v_bandera:=3)
        For Each row As DataRow In data.Rows
            auxRow = New Row
            For Each col In row.ItemArray
                auxRow.AddCell(New Cell With {.Value = col.ToString, .BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border})
            Next
            sheet.AddRow(auxRow)
        Next
    End Sub


    Private Sub _CTAS()
        Dim titulos() As String = {"Grupo de la Cuenta", "Número de Cuenta", "Monto vencido",
            "Saldo deudor", "Monto del próximo payoff.", "No De Cuentas",
            "Monto", "Diferencia", "Número de días de mora Local",
            "Bucket", "Subproducto", "Gestor",
            "Gestor Actual", "Agencia actual", "Fecha de referencia a la agenc"}
        sheet.Columns = New List(Of Column)
        Dim auxRow As New Row
        For Each titulo As String In titulos
            auxRow.AddCell(New Cell() With {.Value = titulo, .Background = "#0070C0", .Color = "white", .Bold = True, .TextAlign = "center"})
            sheet.Columns.Add(New Column With {.Width = 100})
        Next
        sheet.AddRow(auxRow)
        Dim border As New BorderStyle With {.Color = "Black", .Size = 1}

        Dim data As DataTable = SP.RP_HIGH_RISK(v_bandera:=4)
        For Each row As DataRow In data.Rows
            auxRow = New Row
            For Each col In row.ItemArray
                auxRow.AddCell(New Cell With {.Value = col.ToString, .BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border})
            Next
            sheet.AddRow(auxRow)
        Next
    End Sub

    Private Sub _PAGOS()
        Dim titulos() As String = {"Cuenta", "Monto de Transacción/Pago", "Fecha de Transacción/Pago",
            "Fecha de Registro de la Transacción/Pago", "Descripción de trandefs usada", "Subproducto"}
        sheet.Columns = New List(Of Column)
        Dim auxRow As New Row
        For Each titulo As String In titulos
            auxRow.AddCell(New Cell() With {.Value = titulo, .Background = "#0070C0", .Color = "white", .Bold = True, .TextAlign = "center"})
            sheet.Columns.Add(New Column With {.Width = 120})
        Next
        sheet.AddRow(auxRow)
        Dim border As New BorderStyle With {.Color = "Black", .Size = 1}

        Dim data As DataTable = SP.RP_HIGH_RISK(v_bandera:=5)
        For Each row As DataRow In data.Rows
            auxRow = New Row
            For Each col In row.ItemArray
                auxRow.AddCell(New Cell With {.Value = col.ToString, .BorderBottom = border, .BorderLeft = border, .BorderRight = border, .BorderTop = border})
            Next
            sheet.AddRow(auxRow)
        Next
    End Sub
End Class
