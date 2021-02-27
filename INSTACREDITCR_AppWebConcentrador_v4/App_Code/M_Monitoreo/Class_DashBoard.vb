Imports System.Data
Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports Db
Imports System.Globalization
Imports System.IO
Imports System.Web.SessionState.HttpSessionState
Public Class Class_DashBoard
    Public Shared Function LlenarLabel(ByVal V_Usuario As String, ByVal V_Bandera As String) As DataTable
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_DASHBOARD"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("V_Usuario", SqlDbType.VarChar).Value = V_Usuario
        SSCommand.Parameters.Add("V_Bandera", SqlDbType.VarChar).Value = V_Bandera
        Dim DtsUsuarios As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return DtsUsuarios
    End Function
    Public Shared Function LlenarGrafica(ByVal V_Usuario As String, ByVal V_Bandera As String) As DataTable
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_DASHBOARD"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("V_Usuario", SqlDbType.VarChar).Value = V_Usuario
        SSCommand.Parameters.Add("V_Bandera", SqlDbType.VarChar).Value = V_Bandera
        Dim DtsUsuarios As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return DtsUsuarios
    End Function
End Class
