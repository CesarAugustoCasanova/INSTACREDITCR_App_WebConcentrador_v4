
Partial Class Promociones
    Inherits System.Web.UI.UserControl
    Private _dataItem As Object = Nothing
    Public Property DataItem() As Object
        Get
            Return Me._dataItem
        End Get
        Set(ByVal value As Object)
            Me._dataItem = value
        End Set
    End Property

    Private Sub Promociones_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("PostBack") = True Then
            Session("PostBack") = False
            llenar()
        End If
    End Sub
    Private Sub llenar()
        TxtNombre.Text = DataItem.row.item(1).ToString
        DdlComodin.SelectedValue = DataItem.row.item(2).ToString
        DdlCambiaEtapa.SelectedValue = DataItem.row.item(3).ToString
        NtxtDiasRespuesta.Text = DataItem.row.item(4).ToString
        DdlTipoDiasRes.SelectedValue = DataItem.row.item(5).ToString
    End Sub
End Class
