Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports Db
Imports System.Globalization
Imports System.IO
Imports Funciones
Imports System.Web.SessionState.HttpSessionState
Imports System.Data

Public Class Class_CatalogoDispersion
    Public Shared Function InsertarDispersion(ByVal V_Instancia As String, ByVal V_SubClasificacion As String, V_respgestion As String, ByVal V_Dispersion As Integer) As Object
        Dim oraCommand As New sqlcommand
        oraCommand.CommandText = "SP_CATALOGO_DISPERSION"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_Instancia", SqlDbType.VarChar).Value = V_Instancia
        oraCommand.Parameters.Add("V_SubClasificacion", SqlDbType.VarChar).Value = V_SubClasificacion
        oraCommand.Parameters.Add("V_respgestion", SqlDbType.VarChar).Value = V_respgestion
        oraCommand.Parameters.Add("V_Dispersion", SqlDbType.VarChar).Value = V_Dispersion
        oraCommand.Parameters.Add("V_Bandera", SqlDbType.VarChar).Value = 4
        Dim DtsDatos As DataTable = Consulta_Procedure(oraCommand, "ELEMENTOS")
        Return DtsDatos
    End Function

    Public Shared Function BorrarDispersion(ByVal V_Instancia As String, ByVal V_SubClasificacion As String, V_respgestion As String) As Object
        Dim oraCommand As New sqlcommand
        oraCommand.CommandText = "SP_CATALOGO_DISPERSION"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_Instancia", SqlDbType.VarChar).Value = V_Instancia
        oraCommand.Parameters.Add("V_SubClasificacion", SqlDbType.VarChar).Value = V_SubClasificacion
        oraCommand.Parameters.Add("V_respgestion", SqlDbType.VarChar).Value = V_respgestion
        oraCommand.Parameters.Add("V_Bandera", SqlDbType.VarChar).Value = 5

        Dim DtsDatos As DataTable = Consulta_Procedure(oraCommand, "ELEMENTOS")
        Return DtsDatos
    End Function
    Public Shared Function InsertarParametro(ByVal V_Instancia As String, ByVal V_SubClasificacion As String, V_Interno As String, ByVal V_Dispersion As String, ByVal V_CAT_DIS_DESCRIPCIONOPERADOR As String, ByVal V_CAT_DIS_DESCRIPCIONCONECTOR As String, ByVal V_CAT_DIS_DESCRIPCIONTABLA As String, ByVal V_CAT_DIS_DESCRIPCIONCAMPO As String, ByVal V_Cat_DIS_Valor As String, ByVal V_Cat_DIS_Campo As String, ByVal V_CAT_DIS_TABLA As String, ByVal V_Cat_DIS_Operador As String, ByVal V_Cat_DIS_Conector As String, v_tipo As String, v_id As String) As Object
        Dim oraCommand As New SqlCommand
        oraCommand.CommandText = "SP_CATALOGO_DISPERSION"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_Instancia", SqlDbType.VarChar).Value = V_Instancia
        oraCommand.Parameters.Add("V_Dispersion", SqlDbType.VarChar).Value = V_Dispersion
        oraCommand.Parameters.Add("V_SubClasificacion", SqlDbType.VarChar).Value = V_SubClasificacion
        oraCommand.Parameters.Add("v_internoexterno", SqlDbType.VarChar).Value = V_Interno
        oraCommand.Parameters.Add("V_CAT_DIS_DESCRIPCIONOPERADOR", SqlDbType.VarChar).Value = V_CAT_DIS_DESCRIPCIONOPERADOR
        oraCommand.Parameters.Add("V_CAT_DIS_DESCRIPCIONCONECTOR", SqlDbType.VarChar).Value = V_CAT_DIS_DESCRIPCIONCONECTOR
        oraCommand.Parameters.Add("V_CAT_DIS_DESCRIPCIONTABLA", SqlDbType.VarChar).Value = V_CAT_DIS_DESCRIPCIONTABLA
        oraCommand.Parameters.Add("V_CAT_DIS_DESCRIPCIONCAMPO", SqlDbType.VarChar).Value = V_CAT_DIS_DESCRIPCIONCAMPO
        oraCommand.Parameters.Add("V_Cat_DIS_Valor", SqlDbType.VarChar).Value = V_Cat_DIS_Valor
        oraCommand.Parameters.Add("V_Cat_DIS_Campo", SqlDbType.VarChar).Value = V_Cat_DIS_Campo
        oraCommand.Parameters.Add("V_CAT_DIS_TABLA", SqlDbType.VarChar).Value = V_CAT_DIS_TABLA
        oraCommand.Parameters.Add("V_Cat_DIS_Operador", SqlDbType.VarChar).Value = V_Cat_DIS_Operador
        oraCommand.Parameters.Add("V_Cat_DIS_Conector", SqlDbType.VarChar).Value = V_Cat_DIS_Conector
        oraCommand.Parameters.Add("v_tipo", SqlDbType.VarChar).Value = v_tipo
        oraCommand.Parameters.Add("V_CAT_DIS_ID", SqlDbType.Int).Value = v_id
        oraCommand.Parameters.Add("V_Bandera", SqlDbType.VarChar).Value = 3

        Dim DtsDatos As DataTable = Consulta_Procedure(oraCommand, "ELEMENTOS")
        Return DtsDatos
    End Function

    Public Shared Function ActualizarParametro(ByVal V_CAT_DIS_DESCRIPCIONOPERADOR As String, ByVal V_CAT_DIS_DESCRIPCIONCONECTOR As String, ByVal V_CAT_DIS_DESCRIPCIONTABLA As String, ByVal V_CAT_DIS_DESCRIPCIONCAMPO As String, ByVal V_Cat_DIS_Valor As String, ByVal V_Cat_DIS_Campo As String, ByVal V_CAT_DIS_TABLA As String, ByVal V_Cat_DIS_Operador As String, ByVal V_Cat_DIS_Conector As String, v_tipo As String, v_id As String, v_orden As String) As Object
        Dim oraCommand As New SqlCommand
        oraCommand.CommandText = "SP_CATALOGO_DISPERSION"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_CAT_DIS_DESCRIPCIONOPERADOR", SqlDbType.VarChar).Value = V_CAT_DIS_DESCRIPCIONOPERADOR
        oraCommand.Parameters.Add("V_CAT_DIS_DESCRIPCIONCONECTOR", SqlDbType.VarChar).Value = V_CAT_DIS_DESCRIPCIONCONECTOR
        oraCommand.Parameters.Add("V_CAT_DIS_DESCRIPCIONTABLA", SqlDbType.VarChar).Value = V_CAT_DIS_DESCRIPCIONTABLA
        oraCommand.Parameters.Add("V_CAT_DIS_DESCRIPCIONCAMPO", SqlDbType.VarChar).Value = V_CAT_DIS_DESCRIPCIONCAMPO
        oraCommand.Parameters.Add("V_Cat_DIS_Valor", SqlDbType.VarChar).Value = V_Cat_DIS_Valor
        oraCommand.Parameters.Add("V_Cat_DIS_Campo", SqlDbType.VarChar).Value = V_Cat_DIS_Campo
        oraCommand.Parameters.Add("V_CAT_DIS_TABLA", SqlDbType.VarChar).Value = V_CAT_DIS_TABLA
        oraCommand.Parameters.Add("V_Cat_DIS_Operador", SqlDbType.VarChar).Value = V_Cat_DIS_Operador
        oraCommand.Parameters.Add("V_Cat_DIS_Conector", SqlDbType.VarChar).Value = V_Cat_DIS_Conector
        oraCommand.Parameters.Add("v_tipo", SqlDbType.VarChar).Value = v_tipo
        oraCommand.Parameters.Add("V_CAT_DIS_ID", SqlDbType.Int).Value = v_id
        oraCommand.Parameters.Add("V_cat_dis_orden", SqlDbType.Int).Value = v_orden
        oraCommand.Parameters.Add("V_Bandera", SqlDbType.VarChar).Value = 18

        Dim DtsDatos As DataTable = Consulta_Procedure(oraCommand, "ELEMENTOS")
        Return DtsDatos
    End Function

    Public Shared Function BorrarParametro(ByVal V_Instancia As String, ByVal V_SubClasificacion As String, V_respgestion As String, ByVal V_CAT_DIS_DESCRIPCIONOPERADOR As String, ByVal V_CAT_DIS_DESCRIPCIONCONECTOR As String, ByVal V_CAT_DIS_DESCRIPCIONTABLA As String, ByVal V_CAT_DIS_DESCRIPCIONCAMPO As String, ByVal V_Cat_DIS_Valor As String, ByVal v_cat_DIS_ID As String) As Object
        Dim oraCommand As New SqlCommand
        oraCommand.CommandText = "SP_CATALOGO_DISPERSION"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_Instancia", SqlDbType.VarChar).Value = V_Instancia
        oraCommand.Parameters.Add("V_SubClasificacion", SqlDbType.VarChar).Value = V_SubClasificacion
        oraCommand.Parameters.Add("V_respgestion", SqlDbType.VarChar).Value = V_respgestion
        oraCommand.Parameters.Add("V_CAT_DIS_DESCRIPCIONOPERADOR", SqlDbType.VarChar).Value = V_CAT_DIS_DESCRIPCIONOPERADOR
        oraCommand.Parameters.Add("V_CAT_DIS_DESCRIPCIONCONECTOR", SqlDbType.VarChar).Value = V_CAT_DIS_DESCRIPCIONCONECTOR
        oraCommand.Parameters.Add("V_CAT_DIS_DESCRIPCIONTABLA", SqlDbType.VarChar).Value = V_CAT_DIS_DESCRIPCIONTABLA
        oraCommand.Parameters.Add("V_CAT_DIS_DESCRIPCIONCAMPO", SqlDbType.VarChar).Value = V_CAT_DIS_DESCRIPCIONCAMPO
        oraCommand.Parameters.Add("V_Cat_DIS_Valor", SqlDbType.VarChar).Value = V_Cat_DIS_Valor
        oraCommand.Parameters.Add("V_CAT_DIS_ID", SqlDbType.VarChar).Value = v_cat_DIS_ID
        oraCommand.Parameters.Add("V_Bandera", SqlDbType.VarChar).Value = 6

        Dim DtsDatos As DataTable = Consulta_Procedure(oraCommand, "ELEMENTOS")
        Return DtsDatos
    End Function

    Public Shared Function BorrarDispersion(ByVal v_cat_DIS_ID As String) As Object
        Dim oraCommand As New SqlCommand
        oraCommand.CommandText = "SP_CATALOGO_DISPERSION"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_CAT_DIS_ID", SqlDbType.VarChar).Value = v_cat_DIS_ID
        oraCommand.Parameters.Add("V_Bandera", SqlDbType.VarChar).Value = 16

        Dim DtsDatos As DataTable = Consulta_Procedure(oraCommand, "ELEMENTOS")
        Return DtsDatos
    End Function

    Public Shared Function ExisteDispersion(ByVal V_Instancia As String, ByVal V_SubClasificacion As String, V_respgestion As String, ByVal V_Dispersion As String) As Boolean
        Dim oraCommand As New SqlCommand
        oraCommand.CommandText = "SP_CATALOGO_DISPERSION"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_Instancia", SqlDbType.VarChar).Value = V_Instancia
        oraCommand.Parameters.Add("V_SubClasificacion", SqlDbType.VarChar).Value = V_SubClasificacion
        oraCommand.Parameters.Add("V_respgestion", SqlDbType.VarChar).Value = V_respgestion
        oraCommand.Parameters.Add("V_Dispersion", SqlDbType.VarChar).Value = V_Dispersion
        oraCommand.Parameters.Add("V_Bandera", SqlDbType.VarChar).Value = 1

        Dim DtsDatos As DataTable = Consulta_Procedure(oraCommand, "ELEMENTOS")
        Return Not (DtsDatos.Rows(0)(0).ToString = "0")
    End Function

    Public Shared Function TraeDispersion(ByVal V_id As String) As Object
        Dim oraCommand As New SqlCommand
        oraCommand.CommandText = "SP_CATALOGO_DISPERSION"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_CAT_DIS_ID", SqlDbType.Int).Value = V_id
        oraCommand.Parameters.Add("V_Bandera", SqlDbType.VarChar).Value = 17

        Dim DtsDatos As DataTable = Consulta_Procedure(oraCommand, "ELEMENTOS")
        Return DtsDatos
    End Function

    Public Shared Function DatosComboBox(ByVal v_bandera As String, Optional v_aux As String = "") As Object
        Dim oraCommand As New SqlCommand
        oraCommand.CommandText = "SP_CATALOGO_DISPERSION"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("v_bandera", SqlDbType.VarChar).Value = v_bandera
        oraCommand.Parameters.Add("v_aux", SqlDbType.VarChar).Value = v_aux

        Dim DtsDatos As DataTable = Consulta_Procedure(oraCommand, "ELEMENTOS")
        Return DtsDatos
    End Function

    Public Shared Function TraerUsuarios(ByVal V_Instancia As String, ByVal v_internoexterno As String) As DataTable
        Dim oraCommand As New SqlCommand
        oraCommand.CommandText = "SP_CATALOGO_DISPERSION"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("v_bandera", SqlDbType.VarChar).Value = 9
        oraCommand.Parameters.Add("V_Instancia", SqlDbType.VarChar).Value = V_Instancia
        oraCommand.Parameters.Add("@v_internoexterno", SqlDbType.VarChar).Value = v_internoexterno
        Dim DtsDatos As DataTable = Consulta_Procedure(oraCommand, oraCommand.CommandText)
        Return DtsDatos
    End Function

    Public Shared Function GuardarUsuarios(ByVal V_Instancia As String, ByVal V_SubClasificacion As String, ByVal v_internoexterno As String, ByVal v_usuarios As String, ByVal v_id As String, ByVal v_usuarios2 As String) As DataTable
        Dim oraCommand As New SqlCommand
        oraCommand.CommandText = "SP_CATALOGO_DISPERSION"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_Instancia", SqlDbType.VarChar).Value = V_Instancia
        oraCommand.Parameters.Add("V_SubClasificacion", SqlDbType.VarChar).Value = V_SubClasificacion
        'oraCommand.Parameters.Add("V_respgestion", SqlDbType.VarChar).Value = V_respgestion
        oraCommand.Parameters.Add("v_usuarios", SqlDbType.NVarChar).Value = v_usuarios
        oraCommand.Parameters.Add("v_usuarios2", SqlDbType.NVarChar).Value = v_usuarios2
        oraCommand.Parameters.Add("v_internoexterno", SqlDbType.NVarChar).Value = v_internoexterno
        oraCommand.Parameters.Add("V_Bandera", SqlDbType.VarChar).Value = 10
        oraCommand.Parameters.Add("V_CAT_DIS_ID", SqlDbType.VarChar).Value = v_id

        Dim DtsDatos As DataTable = Consulta_Procedure(oraCommand, "ELEMENTOS")
        Return DtsDatos
    End Function

    Public Shared Function TraerUsuariosDispersion(ByVal V_Instancia As String, ByVal V_SubClasificacion As String, V_respgestion As String) As DataTable
        Dim oraCommand As New SqlCommand
        oraCommand.CommandText = "SP_CATALOGO_DISPERSION"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_Instancia", SqlDbType.VarChar).Value = V_Instancia
        oraCommand.Parameters.Add("V_SubClasificacion", SqlDbType.VarChar).Value = V_SubClasificacion
        oraCommand.Parameters.Add("V_respgestion", SqlDbType.VarChar).Value = V_respgestion
        oraCommand.Parameters.Add("V_Bandera", SqlDbType.VarChar).Value = 11

        Dim DtsDatos As DataTable = Consulta_Procedure(oraCommand, "ELEMENTOS")
        Return DtsDatos
    End Function

    Public Shared Function SimularAsignacion(ByVal v_id As String, ByVal v_instancia As String) As DataTable
        Dim oraCommand As New SqlCommand
        oraCommand.CommandText = "SP_ASIGNACION"
        oraCommand.CommandType = CommandType.StoredProcedure
        'oraCommand.Parameters.Add("v_usuario", SqlDbType.VarChar).Value = v_usuario
        oraCommand.Parameters.Add("@v_instancia", SqlDbType.VarChar).Value = v_instancia
        oraCommand.Parameters.Add("@V_Bandera", SqlDbType.Int).Value = 0
        oraCommand.Parameters.Add("@v_id", SqlDbType.VarChar).Value = v_id

        Dim DtsDatos As DataTable = Consulta_Procedure(oraCommand, oraCommand.CommandText)
        Return DtsDatos
    End Function

    Public Shared Function AplicarAsignacion(ByVal v_usuario As String) As DataTable
        Dim oraCommand As New SqlCommand
        oraCommand.CommandText = "SP_ASIGNACION"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("v_usuario", SqlDbType.VarChar).Value = v_usuario
        oraCommand.Parameters.Add("V_Bandera", SqlDbType.VarChar).Value = 1

        Dim DtsDatos As DataTable = Consulta_Procedure(oraCommand, "ELEMENTOS")
        Return DtsDatos
    End Function

    Public Shared Function TraerDispersiones(ByVal Instancia As String) As DataTable
        Dim oraCommand As New SqlCommand
        oraCommand.CommandText = "SP_CATALOGO_DISPERSION"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_Bandera", SqlDbType.VarChar).Value = 0
        oraCommand.Parameters.Add("V_Instancia", SqlDbType.VarChar).Value = Instancia

        Dim DtsDatos As DataTable = Consulta_Procedure(oraCommand, "ELEMENTOS")
        Return DtsDatos
    End Function
    Public Shared Function TraeUsuarios(ByVal V_id As String) As Object
        Dim oraCommand As New SqlCommand
        oraCommand.CommandText = "SP_CATALOGO_DISPERSION"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_CAT_DIS_ID", SqlDbType.Int).Value = V_id
        oraCommand.Parameters.Add("V_Bandera", SqlDbType.VarChar).Value = 13

        Dim DtsDatos As DataTable = Consulta_Procedure(oraCommand, "ELEMENTOS")
        Return DtsDatos
    End Function
    Public Shared Function TraeUsuarios2(ByVal V_id As String) As Object
        Dim oraCommand As New SqlCommand
        oraCommand.CommandText = "SP_CATALOGO_DISPERSION"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_CAT_DIS_ID", SqlDbType.Int).Value = V_id
        oraCommand.Parameters.Add("V_Bandera", SqlDbType.VarChar).Value = 25

        Dim DtsDatos As DataTable = Consulta_Procedure(oraCommand, "ELEMENTOS")
        Return DtsDatos
    End Function
    Public Shared Function InsertarDispersiondummy() As String
        Dim oraCommand As New SqlCommand
        oraCommand.CommandText = "SP_CATALOGO_DISPERSION"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_Bandera", SqlDbType.VarChar).Value = 14

        Dim DtsDatos As DataTable = Consulta_Procedure(oraCommand, "ELEMENTOS")
        Return DtsDatos.Rows(0).Item("MAXIMO").ToString
    End Function
    Public Shared Function UsuariosInstancia(ByVal V_instancia As String) As Object
        Dim oraCommand As New SqlCommand
        oraCommand.CommandText = "SP_CATALOGO_DISPERSION"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_Instancia", SqlDbType.VarChar).Value = V_instancia
        oraCommand.Parameters.Add("V_Bandera", SqlDbType.VarChar).Value = 15

        Dim DtsDatos As DataTable = Consulta_Procedure(oraCommand, "ELEMENTOS")
        Return DtsDatos
    End Function
    Public Shared Function CambiarAsignacion(ByVal V_credito As String, ByVal v_usuario As String) As Object
        Dim oraCommand As New SqlCommand
        oraCommand.CommandText = "SP_CAMBIA_ASIGNACION"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("v_credito", SqlDbType.VarChar).Value = V_credito
        oraCommand.Parameters.Add("v_usuario", SqlDbType.VarChar).Value = v_usuario

        Dim DtsDatos As DataTable = Consulta_Procedure(oraCommand, "ELEMENTOS")
        Return DtsDatos
    End Function
    Public Shared Function TraerAsignacion(ByVal v_id As String) As DataTable
        Dim oraCommand As New SqlCommand
        oraCommand.CommandText = "SP_ASIGNACION"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("v_usuario", SqlDbType.VarChar).Value = ""
        oraCommand.Parameters.Add("V_Bandera", SqlDbType.VarChar).Value = 2
        oraCommand.Parameters.Add("v_id", SqlDbType.VarChar).Value = v_id

        Dim DtsDatos As DataTable = Consulta_Procedure(oraCommand, "ELEMENTOS")
        Return DtsDatos
    End Function
    Public Shared Function ValidaCatalogo(ByVal V_Campo As String) As DataTable
        Dim oraCommand As New SqlCommand
        oraCommand.CommandText = "SP_VALIDA_CATALOGO"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_Campo", SqlDbType.VarChar).Value = V_Campo
        Dim DtsDatos As DataTable = Consulta_Procedure(oraCommand, "ELEMENTOS")
        Return DtsDatos
    End Function
    Public Shared Function TraerAsignacionparaWS() As DataTable
        Dim oraCommand As New SqlCommand
        oraCommand.CommandText = "SP_ASIGNACION"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("v_usuario", SqlDbType.VarChar).Value = ""
        oraCommand.Parameters.Add("V_Bandera", SqlDbType.VarChar).Value = 3
        oraCommand.Parameters.Add("v_id", SqlDbType.VarChar).Value = ""

        Dim DtsDatos As DataTable = Consulta_Procedure(oraCommand, "ELEMENTOS")
        Return DtsDatos
    End Function
End Class
