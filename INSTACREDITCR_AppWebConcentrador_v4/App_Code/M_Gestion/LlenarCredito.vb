Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports Db
Imports System.Web.SessionState.HttpSessionState
Imports System.Data
Imports System.Collections
Imports Newtonsoft.Json

Public Class LlenarCredito
    Public Shared Function Busca(ByVal CREDITO As String, ByVal PRODUCTO As String) As IDictionary

        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_INFORMACION_GENERAL"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_CREDITO", SqlDbType.NVarChar).Value = CREDITO
        SSCommand.Parameters.Add("@V_PRODUCTO", SqlDbType.NVarChar).Value = PRODUCTO
        Dim DtsLlenarCredito As DataTable = Consulta_Procedure(SSCommand, "VI_INFORMACION_CREDIFIEL")
        If DtsLlenarCredito.TableName = "Exeption" Then
            Throw New Exception(DtsLlenarCredito.Rows(0).Item(0).ToString.Replace("'", "").Replace("""", "").Replace(Chr(10), "").Replace(Chr(13), ""))
        End If
        Dim gson As String = JsonConvert.SerializeObject(DtsLlenarCredito, Newtonsoft.Json.Formatting.None)
        gson = gson.Substring(1, gson.Length - 2)
        Dim cr As IDictionary(Of String, String) = New Dictionary(Of String, String)()
        cr = JsonConvert.DeserializeObject(Of IDictionary(Of String, String))(gson)
        Return cr
    End Function

End Class
