
Imports System.Data
Imports Telerik.Web.Spreadsheet
Imports Telerik.Web.UI
Imports WorkSheetHelper

Partial Class M_Reportes_BaseMetaBF7
    Inherits System.Web.UI.Page
    Private Sub llenarSS()
        ssBF7.Sheets.Clear()
        Dim sheet As New Worksheet() With {.Name = "BF7"}
        sheet.AddRows(GenerateHeader)
        sheet.AddMergedCells(getRange(0, 0, 0, 6))
        sheet.AddMergedCells(getRange(0, 7, 0, 8))
        Try
            Dim res As DataTable = SP.RP_BASEMETA_BF7(IIf(CType(RPFechaReporte.SelectedDate, DateTime).ToShortDateString = Today.ToShortDateString, 0, 1), IIf(bucket0.Text = "", 0, bucket0.Text), IIf(bucket1.Text = "", 0, bucket1.Text), IIf(bucket2.Text = "", 0, bucket2.Text), IIf(bucket3.Text = "", 0, bucket3.Text), IIf(bucket4.Text = "", 0, bucket4.Text), CType(RPFechaReporte.SelectedDate, DateTime).ToShortDateString)

            If res.TableName <> "Exception" Then
                Dim configurator As New GroupedSheetGenerator(dt:=res, colsConfig:={
                New ColumnConfig(_numCol:=0, _action:=ColumnConfig.ColumnAction.Group, _format:=WorkSheetHelper.FormatClean),
                New ColumnConfig(_numCol:=1, _action:=ColumnConfig.ColumnAction.Group, _format:=WorkSheetHelper.FormatClean),
                New ColumnConfig(_numCol:=2, _action:=ColumnConfig.ColumnAction.None, _format:=WorkSheetHelper.FormatClean),
                New ColumnConfig(_numCol:=3, _action:=ColumnConfig.ColumnAction.None, _format:=WorkSheetHelper.FormatClean),
                New ColumnConfig(_numCol:=4, _action:=ColumnConfig.ColumnAction.None, _format:=WorkSheetHelper.FormatClean),
                New ColumnConfig(_numCol:=5, _action:=ColumnConfig.ColumnAction.Sum, _format:=WorkSheetHelper.FormatInteger),
                New ColumnConfig(_numCol:=6, _action:=ColumnConfig.ColumnAction.Sum, _format:=WorkSheetHelper.FormatMoney),
                New ColumnConfig(_numCol:=7, _action:=ColumnConfig.ColumnAction.None, _format:=WorkSheetHelper.FormatPercentage),
                New ColumnConfig(_numCol:=8, _action:=ColumnConfig.ColumnAction.Sum, _format:=WorkSheetHelper.FormatMoney)
                })

                Dim rowGenerated As List(Of Row) = configurator.GenerateRows()
                sheet.AddRows(rowGenerated)
            Else
                Throw New Exception("Error")
            End If
        Catch ex As Exception
            Dim gg As String = ex.Message
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "script", "console.error('" & gg & "');", True)
            Funciones.showModal(notificacion1, "deny", "Error", "Error de servidor. Intente de nuevo.")
        End Try
        ssBF7.Sheets.Add(sheet)
    End Sub

    Private Function GenerateHeader() As List(Of Row)
        Dim allRows As New List(Of Row)
        Dim rowHeader As Row = WorkSheetHelper.crearRow(9)
        Dim cell As Cell = rowHeader.Cells(0)
        cell.Value = "Inventario " & MonthYear()
        cell.Color = "white"
        cell.Background = "#002060"

        cell = rowHeader.Cells(7)
        cell.Value = "Meta " & MonthYear()
        cell.Color = "white"
        cell.Background = "#5B9BD5"

        allRows.Add(rowHeader)

        rowHeader = WorkSheetHelper.crearRow(9)

        cell = rowHeader.Cells(0)
        cell.Value = "TIPO"
        cell.Color = "white"
        cell.Background = "#002060"

        cell = rowHeader.Cells(1)
        cell.Value = "AGENCIA"
        cell.Color = "white"
        cell.Background = "#002060"

        cell = rowHeader.Cells(2)
        cell.Value = "PLAZA"
        cell.Color = "white"
        cell.Background = "#002060"

        cell = rowHeader.Cells(3)
        cell.Value = "PRODUCTO"
        cell.Color = "white"
        cell.Background = "#002060"

        cell = rowHeader.Cells(4)
        cell.Value = "ASIGNACION"
        cell.Color = "white"
        cell.Background = "#002060"

        cell = rowHeader.Cells(5)
        cell.Value = "NO. CUENTAS"
        cell.Color = "white"
        cell.Background = "#002060"

        cell = rowHeader.Cells(6)
        cell.Value = "SALDO TOTAL"
        cell.Color = "white"
        cell.Background = "#002060"

        cell = rowHeader.Cells(7)
        cell.Value = "% META"
        cell.Color = "white"
        cell.Background = "#5B9BD5"

        cell = rowHeader.Cells(8)
        cell.Value = "META EFECTIVO"
        cell.Color = "white"
        cell.Background = "#5B9BD5"
        allRows.Add(rowHeader)

        Return allRows
    End Function

    Private Sub BaseMetaBF2_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' llenarSS()
            RPFechaReporte.MaxDate = Today
        End If
    End Sub

    Private Sub btnAplicar_Click(sender As Object, e As EventArgs) Handles btnAplicar.Click
        llenarSS()
    End Sub

    Private Function MonthYear() As String
        Dim fecha() As String = Date.Now.ToLongDateString.Split(",")(1).Replace("de", "").Replace("  ", " ").ToUpper.Split(" ")
        Return fecha(2) & " " & fecha(3)
    End Function

    Private Sub btnGenerarReporte_Click(sender As Object, e As EventArgs) Handles btnGenerarReporte.Click
        ssBF7.Visible = True
        llenarSS()
    End Sub
End Class
