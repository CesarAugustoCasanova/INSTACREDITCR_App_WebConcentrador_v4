Imports System.Data.SqlClient
Imports System.Data
Imports Db
Imports Telerik.Web.UI

Partial Class Permisos_ConfiguracionPermisos
    Inherits System.Web.UI.Page
    Public Enum Modulo
        None
        Gestion
        Administrador
        Reportes
        BackOffice
        Movil
        Judicial
    End Enum
    Private ModuloNombre() As String = {"", "Gestión", "Administrador", "Reportes", "BackOffice", "Móvil", "Judicial"}
    Private Sub txtPassword_TextChanged(sender As Object, e As EventArgs) Handles txtPassword.TextChanged
        If txtPassword.Text = "Hola Mundo" Then
            pnlInicial.Visible = False
            pnlDesarrollador.Visible = True

            gridAdministrador.DataBind()
            gridAdministrador.Rebind()

            gridBackO.DataBind()
            gridBackO.Rebind()

            gridGestion.DataBind()
            gridGestion.Rebind()

            gridJudicial.DataBind()
            gridJudicial.Rebind()

            gridMovil.DataBind()
            gridMovil.Rebind()

            gridReportes.DataBind()
            gridReportes.Rebind()
        End If
    End Sub



    Protected Sub grid_ItemCreated(sender As Object, e As GridItemEventArgs)
        disableTextBox(e)
    End Sub

    Protected Sub grid_ItemCommand(sender As Object, e As GridCommandEventArgs)
        ' INSERT
        If TypeOf e.Item Is GridEditableItem Then
            If e.CommandName = RadGrid.PerformInsertCommandName Then

                Dim editedItem As GridEditableItem = TryCast(e.Item, GridEditableItem)
                Dim newValues As New Hashtable()
                e.Item.OwnerTableView.ExtractValuesFromItem(newValues, editedItem)
                Dim grid As RadGrid = TryCast(sender, RadGrid)
                newValues("CAT_PE_idm") = grid.Attributes("modulo")
                newValues("CAT_PE_modulo") = ModuloNombre(grid.Attributes("modulo"))
                newValues("CAT_PE_orden") = -1
                SaveData(newValues)
            ElseIf e.CommandName = "Update" Then
                ' UPDATE
                Dim editedItem As GridEditableItem = TryCast(e.Item, GridEditableItem)
                Dim newValues As New Hashtable()
                e.Item.OwnerTableView.ExtractValuesFromItem(newValues, editedItem)
                SaveData(newValues)
            ElseIf e.CommandName = "Delete" Then
                Dim editedItem As GridEditableItem = TryCast(e.Item, GridEditableItem)
                Dim newValues As New Hashtable()
                e.Item.OwnerTableView.ExtractValuesFromItem(newValues, editedItem)
                sp_cat_perfiles(v_bandera:=3, V_CAT_PE_IDM:=newValues("CAT_PE_idm"), V_CAT_PE_ORDEN:=newValues("CAT_PE_orden"), V_CAT_PE_MENU:=newValues("CAT_PE_MENU"))
            End If
        End If
    End Sub

    Private Sub disableTextBox(e As GridItemEventArgs)
        If TypeOf e.Item Is GridEditableItem AndAlso e.Item.IsInEditMode Then
            If Not (TypeOf e.Item Is GridEditFormInsertItem) Then
                Dim item As GridEditableItem = TryCast(e.Item, GridEditableItem)
                Dim manager As GridEditManager = item.EditManager

                Dim editor As GridTextBoxColumnEditor = TryCast(manager.GetColumnEditor("Idm"), GridTextBoxColumnEditor)
                editor.TextBoxControl.Enabled = False

                editor = TryCast(manager.GetColumnEditor("Modulo"), GridTextBoxColumnEditor)
                editor.TextBoxControl.Enabled = False

                editor = TryCast(manager.GetColumnEditor("Orden"), GridTextBoxColumnEditor)
                editor.TextBoxControl.Enabled = False

                editor = TryCast(manager.GetColumnEditor("Permiso"), GridTextBoxColumnEditor)
                editor.TextBoxControl.Enabled = False

                editor = TryCast(manager.GetColumnEditor("Menu"), GridTextBoxColumnEditor)
                editor.TextBoxControl.Enabled = False
            End If
        End If
    End Sub

    Private Sub SaveData(values As Hashtable)
        sp_cat_perfiles(v_bandera:=2, V_CAT_PE_IDM:=values("CAT_PE_idm"), V_CAT_PE_ENABLED:=values("CAT_PE_enabled"), V_CAT_PE_MENU:=values("CAT_PE_MENU"), V_CAT_PE_MODULO:=values("CAT_PE_modulo"), V_CAT_PE_ORDEN:=values("CAT_PE_orden"), V_CAT_PE_PERMISO:=values("CAT_PE_PERMISO"))
    End Sub
    Private Function sp_cat_perfiles(v_bandera As Integer, Optional V_CAT_PE_IDM As String = "", Optional V_CAT_PE_MODULO As String = "", Optional V_CAT_PE_PERMISO As String = "", Optional V_CAT_PE_MENU As String = "", Optional V_CAT_PE_ORDEN As String = "", Optional V_CAT_PE_ENABLED As String = "") As DataTable
        Dim SSCommand As New SqlCommand("SP_CAT_PERFILES")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_bandera", SqlDbType.Int).Value = v_bandera
        SSCommand.Parameters.Add("@V_CAT_PE_IDM", SqlDbType.VarChar).Value = V_CAT_PE_IDM
        SSCommand.Parameters.Add("@V_CAT_PE_MODULO", SqlDbType.VarChar).Value = V_CAT_PE_MODULO
        SSCommand.Parameters.Add("@V_CAT_PE_PERMISO", SqlDbType.VarChar).Value = V_CAT_PE_PERMISO
        SSCommand.Parameters.Add("@V_CAT_PE_MENU", SqlDbType.VarChar).Value = V_CAT_PE_MENU
        SSCommand.Parameters.Add("@V_CAT_PE_ORDEN", SqlDbType.VarChar).Value = V_CAT_PE_ORDEN
        SSCommand.Parameters.Add("@V_CAT_PE_ENABLED", SqlDbType.VarChar).Value = V_CAT_PE_ENABLED
        Return Consulta_Procedure(SSCommand, "SP_CAT_PERFILES")
    End Function



    Protected Sub grid_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs)
        Dim grid As RadGrid = TryCast(sender, RadGrid)
        grid.DataSource = sp_cat_perfiles(v_bandera:=1, V_CAT_PE_IDM:=grid.Attributes("Modulo"))
    End Sub

    Private Sub btnRestart_Click(sender As Object, e As EventArgs) Handles btnRestart.Click
        For Each item As ButtonListItem In cblModulosRestart.Items
            If item.Selected Then
                Dim idm = item.Value
                sp_cat_perfiles(v_bandera:=4, V_CAT_PE_IDM:=idm, V_CAT_PE_MENU:=1)
                If idm = 1 Then
                    sp_cat_perfiles(v_bandera:=4, V_CAT_PE_IDM:=idm, V_CAT_PE_MENU:=0)
                End If
            End If
        Next
    End Sub
End Class
