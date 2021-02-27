Imports System.Data
Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports Db
Imports System.Globalization
Imports System.IO
Imports System.Web.SessionState.HttpSessionState
Public Class Class_InfoRutas

    Public Shared Function Inf_Usuarios(ByVal V_Usuario As String, ByVal V_Bandera As String) As DataTable
        Dim SScommand As New SqlCommand
        SScommand.CommandText = "SP_INFORUTAS"
        SScommand.CommandType = CommandType.StoredProcedure
        SScommand.Parameters.Add("V_Usuario", SqlDbType.NVarChar).Value = V_Usuario
        SScommand.Parameters.Add("V_Bandera", SqlDbType.NVarChar).Value = V_Bandera
        Dim DtsUsuarios As DataTable = Consulta_Procedure(SScommand, "ELEMENTOS")
        Return DtsUsuarios
    End Function
End Class