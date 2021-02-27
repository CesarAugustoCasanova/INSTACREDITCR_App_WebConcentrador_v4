
Imports Telerik.Web.UI

Partial Class CierreJuicios
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

    Private Sub CierreJuicios_Load(sender As Object, e As EventArgs) Handles Me.Load
        DdlNombreProm.DataValueField = "ID"
        DdlNombreProm.DataTextField = "VALOR"
        DdlNombreProm.DataSource = Class_Judicial.llenarcatalogojudicial("PROMOCIONES")
        DdlNombreProm.DataBind()
        If Session("PostBack") = True Then
            Session("PostBack") = False
            llenar()
        End If
    End Sub

    Private Sub DdlValidaProm_SelectedIndexChanged(sender As Object, e As DropDownListEventArgs) Handles DdlValidaProm.SelectedIndexChanged
        If DdlValidaProm.SelectedValue = 1 Then
            LblNombreProm.Visible = True
            DdlNombreProm.Visible = True
        Else
            LblNombreProm.Visible = False
            DdlNombreProm.Visible = False
        End If
    End Sub

    Private Sub llenar()
        TxtNombre.Text = DataItem.row.item(1).ToString
        DdlValidaProm.SelectedValue = DataItem.row.item(4).ToString
        DdlNombreProm.SelectedValue = DataItem.row.item(2).ToString
        DdlValidaDiasMora.SelectedValue = DataItem.row.item(3).ToString
        DDLSuperior.SelectedValue = DataItem.row.item(5).ToString
        If DdlValidaProm.SelectedValue = 1 Then
            LblNombreProm.Visible = True
            DdlNombreProm.Visible = True
        Else
            LblNombreProm.Visible = False
            DdlNombreProm.Visible = False
        End If
    End Sub
End Class
