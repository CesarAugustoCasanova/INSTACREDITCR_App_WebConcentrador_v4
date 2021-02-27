Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports Db
Imports Funciones
Imports Telerik.Web.UI
Imports Telerik.Web.UI.Calendar

Partial Class M_Gestion_Judicial

    Inherits System.Web.UI.Page
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
    Shared edit As Boolean = False
    Shared tab As DataTable = makestable()
    Shared Function makestable() As DataTable

        Dim stab As New DataTable("Retroceso")
        stab.Columns.Add("ID")
        stab.Columns.Add("NombreP")
        stab.Columns.Add("PetVal")
        stab.Columns.Add("DtePeticion")
        stab.Columns.Add("NombreA")
        stab.Columns.Add("AcuVal")
        stab.Columns.Add("DteAtuacion")
        stab.Columns.Add("EstadoP")
        stab.Columns.Add("ResultadoA")
        stab.Columns.Add("ResVal")
        stab.Columns.Add("OBSERVACIONES")
        Return stab
    End Function
    Protected Sub GvInpulsos_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs)


        Dim id As Integer = 0
        If Session("IDJUD") IsNot Nothing Then
            id = Session("IDJUD")
        End If
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_JUDICIAL_PETICIONES"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("v_id", SqlDbType.Decimal).Value = id
        SSCommand.Parameters.Add("V_Bandera", SqlDbType.Decimal).Value = 1
        Dim dtejud As DataTable = Consulta_Procedure(SSCommand, "Judicial")
        GvInpulsos.DataSource = dtejud




    End Sub
    Function valoer(v_bandera As Integer, v_credito As String, v_producto As String, Optional v_expediente As String = "", Optional v_juzgado As String = "", Optional v_dtepresentacion As String = "", Optional v_etapa As String = "", Optional v_tipojuicio As String = "", Optional V_PROMOCION As String = "", Optional v_acuerdo As String = "") As DataTable
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_JUDICIAL"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("V_Credito", SqlDbType.VarChar).Value = v_credito
        SSCommand.Parameters.Add("V_Producto", SqlDbType.VarChar).Value = v_producto
        SSCommand.Parameters.Add("v_expediente", SqlDbType.VarChar).Value = v_expediente
        SSCommand.Parameters.Add("v_juzgado", SqlDbType.VarChar).Value = v_juzgado
        SSCommand.Parameters.Add("v_dtepresentacion", SqlDbType.VarChar).Value = v_dtepresentacion
        SSCommand.Parameters.Add("v_etapa", SqlDbType.VarChar).Value = v_etapa
        SSCommand.Parameters.Add("v_tipojuicio", SqlDbType.VarChar).Value = v_tipojuicio
        SSCommand.Parameters.Add("v_promocion", SqlDbType.VarChar).Value = V_PROMOCION
        SSCommand.Parameters.Add("v_acuerdo", SqlDbType.VarChar).Value = v_acuerdo
        SSCommand.Parameters.Add("V_Bandera", SqlDbType.VarChar).Value = v_bandera
        Dim dtejud As DataTable = Consulta_Procedure(SSCommand, "Judicial")
        Return dtejud
    End Function
    Protected Sub ABRE(win As RadWindow)
        Try
            CbPromocion.DataSource = valoer(7, "", "", "", "", "", TxtEtapaProc.Text, CbTipoJuicio.SelectedValue)
            CbPromocion.DataBind()
        Catch
        End Try
        If win.Equals(winConvenios) Then
            CbGarantiaconve.DataSource = valoer(18, Session("IDJUD"), tmpCredito("PR_MC_PRODUCTO"))
            CbGarantiaconve.DataBind()
            Dim SSCommand3 As New SqlCommand
            SSCommand3.CommandText = "SP_JUDICIAL_PETICIONES"
            SSCommand3.CommandType = CommandType.StoredProcedure
            SSCommand3.Parameters.Add("v_consecutivo", SqlDbType.VarChar).Value = 1
            SSCommand3.Parameters.Add("v_id", SqlDbType.VarChar).Value = Session("IDJUD")
            SSCommand3.Parameters.Add("V_Bandera", SqlDbType.Decimal).Value = 14
            Dim dtejud3 As DataTable = Consulta_Procedure(SSCommand3, "Judicial")
            If dtejud3.Rows.Count > 0 Then
                DPFechaConvenio.SelectedDate = CType(dtejud3.Rows(0).Item("fecha"), DateTime)
                TxtPagos.Text = dtejud3.Rows(0).Item("pagos")
                txttotal.Text = dtejud3.Rows(0).Item("total")
                txttotaltotal.Text = dtejud3.Rows(0).Item("totaltotal")
                TxtObservacionesconve.Text = dtejud3.Rows(0).Item("observacion")
                CbGarantiaconve.SelectedValue = dtejud3.Rows(0).Item("garantia")
                txtincumplimiento.Text = dtejud3.Rows(0).Item("incumple")
                DPFechaConvenio.Enabled = False
                txttotal.Enabled = False
                TxtObservacionesconve.Enabled = False
                txtincumplimiento.Enabled = False
                TxtPagos.Enabled = False
                txttotaltotal.Enabled = False
                CbGarantiaconve.Enabled = False
                btnaceptarconve.Enabled = False
                lblconseconv.Text = 1
            End If

        End If
        Dim script As String = "function f(){$find(""" + win.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
    End Sub
    Protected Sub CIERRA(win As RadWindow)
        Dim script As String = "function f(){$find(""" + win.ClientID + """).close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
    End Sub

    Private Sub GvInpulsos_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles GvInpulsos.ItemCommand
        If e.CommandName = "InitInsert" Then
            Try
                ABRE(WinJudicial)
            Catch
                showModal(Notificacion, "warning", "Falta", "Proceso de registro no efectuado,Llene los datos del bloque Inscripción del Expediente Judicial y vuelva a intentarlo")
            End Try
            e.Canceled = True
        ElseIf e.CommandName = "Edit" Then
            lblconse.Text = e.Item.Cells(3).Text
            Dim SSCommand As New SqlCommand
            SSCommand.CommandText = "SP_JUDICIAL_PETICIONES"
            SSCommand.CommandType = CommandType.StoredProcedure
            SSCommand.Parameters.Add("v_id", SqlDbType.Decimal).Value = Split(e.Item.Cells(3).Text, "-")(0)
            SSCommand.Parameters.Add("v_consecutivo", SqlDbType.Decimal).Value = Split(e.Item.Cells(3).Text, "-")(1)
            SSCommand.Parameters.Add("V_Bandera", SqlDbType.Decimal).Value = 2
            Dim dtejud As DataRow = Consulta_Procedure(SSCommand, "Judicial").Rows(0)
            CbPromocion.SelectedValue = dtejud.Item("promo")
            CbAcuerdo.DataSource = valoer(8, "", "", "", "", TxtEtapaProc.Text, CbTipoJuicio.SelectedValue, dtejud.Item("promo"))
            CbAcuerdo.DataBind()
            CbAcuerdo.SelectedValue = dtejud.Item("acuerdo")
            CbResultado.DataSource = valoer(9, "", "", "", "", TxtEtapaProc.Text, CbTipoJuicio.SelectedValue, dtejud.Item("promo"), dtejud.Item("acuerdo"))
            CbResultado.DataBind()
            CbResultado.SelectedValue = dtejud.Item("resultado")
            If Not IsDBNull(dtejud.Item("dteacuerdo")) Then
                DpAcuerdo.SelectedDate = CType(dtejud.Item("dteacuerdo"), DateTime)
            End If
            DpPromocion.SelectedDate = CType(dtejud.Item("dtepromo"), DateTime)
            TxtObservaciones.Text = dtejud.Item("observaciones")
            If dtejud.Item("acuerdo") = 8 Then
                ExoVen.Visible = True
                CbPersona.DataSource = valoer(12, tmpCredito("PR_MC_CREDITO"), tmpCredito("PR_MC_PRODUCTO"))
                CbPersona.DataBind()
                CbCiudad.DataSource = valoer(11, "", tmpCredito("PR_MC_PRODUCTO"))
                CbCiudad.DataBind()
                Dim SSCommand2 As New SqlCommand
                SSCommand2.CommandText = "SP_JUDICIAL_PETICIONES"
                SSCommand2.CommandType = CommandType.StoredProcedure
                SSCommand2.Parameters.Add("v_resultado", SqlDbType.VarChar).Value = e.Item.Cells(3).Text
                SSCommand2.Parameters.Add("V_Bandera", SqlDbType.Decimal).Value = 4
                Dim dtejud2 As DataRow = Consulta_Procedure(SSCommand2, "Judicial").Rows(0)
                CbPersona.SelectedValue = dtejud2.Item("persona")
                MTBNumEx.Text = dtejud2.Item("numeroex")
                CbCiudad.SelectedValue = dtejud2.Item("ciudad")
                CbJuzgadoEx.SelectedValue = dtejud2.Item("juzgado")
                DPRadicacion.SelectedDate = CType(dtejud2.Item("dteradicacion"), DateTime)
                DpRegresaEX.SelectedDate = CType(dtejud2.Item("dteregreso"), DateTime)
                TxtObserEx.Text = dtejud2.Item("observacionesext")
            End If
            If dtejud.Item("promo") = 10 Or dtejud.Item("promo") = 4 Then
                DiliVen.Visible = True
                'ExoVen.Visible = False
                CbGarantiaJud.DataSource = valoer(18, Session("IDJUD"), tmpCredito("PR_MC_PRODUCTO"))
                CbGarantiaJud.DataBind()
                cbParticipante.DataSource = valoer(12, tmpCredito("PR_MC_CREDITO"), tmpCredito("PR_MC_PRODUCTO"))
                cbParticipante.DataBind()
                cbdiligencia.DataSource = valoer(14, "", tmpCredito("PR_MC_PRODUCTO"))
                cbdiligencia.DataBind()
                Dim SSCommand3 As New SqlCommand
                SSCommand3.CommandText = "SP_JUDICIAL_PETICIONES"
                SSCommand3.CommandType = CommandType.StoredProcedure
                SSCommand3.Parameters.Add("v_resultado", SqlDbType.VarChar).Value = e.Item.Cells(3).Text
                SSCommand3.Parameters.Add("V_Bandera", SqlDbType.Decimal).Value = 5
                Dim dtejud3 As DataRow = Consulta_Procedure(SSCommand3, "Judicial").Rows(0)
                cbdiligencia.SelectedValue = dtejud3.Item("tipodili")
                cbParticipante.SelectedValue = dtejud3.Item("persona")
                DPDteDiligencia.SelectedDate = CType(dtejud3.Item("dteprogra"), DateTime)
                CbResultadodili.DataSource = valoer(15, dtejud3.Item("tipodili"), tmpCredito("PR_MC_PRODUCTO"))
                CbResultadodili.DataBind()
                CbResultadodili.SelectedValue = dtejud3.Item("tresultado")
                CbSubResultadodili.Text = dtejud3.Item("subres")
                txtobserDili.Text = dtejud3.Item("observacionesdili")
                If dtejud3.Item("tipodili") = "EMPLAZAMIENTO Y EMBARGO" Then
                    CbGarantiaJud.Visible = True
                    lblgarantia.Visible = True
                    CbGarantiaJud.SelectedValue = dtejud3.Item("garantia")
                End If


            End If
            ABRE(WinJudicial)
            e.Canceled = True
        ElseIf e.CommandName = "Terminar" Then
            CbMotivo.DataSource = valoer(17, "", tmpCredito("PR_MC_PRODUCTO"))
            CbMotivo.DataBind()
            Dim SSCommand As New SqlCommand
            SSCommand.CommandText = "SP_CIERRA_JUICIOS"
            SSCommand.CommandType = CommandType.StoredProcedure
            SSCommand.Parameters.Add("V_IDJUICIO", SqlDbType.Decimal).Value = Session("IDJUD")
            SSCommand.Parameters.Add("V_BANDERA", SqlDbType.VarChar).Value = 2
            Dim dtejud As DataTable = Consulta_Procedure(SSCommand, "Judicial")
            If dtejud.Rows.Count > 0 Then
                CbMotivo.SelectedValue = dtejud.Rows(0).Item("motivo")
                DpCierre.SelectedDate = CType(dtejud.Rows(0).Item("fecha"), DateTime)
                TxtDescrip.Text = dtejud.Rows(0).Item("descr")
            End If
            ABRE(wincierre)
            e.Canceled = True
        End If
    End Sub

    Private Sub BtnCancelar_Click(sender As Object, e As EventArgs) Handles BtnCancelar.Click
        CIERRA(WinJudicial)
        reset()

    End Sub
    Protected Sub GVParticipantes_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs)
        If Session("Credito") IsNot Nothing Then
            GVParticipantes.DataSource = valoer(1, tmpCredito("PR_MC_CREDITO"), tmpCredito("PR_MC_PRODUCTO"))
        End If
    End Sub

    Private Sub M_Gestion_Judicial_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then

            TxtEtapaProc.Text = "ACTOS PREPARATORIOS"
            If Session("Credito") IsNot Nothing Then
                Dim dset As DataTable = valoer(6, tmpCredito("PR_MC_CREDITO"), tmpCredito("PR_MC_PRODUCTO"))
                CbJuzgado.DataSource = valoer(3, "", tmpCredito("PR_MC_PRODUCTO"))
                CbJuzgado.DataBind()
                CbTipoJuicio.DataSource = valoer(4, "", tmpCredito("PR_MC_PRODUCTO"))
                CbTipoJuicio.DataBind()
                DPRegistro.MaxDate = Today
                DpPromocion.MaxDate = Today
                DpAcuerdo.MaxDate = Today
                ImgAlerta.Visible = False
                LblAlerta.Visible = False
                Session("IDJUD") = -1
                ' UpnGen.PostBackControls() = {"BtnAceptar", "btnaceptaravnrt"}
                If dset.Rows.Count <> 0 Then
                    BtnGuardar.Enabled = False
                    DPRegistro.Enabled = False
                    CbJuzgado.Enabled = False
                    CbTipoJuicio.Enabled = False
                    TxtNoExpediente.Enabled = False
                    Dim dfila As DataRow = dset.Rows(0)
                    TxtEtapaProc.Text = dfila.Item("Etapa")

                    TxtNoExpediente.Text = dfila.Item("expediente")
                    CbJuzgado.SelectedItem.Text = dfila.Item("juzgado")
                    CbTipoJuicio.SelectedValue = dfila.Item("juicio")
                    DPRegistro.SelectedDate = CType(dfila.Item("dtepresentacion"), DateTime)
                    lblTipoAct.Text = dfila.Item("actuacion")
                    LblEstatus.Text = dfila.Item("estatus")
                    LblGastosCobranza.Text = dfila.Item("gastos")
                    lblDteUltimaAc.Text = dfila.Item("dteultimaact")
                    LblDiasFalta.Text = dfila.Item("dias")
                    If LblDiasFalta.Text <= 3 Then
                        ImgAlerta.Visible = True
                        LblAlerta.Visible = True
                        LblAlerta.Text = LblDiasFalta.Text & "Dias Para Caducar"
                    End If
                    If dfila.Item("dteultimaact") = "Sin Actuacion" Then
                        If DateDiff(DateInterval.Day, CType(dfila.Item("dtepresentacion"), DateTime), Today) - dfila.Item("dias2") >= 90 Then
                            ImgAlerta.Visible = True
                            LblAlerta.Visible = True
                            LblAlerta.Text += " 90 o más dias de inactividad"
                        End If
                    Else
                        If DateDiff(DateInterval.Day, CType(dfila.Item("dteultimaact"), DateTime), Today) - dfila.Item("dias2") >= 90 Then
                            ImgAlerta.Visible = True
                            LblAlerta.Visible = True
                            LblAlerta.Text += " 90 o más dias de inactividad"
                        End If
                    End If
                    If dfila.Item("gara") <> -1 Then
                        TDGarantias.BgColor = "Green"
                    End If
                    If dfila.Item("conve") <> -1 Then
                        TDConvenios.BgColor = "Green"
                    End If
                    If dfila.Item("tra") <> -1 Then
                        TDTramites.BgColor = "Green"
                    End If

                    If dfila.Item("id") <> -1 Then
                        Session("IDJUD") = dfila.Item("id")
                        LblDiasActivo.Text = DateDiff(DateInterval.Day, CType(dfila.Item("dtepresentacion"), DateTime), Today) - dfila.Item("dias2")
                    End If
                    If dfila.Item("declarativa") = 1 Then
                        Tddias.InnerText = "Días para prescripción"
                    End If
                    If dfila.Item("cierre") = 1 And tmpUSUARIO("CAT_LO_PERFIL") = 31 Then
                        BtnConfirmarCierre.Visible = True
                        ImgAlerta.Visible = True
                        LblAlerta.Visible = True
                        LblAlerta.Text += " Validar Cierre Juicio"
                        BtnAceptarCierre.Enabled = False
                    Else
                        BtnConfirmarCierre.Visible = False
                    End If
                End If

                If Session("MensajeFinal") Then
                    Session.Remove("MensajeFinal")
                    showModal(Notificacion2, "ok", "Correcto", "Registro exitoso")
                End If
            End If
        End If
    End Sub

    Private Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click
        Dim bandera As Integer = 5

        If TxtNoExpediente.Text = "" Then
            showModal(Notificacion, "warning", "Falta", "Ingrese el numero de expediente")
        ElseIf Split(TxtNoExpediente.TextWithLiterals, "/")(1) > Today.Year Then
            showModal(Notificacion, "warning", "Falta", "El año asociado al número de  expediente no puede ser mayor al actual")
        ElseIf CbJuzgado.SelectedItem.Text = "Seleccione" Then
            showModal(Notificacion, "warning", "Falta", "Seleccione un juzgado")
        ElseIf CbTipoJuicio.SelectedValue = 0 Then
            showModal(Notificacion, "warning", "Falta", "Seleccione un tipo de juicio")
        ElseIf DPRegistro.SelectedDate Is Nothing Then
            showModal(Notificacion, "warning", "Falta", "Seleccione una fecha de registro")
        Else
            If TxtNoExpediente.Text.Length < 8 Then
                TxtNoExpediente.Text = TxtNoExpediente.Text.PadLeft(8, "0")
            End If
            If edit Then
                bandera = 20
                Session("IDJUD") = valoer(bandera, Session("IDJUD"), tmpCredito("PR_MC_PRODUCTO"), TxtNoExpediente.Text, CbJuzgado.SelectedItem.Text, CType(DPRegistro.SelectedDate, DateTime).ToShortDateString, TxtEtapaProc.Text, CbTipoJuicio.SelectedItem.Text).Rows(0).Item(0)

                CbJuzgado.Enabled = False
                CbTipoJuicio.Enabled = False
                TxtNoExpediente.Enabled = False
                DPRadicacion.Enabled = False
                BtnEditar.Text = "Editar"
                edit = False
                showModal(Notificacion, "ok", "Exito", "Juicio Actualizado")
                BtnGuardar.Enabled = False
            Else
                CbJuzgado.Enabled = False
                CbTipoJuicio.Enabled = False
                TxtNoExpediente.Enabled = False
                DPRadicacion.Enabled = False
                bandera = 5
                Session("IDJUD") = valoer(bandera, tmpCredito("PR_MC_CREDITO"), tmpCredito("PR_MC_PRODUCTO"), TxtNoExpediente.Text, CbJuzgado.SelectedItem.Text, CType(DPRegistro.SelectedDate, DateTime).ToShortDateString, TxtEtapaProc.Text, CbTipoJuicio.SelectedItem.Text).Rows(0).Item(0)
                showModal(Notificacion, "ok", "Exito", "Juicio Creado")
                BtnGuardar.Enabled = False
            End If



        End If

    End Sub


    Private Sub btnRetroceder_Click(sender As Object, e As EventArgs) Handles btnRetroceder.Click
        CbAvnRt.DataSource = valoer(10, "", "", "", "", "", TxtEtapaProc.Text, CbTipoJuicio.SelectedItem.Text)
        CbAvnRt.DataBind()
        ABRE(WinJudicial)
        CbAvnRt.Visible = True
        BtnAceptar.Text = "Añadir"
        gridregresa.Visible = True
    End Sub

    Private Sub BtnAceptar_Click(sender As Object, e As EventArgs) Handles BtnAceptar.Click
        Dim estado As String = ""
        Dim fecha As String = ""
        Dim fecha2 As String = ""
        Dim id As String = ""
        Dim conse As String = ""
        Dim bande As Integer = 0
        Dim vpart As String = CbPersona.SelectedValue

        If CbAvnRt.SelectedIndex = 1 Then
            Dim SSCommand2 As New SqlCommand
            SSCommand2.CommandText = "SP_JUDICIAL_PETICIONES"
            SSCommand2.CommandType = CommandType.StoredProcedure
            SSCommand2.Parameters.Add("v_id", SqlDbType.Decimal).Value = Session("IDJUD")
            SSCommand2.Parameters.Add("V_Bandera", SqlDbType.Decimal).Value = 19
            Consulta_Procedure(SSCommand2, "Judicial")
            GvInpulsos.Rebind()
            reset()
            CIERRA(WinJudicial)
            Response.Redirect("Judicial.aspx")

        Else

            If CbPromocion.SelectedValue = 0 Then
                showModal(Notificacion2, "deny", "Error", "Seleccione una promoción")
            ElseIf DpPromocion.SelectedDate Is Nothing Then
                showModal(Notificacion2, "deny", "Error", "Seleccione una fecha de promoción")
            ElseIf CbAcuerdo.SelectedValue <> 0 And DpAcuerdo.SelectedDate Is Nothing Then
                showModal(Notificacion2, "deny", "Error", "Seleccione una fecha de acuerdo")
            ElseIf CbAcuerdo.SelectedValue <> 0 And CbResultado.SelectedIndex = 0 Then

                showModal(Notificacion2, "deny", "Error", "Seleccione un resultado")

            ElseIf CbAcuerdo.SelectedValue <> 0 And DpAcuerdo.SelectedDate < DpPromocion.SelectedDate Then
                showModal(Notificacion2, "deny", "Error", "La fecha de resultado no puede ser anterior a la de promoción")
            ElseIf CbAvnRt.Visible = True And CbAvnRt.SelectedIndex = 0 Then
                showModal(Notificacion2, "deny", "Error", "Seleccione la etapa a retroceder")

            ElseIf ExoVen.Visible And CbPersona.SelectedValue = "0" Then
                showModal(Notificacion2, "deny", "Error", "Seleccione la persona a exhortar")
            ElseIf ExoVen.Visible And MTBNumEx.Text = "" Then
                showModal(Notificacion2, "deny", "Error", "Ingrese numero de exhorto")
            ElseIf ExoVen.Visible And CbCiudad.SelectedValue = "AAAA" Then
                showModal(Notificacion2, "deny", "Error", "Seleccione una ciudad")
            ElseIf ExoVen.Visible And CbJuzgadoEx.SelectedIndex = 0 Then
                showModal(Notificacion2, "deny", "Error", "Seleccione un juzgado")

            ElseIf ExoVen.Visible And DPRadicacion.SelectedDate Is Nothing Then
                showModal(Notificacion2, "deny", "Error", "Seleccione una fecha de radicacion")
            Else
                If CbAcuerdo.SelectedValue <> 0 Then
                    estado = "Acordada"
                Else
                    estado = "Presentada"
                End If
                If DpAcuerdo.SelectedDate IsNot Nothing Then
                    fecha = CType(DpAcuerdo.SelectedDate, DateTime).ToShortDateString()
                End If
                If DpRegresaEX.SelectedDate IsNot Nothing Then
                    fecha2 = CType(DpAcuerdo.SelectedDate, DateTime).ToShortDateString()
                End If
                If lblconse.Text <> "" Then
                    id = Split(lblconse.Text, "-")(0)
                    conse = Split(lblconse.Text, "-")(1)
                    bande = 3
                ElseIf BtnAceptar.Text = "Añadir" Then
                    bande = 18
                    id = Session("IDJUD")
                    conse = 0
                    vpart = CbAvnRt.SelectedItem.Text
                Else
                    id = Session("IDJUD")
                    conse = 0
                End If
                Dim SSCommand As New SqlCommand
                SSCommand.CommandText = "SP_JUDICIAL_PETICIONES"
                SSCommand.CommandType = CommandType.StoredProcedure
                SSCommand.Parameters.Add("v_id", SqlDbType.Decimal).Value = id
                SSCommand.Parameters.Add("v_consecutivo", SqlDbType.Decimal).Value = conse
                SSCommand.Parameters.Add("V_PETICION", SqlDbType.VarChar).Value = CbPromocion.SelectedValue
                SSCommand.Parameters.Add("V_DTEPETICION", SqlDbType.VarChar).Value = CType(DpPromocion.SelectedDate, DateTime).ToShortDateString
                SSCommand.Parameters.Add("V_ACUERDO", SqlDbType.VarChar).Value = CbAcuerdo.SelectedValue
                SSCommand.Parameters.Add("V_DTEACUERDO", SqlDbType.VarChar).Value = fecha
                SSCommand.Parameters.Add("v_ESTADO", SqlDbType.VarChar).Value = estado
                SSCommand.Parameters.Add("V_RESULTADO", SqlDbType.VarChar).Value = CbResultado.SelectedValue
                SSCommand.Parameters.Add("V_OBSERVACIONES", SqlDbType.VarChar).Value = TxtObservaciones.Text
                SSCommand.Parameters.Add("V_Bandera", SqlDbType.Decimal).Value = bande
                SSCommand.Parameters.Add("v_participante", SqlDbType.VarChar).Value = vpart
                If ExoVen.Visible = True Then

                    SSCommand.Parameters.Add("v_noexhorto", SqlDbType.VarChar).Value = MTBNumEx.Text
                    SSCommand.Parameters.Add("v_ciudad", SqlDbType.VarChar).Value = CbCiudad.SelectedValue
                    SSCommand.Parameters.Add("v_juzgado", SqlDbType.VarChar).Value = CbJuzgadoEx.SelectedValue
                    SSCommand.Parameters.Add("v_dteradica", SqlDbType.VarChar).Value = CType(DPRadicacion.SelectedDate, DateTime).ToShortDateString
                    SSCommand.Parameters.Add("v_dteregreso", SqlDbType.VarChar).Value = fecha2
                    SSCommand.Parameters.Add("v_observacionesex", SqlDbType.VarChar).Value = TxtObserEx.Text
                End If

                If DiliVen.Visible = True Then
                    SSCommand.Parameters.Add("v_participante", SqlDbType.VarChar).Value = cbParticipante.SelectedValue
                    SSCommand.Parameters.Add("v_tipodili", SqlDbType.VarChar).Value = cbdiligencia.SelectedValue
                    SSCommand.Parameters.Add("v_dteprogra", SqlDbType.VarChar).Value = CType(DPDteDiligencia.SelectedDate, DateTime).ToShortDateString
                    SSCommand.Parameters.Add("v_tiporesul", SqlDbType.Decimal).Value = CbResultadodili.SelectedValue
                    If CbSubResultadodili.Visible = True Then
                        SSCommand.Parameters.Add("v_subres", SqlDbType.VarChar).Value = CbSubResultadodili.SelectedItem.Text
                    Else
                        SSCommand.Parameters.Add("v_subres", SqlDbType.VarChar).Value = ""
                    End If
                    SSCommand.Parameters.Add("vobservacionesdili", SqlDbType.VarChar).Value = txtobserDili.Text
                    SSCommand.Parameters.Add("v_garantiadili", SqlDbType.VarChar).Value = CbGarantiaJud.SelectedValue

                End If




                '  LblDiasFalta.Text = dtejud.Rows(0).Item(1)
                '  UpnGen.RaisePostBackEvent("5")
                If BtnAceptar.Text = "Aceptar" Then
                    Dim dtejud As DataTable = Consulta_Procedure(SSCommand, "Judicial")
                    TxtEtapaProc.Text = dtejud.Rows(0).Item(0)
                    GvInpulsos.Rebind()
                    reset()
                    CIERRA(WinJudicial)
                    Response.Redirect("Judicial.aspx")
                    Session("MensajeFinal") = True
                ElseIf BtnAceptar.Text = "ACEPTAR" Then
                    Dim SSCommand3 As New SqlCommand
                    SSCommand3.CommandText = "SP_JUDICIAL_PETICIONES"
                    SSCommand3.CommandType = CommandType.StoredProcedure
                    SSCommand3.Parameters.Add("v_id", SqlDbType.Decimal).Value = id
                    SSCommand3.Parameters.Add("V_Bandera", SqlDbType.Decimal).Value = 19
                    Dim dtejud As DataTable = Consulta_Procedure(SSCommand3, "Judicial")
                    For Each row As DataRow In tab.Rows
                        Dim SSCommand4 As New SqlCommand
                        SSCommand4.CommandText = "SP_JUDICIAL_PETICIONES"
                        SSCommand4.CommandType = CommandType.StoredProcedure
                        SSCommand4.Parameters.Add("v_id", SqlDbType.Decimal).Value = row.Item("id")
                        SSCommand4.Parameters.Add("v_consecutivo", SqlDbType.Decimal).Value = 0
                        SSCommand4.Parameters.Add("V_PETICION", SqlDbType.VarChar).Value = row.Item("PetVal")
                        SSCommand4.Parameters.Add("V_DTEPETICION", SqlDbType.VarChar).Value = row.Item("DtePeticion")
                        SSCommand4.Parameters.Add("V_ACUERDO", SqlDbType.VarChar).Value = row.Item("AcuVal")
                        SSCommand4.Parameters.Add("V_DTEACUERDO", SqlDbType.VarChar).Value = row.Item("DteAtuacion")
                        SSCommand4.Parameters.Add("v_ESTADO", SqlDbType.VarChar).Value = row.Item("EstadoP")
                        SSCommand4.Parameters.Add("V_RESULTADO", SqlDbType.VarChar).Value = row.Item("ResVal")
                        SSCommand4.Parameters.Add("V_OBSERVACIONES", SqlDbType.VarChar).Value = row.Item("OBSERVACIONES")
                        SSCommand4.Parameters.Add("V_Bandera", SqlDbType.Decimal).Value = 0
                        Dim dtejud2 As DataTable = Consulta_Procedure(SSCommand4, "Judicial")
                    Next
                    Response.Redirect("Judicial.aspx")
                Else
                    Dim fila = tab.NewRow()
                    fila("ID") = id
                    fila("NombreP") = CbPromocion.SelectedItem.Text
                    fila("DtePeticion") = CType(DpPromocion.SelectedDate, DateTime).ToShortDateString
                    fila("NombreA") = CbAcuerdo.SelectedItem.Text
                    fila("DteAtuacion") = fecha
                    fila("EstadoP") = estado
                    fila("ResultadoA") = CbResultado.SelectedItem.Text
                    fila("OBSERVACIONES") = TxtObservaciones.Text
                    fila("PetVal") = CbPromocion.SelectedValue
                    fila("AcuVal") = CbAcuerdo.SelectedValue
                    fila("ResVal") = CbResultado.SelectedValue
                    tab.Rows.Add(fila)
                    gridregresa.Rebind()
                    If gridregresa.Items.Count = CbAvnRt.SelectedIndex - 1 Then
                        BtnAceptar.Text = "ACEPTAR"
                    End If

                End If
            End If
        End If
    End Sub
    Private Sub reset()
        CbPromocion.SelectedIndex = 0
        CbAcuerdo.SelectedIndex = 0
        CbResultado.SelectedIndex = 0
        DpPromocion.SelectedDate = Nothing
        DpAcuerdo.SelectedDate = Nothing
        TxtObservaciones.Text = ""
        CbAvnRt.Visible = False
        lblconse.Text = ""
        ExoVen.Visible = False
        DiliVen.Visible = False
        divInmuele.Visible = False
        divmueble.Visible = False
        lbltope.Text = ""
        lbltope.Visible = False
        BtnAceptar.Text = "Aceptar"
        tab = makestable()
    End Sub

    Private Sub CbAcuerdo_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles CbAcuerdo.SelectedIndexChanged
        If BtnAceptar.Text = "Añadir" Then
            CbResultado.DataSource = valoer(9, "", "", "", "", "", CbAvnRt.Items.ElementAt(CbAvnRt.SelectedIndex - 1).Text, CbTipoJuicio.SelectedValue, CbPromocion.SelectedValue, CbAcuerdo.SelectedValue)
        Else
            CbResultado.DataSource = valoer(9, "", "", "", "", "", TxtEtapaProc.Text, CbTipoJuicio.SelectedValue, CbPromocion.SelectedValue, CbAcuerdo.SelectedValue)
        End If

        CbResultado.DataBind()
        If CbAcuerdo.SelectedValue = 8 Then
            ExoVen.Visible = True
            CbPersona.DataSource = valoer(12, tmpCredito("PR_MC_CREDITO"), tmpCredito("PR_MC_PRODUCTO"))
            CbPersona.DataBind()
            CbCiudad.DataSource = valoer(11, "", tmpCredito("PR_MC_PRODUCTO"))
            CbCiudad.DataBind()
        Else
            ExoVen.Visible = False
        End If
    End Sub

    Private Sub CbCiudad_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles CbCiudad.SelectedIndexChanged
        CbJuzgadoEx.DataSource = valoer(13, CbCiudad.SelectedValue, tmpCredito("PR_MC_PRODUCTO"))
        CbJuzgadoEx.DataBind()
    End Sub

    Private Sub CbPromocion_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles CbPromocion.SelectedIndexChanged
        If BtnAceptar.Text = "Añadir" Then
            CbAcuerdo.DataSource = valoer(8, "", "", "", "", "", CbAvnRt.Items.ElementAt(CbAvnRt.SelectedIndex - 1).Text, CbTipoJuicio.SelectedValue, CbPromocion.SelectedValue)
        Else
            CbAcuerdo.DataSource = valoer(8, "", "", "", "", "", TxtEtapaProc.Text, CbTipoJuicio.SelectedValue, CbPromocion.SelectedValue)
        End If

        CbAcuerdo.DataBind()
        If CbPromocion.SelectedValue = 4 Or CbPromocion.SelectedValue = 10 Then
            DiliVen.Visible = True
            ExoVen.Visible = False
            cbParticipante.DataSource = valoer(12, tmpCredito("PR_MC_CREDITO"), tmpCredito("PR_MC_PRODUCTO"))
            cbParticipante.DataBind()
            cbdiligencia.DataSource = valoer(14, "", tmpCredito("PR_MC_PRODUCTO"))
            cbdiligencia.DataBind()
            If CbPromocion.SelectedValue = 10 Then
                lbllbsub.Visible = False
                LblTipoREs.Visible = False
                CbResultadodili.Visible = False
                CbSubResultadodili.Visible = False
            End If
        Else
            DiliVen.Visible = False
            ExoVen.Visible = False
        End If

    End Sub

    Private Sub cbdiligencia_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cbdiligencia.SelectedIndexChanged
        Dim dset As DataTable = valoer(15, cbdiligencia.SelectedValue, tmpCredito("PR_MC_PRODUCTO"))
        CbResultadodili.DataSource = dset
        CbResultadodili.DataBind()
        If cbdiligencia.SelectedValue = "EMPLAZAMIENTO Y EMBARGO" Then
            CbGarantiaJud.Visible = True
            lblgarantia.Visible = True
            CbGarantiaJud.DataSource = valoer(18, Session("IDJUD"), tmpCredito("PR_MC_PRODUCTO"))
            CbGarantiaJud.DataBind()

        Else
            CbGarantiaJud.Visible = False
            lblgarantia.Visible = False
        End If
    End Sub

    Private Sub CbResultadodili_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles CbResultadodili.SelectedIndexChanged
        CbSubResultadodili.DataSource = valoer(16, CbResultadodili.SelectedItem.Text, tmpCredito("PR_MC_PRODUCTO"))
        CbSubResultadodili.DataBind()

    End Sub

    Private Sub BtnCancelarCierre_Click(sender As Object, e As EventArgs) Handles BtnCancelarCierre.Click
        CIERRA(wincierre)
    End Sub

    Private Sub BtnAceptarCierre_Click(sender As Object, e As EventArgs) Handles BtnAceptarCierre.Click
        If DpCierre.SelectedDate Is Nothing Then
            showModal(Notification3, "deny", "Error", "Seleccione una fecha de cierre")
        ElseIf CbMotivo.SelectedIndex = 0 Then
            showModal(Notification3, "deny", "Error", "Seleccione un motivo")
        ElseIf TxtDescrip.Text = "" Then
            showModal(Notification3, "deny", "Error", "Ingrese una descripción")
        Else
            Dim SSCommand As New SqlCommand
            SSCommand.CommandText = "SP_CIERRA_JUICIOS"
            SSCommand.CommandType = CommandType.StoredProcedure
            SSCommand.Parameters.Add("V_IDJUICIO", SqlDbType.Decimal).Value = Session("IDJUD")
            SSCommand.Parameters.Add("V_DTEJUICIO", SqlDbType.VarChar).Value = CType(DpCierre.SelectedDate, DateTime).ToShortDateString
            SSCommand.Parameters.Add("V_MOTIVO", SqlDbType.Decimal).Value = CbMotivo.SelectedValue
            SSCommand.Parameters.Add("V_DESCRIPCION", SqlDbType.VarChar).Value = TxtDescrip.Text
            SSCommand.Parameters.Add("V_BANDERA", SqlDbType.VarChar).Value = 0
            Dim dtejud As DataTable = Consulta_Procedure(SSCommand, "Judicial")
            CIERRA(wincierre)
            If dtejud.Rows(0).Item(0) = 1 Then
                showModal(Notification3, "info", "Ok", "Cierre pendiente de validar por superior")
            Else
                showModal(Notification3, "info", "Ok", "Juicio Cerrado")
            End If

        End If

    End Sub

    Private Sub cbTembargo_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cbTembargo.SelectedIndexChanged
        If cbTembargo.SelectedValue = 1 Then
            divmueble.Visible = True
            divInmuele.Visible = False
        ElseIf cbTembargo.SelectedValue = 2 Then
            divmueble.Visible = False
            divInmuele.Visible = True
        Else
            divmueble.Visible = False
            divInmuele.Visible = False
        End If
    End Sub

    Private Sub CpExterior_ColorChanged(sender As Object, e As EventArgs) Handles CpExterior.ColorChanged
        Dim s As Integer = CpExterior.SelectedColor.ToArgb
        CpExterior.SelectedColor = Drawing.Color.FromArgb(s)


    End Sub
    Protected Sub GvGarantias_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs)
        Dim SSCommand3 As New SqlCommand
        SSCommand3.CommandText = "SP_JUDICIAL_PETICIONES"
        SSCommand3.CommandType = CommandType.StoredProcedure
        SSCommand3.Parameters.Add("v_id", SqlDbType.VarChar).Value = Session("IDJUD")
        SSCommand3.Parameters.Add("V_Bandera", SqlDbType.Decimal).Value = 6
        GvGarantias.DataSource = Consulta_Procedure(SSCommand3, "Judicial")
    End Sub

    Private Sub BtnGaranias_Click(sender As Object, e As EventArgs) Handles BtnGaranias.Click
        ABRE(Garantias)
    End Sub

    Private Sub GvGarantias_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles GvGarantias.ItemCommand
        If e.CommandName = "InitInsert" Then
            lblEmbargo.Visible = True
            cbTembargo.Visible = True
            GvGarantias.Visible = False
            BtnCancelarGara.Visible = True
            BtnAceptarGara.Visible = True
            e.Canceled = True
        ElseIf e.CommandName = "Edit" Then
            Dim SSCommand3 As New SqlCommand
            SSCommand3.CommandText = "SP_JUDICIAL_PETICIONES"
            SSCommand3.CommandType = CommandType.StoredProcedure
            SSCommand3.Parameters.Add("v_resultado", SqlDbType.VarChar).Value = e.Item.Cells(3).Text
            SSCommand3.Parameters.Add("v_id", SqlDbType.VarChar).Value = Session("IDJUD")
            SSCommand3.Parameters.Add("V_Bandera", SqlDbType.Decimal).Value = 10
            Dim dtejud3 As DataRow = Consulta_Procedure(SSCommand3, "Judicial").Rows(0)
            LblConsegara.Text = e.Item.Cells(3).Text
            lblEmbargo.Visible = True
            cbTembargo.Visible = True
            cbTembargo.SelectedValue = dtejud3.Item("muebleinmueble")
            If dtejud3.Item("muebleinmueble") = 1 Then
                divmueble.Visible = True
                txtipo.Text = dtejud3.Item("tipomueble")
                ' txtmueble.Text = dtejud3.Item("mueble")
                txtmodelo.Text = dtejud3.Item("modelo")
                txtmarca.Text = dtejud3.Item("marca")
                txtserie.Text = dtejud3.Item("serie")
                txtdescripmueble.Text = dtejud3.Item("descrip")
                txtDepositario.Text = dtejud3.Item("depositario")
                TxtAlmacen.Text = dtejud3.Item("alamacen")
                DPEntrada.SelectedDate = CType(dtejud3.Item("dteentra"), DateTime)
                txtLinea.Text = dtejud3.Item("linea")
                CpExterior.SelectedColor = Drawing.Color.FromArgb(dtejud3.Item("color"))
                txtUso.Text = dtejud3.Item("uso")
                TxtVehicular.Text = dtejud3.Item("clave")
                Txtversion.Text = dtejud3.Item("version")
                TxtMotor.Text = dtejud3.Item("motor")
            ElseIf dtejud3.Item("muebleinmueble") = 2 Then
                divInmuele.Visible = True
                TxtInmueble.Text = dtejud3.Item("tipoinmueble")
                Txtubicacion.Text = dtejud3.Item("ubicacion")
                ' TxtGravamen.Text = dtejud3.Item("gravamen")
                TxtRegistrales.Text = dtejud3.Item("datos")
                TxtEscritura.Text = dtejud3.Item("escritura")
                TxtFolio.Text = dtejud3.Item("folio")
                TxtDomicilio.Text = dtejud3.Item("domicilio")
                TxtEstado.Text = dtejud3.Item("estado")
                TxtMunicipio.Text = dtejud3.Item("municipio")
                txtcolonia.Text = dtejud3.Item("colonia")
                Txtnoexterior.Text = dtejud3.Item("exterior")
                txtinterior.Text = dtejud3.Item("interior")
            End If

            GvGarantias.Visible = False
            BtnCancelarGara.Visible = True
            BtnAceptarGara.Visible = True
            e.Canceled = True
        End If
    End Sub

    Private Sub BtnCancelarGara_Click(sender As Object, e As EventArgs) Handles BtnCancelarGara.Click
        lblEmbargo.Visible = False
        cbTembargo.Visible = False
        GvGarantias.Visible = True
        divInmuele.Visible = False
        divmueble.Visible = False
        BtnCancelarGara.Visible = False
        BtnAceptarGara.Visible = False
        cbTembargo.SelectedIndex = 0
        LblConsegara.Text = ""
    End Sub

    Private Sub BtnConvenios_Click(sender As Object, e As EventArgs) Handles BtnConvenios.Click
        ABRE(winConvenios)
    End Sub

    Private Sub BtnTramites_Click(sender As Object, e As EventArgs) Handles BtnTramites.Click
        ABRE(wintramites)
    End Sub

    Protected Sub gridtramites_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs)
        Dim SSCommand3 As New SqlCommand
        SSCommand3.CommandText = "SP_JUDICIAL_PETICIONES"
        SSCommand3.CommandType = CommandType.StoredProcedure
        SSCommand3.Parameters.Add("v_id", SqlDbType.VarChar).Value = Session("IDJUD")
        SSCommand3.Parameters.Add("V_Bandera", SqlDbType.Decimal).Value = 8
        gridtramites.DataSource = Consulta_Procedure(SSCommand3, "Judicial")

    End Sub



    Private Sub btncancelarconve_Click(sender As Object, e As EventArgs) Handles btncancelarconve.Click
        If btncancelarconve.Text = "Editar" Then
            btncancelarconve.Text = "Cancelar"
            DPFechaConvenio.Enabled = True
            txttotal.Enabled = True
            TxtObservacionesconve.Enabled = True
            txtincumplimiento.Enabled = True
            TxtPagos.Enabled = True
            txttotaltotal.Enabled = True
            CbGarantiaconve.Enabled = True
            btnaceptarconve.Enabled = True
        Else
            btncancelarconve.Text = "Editar"
            DPFechaConvenio.Enabled = False
            txttotal.Enabled = False
            TxtObservacionesconve.Enabled = False
            txtincumplimiento.Enabled = False
            TxtPagos.Enabled = False
            txttotaltotal.Enabled = False
            CbGarantiaconve.Enabled = False
            btnaceptarconve.Enabled = False
        End If
    End Sub

    Private Sub gridtramites_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles gridtramites.ItemCommand
        If e.CommandName = "InitInsert" Then
            tramites.Visible = True
            gridtramites.Visible = False
            btncancelartrami.Visible = True
            btnaceptartrami.Visible = True
            cbtramite.DataSource = valoer(19, "", tmpCredito("PR_MC_PRODUCTO"))
            cbtramite.DataBind()
            e.Canceled = True
        ElseIf e.CommandName = "Edit" Then
            lblconsetra.Text = e.Item.Cells(3).Text
            tramites.Visible = True
            gridtramites.Visible = False
            btncancelartrami.Visible = True
            btnaceptartrami.Visible = True
            cbtramite.DataSource = valoer(19, "", tmpCredito("PR_MC_PRODUCTO"))
            cbtramite.DataBind()
            Dim SSCommand3 As New SqlCommand
            SSCommand3.CommandText = "SP_JUDICIAL_PETICIONES"
            SSCommand3.CommandType = CommandType.StoredProcedure
            SSCommand3.Parameters.Add("v_consecutivo", SqlDbType.VarChar).Value = e.Item.Cells(3).Text
            SSCommand3.Parameters.Add("v_id", SqlDbType.VarChar).Value = Session("IDJUD")
            SSCommand3.Parameters.Add("V_Bandera", SqlDbType.Decimal).Value = 17
            Dim dtejud3 As DataRow = Consulta_Procedure(SSCommand3, "Judicial").Rows(0)
            cbtramite.SelectedValue = dtejud3.Item("tramite")
            txtobservacionestrami.Text = dtejud3.Item("observa")
            e.Canceled = True
        End If
    End Sub

    Private Sub btncancelartrami_Click(sender As Object, e As EventArgs) Handles btncancelartrami.Click
        tramites.Visible = False
        gridtramites.Visible = True
        btncancelartrami.Visible = False
        btnaceptartrami.Visible = False
        lblconsetra.Text = ""
    End Sub

    Private Sub btnaceptarconve_Click(sender As Object, e As EventArgs) Handles btnaceptarconve.Click
        Dim bande As Integer = 9
        Dim ades As Integer = 0
        If lblconseconv.Text <> "" Then
            ades = lblconseconv.Text

            bande = 13
        End If
        Dim SSCommand3 As New SqlCommand
        SSCommand3.CommandText = "SP_JUDICIAL_PETICIONES"
        SSCommand3.CommandType = CommandType.StoredProcedure
        SSCommand3.Parameters.Add("v_dteconvenio", SqlDbType.VarChar).Value = CType(DPFechaConvenio.SelectedDate, DateTime).ToShortDateString
        SSCommand3.Parameters.Add("v_pagos", SqlDbType.VarChar).Value = TxtPagos.Text
        SSCommand3.Parameters.Add("v_totalpagos", SqlDbType.VarChar).Value = txttotal.Text
        SSCommand3.Parameters.Add("v_totaltotal", SqlDbType.VarChar).Value = txttotaltotal.Text
        SSCommand3.Parameters.Add("v_observaconve", SqlDbType.VarChar).Value = TxtObservacionesconve.Text
        SSCommand3.Parameters.Add("v_garantia", SqlDbType.VarChar).Value = CbGarantiaconve.SelectedValue
        SSCommand3.Parameters.Add("v_incumple", SqlDbType.VarChar).Value = txtincumplimiento.Text
        SSCommand3.Parameters.Add("v_id", SqlDbType.VarChar).Value = Session("IDJUD")
        SSCommand3.Parameters.Add("V_Bandera", SqlDbType.Decimal).Value = bande
        SSCommand3.Parameters.Add("v_consecutivo", SqlDbType.Decimal).Value = ades
        gridtramites.DataSource = Consulta_Procedure(SSCommand3, "Judicial")
        DPFechaConvenio.Enabled = False
        txttotal.Enabled = False
        TxtObservacionesconve.Enabled = False
        txtincumplimiento.Enabled = False
        TxtPagos.Enabled = False
        txttotaltotal.Enabled = False
        CbGarantiaconve.Enabled = False
        btnaceptarconve.Enabled = False
        lblconseconv.Text = ""
        btncancelarconve.Text = "Editar"
    End Sub

    Private Sub BtnAceptarGara_Click(sender As Object, e As EventArgs) Handles BtnAceptarGara.Click
        Dim bande As Integer = 11
        Dim ades As Integer = 0
        If LblConsegara.Text <> "" Then
            ades = LblConsegara.Text

            bande = 12


        End If
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_JUDICIAL_PETICIONES"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("v_id", SqlDbType.Decimal).Value = Session("IDJUD")
        SSCommand.Parameters.Add("v_consecutivo", SqlDbType.Decimal).Value = ades
        SSCommand.Parameters.Add("V_Bandera", SqlDbType.Decimal).Value = bande
        If cbTembargo.SelectedValue = 1 Then
            SSCommand.Parameters.Add("v_muebelinmueble", SqlDbType.VarChar).Value = "1"
            SSCommand.Parameters.Add("v_tipomueble", SqlDbType.VarChar).Value = txtipo.Text
            ' SSCommand.Parameters.Add("v_mueble", SqlDBType.VarChar).Value = txtmueble.Text
            SSCommand.Parameters.Add("v_modelo", SqlDbType.VarChar).Value = txtmodelo.Text
            SSCommand.Parameters.Add("v_marca", SqlDbType.VarChar).Value = txtmarca.Text
            SSCommand.Parameters.Add("v_serie", SqlDbType.VarChar).Value = txtserie.Text
            SSCommand.Parameters.Add("v_descripcion", SqlDbType.VarChar).Value = txtdescripmueble.Text
            SSCommand.Parameters.Add("v_depositario", SqlDbType.VarChar).Value = txtDepositario.Text
            SSCommand.Parameters.Add("v_almacen", SqlDbType.VarChar).Value = TxtAlmacen.Text
            SSCommand.Parameters.Add("v_dteentrada", SqlDbType.VarChar).Value = CType(DPEntrada.SelectedDate, DateTime).ToString("yyyy-MM-dd")
            SSCommand.Parameters.Add("v_linea", SqlDbType.VarChar).Value = txtLinea.Text
            SSCommand.Parameters.Add("v_uso", SqlDbType.VarChar).Value = txtUso.Text
            SSCommand.Parameters.Add("v_color", SqlDbType.VarChar).Value = CpExterior.SelectedColor.ToArgb
            SSCommand.Parameters.Add("v_clave", SqlDbType.VarChar).Value = TxtVehicular.Text
            SSCommand.Parameters.Add("v_version", SqlDbType.VarChar).Value = Txtversion.Text
            SSCommand.Parameters.Add("v_motor", SqlDbType.VarChar).Value = TxtMotor.Text
        ElseIf cbTembargo.SelectedValue = 2 Then
            SSCommand.Parameters.Add("v_muebelinmueble", SqlDbType.VarChar).Value = "2"
            SSCommand.Parameters.Add("v_tipoinmueble", SqlDbType.VarChar).Value = TxtInmueble.Text
            SSCommand.Parameters.Add("v_ubicacion", SqlDbType.VarChar).Value = Txtubicacion.Text
            'SSCommand.Parameters.Add("v_gravamen", SqlDBType.VarChar).Value = TxtGravamen.Text
            SSCommand.Parameters.Add("v_datos", SqlDbType.VarChar).Value = TxtRegistrales.Text
            SSCommand.Parameters.Add("v_escriura", SqlDbType.VarChar).Value = TxtEscritura.Text
            SSCommand.Parameters.Add("v_folio", SqlDbType.VarChar).Value = TxtFolio.Text
            SSCommand.Parameters.Add("v_domicilio", SqlDbType.VarChar).Value = TxtDomicilio.Text
            SSCommand.Parameters.Add("v_estadodili", SqlDbType.VarChar).Value = TxtEstado.Text
            SSCommand.Parameters.Add("v_municipio", SqlDbType.VarChar).Value = TxtMunicipio.Text
            SSCommand.Parameters.Add("v_colonia", SqlDbType.VarChar).Value = txtcolonia.Text
            SSCommand.Parameters.Add("v_exterior", SqlDbType.VarChar).Value = Txtnoexterior.Text
            SSCommand.Parameters.Add("v_interior", SqlDbType.VarChar).Value = txtinterior.Text
        End If
        Dim dtejud As DataTable = Consulta_Procedure(SSCommand, "Judicial")
        lblEmbargo.Visible = False
        cbTembargo.Visible = False
        GvGarantias.Visible = True
        divInmuele.Visible = False
        divmueble.Visible = False
        BtnCancelarGara.Visible = False
        BtnAceptarGara.Visible = False
        cbTembargo.SelectedIndex = 0
        LblConsegara.Text = ""
        GvGarantias.Rebind()

    End Sub

    Private Sub btnaceptartrami_Click(sender As Object, e As EventArgs) Handles btnaceptartrami.Click
        Dim bande As Integer = 15
        Dim ades As Integer = 0
        If lblconsetra.Text <> "" Then
            ades = lblconsetra.Text

            bande = 16


        End If
        Dim SSCommand3 As New SqlCommand
        SSCommand3.CommandText = "SP_JUDICIAL_PETICIONES"
        SSCommand3.CommandType = CommandType.StoredProcedure
        SSCommand3.Parameters.Add("v_tramite", SqlDbType.VarChar).Value = cbtramite.SelectedValue
        SSCommand3.Parameters.Add("v_observtra", SqlDbType.VarChar).Value = txtobservacionestrami.Text
        SSCommand3.Parameters.Add("v_id", SqlDbType.VarChar).Value = Session("IDJUD")
        SSCommand3.Parameters.Add("V_Bandera", SqlDbType.Decimal).Value = bande
        SSCommand3.Parameters.Add("v_consecutivo", SqlDbType.Decimal).Value = ades
        Consulta_Procedure(SSCommand3, "Judicial")
        convenios.Visible = False
        tramites.Visible = False
        gridtramites.Visible = True
        btncancelartrami.Visible = False
        btnaceptartrami.Visible = False
        gridtramites.Rebind()
        lblconsetra.Text = ""
    End Sub

    Private Sub BtnEditar_Click(sender As Object, e As EventArgs) Handles BtnEditar.Click
        If BtnEditar.Text = "Editar" Then
            edit = True
            BtnGuardar.Enabled = True
            DPRegistro.Enabled = True
            CbJuzgado.Enabled = True
            CbTipoJuicio.Enabled = True
            TxtNoExpediente.Enabled = True
            BtnEditar.Text = "Cancelar"
        Else
            BtnGuardar.Enabled = False
            CbJuzgado.Enabled = False
            CbTipoJuicio.Enabled = False
            TxtNoExpediente.Enabled = False
            BtnEditar.Text = "Editar"
            edit = False
        End If
    End Sub

    Private Sub bloque()
        TxtNoExpediente.Enabled = False
        CbJuzgado.Enabled = False
        DPRegistro.Enabled = False
        TxtEtapaProc.Enabled = False
        CbTipoJuicio.Enabled = False
        BtnGuardar.Enabled = False
        btnRetroceder.Enabled = False
        BtnEditar.Enabled = False
        GvInpulsos.Enabled = False
        GvGarantias.Enabled = False
        gridtramites.Enabled = False
    End Sub
    Protected Sub GVJuiciospasados_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs)
        If Session("Credito") IsNot Nothing Then
            Dim tablilla As DataTable = valoer(21, tmpCredito("PR_MC_CREDITO"), tmpCredito("PR_MC_PRODUCTO"))
            GVJuiciospasados.DataSource = tablilla
        If tablilla.Rows.Count = 0 Then
            GVJuiciospasados.Visible = False
        Else
                GVJuiciospasados.Visible = True
            End If
        End If

    End Sub

    Private Sub CbAvnRt_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles CbAvnRt.SelectedIndexChanged
        Dim s As Integer = CbAvnRt.SelectedIndex
        lbltope.Text = "Ingresar " & s - 1 & " impulso(s) positivos "
        lbltope.Visible = True

        If s - 1 = 0 Then
            BtnAceptar.Text = "Aceptar"
        Else
            CbPromocion.DataSource = valoer(7, "", "", "", "", "", CbAvnRt.Items.ElementAt(s - 1).Text, CbTipoJuicio.SelectedValue)
            CbPromocion.DataBind()
        End If
    End Sub

    Private Sub BtnConfirmarCierre_Click(sender As Object, e As EventArgs) Handles BtnConfirmarCierre.Click
        If DpCierre.SelectedDate Is Nothing Then
            showModal(Notification3, "deny", "Error", "Seleccione una fecha de cierre")
        ElseIf CbMotivo.SelectedIndex = 0 Then
            showModal(Notification3, "deny", "Error", "Seleccione un motivo")
        ElseIf TxtDescrip.Text = "" Then
            showModal(Notification3, "deny", "Error", "Ingrese una descripción")
        Else
            Dim SSCommand As New SqlCommand
            SSCommand.CommandText = "SP_CIERRA_JUICIOS"
            SSCommand.CommandType = CommandType.StoredProcedure
            SSCommand.Parameters.Add("V_IDJUICIO", SqlDbType.Decimal).Value = Session("IDJUD")
            SSCommand.Parameters.Add("V_DTEJUICIO", SqlDbType.VarChar).Value = CType(DpCierre.SelectedDate, DateTime).ToShortDateString
            SSCommand.Parameters.Add("V_MOTIVO", SqlDbType.Decimal).Value = CbMotivo.SelectedValue
            SSCommand.Parameters.Add("V_DESCRIPCION", SqlDbType.VarChar).Value = TxtDescrip.Text
            SSCommand.Parameters.Add("V_BANDERA", SqlDbType.VarChar).Value = 1
            Dim dtejud As DataTable = Consulta_Procedure(SSCommand, "Judicial")
            CIERRA(wincierre)
            showModal(Notification3, "info", "Ok", "Juicio Cerrado")
            Response.Redirect("Judicial.aspx")
        End If
    End Sub
    Protected Sub gridregresa_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs)
        gridregresa.DataSource = tab
    End Sub
End Class
