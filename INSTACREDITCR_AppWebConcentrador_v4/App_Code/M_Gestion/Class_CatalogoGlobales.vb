Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports Db
Imports System.Globalization
Imports System.IO
Imports Funciones
Imports System.Web.SessionState.HttpSessionState
Imports System.Data

Public Class Class_CatalogoGlobales

    Public Shared Function Instancia(ByVal V_Cat_Ai_Nombre_Regla As String, ByVal V_Cat_Ai_Instancia As String, ByVal V_Cat_Ai_Query As String, ByVal V_Cat_Ai_Campo As String, ByVal V_Cat_Ai_Tabla As String, ByVal V_Cat_Ai_Descripciontabla As String, ByVal V_Cat_Ai_Operador As String, ByVal V_Cat_Ai_Valor As String, ByVal V_Cat_Ai_Conector As String, ByVal V_Cat_Ai_Descripcioncampo As String, ByVal V_Cat_Ai_Descripcionconector As String, V_Cat_Ai_Descripcionoperador As String, ByVal V_Cat_Ai_Tipodato As String, ByVal V_Bandera As String) As DataTable
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_CATALOGO_INSTANCIAS"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Cat_Ai_Nombre_Regla", SqlDbType.NVarChar).Value = V_Cat_Ai_Nombre_Regla
        SSCommand.Parameters.Add("@V_Cat_Ai_Instancia", SqlDbType.NVarChar).Value = V_Cat_Ai_Instancia
        SSCommand.Parameters.Add("@V_Cat_Ai_Query", SqlDbType.NVarChar).Value = V_Cat_Ai_Query
        SSCommand.Parameters.Add("@V_Cat_Ai_Campo", SqlDbType.NVarChar).Value = V_Cat_Ai_Campo
        SSCommand.Parameters.Add("@V_Cat_Ai_Tabla", SqlDbType.NVarChar).Value = V_Cat_Ai_Tabla
        SSCommand.Parameters.Add("@V_Cat_Ai_Operador", SqlDbType.NVarChar).Value = V_Cat_Ai_Operador
        SSCommand.Parameters.Add("@V_Cat_Ai_Valor", SqlDbType.NVarChar).Value = V_Cat_Ai_Valor
        SSCommand.Parameters.Add("@V_Cat_Ai_Conector", SqlDbType.NVarChar).Value = V_Cat_Ai_Conector
        SSCommand.Parameters.Add("@V_Cat_Ai_Descripcioncampo", SqlDbType.NVarChar).Value = V_Cat_Ai_Descripcioncampo
        SSCommand.Parameters.Add("@V_Cat_Ai_Descripciontabla", SqlDbType.NVarChar).Value = V_Cat_Ai_Descripciontabla
        SSCommand.Parameters.Add("@V_Cat_Ai_Descripcionconector", SqlDbType.NVarChar).Value = V_Cat_Ai_Descripcionconector
        SSCommand.Parameters.Add("@V_Cat_Ai_Descripcionoperador", SqlDbType.NVarChar).Value = V_Cat_Ai_Descripcionoperador
        SSCommand.Parameters.Add("@V_Cat_Ai_Tipodato", SqlDbType.NVarChar).Value = V_Cat_Ai_Tipodato
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = V_Bandera

        Dim DtsDatos As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return DtsDatos
    End Function


    Public Shared Function DatosComboBox(ByVal v_bandera As String, Optional v_aux As String = "") As Object
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_CATALOGO_INSTANCIAS"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Cat_Ai_Nombre_Regla", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@V_Cat_Ai_Instancia", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@V_Cat_Ai_Query", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@V_Cat_Ai_Campo", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@V_Cat_Ai_Tabla", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@V_Cat_Ai_Operador", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@V_Cat_Ai_Valor", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@V_Cat_Ai_Conector", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@V_Cat_Ai_Descripcioncampo", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@V_Cat_Ai_Descripciontabla", SqlDbType.NVarChar).Value = v_aux
        SSCommand.Parameters.Add("@V_Cat_Ai_Descripcionconector", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@V_Cat_Ai_Descripcionoperador", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@V_Cat_Ai_Tipodato", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = v_bandera

        Dim DtsDatos As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return DtsDatos
    End Function


    Public Shared Function SimularAsignacion() As DataTable
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_ASIGNACION_INSTANCIA"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_usuario", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 0

        Dim DtsDatos As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return DtsDatos
    End Function

    Public Shared Function AplicarAsignacion(ByVal v_usuario As String) As DataTable
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_ASIGNACION_INSTANCIA"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_usuario", SqlDbType.NVarChar).Value = v_usuario
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 1

        Dim DtsDatos As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return DtsDatos
    End Function

    Public Shared Function ValidaCatalogo(ByVal V_Campo As String) As DataTable
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_VALIDA_CATALOGO"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Campo", SqlDbType.NVarChar).Value = V_Campo
        Dim DtsDatos As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return DtsDatos
    End Function
End Class
