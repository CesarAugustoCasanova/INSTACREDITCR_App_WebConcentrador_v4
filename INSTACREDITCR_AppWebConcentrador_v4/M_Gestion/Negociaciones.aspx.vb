Imports System.Data.SqlClient
Imports System.Data
Imports Conexiones
Imports Funciones
Imports System.Web.Services
Imports System.Globalization
Imports Busquedas
Imports Class_Hist_Act
Imports System.Web.Script.Serialization
Imports Telerik.Web.UI
Imports System.Net.Mail

Partial Class Negociaciones
    Inherits System.Web.UI.Page
    Dim LblMto As RadLabel = New RadLabel()
    Dim TxtPago As RadNumericTextBox = New RadNumericTextBox()
    Dim TxtFecha As RadDatePicker = New RadDatePicker()
    Dim LblFecha As RadLabel = New RadLabel()
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
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then
                If tmpCredito IsNot Nothing Then
                    Llenar()
                End If
            End If
        Catch ex As Exception
            Dim V_error As String = ex.ToString
        End Try
    End Sub
    Sub Llenar()
        'Try
        If tmpCredito("PR_MC_ESTATUS") <> "Retirada" Then
            Dim promesa As New DataSet
            promesa.Tables.Add(Class_Negociaciones.LlenarElementosNego(tmpCredito("PR_MC_CREDITO"), tmpUSUARIO("CAT_LO_USUARIO"), tmpCredito("PR_MC_PRODUCTO"), 0))
            If promesa.Tables(0).Rows.Count = 0 Then
                PnlConfiguracion.Visible = True
                PnlNeGociacion.Visible = True
                PnlDetalle.Visible = True
                BtnVisualizar.Visible = True
                BtnCancelarVis.Visible = False
                BtnGuardar.Visible = False
                TxtContrasenaAuto.Enabled = False
                TxtHist_Pr_SupervisorAuto.Enabled = False
                GvCalendario.DataSource = Nothing
                GvCalendario.DataBind()
                PnlNegoVigente.Visible = False
                ValidaConfiguracion()
                RddlTipoNego.DataSource = SP.Negociacion(17, tmpCredito("PR_Credito"), tmpUSUARIO("CAT_LO_NIVELQUITAS"))
                RddlTipoNego.DataBind()
                If tmpPermisos("NEGOCIACIONES_AUTORIZAR") Then
                    TxtHist_Pr_SupervisorAuto.Visible = False
                    TxtContrasenaAuto.Visible = False
                Else
                    TxtHist_Pr_SupervisorAuto.Visible = True
                    TxtContrasenaAuto.Visible = True
                End If
                'Dim tab As DataTable = SP.Negociacion(16, tmpCredito("PR_Credito"))
                rTxtPR_referencia646.Text = tmpCredito("PR_referencia646")
                rTxtPR_referencia012.Text = tmpCredito("PR_referencia012")
                rTxtPR_referencia072.Text = tmpCredito("PR_referencia072")
                rTxtPR_referencia127l.Text = tmpCredito("PR_referencia127l")
                rTxtPR_referenciapaynet.Text = tmpCredito("PR_referenciapaynet")
                TxtPR_Parcialidad.Text = tmpCredito("PR_Parcialidad")
                    TxtPR_TotalAplicado.Text = tmpCredito("PR_TotalAplicado")
                TxtPR_SaldoCapital.Text = tmpCredito("PR_SaldoCapital")
                TxtPr_CapitalAplicado.Text = tmpCredito("PR_CapitalAplicado")
                TxtPR_Capital.Text = tmpCredito("PR_Capital")
                TxtPR_ParcialidadCapital.Text = tmpCredito("PR_ParcialidadCapital")
                    TxtPR_ParcialidadInteres.Text = tmpCredito("PR_ParcialidadInteres")
                    TxtPR_SaldoActual.Text = tmpCredito("PR_SaldoActual")
                    TxtPR_SaldoVencido.Text = tmpCredito("PR_SaldoVencido")
                    TxtPR_Interes.Text = tmpCredito("PR_Interes")
                    TxtVI_IRR.Text = tmpCredito("VI_IRR")
                    TxtVI_SOSTENIDO.Text = tmpCredito("VI_SOSTENIDO")
                    TxtVI_TERMINADO.Text = tmpCredito("VI_TERMINADO")
                    TxtVI_AVANCE_CREDITO.Text = tmpCredito("VI_AVANCE_CREDITO").ToString
                    TxtVI_CUOTAS_VENCIDAS.Text = tmpCredito("VI_CUOTAS_VENCIDAS")
                    LblMsjLlenar.Text = "Nota: No se debe pagar más del saldo total actual (SI IRR), (NO IRR) no debe ser menor al saldo capital."

            Else
                LblPromesa.Text = "Promesa Vigente Para " & promesa.Tables(0).Rows(0).Item(1) & " Por " & to_money(promesa.Tables(0).Rows(0).Item(2))
                GvCalendarioVig.DataSource = promesa
                GvCalendarioVig.DataBind()
                PnlNeGociacion.Visible = False
                PnlNegoVigente.Visible = True
            End If

        End If
    End Sub
    Protected Sub RddlTipoNego_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RddlTipoNego.SelectedIndexChanged
        RlblSaldoMaximo.Text = ""
        RlblSaldoMinimo.Text = ""
        If RddlTipoNego.SelectedValue <> "AAA" Then
            PanelInfoNego.Visible = True
            Dim tab As DataTable = SP.Negociacion(18, RddlTipoNego.SelectedValue, tmpCredito("PR_Credito"))

            LBlMontocampo1.Text = "Porcentaje descuento para " & tab.Rows(0).Item("CAMPO1")
            LBlMontocampo2.Text = "Porcentaje descuento para " & tab.Rows(0).Item("CAMPO2")
            LBlMontocampo3.Text = "Porcentaje descuento para " & tab.Rows(0).Item("CAMPO3")
            TxtMontocampo1.Text = If(tab.Rows(0).Item("CAMPO1") = "No Aplica Saldo", "", tab.Rows(0).Item("MONTO1") & "%")
            TxtMontocampo2.Text = If(tab.Rows(0).Item("CAMPO2") = "No Aplica Saldo", "", tab.Rows(0).Item("MONTO2") & "%")
            TxtMontocampo3.Text = If(tab.Rows(0).Item("CAMPO3") = "No Aplica Saldo", "", tab.Rows(0).Item("MONTO3") & "%")
            TxtMontoCalculado.Text = tab.Rows(0).Item("SUMA")
            LlenarNumPagos(Val(tab.Rows(0).Item("PAGOS")))
            TxtRegla.Text = tab.Rows(0).Item("REGLA")
            lblNivel.Text = tab.Rows(0).Item("NIVEL")
            muestrapagos()
            If tmpCredito("VI_IRR").ToString.ToUpper = "SI" Then
                If Val(TxtSdoNegociado.Text) > Val(tmpCredito("PR_SaldoActual")) Then
                    TxtSdoNegociado.Text = Val(TxtSdoNegociado.Text)
                    TxtPR_SaldoActual.ForeColor = Drawing.Color.Red
                Else
                    TxtSdoNegociado.Text = tab.Rows(0).Item("SUMA")
                End If
            ElseIf tmpCredito("VI_IRR").ToString.ToUpper = "NO" Then
                If Val(tmpCredito("PR_SaldoCapital")) < Val(TxtSdoNegociado.Text) Then
                    TxtSdoNegociado.Text = Val(tmpCredito("PR_SaldoCapital"))
                    TxtPR_SaldoCapital.ForeColor = Drawing.Color.Red
                Else
                    TxtSdoNegociado.Text = tab.Rows(0).Item("SUMA")
                End If
            Else
                TxtSdoNegociado.Text = tab.Rows(0).Item("SUMA")
            End If
        End If

        If RddlTipoNego.SelectedText = "Generico" Then

            RlblSaldoMinimo.Text = to_money("0")
            Dim MTOMAX As String = tmpCredito("PR_MontoEnTransito").ToString
            RlblSaldoMaximo.Text = to_money(MTOMAX)

            'If RddlTipoNego.SelectedText = "Liquidacion" Then

            '    RlblSaldoMinimo.Text = to_money(tmpCredito("PR_IMPORTECAPITALACTUAL"))
            '    RlblSaldoMaximo.Text = to_money(Val(tmpCredito("PR_IMPORTECAPITALACTUAL")) + Val(tmpCredito("PR_IMPORTEINTERESVIGENTES")) + Val(tmpCredito("PR_IVAINTERESVIGENTES")) + Val(tmpCredito("PR_PRIMASEGUROVIGENTE")) + Val(tmpCredito("PR_RENTALOCALIZADORVIGENTE")) + Val(tmpCredito("PR_IVARENTALOCALIZADORVIGENTE")) + Val(tmpCredito("PR_IMPORTEINTERESVENCIDO")) + Val(tmpCredito("PR_IVAINTERESVENCIDO")) + Val(tmpCredito("PR_PRIMASEGUROVENCIDO")) + Val(tmpCredito("PR_RENTALOCALIZADORVENCIDO")) + Val(tmpCredito("PR_IVARENTALOCALIZADORVENCIDO")) + Val(tmpCredito("PR_IMPORTEMORATORIOS")) + Val(tmpCredito("PR_IVAMORATORIOS")))

            'ElseIf RddlTipoNego.SelectedText = "Puesta al corriente" Then
            '    RlblSaldoMinimo.Text = to_money(RlblSaldoMinimo.Text = tmpCredito("PR_TOTALCUOTAVENCIDA"))
            '    RlblSaldoMaximo.Text = to_money(RlblSaldoMaximo.Text = tmpCredito("PR_TOTALCUOTAVENCIDA"))
        End If
    End Sub
    Protected Sub RddlNumPagos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RddlNumPagos.SelectedIndexChanged

        For i As Integer = 1 To 12
            LblMto = TryCast(Page.FindControl("LblMonto" & i), RadLabel)
            TxtPago = TryCast(Page.FindControl("RTxtMonto" & i), RadNumericTextBox)
            LblFecha = TryCast(Page.FindControl("LblFecha" & i), RadLabel)
            TxtFecha = TryCast(Page.FindControl("RdFecha" & i), RadDatePicker)
            MostrarOcultar(LblMto, 1)
            MostrarOcultar(TxtPago, 1)
            MostrarOcultar(TxtFecha, 1)
            MostrarOcultar(LblFecha, 1)
        Next

        'If RddlNumPagos.SelectedValue = "0" Or RddlNumPagos.SelectedValue = "" Or RddlNumPagos.SelectedValue.ToUpper = "SELECCIONE" Then
        'Else
        For i As Integer = 1 To Val(RddlNumPagos.SelectedText)
            LblMto = TryCast(Page.FindControl("LblMonto" & i), RadLabel)
            TxtPago = TryCast(Page.FindControl("RTxtMonto" & i), RadNumericTextBox)
            LblFecha = TryCast(Page.FindControl("LblFecha" & i), RadLabel)
            TxtFecha = TryCast(Page.FindControl("RdFecha" & i), RadDatePicker)
            MostrarOcultar(LblMto, 0)
            MostrarOcultar(TxtPago, 0)
            MostrarOcultar(TxtFecha, 0)
            MostrarOcultar(LblFecha, 0)
        Next
        'End If

        If Val(RddlNumPagos.SelectedText) = 1 Then
            RTxtMonto1.Text = TxtSdoNegociado.Text
        Else
            RTxtMonto1.Text = ""
        End If

    End Sub
    Private Sub muestrapagos()
        For i As Integer = 1 To 12
            LblMto = TryCast(Page.FindControl("LblMonto" & i), RadLabel)
            TxtPago = TryCast(Page.FindControl("RTxtMonto" & i), RadNumericTextBox)
            LblFecha = TryCast(Page.FindControl("LblFecha" & i), RadLabel)
            TxtFecha = TryCast(Page.FindControl("RdFecha" & i), RadDatePicker)
            MostrarOcultar(LblMto, 1)
            MostrarOcultar(TxtPago, 1)
            MostrarOcultar(TxtFecha, 1)
            MostrarOcultar(LblFecha, 1)
        Next

        'If RddlNumPagos.SelectedValue = "0" Or RddlNumPagos.SelectedValue = "" Or RddlNumPagos.SelectedValue.ToUpper = "SELECCIONE" Then
        'Else
        For i As Integer = 1 To Val(RddlNumPagos.SelectedValue)
            LblMto = TryCast(Page.FindControl("LblMonto" & i), RadLabel)
            TxtPago = TryCast(Page.FindControl("RTxtMonto" & i), RadNumericTextBox)
            LblFecha = TryCast(Page.FindControl("LblFecha" & i), RadLabel)
            TxtFecha = TryCast(Page.FindControl("RdFecha" & i), RadDatePicker)
            MostrarOcultar(LblMto, 0)
            MostrarOcultar(TxtPago, 0)
            MostrarOcultar(TxtFecha, 0)
            MostrarOcultar(LblFecha, 0)
        Next
        'End If
    End Sub
    Sub MostrarOcultar(ByVal Elemento As Object, ByVal V_Bandera As String)
        If V_Bandera = 1 Then
            Elemento.visible = False
            If Elemento.id Like "Txt*" Then
                Elemento.text = ""
            End If
        Else
            Elemento.visible = True
        End If
    End Sub
    Protected Sub DdlResultado_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlHist_Ge_Resultado.SelectedIndexChanged
        BtnGuardar.Visible = False
        TxtHist_Pr_SupervisorAuto.Enabled = False
        TxtContrasenaAuto.Enabled = False
        If DdlHist_Ge_Resultado.SelectedText <> "Seleccione" Then
            BtnGuardar.Visible = True
            TxtHist_Pr_SupervisorAuto.Enabled = True
            TxtContrasenaAuto.Enabled = True
        End If
    End Sub

    'Sub Montos_Minimos()
    '    'Descuento mínimo y máximo
    '    TxtDescuento.MinValue = LblCat_Ne_Desc_Min.Value
    '    TxtDescuento.MaxValue = LblCat_Ne_Desc_Max.Value
    '    TTDescuento.Text = LblCat_Ne_Desc_Min.Text & "%"

    '    'Fecha mínima y máxima de primer pago
    '    TxtFechaPagoInicial.MinDate = Now
    '    TxtFechaPagoInicial.MaxDate = verifyWeekends(Now, Val(LblCat_Ne_Diasprimerpago.Text))
    '    'Saldo negociado mínimo y máximo
    '    Dim strCampoNegiciado As Double = 0.0

    '    strCampoNegiciado = Val(tmpCredito("PR_CF_VENCIDO"))

    '    TTMinSaldoNego.Text = Math.Round((strCampoNegiciado) - (strCampoNegiciado * (Val(LblCat_Ne_Desc_Max.Text) / 100)), 2)
    '    If LblCat_Ne_Desc_Max.Text = "0%" Then
    '        TTPagoInicial.Text = TxtSaldoNegociado.Text
    '    Else
    '        TTPagoInicial.Text = Math.Round(((strCampoNegiciado) - (strCampoNegiciado * (Val(LblCat_Ne_Desc_Max.Text) / 100))) * (Val(LblCat_Ne_Porciento.Text.Replace("%", "")) / 100), 2)
    '    End If
    'End Sub

    Private Function verifyWeekends(ByVal fecha As Date, ByVal addDays As Integer) As Date
        Dim aux As DayOfWeek
        For i = 1 To addDays
            aux = fecha.AddDays(1).DayOfWeek
            If aux = DayOfWeek.Saturday Or aux = DayOfWeek.Sunday Then
                i = i - 1
            End If
            fecha = fecha.AddDays(1)
        Next
        Return fecha
    End Function

    'Sub VisualizarSaldo()
    '    Dim strCampoNegiciado As Double = 0.0

    '    strCampoNegiciado = Val(tmpCredito("PR_CF_VENCIDO"))

    '    If LblCat_Ne_Pagounico.Text <> "Si" Then
    '        If LblCat_Ne_Porciento.Text = "0%" Then
    '            TxtPagoInicial.Text = TxtSaldoNegociado.Text
    '        Else
    '            TxtPagoInicial.Text = Math.Round(((strCampoNegiciado) - (strCampoNegiciado * (Val(TxtDescuento.Text) / 100))) * (Val(LblCat_Ne_Porciento.Text.Replace("%", "")) / 100), 2)
    '        End If
    '        TxtMontoPagos.Text = Math.Round((Val(TxtSaldoNegociado.Text) - Val(TxtPagoInicial.Text)) / Val(TxtCat_Ne_Num_Pagos.Text), 2)

    '        TxtSaldoNegociadoF.Text = strCampoNegiciado - Val(TxtSaldoNegociado.Text.Replace("$", "").Replace(",", ""))
    '        TxtDescuentoF.Text = Math.Round(Val(TxtSaldoNegociadoF.Text.Replace("$", "").Replace(",", "") * 100) / Val(strCampoNegiciado), 2) & "%"
    '    Else
    '        TxtSaldoNegociado.Text = Math.Round((strCampoNegiciado) - (strCampoNegiciado * (Val(TxtDescuento.Text) / 100)), 2)

    '        If LblCat_Ne_Porciento.Text = "0%" Then
    '            TxtPagoInicial.Text = TxtSaldoNegociado.Text
    '        Else
    '            TxtPagoInicial.Text = Math.Round(((strCampoNegiciado) - (strCampoNegiciado * (Val(TxtDescuento.Text) / 100))) * (Val(LblCat_Ne_Porciento.Text.Replace("%", "")) / 100), 2)
    '        End If
    '        TxtSaldoNegociadoF.Text = strCampoNegiciado - Val(TxtSaldoNegociado.Text.Replace("$", "").Replace(",", ""))
    '        TxtDescuentoF.Text = Math.Round(Val(TxtSaldoNegociadoF.Text.Replace("$", "").Replace(",", "") * 100) / Val(strCampoNegiciado), 2) & "%"
    '        BtnGuardar.Visible = True
    '        TxtContrasenaAuto.Enabled = True
    '        TxtHist_Pr_SupervisorAuto.Enabled = True
    '    End If
    'End Sub



    Protected Sub BtnAceptarPromesa_Click(sender As Object, e As EventArgs) Handles BtnAceptarPromesa.Click
        Try
            If tmpPermisos("NEGOCIACIONES_CANCELAR") Then
                If TxtHist_Pr_Motivo.Text.Length < 10 Then
                    showModal(Notificacion, "warning", "Aviso", "Capture Un Comentario Valido")
                Else
                    Class_MasterPage.CancelarPP(TxtHist_Pr_Motivo.Text & "~" & tmpCredito("PR_MC_CREDITO"), tmpUSUARIO("CAT_LO_USUARIO"), 9)
                    PnlNeGociacion.Visible = True
                    PnlNegoVigente.Visible = False
                    Llenar()
                End If
            Else
                If TxtHist_Pr_Supervisor.Text = "" Then
                    showModal(Notificacion, "warning", "Aviso", "Capture Un Usuario")
                ElseIf TxtContrasena.Text = "" Then
                    showModal(Notificacion, "warning", "Aviso", "Capture Una Contraseña")
                ElseIf TxtHist_Pr_Motivo.Text.Length < 10 Then
                    showModal(Notificacion, "warning", "Aviso", "Capture Un Comentario Valido")
                ElseIf Class_Negociaciones.LlenarElementosNego(TxtHist_Pr_Supervisor.Text, TxtContrasena.Text, "", 5).Rows(0).Item("Permiso") = 0 Then
                    showModal(Notificacion, "warning", "Aviso", "Usuario O Contraseña Incorrectos O Usuario Sin Facultades")
                Else
                    Class_MasterPage.CancelarPP(TxtHist_Pr_Motivo.Text & "~" & tmpCredito("PR_MC_CREDITO"), tmpUSUARIO("CAT_LO_USUARIO"), 9)
                    PnlNeGociacion.Visible = True
                    PnlNegoVigente.Visible = False
                    Llenar()
                End If
            End If
        Catch ex As Exception
            showModal(Notificacion, "deny", "Error", ex.Message)
        End Try
    End Sub

    Protected Sub BtnVisualizar_Click(sender As Object, e As EventArgs) Handles BtnVisualizar.Click

        PnlPrev.Enabled = True
        If RddlTipoNego.SelectedText = "Seleccione" Then
            showModal(Notificacion2, "warning", "Aviso", "Seleccione el tipo de negociación")
        ElseIf RddlNumPagos.SelectedText = "Seleccione" Then
            showModal(Notificacion2, "warning", "Aviso", "Seleccione el numero de pagos")
        ElseIf TxtSdoNegociado.Text = "" Then
            showModal(Notificacion2, "warning", "Aviso", "Capture el saldo a negociar")
            'ElseIf Val(TxtSdoNegociado.Text) < Val(No_Money(RlblSaldoMinimo.Text)) Then
            '    showModal(Notificacion2, "warning", "Aviso", "El saldo a negociar no puede ser menor que " & to_money(No_Money(RlblSaldoMinimo.Text)))
            'ElseIf Val(TxtSdoNegociado.Text) > Val(No_Money(RlblSaldoMaximo.Text)) Then
            '    showModal(Notificacion2, "warning", "Aviso", "El saldo a negociar no puede ser mayor que " & to_money(No_Money(RlblSaldoMaximo.Text)))
        Else
            Dim Fecha(RddlNumPagos.SelectedText) As Date
            Dim Ok As Integer = 0
            Dim V_Msj As String = ""
            Dim V_Monto As Double
            For i As Integer = 1 To Val(RddlNumPagos.SelectedText)
                TxtPago = TryCast(Page.FindControl("RTxtMonto" & i), RadNumericTextBox)
                TxtFecha = TryCast(Page.FindControl("RdFecha" & i), RadDatePicker)
                If TxtPago.Text = "" Or TxtPago.Text = "0" Then
                    Ok = 1
                    V_Msj = "Capture Un Monto Valido Para El Pago " & i
                    Exit For
                End If
                If TxtFecha.SelectedDate.ToString = "" Then
                    Ok = 1
                    V_Msj = "Seleccione La Fecha De Pago " & i
                    Exit For
                End If
                Fecha(i) = TxtFecha.SelectedDate
                If i = 1 And Fecha(i) < Now.ToShortDateString Then
                    Ok = 1
                    V_Msj = "La Fecha De Pago 1 No Puede Ser Menor A " & Now.ToShortDateString
                    Exit For
                End If
                If Fecha(i - 1) > Fecha(i) And i <> 1 Then
                    Ok = 1
                    V_Msj = "La Fecha De Pago " & i - 1 & " No Puede Ser Mayor A Fecha De Pago " & i
                    Exit For
                End If
                If Fecha(i - 1) = Fecha(i) And i <> 1 Then
                    Ok = 1
                    V_Msj = "La Fecha De Pago " & i - 1 & " No Puede Ser Igual A Fecha De Pago " & i
                    Exit For
                End If
                'If Fecha(i) > LblFechaMaximaNego.Text Then
                '    Ok = 1
                '    V_Msj = "La Fecha De Pago " & i & " No Puede Ser Mayor A " & LblFechaMaximaNego.Text
                '    Exit For
                'End If
                V_Monto = V_Monto + TxtPago.Text
                If i = Val(RddlNumPagos.SelectedText) And V_Monto <> Val(TxtSdoNegociado.Text) Then
                    Ok = 1
                    V_Msj = "La Suma Del Monto De Los Pagos Debe De Ser Igual Al Saldo Negociado"
                    Exit For
                End If
                'If i = Val(RddlNumPagos.SelectedText) And V_Monto > Val(TxtPR_TOTALCUOTAVENCIDA.Text) Then
                '    Ok = 1
                '    V_Msj = "La Suma De Los Pagos No Debe De Ser Mayor Al Saldo Total"
                '    Exit For
                'End If
            Next
            If Ok = 0 Then
                PnlPrev.Enabled = False
                BtnVisualizar.Enabled = False
                BtnCancelarVis.Visible = True
                DdlHist_Ge_Accion.Enabled = True
            ElseIf Ok = 1 Then
                showModal(Notificacion2, "warning", "Aviso", V_Msj)
            End If
        End If

    End Sub
    Sub ValidaConfiguracion()
        'PnlNoPago.Visible = False
        Try
            Dim Aplicacion As Aplicacion = CType(Session("Aplicacion"), Aplicacion)
            If Aplicacion.ACCION = 1 Then
                'PnlAccion.Visible = True
                Class_MasterPage.LlenarElementosMaster(DdlHist_Ge_Accion, "", tmpCredito("PR_MC_PRODUCTO"), tmpUSUARIO("CAT_LO_PERFIL"), "1,3", 8)
                DdlHist_Ge_Resultado.Enabled = False
            Else
                Class_MasterPage.LlenarElementosMaster(DdlHist_Ge_Resultado, "", tmpCredito("PR_MC_PRODUCTO"), tmpUSUARIO("CAT_LO_PERFIL"), "1,3", 2)

            End If
        Catch ex As Exception
            Try
                Class_MasterPage.LlenarElementosMaster(DdlHist_Ge_Resultado, "", tmpCredito("PR_MC_PRODUCTO"), tmpUSUARIO("CAT_LO_PERFIL"), "1,3", 2)
            Catch ex2 As Exception
                Class_MasterPage.LlenarElementosMaster(DdlHist_Ge_Accion, "", tmpCredito("PR_MC_PRODUCTO"), tmpUSUARIO("CAT_LO_PERFIL"), "1,3", 7)
            End Try

        End Try
    End Sub


    Protected Sub DdlAccion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlHist_Ge_Accion.SelectedIndexChanged
        DdlHist_Ge_Resultado.Enabled = False
        BtnGuardar.Visible = False
        DdlHist_Ge_Resultado.ClearSelection()
        DdlHist_Ge_Resultado.Items.Clear()
        If DdlHist_Ge_Accion.SelectedItem.Text <> "Seleccione" Then
            Class_MasterPage.LlenarElementosMaster(DdlHist_Ge_Resultado, DdlHist_Ge_Accion.SelectedValue, tmpCredito("PR_MC_PRODUCTO"), tmpUSUARIO("CAT_LO_PERFIL"), "1,3", 9, tmpCredito("PR_MC_INSTANCIA"))
            DdlHist_Ge_Resultado.Enabled = True
        End If
    End Sub

    Protected Sub DdlHist_Ge_Resultado_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlHist_Ge_Resultado.SelectedIndexChanged
        BtnGuardar.Visible = False
        If DdlHist_Ge_Accion.SelectedItem.Text <> "Seleccione" Then
            BtnGuardar.Visible = True
            TxtHist_Pr_SupervisorAuto.Enabled = True
            TxtContrasenaAuto.Enabled = True
        End If
    End Sub

    Protected Sub BtnCancelarVis_Click(sender As Object, e As EventArgs) Handles BtnCancelarVis.Click
        DdlHist_Ge_Resultado.ClearSelection()
        DdlHist_Ge_Resultado.Items.Clear()
        DdlHist_Ge_Accion.SelectedText = "Seleccione"
        DdlHist_Ge_Resultado.Enabled = False
        DdlHist_Ge_Accion.Enabled = False
        BtnGuardar.Visible = False
        BtnVisualizar.Enabled = True
        BtnCancelarVis.Visible = False
        PnlPrev.Enabled = True
        TxtHist_Pr_SupervisorAuto.Enabled = False
        TxtContrasenaAuto.Enabled = False
    End Sub
    Protected Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click

        If TxtHist_Pr_SupervisorAuto.Text = "" And tmpPermisos("NEGOCIACIONES_AUTORIZAR") = False Then
            showModal(Notificacion2, "warning", "Aviso", "Capture un usuario supervisor")
        ElseIf TxtContrasenaAuto.Text = "" And tmpPermisos("NEGOCIACIONES_AUTORIZAR") = False Then
            showModal(Notificacion2, "warning", "Aviso", "Capture la contraseña del supervisor")
        ElseIf TxtHist_Ge_Comentario.Text.Length < "10" Then
            showModal(Notificacion2, "warning", "Aviso", "Capture un comentario valido")
        ElseIf RddlNumPagos.SelectedText = "Seleccione" Then
            showModal(Notificacion2, "warning", "Aviso", "Capture el numero de pagos")
        ElseIf TxtFechaSeguimiento.DateInput.DisplayText.Trim = "" Then
            showModal(Notificacion2, "warning", "Aviso", "Capture La fecha de Seguimiento")
        ElseIf Class_Negociaciones.LlenarElementosNego(TxtHist_Pr_SupervisorAuto.Text, TxtContrasenaAuto.Text, "", 6).Rows(0).Item("Permiso") = 0 And tmpPermisos("NEGOCIACIONES_AUTORIZAR") = False Then 'pERMISO PARA CAPTURAR NEGOCIACIONES
            showModal(Notificacion2, "warning", "Aviso", "Usuario O Contraseña Incorrectos O Usuario Sin Facultades")
        Else
            If tmpUSUARIO("CAT_LO_NIVELQUITAS") < lblNivel.Text Then
                mail(tmpCredito("PR_Credito"))
                Guardar("Por Validar")
            Else
                Guardar("Pendiente")

            End If
        End If
    End Sub

    Public Shared Function EncodeStrToBase64(valor As String) As String
        Dim myByte As Byte() = System.Text.Encoding.UTF8.GetBytes(valor)
        Dim myBase64 As String = Convert.ToBase64String(myByte)
        Return myBase64
    End Function

    Protected Function GetUrl(ByVal page As String) As String
        Dim splits As String() = Request.Url.AbsoluteUri.Split("/"c)
        If splits.Length >= 2 Then
            Dim url As String = splits(0) & "//"
            For i As Integer = 2 To splits.Length - 3
                url += splits(i)
                url += "/"
            Next
            Return url + page
        End If
        Return page
    End Function
    Private Sub mail(key As String)
        Dim SmtpServer As New SmtpClient()
        SmtpServer.Credentials = New Net.NetworkCredential("soporte@mccollect.com.mx", "Adalesperra2")
        SmtpServer.Port = 587
        SmtpServer.Host = "smtp.gmail.com"
        SmtpServer.EnableSsl = True

        Dim mail As New MailMessage()
        Dim jsonBase As New StringBuilder()
        Dim stringCorreos = Class_Negociaciones.LlenarElementosNego(tmpCredito("PR_MC_CREDITO"), tmpUSUARIO("CAT_LO_USUARIO"), tmpCredito("PR_MC_PRODUCTO"), 7).rows(0).item(0).ToString


        Dim acepta = "[{""usuario"": """ & tmpUSUARIO("CAT_LO_USUARIO") & """,""Id_solicitud"":""1"",""id_castigo"":""" & key & """,""mail"":""" & stringCorreos & """}]"
        Dim rechaza = "[{""usuario"": """ & tmpUSUARIO("CAT_LO_USUARIO") & """,""Id_solicitud"":""2"",""id_castigo"":""" & key & """,""mail"":""" & stringCorreos & """}]"


        mail = New MailMessage()
        mail.From = New MailAddress("soporte@mccollect.com.mx")
        mail.To.Add(stringCorreos) '
        ' mail.To.Add("cesar.casanova@mccollect.com.mx") '
        mail.Subject = "Autorizacion Negociacion"
        mail.IsBodyHtml = True
        mail.Body = "<html><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8'><meta name='Generator' content='Microsoft Word 15 (filtered medium)'></head><body lang='EN-US' link='#0563C1' vlink='#954F72'><div class='WordSection1'><p class='MsoNormal'><b><span lang='ES' style='font-size:20.0pt;color:red'>Autorizacion Negociacion</span></b></p><p class='MsoNormal'>Enviado a: " & stringCorreos & "</p><p class='MsoNormal'><span lang='ES' style='font-size:  4.0pt;color:black'>Fecha :</span><span lang='ES' style='font-size:  16.0pt;color:black'> </span><span lang='ES' style='color:  #1f497d'></span><span lang='ES' style='color:black'>" & Now.ToShortDateString & "</span></p><p class='MsoNormal'><b><span lang='ES' style='color:#1f497d'> </span></b></p><p class='MsoNormal'><span lang='ES' style='color:#1f497d'> </span></p><p class='MsoNormal'><span lang='ES' style='color:#1f497d'> </span></p><table style='border: solid;border-color: black;align-content: center;' ><tr style='border: solid;border-top: gray;border-bottom: gray; '><td>Saldo a Negociar</td><td>Tipo Negociacion</td><td>Numero Pagos</td><td>Monto Calculado</td><td>" & LBlMontocampo1.Text & "</td><td>" & LBlMontocampo2.Text & "</td><td>" & LBlMontocampo3.Text & "</td></tr><tr style='border: solid;border-top: gray;border-bottom: gray; '><td>" & TxtSdoNegociado.Text & "</td><td>" & RddlTipoNego.SelectedText & "</td><td>" & RddlNumPagos.SelectedText & " </td><td>" & TxtMontoCalculado.Text & " </td><td>" & TxtMontocampo1.Text & "</td><td>" & TxtMontocampo2.Text & "</td><td>" & TxtMontocampo3.Text & "</td></tr><tr style='border: solid;border-top: gray;border-bottom: gray; '><td>Parcialidad</td><td>Total Aplicado</td><td>Saldo Capital</td><td>Parcialidad Capital</td><td>Parcialidad Interes</td><td>Saldo Actual</td><td>Saldo Vencido</td></tr><tr style='border: solid;border-top: gray;border-bottom: gray; '><td>" & TxtPR_Parcialidad.Text & "</td><td>" & TxtPR_TotalAplicado.Text & "</td><td>" & TxtPR_SaldoCapital.Text & "</td><td>" & TxtPR_ParcialidadCapital.Text & "</td><td>" & TxtPR_ParcialidadInteres.Text & "</td><td>" & TxtPR_SaldoActual.Text & "</td><td>" & TxtPR_SaldoVencido.Text & "</td></tr><tr style='border: solid;border-top: gray;border-bottom: gray; '><td>Interes</td><td>Credito IRR</td><td>Credito Sostenido</td><td>Credito Terminado</td><td>Avance del Credito</td><td>Cuotas Vencidas</td></tr><tr style='border: solid;border-top: gray;border-bottom: gray; '><td>" & TxtPR_Interes.Text & "</td><td>" & TxtVI_IRR.Text & "</td><td>" & TxtVI_SOSTENIDO.Text & "</td><td>" & TxtVI_TERMINADO.Text & "</td><td>" & TxtVI_AVANCE_CREDITO.Text & "</td><td>" & TxtVI_CUOTAS_VENCIDAS.Text & "</td></tr></table><table class='MsoNormalTable' border='0' cellspacing='0' cellpadding='0' style='border-collapse:collapse'><tr><td width='67' valign='top' style='width: 49.35pt;border:solid windowtext 1.0pt;background: #00b0f0;padding:0cm 5.4pt 0cm 5.4pt'><p class='MsoNormal' align='center' style='text-align:center'><b><span style='font-size: 12.0pt;color:white'><a href='" & GetUrl("DefaultNegociaciones.aspx") & "?castigo=" & EncodeStrToBase64(acepta) & "'><span style='color:white'>Aceptar</span></a></span></b></p></td><td> &nbsp;&nbsp;&nbsp;</td><td width='75' valign='top' style='width: 49.35pt;border:solid windowtext 1.0pt;background: red;padding:0cm 5.4pt 0cm 5.4pt'><p class='MsoNormal' align='center' style='text-align:center'><b><span style='font-size: 12.0pt;color:white'><a href='" & GetUrl("DefaultNegociaciones.aspx") & "?castigo=" & EncodeStrToBase64(rechaza) & "'><span style='color:white'>Rechazar</span></a></span></b></p></td></tr></table><p class='MsoNormal'> </p></div></body></html>"
        'Dim adjunto As Attachment = New Attachment(arch)
        'mail.Attachments.Add(adjunto)


        SmtpServer.Send(mail)


    End Sub
    Sub LlenarNumPagos(num As Integer)
        RddlNumPagos.Items.Clear()
        For x As Integer = 1 To num
            RddlNumPagos.Items.Add(x.ToString)
        Next
        RddlNumPagos.DataBind()
        RddlNumPagos.Items.Add("Seleccione")
        RddlNumPagos.SelectedValue = "Seleccione"
        RddlNumPagos.SelectedText = "Seleccione"

    End Sub
    Protected Sub Guardar(tipo As String)
        Try
            Dim V_Actualizar As String = "0"

            For i As Integer = 1 To Val(RddlNumPagos.SelectedText)
                If i = Val(RddlNumPagos.SelectedText) Then
                    V_Actualizar = "1"
                End If
                TxtPago = TryCast(Page.FindControl("RTxtMonto" & i), RadNumericTextBox)
                TxtFecha = TryCast(Page.FindControl("RdFecha" & i), RadDatePicker)
                Dim dts As String = Class_Negociaciones.GuardarNego(tmpCredito("PR_MC_CREDITO"), tmpCredito("PR_MC_PRODUCTO"), TxtPago.Text, TxtFecha.SelectedDate, tmpUSUARIO("CAT_LO_USUARIO"), "Negociacion", i, "Telefonica", tmpCredito("PR_MC_AGENCIA"), DdlHist_Ge_Accion.SelectedValue, DdlHist_Ge_Resultado.SelectedItem.Text, DdlHist_Ge_Resultado.SelectedValue, "", TxtHist_Ge_Comentario.Text, "", "", 0, tmpCredito("PR_MC_CODIGO"), V_Actualizar, CType(TxtFechaSeguimiento.SelectedDate, DateTime).ToString("yyyy-MM-dd HH:mm:ss"), RddlTipoNego.SelectedText, "", "MONTHLY", Val(TxtSdoNegociado.Text), "", "", "", "", "", tmpCredito("PR_MC_INSTANCIA").ToString.Trim, 0, tipo)
            Next
            Response.Redirect("Negociaciones.aspx")
        Catch ax As System.Threading.ThreadAbortException
        Catch ex As Exception
            showModal(Notificacion2, "warning", "Aviso", ex.Message)
        End Try
    End Sub
End Class
