
Imports Telerik.Web.UI

Partial Class Acuerdos
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

    Private Sub Acuerdos_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("PostBack") = True Then
            DdlEtapaDestino.DataValueField = "ID"
            DdlEtapaDestino.DataTextField = "VALOR"
            DdlEtapaDestino.DataSource = Class_Judicial.llenarcatalogojudicial("ETAPAS")
            DdlEtapaDestino.DataBind()
            Session("PostBack") = False

            If Session("LLenar") = True Then
                Session("LLenar") = False
                llenar()
            End If
        End If
    End Sub

    Private Sub DdlCambiaEtapa_SelectedIndexChanged(sender As Object, e As DropDownListEventArgs) Handles DdlCambiaEtapa.SelectedIndexChanged
        If DdlCambiaEtapa.SelectedValue = 1 Then
            LblEtapaDestino.Visible = True
            DdlEtapaDestino.Visible = True
        Else
            LblEtapaDestino.Visible = False
            DdlEtapaDestino.Visible = False
        End If
    End Sub

    Private Sub DdlPagoHonorarios_SelectedIndexChanged(sender As Object, e As DropDownListEventArgs) Handles DdlPagoHonorarios.SelectedIndexChanged
        NtxtMontoPagar.Text = "0"
        DdlPagoExterno.FindItemByValue(0).Selected = True
        If DdlPagoHonorarios.SelectedValue = 1 Then
            LblMontoPagar.Visible = True
            NtxtMontoPagar.Visible = True
            LblPagoExterno.Visible = True
            DdlPagoExterno.Visible = True
        Else
            LblMontoPagar.Visible = False
            NtxtMontoPagar.Visible = False
            LblPagoExterno.Visible = False
            DdlPagoExterno.Visible = False
            DdlIncluyeIVA.Visible = False
            LblIncluyeIVA.Visible = False
        End If
    End Sub

    Private Sub llenar()
        TxtNombre.Text = DataItem.row.item(1).ToString
        DdlIndicadorimpulso.SelectedValue = DataItem.row.item(2).ToString
        DdlCambiaEtapa.SelectedValue = DataItem.row.item(3).ToString
        DdlEtapaDestino.SelectedValue = DataItem.row.item(4).ToString
        DdlPagoHonorarios.SelectedValue = DataItem.row.item(5).ToString
        NtxtMontoPagar.Text = DataItem.row.item(6).ToString
        DdlPagoExterno.SelectedValue = DataItem.row.item(7).ToString
        DdlIncluyeIVA.SelectedValue = DataItem.row.item(8).ToString
        txtConcepto.Text = DataItem.row.item(11).ToString
        If DdlCambiaEtapa.SelectedValue = 1 Then
            LblEtapaDestino.Visible = True
            DdlEtapaDestino.Visible = True
        Else
            LblEtapaDestino.Visible = False
            DdlEtapaDestino.Visible = False
        End If
        If DdlPagoHonorarios.SelectedValue = 1 Then
            LblMontoPagar.Visible = True
            NtxtMontoPagar.Visible = True
        Else
            LblMontoPagar.Visible = False
            NtxtMontoPagar.Visible = False
        End If
    End Sub
End Class
