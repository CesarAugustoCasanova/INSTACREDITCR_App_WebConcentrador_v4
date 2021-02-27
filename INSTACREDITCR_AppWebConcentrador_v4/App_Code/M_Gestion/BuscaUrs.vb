Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data
Imports System.Data.SqlClient
Imports Db
Imports System.IO
Imports System.Collections.Generic

<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class BusquedasUsr
    Inherits System.Web.Services.WebService
    <WebMethod(True)> _
    Public Function BuscarUsuario(ByVal prefixText As String, ByVal count As Integer) As System.String()
        Dim patronDatos As New List(Of String)
        Dim SSCommandBuscar As New SqlCommand
        SSCommandBuscar.CommandText = "SP_BUSQUEDA_USUARIO"
        SSCommandBuscar.CommandType = CommandType.StoredProcedure
        SSCommandBuscar.Parameters.Add("@V_Patron", SqlDbType.NVarChar).Value = "%" & prefixText.ToUpper & "%"
        SSCommandBuscar.Parameters.Add("@V_Agencia", SqlDbType.NVarChar).Value = CType(Session("Usuario"), USUARIO).cat_Lo_Num_Agencia
        SSCommandBuscar.Parameters.Add("@V_Usuario", SqlDbType.NVarChar).Value = CType(Session("Usuario"), USUARIO).CAT_LO_USUARIO
        SSCommandBuscar.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 1
        Dim registros As DataTable = Consulta_Procedure(SSCommandBuscar, "Busca")

        If registros.Rows.Count <> 0 Then
            For indice As Integer = 0 To registros.Rows.Count - 1
                patronDatos.Add(registros.Rows(indice)("USUARIO"))
            Next
        End If
        Return patronDatos.ToArray()
    End Function
End Class