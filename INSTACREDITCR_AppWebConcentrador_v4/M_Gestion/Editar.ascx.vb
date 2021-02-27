Imports Funciones
Imports System.Data
Imports System.Data.SqlClient
Imports Db

Partial Class Editar
    Inherits System.Web.UI.UserControl
    Private _dataItem As Object = Nothing

    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Gestion", "Editar.aspx", evento, ex, Cuenta, Captura, usr)
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
