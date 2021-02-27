
Partial Class Perfiles
    Inherits System.Web.UI.UserControl
    Private _dataItem As Object = Nothing
    Private Sub Perfiles_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub
    Protected Sub BtnAceptarConfirmacion_Click(sender As Object, e As EventArgs) Handles BtnAceptarConfirmacion.Click

    End Sub
    Protected Sub Confirma(ByVal MSJ As String)
        RadAviso.RadConfirm(MSJ, "confirmCallbackFn", 440, 155, Nothing, "Alerta")
    End Sub
    Public Property DataItem() As Object
        Get
            Return Me._dataItem
        End Get
        Set(ByVal value As Object)
            Me._dataItem = value
        End Set
    End Property
End Class
