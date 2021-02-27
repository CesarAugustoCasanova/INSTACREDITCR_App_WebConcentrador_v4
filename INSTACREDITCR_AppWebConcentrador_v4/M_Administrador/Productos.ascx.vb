
Imports System.Data
Imports System.Data.SqlClient
Imports Db
Imports Telerik.Web.UI
Partial Class Productos
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

    Function Llenar() As DataTable
        Dim SSCommandAgencias As New SqlCommand
        SSCommandAgencias.CommandText = "SP_ADD_CATALOGOS"
        SSCommandAgencias.CommandType = CommandType.StoredProcedure
        SSCommandAgencias.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 5
        Dim DtsVarios As DataTable = Consulta_Procedure(SSCommandAgencias, "Mercado")

        If DtsVarios.Rows.Count > 0 Then

            DdlMercado.DataTextField = "MERCADOS"
            DdlMercado.DataValueField = "MERCADOS"
            DdlMercado.DataSource = DtsVarios
            DdlMercado.DataBind()
            DdlMercado.Items.Add("Seleccione")

        End If
        Return DtsVarios

    End Function
    Private Sub Productos_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("PostBack") = True Then
            Session("PostBack") = False
            Dim tabla As String = Session("Tabla")
            Llenar()
            If Session("Campo") <> "" Then
                DdlMercado.SelectedValue = Session("Campo")
            Else
                DdlMercado.SelectedValue = "Seleccione"
            End If

        End If
        ' If Session("Campo") <> "" Then
        'DdlMercado.SelectedValue = Session("Campo")

        ' Else
        'End If

    End Sub
End Class
