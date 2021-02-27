Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports Db
Imports System.Globalization
Imports System.IO
Public Class CerrarSesion
    Public Shared Sub cerrar(v_usuario As String)


        Dim SSCommand As New SqlCommand("SP_GET_SESSION_ACTIVE")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_USUARIO", SqlDbType.NVarChar).Value = v_usuario
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 2

        Dim Dt As DataTable = Consulta_Procedure(SSCommand, "LogOn")
    End Sub
End Class
