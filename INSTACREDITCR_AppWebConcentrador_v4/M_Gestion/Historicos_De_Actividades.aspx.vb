Imports System.Data
Imports Funciones
Imports Telerik.Web.UI
Partial Class Default3
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Not tmpCredito Is Nothing Then
                    GvHistActMasivo.Rebind()
                    Llenar()
                End If
            End If
        Catch ex As Exception
            EnviarCorreo("Gestion", "Historicos_De_Actividades.ascx", "Page_Load", ex, "", "", TryCast(Session("USUARIO"), IDictionary)("CAT_LO_USUARIO"))
        End Try
    End Sub
    Sub Llenar()

        If tmpCredito("PR_MC_CREDITO") <> "" Then
            Try
                GvHistActMasivo.Rebind()
            Catch ex As Exception
                Dim EXCEPCION = ex
            End Try
        End If
    End Sub

    Protected Sub GvHistActMasivo_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs)
        Try
            GvHistActMasivo.DataSource = Class_Hist_Act.LlenarElementosHistAct(tmpCredito("PR_MC_CREDITO"), "Hist_Ge_Dteactividad", tmpUSUARIO("CAT_LO_NUM_AGENCIA"), 0, tmpCredito("PR_MC_PRODUCTO"))
        Catch ex As Exception
            GvHistActMasivo.DataSource = Nothing
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
End Class
