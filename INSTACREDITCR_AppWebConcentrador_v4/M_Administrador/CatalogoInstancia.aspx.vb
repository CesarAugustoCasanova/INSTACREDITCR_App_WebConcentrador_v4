Imports System.Web.Services
Imports Telerik.Web.UI
Imports System.Data
Imports Funciones
Imports Class_CatalogoInstancia
Imports Spire.Xls
Imports System.IO
Imports System.Data.SqlClient
Imports Db
Partial Class CatalogoInstancia
    Inherits System.Web.UI.Page
    Protected Sub DDLInstancia_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles RDDLInstancia.SelectedIndexChanged
        PnlConfiguracion.Visible = False
        PnlSimular.Visible = False
        PnlExisten.Visible = False
        If RDDLInstancia.SelectedValue <> "Seleccione" Then
            If RDDLInstancia.SelectedValue = "Simular" Then
                PnlSimular.Visible = True
            Else
                PnlExisten.Visible = True
                gridReglas.Rebind()
            End If
            'Else
            '    PnlSimular.Visible = False
            '    PnlExisten.Visible = False
        End If
    End Sub
    Public Property tmpUSUARIO As IDictionary
        Get
            Return CType(Session("USUARIO"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("USUARIO") = value
        End Set
    End Property

    Sub Limpiar()
        RDDLInstancia.SelectedValue = "Seleccione"
        PnlConfigirar.Visible = True
        PnlInicio.Visible = True
        PnlConfiguracion.Visible = False
        PnlExisten.Visible = False
        RTNombre.Text = ""
        RbtnRegresar.Visible = False
        btnSimularInstancias.Visible = False
    End Sub
    Private Sub btnNuevaRegla_Click(sender As Object, e As EventArgs) Handles btnNuevaRegla.Click
        PnlInicio.Visible = False
        btnSimularInstancias.Visible = True
        RbtnRegresar.Visible = True
        PnlConfiguracion.Visible = True
        gridInstancias.Visible = True
        gridInstancias.Rebind()
        Session("NuevaInstancia") = 0
        RTNombre.Text = ""
        RTNombre.Enabled = True
    End Sub
    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Administrador", "CatalogoInstancia.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub
    Protected Sub Aviso(ByVal MSJ As String)
        RadAviso.RadAlert(MSJ, 440, 155, "AVISO", Nothing)
    End Sub
    Private Sub gridInstancias_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles gridInstancias.NeedDataSource
        gridInstancias.DataSource = Instancia(RTNombre.Text, RDDLInstancia.SelectedValue, "", "", "", "", "", "", "", "", "", "", "", 14)
    End Sub

    Private Sub gridDispersion_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles gridInstancias.ItemCommand
        Dim comando As String = e.CommandName
        If comando = "PerformInsert" Then
            If RTNombre.Text.Trim = "" Then
                Aviso("Capture un nombre valido")
            ElseIf Instancia(RTNombre.Text, RDDLInstancia.SelectedValue, "", "", "", "", "", "", "", "", "", "", "", 2).Rows(0).Item("Cuantos") <> "0" And Session("NuevaInstancia") = 0 Then
                Aviso("EL nombre ya existe , intente con otro")
            Else
                RTNombre.Enabled = False
                Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
                Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
                Dim valores As Hashtable = Reglas.getGridValues(MyUserControl)

                Instancia(RTNombre.Text, RDDLInstancia.SelectedValue, "", valores("campoValue"), valores("tablaValue"), valores("tablaText"), valores("operadorValue"), valores("valor"), valores("conectorValue"), valores("campoText"), valores("conectorText"), valores("operadorText"), valores("tipo"), 3)
                Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "CatalogoInstancia", "Se agrego a la regla " & RTNombre.Text & ", de la instancia " & RDDLInstancia.Text & ", los siguientes valores: " & valores(2) & "|" & valores(0) & "|" & valores(1) & "|" & valores(4) & "|" & valores(6) & "|" & valores(7) & "|" & valores(3) & "|" & valores(8) & "|" & valores(5) & "|" & valores("tipo") & "|" & "3")
                gridInstancias.Rebind()
                Session("NuevaInstancia") = 1
                btnSimularInstancias.Visible = True
                Aviso("Asignación actualizada")
            End If
        ElseIf comando = "onDelete" Then
            Instancia(RTNombre.Text, "", "", "", "", "", "", "", "", "", "", "", "", 6, e.Item.Cells.Item(3).Text)
            gridInstancias.Rebind()
            Aviso("Asignación Actualizada")
        ElseIf comando = "Edit" Then
            Session("Edit") = True
        ElseIf comando = "Update" Then
            RTNombre.Enabled = False
            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores As Hashtable = Reglas.getGridValues(MyUserControl)

            Instancia(RTNombre.Text, RDDLInstancia.SelectedValue, "", valores("campoValue"), valores("tablaValue"), valores("tablaText"), valores("operadorValue"), valores("valor"), valores("conectorValue"), valores("campoText"), valores("conectorText"), valores("operadorText"), valores("tipo"), 13, valores("consecutivo"))

            gridInstancias.Rebind()
            Session("NuevaInstancia") = 1
            btnSimularInstancias.Visible = True
            Aviso("Asignación actualizada")

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
    Private Sub gridReglas_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles gridReglas.NeedDataSource
        gridReglas.DataSource = Instancia("", RDDLInstancia.SelectedValue, "", "", "", "", "", "", "", "", "", "", "", 0)
    End Sub

    Private Sub gridReglas_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles gridReglas.ItemCommand
        Dim item As GridItem = CType(e.Item, GridItem)
        Dim regla As GridTableCell = CType(item.Controls(3), GridTableCell)
        If e.CommandName = "onSelect" Then
            Dim DtsDatos As DataTable = Instancia(regla.Text, RDDLInstancia.SelectedValue, "", "", "", "", "", "", "", "", "", "", "", 14)
            gridInstancias.DataSource = DtsDatos
            PnlInicio.Visible = False
            PnlConfiguracion.Visible = True
            gridInstancias.Visible = True
            gridInstancias.Rebind()
            RTNombre.Text = regla.Text
            RTNombre.Enabled = False
            btnSimularInstancias.Visible = True
            RbtnRegresar.Visible = True
            Session("NuevaInstancia") = 1
        ElseIf e.CommandName = "onBorrar" Then
            Dim DtsDatos As DataTable = Instancia(regla.Text, RDDLInstancia.SelectedValue, "", "", "", "", "", "", "", "", "", "", "", 12)
            Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "CatalogoInstancia", "Se elimino la regla " & regla.Text & ", de la instancia " & RDDLInstancia.Text)
            Aviso("Regla Eliminada")
            gridReglas.Rebind()
        End If
    End Sub
    Private Sub btnAcpetarBorrar_Click(sender As Object, e As EventArgs) Handles btnAcpetarBorrar.Click
        ScriptManager.RegisterStartupScript(Me, GetType(Page), "modal", "<script>document.getElementById('modalDelete').style.display='none'</script>", False)
        'Borra la dispersion anterior
        Instancia(RTNombre.Text, RDDLInstancia.SelectedValue, "", "", "", "", "", "", "", "", "", "", "", 1)
        'Se inserta la nueva dispersion
        Instancia(RTNombre.Text, RDDLInstancia.SelectedValue, "", "", "", "", "", "", "", "", "", "", "", 1)
        Aviso("Asignación actualizada")
        btnSimularInstancias.Visible = True
    End Sub

    Private Sub btnCancelarBorrar_Click(sender As Object, e As EventArgs) Handles btnCancelarBorrar.Click
        ScriptManager.RegisterStartupScript(Me, GetType(Page), "modal", "<script>document.getElementById('modalDelete').style.display='none'</script>", False)
        RDDLInstancia.ClearSelection()
    End Sub

    Protected Sub limpiar_gridAsig()
        Dim tabla As DataTable = Nothing
        Session("asignacion") = Nothing
        gridAsignacion.Visible = False
    End Sub
    Protected Sub limpiar_gridAsigPreview()
        Dim tabla As DataTable = Nothing
        Session("AsignacionPre") = Nothing
        RadGvPreview.Visible = False
    End Sub
    Private Sub btnSimularCamp_Click(sender As Object, e As EventArgs) Handles btnSimularInstancias.Click
        Try

            Dim data As DataTable = Instancia(RTNombre.Text, "", "", "", "", "", "", "", "", "", "", "", "", 1)

            If data.Rows.Count = 0 Then
                Aviso("No hay reglas para una simulación")
                limpiar_gridAsig()
            Else
                Dim tabla As DataTable = Instancia(RTNombre.Text, "", "", "", "", "", "", "", "", "", "", "", "", 5)

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
        Catch ex As Exception
            Aviso(ex.Message)
        End Try
    End Sub

    Private Sub RadGvPreview_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RadGvPreview.NeedDataSource
        RadGvPreview.DataSource = Session("AsignacionPre")
    End Sub
    Protected Sub RbtnRegresar_Click(sender As Object, e As EventArgs) Handles RbtnRegresar.Click
        limpiar_gridAsig()
        Limpiar()
    End Sub
    Protected Sub BtnPreviewInstancia_Click(sender As Object, e As EventArgs) Handles BtnPreviewInstancia.Click
        Try
            Dim data As DataTable
            If RDDLInstancia2.SelectedValue <> "Todas" Then
                data = Instancia("", RDDLInstancia2.SelectedValue, "", "", "", "", "", "", "", "", "", "", "", 9)
            Else
                data = Instancia("", RDDLInstancia2.SelectedValue, "", "", "", "", "", "", "", "", "", "", "", 10)
            End If
            If data.Rows.Count = 0 Then
                Aviso("No hay reglas para una simulación")
                limpiar_gridAsigPreview()
            Else
                Dim tabla As DataTable = Instancia("", RDDLInstancia2.SelectedValue, "", "", "", "", "", "", "", "", "", "", "", 11)
                If tabla.Rows.Count = 0 Then
                    Session("AsignacionPre") = Nothing
                    Aviso("No se encontraron coincidencias")
                    limpiar_gridAsigPreview()
                Else
                    'Session("AsignacionPre") = tabla
                    Aviso("Simulacion terminada, se encontraron " & tabla.Rows.Count & " registro(s)")

                    Dim arch_Excel As String = Db.StrRuta() & "Salida\Simulacion" & DateTime.Now.ToString("ddMMyyyy_hhmm") & ".xlsx"
                    Dim book As New Workbook()
                    Dim sheet As Worksheet = book.Worksheets(0)
                    sheet.InsertDataTable(tabla, True, 1, 1)
                    sheet.Name = "Simulacion"
                    book.SaveToFile(arch_Excel, ExcelVersion.Version2010)

                    If File.Exists(arch_Excel) Then
                        Dim ioflujo As FileInfo = New FileInfo(arch_Excel)
                        Response.Clear()
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + ioflujo.Name)
                        Response.AddHeader("Content-Length", ioflujo.Length.ToString())
                        Response.ContentType = "application/octet-stream"
                        Response.WriteFile(arch_Excel)
                        Response.End()
                    End If
                End If
            End If
        Catch ex As Exception
            Aviso(ex.Message)
        End Try
    End Sub
    Private Sub gridAsignacion_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles gridAsignacion.NeedDataSource
        gridAsignacion.DataSource = Session("asignacion")
    End Sub

    Private Sub CatalogoInstancia_Load(sender As Object, e As EventArgs) Handles Me.Load
        ScriptManager.GetCurrent(Page).RegisterPostBackControl(BtnPreviewInstancia)

        Dim SSCommandInstancia As New SqlCommand
        SSCommandInstancia.CommandText = "SP_INSTANCIAS"
        SSCommandInstancia.CommandType = CommandType.StoredProcedure
        RDDLInstancia.DataSource = Consulta_Procedure(SSCommandInstancia, "Instancias")
        RDDLInstancia.DataValueField = "INST_val"
        RDDLInstancia.DataTextField = "INST_text"
        RDDLInstancia.DataBind()
    End Sub
End Class
