
Imports System.Data
Imports Telerik.Web.UI
Imports Telerik.Web.Spreadsheet
Imports WorkSheetHelper
Imports ClosedXML.Excel
Imports System.IO

Partial Class M_Reportes_BaseMetaINST
    Inherits System.Web.UI.Page

    Private Function MonthYear() As String
        Dim fecha() As String = Date.Now.ToLongDateString.Split(",")(1).Replace("de", "").Replace("  ", " ").ToUpper.Split(" ")
        Return fecha(2) & " " & fecha(3)

    End Function

    Public Sub printReporte()
        ssINST.Sheets().Add(New Worksheet With {.Name = "Base Meta Inst"})
        printHeader()
        Try

            Dim res As DataTable = SP.RP_BASEMETA_INST(v_bandera:=IIf(CType(RPFechaReporte.SelectedDate, DateTime).ToShortDateString = Today.ToShortDateString, 0, 1), fecha:=CType(RPFechaReporte.SelectedDate, DateTime).ToShortDateString)

            If res.TableName = "Exception" Then
                Throw New Exception(res(0)(0).ToString)
            ElseIf res.Rows.Count = 0 Then
                Throw New Exception("No hay información para generar el reporte.")
            Else
                Dim configurator As New GroupedSheetGenerator(dt:=res, colsConfig:={
                New ColumnConfig(_numCol:=0, _action:=ColumnConfig.ColumnAction.Group, _format:=WorkSheetHelper.FormatClean),
                New ColumnConfig(_numCol:=1, _action:=ColumnConfig.ColumnAction.Group, _format:=WorkSheetHelper.FormatClean),
                New ColumnConfig(_numCol:=2, _action:=ColumnConfig.ColumnAction.Group, _format:=WorkSheetHelper.FormatClean),
                New ColumnConfig(_numCol:=3, _action:=ColumnConfig.ColumnAction.None, _format:=WorkSheetHelper.FormatClean),
                New ColumnConfig(_numCol:=4, _action:=ColumnConfig.ColumnAction.Sum, _format:=WorkSheetHelper.FormatInteger),
                New ColumnConfig(_numCol:=5, _action:=ColumnConfig.ColumnAction.Sum, _format:=WorkSheetHelper.FormatMoney),
                New ColumnConfig(_numCol:=6, _action:=ColumnConfig.ColumnAction.Sum, _format:=WorkSheetHelper.FormatMoney),
                New ColumnConfig(_numCol:=7, _action:=ColumnConfig.ColumnAction.Sum, _format:=WorkSheetHelper.FormatMoney),
                New ColumnConfig(_numCol:=8, _action:=ColumnConfig.ColumnAction.None, _format:=WorkSheetHelper.FormatPercentage),
                New ColumnConfig(_numCol:=9, _action:=ColumnConfig.ColumnAction.Sum, _format:=WorkSheetHelper.FormatMoney),
                New ColumnConfig(_numCol:=10, _action:=ColumnConfig.ColumnAction.None, _format:=WorkSheetHelper.FormatPercentage),
                New ColumnConfig(_numCol:=11, _action:=ColumnConfig.ColumnAction.Sum, _format:=WorkSheetHelper.FormatMoney),
                New ColumnConfig(_numCol:=12, _action:=ColumnConfig.ColumnAction.None, _format:=WorkSheetHelper.FormatPercentage),
                New ColumnConfig(_numCol:=13, _action:=ColumnConfig.ColumnAction.Sum, _format:=WorkSheetHelper.FormatMoney),
                New ColumnConfig(_numCol:=14, _action:=ColumnConfig.ColumnAction.Sum, _format:=WorkSheetHelper.FormatMoney)
                })

                Dim rowGenerated As List(Of Row) = configurator.GenerateRows()
                ssINST.Sheets(0).AddRows(rowGenerated)
                pnlDetalles.Visible = True
            End If
        Catch ex As Exception
            Dim gg As String = ex.Message
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "script", "console.error('" & gg & "');", True)
            Funciones.showModal(notificacion1, "deny", "Error", "Error de servidor. Intente de nuevo.")
        End Try
    End Sub

    Private Sub printHeader()
        Dim rrow As New Row
        rrow.AddCell(New Cell() With {
                       .Value = "INVENTARIO " & MonthYear(),
                       .Background = "#004085",
                       .Color = "#fefefe",
                       .TextAlign = "center"})
        rrow.AddCell(New Cell())
        rrow.AddCell(New Cell())
        rrow.AddCell(New Cell())
        rrow.AddCell(New Cell())
        rrow.AddCell(New Cell())
        rrow.AddCell(New Cell())
        rrow.AddCell(New Cell())
        rrow.AddCell(New Cell() With {.Value = "META " & MonthYear(), .Background = "#0c5460", .Color = "#fefefe", .TextAlign = "center"})
        rrow.AddCell(New Cell())

        rrow.AddCell(New Cell() With {.Value = "META CONTENCIÓN", .Background = "#0c5460", .Color = "#fefefe", .TextAlign = "center"})
        rrow.AddCell(New Cell())

        rrow.AddCell(New Cell() With {.Value = "META CAPITAL", .Background = "#0c5460", .Color = "#fefefe", .TextAlign = "center"})
        rrow.AddCell(New Cell())


        ssINST.Sheets(0).AddRow(rrow)

        rrow = New Row
        rrow.AddCell(New Cell() With {.Value = "TIPO", .Background = "#004085", .Color = "#fefefe", .TextAlign = "center"})
        rrow.AddCell(New Cell() With {.Value = "AGENCIA", .Background = "#004085", .Color = "#fefefe", .TextAlign = "center"})
        rrow.AddCell(New Cell() With {.Value = "PLAZA", .Background = "#004085", .Color = "#fefefe", .TextAlign = "center"})
        rrow.AddCell(New Cell() With {.Value = "PRIORIDAD", .Background = "#004085", .Color = "#fefefe", .TextAlign = "center"})
        rrow.AddCell(New Cell() With {.Value = "NUMERO DE CUENTAS", .Background = "#004085", .Color = "#fefefe", .TextAlign = "center"})
        rrow.AddCell(New Cell() With {.Value = "SALDO VENCIDO", .Background = "#004085", .Color = "#fefefe", .TextAlign = "center"})
        rrow.AddCell(New Cell() With {.Value = "SALDO INSOLUTO", .Background = "#004085", .Color = "#fefefe", .TextAlign = "center"})
        rrow.AddCell(New Cell() With {.Value = "SALDO CAPITAL", .Background = "#004085", .Color = "#fefefe", .TextAlign = "center"})

        rrow.AddCell(New Cell() With {.Value = "% META EFECTIVO", .Background = "#0c5460", .Color = "#fefefe", .TextAlign = "center"})
        rrow.AddCell(New Cell() With {.Value = "META EFECTIVO", .Background = "#0c5460", .Color = "#fefefe", .TextAlign = "center"})
        rrow.AddCell(New Cell() With {.Value = "% CONTENCION INSOLUTO", .Background = "#0c5460", .Color = "#fefefe", .TextAlign = "center"})
        rrow.AddCell(New Cell() With {.Value = "META CONTENCION INSOLUTO", .Background = "#0c5460", .Color = "#fefefe", .TextAlign = "center"})
        rrow.AddCell(New Cell() With {.Value = "% EFECTIVO CAPITAL", .Background = "#0c5460", .Color = "#fefefe", .TextAlign = "center"})
        rrow.AddCell(New Cell() With {.Value = "META EFECTIVO CAPITAL", .Background = "#0c5460", .Color = "#fefefe", .TextAlign = "center"})
        rrow.AddCell(New Cell() With {.Value = "META EFECTIVO", .Background = "#0c5460", .Color = "#fefefe", .TextAlign = "center"})

        ssINST.Sheets(0).AddRow(rrow)

        ssINST.Sheets(0).AddMergedCells("A1:H1")
        ssINST.Sheets(0).AddMergedCells("I1:J1")
        ssINST.Sheets(0).AddMergedCells("K1:L1")
        ssINST.Sheets(0).AddMergedCells("M1:N1")

        ssINST.Sheets(0).FrozenRows = 2
    End Sub

    Private Sub M_Reportes_BaseMetaINST_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' printReporte()
            RPFechaReporte.MaxDate = Today

        End If
    End Sub

    Private Sub btnGenerarReporte_Click(sender As Object, e As EventArgs) Handles btnGenerarReporte.Click
        ssINST.Visible = True
        printReporte()
    End Sub

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
            Dim res As DataTable = SP.RP_BASEMETA_INST(v_bandera:=IIf(CType(RPFechaReporte.SelectedDate, DateTime).ToShortDateString = Today.ToShortDateString, 2, 3), fecha:=CType(RPFechaReporte.SelectedDate, DateTime).ToShortDateString)

            If res.TableName <> "Exception" Then
                ExportToXcel_SomeReport(res, "Detalle INST " & CType(RPFechaReporte.SelectedDate, DateTime).ToShortDateString.Replace("/", "").Replace(" ", ""), Me)
            End If
        Catch ex As Exception
            Funciones.showModal(notificacion1, "deny", "Error", "Error de servidor. Intente de nuevo.")

        End Try
    End Sub
End Class
