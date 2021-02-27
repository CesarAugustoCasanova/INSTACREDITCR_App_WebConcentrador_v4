Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports Conexiones
Imports System.Globalization
Imports System.IO
Imports Funciones
Imports Db
Imports System.Web.SessionState.HttpSessionState
Imports System.Data
Public Class Class_Honorarios

    Public Shared Function LlenarCtrlHono(ByVal V_Bandera As Integer, ByVal V_Usuario As String, ByVal V_Agencia As String, ByVal V_condiciones As String) As DataTable
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_RP_CTRL_HONORARIOS_V2"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_bandera", SqlDbType.Decimal).Value = V_Bandera
        SSCommand.Parameters.Add("@v_usuario", SqlDbType.NVarChar).Value = V_Usuario
        SSCommand.Parameters.Add("@v_agencia", SqlDbType.NVarChar).Value = V_Agencia
        SSCommand.Parameters.Add("@v_condiciones", SqlDbType.NVarChar).Value = V_condiciones

        Dim Dts As DataTable = Consulta_Procedure(SSCommand, "Honorarios")

        Return Dts

    End Function


    Public Shared Function DTCtrlHono(ByVal V_Bandera As Integer, ByVal V_Usuario As String, ByVal V_Agencia As String, ByVal v_credito As String, ByVal v_folio As String, ByVal v_valor As String, ByVal V_condiciones As String) As DataTable
        dim SSCommand as new sqlcommand
        SSCommand.CommandText = "SP_RP_CTRL_HONORARIOS_V2"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_bandera", SqlDbType.Decimal).Value = V_Bandera
        SSCommand.Parameters.Add("@v_usuario", SqlDbType.NVarChar).Value = V_Usuario
        SSCommand.Parameters.Add("@v_agencia", SqlDbType.NVarChar).Value = V_Agencia
        SSCommand.Parameters.Add("@v_credito", SqlDbType.NVarChar).Value = v_credito
        SSCommand.Parameters.Add("@v_folio", SqlDbType.NVarChar).Value = v_folio
        SSCommand.Parameters.Add("@v_valor", SqlDbType.NVarChar).Value = v_valor
        SSCommand.Parameters.Add("@v_condiciones", SqlDbType.NVarChar).Value = V_condiciones

        Dim DtsInvCampo As DataTable = Consulta_Procedure(SSCommand, "Catalogo")

        Return DtsInvCampo

    End Function





End Class
