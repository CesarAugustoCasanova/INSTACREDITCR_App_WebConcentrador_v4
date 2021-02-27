 
Partial Class ExpiroSesion
    Inherits System.Web.UI.Page

    Protected Sub guardarcon_Click(sender As Object, e As EventArgs) Handles BtnSalir.Click
        Response.Redirect("LogIn.aspx")
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        MpuMensajes.Show()
    End Sub
End Class
