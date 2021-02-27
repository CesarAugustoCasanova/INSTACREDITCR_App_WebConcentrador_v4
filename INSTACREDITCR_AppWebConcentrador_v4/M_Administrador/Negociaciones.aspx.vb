Imports System.Data.SqlClient
Imports System.Data
Imports System.Web.UI.WebControls.Label
Imports Db
Imports Funciones
Imports Telerik.Web.UI
Imports ClosedXML.Excel
Imports System.IO

Partial Class MAdministrador_Negociaciones
    Inherits System.Web.UI.Page


    Public Property tmpUSUARIO As IDictionary
        Get
            Return CType(Session("USUARIO"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("USUARIO") = value
        End Set
    End Property

    Public DdlTabla As DropDownList = New DropDownList()
    Public DdlCampo As DropDownList = New DropDownList()
    Public DdlOperador As DropDownList = New DropDownList()
    Public TxtValores As TextBox = New TextBox()
    Public DdlConector As DropDownList = New DropDownList()
    Public GvCatalogos As GridView
    Public Panel As Panel
    ' Public'Calendario As AjaxControlToolkit.CalendarExtender
    Public DdlTipo As DropDownList = New DropDownList()
    Dim class_neg As Class_Negociaciones

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        ScriptManager.GetCurrent(Page).RegisterPostBackControl(RGVNegociaciones)
        ScriptManager.GetCurrent(Page).RegisterPostBackControl(BtnDescargar)
        ScriptManager.GetCurrent(Page).RegisterPostBackControl(UpnGral)


        If Session("USUARIO") Is Nothing Then
            OffLine(HidenUrs.Value)
            AUDITORIA(HidenUrs.Value, "Administrador", "Negociaciones", " ", "Page_Load", "", Request.ServerVariables("REMOTE_ADDR"), Request.ServerVariables("REMOTE_HOST"))
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/SesionExpirada.aspx")
        Else
            HidenUrs.Value = tmpUSUARIO("CAT_LO_USUARIO")
            Try
                If Not IsPostBack Then
                    'LLenarGv(0)
                End If
            Catch ex As Exception
                SendMail("Page_Load", ex, "", "", HidenUrs.Value)
            End Try
        End If
    End Sub

    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Administrador", "Negociaciones.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub

    Function LLenarGv(BANDERA As Integer) As DataTable
        Try
            Dim SSCommandF As New SqlCommand
            SSCommandF.CommandText = "SP_CATALOGO_NEGO"
            SSCommandF.CommandType = CommandType.StoredProcedure
            SSCommandF.Parameters.Add("@V_BANDERA", SqlDbType.NVarChar).Value = BANDERA
            SSCommandF.Parameters.Add("@v_nivel", SqlDbType.NVarChar).Value = DdlNivel.SelectedValue
            Dim DtsFilas As DataTable = Consulta_Procedure(SSCommandF, SSCommandF.CommandText)
            'Dim SSCommandF As New SqlCommand
            'SSCommandF.CommandText = "SP_CATALOGOS"
            'SSCommandF.CommandType = CommandType.StoredProcedure
            'SSCommandF.Parameters.Add("@V_BANDERA", SqlDbType.NVarChar).Value = 18
            'Dim DtsFilas As DataTable = Consulta_Procedure(SSCommandF, "CATALOGO")
            If DtsFilas.Rows.Count > 0 Then
                rBtnEjecutarAsig.Enabled = True
                BtnDescargar.Enabled = True
                'Session("ID_NEGO") = DtsFilas.Rows.Count + 1
                'GvNEGOCIACIONES.Visible = True
                'GvNEGOCIACIONES.DataSource = DtsFilas.DefaultView
                'GvNEGOCIACIONES.DataBind()
                'GvNEGOCIACIONES.Enabled = True
                'RblOpcion.Visible = True
                'RblOpcion.SelectedValue = "Seleccione"
            Else
                rBtnEjecutarAsig.Enabled = False
                BtnDescargar.Enabled = False
                'Session("ID_NEGO") = 1
                'GvNEGOCIACIONES.DataSource = Nothing
                'GvNEGOCIACIONES.DataBind()
                'GvNEGOCIACIONES.Visible = False
            End If
            Return DtsFilas
        Catch ex As Exception
            Aviso(ex.Message)
        End Try

    End Function

    Sub ValidaCatalogo(ByVal V_Campo As String, ByVal GvCatalogo As GridView, ByVal DdlOperador As DropDownList, ByVal TxtValores As TextBox)
        Dim SSCommandCat As New SqlCommand
        SSCommandCat.CommandText = "SP_VALIDA_CATALOGO"
        SSCommandCat.CommandType = CommandType.StoredProcedure
        SSCommandCat.Parameters.Add("@V_Campo", SqlDbType.NVarChar).Value = V_Campo

        Dim DtsObjetos As DataTable = Consulta_Procedure(SSCommandCat, "Catalogo")
        Dim DtvObjetos As DataView = DtsObjetos.DefaultView
        If DtsObjetos.Rows(0).Item("CAMPO") <> "1" Then
            GvCatalogo.DataSource = DtsObjetos
            GvCatalogo.DataBind()
            DdlOperador.Items.Add(New ListItem("Que Contenga", "In", True))
            DdlOperador.Items.Add(New ListItem("Que No Contenga", "Not In", True))
            'TxtValores.ReadOnly = True
        Else
            DdlOperador.Items.Add(New ListItem("Seleccione", "Seleccione", True))
            DdlOperador.Items.Add(New ListItem("Mayor Que", ">", True))
            DdlOperador.Items.Add(New ListItem("Menor Que", "<", True))
            DdlOperador.Items.Add(New ListItem("Igual", "=", True))
            DdlOperador.Items.Add(New ListItem("Mayor O Igual", ">=", True))
            DdlOperador.Items.Add(New ListItem("Menor O Igual", "<=", True))
            DdlOperador.Items.Add(New ListItem("Distinto", "!=", True))
            DdlOperador.Items.Add(New ListItem("Que Contenga", "In", True))
            DdlOperador.Items.Add(New ListItem("Que No Contenga", "Not In", True))
            DdlOperador.SelectedValue = "Seleccione"
            'TxtValores.ReadOnly = False
        End If
    End Sub

    Protected Sub LLENAR_DROP(ByVal Item As DropDownList, Tabla As String)
        Try
            'Item.Items.Clear()

            Dim SSCommand As New SqlCommand
            SSCommand.CommandText = "Sp_Varios_Negociaciones"
            SSCommand.CommandType = CommandType.StoredProcedure
            'SSCommand.Parameters.Add("@V_USUARIO", SqlDbType.NVarChar).Value = DesEncriptarCadena(Conexiones.StrConexion(1))
            SSCommand.Parameters.Add("@V_USUARIO", SqlDbType.NVarChar).Value = (StrConexion(1))
            SSCommand.Parameters.Add("@V_VALOR", SqlDbType.NVarChar).Value = Tabla
            SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 1

            Dim DtsVariable As DataTable = Consulta_Procedure(SSCommand, "Campos")
            Item.DataTextField = "COMMENTS"
            Item.DataValueField = "COLUMN_NAME"
            Item.DataSource = DtsVariable
            Item.DataBind()
            Item.Items.Add("Seleccione")
            Item.SelectedValue = "Seleccione"
        Catch ex As Exception
            '    'MENSJAE.Text = ex.ToString
            '    'SendMail("LLENAR_DROP", ex, "", "", HidenUrs.Value)
        End Try
    End Sub



    Sub HabilitarVisualizar(Quien As Integer)
        Try
            DdlTabla = TryCast(Me.FindControl("ctl00$CPHMaster$DdlTabla" & Quien), DropDownList)
            DdlCampo = TryCast(Me.FindControl("ctl00$CPHMaster$DdlCampo" & Quien), DropDownList)
            DdlOperador = TryCast(Me.FindControl("ctl00$CPHMaster$DdlOperador" & Quien), DropDownList)
            TxtValores = TryCast(Me.FindControl("ctl00$CPHMaster$TxtValores" & Quien), TextBox)
            DdlConector = TryCast(Me.FindControl("ctl00$CPHMaster$DdlConector" & Quien), DropDownList)
            DdlTipo = TryCast(FindControl("ctl00$CPHMaster$DdlTipo" & Quien), DropDownList)
            DdlTipo.Enabled = False
            If DdlCampo.Visible = False Then
                DdlTabla.Items.Clear()
                DdlTabla.Visible = True
                DdlTabla.Items.Add(New ListItem("Datos Demograficos", "PR_CLIE_DEMO", True))
                DdlTabla.Items.Add(New ListItem("Datos Financieros", "PR_CLIE_FINAN", True))
                DdlTabla.Items.Add(New ListItem("Datos Agencia", "PR_CLIE_AGENCY", True))
                DdlTabla.Items.Add(New ListItem("Datos Sistema", "PR_MC_GRAL", True))
                DdlTabla.Items.Add(New ListItem("Seleccione", "Seleccione", True))
                DdlTabla.SelectedValue = "Seleccione"
                DdlCampo.Visible = True
                DdlOperador.Visible = True
                TxtValores.Visible = True
                DdlConector.Visible = True
                DdlConector.Items.Clear()
                DdlConector.Items.Add(New ListItem("Seleccione", "Seleccione", True))
                DdlConector.Items.Add(New ListItem("Y", "AND", True))
                DdlConector.Items.Add(New ListItem("O", "OR", True))
                DdlConector.SelectedValue = "Seleccione"
                DdlTipo.Visible = True
                DdlTipo.Items.Clear()
                DdlTipo.Items.Add(New ListItem("Seleccione", "Seleccione", True))
                DdlTipo.Items.Add(New ListItem("TEXTO", "VARCHAR2", True))
                DdlTipo.Items.Add(New ListItem("FECHA", "DATE", True))
                DdlTipo.Items.Add(New ListItem("NUMERO", "NUMBER", True))
                DdlTipo.SelectedValue = "Seleccione"
            End If
        Catch ex As Exception
        End Try

    End Sub

    Sub DesHabilitarVisualizar()
        Try
            For i As Integer = 0 To 11
                Dim DdlOperadorO As DropDownList = TryCast(Me.FindControl("ctl00$CPHMaster$DdlConector" & i), DropDownList)
                If DdlOperadorO.SelectedValue = "Seleccione" Then
                    For i2 As Integer = i + 1 To 3
                        DdlTabla = TryCast(Me.FindControl("ctl00$CPHMaster$DdlTabla" & i2), DropDownList)
                        DdlCampo = TryCast(Me.FindControl("ctl00$CPHMaster$DdlCampo" & i2), DropDownList)
                        DdlOperador = TryCast(Me.FindControl("ctl00$CPHMaster$DdlOperador" & i2), DropDownList)
                        TxtValores = TryCast(Me.FindControl("ctl00$CPHMaster$TxtValores" & i2), TextBox)
                        DdlConector = TryCast(Me.FindControl("ctl00$CPHMaster$DdlConector" & i2), DropDownList)
                        DdlTipo = TryCast(Me.FindControl("ctl00$CPHMaster$DdlTipo" & i2), DropDownList)
                        GvCatalogos = TryCast(Me.FindControl("ctl00$CPHMaster$GVCatalogos" & i2), GridView)
                        Panel = TryCast(FindControl("ctl00$CPHMaster$Pnl" & i2), Panel)
                        'Calendario = TryCast(FindControl("ctl00$CPHMaster$TxtValores" & i2 & "_CalendarExtender"), AjaxControlToolkit.CalendarExtender)
                        If DdlOperador.Visible = True Then
                            DdlTabla.Items.Clear()
                            DdlTabla.Visible = False
                            DdlCampo.Items.Clear()
                            DdlCampo.Visible = False
                            DdlOperador.Items.Clear()
                            DdlOperador.Visible = False
                            TxtValores.Text = ""
                            TxtValores.Visible = False
                            DdlConector.Items.Clear()
                            DdlConector.Visible = False
                            DdlTipo.Items.Clear()
                            DdlTipo.Visible = False
                            Panel.CssClass = ""
                            GvCatalogos.DataSource = Nothing
                            GvCatalogos.DataBind()
                            'Calendario.Enabled = False
                        Else
                            Exit For
                        End If
                    Next
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub DdlConector_SelectedIndexChanged(sender As Object, e As EventArgs)
        DdlConector = sender
        If DdlConector.SelectedValue <> "Seleccione" Then
            HabilitarVisualizar(DdlConector.ID.ToString.Substring(11, 1) + 1)
        Else
            DesHabilitarVisualizar()
        End If
    End Sub
    Function Valida(where As String) As String
        Dim SSCommandValidaQ As New SqlCommand
        SSCommandValidaQ.CommandText = "Sp_Valida_Query"
        SSCommandValidaQ.CommandType = CommandType.StoredProcedure
        SSCommandValidaQ.Parameters.Add("@V_Valor", SqlDbType.NVarChar).Value = where

        Dim DtsExiste As DataTable = Consulta_Procedure(SSCommandValidaQ, "Sp_Valida_Query")
        Return DtsExiste.Rows(0).Item("RESULTADO")
    End Function

    Sub BorrarFilas(Nombre As String)
        Dim SSCommandBDetalles As New SqlCommand
        SSCommandBDetalles.CommandText = "Sp_Varios_Negociaciones"
        SSCommandBDetalles.CommandType = CommandType.StoredProcedure
        SSCommandBDetalles.Parameters.Add("@V_USUARIO", SqlDbType.NVarChar).Value = (StrConexion(1))
        SSCommandBDetalles.Parameters.Add("@V_VALOR", SqlDbType.NVarChar).Value = Nombre
        SSCommandBDetalles.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 2

        Dim DtsDetalles As DataTable = Consulta_Procedure(SSCommandBDetalles, "Campos")

        Dim SSCommandFilas As New SqlCommand
        SSCommandFilas.CommandText = "Sp_Varios_Negociaciones"
        SSCommandFilas.CommandType = CommandType.StoredProcedure
        SSCommandFilas.Parameters.Add("@V_USUARIO", SqlDbType.NVarChar).Value = (StrConexion(1))
        SSCommandFilas.Parameters.Add("@V_VALOR", SqlDbType.NVarChar).Value = Nombre
        SSCommandFilas.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 3
        Dim DtsF As DataTable = Consulta_Procedure(SSCommandFilas, "Campos")
    End Sub

    Sub CrearFila(Where As String, Nombre As String, Bandera As Integer)
        For i As Integer = 0 To 14
            DdlTabla = TryCast(Me.FindControl("ctl00$CPHMaster$DdlTabla" & i), DropDownList)
            DdlCampo = TryCast(Me.FindControl("ctl00$CPHMaster$DdlCampo" & i), DropDownList)
            DdlOperador = TryCast(Me.FindControl("ctl00$CPHMaster$DdlOperador" & i), DropDownList)
            TxtValores = TryCast(Me.FindControl("ctl00$CPHMaster$TxtValores" & i), TextBox)
            DdlConector = TryCast(Me.FindControl("ctl00$CPHMaster$DdlConector" & i), DropDownList)
            If DdlTabla.Visible = True Then

                Dim SSCommandNEGOCIACIONESDetalle As New SqlCommand
                SSCommandNEGOCIACIONESDetalle.CommandText = "SP_ADD_NEGOCIACIONES_DETALLE"
                SSCommandNEGOCIACIONESDetalle.CommandType = CommandType.StoredProcedure
                SSCommandNEGOCIACIONESDetalle.Parameters.Add("@V_CAT_DN_SECUENCIA", SqlDbType.Decimal).Value = i
                SSCommandNEGOCIACIONESDetalle.Parameters.Add("@V_CAT_DN_NOMBRE", SqlDbType.NVarChar).Value = Nombre
                SSCommandNEGOCIACIONESDetalle.Parameters.Add("@V_CAT_DN_TABLA", SqlDbType.NVarChar).Value = DdlTabla.SelectedValue
                SSCommandNEGOCIACIONESDetalle.Parameters.Add("@V_CAT_DN_CAMPO", SqlDbType.NVarChar).Value = DdlCampo.SelectedValue
                SSCommandNEGOCIACIONESDetalle.Parameters.Add("@V_Cat_Dn_Operdor", SqlDbType.NVarChar).Value = DdlOperador.SelectedValue
                SSCommandNEGOCIACIONESDetalle.Parameters.Add("@V_CAT_DN_VALOR", SqlDbType.NVarChar).Value = TxtValores.Text
                SSCommandNEGOCIACIONESDetalle.Parameters.Add("@V_CAT_DN_CONECTOR", SqlDbType.NVarChar).Value = DdlConector.SelectedValue
                Ejecuta_Procedure(SSCommandNEGOCIACIONESDetalle)
            Else
                Exit For
            End If
        Next
        Dim SSCommandNEGOCIACIONES As New SqlCommand
        SSCommandNEGOCIACIONES.CommandText = "SP_ADD_NEGOCIACIONES"
        SSCommandNEGOCIACIONES.CommandType = CommandType.StoredProcedure
        SSCommandNEGOCIACIONES.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = Bandera
        SSCommandNEGOCIACIONES.Parameters.Add("@V_Cat_Ne_Nombre", SqlDbType.NVarChar).Value = Nombre
        SSCommandNEGOCIACIONES.Parameters.Add("@V_Cat_Ne_Query", SqlDbType.NVarChar).Value = "" '"select  PR_MC_CREDITO CREDITO, '" & TxtNombre.Text & "' NEGOCIACION  from VI_INFORMACION_RE WHERE " & Where
        SSCommandNEGOCIACIONES.Parameters.Add("@V_Cat_Ne_Etiqueta", SqlDbType.NVarChar).Value = "" 'TxtCat_Ne_Etiqueta.Text
        SSCommandNEGOCIACIONES.Parameters.Add("@V_Cat_Ne_Desc_Min", SqlDbType.Decimal).Value = "" 'TxtCAT_NE_DESC_MIN.Text
        SSCommandNEGOCIACIONES.Parameters.Add("@V_Cat_Ne_Desc_Max", SqlDbType.Decimal).Value = "" 'TxtCAT_NE_DESC_MAX.Text
        SSCommandNEGOCIACIONES.Parameters.Add("@V_Cat_Ne_Num_Pagos", SqlDbType.Decimal).Value = "" 'DdlCAT_NE_NUM_PAGOS.SelectedValue
        SSCommandNEGOCIACIONES.Parameters.Add("@V_Cat_Ne_Diasprimerpago", SqlDbType.Decimal).Value = "" 'TxtCAT_NE_DIASPRIMERPAGO.Text
        SSCommandNEGOCIACIONES.Parameters.Add("@V_Cat_Ne_Diaspagos", SqlDbType.Decimal).Value = "" 'TxtCat_Ne_Diaspagos.Text
        SSCommandNEGOCIACIONES.Parameters.Add("@V_Cat_Ne_Porciento", SqlDbType.Decimal).Value = "" 'TxtCAT_NE_PORCIENTO.Text
        SSCommandNEGOCIACIONES.Parameters.Add("@V_Cat_Ne_Porciento2", SqlDbType.Decimal).Value = "" 'TxtCAT_NE_PORCIENTO2.Text
        SSCommandNEGOCIACIONES.Parameters.Add("@V_Cat_Ne_Pagounico", SqlDbType.Decimal).Value = "" 'DdlCAT_NE_PAGOUNICO.SelectedValue
        Dim DtsFilas As DataTable = Consulta_Procedure(SSCommandNEGOCIACIONES, "Direccion")


        Dim SSCommandRehacerVista As New SqlCommand
        SSCommandRehacerVista.CommandText = "SP_VI_NEGOCIACIONES"
        SSCommandRehacerVista.CommandType = CommandType.StoredProcedure
        Ejecuta_Procedure(SSCommandRehacerVista)

        Aviso(DtsFilas.Rows(0).Item("NEGOCIACIONES"))

    End Sub
    Protected Sub Cbx_(sender As Object, e As System.EventArgs)
        Dim Quien As CheckBox = DirectCast(sender, CheckBox)
        GvCatalogos = TryCast(Me.FindControl("ctl00$CPHMaster$GVCatalogos" & Quien.ID.Substring(9, 1)), GridView)
        TxtValores = TryCast(Me.FindControl("ctl00$CPHMaster$TxtValores" & Quien.ID.Substring(9, 1)), TextBox)
        Dim Valores As String = ""
        For Each gvRow As GridViewRow In GvCatalogos.Rows
            Dim chkSelect As CheckBox = DirectCast(gvRow.FindControl(Quien.ID), CheckBox)
            If chkSelect.Checked = True Then
                Valores = Valores & "'" & HttpUtility.HtmlDecode(gvRow.Cells(1).Text) & "',"
            End If
        Next
        TxtValores.Text = Valores.Substring(0, Len(Valores) - 1)
    End Sub

    Function GenerarReporte(ByVal V_Campo As String, ByVal V_Reporte As String, ByVal V_Valor As String, ByVal V_Bandera As String) As Object
        If V_Valor <> "" And V_Bandera = 5 Then
            V_Valor = " where " & V_Valor
        End If
        Dim SSCommandId As New SqlCommand
        SSCommandId.CommandText = "Sp_Generar_Reporte"
        SSCommandId.CommandType = CommandType.StoredProcedure
        SSCommandId.Parameters.Add("@V_Campo", SqlDbType.NVarChar).Value = V_Campo
        SSCommandId.Parameters.Add("@V_Reporte", SqlDbType.NVarChar).Value = V_Reporte
        SSCommandId.Parameters.Add("@V_Valor", SqlDbType.NVarChar).Value = V_Valor
        SSCommandId.Parameters.Add("@V_Agencia", SqlDbType.NVarChar).Value = CType(Session("Usuario"), USUARIO).CAT_LO_AGENCIA
        SSCommandId.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = V_Bandera
        Dim DtsObjetos As DataTable = Consulta_Procedure(SSCommandId, "Reporte")
        If V_Bandera = 0 Then
            Return DtsObjetos
        Else
            Return DtsObjetos
        End If
    End Function

    Protected Sub Aviso(ByVal MSJ As String)
        RadAviso.RadAlert(MSJ.Replace("""", "").Replace("'", "").Replace(Chr(10), "").Replace(Chr(13), ""), 440, 155, "AVISO", Nothing)
    End Sub
    Protected Sub Confirma(ByVal MSJ As String)
        RadAviso.RadConfirm(MSJ, "confirmCallbackFn", 440, 155, Nothing, "Alerta")
    End Sub



    Public Sub RGvNEGOCIACIONES_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RGVNegociaciones.NeedDataSource
        Dim tab As DataTable = LLenarGv(0)

        RGVNegociaciones.DataSource = tab
        If tab.Rows.Count > 0 Then
            rBtnEjecutarAsig.Enabled = True
            BtnDescargar.Enabled = True
            'Session("ID_NEGO") = DtsFilas.Rows.Count + 1
            'GvNEGOCIACIONES.Visible = True
            'GvNEGOCIACIONES.DataSource = DtsFilas.DefaultView
            'GvNEGOCIACIONES.DataBind()
            'GvNEGOCIACIONES.Enabled = True
            'RblOpcion.Visible = True
            'RblOpcion.SelectedValue = "Seleccione"
        Else
            rBtnEjecutarAsig.Enabled = False
            BtnDescargar.Enabled = False
        End If
    End Sub

    Private Sub RGVNegociaciones_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RGVNegociaciones.ItemCommand
        If e.CommandName = "Update" Then
            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(10) As String

            valores(0) = CType(MyUserControl.FindControl("TxtNombre"), RadTextBox).Text
            valores(1) = CType(MyUserControl.FindControl("TxtCAT_NE_DESC_MAX1"), RadNumericTextBox).Text
            valores(2) = CType(MyUserControl.FindControl("DdlCampoNego1"), RadDropDownList).SelectedValue
            valores(3) = CType(MyUserControl.FindControl("TxtCAT_NE_DESC_MAX2"), RadNumericTextBox).Text
            valores(4) = CType(MyUserControl.FindControl("DdlCampoNego2"), RadDropDownList).SelectedValue
            valores(5) = CType(MyUserControl.FindControl("TxtCAT_NE_DESC_MAX3"), RadNumericTextBox).Text
            valores(6) = CType(MyUserControl.FindControl("DdlCampoNego3"), RadDropDownList).SelectedValue
            valores(7) = CType(MyUserControl.FindControl("TxtCAT_NE_NUM_PAGOS"), RadNumericTextBox).Text
            valores(8) = CType(MyUserControl.FindControl("TxtID"), RadTextBox).Text
            valores(9) = CType(MyUserControl.FindControl("NTxtNivel"), RadNumericTextBox).Text

            If valores(0) = "" Then
                Aviso("El nombre no puede estar vacío")
                e.Canceled = True
            Else
                Try
                    Dim SSCommannego As New SqlCommand
                    SSCommannego.CommandText = "SP_CATALOGO_NEGO"
                    SSCommannego.CommandType = CommandType.StoredProcedure
                    SSCommannego.Parameters.Add("@v_bandera", SqlDbType.Decimal).Value = 6
                    SSCommannego.Parameters.Add("@v_NOMBRE", SqlDbType.NVarChar).Value = valores(0)
                    SSCommannego.Parameters.Add("@v_DESC_MAX1", SqlDbType.NVarChar).Value = valores(1)
                    SSCommannego.Parameters.Add("@v_CAMPO_DESC_MAX1", SqlDbType.NVarChar).Value = valores(2)
                    SSCommannego.Parameters.Add("@v_DESC_MAX2", SqlDbType.NVarChar).Value = valores(3)
                    SSCommannego.Parameters.Add("@v_CAMPO_DESC_MAX2", SqlDbType.NVarChar).Value = valores(4)
                    SSCommannego.Parameters.Add("@v_DESC_MAX3", SqlDbType.NVarChar).Value = valores(5)
                    SSCommannego.Parameters.Add("@v_CAMPO_DESC_MAX3", SqlDbType.NVarChar).Value = valores(6)
                    SSCommannego.Parameters.Add("@v_NUM_PAG", SqlDbType.NVarChar).Value = valores(7)
                    SSCommannego.Parameters.Add("@v_ID", SqlDbType.NVarChar).Value = valores(8)
                    SSCommannego.Parameters.Add("@v_nivel", SqlDbType.NVarChar).Value = valores(9)

                    Dim DtsVarios As DataTable = Consulta_Procedure(SSCommannego, "SP_CATALOGO_NEGO")
                    If DtsVarios.Rows(0)(0).ToString = "OK" Then
                        RGVNegociaciones.Rebind()
                        Aviso("Negociacion Actualizda")
                    Else
                        Aviso(DtsVarios.Rows(0)(0).ToString)
                    End If

                Catch ex As Exception

                    Aviso("No Fue Posible Actualizar. Razon: " + ex.Message)
                    e.Canceled = True
                End Try
            End If
        ElseIf e.CommandName = "PerformInsert" Then
            Try
                RGVNegociaciones.Rebind()
                Aviso("Negociacion Creada")
            Catch ex As Exception

                Aviso("No Fue Posible Actualizar. Razon: " + ex.Message)
                e.Canceled = True
            End Try
        ElseIf e.CommandName = "Delete" Then

            'Dim ID As String = (CType(e.Item, GridDataItem)).OwnerTableView.DataKeyValues(e.Item.ItemIndex)("Negociacion").ToString
            Opciones(7, e.Item.Cells.Item(5).Text)
            Aviso("Se Elimino el registro")
        ElseIf e.CommandName = "Edit" Then
            Session("Negociacion") = e.Item.Cells.Item(3).Text
            Session("PostBack") = True
            Session("ID_NEGO") = e.Item.Cells.Item(5).Text
        ElseIf e.CommandName = "InitInsert" Then
            Session("Negociacion") = ""
            Session("PostBack") = True
            Dim ID_NEGO As DataTable = LLenarGv(5)
            Dim ID_NEGO1 As Integer = ID_NEGO.Rows(0)(0).ToString
            Session("ID_NEGO") = ID_NEGO1
        ElseIf e.CommandName = "Habilitar" Then
            Opciones(8, e.Item.Cells.Item(5).Text)

            RGVNegociaciones.Rebind()
        ElseIf e.CommandName = "Deshabilitar" Then
            Opciones(9, e.Item.Cells.Item(5).Text)
            RGVNegociaciones.Rebind()
        ElseIf e.CommandName = "Simular" Then
            simular(e.Item.Cells.Item(3).Text)
            e.Canceled = True
        ElseIf e.CommandName = "Descargar" Then
            decargar(e.Item.Cells.Item(3).Text)
            e.Canceled = True
        End If
    End Sub
    Sub Opciones(Bandera As Integer, Valor As String)
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_CATALOGO_NEGO"
        SSCommand.CommandType = CommandType.StoredProcedure
        ' SSCommand.Parameters.Add("@V_USUARIO", SqlDbType.NVarChar).Value = DesEncriptarCadena(Conexiones.StrConexion(1))
        SSCommand.Parameters.Add("@v_ID", SqlDbType.NVarChar).Value = Valor
        SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = Bandera

        Dim DtsVariable As DataTable = Consulta_Procedure(SSCommand, "Campos")

    End Sub
    Private Sub simular(cual As String)

        Dim SSCommand2 As New SqlCommand
        SSCommand2.CommandText = "SP_CATALOGO_NEGO"
        SSCommand2.CommandType = CommandType.StoredProcedure
        SSCommand2.Parameters.Add("@v_NOMBRE", SqlDbType.NVarChar).Value = cual
        SSCommand2.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 15
        Dim DtsVariable2 As DataTable = Consulta_Procedure(SSCommand2, "Campos")
        Dim Cuantos As Integer = DtsVariable2.Rows(0).Item(0)
        If Cuantos <= 1000 Then
            lblCuantos.Text = Cuantos & " creditos totales"
        Else
            lblCuantos.Text = "Mostrando 1000 de " & Cuantos & " creditos totales"
        End If

        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_CATALOGO_NEGO"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_NOMBRE", SqlDbType.NVarChar).Value = cual
        SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 14
        Dim DtsVariable As DataTable = Consulta_Procedure(SSCommand, "Campos")
        GvSimulacion.DataSource = DtsVariable
        GvSimulacion.DataBind()

        MostrarOcultarVentana(WinSim, 1, 0)

    End Sub
    Private Sub decargar(cual As String)


        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_CATALOGO_NEGO"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_NOMBRE", SqlDbType.NVarChar).Value = cual
        SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 21
        Dim DtsVariable As DataTable = Consulta_Procedure(SSCommand, "Campos")
        ExportToXcel_SomeReport(DtsVariable, cual, Me.Page)

    End Sub
    Protected Sub ExportToXcel_SomeReport(dt As DataTable, fileName As String, page As Page)
        'dt.Columns.RemoveAt(0)
        Dim recCount = dt.Rows.Count
        fileName = String.Format(fileName, DateTime.Now.ToString("MMddyyyy_hhmmss"))
        Dim xlsx = New XLWorkbook()
        'Dim ws = xlsx.Worksheets.Add(fileName)
        Dim ws = xlsx.Worksheets.Add("Datos")
        ws.Style.Font.Bold = False



        '''''''''''''''' CICLO ''''''''''''''''''''''''''''''''
        For r As Integer = 0 To dt.Columns.Count - 1
            ws.Cell(1, r + 1).Value = dt.Columns(r).ColumnName
            ws.Cell(1, r + 1).SetDataType(XLCellValues.Text)
            ws.Cell(1, r + 1).Style.Fill.BackgroundColor = XLColor.BlueGray
            ws.Cell(1, r + 1).Style.Font.FontColor = XLColor.White
            ws.Cell(1, r + 1).Style.Font.Bold = True
            ws.Column(r + 1).AdjustToContents()
        Next

        For e As Integer = 0 To dt.Rows.Count - 1

            For r As Integer = 0 To dt.Columns.Count - 1
                Dim v_valor As String = ""

                If Not IsDBNull(dt.Rows(e)(dt.Columns(r).ColumnName)) Then
                    v_valor = dt.Rows(e)(dt.Columns(r).ColumnName)
                Else
                    v_valor = Nothing
                End If

                ws.Cell(e + 2, r + 1).Value = v_valor
                If dt.Columns(r).ColumnName.ToLower Like "*credito*" Or dt.Columns(r).ColumnName = "Referencia Bancaria" Then
                    ws.Cell(e + 2, r + 1).SetDataType(XLCellValues.Text)
                End If
            Next
        Next
        ''''''''''''''' CICLO ''''''''''''''''''''''''''''''''


        DynaGenExcelFile(fileName, page, xlsx)

    End Sub

    Protected Sub DynaGenExcelFile(fileName As String, page As Page, xlsx As XLWorkbook)
        Try
            page.Response.ClearContent()
            page.Response.ClearHeaders()
            page.Response.ContentType = "application/vnd.ms-excel"
            page.Response.AppendHeader("Content-Disposition", String.Format("attachment;filename={0}.xlsx", fileName))

            Using memoryStream As New MemoryStream()
                xlsx.SaveAs(memoryStream)
                memoryStream.WriteTo(page.Response.OutputStream)
            End Using
            page.Response.Flush()
            page.Response.[End]()
        Catch ex As Exception
            Dim msj As String = ex.Message
        End Try


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

    Private Sub rBtnEjecutarAsig_Click(sender As Object, e As EventArgs) Handles rBtnEjecutarAsig.Click
        Try

            Dim Commd10 As New SqlCommand 'elimina de la cat_nego_creditos todos los que sean de nivle x
            Commd10.CommandText = "SP_CATALOGO_NEGO"
            Commd10.CommandType = CommandType.StoredProcedure
            Commd10.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 23
            Commd10.Parameters.Add("@v_nivel", SqlDbType.Decimal).Value = DdlNivel.SelectedValue
            Dim Dts10 As DataTable = Consulta_Procedure(Commd10, Commd10.CommandText)
            If Dts10.TableName = "Exception" Then
                Throw New Exception(Dts10.Rows(0).Item(0).ToString)
            End If



            Dim Commd As New SqlCommand
            Commd.CommandText = "SP_CATALOGO_NEGO"
            Commd.CommandType = CommandType.StoredProcedure
            Commd.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 19
            Commd.Parameters.Add("@v_nivel", SqlDbType.Decimal).Value = DdlNivel.SelectedValue
            Dim DtsN As DataTable = Consulta_Procedure(Commd, Commd.CommandText)
            If DtsN.TableName = "Exception" Then
                Throw New Exception(DtsN.Rows(0).Item(0).ToString)
            End If
            For i = 0 To DtsN.Rows.Count - 1

                Dim Cmmd As New SqlCommand 'insert a la cat_nego_creditos
                Cmmd.CommandText = "SP_CATALOGO_NEGO"
                Cmmd.CommandType = CommandType.StoredProcedure
                Cmmd.Parameters.Add("@v_NOMBRE", SqlDbType.NVarChar).Value = DtsN.Rows(i).Item(0).ToString
                Cmmd.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 13
                Dim DtsV As DataTable = Consulta_Procedure(Cmmd, Cmmd.CommandText)
                If DtsV.TableName = "Exception" Then
                    Dim err As String = ""
                    Try
                        err = "Fail " & DtsN.Rows(i).Item(0).ToString & ": " & DtsV.Rows(1).Item(1).ToString
                    Catch
                    End Try
                    If err = "" Then
                        err = "Fail " & DtsN.Rows(i).Item(0).ToString & ": " & DtsV.Rows(0).Item(0).ToString
                    End If
                    Throw New Exception(err)
                End If
            Next
            Aviso("Ejecutada exitosamente")
        Catch ex As Exception
            Aviso(ex.Message)
        End Try
    End Sub

    Private Sub BtnAceptarConfirmacion_Click(sender As Object, e As EventArgs) Handles BtnAceptarConfirmacion.Click

    End Sub

    Private Sub DdlNivel_SelectedIndexChanged(sender As Object, e As DropDownListEventArgs) Handles DdlNivel.SelectedIndexChanged
        If DdlNivel.SelectedIndex <> 0 Then
            RGVNegociaciones.Visible = True
            rBtnEjecutarAsig.Enabled = True
            BtnDescargar.Enabled = True
            RGVNegociaciones.Rebind()

        Else
            RGVNegociaciones.Visible = False
            rBtnEjecutarAsig.Enabled = False
            BtnDescargar.Enabled = False
        End If
    End Sub

    Private Sub BtnDescargar_Click(sender As Object, e As EventArgs) Handles BtnDescargar.Click
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_CATALOGO_NEGO"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_nivel", SqlDbType.NVarChar).Value = DdlNivel.SelectedValue
        SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 22
        Dim DtsVariable As DataTable = Consulta_Procedure(SSCommand, "Campos")
        ExportToXcel_SomeReport(DtsVariable, "Reglas Nivel " & DdlNivel.SelectedValue, Me.Page)
    End Sub

    'Private Sub GvSimulacion_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles GvSimulacion.NeedDataSource
    '    Try
    '        GvSimulacion.DataSource = Session("TbS")
    '    Catch ex As Exception
    '        GvSimulacion.DataSource = Nothing
    '    End Try
    'End Sub
End Class
