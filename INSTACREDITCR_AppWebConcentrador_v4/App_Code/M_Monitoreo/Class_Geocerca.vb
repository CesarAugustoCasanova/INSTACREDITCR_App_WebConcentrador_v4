Imports System.Data
Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports Db
Imports System.Globalization
Imports System.IO
Imports System.Web.SessionState.HttpSessionState
Public Class Class_GeoCerca

    Public Shared Function Geo_Usuarios(ByVal V_Usuario As String, ByVal V_ID As String, ByVal V_Bandera As String) As DataTable
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_GEOCERCA"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("V_ID", SqlDbType.NVarChar).Value = V_ID
        SSCommand.Parameters.Add("V_Usuario", SqlDbType.NVarChar).Value = V_Usuario
        SSCommand.Parameters.Add("V_Bandera", SqlDbType.NVarChar).Value = V_Bandera
        SSCommand.Parameters.Add("V_Valor", SqlDbType.NVarChar).Value = ""
        Dim DtsUsuarios As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return DtsUsuarios
    End Function

    Public Shared Function Guardar_Coordenadas(ByVal V_ID As String, ByVal V_Usuario As String, ByVal V_Valor As String, ByVal V_Bandera As String) As DataTable
        Dim tabla As New DataTable()
        tabla.Columns.Add("Mensaje")
        Dim row As DataRow = tabla.NewRow()


        Try
            Borrar_Coordenadas(V_ID, V_Usuario, "", 3)
            Dim ArrayGeneral() As String = Strings.Split(V_Valor, "|")
            For i As Integer = 0 To ArrayGeneral.Count() - 2
                Dim SSCommand As New SqlCommand
                SSCommand.CommandText = "SP_GEOCERCA"
                SSCommand.CommandType = CommandType.StoredProcedure
                SSCommand.Parameters.Add("V_ID", SqlDbType.NVarChar).Value = V_ID
                SSCommand.Parameters.Add("V_Usuario", SqlDbType.NVarChar).Value = V_Usuario
                SSCommand.Parameters.Add("V_Bandera", SqlDbType.NVarChar).Value = V_Bandera
                SSCommand.Parameters.Add("V_Valor", SqlDbType.NVarChar).Value = ArrayGeneral(i)
                Dim DtsUsuarios As DataTable = Consulta_Procedure(SSCommand, "Elementos")
            Next
            row("Mensaje") = "GeoCerca Guardada"
            tabla.Rows.Add(row)
            Return tabla

        Catch ex As Exception
            row("Mensaje") = ex.Message
            tabla.Rows.Add(row)
            Return tabla
        End Try
    End Function
    Public Shared Function Borrar_Coordenadas(ByVal V_ID As String, ByVal V_Usuario As String, ByVal V_Valor As String, ByVal V_Bandera As String) As DataTable
        Dim tabla As New DataTable()
        tabla.Columns.Add("Mensaje")
        Dim row As DataRow = tabla.NewRow()
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_GEOCERCA"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("V_ID", SqlDbType.NVarChar).Value = V_ID
        SSCommand.Parameters.Add("V_Usuario", SqlDbType.NVarChar).Value = V_Usuario
        SSCommand.Parameters.Add("V_Bandera", SqlDbType.NVarChar).Value = V_Bandera
        SSCommand.Parameters.Add("V_Valor", SqlDbType.NVarChar).Value = ""
        Dim DtsUsuarios As DataTable = Consulta_Procedure(SSCommand, "Elementos")
        row("Mensaje") = "GeoCerca Guardada"
        tabla.Rows.Add(row)
        Return tabla
    End Function

End Class
