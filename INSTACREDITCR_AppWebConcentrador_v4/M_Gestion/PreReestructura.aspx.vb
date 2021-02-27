Imports System.Data
Imports System.Data.SqlClient
Imports Db
Imports Telerik.Web.UI
Imports Funciones
Imports System.Web.Services
Imports System.IO
Imports System.Net
Imports Newtonsoft.Json
Imports RestSharp

Partial Class M_Gestion_PreReestructura
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
    Private Const ItemsPerRequest As Integer = 10
    Shared creditos As String = ""
    Shared Clacificacion As String = ""
    Private Sub M_Gestion_PreReestructura_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                If Not tmpCredito Is Nothing Then
                    llenar()
                End If

            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub llenar()
        DDLSucursal.DataSource = consultaU("", 1)
        DDLSucursal.DataTextField = "stex"
        DDLSucursal.DataValueField = "sval"
        DDLSucursal.DataBind()
        DDL_plazo.DataSource = consultaU("", 15)
        DDL_plazo.DataTextField = "tdesc"
        DDL_plazo.DataValueField = "vid"
        DDL_plazo.DataBind()
    End Sub

    <WebMethod>
    Public Shared Function GetResults(context As SearchBoxContext) As SearchBoxItemData()
        Dim data As DataTable = Busquedas.Search("'%" & context.Text.ToUpper & "%'")

        Dim result As New List(Of SearchBoxItemData)()

        For i As Integer = 0 To data.Rows.Count - 1
            Dim itemData As New SearchBoxItemData()
            itemData.Text = data.Rows(i)("Credito").ToString() & " - " & data.Rows(i)("Nombre").ToString()
            itemData.Value = data.Rows(i)("Credito").ToString()
            result.Add(itemData)
        Next
        Return result.ToArray()
    End Function
    Private Function consultaU(credito As String, bandera As Integer) As DataTable
        Dim SSCommand As New SqlCommand("SP_REESTRUCTURA")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Credito", SqlDbType.NVarChar).Value = credito
        SSCommand.Parameters.Add("@v_usuario", SqlDbType.NVarChar).Value = tmpUSUARIO("CAT_LO_USUARIO")
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = bandera
        Dim dset As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return dset
    End Function
    Private Function consultaU(credito As String, motivo As String, bandera As Integer) As DataTable
        Dim SSCommand As New SqlCommand("SP_REESTRUCTURA")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Credito", SqlDbType.NVarChar).Value = credito
        SSCommand.Parameters.Add("@v_usuario", SqlDbType.NVarChar).Value = tmpUSUARIO("CAT_LO_USUARIO")
        SSCommand.Parameters.Add("@v_motivo", SqlDbType.NVarChar).Value = motivo
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = bandera
        Dim dset As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return dset
    End Function
    Private Function guardar(credito As String, solicitante As String, exterior As Integer, Optional nombre As String = "") As DataTable

        Dim SSCommand as new sqlcommand
        SSCommand.CommandText = "SP_REESTRUCTURA"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Credito", SqlDbType.NVarChar).Value = credito
        SSCommand.Parameters.Add("@v_usuario", SqlDbType.NVarChar).Value = solicitante
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = 4
        SSCommand.Parameters.Add("@v_sucursal", SqlDbType.NVarChar).Value = DDLSucursal.SelectedValue
        SSCommand.Parameters.Add("@v_monto", SqlDbType.Decimal).Value = IIf(TxtMonto.Text = "", 0, TxtMonto.Text)
        SSCommand.Parameters.Add("@v_clasificacion", SqlDbType.NVarChar).Value = DDLClasificacion.SelectedText
        SSCommand.Parameters.Add("@v_plazo", SqlDbType.Decimal).Value = DDL_plazo.SelectedValue
        SSCommand.Parameters.Add("@v_tasa", SqlDbType.Decimal).Value = IIf(TxtTaza.Text = "", 0, TxtTaza.Text)
        SSCommand.Parameters.Add("@v_frecuencia", SqlDbType.NVarChar).Value = DDLFrecuenciaPago.SelectedValue
        SSCommand.Parameters.Add("@v_gliquida", SqlDbType.NVarChar).Value = DDLGarantiaLiquida.SelectedValue
        SSCommand.Parameters.Add("@v_montoliq", SqlDbType.Decimal).Value = IIf(TxtMontoLiquido.Text = "", 0, TxtMontoLiquido.Text)
        SSCommand.Parameters.Add("@v_gprendaria", SqlDbType.NVarChar).Value = DDLGarantiaPrendataria.SelectedValue
        SSCommand.Parameters.Add("@v_ghipotecaria", SqlDbType.NVarChar).Value = DDLGarantiaHipotecaria.SelectedValue
        SSCommand.Parameters.Add("@v_nomina", SqlDbType.NVarChar).Value = DDLNomina.SelectedValue
        SSCommand.Parameters.Add("@v_noempleado", SqlDbType.NVarChar).Value = TxtNumeroEmpleado.Text
        SSCommand.Parameters.Add("@v_noconvenio", SqlDbType.NVarChar).Value = TxtNumeroConvenio.Text
        SSCommand.Parameters.Add("@v_observacion", SqlDbType.NVarChar).Value = TxtObservaciones.Text
        SSCommand.Parameters.Add("@v_convenio", SqlDbType.NVarChar).Value = DDLConvenio.SelectedValue
        SSCommand.Parameters.Add("@v_noexpediente", SqlDbType.NVarChar).Value = TxtNoExpediente.SelectedValue
        SSCommand.Parameters.Add("@v_juzgado", SqlDbType.NVarChar).Value = TxtJuzgado.SelectedValue
        SSCommand.Parameters.Add("@v_nombre", SqlDbType.NVarChar).Value = nombre
        SSCommand.Parameters.Add("@v_externo", SqlDbType.Decimal).Value = exterior
        SSCommand.Parameters.Add("@v_instancia", SqlDbType.NVarChar).Value = tmpUSUARIO("CAT_LO_INSTANCIA")
        
        Dim dset As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return dset
    End Function




    Private Sub TxtNumero_cliente_TextChanged(sender As Object, e As EventArgs) Handles TxtNumero_cliente.TextChanged
        If TxtNumero_cliente.Text <> "" Then
            Dim ExisteCredito As DataTable = consultaU(TxtNumero_cliente.Text, 9)
            If ExisteCredito.Rows.Count = 0 Then
                BtnGuardar.Enabled = True
            Else
                showModal(Notificacion, "deny", "Aviso", "El credito ingresado ya se encuentra asignado; ingrese uno nuevo")
                BtnGuardar.Enabled = False
            End If
        End If
    End Sub

    Private Sub DDLGarantiaLiquida_SelectedIndexChanged(sender As Object, e As DropDownListEventArgs) Handles DDLGarantiaLiquida.SelectedIndexChanged
        If DDLGarantiaLiquida.SelectedValue = "S" Then
            pnlMontoLiquido.Visible = True
        Else
            pnlMontoLiquido.Visible = False
        End If
    End Sub

    Private Sub DDLNomina_SelectedIndexChanged(sender As Object, e As DropDownListEventArgs) Handles DDLNomina.SelectedIndexChanged
        If DDLNomina.SelectedValue = "S" Then
            pnlNumeroEmpleado.Visible = True
            pnlNumeroConvenio.Visible = True
            '  Dim prueba As String = cadenacb(DDLNumero_cliente)
            'If prueba <> "NO" Then
            '    If consultaU(prueba, 2).Rows.Count <> 0 Then
            '        TxtNumeroConvenio.Text = consultaU(prueba, 2).Rows(0).Item(0)
            '        If TxtNumeroConvenio.Text = " " Then
            '            TxtNumeroConvenio.Text = ""
            '        End If
            '    End If
            'Else
            '    showModal(Notificacion, "deny", "Aviso", "Seleccione al menos un crédito")
            '    DDLNomina.SelectedIndex = 0
            'End If
        Else
            pnlNumeroEmpleado.Visible = False
            pnlNumeroConvenio.Visible = False
        End If
    End Sub
    Private Sub Avisar(MSJ As String)
        RadAviso.RadAlert(MSJ, 350, 250, "Aviso", Nothing)
    End Sub
    Private Function cadenacb(cb As RadComboBox) As String
        If cb.CheckedItems.Count > 0 Then
            Dim regreso As String = ""
            For Each item In cb.CheckedItems
                regreso += item.Value & ","
            Next
            regreso = regreso.Substring(0, regreso.Length - 1)
            Return regreso
        End If
        Return "NO"
    End Function

    Private Sub DDLConvenio_SelectedIndexChanged(sender As Object, e As DropDownListEventArgs) Handles DDLConvenio.SelectedIndexChanged

        '
        If DDLConvenio.SelectedValue = "S" Then
            pnlNoExpediente.Visible = True
            pnlJuzgado.Visible = True
        Else
            pnlNoExpediente.Visible = False
            pnlJuzgado.Visible = False
        End If

    End Sub

    Private Sub gridInformacion_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles gridInformacion.NeedDataSource
        Try
            gridInformacion.DataSource = consultaU(creditos.Substring(0, creditos.Length - 1), 3)
        Catch ex As Exception
            gridInformacion.DataSource = Nothing
        End Try
    End Sub

    Private Sub CBCreditoExterno_CheckedChanged(sender As Object, e As EventArgs) Handles CBCreditoExterno.CheckedChanged
        If CBCreditoExterno.Checked Then
            'DDLNumero_cliente.ClearCheckedItems()
            TxtNombre_cliente.Enabled = True
            TxtNumero_cliente.Enabled = True
            'rsb1.Enabled = False
            'gridInformacion.Visible = False
            'gridInformacion.Rebind()
            'Limpiar()
        Else
            TxtNombre_cliente.Enabled = False
            TxtNumero_cliente.Enabled = False
            TxtNombre_cliente.Text = ""
            TxtNumero_cliente.Text = ""
            '    rsb1.Enabled = True
            '    gridInformacion.Visible = True
            '    gridInformacion.Rebind()
            '    Limpiar()
        End If
    End Sub

    Private Sub Limpiar()
        DDLSucursal.ClearSelection()
        TxtMonto.Text = ""
        DDLClasificacion.ClearSelection()
        DDL_plazo.SelectedIndex = 0
        TxtTaza.Text = ""
        DDLFrecuenciaPago.ClearSelection()
        DDLGarantiaLiquida.ClearSelection()
        TxtMontoLiquido.Text = ""
        DDLGarantiaPrendataria.ClearSelection()
        DDLGarantiaHipotecaria.ClearSelection()
        DDLNomina.ClearSelection()
        TxtNumeroEmpleado.Text = ""
        ' TxtNoExpediente.Text = ""
        TxtObservaciones.Text = ""
        DDLConvenio.ClearSelection()
        TxtNumeroConvenio.Text = ""
        Clacificacion = ""
        TxtJuzgado.SelectedIndex = 0
        TxtNoExpediente.SelectedIndex = 0
        pnlJuzgado.Visible = False
        pnlNoExpediente.Visible = False
        TxtTitular.Text = ""
        TxtTitular.Visible = False
        pnlMontoLiquido.Visible = False
        pnlNumeroConvenio.Visible = False
        pnlNumeroEmpleado.Visible = False
        txtmotivo.Text = ""

    End Sub

    Private Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click
        If gridInformacion.Items.Count = 0 Then
            showModal(Notificacion, "warning", "Falla", "Seleccione al menos un crédito a reestructurar")
        ElseIf CBCreditoExterno.Checked And TxtNombre_cliente.Text = "" Then
            showModal(Notificacion, "warning", "Falla", "Ingrese Nombre de Cliente Externo")
        ElseIf CBCreditoExterno.Checked And TxtNumero_cliente.Text = "" Then
            showModal(Notificacion, "warning", "Falla", "Ingrese Nombre de Cliente Externo")
        ElseIf TxtMonto.Text = "" Then
            showModal(Notificacion, "warning", "Falla", "Ingrese un Monto")
        ElseIf DDL_plazo.SelectedIndex = 0 Then
            showModal(Notificacion, "warning", "Falla", "Seleccione un Plazo")
        ElseIf TxtTaza.Text = "" Then
            showModal(Notificacion, "warning", "Falla", "Ingrese una Tasa")
        ElseIf DDLFrecuenciaPago.SelectedIndex = -1 Then
            showModal(Notificacion, "warning", "Falla", "Seleccione una Frecuencia")
        ElseIf DDLGarantiaLiquida.SelectedIndex = -1 Then
            showModal(Notificacion, "warning", "Falla", "Seleccione si tiene Garantia Liquida")
        ElseIf DDLGarantiaLiquida.SelectedValue = "S" And TxtMontoLiquido.Text = "" Then
            showModal(Notificacion, "warning", "Falla", "Ingrese un Monto de Garantia Liquida")
        ElseIf DDLGarantiaHipotecaria.SelectedIndex = -1 Then
            showModal(Notificacion, "warning", "Falla", "Seleccione si tiene Garantia Hipotecaria")
        ElseIf DDLNomina.SelectedIndex = -1 Then
            showModal(Notificacion, "warning", "Falla", "Seleccione si tiene Tratamiento de Nomina")
        ElseIf DDLNomina.SelectedValue = "S" And TxtNumeroEmpleado.Text = "" Then
            showModal(Notificacion, "warning", "Falla", "Ingrese un Numero de Empleado")
        ElseIf DDLNomina.SelectedValue = "S" And TxtNumeroConvenio.Text = "" Then
            showModal(Notificacion, "warning", "Falla", "Ingrese un Numero de Convenio")
        ElseIf TxtObservaciones.Text = "" Then
            showModal(Notificacion, "warning", "Falla", "Ingrese Observaciones")
        ElseIf DDLConvenio.SelectedIndex = -1 Then
            showModal(Notificacion, "warning", "Falla", "Seleccione si tiene Convenio Judicial")
        ElseIf DDLConvenio.SelectedValue = "S" And TxtNoExpediente.SelectedIndex = 0 Then
            showModal(Notificacion, "warning", "Falla", "Seleccione Expediente Judicial")
        ElseIf DDLConvenio.SelectedValue = "S" And TxtJuzgado.SelectedIndex = 0 Then
            showModal(Notificacion, "warning", "Falla", "Seleccione Juzgado Judicial")
        Else
            If CBCreditoExterno.Checked Then
                guardar(creditos.Substring(0, creditos.Length - 1), TxtNumero_cliente.Text, 1, TxtNombre_cliente.Text)
            Else
                guardar(creditos.Substring(0, creditos.Length - 1), tmpCredito("PR_MC_CLIENTE"), 0)
            End If
            showModal(Notificacion, "ok", "Exito", "Pre Reestructura guardada correctamente")
            Limpiar()
            gridAjenos.Rebind()
            gridPreReestructurados.Rebind()
            pnlGrids.Visible = True
            pnlReestructura.Visible = False

        End If




    End Sub
    Public Sub rsb1_Search(sender As Object, e As SearchBoxEventArgs)
        Dim s = e
        If Clacificacion = "" Or Clacificacion = consultaU(e.Value, 12).Rows(0).Item(0) Then
            Clacificacion = consultaU(e.Value, 12).Rows(0).Item(0)
            creditos += e.Value & ","
            gridInformacion.Visible = True
            gridInformacion.Rebind()
            rsb1.Text = ""
        Else
            showModal(Notificacion, "deny", "Error", "No se puede agregar crédito ya que corresponde a clasificación distinta")
            rsb1.Text = ""
        End If
    End Sub
    Private Sub gridPreReestructurados_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles gridPreReestructurados.NeedDataSource
        gridPreReestructurados.MasterTableView.Columns.FindByUniqueName("INSTANCIA").Visible = True
        Try
            gridPreReestructurados.DataSource = consultaU(tmpCredito("PR_MC_CREDITO"), 6)
            gridPreReestructurados.MasterTableView.Columns.FindByUniqueName("INSTANCIA").Visible = False
        Catch ex As Exception
            gridPreReestructurados.DataSource = Nothing
        End Try
    End Sub

    Private Sub gridAjenos_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles gridAjenos.NeedDataSource
        gridAjenos.MasterTableView.Columns.FindByUniqueName("INSTANCIA").Visible = True
        Try
            gridAjenos.DataSource = consultaU("", 7)
            gridAjenos.MasterTableView.Columns.FindByUniqueName("INSTANCIA").Visible = False
        Catch ex As Exception
            gridAjenos.DataSource = Nothing
        End Try
    End Sub

    Private Sub gridAjenos_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles gridAjenos.ItemCommand
        If e.CommandName = "onCancel" Then
            lbloc.Text = e.Item.Cells(11).Text
            ABRE(WinCancelar)
        ElseIf e.CommandName = "onAprobar" Then

            Dim s As String = Safi_SolicitudReestructuraCredito(consultaU(e.Item.Cells(11).Text, 13))
            Dim n As String() = s.Split(",")
            If n(0) = "000000" Then
                Dim Existen As DataTable = consultaU(e.Item.Cells(11).Text, 10)
                If Existen.Rows.Count <> 0 Then
                    showModal(Notificacion2, "ok", "Éxito", Existen(0)(0))
                    'Avisar("Informacion enviada")
                    gridAjenos.Rebind()
                    Response.Redirect("PreReestructura.aspx", True)
                End If
            Else
                showModal(Notificacion2, "warning", "Error", n(1))
                'Avisar(n(1))

            End If
        ElseIf e.CommandName = "onVizualizar" Then
            pnlGrids.Visible = False
            pnlReestructura.Visible = True
            visualizar(e.Item.Cells(11).Text)
            previsualizar(False)
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "myFuncionAlerta", "realizarPostBack(true);", True)
        End If
    End Sub

    Private Sub gridPreReestructurados_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles gridPreReestructurados.ItemCommand
        If e.CommandName = "onCancel" Then
            'Dim Existen As datatable = consultaU(e.Item.Cells(10).Text, 8)
            'If Existen.Rows.Count <> 0 Then
            '    showModal(Notificacion, "ok", "Éxito", Existen(0)(0))
            '    gridPreReestructurados.Rebind()
            'End If
            lbloc.Text = e.Item.Cells(10).Text
            ABRE(WinCancelar)
        ElseIf e.CommandName = "onAprobar" Then

            Dim s As String = Safi_SolicitudReestructuraCredito(consultaU(e.Item.Cells(10).Text, 13))
            Dim n As String() = s.Split(",")
            If n(0) = "000000" Then
                Dim Existen As DataTable = consultaU(e.Item.Cells(10).Text, 10)
                If Existen.Rows.Count <> 0 Then
                    showModal(Notificacion2, "ok", "Éxito", Existen(0)(0))
                    ' Avisar("Informacion enviada")
                    gridPreReestructurados.Rebind()
                    Response.Redirect("PreReestructura.aspx", True)
                End If
            Else
                showModal(Notificacion2, "warning", "Error", n(1))
                'Avisar(n(1))
            End If
        ElseIf e.CommandName = "onVizualizar" Then
            pnlGrids.Visible = False
            pnlReestructura.Visible = True
            visualizar(e.Item.Cells(10).Text)
            previsualizar(False)
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "myFuncionAlerta", "realizarPostBack(true);", True)
        End If
    End Sub

    Private Sub btnGenerarPreReestructura_Click(sender As Object, e As EventArgs) Handles btnGenerarPreReestructura.Click
        pnlGrids.Visible = Not pnlGrids.Visible
        pnlReestructura.Visible = Not pnlReestructura.Visible
        btnGenerarPreReestructura.Text = IIf(pnlReestructura.Visible, "Regresar", "Generar Pre-Reestructura")
        creditos = ""
        previsualizar(True)
        Limpiar()
        gridInformacion.Rebind()
    End Sub

    Private Sub gridInformacion_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles gridInformacion.ItemCommand
        If e.CommandName = "Quitar" Then
            Dim creditos_seprados As String() = creditos.Split(",")
            creditos = ""
            For i = 0 To creditos_seprados.Count - 1
                If creditos_seprados(i) <> creditos_seprados.ElementAt(e.Item.ItemIndex) Then
                    creditos += creditos_seprados(i) + ","
                End If
            Next
        End If
        creditos = creditos.Replace(",,", ",")
        gridInformacion.Rebind()
    End Sub
    Private Sub previsualizar(esta As Boolean)
        rsb1.Enabled = esta
        CBCreditoExterno.Enabled = esta
        TxtNumero_cliente.Enabled = esta
        TxtNombre_cliente.Enabled = esta
        gridInformacion.Enabled = esta
        DDLSucursal.Enabled = esta
        TxtMonto.Enabled = esta
        DDL_plazo.Enabled = esta
        TxtTaza.Enabled = esta
        DDLClasificacion.Enabled = esta
        DDLFrecuenciaPago.Enabled = esta
        DDLGarantiaLiquida.Enabled = esta
        pnlMontoLiquido.Enabled = esta
        DDLGarantiaPrendataria.Enabled = esta
        DDLGarantiaHipotecaria.Enabled = esta
        DDLNomina.Enabled = esta
        pnlNumeroEmpleado.Enabled = esta
        pnlNumeroConvenio.Enabled = esta
        TxtObservaciones.Enabled = esta
        DDLConvenio.Enabled = esta
        pnlNoExpediente.Enabled = esta
        pnlJuzgado.Enabled = esta
        BtnGuardar.Enabled = esta
    End Sub
    Private Sub visualizar(ByVal id As Integer)
        btnGenerarPreReestructura.Text = "Regresar"
        dim SSCommand as new sqlcommand
        SSCommand.CommandText = "SP_REESTRUCTURA"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_id", SqlDbType.NVarChar).Value = id
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = 11
        
        Dim drow As DataRow = Consulta_Procedure(SSCommand, "ELEMENTOS").Rows(0)
        If drow.Item("NOMBRE") <> " " Then
            CBCreditoExterno.Checked = True
            TxtNumero_cliente.Text = drow.Item("CREDITOSOL")
            TxtNombre_cliente.Text = drow.Item("NOMBRE")
        Else
            CBCreditoExterno.Checked = False
        End If

        creditos = drow.Item("CREDITO") & ","
        gridInformacion.Visible = True
        gridInformacion.Rebind()


        TxtTitular.Visible = True
        TxtTitular.Text = drow.Item("TITULAR")
        DDLSucursal.SelectedValue = drow.Item("SUCURSAL")
        TxtMonto.Text = drow.Item("MONTO")
        DDL_plazo.SelectedValue = drow.Item("PLAZO")
        TxtTaza.Text = drow.Item("TASA")
        'DDLClasificacion.SelectedValue = drow.Item("CLASIFICACION")
        DDLFrecuenciaPago.SelectedValue = drow.Item("FRECUENCIA")
        DDLGarantiaLiquida.SelectedValue = drow.Item("GLIQUIDA")
        If drow.Item("GLIQUIDA") = "S" Then
            pnlMontoLiquido.Visible = True
            TxtMontoLiquido.Text = drow.Item("MONTOLIQUIDO")
        End If

        DDLGarantiaPrendataria.SelectedValue = drow.Item("GPRENDATARIA")
        DDLGarantiaHipotecaria.SelectedValue = drow.Item("GHIPOTECARIA")
        DDLNomina.SelectedValue = drow.Item("NOMINA")
        If drow.Item("NOMINA") = "S" Then
            pnlNumeroEmpleado.Visible = True
            pnlNumeroConvenio.Visible = True
            TxtNumeroConvenio.Text = drow.Item("NOCONVENIO")
            TxtNumeroEmpleado.Text = drow.Item("NOEMPLEADO")
        End If

        TxtObservaciones.Text = drow.Item("OBSERVACION")
        DDLConvenio.SelectedValue = drow.Item("CONVENIOJUD")
        If drow.Item("CONVENIOJUD") = "S" Then
            pnlNoExpediente.Visible = True
            pnlJuzgado.Visible = True
            TxtNoExpediente.SelectedText = drow.Item("NOEXPEDIENTE")
            TxtJuzgado.SelectedText = drow.Item("JUZGADO")
        End If

        If drow.Item("MOTIVO") <> " " Then
            PanelCancela.Visible = True
            TxtCancelacion.Text = drow.Item("MOTIVO")
        Else
            PanelCancela.Visible = False
            TxtCancelacion.Text = ""
        End If

    End Sub

    Private Sub pnlReestructura_Load(sender As Object, e As EventArgs) Handles pnlReestructura.Load

    End Sub

    Protected Function Safi_SolicitudReestructuraCredito(dset As DataTable) As String
        Try
            Dim usuarioEnvioID As String = tmpUSUARIO("CAT_LO_ID")
            Dim usuarioEnvioNombre As String = tmpUSUARIO("CAT_LO_NOMBRE")
            Dim usuarioIP As String = Page.Request.ServerVariables("REMOTE_HOST")
            Dim clienteID As String = dset.Rows(0).Item("clienteid")
            Dim sucursalReestructuraID As String = dset.Rows(0).Item("sucursalID")
            Dim montoReestructura As String = dset.Rows(0).Item("monto")
            Dim plazoReestructura As String = dset.Rows(0).Item("plazo")
            Dim tasaReestructura As String = dset.Rows(0).Item("tasa")
            Dim frecuenciaReestructura As String = dset.Rows(0).Item("frecuencia")
            Dim requiereGarLiq As String = dset.Rows(0).Item("gliquida")
            Dim montoGarLiq As String = dset.Rows(0).Item("mliquido")
            Dim requiereGarPrenda As String = dset.Rows(0).Item("gprendaria")
            Dim requiereGarHipo As String = dset.Rows(0).Item("ghipo")
            Dim tipoInstancia As String = dset.Rows(0).Item("INSTANCIA") 'n no esta asignado,p= preventiva, a = administrativa, e= extrajudicial y j judicial
            Dim despachoID As String = tmpUSUARIO("DESPACHOID")
            Dim despachoNombre As String = tmpUSUARIO("DESPACHONOMBRE")
            Dim tipoDespacho As String = tmpUSUARIO("TIPODESPACHO")
            Dim abogadoID As String = tmpUSUARIO("ABOGADOID")
            Dim abogadoNombre As String = tmpUSUARIO("ABOGADONOMBRE")
            Dim supervisorID As String = tmpUSUARIO("SUPERVISORID")
            Dim supervisorNombre As String = tmpUSUARIO("SUPERVISORNOMBRE")
            Dim trtamientoNomina As String = dset.Rows(0).Item("nomina")
            Dim numEmpleado As String = dset.Rows(0).Item("Nempleado")
            Dim numConvenio As String = dset.Rows(0).Item("nconvenio")
            Dim tieneConvenioJudicial As String = dset.Rows(0).Item("conveniojud")
            Dim numExpedienteJudicial As String = dset.Rows(0).Item("NExpediente")
            Dim juzgado As String = dset.Rows(0).Item("juzgado")
            Dim observaciones As String = dset.Rows(0).Item("observaciones")
            '-----creditosPorReestructurar----------------
            Dim cpr_clienteID As String = dset.Rows(0).Item("creditosREe")
            Dim cpr_creditoID As String = ""
            Dim cpr_saldoRequerido As String = ""
            '---------------------------------------------
            Dim cadacredito As String() = Split(dset.Rows(0).Item("creditosREe"), ",")
            Dim arreglocreditos As String = ""
            For i = 0 To cadacredito.Length - 1
                Dim drow As DataRow = consultaU(cadacredito(i), 14).Rows(0)
                arreglocreditos += " {" & vbCrLf &
                        " ""clienteID"":""" & drow.Item(0) & """," & vbCrLf &
                        " ""creditoID"":""" & cadacredito(i) & """," & vbCrLf &
                        " ""saldoRequerido"":""" & drow.Item(1) & """" & vbCrLf &
                        " },"
            Next
            arreglocreditos = arreglocreditos.Substring(0, arreglocreditos.Length - 1)

            Dim v_endpoint As String = Db.StrEndPoint("SAFI", 1)
            Dim v_metodo As String = "cartera/SolicitudReestructuraCredito"


            Dim data As String = "{" & vbCrLf &
            " ""usuarioEnvioID"":""" & usuarioEnvioID & """," & vbCrLf &
            " ""usuarioEnvioNombre"":""" & usuarioEnvioNombre & """," & vbCrLf &
            " ""usuarioIP"":""" & usuarioIP & """," & vbCrLf &
            " ""clienteID"":""" & clienteID & """," & vbCrLf &
            " ""sucursalReestructuraID"":""" & sucursalReestructuraID & """," & vbCrLf &
            " ""montoReestructura"":""" & montoReestructura & """," & vbCrLf &
            " ""plazoReestructura"":""" & plazoReestructura & """," & vbCrLf &
            " ""tasaReestructura"":""" & tasaReestructura & """," & vbCrLf &
            " ""frecuenciaReestructura"":""" & frecuenciaReestructura & """," & vbCrLf &
            " ""requiereGarLiq"":""" & requiereGarLiq & """," & vbCrLf &
            " ""montoGarLiq"":""" & montoGarLiq & """," & vbCrLf &
            " ""requiereGarPrenda"":""" & requiereGarPrenda & """," & vbCrLf &
            " ""requiereGarHipo"":""" & requiereGarHipo & """," & vbCrLf &
            " ""tipoInstancia"":""" & tipoInstancia & """," & vbCrLf &
            " ""despachoID"":""" & despachoID & """," & vbCrLf &
            " ""despachoNombre"":""" & despachoNombre & """," & vbCrLf &
            " ""tipoDespacho"":""" & tipoDespacho & """," & vbCrLf &
            " ""abogadoID"":""" & abogadoID & """," & vbCrLf &
            " ""abogadoNombre"":""" & abogadoNombre & """," & vbCrLf &
            " ""supervisorID"":""" & supervisorID & """," & vbCrLf &
            " ""supervisorNombre"":""" & supervisorNombre & """," & vbCrLf &
            " ""trtamientoNomina"":""" & trtamientoNomina & """," & vbCrLf &
            " ""numEmpleado"":""" & numEmpleado & """," & vbCrLf &
            " ""numConvenio"":""" & numConvenio & """," & vbCrLf &
            " ""tieneConvenioJudicial"":""" & tieneConvenioJudicial & """," & vbCrLf &
            " ""numExpedienteJudicial"":""" & numExpedienteJudicial & """," & vbCrLf &
            " ""juzgado"":""" & juzgado & """," & vbCrLf &
            " ""observaciones"":""" & observaciones & """," & vbCrLf &
            " ""creditosPorReestructurar"": [" & vbCrLf &
            arreglocreditos &
                 " ]" & vbCrLf &
              "}" & vbCrLf

            Dim client = New RestClient(v_endpoint & v_metodo)
            Dim request = New RestRequest(Method.POST)
            request.AddHeader("cache-control", "no-cache")
            request.AddHeader("Connection", "keep-alive")
            request.AddHeader("Content-Length", data.Length)
            request.AddHeader("Accept", "*/*")
            request.AddHeader("Content-Type", "application/json")
            request.AddHeader("autentificacion", "dXN1YXJpb1BydWViYVdTOjEyMw==")
            request.AddParameter("undefined", data, ParameterType.RequestBody)

            Dim response As IRestResponse = client.Execute(request)

            If response.StatusCode.ToString = "OK" Then


                Return ""

            Else
                Return "Ña" & "," & "Error de comunicacion con el servidor"

            End If



        Catch ex As WebException
            Dim abd As String = ex.Message

            Return "Ño" & "," & abd
        End Try

    End Function
    Protected Sub ABRE(win As RadWindow)
        Dim script As String = "function f(){$find(""" + win.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
    End Sub
    Protected Sub CIERRA(win As RadWindow)
        Dim script As String = "function f(){$find(""" + win.ClientID + """).close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        consultaU(lbloc.Text, txtmotivo.Text, 8)
        showModal(Notificacion, "ok", "Éxito", "Cancelado")
        gridPreReestructurados.Rebind()
        txtmotivo.Text = ""
        CIERRA(WinCancelar)
    End Sub

    Private Sub txtmotivo_TextChanged(sender As Object, e As EventArgs) Handles txtmotivo.TextChanged
        If txtmotivo.Text <> "" Then
            btnCancelar.Enabled = True
        Else
            btnCancelar.Enabled = False
        End If
    End Sub
End Class
