Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports Db
Imports System.Data
Public Class Class_Hist_Promesas
    Public Shared Function LlenarElementosHist_Promesas(ByVal V_Credito As String, ByVal V_Bandera As String, ByVal V_Producto As String) As Object

        Dim SSCommand As New SqlCommand("SP_HISTORICO_PROMESAS")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Credito", SqlDbType.NVarChar).Value = V_Credito
        SSCommand.Parameters.Add("@V_Producto", SqlDbType.NVarChar).Value = V_Producto
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = V_Bandera
        Dim DtsTelefonos As DataTable = Consulta_Procedure(SSCommand, SSCommand.CommandText)
        Return DtsTelefonos
    End Function
End Class