
Imports Telerik.Web.UI

Partial Class Modulos_Campana
    Inherits System.Web.UI.Page
    Private Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        Response.Redirect("./Modulos_Acciones.aspx")
    End Sub

    Private Sub ddlCampana_SelectedIndexChanged()
        phTipoCamapana.Controls.Clear()

        Select Case ddlCampana.SelectedValue
            Case 0
                Dim campanaSMS As M_Administrador_Modulos_Campana_SMS = TryCast(LoadControl("~/M_Administrador/Modulos_Campana_SMS.ascx"), M_Administrador_Modulos_Campana_SMS)
                phTipoCamapana.Controls.Add(campanaSMS)
        End Select
    End Sub

    Private Sub M_Administrador_Default_Load(sender As Object, e As EventArgs) Handles Me.Load
        'NO CAMBIAR DE LUGAR ESTE METODO
        'Los controles de usuario generados dinamicamente, deben de ir en el page load para que
        ' puedan hacer postback y manejo de eventos
        ddlCampana_SelectedIndexChanged()
    End Sub
End Class
