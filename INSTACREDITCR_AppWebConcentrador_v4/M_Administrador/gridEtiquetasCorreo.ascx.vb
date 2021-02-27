
Imports Telerik.Web.UI
Imports Db
Imports Funciones
Imports System.Data.SqlClient
Imports System.Data

Partial Class M_Administrador_gridEtiquetasCorreo
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

    Private Sub comboTablas_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles comboTablas.SelectedIndexChanged

        Dim SSCommandAgencias As New SqlCommand
        SSCommandAgencias.CommandText = "SP_ADD_CAT_ETIQUETAS_CORREO"
        SSCommandAgencias.CommandType = CommandType.StoredProcedure
        SSCommandAgencias.Parameters.Add("@V_Cat_Ec_Id", SqlDbType.Decimal).Value = 0
        SSCommandAgencias.Parameters.Add("@V_Cat_Ec_Tabla", SqlDbType.NVarChar).Value = comboTablas.SelectedValue
        SSCommandAgencias.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 5
        Dim DtvVarios As DataTable = Consulta_Procedure(SSCommandAgencias, "SP_ADD_CAT_ETIQUETAS_CORREO")

        comboCampos.DataTextField = "COMMENTS"
        comboCampos.DataValueField = "COLUMN_NAME"
        comboCampos.DataSource = DtvVarios
        comboCampos.DataBind()
        comboCampos.Enabled = True
        comboCampos.Focus()
    End Sub

    Private Sub M_Administrador_gridEtiquetasCorreo_PreRender(sender As Object, e As EventArgs) Handles Me.Load
        If Session("Edit") Then
            Session.Remove("Edit")
            txtID.Text = DataItem("Id")
            txtEtiqueta.Text = DataItem("Etiqueta")
            Try
                comboTablas.Items.FindItemByValue(DataItem("Tabla")).Selected = True
                ' comboTablas.Enabled = False
            Catch ex As Exception
            End Try
            Dim SSCommandAgencias As New SqlCommand
            SSCommandAgencias.CommandText = "SP_ADD_CAT_ETIQUETAS_CORREO"
            SSCommandAgencias.CommandType = CommandType.StoredProcedure
            SSCommandAgencias.Parameters.Add("@V_Cat_Ec_Id", SqlDbType.Decimal).Value = 0
            SSCommandAgencias.Parameters.Add("@V_Cat_Ec_Tabla", SqlDbType.NVarChar).Value = comboTablas.SelectedValue
            SSCommandAgencias.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 5
            Dim DtsVarios As DataTable = Consulta_Procedure(SSCommandAgencias, "SP_ADD_CAT_ETIQUETAS_CORREO")

            comboCampos.DataTextField = "COMMENTS"
            comboCampos.DataValueField = "COLUMN_NAME"
            comboCampos.DataSource = DtsVarios
            comboCampos.DataBind()

            Dim SSCommandAgencias2 As New SqlCommand
            SSCommandAgencias2.CommandText = "SP_ADD_CAT_ETIQUETAS_CORREO"
            SSCommandAgencias2.CommandType = CommandType.StoredProcedure
            SSCommandAgencias2.Parameters.Add("@V_Cat_Ec_Id", SqlDbType.Decimal).Value = txtID.Text
            SSCommandAgencias2.Parameters.Add("@V_Cat_Ec_Tabla", SqlDbType.NVarChar).Value = comboTablas.SelectedValue
            SSCommandAgencias2.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 7
            Dim DtsVarios2 As DataTable = Consulta_Procedure(SSCommandAgencias2, "SP_ADD_CAT_ETIQUETAS_CORREO")


            Try
                comboCampos.SelectedValue = DataItem("CampoReal")
                comboCampos.Enabled = True
            Catch ex As Exception
            End Try
            Try
                comboTablas.SelectedValue = DtsVarios2.Rows(0).Item("TABLA")
                comboTablas.Enabled = True
            Catch ex As Exception
            End Try
        End If
    End Sub
End Class
