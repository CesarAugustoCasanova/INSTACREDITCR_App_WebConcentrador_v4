Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Services
Imports Db
Imports Telerik.Web.UI

Public Class Busquedas
    Public Shared Function Search(ByVal V_Valor As String) As Object

        Dim SSCommand As New sqlcommand
        SSCommand.CommandText = "SP_BUSQUEDAS"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_valorBuscar", SqlDbType.NVarChar).Value = V_Valor
        SSCommand.Parameters.Add("@V_AGENCIA", SqlDbType.NVarChar).Value = CType(HttpContext.Current.Session("Usuario"), IDictionary)("CAT_LO_CADENAAGENCIAS").ToString
        SSCommand.Parameters.Add("@V_PRODUCTOS", SqlDbType.NVarChar).Value = CType(HttpContext.Current.Session("Usuario"), IDictionary)("CAT_LO_CADENAPRODUCTOS").ToString

        Dim DtsBusca As DataTable = Consulta_Procedure(SSCommand, "Busca")
        Return DtsBusca
    End Function

End Class
