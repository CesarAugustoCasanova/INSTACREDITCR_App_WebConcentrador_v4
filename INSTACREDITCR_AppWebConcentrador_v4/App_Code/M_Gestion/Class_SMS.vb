Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports Db
Imports System.Globalization
Imports System.IO
'Imports Funciones
Imports System.Web.SessionState.HttpSessionState
Imports System.Data
Public Class Class_SMS
    Public Shared Function LlenarElementos(ByVal V_Valor1 As String, ByVal V_Valor2 As String, ByVal V_Valor3 As String, ByVal V_Valor4 As String, ByVal V_Valor5 As String, ByVal V_Valor6 As String, ByVal V_Valor7 As String, ByVal V_Valor8 As String, ByVal V_Valor9 As String, ByVal V_Bandera As String) As Object
        Dim SSCommandSms As New SqlCommand
        SSCommandSms.CommandText = "SP_VARIOS_SMS"
        SSCommandSms.CommandType = CommandType.StoredProcedure
        SSCommandSms.Parameters.Add("@V_Valor1", SqlDbType.NVarChar).Value = V_Valor1
        SSCommandSms.Parameters.Add("@V_Valor2", SqlDbType.NVarChar).Value = V_Valor2
        SSCommandSms.Parameters.Add("@V_Valor3", SqlDbType.NVarChar).Value = V_Valor3
        SSCommandSms.Parameters.Add("@V_Valor4", SqlDbType.NVarChar).Value = V_Valor4
        SSCommandSms.Parameters.Add("@V_Valor5", SqlDbType.NVarChar).Value = V_Valor5
        SSCommandSms.Parameters.Add("@V_Valor6", SqlDbType.NVarChar).Value = V_Valor6
        SSCommandSms.Parameters.Add("@V_Valor7", SqlDbType.NVarChar).Value = V_Valor7
        SSCommandSms.Parameters.Add("@V_Valor8", SqlDbType.NVarChar).Value = V_Valor8
        SSCommandSms.Parameters.Add("@V_Valor9", SqlDbType.NVarChar).Value = V_Valor9
        SSCommandSms.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = V_Bandera
        Dim DtsNegociaciones As DataTable = Consulta_Procedure(SSCommandSms, "ELEMENTOS")
        Return DtsNegociaciones
    End Function
End Class
