﻿Imports System.Web.Services
Imports Telerik.Web.UI
Imports System.Data
Imports Funciones
Imports Class_CatalogoDispersion
Imports Db
Imports System.Net
Imports System.IO
Imports Newtonsoft.Json
Imports RestSharp
Imports System.Data.SqlClient
Imports Telerik.Web.UI.Calendar
Imports System.Xml

Partial Class CatalogoCampanas
    Inherits System.Web.UI.Page
    Public Property tmpUSUARIO As IDictionary
        Get
            Return CType(Session("USUARIO"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("USUARIO") = value
        End Set
    End Property
    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Administrador", "CatalogoCampanas.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub
    Protected Sub Aviso(ByVal MSJ As String)
        RadAviso.RadAlert(MSJ.Replace(Chr(13), "").Replace(Chr(10), "").Replace("""", "").Replace("'", ""), 440, 155, "AVISO", Nothing)
    End Sub

    Private Sub DDLDispersion_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles DDLDispersion.SelectedIndexChanged
        If DDLDispersion.SelectedValue = 1 Then

            gridAsignacion.Visible = False
            PnlDatos.Visible = True
            Dim SSCommand As New SqlCommand
            SSCommand.CommandText = "SP_CATALOGO_CAMPANAS"
            SSCommand.CommandType = CommandType.StoredProcedure
            SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 14

            Dim DtsDatos As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")

            Session("DispDUMMY") = DtsDatos.Rows(0).Item("MAXIMO").ToString
            gridDispersion.Rebind()
            'RCBUsuarios.DataTextField = "cat_lo_nombre"
            'RCBUsuarios.DataValueField = "cat_lo_usuario"
            'RCBUsuarios.DataSource = TraerUsuarios(DDLInstancia.SelectedValue)
            'RCBUsuarios.DataBind()
        End If
        'Try
        '    seleccionarUsuarios()
        'Catch ex As Exception
        'End Try
    End Sub

    'Private Sub seleccionarUsuarios()
    '    Dim usrs As String = TraerUsuariosDispersion(DDLInstancia.SelectedValue, ddlsubclasificacion.SelectedValue, DDLRespGestion.SelectedValue)(0)(0).ToString
    '    For Each usr As String In usrs.Split(",")
    '        Try
    '            'RCBUsuarios.FindItemByText(usr).Checked = True
    '        Catch ex As Exception
    '        End Try
    '    Next

    '    'If RCBUsuarios.CheckedItems.Count > 0 Then
    '    '    ' btnSimularDispersion.Visible = True
    '    'End If
    'End Sub

    Private Sub DDLInstancia_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles DDLInstancia.SelectedIndexChanged
        PnlDatos.Visible = False
        gridAsignacion.Visible = False
        Funciones.LLENAR_DROP2(32, DDLInstancia.SelectedValue, DDLRespGestion, "V_VALOR", "T_VALOR")
        DDLRespGestion.ClearSelection()
        DDLRespGestion.Enabled = True
        DDLDispersion.ClearSelection()
        DDLDispersion.Enabled = False
    End Sub

    Private Sub DDLRespGestion_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles DDLRespGestion.SelectedIndexChanged
        PnlDatos.Visible = False
        ' ddlsubclasificacion.ClearSelection()
        gridAsignacion.Visible = False
        DDLDispersion.ClearSelection()
        DDLDispersion.Enabled = True

    End Sub

    Private Sub gridDispersion_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles gridDispersion.NeedDataSource
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_CATALOGO_CAMPANAS"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_CAT_DIS_ID", SqlDbType.Decimal).Value = Session("DispDUMMY")
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 17

        Dim DtsDatos As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")

        gridDispersion.DataSource = DtsDatos
    End Sub

    Private Sub gridDispersion_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles gridDispersion.ItemCommand
        Dim comando As String = e.CommandName
        If comando = "PerformInsert" Then
            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)

            Dim valores As Hashtable = Reglas.getGridValues(MyUserControl)

            'Dim v_usuarios As String = ""
            'For Each item As RadComboBoxItem In RCBUsuarios.CheckedItems
            '    v_usuarios &= item.Value & ","
            'Next
            'Try
            '    v_usuarios = v_usuarios.Substring(0, v_usuarios.Length - 1)
            'Catch ex As Exception
            'End Try
            Dim SSCommand As New SqlCommand("SP_CATALOGO_CAMPANAS")
            SSCommand.CommandType = CommandType.StoredProcedure
            ' SSCommand.Parameters.Add("@V_Instancia", SqlDbType.NVarChar).Value = DDLInstancia.SelectedValue
            SSCommand.Parameters.Add("@V_Active", SqlDbType.NVarChar).Value = "1"
            '   SSCommand.Parameters.Add("@V_SubClasificacion", SqlDbType.NVarChar).Value = ddlsubclasificacion.SelectedValue
            SSCommand.Parameters.Add("@V_respgestion", SqlDbType.NVarChar).Value = DDLRespGestion.SelectedValue
            SSCommand.Parameters.Add("@V_CAT_DIS_DESCRIPCIONOPERADOR", SqlDbType.NVarChar).Value = valores("operadorText")
            SSCommand.Parameters.Add("@V_CAT_DIS_DESCRIPCIONCONECTOR", SqlDbType.NVarChar).Value = valores("conectorText")
            SSCommand.Parameters.Add("@V_CAT_DIS_DESCRIPCIONTABLA", SqlDbType.NVarChar).Value = valores("tablaText")
            SSCommand.Parameters.Add("@V_CAT_DIS_DESCRIPCIONCAMPO", SqlDbType.NVarChar).Value = valores("campoText")
            SSCommand.Parameters.Add("@V_Cat_DIS_Valor", SqlDbType.NVarChar).Value = valores("valor")
            SSCommand.Parameters.Add("@V_Cat_DIS_Campo", SqlDbType.NVarChar).Value = valores("campoValue")
            SSCommand.Parameters.Add("@V_CAT_DIS_TABLA", SqlDbType.NVarChar).Value = valores("tablaValue")
            SSCommand.Parameters.Add("@V_Cat_DIS_Operador", SqlDbType.NVarChar).Value = valores("operadorValue")
            SSCommand.Parameters.Add("@V_Cat_DIS_Conector", SqlDbType.NVarChar).Value = valores("conectorValue")
            SSCommand.Parameters.Add("@v_tipo", SqlDbType.NVarChar).Value = valores("tipo")
            '   SSCommand.Parameters.Add("@v_usuarios", SqlDbType.NVarChar).Value = v_usuarios
            'SSCommand.Parameters.Add("@v_internoexterno", SqlDbType.NVarChar).Value = v_internoexterno
            'SSCommand.Parameters.Add("@v_periodo", SqlDbType.NVarChar).Value = v_internoexterno
            'SSCommand.Parameters.Add("@v_duracion", SqlDbType.NVarChar).Value = v_internoexterno

            SSCommand.Parameters.Add("@V_CAT_DIS_ID", SqlDbType.Decimal).Value = Session("DispDUMMY")
            SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 3
            Dim DtsDatos As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")

            'InsertarParametro(DDLInstancia.SelectedValue, ddlsubclasificacion.SelectedValue, DDLRespGestion.SelectedValue, DDLDispersion.SelectedValue, valores("operadorText"), valores("conectorText"), valores("tablaText"), valores("campoText"), valores("valor"),
            '                  valores("campoValue"), valores("tablaValue"), valores("operadorValue"), valores("conectorValue"), valores("tipo"), Session("DispDUMMY"), v_usuarios, RCBTipoUsuarios.SelectedValue)
            gridDispersion.Rebind()
            Aviso("Dispersion Actualizada")
        ElseIf comando = "Edit" Then
            Session("Edit") = True
        ElseIf comando = "Update" Then
            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores As Hashtable = Reglas.getGridValues(MyUserControl)
            Dim SSCommand As New SqlCommand("SP_CATALOGO_CAMPANAS")
            SSCommand.CommandType = CommandType.StoredProcedure
            SSCommand.Parameters.Add("@V_CAT_DIS_DESCRIPCIONOPERADOR", SqlDbType.NVarChar).Value = valores("operadorText")
            SSCommand.Parameters.Add("@V_CAT_DIS_DESCRIPCIONCONECTOR", SqlDbType.NVarChar).Value = valores("conectorText")
            SSCommand.Parameters.Add("@V_CAT_DIS_DESCRIPCIONTABLA", SqlDbType.NVarChar).Value = valores("tablaText")
            SSCommand.Parameters.Add("@V_CAT_DIS_DESCRIPCIONCAMPO", SqlDbType.NVarChar).Value = valores("campoText")
            SSCommand.Parameters.Add("@V_Cat_DIS_Valor", SqlDbType.NVarChar).Value = valores("valor")
            SSCommand.Parameters.Add("@V_Cat_DIS_Campo", SqlDbType.NVarChar).Value = valores("campoValue")
            SSCommand.Parameters.Add("@V_CAT_DIS_TABLA", SqlDbType.NVarChar).Value = valores("tablaValue")
            SSCommand.Parameters.Add("@V_Cat_DIS_Operador", SqlDbType.NVarChar).Value = valores("operadorValue")
            SSCommand.Parameters.Add("@V_Cat_DIS_Conector", SqlDbType.NVarChar).Value = valores("conectorValue")
            SSCommand.Parameters.Add("@v_tipo", SqlDbType.NVarChar).Value = valores("tipo")
            SSCommand.Parameters.Add("@V_CAT_DIS_ID", SqlDbType.Decimal).Value = Session("DispDUMMY")
            SSCommand.Parameters.Add("@V_cat_dis_orden", SqlDbType.Decimal).Value = valores("consecutivo")
            SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 18
            Dim DtsDatos As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
            'ActualizarParametro(valores("operadorText"), valores("conectorText"), valores("tablaText"), valores("campoText"), valores("valor"), valores("campoValue"), valores("tablaValue"), valores("operadorValue"), valores("conectorValue"), valores("tipo"), Session("DispDUMMY"), valores("consecutivo"))

            gridDispersion.Rebind()
            Aviso("Dispersion Actualizada")
        ElseIf comando = "onDelete" Then
            Dim valores(7) As String
            valores(0) = DDLInstancia.SelectedValue
            valores(1) = DDLRespGestion.SelectedValue
            valores(2) = e.Item.Cells.Item(4).Text
            valores(3) = e.Item.Cells.Item(5).Text
            valores(4) = e.Item.Cells.Item(6).Text
            valores(5) = e.Item.Cells.Item(7).Text
            valores(6) = e.Item.Cells.Item(8).Text
            BorrarParametro(valores(0), ddlsubclasificacion.SelectedValue, valores(1), valores(4), valores(6), valores(2), valores(3), valores(5), Session("DispDUMMY"))
            gridDispersion.Rebind()
            Aviso("Dispersion Actualizada")
        End If
    End Sub

    Private Sub btnAcpetarBorrar_Click(sender As Object, e As EventArgs) Handles btnAcpetarBorrar.Click
        ScriptManager.RegisterStartupScript(Me, GetType(Page), "modal", "<script>document.getElementById('modalDelete').style.display='none'</script>", False)
        'Borra la dispersion anterior
        BorrarDispersion(DDLInstancia.SelectedValue, ddlsubclasificacion.SelectedValue, DDLRespGestion.SelectedValue)
        'Se inserta la nueva dispersion
        InsertarDispersion(DDLInstancia.SelectedValue, ddlsubclasificacion.SelectedValue, DDLRespGestion.SelectedValue, DDLDispersion.SelectedValue)
        Aviso("Dispersión actualizada")
        '  btnSimularDispersion.Visible = True
    End Sub

    Private Sub btnCancelarBorrar_Click(sender As Object, e As EventArgs) Handles btnCancelarBorrar.Click
        ScriptManager.RegisterStartupScript(Me, GetType(Page), "modal", "<script>document.getElementById('modalDelete').style.display='none'</script>", False)
        DDLDispersion.ClearSelection()
        'RCBUsuarios.Enabled = False
        'RCBUsuarios.ClearCheckedItems()
        'RCBUsuarios.Items.Clear()

        'RCBTipoUsuarios.SelectedIndex = -1
    End Sub

    Private Sub btnSimularDispersion_Click(sender As Object, e As EventArgs) Handles btnSimularDispersion.Click
        Try
            Dim SSCommand As New SqlCommand
            SSCommand.CommandText = "SP_CATALOGO_CAMPANAS"
            SSCommand.CommandType = CommandType.StoredProcedure
            SSCommand.Parameters.Add("@V_CAT_DIS_ID", SqlDbType.Decimal).Value = Session("DispDUMMY")

            If RCBTipoCampana.SelectedValue = 0 Then
                SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 29
            ElseIf RCBTipoCampana.SelectedValue = 1 Then
                SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 31 'aqui
            ElseIf RCBTipoCampana.SelectedValue = 2 Then
                SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 29
            End If
            Dim DtsDatos As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")

            If DtsDatos.TableName = "Exception" Then
                Aviso("Error en la simulacion: " & DtsDatos.Rows(0)(0).ToString)
            Else
                '  Dim tabla As DataTable = SimularAsignacion(Session("DispDUMMY"), DDLInstancia.SelectedValue)
                Session("simulacion") = DtsDatos
                Aviso("Simulación Terminada")
                gridAsignacion.DataSource = DtsDatos
                pnlResultadoAsignacion.Visible = True
                gridAsignacion.Rebind()
                'btnAplicarAsignacion.Visible = True
                'btnSimularDispersion.Enabled = False
                'gridAsignacion.MasterTableView.ExportToCSV()
            End If
        Catch ex As Exception
            Aviso(ex.Message)
        End Try
    End Sub

    Private Sub ddlsubclasificacion_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles ddlsubclasificacion.SelectedIndexChanged
        PnlDatos.Visible = False
        DDLDispersion.ClearSelection()
        DDLDispersion.Enabled = True
    End Sub

    'Private Sub RCBUsuarios_TextChanged(sender As Object, e As EventArgs) Handles RCBUsuarios.TextChanged
    '    gridAsignacion.Visible = False
    '    If RCBUsuarios.CheckedItems.Count = 0 Then
    '        btnSimularDispersion.Visible = False
    '    ElseIf gridDispersion.Items.Count = 0 Then
    '        Aviso("Imposible Guardar Usuarios. Inserte Datos De Parametrización")
    '        btnSimularDispersion.Visible = False
    '    Else
    '        Dim v_usuarios As String = ""
    '        For Each item As RadComboBoxItem In RCBUsuarios.CheckedItems
    '            v_usuarios &= item.Value & ","
    '        Next
    '        Try
    '            v_usuarios = v_usuarios.Substring(0, v_usuarios.Length - 1)
    '        Catch ex As Exception
    '        End Try
    '        GuardarUsuarios(RCBTipoUsuarios.SelectedValue, ddlsubclasificacion.SelectedValue, DDLRespGestion.SelectedValue, v_usuarios, Session("DispDUMMY"))
    '        Aviso("Usuarios Guardados Correctamente")
    '        '  btnSimularDispersion.Visible = True
    '    End If
    'End Sub

    Private Sub gridAsignacion_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles gridAsignacion.NeedDataSource
        gridAsignacion.DataSource = Session("simulacion")
    End Sub

    Private Sub btnAplicarAsignacion_Click(sender As Object, e As EventArgs) Handles btnAplicarAsignacion.Click
        Dim resultado As String = AplicarAsignacion(tmpUSUARIO("CAT_LO_USUARIO")).Rows(0).Item("MSJ")
        ' If resultado = "ASIGNACION APLICADA" Then
        ' Dim dtscarga As DataTable = TraerAsignacionparaWS()
        'Safi_asignacionCarteraCobranzaMasivo(dtscarga)
        ' End If
        Aviso(resultado)
        btnAplicarAsignacion.Visible = False
        btnSimularDispersion.Enabled = True
    End Sub

    Private Sub CatalogoDispercion_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                'Dim Valores As ArrayList = New ArrayList()
                'Valores.Add(New ListItem("Seleccione", "Seleccione"))
                'Valores.Add(New ListItem(tmpUSUARIO("CAT_LO_DESC_INSTANCIA"), tmpUSUARIO("CAT_LO_INSTANCIA")))
                'Valores.Add(New ListItem("Preventiva", 0))
                'Valores.Add(New ListItem("Administrativa", 1))
                ' Valores.Add(New ListItem("Extrajudicial", 2))
                ' Valores.Add(New ListItem("Judicial", 3))
                'DDLInstancia.DataSource = Valores
                'DDLInstancia.DataValueField = "Value"
                'DDLInstancia.DataTextField = "Text"
                'DDLInstancia.DataBind()
                ' DDLInstancia.SelectedIte = DDLInstancia.FindItemByValue("2")
                RGDispersiones.Rebind()
            Catch ex As Exception
                SendMail("CatalogoDispercion_Load", ex, "", "", tmpUSUARIO("CAT_LO_USUARIO"))
            End Try

        End If

    End Sub


    Protected Sub CheckBox0_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkTest As CheckBox = DirectCast(sender, CheckBox)
        If chkTest.Checked = True Then
            Dim grdRow As GridDataItem = DirectCast(chkTest.NamingContainer, GridDataItem)
            Dim SSCommand As New SqlCommand
            SSCommand.CommandText = "SP_CATALOGO_CAMPANAS"
            SSCommand.CommandType = CommandType.StoredProcedure
            SSCommand.Parameters.Add("@V_CAT_DIS_ID", SqlDbType.Decimal).Value = grdRow.Item("ID").Text
            SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 17

            Dim DtsDatos As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
            Dim dispersiones As DataTable = DtsDatos
            Session("DispDUMMY") = grdRow.Item("ID").Text
            RBCancelar.Visible = True
            RGDispersiones.Visible = False
            PnlInstancia.Visible = True
            DDLInstancia.Visible = True
            PnlResp.Visible = True
            DDLRespGestion.Visible = True
            PnlDisp.Visible = True
            DDLDispersion.Visible = True
            ' DDLInstancia.SelectedValue = dispersiones.Rows(0).Item("CAT_DIS_INSTANCIA").ToString
            Funciones.LLENAR_DROP2(32, DDLInstancia.SelectedValue, DDLRespGestion, "V_VALOR", "T_VALOR")
            DDLInstancia.Enabled = False
            ' DDLRespGestion.SelectedValue = dispersiones.Rows(0).Item("CAT_DIS_RESPGESTION").ToString
            DDLRespGestion.Enabled = True
            ' DDLDispersion.SelectedValue = dispersiones.Rows(0).Item("CAT_DIS_DISPERSION").ToString
            DDLDispersion.Enabled = True
            PnlDatos.Visible = True
            PNLDispersiones.Visible = False
            gridDispersion.DataSource = dispersiones
            gridDispersion.DataBind()
            'If grdRow.Item("PLANTILLA").Text = "CAMPAÑA" Then
            '    'If DtsDatos.Rows(0).Item("CAT_CAM_CAMPANA") = "Envio de SMS" Then
            '    '    RCBTipoCampana.SelectedValue = 0
            '    'End If
            'Else
            If DtsDatos.Rows(0).Item("CAT_CAM_PLANTILLA") = "Envio de SMS" Then
                    RCBTipoCampana.SelectedValue = 0
                ElseIf DtsDatos.Rows(0).Item("CAT_CAM_PLANTILLA") = "Envio de correo" Then
                    RCBTipoCampana.SelectedValue = 1
                ElseIf DtsDatos.Rows(0).Item("CAT_CAM_PLANTILLA") = "Envio de Whatsapp" Then
                    RCBTipoCampana.SelectedValue = 2
                End If

            'End If
            Try


                Dim hora As String = DtsDatos.Rows(0).Item("CAT_CAM_HORA")
                hora = hora.Substring(0, 2)
                TPHora.Text = hora
                TPFechaI.SelectedDate = DtsDatos.Rows(0).Item("CAT_CAM_FECHA")
                TPFechaF.SelectedDate = DtsDatos.Rows(0).Item("CAT_CAM_FECHAFIN")
                RCBTipoEnvio.SelectedValue = DtsDatos.Rows(0).Item("CAT_CAM_TIPOENVIO")
                TPFechaF.Enabled = True
            Catch ex As Exception
                Dim MSJ As String = ex.Message
            End Try
            If DtsDatos.Rows(0).Item("CAT_CAM_CAMPANA") <> "" Or DtsDatos.Rows(0).Item("CAT_CAM_CAMPANA") <> Nothing Then
                ex()

                RCBCampana.SelectedValue = DtsDatos.Rows(0).Item("CAT_CAM_NOMBRECAMPANA")

            ElseIf DtsDatos.Rows(0).Item("CAT_CAM_PLANTILLA") <> "" Or DtsDatos.Rows(0).Item("CAT_CAM_PLANTILLA") <> Nothing Then
                If DtsDatos.Rows(0).Item("CAT_CAM_PLANTILLA") = "Envio de SMS" Then
                    Dim SSCommand1 As New SqlCommand
                    SSCommand1.CommandText = "SP_CATALOGO_CAMPANAS"
                    SSCommand1.CommandType = CommandType.StoredProcedure

                    SSCommand1.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 23
                    SSCommand1.Parameters.Add("@V_CAT_DIS_ID", SqlDbType.NVarChar).Value = Session("DispDUMMY")

                    Dim DtsDatos1 As DataTable = Consulta_Procedure(SSCommand1, "ELEMENTOS")
                    RCBCampana.Items.Clear()
                    RCBCampana.DataTextField = "Nombre"
                    RCBCampana.DataValueField = "Nombre"
                    RCBCampana.DataSource = DtsDatos1
                    RCBCampana.DataBind()
                    RCBCampana.Enabled = True
                    RCBCampana.SelectedValue = DtsDatos.Rows(0).Item("CAT_CAM_NOMBRECAMPANA")
                    PNLPerfilMail.Visible = False

                ElseIf DtsDatos.Rows(0).Item("CAT_CAM_PLANTILLA") = "Envio de correo" Then
                    Dim SSCommand2 As New SqlCommand
                    SSCommand2.CommandText = "SP_CATALOGO_CAMPANAS"
                    SSCommand2.CommandType = CommandType.StoredProcedure

                    SSCommand2.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 24
                    SSCommand2.Parameters.Add("@V_CAT_DIS_ID", SqlDbType.NVarChar).Value = Session("DispDUMMY")

                    Dim DtsDatos2 As DataTable = Consulta_Procedure(SSCommand2, "ELEMENTOS")
                    RCBCampana.Items.Clear()
                    RCBCampana.DataTextField = "Nombre"
                    RCBCampana.DataValueField = "Nombre"
                    RCBCampana.DataSource = DtsDatos2
                    RCBCampana.DataBind()
                    RCBCampana.Enabled = True
                    RCBCampana.SelectedValue = DtsDatos.Rows(0).Item("CAT_CAM_NOMBRECAMPANA")
                    Dim SSCommandMail As New SqlCommand("SP_EMAILS")
                    SSCommandMail.CommandType = CommandType.StoredProcedure
                    SSCommandMail.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = 7
                    Dim DTSMail As DataTable = Consulta_Procedure(SSCommandMail, "SP_EMAILS")
                    RCBPerfilMail.Items.Clear()
                    RCBPerfilMail.DataTextField = "PERFIL"
                    RCBPerfilMail.DataValueField = "PERFIL"
                    RCBPerfilMail.DataSource = DTSMail
                    RCBPerfilMail.DataBind()
                    RCBPerfilMail.SelectedValue = DtsDatos.Rows(0).Item("CAT_CAM_PERFILMAIL")
                    RCBPerfilMail.Enabled = True
                    PNLPerfilMail.Visible = True

                ElseIf DtsDatos.Rows(0).Item("CAT_CAM_PLANTILLA") = "Envio de Whatsapp" Then
                    Dim SSCommand2 As New SqlCommand
                    SSCommand2.CommandText = "SP_CATALOGO_CAMPANAS"
                    SSCommand2.CommandType = CommandType.StoredProcedure

                    SSCommand2.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 30
                    SSCommand2.Parameters.Add("@V_CAT_DIS_ID", SqlDbType.NVarChar).Value = Session("DispDUMMY")

                    Dim DtsDatos2 As DataTable = Consulta_Procedure(SSCommand2, "ELEMENTOS")
                    RCBCampana.Items.Clear()
                    RCBCampana.DataTextField = "Nombre"
                    RCBCampana.DataValueField = "Nombre"
                    RCBCampana.DataSource = DtsDatos2
                    RCBCampana.DataBind()
                    RCBCampana.Enabled = True
                    RCBCampana.SelectedValue = DtsDatos.Rows(0).Item("CAT_CAM_NOMBRECAMPANA")
                    PNLPerfilMail.Visible = False

                End If


            End If
            btnSimularDispersion.Visible = True
            btnGuardar.Visible = True
            'Dim usuarios As DataTable = TraeUsuarios(grdRow.Item("ID").Text)
            '    Dim tipoUsuarios As String = usuarios.Rows(0)(1).ToString
            'RCBTipoUsuarios.SelectedValue = tipoUsuarios
            'RCBUsuarios.DataTextField = "cat_lo_nombre"
            'RCBUsuarios.DataValueField = "cat_lo_usuario"
            'RCBUsuarios.DataSource = TraerUsuarios(tipoUsuarios)
            'RCBUsuarios.DataBind()
            'RCBUsuarios.Enabled = True
            'Dim tabvig As DataTable = TraeVigrncia(grdRow.Item("ID").Text)
            'Try
            '    CBPeriodo.SelectedValue = tabvig.Rows(0).Item("periodo")
            '    TxtDuracion.Text = tabvig.Rows(0).Item("duracion")
            'Catch ex As Exception

            'End Try
            'For Each usuario As DataRow In usuarios.Rows
            '    Try
            '        RCBUsuarios.FindItemByValue(usuario("USUARIOS").ToString).Checked = True
            '    Catch ex As Exception
            '    End Try
            'Next
        End If
    End Sub

    Private Sub RBCancelar_Click(sender As Object, e As EventArgs) Handles RBCancelar.Click
        RBCancelar.Visible = False
        PnlInstancia.Visible = False
        PnlResp.Visible = False
        PnlDisp.Visible = False
        PnlDatos.Visible = False
        btnSimularDispersion.Visible = False
        PNLDispersiones.Visible = True
        Response.Redirect("CatalogoCampanas.aspx")
        gridAsignacion.Visible = False
        btnAplicarAsignacion.Visible = False
    End Sub

    Private Sub RGDispersiones_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RGDispersiones.NeedDataSource
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_CATALOGO_CAMPANAS"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 0
        SSCommand.Parameters.Add("@V_Instancia", SqlDbType.NVarChar).Value = tmpUSUARIO("CAT_LO_INSTANCIA")

        Dim DtsDatos As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        RGDispersiones.DataSource = DtsDatos
    End Sub

    Private Sub RGDispersiones_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RGDispersiones.ItemCommand
        Dim comando As String = e.CommandName
        If comando = "InitInsert" Then
            PNLDispersiones.Visible = False
            'PnlInstancia.Visible = True
            'DDLInstancia.Visible = True
            'PnlResp.Visible = True
            'DDLRespGestion.Visible = True
            'PnlDisp.Visible = True
            'DDLDispersion.Visible = True
            RBCancelar.Visible = True
            '   DDLInstancia.Enabled = True
            PnlDatos.Visible = True
            Dim SSCommand As New SqlCommand
            SSCommand.CommandText = "SP_CATALOGO_CAMPANAS"
            SSCommand.CommandType = CommandType.StoredProcedure
            SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 14

            Dim DtsDatos As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")

            Session("DispDUMMY") = DtsDatos.Rows(0).Item("MAXIMO").ToString
            gridDispersion.Rebind()
            'RCBUsuarios.DataTextField = "cat_lo_nombre"
            'RCBUsuarios.DataValueField = "cat_lo_usuario"
            'RCBUsuarios.DataSource = TraerUsuarios("2")
            'RCBUsuarios.DataBind()
            e.Canceled = True
        ElseIf comando = "Delete" Then
            Dim id As String = e.Item.Cells(3).Text
            Dim SSCommand As New SqlCommand
            SSCommand.CommandText = "SP_CATALOGO_CAMPANAS"
            SSCommand.CommandType = CommandType.StoredProcedure
            SSCommand.Parameters.Add("@V_CAT_DIS_ID", SqlDbType.NVarChar).Value = id
            SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 16

            Dim DtsDatos As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")

            Aviso("Campaña eliminada correctamente")
        End If
    End Sub

    Private Sub gridAsignacion_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles gridAsignacion.ItemCommand

        'Session("PostBack") = True
        'If e.CommandName = "Update" Then
        '    Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
        '    Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
        '    Dim valores(1) As String
        '    valores(0) = CType(MyUserControl.FindControl("DdlUsuario"), RadComboBox).SelectedValue
        '    Class_CatalogoDispersion.CambiarAsignacion(Session("CreditoACambiar"), valores(0))



        '    Dim tabla As DataTable = TraerAsignacion(Session("DispDUMMY"))
        '    Session("simulacion") = tabla
        '    gridAsignacion.Rebind()
        '    Aviso("Credito Reasignado")
        'ElseIf e.CommandName = "Edit" Then
        '    Session("CreditoACambiar") = e.Item.Cells.Item(3).Text
        'End If
    End Sub

    Private Sub eliminarDispersion(id As Integer)

    End Sub
    Public Shared Function Rad_Tcadena(ByRef v_item As RadComboBox) As String
        Dim v_cadena As String = "'"
        Dim collection As IList(Of RadComboBoxItem) = v_item.CheckedItems

        For Each item As RadComboBoxItem In collection
            v_cadena = v_cadena & item.Value & "','"
        Next

        If collection.Count = 0 Then
            v_cadena = ""
        Else
            v_cadena = v_cadena.Substring(0, Len(v_cadena) - 2)
        End If

        Return v_cadena
    End Function

    Public Shared Function Rad_Ncadena(ByRef v_item As RadComboBox) As String
        Dim v_cadena As String = "'"
        Dim collection As IList(Of RadComboBoxItem) = v_item.CheckedItems

        For Each item As RadComboBoxItem In collection
            v_cadena = v_cadena & item.Value & ","
        Next

        If collection.Count = 0 Then
            v_cadena = ""
        Else
            v_cadena = v_cadena.Substring(0, Len(v_cadena) - 1)
        End If

        Return v_cadena
    End Function

    'Protected Sub Safi_asignacionCarteraCobranzaMasivo(ByVal info As DataTable)
    '    Try
    '        Dim usuarioIP As String = Page.Request.ServerVariables("REMOTE_HOST")
    '        Dim asignacion As String = ""
    '        '------asignacionCartera-----------------------
    '        info.Columns(0).ColumnName = "creditoID"
    '        info.Columns(2).ColumnName = "tipoInstancia"
    '        info.Columns(5).ColumnName = "fechaAsignacion"
    '        info.Columns(6).ColumnName = "litigio"
    '        info.Columns(3).ColumnName = "despachoID"
    '        info.Columns(4).ColumnName = "despachoNombre"
    '        info.Columns(7).ColumnName = "tipoDespacho"
    '        info.Columns(8).ColumnName = "abogadoID"
    '        info.Columns(9).ColumnName = "abogadoNombre"
    '        info.Columns(10).ColumnName = "supervisorID"
    '        info.Columns(11).ColumnName = "supervisorNombre"
    '        info.Columns.Remove("PR_MC_UASIGNADO")
    '        Dim cadena As String = JsonConvert.SerializeObject(info)

    '        Dim v_endpoint As String = Db.StrEndPoint("SAFI", 1)
    '        Dim v_metodo As String = "cobranza/asignacionCarteraCobranzaMasivo"

    '        Dim CUrl As WebRequest = WebRequest.Create(v_endpoint & v_metodo)

    '        Dim data As String = "{" & vbLf &
    '                " ""usuarioIP"": """ & usuarioIP & """," & vbLf &
    '                " ""asignacionCartera"": " & vbLf &
    '                cadena &
    '                    " " & vbLf & "}"



    '        Dim client = New RestClient(v_endpoint & v_metodo)
    '        Dim request = New RestRequest(Method.POST)
    '        request.AddHeader("cache-control", "no-cache")
    '        request.AddHeader("Connection", "keep-alive")
    '        request.AddHeader("Content-Length", data.Length)
    '        request.AddHeader("Accept", "*/*")
    '        request.AddHeader("Content-Type", "application/json")
    '        request.AddHeader("autentificacion", "dXN1YXJpb1BydWViYVdTOjEyMw==")
    '        request.AddParameter("undefined", data, ParameterType.RequestBody)
    '        request.Timeout = 1000000
    '        Dim response As IRestResponse = client.Execute(request)



    '    Catch ex As WebException
    '        Dim abd As String = ex.Message
    '    End Try


    'End Sub




    Public Shared Function WM_getCampanasList() As String

        Dim usuario As String = "UserWebS"
        Dim pass As String = "UserWebS40.@"
        Dim v_url As String = "http://frd.nimbuscc.mx/ccs/servicios.php?wsdl"

        Dim xml As String = "<soapenv:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:ser=""http://www.nimbuscc.mx/ccs/servicios.php"">" + vbCrLf
        xml += "<soapenv:Header/>" + vbCrLf
        xml += " <soapenv:Body>" + vbCrLf
        xml += "		<ser:CAMPANAS_LIST soapenv:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/"">   " + vbCrLf
        xml += "			<usuario xsi:type=""xsd:String"">" & usuario & "</usuario>   " + vbCrLf
        xml += "			<contrasena xsi:type=""xsd:string"">" & pass & "</contrasena>   " + vbCrLf
        xml += "		</ser:CAMPANAS_LIST>" + vbCrLf
        xml += "	</soapenv:Body>" + vbCrLf
        xml += "</soapenv:Envelope>"

        Return M_DONDE.ConsumeWS_DONDE(v_url, "POST", XElement.Parse(xml).ToString)

    End Function
    Sub ex()

        Dim xlmstr As String = WM_getCampanasList()
        Dim xmll As New XmlDocument
        Dim xmlnode As XmlNode
        Dim nodelist As XmlNodeList
        Dim nametable As New XmlNamespaceManager(xmll.NameTable)
        nametable.AddNamespace("SOAP-ENV", "http://schemas.xmlsoap.org/soap/envelope/")
        nametable.AddNamespace("ns1", "http://www.nimbuscc.mx/ccs/servicios.php")
        xmll.LoadXml(xlmstr)

        nodelist = xmll.SelectNodes("/SOAP-ENV:Envelope/SOAP-ENV:Body/ns1:CAMPANAS_LISTResponse/return/item", nametable)
        RCBCampana.Items.Clear()
        For Each xmlnode In nodelist

            If (xmlnode.ChildNodes(7).InnerText = "2813") Then
                Dim id = xmlnode.ChildNodes(2).InnerText
                Dim nombre = xmlnode.ChildNodes(3).InnerText
                Dim item As New RadComboBoxItem
                item.Value = id.ToString
                item.Text = "[" + id.ToString + "]" + " " + nombre.ToString

                RCBCampana.Items.Add(item)

            End If



        Next
        RCBCampana.DataBind()
        RCBCampana.Enabled = True


        'Try
        '    Dim mensajes As String = M_DONDE.EnviarRequestGet("")

        '    Dim Des As New System.Web.Script.Serialization.JavaScriptSerializer
        '    Dim ListaDatos = Des.Deserialize(Of List(Of Dictionary(Of String, Object)))(mensajes)

        '    Dim dtDes As New DataTable
        '    'creamos las columnas
        '    For i As Integer = 0 To ListaDatos(0).Keys.Count - 1
        '        dtDes.Columns.Add(ListaDatos(0).Keys(i).ToString)
        '    Next
        '    'Ahora los datos
        '    Dim drDes As DataRow
        '    For Rows As Integer = 0 To ListaDatos.Count - 1
        '        drDes = dtDes.NewRow
        '        For Columns As Integer = 0 To ListaDatos(Rows).Keys.Count - 1
        '            drDes(ListaDatos(Rows).Keys(Columns)) = ListaDatos(Rows).Item(ListaDatos(Rows).Keys(Columns))
        '        Next
        '        dtDes.Rows.Add(drDes)
        '    Next
        '    Dim mensajes2 As String = dtDes.Rows(0).Item(2).ToString
        '    Session("datatable") = dtDes



        '    RCBCampana.DataTextField = "campana"
        '    RCBCampana.DataValueField = "campana"
        '    RCBCampana.DataSource = dtDes
        '    RCBCampana.DataBind()


        '    'RGridContenidoJSON.DataSource = dtDes
        '    'RGridContenidoJSON.Rebind()
        '    Session("valorini") = 1
        'Catch
        '    Aviso("No se encontro informacion")
        'End Try
    End Sub

    Private Sub RCBTipoCampana_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles RCBTipoCampana.SelectedIndexChanged
        'If e.Value = 0 Then
        '    ex()
        'ElseIf e.Value = 0 Then
        '    Dim SSCommand As New SqlCommand
        '    SSCommand.CommandText = "SP_CATALOGO_CAMPANAS"
        '    SSCommand.CommandType = CommandType.StoredProcedure

        '    SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 23
        '    SSCommand.Parameters.Add("@V_CAT_DIS_ID", SqlDbType.NVarChar).Value = Session("DispDUMMY")

        '    Dim DtsDatos As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        '    RCBCampana.Items.Clear()
        '    RCBCampana.DataTextField = "Nombre"
        '    RCBCampana.DataValueField = "Nombre"
        '    RCBCampana.DataSource = DtsDatos
        '    RCBCampana.DataBind()
        '    RCBCampana.Enabled = True
        If e.Value = 0 Then
            Dim SSCommand As New SqlCommand
            SSCommand.CommandText = "SP_CATALOGO_CAMPANAS"
            SSCommand.CommandType = CommandType.StoredProcedure

            SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 23
            SSCommand.Parameters.Add("@V_CAT_DIS_ID", SqlDbType.NVarChar).Value = Session("DispDUMMY")

            Dim DtsDatos As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
            RCBCampana.Items.Clear()
            RCBCampana.DataTextField = "Nombre"
            RCBCampana.DataValueField = "Nombre"
            RCBCampana.DataSource = DtsDatos
            RCBCampana.DataBind()
            RCBCampana.Enabled = True
            PNLPerfilMail.Visible = False
            RCBPerfilMail.Items.Clear()
        ElseIf e.Value = 1 Then
            Dim SSCommand As New SqlCommand
            SSCommand.CommandText = "SP_CATALOGO_CAMPANAS"
            SSCommand.CommandType = CommandType.StoredProcedure

            SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 24
            SSCommand.Parameters.Add("@V_CAT_DIS_ID", SqlDbType.NVarChar).Value = Session("DispDUMMY")

            Dim DtsDatos As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
            RCBCampana.Items.Clear()
            RCBCampana.DataTextField = "Nombre"
            RCBCampana.DataValueField = "Nombre"
            RCBCampana.DataSource = DtsDatos
            RCBCampana.DataBind()
            RCBCampana.Enabled = True
            PNLPerfilMail.Visible = True
            Dim SSCommandMail As New SqlCommand("SP_EMAILS")
            SSCommandMail.CommandType = CommandType.StoredProcedure
            SSCommandMail.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = 7
            Dim DTSMail As DataTable = Consulta_Procedure(SSCommandMail, "SP_EMAILS")
            RCBPerfilMail.Items.Clear()
            RCBPerfilMail.DataTextField = "PERFIL"
            RCBPerfilMail.DataValueField = "PERFIL"
            RCBPerfilMail.DataSource = DTSMail
            RCBPerfilMail.DataBind()
            RCBPerfilMail.Enabled = True
        ElseIf e.Value = 2 Then
            Dim SSCommand As New SqlCommand
            SSCommand.CommandText = "SP_CATALOGO_CAMPANAS"
            SSCommand.CommandType = CommandType.StoredProcedure

            SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 30
            SSCommand.Parameters.Add("@V_CAT_DIS_ID", SqlDbType.NVarChar).Value = Session("DispDUMMY")

            Dim DtsDatos As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
            RCBCampana.Items.Clear()
            RCBCampana.DataTextField = "Nombre"
            RCBCampana.DataValueField = "Nombre"
            RCBCampana.DataSource = DtsDatos
            RCBCampana.DataBind()
            RCBCampana.Enabled = True
            PNLPerfilMail.Visible = False
            RCBPerfilMail.Items.Clear()
        End If
    End Sub

    Private Sub RCBCampana_TextChanged(sender As Object, e As EventArgs) Handles RCBCampana.TextChanged
        Dim SSCommand2 As New SqlCommand
        SSCommand2.CommandText = "SP_CATALOGO_CAMPANAS"
        SSCommand2.CommandType = CommandType.StoredProcedure

        SSCommand2.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 26
        SSCommand2.Parameters.Add("@V_CAT_DIS_ID", SqlDbType.NVarChar).Value = Session("DispDUMMY")

        Dim DtsDatos2 As DataTable = Consulta_Procedure(SSCommand2, "ELEMENTOS")
        If DtsDatos2.Rows(0).Item(0).ToString >= "1" Then



            Dim SSCommand As New SqlCommand
            SSCommand.CommandText = "SP_CATALOGO_CAMPANAS"
            SSCommand.CommandType = CommandType.StoredProcedure
            ' SSCommand.Parameters.Add("@V_Instancia", SqlDbType.NVarChar).Value = V_Instancia

            If RCBTipoCampana.SelectedValue = 0 Then
                'SSCommand.Parameters.Add("@V_CAMPANA", SqlDbType.NVarChar).Value = RCBTipoCampana.Text
                SSCommand.Parameters.Add("@V_PLANTILLA", SqlDbType.NVarChar).Value = RCBTipoCampana.Text
            ElseIf RCBTipoCampana.SelectedValue = 1 Or RCBTipoCampana.SelectedValue = 2 Then
                SSCommand.Parameters.Add("@V_PLANTILLA", SqlDbType.NVarChar).Value = RCBTipoCampana.Text
            End If

            ' SSCommand.Parameters.Add("@V_SubClasificacion", SqlDbType.NVarChar).Value = V_SubClasificacion
            SSCommand.Parameters.Add("@V_respgestion", SqlDbType.NVarChar).Value = "4"
            SSCommand.Parameters.Add("@v_usuarios", SqlDbType.NVarChar).Value = RCBCampana.SelectedValue
            SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 10
            SSCommand.Parameters.Add("@V_CAT_DIS_ID", SqlDbType.NVarChar).Value = Session("DispDUMMY")

            Dim DtsDatos As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
            If DtsDatos.Rows(0).Item(0) = "ok" Then
                Aviso("Campaña actualizada")
            Else
                Aviso("Verifique que exista al menos un parametro en parametrización")
            End If
        Else
            Aviso("Verifique que exista al menos un parametro en parametrización")
            RCBCampana.ClearSelection()
        End If
    End Sub
    Private Sub RCBPerfilMail_TextChanged(sender As Object, e As EventArgs) Handles RCBPerfilMail.TextChanged
        Dim SSCommand2 As New SqlCommand
        SSCommand2.CommandText = "SP_CATALOGO_CAMPANAS"
        SSCommand2.CommandType = CommandType.StoredProcedure

        SSCommand2.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 26
        SSCommand2.Parameters.Add("@V_CAT_DIS_ID", SqlDbType.NVarChar).Value = Session("DispDUMMY")

        Dim DtsDatos2 As DataTable = Consulta_Procedure(SSCommand2, "ELEMENTOS")
        If DtsDatos2.Rows(0).Item(0).ToString >= "1" Then



            Dim SSCommand As New SqlCommand
            SSCommand.CommandText = "SP_CATALOGO_CAMPANAS"
            SSCommand.CommandType = CommandType.StoredProcedure
            ' SSCommand.Parameters.Add("@V_Instancia", SqlDbType.NVarChar).Value = V_Instancia

            If RCBTipoCampana.SelectedValue = 0 Then
                'SSCommand.Parameters.Add("@V_CAMPANA", SqlDbType.NVarChar).Value = RCBTipoCampana.Text
                SSCommand.Parameters.Add("@V_PLANTILLA", SqlDbType.NVarChar).Value = RCBTipoCampana.Text
            ElseIf RCBTipoCampana.SelectedValue = 1 Or RCBTipoCampana.SelectedValue = 2 Then
                SSCommand.Parameters.Add("@V_PLANTILLA", SqlDbType.NVarChar).Value = RCBTipoCampana.Text
            End If

            ' SSCommand.Parameters.Add("@V_SubClasificacion", SqlDbType.NVarChar).Value = V_SubClasificacion
            SSCommand.Parameters.Add("@V_respgestion", SqlDbType.NVarChar).Value = "4"
            SSCommand.Parameters.Add("@v_usuarios", SqlDbType.NVarChar).Value = RCBCampana.SelectedValue
            SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 10
            SSCommand.Parameters.Add("@V_PerfilMail", SqlDbType.NVarChar).Value = RCBPerfilMail.SelectedValue
            SSCommand.Parameters.Add("@V_CAT_DIS_ID", SqlDbType.NVarChar).Value = Session("DispDUMMY")

            Dim DtsDatos As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
            If DtsDatos.Rows(0).Item(0) = "ok" Then
                Aviso("Campaña actualizada")
            Else
                Aviso("Verifique que exista al menos un parametro en parametrización")
            End If
        Else
            Aviso("Verifique que exista al menos un parametro en parametrización")
            RCBCampana.ClearSelection()
        End If
    End Sub


    Private Sub TPHora_TextChanged(sender As Object, e As EventArgs) Handles TPHora.TextChanged

        If TPFechaI.SelectedDate.ToString.Length = 0 Then
            Aviso("Ingrese fecha")
            TPHora.Text = Nothing
            btnSimularDispersion.Visible = False
            btnGuardar.Visible = False
        ElseIf TPFechaI.SelectedDate = Date.Now.Date And CInt(TPHora.Text) <= Date.Now.Hour Then
            Aviso("La hora debe ser mayor a la hora actual")
            TPHora.Text = Nothing
            btnSimularDispersion.Visible = False
            btnGuardar.Visible = False
        ElseIf TPFechaI.SelectedDate < Date.Now.Date Then
            Aviso("La fecha no puede ser anterior al dia de hoy")
            btnSimularDispersion.Visible = False
            btnGuardar.Visible = False
        Else

            btnSimularDispersion.Visible = True
            btnGuardar.Visible = True



        End If

    End Sub

    Private Sub TPFechaI_SelectedDateChanged(sender As Object, e As SelectedDateChangedEventArgs) Handles TPFechaI.SelectedDateChanged


        If TPFechaI.SelectedDate < Date.Now.Date Then
            Aviso("La fecha no puede ser anterior al dia de hoy")
            TPFechaI.SelectedDate = Nothing
            btnSimularDispersion.Visible = False
            btnGuardar.Visible = False
        End If
        TPFechaF.Enabled = True
    End Sub
    Private Sub TPFechaF_SelectedDateChanged(sender As Object, e As SelectedDateChangedEventArgs) Handles TPFechaF.SelectedDateChanged


        If TPFechaF.SelectedDate < TPFechaI.SelectedDate Then
            Aviso("La fecha no puede ser anterior a la fecha de inicio")
            TPFechaF.SelectedDate = Nothing
            btnSimularDispersion.Visible = False
            btnGuardar.Visible = False
        End If
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If Not TPFechaI.SelectedDate.HasValue Then
            Aviso("Seleccione una Fecha Inicial")
        ElseIf Not TPFechaF.SelectedDate.HasValue Then
            Aviso("Seleccione una Fecha Final")
        ElseIf RCBTipoEnvio.SelectedValue = "" Then
            Aviso("Seleccione un tipo de envio")
        ElseIf TPHora.Text.Trim = "" Then
            Aviso("Selecciona una Hora")
        Else
            Dim fi As Date = TPFechaI.SelectedDate
            Dim ff As Date = TPFechaF.SelectedDate
            Dim SSCommand2 As New SqlCommand
            SSCommand2.CommandText = "SP_CATALOGO_CAMPANAS"
            SSCommand2.CommandType = CommandType.StoredProcedure

            SSCommand2.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 26
            SSCommand2.Parameters.Add("@V_CAT_DIS_ID", SqlDbType.NVarChar).Value = Session("DispDUMMY")

            Dim DtsDatos2 As DataTable = Consulta_Procedure(SSCommand2, "ELEMENTOS")
            If DtsDatos2.Rows(0).Item(0).ToString >= "1" Then
                Dim fechaini As String = ""
                Dim fechafin As String = ""
                Dim hora As String = ""
                If CInt(TPHora.Text) < 10 Then
                    hora = "0" + TPHora.Text
                Else
                    hora = TPHora.Text
                End If
                hora = hora + ":00"
                Try
                    If TPFechaI.SelectedDate.Value.Month < 10 Then
                        fechaini = TPFechaI.SelectedDate.Value.Year.ToString + "/0" + TPFechaI.SelectedDate.Value.Month.ToString
                    Else
                        fechaini = TPFechaI.SelectedDate.Value.Year.ToString + "/" + TPFechaI.SelectedDate.Value.Month.ToString
                    End If
                    If TPFechaI.SelectedDate.Value.Day < 10 Then
                        fechaini += "/0" + TPFechaI.SelectedDate.Value.Day.ToString
                    Else
                        fechaini += "/" + TPFechaI.SelectedDate.Value.Day.ToString
                    End If
                    If TPFechaF.SelectedDate.Value.Month < 10 Then
                        fechafin = TPFechaF.SelectedDate.Value.Year.ToString + "/0" + TPFechaF.SelectedDate.Value.Month.ToString
                    Else
                        fechafin = TPFechaF.SelectedDate.Value.Year.ToString + "/" + TPFechaF.SelectedDate.Value.Month.ToString
                    End If
                    If TPFechaF.SelectedDate.Value.Day < 10 Then
                        fechafin += "/0" + TPFechaF.SelectedDate.Value.Day.ToString
                    Else
                        fechafin += "/" + TPFechaF.SelectedDate.Value.Day.ToString
                    End If
                Catch
                End Try
                Dim SSCommand As New SqlCommand
                SSCommand.CommandText = "SP_CATALOGO_CAMPANAS"
                SSCommand.CommandType = CommandType.StoredProcedure
                SSCommand.Parameters.Add("@v_fecha", SqlDbType.NVarChar).Value = fechaini
                SSCommand.Parameters.Add("@v_fecha1", SqlDbType.NVarChar).Value = fechafin
                SSCommand.Parameters.Add("@v_frecuencia", SqlDbType.NVarChar).Value = RCBTipoEnvio.SelectedValue
                'SSCommand.Parameters.Add("@V_TIPOENVIO", SqlDbType.NVarChar).Value = RCBTipoEnvio.SelectedItem
                SSCommand.Parameters.Add("@v_hora", SqlDbType.NVarChar).Value = hora
                If RCBTipoCampana.SelectedValue = 0 Then
                    SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 27
                ElseIf RCBTipoCampana.SelectedValue = 1 Then
                    SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 32 'aqui
                ElseIf RCBTipoCampana.SelectedValue = 2 Then
                    SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 23
                End If

                SSCommand.Parameters.Add("@v_campanaid", SqlDbType.NVarChar).Value = RCBCampana.SelectedValue
                SSCommand.Parameters.Add("@V_CAT_DIS_ID", SqlDbType.NVarChar).Value = Session("DispDUMMY")

                Dim DtsDatos As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
                If DtsDatos.Rows(0).Item(0) = "Ok" Then
                    Aviso("Tarea Programada exitosamente")
                Else
                    Aviso(DtsDatos.Rows(0).Item(0))
                End If
            Else
                Aviso("Verifique que exista al menos un parametro en parametrización")
                TPHora.Text = Nothing
            End If
        End If
        'If RCBTipoEnvio.SelectedValue = "D" Then

        '    Dim days As Long = DateDiff(DateInterval.Day, fi, ff)
        '    For cuantos As Integer = 0 To days


        '        Dim SSCommand2 As New SqlCommand
        '        SSCommand2.CommandText = "SP_CATALOGO_CAMPANAS"
        '        SSCommand2.CommandType = CommandType.StoredProcedure

        '        SSCommand2.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 26
        '        SSCommand2.Parameters.Add("@V_CAT_DIS_ID", SqlDbType.NVarChar).Value = Session("DispDUMMY")

        '        Dim DtsDatos2 As DataTable = Consulta_Procedure(SSCommand2, "ELEMENTOS")
        '        If DtsDatos2.Rows(0).Item(0).ToString >= "1" Then
        '            Dim fechaini As String = ""

        '            Dim hora As String = ""
        '            If CInt(TPHora.Text) < 10 Then
        '                hora = "0" + TPHora.Text
        '            Else
        '                hora = TPHora.Text
        '            End If
        '            hora = hora + ":00"
        '            Try
        '                If TPFechaI.SelectedDate.Value.Month < 10 Then
        '                    fechaini = TPFechaI.SelectedDate.Value.Year.ToString + "/0" + TPFechaI.SelectedDate.Value.Month.ToString
        '                Else
        '                    fechaini = TPFechaI.SelectedDate.Value.Year.ToString + "/" + TPFechaI.SelectedDate.Value.Month.ToString
        '                End If
        '                If TPFechaI.SelectedDate.Value.Day < 10 Then
        '                    fechaini += "/0" + TPFechaI.SelectedDate.Value.Day.ToString
        '                Else
        '                    fechaini += "/" + TPFechaI.SelectedDate.Value.Day.ToString
        '                End If
        '            Catch
        '            End Try
        '            Dim SSCommand As New SqlCommand
        '            SSCommand.CommandText = "SP_CATALOGO_CAMPANAS"
        '            SSCommand.CommandType = CommandType.StoredProcedure
        '            SSCommand.Parameters.Add("@v_fecha", SqlDbType.NVarChar).Value = fechaini
        '            SSCommand.Parameters.Add("@v_hora", SqlDbType.NVarChar).Value = hora
        '            If RCBTipoCampana.SelectedValue = 0 Then
        '                SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 27
        '            ElseIf RCBTipoCampana.SelectedValue = 1 Then
        '                SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 32 'aqui
        '            ElseIf RCBTipoCampana.SelectedValue = 2 Then
        '                SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 33
        '            End If

        '            SSCommand.Parameters.Add("@v_campanaid", SqlDbType.NVarChar).Value = RCBCampana.SelectedValue
        '            SSCommand.Parameters.Add("@V_CAT_DIS_ID", SqlDbType.NVarChar).Value = Session("DispDUMMY")

        '            Dim DtsDatos As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        '            If DtsDatos.Rows(0).Item(0) = "Ok" Then
        '                ' Aviso("Tarea Programada exitosamente")
        '            Else
        '                Aviso(DtsDatos.Rows(0).Item(0))
        '            End If
        '        Else
        '            Aviso("Verifique que exista al menos un parametro en parametrización")
        '            TPHora.Text = Nothing
        '        End If

        '        TPFechaI.SelectedDate = DateAdd(DateInterval.Day, 1, fi)
        '        fi = TPFechaI.SelectedDate
        '    Next
        'ElseIf RCBTipoEnvio.SelectedValue = "M" Then
        '    Dim days As Long = DateDiff(DateInterval.Month, fi, ff)

        '    For cuantos As Integer = 0 To days


        '        Dim SSCommand2 As New SqlCommand
        '        SSCommand2.CommandText = "SP_CATALOGO_CAMPANAS"
        '        SSCommand2.CommandType = CommandType.StoredProcedure

        '        SSCommand2.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 26
        '        SSCommand2.Parameters.Add("@V_CAT_DIS_ID", SqlDbType.NVarChar).Value = Session("DispDUMMY")

        '        Dim DtsDatos2 As DataTable = Consulta_Procedure(SSCommand2, "ELEMENTOS")
        '        If DtsDatos2.Rows(0).Item(0).ToString >= "1" Then
        '            Dim fechaini As String = ""

        '            Dim hora As String = ""
        '            If CInt(TPHora.Text) < 10 Then
        '                hora = "0" + TPHora.Text
        '            Else
        '                hora = TPHora.Text
        '            End If
        '            hora = hora + ":00"
        '            Try
        '                If TPFechaI.SelectedDate.Value.Month < 10 Then
        '                    fechaini = TPFechaI.SelectedDate.Value.Year.ToString + "/0" + TPFechaI.SelectedDate.Value.Month.ToString
        '                Else
        '                    fechaini = TPFechaI.SelectedDate.Value.Year.ToString + "/" + TPFechaI.SelectedDate.Value.Month.ToString
        '                End If
        '                If TPFechaI.SelectedDate.Value.Day < 10 Then
        '                    fechaini += "/0" + TPFechaI.SelectedDate.Value.Day.ToString
        '                Else
        '                    fechaini += "/" + TPFechaI.SelectedDate.Value.Day.ToString
        '                End If
        '            Catch
        '            End Try
        '            Dim SSCommand As New SqlCommand
        '            SSCommand.CommandText = "SP_CATALOGO_CAMPANAS"
        '            SSCommand.CommandType = CommandType.StoredProcedure
        '            SSCommand.Parameters.Add("@v_fecha", SqlDbType.NVarChar).Value = fechaini
        '            SSCommand.Parameters.Add("@v_hora", SqlDbType.NVarChar).Value = hora
        '            If RCBTipoCampana.SelectedValue = 0 Then
        '                SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 27
        '            ElseIf RCBTipoCampana.SelectedValue = 1 Then
        '                SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 32 'aqui
        '            ElseIf RCBTipoCampana.SelectedValue = 2 Then
        '                SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 33
        '            End If

        '            SSCommand.Parameters.Add("@v_campanaid", SqlDbType.NVarChar).Value = RCBCampana.SelectedValue
        '            SSCommand.Parameters.Add("@V_CAT_DIS_ID", SqlDbType.NVarChar).Value = Session("DispDUMMY")

        '            Dim DtsDatos As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        '            If DtsDatos.Rows(0).Item(0) = "Ok" Then
        '                ' Aviso("Tarea Programada exitosamente")
        '            Else
        '                Aviso(DtsDatos.Rows(0).Item(0))
        '            End If
        '        Else
        '            Aviso("Verifique que exista al menos un parametro en parametrización")
        '            TPHora.Text = Nothing
        '        End If

        '        TPFechaI.SelectedDate = DateAdd(DateInterval.Month, 1, fi)
        '        fi = TPFechaI.SelectedDate
        '    Next
        'ElseIf RCBTipoEnvio.SelectedValue = "S" Then
        '    Dim days As Long = DateDiff(DateInterval.Weekday, fi, ff)
        '    For cuantos As Integer = 0 To days


        '        Dim SSCommand2 As New SqlCommand
        '        SSCommand2.CommandText = "SP_CATALOGO_CAMPANAS"
        '        SSCommand2.CommandType = CommandType.StoredProcedure

        '        SSCommand2.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 26
        '        SSCommand2.Parameters.Add("@V_CAT_DIS_ID", SqlDbType.NVarChar).Value = Session("DispDUMMY")

        '        Dim DtsDatos2 As DataTable = Consulta_Procedure(SSCommand2, "ELEMENTOS")
        '        If DtsDatos2.Rows(0).Item(0).ToString >= "1" Then
        '            Dim fechaini As String = ""

        '            Dim hora As String = ""
        '            If CInt(TPHora.Text) < 10 Then
        '                hora = "0" + TPHora.Text
        '            Else
        '                hora = TPHora.Text
        '            End If
        '            hora = hora + ":00"
        '            Try
        '                If TPFechaI.SelectedDate.Value.Month < 10 Then
        '                    fechaini = TPFechaI.SelectedDate.Value.Year.ToString + "/0" + TPFechaI.SelectedDate.Value.Month.ToString
        '                Else
        '                    fechaini = TPFechaI.SelectedDate.Value.Year.ToString + "/" + TPFechaI.SelectedDate.Value.Month.ToString
        '                End If
        '                If TPFechaI.SelectedDate.Value.Day < 10 Then
        '                    fechaini += "/0" + TPFechaI.SelectedDate.Value.Day.ToString
        '                Else
        '                    fechaini += "/" + TPFechaI.SelectedDate.Value.Day.ToString
        '                End If
        '            Catch
        '            End Try
        '            Dim SSCommand As New SqlCommand
        '            SSCommand.CommandText = "SP_CATALOGO_CAMPANAS"
        '            SSCommand.CommandType = CommandType.StoredProcedure
        '            SSCommand.Parameters.Add("@v_fecha", SqlDbType.NVarChar).Value = fechaini
        '            SSCommand.Parameters.Add("@v_hora", SqlDbType.NVarChar).Value = hora
        '            If RCBTipoCampana.SelectedValue = 0 Then
        '                SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 27
        '            ElseIf RCBTipoCampana.SelectedValue = 1 Then
        '                SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 32 'aqui
        '            ElseIf RCBTipoCampana.SelectedValue = 2 Then
        '                SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 33
        '            End If

        '            SSCommand.Parameters.Add("@v_campanaid", SqlDbType.NVarChar).Value = RCBCampana.SelectedValue
        '            SSCommand.Parameters.Add("@V_CAT_DIS_ID", SqlDbType.NVarChar).Value = Session("DispDUMMY")

        '            Dim DtsDatos As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        '            If DtsDatos.Rows(0).Item(0) = "Ok" Then
        '                ' Aviso("Tarea Programada exitosamente")
        '            Else
        '                Aviso(DtsDatos.Rows(0).Item(0))
        '            End If
        '        Else
        '            Aviso("Verifique que exista al menos un parametro en parametrización")
        '            TPHora.Text = Nothing
        '        End If

        '        TPFechaI.SelectedDate = DateAdd(DateInterval.Weekday, 1, fi)
        '        fi = TPFechaI.SelectedDate
        '    Next
        'End If
        '    Aviso("Tarea Programada exitosamente")

    End Sub

    Private Sub RGDispersiones_PageIndexChanged(sender As Object, e As GridPageChangedEventArgs) Handles RGDispersiones.PageIndexChanged
        RGDispersiones.CurrentPageIndex = e.NewPageIndex
        RGDispersiones.DataSource = Session("simulacion")
        RGDispersiones.DataBind()
    End Sub
End Class