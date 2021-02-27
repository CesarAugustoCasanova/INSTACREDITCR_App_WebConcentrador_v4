Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports Db
Imports System.Globalization
Imports System.IO
Imports Funciones
Imports System.Web.SessionState.HttpSessionState
Imports System.Data

Public Class Class_AvisosEnvio

    Public Shared Function Avisos(ByVal V_Cat_Av_Nombre_Regla As String, ByVal V_Cat_Av_Instancia As String, ByVal V_Cat_Av_Query As String, ByVal V_Cat_Av_Campo As String, ByVal V_Cat_Av_Tabla As String, ByVal V_Cat_Av_Descripciontabla As String, ByVal V_Cat_Av_Operador As String, ByVal V_Cat_Av_Valor As String, ByVal V_Cat_Av_Conector As String, ByVal V_Cat_Av_Descripcioncampo As String, ByVal V_Cat_Av_Descripcionconector As String, V_Cat_Av_Descripcionoperador As String, ByVal V_Cat_Av_Tipodato As String, ByVal V_Bandera As String, Optional v_consecutivo As Integer = -1) As DataTable
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_CATALOGO_AVISOS"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Cat_Av_Nombre_Regla", SqlDbType.NVarChar).Value = V_Cat_Av_Nombre_Regla
        SSCommand.Parameters.Add("@V_Cat_Av_Instancia", SqlDbType.NVarChar).Value = V_Cat_Av_Instancia
        SSCommand.Parameters.Add("@V_Cat_Av_Query", SqlDbType.NVarChar).Value = V_Cat_Av_Query
        SSCommand.Parameters.Add("@V_Cat_Av_Campo", SqlDbType.NVarChar).Value = V_Cat_Av_Campo
        SSCommand.Parameters.Add("@V_Cat_Av_Tabla", SqlDbType.NVarChar).Value = V_Cat_Av_Tabla
        SSCommand.Parameters.Add("@V_Cat_Av_Operador", SqlDbType.NVarChar).Value = V_Cat_Av_Operador
        SSCommand.Parameters.Add("@V_Cat_Av_Valor", SqlDbType.NVarChar).Value = V_Cat_Av_Valor
        SSCommand.Parameters.Add("@V_Cat_Av_Conector", SqlDbType.NVarChar).Value = V_Cat_Av_Conector
        SSCommand.Parameters.Add("@V_Cat_Av_Descripcioncampo", SqlDbType.NVarChar).Value = V_Cat_Av_Descripcioncampo
        SSCommand.Parameters.Add("@V_Cat_Av_Descripciontabla", SqlDbType.NVarChar).Value = V_Cat_Av_Descripciontabla
        SSCommand.Parameters.Add("@V_Cat_Av_Descripcionconector", SqlDbType.NVarChar).Value = V_Cat_Av_Descripcionconector
        SSCommand.Parameters.Add("@V_Cat_Av_Descripcionoperador", SqlDbType.NVarChar).Value = V_Cat_Av_Descripcionoperador
        SSCommand.Parameters.Add("@V_Cat_Av_Tipodato", SqlDbType.NVarChar).Value = V_Cat_Av_Tipodato
        SSCommand.Parameters.Add("@v_consecutivo", SqlDbType.Decimal).Value = v_consecutivo
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = V_Bandera
        Dim DtsDatos As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return DtsDatos
    End Function


    Public Shared Function DatosComboBox(ByVal v_bandera As String, Optional v_aux As String = "") As Object
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_CATALOGO_AVISOS"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Cat_Av_Nombre_Regla", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@V_Cat_Av_Instancia", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@V_Cat_Av_Query", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@V_Cat_Av_Campo", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@V_Cat_Av_Tabla", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@V_Cat_Av_Operador", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@V_Cat_Av_Valor", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@V_Cat_Av_Conector", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@V_Cat_Av_Descripcioncampo", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@V_Cat_Av_Descripciontabla", SqlDbType.NVarChar).Value = v_aux
        SSCommand.Parameters.Add("@V_Cat_Av_Descripcionconector", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@V_Cat_Av_Descripcionoperador", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@V_Cat_Av_Tipodato", SqlDbType.NVarChar).Value = ""
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
