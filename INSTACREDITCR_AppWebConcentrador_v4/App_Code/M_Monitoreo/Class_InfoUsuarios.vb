Imports System.Data
Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports Db
Imports System.Globalization
Imports System.IO
Imports System.Web.SessionState.HttpSessionState
Public Class Class_InfoUsuarios

    Public Shared Function Inf_Usuarios(ByVal V_Usuario As String, ByVal V_Condicion As String, ByVal V_Bandera As String) As DataTable
        Dim SSCOMMAND As New SqlCommand
        SSCOMMAND.CommandText = "SP_INFOUSUARIOS"
        SSCOMMAND.CommandType = CommandType.StoredProcedure
        SSCOMMAND.Parameters.Add("@V_Usuario", SqlDbType.NVarChar).Value = V_Usuario
        SSCOMMAND.Parameters.Add("@V_Condicion", SqlDbType.NVarChar).Value = V_Condicion
        SSCOMMAND.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = V_Bandera
        Dim DtsUsuarios As DataTable = Consulta_Procedure(SSCOMMAND, "ELEMENTOS")
        Return DtsUsuarios
    End Function
    Public Shared Function Monitoreo(ByVal V_Usuario As String, ByVal V_Sucursales As String, ByVal V_Bandera As Integer) As DataTable
        Dim SSCOMMAND As New SqlCommand
        SSCOMMAND.CommandText = "SP_MONITOREO"
        SSCOMMAND.CommandType = CommandType.StoredProcedure
        SSCOMMAND.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = V_Bandera
        SSCOMMAND.Parameters.Add("@v_sucursales", SqlDbType.NVarChar).Value = V_Sucursales
        SSCOMMAND.Parameters.Add("@V_Usuario", SqlDbType.NVarChar).Value = V_Usuario
        Dim DtsUsuarios As DataTable = Consulta_Procedure(SSCOMMAND, "ELEMENTOS")
        Return DtsUsuarios
    End Function
    Public Shared Function MonitoreoV2(ByVal V_agrupacion As String, ByVal V_tipo As String, ByVal v_filtro As String, ByVal v_Tipotope As String, ByVal V_Bandera As Integer) As DataTable
        Dim cadenacomas As String = "'"
        If V_tipo = "1" Then
            Dim s As String() = V_agrupacion.Split(",")
            For Each separado As String In s
                cadenacomas += separado & "','"
            Next
            cadenacomas = cadenacomas.Substring(0, cadenacomas.Length - 2)
            V_agrupacion = cadenacomas
        End If
        Dim cambio As String = "'"
        Dim h As String() = v_filtro.Split(",")
        For Each separados As String In h
            cambio += separados & "','"
        Next
        v_filtro = cambio.Substring(0, cambio.Length - 2)

        Dim SSCOMMAND As New SqlCommand
        SSCOMMAND.CommandText = "SP_RP_MONITOREO_V2"
        SSCOMMAND.CommandType = CommandType.StoredProcedure
        SSCOMMAND.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = V_Bandera
        SSCOMMAND.Parameters.Add("@v_dato1", SqlDbType.NVarChar).Value = V_agrupacion
        SSCOMMAND.Parameters.Add("@v_dato2", SqlDbType.NVarChar).Value = V_tipo
        SSCOMMAND.Parameters.Add("@v_dato3", SqlDbType.NVarChar).Value = v_filtro
        SSCOMMAND.Parameters.Add("@v_condiciones", SqlDbType.NVarChar).Value = v_Tipotope
        Dim DtsUsuarios As DataTable = Consulta_Procedure(SSCOMMAND, "ELEMENTOS")
        Return DtsUsuarios
    End Function
End Class
