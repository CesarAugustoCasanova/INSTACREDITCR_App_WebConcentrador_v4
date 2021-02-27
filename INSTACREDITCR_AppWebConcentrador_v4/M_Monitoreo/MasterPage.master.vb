
Imports Telerik.Web.UI

Partial Class MasterPage
    Inherits System.Web.UI.MasterPage
    Public Property tmpUSUARIO As IDictionary
        Get
            Return CType(Session("USUARIO"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("USUARIO") = value
        End Set
    End Property

    Private Sub MasterPage_Init(sender As Object, e As EventArgs) Handles Me.Init
        Try
            Dim sessionactive As Integer = GetSessionActive.Search(tmpUSUARIO("CAT_LO_USUARIO"), 1)

            If tmpUSUARIO Is Nothing Or sessionactive = 0 Then
                Session.Clear()
                Session.Abandon()
                Response.Redirect("~/SesionExpirada.aspx")
            End If

        Catch ex As System.Threading.ThreadAbortException

        Catch ex As Exception
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/SesionExpirada.aspx")
        End Try
    End Sub

    'Protected Sub RadMenu1_ItemClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadMenuEventArgs) Handles MenuMaster.ItemClick

    '    Dim ItemClicked As Telerik.Web.UI.RadMenuItem = e.Item
    '    If ItemClicked.ImageUrl = "Images/regresar.png" Then
    '        Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Bakcoffice", "Cierre de modulo")
    '        Response.Redirect("~/Modulos.aspx")
    '    ElseIf ItemClicked.ImageUrl = "Images/salir.png" Then
    '        Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Bakcoffice", "Cierre de sesión")
    '        Session.Abandon()
    '        Response.Redirect("~/Login.aspx", False)
    '    End If
    'End Sub
End Class

