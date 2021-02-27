Imports Db
Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Class CatalogoPerfiles
    Inherits System.Web.UI.Page
    Public Sub RGVPerfiles_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RGVPerfiles.NeedDataSource
        Me.RGVPerfiles.DataSource = GetDataTablePerfil()
    End Sub
    Public Function GetDataTablePerfil() As DataTable
        Dim table1 As New DataTable

        table1 = Llenar(0, "", "CAT_PERFILES", 0)
        Return table1
    End Function
    Function Llenar(V_Cat_Id As Integer, V_Cat_Descripcion As String, V_Catalogo As String, V_Bandera As String) As DataTable
        Dim DtsVarios As DataTable
        Try
            Dim SSCommandAgencias As New SqlCommand
            SSCommandAgencias.CommandText = "Sp_Add_Catalogos"
            SSCommandAgencias.CommandType = CommandType.StoredProcedure
            SSCommandAgencias.Parameters.Add("@V_Cat_Id", SqlDbType.Decimal).Value = V_Cat_Id
            SSCommandAgencias.Parameters.Add("@V_Cat_Descripcion", SqlDbType.NVarChar).Value = V_Cat_Descripcion
            SSCommandAgencias.Parameters.Add("@V_Catalogo", SqlDbType.NVarChar).Value = V_Catalogo
            SSCommandAgencias.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = V_Bandera
            If V_Catalogo = "CAT_PERFILES" Then
                DtsVarios = Consulta_Procedure(SSCommandAgencias, "Perfiles")
                Dim DtvVarios As DataView = DtsVarios.DefaultView
            ElseIf V_Catalogo = "CAT_PRODUCTOS" Then
                DtsVarios = Consulta_Procedure(SSCommandAgencias, "Productos")
                Dim DtvVarios As DataView = DtsVarios.DefaultView
            ElseIf V_Catalogo = "CAT_LUGAR_PAGO" Then
                DtsVarios = Consulta_Procedure(SSCommandAgencias, "Pagos")
                Dim DtvVarios As DataView = DtsVarios.DefaultView
            ElseIf V_Catalogo = "CAT_GRUPO_REPORTES" Then
                DtsVarios = Consulta_Procedure(SSCommandAgencias, "Reportes")
                Dim DtvVarios As DataView = DtsVarios.DefaultView
            End If
            Return DtsVarios
        Catch ex As Exception
            Aviso(ex.Message)
        End Try
    End Function
    Protected Sub Aviso(ByVal MSJ As String)
        RadAviso.RadAlert(MSJ, 440, 155, "AVISO", Nothing)
    End Sub
End Class
