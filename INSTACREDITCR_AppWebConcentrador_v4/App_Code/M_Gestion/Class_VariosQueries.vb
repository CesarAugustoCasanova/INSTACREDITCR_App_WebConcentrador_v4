Imports System.Data
Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports Db
Imports System.Globalization
Imports System.IO
Imports System.Web.SessionState.HttpSessionState
Public Class Class_VariosQueries

    Public Shared Function VARIOS_QUERIES(ByVal V_Usuario As String, ByVal V_Bandera As String) As Object
        dim SSCommand as new sqlcommand
        SSCommand.CommandText = "SP_VARIOS_QUERIES"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Usuario", SqlDbType.NVarChar).Value = V_Usuario
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = V_Bandera
        Dim DtsUsuarios As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return DtsUsuarios
    End Function

    Public Shared Function VARIOS_QRS(ByVal V_Usuario As String, ByVal V_VALOR As String, ByVal V_Bandera As String) As Object
        dim SSCommand as new sqlcommand
        SSCommand.CommandText = "SP_VARIOS_QRS"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_USUARIO", SqlDbType.NVarChar).Value = V_Usuario
        SSCommand.Parameters.Add("@V_VALOR", SqlDbType.NVarChar).Value = V_VALOR
        SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.NVarChar).Value = V_Bandera

        Dim DtsVariable As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return DtsVariable
    End Function
End Class
