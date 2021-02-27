Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports Db
Imports System.Data
Public Class Class_Hist_Garantias
    Public Shared Function LlenarInfoGarantias(ByVal V_Credito As String, ByVal V_Bandera As Integer) As DataTable

        Dim SSCommand As New SqlCommand("SP_HISTORICO_GARANTIAS")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Credito", SqlDbType.NVarChar).Value = V_Credito
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = V_Bandera


        Dim DtsHist_Act As DataTable = Consulta_Procedure(SSCommand, "GARANTIAS")
        Return DtsHist_Act
    End Function

    Public Shared Function LlenarInfoInventarios(ByVal V_Credito As String, ByVal V_Bandera As Integer) As DataTable

        Dim SSCommand As New SqlCommand("SP_HISTORICO_INVENTARIOS")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Credito", SqlDbType.NVarChar).Value = V_Credito
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = V_Bandera


        Dim DtsHist_Act As DataTable = Consulta_Procedure(SSCommand, "INVENTARIOS")
        Return DtsHist_Act
    End Function

End Class
