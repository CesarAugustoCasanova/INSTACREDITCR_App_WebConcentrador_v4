Imports System.Data.SqlClient
Imports System.Data
Imports Db
Imports Telerik.Web.UI

Partial Class M_Administrador_PagosConciliacion
    Inherits System.Web.UI.Page

    Public Property tmpUSUARIO As IDictionary
        Get
            Return CType(Session("USUARIO"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("USUARIO") = value
        End Set
    End Property

    Private Sub GVPagos_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles GVPagos.ItemCommand

        If e.CommandName = "Update" Then
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)

            Dim SSCommand As New SqlCommand("SP_PAGOS_ADMIN")
            SSCommand.CommandType = CommandType.StoredProcedure
            SSCommand.Parameters.Add("@v_bandera", SqlDbType.NVarChar).Value = 4
            SSCommand.Parameters.Add("@v_motivo", SqlDbType.NVarChar).Value = CType(MyUserControl.FindControl("txtMotivo"), RadTextBox).Text
            SSCommand.Parameters.Add("@v_usuario", SqlDbType.NVarChar).Value = CType(MyUserControl.FindControl("CbUsuario"), RadComboBox).SelectedValue
            SSCommand.Parameters.Add("@v_usuarioc", SqlDbType.NVarChar).Value = tmpUSUARIO("CAT_LO_USUARIO")
            SSCommand.Parameters.Add("@v_id_pago", SqlDbType.NVarChar).Value = GVPagos.MasterTableView.Items(e.Item.ItemIndex).Cells(3).Text & GVPagos.MasterTableView.Items(e.Item.ItemIndex).Cells(4).Text & GVPagos.MasterTableView.Items(e.Item.ItemIndex).Cells(5).Text
            Dim m As DataTable = Consulta_Procedure(SSCommand, "Filtros")
            GVPagos.Rebind()
        ElseIf e.CommandName = "Edit" Then
            Session("Empieza") = True
            Session("ID") = e.Item.Cells(3).Text & e.Item.Cells(4).Text & e.Item.Cells(5).Text
        End If
    End Sub

    Private Sub GVPagos_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles GVPagos.NeedDataSource
        Dim SSCommand As New SqlCommand("SP_PAGOS_ADMIN")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_bandera", SqlDbType.NVarChar).Value = 0
        Dim m As DataTable = Consulta_Procedure(SSCommand, "Filtros")
        GVPagos.DataSource = m
    End Sub
End Class
