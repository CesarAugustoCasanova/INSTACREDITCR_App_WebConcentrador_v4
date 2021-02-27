Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports Db
Imports System.Globalization
Imports System.IO
Imports Funciones
Imports System.Web.SessionState.HttpSessionState
Imports System.Data

Public Class Class_Cat_ReglasGlobal

    Public Shared Function ExisteRegla(ByVal V_Nombre As String) As Integer
        dim SSCommand as new sqlcommand
        SSCommand.CommandText = "SP_CATALOGO_CONFIG_RGLOBALES"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_CAT_CRG_GLOBAL", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@V_CAT_CRG_NOMBRE", SqlDbType.NVarChar).Value = V_Nombre
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 13

        Dim DtsDatos As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")

        Dim v_valor As Integer = DtsDatos.Rows(0)("EXISTE")

        Return v_valor
    End Function


    Public Shared Function InsertarRegla(ByVal V_Nombre As String, ByVal V_Usuario As String) As Object
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_CATALOGO_CONFIG_RGLOBALES"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_CAT_CRG_GLOBAL", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@V_CAT_CRG_NOMBRE", SqlDbType.NVarChar).Value = V_Nombre
        SSCommand.Parameters.Add("@V_CAT_CRG_QUERY", SqlDbType.NVarChar).Value = V_Usuario
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 4

        Dim DtsDatos As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return DtsDatos
    End Function

    Public Shared Function BorrarInstancia(ByVal V_Instancia As String) As Object
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_CATALOGO_CONFIG_RGLOBALES"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Instancia", SqlDbType.NVarChar).Value = V_Instancia
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 5

        Dim DtsDatos As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return DtsDatos
    End Function
    Public Shared Function InsertarParametro(ByVal V_IdRegla As String, ByVal V_NombreRegla As String, ByVal V_CAT_DIS_DESCRIPCIONOPERADOR As String, ByVal V_CAT_DIS_DESCRIPCIONCONECTOR As String, ByVal V_CAT_DIS_DESCRIPCIONTABLA As String, ByVal V_CAT_DIS_DESCRIPCIONCAMPO As String, ByVal V_Cat_DIS_Valor As String, ByVal V_Cat_DIS_Campo As String, ByVal V_CAT_DIS_TABLA As String, ByVal V_Cat_DIS_Operador As String, ByVal V_Cat_DIS_Conector As String, v_tipo As String) As Object

        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_CATALOGO_CONFIG_RGLOBALES"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_CAT_CRG_GLOBAL", SqlDbType.NVarChar).Value = V_IdRegla
        SSCommand.Parameters.Add("@V_CAT_CRG_NOMBRE", SqlDbType.NVarChar).Value = V_NombreRegla
        SSCommand.Parameters.Add("@V_CAT_CRG_QUERY", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@V_CAT_CRG_CAMPO", SqlDbType.NVarChar).Value = V_Cat_DIS_Campo
        SSCommand.Parameters.Add("@V_CAT_CRG_TABLA", SqlDbType.NVarChar).Value = V_CAT_DIS_TABLA
        SSCommand.Parameters.Add("@V_CAT_CRG_VALOR", SqlDbType.NVarChar).Value = V_Cat_DIS_Valor
        SSCommand.Parameters.Add("@V_CAT_CRG_OPERADOR", SqlDbType.NVarChar).Value = V_Cat_DIS_Operador
        SSCommand.Parameters.Add("@V_CAT_CRG_CONECTOR", SqlDbType.NVarChar).Value = V_Cat_DIS_Conector
        SSCommand.Parameters.Add("@V_CAT_CRG_DESCRIPCIONCAMPO", SqlDbType.NVarChar).Value = V_CAT_DIS_DESCRIPCIONCAMPO
        SSCommand.Parameters.Add("@V_CAT_CRG_DESCRIPCIONTABLA", SqlDbType.NVarChar).Value = V_CAT_DIS_DESCRIPCIONTABLA
        SSCommand.Parameters.Add("@V_CAT_CRG_DESCRIPCIONCONECTOR", SqlDbType.NVarChar).Value = V_CAT_DIS_DESCRIPCIONCONECTOR
        SSCommand.Parameters.Add("@V_CAT_CRG_DESCRIPCIONOPERADOR", SqlDbType.NVarChar).Value = V_CAT_DIS_DESCRIPCIONOPERADOR
        SSCommand.Parameters.Add("@V_CAT_CRG_RESPGESTION", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@v_CAT_CRG_TIPODATO", SqlDbType.NVarChar).Value = v_tipo
        SSCommand.Parameters.Add("@v_CAT_CRG_USUARIOS", SqlDbType.NVarChar).Value = ""

        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 3

        Dim DtsDatos As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return DtsDatos
    End Function
    Public Shared Function ActualizarParametro(ByVal V_IdRegla As String, ByVal V_NombreRegla As String, ByVal V_CAT_DIS_DESCRIPCIONOPERADOR As String, ByVal V_CAT_DIS_DESCRIPCIONCONECTOR As String, ByVal V_CAT_DIS_DESCRIPCIONTABLA As String, ByVal V_CAT_DIS_DESCRIPCIONCAMPO As String, ByVal V_Cat_DIS_Valor As String, ByVal V_Cat_DIS_Campo As String, ByVal V_CAT_DIS_TABLA As String, ByVal V_Cat_DIS_Operador As String, ByVal V_Cat_DIS_Conector As String, v_tipo As String, v_orden As String) As Object

        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_CATALOGO_CONFIG_RGLOBALES"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_CAT_CRG_GLOBAL", SqlDbType.Int).Value = V_IdRegla
        SSCommand.Parameters.Add("@V_CAT_CRG_NOMBRE", SqlDbType.NVarChar).Value = V_NombreRegla
        SSCommand.Parameters.Add("@V_CAT_CRG_QUERY", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@V_CAT_CRG_CAMPO", SqlDbType.NVarChar).Value = V_Cat_DIS_Campo
        SSCommand.Parameters.Add("@V_CAT_CRG_TABLA", SqlDbType.NVarChar).Value = V_CAT_DIS_TABLA
        SSCommand.Parameters.Add("@V_CAT_CRG_VALOR", SqlDbType.NVarChar).Value = V_Cat_DIS_Valor
        SSCommand.Parameters.Add("@V_CAT_CRG_OPERADOR", SqlDbType.NVarChar).Value = V_Cat_DIS_Operador
        SSCommand.Parameters.Add("@V_CAT_CRG_CONECTOR", SqlDbType.NVarChar).Value = V_Cat_DIS_Conector
        SSCommand.Parameters.Add("@V_CAT_CRG_DESCRIPCIONCAMPO", SqlDbType.NVarChar).Value = V_CAT_DIS_DESCRIPCIONCAMPO
        SSCommand.Parameters.Add("@V_CAT_CRG_DESCRIPCIONTABLA", SqlDbType.NVarChar).Value = V_CAT_DIS_DESCRIPCIONTABLA
        SSCommand.Parameters.Add("@V_CAT_CRG_DESCRIPCIONCONECTOR", SqlDbType.NVarChar).Value = V_CAT_DIS_DESCRIPCIONCONECTOR
        SSCommand.Parameters.Add("@V_CAT_CRG_DESCRIPCIONOPERADOR", SqlDbType.NVarChar).Value = V_CAT_DIS_DESCRIPCIONOPERADOR
        SSCommand.Parameters.Add("@v_orden", SqlDbType.NVarChar).Value = v_orden
        SSCommand.Parameters.Add("@v_CAT_CRG_TIPODATO", SqlDbType.NVarChar).Value = v_tipo
        SSCommand.Parameters.Add("@v_CAT_CRG_USUARIOS", SqlDbType.NVarChar).Value = ""

        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 16

        Dim DtsDatos As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return DtsDatos
    End Function

    Public Shared Function BorrarParametro(ByVal V_Instancia As String, ByVal V_CAT_DIS_DESCRIPCIONOPERADOR As String, ByVal V_CAT_DIS_DESCRIPCIONCONECTOR As String, ByVal V_CAT_DIS_DESCRIPCIONTABLA As String, ByVal V_CAT_DIS_DESCRIPCIONCAMPO As String, ByVal V_Cat_DIS_Valor As String) As Object

        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_CATALOGO_CONFIG_RGLOBALES"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_CAT_CRG_GLOBAL", SqlDbType.NVarChar).Value = V_Instancia
        SSCommand.Parameters.Add("@V_CAT_CRG_DESCRIPCIONOPERADOR", SqlDbType.NVarChar).Value = V_CAT_DIS_DESCRIPCIONOPERADOR
        SSCommand.Parameters.Add("@V_CAT_CRG_DESCRIPCIONCONECTOR", SqlDbType.NVarChar).Value = V_CAT_DIS_DESCRIPCIONCONECTOR
        SSCommand.Parameters.Add("@V_CAT_CRG_DESCRIPCIONTABLA", SqlDbType.NVarChar).Value = V_CAT_DIS_DESCRIPCIONTABLA
        SSCommand.Parameters.Add("@V_CAT_CRG_DESCRIPCIONCAMPO", SqlDbType.NVarChar).Value = V_CAT_DIS_DESCRIPCIONCAMPO
        SSCommand.Parameters.Add("@V_CAT_CRG_VALOR", SqlDbType.NVarChar).Value = V_Cat_DIS_Valor
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 6

        Dim DtsDatos As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return DtsDatos
    End Function

    Public Shared Function ExisteInstancia(ByVal V_Instancia As String) As Boolean
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_CATALOGO_CONFIG_RGLOBALES"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Instancia", SqlDbType.NVarChar).Value = V_Instancia
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 1

        Dim DtsDatos As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return Not (DtsDatos.Rows(0)(0).ToString = "0")
    End Function

    Public Shared Function TraeRegla(ByVal V_id As String) As Object
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_CATALOGO_CONFIG_RGLOBALES"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 2
        SSCommand.Parameters.Add("@V_CAT_CRG_GLOBAL", SqlDbType.NVarChar).Value = V_id

        Dim DtsDatos As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return DtsDatos
    End Function

    Public Shared Function DatosComboBox(ByVal v_bandera As String, Optional v_aux As String = "") As Object
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_CATALOGO_CONFIG_RGLOBALES"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_bandera", SqlDbType.NVarChar).Value = v_bandera
        SSCommand.Parameters.Add("@v_aux", SqlDbType.NVarChar).Value = v_aux

        Dim DtsDatos As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return DtsDatos
    End Function


    Public Shared Function SimularRegla(ByVal v_bandera As String, ByVal V_CAT_CRG_VALOR As String) As DataTable
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_CATALOGO_CONFIG_RGLOBALES"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 15
        SSCommand.Parameters.Add("@V_CAT_CRG_GLOBAL", SqlDbType.NVarChar).Value = v_bandera
        SSCommand.Parameters.Add("@V_CAT_CRG_VALOR", SqlDbType.NVarChar).Value = V_CAT_CRG_VALOR

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

End Class
