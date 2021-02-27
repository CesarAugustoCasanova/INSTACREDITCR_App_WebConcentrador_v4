
Imports Telerik.Web.UI
Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports Db
Imports System.Web.SessionState.HttpSessionState
Imports System.Data
Imports Funciones
Imports System.Web.Services


Partial Class _CatalogoPermisos
    Inherits System.Web.UI.Page

    Public Property TmpUSUARIO As IDictionary
        Get
            Return CType(Session("USUARIO"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("USUARIO") = value
        End Set
    End Property

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
        radnot.Text = msg.Replace("'", "").Replace("""", "").Replace(Chr(13), "").Replace(Chr(10), "")
        radnot.Show()
    End Sub
    Private Sub _CatalogoPermisos_Init(sender As Object, e As EventArgs) Handles Me.Init
        If Not IsPostBack Then
            gridPerfiles.Rebind()
        End If
    End Sub

    Private Sub btnPerfilCancelar_Click(sender As Object, e As EventArgs) Handles btnPerfilCancelar.Click
        '  limpiarCondonacion()
        pnlPerfil.Visible = False
        pnlPerfiles.Visible = True
    End Sub

    Private Sub btnPerfilGuardar_Click(sender As Object, e As EventArgs) Handles btnPerfilGuardar.Click
        If Page.IsValid Then
            Try

                Dim ds As DataTable = SP.ADD_PERFILES(3, txtNombrePerfil.Text, comboToString(comboAGestion), comboToString(comboPGestion), comboToString(comboAdmin), comboToString(comboBackOffice), comboToString(comboReportes), comboToString(comboMovil), comboToString(comboJudicial), 0, 0)
                If ds.TableName = "Exception" Then
                    Throw New Exception(ds.Rows(0).Item(0).ToString)
                End If
                If ds.Rows(0).Item(0) = "UPDATE" Then
                    showModal(RadNotification1, "ok", "Correcto", "Perfil Actualizado correctamente")
                Else
                    showModal(RadNotification1, "ok", "Correcto", "Perfil Creado correctamente")
                End If

                pnlPerfil.Visible = False
                pnlPerfiles.Visible = True
                gridPerfiles.Rebind()
            Catch ex As Exception
                showModal(RadNotification1, "deny", "Error de sistema", ex.Message)
            End Try
        End If
    End Sub

    Private Sub gridPerfiles_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles gridPerfiles.ItemCommand
        If e.CommandName = "Edit" Then
            Dim item As GridDataItem = CType(e.Item, GridDataItem)
            Dim idCell As GridTableCell = CType(item.Controls(4), GridTableCell)
            Dim perfilCell As GridTableCell = CType(item.Controls(5), GridTableCell)
            Session("Actualizar") = True
            Session("idPerfil") = idCell.Text
            pnlPerfil.Visible = True
            pnlPerfiles.Visible = False
            llenarCombo(comboAdmin, 2, idCell.Text)
            llenarCombo(comboAGestion, 0, idCell.Text)
            llenarCombo(comboPGestion, 1, idCell.Text)
            llenarCombo(comboJudicial, 6, idCell.Text)
            llenarCombo(comboBackOffice, 4, idCell.Text)
            llenarCombo(comboReportes, 3, idCell.Text)
            llenarCombo(comboMovil, 5, idCell.Text)
            txtNombrePerfil.Text = perfilCell.Text
            txtNombrePerfil.ReadOnly = True

            e.Canceled = True
        ElseIf e.CommandName = "InitInsert" Then
            pnlPerfil.Visible = True
            pnlPerfiles.Visible = False
            Session("Actualizar") = False
            llenarCombo(comboAdmin, 2, 0)
            llenarCombo(comboAGestion, 0, 0)
            llenarCombo(comboPGestion, 1, 0)
            llenarCombo(comboJudicial, 6, 0)
            llenarCombo(comboBackOffice, 4, 0)
            llenarCombo(comboReportes, 3, 0)
            llenarCombo(comboMovil, 5, 0)
            txtNombrePerfil.ReadOnly = False
            txtNombrePerfil.Text = ""
            txtNombrePerfil.Focus()
            e.Canceled = True

        ElseIf e.CommandName = "Eliminar" Then
            Dim item As GridItem = e.Item
            Try
                Dim ds As DataTable = SP.ADD_PERFILES(7, "", "", "", "", "", "", "", "", item.Cells(4).Text, 0)
                If ds.TableName = "Exception" Then
                    Throw New Exception(ds.Rows(0).Item(0).ToString)
                End If
                showModal(RadNotification1, "ok", "Correcto", "Perfil " & item.Cells(4).Text & " Eliminado")
                AUDITORIA(TmpUSUARIO("CAT_LO_USUARIO"), "Administrador", "Perfiles", "", "Eliminar perfil", e.Item.Cells(5).Text, "", "")
                gridPerfiles.Rebind()
                e.Canceled = True
            Catch ex As Exception
                showModal(RadNotification1, "delete", "Error", ex.Message)
            End Try
        End If
    End Sub

    Private Sub gridPerfiles_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles gridPerfiles.NeedDataSource

        gridPerfiles.DataSource = SP.ADD_PERFILES(1, "", "", "", "", "", "", "", "", 0, 0)

    End Sub

    ''' <summary>
    ''' Llena el combo de permisos
    ''' </summary>
    ''' <param name="combo">RadComboBox que se va a llenar</param>
    ''' <param name="modulo">0:Permisos gestion - 1:pantallas gestion - 2:pantallas admin - 3:pantallas reportes - 4:pantallas backoffice - 5: pantallas movil - 6:pantallas judicial</param>
    ''' <param name="perfil">0:Platilla vacia - otro:Plantilla del perfil y modulo solicitado</param>
    Private Sub llenarCombo(combo As RadComboBox, modulo As Integer, perfil As Integer)
        combo.Items.Clear()
        Dim dt As DataTable = SP.ADD_PERFILES(2, "", "", "", "", "", "", "", "", perfil, modulo)
        For Each row As DataRow In dt.Rows()
            Dim item As RadComboBoxItem = New RadComboBoxItem
            item.Text = row("PERMISO").ToString.Replace("_", " ")
            item.Value = row("PERMISO").ToString
            item.Enabled = IIf(row("CAT_PE_ENABLE").ToString = "1", True, False)
            If perfil <> 0 Then
                item.Checked = IIf(row("VALOR").ToString = "1", True, False)

            End If
            combo.Items.Add(item)
        Next
    End Sub



    Private Function comboToString(combo As RadComboBox) As String
        Dim permisosChecked As String = ""
        For Each item As RadComboBoxItem In combo.Items
            permisosChecked &= IIf(item.Checked, "1", "0")
        Next
        Return permisosChecked
    End Function

    Private Sub RdlInstancia_SelectedIndexChanged(sender As Object, e As DropDownListEventArgs) Handles RdlInstancia.SelectedIndexChanged
        If RdlInstancia.SelectedValue = "Ambas" Then
            PnlAmbas.Visible = True
            HonorarioVariable.Text = "Porcentaje de honorarios Instancia Judicial"
        Else
            PnlAmbas.Visible = False
            HonorarioVariable.Text = "Porcentaje de honorarios"
            NtxtPuestaCorriente2.Text = ""
            NtxtLiquidaciones2.Text = ""
            NtxtPagosParciales2.Text = ""
            NtxtReestructuras2.Text = ""
        End If
    End Sub

    Private Sub btnCancelarCondonacion_Click(sender As Object, e As EventArgs) Handles btnCancelarCondonacion.Click
        'limpiarCondonacion()
        pnlCondonaciones.Visible = False
        pnlPerfiles.Visible = True
    End Sub

End Class
