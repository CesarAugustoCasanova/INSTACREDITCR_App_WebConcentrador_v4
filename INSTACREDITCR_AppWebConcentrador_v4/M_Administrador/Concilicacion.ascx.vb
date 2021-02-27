Imports System.Data.SqlClient
Imports System.Data
Imports Db
Imports Telerik.Web.UI
Partial Class M_Administrador_Concilicacion
    Inherits System.Web.UI.UserControl

    Private Sub CbPlaza_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles CbPlaza.SelectedIndexChanged
        CbUsuario.DataSource = drops(3, CbPlaza.SelectedValue)
        CbUsuario.DataBind()
    End Sub

    Private Sub M_Administrador_Concilicacion_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("Empieza") Then
            Session("Empieza") = False
            CbPlaza.DataSource = drops(1)
            CbPlaza.DataBind()
            CbUsuario.DataSource = drops(2)
            CbUsuario.DataBind()
            llenarsihay()
        End If
    End Sub

    Private Function drops(bandera As Integer, Optional plaza As Integer = -1) As DataTable
        Dim SSCommand As New SqlCommand("SP_PAGOS_ADMIN")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_bandera", SqlDbType.NVarChar).Value = bandera
        SSCommand.Parameters.Add("@v_plaza", SqlDbType.NVarChar).Value = plaza
        Dim m As DataTable = Consulta_Procedure(SSCommand, "Filtros")
        Return m
    End Function
    Private Sub llenarsihay()
        Dim SSCommand As New SqlCommand("SP_PAGOS_ADMIN")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_bandera", SqlDbType.NVarChar).Value = 5
        SSCommand.Parameters.Add("@v_id_pago", SqlDbType.NVarChar).Value = Session("ID")
        Dim m As DataTable = Consulta_Procedure(SSCommand, "Filtros")
        If Not IsDBNull(m.Rows(0).Item("USUARIO")) Then
            txtMotivo.Text = m.Rows(0).Item("MOTIVO")
            CbUsuario.SelectedValue = CbUsuario.FindItemByText(m.Rows(0).Item("USUARIO")).Value
        End If
    End Sub
End Class
