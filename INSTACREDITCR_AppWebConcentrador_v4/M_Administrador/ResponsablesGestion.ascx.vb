

Imports System.Data
    Imports System.Data.SqlClient
    Imports Db
    Imports Telerik.Web.UI
Partial Class M_Administrador_ResponsablesGestion
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

    Private Sub TiposJuicio_Load(sender As Object, e As EventArgs) Handles Me.PreRender
        If Session("PostBack") = True Then
            Session("PostBack") = False
            llenar()
        End If
    End Sub

    Private Sub ResponsablesGestion_Init(sender As Object, e As EventArgs) Handles Me.Init
        If Session("PostBack") = True Then
            LLENAR_DROP(1, Session("Responsable"), RCBUsuarioResp, "V_ID", "T_NOMBRE", "Sin documentos", "")
        End If
    End Sub
    Private Sub llenar()
        TxtNombre.Text = DataItem.row.item(1).ToString
        RCBUsuarioResp.SelectedValue = DataItem.row.item(2).ToString
        RTBEmailResp.Text = DataItem.row.item(3).ToString
    End Sub
    Protected Sub LLENAR_DROP(ByVal bandera As Integer, ByVal v_dato1 As String, ByRef ITEM As RadComboBox, ByVal DataValueField As String, ByVal DataTextField As String, ByVal V_Nohay As String, ByVal v_usuario As String)
        Try
            ITEM.ClearCheckedItems()
            ITEM.ClearSelection()
            ITEM.Items.Clear()

            dim SSCommand as new sqlcommand
            SSCommand.CommandText = "SP_EDITA_RESPONSABLES_GESTION"
            SSCommand.CommandType = CommandType.StoredProcedure
            SSCommand.Parameters.Add("@v_dato1", SqlDbType.NVarChar).Value = v_dato1
            SSCommand.Parameters.Add("@v_dato2", SqlDbType.NVarChar).Value = ""
            SSCommand.Parameters.Add("@v_dato3", SqlDbType.NVarChar).Value = ""
            SSCommand.Parameters.Add("@v_dato4", SqlDbType.NVarChar).Value = ""
            SSCommand.Parameters.Add("@v_dato5", SqlDbType.NVarChar).Value = ""
            SSCommand.Parameters.Add("@v_dato6", SqlDbType.NVarChar).Value = ""
            SSCommand.Parameters.Add("@v_dato7", SqlDbType.NVarChar).Value = ""
            SSCommand.Parameters.Add("@v_dato8", SqlDbType.NVarChar).Value = ""
            SSCommand.Parameters.Add("@v_dato9", SqlDbType.NVarChar).Value = ""
            SSCommand.Parameters.Add("@v_dato10", SqlDbType.NVarChar).Value = ""
            SSCommand.Parameters.Add("@v_bandera", SqlDbType.Decimal).Value = bandera

            Dim objDSa As DataTable = Consulta_Procedure(SSCommand, "Filtros")

            If objDSa.Rows.Count > 0 Then
                ITEM.DataTextField = DataTextField
                ITEM.DataValueField = DataValueField
                ITEM.DataSource = objDSa
                ITEM.DataBind()
            End If

        Catch ex As Exception
            Dim ALGO As String = ex.ToString
        End Try
    End Sub
    Protected Sub to_limpia_ddl(ByVal v_item As RadComboBox, ByVal v_NoHay As String)
        v_item.Items.Clear()
    End Sub

End Class
