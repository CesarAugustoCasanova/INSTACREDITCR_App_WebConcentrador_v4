Imports System.Data
Imports Telerik.Web.UI
Imports Db
Imports Conexiones
Imports System.Net
Imports System.IO
Imports Newtonsoft.Json
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports Funciones

Partial Class M_Gestion_Promesas
    Inherits System.Web.UI.Page
    Public Property tmpPermisos As IDictionary
        Get
            Return CType(Session("Permisos"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("Permisos") = value
        End Set
    End Property
    Protected Sub RGCampEspVig_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs)
        Try
            RGCampEspVig.DataSource = Class_Fichas.LlenarCampEsp(tmpCredito("PR_MC_CREDITO"), "", "", "", "", "", "")
        Catch ex As Exception
            RGCampEspVig.DataSource = Nothing
        End Try
    End Sub

    Private Sub Promesas_Load(sender As Object, e As EventArgs) Handles Me.Load
        RNTNumCuotasImp.NumberFormat.DecimalDigits = 0
        Dim SSCommand As New SqlCommand("SP_DIA_SAFIMAS1")
        SSCommand.CommandType = CommandType.StoredProcedure
        Dim DtsHist_Fichas As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Dim diasafi As DateTime = DateTime.Parse(DtsHist_Fichas.Rows(0).Item("FECHA").ToString).ToShortDateString
        RDPDteSimulacion.MinDate = diasafi
        If Not IsPostBack Then
            RNTPorcHonorario.Text = 0
            Dim Promesa As String = Class_CapturaVisitas.VariosQ(tmpCredito("PR_MC_CREDITO"), tmpCredito("CAT_LO_USUARIO"), 20, "CREDIFIEL").Split("|")(1)
            'Class_MasterPage.LlenarElementosMaster(RDDLAccion, "", tmpCredito("PR_MC_PRODUCTO"), tmpUSUARIO("CAT_LO_PERFIL"), "1,3", 1)

            Try
                Dim Aplicacion As Aplicacion = CType(Session("Aplicacion"), Aplicacion)
                If Aplicacion.ACCION = 1 Then
                    Class_MasterPage.LlenarElementosMaster(RDDLAccion, "", tmpCredito("PR_MC_PRODUCTO"), tmpUSUARIO("CAT_LO_PERFIL"), "1,3", 1)
                End If
            Catch ex As Exception

            End Try

            If Promesa = "0,0" Then
                PnlNeGociacion.Visible = True
                PnlNegoVigente.Visible = False
            Else
                GvCalendarioVig.DataSource = Class_Negociaciones.LlenarElementosNego(tmpCredito("PR_MC_CREDITO"), "", "", 6)
                GvCalendarioVig.DataBind()

                PnlNegoVigente.Visible = True
            End If
            llenarsaldos()
        End If
    End Sub
    Private Sub Llenar()
        Try
            If tmpCredito("PR_MC_CREDITO") <> "" Then
                RGCampEspVig.DataSource = Class_Fichas.LlenarCampEsp(tmpCredito("PR_MC_CREDITO"), "", "", "", "", "", "")
                RGCampEspVig.DataBind()

            End If
        Catch ex As Exception
            Dim eror As String = ex.ToString
            showModal(Notificacion2, "warning", "Aviso", eror)
        End Try
    End Sub

    Private Sub RBSimular_Click(sender As Object, e As EventArgs) Handles RBSimular.Click

        If RDPDteSimulacion.DateInput.Text = "" Then
            showModal(Notificacion2, "warning", "Aviso", "Por favor selecione una fecha")
        Else
            Dim diamc As Date = DateTime.Parse(RDPDteSimulacion.DateInput.Text.Replace("-00-00-00", "")).ToShortDateString


        End If

        RBAplicar.Visible = True
        RBGenFicha.Visible = False

        'End If
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
    Public Shared Sub showModal(ByRef Notificacion As Object, ByVal icono As String, ByVal titulo As String, ByVal msg As String)
        Dim radnot As RadNotification = TryCast(Notificacion, RadNotification)
        radnot.TitleIcon = icono
        radnot.ContentIcon = icono
        radnot.Title = titulo
        radnot.Text = msg
        radnot.Show()
    End Sub

    Private Sub RBGenFicha_Click(sender As Object, e As EventArgs) Handles RBGenFicha.Click

        If RDDLAccion.SelectedValue = "" Then
            showModal(Notificacion2, "warning", "Aviso", "Por favor selecione una accion")
        ElseIf RTBObservacionesGest.Text.Replace("'", "").Length < 15 Then
            showModal(Notificacion2, "warning", "Aviso", "Por favor capture un comentario mayor a 14 caracteres")
        Else 'Fechas diferentes indican simulacion
            Dim tmp_archivo As String

            'If DesEncriptarCadena(StrConexion(3)) = "MCCOLLECT" Then
            tmp_archivo = "https://pruebasmc.com.mx/GestionV4Imagenes/Ficha_Simulacion_" + tmpCredito("PR_MC_CREDITO") + ".pdf"
            'Else
            '    tmp_archivo = "https://mcnoc.com.mx/GESTIONV4/plantilla.pdf"
            'End If

            Dim gestion As New GestionPDF(tmpCredito("PR_MC_CREDITO"))

            Dim DTFICHA As DataTable = Session("RESULTADOFICHA")

            'Dim DTFICHA As DataSet = Session("RESULTADOFICHA")
            If DTFICHA.Rows.Count = 0 Then
                showModal(Notificacion, "warning", "Aviso", "No se encontro la ficha solicitada")
            Else
                Dim Promesa As String = Class_CapturaVisitas.VariosQ(tmpCredito("PR_MC_CREDITO"), tmpCredito("CAT_LO_USUARIO"), 20, "CREDIFIEL").Split("|")(1)

                If Promesa = "0,0" Then
                    Dim NombreFila As String = CType(Session("NombreFila"), String)
                    Class_Negociaciones.GuardarNego(tmpCredito("PR_MC_CREDITO"), tmpCredito("PR_MC_PRODUCTO"), RNTMtoPagar.Text, RDPDteSimulacion.DateInput.Text, tmpUSUARIO("CAT_LO_USUARIO"), RCBTipoPago.Text, "1", "Telefonica", tmpCredito("PR_MC_AGENCIA"), RDDLAccion.SelectedValue, "PP", RDDLAccion.SelectedValue & "PP", "", RTBObservacionesGest.Text.Replace("'", ""), "", "", 0, tmpCredito("PR_MC_CODIGO"), "0", RDPDteSimulacion.DateInput.Text, "", "", "", RNTMtoPagar.Text, "0", DTFICHA.Rows(0).Item("totalcond3"), NombreFila, "", "", tmpCredito("PR_MC_INSTANCIA"), DTFICHA.Rows(0).Item("KEYMC"), "")
                Else
                    Dim NombreFila As String = CType(Session("NombreFila"), String)
                    Class_Negociaciones.AplazarNego(tmpCredito("PR_MC_CREDITO"), tmpCredito("PR_MC_PRODUCTO"), RNTMtoPagar.Text, RDPDteSimulacion.DateInput.Text, tmpUSUARIO("CAT_LO_USUARIO"), RCBTipoPago.Text, "1", "Telefonica", tmpCredito("PR_MC_AGENCIA"), RDDLAccion.SelectedValue, "PP", RDDLAccion.SelectedValue & "PP", "", RTBObservacionesGest.Text.Replace("'", ""), "", "", 0, tmpCredito("PR_MC_CODIGO"), "0", RDPDteSimulacion.DateInput.Text, "", "", "", RNTMtoPagar.Text, "0", DTFICHA.Rows(0).Item("totalcond3"), NombreFila, "", "", tmpCredito("PR_MC_INSTANCIA"), DTFICHA.Rows(0).Item("KEYMC"), "")
                End If

                gestion.CreateFichaSimulacion(DTFICHA.Rows(0).Item("FOLIO"), "01", tmpUSUARIO("CAT_LO_NOMBRE"), Request.ServerVariables("REMOTE_HOST"), tmpCredito("PR_MC_CLIENTE"), tmpCredito("PR_MC_CREDITO"), IIf(RCBTipoPago.SelectedValue = "PC", "P", IIf(RCBTipoPago.SelectedValue = "PP", "P", "T")), DTFICHA.Rows(0).Item("total3"), RDPDteSimulacion.DateInput.Text.Substring(0, 10), RTBObservacionesGest.Text.Replace("'", ""), "0.00", DTFICHA.Rows(0).Item("PAGOCAPITAL"), DTFICHA.Rows(0).Item("PAGOINTNORMALES"), DTFICHA.Rows(0).Item("PAGOIVAINTNORMALES"), DTFICHA.Rows(0).Item("PAGOMORA"), DTFICHA.Rows(0).Item("PAGOIVAMORA"), DTFICHA.Rows(0).Item("PAGOCOMISIONES"), DTFICHA.Rows(0).Item("PAGOIVACOMISIONES"), DTFICHA.Rows(0).Item("montocomsumacondnuevo"), DTFICHA.Rows(0).Item("montomorsumacondnuevo"), DTFICHA.Rows(0).Item("montomintsumacondnuevo"), DTFICHA.Rows(0).Item("montomcapcondnuevo"), DTFICHA.Rows(0).Item("GASTOSCOB"), DTFICHA.Rows(0).Item("IVAGASTOSCOB"), DTFICHA.Rows(0).Item("honorariocobrartot"), tmpUSUARIO("DESPACHOID"), tmpUSUARIO("DESPACHONOMBRE"), tmpUSUARIO("TIPODESPACHO"), tmpUSUARIO("ABOGADOID"), tmpUSUARIO("ABOGADONOMBRE"), tmpUSUARIO("SUPERVISORID"), tmpUSUARIO("SUPERVISORNOMBRE"), " ", " ", " ", " ", " ", " ", RNTComisiones.Text, RNTMoratorios.Text, RNTIntNormal.Text, RNTCapital.Text, "0", DTFICHA.Rows(0).Item("totalsumacond3"), RNTBaseHonorario.Text, RNTPorcHonorario.Text, DTFICHA.Rows(0).Item("total3"), RLDiasMoraSimVal.Text)

                Dim vtn As String = "window.open('" & tmp_archivo & "','M2','scrollbars=yes,resizable=yes','height=300', 'width=300');"

                'Dim vtn As String = "window.open(""http://jsc.simfatic-solutions.com"", 'mywindow', 'location=1,status=1,scrollbars=1, width=100,height=100')"
                'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup3", vtn, True)

                ScriptManager.RegisterStartupScript(Me, GetType(Page), "alerta", vtn, True)
            End If

        End If

    End Sub
    Private Sub llenarsaldos()
        RNTTasaNormal.Text = tmpCredito("PR_BI_TASAFIJA")
        RNTTasaMoratoria.Text = tmpCredito("PR_BI_TASAMORA")
        RTBPlazo.Text = tmpCredito("PR_BI_NUMAMORTIZACION")
        RTBFrecuenciaPago.Text = tmpCredito("PR_BI_FRECUENCIA")
        RTBCSGarantia.Text = "Prendaria: " & tmpCredito("PR_BI_CONGARPRENDA") & " Liquida: " & tmpCredito("PR_BI_CONGARLIQ")
        RTBDteVencCredito.Text = tmpCredito("PR_BI_FECHAVENCIMIENTO").Replace(" 00:00:00", "") 'quitAR 0
        RTBDteOtorgamientoFinan.Text = tmpCredito("PR_BI_FECHAINICIO").Replace(" 00:00:00", "") 'quitAR 0
        RNTSdoCredito.Text = tmpCredito("PR_BI_SDOCREDITO")
        RNTCapitalPendiente.Text = tmpCredito("PR_BI_CAPVIGENTE")
        RNTCapVencImp.Text = tmpCredito("PR_BI_CAPVENIMP")
        RNTIntNormalFinan.Text = tmpCredito("PR_BI_SALINTVENCIDO")
        RNTIVAIntNormal.Text = tmpCredito("PR_BI_INTPENDVENCIVA")
        RNTIntMoratorios.Text = tmpCredito("PR_BI_SALMORATORIOS")
        RNTIVAIntMoratorio.Text = tmpCredito("PR_BI_SALIVAMORATORIOS")
        RNTComisionesFinan.Text = tmpCredito("PR_BI_COMISIONES")
        RNTIVAComisiones.Text = tmpCredito("PR_BI_IVACOMISIONES")
        RNTNumCuotasImp.Text = tmpCredito("PR_BI_NOCUOTASATRASO")
        RTBDteUltPago.Text = tmpCredito("PR_MC_DTEUP") 'revisar el llenado de este campo
        RTBDteCastigo.Text = tmpCredito("Pendiente")
        RTBInstanciaFinan.Text = tmpCredito("PR_MC_INSTANCIA")

        If tmpCredito("PR_BI_CAPITALACT") = "-1" Or tmpCredito("PR_BI_INTACT") = "-1" Or tmpCredito("PR_BI_INTMORAACT") = "-1" Or tmpCredito("PR_BI_COMISACT") = "-1" Then
            DivActualiza.Visible = False
            DivActualiza2.Visible = False
        Else
            DivActualiza.Visible = True
            DivActualiza2.Visible = True
            RNTCapitalAct.Text = tmpCredito("PR_BI_CAPITALACT")
            RNTIntNormAct.Text = tmpCredito("PR_BI_INTACT")
            RNTIntMoraAct.Text = tmpCredito("PR_BI_INTMORAACT")
            RNTComisAct.Text = tmpCredito("PR_BI_COMISACT")
        End If

        'RTBDteAsignacion.Text = tmpCredito("PR_MC_DTEASIGNA")
        'RTBDespacho.Text = tmpCredito("PR_MC_DESPACHOASIGNADO")
        'RTBNombreAbogado.Text = tmpCredito("PR_MC_UASIGNADO")
        Try
            Dim SSCommand As New SqlCommand("SP_INFORMACION_CREDITO")
            SSCommand.CommandType = CommandType.StoredProcedure
            SSCommand.Parameters.Add("@v_credito", SqlDbType.NVarChar).Value = tmpCredito("PR_MC_CREDITO")
            SSCommand.Parameters.Add("V_PRODUCTO", SqlDbType.NVarChar).Value = tmpCredito("PR_MC_PRODUCTO")
            SSCommand.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = 7
            Dim Dt As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
            RNTGastosCobranza.Text = Dt(0)(0).ToString
            RNTGastosPend.Text = Dt(0)(0).ToString
        Catch ex As Exception
            RNTGastosCobranza.Text = 0
            RNTGastosPend.Text = 0
        End Try
        'RTBNumCtaAhorroVSC.Text = tmpCredito("")
        'RTBNUMCtaEjeSaldo.Text = tmpCredito("")
        'RTBNUMCtaReciproSaldo.Text = tmpCredito("")
        'RTBCalifCartera.Text = tmpCredito("PR_BI_ESTATUSCREDITO")

        'I.- Inactivo
        'A.- Autorizado
        'V.- Vigente
        'P.- Pagado
        'C.- Cancelado
        'B.- Vencido
        'K.- Castigado
        'E.- Eliminado

        Select Case tmpCredito("PR_BI_ESTATUSCREDITO")
            Case "I"
                RTBCalifCartera.Text = "Inactivo"
            Case "A"
                RTBCalifCartera.Text = "Autorizado"
            Case "V"
                RTBCalifCartera.Text = "Vigente"'cartera activa
            Case "P"
                RTBCalifCartera.Text = "Pagado"
            Case "C"
                RTBCalifCartera.Text = "Cancelado"
            Case "B"
                RTBCalifCartera.Text = "Vencido"'cartera activa
            Case "K"
                RTBCalifCartera.Text = "Castigado"
            Case "E"
                RTBCalifCartera.Text = "Eliminado" 'cartera inactiva
        End Select

        RTBDiasMora.Text = tmpCredito("PR_BI_DIASATRASO")
        RNTCapVencImpFinan.Text = tmpCredito("PR_BI_CAPVENIMP")
        Select Case tmpCredito("PR_BI_ROL")
            Case "T"
                RTBRol.Text = "Titular"
            Case "A"
                RTBRol.Text = "Aval"
            Case "C"
                RTBRol.Text = "Codeudor"
            Case "G"
                RTBRol.Text = "Garante"
        End Select
        'RTBInstancia.Text = tmpCredito("PR_MC_INSTANCIA")

    End Sub


    Private Sub RBAplicar_Click(sender As Object, e As EventArgs) Handles RBAplicar.Click
        If RCBTipoPago.SelectedValue = "PP" And (CDec(RNTMtoPagar.Text) < 0 Or CDec(RNTMtoPagar.Text) = 0) Then
            showModal(Notificacion2, "warning", "Aviso", "El monto de pago debe ser mayor a 0")
        Else
            Dim SSCommand As New SqlCommand("SP_APLICA_PAGO")
            SSCommand.CommandType = CommandType.StoredProcedure
            SSCommand.Parameters.Add("@credito", SqlDbType.NVarChar).Value = tmpCredito("PR_MC_CREDITO")
            SSCommand.Parameters.Add("@comisionesCOND", SqlDbType.Decimal).Value = Val(RNTComisiones.Text)
            SSCommand.Parameters.Add("@moratoriosCOND", SqlDbType.Decimal).Value = Val(RNTMoratorios.Text)
            SSCommand.Parameters.Add("@interesnormalCOND", SqlDbType.Decimal).Value = Val(RNTIntNormal.Text)
            SSCommand.Parameters.Add("@capitalCOND", SqlDbType.Decimal).Value = Val(RNTCapital.Text)
            SSCommand.Parameters.Add("@honorario", SqlDbType.Decimal).Value = Val(RNTPorcHonorario.Text)
            SSCommand.Parameters.Add("@pago", SqlDbType.Decimal).Value = IIf(RCBTipoPago.SelectedValue = "PP", Val(Replace(Replace(RNTMtoPagar.Text, "$", ""), ",", "")), Val(Replace(Replace(RLAdedudoTotVal.Text, "$", ""), ",", "")))
            SSCommand.Parameters.Add("@MONTOCAPITAL", SqlDbType.Decimal).Value = Val(Replace(Replace(RLTCap.Text, "$", ""), ",", ""))
            SSCommand.Parameters.Add("@MONTOINTNORMAL", SqlDbType.Decimal).Value = Val(Replace(Replace(RLTInt.Text, "$", ""), ",", ""))
            SSCommand.Parameters.Add("@MONTOINTNORMALIVA", SqlDbType.Decimal).Value = Val(Replace(Replace(RLTIntIVA.Text, "$", ""), ",", ""))
            SSCommand.Parameters.Add("@MONTOINTMORATORIO", SqlDbType.Decimal).Value = Val(Replace(Replace(RLMoratorios.Text, "$", ""), ",", ""))
            SSCommand.Parameters.Add("@MONTOINTMORATORIOIVA", SqlDbType.Decimal).Value = Val(Replace(Replace(RLMoratoriosIVA.Text, "$", ""), ",", ""))
            SSCommand.Parameters.Add("@MONTOCOMISIONES", SqlDbType.Decimal).Value = Val(Replace(Replace(RLCom.Text, "$", ""), ",", ""))
            SSCommand.Parameters.Add("@MONTOCOMISIONESIVA", SqlDbType.Decimal).Value = Val(Replace(Replace(RLComIVA.Text, "$", ""), ",", ""))
            SSCommand.Parameters.Add("@tipopago", SqlDbType.NVarChar).Value = RCBTipoPago.SelectedValue & IIf(Session("FECHAIGUAL") = 0, "S", "")

            Dim DTFICHA As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
            If DTFICHA.Rows(0).Item("folio") = "0" Then
                showModal(Notificacion2, "warning", "Aviso", "El monto de pago no puede ser mayor a los saldos reportados por SAFI para un pago parcial")
            Else
                Session("RESULTADOFICHA") = DTFICHA
                Dim monto As Decimal
                If RCBTipoPago.SelectedValue <> "PP" Then
                    monto = CDec(RLAdedudoTotVal.Text)
                    RNTMontoTotalPagar.Text = DTFICHA.Rows(0).Item("total3")
                    'RNTMontoTotalPagar.Text = monto - CDec(DTFICHA.Rows(0).Item("montocomcondnuevo")) - CDec(DTFICHA.Rows(0).Item("montoivacomcondnuevo")) - CDec(DTFICHA.Rows(0).Item("montomorcondnuevo")) - CDec(DTFICHA.Rows(0).Item("montoivamorcondnuevo")) - CDec(DTFICHA.Rows(0).Item("montomintcondnuevo")) - CDec(DTFICHA.Rows(0).Item("montomivaintcondnuevo")) - CDec(DTFICHA.Rows(0).Item("montomcapcondnuevo")) + CDec(DTFICHA.Rows(0).Item("honorariocobrartot")) + CDec(DTFICHA.Rows(0).Item("gastoscobtotal"))
                    RNTMtoPagar.Text = RNTMontoTotalPagar.Text
                Else
                    monto = CDec(RNTMtoPagar.Text)
                    RNTMontoTotalPagar.Text = CDec(DTFICHA.Rows(0).Item("PAGOCOMISIONES")) + CDec(DTFICHA.Rows(0).Item("PAGOIVACOMISIONES")) + CDec(DTFICHA.Rows(0).Item("PAGOIVAMORA")) + CDec(DTFICHA.Rows(0).Item("PAGOMORA")) + CDec(DTFICHA.Rows(0).Item("PAGOIVAINTNORMALES")) + CDec(DTFICHA.Rows(0).Item("PAGOINTNORMALES")) + CDec(DTFICHA.Rows(0).Item("PAGOCAPITAL")) + CDec(DTFICHA.Rows(0).Item("honorariocobrartot")) + CDec(DTFICHA.Rows(0).Item("gastoscobtotal"))
                End If

                RNTBaseHonorario.Text = DTFICHA.Rows(0).Item("basehonorario")
                RNTImporteHonorario.Text = DTFICHA.Rows(0).Item("honorario")
                RNTImpuestoHonorario.Text = DTFICHA.Rows(0).Item("ivahonorariocobrar")
                RNTTotalHonorario.Text = DTFICHA.Rows(0).Item("honorariocobrartot")
                RBGenFicha.Visible = True
            End If

        End If

    End Sub




    Private Sub RGVAgencias_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles GvSaldosCredito.ItemCommand
        If e.CommandName = "Update" Then
            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(5) As String


            valores(0) = CType(MyUserControl.FindControl("TxtNUMERO"), RadTextBox).Text
            valores(1) = CType(MyUserControl.FindControl("TxtCUENTAID"), RadTextBox).Text
            valores(2) = CType(MyUserControl.FindControl("TxtPRODUCTO"), RadTextBox).Text
            valores(3) = CType(MyUserControl.FindControl("TxtSALDO_DISP"), RadNumericTextBox).Text
            valores(4) = CType(MyUserControl.FindControl("TxtSALDO_DISPUESTO"), RadNumericTextBox).Text

            If valores(4) = "" Then
                Aviso("Por favor capture el monto del saldo dispuesto, el valor minimo es 0")
            ElseIf CDec(valores(4)) > CDec(valores(3)) Then
                Aviso("El monto a disponer es mayor al saldo disponible")
                'showModal(Notificacion, "warning", "Aviso", "El monto a disponer es mayor al saldo disponible")
                e.Canceled = True
            ElseIf valores(1) = "" Then
                'Aviso("CAPTURA UN USUARIO VALIDO")
                e.Canceled = True
            ElseIf valores(2) = "Seleccione" Then
                'Aviso("SELECCIONE UN ESTATUS")
                e.Canceled = True

            Else
                Dim changedRows As DataRow()
                changedRows = Me.SaldosCredito.Select(" NUMERO = '" & editedItem.OwnerTableView.DataKeyValues(editedItem.ItemIndex)("NUMERO") & "'")
                Try
                    If (Not changedRows.Length = 1) Then
                        e.Canceled = True
                        Return
                    End If
                    Dim newValues As Hashtable = New Hashtable
                    newValues("NUMERO") = valores(0)
                    newValues("CUENTAID") = valores(1)
                    newValues("PRODUCTO") = valores(2)
                    newValues("SALDO_DISP") = valores(3)
                    newValues("SALDO_DISPUESTO") = valores(4)

                    changedRows(0).BeginEdit()
                    Dim entry As DictionaryEntry
                    For Each entry In newValues
                        changedRows(0)(CType(entry.Key, String)) = entry.Value
                    Next
                    changedRows(0).EndEdit()
                    Me.SaldosCredito.AcceptChanges()


                    Dim V_MSJ As DataTable = Class_Fichas.LlenarSaldosCredito2(valores(1), 10, valores(2), valores(4), tmpCredito("CAT_LO_USUARIO"))

                Catch ex As Exception
                    changedRows(0).CancelEdit()
                    'Aviso("No Fue Posible Actualizar El  Usuario. Razon: " + ex.Message)
                    e.Canceled = True
                End Try
            End If

        End If
    End Sub
    Protected Sub GvSaldosCredito_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs)
        Try
            'GvSaldosCredito.DataSource = Class_Fichas.LlenarSaldosCredito(tmpCredito("PR_MC_CREDITO"))

            Me.GvSaldosCredito.DataSource = Me.SaldosCredito
            Me.SaldosCredito.PrimaryKey = New DataColumn() {Me.SaldosCredito.Columns("PR_MC_CREDITO")}

        Catch ex As Exception
            GvSaldosCredito.DataSource = Nothing
        End Try
    End Sub
    Public ReadOnly Property SaldosCredito() As DataTable
        Get
            Dim obj As Object = Me.Session("Usuarios")
            If (Not obj Is Nothing) Then
                Return CType(obj, DataTable)
            End If


            Dim myDataTable As DataTable = New DataTable
            myDataTable = Class_Fichas.LlenarSaldosCredito2(tmpCredito("PR_MC_CREDITO"), 9, "", "", "")
            Me.Session("Agencias") = myDataTable
            Return myDataTable
        End Get
    End Property

    Protected Sub Aviso(ByVal MSJ As String)
        RadAviso.RadAlert(MSJ, 440, 155, "AVISO", Nothing)
    End Sub

    Private Sub RCBTipoPago_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles RCBTipoPago.SelectedIndexChanged
        If RCBTipoPago.SelectedValue = "PP" Then
            RNTMtoPagar.Enabled = True
        Else
            RNTMtoPagar.Enabled = False
        End If
    End Sub

End Class
