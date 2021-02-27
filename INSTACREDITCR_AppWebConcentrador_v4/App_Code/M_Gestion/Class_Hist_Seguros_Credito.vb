Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports Db
Imports System.Data
Public Class Class_Hist_Seguros_Credito
    Public Shared Function LlenarInfoSegurosCredito(ByVal V_Credito As String, ByVal V_Bandera As Integer) As DataTable
        Dim SSCommand As New SqlCommand("SP_HISTORICO_SEGUROSCREDITO")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Credito", SqlDbType.NVarChar).Value = V_Credito
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = V_Bandera
        Dim DtsHist_Act As DataTable = Consulta_Procedure(SSCommand, "SEGUROS")
        Return DtsHist_Act
    End Function
End Class
