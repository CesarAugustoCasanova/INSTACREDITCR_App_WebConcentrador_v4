Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports Conexiones
Imports System.Globalization
Imports System.IO
Imports Funciones
Imports Db
Imports System.Web.SessionState.HttpSessionState
Imports System.Data
Public Class Class_Visitas
    Public Shared Function LlenarElementos(ByVal V_HIST_VI_PRODUCTO As String, ByVal V_HIST_VI_RESULTADO As String, ByVal V_HIST_VI_VISITADOR As String, ByVal V_FECHA_INICIAL As String, ByVal V_FECHA_FINAL As String, ByVal V_CAMPO_FECHA As String, ByVal V_Where As String, ByVal V_Agencia As String, ByVal V_Bandera As String) As DataTable
        Dim SScommand As New SqlCommand
        SScommand.CommandText = "SP_RP_VISITAS"
        SScommand.CommandType = CommandType.StoredProcedure
        SScommand.Parameters.Add("@@V_HIST_VI_PRODUCTO", SqlDbType.NVarChar).Value = V_HIST_VI_PRODUCTO
        SScommand.Parameters.Add("@V_HIST_VI_RESULTADO", SqlDbType.NVarChar).Value = V_HIST_VI_RESULTADO
        SScommand.Parameters.Add("@V_HIST_VI_VISITADOR", SqlDbType.NVarChar).Value = V_HIST_VI_VISITADOR
        SScommand.Parameters.Add("@V_FECHA_INICIAL", SqlDbType.NVarChar).Value = V_FECHA_INICIAL
        SScommand.Parameters.Add("@V_FECHA_FINAL", SqlDbType.NVarChar).Value = V_FECHA_FINAL
        SScommand.Parameters.Add("@V_CAMPO_FECHA", SqlDbType.NVarChar).Value = V_CAMPO_FECHA
        SScommand.Parameters.Add("@V_Where", SqlDbType.NVarChar).Value = V_Where
        SScommand.Parameters.Add("@V_Agencia", SqlDbType.NVarChar).Value = V_Agencia
        SScommand.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = V_Bandera

        Dim DtsCodigos As DataTable = Consulta_Procedure(SScommand, "Catalogo")
        Return DtsCodigos
    End Function

    Public Shared Function LlenarGridVisitas(ByVal V_Bandera As Integer, ByVal V_Agencia As String, ByVal V_condiciones As String, ByVal v_dato1 As String, ByVal v_dato2 As String, ByVal v_dato3 As String) As DataTable
        Dim SScommand As New SqlCommand
        SScommand.CommandText = "SP_RP_VISITAS_V3"
        SScommand.CommandType = CommandType.StoredProcedure
        SScommand.Parameters.Add("@v_bandera", SqlDbType.Decimal).Value = V_Bandera
        SScommand.Parameters.Add("@v_agencia", SqlDbType.NVarChar).Value = V_Agencia
        SScommand.Parameters.Add("@v_condiciones", SqlDbType.NVarChar).Value = V_condiciones
        SScommand.Parameters.Add("@v_dato1", SqlDbType.NVarChar).Value = v_dato1
        SScommand.Parameters.Add("@v_dato2", SqlDbType.NVarChar).Value = v_dato2
        SScommand.Parameters.Add("@v_dato3", SqlDbType.NVarChar).Value = v_dato3

        Dim DtsCodigos As DataTable = Consulta_Procedure(SScommand, "Catalogo")
        Return DtsCodigos
    End Function

    Public Shared Function LlenarGridVisitasDT(ByVal V_Bandera As Integer, ByVal V_Agencia As String, ByVal V_condiciones As String, ByVal v_dato1 As String, ByVal v_dato2 As String, ByVal v_dato3 As String) As DataTable
        Dim SScommand As New SqlCommand
        SScommand.CommandText = "SP_RP_VISITAS_V3"
        SScommand.CommandType = CommandType.StoredProcedure
        SScommand.Parameters.Add("@v_bandera", SqlDbType.Decimal).Value = V_Bandera
        SScommand.Parameters.Add("@v_agencia", SqlDbType.NVarChar).Value = V_Agencia
        SScommand.Parameters.Add("@v_condiciones", SqlDbType.NVarChar).Value = V_condiciones
        SScommand.Parameters.Add("@v_dato1", SqlDbType.NVarChar).Value = v_dato1
        SScommand.Parameters.Add("@v_dato2", SqlDbType.NVarChar).Value = v_dato2
        SScommand.Parameters.Add("@v_dato3", SqlDbType.NVarChar).Value = v_dato3

        Dim DtsCodigos As DataTable = Consulta_Procedure(SScommand, "Catalogo")
        Return DtsCodigos
    End Function



End Class