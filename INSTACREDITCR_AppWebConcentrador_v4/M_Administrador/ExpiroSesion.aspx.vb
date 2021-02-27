 
Partial Class ExpiroSesion
    Inherits System.Web.UI.Page


    Protected Sub Confirma(ByVal MSJ As String)
        RadAviso.RadAlert(MSJ, 440, 155, "Alerta", "confirmCallbackFn")

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Confirma("Expiro Sesion")
    End Sub

    Private Sub BtnAceptarConfirmacion_Click(sender As Object, e As EventArgs) Handles BtnAceptarConfirmacion.Click
        Session.Clear()
        Session.Abandon()
        Response.Redirect("LogIn.aspx")
    End Sub
End Class
