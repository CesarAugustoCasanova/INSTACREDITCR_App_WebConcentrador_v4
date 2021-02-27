
Imports Telerik.Web.UI

Partial Class Juzgados
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

    Private Sub DdlEstado_SelectedIndexChanged(sender As Object, e As DropDownListEventArgs) Handles DdlEstado.SelectedIndexChanged
        DdlMunicipio.DataValueField = "MUNICIPIO"
        DdlMunicipio.DataTextField = "MUNICIPIO"
        DdlMunicipio.DataSource = Class_Judicial.Catalogos(DdlEstado.SelectedValue, 12)
        DdlMunicipio.DataBind()
        LblMunicipio.Visible = True
        DdlMunicipio.Visible = True
        Session("Mun") = "No"
    End Sub

    Private Sub Juzgados_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("Mun") <> "No" Then
            Session("Mun") = "No"

            DdlEstado.DataValueField = "ESTADO"
            DdlEstado.DataTextField = "ESTADO"
            DdlEstado.DataSource = Class_Judicial.llenarcatalogojudicial("ESTADOS")
            DdlEstado.DataBind()
        End If

        LblMunicipio.Visible = False
        DdlMunicipio.Visible = False
        If Session("PostBack") = True Then
            Session("PostBack") = False
            llenar()
        End If
    End Sub
    Private Sub llenar()
        DdlMunicipio.DataValueField = "MUNICIPIO"
        DdlMunicipio.DataTextField = "MUNICIPIO"
        DdlMunicipio.DataSource = Class_Judicial.Catalogos(DataItem.row.item(1), 12)
        DdlMunicipio.DataBind()
        LblMunicipio.Visible = True
        DdlMunicipio.Visible = True
        TxtNombre.Text = DataItem.row.item(3).ToString
        DdlEstado.SelectedValue = DataItem.row.item(1)
        DdlMunicipio.SelectedValue = DataItem.row.item(2).ToString
        DdlEstatus.SelectedValue = DataItem.row.item(4)
    End Sub
End Class
