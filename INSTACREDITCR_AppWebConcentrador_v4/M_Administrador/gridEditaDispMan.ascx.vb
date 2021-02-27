
Partial Class M_Administrador_gridEditaDispMan
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
    Public Property tmpUSUARIO As IDictionary
        Get
            Return CType(Session("USUARIO"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("USUARIO") = value
        End Set
    End Property

    Private Sub M_Administrador_gridEditaDispMan_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("PostBack") = True Then
            Session("PostBack") = False
            DdlUsuario.DataValueField = "usuario"
            DdlUsuario.DataTextField = "nombre"
            DdlUsuario.DataSource = Class_CatalogoDispersion.UsuariosInstancia(tmpUSUARIO("CAT_LO_INSTANCIA"))
            DdlUsuario.DataBind()
        End If
    End Sub
End Class
