Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports Db
Imports Funciones
Imports Telerik.Web.UI
Partial Class Hist_Pagos
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Not tmpCredito Is Nothing Then
                    GVHist_PagP.Rebind()
                    GVHist_PagV.Rebind()
                    GVPP_CI.Rebind()
                    GVPP_P.Rebind()
                    pnlPagos1.DataBind()
                    pnlPagos2.DataBind()
                    Llenar()
                End If
            End If
        Catch ex As Exception
            EnviarCorreo("Gestion", "Hist_Pagos.ascx", "Page_Load", ex, "", "", tmpUSUARIO("CAT_LO_USUARIO"))
        End Try
    End Sub

    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Gestion", "Hist_Pagos.ascx", evento, ex, Cuenta, Captura, usr)
    End Sub

    Sub Llenar()
        TxtHist_Pa_Dtepago.MaxDate = Now.Date '.AddDays(1)
    End Sub

    Protected Sub GVHist_PagP_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs)
        If Session("usuario") IsNot Nothing Then
            If tmpCredito("PR_MC_CREDITO") IsNot Nothing Then
                Try
                    Dim dts As DataTable = Class_Hist_Pagos.LlenarElementosHist_Pagos(tmpCredito("PR_MC_CREDITO"), 1, tmpCredito("PR_MC_PRODUCTO"))
                    GVHist_PagP.DataSource = dts
                    Dim abc As String = ""
                Catch ex As Exception
                    Dim v_error As String = ex.Message
                End Try
            End If
        End If
    End Sub
    Protected Sub GVHist_PagV_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs)
        If Session("usuario") IsNot Nothing Then
            If tmpCredito("PR_MC_CREDITO") IsNot Nothing Then
                Try
                    GVHist_PagV.DataSource = Class_Hist_Pagos.LlenarElementosHist_Pagos(tmpCredito("PR_MC_CREDITO"), 0, tmpCredito("PR_MC_PRODUCTO"))
                    TextPag.Text = Class_Hist_Pagos.LlenarElementosHist_Pagos(tmpCredito("PR_MC_CREDITO"), 2, tmpCredito("PR_MC_PRODUCTO")).Rows(0).Item("Suma")
                Catch ex As Exception
                    Dim v_error As String = ex.Message
                End Try
            End If
        End If
    End Sub
    Protected Sub GVPP_CI_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs)
        GVPP_CI.DataSource = Nothing
        Try
            GVPP_CI.DataSource = Class_Hist_Promesas.LlenarElementosHist_Promesas(tmpCredito("PR_MC_CREDITO"), 1, tmpCredito("PR_MC_PRODUCTO"))
        Catch ex As Exception
            GVPP_CI.DataSource = Nothing
        End Try
    End Sub

    Protected Sub GVPP_P_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs)
        Try
            GVPP_P.DataSource = Class_Hist_Promesas.LlenarElementosHist_Promesas(tmpCredito("PR_MC_CREDITO"), 2, tmpCredito("PR_MC_PRODUCTO"))
        Catch ex As Exception
            GVPP_P.DataSource = Nothing
        End Try
    End Sub

    Private Sub RGInfoPagos_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RGInfoPagos.NeedDataSource
        Try
            RGInfoPagos.DataSource = Class_Hist_Promesas.LlenarElementosHist_Promesas(tmpCredito("PR_MC_CREDITO"), 3, tmpCredito("PR_MC_PRODUCTO"))
        Catch ex As Exception
            RGInfoPagos.DataSource = Nothing
        End Try
    End Sub

    Private Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click
        Try
            Dim msj As String = Class_Hist_Pagos.AgregarPago(tmpCredito("PR_MC_CREDITO"), tmpCredito("PR_MC_PRODUCTO"), tmpUSUARIO("CAT_LO_USUARIO"), TxtHist_Pa_Referencia.Text, TxtHist_Pa_Montopago.Text, TxtHist_Pa_Dtepago.DateInput.DisplayText, DdlHist_Pa_Confirmacion.SelectedText, DdlHist_Pa_Lugarpago.SelectedText, tmpUSUARIO("CAT_LO_AGENCIA"))
            showModal(Notificacion, "warning", "Aviso", msj)
            If msj = "Pago agregado" Then
                TxtHist_Pa_Montopago.Text = ""
                TxtHist_Pa_Referencia.Text = ""
                DdlHist_Pa_Confirmacion.SelectedValue = "Seleccione"
                GVHist_PagP.Rebind()

            End If
        Catch ex As Exception
            showModal(Notificacion, "warning", "Aviso", ex.Message)
        End Try
    End Sub

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
    Public Property tmpPermisos As IDictionary
        Get
            Return CType(Session("Permisos"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("Permisos") = value
        End Set
    End Property


End Class
