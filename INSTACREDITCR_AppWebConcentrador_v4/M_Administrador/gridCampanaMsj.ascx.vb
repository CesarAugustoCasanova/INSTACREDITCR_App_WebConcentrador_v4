Imports System.Data.SqlClient
Imports Db
Imports System.Web.SessionState.HttpSessionState
Imports System.Data
Imports Telerik.Web.UI

Partial Class M_Administrador_gridCampanaMsj
    Inherits System.Web.UI.UserControl
    Private _dataItem As Object = Nothing
    Public Property DataItem() As Object
        Get
            Return Me._dataItem
        End Get
        Set(ByVal value As Object)
            Me._dataItem = value
        End Set
    End Property

    Private Sub M_Administrador_gridCampanaMsj_Load(sender As Object, e As EventArgs) Handles Me.Load
        TxtSimulacion.MinDate = Now
        If Session("Edit") Then
            llenaComboProveedor()
            llenaComboReglasGlobales()
            llenarCampos(DataItem("Campana"))
        ElseIf Session("InitInsert") Then
            llenaComboProveedor()
            llenaComboReglasGlobales()
        End If
    End Sub

    Private Sub M_Administrador_gridCampanaMsj_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        If Session("Edit") Then
            Session.Remove("Edit")
        ElseIf Session("InitInsert") Then
            Session.Remove("InitInsert")
        End If
    End Sub
    Public Sub llenaComboTipoCampana()

        If comboProveedor.SelectedValue = "CALIXTA" Then
            comboTipoCampana.Items.Clear()
            For Each res As DataRow In informacionCombo(1, comboProveedor.SelectedValue).Rows
                Dim item As RadComboBoxItem = New RadComboBoxItem
                item.Text = res("texto")
                item.Value = res("texto")
                comboTipoCampana.Items.Add(item)
            Next
            comboTipoCampana.DataBind()
        ElseIf comboProveedor.SelectedValue = "INCONCERT" Then
            comboTipoCampanaI.Items.Clear()
            For Each res As DataRow In informacionCombo(1, comboProveedor.SelectedValue).Rows
                Dim item As RadComboBoxItem = New RadComboBoxItem
                item.Text = res("texto")
                item.Value = res("texto")
                comboTipoCampanaI.Items.Add(item)
            Next
            comboTipoCampanaI.DataBind()
        ElseIf comboProveedor.SelectedValue = "LSC Communications" Then
            comboTipoCampanaI.Items.Clear()
            For Each res As DataRow In informacionCombo(1, comboProveedor.SelectedValue).Rows
                Dim item As RadComboBoxItem = New RadComboBoxItem
                item.Text = res("texto")
                item.Value = res("texto")
                comboTipoCampanaI.Items.Add(item)
            Next
            comboTipoCampanaI.DataBind()
        End If
    End Sub
    Public Sub llenaComboReglasGlobales()
        comboReglasGlobales.Items.Clear()
        For Each res As DataRow In informacionCombo(2, comboProveedor.SelectedValue).Rows
            Dim item As RadComboBoxItem = New RadComboBoxItem
            item.Text = res("texto")
            item.Value = res("valor")
            comboReglasGlobales.Items.Add(item)
        Next
        comboReglasGlobales.DataBind()
        comboTipoCampanaI.Items.Clear()
        For Each resI As DataRow In informacionCombo(2, comboProveedor.SelectedValue).Rows
            Dim itemI As RadComboBoxItem = New RadComboBoxItem
            itemI.Text = resI("texto")
            itemI.Value = resI("valor")
            comboReglasGlobalesI.Items.Add(itemI)
        Next
        comboReglasGlobalesI.DataBind()

        comboReglasGlobalesCarteo.Items.Clear()
        For Each resI As DataRow In informacionCombo(2, comboProveedor.SelectedValue).Rows
            Dim itemI As RadComboBoxItem = New RadComboBoxItem
            itemI.Text = resI("texto")
            itemI.Value = resI("valor")
            comboReglasGlobalesCarteo.Items.Add(itemI)
        Next
        comboReglasGlobalesCarteo.DataBind()

    End Sub
    Public Sub llenaComboProveedor()
        comboProveedor.Items.Clear()
        comboProveedor.ClearSelection()
        For Each res As DataRow In informacionCombo(3).Rows
            Dim item As RadComboBoxItem = New RadComboBoxItem
            item.Text = res("texto")
            item.Value = res("texto")
            comboProveedor.Items.Add(item)
            'comboProveedor.SelectedValue = item.Value
        Next
    End Sub
    Private Sub llenarCampos(nombre As String)
        Dim SSCommandCat As New SqlCommand
        SSCommandCat.CommandText = "SP_CAMPANA_MSJ"
        SSCommandCat.CommandType = CommandType.StoredProcedure
        SSCommandCat.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 8
        SSCommandCat.Parameters.Add("@v_nombre", SqlDbType.NVarChar).Value = nombre
        Dim row As DataRow = Consulta_Procedure(SSCommandCat, "Catalogo")(0)
        txtNombre.Text = nombre
        TableInconcert.Visible = False
        TableCalixta.Visible = False
        TableLSC.Visible = False
        If DataItem("proveedor") = "CALIXTA" Then
            TableCalixta.Visible = True
            DIVTipoTEL.Visible = False
            DIVRol.Visible = False
            ' RDDLTipoMsj.Items.Clear()
            If DataItem("Tipo") = "SMS" Then
                DIVTipoTEL.Visible = True
                llenarTipoTel()
                DIVRol.Visible = True
                ' RDDLTipoMsj.DataSource = Valores_Seleccion()
                ' RDDLTipoMsj.DataValueField = "Value"
                ' RDDLTipoMsj.DataTextField = "Text"
                For Each res As DataRow In Llenar(0, "", "", "", "", 9).Rows
                    Dim item As RadComboBoxItem = New RadComboBoxItem
                    item.Text = res("Nombre")
                    item.Value = "P" 'Indica que es una plantilla
                    RDDLTipoMsj.Items.Add(item)
                Next
                'RDDLTipoMsj.DataBind()
            ElseIf DataItem("Tipo") = "BLASTER" Then
                DIVRol.Visible = True
                DIVTipoTEL.Visible = True
                llenarTipoTel()
                ' RDDLTipoMsj.DataSource = Valores_Seleccion()
                ' RDDLTipoMsj.DataValueField = "Value"
                ' RDDLTipoMsj.DataTextField = "Text"
                For Each res As DataRow In Llenar(0, "", "", "", "", 9).Rows
                    Dim item As RadComboBoxItem = New RadComboBoxItem
                    item.Text = res("Nombre")
                    item.Value = "P" 'Indica que es una plantilla
                    RDDLTipoMsj.Items.Add(item)
                Next
                ' RDDLTipoMsj.DataBind()
            ElseIf DataItem("Tipo") = "CORREO" Then
                DIVRol.Visible = False
                llenarTipoCorreo()
                DIVTipoTEL.Visible = True
                For Each res As DataRow In LlenarCorreo(4).Rows
                    Dim item As RadComboBoxItem = New RadComboBoxItem
                    item.Text = res("Nombre")
                    item.Value = "P" 'Indica que es una plantilla
                    RDDLTipoMsj.Items.Add(item)
                Next
                ' RDDLTipoMsj.DataBind()
            End If

            Try
                RDDLTipoMsj.FindItemByText(row("HIST_CM_PLANTILLA")).Selected = True
            Catch ex As Exception
                Dim errors As String = ex.ToString
            End Try

            Try
                RDDLTipoTel.FindItemByValue(row("HIST_CM_TIPOTEL")).Selected = True
            Catch ex As Exception
                Dim errors As String = ex.ToString
            End Try

            txtNombre.ReadOnly = True
            Try
                txtVigencia.SelectedDate = CType(row("HIST_CM_VIGENCIA"), Date)
            Catch ex As Exception

            End Try
            Try
                RCBPrefijo.FindItemByValue(row("HIST_CM_PREFIJO")).Selected = True
            Catch ex As Exception

            End Try

            Try
                RCBRol.FindItemByValue(row("HIST_CM_ROL")).Selected = True
            Catch ex As Exception

            End Try
            Try
                comboReglasGlobales.FindItemByValue(row("HIST_CM_REGLASGLOBALES")).Selected = True
            Catch ex As Exception
            End Try
            comboProveedor.FindItemByValue(row("HIST_CM_PROVEEDOR").ToString).Selected = True
            llenaComboTipoCampana()
            comboTipoCampana.FindItemByValue(row("HIST_CM_TIPO")).Selected = True
            comboProgramacion.FindItemByValue(row("HIST_CM_TP_PROGRAM")).Selected = True
            If comboProgramacion.SelectedValue = "A" Then
                dteProgramacion.Enabled = True
                dteProgramacion.DateInput.EmptyMessage = "Seleccione..."
                dteProgramacion.MinDate = Now
                DIVFECHA.Visible = True
                DIVVIGENCIA.Visible = True
            Else
                DIVFECHA.Visible = False
                DIVVIGENCIA.Visible = False
                dteProgramacion.Enabled = False
                dteProgramacion.DateInput.EmptyMessage = "No aplica"
            End If

            'Tipo Camapaña se llena en el PreRender
            Session("Tipo") = row("HIST_CM_TIPO").ToString
            Try
                ' RDDLTipoMsj.FindItemByText(row("HIST_CM_PLANTILLA").ToString).Selected = True
                txtMensaje.Enabled = (RDDLTipoMsj.SelectedValue = "M")
                txtMensaje.Text = row("HIST_CM_MENSAJE").ToString
            Catch ex As Exception
                txtMensaje.Enabled = False

            End Try

        ElseIf DataItem("proveedor") = "INCONCERT" Then
            TableInconcert.Visible = True
            comboProveedor.FindItemByValue(row("HIST_CM_PROVEEDOR").ToString).Selected = True
            comboTipoCampanaI.Items.Clear()
            For Each res As DataRow In informacionCombo(1, comboProveedor.SelectedValue).Rows
                Dim item As RadComboBoxItem = New RadComboBoxItem
                item.Text = res("texto")
                item.Value = res("texto")
                comboTipoCampanaI.Items.Add(item)
            Next
            comboTipoCampanaI.DataBind()
            Try
                comboTipoCampanaI.FindItemByValue(row("HIST_CM_TIPO")).Selected = True
            Catch ex As Exception

            End Try
            Try
                comboReglasGlobalesI.FindItemByValue(row("HIST_CM_REGLASGLOBALES")).Selected = True
            Catch ex As Exception
            End Try
        ElseIf DataItem("proveedor") = "LSC Communications" Then
            TableLSC.Visible = True
            TxtMensajeC.Text = row("HIST_CM_MENSAJE").ToString
            comboProveedor.FindItemByValue(row("HIST_CM_PROVEEDOR").ToString).Selected = True
            Try
                comboReglasGlobalesCarteo.FindItemByValue(row("HIST_CM_REGLASGLOBALES")).Selected = True
            Catch ex As Exception
            End Try
        End If

    End Sub
    ''' <summary>
    ''' Trae la informacion necesaria para cada RadCombo
    ''' </summary>
    ''' <param name="flag">1: Tipo de campaña - 2: Reglas globales - 3: Proveedores</param>
    ''' <param name="proveedor">Solo aplica para llenar tipo campaña con base al proveedor seleccionado</param>
    ''' <returns></returns>
    Protected Function informacionCombo(ByVal flag As Integer, Optional ByVal proveedor As String = "") As DataTable
        Dim SSCommandCat As New SqlCommand
        SSCommandCat.CommandText = "SP_CAMPANA_MSJ"
        SSCommandCat.CommandType = CommandType.StoredProcedure
        SSCommandCat.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = flag
        SSCommandCat.Parameters.Add("@v_nombre", SqlDbType.NVarChar).Value = proveedor
        Return Consulta_Procedure(SSCommandCat, "Catalogo")
    End Function
    Protected Sub comboProgramacion_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
        If comboProgramacion.SelectedValue = "A" Then
            dteProgramacion.Enabled = True
            dteProgramacion.DateInput.EmptyMessage = "Seleccione..."
            dteProgramacion.MinDate = Now
            DIVFECHA.Visible = True
            DIVVIGENCIA.Visible = True
        Else
            DIVFECHA.Visible = False
            DIVVIGENCIA.Visible = False
            dteProgramacion.Enabled = False
            dteProgramacion.DateInput.EmptyMessage = "No aplica"
        End If
    End Sub

    Private Sub comboProveedor_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles comboProveedor.SelectedIndexChanged
        TableCalixta.Visible = False
        TableInconcert.Visible = False
        TableLSC.Visible = False
        llenaComboTipoCampana()
        If comboProveedor.SelectedValue = "CALIXTA" Then
            TableCalixta.Visible = True
        ElseIf comboProveedor.SelectedValue = "INCONCERT" Then
            TableInconcert.Visible = True
        ElseIf comboProveedor.SelectedValue = "LSC Communications" Then
            TableLSC.Visible = True
        End If
    End Sub
    Function Valores_Seleccion() As ArrayList
        Dim Valores As ArrayList = New ArrayList()
        Valores.Add(New ListItem("Manual", "M"))
        Return Valores
    End Function

    Private Sub comboTipoCampana_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles comboTipoCampana.SelectedIndexChanged
        TableCalixta.Visible = False
        TableInconcert.Visible = False
        TableLSC.Visible = False
        If comboProveedor.SelectedValue = "CALIXTA" Then
            RDDLTipoMsj.Items.Clear()
            DIVTipoTEL.Visible = False
            DIVRol.Visible = False
            TableCalixta.Visible = True
            If comboTipoCampana.SelectedValue = "SMS" Then
                DIVTipoTEL.Visible = True
                llenarTipoTel()
                DIVRol.Visible = True
                RDDLTipoMsj.DataSource = Valores_Seleccion()
                RDDLTipoMsj.DataValueField = "Value"
                RDDLTipoMsj.DataTextField = "Text"
                RDDLTipoMsj.DataBind()
                For Each res As DataRow In Llenar(0, "", "", "", "", 9).Rows
                    Dim item As RadComboBoxItem = New RadComboBoxItem
                    item.Text = res("Nombre")
                    item.Value = "P" 'Indica que es una plantilla
                    RDDLTipoMsj.Items.Add(item)
                Next
            ElseIf comboTipoCampana.SelectedValue = "BLASTER" Then
                DIVRol.Visible = True
                DIVTipoTEL.Visible = True
                llenarTipoTel()
                RDDLTipoMsj.DataSource = Valores_Seleccion()
                RDDLTipoMsj.DataValueField = "Value"
                RDDLTipoMsj.DataTextField = "Text"
                RDDLTipoMsj.DataBind()
                For Each res As DataRow In Llenar(0, "", "", "", "", 9).Rows
                    Dim item As RadComboBoxItem = New RadComboBoxItem
                    item.Text = res("Nombre")
                    item.Value = "P" 'Indica que es una plantilla
                    RDDLTipoMsj.Items.Add(item)
                Next
            ElseIf comboTipoCampana.SelectedValue = "CORREO" Then
                DIVRol.Visible = False
                llenarTipoCorreo()
                DIVTipoTEL.Visible = True
                For Each res As DataRow In LlenarCorreo(4).Rows
                    Dim item As RadComboBoxItem = New RadComboBoxItem
                    item.Text = res("Nombre")
                    item.Value = "P" 'Indica que es una plantilla
                    RDDLTipoMsj.Items.Add(item)
                Next
            End If
        ElseIf comboProveedor.SelectedValue = "CALIXTA" Then
            TableInconcert.Visible = True
        ElseIf comboProveedor.SelectedValue = "LSC Communications" Then
            TableLSC.Visible = True
        End If
    End Sub
    Private Sub llenarTipoCorreo()
        RDDLTipoTel.Items.Clear()
        RDDLTipoTel.ClearSelection()
        For Each res As DataRow In informacionCombo(13).Rows
            Dim item As RadComboBoxItem = New RadComboBoxItem
            item.Text = res("Valor")
            item.Value = res("Valor")
            RDDLTipoTel.Items.Add(item)
        Next
        RDDLTipoTel.DataBind()
    End Sub

    Private Sub llenarTipoTel()
        RDDLTipoTel.Items.Clear()
        RDDLTipoTel.ClearSelection()
        Dim maxtel As Integer = informacionCombo(9).Rows(0)(0)
        For i = 1 To maxtel
            Dim item As RadComboBoxItem = New RadComboBoxItem
            item.Text = "Teléfono " & i
            item.Value = i
            RDDLTipoTel.Items.Add(item)
        Next
        RDDLTipoTel.DataBind()
    End Sub

    Function Llenar(V_Cat_Sm_Id As Integer, V_Cat_Sm_Descripcion As String, V_Cat_Sm_Valor As String, V_Cat_Sm_Tabla As String, V_Cat_Sm_Camporeal As String, V_Bandera As Integer) As DataTable
        Dim SSCommandAgencias As New SqlCommand
        SSCommandAgencias.CommandText = "Sp_Add_Cat_Etiquetas"
        SSCommandAgencias.CommandType = CommandType.StoredProcedure
        SSCommandAgencias.Parameters.Add("@V_Cat_Sm_Id", SqlDbType.Decimal).Value = V_Cat_Sm_Id
        SSCommandAgencias.Parameters.Add("@V_Cat_Sm_Descripcion", SqlDbType.NVarChar).Value = V_Cat_Sm_Descripcion
        SSCommandAgencias.Parameters.Add("@V_Cat_Sm_Valor", SqlDbType.NVarChar).Value = V_Cat_Sm_Valor
        SSCommandAgencias.Parameters.Add("@V_Cat_Sm_Tabla", SqlDbType.NVarChar).Value = V_Cat_Sm_Tabla
        SSCommandAgencias.Parameters.Add("@V_Cat_Sm_Camporeal", SqlDbType.NVarChar).Value = V_Cat_Sm_Camporeal
        SSCommandAgencias.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = V_Bandera
        Dim DtsVarios As DataTable = Consulta_Procedure(SSCommandAgencias, "Etiqueta")
        Dim DtvVarios As DataView = DtsVarios.DefaultView
        Return DtsVarios
    End Function
    Function LlenarCorreo(V_Bandera As Integer) As DataTable
        Dim SSCommandAgencias As New SqlCommand
        SSCommandAgencias.CommandText = "SP_ADD_CAT_PLANTILLAS_CORREO"
        SSCommandAgencias.CommandType = CommandType.StoredProcedure
        SSCommandAgencias.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = V_Bandera
        Dim DtsVarios As DataTable = Consulta_Procedure(SSCommandAgencias, "SP_ADD_CAT_PLANTILLAS_CORREO")
        Return DtsVarios
    End Function

    Private Sub RDDLTipoMsj_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles RDDLTipoMsj.SelectedIndexChanged
        If e.Value = "M" And comboTipoCampana.SelectedValue <> "CORREO" Then
            txtMensaje.Enabled = True
            txtMensaje.Text = ""
        Else
            If comboTipoCampana.SelectedValue = "SMS" And comboProveedor.SelectedValue = "CALIXTA" Then
                txtMensaje.Enabled = False
                txtMensaje.Text = Llenar(0, "", e.Text, "", "", 14)(0)(0).ToString
            ElseIf comboTipoCampana.SelectedValue = "BLASTER" And comboProveedor.SelectedValue = "CALIXTA" Then
                txtMensaje.Enabled = False
                txtMensaje.Text = Llenar(0, "", e.Text, "", "", 14)(0)(0).ToString
            End If
        End If
        RDDLTipoMsj.FindItemByText(e.Text).Selected = True
    End Sub
End Class
