
Partial Class M_Administrador_PerfilUsuario
    Inherits System.Web.UI.Page
    Public Property tmpUSUARIO As IDictionary
        Get
            Return CType(Session("USUARIO"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("USUARIO") = value
        End Set
    End Property

    Private Sub M_Administrador_PerfilUsuario_Init(sender As Object, e As EventArgs) Handles Me.Init
        pnlPerfil.DataBind()
    End Sub
End Class
