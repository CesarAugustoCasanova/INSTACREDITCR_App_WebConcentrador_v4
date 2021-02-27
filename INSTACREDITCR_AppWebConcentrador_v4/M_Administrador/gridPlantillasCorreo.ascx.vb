Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Services
Imports Conexiones
Imports Db
Imports Funciones
Partial Class M_Administrador_gridPlantillasCorreo
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

    Private Sub M_Administrador_gridPlantillasCorreo_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("InitInsert") Then
            Session.Remove("InitInsert")
            initEtiquetas()
        End If

        If Session("Edit") Then
            Session.Remove("Edit")
            initEtiquetas()
            txtID.Text = DataItem("IDENTIFICADOR")
            dim SSCommand as new sqlcommand
            SSCommand.CommandText = "SP_ADD_CAT_PLANTILLAS_CORREO"
            SSCommand.CommandType = CommandType.StoredProcedure
            SSCommand.Parameters.Add("@V_Cat_Pc_Id", SqlDbType.Decimal).Value = Val(txtID.Text)
            SSCommand.Parameters.Add("@V_Cat_Pc_Nombre", SqlDbType.NVarChar).Value = ""
            SSCommand.Parameters.Add("@V_CAT_PC_CONFIGURACION", SqlDbType.Nvarchar).Value = DBNull.Value
            SSCommand.Parameters.Add("@V_CAT_PC_PRODUCTO", SqlDbType.NVarChar).Value = ""
            SSCommand.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = 5

            Dim DtsCorreo As DataTable = Consulta_Procedure(SSCommand, "Correo")
            editor.Content = DtsCorreo.Rows(0)("Configuracion")
            txtNombre.Text = DtsCorreo.Rows(0)("Nombre")
            txtNombre.Enabled = False
        End If
    End Sub

    Private Sub initEtiquetas()
        Dim SSCommandAgencias As New SqlCommand
        SSCommandAgencias.CommandText = "SP_ADD_CAT_ETIQUETAS_CORREO"
        SSCommandAgencias.CommandType = CommandType.StoredProcedure
        SSCommandAgencias.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 0
        Dim DtsVarios As DataTable = Consulta_Procedure(SSCommandAgencias, "SP_ADD_CAT_ETIQUETAS_CORREO")
        comboEtiquetas.DataTextField = "Etiqueta"
        comboEtiquetas.DataValueField = "CAMPOREAL"
        comboEtiquetas.DataSource = DtsVarios
        comboEtiquetas.DataBind()
    End Sub

End Class
