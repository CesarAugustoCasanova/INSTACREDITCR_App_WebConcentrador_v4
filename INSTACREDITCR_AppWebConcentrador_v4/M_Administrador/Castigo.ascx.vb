
Imports Telerik.Web.UI

Partial Class Castigo
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

    Private Sub Castigo_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("PostBack") = True Then
            DdlInstancia.DataValueField = "ID"
            DdlInstancia.DataTextField = "VALOR"
            DdlInstancia.ClearSelection()
            DdlInstancia.DataSource = Class_Judicial.llenarcatalogojudicial("INSTANCIAS")
            DdlInstancia.DataBind()
            Session("PostBack") = False
            Try
                llenar()
            Catch ex As Exception

            End Try

        End If
    End Sub

    Private Sub DdlInstancia_SelectedIndexChanged(sender As Object, e As DropDownListEventArgs) Handles DdlInstancia.SelectedIndexChanged
        DdlUsuario.DataValueField = "USUARIO"
        DdlUsuario.DataTextField = "NOMBRE"
        DdlUsuario.DataSource = Class_Judicial.Catalogos(DdlInstancia.SelectedValue, 14)
        DdlUsuario.DataBind()
        DdlUsuario.Visible = True
        DdlUsuario.Visible = True
    End Sub
    Private Sub llenar()
        DdlUsuario.DataValueField = "USUARIO"
        DdlUsuario.DataTextField = "NOMBRE"
        DdlUsuario.DataSource = Class_Judicial.Catalogos(DataItem.row.item(1), 14)
        DdlUsuario.DataBind()
        DdlUsuario.Visible = True
        DdlUsuario.Visible = True
        DdlInstancia.SelectedValue = DataItem.row.item(1).ToString
        DdlUsuario.SelectedValue = DataItem.row.item(2).ToString
        DdlOrden.SelectedValue = DataItem.row.item(3).ToString
        RTBCorreos.Text = DataItem.row.item(6).ToString
    End Sub
End Class
