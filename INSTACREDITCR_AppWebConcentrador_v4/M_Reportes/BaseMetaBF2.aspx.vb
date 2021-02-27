
Imports System.Data
Imports Telerik.Web.Spreadsheet
Imports Telerik.Web.UI
Imports WorkSheetHelper
Imports ClosedXML.Excel
Imports System.IO

Partial Class BaseMetaBF2
    Inherits System.Web.UI.Page

    Private Sub BaseMetaBF2_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' llenarSS()
            RPFechaReporte.MaxDate = Today
        End If
    End Sub

    Private Sub btnAplicarPorcentaje_Click(sender As Object, e As EventArgs) Handles btnAplicarPorcentaje.Click
        ssBF2.Visible = True
        llenarSS()
    End Sub

    Private Sub btnGenerarReporte_Click(sender As Object, e As EventArgs) Handles btnGenerarReporte.Click
        ssBF2.Visible = True
        llenarSS()

    End Sub

    Private Sub llenarSS()
        ssBF2.Sheets.Clear()
        Dim sheet As New Worksheet() With {.Name = "BF2"}
        sheet.AddRows(GenerateHeader)
        sheet.AddMergedCells(getRange(0, 0, 0, 6))
        sheet.AddMergedCells(getRange(0, 7, 0, 8))
        Try
            'If CType(RPFechaReporte.SelectedDate, DateTime).ToShortDateString = Today.ToShortDateString Then
            '    Dim s As String = "sip"
            'End If

            Dim res As DataTable = SP.RP_BASEMETA_BF2(v_bandera:=IIf(CType(RPFechaReporte.SelectedDate, DateTime).ToShortDateString = Today.ToShortDateString, 0, 1), v_porc_bucket0:=bucket0.Text, v_porc_bucket1:=bucket1.Text, v_porc_bucket2:=bucket2.Text, v_porc_bucket3:=bucket3.Text, v_porc_bucket4:=bucket4.Text, v_porc_bucket5:=bucket5.Text, v_porc_bucket6:=bucket6.Text, v_porc_bucket7:=bucket7.Text, v_porc_bucket8:=bucket8.Text, v_porc_bucket9:=bucket9.Text, v_fecha:=CType(RPFechaReporte.SelectedDate, DateTime).ToShortDateString)

            'res = fakeDataTable()
            If res.TableName <> "Exception" Then
                Dim configurator As New GroupedSheetGenerator(dt:=res, colsConfig:={
                New ColumnConfig(_numCol:=0, _action:=ColumnConfig.ColumnAction.Group, _format:=WorkSheetHelper.FormatClean),
                New ColumnConfig(_numCol:=1, _action:=ColumnConfig.ColumnAction.Group, _format:=WorkSheetHelper.FormatClean),
                New ColumnConfig(_numCol:=2, _action:=ColumnConfig.ColumnAction.Group, _format:=WorkSheetHelper.FormatClean),
                New ColumnConfig(_numCol:=3, _action:=ColumnConfig.ColumnAction.None, _format:=WorkSheetHelper.FormatClean),
                New ColumnConfig(_numCol:=4, _action:=ColumnConfig.ColumnAction.None, _format:=WorkSheetHelper.FormatClean),
                New ColumnConfig(_numCol:=5, _action:=ColumnConfig.ColumnAction.Sum, _format:=WorkSheetHelper.FormatInteger),
                New ColumnConfig(_numCol:=6, _action:=ColumnConfig.ColumnAction.Sum, _format:=WorkSheetHelper.FormatMoney),
                New ColumnConfig(_numCol:=7, _action:=ColumnConfig.ColumnAction.None, _format:=WorkSheetHelper.FormatPercentage),
                New ColumnConfig(_numCol:=8, _action:=ColumnConfig.ColumnAction.Sum, _format:=WorkSheetHelper.FormatMoney)
                })

                Dim rowGenerated As List(Of Row) = configurator.GenerateRows()
                sheet.AddRows(rowGenerated)
                pnlDetalle.Visible = True
            Else
                Throw New Exception(res(0)(0).ToString)
            End If
        Catch ex As Exception
            Dim gg As String = ex.Message
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "script", "console.error('" & gg & "');", True)
            Funciones.showModal(notificacion1, "deny", "Error", "Error de servidor. Intente de nuevo.")
        End Try
        ssBF2.Sheets.Add(sheet)
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
        cell.Value = "PORTAFOLIO"
        cell.Color = "white"
        cell.Background = "#002060"

        cell = rowHeader.Cells(4)
        cell.Value = "BUCKET / SEGMENTO"
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


    Private Function MonthYear() As String
        Dim fecha() As String = Date.Now.ToLongDateString.Split(",")(1).Replace("de", "").Replace("  ", " ").ToUpper.Split(" ")
        Return fecha(2) & " " & fecha(3)
    End Function
    Protected Sub ExportToXcel_SomeReport(dt As DataTable, fileName As String, page As Page)
        Dim recCount = dt.Rows.Count
        fileName = String.Format(fileName, DateTime.Now.ToString("MMddyyyy_hhmmss"))
        Dim xlsx = New XLWorkbook()
        Dim ws = xlsx.Worksheets.Add(fileName)
        ws.Style.Font.Bold = True

        ws.Style.Font.Bold = False

        ws.Cell(1, 1).InsertTable(dt.AsEnumerable)

        For co As Integer = 1 To dt.Columns.Count + 1
            ws.Column(co).AdjustToContents()
        Next

        ws.Tables.Table(0).ShowAutoFilter = False
        ws.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.General


        DynaGenExcelFile(fileName, page, xlsx)

    End Sub
    Protected Sub DynaGenExcelFile(fileName As String, page As Page, xlsx As XLWorkbook)
        Try
            page.Response.ClearContent()
            page.Response.ClearHeaders()
            page.Response.ContentType = "application/vnd.ms-excel"
            page.Response.AppendHeader("Content-Disposition", String.Format("attachment;filename={0}.xlsx", fileName))

            Using memoryStream As New MemoryStream()
                xlsx.SaveAs(memoryStream)
                memoryStream.WriteTo(page.Response.OutputStream)
            End Using
            page.Response.Flush()
            page.Response.[End]()
        Catch ex As Exception
            Dim msj As String = ex.Message
        End Try


    End Sub

    Private Sub btnGenerarDetalle_Click(sender As Object, e As EventArgs) Handles btnGenerarDetalle.Click
        Try
            Dim res As DataTable = SP.RP_BASEMETA_BF2(v_bandera:=IIf(CType(RPFechaReporte.SelectedDate, DateTime).ToShortDateString = Today.ToShortDateString, 2, 3), v_porc_bucket0:=bucket0.Text, v_porc_bucket1:=bucket1.Text, v_porc_bucket2:=bucket2.Text, v_porc_bucket3:=bucket3.Text, v_porc_bucket4:=bucket4.Text, v_porc_bucket5:=bucket5.Text, v_porc_bucket6:=bucket6.Text, v_porc_bucket7:=bucket7.Text, v_porc_bucket8:=bucket8.Text, v_porc_bucket9:=bucket9.Text, v_fecha:=CType(RPFechaReporte.SelectedDate, DateTime).ToShortDateString)

            If res.TableName <> "Exception" Then
                ExportToXcel_SomeReport(res, "Detalle BF2 " & CType(RPFechaReporte.SelectedDate, DateTime).ToShortDateString.Replace("/", "").Replace(" ", ""), Me)
            Else
                Throw New Exception(res(0)(0).ToString)
            End If
        Catch ex As Exception
            Dim gg As String = ex.Message
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "script", "console.error('" & gg & "');", True)
            Funciones.showModal(notificacion1, "deny", "Error", "Error de servidor. Intente de nuevo.")
        End Try
    End Sub

End Class
