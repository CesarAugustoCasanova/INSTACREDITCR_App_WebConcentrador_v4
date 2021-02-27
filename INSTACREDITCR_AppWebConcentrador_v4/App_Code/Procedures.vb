Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports Db
'Imports System.Data.Odbc

Public Class SP
    Public Shared Function ACTUALIZA_UIDS(v_valor As String) As DataTable
        Dim SSCommand As New SqlCommand("SP_ACTUALIZA_UDIS")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_valor", SqlDbType.NVarChar).Value = v_valor
        Return Consulta_Procedure(SSCommand, "Filtros")
    End Function

    Public Shared Function CARGA_DIASDEBITO(bandera As Integer, dt As DataTable) As DataTable
        Dim SSCommand2 As New SqlCommand("SP_CARGA_DIASDEBITO")
        SSCommand2.CommandType = CommandType.StoredProcedure
        SSCommand2.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = bandera
        SSCommand2.Parameters.Add(New SqlParameter("@DT_CARGAS_DIASDEBITO", dt))
        Return Consulta_Procedure(SSCommand2, "SP_CARGA_DIASDEBITO")
    End Function

    Public Shared Function CARGA_DIASDEBITO(bandera As Integer) As DataTable
        Dim SSCommand2 As New SqlCommand("SP_CARGA_DIASDEBITO")
        SSCommand2.CommandType = CommandType.StoredProcedure
        SSCommand2.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = bandera
        Return Consulta_Procedure(SSCommand2, "SP_CARGA_DIASDEBITO")
    End Function
    Public Shared Function ADD_ARCHIVO(valores As IDictionary) As DataTable
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_RP_MONITOREO_V2"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_valor", SqlDbType.NVarChar).Value = valores("v_valor")
        Return Consulta_Procedure(SSCommand, "Filtros")
    End Function
    Public Shared Function CATALOGO_APLICACIONES(V_BANDERA As Integer, Optional V_ID As Integer = Nothing, Optional V_ICONO As Byte() = Nothing, Optional V_NOMBRE As String = Nothing, Optional V_PAQUETE As String = Nothing, Optional V_TIPO As Integer = Nothing, Optional V_GRUPO As Integer = Nothing) As DataTable
        Dim SSCommand As New SqlCommand("SP_CATALOGO_APLICACIONES")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = V_BANDERA
        SSCommand.Parameters.Add("@V_ID", SqlDbType.Int).Value = V_ID
        SSCommand.Parameters.Add("@V_ICONO", SqlDbType.VarBinary).Value = V_ICONO
        SSCommand.Parameters.Add("@V_NOMBRE", SqlDbType.VarChar).Value = V_NOMBRE
        SSCommand.Parameters.Add("@V_PAQUETE", SqlDbType.VarChar).Value = V_PAQUETE
        SSCommand.Parameters.Add("@V_TIPO", SqlDbType.Int).Value = V_TIPO
        SSCommand.Parameters.Add("@V_GRUPO", SqlDbType.Int).Value = V_GRUPO
        Return Consulta_Procedure(SSCommand, "SP_CATALOGO_APLICACIONES")
    End Function
    Public Shared Function ADD_AVISOS(V_BANDERA As String, V_HIST_AV_DTEEXPIRA As String, V_HIST_AV_ESTATUS As String, V_HIST_AV_MENSAJE As String, V_HIST_AV_PRIORIDAD As String, V_HIST_AV_EMISOR As String, V_HIST_AV_RECEPTOR As String) As DataTable
        Dim SSCommand As New SqlCommand("SP_ADD_AVISOS")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_HIST_AV_DTEEXPIRA", SqlDbType.NVarChar).Value = V_HIST_AV_DTEEXPIRA
        SSCommand.Parameters.Add("@V_HIST_AV_ESTATUS", SqlDbType.NVarChar).Value = V_HIST_AV_ESTATUS
        SSCommand.Parameters.Add("@V_HIST_AV_MENSAJE", SqlDbType.NVarChar).Value = V_HIST_AV_MENSAJE
        SSCommand.Parameters.Add("@V_HIST_AV_PRIORIDAD", SqlDbType.NVarChar).Value = V_HIST_AV_PRIORIDAD
        SSCommand.Parameters.Add("@V_HIST_AV_EMISOR", SqlDbType.NVarChar).Value = V_HIST_AV_EMISOR
        SSCommand.Parameters.Add("@V_HIST_AV_RECEPTOR", SqlDbType.NVarChar).Value = V_HIST_AV_RECEPTOR
        SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.NVarChar).Value = V_BANDERA
        Return Consulta_Procedure(SSCommand, "Filtros")
    End Function

    Public Shared Function ADD_CAT_AGENCIAS(valores As IDictionary) As DataTable
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_RP_MONITOREO_V2"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_valor", SqlDbType.NVarChar).Value = valores("v_valor")
        Return Consulta_Procedure(SSCommand, "Filtros")
    End Function

    Public Shared Function ADD_CAT_BLOQUEO_MEDIOS(valores As IDictionary) As DataTable
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_RP_MONITOREO_V2"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_valor", SqlDbType.NVarChar).Value = valores("v_valor")
        Return Consulta_Procedure(SSCommand, "Filtros")
    End Function

    Public Shared Function ADD_CAT_REPORTE_DETALLE(valores As IDictionary) As DataTable
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_RP_MONITOREO_V2"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_valor", SqlDbType.NVarChar).Value = valores("v_valor")
        Return Consulta_Procedure(SSCommand, "Filtros")
    End Function

    Public Shared Function ADD_CAT_REPORTE_GRAFICA(valores As IDictionary) As DataTable
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_RP_MONITOREO_V2"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_valor", SqlDbType.NVarChar).Value = valores("v_valor")
        Return Consulta_Procedure(SSCommand, "Filtros")
    End Function

    Public Shared Function ADD_CATALOGOS(valores As IDictionary) As DataTable
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_RP_MONITOREO_V2"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_valor", SqlDbType.NVarChar).Value = valores("v_valor")
        Return Consulta_Procedure(SSCommand, "Filtros")
    End Function

    Public Shared Function ADD_CODIGOS(V_Cat_Co_Id As Integer, V_Cat_Co_Accion As String, V_Cat_Co_Adescripcion As String, V_Cat_Co_Resultado As String,
        V_Cat_Co_Rdescripcion As String, V_Cat_Co_Significativo As Integer,
        V_Cat_Co_Perfiles As String, V_Cat_Co_Ponderacion As String, V_Cat_Co_Producto As String, V_Cat_Co_Configuracion As Integer,
        V_Cat_Co_Verde As Integer, V_Cat_Co_Amarillo As Integer, V_Cat_Co_Tipo As Integer, ByVal V_Cat_Co_NoPago As String, ByVal V_Cat_Co_Atencion_A As Integer, V_Bandera As Integer, Optional ByVal V_Cat_Co_Instancia As String = "", Optional ByVal V_Cat_Co_R1 As String = "", Optional ByVal V_Cat_Co_R2 As String = "", Optional ByVal V_Cat_Co_R3 As String = "") As DataTable
        Dim SSCommandCodigos As New SqlCommand
        SSCommandCodigos.CommandText = "Sp_Add_Codigos"
        SSCommandCodigos.CommandType = CommandType.StoredProcedure
        SSCommandCodigos.Parameters.Add("@V_Cat_Co_Id", SqlDbType.Decimal).Value = V_Cat_Co_Id
        SSCommandCodigos.Parameters.Add("@V_Cat_Co_Accion", SqlDbType.NVarChar).Value = V_Cat_Co_Accion
        SSCommandCodigos.Parameters.Add("@V_Cat_Co_Adescripcion", SqlDbType.NVarChar).Value = HttpUtility.HtmlDecode(V_Cat_Co_Adescripcion)
        SSCommandCodigos.Parameters.Add("@V_Cat_Co_Resultado", SqlDbType.NVarChar).Value = V_Cat_Co_Resultado
        SSCommandCodigos.Parameters.Add("@V_Cat_Co_Rdescripcion", SqlDbType.NVarChar).Value = HttpUtility.HtmlDecode(V_Cat_Co_Rdescripcion)
        SSCommandCodigos.Parameters.Add("@V_Cat_Co_Significativo", SqlDbType.Decimal).Value = V_Cat_Co_Significativo
        SSCommandCodigos.Parameters.Add("@V_Cat_Co_Perfiles", SqlDbType.NVarChar).Value = V_Cat_Co_Perfiles
        SSCommandCodigos.Parameters.Add("@V_Cat_Co_Ponderacion", SqlDbType.NVarChar).Value = V_Cat_Co_Ponderacion
        SSCommandCodigos.Parameters.Add("@V_Cat_Co_Producto", SqlDbType.NVarChar).Value = V_Cat_Co_Producto
        SSCommandCodigos.Parameters.Add("@V_Cat_Co_Configuracion", SqlDbType.Decimal).Value = V_Cat_Co_Configuracion
        SSCommandCodigos.Parameters.Add("@V_Cat_Co_Verde", SqlDbType.Decimal).Value = V_Cat_Co_Verde
        SSCommandCodigos.Parameters.Add("@V_Cat_Co_Amarillo", SqlDbType.Decimal).Value = V_Cat_Co_Amarillo
        SSCommandCodigos.Parameters.Add("@V_Cat_Co_Tipo", SqlDbType.Decimal).Value = V_Cat_Co_Tipo
        SSCommandCodigos.Parameters.Add("@V_Cat_Co_NoPago", SqlDbType.NVarChar).Value = V_Cat_Co_NoPago
        SSCommandCodigos.Parameters.Add("@V_Cat_Co_Atencion_A", SqlDbType.Decimal).Value = V_Cat_Co_Atencion_A
        ' SSCommandCodigos.Parameters.Add("@V_Cat_Co_Instancia", SqlDbType.NVarChar).Value = V_Cat_Co_Instancia
        SSCommandCodigos.Parameters.Add("@V_Cat_Co_R1", SqlDbType.NVarChar).Value = V_Cat_Co_R1
        'SSCommandCodigos.Parameters.Add("@V_Cat_Co_R2", SqlDbType.NVarChar).Value = V_Cat_Co_R2
        'SSCommandCodigos.Parameters.Add("@V_Cat_Co_R3", SqlDbType.NVarChar).Value = V_Cat_Co_R3
        SSCommandCodigos.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = V_Bandera
        Return Consulta_Procedure(SSCommandCodigos, "Codigos")
    End Function

    Public Shared Function ADD_ETIQUETAS_CORREO(valores As IDictionary) As DataTable
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_RP_MONITOREO_V2"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_valor", SqlDbType.NVarChar).Value = valores("v_valor")
        Return Consulta_Procedure(SSCommand, "Filtros")
    End Function

    Public Shared Function ADD_HIST_CORREO(valores As IDictionary) As DataTable
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_RP_MONITOREO_V2"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_valor", SqlDbType.NVarChar).Value = valores("v_valor")
        Return Consulta_Procedure(SSCommand, "Filtros")
    End Function

    Public Shared Function ADD_HIST_DIRECCIONES(valores As IDictionary) As DataTable
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_RP_MONITOREO_V2"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_valor", SqlDbType.NVarChar).Value = valores("v_valor")
        Return Consulta_Procedure(SSCommand, "Filtros")
    End Function

    Public Shared Function ADD_HIST_GESTIONES(valores As IDictionary) As DataTable
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_RP_MONITOREO_V2"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_valor", SqlDbType.NVarChar).Value = valores("v_valor")
        Return Consulta_Procedure(SSCommand, "Filtros")
    End Function

    Public Shared Function ADD_HIST_PAGOS(valores As IDictionary) As DataTable
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_RP_MONITOREO_V2"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_valor", SqlDbType.NVarChar).Value = valores("v_valor")
        Return Consulta_Procedure(SSCommand, "Filtros")
    End Function

    Public Shared Function ADD_HIST_PROMESAS(valores As IDictionary) As DataTable
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_RP_MONITOREO_V2"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_valor", SqlDbType.NVarChar).Value = valores("v_valor")
        Return Consulta_Procedure(SSCommand, "Filtros")
    End Function

    Public Shared Function ADD_HIST_TELEFONOS(valores As IDictionary) As DataTable
        Dim SSCommand As New SqlCommand("SP_RP_MONITOREO_V2")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_valor", SqlDbType.NVarChar).Value = valores("v_valor")
        Return Consulta_Procedure(SSCommand, "Filtros")
    End Function

    Public Shared Function ADD_LOGINS(valores As IDictionary) As DataTable
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_RP_MONITOREO_V2"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_valor", SqlDbType.NVarChar).Value = valores("v_valor")
        Return Consulta_Procedure(SSCommand, "Filtros")
    End Function

    Public Shared Function ADD_NEGOCIACIONES(valores As IDictionary) As DataTable
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_RP_MONITOREO_V2"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_valor", SqlDbType.NVarChar).Value = valores("v_valor")
        Return Consulta_Procedure(SSCommand, "Filtros")
    End Function

    Public Shared Function INFOUSUARIOS(ByVal V_Usuario As String, ByVal V_Condicion As String, ByVal V_Bandera As String) As DataTable
        Dim SSCOMMAND As New SqlCommand("SP_INFOUSUARIOS")
        SSCOMMAND.CommandType = CommandType.StoredProcedure
        SSCOMMAND.Parameters.Add("@V_Usuario", SqlDbType.NVarChar).Value = V_Usuario
        SSCOMMAND.Parameters.Add("@V_Condicion", SqlDbType.NVarChar).Value = V_Condicion
        SSCOMMAND.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = V_Bandera
        Dim DtsUsuarios As DataTable = Consulta_Procedure(SSCOMMAND, "ELEMENTOS")
        Return DtsUsuarios
    End Function
    Public Shared Function MONITOREO(ByVal V_Usuario As String, ByVal V_Sucursales As String, ByVal V_Bandera As Integer) As DataTable
        Dim SSCOMMAND As New SqlCommand("SP_MONITOREO")
        SSCOMMAND.CommandType = CommandType.StoredProcedure
        SSCOMMAND.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = V_Bandera
        SSCOMMAND.Parameters.Add("@v_sucursales", SqlDbType.NVarChar).Value = V_Sucursales
        SSCOMMAND.Parameters.Add("@V_Usuario", SqlDbType.NVarChar).Value = V_Usuario
        Dim DtsUsuarios As DataTable = Consulta_Procedure(SSCOMMAND, "ELEMENTOS")
        Return DtsUsuarios
    End Function

    Public Shared Function RP_MONITOREO_V2(ByVal V_agrupacion As String, ByVal V_tipo As String, ByVal v_filtro As String, ByVal v_Tipotope As String, ByVal V_Bandera As Integer) As DataTable
        Dim cadenacomas As String = "'"

        If V_tipo = "1" Then
            V_agrupacion = "'" & V_agrupacion & "'"
            V_agrupacion = V_agrupacion.Replace(",", "','")
        End If

        v_filtro = "'" & v_filtro & "'"
        v_filtro = v_filtro.Replace(",", "','")

        Dim SSCOMMAND As New SqlCommand("SP_RP_MONITOREO_V2")
        SSCOMMAND.CommandType = CommandType.StoredProcedure
        SSCOMMAND.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = V_Bandera
        SSCOMMAND.Parameters.Add("@v_dato1", SqlDbType.NVarChar).Value = V_agrupacion
        SSCOMMAND.Parameters.Add("@v_dato2", SqlDbType.NVarChar).Value = V_tipo
        SSCOMMAND.Parameters.Add("@v_dato3", SqlDbType.NVarChar).Value = v_filtro
        SSCOMMAND.Parameters.Add("@v_condiciones", SqlDbType.NVarChar).Value = v_Tipotope
        Dim DtsUsuarios As DataTable = Consulta_Procedure(SSCOMMAND, "ELEMENTOS")
        Return DtsUsuarios
    End Function

    Public Shared Function RP_SOLICITUDINVCAMPO(ByVal V_Bandera As Integer, Optional V_Usuario As String = "", Optional V_Agencia As String = "", Optional V_condiciones As String = "") As DataTable
        Dim SSCommand As New SqlCommand("SP_RP_SOLICITUDINVCAMPO")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_bandera", SqlDbType.Decimal).Value = V_Bandera
        SSCommand.Parameters.Add("@v_usuario", SqlDbType.NVarChar).Value = V_Usuario
        SSCommand.Parameters.Add("@v_agencia", SqlDbType.NVarChar).Value = V_Agencia
        SSCommand.Parameters.Add("@v_condiciones", SqlDbType.NVarChar).Value = V_condiciones
        Dim DtsInvCampo As DataTable = Consulta_Procedure(SSCommand, "Catalogo")
        Return DtsInvCampo
    End Function

    Public Shared Function PERMISOS(v_usuario As String, v_contrasena As String, v_modulo As String) As DataTable
        Dim SSCommand As New SqlCommand("SP_PERMISOS")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_USUARIO", SqlDbType.NVarChar).Value = v_usuario
        SSCommand.Parameters.Add("@V_CONTRASENA", SqlDbType.NVarChar).Value = v_contrasena
        SSCommand.Parameters.Add("@V_MODULO", SqlDbType.NVarChar).Value = v_modulo
        Dim DtsPermiso As DataTable = Consulta_Procedure(SSCommand, "LogOn")
        Return DtsPermiso
    End Function

    Public Shared Function USUARIO(v_bandera As String, v_usuario As String, v_contrasena As String, v_modulo As String, Optional v_imei As String = "000") As DataTable
        Dim SSCommandL As New SqlCommand("SP_USUARIO")
        SSCommandL.CommandType = CommandType.StoredProcedure
        SSCommandL.Parameters.Add("@V_USUARIO", SqlDbType.NVarChar).Value = v_usuario
        SSCommandL.Parameters.Add("@V_CONTRASENA", SqlDbType.NVarChar).Value = v_contrasena
        SSCommandL.Parameters.Add("@V_imei", SqlDbType.NVarChar).Value = v_imei
        SSCommandL.Parameters.Add("@V_MODULO", SqlDbType.NVarChar).Value = v_modulo
        SSCommandL.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = v_bandera
        Dim DtsLogin As DataTable = Consulta_Procedure(SSCommandL, "LogOn")
        Return DtsLogin
    End Function

    Public Shared Function AUDITORIA_GLOBAL(v_bandera As String, ByVal quien As String, ByVal donde As String, ByVal que As String) As DataTable
        Dim SSCommand As New SqlCommand("SP_AUDITORIA_GLOBAL")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_quien", SqlDbType.NVarChar).Value = quien
        SSCommand.Parameters.Add("@v_donde", SqlDbType.NVarChar).Value = donde
        SSCommand.Parameters.Add("@v_que", SqlDbType.NVarChar).Value = que
        SSCommand.Parameters.Add("@v_bandera", SqlDbType.NVarChar).Value = 0
        Return Consulta_Procedure(SSCommand, "ELEMENTOS")
    End Function

    Public Shared Function INGRESO(v_bandera As String, ByVal v_usuario As String, ByVal v_ip As String, ByVal v_modulo As String) As DataTable
        Dim SSCommand As New SqlCommand("SP_INGRESO")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_USUARIO", SqlDbType.NVarChar).Value = v_usuario
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = v_bandera
        SSCommand.Parameters.Add("@V_IP", SqlDbType.NVarChar).Value = v_ip
        SSCommand.Parameters.Add("@V_MODULO", SqlDbType.NVarChar).Value = v_modulo
        Return Consulta_Procedure(SSCommand, "ELEMENTOS")
    End Function

    Public Shared Function GUARDAR_CONTRASENA(ByVal v_usuario As String, ByVal v_contrasena As String) As DataTable
        Dim SSCommand As New SqlCommand("SP_GUARDAR_CONTRASENA")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_LUSUARIO", SqlDbType.NVarChar).Value = v_usuario
        SSCommand.Parameters.Add("@v_CONTRASENA", SqlDbType.NVarChar).Value = v_contrasena
        Return Consulta_Procedure(SSCommand, "ELEMENTOS")
    End Function
    Public Shared Function ADD_PERFILES(V_BANDERA As Integer, V_CAT_PE_PERFIL As String, V_CAT_PE_A_GESTION As String, V_CAT_PE_P_GESTION As String, V_CAT_PE_P_ADMINISTRADOR As String, V_CAT_PE_P_BACKOFFICE As String, V_CAT_PE_P_REPORTES As String, V_CAT_PE_P_MOVIL As String, V_CAT_PE_P_JUDICIAL As String, V_CAT_PE_ID As Integer, V_MODULO As Integer) As DataTable
        Dim SSCommandCat As New SqlCommand
        SSCommandCat.CommandText = "SP_ADD_PERFILES"
        SSCommandCat.CommandType = CommandType.StoredProcedure
        SSCommandCat.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = V_BANDERA
        SSCommandCat.Parameters.Add("@V_CAT_PE_PERFIL", SqlDbType.NVarChar).Value = V_CAT_PE_PERFIL
        SSCommandCat.Parameters.Add("@V_CAT_PE_A_GESTION", SqlDbType.NVarChar).Value = V_CAT_PE_A_GESTION
        SSCommandCat.Parameters.Add("@V_CAT_PE_P_GESTION", SqlDbType.NVarChar).Value = V_CAT_PE_P_GESTION
        SSCommandCat.Parameters.Add("@V_CAT_PE_P_ADMINISTRADOR", SqlDbType.NVarChar).Value = V_CAT_PE_P_ADMINISTRADOR
        SSCommandCat.Parameters.Add("@V_CAT_PE_P_BACKOFFICE", SqlDbType.NVarChar).Value = V_CAT_PE_P_BACKOFFICE
        SSCommandCat.Parameters.Add("@V_CAT_PE_P_REPORTES", SqlDbType.NVarChar).Value = V_CAT_PE_P_REPORTES
        SSCommandCat.Parameters.Add("@V_CAT_PE_P_MOVIL", SqlDbType.NVarChar).Value = V_CAT_PE_P_MOVIL
        SSCommandCat.Parameters.Add("@V_CAT_PE_P_JUDICIAL", SqlDbType.NVarChar).Value = V_CAT_PE_P_JUDICIAL
        SSCommandCat.Parameters.Add("@V_CAT_PE_ID", SqlDbType.Decimal).Value = V_CAT_PE_ID
        SSCommandCat.Parameters.Add("@V_MODULO", SqlDbType.Decimal).Value = V_MODULO
        Dim ds As DataTable = Consulta_Procedure(SSCommandCat, SSCommandCat.CommandText)
        Return ds
    End Function
    Public Shared Function Add_AGENCIAS(V_Cat_Ag_Id As Integer, V_Cat_Ag_Usuario As String, V_Cat_Ag_Nombre As String, V_Cat_Ag_Estatus As String, v_Cat_Ag_Motivo As String, V_Cat_Ag_UsuarioR As String, V_Bandera As Integer) As DataTable
        Dim SSCommand As New SqlCommand("Sp_Add_Cat_Agencias")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Cat_Ag_Id", SqlDbType.NVarChar).Value = V_Cat_Ag_Id
        SSCommand.Parameters.Add("@V_Cat_Ag_Usuario", SqlDbType.NVarChar).Value = V_Cat_Ag_Usuario
        SSCommand.Parameters.Add("@V_Cat_Ag_Nombre", SqlDbType.NVarChar).Value = V_Cat_Ag_Nombre
        SSCommand.Parameters.Add("@V_Cat_Ag_Estatus", SqlDbType.NVarChar).Value = V_Cat_Ag_Estatus
        SSCommand.Parameters.Add("@v_Cat_Ag_Motivo", SqlDbType.NVarChar).Value = v_Cat_Ag_Motivo
        SSCommand.Parameters.Add("@V_Cat_Ag_UsuarioR", SqlDbType.NVarChar).Value = V_Cat_Ag_UsuarioR
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = V_Bandera
        Dim DtsVarios As DataTable = Consulta_Procedure(SSCommand, SSCommand.CommandText)
        If DtsVarios.TableName = "Exception" Then
            Throw New Exception(DtsVarios.Rows(0).Item(0).ToString)
        End If
        Return DtsVarios
    End Function

    Public Shared Function CARGA_ASIGNACION_MOVIL(bandera As Integer, usuario As String, dt As DataTable) As DataTable
        Dim SSCommand2 As New SqlCommand("SP_CARGA_ASIGNACION_MOVIL") With {
                            .CommandType = CommandType.StoredProcedure
                        }
        SSCommand2.Parameters.Add(New SqlParameter("@DT_CARGA_ASIGNACION", dt))
        SSCommand2.Parameters.Add("@V_USUARIO", SqlDbType.VarChar).Value = usuario
        SSCommand2.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = bandera
        Return Consulta_Procedure(SSCommand2, "SP_CARGA_CARTERA")
    End Function

    Public Shared Function CARGA_CARTERA(bandera As Integer, usuario As String, dt As DataTable, sp As String) As DataTable
        Dim SSCommand2 As New SqlCommand(sp) With {
                            .CommandType = CommandType.StoredProcedure
                        }
        If bandera > 0 Then
            SSCommand2.CommandTimeout = dt.Rows.Count * 0.2
        End If
        SSCommand2.Parameters.Add(New SqlParameter("@DT_CARGA_CLIENTE", dt))
        'SSCommand2.Parameters.Add("@V_USUARIO", SqlDbType.VarChar).Value = usuario
        SSCommand2.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = bandera
        Return Consulta_Procedure(SSCommand2, "SP_CARGA_CARTERA")
    End Function
    Public Shared Function CARGA_SMS(bandera As Integer, dt As DataTable, credito As String) As DataTable
        Dim SSCommand2 As New SqlCommand("SP_CARGA_SMS")
        SSCommand2.CommandType = CommandType.StoredProcedure
        SSCommand2.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = bandera
        SSCommand2.Parameters.Add("@V_CREDITO", SqlDbType.VarChar).Value = credito
        SSCommand2.Parameters.Add(New SqlParameter("@DT_CARGA_CLIENTE", dt))
        Return Consulta_Procedure(SSCommand2, "SP_CARGA_CARTERA")
    End Function
    Public Shared Function CARGA_GESTION(bandera As Integer, usuario As String, dt As DataTable, agencia As Integer) As DataTable
        Dim SSCommand2 As New SqlCommand("SP_CARGA_GESTIO_MASIVA")
        SSCommand2.CommandType = CommandType.StoredProcedure
        SSCommand2.Parameters.Add("@V_USUARIO", SqlDbType.VarChar).Value = usuario
        SSCommand2.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = bandera
        SSCommand2.Parameters.Add("@V_AGNUSUARIO", SqlDbType.Int).Value = agencia
        Return Consulta_Procedure(SSCommand2, "SP_CARGA_CARTERA")
    End Function
    Public Shared Function CARGA_PROCEDEPAGOS(bandera As Integer, usuario As String, dt As DataTable) As DataTable
        Dim SSCommand2 As New SqlCommand("SP_CARGA_GESTIO_MASIVA")
        SSCommand2.CommandType = CommandType.StoredProcedure
        SSCommand2.Parameters.Add("@V_USUARIO", SqlDbType.VarChar).Value = usuario
        SSCommand2.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = bandera
        Return Consulta_Procedure(SSCommand2, "SP_CARGA_CARTERA")
    End Function

    Public Shared Function INFORMACION_CREDITO(credito As String, bandera As Integer, ByVal V_PRODUCTO As String) As DataTable
        Dim SSCommand As New SqlCommand("SP_INFORMACION_CREDITO")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Credito", SqlDbType.NVarChar).Value = credito
        SSCommand.Parameters.Add("@V_PRODUCTO", SqlDbType.NVarChar).Value = V_PRODUCTO
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = bandera
        Return Consulta_Procedure(SSCommand, "SP_INFORMACION_CREDITO")
    End Function
    Public Shared Function IMAGENES_LOGIN(ID As Integer, bandera As Integer, Optional imagen As Byte() = Nothing) As DataTable


        Dim SSCommand As New SqlCommand("SP_IMAGENES_LOGIN")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_ID", SqlDbType.Decimal).Value = ID
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = bandera
        If imagen IsNot Nothing Then
            SSCommand.Parameters.Add("@V_Imagen", SqlDbType.VarBinary).Value = imagen
        End If
        Return Consulta_Procedure(SSCommand, "SP_INFORMACION_CREDITO")

        'Dim SSCommand As New OdbcCommand("SP_IMAGENES_LOGIN")
        'SSCommand.CommandType = CommandType.StoredProcedure
        'SSCommand.CommandText = "EXEC dbo.SP_IMAGENES_LOGIN @V_ID=?,@V_Bandera=? "
        'SSCommand.Parameters.Add("@V_ID", OdbcType.Int).Value = ID
        'SSCommand.Parameters.Add("@V_Bandera", OdbcType.Decimal).Value = bandera
        'If imagen IsNot Nothing Then
        '    SSCommand.Parameters.Add("@V_Imagen", OdbcType.VarBinary).Value = imagen
        'End If
        'Return Consulta_ProcedureDns(SSCommand, "SP_INFORMACION_CREDITO")


    End Function
    Public Shared Function DISTRITOS(ID As Integer, CPI As String, CPF As String, plaza As String, numplaza As Integer, conservar As Integer, Bandera As Integer) As DataTable
        Dim SSCommand As New SqlCommand("SP_DISTRITOS")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@ID", SqlDbType.Int).Value = ID
        SSCommand.Parameters.Add("@CPI", SqlDbType.VarChar).Value = CPI
        SSCommand.Parameters.Add("@CPF", SqlDbType.VarChar).Value = CPF
        SSCommand.Parameters.Add("@Plaza", SqlDbType.VarChar).Value = plaza
        SSCommand.Parameters.Add("@NUMPlaza", SqlDbType.VarChar).Value = numplaza
        SSCommand.Parameters.Add("@Conservar", SqlDbType.Int).Value = conservar
        SSCommand.Parameters.Add("@Bandera", SqlDbType.Int).Value = Bandera
        Return Consulta_Procedure(SSCommand, "SP_DISTRITOS")
    End Function
    Public Shared Function RP_BASEMETA_BF2(v_bandera As Integer, Optional v_porc_bucket0 As Decimal = Nothing, Optional v_porc_bucket1 As Decimal = Nothing, Optional v_porc_bucket2 As Decimal = Nothing, Optional v_porc_bucket3 As Decimal = Nothing, Optional v_porc_bucket4 As Decimal = Nothing, Optional v_porc_bucket5 As Decimal = Nothing, Optional v_porc_bucket6 As Decimal = Nothing, Optional v_porc_bucket7 As Decimal = Nothing, Optional v_porc_bucket8 As Decimal = Nothing, Optional v_porc_bucket9 As Decimal = Nothing, Optional v_fecha As String = Nothing) As DataTable
        Dim SSCommand As New SqlCommand("SP_RP_BASEMETA_BF2")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = v_bandera
        SSCommand.Parameters.Add("@v_porc_bucket0", SqlDbType.Decimal).Value = v_porc_bucket0 / 100
        SSCommand.Parameters.Add("@v_porc_bucket1", SqlDbType.Decimal).Value = v_porc_bucket1 / 100
        SSCommand.Parameters.Add("@v_porc_bucket2", SqlDbType.Decimal).Value = v_porc_bucket2 / 100
        SSCommand.Parameters.Add("@v_porc_bucket3", SqlDbType.Decimal).Value = v_porc_bucket3 / 100
        SSCommand.Parameters.Add("@v_porc_bucket4", SqlDbType.Decimal).Value = v_porc_bucket4 / 100
        SSCommand.Parameters.Add("@v_porc_bucket5", SqlDbType.Decimal).Value = v_porc_bucket5 / 100
        SSCommand.Parameters.Add("@v_porc_bucket6", SqlDbType.Decimal).Value = v_porc_bucket6 / 100
        SSCommand.Parameters.Add("@v_porc_bucket7", SqlDbType.Decimal).Value = v_porc_bucket7 / 100
        SSCommand.Parameters.Add("@v_porc_bucket8", SqlDbType.Decimal).Value = v_porc_bucket8 / 100
        SSCommand.Parameters.Add("@v_porc_bucket9", SqlDbType.Decimal).Value = v_porc_bucket9 / 100
        SSCommand.Parameters.Add("@V_FECHA", SqlDbType.VarChar).Value = v_fecha
        Return Consulta_Procedure(SSCommand, "SP_RP_BASEMETA_BF2")
    End Function
    Public Shared Function CARGA_CREDITOS_EMPLEADOS(bandera As Integer, dt As DataTable) As DataTable
        Dim SSCommand2 As New SqlCommand("SP_CARGA_CREDITOSEMPLEADOS") With {.CommandType = CommandType.StoredProcedure}
        If bandera > 0 Then
            SSCommand2.CommandTimeout = dt.Rows.Count * 0.2
        End If
        ' SSCommand2.Parameters.Add(New SqlParameter("@DT_CARGA", dt))
        SSCommand2.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = bandera
        Return Consulta_Procedure(SSCommand2, SSCommand2.CommandText)
    End Function
    Public Shared Function RP_BASEMETA_BF7(bandera As Integer, prime As Double, primeMed As Double, second As Double, terch As Double, fourth As Double, fecha As String) As DataTable
        Dim SSCommand As New SqlCommand("SP_RP_BASE_META_BF7")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = bandera
        If prime <> 0 Then
            SSCommand.Parameters.Add("@v_porc_prime", SqlDbType.Int).Value = (prime / 100)
        End If
        If primeMed <> 0 Then
            SSCommand.Parameters.Add("@v_porc_prime", SqlDbType.Int).Value = (primeMed / 100)
        End If
        If second <> 0 Then
            SSCommand.Parameters.Add("@v_porc_prime", SqlDbType.Int).Value = (second / 100)
        End If
        If terch <> 0 Then
            SSCommand.Parameters.Add("@v_porc_prime", SqlDbType.Int).Value = (terch / 100)
        End If
        If fourth <> 0 Then
            SSCommand.Parameters.Add("@v_porc_prime", SqlDbType.Int).Value = (fourth / 100)
        End If
        SSCommand.Parameters.Add("@V_FECHA", SqlDbType.VarChar).Value = fecha
        Return Consulta_Procedure(SSCommand, "SP_RP_BASEMETA_BF2")
    End Function
    Public Shared Function RP_BASEMETA_INST(v_bandera As Integer, fecha As String) As DataTable
        Dim SSCommand As New SqlCommand("SP_RP_BASEMETA_INST")
        SSCommand.CommandTimeout = 120
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = v_bandera
        SSCommand.Parameters.Add("@V_FECHA", SqlDbType.VarChar).Value = fecha
        Return Consulta_Procedure(SSCommand, "RP_BASEMETA_INST")
    End Function
    Public Shared Function RP_ROLLBACK(v_bandera As Integer, Optional subproducto As String = Nothing, Optional tipo As String = Nothing, Optional v_bucket As String = Nothing, Optional v_fecha As String = Nothing) As DataTable
        Dim SSCommand As New SqlCommand("SP_RP_ROLLBACK")
        SSCommand.CommandTimeout = 120
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_SUBPRODUCTO", SqlDbType.VarChar).Value = subproducto
        SSCommand.Parameters.Add("@V_TIPO", SqlDbType.VarChar).Value = tipo
        SSCommand.Parameters.Add("@v_bucket", SqlDbType.Int).Value = v_bucket
        SSCommand.Parameters.Add("@V_FECHA", SqlDbType.VarChar).Value = v_fecha
        SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = v_bandera
        Return Consulta_Procedure(SSCommand, "SP_RP_ROLLBACK", 1800)
    End Function

    Public Shared Function RP_HIGH_RISK(v_bandera As Integer, Optional V_SUBPRODUCTO As Integer = Nothing, Optional v_fecha As String = Nothing) As DataTable
        Dim SSCommand As New SqlCommand("SP_RP_HIGH_RISK")
        SSCommand.CommandTimeout = 120
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.NVarChar).Value = v_bandera
        SSCommand.Parameters.Add("@V_FECHA", SqlDbType.VarChar).Value = v_fecha
        SSCommand.Parameters.Add("@V_SUBPRODUCTO", SqlDbType.NVarChar).Value = V_SUBPRODUCTO
        Return Consulta_Procedure(SSCommand, "SP_RP_HIGH_RISK")
    End Function

    Public Shared Function DISTRITOSINSERT(ID As Integer, CPI As String, CPF As String, plaza As String, numplaza As Integer, conservar As Integer, Region As String, Regional As String, JefePlaza As String, Gestor As String, Auxiliar As String, USUARIO As String, ZONA As String, Bandera As Integer) As DataTable
        Dim SSCommand As New SqlCommand("SP_DISTRITOS")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@ID", SqlDbType.Int).Value = ID
        SSCommand.Parameters.Add("@CPI", SqlDbType.VarChar).Value = CPI
        SSCommand.Parameters.Add("@CPF", SqlDbType.VarChar).Value = CPF
        SSCommand.Parameters.Add("@Plaza", SqlDbType.VarChar).Value = plaza
        SSCommand.Parameters.Add("@NUMPlaza", SqlDbType.VarChar).Value = numplaza
        SSCommand.Parameters.Add("@Conservar", SqlDbType.Int).Value = conservar
        SSCommand.Parameters.Add("@Bandera", SqlDbType.Int).Value = Bandera
        '  SSCommand.Parameters.Add("@GERENTE", SqlDbType.VarChar).Value = Gerente
        ' SSCommand.Parameters.Add("@SUPERVISOR", SqlDbType.VarChar).Value = Supervisor
        SSCommand.Parameters.Add("@REGION", SqlDbType.VarChar).Value = Region
        SSCommand.Parameters.Add("@REGIONAL", SqlDbType.VarChar).Value = Regional
        SSCommand.Parameters.Add("@JEFEPLAZA", SqlDbType.VarChar).Value = JefePlaza
        SSCommand.Parameters.Add("@GESTOR", SqlDbType.VarChar).Value = Gestor
        SSCommand.Parameters.Add("@AUXILIAR", SqlDbType.VarChar).Value = Auxiliar
        'SSCommand.Parameters.Add("@DISTRITO", SqlDbType.VarChar).Value = Distrito
        SSCommand.Parameters.Add("@USUARIO", SqlDbType.VarChar).Value = USUARIO
        SSCommand.Parameters.Add("@ZONA", SqlDbType.VarChar).Value = ZONA

        Return Consulta_Procedure(SSCommand, "SP_DISTRITOS")
    End Function

    Public Shared Function REGLAS(bandera As Integer, Optional v_usuario As String = Nothing, Optional v_regla_id As String = Nothing, Optional v_nombre_regla As String = Nothing, Optional v_campos_regla As String = Nothing, Optional v_tablas_regla As String = Nothing, Optional V_SELECT As String = Nothing, Optional V_LEFT_JOIN As String = Nothing, Optional V_FROM As String = Nothing, Optional V_WHERE As String = Nothing, Optional V_WHERE_B64 As String = Nothing) As DataTable
        Dim SSCommand As New SqlCommand("SP_REGLAS")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_usuario", SqlDbType.VarChar).Value = v_usuario
        SSCommand.Parameters.Add("@v_regla_id", SqlDbType.VarChar).Value = v_regla_id
        SSCommand.Parameters.Add("@v_nombre_regla", SqlDbType.VarChar).Value = v_nombre_regla

        SSCommand.Parameters.Add("@v_campos_regla", SqlDbType.VarChar).Value = v_campos_regla
        SSCommand.Parameters.Add("@v_tablas_regla", SqlDbType.VarChar).Value = v_tablas_regla

        SSCommand.Parameters.Add("@V_SELECT", SqlDbType.VarChar).Value = V_SELECT
        SSCommand.Parameters.Add("@V_LEFT_JOIN", SqlDbType.VarChar).Value = V_LEFT_JOIN
        SSCommand.Parameters.Add("@V_WHERE", SqlDbType.VarChar).Value = V_WHERE
        SSCommand.Parameters.Add("@V_WHERE_B64", SqlDbType.VarChar).Value = V_WHERE_B64
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.Int).Value = bandera
        Return Consulta_Procedure(SSCommand, "SP_REGLAS")
    End Function

    Public Shared Function MODULOS_ASIGNACION(bandera As Integer, Optional v_regla_id As String = Nothing, Optional v_usuarios_asignados As String = Nothing, Optional v_vigencia As String = Nothing) As DataTable
        Dim SSCommand As New SqlCommand("SP_MODULOS_ASIGNACION")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.Int).Value = bandera
        SSCommand.Parameters.Add("@v_regla_id", SqlDbType.Int).Value = v_regla_id
        SSCommand.Parameters.Add("@v_usuarios_asignados", SqlDbType.VarChar).Value = v_usuarios_asignados
        SSCommand.Parameters.Add("@v_vigencia", SqlDbType.VarChar).Value = v_vigencia
        Return Consulta_Procedure(SSCommand, "SP_MODULOS_ASIGNACION")
    End Function


    Public Shared Function MODULOS_CAMAPANA_SMS(bandera As Integer, Optional regla_id As Integer = Nothing, Optional plantilla_id As String = Nothing, Optional v_credito As String = Nothing, Optional v_etiqueta As String = Nothing) As DataTable
        Dim SSCommand As New SqlCommand("SP_MODULOS_CAMAPANA_SMS")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_bandera", SqlDbType.Int).Value = bandera
        SSCommand.Parameters.Add("@v_regla_id", SqlDbType.Int).Value = regla_id
        SSCommand.Parameters.Add("@v_plantilla_id", SqlDbType.VarChar).Value = plantilla_id
        SSCommand.Parameters.Add("@V_CREDITO", SqlDbType.VarChar).Value = v_credito
        SSCommand.Parameters.Add("@V_ETIQUETA", SqlDbType.VarChar).Value = v_etiqueta
        Return Consulta_Procedure(SSCommand, "SP_MODULOS_CAMAPANA_SMS")
    End Function
    Public Shared Function PonderaReglas(bandera As Integer, Optional V_CAT_DIS_ID As Integer = Nothing, Optional v_aux As String = Nothing) As DataTable
        Dim SSCommand As New SqlCommand("SP_CATALOGO_DISPERSION")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_bandera", SqlDbType.Int).Value = bandera
        SSCommand.Parameters.Add("@V_CAT_DIS_ID", SqlDbType.Int).Value = V_CAT_DIS_ID
        SSCommand.Parameters.Add("@v_aux", SqlDbType.VarChar).Value = v_aux
        Return Consulta_Procedure(SSCommand, "SP_CATALOGO_DISPERSION")
    End Function

    Public Shared Function VISOR_EXPEDIENTES(bandera As Integer, Optional v_agencia As String = Nothing) As DataTable
        Dim SSCommand As New SqlCommand("SP_VISOR_EXPEDIENTES")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_bandera", SqlDbType.Int).Value = bandera
        SSCommand.Parameters.Add("@v_agencia", SqlDbType.VarChar).Value = v_agencia
        Return Consulta_Procedure(SSCommand, "SP_VISOR_EXPEDIENTES")
    End Function
    Public Shared Function Performance(v_bandera As Integer, Optional v_anio As Integer = Nothing, Optional v_mes As Integer = Nothing) As DataTable
        Dim SSCommand As New SqlCommand("SP_PERFORMANCE")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@Bandera", SqlDbType.Int).Value = v_bandera
        SSCommand.Parameters.Add("@V_Anio", SqlDbType.Int).Value = v_anio
        SSCommand.Parameters.Add("@V_Mes", SqlDbType.Int).Value = v_mes
        Return Consulta_Procedure(SSCommand, "Filtros")
    End Function
    Public Shared Function EMAILS(bandera As Integer) As DataTable
        Dim SSCommand As New SqlCommand("SP_EMAILS")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = bandera
        Return Consulta_Procedure(SSCommand, "SP_EMAILS")
    End Function


    Public Shared Function CHAT_WHATSAPP(bandera As Integer, credito As String, mensaje As String, gestor As String, origen As String, id_p As String, id_h As String, telefono As String) As DataTable
        Dim SSCommand2 As New SqlCommand("SP_CHAT_WHATSAPP")
        SSCommand2.CommandType = CommandType.StoredProcedure
        SSCommand2.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = bandera
        SSCommand2.Parameters.Add("@V_CREDITO", SqlDbType.VarChar).Value = credito
        SSCommand2.Parameters.Add("@V_MENSAJE", SqlDbType.VarChar).Value = mensaje
        SSCommand2.Parameters.Add("@V_GESTOR", SqlDbType.VarChar).Value = gestor
        SSCommand2.Parameters.Add("@V_ORIGEN", SqlDbType.VarChar).Value = origen
        SSCommand2.Parameters.Add("@V_ID_PRINCIPAL", SqlDbType.VarChar).Value = id_p
        SSCommand2.Parameters.Add("@V_ID_HIJO", SqlDbType.VarChar).Value = id_h
        SSCommand2.Parameters.Add("@V_TELEFONO", SqlDbType.VarChar).Value = telefono
        Return Consulta_Procedure(SSCommand2, "SP_CHAT_WHATSAPP")
    End Function
    Public Shared Function GARANTIA(bandera As Integer) As DataTable
        Dim SSCommand As New SqlCommand("SP_GARANTIAS")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = bandera
        'SSCommand.Parameters.Add("@V_NombreB", SqlDbType.Int).Value = V_ID
        'SSCommand.Parameters.Add("@V_NombreB", SqlDbType.VarChar).Value = V_NOMBREB

        Return Consulta_Procedure(SSCommand, "SP_GARANTIAS")
    End Function



    Public Shared Function Negociacion(v_bandera As String, Optional id As String = "", Optional credito As String = "") As DataTable
        Dim SSCommand As New SqlCommand("SP_CATALOGO_NEGO")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_ID", SqlDbType.NVarChar).Value = id
        SSCommand.Parameters.Add("@v_NOMBRE", SqlDbType.NVarChar).Value = credito
        SSCommand.Parameters.Add("@v_bandera", SqlDbType.Decimal).Value = v_bandera
        Return Consulta_Procedure(SSCommand, "ELEMENTOS")
    End Function
    Public Shared Function ADD_CODIGOS(V_Bandera As Integer, Optional V_Cat_Co_Id As Integer = Nothing, Optional V_Cat_Co_Accion As String = Nothing, Optional V_Cat_Co_Adescripcion As String = Nothing, Optional V_Cat_Co_Resultado As String = Nothing, Optional V_Cat_Co_Rdescripcion As String = Nothing, Optional V_Cat_Co_Significativo As Integer = Nothing, Optional V_Cat_Co_Perfiles As String = Nothing, Optional V_Cat_Co_Ponderacion As String = Nothing, Optional V_Cat_Co_Producto As String = Nothing, Optional V_Cat_Co_Configuracion As Integer = Nothing, Optional V_Cat_Co_Verde As Integer = Nothing, Optional V_Cat_Co_Amarillo As Integer = Nothing, Optional V_Cat_Co_Tipo As Integer = Nothing, Optional V_Cat_Co_NoPago As String = Nothing, Optional V_Cat_Co_Atencion_A As Integer = Nothing, Optional ByVal V_Cat_Co_Instancia As String = Nothing, Optional ByVal V_Cat_Co_R1 As String = Nothing, Optional ByVal AliasCod As String = Nothing, Optional ByVal AliasDesc As String = Nothing) As DataTable
        Dim SSCommandCodigos As New SqlCommand
        SSCommandCodigos.CommandText = "Sp_Add_Codigos"
        SSCommandCodigos.CommandType = CommandType.StoredProcedure
        SSCommandCodigos.Parameters.Add("@V_Cat_Co_Id", SqlDbType.Decimal).Value = V_Cat_Co_Id
        SSCommandCodigos.Parameters.Add("@V_Cat_Co_Accion", SqlDbType.NVarChar).Value = V_Cat_Co_Accion
        SSCommandCodigos.Parameters.Add("@V_Cat_Co_Adescripcion", SqlDbType.NVarChar).Value = HttpUtility.HtmlDecode(V_Cat_Co_Adescripcion)
        SSCommandCodigos.Parameters.Add("@V_Cat_Co_Resultado", SqlDbType.NVarChar).Value = V_Cat_Co_Resultado
        SSCommandCodigos.Parameters.Add("@V_Cat_Co_Rdescripcion", SqlDbType.NVarChar).Value = HttpUtility.HtmlDecode(V_Cat_Co_Rdescripcion)
        SSCommandCodigos.Parameters.Add("@V_Cat_Co_Significativo", SqlDbType.Decimal).Value = V_Cat_Co_Significativo
        SSCommandCodigos.Parameters.Add("@V_Cat_Co_Perfiles", SqlDbType.NVarChar).Value = V_Cat_Co_Perfiles
        SSCommandCodigos.Parameters.Add("@V_Cat_Co_Ponderacion", SqlDbType.NVarChar).Value = V_Cat_Co_Ponderacion
        SSCommandCodigos.Parameters.Add("@V_Cat_Co_Producto", SqlDbType.NVarChar).Value = V_Cat_Co_Producto
        SSCommandCodigos.Parameters.Add("@V_Cat_Co_Configuracion", SqlDbType.Decimal).Value = V_Cat_Co_Configuracion
        SSCommandCodigos.Parameters.Add("@V_Cat_Co_Verde", SqlDbType.Decimal).Value = V_Cat_Co_Verde
        SSCommandCodigos.Parameters.Add("@V_Cat_Co_Amarillo", SqlDbType.Decimal).Value = V_Cat_Co_Amarillo
        SSCommandCodigos.Parameters.Add("@V_Cat_Co_Tipo", SqlDbType.Decimal).Value = V_Cat_Co_Tipo
        SSCommandCodigos.Parameters.Add("@V_Cat_Co_NoPago", SqlDbType.NVarChar).Value = V_Cat_Co_NoPago
        SSCommandCodigos.Parameters.Add("@V_Cat_Co_Atencion_A", SqlDbType.Decimal).Value = V_Cat_Co_Atencion_A
        ' SSCommandCodigos.Parameters.Add("@V_Cat_Co_Instancia", SqlDbType.NVarChar).Value = V_Cat_Co_Instancia
        SSCommandCodigos.Parameters.Add("@V_Cat_Co_R1", SqlDbType.NVarChar).Value = V_Cat_Co_R1
        SSCommandCodigos.Parameters.Add("@V_ALIAS_COD", SqlDbType.NVarChar).Value = AliasCod
        SSCommandCodigos.Parameters.Add("@V_ALIAS_DESC", SqlDbType.NVarChar).Value = AliasDesc
        'SSCommandCodigos.Parameters.Add("@V_Cat_Co_R2", SqlDbType.NVarChar).Value = V_Cat_Co_R2
        'SSCommandCodigos.Parameters.Add("@V_Cat_Co_R3", SqlDbType.NVarChar).Value = V_Cat_Co_R3
        SSCommandCodigos.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = V_Bandera
        Return Consulta_Procedure(SSCommandCodigos, "Codigos")
    End Function

    Public Shared Function CATALOGOS(v_bandera As Integer, Optional v_producto As Integer = Nothing, Optional v_valor As String = Nothing, Optional v_valor2 As String = Nothing) As DataTable
        Dim SSCommand As New SqlCommand("SP_CATALOGOS")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_bandera", SqlDbType.Int).Value = v_bandera
        SSCommand.Parameters.Add("@v_producto", SqlDbType.Int).Value = v_producto
        SSCommand.Parameters.Add("@v_valor", SqlDbType.VarChar).Value = v_valor
        SSCommand.Parameters.Add("@v_valor2", SqlDbType.VarChar).Value = v_valor2
        Return Consulta_Procedure(SSCommand, "Filtros")
    End Function
End Class
