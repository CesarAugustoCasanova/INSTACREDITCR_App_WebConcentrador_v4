
Partial Class TramitesJudiciales
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

    Private Sub TramitesJudiciales_Load(sender As Object, e As EventArgs) Handles Me.Load
        Session("tramite") = DdlTipoTramite.SelectedValue
        DdlTipoTramite.DataValueField = "VALOR"
        DdlTipoTramite.DataTextField = "VALOR"
        DdlTipoTramite.DataSource = Class_Judicial.llenarcatalogojudicial("TRAMITES")
        DdlTipoTramite.DataBind()
        If Session("PostBack") = True Then
            Session("PostBack") = False
            llenar()
        End If
    End Sub
    Private Sub llenar()
        DdlTipoTramite.SelectedValue = DataItem.row.item(1).ToString
        DdlTipoInscripcion.SelectedValue = DataItem.row.item(2).ToString
        DdlBienEmbargado.SelectedValue = DataItem.row.item(3).ToString
        DdlFolioInmobiliario.SelectedValue = DataItem.row.item(4).ToString
        DdlFechaEvento.SelectedValue = DataItem.row.item(5).ToString
        DdlHoraEvento.SelectedValue = DataItem.row.item(6).ToString
        DdlPosicion.SelectedValue = DataItem.row.item(7).ToString
        DdlObservaciones.SelectedValue = DataItem.row.item(8).ToString
        DdlFechaAvaluo.SelectedValue = DataItem.row.item(9).ToString
        DdlValorComercial.SelectedValue = DataItem.row.item(10).ToString
        DdlGarantia.SelectedValue = DataItem.row.item(11).ToString
        DdlOrigenGarantia.SelectedValue = DataItem.row.item(12).ToString
    End Sub
End Class
