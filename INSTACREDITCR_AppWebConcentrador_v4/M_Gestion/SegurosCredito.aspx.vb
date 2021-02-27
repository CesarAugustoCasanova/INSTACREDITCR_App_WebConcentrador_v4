Imports System.Data
Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports Db
Imports System.Globalization
Imports System.IO
Imports Funciones
Imports Telerik.Web.UI
Partial Class SegurosCredito
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not tmpCredito Is Nothing Then
                Llenar()
            End If
        Catch ex As Exception
            'EnviarCorreo("InformacionGeneral.ascx", "Page_Load", ex, "", "", TryCast(Session("USUARIO"), IDictionary)("CAT_LO_USUARIO"))
        End Try
    End Sub

    Sub Llenar()
        Try
            Dim tablaG As DataTable = Class_Hist_Seguros_Credito.LlenarInfoSegurosCredito(tmpCredito("PR_MC_CLIENTE"), 0)
            If Not IsDBNull(tablaG.Rows(0).Item("HIST_SC_CREDITOID")) Then
                TxtHIST_SC_TIPOSEGURO.Text = tablaG.Rows(0).Item("HIST_SC_TIPOSEGURO")
                TxtHIST_SC_POLIZASEGURO.Text = tablaG.Rows(0).Item("HIST_SC_POLIZASEGURO")
                TxtHIST_SC_MONTOASEGURADO.Text = tablaG.Rows(0).Item("HIST_SC_MONTOASEGURADO")
                TxtHIST_SC_VIGENCIA.Text = tablaG.Rows(0).Item("HIST_SC_VIGENCIA")
                TxtHIST_SC_NOMBREASEGRDORA.Text = tablaG.Rows(0).Item("HIST_SC_NOMBREASEGRDORA")
                TxtHIST_SC_BENEFICIARIO.Text = tablaG.Rows(0).Item("HIST_SC_BENEFICIARIO")
                TxtHIST_SC_PROCENTAJE.Text = tablaG.Rows(0).Item("HIST_SC_PROCENTAJE")
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Gestion", "InformacionGeneral.aspx", evento, ex, Cuenta, Captura, usr)
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
