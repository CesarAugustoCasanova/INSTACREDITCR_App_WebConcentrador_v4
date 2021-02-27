Imports System.Data.SqlClient
Imports Db
Imports System.Data

Public Class Class_Auditoria
    Public Shared Sub GuardarValorAuditoria(ByVal quien As String, ByVal donde As String, ByVal que As String)

        Dim SSCommand As New SqlCommand("SP_AUDITORIA_GLOBAL")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_quien", SqlDbType.NVarChar).Value = quien
        SSCommand.Parameters.Add("@v_donde", SqlDbType.NVarChar).Value = donde
        SSCommand.Parameters.Add("@v_que", SqlDbType.NVarChar).Value = que
        SSCommand.Parameters.Add("@v_bandera", SqlDbType.Int).Value = 0
        Dim DtsHist_Fichas As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
    End Sub
End Class
