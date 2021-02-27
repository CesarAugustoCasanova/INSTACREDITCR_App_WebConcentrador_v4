
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Services
Imports Db
Imports Telerik.Web.UI

Public Class GetSessionActive
    Public Shared Function Search(ByVal user As String, ByVal bandera As Int16) As Object

        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_GET_SESSION_ACTIVE"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_usuario", SqlDbType.VarChar).Value = user
        SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = bandera

        Dim DtsBusca As DataTable = Consulta_Procedure(SSCommand, "Busca")
        Return DtsBusca.Rows(0).Item(0)
    End Function
End Class







