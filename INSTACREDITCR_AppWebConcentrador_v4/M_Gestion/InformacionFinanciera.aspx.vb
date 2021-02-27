Imports System.Data
Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports Db
Imports System.Globalization
Imports System.IO
Imports Funciones
Imports Telerik.Web.UI

Partial Class InformacionFinanciera
    Inherits System.Web.UI.Page



    'Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
    '    Try
    '        Dim USR As String = (CType(Session("Usuario"), USUARIO)).CAT_LO_USUARIO
    '    Catch ex As Exception
    '        Session.Clear()
    '        Session.Abandon()
    '        Response.Redirect("LogIn.aspx")
    '    End Try
    '    Try
    '        Dim USUARIO As USUARIO = CType(Session("Usuario"), USUARIO)
    '        LblCat_Lo_Usuario.Text = USUARIO.CAT_LO_USUARIO
    '    Catch ex As Exception
    '        SendMail("Page_PreInit", ex, "", "", "")
    '    End Try

    'End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not (tmpCredito Is Nothing) Then
                Llenar()
            End If
        Catch ex As Exception
            'EnviarCorreo("InformacionFinanciera.ascx", "Page_Load", ex, "", "", tmpUSUARIO("CAT_LO_USUARIO"))
        End Try
    End Sub

    Sub Llenar()
        If tmpCredito("PR_MC_CREDITO") <> "" Then

            TxtPR_BI_TASABASE.Text = tmpCredito("PR_BI_TASABASE")
            TxtPR_BI_SOBRETASA.Text = tmpCredito("PR_BI_SOBRETASA")
            TxtPR_BI_PISOTASA.Text = tmpCredito("PR_BI_SOBRETASA")
            TxtPR_BI_TECHOTASA.Text = tmpCredito("PR_BI_TECHOTASA")
            TxtPR_BI_TIPCOBCOMMORATO.Text = tmpCredito("PR_BI_TIPCOBCOMMORATO")
            TxtPR_BI_FACTORMORA.Text = tmpCredito("PR_BI_FACTORMORA")
            TxtPR_BI_SALCAPVENNOEXI.Text = tmpCredito("PR_BI_SALCAPVENNOEXI")
            TxtPR_BI_SALINTORDINARIO.Text = tmpCredito("PR_BI_SALINTORDINARIO")
            TxtPR_BI_SALINTATRASADO.Text = tmpCredito("PR_BI_SALINTATRASADO")
            TxtPR_BI_SALINTVENCIDO.Text = tmpCredito("PR_BI_SALINTVENCIDO")
            TxtPR_BI_SALINTPROVISION.Text = tmpCredito("PR_BI_SALINTPROVISION")
            TxtPR_BI_SALINTNOCONTA.Text = tmpCredito("PR_BI_SALINTNOCONTA")
            TxtPR_BI_SALMORATORIOS.Text = tmpCredito("PR_BI_SALMORATORIOS")
            TxtPR_BI_SALDOMORAVENCIDO.Text = tmpCredito("PR_BI_SALDOMORAVENCIDO")
            TxtPR_BI_SALDOMORACARVEN.Text = tmpCredito("PR_BI_SALDOMORACARVEN")
            TxtPR_BI_SALCOMFALTAPAGO.Text = tmpCredito("PR_BI_SALCOMFALTAPAGO")
            TxtPR_BI_SALOTRASCOMISI.Text = tmpCredito("PR_BI_SALOTRASCOMISI")
            TxtPR_BI_SALIVAINTERES.Text = tmpCredito("PR_BI_SALIVAINTERES")
            TxtPR_BI_SALIVAMORATORIOS.Text = tmpCredito("PR_BI_SALIVAMORATORIOS")
            TxtPR_BI_SALIVACOMFALPAGO.Text = tmpCredito("PR_BI_SALIVACOMFALPAGO")
            TxtPR_BI_SALIVACOMISI.Text = tmpCredito("PR_BI_SALIVACOMISI")
            TxtPR_BI_PASOCAPATRADIA.Text = tmpCredito("PR_BI_PASOCAPATRADIA")
            TxtPR_BI_PASOCAPVENDIA.Text = tmpCredito("PR_BI_PASOCAPVENDIA")
            TxtPR_BI_PASOCAPVNEDIA.Text = tmpCredito("PR_BI_PASOCAPVNEDIA")
            TxtPR_BI_PASOINTATRADIA.Text = tmpCredito("PR_BI_PASOINTATRADIA")
            TxtPR_BI_PASOINTVENDIA.Text = tmpCredito("PR_BI_PASOINTVENDIA")
            TxtPR_BI_INTORDDEVENGADO.Text = tmpCredito("PR_BI_INTORDDEVENGADO")
            TxtPR_BI_INTMORDEVENGADO.Text = tmpCredito("PR_BI_INTMORDEVENGADO")
            TxtPR_BI_COMISIDEVENGADO.Text = tmpCredito("PR_BI_COMISIDEVENGADO")
            TxtPR_BI_PAGOCAPVIGDIA.Text = tmpCredito("PR_BI_PAGOCAPVIGDIA")
            TxtPR_BI_PAGOCAPATRDIA.Text = tmpCredito("PR_BI_PAGOCAPATRDIA")
            TxtPR_BI_PAGOCAPVENDIA.Text = tmpCredito("PR_BI_PAGOCAPVENDIA")
            TxtPR_BI_PAGOCAPVENNEXDIA.Text = tmpCredito("PR_BI_PAGOCAPVENNEXDIA")
            TxtPR_BI_PAGOINTORDDIA.Text = tmpCredito("PR_BI_PAGOINTORDDIA")
            TxtPR_BI_PAGOINTVENDIA.Text = tmpCredito("PR_BI_PAGOINTVENDIA")
            TxtPR_BI_PAGOINTATRDIA.Text = tmpCredito("PR_BI_PAGOINTATRDIA")
            TxtPR_BI_PAGOINTCALNOCON.Text = tmpCredito("PR_BI_PAGOINTCALNOCON")
            TxtPR_BI_PAGOCOMISIDIA.Text = tmpCredito("PR_BI_PAGOCOMISIDIA")
            TxtPR_BI_PAGOMORATORIOS.Text = tmpCredito("PR_BI_PAGOMORATORIOS")
            TxtPR_BI_PAGOIVADIA.Text = tmpCredito("PR_BI_PAGOIVADIA")
            TxtPR_BI_INTCONDONADODIA.Text = tmpCredito("PR_BI_INTCONDONADODIA")
            TxtPR_BI_MORCONDONADODIA.Text = tmpCredito("PR_BI_MORCONDONADODIA")
            TxtPR_BI_DIASATRASO.Text = tmpCredito("PR_BI_DIASATRASO")
            TxtPR_BI_NOCUOTASATRASO.Text = tmpCredito("PR_BI_NOCUOTASATRASO")
            TxtPR_BI_MAXIMODIASATRASO.Text = tmpCredito("PR_BI_MAXIMODIASATRASO")
            TxtPR_BI_FECHAINICIO.Text = tmpCredito("PR_BI_FECHAINICIO")
            TxtPR_BI_FECHAINICIOAMOR.Text = tmpCredito("PR_BI_FECHAINICIOAMOR")
            TxtPR_BI_SALDOPROMEDIO.Text = tmpCredito("PR_BI_SALDOPROMEDIO")
            TxtPR_BI_FRECUENCIACAP.Text = tmpCredito("PR_BI_FRECUENCIACAP")
            TxtPR_BI_PERIODICIDADCAP.Text = tmpCredito("PR_BI_PERIODICIDADCAP")
            TxtPR_BI_FRECUENCIAINT.Text = tmpCredito("PR_BI_FRECUENCIAINT")
            TxtPR_BI_PERIODICIDADINT.Text = tmpCredito("PR_BI_PERIODICIDADINT")
            TxtPR_BI_TIPOPAGOCAPITAL.Text = tmpCredito("PR_BI_TIPOPAGOCAPITAL")
            TxtPR_BI_NUMAMORTIZACION.Text = tmpCredito("PR_BI_NUMAMORTIZACION")
            TxtPR_BI_INTDEVCTAORDEN.Text = tmpCredito("PR_BI_INTDEVCTAORDEN")
            TxtPR_BI_CAPCONDONADODIA.Text = tmpCredito("PR_BI_CAPCONDONADODIA")
            TxtPR_BI_COMADMONPAGDIA.Text = tmpCredito("PR_BI_COMADMONPAGDIA")
            TxtPR_BI_COMCONDONADODIA.Text = tmpCredito("PR_BI_COMCONDONADODIA")

            TxtPR_BI_REFERENCIA.Text = tmpCredito("PR_BI_REFERENCIA")
            TxtPR_BI_PLAZOID.Text = tmpCredito("PR_BI_PLAZOID")
            TxtPR_BI_TIPOCARTERA.Text = tmpCredito("PR_BI_TIPOCARTERA")
            TxtPR_BI_CONGARPRENDA.Text = tmpCredito("PR_BI_CONGARPRENDA")
            TxtPR_BI_CONGARLIQ.Text = tmpCredito("PR_BI_CONGARLIQ")
            TxtPR_BI_MONEDAIDCREDITO.Text = tmpCredito("PR_BI_MONEDAIDCREDITO")
            TxtPR_BI_DESCRICORTAMONEDA.Text = tmpCredito("PR_BI_DESCRICORTAMONEDA")
            TxtPR_BI_LINEACREDITOID.Text = tmpCredito("PR_BI_LINEACREDITOID")
            TxtPR_BI_MONEDAID.Text = tmpCredito("PR_BI_MONEDAID")
            TxtPR_BI_CALENDIRREGULAR.Text = tmpCredito("PR_BI_CALENDIRREGULAR")
            TxtPR_BI_FECHAINHABIL.Text = tmpCredito("PR_BI_FECHAINHABIL")
            TxtPR_BI_DIAPAGOINTERES.Text = tmpCredito("PR_BI_DIAPAGOINTERES")
            TxtPR_BI_DIAPAGOCAPITAL.Text = tmpCredito("PR_BI_DIAPAGOCAPITAL")
            TxtPR_BI_DIAPAGOPROD.Text = tmpCredito("PR_BI_DIAPAGOPROD")
            TxtPR_BI_FECHTRASPASVENC.Text = tmpCredito("PR_BI_FECHTRASPASVENC")
            TxtPR_BI_FECHAUTORIZA.Text = tmpCredito("PR_BI_FECHAUTORIZA")
            TxtPR_BI_DESTINOCREID.Text = tmpCredito("PR_BI_DESTINOCREID")
            TxtPR_BI_CALIFICACION.Text = tmpCredito("PR_BI_CALIFICACION")
            TxtPR_BI_PORCRESERVA.Text = tmpCredito("PR_BI_PORCRESERVA")
            TxtPR_BI_TIPOFONDEO.Text = tmpCredito("PR_BI_TIPOFONDEO")
            TxtPR_BI_INSTITFONDEOID.Text = tmpCredito("PR_BI_INSTITFONDEOID")
            TxtPR_BI_DESEMBOLSOSDIA.Text = tmpCredito("PR_BI_DESEMBOLSOSDIA")

        End If
    End Sub

    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Gestion", "InformacionFinanciera.aspx", evento, ex, Cuenta, Captura, usr)
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
