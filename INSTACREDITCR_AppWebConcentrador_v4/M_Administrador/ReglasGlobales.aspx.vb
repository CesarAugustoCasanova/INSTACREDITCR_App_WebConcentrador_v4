Imports System.Web.Services
Imports Telerik.Web.UI
Imports System.Data
Imports Funciones
Imports Class_Cat_ReglasGlobal

Partial Class ReglasGlobales
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
        If Not IsPostBack Then
            ddlReglaGlobal()
            limpiar_gridAsig()
            limpiar_panel()
        End If
    End Sub

    Protected Sub ddlReglaGlobal()
        Funciones.LLENAR_DROP2(34, " ", DDLRegla, "V_VALOR", "T_VALOR")
    End Sub

    Protected Sub limpiar_gridAsig()
        Dim tabla As DataTable = Nothing
        Session("asignacion") = Nothing
        gridAsignacion.Visible = False
    End Sub

    Protected Sub limpiar_panel()
        PnlDatos.Visible = False
        btnSimularCamp.Visible = False
    End Sub

    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Administrador", "ReglasGlobales.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub
    Protected Sub Aviso(ByVal MSJ As String)
        RadAviso.RadAlert(MSJ, 440, 155, "AVISO", Nothing)
    End Sub
    Private Sub gridReglasGlob_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles gridReglasGlob.NeedDataSource
        Dim dato As String = DDLRegla.SelectedValue
        gridReglasGlob.DataSource = TraeRegla(DDLRegla.SelectedValue)
    End Sub

    Private Sub gridReglasGlob_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles gridReglasGlob.ItemCommand
        Dim comando As String = e.CommandName
        If comando = "PerformInsert" Then
            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)

            Dim valores As Hashtable = Reglas.getGridValues(MyUserControl)

            Dim v_idRegla As String = DDLRegla.SelectedValue
            Dim v_NomRegla As String = DDLRegla.SelectedItem.Text.ToString

            InsertarParametro(v_idRegla, v_NomRegla, valores("operadorText"), valores("conectorText"), valores("tablaText"), valores("campoText"), valores("valor"), valores("campoValue"), valores("tablaValue"), valores("operadorValue"), valores("conectorValue"), valores("tipo"))
            gridReglasGlob.Rebind()
            Aviso("Regla actualizada")
        ElseIf comando = "Edit" Then
            Session("Edit") = True
        ElseIf comando = "Update" Then
            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)

            Dim valores As Hashtable = Reglas.getGridValues(MyUserControl)
            Dim v_idRegla As String = DDLRegla.SelectedValue
            Dim v_NomRegla As String = DDLRegla.SelectedItem.Text.ToString

            ActualizarParametro(v_idRegla, v_NomRegla, valores("operadorText"), valores("conectorText"), valores("tablaText"), valores("campoText"), valores("valor"), valores("campoValue"), valores("tablaValue"), valores("operadorValue"), valores("conectorValue"), valores("tipo"), valores("consecutivo"))

            gridReglasGlob.Rebind()
            Aviso("Regla Actualizada")
        ElseIf comando = "onDelete" Then
            Dim valores(7) As String
            valores(0) = DDLRegla.SelectedValue
            'valores(1) = DDLRespGestion.SelectedValue
            valores(2) = e.Item.Cells.Item(4).Text
            valores(3) = e.Item.Cells.Item(5).Text
            valores(4) = e.Item.Cells.Item(6).Text
            valores(5) = e.Item.Cells.Item(7).Text
            valores(6) = e.Item.Cells.Item(8).Text
            BorrarParametro(valores(0), valores(4), valores(6), valores(2), valores(3), valores(5))
            gridReglasGlob.Rebind()
            Aviso("Regla actualizada")
        End If
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
        Dim v_cadena As String = ""
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


    Private Sub btnCancelarBorrar_Click(sender As Object, e As EventArgs) Handles btnCancelarBorrar.Click
        ScriptManager.RegisterStartupScript(Me, GetType(Page), "modal", "<script>document.getElementById('modalDelete').style.display='none'</script>", False)
    End Sub

    Private Sub btnSimularCamp_Click(sender As Object, e As EventArgs) Handles btnSimularCamp.Click
        Try

            Dim data As DataTable = TraeRegla(DDLRegla.SelectedValue)

            If data.Rows.Count = 0 Then
                Aviso("No hay reglas para una simulación")
                limpiar_gridAsig()
            Else
                Dim tabla As DataTable = SimularRegla(DDLRegla.SelectedValue, tmpUSUARIO("CAT_LO_INSTANCIA"))

                If tabla.Rows.Count = 0 Then
                    Session("asignacion") = Nothing
                    Aviso("No se encontraron coincidencias")
                    limpiar_gridAsig()
                Else
                    Session("asignacion") = tabla
                    Aviso("Simulacion terminada, se encontraron " & tabla.Rows.Count & " registro(s)")
                    gridAsignacion.Visible = True
                    gridAsignacion.Rebind()
                End If
            End If



            'btnSimularCamp.Enabled = False
            'gridAsignacion.MasterTableView.ExportToCSV()
        Catch ex As Exception
            Aviso(ex.Message)
        End Try
    End Sub

    Private Sub gridAsignacion_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles gridAsignacion.NeedDataSource
        gridAsignacion.DataSource = Session("asignacion")
    End Sub
    Protected Sub btnCrearRegla_Click(sender As Object, e As EventArgs) Handles btnCrearRegla.Click
        If RTBNomRegla.Text.ToUpper <> "" Then

            Dim v_id As Integer = ExisteRegla(RTBNomRegla.Text)

            If v_id = 0 Then
                InsertarRegla(RTBNomRegla.Text, tmpUSUARIO("CAT_LO_USUARIO"))
                ddlReglaGlobal()
                PnlDatos.Visible = True

                DDLRegla.SelectedValue = ExisteRegla(RTBNomRegla.Text)
                gridReglasGlob.Rebind()
                RTBNomRegla.Text = ""
                btnSimularCamp.Visible = True
                limpiar_gridAsig()
            Else
                Aviso("La nombre ya esta siendo utilizado por otra regla")
                limpiar_panel()
                gridReglasGlob.Rebind()
                limpiar_gridAsig()
            End If
        Else
            Aviso("Nombre no válido")
            limpiar_panel()
            limpiar_gridAsig()
        End If
    End Sub


    Protected Sub DDLRegla_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles DDLRegla.SelectedIndexChanged
        If DDLRegla.SelectedValue <> "Seleccione" Then
            PnlDatos.Visible = True
            limpiar_gridAsig()
            gridReglasGlob.Rebind()
            btnSimularCamp.Visible = True
        Else
            limpiar_panel()
            limpiar_gridAsig()
            gridReglasGlob.Visible = False

            'btnSimularDispersion.Visible = True
        End If
    End Sub
End Class
