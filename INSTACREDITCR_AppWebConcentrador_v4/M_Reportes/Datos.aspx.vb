Imports System.Data.SqlClient
Imports System.Data
Imports AjaxControlToolkit
Imports System.Web.UI.WebControls.Label
Imports Conexiones
Imports Funciones
Imports Db
Imports Telerik.Web.UI

Partial Class Datos
    Inherits System.Web.UI.Page

    Public Property tmpUSUARIO As IDictionary
        Get
            Return CType(Session("USUARIO"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("USUARIO") = value
        End Set
    End Property

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim SSCommand As New SqlCommand
                SSCommand.CommandText = "SP_CATALOGOS"
                SSCommand.CommandType = CommandType.StoredProcedure
                SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 17
                SSCommand.Parameters.Add("@V_Valor", SqlDbType.NVarChar).Value = ""

                Dim DtsCodigos As DataTable = Consulta_Procedure(SSCommand, "Codigos")
                DdlCAT_RED_BLOQUE.DataTextField = "Grupo"
                DdlCAT_RED_BLOQUE.DataValueField = "Grupo"
                DdlCAT_RED_BLOQUE.DataSource = DtsCodigos
                DdlCAT_RED_BLOQUE.DataBind()
                listBoxCatalogosDisponibles.DataBind()
                If Session("Nuevo") = "Nuevo" Then
                    DdlCAT_RED_BLOQUE.Enabled = True
                    TxtCAT_RED_NOMBRE.Enabled = True
                    TxtCAT_RED_DESCRIPCION.Enabled = True
                Else
                    'LlenarExistente(5, Session("Nuevo"))
                    LlenarExistente(6, Session("Nuevo"))

                    LlenarExistente(8, Session("Nuevo"))
                    LlenarExistente(7, Session("Nuevo"))
                End If
            End If
        Catch ex As Exception
            'SendMail("Page_Load", ex, "", "", HidenUrs.Value)
        End Try
    End Sub
    Sub LlenarExistente(ByVal V_Bandera As Integer, ByVal V_Valor As String)
        Dim SSCommandF As New SqlCommand
        SSCommandF.CommandText = "Sp_Cat_Reportes"
        SSCommandF.CommandType = CommandType.StoredProcedure
        SSCommandF.Parameters.Add("@V_BANDERA", SqlDbType.NVarChar).Value = V_Bandera
        SSCommandF.Parameters.Add("@V_Agencia", SqlDbType.NVarChar).Value = tmpUSUARIO("CAT_LO_NUM_AGENCIA")
        SSCommandF.Parameters.Add("@V_Valor", SqlDbType.NVarChar).Value = V_Valor
        Dim DtsCatalogos As DataTable = Consulta_Procedure(SSCommandF, "CATALOGO")
        If DtsCatalogos.Rows.Count > 0 Then
            If V_Bandera = 5 Then
                For Each row As DataRow In DtsCatalogos.Rows
                    Dim item As New RadListBoxItem(row("Cat_Ta_Desc"), row("Cat_Ta_Tabla"))
                    listBoxCatalogosSeleccionados.Items.Add(item)
                Next
            ElseIf V_Bandera = 6 Then
                DdlCAT_RED_BLOQUE.SelectedValue = DtsCatalogos.Rows(0)("Cat_Red_Bloque")
                TxtCAT_RED_NOMBRE.Text = DtsCatalogos.Rows(0)("CAT_RED_NOMBRE")
                TxtCAT_RED_DESCRIPCION.Text = DtsCatalogos.Rows(0)("CAT_RED_DESCRIPCION")
                DdlCAT_RED_BLOQUE.Enabled = False
                TxtCAT_RED_NOMBRE.Enabled = False
                TxtCAT_RED_DESCRIPCION.Enabled = False
            ElseIf V_Bandera = 7 Then
                For Each row As DataRow In DtsCatalogos.Rows
                    Dim item As New RadListBoxItem(row("CAT_RED_CAMPO_DESC").ToString, row("CAT_RED_CAMPO").ToString)
                    item.Attributes.Add("campoDisplay", row("CAT_RED_CAMPO_DESC").ToString)
                    item.Attributes.Add("campoDb", row("CAT_RED_CAMPO").ToString)
                    item.Attributes.Add("TipoDisplay", row("CAT_RED_TIPO").ToString)
                    item.Attributes.Add("TipoDb", "")
                    item.Attributes.Add("Tabla", row("CAT_RED_TABLA").ToString)
                    item.Attributes.Add("Condicion", row("CAT_RED_CONDICION").ToString)
                    item.Attributes.Add("Formato", row("CAT_RED_FORMATO").ToString)
                    listBoxCamposSeleccionados.Items.Add(item)
                Next
                listBoxCamposSeleccionados.DataBind()
                'For indice As Integer = 0 To DtsCatalogos.Rows.Count - 1
                '    ArregloBotonesOrden(indice, 0) = DtsCatalogos.Rows(indice)("campo").ToString.Split("|")(0).Trim & " | " & DtsCatalogos.Rows(indice)("campo").ToString.Split("|")(1).Trim
                '    ArregloBotonesOrden(indice, 1) = DtsCatalogos.Rows(indice)("campo")
                '    ArregloBotonesOrden(indice, 2) = DtsCatalogos.Rows(indice)("Formato")
                '    ArregloBotonesOrden(indice, 3) = DtsCatalogos.Rows(indice)("Condicion")
                'Next
                'Session("ArregloBotonesOrden") = ArregloBotonesOrden
                'ReconstruirOrden(GvConfiguracion, Session("ArregloBotonesOrden"))
            ElseIf V_Bandera = 8 Then
                For Each row As DataRow In DtsCatalogos.Rows
                    Dim item As New RadListBoxItem(row("Cat_Ta_Desc"), row("Cat_Ta_Tabla"))
                    listBoxCatalogosSeleccionados.Items.Add(item)
                    EliminarDuplicados(row("Cat_Ta_Tabla"))
                Next
                listBoxCamposSeleccionados.DataBind()
                cargarCamposDeTablasSeleccionadas()
            ElseIf V_Bandera = 10 Then
                For indice As Integer = 0 To DtsCatalogos.Rows.Count - 1
                    'For Each gvRow As GridViewRow In GvConfiguracion.Rows
                    '    Dim DdlFormato As DropDownList = DirectCast(gvRow.FindControl("DdlFormato"), DropDownList)
                    '    Dim Campo As Button = DirectCast(gvRow.FindControl("BtnCampo"), Button)
                    '    If DtsCatalogos.Rows(indice)("Cat_Red_Campo_Desc") = Campo.Text.ToString.Split("|")(0).Trim Then
                    '        DdlFormato.SelectedValue = DtsCatalogos.Rows(indice)("Cat_Red_Formato")
                    '        Exit For
                    '    End If
                    'Next
                Next
            ElseIf V_Bandera = 11 Then
                For indice As Integer = 0 To DtsCatalogos.Rows.Count - 1
                    'For Each gvRow As GridViewRow In GvConfiguracion.Rows
                    '    Dim cbx1 As CheckBox = DirectCast(gvRow.FindControl("cbx1"), CheckBox)
                    '    Dim Campo As Button = DirectCast(gvRow.FindControl("BtnCampo"), Button)
                    '    If DtsCatalogos.Rows(indice)("Cat_Red_Campo_Desc") = Campo.Text.ToString.Split("|")(0).Trim Then
                    '        cbx1.Checked = True
                    '        Exit For
                    '    End If
                    'Next
                Next
            End If

        End If

    End Sub

    Private Sub EliminarDuplicados(tabla As String)
        Try
            listBoxCatalogosDisponibles.Items.Remove(listBoxCatalogosDisponibles.FindItemByValue(tabla))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Reportes", "Datos.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub
    'Sub QueCampos(ByVal Tabla As String, ByVal V_Bandera As Integer)
    '    Dim SSCommandF As New SqlCommand
    '    SSCommandF.CommandText = "Sp_Cat_Reportes"
    '    SSCommandF.CommandType = CommandType.StoredProcedure
    '    SSCommandF.Parameters.Add("@V_BANDERA", SqlDbType.NVarChar).Value = V_Bandera
    '    SSCommandF.Parameters.Add("@V_Agencia", SqlDbType.NVarChar).Value = tmpUSUARIO("CAT_LO_NUM_AGENCIA")
    '    SSCommandF.Parameters.Add("@V_Valor", SqlDbType.NVarChar).Value = Tabla
    '    Dim DtsCatalogos As DataTable = Consulta_Procedure(SSCommandF, "CATALOGO")
    '    If DtsCatalogos.Rows.Count > 0 Then
    '        Dim posicion As Integer = 0
    '        For indiceC As Integer = 0 To 599
    '            If IsNothing(ArregloBotonesCampos(indiceC, 0)) Then
    '                posicion = indiceC
    '                For indice As Integer = 0 To DtsCatalogos.Rows.Count - 1
    '                    ArregloBotonesCampos(posicion, 0) = Tabla.ToString.Split(",")(0)
    '                    ArregloBotonesCampos(posicion, 1) = (DtsCatalogos.Rows(indice)("Campo_Nombre"))
    '                    posicion = posicion + 1
    '                Next
    '                Exit For
    '            End If
    '        Next

    '        Session("ArregloBotonesCampos") = ArregloBotonesCampos
    '        'ReconstruirCampos(GvCampos, Session("ArregloBotonesCampos"))
    '    End If
    'End Sub


    Function ReporteDetalle(ByVal V_CaT_Red_Id As Integer, ByVal V_Cat_Red_Nombre As String, ByVal V_Cat_Red_Descripcion As String, ByVal V_Cat_Red_Tabla_Desc As String, ByVal V_Cat_Red_Campo_Desc As String, ByVal V_Cat_Red_Condicion As Integer, ByVal V_Cat_Red_Formato As String, ByVal V_Cat_Red_Orden As Integer, ByVal V_Cat_Red_Tipo As String, ByVal v_CAT_RED_BLOQUE As String, ByVal V_Bandera As Integer) As String
        Dim SSCommandId As New SqlCommand
        SSCommandId.CommandText = "Sp_Add_Cat_Reporte_Detalle"
        SSCommandId.CommandType = CommandType.StoredProcedure
        SSCommandId.Parameters.Add("@V_Cat_Red_Id", SqlDbType.Decimal).Value = V_CaT_Red_Id
        SSCommandId.Parameters.Add("@V_Cat_Red_Nombre", SqlDbType.NVarChar).Value = V_Cat_Red_Nombre
        SSCommandId.Parameters.Add("@V_Cat_Red_Descripcion", SqlDbType.NVarChar).Value = V_Cat_Red_Descripcion
        SSCommandId.Parameters.Add("@V_Cat_Red_Tabla_Desc", SqlDbType.NVarChar).Value = V_Cat_Red_Tabla_Desc
        SSCommandId.Parameters.Add("@V_Cat_Red_Campo_Desc", SqlDbType.NVarChar).Value = V_Cat_Red_Campo_Desc
        SSCommandId.Parameters.Add("@V_Cat_Red_Condicion", SqlDbType.Decimal).Value = V_Cat_Red_Condicion
        SSCommandId.Parameters.Add("@V_Cat_Red_Formato", SqlDbType.NVarChar).Value = V_Cat_Red_Formato
        SSCommandId.Parameters.Add("@V_Cat_Red_Orden", SqlDbType.Decimal).Value = V_Cat_Red_Orden
        SSCommandId.Parameters.Add("@V_Cat_Red_Tipo", SqlDbType.NVarChar).Value = V_Cat_Red_Tipo
        SSCommandId.Parameters.Add("@v_CAT_RED_BLOQUE", SqlDbType.NVarChar).Value = v_CAT_RED_BLOQUE
        SSCommandId.Parameters.Add("@V_Agencia", SqlDbType.NVarChar).Value = tmpUSUARIO("CAT_LO_NUM_AGENCIA")
        SSCommandId.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = V_Bandera
        Dim DtsId As DataTable = Consulta_Procedure(SSCommandId, "Reporte")
        Return DtsId.Rows(0)(0)
    End Function

    Private Sub listBoxCatalogosDisponibles_DataBinding(sender As Object, e As EventArgs) Handles listBoxCatalogosDisponibles.DataBinding
        Try
            Dim SSCommandF As New SqlCommand
            SSCommandF.CommandText = "Sp_Cat_Reportes"
            SSCommandF.CommandType = CommandType.StoredProcedure
            SSCommandF.Parameters.Add("@V_BANDERA", SqlDbType.NVarChar).Value = 1
            SSCommandF.Parameters.Add("@V_Agencia", SqlDbType.NVarChar).Value = tmpUSUARIO("CAT_LO_NUM_AGENCIA")
            Dim DtsCatalogos As DataTable = Consulta_Procedure(SSCommandF, "CATALOGO")
            listBoxCatalogosDisponibles.DataTextField = "Cat_Ta_Desc"
            listBoxCatalogosDisponibles.DataValueField = "Cat_Ta_Tabla"
            listBoxCatalogosDisponibles.DataSource = DtsCatalogos
        Catch ex As Exception
            RadNotification1.Text = "Error al cargar las tablas. Intente de neuvo más tarde"
            RadNotification1.Title = "Error"
            RadNotification1.Show()
        End Try
    End Sub

    Private Sub cargarCamposDeTablasSeleccionadas()
        Dim dataSource As New DataTable
        listBoxCamposDisponibles.Items.Clear()
        listBoxCamposSeleccionados.Items.Clear()
        'Traemos todos los campos de las tablas seleccionadas
        For Each item As RadListBoxItem In listBoxCatalogosSeleccionados.Items
            Dim SSCommandF As New SqlCommand
            SSCommandF.CommandText = "Sp_Cat_Reportes"
            SSCommandF.CommandType = CommandType.StoredProcedure
            SSCommandF.Parameters.Add("@V_BANDERA", SqlDbType.NVarChar).Value = 2
            SSCommandF.Parameters.Add("@V_Agencia", SqlDbType.NVarChar).Value = tmpUSUARIO("CAT_LO_NUM_AGENCIA")
            SSCommandF.Parameters.Add("@V_Valor", SqlDbType.NVarChar).Value = item.Value
            Dim DtsCatalogos As DataTable = Consulta_Procedure(SSCommandF, "CATALOGO")
            dataSource.Merge(DtsCatalogos)
        Next
        If dataSource.Rows.Count > 1 Then
            'Personalizamos los items para que cada item tenga
            'informacion más detallada
            For Each row As DataRow In dataSource.Rows
                Dim item As New RadListBoxItem(row("campo_nombre").ToString.Split("|")(0), row("campo"))
                item.Attributes.Add("campoDisplay", row("campo_nombre").ToString.Split("|")(0))
                item.Attributes.Add("campoDb", row("campo").ToString)
                item.Attributes.Add("TipoDisplay", row("campo_nombre").ToString.Split("|")(1))
                item.Attributes.Add("TipoDb", row("tipo").ToString)
                item.Attributes.Add("Tabla", row("Tabla1").ToString)

                listBoxCamposDisponibles.Items.Add(item)
            Next
            listBoxCamposDisponibles.DataBind()

            'btnConfirmarCampos.Focus()

            RadNotification1.Text = "Catalogos Cargados Correctamente"
            RadNotification1.Title = "OK"
            RadNotification1.Show()
        Else
            listBoxCamposDisponibles.Items.Clear()
            RadNotification1.Text = "Catalogos Cargados Incorrectamente"
            RadNotification1.Title = "OK"
            RadNotification1.Show()
        End If
    End Sub

    Private Sub listBoxCatalogosDisponibles_Transferred(sender As Object, e As RadListBoxTransferredEventArgs) Handles listBoxCatalogosDisponibles.Transferred
        cargarCamposDeTablasSeleccionadas()
        baja()
    End Sub

    Private Sub listBoxCamposDisponibles_Transferred(sender As Object, e As RadListBoxTransferredEventArgs) Handles listBoxCamposDisponibles.Transferred
        listBoxCamposSeleccionados.DataBind()
        baja()
    End Sub
    Protected Sub ddlFormato_DataBinding(sender As Object, e As EventArgs)
        Dim ddl As RadDropDownList = sender
        'Verificamos que el DDL no tenga items
        'De otra forma, agregará los items una y otra vez xD
        If ddl.Items.Count = 0 Then
            Dim item As RadListBoxItem = ddl.NamingContainer
            Dim tipo As String = item.Attributes("TipoDisplay").ToString.Trim(" ")
            If tipo.ToUpper = "FECHA" Then
                ddl.Items.Add(New DropDownListItem("DD/MM/AAAA HH:MI:SS", "Minutos"))
                ddl.Items.Add(New DropDownListItem("DD/MM/AAAA", "Hora"))
                ddl.Items.Add(New DropDownListItem("Contar", "Contar"))
            ElseIf tipo.ToUpper = "NUMERO" Then
                ddl.Items.Add(New DropDownListItem("Numerico", "Numero"))
                ddl.Items.Add(New DropDownListItem("Moneda", "Moneda"))
                ddl.Items.Add(New DropDownListItem("Contar", "Contar"))
                ddl.Items.Add(New DropDownListItem("Sumar", "Sumar"))
                ddl.Items.Add(New DropDownListItem("Promedio", "Promedio"))
            Else
                ddl.Items.Add(New DropDownListItem("Contar", "Contar"))
            End If
            If item.Attributes("Condicion") = "1" Then
                CType(item.FindControl("checkCondicional"), RadCheckBox).Checked = True
            End If
            If item.Attributes("Formato") <> "" Then
                ddl.FindItemByValue(item.Attributes("Formato")).Selected = True
            End If
        End If
    End Sub

    Private Function ObtenerDatoslistBoxCamposSeleccionados(ByRef item As RadListBoxItem) As IDictionary(Of String, String)
        Dim datos As IDictionary(Of String, String) = New Dictionary(Of String, String)
        Dim ddl As String = CType(item.FindControl("ddlFormato"), RadDropDownList).SelectedValue
        Dim condicional As String = IIf(CType(item.FindControl("checkCondicional"), RadCheckBox).Checked, "1", "0")
        datos.Add("Formato", ddl)
        datos.Add("Condicional", condicional)
        datos.Add("CampoDisplay", item.Attributes("campoDisplay"))
        datos.Add("CampoDb", item.Attributes("campoDb"))
        datos.Add("TipoDisplay", item.Attributes("TipoDisplay"))
        datos.Add("TipoDb", item.Attributes("TipoDb"))
        datos.Add("Tabla", item.Attributes("Tabla"))
        Return datos
    End Function

    Private Sub btnConfirmar_Click(sender As Object, e As EventArgs) Handles btnConfirmar.Click
        Dim Condiciones As Integer = 0
        For Each item As RadListBoxItem In listBoxCamposSeleccionados.Items
            Dim Condicion As Boolean = CType(item.FindControl("checkCondicional"), RadCheckBox).Checked
            If Condicion Then
                Condiciones += 1
            End If
        Next

        If Session("Nuevo") = "Nuevo" Then
            If TxtCAT_RED_NOMBRE.Text = "" Then
                showModal(RadNotification1, "deny", "Error", "Escriba El Nombre Del Reporte")
                TxtCAT_RED_NOMBRE.Focus()
            ElseIf ReporteDetalle(0, TxtCAT_RED_NOMBRE.Text, "", "", "", 0, "", 0, "", "", 0) >= 1 Then
                showModal(RadNotification1, "deny", "Error", "Ya Existe Este Nombre Valide")
                TxtCAT_RED_NOMBRE.Focus()
            ElseIf TxtCAT_RED_DESCRIPCION.Text = "" Then
                showModal(RadNotification1, "deny", "Error", "Escriba Una Descripción Del Reporte")
                TxtCAT_RED_DESCRIPCION.Focus()
            ElseIf DdlCAT_RED_BLOQUE.SelectedValue = "" Then
                showModal(RadNotification1, "deny", "Error", "Debes De Seleccionar Un Grupo")
                DdlCAT_RED_BLOQUE.Focus()
            ElseIf Condiciones = 0 Then
                showModal(RadNotification1, "deny", "Error", "Debes De Seleccionar Al Menos Un Campo Para Condición")
                listBoxCamposSeleccionados.Focus()
            Else
                Session("ID") = ReporteDetalle(0, "", "", "", "", 0, "", 0, "", "", 1)
                Dim Orden As Integer = 1
                Dim uniquetablas As List(Of String) = New List(Of String)
                For Each item As RadListBoxItem In listBoxCamposSeleccionados.Items
                    Dim itemData As IDictionary(Of String, String) = ObtenerDatoslistBoxCamposSeleccionados(item)
                    ReporteDetalle(Session("ID"), TxtCAT_RED_NOMBRE.Text, TxtCAT_RED_DESCRIPCION.Text, itemData("Tabla"), itemData("CampoDb"), itemData("Condicional"), itemData("Formato"), Orden, itemData("TipoDisplay"), DdlCAT_RED_BLOQUE.SelectedValue, 2)
                    Orden += 1
                    If itemData("Formato") = "Contar" Then
                        Session("Agrupar") = itemData("CampoDisplay")
                    End If
                    If Not uniquetablas.Contains(itemData("Tabla")) Then
                        uniquetablas.Add(itemData("Tabla"))
                    End If
                Next
                Dim leftJoin As String = IIf(switch1.Checked, "1", "0")
                Dim tablas As String = "'" & String.Join("','", uniquetablas) & "'"
                ReporteDetalle(Session("ID"), TxtCAT_RED_NOMBRE.Text, leftJoin, tablas, "", 0, "", 0, "", "", 6)
                Response.Redirect("Grafica.aspx", True)
            End If
        Else
            If Condiciones = 0 Then
                showModal(RadNotification1, "deny", "Error", "Debes De Seleccionar Al Menos Un Campo Para Condición")
                listBoxCamposSeleccionados.Focus()
            Else
                Session("ID") = ReporteDetalle(0, Session("Nuevo"), "", "", "", 0, "", 0, "", "", 8)
                Dim Orden As Integer = 1
                Dim uniquetablas As List(Of String) = New List(Of String)
                For Each item As RadListBoxItem In listBoxCamposSeleccionados.Items
                    Dim itemData As IDictionary(Of String, String) = ObtenerDatoslistBoxCamposSeleccionados(item)
                    ReporteDetalle(Session("ID"), TxtCAT_RED_NOMBRE.Text, TxtCAT_RED_DESCRIPCION.Text, itemData("Tabla"), itemData("CampoDb"), itemData("Condicional"), itemData("Formato"), Orden, itemData("TipoDisplay"), DdlCAT_RED_BLOQUE.SelectedValue, 2)
                    Orden += 1
                    If itemData("Formato") = "Contar" Then
                        Session("Agrupar") = itemData("CampoDisplay")
                    End If
                    If Not uniquetablas.Contains(itemData("Tabla")) Then
                        uniquetablas.Add(itemData("Tabla"))
                    End If
                Next
                Dim tablas As String = "'" & String.Join("','", uniquetablas) & "'"
                Dim leftJoin As String = IIf(switch1.Checked, "1", "0")
                ReporteDetalle(Session("ID"), TxtCAT_RED_NOMBRE.Text, leftJoin, tablas, "", 0, "", 0, "", "", 6)
                Response.Redirect("Grafica.aspx", True)
            End If
        End If
    End Sub
    Private Sub baja()
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "godown", "gotoEndPage();", True)
    End Sub
End Class

