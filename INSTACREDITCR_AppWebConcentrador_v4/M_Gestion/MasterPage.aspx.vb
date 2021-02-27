Imports Conexiones
Imports System.Data.SqlClient
Imports System.Data
Imports Db
Imports Funciones
Imports Telerik.Web.UI
Imports System.Web.Services
Imports AvisosPDF
Imports System.IO
Imports RestSharp
Imports System.Net


Partial Class MGestion_MasterPage
    Inherits System.Web.UI.Page
    Public Property tmpUSUARIO As IDictionary
        Get
            Return CType(Session("USUARIO"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("USUARIO") = value
        End Set
    End Property
    <WebMethod(EnableSession:=True)>
    Public Shared Function KeepActiveSession(ByVal Usuario As String) As String
        Try
            Dim DtsConectado As DataTable = Class_Sesion.LlenarElementos(Usuario, "", "", "", 3, "", "", "", "")
            If DtsConectado.Rows(0).Item("Cuantas") <> "0" Then
                Return "Hola"
            Else
                Return "Bye"
            End If
        Catch ex As Exception
            Return "Bye"
        End Try
    End Function

    Public Property tmpCredito As IDictionary
        Get
            Return CType(Session("Credito"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("Credito") = value
        End Set
    End Property

    Public Property tmpGarantias As IDictionary
        Get
            Return CType(Session("garantias"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("garantias") = value
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

    Private Function CleanCampo(dato As String) As String
        If String.IsNullOrEmpty(dato) Or String.IsNullOrWhiteSpace(dato) Then
            Return ""
        End If
        Return dato
    End Function

    'Private Function bCredito(dato As String) As datatable
    '    Dim SSCommand As New SqlCommand
    '    SSCommand.CommandText = "SP_VARIOS_QUERIES"
    '    SSCommand.CommandType = CommandType.StoredProcedure
    '    SSCommand.Parameters.Add("@V_Usuario", SqlDbType.NVarChar).Value = dato
    '    SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 6
    '    Return Consulta_Procedure(SSCommand, SSCommand.CommandText)
    'End Function

    Private Sub MGestion_MasterPage_Init(sender As Object, e As EventArgs) Handles Me.Init
        If Session("USUARIO") IsNot Nothing Then
            LblCat_Lo_Usuario.Text = tmpUSUARIO("CAT_LO_USUARIO")
            Dim ABC As String = tmpPermisos("CHAT_WHATSAPP")
            Dim sessionactive As Integer = GetSessionActive.Search(tmpUSUARIO("CAT_LO_USUARIO"), 1)
            If sessionactive = 0 Then
                Response.Redirect("~/Login.aspx")
            ElseIf Not tmpUSUARIO("CAT_PE_A_GESTION").ToString.Contains("1") Then
                Response.Redirect("~/Modulos.aspx")
            Else
                'Try
                If Not IsPostBack Then
                    Dim TmpAplicacion As New Aplicacion(0, 0, 0, 0, 0, 0, 0)
                    Session("Aplicacion") = TmpAplicacion
                    LlenarAplicacion.APLICACION()
                End If
                'Catch ex As System.Threading.ThreadAbortException
                'Catch ex As Exception
                'End Try
            End If
        Else
            OffLine(LblCat_Lo_Usuario.Text)
            Session.Clear()
            Session.Abandon()
            'Response.Redirect("~/SesionExpirada.aspx")
        End If
    End Sub
    Private Sub Page_Load(sender As Object, e As System.EventArgs) Handles MyBase.Load
        Dim DtsChat As DataTable = Nothing

        TxtHist_Pr_Dtepromesa.MinDate = Today
        If Session("USUARIO") IsNot Nothing Then
            Try
                listViewAvisos.Rebind()
                RadWindow1.OpenerElementID = BtnNota.ClientID
                RadWindow2.OpenerElementID = BtnCalculadora.ClientID
                RadWindow5.OpenerElementID = BtnChatWhat.ClientID
                'RadWindow4.OpenerElementID = btnGeneraAviso.ClientID
                'RadWindow3.OpenerElementID = BtnGestion.ClientID

                If Not IsPostBack Then
                    BtnModulo.Visible = Not Session("Unicomodulo")
                    'configure the notification to automatically show 1 min before session expiration
                    RadNotification1.ShowInterval = (Session.Timeout - 1) * 60 * 1000
                    ''set the redirect url as a value for an easier and faster extraction in on the client
                    RadNotification1.Value = Page.ResolveClientUrl("~/SesionExpirada.aspx")
                    lblUsr.Text = "Usuario: " & CleanCampo(tmpUSUARIO("CAT_LO_USUARIO"))
                    LblCUANTASPP.Text = CleanCampo(tmpUSUARIO("CUANTASPP")).Split(".")(0)
                    LblGESTIONES.Text = CleanCampo(tmpUSUARIO("GESTIONES")).Split(".")(0)
                    LblMONTOPP.Text = to_money(CleanCampo(tmpUSUARIO("MONTOPP")))
                    LblCat_Lo_Usuario.Text = CleanCampo(tmpUSUARIO("CAT_LO_USUARIO"))
                    LblCat_Lo_Nombre.Text = CleanCampo(tmpUSUARIO("CAT_LO_NOMBRECOMPLETO"))
                    LblCat_Pe_Perfil.Text = CleanCampo(tmpUSUARIO("CAT_PE_PERFIL"))
                    LblHorario.Text = CleanCampo(tmpUSUARIO("CAT_LO_HENTRADA").ToString & " - " & tmpUSUARIO("CAT_LO_HSALIDA"))
                    LblCat_Lo_Supervisor.Text = CleanCampo(tmpUSUARIO("CAT_LO_SUPERVISOR"))
                    RCBTipoAvisos.DataTextField = "TIPO"
                    RCBTipoAvisos.DataValueField = "TIPO"
                    RCBTipoAvisos.DataSource = AvisoGestion.traetiposalertas(CleanCampo(tmpUSUARIO("CAT_LO_USUARIO")))
                    RCBTipoAvisos.DataBind()

                    'Try
                    '    'If Request.Url.ToString().Split("?")(1).Split("&")(0).Replace("cuenta=", "").Split("_")(0) <> "" Then
                    '    'Session("CalledId") = Request.Url.ToString().Split("?")(1).Split("&")(1).Replace("CalledId=", "").Replace("]", "").Replace("-", "").Replace("[", "")
                    '    ' Llenar(Request.Url.ToString().Split("?")(1).Split("&")(0).Replace("cuenta=", "").Split("_")(0))
                    '    'LblMsj.Text = Request.Url.ToString()
                    '    ' MpuMensajes.Show()
                    '    'End If

                    '    Dim mId As String = Request.QueryString("V_ID_Nimbus")
                    '    Dim mTelefono As String = Request.QueryString("V_Telefono")
                    '    Dim mExpediente As String = Request.QueryString("V_Expediente")

                    '    Session("CallerId") = mId
                    '    Session("mTelefono") = mTelefono
                    '    Session("mExpediente") = mExpediente

                    '    If mExpediente <> Nothing Then
                    '        Dim mCredito As String = bCredito(mExpediente)
                    '        TxtHist_Ge_Telefono.Text = mTelefono
                    '        Class_MasterPage.Tocar(mCredito, tmpUSUARIO("CAT_LO_USUARIO"))
                    '        tmpCredito = LlenarCredito.Busca(mCredito)
                    '    End If

                    'Catch ex As Exception
                    '    Session("CalledId") = ""
                    '    Session("mTelefono") = ""
                    '    showModal(Notificacion, "warning", "Aviso", "El Expediente " & Session("mExpediente") & " no se encuentre en la base")
                    'End Try


                    Llenar()

                    If tmpCredito Is Nothing Then
                        LblRetirado.Visible = False
                        PnlInfoRapida.Visible = False
                        'RadTabStrip1.Enabled = False
                        'RadTabStrip1.Tabs(1).Enabled = False
                        'RadTabStrip1.Tabs(2).Enabled = False
                        'RadTabStrip1.Tabs(3).Enabled = False
                        'RadTabStrip1.Tabs(4).Enabled = False
                        'RadTabStrip1.Tabs(5).Enabled = False
                        'RadTabStrip1.Tabs(6).Enabled = False
                        'RadTabStrip1.Tabs(7).Enabled = False
                        'RadTabStrip1.Tabs(8).Enabled = False
                        'RadTabStrip1.Tabs(9).Enabled = False
                        'RadTabStrip1.Tabs(10).Enabled = False
                        'RadTabStrip1.Tabs(11).Enabled = False
                        'RadTabStrip1.Tabs(12).Enabled = False
                        'RadTabStrip1.Tabs(13).Enabled = False
                        'RadTabStrip1.Tabs(14).Enabled = False
                        'RadTabStrip1.Tabs(15).Enabled = False
                        'RadTabStrip1.Tabs(16).Enabled = False
                        'RadTabStrip1.Tabs(17).Enabled = True
                        For Each tab As RadTab In RadTabStrip1.Tabs
                            tab.Enabled = False
                        Next
                        RadTabStrip1.Tabs.FindTabByValue("10").Enabled = True
                        BtnGestion_Disable("true")
                    Else
                        Limpiar()

                        DdlHist_Ge_Accion.ClearSelection()
                        DdlHist_Ge_Resultado.ClearSelection()
                        'DdlHist_Ge_NoPago.ClearSelection()
                        ValidaConfiguracion()
                        'TxtHist_Pr_Dtepromesa.MinDate = New Date(Now.Year, Now.Month, Now.Day, 0, 0, 0)
                        PnlInfoRapida.Visible = True
                        LblName.Text = CleanCampo(tmpCredito("PR_Nombre"))
                        LblCredit.Text = CleanCampo(tmpCredito("PR_MC_CREDITO"))
                        LblFolio.Text = CleanCampo(tmpCredito("PR_MC_EXPEDIENTE"))
                        LblDteAsignacion.Text = CleanCampo(tmpCredito("PR_MC_DTEASIGNA")).Split(" ")(0)

                        TxtHist_Ge_Telefono.Text = Session("mTelefono")

                        'Dim abogado = "", despacho = ""
                        'Try
                        '    Dim Dts As DataTable = SP.INFORMACION_CREDITO(CleanCampo(tmpCredito("PR_MC_UASIGNADO")), 8)
                        '    abogado = Dts(0)(0).ToString
                        '    despacho = Dts(0)(1).ToString
                        'Catch ex As Exception
                        '    abogado = "No Asignado*"
                        '    despacho = "No Asignado*"
                        'Finally
                        '    LblAbogado.Text = abogado
                        '    LblDespacho.Text = despacho
                        'End Try

                        LblAbogado.Text = CleanCampo(tmpCredito("PR_MC_UASIGNADOA"))
                        LblDespacho.Text = CleanCampo(tmpCredito("PR_MC_DESPACHOASIGNADO"))
                        LblExclusion.Text = CleanCampo(tmpCredito("PR_MC_EXCLUSION"))

                        If CleanCampo(tmpCredito("PR_MC_ESTATUS")) = "Retirada" Then
                            BtnGestion_Disable("true")
                            LblRetirado.Visible = True
                        Else
                            LblRetirado.Visible = False
                            'If CleanCampo(tmpPermisos("GESTION_CALIFICACION_TELEFONICA")) Then
                            If 1 = 1 Then
                                BtnGestion_Disable("false")
                            Else
                                BtnGestion_Disable("true")
                            End If
                        End If
                    End If
                    If Session("Modulounico") Then
                        BtnModulo.Visible = Not Session("Modulounico")
                    End If
                End If
            Catch ex As Exception
                EnviarCorreo("Gestion", "MasterPage.aspx", "Page_Load", ex, "", "", LblCat_Lo_Usuario.Text)
            End Try
        End If
    End Sub

    Public Function validarAcceso(permiso As Integer) As Boolean
        Return True
    End Function
    'Protected Sub BtnChatWhat_click(sender As Object, e As EventArgs) Handles BtnChatWhat.Click
    '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "radalert", "document.getElementById('PnlChatWhat').style.display='block';return false;")
    'End Sub

    'Protected Sub RBEnviar_click(sender As Object, e As EventArgs) Handles RBEnviar.Click
    '    Dim DtsChat As DataTable
    '    PnlChatWhat.Style.Add("display", "block")

    '    DtsChat = SP.CHAT_WHATSAPP(0, tmpCredito("PR_MC_CREDITO"), TxtMensaje.Text, tmpUSUARIO("CAT_LO_USUARIO"), "MC")
    '        If DtsChat.Rows(0).Item("ORIGEN") = "MC" Then
    '            DtsChat.Rows(0).Item("ORIGEN") = ""
    '        End If

    '    Dim DtsChat1 As DataTable = SP.CHAT_WHATSAPP(0, tmpCredito("PR_MC_CREDITO"), TxtMensaje.Text, tmpUSUARIO("CAT_LO_USUARIO"), "MC")
    '        Dim Renglon As DataRow = DtsChat.NewRow()
    '        Renglon("ORIGEN") = ""
    '        Renglon("MENSAJE") = DtsChat1.Rows(0).Item("MENSAJE")
    '        DtsChat.Rows.Add(Renglon)


    '    GrdWhatsapp.DataSource = DtsChat
    '    GrdWhatsapp.DataBind()

    '    'If GrdWhatsapp.Items(0)(1).ToString = "" Then
    '    '    Dim miDataTable As New DataTable
    '    '    miDataTable.Columns.Add("Nombre")
    '    '    miDataTable.Columns.Add("Sexo")
    '    'End If

    '    'miDataTable = GrdWhatsapp
    '    'Dim Renglon As DataRow = miDataTable.NewRow()
    '    'Renglon("Nombre") = ""
    '    'Renglon("Sexo") = TxtMensaje.Text
    '    'miDataTable.Rows.Add(Renglon)

    '    'GrdWhatsapp.DataSource = miDataTable
    '    'GrdWhatsapp.DataBind()
    '    TxtMensaje.Text = ""
    'End Sub

    Protected Sub BtnGestion_Disable(valor As String)
        'Dim gg As String = "$('#BtnGestion').attr('disabled'," & valor & ");$('#divGestion').removeClass('w3-show');"
        'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "btn", gg, True)
        If valor = "true" Then
            btnGestion.Enabled = False
        Else
            btnGestion.Enabled = True
        End If
    End Sub
    Protected Sub OnCallbackUpdate2(sender As Object, e As RadNotificationEventArgs)

    End Sub


    Protected Sub Llenar()
        Dim Credito As String = ""
        Dim Produto As String = ""

        Dim dtsFilas As DataTable = Class_Agenda.LlenarElementosAgenda("", tmpUSUARIO("CAT_LO_USUARIO"), 9) 'valida si hay creditos en fila de trabajo
        Credito = dtsFilas.Rows(0).Item("CREDITO")
        Produto = dtsFilas.Rows(0).Item("PRODUCTO")
        'Credito = "NA"
        'Produto = "NA"


        If Credito <> "NA" And Session("BUSCAR") = 0 Then
            Class_MasterPage.Tocar(Credito, LblCat_Lo_Usuario.Text, Produto)
            tmpCredito = LlenarCredito.Busca(Credito, "CREDIFIEL")
            Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Gestión", "Visualizo crédito " & Credito & " de fila de trabajo")
            imgNext.Visible = True
            tmpCredito("PR_MC_CUENTATRABAJADAFILA") = "0"
            Session("NombreFila") = dtsFilas.Rows(0).Item("NomFila").ToString
            lblNomFila.Text = Session("NombreFila")
            lblNomFila.Visible = True
            RadTabStrip1.SelectedIndex = 1
            RadMultiPage1.SelectedIndex = 1
        ElseIf Credito <> "NA" And Session("BUSCAR") = 1 Then
            imgNext.Visible = True
            lblNomFila.Text = Session("NombreFila")
            lblNomFila.Visible = True
            RadTabStrip1.SelectedIndex = 1
            RadMultiPage1.SelectedIndex = 1

        Else
            imgNext.Visible = False
            Session("NombreFila") = ""
            lblNomFila.Visible = False
        End If
    End Sub

    Private Sub imgNext_Click(sender As Object, e As EventArgs) Handles imgNext.Click
        If tmpCredito("PR_MC_CUENTATRABAJADAFILA") = "0" Then
            showModal(Notificacion, "warning", "Aviso", "Debe trabajar el credito antes de continuar.")
        Else
            Dim Credito As String = ""
            Dim Producto As String = ""

            Dim dtsFilas As DataTable = Class_Agenda.LlenarElementosAgenda("", tmpUSUARIO("CAT_LO_USUARIO"), 9) 'valida si hay creditos en fila de trabajo
            Credito = dtsFilas.Rows(0).Item("Credito")
            Producto = dtsFilas.Rows(0).Item("Producto")

            'If Credito <> "NA" And Session("BUSCAR") = 0 Then
            '    imgNext.Visible = True

            If Credito <> "NA" Then 'And Session("BUSCAR") = 1
                Class_MasterPage.Tocar(Credito, LblCat_Lo_Usuario.Text, Producto)
                tmpCredito = LlenarCredito.Busca(Credito, "CREDIFIEL")
                imgNext.Visible = True
                tmpCredito("PR_MC_CUENTATRABAJADAFILA") = "0"
                Session("NombreFila") = dtsFilas.Rows(0).Item("NomFila").ToString
                lblNomFila.Text = Session("NombreFila")
                lblNomFila.Visible = True
                Limpiar()
                ValidaConfiguracion()
                'TxtHist_Pr_Dtepromesa.MinDate = New Date(Now.Year, Now.Month, Now.Day, 0, 0, 0)
                LblRetirado.Visible = False
                GvCalTelefonos.Rebind()
                LblName.Text = CleanCampo(tmpCredito("PR_Nombre"))
                LblCredit.Text = CleanCampo(tmpCredito("PR_MC_CREDITO"))
                LblFolio.Text = CleanCampo(tmpCredito("PR_MC_EXPEDIENTE"))
                LblDteAsignacion.Text = CleanCampo(tmpCredito("PR_MC_DTEASIGNA")).Split(" ")(0)
            Else
                showModal(Notificacion, "deny", "Aviso", "No hay más créditos para trabajar.")
                imgNext.Visible = False
            End If
        End If
    End Sub

    Private Sub MGestion_MasterPage_Error(sender As Object, e As EventArgs) Handles Me.[Error]
        'showModal("Error", TryCast(sender, ASP.masterpage_aspx).Context.Error.Message)
    End Sub

    Public Function nvl(text As String) As String
        Return IIf(text = "", " ", text)
    End Function

    <WebMethod>
    Public Shared Function GetResults(context As SearchBoxContext) As SearchBoxItemData()
        Try
            Dim data As DataTable = Busquedas.Search("'%" & context.Text.ToUpper & "%'")
            Dim result As New List(Of SearchBoxItemData)()

            For i As Integer = 0 To data.Rows.Count - 1
                Dim itemData As New SearchBoxItemData()
                Dim producto As String = data.Rows(i)("Producto").ToString()
                itemData.Text = data.Rows(i)("Credito").ToString() & "$$" & data.Rows(i)("Nombre").ToString() & "$$" & data.Rows(i)("RFC").ToString() & "$$" & data.Rows(i)(5).ToString() & "$$" & IIf(producto = "MERCADO ABIERTO", "CLASICO PAGO FIJO", producto) & "$$" & data.Rows(i)("Expediente").ToString() & "$$" & data.Rows(i)("No.Cliente").ToString() & "$$" & data.Rows(i)("Expediente Jud").ToString()
                itemData.Value = data.Rows(i)("Credito").ToString()
                result.Add(itemData)
            Next
            Return result.ToArray()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Sub rsb1_Search(sender As Object, e As SearchBoxEventArgs)
        rsb1.Text = ""
        If e.Value <> "" And e.Value <> "SinCredito" Then
            Try
                Dim credit As String = e.Value

                Dim vCS() As String = e.Text.Split("$")
                Dim producto As String = vCS(8)

                Class_MasterPage.Tocar(credit, tmpUSUARIO("CAT_LO_USUARIO"), producto)
                tmpCredito = LlenarCredito.Busca(credit, producto)
                Session("Buscar") = 1
                tmpCredito("PR_MC_CUENTATRABAJADAFILA") = "1"
                PnlInfoRapida.Visible = True
                'RadTabStrip1.Enabled = True
                For Each tab As RadTab In RadTabStrip1.Tabs
                    tab.Enabled = True
                Next
                'RadTabStrip1.Tabs(1).Enabled = True
                'RadTabStrip1.Tabs(2).Enabled = True
                'RadTabStrip1.Tabs(3).Enabled = True
                'RadTabStrip1.Tabs(4).Enabled = True
                'RadTabStrip1.Tabs(5).Enabled = True
                'RadTabStrip1.Tabs(6).Enabled = True
                'RadTabStrip1.Tabs(7).Enabled = True
                'RadTabStrip1.Tabs(8).Enabled = True
                'RadTabStrip1.Tabs(9).Enabled = True
                'RadTabStrip1.Tabs(10).Enabled = True
                'RadTabStrip1.Tabs(11).Enabled = True
                'RadTabStrip1.Tabs(12).Enabled = True
                'RadTabStrip1.Tabs(13).Enabled = True
                'RadTabStrip1.Tabs(14).Enabled = True
                LblName.Text = tmpCredito("PR_Nombre")
                LblCredit.Text = tmpCredito("PR_MC_CREDITO")
                LblFolio.Text = tmpCredito("PR_MC_EXPEDIENTE")
                LblDteAsignacion.Text = tmpCredito("PR_MC_DTEASIGNA").ToString.Split(" ")(0)
                'Dim abogado = "", despacho = ""
                'Try
                '    Dim Dts As DataTable = SP.INFORMACION_CREDITO(tmpCredito("PR_MC_UASIGNADO"), 8)
                '    abogado = Dts(0)(0).ToString
                '    despacho = Dts(0)(1).ToString
                'Catch ex As Exception
                '    abogado = "No Asignado*"
                '    despacho = "No Asignado*"
                'Finally
                '    LblAbogado.Text = abogado
                '    LblDespacho.Text = despacho
                'End Try
                LblAbogado.Text = CleanCampo(tmpCredito("PR_MC_UASIGNADOA"))
                LblDespacho.Text = CleanCampo(tmpCredito("PR_MC_DESPACHOASIGNADO"))
                LblExclusion.Text = CleanCampo(tmpCredito("PR_MC_EXCLUSION"))

                ' DDLParticipante.ClearSelection()
                ' LLENAR_DROP2(37, tmpCredito("PR_MC_CREDITO"), DDLParticipante, "ValPar", "TexPar")
                'If tmpPermisos("GESTION_CALIFICACION_TELEFONICA") = "1" Then
                If 1 = 1 Then
                    If tmpCredito("PR_MC_ESTATUS") = "Retirada" Then
                        BtnGestion_Disable("true")
                        LblRetirado.Visible = True
                    Else
                        Limpiar()
                        DdlHist_Ge_Accion.ClearSelection()
                        DdlHist_Ge_Resultado.ClearSelection()
                        'DdlHist_Ge_NoPago.ClearSelection()
                        ValidaConfiguracion()
                        'TxtHist_Pr_Dtepromesa.MinDate = New Date(Now.Year, Now.Month, Now.Day, 0, 0, 0)
                        LblRetirado.Visible = False
                        BtnGestion_Disable("false")
                        GvCalTelefonos.Rebind()
                        'DdlHist_Ge_Accion.ClearSelection()
                        'DdlHist_Ge_Resultado.ClearSelection()
                        ''DdlHist_Ge_NoPago.ClearSelection()
                        RadTabStrip1.SelectedIndex = 1
                        RadMultiPage1.SelectedIndex = 1
                        'BtnGestion.Enabled = True
                    End If
                Else
                    BtnGestion_Disable("true")
                    If tmpCredito("PR_MC_ESTATUS") = "Retirada" Then
                        Limpiar()
                        ValidaConfiguracion()
                        'TxtHist_Pr_Dtepromesa.MinDate = New Date(Now.Year, Now.Month, Now.Day, 0, 0, 0)
                        LblRetirado.Visible = False
                        GvCalTelefonos.Rebind()
                        RadTabStrip1.SelectedIndex = 1
                        RadMultiPage1.SelectedIndex = 1
                    End If
                End If

            Catch ex As Exception
                showModal(Notificacion, "delete", "Error", "Hubo un problema al consultar la informacion")
            End Try
        End If
    End Sub



    Protected Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click
        Try
            Dim TmpAplcc As Aplicacion = CType(Session("Aplicacion"), Aplicacion)
            Dim V_InOut As String = 0
            If DdlHist_Ge_Resultado.SelectedItem.Text.Length = 0 Then
                showModal(Notificacion, "warning", "Aviso", "Seleccione Un Resultado")
            Else 'CUndo resultado es promesa de pago
                If DdlHist_Ge_Resultado.SelectedValue.Split(",")(1) = 1 Then
                    If ValidaMonto(TxtHist_Pr_Montopp.Text) = 1 Then
                        showModal(Notificacion, "warning", "Aviso", "Monto Incorrecto, Valide " & TxtHist_Pr_Montopp.Text)
                    ElseIf Val(TxtHist_Pr_Montopp.Text) <= 0 Then
                        showModal(Notificacion, "warning", "Aviso", "El Monto No Puede Ser Menor O Igual A 0")
                    ElseIf Not TxtHist_Pr_Dtepromesa.SelectedDate.HasValue Then
                        showModal(Notificacion, "warning", "Aviso", "Captura La Fecha De La Promesa")
                    ElseIf DDLTipoPago.SelectedValue = "" Then
                        showModal(Notificacion, "warning", "Aviso", "Capture Un Tipo de Pago Para La Promesa")
                    Else
                        Dim Promesa As String = Class_CapturaVisitas.VariosQ(tmpCredito("PR_MC_CREDITO"), tmpUSUARIO("CAT_LO_USUARIO"), 20, ("CREDIFIEL")).Split("|")(1)

                        If Promesa = "0,0" Then
                            Dim objGestion As Gestion = New Gestion()
                            objGestion.Credito = tmpCredito("PR_MC_CREDITO")
                            objGestion.Producto = tmpCredito("PR_MC_PRODUCTO")

                            objGestion.Usuario = tmpUSUARIO("CAT_LO_USUARIO")
                            objGestion.CodigoAccion = DdlHist_Ge_Accion.SelectedValue
                            objGestion.Resultado = DdlHist_Ge_Resultado.SelectedItem.Text
                            objGestion.CodigoResultado = DdlHist_Ge_Resultado.SelectedValue
                            'objGestion.CodigoNoPago = DdlHist_Ge_NoPago.SelectedValue
                            objGestion.Comentario = TxtHist_Ge_Comentario.Text
                            objGestion.InOutBound = V_InOut
                            objGestion.Telefono = TxtHist_Ge_Telefono.Text
                            objGestion.Agencia = tmpCredito("PR_MC_AGENCIA")
                            objGestion.FechaPromesa = TxtHist_Pr_Dtepromesa.SelectedDate.Value.ToShortDateString
                            objGestion.Montopp = TxtHist_Pr_Montopp.Text
                            objGestion.AplicacionAccion = TmpAplcc.ACCION
                            objGestion.AplicacionNoPago = TmpAplcc.NOPAGO
                            objGestion.Anterior = tmpCredito("PR_MC_CODIGO")

                            objGestion.FilasTrabajo = Session("NombreFila")
                            objGestion.CallID = Session("CallerId")
                            objGestion.CampanaMarcador = Session("CAMPAIGNID")
                            objGestion.instancia = tmpCredito("PR_MC_INSTANCIA")
                            ' objGestion.Participante = DDLParticipante.SelectedValue
                            objGestion.TipoPago = DDLTipoPago.SelectedValue

                            Dim Resultado As Object = objGestion.guardarPromesa

                            If TypeOf Resultado Is DataTable Then
                                Dim Valores As DataTable = Resultado
                                CreditoAjenoGestionado()
                                If Valores.TableName = "Exception" Then
                                    Throw New Exception("0x001 " & Valores(0).Item(0).ToString)
                                ElseIf Valores.Rows.Count = 0 Then
                                    Throw New Exception("0x002 " & "Error al guardar Gestion. Intenta más tarde.")
                                Else
                                    tmpUSUARIO("GESTIONES") = CStr(Val(tmpUSUARIO("GESTIONES")) + 1)
                                    Dim WCANPP As Double = tmpUSUARIO("MONTOPP")
                                    If DdlHist_Ge_Resultado.SelectedValue.Split(",")(1) = "1" Then
                                        tmpUSUARIO("CUANTASPP") = CStr(Val(tmpUSUARIO("CUANTASPP")) + 1)
                                        'TmpUsr.MONTOPP = to_money(Val(WCANPP) + Val(TxtHist_Pr_Montopp.Text))
                                        tmpUSUARIO("MONTOPP") = to_money(Val(WCANPP) + Val(TxtHist_Pr_Montopp.Text))
                                    End If
                                    LblCUANTASPP.Text = tmpUSUARIO("CUANTASPP")
                                    LblGESTIONES.Text = tmpUSUARIO("GESTIONES")
                                    LblMONTOPP.Text = to_money(tmpUSUARIO("MONTOPP"))
                                    tmpCredito = LlenarCredito.Busca(tmpCredito("PR_MC_CREDITO"), tmpCredito("PR_MC_PRODUCTO"))
                                    tmpCredito("PR_MC_CUENTATRABAJADAFILA") = "1"
                                    pnlGestion.Visible = False
                                    ValidaConfiguracion()
                                    'TxtHist_Pr_Dtepromesa.MinDate = New Date(Now.Year, Now.Month, Now.Day, 0, 0, 0)
                                    showModal(Notificacion, "ok", "Correcto", "Operacion completada exitosamente")
                                    Limpiar()
                                End If
                            Else
                                showModal(Notificacion, "warning", "Aviso", Resultado.ToString)
                            End If
                        Else
                            showModal(Notificacion, "warning", "Aviso", "Promesa vigente para " & Promesa.Split(",")(1) & " por " & to_money(Promesa.Split(",")(0)))
                        End If
                    End If
                Else
                    If Not TxtHist_Pr_Dtepromesa.SelectedDate.HasValue Then
                        showModal(Notificacion, "warning", "Aviso", "Selecciona Una Fecha De Proximo Contacto")
                    ElseIf DDLHist_Parentesco.SelectedText = "" Then
                        showModal(Notificacion, "warning", "Aviso", "Selecciona Un Parentesco")
                    Else
                        Dim objGestion As Gestion = New Gestion()
                        objGestion.Credito = tmpCredito("PR_MC_CREDITO")
                        objGestion.Producto = tmpCredito("PR_MC_PRODUCTO")
                        objGestion.Usuario = tmpUSUARIO("CAT_LO_USUARIO")
                        objGestion.CodigoAccion = DdlHist_Ge_Accion.SelectedValue
                        objGestion.Resultado = DdlHist_Ge_Resultado.SelectedItem.Text
                        objGestion.CodigoResultado = DdlHist_Ge_Resultado.SelectedValue
                        'objGestion.CodigoNoPago = DdlHist_Ge_NoPago.SelectedValue
                        objGestion.Comentario = TxtHist_Ge_Comentario.Text
                        objGestion.InOutBound = V_InOut
                        objGestion.Telefono = TxtHist_Ge_Telefono.Text
                        objGestion.Agencia = tmpCredito("PR_MC_AGENCIA")
                        objGestion.FechaPromesa = CType(TxtHist_Pr_Dtepromesa.SelectedDate, DateTime).ToString("yyyy-MM-dd hh:mm:ss")
                        objGestion.Anterior = tmpCredito("PR_MC_CODIGO")
                        objGestion.AplicacionAccion = TmpAplcc.ACCION
                        objGestion.AplicacionNoPago = TmpAplcc.NOPAGO
                        objGestion.FilasTrabajo = Session("NombreFila")
                        objGestion.CallID = Session("CallerId")
                        objGestion.CampanaMarcador = Session("CAMPAIGNID")
                        objGestion.instancia = tmpCredito("PR_MC_INSTANCIA")
                        'objGestion.Participante = DDLParticipante.SelectedValue
                        objGestion.TipoPago = ""
                        objGestion.parentesco = DDLHist_Parentesco.SelectedText
                        Dim Resultado As Object = objGestion.guardarGestion
                        If TypeOf Resultado Is DataTable Then
                            Dim Valores As DataTable = Resultado
                            CreditoAjenoGestionado()
                            If Valores.TableName = "Exception" Then
                                Throw New Exception("0x003 " & Valores(0)(0).ToString)
                            ElseIf Valores.Rows.Count = 0 Then
                                Throw New Exception("0x004 " & "Error al guardar Gestion. Intenta más tarde.")
                            Else
                                tmpUSUARIO("GESTIONES") = CStr(Val(tmpUSUARIO("GESTIONES")) + 1)
                                LblCUANTASPP.Text = tmpUSUARIO("CUANTASPP")
                                LblGESTIONES.Text = tmpUSUARIO("GESTIONES")
                                LblMONTOPP.Text = to_money(tmpUSUARIO("MONTOPP"))
                                tmpCredito = LlenarCredito.Busca(tmpCredito("PR_MC_CREDITO"), tmpCredito("PR_MC_PRODUCTO"))
                                tmpCredito("PR_MC_CUENTATRABAJADAFILA") = "1"
                                pnlGestion.Visible = False
                                Limpiar()
                                ValidaConfiguracion()
                                showModal(Notificacion, "ok", "Correcto", "Operacion completada exitosamente")
                                'TxtHist_Pr_Dtepromesa.MinDate = New Date(Now.Year, Now.Month, Now.Day, 0, 0, 0)
                            End If
                        Else
                            showModal(Notificacion, "warning", "Aviso", Resultado.ToString)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            showModal(Notificacion, "deny", "Error", ex.Message)
        End Try
    End Sub
    Public Sub cerrar_gestion()
        Dim script As String = "$('#divGestion').removeClass('w3-show');"
        ScriptManager.RegisterStartupScript(Me, GetType(Page), "cerrar_gestion", script, True)
    End Sub
    Sub Limpiar()
        TxtHist_Pr_Montopp.Text = ""
        PnlMontoPP.Visible = False
        TxtHist_Pr_Dtepromesa.Clear()
        CbxHist_Ge_Inoutbound.Checked = False
        TxtHist_Ge_Telefono.Text = ""
        TxtHist_Ge_Comentario.Text = ""
        'LblMONTOPP.Text = ""
        'LblCUANTASPP.Text = ""
        'LblGESTIONES.Text = ""
        'Session("CallerId") = ""
        'Session("mTelefono") = ""
        'ValidaConfiguracion()
        'GvHistAct.DataSource = Class_Hist_Act.LlenarElementosHistAct(tmpCredito("PR_MC_CREDITO"), "Hist_Ge_Dteactividad", "DESC", 0)
        'GvHistAct.DataBind()

        'GVHist_Atencion_C.DataSource = Class_Hist_Act.LlenarElementosHistAct(TmpCredito.PR_MC_CREDITO, "Hist_At_Dteactividad", "DESC", 3)
        'GVHist_Atencion_C.DataBind()

    End Sub

    Sub ValidaConfiguracion()
        'PnlNoPago.Visible = False
        Try
            Dim Aplicacion As Aplicacion = CType(Session("Aplicacion"), Aplicacion)
            If Aplicacion.ACCION = 1 Then
                'PnlAccion.Visible = True
                Class_MasterPage.LlenarElementosMaster(DdlHist_Ge_Accion, "", tmpCredito("PR_MC_PRODUCTO"), tmpUSUARIO("CAT_LO_PERFIL"), "1,3", 1)
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
        Limpiar()
        rGvReferencias.DataSource = Nothing
        rGvReferencias.DataBind()
        DDLTipoPago.ClearSelection()
        'DdlHist_Ge_NoPago.Visible = False
        BtnGuardar.Visible = False
        DdlHist_Ge_Resultado.ClearSelection()
        DdlHist_Ge_Resultado.Items.Clear()
        'DdlHist_Ge_NoPago.ClearSelection()
        'DdlHist_Ge_NoPago.Items.Clear()
        rGvReferencias.DataSource = Nothing
        rGvReferencias.DataBind()
        DDLTipoPago.ClearSelection()
        If DdlHist_Ge_Accion.SelectedItem.Text <> "CLASICO PAGO FIJO" Then
            Class_MasterPage.LlenarElementosMaster(DdlHist_Ge_Resultado, DdlHist_Ge_Accion.SelectedValue, tmpCredito("PR_MC_PRODUCTO"), tmpUSUARIO("CAT_LO_PERFIL"), "1,3", 3, tmpCredito("PR_MC_INSTANCIA"))
            DdlHist_Ge_Resultado.Enabled = True
        End If

        TxtHist_Ge_Telefono.Text = Session("mTelefono")

    End Sub
    Sub llenardrop()
        Dim comand As New SqlCommand("select * from cat_parentesco")

        DDLHist_Parentesco.Items.Clear()
        Dim tabla As DataTable = Consulta_Procedure(comand, "parentesco")
        For i = 0 To tabla.Rows.Count - 1
            DDLHist_Parentesco.Items.Add(CType(tabla.Rows(i).Item(1), String))
        Next
        DDLHist_Parentesco.DataBind()

    End Sub
    Protected Sub DdlResultado_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlHist_Ge_Resultado.SelectedIndexChanged
        'Limpiar()
        PnlMontoPP.Visible = False
        Dim Aplicacion As Aplicacion = CType(Session("Aplicacion"), Aplicacion)

        BtnGuardar.Visible = True
        'PnlResultado.Visible = True
        If DdlHist_Ge_Resultado.SelectedValue.Split(",")(1) = 1 Then
            PnlMontoPP.Visible = True
            LblHist_Pr_Dtepromesa.Text = "Fecha promesa"
            PnlSigContacto.Visible = False
            TxtHist_Pr_Dtepromesa.TimePopupButton.Visible = False
            DDLHist_Parentesco.Items.Clear()
        Else
            llenardrop()
            PnlMontoPP.Visible = False
            LblHist_Pr_Dtepromesa.Text = "Siguiente contacto"
            PnlSigContacto.Visible = True
            TxtHist_Pr_Dtepromesa.TimePopupButton.Visible = True
            'TxtHist_Pr_Dtepromesa.MinDate = New Date(Now.Year, Now.Month, Now.Day, 0, 0, 0)
            Try
                If Now.Month = 12 Then
                    TxtHist_Pr_Dtepromesa.MaxDate = New Date((Now.Year + 1), (Now.Month - 11), Now.Day, 23, 59, 59)
                Else
                    TxtHist_Pr_Dtepromesa.MaxDate = New Date(Now.Year, (Now.Month + 1), Now.Day, 23, 59, 59)
                End If
            Catch EX As Exception
                TxtHist_Pr_Dtepromesa.MaxDate = New Date(Now.Year, (Now.Month + 2), 1, 23, 59, 59)
            End Try
        End If

        TxtHist_Ge_Telefono.Text = Session("mTelefono")

    End Sub

    Protected Sub GvCalTelefonos_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs)
        Try
            GvCalTelefonos.DataSource = Class_MasterPage.Telefonos(tmpCredito("PR_MC_CREDITO"), "", "", "", "", "", "", 0)
        Catch ex As Exception
            GvCalTelefonos.DataSource = Nothing
        End Try
    End Sub

    Private Sub GvCalTelefonos_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles GvCalTelefonos.ItemCommand
        Dim comando As String
        Dim fila As GridDataItem
        Try
            comando = e.CommandName
            If comando = "SMS" Then
                fila = TryCast(e.Item, GridDataItem)
                Sms_Click(GridDataItemToDictionary(fila))
            End If
        Catch ex As Exception
            showModal(Notificacion, "deny", "Error", ex.Message)
        End Try
    End Sub

    Private Sub Sms_Click(fila As IDictionary)

    End Sub

    Private Sub RCBPlantillaSMS_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles RCBPlantillaSMS.SelectedIndexChanged
        Dim plantilla As String = e.Value.ToString
        Dim oraCommanAgencias As New SqlCommand
        oraCommanAgencias.CommandText = "SP_SMS"
        oraCommanAgencias.CommandType = CommandType.StoredProcedure
        oraCommanAgencias.Parameters.Add("@v_credito", SqlDbType.VarChar).Value = tmpCredito("PR_MC_CREDITO")
        oraCommanAgencias.Parameters.Add("@v_plantilla", SqlDbType.VarChar).Value = plantilla
        oraCommanAgencias.Parameters.Add("@V_Bandera", SqlDbType.VarChar).Value = 9

        Dim DtsVarios As DataTable = Consulta_Procedure(oraCommanAgencias, "Etiqueta")
        RTBMessageSMS.Text = DtsVarios.Rows(0).Item(0)
        RBTEnviarSMS.Enabled = True
        MostrarOcultarVentana(WindowAcciones, 1, 0)
    End Sub

    Sub MostrarOcultarVentana(ByVal V_Ventana As Object, ByVal V_Bandera As String, ByVal V_Maximizada As String)

        If V_Bandera = 1 Then
            Dim script As String
            If V_Maximizada = 1 Then
                script = "function f(){$find(""" + V_Ventana.ClientID + """).show();$find(""" + V_Ventana.ClientID + """).maximize(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
            Else
                script = "function f(){$find(""" + V_Ventana.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
            End If
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
        Else
            Dim script As String = "function f(){$find(""" + V_Ventana.ClientID + """).hide(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
        End If
    End Sub
    Protected Sub validaSMS_Click(sender As Object, e As ButtonClickEventArgs)
        Dim btn As RadButton = TryCast(sender, Telerik.Web.UI.RadButton)
        Dim fila As GridDataItem = TryCast(btn.NamingContainer, GridDataItem)
        Dim dict As IDictionary = GridDataItemToDictionary(fila)
        telefonoselect.Text = "52" + dict("TELEFONO")
        'Dim telefono As String =
        Dim oraCommanEtiquetas As New SqlCommand
        oraCommanEtiquetas.CommandText = "Sp_Add_Cat_Etiquetas"
        oraCommanEtiquetas.CommandType = CommandType.StoredProcedure

        oraCommanEtiquetas.Parameters.Add("@V_Bandera", SqlDbType.VarChar).Value = 9

        Dim DtsVarios As DataTable = Consulta_Procedure(oraCommanEtiquetas, "Etiqueta")
        Dim DtvVarios As DataView = DtsVarios.DefaultView
        RCBPlantillaSMS.DataTextField = "Nombre"
        RCBPlantillaSMS.DataValueField = "Nombre"
        RCBPlantillaSMS.DataSource = DtsVarios
        RCBPlantillaSMS.DataBind()
        MostrarOcultarVentana(WindowAcciones, 1, 0)
    End Sub
    Private Sub RBTEnviarSMS_Click(sender As Object, e As EventArgs) Handles RBTEnviarSMS.Click

        'Dim btn As RadButton = TryCast(sender, Telerik.Web.UI.RadButton)
        'Dim fila As GridDataItem = TryCast(btn.NamingContainer, GridDataItem)
        'Dim dict As IDictionary = GridDataItemToDictionary(fila)
        'Dim telefono As String = "52" + dict("TELEFONO")

        Dim oracmjsonsms As New SqlCommand
        oracmjsonsms.CommandText = "Sp_getdatos"
        oracmjsonsms.CommandType = CommandType.StoredProcedure
        oracmjsonsms.Parameters.Add("@plantilla", SqlDbType.VarChar).Value = RCBPlantillaSMS.SelectedValue
        oracmjsonsms.Parameters.Add("@mensaje", SqlDbType.VarChar).Value = RTBMessageSMS.Text
        oracmjsonsms.Parameters.Add("@telefono", SqlDbType.VarChar).Value = telefonoselect.Text
        oracmjsonsms.Parameters.Add("@Origen", SqlDbType.VarChar).Value = "Gestion"
        oracmjsonsms.Parameters.Add("@Credito", SqlDbType.VarChar).Value = tmpCredito("PR_MC_CREDITO")
        oracmjsonsms.Parameters.Add("@usuario", SqlDbType.VarChar).Value = tmpUSUARIO("CAT_LO_USUARIO")
        oracmjsonsms.Parameters.Add("@Agencia", SqlDbType.VarChar).Value = tmpUSUARIO("CAT_LO_NUM_AGENCIA")
        oracmjsonsms.Parameters.Add("@producto", SqlDbType.VarChar).Value = "Credifiel"
        Dim DtsVarios As DataTable = Consulta_Procedure(oracmjsonsms, "Envio")
        If DtsVarios.TableName = "Exception" Then
            showModal(Notificacion, "warning", "Aviso", DtsVarios.Rows(0).Item("Mensaje"))

        Else
            showModal(Notificacion, "ok", "Aviso", "Mensaje enviado")
        End If


        'Dim SSCommand As New SqlCommand
        'SSCommand.CommandText = "SP_VARIOS_QUERIES"
        'SSCommand.CommandType = CommandType.StoredProcedure
        'SSCommand.Parameters.Add("@V_Usuario", SqlDbType.NVarChar).Value = dato
        'SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 6
        'Consulta_Procedure(SSCommand, SSCommand.CommandText)
    End Sub
    Private Function GridDataItemToDictionary(grid As GridDataItem) As IDictionary
        Dim dictionary As New Dictionary(Of String, String)
        Try
            dictionary.Add("TIPO", TryCast(grid.FindControl("LblTIPO"), RadLabel).Text)
            dictionary.Add("FUENTE", TryCast(grid.FindControl("LblFUENTE"), RadLabel).Text)
            dictionary.Add("VALIDO", TryCast(grid.FindControl("LblVALIDO"), RadLabel).Text)
            dictionary.Add("CAMPOPOS", TryCast(grid.FindControl("LblCAMPOPOS"), RadLabel).Text)
            dictionary.Add("CAMPONES", TryCast(grid.FindControl("LblCAMPONES"), RadLabel).Text)
            dictionary.Add("CAMPODTEPOS", TryCast(grid.FindControl("LblCAMPODTEPOS"), RadLabel).Text)
            dictionary.Add("CAMPODTENES", TryCast(grid.FindControl("LblCAMPODTENES"), RadLabel).Text)
            dictionary.Add("TABLA", TryCast(grid.FindControl("LblTABLA"), RadLabel).Text)
            dictionary.Add("TELEFONO", TryCast(grid.FindControl("LblTELEFONO"), RadLabel).Text)
            dictionary.Add("NOMBRE", TryCast(grid.FindControl("LblNOMBRE"), RadLabel).Text)
        Catch ex As Exception
        End Try
        Return dictionary
    End Function
    Protected Sub Valido_Click(sender As Object, e As ButtonClickEventArgs)
        Dim btn As RadButton = TryCast(sender, Telerik.Web.UI.RadButton)
        Dim fila As GridDataItem = TryCast(btn.NamingContainer, GridDataItem)
        Dim dict As IDictionary = GridDataItemToDictionary(fila)
        Class_MasterPage.Telefonos(tmpCredito("PR_MC_CREDITO"), dict("CAMPOPOS"), dict("CAMPODTEPOS"), dict("TABLA"), dict("TIPO"), "Valido", dict("TELEFONO"), 1)
        TxtHist_Ge_Telefono.Text = dict("TELEFONO")
        GvCalTelefonos.Rebind()
    End Sub
    Protected Sub NoValido_Click(sender As Object, e As ButtonClickEventArgs)
        Dim fila As GridDataItem = TryCast(TryCast(sender, Telerik.Web.UI.RadButton).NamingContainer, GridDataItem)
        Dim dict As IDictionary = GridDataItemToDictionary(fila)
        Class_MasterPage.Telefonos(tmpCredito("PR_MC_CREDITO"), dict("CAMPONES"), dict("CAMPODTENES"), dict("TABLA"), dict("TIPO"), "NoValido", dict("TELEFONO"), 1)
        TxtHist_Ge_Telefono.Text = dict("TELEFONO")
        GvCalTelefonos.Rebind()
    End Sub
    ''' <summary>
    ''' Verifica si el usuario tiene permisos y el telefono el valido para los mensajes
    ''' </summary>
    ''' <returns></returns>
    Public Function validarSMS(ByVal flag As Integer, ByVal telefono As String) As Object
        Select Case flag
            Case 0
                Try
                    If tmpUSUARIO("CAT_LO_PGESTION").ToString.Substring(5, 1) = "1" And telefono.Substring(0, 2) <> "00" Then
                        Return "Imagenes/icons8-enviar-48.png"
                    Else
                        Return "Imagenes/icons8-mensaje-borrado-48.png"
                    End If
                Catch ex As Exception
                    Return "Imagenes/icons8-mensaje-borrado-48.png"
                End Try
            Case 1
                Try
                    If tmpUSUARIO("CAT_LO_PGESTION").ToString.Substring(5, 1) = "1" And telefono.Substring(0, 2) <> "00" Then
                        Return True
                    Else
                        Return False
                    End If
                Catch ex As Exception
                    Return False
                End Try
            Case 2
                Try
                    If tmpUSUARIO("CAT_LO_PGESTION").ToString.Substring(5, 1) = "1" And telefono.Substring(0, 2) <> "00" Then
                        Return "Enviar mensaje"
                    Else
                        Return "No disponible"
                    End If
                Catch ex As Exception
                    Return "No disponible"
                End Try
        End Select
        Return Nothing
    End Function

    Private Sub MGestion_MasterPage_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        If Not IsPostBack Then
            '--Se aplican los permisos--
            RadTabStrip1.DataBind()
            PnlAccesorios.DataBind()
            'BtnGestion_Disable(IIf(tmpPermisos("GESTION_CALIFICACION_TELEFONICA") = "1", "false", "true"))
            '-- --

            'Evita que salga una pï¿½gina sin datos
            RadTabStrip1.SelectedIndex = 0
            RadMultiPage1.SelectedIndex = 0

        End If
    End Sub

    Private Sub BtnCerrar_Click(sender As Object, e As EventArgs) Handles BtnCerrar.Click
        If Session("Usuario") IsNot Nothing Then
            CerrarSesion.cerrar(tmpUSUARIO("CAT_LO_USUARIO"))
        End If
        Session.Abandon()
        Response.Redirect("~/Login.aspx")
    End Sub

    Private Sub BtnModulo_Click(sender As Object, e As EventArgs) Handles BtnModulo.Click
        Response.Redirect("~/Modulos.aspx")
    End Sub

    Private Sub BtnDescargar_Click(sender As Object, e As EventArgs) Handles BtnDescargar.Click
        Dim tmp_archivo As String

        If DesEncriptarCadena(StrConexion(3)) = "MCCOLLECT" Then
            tmp_archivo = "https://pruebasmc.com.mx/GestionV4Imagenes/plantilla.pdf"
        Else
            tmp_archivo = "https://mcnoc.com.mx/GESTIONV4/plantilla.pdf"
        End If

        Dim vtn As String = "window.open('" & tmp_archivo & "','M2','scrollbars=yes,resizable=yes','height=300', 'width=300')"
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup2", vtn, True)
    End Sub

    Private Sub BtnRecargar_Click(sender As Object, e As EventArgs) Handles BtnRecargar.Click
        GvCalTelefonos.Rebind()
    End Sub

    Private Sub btnGestion_Click(sender As Object, e As EventArgs) Handles btnGestion.Click
        pnlGestion.Visible = Not pnlGestion.Visible
    End Sub

    Private Function CreditoAjenoGestionado() As Boolean
        Dim resultado As Boolean = False
        If String.IsNullOrWhiteSpace(tmpCredito("PR_MC_UASIGNADO").ToString) Then
            resultado = False
            'ElseIf tmpUSUARIO("CAT_LO_INTERNOEXTERNO").ToString = "1" Then
            '    resultado = False
        ElseIf tmpCredito("PR_MC_UASIGNADO").ToString <> tmpUSUARIO("CAT_LO_USUARIO").ToString Then
            resultado = True
            Dim mensajePlantilla As String = "El usuario '" & tmpUSUARIO("CAT_LO_USUARIO") & "' ha trabajado el credito '" & tmpCredito("PR_MC_CREDITO").ToString & "' que tenias asignado"
            Dim aviso As New AvisoGestion(mensajePlantilla, tmpCredito("PR_MC_UASIGNADO"), "", "Trabajo Usuario No Asignado")
            aviso.Insertar()
        End If
        Return resultado
    End Function

    Private Sub listViewAvisos_NeedDataSource(sender As Object, e As RadListViewNeedDataSourceEventArgs) Handles listViewAvisos.NeedDataSource
        If Session("USUARIO") IsNot Nothing Then
            listViewAvisos.DataSource = AvisoGestion.getAllAvisosDataTable(tmpUSUARIO("CAT_LO_USUARIO"))
        Else
            Response.Redirect("~/SesionExpirada.aspx")
        End If
    End Sub

    Private Sub imgAvisos_Load(sender As Object, e As EventArgs) Handles imgAvisos.Load
        If Session("USUARIO") IsNot Nothing Then
            Dim vistos As Integer = AvisoGestion.getAllUnseen(tmpUSUARIO("CAT_LO_USUARIO"))
            imgAvisos.Visible = IIf(vistos = 0, False, True)
        Else
            Response.Redirect("~/SesionExpirada.aspx")
        End If
    End Sub

    <WebMethod>
    Public Shared Function setSeen(v_usuario As String) As String
        AvisoGestion.setAllSeen(v_usuario)
        Return "Ok"
    End Function

    Private Sub pnlGestion_Load(sender As Object, e As EventArgs) Handles pnlGestion.Load
        If Not IsPostBack Then
            DdlHist_Ge_Accion.ClearSelection()
            DdlHist_Ge_Accion.Items.Clear()

            DdlHist_Ge_Resultado.ClearSelection()
            DdlHist_Ge_Resultado.Items.Clear()
            DdlHist_Ge_Resultado.Enabled = False

            DDLTipoPago.ClearSelection()

            'DDLParticipante.ClearSelection()
            If tmpCredito IsNot Nothing Then
                ValidaConfiguracion()
            End If
            'Try
            '    LLENAR_DROP2(37, tmpCredito("PR_MC_CREDITO"), DDLParticipante, "ValPar", "TexPar")
            'Catch ex As Exception
            '    Dim s As String = ex.Message
            'End Try
        End If
    End Sub

    Private Sub RBTraeAlertas_Click(sender As Object, e As EventArgs) Handles RBTraeAlertas.Click
        RGAlertasGral.DataSource = AvisoGestion.traealertasgral(tmpUSUARIO("CAT_LO_USUARIO"), RCBTipoFechaAvisos.SelectedValue, RDPFechaInicioAvisos.DateInput.Text.Substring(0, 10), RDPFechaFinAvisos.DateInput.Text.Substring(0, 10))
        RGAlertasGral.DataBind()
        PnlAvisosGral.Style.Add("display", "block")
    End Sub
    Private Sub RBCerrarAlertas_Click(sender As Object, e As EventArgs) Handles RBCerrarAlertas.Click
        PnlAvisosGral.Style.Add("display", "none")
    End Sub
    'Private Sub RBCerrarW_Click(sender As Object, e As EventArgs) Handles RBCerrarW.Click
    '    PnlChatWhat.Style.Add("display", "none")
    'End Sub

    Protected Sub BtnIr_Click(sender As Object, e As EventArgs)
        Dim BtnToltip As RadButton = CType(sender, RadButton)

        Dim tooltip As String = BtnToltip.ToolTip

        Dim CREDIT As String = tooltip.Split(":")(1)
        CREDIT = CREDIT.Replace(" ", "").Replace("hoy", "").Replace("vencida el día de ayer", "").Replace("para el día de hoy", "").Replace("para el día de mañana", "")
        If CREDIT = "" Then
            showModal(Notificacion, "warning", "Aviso", "Problemas al abrir el credito: " + CREDIT)
        Else
            Class_MasterPage.Tocar(CREDIT, tmpUSUARIO("CAT_LO_USUARIO"), "CREDIFIEL")
            tmpCredito = LlenarCredito.Busca(CREDIT, "CREDIFIEL")
            Session("Buscar") = 1S
            tmpCredito("PR_MC_CUENTATRABAJADAFILA") = "1"
            PnlInfoRapida.Visible = True
            Response.Redirect("MasterPage.aspx", True)

        End If
    End Sub

    Private Sub DdlHist_Ge_Accion_ItemCreated(sender As Object, e As RadComboBoxItemEventArgs) Handles DdlHist_Ge_Accion.ItemCreated

    End Sub

    Private Sub DDLTipoPago_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles DDLTipoPago.SelectedIndexChanged
        rGvReferencias.Visible = False
        Dim miDataTable As New DataTable
        miDataTable.Columns.Add("Tipo Pago")
        miDataTable.Columns.Add("Referencia")
        DDLTipoPago.Text = ""


        Dim Renglon As DataRow = miDataTable.NewRow()
        Dim Renglon2 As DataRow = miDataTable.NewRow()
        Dim Renglon3 As DataRow = miDataTable.NewRow()
        Dim Renglon4 As DataRow = miDataTable.NewRow()
        Dim Renglon5 As DataRow = miDataTable.NewRow()

        If sender.SelectedValue = "Parcial" Then


            Renglon("Tipo Pago") = "CLABE STP"
            Renglon("Referencia") = tmpCredito("PR_referencia646")
            miDataTable.Rows.Add(Renglon)

            Renglon2("Tipo Pago") = "REFERENCIA PARCIAL BBVA"
            Renglon2("Referencia") = tmpCredito("PR_referencia012")
            miDataTable.Rows.Add(Renglon2)

            Renglon3("Tipo Pago") = "REFERENCIA PARCIAL BANORTE"
            Renglon3("Referencia") = tmpCredito("PR_referencia072")
            miDataTable.Rows.Add(Renglon3)

            Renglon4("Tipo Pago") = "REFERENCIA PARCIAL BANCO AZTECA"
            Renglon4("Referencia") = tmpCredito("PR_referencia127p")
            miDataTable.Rows.Add(Renglon4)

            Renglon5("Tipo Pago") = "PAYNET"
            Renglon5("Referencia") = tmpCredito("PR_referenciapaynet")
            miDataTable.Rows.Add(Renglon5)

            rGvReferencias.Visible = True
            rGvReferencias.DataSource = miDataTable
            rGvReferencias.DataBind()
        ElseIf sender.SelectedValue = "Liquidacion Pago Unico" Or sender.SelectedValue = "Liquidacion En Exhibiciones" Then

            Renglon("Tipo Pago") = "CLABE STP"
            Renglon("Referencia") = tmpCredito("PR_referencia646")
            miDataTable.Rows.Add(Renglon)

            Renglon2("Tipo Pago") = "REF. LIQUIDACION BBVA"
            Renglon2("Referencia") = tmpCredito("PR_referencia012l")
            miDataTable.Rows.Add(Renglon2)

            Renglon3("Tipo Pago") = "REF. LIQUIDACION BANORTE"
            Renglon3("Referencia") = tmpCredito("PR_referencia072l")
            miDataTable.Rows.Add(Renglon3)

            Renglon4("Tipo Pago") = "REF. LIQUIDACION BANCO AZTECA"
            Renglon4("Referencia") = tmpCredito("PR_referencia127l")
            miDataTable.Rows.Add(Renglon4)

            Renglon5("Tipo Pago") = "PAYNET"
            Renglon5("Referencia") = tmpCredito("PR_referenciapaynet")
            miDataTable.Rows.Add(Renglon5)
            rGvReferencias.Visible = True
            rGvReferencias.DataSource = miDataTable
            rGvReferencias.DataBind()
            If sender.SelectedValue = "Liquidacion En Exhibiciones" Then
                pnlGestion.Visible = False
                For Each tab As RadTab In RadTabStrip1.GetAllTabs()
                    If tab.Index = 6 Then '6 para Negociaciones
                        'tab.Selected = True
                        tab.PageView.Selected = True
                        'RadTabStrip1.DataBind()
                    End If
                Next
            End If
        Else

        End If
    End Sub
End Class
