Imports Telerik.Web.UI
Imports Db
Imports Funciones
Imports System.Data.OracleClient
Imports System.Data
Imports System.Data.SqlClient

Partial Class M_Administrador_CatalogoAvisosEtiquetas
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

    Private Sub comboTablas_PreRender(sender As Object, e As EventArgs) Handles comboTablas.PreRender



        Dim oraCommanAgencias As New SqlCommand
        oraCommanAgencias.CommandText = "SP_ADD_CAT_ETIQUETAS_AVISOS"
        oraCommanAgencias.CommandType = CommandType.StoredProcedure
        oraCommanAgencias.Parameters.Add("V_Cat_Eav_Id", SqlDbType.Int).Value = 0

        oraCommanAgencias.Parameters.Add("V_Bandera", SqlDbType.Int).Value = 7
        ' oraCommanAgencias.Parameters.Add("CV_1", SqlDbTypeCursor).Direction = ParameterDirection.Output
        Dim DtsVarios As DataTable = Consulta_Procedure(oraCommanAgencias, "SP_ADD_CAT_ETIQUETAS_CORREO")
        Dim DtvVarios As DataTable = DtsVarios
        comboTablas.DataTextField = "CAT_TA_DESC"
        comboTablas.DataValueField = "CAT_TA_TABLA"
        comboTablas.DataSource = DtsVarios
        comboTablas.DataBind()


    End Sub

    Private Sub comboTablas_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles comboTablas.SelectedIndexChanged

        comboCampos.ClearSelection()
        comboCampos.Items.Clear()

        Dim oraCommanAgencias As New SqlCommand
        oraCommanAgencias.CommandText = "SP_ADD_CAT_ETIQUETAS_AVISOS"
        oraCommanAgencias.CommandType = CommandType.StoredProcedure
        oraCommanAgencias.Parameters.Add("V_Cat_Eav_Id", SqlDbType.Int).Value = 0
        oraCommanAgencias.Parameters.Add("V_CAT_EAV_Tabla", SqlDbType.VarChar).Value = comboTablas.SelectedValue
        oraCommanAgencias.Parameters.Add("V_Bandera", SqlDbType.Int).Value = 5
        ' oraCommanAgencias.Parameters.Add("CV_1", SqlDbTypeCursor).Direction = ParameterDirection.Output
        Dim DtsVarios As DataTable = Consulta_Procedure(oraCommanAgencias, "SP_ADD_CAT_ETIQUETAS_CORREO")
        Dim DtvVarios As DataTable = DtsVarios
        comboCampos.DataTextField = "Campo_nombre"
        comboCampos.DataValueField = "Campo"
        comboCampos.DataSource = DtsVarios
        comboCampos.DataBind()
        comboCampos.Enabled = True
        comboCampos.Focus()
        Session("Tabla") = comboTablas.SelectedValue
    End Sub

    Private Sub M_Administrador_gridEtiquetasCorreo_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        If Session("Edit") Then


            Session.Remove("Edit")
            txtID.Text = DataItem("Id")
            txtEtiqueta.Text = DataItem("Etiqueta")
            Try
                Try
                    comboTablas.Items.FindItemByValue(DataItem("CampoReal")).Selected = True
                    comboTablas.Enabled = False
                Catch ex As Exception
                    Dim data As DataTable = Session("DataTableEtiquetas")
                    comboTablas.SelectedValue = data.Rows(0).Item(3)
                    ' comboTablas.Items.FindItemByValue(data.Rows(0).Item(4)).Selected = True
                    comboTablas.Enabled = False
                End Try

            Catch ex As Exception

                Dim a As String = ex.Message
            End Try
            Dim oraCommanAgencias As New SqlCommand
            oraCommanAgencias.CommandText = "SP_ADD_CAT_ETIQUETAS_AVISOS"
            oraCommanAgencias.CommandType = CommandType.StoredProcedure
            oraCommanAgencias.Parameters.Add("V_CAT_EAV_Id", SqlDbType.Int).Value = 0
            oraCommanAgencias.Parameters.Add("V_CAT_EAV_Tabla", SqlDbType.VarChar).Value = comboTablas.SelectedValue
            oraCommanAgencias.Parameters.Add("V_Bandera", SqlDbType.Int).Value = 5
            ' oraCommanAgencias.Parameters.Add("CV_1", SqlDbType.Cursor).Direction = ParameterDirection.Output
            Dim DtsVarios As DataTable = Consulta_Procedure(oraCommanAgencias, "SP_ADD_CAT_ETIQUETAS_AVISOS")
            Dim DtvVarios As DataTable = DtsVarios
            comboCampos.DataTextField = "COMMENTS"
            comboCampos.DataValueField = "COLUMN_NAME"
            comboCampos.DataSource = DtsVarios
            comboCampos.DataBind()
            Try
                comboCampos.Items.FindItemByValue(DataItem("CampoReal")).Selected = True
                comboCampos.Enabled = False
            Catch ex As Exception
            End Try
        End If
    End Sub



End Class
