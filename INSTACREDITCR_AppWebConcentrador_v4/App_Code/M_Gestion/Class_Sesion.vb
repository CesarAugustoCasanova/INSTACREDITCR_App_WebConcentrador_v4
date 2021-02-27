Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports Db
Imports System.Globalization
Imports System.IO
Imports Funciones
Imports System.Web.SessionState.HttpSessionState
Imports System.Data
Public Class Class_Sesion
    Public Shared Function LlenarElementos(ByVal V_USUARIO As String, ByVal V_CAMPANA As String, ByVal V_MODULO As String, ByVal V_MOTIVO As String, ByVal V_BANDERA As String, ByVal V_CONTRASENA As String, ByVal V_IP As String, ByVal V_EVENTO As String, ByVal V_HIST_LO_ID_LOGIN As String) As DataTable
        Dim SSCommand As New SqlCommand("SP_INGRESO")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_USUARIO", SqlDbType.NVarChar).Value = V_USUARIO
        SSCommand.Parameters.Add("@V_CAMPANA", SqlDbType.NVarChar).Value = V_CAMPANA
        SSCommand.Parameters.Add("@V_MODULO", SqlDbType.NVarChar).Value = V_MODULO
        SSCommand.Parameters.Add("@V_MOTIVO", SqlDbType.NVarChar).Value = V_MOTIVO
        SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = V_BANDERA
        SSCommand.Parameters.Add("@V_CONTRASENA", SqlDbType.NVarChar).Value = V_CONTRASENA
        SSCommand.Parameters.Add("@V_IP", SqlDbType.NVarChar).Value = V_IP
        SSCommand.Parameters.Add("@V_EVENTO", SqlDbType.NVarChar).Value = V_EVENTO
        SSCommand.Parameters.Add("@V_HIST_LO_ID_LOGIN", SqlDbType.NVarChar).Value = V_HIST_LO_ID_LOGIN
        Dim DtsIngreso As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return DtsIngreso
    End Function

End Class
