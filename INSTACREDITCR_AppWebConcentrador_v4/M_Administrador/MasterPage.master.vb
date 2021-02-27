Imports Db
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Services
Imports Funciones
Imports Telerik.Web.UI

Partial Class MasterPage
    Inherits System.Web.UI.MasterPage
    Public Property tmpUSUARIO As IDictionary
        Get
            Return CType(Session("USUARIO"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("USUARIO") = value
        End Set
    End Property
    Public Property tmpPermisos As IDictionary
        Get
            Return CType(Session("Permisos"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("Permisos") = value
        End Set
    End Property

    <WebMethod(EnableSession:=True)>
    Public Shared Function KeepActiveSession(ByVal Usuario As String) As String
        Dim DtsConectado As DataTable = Class_Sesion.LlenarElementos(Usuario, "", "", "", 3, "", "", "", "")
        If DtsConectado.Rows(0).Item("Cuantas") <> "0" Then
            Return "Hola"
        Else
            Return "Bye"
        End If
    End Function


    Private Sub MasterPage_Init(sender As Object, e As EventArgs) Handles Me.Init
        'Primero se verifica la sesion del concentrador
        'Segundo se verifica si tiene permisos para el módulo
        'Tercero se verifica si existe la sesion de este modulo y si no, la inicializa
        If Session("USUARIO") Is Nothing Then
            Response.Redirect("~/SesionExpirada.aspx")
        ElseIf tmpUSUARIO("CAT_PE_A_ADMINISTRADOR").ToString <> "1" Then
            Response.Redirect("~/Modulos.aspx")
        Else
            Dim TmpAplicacion As New Aplicacion(0, 0, 0, 0, 1, 1, 1)
            Session("Aplicacion") = TmpAplicacion
            'LlenarAplicacion.APLICACION()
            Try
                PERMISOS()
            Catch ex As Exception
                SendMail("PERMISOS()", ex, "", "", tmpUSUARIO("Cat_lo_usuario"))
            End Try
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Session("Usuario") IsNot Nothing Then
            HiddUser.Value = tmpUSUARIO("CAT_LO_USUARIO")
            Try
                If CType(GetSessionActive.Search(tmpUSUARIO("CAT_LO_USUARIO"), 1), Integer) = 0 Then
                    Session.Clear()
                    Session.Abandon()
                    Response.Redirect("~/SesionExpirada.aspx")
                Else
                    If Not IsPostBack Then
                        Llenar()
                    End If
                End If
            Catch ex As Exception
                'SendMail()
            End Try
        Else
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/SesionExpirada.aspx")
        End If


    End Sub
    Protected Sub Llenar()

    End Sub
    Sub PERMISOS()

        Perfiles.Visible = tmpPermisos("PERFILES_PERMISOS")

        Politicas.Visible = tmpPermisos("POLITICAS_ACCESO")

        Agencias.Visible = tmpPermisos("AGENCIAS")

        Usuarios.Visible = tmpPermisos("USUARIOS")

        Conectados.Visible = tmpPermisos("USUARIOS_CONECTADOS")

        Codigos.Visible = tmpPermisos("CODIGOS")

        Judiciales.Visible = tmpPermisos("CATALOGOS_JUDICIALES")

        ImgLogin.Visible = tmpPermisos("IMAGEN_LOGIN")

        Sistema.Visible = tmpPermisos("SISTEMA")

        '----------------------------------------------

        Dispersion.Visible = tmpPermisos("DISPERSION")

        PrioridadReglas.Visible = tmpPermisos("PRIORIDAD_REGLAS")

        Platillas_SMS.Visible = tmpPermisos("PLANTILLAS_SMS")

        Plantillas_Correo.Visible = tmpPermisos("PLANTILLAS_CORREOS")

        ConfigCorreo.Visible = tmpPermisos("CONFIG_CORREO")

        Reglas_Globlales.Visible = tmpPermisos("REGLAS_GLOBALES")

        Juicios.Visible = tmpPermisos("CONFIG_JUICIOS")

        Campanas.Visible = tmpPermisos("CAMPANAS_MENSAJES")

        CargaGestiones.Visible = tmpPermisos("GESTIONES_MASIVAS")

        CargarAsignacionCon.Visible = tmpPermisos("ASIGNACION_MANUAL")

        CargaCartera.Visible = tmpPermisos("CARGA_CARTERA")

        CargaPagos.Visible = tmpPermisos("CARGA_PAGOS")

        Ponderacion_codigos.Visible = tmpPermisos("PONDERACION")

        Blocked_apps.Visible = tmpPermisos("BLOCKED_APPS")

        Plantillas_Whatsapp.Visible = tmpPermisos("PLANTILLAS_WHATSAPP")

        Comisiones.Visible = tmpPermisos("COMISIONES")

        CreditosEmpleados.Visible = tmpPermisos("CREDITOS_EMPLEADOS")

        CargasDiasDebito.Visible = tmpPermisos("DIAS_DEBITO")

        Exclusiones.Visible = tmpPermisos("EXCLUSIONES")

        CrearFila.Visible = tmpPermisos("FILAS")

        Plantillas_Avisos.Visible = tmpPermisos("PLANTILLAS_AVISO")

        Negociaciones.Visible = tmpPermisos("NEGOCIACIONES")

        CargaEtiquetaDom.Visible = tmpPermisos("CARGA_ETIQUETAS")

        ''''''''''''''''''''''''''''''''''''''''
        Dim ass = CType(Session("Permisos"), IDictionary)
    End Sub

    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Administrador", "MasterPage.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub



    Private Sub FakeLogOn()
        Dim SSCommandL As New SqlCommand
        SSCommandL.CommandText = "SP_USUARIO"
        SSCommandL.CommandType = CommandType.StoredProcedure
        SSCommandL.Parameters.Add("@V_USUARIO", SqlDbType.NVarChar).Value = tmpUSUARIO("CAT_LO_USUARIO")
        SSCommandL.Parameters.Add("@V_CONTRASENA", SqlDbType.NVarChar).Value = tmpUSUARIO("CAT_LO_CONTRASENA")
        SSCommandL.Parameters.Add("@V_MODULO", SqlDbType.NVarChar).Value = "Administrador"
        '  SSCommandL.Parameters.Add("@V_PANTALLA", SqlDbType.NVarChar).Value = "LogOn"
        Try
            Dim DtsLogin As DataTable = Consulta_Procedure(SSCommandL, "LogOn")
            Dim DtvLogin As DataView
            DtvLogin = DtsLogin.DefaultView
            Dim varUsuario As USUARIO = New USUARIO(DtsLogin.Rows(0)("CAT_LO_ID"), DtsLogin.Rows(0)("CAT_LO_USUARIO"), DtsLogin.Rows(0)("CAT_LO_NOMBRE"), DtsLogin.Rows(0)("CAT_LO_CONTRASENA"), DtsLogin.Rows(0)("CAT_PE_PERFIL"), DtsLogin.Rows(0)("CAT_LO_SUPERVISOR"), DtsLogin.Rows(0)("CAT_LO_DTEALTA"), DtsLogin.Rows(0)("CAT_LO_MOTIVO"), "1", DtsLogin.Rows(0)("CAT_LO_PGESTION"), "1", "1", DtsLogin.Rows(0)("CAT_LO_HENTRADA"), DtsLogin.Rows(0)("CAT_LO_HSALIDA"), DtsLogin.Rows(0)("CAT_LO_AGENCIA"), DtsLogin.Rows(0)("CAT_LO_PRODUCTOS"), "TUIIO", "1", DtsLogin.Rows(0)("CAT_LO_AGENCIASVER"), "1", DtsLogin.Rows(0)("CAT_LO_ESTATUS"), "Activa", "", "1", "1", "1", DtsLogin.Rows(0)("CAT_LO_PMOVIL"), "1") ' DtsLogin.Rows(0)("CAT_LO_MGESTION") , DtsLogin.Rows(0)("CAT_LO_MADMINISTRADOR") , DtsLogin.Rows(0)("CAT_LO_CADENAPRODUCTOS"),DtsLogin.Rows(0)("CAT_LO_CADENAAGENCIAS"),DtsLogin.Rows(0)("CAT_AG_ESTATUS"),DtsLogin.Rows(0)("cat_Lo_Num_Agencia"),DtsLogin.Rows(0)("Licencias"),DtsLogin.Rows(0)("CAT_LO_MSESSION"),DtsLogin.Rows(0)("CAT_LO_MSESSION")
            Session("USUARIOADMIN") = varUsuario
        Catch ex As Exception
            Funciones.EnviarCorreoSinEx("Administrador", "MasterPage.aspx", "FakeLogOn", "Credeniales Erroneas" & ex.Message, "", tmpUSUARIO("CAT_LO_USUARIO") & "|" & tmpUSUARIO("CAT_LO_CONTRASENA"), tmpUSUARIO("CAT_LO_USUARIO"))
        End Try
    End Sub
End Class
