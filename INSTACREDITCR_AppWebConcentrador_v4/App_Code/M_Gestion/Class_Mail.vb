Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports Db
Imports System.Globalization
Imports System.IO
Imports Funciones
Imports System.Web.SessionState.HttpSessionState
Imports System.Data

Public Class Class_Mail


    Public Shared Function BorrarPerfil(ByVal MAIL_NOMBRE As String, ByVal MAIL_NCUENTA As String) As Object


        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_EMAILS"
        SSCommand.CommandType = CommandType.StoredProcedure

        SSCommand.Parameters.Add("@MAIL_NOMBRE", SqlDbType.NVarChar).Value = MAIL_NOMBRE
        SSCommand.Parameters.Add("@MAIL_NCUENTA", SqlDbType.NVarChar).Value = MAIL_NCUENTA
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 2

        Dim DtsDatos As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return DtsDatos


    End Function


    Public Shared Function EnviarMensaje(ByVal MAIL_NCUENTA As String, ByVal MAIL_CORREO As String, ByVal MAIL_NOMBRE As String, ByVal MAIL_PASS As String, ByVal MAIL_DESTINO As String, ByVal MAIL_MENSAJE As String, ByVal MAIL_NSERVER As String, ByVal MAIL_PORT As String) As Object

        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_EMAILS"
        SSCommand.CommandType = CommandType.StoredProcedure

        SSCommand.Parameters.Add("@MAIL_NCUENTA", SqlDbType.NVarChar).Value = MAIL_NCUENTA
        SSCommand.Parameters.Add("@MAIL_NOMBRE", SqlDbType.NVarChar).Value = MAIL_NOMBRE
        SSCommand.Parameters.Add("@MAIL_CORREO", SqlDbType.NVarChar).Value = MAIL_CORREO
        SSCommand.Parameters.Add("@MAIL_PASS", SqlDbType.NVarChar).Value = MAIL_PASS
        SSCommand.Parameters.Add("@MAIL_DESTINO", SqlDbType.NVarChar).Value = MAIL_DESTINO
        SSCommand.Parameters.Add("@MAIL_MENSAJE", SqlDbType.NVarChar).Value = MAIL_MENSAJE
        SSCommand.Parameters.Add("@MAIL_NSERVER", SqlDbType.NVarChar).Value = MAIL_NSERVER
        SSCommand.Parameters.Add("@MAIL_PORT", SqlDbType.NVarChar).Value = MAIL_PORT
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 6

        Dim DtsDatos As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return DtsDatos




    End Function


    Public Shared Function ActualizarPerfil(ByVal MAIL_NOMBRE As String, ByVal MAIL_NCUENTA As String, ByVal MAIL_CORREO As String, ByVal MAIL_PASS As String, ByVal MAIL_DESCRIPCION As String, ByVal MAIL_NSERVER As String) As Object


        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_EMAILS"
        SSCommand.CommandType = CommandType.StoredProcedure

        SSCommand.Parameters.Add("@MAIL_NOMBRE", SqlDbType.NVarChar).Value = MAIL_NOMBRE
        SSCommand.Parameters.Add("@MAIL_NCUENTA", SqlDbType.NVarChar).Value = MAIL_NCUENTA
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 5

        Dim DtsDatos As DataTable = Consulta_Procedure(SSCommand, "MAILS")
        Return DtsDatos


    End Function


    Public Shared Function RegistrarPerfil(ByVal MAIL_NOMBRE As String, ByVal MAIL_NCUENTA As String, ByVal MAIL_CORREO As String, ByVal MAIL_PASS As String, ByVal MAIL_DESCRIPCION As String, ByVal MAIL_NSERVER As String) As Object


        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_EMAILS"
        SSCommand.CommandType = CommandType.StoredProcedure

        SSCommand.Parameters.Add("@MAIL_NOMBRE", SqlDbType.NVarChar).Value = MAIL_NOMBRE
        SSCommand.Parameters.Add("@MAIL_NCUENTA", SqlDbType.NVarChar).Value = MAIL_NCUENTA
        SSCommand.Parameters.Add("@MAIL_CORREO", SqlDbType.NVarChar).Value = MAIL_CORREO
        SSCommand.Parameters.Add("@MAIL_PASS", SqlDbType.NVarChar).Value = MAIL_PASS
        SSCommand.Parameters.Add("@MAIL_DESCRIPCION", SqlDbType.NVarChar).Value = MAIL_DESCRIPCION
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 5



        Dim DtsDatos As DataTable = Consulta_Procedure(SSCommand, "MAILS")
        Return DtsDatos


    End Function















End Class
