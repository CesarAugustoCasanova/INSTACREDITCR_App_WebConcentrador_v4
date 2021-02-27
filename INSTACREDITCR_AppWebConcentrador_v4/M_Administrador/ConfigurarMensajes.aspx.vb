Imports Telerik.Web.UI
Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports Db
Imports System.Web.SessionState.HttpSessionState
Imports System.Data
Imports Funciones
Imports System.Web.Services
Partial Class _ConfigurarMensajes
    Inherits System.Web.UI.Page
    ''' <summary>
    ''' Establece los parametros para mostrar una notificacion
    ''' </summary>
    ''' <param name="Notificacion">Objeto RadNotification de Telerik</param>
    ''' <param name="icono">info - delete - deny - edit - ok - warning - none</param>
    ''' <param name="titulo">título de la notificación</param>
    ''' <param name="msg">mensaje de la notificación</param>
    Public Shared Sub showModal(ByRef Notificacion As Object, ByVal icono As String, ByVal titulo As String, ByVal msg As String)
        Dim radnot As RadNotification = TryCast(Notificacion, RadNotification)
        radnot.TitleIcon = icono
        radnot.ContentIcon = icono
        radnot.Title = titulo
        radnot.Text = msg
        radnot.Show()
    End Sub

    Private Sub _ConfigurarMensajes_Init(sender As Object, e As EventArgs) Handles Me.Init
        If Not IsPostBack Then
            gridMensajes.Rebind()
        End If
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        pnlMensaje.Visible = False
        pnlMensajes.Visible = True
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If Page.IsValid() Then
            Dim referencias As String = ""
            For Each item As RadComboBoxItem In comboReferencias.CheckedItems
                referencias &= item.Value & ","
            Next
            'Se elimina la ultima ',' insertada
            referencias = referencias.Substring(0, referencias.Length - 1)
            Dim SSCommandCat As New SqlCommand
            SSCommandCat.CommandText = "SP_CONFIG_MENSAJES"
            SSCommandCat.CommandType = CommandType.StoredProcedure
            SSCommandCat.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 1
            SSCommandCat.Parameters.Add("@V_NOMBRE", SqlDbType.NVarChar).Value = txtNombre.Text
            SSCommandCat.Parameters.Add("@v_tipo", SqlDbType.NVarChar).Value = comboTipo.SelectedValue
            SSCommandCat.Parameters.Add("@v_campana", SqlDbType.NVarChar).Value = comboCampana.SelectedValue
            SSCommandCat.Parameters.Add("@v_rol", SqlDbType.NVarChar).Value = comboRol.SelectedValue
            SSCommandCat.Parameters.Add("@v_referencias", SqlDbType.NVarChar).Value = referencias
            SSCommandCat.Parameters.Add("@v_mensaje", SqlDbType.NVarChar).Value = txtMensaje.Text
            Try
                Consulta_Procedure(SSCommandCat, "Catalogo")
                limpiar()
                pnlMensaje.Visible = False
                pnlMensajes.Visible = True
                showModal(RadNotification1, "ok", "Correcto", "Mensaje guardada correctamente")
                gridMensajes.Rebind()
            Catch ex As Exception
                showModal(RadNotification1, "deny", "Error de sistema", ex.Message)
            End Try
        End If
    End Sub

    Private Sub btnNuevoMensaje_Click(sender As Object, e As EventArgs) Handles btnNuevoMensaje.Click
        pnlMensaje.Visible = True
        pnlMensajes.Visible = False
        llenarCombo(5)
        llenarCombo(6)
        llenarReferencias()
        txtNombre.Text = ""
        txtNombre.ReadOnly = False
        txtNombre.Focus()
    End Sub

    Private Sub gridMensajes_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles gridMensajes.ItemCommand
        Dim item As GridItem = CType(e.Item, GridItem)
        Dim mensaje As GridTableCell = CType(item.Controls(4), GridTableCell)
        Select Case e.CommandName
            Case "onEdit"
                pnlMensaje.Visible = True
                pnlMensajes.Visible = False
                llenarCombo(5)
                llenarCombo(6)
                llenarReferencias()

                Dim SSCommandCat As New SqlCommand
                SSCommandCat.CommandText = "SP_CONFIG_MENSAJES"
                SSCommandCat.CommandType = CommandType.StoredProcedure
                SSCommandCat.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 8
                SSCommandCat.Parameters.Add("@V_NOMBRE", SqlDbType.NVarChar).Value = mensaje.Text
                Dim row As DataRow = Consulta_Procedure(SSCommandCat, "Catalogo")(0)

                comboCampana.Items.FindItemByValue(row("HIST_CM_CAMPANA")).Selected = True
                comboRol.Items.FindItemByValue(row("HIST_CM_ROL")).Selected = True
                comboTipo.Items.FindItemByValue(row("HIST_CM_TIPO")).Selected = True
                txtMensaje.Text = row("HIST_CM_MENSAJE")
                txtNombre.Text = row("HIST_CM_NOMBRE")
                txtNombre.ReadOnly = True

                For Each res As String In row("HIST_CM_REFERENCIAS").ToString.Split(",")
                    Try
                        comboReferencias.Items.FindItemByValue(res).Checked = True
                    Catch ex As Exception
                    End Try
                Next
            Case "onDelete"
                Dim SSCommandCat As New SqlCommand
                SSCommandCat.CommandText = "SP_CONFIG_MENSAJES"
                SSCommandCat.CommandType = CommandType.StoredProcedure
                SSCommandCat.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 4
                SSCommandCat.Parameters.Add("@V_NOMBRE", SqlDbType.NVarChar).Value = mensaje.Text
                Consulta_Procedure(SSCommandCat, "Catalogo")
                gridMensajes.Rebind()
        End Select
    End Sub

    Private Sub gridMensajes_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles gridMensajes.NeedDataSource
        Dim SSCommandCat As New SqlCommand
        SSCommandCat.CommandText = "SP_CONFIG_MENSAJES"
        SSCommandCat.CommandType = CommandType.StoredProcedure
        SSCommandCat.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 3
        gridMensajes.DataSource = Consulta_Procedure(SSCommandCat, "Catalogo")
    End Sub
    ''' <summary>
    ''' Llena los combos correspondientes
    ''' </summary>
    ''' <param name="flag">5: comboCampaña - 6: comboRol</param>
    Private Sub llenarCombo(ByRef flag As Integer)
        Dim combo As RadComboBox = New RadComboBox
        Select Case flag
            Case 5
                combo = comboCampana
            Case 6
                combo = comboRol
            Case 7
                combo = comboReferencias
        End Select
        combo.Items.Clear()
        Dim SSCommandCat As New SqlCommand
        SSCommandCat.CommandText = "SP_CONFIG_MENSAJES"
        SSCommandCat.CommandType = CommandType.StoredProcedure
        SSCommandCat.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = flag
        Dim resultados As DataTable = Consulta_Procedure(SSCommandCat, "Catalogo")
        For Each resultado As DataRow In resultados.Rows
            Try

                Dim item As RadComboBoxItem = New RadComboBoxItem
                'Casos para comboRol
                Select Case resultado("TEXTO")
                    Case "A"
                        item.Text = "Aval"
                    Case "T"
                        item.Text = "Titular"
                    Case "C"
                        item.Text = "Codeudor"
                    Case "G"
                        item.Text = "Garante"
                    Case Else
                        item.Text = resultado("TEXTO")
                End Select

                item.Value = resultado("TEXTO")
                combo.Items.Add(item)
            Catch ex As Exception

            End Try
        Next
    End Sub

    Private Sub llenarReferencias()
        comboReferencias.Items.Clear()
        Dim SSCommandCat As New SqlCommand
        SSCommandCat.CommandText = "SP_CONFIG_MENSAJES"
        SSCommandCat.CommandType = CommandType.StoredProcedure
        SSCommandCat.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 7
        Dim resultados As DataTable = Consulta_Procedure(SSCommandCat, "Catalogo")
        For Each resultado As DataRow In resultados.Rows
            Try
                Dim item As RadComboBoxItem = New RadComboBoxItem
                item.Text = resultado("TEXTO")
                item.Value = resultado("VALOR")
                comboReferencias.Items.Add(item)
            Catch ex As Exception

            End Try
        Next
    End Sub

    Private Sub limpiar()
        comboCampana.ClearSelection()
        comboRol.ClearSelection()
        comboTipo.ClearSelection()
        comboReferencias.ClearCheckedItems()
        txtMensaje.Text = ""

    End Sub
End Class
