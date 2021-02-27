Imports System.Data
Imports System.Data.OracleClient
Imports System.Data.SqlClient
Imports Conexiones
Imports Db
Imports Funciones
Imports Telerik.Web.UI

Partial Class CatalogoAvisos
    Inherits System.Web.UI.Page
    Public Property tmpUSUARIO As IDictionary
        Get
            Return CType(Session("USUARIO"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("USUARIO") = value
        End Set
    End Property
    Public Property tmpPermisos As IDictionary
        Get
            Return CType(Session("Permisos"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("Permisos") = value
        End Set
    End Property

    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Administrador", "CatalogoAvisos.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If tmpPermisos("CONFIG_PLANTILLAS") <> False Then
                Session.Clear()
                Session.Abandon()
                Response.Redirect("~/SesionExpirada.aspx")
            End If
        Catch ex As Exception
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/SesionExpirada.aspx")
        End Try
        Try
            If Not IsPostBack Then
                Llenar(0, "", "", "", "", 0)
            End If
        Catch ex As Exception
            SendMail("Page_Load", ex, "", "", tmpUSUARIO("CAT_LO_USUARIO"))
        End Try
    End Sub
    Sub Llenar(V_CAT_EAV_Id As Integer, V_CAT_EAV_Descripcion As String, V_CAT_EAV_Valor As String, V_CAT_EAV_Tabla As String, V_CAT_EAV_Camporeal As String, V_Bandera As Integer)
        Dim oraCommanAgencias As New SqlCommand
        oraCommanAgencias.CommandText = "SP_ADD_CAT_ETIQUETAS_AVISOS"
        oraCommanAgencias.CommandType = CommandType.StoredProcedure
        oraCommanAgencias.Parameters.Add("@V_CAT_EAV_Id", SqlDbType.Float).Value = V_CAT_EAV_Id
        oraCommanAgencias.Parameters.Add("@V_CAT_EAV_DESCRIPCION", SqlDbType.VarChar).Value = V_CAT_EAV_Descripcion
        oraCommanAgencias.Parameters.Add("@V_CAT_EAV_Valor", SqlDbType.VarChar).Value = V_CAT_EAV_Valor
        oraCommanAgencias.Parameters.Add("@V_CAT_EAV_Tabla", SqlDbType.VarChar).Value = V_CAT_EAV_Tabla
        oraCommanAgencias.Parameters.Add("@V_CAT_EAV_Camporeal", SqlDbType.VarChar).Value = V_CAT_EAV_Camporeal
        oraCommanAgencias.Parameters.Add("@V_Bandera", SqlDbType.VarChar).Value = V_Bandera
        '  oraCommanAgencias.Parameters.Add("CV_1", SqlDbType.Cursor).Direction = ParameterDirection.Output
        Dim DtsVarios As DataTable = Consulta_Procedure(oraCommanAgencias, "SP_ADD_CAT_ETIQUETAS_AVISOS")
        Dim DtvVarios As DataView = DtsVarios.DefaultView

    End Sub

    Protected Sub Mensaje(ByVal v_mensaje As String)
        RadWindowManager1.RadAlert(v_mensaje, 400, 150, Nothing, Nothing)
    End Sub

    Protected Function DirectorioImg(ByVal V_Valor1 As String) As String
        Dim aux As Integer
        Dim diagonal As Integer
        Dim archivo As String
        For i As Integer = 0 To V_Valor1.Length - 1
            If i + 4 <= V_Valor1.Length Then
                If V_Valor1.Substring(i, 4) = "src=" Then
                    aux = InStr(i + 6, V_Valor1, """")
                    diagonal = InStrRev(V_Valor1, "/", aux)
                    archivo = V_Valor1.Substring(diagonal, aux - diagonal - 1)
                    If DesEncriptarCadena(StrConexion(3)) = "BIENESTAR" Then
                        V_Valor1 = V_Valor1.Replace(V_Valor1.Substring(i, aux - i), "src=""https://ahorrosbienestar.app/Imagenes/" & archivo & """")
                    Else
                        V_Valor1 = V_Valor1.Replace(V_Valor1.Substring(i, aux - i), "src=""https://dev.mcnoc.mx/AhorrosBienestar/Imagenes/" & archivo & """") 'Nuevo endpoint de pruebas
                    End If
                End If
            End If
        Next
        Return V_Valor1
    End Function

    Private Sub gridPlantillas_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles gridPlantillas.NeedDataSource
        Dim oraCommand As New SqlCommand
        oraCommand.CommandText = "SP_ADD_CAT_PLANTILLAS_AVISOS"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("@V_CAT_PAV_Id", SqlDbType.Float).Value = 0
        oraCommand.Parameters.Add("@V_CAT_PAV_Nombre", SqlDbType.VarChar).Value = ""
        oraCommand.Parameters.Add("@V_CAT_PAV_CONFIGURACION", SqlDbType.VarChar).Value = DBNull.Value
        oraCommand.Parameters.Add("@V_CAT_PAV_PRODUCTO", SqlDbType.VarChar).Value = ""
        oraCommand.Parameters.Add("@V_Bandera", SqlDbType.Int).Value = 4
        '  oraCommand.Parameters.Add("CV_1", SqlDbType.Cursor).Direction = ParameterDirection.Output
        gridPlantillas.DataSource = Consulta_Procedure(oraCommand, "Avisos")
    End Sub

    Private Sub gridEtiquetas_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles gridEtiquetas.NeedDataSource
        Dim oraCommanAgencias As New SqlCommand
        oraCommanAgencias.CommandText = "SP_ADD_CAT_ETIQUETAS_AVISOS"
        oraCommanAgencias.CommandType = CommandType.StoredProcedure
        oraCommanAgencias.Parameters.Add("@V_Bandera", SqlDbType.VarChar).Value = 0
        '  oraCommanAgencias.Parameters.Add("CV_1", SqlDbType.Cursor).Direction = ParameterDirection.Output
        Dim datatab As DataTable = Consulta_Procedure(oraCommanAgencias, "SP_ADD_CAT_ETIQUETAS_AVISOS")
        Session("DataTableEtiquetas") = datatab
        gridEtiquetas.DataSource = datatab
    End Sub

    Private Sub CatalogoAvisos_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        If Not IsPostBack Then
            Try
                gridEtiquetas.MasterTableView.GetColumn("Tabla").Visible = False
                gridEtiquetas.MasterTableView.GetColumn("Campo").Visible = False
                gridEtiquetas.Rebind()
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub gridEtiquetas_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles gridEtiquetas.ItemCommand
        If e.CommandName = "PerformInsert" Then
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim hash As New Hashtable
            hash("Tabla") = CType(MyUserControl.FindControl("comboTablas"), RadComboBox).SelectedValue
            If hash("Tabla") = "" Or hash("Tabla") = Nothing Then
                hash("Tabla") = Session("Tabla")
                Session("Tabla") = Nothing

            End If

            hash("CampoVal") = CType(MyUserControl.FindControl("comboCampos"), RadComboBox).SelectedValue
            hash("CampoText") = CType(MyUserControl.FindControl("comboCampos"), RadComboBox).SelectedItem.Text
            If hash("CampoText").ToString.Length >= 50 Then
                hash("CampoText") = hash("CampoText").ToString.Substring(0, 46) & "..."
            End If
            hash("Etiqueta") = CType(MyUserControl.FindControl("txtEtiqueta"), RadTextBox).Text

            Dim oraCommanAgencias As New SqlCommand
            oraCommanAgencias.CommandText = "SP_ADD_CAT_ETIQUETAS_AVISOS"
            oraCommanAgencias.CommandType = CommandType.StoredProcedure
            oraCommanAgencias.Parameters.Add("@V_CAT_EAV_Id", SqlDbType.Int).Value = 0
            oraCommanAgencias.Parameters.Add("@V_CAT_EAV_Descripcion", SqlDbType.VarChar).Value = hash("Etiqueta")
            oraCommanAgencias.Parameters.Add("@V_Bandera", SqlDbType.VarChar).Value = 4
            'oraCommanAgencias.Parameters.Add("CV_1", SqlDbType.Cursor).Direction = ParameterDirection.Output
            Dim DtsVarios As DataTable = Consulta_Procedure(oraCommanAgencias, "Fuente")
            If DtsVarios.Rows(0).Item("Cuantos") = 0 Then
                Llenar(1, hash("Etiqueta"), hash("CampoText"), hash("Tabla"), hash("CampoVal"), 1)
                Mensaje("Fuente De Información Creada")
                Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "CAT Avisos", "Aviso agregado")
            Else
                Mensaje("ERROR LA ETIQUEDA QUE DESEA INGRESAR YA SE ENCUENTRA REGISTRADA")
            End If
        ElseIf e.CommandName = "Update" Then
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim hash As New Hashtable
            hash("Tabla") = CType(MyUserControl.FindControl("comboTablas"), RadComboBox).SelectedValue
            hash("CampoVal") = CType(MyUserControl.FindControl("comboCampos"), RadComboBox).SelectedValue
            hash("CampoText") = CType(MyUserControl.FindControl("comboCampos"), RadComboBox).SelectedItem.Text
            If hash("CampoText").ToString.Length >= 50 Then
                hash("CampoText") = hash("CampoText").ToString.Substring(0, 46) & "..."
            End If
            hash("Etiqueta") = CType(MyUserControl.FindControl("txtEtiqueta"), RadTextBox).Text
            hash("ID") = CType(MyUserControl.FindControl("txtID"), RadTextBox).Text

            Dim oraCommanAgencias As New SqlCommand
            oraCommanAgencias.CommandText = "SP_ADD_CAT_ETIQUETAS_AVISOS"
            oraCommanAgencias.CommandType = CommandType.StoredProcedure
            oraCommanAgencias.Parameters.Add("@V_CAT_EAV_Id", SqlDbType.Int).Value = hash("ID")
            oraCommanAgencias.Parameters.Add("@V_CAT_EAV_Descripcion", SqlDbType.VarChar).Value = hash("Etiqueta")
            oraCommanAgencias.Parameters.Add("@V_Bandera", SqlDbType.VarChar).Value = 6
            ' oraCommanAgencias.Parameters.Add("CV_1", SqlDbType.Cursor).Direction = ParameterDirection.Output
            Dim DtsVarios As DataTable = Consulta_Procedure(oraCommanAgencias, "Fuente")
            If DtsVarios.Rows(0).Item("Cuantos") = 0 Then
                Llenar(hash("ID"), hash("Etiqueta"), hash("CampoText"), hash("Tabla"), hash("CampoVal"), 2)
                Mensaje("Se Han Guardado Los Cambios")
                Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "CAT Avisos", "Aviso actualizado")
            Else
                Mensaje("Erro al actualizar. El nombre de la etiqueta ya existe.")
            End If
        ElseIf e.CommandName = "Edit" Then
            Session("Edit") = True
        ElseIf e.CommandName = "Delete" Then
            Try
                Dim item As GridItem = CType(e.Item, GridItem)
                Dim idcell As GridTableCell = CType(item.Controls(6), GridTableCell)
                Llenar(idcell.Text, "", "", "", "", 3)
                Mensaje("Se Ha Eliminado La Etiqueta")
                Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "CAT Avisos", "Aviso eliminadao")
                gridEtiquetas.Rebind()
            Catch ex As Exception
                SendMail("BtnEliminar_Click", ex, "", "", tmpUSUARIO("CAT_LO_USUARIO"))
            End Try
        End If
    End Sub

    Private Sub gridPlantillas_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles gridPlantillas.ItemCommand
        If e.CommandName = "InitInsert" Then
            Session("InitInsert") = True
        ElseIf e.CommandName = "Edit" Then
            Session("Edit") = True
        ElseIf e.CommandName = "Update" Or e.CommandName = "PerformInsert" Then
            Try
                Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
                Dim hash As New Hashtable
                hash("ID") = CType(MyUserControl.FindControl("txtID"), RadTextBox).Text
                hash("Nombre") = CType(MyUserControl.FindControl("txtNombre"), RadTextBox).Text
                hash("Editor") = CType(MyUserControl.FindControl("editor"), RadEditor).Content
                hash("Instancia") = CType(MyUserControl.FindControl("DdlInstancia"), RadDropDownList).SelectedText
                hash("Rol") = CType(MyUserControl.FindControl("DdlRolParticipante"), RadDropDownList).SelectedText
                'hash("Referencia") = CType(MyUserControl.FindControl("DdlReferencia"), RadDropDownList).SelectedText

                Dim oraCommand As New SqlCommand
                oraCommand.CommandText = "SP_ADD_CAT_PLANTILLAS_AVISOS"
                oraCommand.CommandType = CommandType.StoredProcedure
                If hash("ID").ToString = "" Then
                    oraCommand.Parameters.Add("@V_CAT_PAV_Id", SqlDbType.Int).Value = 0
                    oraCommand.Parameters.Add("@V_Bandera", SqlDbType.Int).Value = 1
                Else
                    oraCommand.Parameters.Add("@V_CAT_PAV_Id", SqlDbType.Int).Value = Val(hash("ID"))
                    oraCommand.Parameters.Add("@V_Bandera", SqlDbType.Int).Value = 2
                End If
                oraCommand.Parameters.Add("@V_CAT_PAV_Instancia", SqlDbType.VarChar).Value = hash("Instancia")
                oraCommand.Parameters.Add("@V_CAT_PAV_Rol", SqlDbType.VarChar).Value = hash("Rol")
                'oraCommand.Parameters.Add("@V_CAT_PAV_Referencia", SqlDbType.VarChar).Value = hash("Referencia")
                oraCommand.Parameters.Add("@V_CAT_PAV_Nombre", SqlDbType.VarChar).Value = hash("Nombre")
                oraCommand.Parameters.Add("@V_CAT_PAV_CONFIGURACION", SqlDbType.VarChar).Value = hash("Editor")
                oraCommand.Parameters.Add("@V_CAT_PAV_PRODUCTO", SqlDbType.VarChar).Value = tmpUSUARIO("CAT_LO_PRODUCTO")
                ' oraCommand.Parameters.Add("CV_1", SqlDbType.Cursor).Direction = ParameterDirection.Output
                Dim DtsAviso As DataTable = Consulta_Procedure(oraCommand, "Aviso")
                Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "CAT Avisos", "Plantilla " & Val(hash("ID")) & " actualizada")
                Mensaje(DtsAviso.Rows(0).Item("Aviso"))
            Catch ex As Exception
                Mensaje(ex.Message)
                e.Canceled = True
            End Try
        ElseIf e.CommandName = "Delete" Then
            Try
                Dim item As GridItem = CType(e.Item, GridItem)
                Dim idcell As GridTableCell = CType(item.Controls(4), GridTableCell)

                Dim oraCommand As New SqlCommand
                oraCommand.CommandText = "SP_ADD_CAT_PLANTILLAS_AVISOS"
                oraCommand.CommandType = CommandType.StoredProcedure
                oraCommand.Parameters.Add("@V_CAT_PAV_Id", SqlDbType.Int).Value = Val(idcell.Text)
                oraCommand.Parameters.Add("@V_CAT_PAV_Nombre", SqlDbType.VarChar).Value = ""
                oraCommand.Parameters.Add("@V_CAT_PAV_CONFIGURACION", SqlDbType.NVarChar).Value = DBNull.Value
                oraCommand.Parameters.Add("@V_CAT_PAV_PRODUCTO", SqlDbType.VarChar).Value = ""
                oraCommand.Parameters.Add("@V_Bandera", SqlDbType.Int).Value = 3
                '  oraCommand.Parameters.Add("CV_1", SqlDbType.Cursor).Direction = ParameterDirection.Output
                Dim DtsAviso As DataTable = Consulta_Procedure(oraCommand, "Aviso")
                Mensaje("Plantilla eliminada")
                Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "CAT Avisos", "Plantilla " & Val(idcell.Text) & " eliminda")
            Catch ex As Exception
                Mensaje(ex.Message)
            End Try
        End If
    End Sub
End Class
