
Imports Telerik.Web.UI

Partial Class Modulos_Acciones
    Inherits System.Web.UI.Page
    Public Property tmpPermisos As IDictionary
        Get
            Return CType(Session("Permisos"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("Permisos") = value
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

    Protected Sub Change_UserControl_Click(sender As Object, e As EventArgs)
        Dim button As RadButton = DirectCast(sender, RadButton)
        Dim controlToLoad As String = ""
        Select Case button.ID
            Case "PT1"
                Response.Redirect("./Modulos_GenerarArchivo.aspx")
            Case "PT2"
                Response.Redirect("./Modulos_Asignacion.aspx")
            Case "PT3"
                Response.Redirect("./Modulos_GenerarEtiqueta.aspx")
            Case "PT4"
                Response.Redirect("./Modulos_GenerarScript.aspx")
            Case "PT5"
                Response.Redirect("./Modulos_Campana.aspx")
            Case "PT6"
                Response.Redirect("./Modulos_ColoresCuentas.aspx")

        End Select

    End Sub

    Private Sub M_Administrador_Default_Init(sender As Object, e As EventArgs) Handles Me.Init
        If Session("ReglaID") Is Nothing Then
            Response.Redirect("./Modulos_Reglas.aspx")
        End If
    End Sub
End Class
