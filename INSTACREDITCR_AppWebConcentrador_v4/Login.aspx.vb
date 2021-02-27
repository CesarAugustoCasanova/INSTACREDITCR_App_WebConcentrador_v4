Imports System.Data
Imports System.Data.SqlClient
Imports Funciones
Imports Db
Imports Class_LDAP
Imports Conexiones
Imports System
Imports System.DirectoryServices
Partial Class Login
    Inherits System.Web.UI.Page
    Public Property tmpPermisos As IDictionary
        Get
            Return CType(Session("Permisos"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("Permisos") = value
        End Set
    End Property
    Public Property tmpPermisosAdmin As IDictionary
        Get
            Return CType(Session("PermisosAdmin"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("PermisosAdmin") = value
        End Set
    End Property
    Public Property tmpUSUARIO As IDictionary
        Get
            Return CType(Session("USUARIO"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("USUARIO") = value
        End Set
    End Property
    Private Sub Login_Load(sender As Object, e As EventArgs) Handles Me.Load
        Response.AppendHeader("Cache-Control", "no-cache, no-store, must-revalidate")
        Response.AppendHeader("Pragma", "no-cache")
        Response.AppendHeader("Expires", "0")
        LblAmbiente.Text = ""
        If Session("USUARIO") IsNot Nothing Then
            Response.Redirect("~/Modulos.aspx")
        End If
        If StrConexion(1) <> "189.203.240.93" Then
            LblAmbiente.Text = "Ambiente de Desarrollo MC (" & StrConexion(2) & ")"
        End If
        If Not IsPostBack Then

        End If
    End Sub
    Function LogOn(ByVal strEmail As String, ByVal strPassword As String) As String
        Session("USR") = strEmail
        Session("PSW") = strPassword

        Dim validaLDAP = ValidaUsuarioLDAP(strEmail, strPassword)

        If validaLDAP = "OK" Or strEmail = "ROOT" Or strEmail = "PRENDARIO" Then

            Try

                Dim DtsPermiso As DataTable = SP.PERMISOS(strEmail, strPassword, 1)

                If DtsPermiso.TableName = "Exception" Then
                    showModal(Notificacion, "deny", "Error al iniciar sesion", "Hay un problema al comunicarse con el servidor. Intente de nuevo más tarde.")
                    Return -1
                Else
                    Dim permisos As IDictionary(Of String, Boolean) = New Dictionary(Of String, Boolean)()

                    For Each row As DataRow In DtsPermiso.Rows
                        permisos.Add(row(0), IIf(row(1) = "1", True, False))
                    Next
                    tmpPermisos = permisos
                End If

                Dim DtsPermisoA As DataTable = SP.PERMISOS(strEmail, strPassword, 2)

                If DtsPermisoA.TableName = "Exception" Then
                    showModal(Notificacion, "deny", "Error al iniciar sesion", "Hay un problema al comunicarse con el servidor. Intente de nuevo más tarde.")
                    Return -1
                Else
                    Dim permisos As IDictionary(Of String, Boolean) = New Dictionary(Of String, Boolean)()

                    For Each row As DataRow In DtsPermisoA.Rows
                        permisos.Add(row(0), IIf(row(1) = "1", True, False))
                    Next
                    tmpPermisosAdmin = permisos
                End If
            Catch
            End Try

            Dim validaSession As String = 0
            Try

                Dim DtsLogin As DataTable = SP.USUARIO(1, strEmail, strPassword, "WEB")
                If DtsLogin.TableName = "Exception" Then
                    showModal(Notificacion, "deny", "Error al iniciar sesion", "Hay un problema al comunicarse con el servidor. Intente de nuevo más tarde.")
                    Return -1
                End If
                tmpUSUARIO = TableToDictionary(DtsLogin)

                If tmpUSUARIO("CAT_LO_PERFIL") = "0" Then
                    showModal(Notificacion, "deny", "Error al iniciar sesion", "Privilegios insuficientes.")
                    Session("USUARIO") = Nothing
                    Return -1
                End If

                Select Case tmpUSUARIO("CAT_LO_ESTATUS")
                    Case Is = "Cancelado"
                        validaSession = 1
                    Case Is = "Expirado"
                        validaSession = 2
                    Case Is = "Activo"
                        Dim Hora As String = Now.Hour & ":" & Now.Minute
                        If (Val(tmpUSUARIO("CAT_LO_HENTRADA")) <= Val(Hora)) = False Or (Val(tmpUSUARIO("CAT_LO_HSALIDA")) >= Val(Hora)) = False Then
                            validaSession = 3
                        Else
                            Dim Licencias As Integer = tmpUSUARIO("Licencias")
                            If ValidaLicencias(Licencias, strEmail, "", DateTime.Now, 1, "") = 1 Then
                                'validaSession = 4
                                validaSession = 5
                            Else
                                If ValidaLicencias(Licencias, strEmail, "", DateTime.Now, 3, "") = 1 Then
                                    Session("UserLoggedIn") = strEmail
                                    validaSession = 6
                                Else
                                    validaSession = 5
                                End If

                            End If
                        End If
                End Select
                'End If

            Catch ex As Exception
                Dim asda As String = "Error: " & ex.Message
                'SendMail("LogOn", ex, "", UserName.Text & "|" & UserPass.Text, UserName.Text)
            End Try
            Return validaSession
        Else
            showModal(Notificacion, "deny", "Error al iniciar sesion", validaLDAP)
            Return -1
        End If

    End Function
    Protected Sub BtnAceptar_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        iniciarSesion(txtUsr.Text, txtPwd.Text)
    End Sub
    Protected Sub iniciarSesion(usr As String, pwd As String)
        If usr = "" Then
            showModal(Notificacion, "warning", "Inicio de sesíon", "Capture Un Usuario Valido.")
            Class_Auditoria.GuardarValorAuditoria(txtUsr.Text, "Login", "Intento de conexion, Usuario no válido")

        ElseIf txtPwd.Text = "" Then
            Class_Auditoria.GuardarValorAuditoria(txtUsr.Text, "Login", "Intento de conexion, Contraseña no válida")

            showModal(Notificacion, "warning", "Inicio de sesíon", "Capture Una Contraseña Valida.")
        Else
            Dim licencia As Integer = LogOn(usr, pwd)
            Select Case licencia
                Case "0"
                    Session.Abandon()
                    Class_Auditoria.GuardarValorAuditoria(txtUsr.Text, "Login", "Intento de conexion, credenciales incorrectas: " & txtPwd.Text)
                    SP.AUDITORIA_GLOBAL(0, txtUsr.Text, "Login", "Intento de conexion, credenciales incorrectas: " & txtPwd.Text)
                    showModal(Notificacion, "warning", "Inicio de sesíon", "Credenciales incorrectas.")
                Case "1"
                    Session.Abandon()
                    Class_Auditoria.GuardarValorAuditoria(txtUsr.Text, "Login", "Intento de conexion, usuario cancelado")

                    SP.AUDITORIA_GLOBAL(0, txtUsr.Text, "Login", "Intento de conexion, usuario cancelado")
                    showModal(Notificacion, "warning", "Inicio de sesíon", "Usuario cancelado. Contacta a tu supervisor.")
                Case "2"
                    lblExpirado.Text = "Usuario " & txtUsr.Text & " expirado"
                    showModal(Notificacion, "warning", "Inicio de sesíon", "Usuario expirado. Cambia tu contrasena.")
                    ScriptManager.RegisterStartupScript(Me, GetType(Page), "modal", "<script>$('#pnlExpirado').modal();</script>", False)
                    Session.Abandon()
                Case "3"
                    Session.Abandon()

                    Class_Auditoria.GuardarValorAuditoria(txtUsr.Text, "Login", "Intento de conexion, usuario fuera de horario")
                    SP.AUDITORIA_GLOBAL(0, txtUsr.Text, "Login", "Intento de conexion, usuario fuera de horario")
                    showModal(Notificacion, "warning", "Inicio de sesíon", "Usuario fuera de horario.")
                Case "4"
                    Session.Abandon()
                    Class_Auditoria.GuardarValorAuditoria(txtUsr.Text, "Login", "Intento de conexion, usuario multiple conexion")

                    SP.AUDITORIA_GLOBAL(0, txtUsr.Text, "Login", "Intento de conexion, usuario multiple conexion")
                    showModal(Notificacion, "warning", "Inicio de sesíon", "Usuario conectado.")
                Case "5"
                    Dim Dt As DataTable = SP.INGRESO(2, tmpUSUARIO("CAT_LO_USUARIO"), GetIPv4Address(), "Modulos")

                    Class_Auditoria.GuardarValorAuditoria(txtUsr.Text, "Login", "Intento de conexion, ingreso correcto")
                    SP.AUDITORIA_GLOBAL(0, txtUsr.Text, "Login", "Intento de conexion, ingreso correcto")
                    Response.Redirect("~/Modulos")
                Case "6"
                    Session.Abandon()
                    showModal(Notificacion, "warning", "Inicio de sesíon", "6")
            End Select
        End If
    End Sub

    Private Function GetIPv4Address() As String
        GetIPv4Address = String.Empty
        Dim strHostName As String = System.Net.Dns.GetHostName()
        Dim iphe As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(strHostName)

        For Each ipheal As System.Net.IPAddress In iphe.AddressList
            If ipheal.AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork Then
                GetIPv4Address = ipheal.ToString()
            End If
        Next
    End Function

    Protected Sub LinqDataSource1_Selecting(sender As Object, e As LinqDataSourceSelectEventArgs)
        Dim avisos As List(Of Aviso) = New List(Of Aviso)

        Dim dtab As DataTable = SP.IMAGENES_LOGIN(0, 0)
        For Each row As DataRow In dtab.Rows

            Try
                avisos.Add(New Aviso("data:image;base64," + Convert.ToBase64String(row.Item("IMAGEN")), "Credifiel", "Prendario"))

            Catch ex As Exception
                avisos.Add(New Aviso("data:image;base64," + " ", "Credifiel", "Error Conexion"))
            End Try
        Next

        e.Result = From aviso In avisos

    End Sub

    Private Sub btnExpirado_Click(sender As Object, e As EventArgs) Handles btnExpirado.Click
        Try
            If txtNuevaContrasena.Text = txtRepiteContrasena.Text Then
                If Len(txtNuevaContrasena.Text) >= 1 Then

                    Dim DTS As DataTable = SP.INGRESO(2, txtUsr.Text, GetIPv4Address(), "Modulos")

                    If DTS.TableName <> "Exception" Then
                        Class_Auditoria.GuardarValorAuditoria(txtUsr.Text, "Login", "Usuario expirado, ingreso correcto")

                        SP.AUDITORIA_GLOBAL(0, txtUsr.Text, "Login", "Usuario expirado, ingreso correcto")
                        'Licencias(User.CAT_LO_USUARIO, Campana, Modulos, ("Cambio De Contrase&ntildea"), 2, TxtPassword.Text, Request.ServerVariables("REMOTE_HOST"), ("Cambio De Contrase&ntildea"), "")
                        guardar_psw(txtUsr.Text, txtNuevaContrasena.Text)
                        iniciarSesion(txtUsr.Text, txtNuevaContrasena.Text)
                    Else
                        showModal(Notificacion, "warning", "Inicio de sesíon", DTS.Rows.Item(0)("MENSAJE"))
                    End If
                Else
                    Class_Auditoria.GuardarValorAuditoria(txtUsr.Text, "Login", "Usuario expirado, contraseña no valida")

                    SP.AUDITORIA_GLOBAL(0, txtUsr.Text, "Login", "Usuario expirado, contraseña no valida")
                    showModal(Notificacion, "warning", "Inicio de sesíon", "Contrase&ntildea no Valida, Por Favor Verifique.")
                End If
            Else
                Class_Auditoria.GuardarValorAuditoria(txtUsr.Text, "Login", "Usuario expirado, contraseñas no coinciden")

                SP.AUDITORIA_GLOBAL(0, txtUsr.Text, "Login", "Usuario expirado, contraseñas no coinciden")
                showModal(Notificacion, "warning", "Inicio de sesíon", "La Contrase&ntildea No Coincide, Por Favor Verifique.")
            End If

        Catch ex As Exception
            showModal(Notificacion, "deny", "Inicio de sesíon", ex.Message)
            'SendMail2("Function Search", ex, "", "", User.CAT_LO_CONTRASENA)
        End Try

    End Sub

    Private Sub guardar_psw(usr As String, pwd As String)
        Dim USER As USUARIO = CType(Session("USUARIOADMIN"), USUARIO)
        Try
            SP.GUARDAR_CONTRASENA(usr, pwd)
            tmpUSUARIO("CAT_LO_CONTRASENA") = pwd
        Catch ex As Exception
            showModal(Notificacion, "deny", "Inicio de sesíon", ex.Message)
        End Try
    End Sub

End Class

Public Class Aviso
    Private _imgURL As String
    Private _titulo As String
    Private _descripcion As String

    Public Sub New(_imgURL As String, _titulo As String, _descripcion As String)
        Me._imgURL = _imgURL
        Me._titulo = _titulo
        Me._descripcion = _descripcion
    End Sub

    Public Property ImgURL As String
        Get
            Return _imgURL
        End Get
        Set(value As String)
            _imgURL = value
        End Set
    End Property

    Public Property Titulo As String
        Get
            Return _titulo
        End Get
        Set(value As String)
            _titulo = value
        End Set
    End Property
    Public Property Descripcion As String
        Get
            Return _descripcion
        End Get
        Set(value As String)
            _descripcion = value
        End Set
    End Property
End Class