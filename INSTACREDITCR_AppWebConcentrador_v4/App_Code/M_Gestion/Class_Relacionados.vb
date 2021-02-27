Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports Db
Imports System.Globalization
Imports System.IO
Imports Funciones
Imports System.Web.SessionState.HttpSessionState
Imports System.Data
Public Class Class_Relacionados
    Public Shared Function LlenarElementosRelacionados(ByVal V_rfc As String, ByVal V_IDcliente As String, ByVal V_Bandera As String) As Object
        Dim SSCommand As New SqlCommand("SP_CREDITOS_RELACIONADOS2")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_rfc", SqlDbType.NVarChar).Value = V_rfc
        SSCommand.Parameters.Add("@V_IDcliente", SqlDbType.NVarChar).Value = V_IDcliente
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = V_Bandera
        Dim DtsTelefonos As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return DtsTelefonos
    End Function

    Public Shared Function LlenarParticipantes(ByVal V_rfc As String, ByVal V_IDcliente As String, ByVal V_Bandera As String) As Object
        Dim SSCommand As New SqlCommand("SP_CREDITOS_RELACIONADOS2")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_rfc", SqlDbType.NVarChar).Value = V_rfc
        SSCommand.Parameters.Add("@V_IDcliente", SqlDbType.NVarChar).Value = V_IDcliente
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = V_Bandera
        Dim DtsTelefonos As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return DtsTelefonos
    End Function
End Class
