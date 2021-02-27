Imports System.Web.Services
Imports Telerik.Web.UI
Imports System.Data
Imports Funciones
Imports Class_CatalogoDispersion
Imports System.Net
Imports System.IO
Imports Newtonsoft.Json
Imports RestSharp

Partial Class CatalogoDispercion
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



    Private Sub CatalogoDispercion_Load(sender As Object, e As EventArgs) Handles Me.Load

        If tmpPermisos("DISPERSION") = False Then
            Session.Clear()
            Session.Abandon()
            Response.Redirect("../SesionExpirada.aspx")
        Else
            Try
                If Not IsPostBack Then
                    'Dim Valores As ArrayList = New ArrayList()
                    'Valores.Add(New ListItem("Seleccione", "Seleccione"))
                    'Valores.Add(New ListItem("Domiciliacion", 1))
                    'Valores.Add(New ListItem("Nomina", 2))
                    'Valores.Add(New ListItem("Extrajudicial", 3))
                    Dim Valores As DataTable = Class_CatalogoGlobales.DatosComboBox(15, Nothing)
                    DDLInstancia.DataSource = Valores
                    DDLInstancia.DataValueField = "Value"
                    DDLInstancia.DataTextField = "Text"
                    DDLInstancia.DataBind()
                    RGDispersiones.Rebind()
                End If
            Catch ex As Exception
                Dim abc As String = ex.Message
        End Try

        End If
    End Sub
    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Administrador", "CatalogoDispersion.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub
    Protected Sub Aviso(ByVal MSJ As String)
        RadAviso.RadAlert(MSJ.Replace(Chr(10), "").Replace(Chr(13), "").Replace("""", "").Replace("'", ""), 440, 155, "AVISO", Nothing)
    End Sub

    'Private Sub DDLDispersion_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles DDLDispersion.SelectedIndexChanged
    '    If DDLDispersion.SelectedValue = 1 Then
    '        btnSimularDispersion.Visible = False
    '        gridAsignacion.Visible = False
    '        PnlDatos.Visible = True
    '        Session("DispDUMMY") = InsertarDispersiondummy()
    '        gridDispersion.Rebind()
    '        RCBUsuarios.DataTextField = "cat_lo_nombre"
    '        RCBUsuarios.DataValueField = "cat_lo_usuario"
    '        RCBUsuarios.DataSource = TraerUsuarios(DDLInstancia.SelectedValue)
    '        RCBUsuarios.DataBind()
    '    End If
    '    'Try
    '    '    seleccionarUsuarios()
    '    'Catch ex As Exception
    '    'End Try
    'End Sub

    Private Sub seleccionarUsuarios()
        Dim usrs As String = TraerUsuariosDispersion(DDLInstancia.SelectedValue, ddlsubclasificacion.SelectedValue, rDdlInterno.SelectedValue)(0)(0).ToString
        For Each usr As String In usrs.Split(",")
            Try
                RCBUsuarios.FindItemByText(usr).Checked = True
            Catch ex As Exception
            End Try
        Next

        'If RCBUsuarios.CheckedItems.Count > 0 Then
        '    btnSimularDispersion.Visible = True
        'End If
    End Sub
    Private Sub seleccionarUsuarios2()
        Dim usrs As String = TraerUsuariosDispersion(DDLInstancia.SelectedValue, ddlsubclasificacion.SelectedValue, rDdlInterno.SelectedValue)(0)(0).ToString
        For Each usr As String In usrs.Split(",")
            Try
                RCBUsuarios2.FindItemByText(usr).Checked = True
            Catch ex As Exception
            End Try
        Next

        'If RCBUsuario2s.CheckedItems.Count > 0 Then
        '    btnSimularDispersion.Visible = True
        'End If
    End Sub

    Private Sub DDLInstancia_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles DDLInstancia.SelectedIndexChanged
        PnlDatos.Visible = False
        gridAsignacion.Visible = False
        Funciones.LLENAR_DROP2(32, DDLInstancia.SelectedValue, rDdlInterno, "V_VALOR", "T_VALOR")
        rDdlInterno.ClearSelection()
        rDdlInterno.Enabled = True
        'DDLDispersion.ClearSelection()
        'DDLDispersion.Enabled = False
    End Sub

    Private Sub rDdlInterno_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rDdlInterno.SelectedIndexChanged

        gridAsignacion.Visible = False
        btnSimularDispersion.Visible = False
        PnlDatos.Visible = True
            Session("DispDUMMY") = InsertarDispersiondummy()
            gridDispersion.Rebind()
            RCBUsuarios.DataTextField = "cat_lo_nombre"
            RCBUsuarios.DataValueField = "cat_lo_usuario"
        RCBUsuarios.DataSource = TraerUsuarios(DDLInstancia.SelectedValue, rDdlInterno.SelectedValue)
        RCBUsuarios.DataBind()

        If rDdlInterno.SelectedValue = 1 Then
            RCBUsuarios2.Enabled = True
            RCBUsuarios2.DataTextField = "cat_lo_nombre"
            RCBUsuarios2.DataValueField = "cat_lo_usuario"
            RCBUsuarios2.DataSource = TraerUsuarios(DDLInstancia.SelectedValue, rDdlInterno.SelectedValue)
            RCBUsuarios2.DataBind()
            RCBUsuarios2.EmptyMessage = "Selecciona al menos un usuario"
        Else
            RCBUsuarios2.Enabled = False
            RCBUsuarios2.ClearCheckedItems()
            RCBUsuarios2.DataSource = Nothing
            RCBUsuarios2.DataBind()
            RCBUsuarios2.EmptyMessage = "No aplica"
        End If

        'Try
        '    seleccionarUsuarios()
        'Catch ex As Exception
        'End Try
    End Sub

    Private Sub gridDispersion_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles gridDispersion.NeedDataSource
        Dim DtsDatos As DataTable = TraeDispersion(Session("DispDUMMY"))
        gridDispersion.DataSource = DtsDatos
    End Sub

    Private Sub gridDispersion_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles gridDispersion.ItemCommand
        Dim comando As String = e.CommandName
        If comando = "PerformInsert" Then
            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)

            Dim valores As Hashtable = Reglas.getGridValues(MyUserControl)

            Dim DtsDatos As DataTable = InsertarParametro(DDLInstancia.SelectedValue, ddlsubclasificacion.SelectedValue, rDdlInterno.SelectedValue, Nothing, valores("operadorText"), valores("conectorText"), valores("tablaText"), valores("campoText"), valores("valor"), valores("campoValue"), valores("tablaValue"), valores("operadorValue"), valores("conectorValue"), valores("tipo"), Session("DispDUMMY"))
            Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogo dispersión", "Insertar dispersion: " & DDLInstancia.SelectedValue & "|" & ddlsubclasificacion.SelectedValue & "|" & rDdlInterno.SelectedValue & "|" & Nothing & "|" & valores("operadorText") & "|" & valores("conectorText") & "|" & valores("tablaText") & "|" & valores("campoText") & "|" & valores("valor") & "|" & valores("campoValue") & "|" & valores("tablaValue") & "|" & valores("operadorValue") & "|" & valores("conectorValue") & "|" & valores("tipo") & "|" & Session("DispDUMMY"))
            gridDispersion.Rebind()
            Aviso("Dispersion Actualizada")
        ElseIf comando = "Edit" Then
            Session("Edit") = True
        ElseIf comando = "Update" Then
            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores As Hashtable = Reglas.getGridValues(MyUserControl)

            ActualizarParametro(valores("operadorText"), valores("conectorText"), valores("tablaText"), valores("campoText"), valores("valor"), valores("campoValue"), valores("tablaValue"), valores("operadorValue"), valores("conectorValue"), valores("tipo"), Session("DispDUMMY"), valores("consecutivo"))
            asignaUsusarios()
            Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogo dispersión", "Actualizar dispersion: " & valores("operadorText") & "|" & valores("conectorText") & "|" & valores("tablaText") & "|" & valores("campoText") & "|" & valores("valor") & "|" & valores("campoValue") & "|" & valores("tablaValue") & "|" & valores("operadorValue") & "|" & valores("conectorValue") & "|" & valores("tipo") & "|" & Session("DispDUMMY") & "|" & valores("consecutivo"))
            gridDispersion.Rebind()
            Aviso("Dispersion Actualizada")
        ElseIf comando = "onDelete" Then
            Dim valores(7) As String
            valores(0) = DDLInstancia.SelectedValue
            valores(1) = rDdlInterno.SelectedValue
            valores(2) = e.Item.Cells.Item(4).Text
            valores(3) = e.Item.Cells.Item(5).Text
            valores(4) = e.Item.Cells.Item(6).Text
            valores(5) = e.Item.Cells.Item(7).Text
            valores(6) = e.Item.Cells.Item(8).Text
            BorrarParametro(valores(0), ddlsubclasificacion.SelectedValue, valores(1), valores(4), valores(6), valores(2), valores(3), valores(5), Session("DispDUMMY"))
            Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogo dispersión", "Borrar dispersion: " & valores(0) & "|" & ddlsubclasificacion.SelectedValue & "|" & valores(1) & "|" & valores(4) & "|" & valores(6) & "|" & valores(2) & "|" & valores(3) & "|" & valores(5) & "|" & Session("DispDUMMY"))
            gridDispersion.Rebind()
            Aviso("Dispersion Actualizada")
        End If
    End Sub

    Private Sub btnAcpetarBorrar_Click(sender As Object, e As EventArgs) Handles btnAcpetarBorrar.Click
        ScriptManager.RegisterStartupScript(Me, GetType(Page), "modal", "<script>document.getElementById('modalDelete').style.display='none'</script>", False)
        'Borra la dispersion anterior
        BorrarDispersion(DDLInstancia.SelectedValue, ddlsubclasificacion.SelectedValue, rDdlInterno.SelectedValue)
        'Se inserta la nueva dispersion
        InsertarDispersion(DDLInstancia.SelectedValue, ddlsubclasificacion.SelectedValue, rDdlInterno.SelectedValue, Nothing)
        Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogo dispersión", "Borrar dispersion: " & DDLInstancia.SelectedValue & "|" & ddlsubclasificacion.SelectedValue & "|" & rDdlInterno.SelectedValue & "|" & DDLInstancia.SelectedValue & "|" & ddlsubclasificacion.SelectedValue & "|" & rDdlInterno.SelectedValue & "|" & Nothing)
        Aviso("Dispersión actualizada")
        'btnSimularDispersion.Visible = True
    End Sub

    Private Sub btnCancelarBorrar_Click(sender As Object, e As EventArgs) Handles btnCancelarBorrar.Click
        ScriptManager.RegisterStartupScript(Me, GetType(Page), "modal", "<script>document.getElementById('modalDelete').style.display='none'</script>", False)
        'DDLDispersion.ClearSelection()
    End Sub

    Private Sub btnSimularDispersion_Click(sender As Object, e As EventArgs) Handles btnSimularDispersion.Click
        Try
            Dim tabla As DataTable = SimularAsignacion(Session("DispDUMMY"), DDLInstancia.SelectedValue)
            If tabla.TableName = "Exception" Then
                Aviso(tabla.Rows(0)(0))
            Else
                Session("simulacion") = tabla
                Aviso("Simulación Terminada")
                gridAsignacion.Visible = True
                gridAsignacion.Rebind()
                btnAplicarAsignacion.Visible = True
                btnSimularDispersion.Enabled = False
                'gridAsignacion.MasterTableView.ExportToCSV()
            End If
        Catch ex As Exception
            Aviso(ex.Message)
        End Try
    End Sub

    Private Sub ddlsubclasificacion_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles ddlsubclasificacion.SelectedIndexChanged
        PnlDatos.Visible = False
        'DDLDispersion.ClearSelection()
        'DDLDispersion.Enabled = True
    End Sub

    Sub asignaUsusarios()
        gridAsignacion.Visible = False
        If RCBUsuarios.CheckedItems.Count = 0 And RCBUsuarios2.CheckedItems.Count Then
            btnSimularDispersion.Visible = False
        ElseIf gridDispersion.Items.Count = 0 Then
            Aviso("Imposible Guardar Usuarios. Inserte Datos De Parametrización")
            btnSimularDispersion.Visible = False
        Else
            Dim v_usuarios As String = ""
            For Each item As RadComboBoxItem In RCBUsuarios.CheckedItems
                v_usuarios &= item.Value & ","
            Next
            Try
                v_usuarios = v_usuarios.Substring(0, v_usuarios.Length - 1)
            Catch ex As Exception
            End Try
            Dim v_usuarios2 As String = ""
            For Each item As RadComboBoxItem In RCBUsuarios2.CheckedItems
                v_usuarios2 &= item.Value & ","
            Next
            Try
                v_usuarios2 = v_usuarios2.Substring(0, v_usuarios2.Length - 1)
            Catch ex As Exception
            End Try
            GuardarUsuarios(DDLInstancia.SelectedValue, ddlsubclasificacion.SelectedValue, rDdlInterno.SelectedValue, v_usuarios, Session("DispDUMMY"), v_usuarios2)
            Aviso("Usuarios Guardados Correctamente")
            'btnSimularDispersion.Visible = True
        End If
    End Sub
    Private Sub RCBUsuarios_TextChanged(sender As Object, e As EventArgs) Handles RCBUsuarios.TextChanged
        asignaUsusarios()
    End Sub
    Private Sub RCBUsuarios2_TextChanged(sender As Object, e As EventArgs) Handles RCBUsuarios2.TextChanged
        asignaUsusarios()
    End Sub

    Private Sub gridAsignacion_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles gridAsignacion.NeedDataSource
        gridAsignacion.DataSource = Session("simulacion")
    End Sub

    Private Sub btnAplicarAsignacion_Click(sender As Object, e As EventArgs) Handles btnAplicarAsignacion.Click
        'Dim resultado As String = AplicarAsignacion((CType(Session("USUARIO"), USUARIO)).CAT_LO_USUARIO)(0)(0).ToString
        'If resultado = "ASIGNACION APLICADA" Then
        '    Dim dtscarga As DataTable = TraerAsignacionparaWS()
        '    'Safi_asignacionCarteraCobranzaMasivo(dtscarga)
        '    Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogo dispersión", "Asignacion aplicada manualmente")
        'End If
        'Aviso(resultado)
        'btnAplicarAsignacion.Visible = False
        'btnSimularDispersion.Enabled = True






        Aviso("ASIGNACION APLICADA")
        btnAplicarAsignacion.Visible = False
        'btnSimularDispersion.Enabled = True

    End Sub



    Protected Sub CheckBox0_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkTest As CheckBox = DirectCast(sender, CheckBox)
        If chkTest.Checked = True Then
            Dim grdRow As GridDataItem = DirectCast(chkTest.NamingContainer, GridDataItem)
            Dim idDispersion As Integer = grdRow.Cells(3).Text 'grdRow.Item("ID").Text
            Dim dispersiones As DataTable = TraeDispersion(idDispersion.ToString)
            Session("DispDUMMY") = idDispersion.ToString
            RBCancelar.Visible = True
            RGDispersiones.Visible = False
            PnlInstancia.Visible = True
            DDLInstancia.Visible = True
            PnlResp.Visible = True
            rDdlInterno.Visible = True
            'PnlDisp.Visible = True
            'DDLDispersion.Visible = True
            DDLInstancia.SelectedValue = dispersiones.Rows(0).Item("CAT_DIS_INSTANCIA").ToString
            Funciones.LLENAR_DROP2(32, DDLInstancia.SelectedValue, rDdlInterno, "V_VALOR", "T_VALOR")
            DDLInstancia.Enabled = False
            rDdlInterno.SelectedValue = If(grdRow.Cells(6).Text = "Externo", 0, 1) 'dispersiones.Rows(0).Item("CAT_DIS_RESPGESTION").ToString
            rDdlInterno.Enabled = True
            'DDLDispersion.SelectedValue = dispersiones.Tables(0).Rows(0).Item("CAT_DIS_DISPERSION").ToString
            'DDLDispersion.Enabled = True
            PnlDatos.Visible = True
            PNLDispersiones.Visible = False
            gridDispersion.DataSource = dispersiones
            gridDispersion.DataBind()
            Dim usuarios As DataTable = TraeUsuarios(idDispersion.ToString)
            RCBUsuarios.DataTextField = "cat_lo_nombre"
            RCBUsuarios.DataValueField = "cat_lo_usuario"
            RCBUsuarios.DataSource = TraerUsuarios(DDLInstancia.SelectedValue, rDdlInterno.SelectedValue)
            RCBUsuarios.DataBind()
            For Each algo As RadComboBoxItem In RCBUsuarios.Items
                Dim dato As RadComboBoxItem = DirectCast(algo, RadComboBoxItem)
                For x As Integer = 0 To usuarios.Rows.Count - 1
                    If dato.Value = usuarios.Rows(x).Item("USUARIOS").ToString Then
                        dato.Checked = True
                        'btnSimularDispersion.Visible = True
                    End If
                Next
            Next
            Dim usuarios2 As DataTable = TraeUsuarios2(idDispersion.ToString)
            RCBUsuarios2.DataTextField = "cat_lo_nombre"
            RCBUsuarios2.DataValueField = "cat_lo_usuario"
            RCBUsuarios2.DataSource = TraerUsuarios(DDLInstancia.SelectedValue, rDdlInterno.SelectedValue)
            RCBUsuarios2.DataBind()
            For Each algo2 As RadComboBoxItem In RCBUsuarios2.Items
                Dim dato2 As RadComboBoxItem = DirectCast(algo2, RadComboBoxItem)
                For x As Integer = 0 To usuarios2.Rows.Count - 1
                    If dato2.Value = usuarios2.Rows(x).Item("USUARIOS").ToString Then
                        dato2.Checked = True
                        'btnSimularDispersion.Visible = True
                    End If
                Next
            Next

        End If
    End Sub

    Private Sub RBCancelar_Click(sender As Object, e As EventArgs) Handles RBCancelar.Click
        RBCancelar.Visible = False
        PnlInstancia.Visible = False
        PnlResp.Visible = False
        'PnlDisp.Visible = False
        PnlDatos.Visible = False
        btnSimularDispersion.Visible = False
        PNLDispersiones.Visible = True
        Response.Redirect("CatalogoDispercion.aspx")
        gridAsignacion.Visible = False
        btnAplicarAsignacion.Visible = False
    End Sub

    Private Sub RGDispersiones_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RGDispersiones.NeedDataSource
        Dim dts As DataTable = TraerDispersiones(tmpUSUARIO("CAT_LO_INSTANCIA"))
        RGDispersiones.DataSource = dts
    End Sub

    Private Sub RGDispersiones_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RGDispersiones.ItemCommand
        Dim comando As String = e.CommandName
        If comando = "InitInsert" Then
            PNLDispersiones.Visible = False
            PnlInstancia.Visible = True
            DDLInstancia.Visible = True
            PnlResp.Visible = True
            rDdlInterno.Visible = True
            'PnlDisp.Visible = True
            'DDLDispersion.Visible = True
            RBCancelar.Visible = True
            DDLInstancia.Enabled = True
        ElseIf comando = "Delete" Then
            Dim id As String = e.Item.Cells(3).Text
            BorrarDispersion(id)
            Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogo dispersión", "Borrar dispersion: " & id)
            Aviso("Dispersión eliminada correctamente")
        End If
    End Sub

    Private Sub gridAsignacion_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles gridAsignacion.ItemCommand
        Session("PostBack") = True
        If e.CommandName = "Update" Then
            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(1) As String
            valores(0) = CType(MyUserControl.FindControl("DdlUsuario"), RadComboBox).Text
            Class_CatalogoDispersion.CambiarAsignacion(Session("CreditoACambiar"), valores(0))
            Dim tabla As DataTable = TraerAsignacion(Session("DispDUMMY"))
            Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogo dispersión", "Cambiar asignación: " & Session("CreditoACambiar") & "|" & valores(0))
            Session("simulacion") = tabla
            gridAsignacion.Rebind()
            Aviso("Credito Reasignado")
        ElseIf e.CommandName = "Edit" Then
            Session("CreditoACambiar") = e.Item.Cells.Item(3).Text
        End If
    End Sub

    Private Sub eliminarDispersion(id As Integer)

    End Sub
    Public Shared Function Rad_Tcadena(ByRef v_item As RadComboBox) As String
        Dim v_cadena As String = "'"
        Dim collection As IList(Of RadComboBoxItem) = v_item.CheckedItems

        For Each item As RadComboBoxItem In collection
            v_cadena = v_cadena & item.Value & "','"
        Next

        If collection.Count = 0 Then
            v_cadena = ""
        Else
            v_cadena = v_cadena.Substring(0, Len(v_cadena) - 2)
        End If

        Return v_cadena
    End Function

    Public Shared Function Rad_Ncadena(ByRef v_item As RadComboBox) As String
        Dim v_cadena As String = "'"
        Dim collection As IList(Of RadComboBoxItem) = v_item.CheckedItems

        For Each item As RadComboBoxItem In collection
            v_cadena = v_cadena & item.Value & ","
        Next

        If collection.Count = 0 Then
            v_cadena = ""
        Else
            v_cadena = v_cadena.Substring(0, Len(v_cadena) - 1)
        End If

        Return v_cadena
    End Function

End Class