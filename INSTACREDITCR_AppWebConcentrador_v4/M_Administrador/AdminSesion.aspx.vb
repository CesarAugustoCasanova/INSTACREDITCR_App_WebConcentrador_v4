Imports System.Data.SqlClient
Imports System.Data
Imports Db
Imports Funciones
Imports System.Web.Services
Imports System.Globalization
Imports Telerik.Web.UI

Partial Class AdminSesion
    Inherits System.Web.UI.Page

    Public Property TmpUSUARIO As IDictionary
        Get
            Return CType(Session("USUARIO"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("USUARIO") = value
        End Set
    End Property
    Public Property TmpPermisos As IDictionary
        Get
            Return CType(Session("Permisos"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("Permisos") = value
        End Set
    End Property

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If TmpUSUARIO Is Nothing Then
            OffLine(HidenUrs.Value)
            AUDITORIA(HidenUrs.Value, "Administrador", "Admin Sesiones", "", "Page_Load", "", Request.ServerVariables("REMOTE_ADDR"), Request.ServerVariables("REMOTE_HOST"))
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/SesionExpirada.aspx")
        Else
            HidenUrs.Value = TmpUSUARIO("CAT_LO_USUARIO")
            Try
                If Not IsPostBack Then

                End If
            Catch ex As Exception
                SendMail("Page_Load", ex, "", "", "")
            End Try
        End If

    End Sub

    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Administrador", "AdminSesion.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub
    Protected Sub Aviso(ByVal MSJ As String)
        RadAviso.RadAlert(MSJ, 440, 155, "AVISO", Nothing)
    End Sub

    Protected Sub RGVUsuarios_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RGVUsuarios.NeedDataSource
        RGVUsuarios.DataSource = SP.INGRESO(7, "", "", "")
    End Sub

    Private Sub RGVUsuarios_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RGVUsuarios.ItemCommand
        If e.CommandName = "DESCONECTAR" Then
            Dim Usr As String = TmpUSUARIO("CAT_LO_USUARIO"), usrDisconnect As String = e.Item.Cells.Item(3).Text
            '  Class_Auditoria.GuardarValorAuditoria(TmpUSUARIO("CAT_LO_USUARIO"), "Usuarios conectados", "Usuario desconectado: " & CType(Session("USUARIOADMIN"), USUARIO).Licencias & "|" & e.Item.Cells.Item(4).Text)

            If Usr <> usrDisconnect Then
                'SP.INGRESO(5, usrDisconnect, "", "")
                ValidaLicencias(TmpUSUARIO("LICENCIAS"), e.Item.Cells.Item(3).Text, e.Item.Cells.Item(4).Text, DateTime.Now, 4, "")
                ' Class_Auditoria.GuardarValorAuditoria(TmpUSUARIO("CAT_LO_USUARIO"), "Usuarios conectados", "Usuario desconectado: " & CType(Session("USUARIOADMIN"), USUARIO).Licencias & "|" & e.Item.Cells.Item(4).Text)

                RGVUsuarios.Rebind()
                Aviso("Usuario desconectado")
            End If
        End If

    End Sub

    Private Sub AdminSesion_Error(sender As Object, e As EventArgs) Handles Me.[Error]

    End Sub
End Class
