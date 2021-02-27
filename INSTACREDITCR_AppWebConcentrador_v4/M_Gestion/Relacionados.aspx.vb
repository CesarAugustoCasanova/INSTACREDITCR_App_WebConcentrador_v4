Imports System.Data
Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports Db
Imports System.Globalization
Imports System.IO
Imports Funciones
Imports Telerik.Web.UI

Partial Class Relacionados
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                GvRelacionados.Rebind()
                ' GvParticipantes.Rebind()
                'If Not tmpCredito Is Nothing Then
                '    Llenar()
                'End If

            End If
        Catch ex As Exception
            EnviarCorreo("Gestion", "Relacionados.ascx", "Page_Load", ex, "", "", tmpUSUARIO("CAT_LO_USUARIO"))
        End Try
    End Sub

    'Sub Llenar()
    '    If tmpCredito("PR_MC_CREDITO") <> "" Then
    '        GvRelacionados.DataSource = Class_Relacionados.LlenarElementosRelacionados(tmpCredito("PR_MC_CLIENTE"), tmpCredito("PR_MC_CREDITO"), tmpCredito("PR_BI_NOMBREPRODUCTO"), 0)
    '        GvRelacionados.DataBind()
    '        'For Each GVrow As GridView In GvRelacionados.Rows
    '        'Next
    '    End If
    'End Sub

    Public Sub SeleccionarCto(sender As Object, e As ButtonClickEventArgs)
        Try
            Dim fila As GridDataItem = TryCast(TryCast(sender, Telerik.Web.UI.RadButton).NamingContainer, GridDataItem)
            Dim credit As String = fila.Cells(3).Text
            Dim producto As String = "CREDIFIEL"

            tmpCredito = LlenarCredito.Busca(credit, producto)
            Session("Buscar") = 1
            tmpCredito("PR_MC_CUENTATRABAJADAFILA") = "1"


            'LoadUserControl("InformacionGeneral")
            actualizar_pag()
        Catch ex As Exception
            'showModal("Error", ex.Message)
        End Try
    End Sub

    Public Sub actualizar_pag()
        Dim script As String = "top.location.reload();"
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "actualizar_pag", script, True)
    End Sub

    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Gestion", "Relacionados.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub

    Protected Sub GvRelacionados_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs)
        Try
            GvRelacionados.DataSource = Class_Relacionados.LlenarParticipantes(tmpCredito("PR_RFC"), tmpCredito("PR_IdCliente"), 0)
        Catch
            GvRelacionados.DataSource = Nothing
        End Try
    End Sub

    'Protected Sub GvParticipantes_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs)
    '    Try
    '        GvParticipantes.DataSource = Class_Relacionados.LlenarParticipantes(tmpCredito("PR_MC_CREDITO"), "Hist_Ge_Dteactividad", "DESC", 1)
    '    Catch ex As exception
    '        GvParticipantes.DataSource = Nothing
    '    End Try
    'End Sub

    Public Property tmpUSUARIO As IDictionary
        Get
            Return CType(Session("USUARIO"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("USUARIO") = value
        End Set
    End Property

    Public Property tmpCredito As IDictionary
        Get
            Return CType(Session("Credito"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("Credito") = value
        End Set
    End Property
End Class
