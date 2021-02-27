Imports System.Data
Imports System.Data.SqlClient
Imports Db
Imports Telerik.Web.UI
Partial Class Mercados
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

    Function Llenar() As DataSet
        DdlTipo.Items.Add("Activo")
        DdlTipo.Items.Add("Inactivo")

    End Function
    Private Sub Mercados_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("PostBack") = True Then
            Session("PostBack") = False
            Llenar()
        End If
    End Sub
End Class
