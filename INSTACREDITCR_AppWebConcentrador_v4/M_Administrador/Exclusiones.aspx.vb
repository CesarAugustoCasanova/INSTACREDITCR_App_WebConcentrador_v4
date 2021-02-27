
Imports Telerik.Web.UI
Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports Db
Imports System.Web.SessionState.HttpSessionState
Imports System.Data
Imports Funciones
Imports System.Web.Services

Partial Class _Exclusiones
    Inherits System.Web.UI.Page
    ''' <summary>
    ''' Establece los parametros para mostrar una notificacion
    ''' </summary>
    ''' <param name="Notificacion">Objeto RadNotification de Telerik</param>
    ''' <param name="icono">info - delete - deny - edit - ok - warning - none</param>
    ''' <param name="titulo">título de la notificación</param>
    ''' <param name="msg">mensaje de la notificación</param>
    Public Shared Sub showModal(ByRef Notificacion As Object, ByVal icono As String, ByVal titulo As String, ByVal msg As String)
        Dim radnot As RadNotification = TryCast(Notificacion, RadNotification)
        radnot.TitleIcon = icono
        radnot.ContentIcon = icono
        radnot.Title = titulo
        radnot.Text = msg
        radnot.Show()
    End Sub
    Public Property tmpUSUARIO As IDictionary
        Get
            Return CType(Session("USUARIO"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("USUARIO") = value
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
    Private Sub _Exclusiones_Init(sender As Object, e As EventArgs) Handles Me.Init
        If Not IsPostBack Then
            Dim ds As DataTable = traerNombreExclusiones()

            'initgridExclusiones()
            initProgressGrid()

            'If ds.Rows.Count > 0 Then
            pnlExclusion.Visible = False
                pnlExclusiones.Visible = True
                Session("gridExclusiones") = ds
                gridExclusiones.Rebind()
                'Else
                '    pnlExclusion.Visible = True
                '    pnlExclusiones.Visible = False
                'End If

            End If
    End Sub


    Private Function traerNombreExclusiones() As DataTable
        Dim SSCommandCat As New SqlCommand
        SSCommandCat.CommandText = "SP_CAT_EXCLUSIONES"
        SSCommandCat.CommandType = CommandType.StoredProcedure
        SSCommandCat.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 4
        Return Consulta_Procedure(SSCommandCat, "Catalogo")
    End Function

    Sub initProgressGrid()
        Dim dt As DataTable = New DataTable()
        Dim campos(13) As String
        'Informacion tabla
        campos(0) = "Cat_Ta_Desc"
        campos(1) = "Cat_Ta_Tabla"
        campos(2) = "Cat_Ta_Pk"
        'Informacion del campo seleccionado
        campos(3) = "Campo_Nombre"
        campos(4) = "Campo"
        campos(5) = "Tabla"
        campos(6) = "Tipo"
        campos(7) = "Nombre"
        'informacion de la condicion
        campos(8) = "CondicionValue"
        campos(9) = "CondicionText"
        'informacion del valor
        campos(10) = "Valor"
        'Informacion del conector
        campos(11) = "ConectorValue"
        campos(12) = "ConectorText"
        For Each campo As String In campos
            Dim dc As DataColumn = New DataColumn(campo)
            dt.Columns.Add(dc)
        Next
        Session("progressGrid") = dt
    End Sub
    <WebMethod()>
    Public Shared Function GetTablas() As RadListBoxItemData()
        dim SSCommand as new sqlcommand
        SSCommand.CommandText = "SP_CAT_EXCLUSIONES"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_bandera", SqlDbType.Decimal).Value = 1

        Dim ds As DataTable = Consulta_Procedure(SSCommand, "Tablas")

        Dim data As DataTable = ds

        Dim result As New List(Of RadListBoxItemData)()
        For Each tabla In data.Rows
            Dim itemData As New RadListBoxItemData()
            itemData.Text = tabla("Cat_Ta_Desc").ToString()
            itemData.Attributes.Add("Cat_Ta_Tabla", tabla("Cat_Ta_Tabla").ToString())
            itemData.Attributes.Add("Cat_Ta_Pk", tabla("Cat_Ta_Pk").ToString())
            result.Add(itemData)
        Next

        Return result.ToArray()
    End Function
    Private Function traeCampos() As DataTable
        lbCampos.Items.Clear()
        Dim valor As String
        If lbTablas.SelectedItem Is Nothing Then
            valor = ""
        Else
            valor = lbTablas.SelectedItem.Text
        End If
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_CAT_EXCLUSIONES"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_bandera", SqlDbType.Decimal).Value = 2
        SSCommand.Parameters.Add("@V_Valor", SqlDbType.NVarChar).Value = valor

        Dim ds As DataTable = Consulta_Procedure(SSCommand, "Tablas")
        Return ds
    End Function

    Sub ValidaCatalogo(ByVal V_Campo As String, ByVal Operador As RadDropDownList, ByVal Valores As RadTextBox)
        Try
            'Dim SSCommandCat As New SqlCommand
            'SSCommandCat.CommandText = "SP_VALIDA_CATALOGO_EXCLUSIONES"
            'SSCommandCat.CommandType = CommandType.StoredProcedure
            'SSCommandCat.Parameters.Add("@V_Campo", SqlDbType.NVarChar).Value = V_Campo
            'SSCommandCat.Parameters.Add("@cv_1", OracleType.Cursor).Direction = ParameterDirection.Output
            'Dim DtsObjetos As datatable = Consulta_Procedure(SSCommandCat, "Catalogo")
            'Operador.Items.Clear()
            'If DtsObjetos.Rows(0).Item("CAMPO") <> "1" Then
            '    Operador.Items.Add(New DropDownListItem("Que Contenga", "In"))
            '    Operador.Items.Add(New DropDownListItem("Que No Contenga", "Not In"))
            '    Valores.ReadOnly = True
            'Else
            '    Operador.Items.Add(New DropDownListItem("Mayor Que", ">"))
            '    Operador.Items.Add(New DropDownListItem("Menor Que", "<"))
            '    Operador.Items.Add(New DropDownListItem("Igual", "="))
            '    Operador.Items.Add(New DropDownListItem("Mayor O Igual", ">="))
            '    Operador.Items.Add(New DropDownListItem("Menor O Igual", "<="))
            '    Operador.Items.Add(New DropDownListItem("Distinto", "!="))
            '    Operador.Items.Add(New DropDownListItem("Que Contenga", "In"))
            '    Operador.Items.Add(New DropDownListItem("Que No Contenga", "Not In"))
            '    Valores.ReadOnly = False
            'End If

        Catch ex As Exception
            'mostrar mensaje
        End Try
    End Sub

    Private Sub lbTablas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbTablas.SelectedIndexChanged
        pnlDatosCampo.Visible = False
        lbCampos.Visible = True
        lbCampos.DataBind()
    End Sub

    Private Sub lbCampos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbCampos.SelectedIndexChanged
        pnlDatosCampo.Visible = True
        lblCampo.Text = CType(lbCampos.SelectedItem.FindControl("Campo_Nombre"), RadLabel).Text.Split(",")(0)
        Dim campoReal As String = CType(lbCampos.SelectedItem.FindControl("Campo"), RadLabel).Text
        Dim tipo As String = CType(lbCampos.SelectedItem.FindControl("Tipo"), RadLabel).Text
        If tipo.ToUpper = "DATE" Then
            TxtValores.Text = ""
            TxtValores.Visible = False
            DteValores.Clear()
            DteValores.Visible = True
            NumValores.Text = ""
            NumValores.Visible = False
        ElseIf tipo.ToUpper = "NUMBER" Then
            DteValores.Clear()
            DteValores.Visible = False
            TxtValores.Text = ""
            TxtValores.Visible = False
            NumValores.Text = ""
            NumValores.Visible = True
        Else
            DteValores.Clear()
            DteValores.Visible = False
            TxtValores.Visible = True
            TxtValores.Text = ""
            NumValores.Text = ""
            NumValores.Visible = False
        End If
    End Sub

    Private Sub lbCampos_DataBinding(sender As Object, e As EventArgs) Handles lbCampos.DataBinding
        lbCampos.DataSource = traeCampos()
    End Sub

    Private Sub btnAnadir_Click(sender As Object, e As EventArgs) Handles btnAnadir.Click
        If validaAddDatos() Then

            'Dos valores siempre irán vacios
            Dim valor As String = TxtValores.Text & DteValores.DbSelectedDate & NumValores.Text

            Dim dt As DataTable = CType(Session("progressGrid"), DataTable)
            Dim dr As DataRow = dt.NewRow()

            dr("Cat_Ta_Desc") = lbTablas.SelectedItem.Text
            dr("Cat_Ta_Tabla") = lbTablas.SelectedItem.Attributes("Cat_Ta_Tabla")
            dr("Cat_Ta_Pk") = lbTablas.SelectedItem.Attributes("Cat_Ta_Pk")
            dr("Campo_Nombre") = CType(lbCampos.SelectedItem.FindControl("Campo_Nombre"), RadLabel).Text
            dr("Campo") = CType(lbCampos.SelectedItem.FindControl("Campo"), RadLabel).Text
            dr("Tabla") = CType(lbCampos.SelectedItem.FindControl("Tabla"), RadLabel).Text
            dr("Tipo") = CType(lbCampos.SelectedItem.FindControl("Tipo"), RadLabel).Text
            dr("Nombre") = CType(lbCampos.SelectedItem.FindControl("Nombre"), RadLabel).Text
            dr("CondicionValue") = DdlOperador.SelectedValue
            dr("CondicionText") = DdlOperador.SelectedText
            dr("Valor") = valor
            dr("ConectorValue") = DdlConector.SelectedValue
            dr("ConectorText") = DdlConector.SelectedText

            dt.Rows.Add(dr)

            Session("progressGrid") = dt

            progressGrid.Rebind()
        End If

        lbTablas.SelectedIndex = -1
        lbCampos.Visible = False
        pnlDatosCampo.Visible = False

    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        lbCampos.Visible = False
        pnlDatosCampo.Visible = False
    End Sub

    Private Function validaAddDatos() As String
        If txtNombreExclusion.Text = "" Then
            showModal(RadNotification1, "warning", "Error", "Escribe el nombre de la exclusión antes de continuar")
            Return False
        ElseIf TxtValores.Text = "" And DteValores.DbSelectedDate = "" And NumValores.Text = "" Then
            showModal(RadNotification1, "warning", "Error", "Pon un valor para la condición")
            Return False
        ElseIf DdlOperador.SelectedText = "" Then
            showModal(RadNotification1, "warning", "Error", "Selecciona un operador antes de continuar")
            Return False
        End If
        Return True
    End Function

    Private Function validaExclusionDatos() As String
        Dim dt As DataTable = CType(Session("progressGrid"), DataTable)
        If txtNombreExclusion.Text = "" Then
            showModal(RadNotification1, "warning", "Error", "Introduce nombre de la exclusión")
            Return False
        ElseIf dt.Rows.Count = 0 Then
            showModal(RadNotification1, "warning", "Error", "Exclusion inválida.")
            Return False
        ElseIf txtMotivo.Text.Length < 10 Then
            showModal(RadNotification1, "warning", "Error", "Motivo de exclusión inválido.")
            Return False
        ElseIf dpVigencia.SelectedDate Is Nothing And cbVigencia.Checked And cbActivo.Checked Then
            showModal(RadNotification1, "warning", "Error", "Fecha de vigencia inválida")
            Return False
        ElseIf dpVigencia.SelectedDate < Now Then
            showModal(RadNotification1, "warning", "Error", "Fecha de vigencia inválida")
            Return False
        End If
        Return True
    End Function

    Private Sub progressGrid_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles progressGrid.NeedDataSource
        progressGrid.DataSource = CType(Session("progressGrid"), DataTable)
    End Sub

    Private Sub progressGrid_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles progressGrid.ItemCommand
        If e.CommandName = "onDelete" Then
            Dim dt As DataTable = CType(Session("progressGrid"), DataTable)
            dt.Rows.RemoveAt(e.Item.ItemIndex)
            Session("progressGrid") = dt
            progressGrid.Rebind()
        End If
    End Sub

    Private Sub btnGuardarExclusion_Click(sender As Object, e As EventArgs) Handles btnGuardarExclusion.Click
        Dim dt As DataTable = CType(Session("progressGrid"), DataTable)
        If validaExclusionDatos() Then

            If Session("Actualizar") Then
                borrarExclusion(Session("ExclusionID"), txtNombreExclusion.Text)
            End If
            For Each row As DataRow In dt.Rows
                Dim SSCommand As New SqlCommand
                SSCommand.CommandText = "SP_ADD_WHERE_EXCLUSIONES"
                SSCommand.CommandType = CommandType.StoredProcedure
                SSCommand.Parameters.Add("@v_bandera", SqlDbType.Decimal).Value = 1
                SSCommand.Parameters.Add("@V_Cat_WEX_ID", SqlDbType.NVarChar).Value = Session("ExclusionID")
                SSCommand.Parameters.Add("@V_Cat_WEX_Usuario", SqlDbType.NVarChar).Value = tmpUSUARIO("CAT_LO_USUARIO")
                SSCommand.Parameters.Add("@V_Cat_WEX_EXCLUSION", SqlDbType.NVarChar).Value = txtNombreExclusion.Text
                SSCommand.Parameters.Add("@V_Cat_WEX_Campo", SqlDbType.NVarChar).Value = row("Campo")
                SSCommand.Parameters.Add("@V_CAT_WEX_TABLA", SqlDbType.NVarChar).Value = row("Cat_Ta_Tabla")
                SSCommand.Parameters.Add("@V_Cat_WEX_Operador", SqlDbType.NVarChar).Value = row("CondicionValue")
                SSCommand.Parameters.Add("@V_Cat_WEX_valor", SqlDbType.NVarChar).Value = row("Valor")
                SSCommand.Parameters.Add("@V_Cat_WEX_Conector", SqlDbType.NVarChar).Value = row("ConectorValue")
                SSCommand.Parameters.Add("@V_CAT_WEX_DESCRIPCIONCAMPO", SqlDbType.NVarChar).Value = row("Campo_Nombre")
                SSCommand.Parameters.Add("@V_CAT_WEX_DESCRIPCIONTABLA", SqlDbType.NVarChar).Value = row("Cat_Ta_Desc")
                SSCommand.Parameters.Add("@V_CAT_WEX_DESCRIPCIONCONECTOR", SqlDbType.NVarChar).Value = row("ConectorText")
                SSCommand.Parameters.Add("@v_CAT_WEX_ACTIVA", SqlDbType.NVarChar).Value = IIf(cbActivo.Checked, 1, 0)
                SSCommand.Parameters.Add("@v_CAT_WEX_MOTIVO", SqlDbType.NVarChar).Value = txtMotivo.Text
                If cbActivo.Checked Then
                    SSCommand.Parameters.Add("@v_CAT_WEX_PERMANENTE", SqlDbType.NVarChar).Value = IIf(cbVigencia.Checked, 1, 0)
                End If
                If cbVigencia.Enabled Then
                    SSCommand.Parameters.Add("@v_CAT_WEX_DTEVIGENCIA", SqlDbType.NVarChar).Value = dpVigencia.DbSelectedDate
                End If

                Dim ds As DataTable = Consulta_Procedure(SSCommand, "Tablas")
            Next
            pnlExclusion.Visible = False
            pnlExclusiones.Visible = True
            Session("gridExclusiones") = traerNombreExclusiones()
            gridExclusiones.Rebind()
            limpiar()
            showModal(RadNotification1, "ok", "OK", "Guardado correctamente")
        End If
    End Sub

    Private Sub gridExclusiones_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles gridExclusiones.NeedDataSource

        gridExclusiones.DataSource = CType(Session("gridExclusiones"), DataTable)

    End Sub

    Private Sub btnAddExclusion_Click(sender As Object, e As EventArgs) Handles btnAddExclusion.Click
        Session("Actualizar") = False
        Dim SSCommandid As New SqlCommand
        SSCommandid.CommandText = "SP_CAT_EXCLUSIONES"
        SSCommandid.CommandType = CommandType.StoredProcedure
        SSCommandid.Parameters.Add("@v_bandera", SqlDbType.Decimal).Value = 3
        Dim ds1 As DataTable = Consulta_Procedure(SSCommandid, "Tablas")
        Session("ExclusionID") = ds1.Rows(0)(0)
        pnlExclusion.Visible = True
        pnlExclusiones.Visible = False

        txtNombreExclusion.Text = ""
        txtNombreExclusion.ReadOnly = False
        initProgressGrid()
        progressGrid.Rebind()
    End Sub

    Private Sub btnCancelarExclusion_Click(sender As Object, e As EventArgs) Handles btnCancelarExclusion.Click
        pnlExclusion.Visible = False
        pnlExclusiones.Visible = True
        limpiar()
    End Sub

    Private Sub gridExclusiones_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles gridExclusiones.ItemCommand
        Dim item As GridDataItem = CType(e.Item, GridDataItem)
        Dim idCell As GridTableCell = CType(item.Controls(3), GridTableCell)
        Dim exclusionCell As GridTableCell = CType(item.Controls(4), GridTableCell)
        If e.CommandName = "onSelected" Then
            cargaExclusion(idCell.Text, exclusionCell.Text)
            pnlExclusion.Visible = True
            pnlExclusiones.Visible = False
            txtNombreExclusion.Text = exclusionCell.Text
            txtNombreExclusion.ReadOnly = True
        ElseIf e.CommandName = "onDelete" Then
            borrarExclusion(idCell.Text, exclusionCell.Text)
            Dim dt As DataTable = CType(Session("gridExclusiones"), DataTable)
            dt.Rows.RemoveAt(item.ItemIndex)
            Session("gridExclusiones") = dt
            gridExclusiones.Rebind()
        End If
    End Sub

    Private Sub cargaExclusion(id As String, nombreExclusion As String)
        Session("Actualizar") = True
        Session("ExclusionID") = id

        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_ADD_WHERE_EXCLUSIONES"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_bandera", SqlDbType.Decimal).Value = 2
        SSCommand.Parameters.Add("@V_Cat_WEX_EXCLUSION", SqlDbType.NVarChar).Value = nombreExclusion
        SSCommand.Parameters.Add("@V_Cat_WEX_ID", SqlDbType.NVarChar).Value = id

        Dim ds As DataTable = Consulta_Procedure(SSCommand, "Tablas")

        initProgressGrid()
        Dim dt As DataTable = CType(Session("progressGrid"), DataTable)
        For Each row As DataRow In ds.Rows
            Dim dr As DataRow = dt.NewRow()

            dr("Cat_Ta_Desc") = row("CAT_WEX_DESCRIPCIONTABLA")
            dr("Cat_Ta_Tabla") = row("CAT_WEX_TABLA")
            dr("Cat_Ta_Pk") = ""
            dr("Campo_Nombre") = row("CAT_WEX_DESCRIPCIONCAMPO")
            dr("Campo") = row("CAT_WEX_CAMPO")
            dr("Tabla") = ""
            dr("Tipo") = ""
            dr("Nombre") = ""
            dr("CondicionValue") = row("CAT_WEX_OPERADOR")
            dr("CondicionText") = row("CAT_WEX_DESCRIPCIONOPERADOR")
            dr("Valor") = row("CAT_WEX_VALOR")
            dr("ConectorValue") = row("CAT_WEX_CONECTOR")
            dr("ConectorText") = row("CAT_WEX_DESCRIPCIONCONECTOR")

            dt.Rows.Add(dr)

        Next
        Try
            txtMotivo.Text = ds.Rows(0)("CAT_WEX_MOTIVO")
        Catch ex As Exception
            txtMotivo.Text = ""
        End Try
        cbActivo.Checked = IIf(ds.Rows(0)("CAT_WEX_ACTIVA") = 1, True, False)
        If cbActivo.Checked Then
            cbVigencia.Checked = IIf(ds.Rows(0)("CAT_WEX_PERMANENTE") = 1, True, False)
            If cbVigencia.Checked Then
                dpVigencia.DbSelectedDate = ds.Rows(0)("CAT_WEX_DTEVIGENCIA")
                dpVigencia.Enabled = True
                dpVigencia.DateInput.EmptyMessage = "Seleccione"
            Else
                dpVigencia.Enabled = False
                dpVigencia.DateInput.EmptyMessage = "No aplica"
            End If
        Else
            cbVigencia.Enabled = False
            dpVigencia.Enabled = False
            dpVigencia.DateInput.EmptyMessage = "No aplica"
        End If
        Session("progressGrid") = dt
        progressGrid.Rebind()
    End Sub

    Private Sub borrarExclusion(id As String, nombreExclusion As String)
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_ADD_WHERE_EXCLUSIONES"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_bandera", SqlDbType.Decimal).Value = 0
        SSCommand.Parameters.Add("@V_Cat_WEX_EXCLUSION", SqlDbType.NVarChar).Value = nombreExclusion
        SSCommand.Parameters.Add("@V_Cat_WEX_ID", SqlDbType.NVarChar).Value = id

        Dim ds As DataTable = Consulta_Procedure(SSCommand, "Tablas")
        showModal(RadNotification1, "ok", "OK", "Eliminada correctamente")
    End Sub

    Private Sub cbVigencia_CheckedChanged(sender As Object, e As EventArgs) Handles cbVigencia.CheckedChanged
        If cbVigencia.Checked Then
            dpVigencia.Enabled = True
            dpVigencia.DateInput.EmptyMessage = "Seleccione"
            dpVigencia.MinDate = Today
        Else
            dpVigencia.Enabled = False
            dpVigencia.DateInput.EmptyMessage = "No aplica"
        End If
    End Sub

    Private Sub cbActivo_CheckedChanged(sender As Object, e As EventArgs) Handles cbActivo.CheckedChanged
        If cbActivo.Checked Then
            cbVigencia.Enabled = True
            dpVigencia.Enabled = True
            dpVigencia.DateInput.EmptyMessage = "Seleccione"
        Else
            cbVigencia.Enabled = False
            dpVigencia.Enabled = False
            dpVigencia.DateInput.EmptyMessage = "No aplica"
        End If
    End Sub

    Private Sub limpiar()
        initProgressGrid()
        lbCampos.Items.Clear()
        lbCampos.Visible = False
        txtMotivo.Text = ""
        TxtValores.Text = ""
        DdlConector.ClearSelection()
        DdlOperador.ClearSelection()
        cbActivo.Checked = True
        cbVigencia.Enabled = True
        cbVigencia.Checked = True
        dpVigencia.Enabled = True
        dpVigencia.DbSelectedDate = Nothing
        dpVigencia.DateInput.EmptyMessage = "Seleccione"
    End Sub
End Class
