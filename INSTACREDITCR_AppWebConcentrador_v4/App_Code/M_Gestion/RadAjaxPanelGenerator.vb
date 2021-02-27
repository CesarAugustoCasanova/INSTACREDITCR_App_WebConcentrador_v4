Imports Microsoft.VisualBasic
Imports Telerik.Web.UI
Imports System.Data

Public Class RadAjaxPanelGenerator
    Private _col_size As Integer
    Private _col_large_size As Integer

    ''' <summary>
    ''' Esta clase genera bloques de informacion de manera facil.
    ''' </summary>
    ''' <param name="col_size">Tamaño de cada columna en pantallas >= medianas (multiplo de 12)</param>
    ''' <param name="col_large_size">Tamaño de cada columna en pantallas grandes (multiplo de 12)</param>
    Public Sub New(col_size As Integer, Optional col_large_size As Integer = 0)
        _col_size = col_size
        _col_large_size = col_large_size
    End Sub

    Private Function crearRow() As RadAjaxPanel
        Dim pnl As New RadAjaxPanel
        pnl.CssClass = "w3-row-padding"
        Return pnl
    End Function

    Private Function crearCol(text As String, value As String) As RadAjaxPanel
        Dim pnl As New RadAjaxPanel
        pnl.CssClass = "w3-col s12 m" & _col_size & IIf(_col_large_size = 0, " ", " l" & _col_large_size) & " w3-leftbar w3-border-white w3-hover-border-blue"
        Dim lblText As New RadLabel
        lblText.Text = text
        Dim lblVal As New RadLabel
        lblVal.Text = IIf(String.IsNullOrEmpty(value) Or String.IsNullOrWhiteSpace(value), "-", value)
        'lblText.Width = "50"
        ' lblVal.Width = "50"
        lblText.Font.Bold = True

        If text Like "Vacio_*" Then
            lblText.Text = "Vacio"
            lblText.CssClass = "w3-text-white"
            lblVal.CssClass = "w3-round w3-block w3-text-white"
        Else
            lblVal.CssClass = "w3-border w3-round w3-hover-blue  w3-text-black w3-hover-text-white w3-block"
        End If


        pnl.Controls.Add(lblText)
        pnl.Controls.Add(lblVal)
        Return pnl
    End Function

    Public Function generarPanel(tabla As DataTable, Optional PanelID As String = "") As RadAjaxPanel
        Dim row As RadAjaxPanel = crearRow()
        If tabla.Rows.Count = 0 Then
            For Each col As DataColumn In tabla.Columns
                row.Controls.Add(crearCol(col.ColumnName, ""))
            Next
        Else
            Dim cuantos As Integer = tabla.Rows.Count - 1
            For Each dr As DataRow In tabla.Rows
                For i = 0 To tabla.Columns.Count - 1
                    row.Controls.Add(crearCol(tabla.Columns(i).ColumnName, dr(i).ToString))
                Next
                If cuantos > 0 Then
                    Dim pnl As New RadAjaxPanel
                    pnl.CssClass = "w3-col s12"
                    Dim lblVal As New RadLabel
                    lblVal.Text = " "
                    lblVal.CssClass = "w3-border w3-round w3-block w3-border-indigo"
                    pnl.Controls.Add(lblVal)
                    row.Controls.Add(pnl)
                End If
            Next
        End If
        Return row
    End Function
End Class
