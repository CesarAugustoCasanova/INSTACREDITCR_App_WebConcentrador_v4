Imports System.Data
Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports Db
Imports System.Globalization
Imports System.IO
Imports System.Web.SessionState.HttpSessionState
Public Class Class_CheckInOut

    Public Shared Function Check_Usuarios(ByVal V_Usuario As String, ByVal V_Valor As String, ByVal V_Bandera As String) As DataTable
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_CheckInOut"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("V_Usuario", SqlDbType.NVarChar).Value = V_Usuario
        SSCommand.Parameters.Add("V_Valor", SqlDbType.NVarChar).Value = V_Valor
        SSCommand.Parameters.Add("V_Bandera", SqlDbType.NVarChar).Value = V_Bandera
        Dim DtsUsuarios As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return DtsUsuarios
    End Function
    Public Shared Function Check_Info(ByVal V_Usuario As String, ByVal V_Valor As String, ByVal V_Bandera As String) As DataTable
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_CheckInOut"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("V_Usuario", SqlDbType.NVarChar).Value = V_Usuario
        SSCommand.Parameters.Add("V_Valor", SqlDbType.NVarChar).Value = V_Valor
        SSCommand.Parameters.Add("V_Bandera", SqlDbType.NVarChar).Value = V_Bandera
        Dim DtsUsuarios As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return DtsUsuarios
    End Function


End Class
