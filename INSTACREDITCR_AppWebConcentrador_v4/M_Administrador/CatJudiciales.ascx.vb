
Partial Class CatJudiciales
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

    Private Sub CatJudiciales_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("Tipo") = "CAT_PROMOCIONES_JUDICIALES" Then
            LblComodin.Visible = True
            ChkbxComodin.Visible = True
            If Session("Chck") = True Then
                ChkbxComodin.Checked = True
            End If
        End If
    End Sub
End Class
