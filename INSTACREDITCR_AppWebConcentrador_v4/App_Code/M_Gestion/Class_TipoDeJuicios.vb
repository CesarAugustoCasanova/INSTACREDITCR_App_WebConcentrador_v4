﻿Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.IO
Imports Funciones
Imports Db
Imports System.Web.SessionState.HttpSessionState
Imports System.Data
Public Class Class_TipoDeJuicios
    Public Shared Function LlenarElementos(ByVal V_Valor1 As String, ByVal V_Valor2 As String, ByVal V_Valor3 As String, ByVal V_Valor4 As String, ByVal V_Bandera As String) As DataTable
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_VARIOS_TIPOSjUICIOS"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Valor1", SqlDbType.NVarChar).Value = V_Valor1
        SSCommand.Parameters.Add("@V_Valor2", SqlDbType.NVarChar).Value = V_Valor2
        SSCommand.Parameters.Add("@V_Valor3", SqlDbType.NVarChar).Value = V_Valor3
        SSCommand.Parameters.Add("@V_Valor4", SqlDbType.NVarChar).Value = V_Valor4
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = V_Bandera

        Dim DtsNegociaciones As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return DtsNegociaciones
    End Function
End Class