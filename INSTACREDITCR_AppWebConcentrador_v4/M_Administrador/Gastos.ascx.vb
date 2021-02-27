
Partial Class Gastos

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
    Protected Sub Confirma(ByVal MSJ As String)
        RadAviso.RadConfirm(MSJ, "confirmCallbackFn", 440, 155, Nothing, "Alerta")
    End Sub


End Class
