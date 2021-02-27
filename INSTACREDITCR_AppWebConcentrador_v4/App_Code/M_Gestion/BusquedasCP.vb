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
Public Class BusquedasCP
    Inherits System.Web.Services.WebService
    <WebMethod(True)> _
    Public Function BuscarCodigo(ByVal prefixText As String, ByVal count As Integer) As System.String()
        Dim patronDatos As New List(Of String)
        dim SSCommand as new sqlcommand

        SSCommand.CommandText = "SP_BUSQUEDA_CP"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_PATRON", SqlDbType.NVarChar).Value = "%" & prefixText.ToUpper & "%"
        SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 1

        Dim registros As DataTable = Consulta_Procedure(SSCommand, "Busca")
        If registros.Rows.Count <> 0 Then
            For indice As Integer = 0 To registros.Rows.Count - 1
                patronDatos.Add(registros.Rows(indice)("CAT_SE_CODIGO"))
            Next
        End If
        Return patronDatos.ToArray()
    End Function
End Class