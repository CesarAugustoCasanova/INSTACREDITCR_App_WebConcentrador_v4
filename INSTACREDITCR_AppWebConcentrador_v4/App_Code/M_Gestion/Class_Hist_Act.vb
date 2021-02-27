Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports Db
Imports System.Globalization
Imports System.IO
Imports Funciones
Imports System.Web.SessionState.HttpSessionState
Imports System.Data
Public Class Class_Hist_Act
    Public Shared Function LlenarElementosHistAct(ByVal V_Credito As String, ByVal V_Campo As String, ByVal V_Tipo As String, ByVal V_Bandera As Integer, ByVal V_Producto As String) As Object

        Dim SSCommand As New SqlCommand("SP_HISTORICO_ACTIVIDADES")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Credito", SqlDbType.NVarChar).Value = V_Credito
        SSCommand.Parameters.Add("@V_Producto", SqlDbType.NVarChar).Value = V_Producto
        SSCommand.Parameters.Add("@V_Campo", SqlDbType.NVarChar).Value = V_Campo
        SSCommand.Parameters.Add("@V_Tipo", SqlDbType.NVarChar).Value = V_Tipo
        SSCommand.Parameters.Add("@v_bandera", SqlDbType.NVarChar).Value = V_Bandera

        Dim DtsHist_Act As DataTable = Consulta_Procedure(SSCommand, SSCommand.CommandText)
        Return DtsHist_Act
    End Function


End Class
