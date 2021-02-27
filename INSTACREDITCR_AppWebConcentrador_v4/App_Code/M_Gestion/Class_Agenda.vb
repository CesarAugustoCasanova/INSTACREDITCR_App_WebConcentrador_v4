Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports Db
Imports System.Globalization
Imports System.IO
Imports Funciones
Imports System.Web.SessionState.HttpSessionState
Imports System.Data
Public Class Class_Agenda
    Public Shared Function LlenarElementosAgenda(ByVal V_Valor As String, ByVal V_Usuario As String, ByVal V_Bandera As String) As DataTable
        Dim DtsHist_Act As DataTable
        Dim SSCommand As New SqlCommand("Sp_Filasagenda")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Valor", SqlDbType.NVarChar).Value = V_Valor
        SSCommand.Parameters.Add("@V_Usuario", SqlDbType.NVarChar).Value = V_Usuario
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = V_Bandera

        DtsHist_Act = Consulta_Procedure(SSCommand, "ELEMENTOS")


        Return DtsHist_Act
    End Function

End Class
