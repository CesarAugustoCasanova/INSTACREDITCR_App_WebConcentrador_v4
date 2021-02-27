Imports Funciones
Imports System.Data
Imports Telerik.Web.UI
Imports System.Data.SqlClient
Imports Db
Partial Class M_Administrador_CatalogoComisiones
    Inherits System.Web.UI.Page

    Private Sub M_Administrador_CatalogoComisiones_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            RGComisiones.Rebind()
        End If
    End Sub

    Protected Sub RGComisiones_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs)
        RGComisiones.DataSource = COMISIONES(0)
    End Sub
    Function COMISIONES(v_bandera As Integer, Optional V_ID As String = "") As DataTable
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_CATALOGO_COMISIONES"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = v_bandera
        SSCommand.Parameters.Add("@V_ID", SqlDbType.NVarChar).Value = V_ID
        Dim DtsComisiones As DataTable = Consulta_Procedure(SSCommand, SSCommand.CommandText)
        Return DtsComisiones
    End Function

    Public Sub RGComisiones_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RGComisiones.ItemCommand
        Dim comando As String = e.CommandName
        If comando = "Ver" Then
            PnlRangos.Visible = True
            rLblIdTipo.Text = e.Item.Cells(3).Text
            rLblTipo.Text = e.Item.Cells(4).Text
            Dim DtsRango As DataTable = COMISIONES(1, rLblIdTipo.Text)
            RGCRangos.DataSource = DtsRango
            RGCRangos.DataBind()
        End If
    End Sub

    Private Sub BtnCalcular_Click(sender As Object, e As EventArgs) Handles BtnCalcular.Click

        Dim dts As DataTable = COMISIONES(2, rLblIdTipo.Text)
        RadGrid1.DataSource = dts
        RadGrid1.DataBind()
    End Sub
End Class
