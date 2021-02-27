Imports System.Data
Imports System.Data.OracleClient
Imports Db
Imports Funciones
Imports System.IO
Imports Telerik.Web.UI

Partial Class CatalogosJudiciales
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
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If tmpPermisos("CATALOGOS_JUDICIALES") = False Then
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
                HidenUrs.Value = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
                Session("gridAnterior") = Nothing
            End If
        Catch ex As Exception
            SendMail("Page_Load", ex, "", "", HidenUrs.Value)
        End Try
    End Sub
    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Administrador", "CatalogosJudiciales.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub
    Protected Sub Aviso(ByVal MSJ As String)
        RadAviso.RadAlert(MSJ, 440, 155, "AVISO", Nothing)
    End Sub
    Protected Sub Confirma(ByVal MSJ As String)
        RadAviso.RadConfirm(MSJ, "confirmCallbackFn", 440, 155, Nothing, "Alerta")

    End Sub

    Protected Sub BtnAceptarConfirmacion_Click(sender As Object, e As EventArgs) Handles BtnAceptarConfirmacion.Click
        Dim TmpUsr As USUARIO = CType(Session("USUARIOADMIN"), USUARIO)

    End Sub
    Protected Sub DdlTipo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlTipo.SelectedIndexChanged
        Session("Tipo") = ""
        If DdlTipo.SelectedValue <> "" Then
            mostrar()

            Dim dtscatalogos As DataTable = Class_Judicial.llenarcatalogojudicial(DdlTipo.SelectedValue)
            'Dim dtscatalogos As datatable = Class_Judicial.Catalogos(DdlTipo.SelectedValue, 0)
            Session("Tipo") = DdlTipo.SelectedValue
            If dtscatalogos.Rows.Count = 0 Then
                Aviso("No existen datos para el catalogo seleccionado")
                llenarcatalogos()
            Else
                llenarcatalogos()
                'For Each item As GridDataItem In RGVCatalogosJudiciales.MasterTableView.Items
                '    item.Edit = False
                'Next

                'RGVCatalogosJudiciales.Rebind()
            End If
        Else
            mostrar()
        End If
    End Sub
    Private Sub mostrar()
        Select Case Session("gridAnterior")
            Case "CAT_ETAPAS_JUDICIALES"
                RGVEtapas.Visible = False
            Case "CAT_PROMOCIONES_JUDICIALES"
                RGVPromociones.Visible = False
            Case "CAT_RESULTADOS_JUDICIALES"
                RGVResultados.Visible = False
            Case "CAT_TIPOS_JUICIOS"
                gridTipoJuicio.Visible = False
            Case "CAT_DOCUMENTOS_BASE"
                RGVDocumentosBase.Visible = False
            Case "CAT_DILIGENCIAS"
                RGVDiligencias.Visible = False
            Case "CAT_JUZGADOS"
                RGVJuzgados.Visible = False
            Case "CAT_DIAS_INHABILES"
                RGVDiasInhabiles.Visible = False
            Case "CAT_CIERRE_JUICIOS"
                RGVCierreJuicios.Visible = False
            Case "CAT_TRAMITES_JUDICIALES"
                RGVTramitesJudiciales.Visible = False
            Case "CAT_CASTIGOS"
                RGVCastigo.Visible = False
            Case "CAT_MOTIVOS_CASTIGO"
                RGVMotivos.Visible = False
            Case "CAT_ETIQUETAS_JUDICIALES"
                RGVEtiquetas.Visible = False
            Case "CAT_ACUERDOS_JUDICIALES"
                RGVAcuerdos.Visible = False
            Case Else
                RGVEtapas.Visible = False
                gridTipoJuicio.Visible = False
                RGVDocumentosBase.Visible = False
                RGVDiligencias.Visible = False
                RGVJuzgados.Visible = False
                RGVDiasInhabiles.Visible = False
                RGVCierreJuicios.Visible = False
                RGVTramitesJudiciales.Visible = False
                RGVCastigo.Visible = False
                RGVMotivos.Visible = False
                RGVEtiquetas.Visible = False
                RGVPromociones.Visible = False
                RGVResultados.Visible = False
                RGVAcuerdos.Visible = False
        End Select
        Select Case DdlTipo.SelectedValue
            Case "CAT_ETAPAS_JUDICIALES"
                RGVEtapas.Visible = True
                Session("gridAnterior") = DdlTipo.SelectedValue
            Case "CAT_PROMOCIONES_JUDICIALES"
                RGVPromociones.Visible = True
                Session("gridAnterior") = DdlTipo.SelectedValue
            Case "CAT_RESULTADOS_JUDICIALES"
                RGVResultados.Visible = True
                Session("gridAnterior") = DdlTipo.SelectedValue
            Case "CAT_TIPOS_JUICIOS"
                gridTipoJuicio.Visible = True
                Session("gridAnterior") = DdlTipo.SelectedValue
            Case "CAT_DOCUMENTOS_BASE"
                RGVDocumentosBase.Visible = True
                Session("gridAnterior") = DdlTipo.SelectedValue
            Case "CAT_DILIGENCIAS"
                RGVDiligencias.Visible = True
                Session("gridAnterior") = DdlTipo.SelectedValue
            Case "CAT_JUZGADOS"
                RGVJuzgados.Visible = True
                Session("gridAnterior") = DdlTipo.SelectedValue
            Case "CAT_DIAS_INHABILES"
                RGVDiasInhabiles.Visible = True
                Session("gridAnterior") = DdlTipo.SelectedValue
            Case "CAT_CIERRE_JUICIOS"
                RGVCierreJuicios.Visible = True
                Session("gridAnterior") = DdlTipo.SelectedValue
            Case "CAT_TRAMITES_JUDICIALES"
                RGVTramitesJudiciales.Visible = True
                Session("gridAnterior") = DdlTipo.SelectedValue
            Case "CAT_CASTIGOS"
                RGVCastigo.Visible = True
                Session("gridAnterior") = DdlTipo.SelectedValue
            Case "CAT_MOTIVOS_CASTIGO"
                RGVMotivos.Visible = True
                Session("gridAnterior") = DdlTipo.SelectedValue
            Case "CAT_ETIQUETAS_JUDICIALES"
                RGVEtiquetas.Visible = True
                Session("gridAnterior") = DdlTipo.SelectedValue
            Case "CAT_ACUERDOS_JUDICIALES"
                RGVAcuerdos.Visible = True
                Session("gridAnterior") = DdlTipo.SelectedValue
        End Select

    End Sub
    Private Sub llenarcatalogos()
        Select Case DdlTipo.SelectedValue
            Case "CAT_ETAPAS_JUDICIALES"
                For Each item As GridDataItem In RGVEtapas.MasterTableView.Items
                    item.Edit = False
                Next
                RGVEtapas.Rebind()
            Case "CAT_PROMOCIONES_JUDICIALES"
                For Each item As GridDataItem In RGVPromociones.MasterTableView.Items
                    item.Edit = False
                Next
                RGVPromociones.Rebind()
            Case "CAT_RESULTADOS_JUDICIALES"
                For Each item As GridDataItem In RGVResultados.MasterTableView.Items
                    item.Edit = False
                Next
                RGVResultados.Rebind()
            Case "CAT_TIPOS_JUICIOS"
                For Each item As GridDataItem In gridTipoJuicio.MasterTableView.Items
                    item.Edit = False
                Next
                gridTipoJuicio.Rebind()
            Case "CAT_DOCUMENTOS_BASE"
                For Each item As GridDataItem In RGVDocumentosBase.MasterTableView.Items
                    item.Edit = False
                Next
                RGVDocumentosBase.Rebind()
            Case "CAT_DILIGENCIAS"
                For Each item As GridDataItem In RGVDiligencias.MasterTableView.Items
                    item.Edit = False
                Next
                RGVDiligencias.Rebind()
            Case "CAT_JUZGADOS"
                For Each item As GridDataItem In RGVJuzgados.MasterTableView.Items
                    item.Edit = False
                Next
                RGVJuzgados.Rebind()
            Case "CAT_DIAS_INHABILES"
                For Each item As GridDataItem In RGVDiasInhabiles.MasterTableView.Items
                    item.Edit = False
                Next
                RGVDiasInhabiles.Rebind()
            Case "CAT_CIERRE_JUICIOS"
                For Each item As GridDataItem In RGVCierreJuicios.MasterTableView.Items
                    item.Edit = False
                Next
                RGVCierreJuicios.Rebind()
            Case "CAT_TRAMITES_JUDICIALES"
                For Each item As GridDataItem In RGVTramitesJudiciales.MasterTableView.Items
                    item.Edit = False
                Next
                RGVTramitesJudiciales.Rebind()
            Case "CAT_CASTIGOS"
                For Each item As GridDataItem In RGVCastigo.MasterTableView.Items
                    item.Edit = False
                Next
                RGVCastigo.Rebind()
            Case "CAT_MOTIVOS_CASTIGO"
                For Each item As GridDataItem In RGVMotivos.MasterTableView.Items
                    item.Edit = False
                Next
                RGVMotivos.Rebind()
            Case "CAT_ETIQUETAS_JUDICIALES"
                For Each item As GridDataItem In RGVEtiquetas.MasterTableView.Items
                    item.Edit = False
                Next
                RGVEtiquetas.Rebind()
            Case "CAT_ACUERDOS_JUDICIALES"
                For Each item As GridDataItem In RGVAcuerdos.MasterTableView.Items
                    item.Edit = False
                Next
                RGVAcuerdos.Rebind()
        End Select
    End Sub
    'Protected Sub RGVCatalogosJudiciales_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RGVCatalogosJudiciales.NeedDataSource
    '    If DdlTipo.SelectedValue <> "" Then
    '        Dim dtscatalogos As datatable = Class_Judicial.Catalogos(DdlTipo.SelectedValue, 0)
    '        RGVCatalogosJudiciales.DataSource = dtscatalogos
    '        If DdlTipo.SelectedValue = "CAT_PROMOCIONES_JUDICIALES" Then
    '            RGVCatalogosJudiciales.Columns.FindByUniqueName("COMODIN").Visible = True
    '        Else
    '            RGVCatalogosJudiciales.Columns.FindByUniqueName("COMODIN").Visible = False
    '        End If
    '    End If
    'End Sub
    Protected Sub RGVEtapas_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RGVEtapas.NeedDataSource
        If DdlTipo.SelectedValue <> "" Then
            Dim dtscatalogos As DataTable = Class_Judicial.llenarcatalogojudicial(DdlTipo.SelectedValue)
            RGVEtapas.DataSource = dtscatalogos
        End If
    End Sub
    Protected Sub gridTipoJuicio_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles gridTipoJuicio.NeedDataSource
        If DdlTipo.SelectedValue <> "" And DdlTipo.SelectedValue <> "CAT_DIAS_INHABILES" Then
            Dim dtscatalogos As DataTable = Class_Judicial.llenarcatalogojudicial(DdlTipo.SelectedValue)
            gridTipoJuicio.DataSource = dtscatalogos
        End If
    End Sub
    Protected Sub RGVDiligencias_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RGVDiligencias.NeedDataSource
        If DdlTipo.SelectedValue <> "" And DdlTipo.SelectedValue <> "CAT_DIAS_INHABILES" Then
            Dim dtscatalogos As DataTable = Class_Judicial.llenarcatalogojudicial(DdlTipo.SelectedValue)
            RGVDiligencias.DataSource = dtscatalogos
        End If
    End Sub
    Protected Sub RGVDocumentosBase_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RGVDocumentosBase.NeedDataSource
        If DdlTipo.SelectedValue <> "" And DdlTipo.SelectedValue <> "CAT_DIAS_INHABILES" Then
            Dim dtscatalogos As DataTable = Class_Judicial.llenarcatalogojudicial(DdlTipo.SelectedValue)
            RGVDocumentosBase.DataSource = dtscatalogos
        End If
    End Sub
    Protected Sub RGVJuzgados_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RGVJuzgados.NeedDataSource
        If DdlTipo.SelectedValue <> "" And DdlTipo.SelectedValue <> "CAT_DIAS_INHABILES" Then
            Dim dtscatalogos As DataTable = Class_Judicial.llenarcatalogojudicial(DdlTipo.SelectedValue)
            RGVJuzgados.DataSource = dtscatalogos
        End If
    End Sub
    Protected Sub RGVDiasInhabiles_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RGVDiasInhabiles.NeedDataSource
        If DdlTipo.SelectedValue <> "" Then
            Dim dtscatalogos As DataTable = Class_Judicial.llenarcatalogojudicial(DdlTipo.SelectedValue)
            RGVDiasInhabiles.DataSource = dtscatalogos
        End If
    End Sub
    Protected Sub RGVCierreJuicios_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RGVCierreJuicios.NeedDataSource
        If DdlTipo.SelectedValue <> "" And DdlTipo.SelectedValue <> "CAT_DIAS_INHABILES" Then
            Dim dtscatalogos As DataTable = Class_Judicial.llenarcatalogojudicial(DdlTipo.SelectedValue)
            RGVCierreJuicios.DataSource = dtscatalogos
        End If
    End Sub
    Protected Sub RGVTramitesJudiciales_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RGVTramitesJudiciales.NeedDataSource
        If DdlTipo.SelectedValue <> "" And DdlTipo.SelectedValue <> "CAT_DIAS_INHABILES" Then
            Dim dtscatalogos As DataTable = Class_Judicial.llenarcatalogojudicial(DdlTipo.SelectedValue)
            RGVTramitesJudiciales.DataSource = dtscatalogos
        End If
    End Sub
    Protected Sub RGVCastigo_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RGVCastigo.NeedDataSource
        If DdlTipo.SelectedValue <> "" And DdlTipo.SelectedValue <> "CAT_DIAS_INHABILES" Then
            Dim dtscatalogos As DataTable = Class_Judicial.llenarcatalogojudicial(DdlTipo.SelectedValue)
            RGVCastigo.DataSource = dtscatalogos
        End If
    End Sub
    Protected Sub RGVMotivos_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RGVMotivos.NeedDataSource
        If DdlTipo.SelectedValue <> "" And DdlTipo.SelectedValue <> "CAT_DIAS_INHABILES" Then
            Dim dtscatalogos As DataTable = Class_Judicial.llenarcatalogojudicial(DdlTipo.SelectedValue)
            RGVMotivos.DataSource = dtscatalogos
        End If
    End Sub
    Protected Sub RGVEtiquetas_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RGVEtiquetas.NeedDataSource
        If DdlTipo.SelectedValue <> "" And DdlTipo.SelectedValue <> "CAT_DIAS_INHABILES" Then
            Dim dtscatalogos As DataTable = Class_Judicial.llenarcatalogojudicial(DdlTipo.SelectedValue)
            RGVEtiquetas.DataSource = dtscatalogos
        End If
    End Sub
    Protected Sub RGVPromociones_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RGVPromociones.NeedDataSource
        If DdlTipo.SelectedValue <> "" And DdlTipo.SelectedValue <> "CAT_DIAS_INHABILES" Then
            Dim dtscatalogos As DataTable = Class_Judicial.llenarcatalogojudicial(DdlTipo.SelectedValue)
            RGVPromociones.DataSource = dtscatalogos
        End If
    End Sub
    Protected Sub RGVResultados_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RGVResultados.NeedDataSource
        If DdlTipo.SelectedValue <> "" And DdlTipo.SelectedValue <> "CAT_DIAS_INHABILES" Then
            Dim dtscatalogos As DataTable = Class_Judicial.llenarcatalogojudicial(DdlTipo.SelectedValue)
            RGVResultados.DataSource = dtscatalogos
        End If
    End Sub
    Protected Sub RGVAcuerdos_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RGVAcuerdos.NeedDataSource
        If DdlTipo.SelectedValue <> "" And DdlTipo.SelectedValue <> "CAT_DIAS_INHABILES" Then
            Dim dtscatalogos As DataTable = Class_Judicial.llenarcatalogojudicial(DdlTipo.SelectedValue)
            RGVAcuerdos.DataSource = dtscatalogos
        End If
    End Sub

    'Private Sub RGVCatalogosJudiciales_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RGVCatalogosJudiciales.ItemCommand
    '    Session("Chck") = False
    '    If e.CommandName = "Update" Then

    '        Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
    '        Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
    '        Dim valores(3) As String

    '        valores(0) = CType(MyUserControl.FindControl("LblHLabel"), RadTextBox).Text
    '        valores(1) = CType(MyUserControl.FindControl("TxtCat_Descripcion"), RadTextBox).Text
    '        valores(2) = CType(MyUserControl.FindControl("ChkbxComodin"), RadCheckBox).Checked

    '        Try
    '            If valores(1) = "" Then
    '                Aviso("Favor de capturar un nombre valido")
    '            Else
    '                Dim dtsguardado As datatable = Class_Judicial.GuardarCatalogos(2, valores(0), DdlTipo.SelectedValue, valores(1), IIf(valores(2) = True, "1", "0"), "", "", "", "", "", "", "", "")
    '                Aviso(dtsguardado.Rows.Item(0)("MSJ").ToString)
    '            End If
    '            DdlTipo.Enabled = True
    '        Catch ex As Exception
    '            Aviso("No se realizo la actualizacion debido a: " & ex.Message)
    '        End Try
    '    ElseIf e.CommandName = "PerformInsert" Then

    '        Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
    '        Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
    '        Dim valores(3) As String

    '        valores(0) = CType(MyUserControl.FindControl("LblHLabel"), RadTextBox).Text
    '        valores(1) = CType(MyUserControl.FindControl("TxtCat_Descripcion"), RadTextBox).Text
    '        If DdlTipo.SelectedValue = "CAT_PROMOCIONES_JUDICIALES" Then
    '            valores(2) = CType(MyUserControl.FindControl("ChkbxComodin"), RadCheckBox).Checked
    '        Else
    '            valores(2) = False
    '        End If


    '        Try
    '            If valores(1) = "" Then
    '                Aviso("Favor de capturar un nombre valido")
    '            Else
    '                Dim dtsguardado As datatable = Class_Judicial.GuardarCatalogos(1, valores(0), DdlTipo.SelectedValue, valores(1), IIf(valores(2) = True, "1", "0"), "", "", "", "", "", "", "", "")

    '                Aviso(dtsguardado.Rows.Item(0)("MSJ").ToString)
    '            End If
    '            DdlTipo.Enabled = True
    '        Catch ex As Exception
    '            Aviso("No se realizo la insercion debido a: " & ex.Message)
    '        End Try
    '    ElseIf e.CommandName = "Edit" Then
    '        DdlTipo.Enabled = False
    '        If e.Item.Cells.Item(5).Text = "1" Then
    '            Session("Chck") = True
    '        End If
    '    ElseIf e.CommandName = "InitInsert" Then
    '        DdlTipo.Enabled = False
    '    ElseIf e.CommandName = "Cancel" Then
    '        DdlTipo.Enabled = True
    '    End If
    'End Sub
    Private Sub RGVMotivos_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RGVMotivos.ItemCommand
        Session("PostBack") = True
        If e.CommandName = "Update" Then

            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(4) As String

            valores(0) = CType(MyUserControl.FindControl("DdlTipo"), RadDropDownList).SelectedValue
            valores(1) = CType(MyUserControl.FindControl("TxtMotivo"), RadTextBox).Text
            valores(2) = CType(MyUserControl.FindControl("DdlEstatus"), RadDropDownList).SelectedValue

            Try
                If valores(1) = "" Then
                    Aviso("Favor de capturar un motivo valido")
                    e.Canceled = True
                Else
                    Dim dtsguardado As DataTable = Class_Judicial.EditarCatalogosJudicial(valores(0), valores(1), Session("VALORID"), valores(2), "", "", "", "", "", "", "", "", "", "", 25)
                    Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogos judiciales", "Etapas judiciales: " & valores(0) & "|" & valores(1) & "|" & Session("VALORID") & "|" & valores(2) & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "25")
                    Aviso(dtsguardado.Rows.Item(0)("MSJ").ToString)
                    DdlTipo.Enabled = True
                End If
            Catch ex As Exception
                e.Canceled = True
                DdlTipo.Enabled = False
                Aviso("No se realizo la actualizacion debido a: " & ex.Message)
            End Try
        ElseIf e.CommandName = "PerformInsert" Then

            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(4) As String

            valores(0) = CType(MyUserControl.FindControl("DdlTipo"), RadDropDownList).SelectedValue
            valores(1) = CType(MyUserControl.FindControl("TxtMotivo"), RadTextBox).Text
            valores(2) = CType(MyUserControl.FindControl("DdlEstatus"), RadDropDownList).SelectedValue

            Try
                If valores(1) = "" Then
                    e.Canceled = True
                    Aviso("Favor de capturar un nombre valido")
                Else
                    Dim dtsguardado As DataTable = Class_Judicial.EditarCatalogosJudicial(valores(0), valores(1), valores(2), "", "", "", "", "", "", "", "", "", "", "", 11)
                    Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogos judiciales", "Etapas judiciales: " & valores(0) & "|" & valores(1) & "|" & valores(2) & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "11")
                    DdlTipo.Enabled = True

                    Aviso(dtsguardado.Rows.Item(0)("MSJ").ToString)
                End If
            Catch ex As Exception
                e.Canceled = True
                DdlTipo.Enabled = False
                Aviso("No se realizo la insercion debido a: " & ex.Message)
            End Try
        ElseIf e.CommandName = "Edit" Then
            DdlTipo.Enabled = False
            Session("VALORID") = e.Item.Cells.Item(3).Text
            VALORID.Value = e.Item.Cells.Item(3).Text
        ElseIf e.CommandName = "InitInsert" Then
            DdlTipo.Enabled = False
            Session("PostBack") = False
        ElseIf e.CommandName = "Cancel" Then
            DdlTipo.Enabled = True
        End If
    End Sub
    Private Sub RGVJuzgados_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RGVJuzgados.ItemCommand
        Session("PostBack") = True
        Session("Mun") = "N"
        If e.CommandName = "Update" Then

            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(4) As String

            valores(0) = CType(MyUserControl.FindControl("TxtNombre"), RadTextBox).Text
            valores(1) = CType(MyUserControl.FindControl("DdlEstado"), RadDropDownList).SelectedValue
            valores(2) = CType(MyUserControl.FindControl("DdlMunicipio"), RadDropDownList).SelectedValue
            valores(3) = CType(MyUserControl.FindControl("DdlEstatus"), RadDropDownList).SelectedValue

            Try
                If valores(1) = "" Then
                    Aviso("Error al guardar. Favor de capturar un nombre valido")
                    e.Canceled = True

                Else
                    Dim dtsguardado As DataTable = Class_Judicial.EditarCatalogosJudicial(valores(0), valores(1), valores(2), Session("VALORID"), valores(3), "", "", "", "", "", "", "", "", "", 20)
                    Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogos judiciales", "Juzgados: " & valores(0) & "|" & valores(1) & "|" & valores(2) & "|" & Session("VALORID") & "|" & valores(3) & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "20")
                    Dim resultado() As String = dtsguardado.Rows.Item(0)("MSJ").ToString.Split(",")
                    DdlTipo.Enabled = True
                    If resultado(0) = "0" Then
                        Aviso("Error al guardar. " & resultado(1))
                        e.Canceled = True
                        DdlTipo.Enabled = False
                    End If
                    Aviso("Juzgado actualizado. " & resultado(1))
                End If
            Catch ex As Exception
                Aviso("Error de sistem. No se realizo la actualizacion debido a: " & ex.Message)
                DdlTipo.Enabled = False
                e.Canceled = True
            End Try
        ElseIf e.CommandName = "PerformInsert" Then

            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(4) As String

            valores(0) = CType(MyUserControl.FindControl("TxtNombre"), RadTextBox).Text
            valores(1) = CType(MyUserControl.FindControl("DdlEstado"), RadDropDownList).SelectedValue
            valores(2) = CType(MyUserControl.FindControl("DdlMunicipio"), RadDropDownList).SelectedValue
            valores(3) = CType(MyUserControl.FindControl("DdlEstatus"), RadDropDownList).SelectedValue

            Try
                If valores(1) = "" Then
                    Aviso("Error al guardar. Favor de capturar un nombre valido")
                    e.Canceled = True
                Else
                    Dim dtsguardado As DataTable = Class_Judicial.EditarCatalogosJudicial(valores(0), valores(1), valores(2), valores(3), "", "", "", "", "", "", "", "", "", "", 6)
                    Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogos judiciales", "Juzgados: " & valores(0) & "|" & valores(1) & "|" & valores(2) & "|" & valores(3) & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "6")
                    Dim resultado() As String = dtsguardado.Rows.Item(0)("MSJ").ToString.Split(",")
                    DdlTipo.Enabled = True
                    If resultado(0) = "0" Then
                        Aviso("Error al guardar" & resultado(1))
                        e.Canceled = True
                        DdlTipo.Enabled = False
                    Else
                        Aviso("EXITO. " & resultado(1))
                    End If
                End If
            Catch ex As Exception
                Aviso("Error de sistema. No se realizo la actualizacion debido a: " & ex.Message)
                e.Canceled = True
                DdlTipo.Enabled = False
            End Try
        ElseIf e.CommandName = "Edit" Then
            DdlTipo.Enabled = False
            Session("VALORID") = e.Item.Cells.Item(3).Text
            VALORID.Value = e.Item.Cells.Item(3).Text
        ElseIf e.CommandName = "InitInsert" Then
            DdlTipo.Enabled = False
            Session("PostBack") = False
        ElseIf e.CommandName = "Cancel" Then
            DdlTipo.Enabled = True
        End If
    End Sub
    Private Sub RGVDocumentosBase_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RGVDocumentosBase.ItemCommand
        Session("PostBack") = True
        If e.CommandName = "Update" Then

            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(5) As String

            valores(0) = CType(MyUserControl.FindControl("TxtNombre"), RadTextBox).Text
            valores(1) = CType(MyUserControl.FindControl("TxtDescripcion"), RadTextBox).Text
            valores(2) = CType(MyUserControl.FindControl("RDPFechaInicio"), RadDatePicker).SelectedDate
            valores(3) = CType(MyUserControl.FindControl("NtxtDias"), RadNumericTextBox).Text
            valores(4) = CType(MyUserControl.FindControl("DdlTipo"), RadDropDownList).SelectedValue

            Try
                If valores(0) = "" Or valores(1) = "" Or valores(2) = "" Or valores(3) = "" Then
                    e.Canceled = True
                    DdlTipo.Enabled = False
                    Aviso("Falta capturar al menos un dato")
                Else
                    Dim dtsguardado As DataTable = Class_Judicial.EditarCatalogosJudicial(valores(0), valores(1), valores(2), valores(3), valores(4), Session("VALORID"), "", "", "", "", "", "", "", "", 18)
                    Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogos judiciales", "Documentos base: " & valores(0) & "|" & valores(1) & "|" & valores(2) & "|" & valores(3) & "|" & valores(4) & "|" & Session("VALORID") & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "18")
                    Aviso(dtsguardado.Rows.Item(0)("MSJ").ToString)
                    DdlTipo.Enabled = True
                End If
            Catch ex As Exception
                e.Canceled = True
                DdlTipo.Enabled = False
                Aviso("No se realizo la actualizacion debido a: " & ex.Message)
            End Try
        ElseIf e.CommandName = "PerformInsert" Then

            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(5) As String

            valores(0) = CType(MyUserControl.FindControl("TxtNombre"), RadTextBox).Text
            valores(1) = CType(MyUserControl.FindControl("TxtDescripcion"), RadTextBox).Text
            valores(2) = CType(MyUserControl.FindControl("RDPFechaInicio"), RadDatePicker).SelectedDate
            valores(3) = CType(MyUserControl.FindControl("NtxtDias"), RadNumericTextBox).Text
            valores(4) = CType(MyUserControl.FindControl("DdlTipo"), RadDropDownList).SelectedValue

            Try
                If valores(0) = "" Or valores(1) = "" Or valores(2) = "" Or valores(3) = "" Then
                    Aviso("Falta capturar al menos un dato")
                    e.Canceled = True

                Else
                    Dim dtsguardado As DataTable = Class_Judicial.EditarCatalogosJudicial(valores(0), valores(1), valores(2), valores(3), valores(4), "", "", "", "", "", "", "", "", "", 4)
                    Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogos judiciales", "Documentos base: " & valores(0) & "|" & valores(1) & "|" & valores(2) & "|" & valores(3) & "|" & valores(4) & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "4")
                    Aviso(dtsguardado.Rows.Item(0)("MSJ").ToString)
                    DdlTipo.Enabled = True
                End If
            Catch ex As Exception
                e.Canceled = True
                DdlTipo.Enabled = False
                Aviso("No se realizo la insercion debido a: " & ex.Message)
            End Try
        ElseIf e.CommandName = "Edit" Then
            DdlTipo.Enabled = False
            Session("VALORID") = e.Item.Cells.Item(3).Text
            VALORID.Value = e.Item.Cells.Item(3).Text
        ElseIf e.CommandName = "InitInsert" Then
            DdlTipo.Enabled = False
            Session("PostBack") = False
        ElseIf e.CommandName = "Cancel" Then
            DdlTipo.Enabled = True
        End If
    End Sub
    Private Sub RGVEtiquetas_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RGVEtiquetas.ItemCommand
        Session("PostBack") = True
        If e.CommandName = "Update" Then

            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(6) As String

            valores(0) = CType(MyUserControl.FindControl("NtxtAncho"), RadNumericTextBox).Text
            valores(1) = CType(MyUserControl.FindControl("NtxtAlto"), RadNumericTextBox).Text
            valores(2) = CType(MyUserControl.FindControl("DdlTipoLetra"), RadDropDownList).SelectedValue
            valores(3) = CType(MyUserControl.FindControl("NtxtTamano1"), RadNumericTextBox).Text
            valores(4) = CType(MyUserControl.FindControl("NtxtTamano2"), RadNumericTextBox).Text
            valores(5) = CType(MyUserControl.FindControl("NtxtTamano3"), RadNumericTextBox).Text

            Try
                If valores(0) = "" Or valores(1) = "" Or valores(2) = "" Or valores(3) = "" Then
                    Aviso("Falta capturar al menos un dato")
                    e.Canceled = True
                Else
                    Dim dtsguardado As DataTable = Class_Judicial.EditarCatalogosJudicial(valores(0), valores(1), valores(2), valores(3), valores(4), valores(5), "", "", "", "", "", "", "", "", 26)
                    Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogos judiciales", "Etiquetas Judiciales: " & valores(0) & "|" & valores(1) & "|" & valores(2) & "|" & valores(3) & "|" & valores(4) & "|" & valores(4) & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "26")
                    Aviso(dtsguardado.Rows.Item(0)("MSJ").ToString)
                    DdlTipo.Enabled = True
                End If
            Catch ex As Exception
                e.Canceled = True
                DdlTipo.Enabled = False
                Aviso("No se realizo la actualizacion debido a: " & ex.Message)
            End Try
        ElseIf e.CommandName = "Edit" Then
            DdlTipo.Enabled = False
            Session("VALORID") = e.Item.Cells.Item(3).Text
            VALORID.Value = e.Item.Cells.Item(3).Text
        ElseIf e.CommandName = "Cancel" Then
            DdlTipo.Enabled = True
        End If
    End Sub
    Private Sub RGVDiasInhabiles_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RGVDiasInhabiles.ItemCommand
        Session("PostBack") = True
        If e.CommandName = "Update" Then

            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(4) As String

            valores(0) = CType(MyUserControl.FindControl("TxtNombre"), RadTextBox).Text
            valores(1) = CType(MyUserControl.FindControl("RDPFechaInicio"), RadDatePicker).SelectedDate
            valores(2) = CType(MyUserControl.FindControl("RDPFechaFin"), RadDatePicker).SelectedDate
            valores(3) = encadena(CType(MyUserControl.FindControl("DdlEstado"), RadComboBox))

            Try
                If valores(0) = "" Or valores(1) = "" Or valores(2) = "" Or valores(3) = "" Then
                    Aviso("Falta capturar al menos un dato")
                    e.Canceled = True
                Else
                    Dim dtsguardado As DataTable = Class_Judicial.EditarCatalogosJudicial(valores(0), valores(1), valores(2), valores(3), "", "", "", "", "", "", "", "", "", "", 21)
                    Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogos judiciales", "Días inhábiles: " & valores(0) & "|" & valores(1) & "|" & valores(2) & "|" & valores(3) & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "21")
                    Aviso(dtsguardado.Rows.Item(0)("MSJ").ToString)
                    DdlTipo.Enabled = True
                End If
            Catch ex As Exception
                DdlTipo.Enabled = False
                e.Canceled = True
                Aviso("No se realizo la actualizacion debido a: " & ex.Message)
            End Try
        ElseIf e.CommandName = "PerformInsert" Then
            Session("PostBack") = False
            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(4) As String

            valores(0) = CType(MyUserControl.FindControl("TxtNombre"), RadTextBox).Text
            valores(1) = CType(MyUserControl.FindControl("RDPFechaInicio"), RadDatePicker).SelectedDate
            valores(2) = CType(MyUserControl.FindControl("RDPFechaFin"), RadDatePicker).SelectedDate
            valores(3) = encadena(CType(MyUserControl.FindControl("DdlEstado"), RadComboBox))

            Try
                If valores(0) = "" Or valores(1) = "" Or valores(2) = "" Or valores(3) = "" Then
                    Aviso("Falta capturar al menos un dato")
                    e.Canceled = True
                Else
                    Dim dtsguardado As DataTable = Class_Judicial.EditarCatalogosJudicial(valores(0), valores(1), valores(2), valores(3), "", "", "", "", "", "", "", "", "", "", 7)
                    Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogos judiciales", "Días inhábiles: " & valores(0) & "|" & valores(1) & "|" & valores(2) & "|" & valores(3) & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "7")

                    Aviso(dtsguardado.Rows.Item(0)("MSJ").ToString)
                    DdlTipo.Enabled = True
                End If
            Catch ex As Exception
                DdlTipo.Enabled = False
                e.Canceled = True
                Aviso("No se realizo la insercion debido a: " & ex.Message)
            End Try
        ElseIf e.CommandName = "Edit" Then
            DdlTipo.Enabled = False
            Session("VALORID") = e.Item.Cells.Item(3).Text
            VALORID.Value = e.Item.Cells.Item(3).Text
        ElseIf e.CommandName = "InitInsert" Then
            DdlTipo.Enabled = False
            Session("PostBack") = False
        ElseIf e.CommandName = "Cancel" Then
            DdlTipo.Enabled = True
        End If
    End Sub
    Private Sub gridTipoJuicio_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles gridTipoJuicio.ItemCommand
        Session("PostBack") = True
        If e.CommandName = "Update" Then

            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(7) As String

            valores(0) = CType(MyUserControl.FindControl("TxtNombre"), RadTextBox).Text
            valores(1) = CType(MyUserControl.FindControl("TxtDescripcion"), RadTextBox).Text
            valores(2) = Session("tipodocumentos") 'Class_Judicial.Rad_Tcadena(CType(MyUserControl.FindControl("RCBTipoDocumentos"), RadComboBox))
            valores(3) = CType(MyUserControl.FindControl("NtxtDiasCaducidad"), RadNumericTextBox).Text
            valores(4) = CType(MyUserControl.FindControl("DdlTipoDiasCad"), RadDropDownList).SelectedValue
            valores(5) = CType(MyUserControl.FindControl("NtxtDiasPrescripcion"), RadNumericTextBox).Text
            valores(6) = CType(MyUserControl.FindControl("DdlTipoDiasPres"), RadDropDownList).SelectedValue

            Try
                If valores(0) = "" Or valores(1) = "" Or valores(2) = "" Or valores(3) = "" Or valores(4) = "" Or valores(5) = "" Or valores(6) = "" Then
                    Aviso("Falta capturar al menos un dato")
                    e.Canceled = True
                Else
                    Dim dtsguardado As DataTable = Class_Judicial.EditarCatalogosJudicial(valores(0), valores(1), valores(2), valores(3), valores(4), valores(5), valores(6), Session("VALORID"), "", "", "", "", "", "", 17)
                    Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogos judiciales", "Tipo juicio: " & valores(0) & "|" & valores(1) & "|" & valores(2) & "|" & valores(3) & "|" & valores(4) & "|" & valores(5) & "|" & valores(6) & "|" & Session("VALORID") & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "17")
                    Aviso(dtsguardado.Rows.Item(0)("MSJ").ToString)
                    DdlTipo.Enabled = True
                End If
            Catch ex As Exception
                DdlTipo.Enabled = False
                e.Canceled = True
                Aviso("No se realizo la actualizacion debido a: " & ex.Message)
            End Try
        ElseIf e.CommandName = "PerformInsert" Then

            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(7) As String

            valores(0) = CType(MyUserControl.FindControl("TxtNombre"), RadTextBox).Text
            valores(1) = CType(MyUserControl.FindControl("TxtDescripcion"), RadTextBox).Text
            valores(2) = Session("tipodocumentos") 'Class_Judicial.Rad_Tcadena(CType(MyUserControl.FindControl("RCBTipoDocumentos"), RadComboBox))
            valores(3) = CType(MyUserControl.FindControl("NtxtDiasCaducidad"), RadNumericTextBox).Text
            valores(4) = CType(MyUserControl.FindControl("DdlTipoDiasCad"), RadDropDownList).SelectedValue
            valores(5) = CType(MyUserControl.FindControl("NtxtDiasPrescripcion"), RadNumericTextBox).Text
            valores(6) = CType(MyUserControl.FindControl("DdlTipoDiasPres"), RadDropDownList).SelectedValue
            Session.Remove("tipodocumentos")
            Try
                If valores(0) = "" Or valores(1) = "" Or valores(2) = "" Or valores(3) = "" Or valores(4) = "" Or valores(5) = "" Or valores(6) = "" Then
                    Aviso("Falta capturar al menos un dato")
                    e.Canceled = True
                Else
                    Dim dtsguardado As DataTable = Class_Judicial.EditarCatalogosJudicial(valores(0), valores(1), valores(2), valores(3), valores(4), valores(5), valores(6), "", "", "", "", "", "", "", 3)
                    Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogos judiciales", "Tipo juicio: " & valores(0) & "|" & valores(1) & "|" & valores(2) & "|" & valores(3) & "|" & valores(4) & "|" & valores(5) & "|" & valores(6) & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "3")
                    Aviso(dtsguardado.Rows.Item(0)("MSJ").ToString)
                    DdlTipo.Enabled = True
                End If
            Catch ex As Exception
                DdlTipo.Enabled = False
                e.Canceled = True
                Aviso("No se realizo la insercion debido a: " & ex.Message)
            End Try
        ElseIf e.CommandName = "Edit" Then
            DdlTipo.Enabled = False
            Session("VALORID") = e.Item.Cells.Item(3).Text
            VALORID.Value = e.Item.Cells.Item(3).Text
        ElseIf e.CommandName = "InitInsert" Then
            DdlTipo.Enabled = False
            ' Session("PostBack") = False
        ElseIf e.CommandName = "Cancel" Then
            DdlTipo.Enabled = True
        ElseIf e.CommandName = "Desactivar" Then
            Class_Judicial.EditarCatalogosJudicial(e.Item.Cells(3).Text, 0, "", "", "", "", "", "", "", "", "", "", "", "", 39)
            Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogos judiciales", "Tipo juicio: " & e.Item.Cells(3).Text & "|" & "0" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "39")
            gridTipoJuicio.Rebind()
        ElseIf e.CommandName = "Activar" Then
            Class_Judicial.EditarCatalogosJudicial(e.Item.Cells(3).Text, 1, "", "", "", "", "", "", "", "", "", "", "", "", 39)
            Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogos judiciales", "Tipo juicio: " & e.Item.Cells(3).Text & "|" & "1" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "39")
            gridTipoJuicio.Rebind()
        End If
    End Sub
    Private Sub RGVEtapas_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RGVEtapas.ItemCommand
        Session("PostBack") = True
        If e.CommandName = "Update" Then

            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(4) As String

            valores(0) = CType(MyUserControl.FindControl("TxtNombre"), RadTextBox).Text
            valores(1) = Session("Juicios")
            'valores(1) = CType(MyUserControl.FindControl("DdlTipoJuicio"), RadDropDownList).SelectedValue
            valores(2) = CType(MyUserControl.FindControl("TxtDescripcion"), RadTextBox).Text
            valores(3) = CType(MyUserControl.FindControl("NtxtImporte"), RadNumericTextBox).Text


            Try
                If valores(0) = "" Or valores(1) = "" Or valores(2) = "" Or valores(3) = "" Then
                    Aviso("Falta capturar al menos un dato")
                    e.Canceled = True
                Else
                    Dim dtsguardado As DataTable = Class_Judicial.EditarCatalogosJudicial(valores(0), valores(1), valores(2), valores(3), Session("VALORID"), "", "", "", "", "", "", "", "", "", 14)
                    Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogos judiciales", "Etapas judiciales: " & valores(0) & "|" & valores(1) & "|" & valores(2) & "|" & valores(3) & "|" & Session("VALORID") & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "14")
                    Aviso(dtsguardado.Rows.Item(0)("MSJ").ToString)
                    DdlTipo.Enabled = True
                End If

            Catch ex As Exception
                e.Canceled = True
                DdlTipo.Enabled = False
                Aviso("No se realizo la actualizacion debido a: " & ex.Message)
            End Try
        ElseIf e.CommandName = "PerformInsert" Then

            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(4) As String

            valores(0) = CType(MyUserControl.FindControl("TxtNombre"), RadTextBox).Text
            valores(1) = Session("Juicios")
            'valores(1) = CType(MyUserControl.FindControl("DdlTipoJuicio"), RadDropDownList).SelectedValue
            valores(2) = CType(MyUserControl.FindControl("TxtDescripcion"), RadTextBox).Text
            valores(3) = CType(MyUserControl.FindControl("NtxtImporte"), RadNumericTextBox).Text


            Try
                If valores(0) = "" Or valores(1) = "" Or valores(2) = "" Or valores(3) = "" Then
                    Aviso("Falta capturar al menos un dato")
                    e.Canceled = True
                Else
                    Dim dtsguardado As DataTable = Class_Judicial.EditarCatalogosJudicial(valores(0), valores(1), valores(2), valores(3), "", "", "", "", "", "", "", "", "", "", 0)
                    Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogos judiciales", "Etapas judiciales: " & valores(0) & "|" & valores(1) & "|" & valores(2) & "|" & valores(3) & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "0")
                    Aviso(dtsguardado.Rows.Item(0)("MSJ").ToString)
                    DdlTipo.Enabled = True
                End If
            Catch ex As Exception
                e.Canceled = True
                DdlTipo.Enabled = False
                Aviso("No se realizo la insercion debido a: " & ex.Message)
            End Try
        ElseIf e.CommandName = "Edit" Then
            DdlTipo.Enabled = False
            Session("VALORID") = e.Item.Cells.Item(3).Text
            VALORID.Value = e.Item.Cells.Item(3).Text
        ElseIf e.CommandName = "InitInsert" Then
            DdlTipo.Enabled = False
            ' Session("PostBack") = False
        ElseIf e.CommandName = "Cancel" Then
            DdlTipo.Enabled = True
        End If
    End Sub
    Private Sub RGVResultados_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RGVResultados.ItemCommand
        Session("PostBack") = True
        If e.CommandName = "Update" Then

            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(3) As String

            valores(0) = CType(MyUserControl.FindControl("TxtNombre"), RadTextBox).Text
            valores(1) = CType(MyUserControl.FindControl("DdlCambiaEtapa"), RadDropDownList).SelectedValue
            valores(2) = CType(MyUserControl.FindControl("DdlDisminucion"), RadDropDownList).SelectedValue


            Try
                If valores(0) = "" Or valores(1) = "" Or valores(2) = "" Then
                    Aviso("Falta capturar al menos un dato")
                    e.Canceled = True
                Else
                    Dim dtsguardado As DataTable = Class_Judicial.EditarCatalogosJudicial(valores(0), valores(1), valores(2), Session("VALORID"), "", "", "", "", "", "", "", "", "", "", 16)
                    Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogos judiciales", "Resultados judiciales: " & valores(0) & "|" & valores(1) & "|" & valores(2) & "|" & Session("VALORID") & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "16")
                    Aviso(dtsguardado.Rows.Item(0)("MSJ").ToString)
                    DdlTipo.Enabled = True
                End If
            Catch ex As Exception
                DdlTipo.Enabled = False
                Aviso("No se realizo la actualizacion debido a: " & ex.Message)
                e.Canceled = True
            End Try
        ElseIf e.CommandName = "PerformInsert" Then

            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(3) As String

            valores(0) = CType(MyUserControl.FindControl("TxtNombre"), RadTextBox).Text
            valores(1) = CType(MyUserControl.FindControl("DdlCambiaEtapa"), RadDropDownList).SelectedValue
            valores(2) = CType(MyUserControl.FindControl("DdlDisminucion"), RadDropDownList).SelectedValue


            Try
                If valores(0) = "" Or valores(1) = "" Or valores(2) = "" Then
                    Aviso("Falta capturar al menos un dato")
                    e.Canceled = True
                Else
                    Dim dtsguardado As DataTable = Class_Judicial.EditarCatalogosJudicial(valores(0), valores(1), valores(2), "", "", "", "", "", "", "", "", "", "", "", 2)
                    Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogos judiciales", "Resultados judiciales: " & valores(0) & "|" & valores(1) & "|" & valores(2) & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "2")
                    Aviso(dtsguardado.Rows.Item(0)("MSJ").ToString)
                    DdlTipo.Enabled = True
                End If
            Catch ex As Exception
                DdlTipo.Enabled = False
                Aviso("No se realizo la insercion debido a: " & ex.Message)
                e.Canceled = True
            End Try
        ElseIf e.CommandName = "Edit" Then
            DdlTipo.Enabled = False
            Session("VALORID") = e.Item.Cells.Item(3).Text
            VALORID.Value = e.Item.Cells.Item(3).Text
        ElseIf e.CommandName = "InitInsert" Then
            DdlTipo.Enabled = False
            Session("PostBack") = False
        ElseIf e.CommandName = "Cancel" Then
            DdlTipo.Enabled = True
        ElseIf e.CommandName = "Desactivar" Then
            Class_Judicial.EditarCatalogosJudicial(e.Item.Cells(3).Text, 0, "", "", "", "", "", "", "", "", "", "", "", "", 37)
            Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogos judiciales", "Resultados judiciales: " & e.Item.Cells(3).Text & "|" & "0" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "37")
            RGVResultados.Rebind()
        ElseIf e.CommandName = "Activar" Then
            Class_Judicial.EditarCatalogosJudicial(e.Item.Cells(3).Text, 1, "", "", "", "", "", "", "", "", "", "", "", "", 37)
            Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogos judiciales", "Resultados judiciales: " & e.Item.Cells(3).Text & "|" & "1" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "37")
            RGVResultados.Rebind()
        End If
    End Sub
    Private Sub RGVPromociones_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RGVPromociones.ItemCommand
        Session("PostBack") = True
        If e.CommandName = "Update" Then

            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(5) As String

            valores(0) = CType(MyUserControl.FindControl("TxtNombre"), RadTextBox).Text
            valores(1) = CType(MyUserControl.FindControl("DdlComodin"), RadDropDownList).SelectedValue
            valores(2) = CType(MyUserControl.FindControl("DdlCambiaEtapa"), RadDropDownList).SelectedValue
            valores(3) = CType(MyUserControl.FindControl("NtxtDiasRespuesta"), RadNumericTextBox).Text
            valores(4) = CType(MyUserControl.FindControl("DdlTipoDiasRes"), RadDropDownList).SelectedValue


            Try
                If valores(0) = "" Or valores(1) = "" Or valores(2) = "" Or valores(3) = "" Or valores(4) = "" Then
                    Aviso("Falta capturar al menos un dato")
                    e.Canceled = True
                Else
                    Dim dtsguardado As DataTable = Class_Judicial.EditarCatalogosJudicial(valores(0), valores(1), valores(2), valores(3), valores(4), Session("VALORID"), "", "", "", "", "", "", "", "", 15)
                    Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogos judiciales", "Promociones judiciales: " & valores(0) & "|" & valores(1) & "|" & valores(2) & "|" & "" & "|" & valores(3) & "|" & valores(4) & "|" & Session("VALORID") & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "15")
                    If dtsguardado.Rows.Item(0)("MSJ").ToString = "EXITO" Then
                        Aviso("Promocion actualizada correctamente")
                        DdlTipo.Enabled = True
                    ElseIf dtsguardado.Rows.Item(0)("MSJ").ToString = "ERROR" Then
                        Aviso("Error al actualizar promoción. Ya existe una promoción registrada con este nombre.")
                        DdlTipo.Enabled = True
                    End If
                End If
            Catch ex As Exception
                Aviso("No se realizo la actualizacion debido a: " & ex.Message)
                DdlTipo.Enabled = False
                e.Canceled = True
            End Try
        ElseIf e.CommandName = "PerformInsert" Then

            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(5) As String

            valores(0) = CType(MyUserControl.FindControl("TxtNombre"), RadTextBox).Text
            valores(1) = CType(MyUserControl.FindControl("DdlComodin"), RadDropDownList).SelectedValue
            valores(2) = CType(MyUserControl.FindControl("DdlCambiaEtapa"), RadDropDownList).SelectedValue
            valores(3) = CType(MyUserControl.FindControl("NtxtDiasRespuesta"), RadNumericTextBox).Text
            valores(4) = CType(MyUserControl.FindControl("DdlTipoDiasRes"), RadDropDownList).SelectedValue


            Try
                If valores(0) = "" Or valores(1) = "" Or valores(2) = "" Or valores(3) = "" Or valores(4) = "" Then
                    Aviso("Falta capturar al menos un dato")
                    e.Canceled = True
                Else
                    Dim dtsguardado As DataTable = Class_Judicial.EditarCatalogosJudicial(valores(0), valores(1), valores(2), valores(3), valores(4), "", "", "", "", "", "", "", "", "", 1)
                    If dtsguardado.Rows.Item(0)("MSJ").ToString = "EXITO" Then
                        Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogos judiciales", "Promociones judiciales: " & valores(0) & "|" & valores(1) & "|" & valores(2) & "|" & "" & "|" & valores(3) & "|" & valores(4) & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "1")
                        Aviso("Promocion insertada correctamente")
                        DdlTipo.Enabled = True
                    ElseIf dtsguardado.Rows.Item(0)("MSJ").ToString = "ERROR" Then
                        Aviso("Error al insertar promoción. Ya existe una promoción registrada con este nombre.")
                        DdlTipo.Enabled = True
                    End If
                End If
            Catch ex As Exception
                DdlTipo.Enabled = False
                Aviso("No se realizo la insercion debido a: " & ex.Message)
                e.Canceled = True
            End Try
        ElseIf e.CommandName = "Edit" Then
            DdlTipo.Enabled = False
            Session("VALORID") = e.Item.Cells.Item(3).Text
            VALORID.Value = e.Item.Cells.Item(3).Text
        ElseIf e.CommandName = "InitInsert" Then
            DdlTipo.Enabled = False
            Session("PostBack") = False
        ElseIf e.CommandName = "Cancel" Then
            DdlTipo.Enabled = True
        ElseIf e.CommandName = "Desactivar" Then
            Class_Judicial.EditarCatalogosJudicial(e.Item.Cells(3).Text, 0, "", "", "", "", "", "", "", "", "", "", "", "", 38)
            Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogos judiciales", "Resultados judiciales: " & e.Item.Cells(3).Text & "|" & "0" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "38")
            RGVPromociones.Rebind()
        ElseIf e.CommandName = "Activar" Then
            Class_Judicial.EditarCatalogosJudicial(e.Item.Cells(3).Text, 1, "", "", "", "", "", "", "", "", "", "", "", "", 38)
            Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogos judiciales", "Resultados judiciales: " & e.Item.Cells(3).Text & "|" & "1" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "38")
            RGVPromociones.Rebind()
        End If
    End Sub
    Private Sub RGVCierreJuicios_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RGVCierreJuicios.ItemCommand
        Session("PostBack") = True
        If e.CommandName = "Update" Then

            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(5) As String

            valores(0) = CType(MyUserControl.FindControl("TxtNombre"), RadTextBox).Text
            valores(1) = CType(MyUserControl.FindControl("DdlValidaProm"), RadDropDownList).SelectedValue
            valores(2) = CType(MyUserControl.FindControl("DdlNombreProm"), RadDropDownList).SelectedValue
            valores(3) = CType(MyUserControl.FindControl("DdlValidaDiasMora"), RadDropDownList).SelectedValue
            valores(4) = CType(MyUserControl.FindControl("DDLSuperior"), RadDropDownList).SelectedValue

            Try
                If valores(0) = "" Or valores(1) = "" Or valores(2) = "" Or valores(3) = "" Then
                    Aviso("Falta capturar al menos un dato")
                    e.Canceled = True
                Else
                    Dim dtsguardado As DataTable = Class_Judicial.EditarCatalogosJudicial(valores(0), valores(1), valores(2), valores(3), Session("VALORID"), valores(4), "", "", "", "", "", "", "", "", 22)
                    Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogos judiciales", "Cierre juicios: " & valores(0) & "|" & valores(1) & "|" & valores(2) & "|" & "" & "|" & valores(3) & "|" & valores(4) & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "22")
                    Aviso(dtsguardado.Rows.Item(0)("MSJ").ToString)
                    DdlTipo.Enabled = True
                End If
            Catch ex As Exception
                DdlTipo.Enabled = False
                Aviso("No se realizo la actualizacion debido a: " & ex.Message)
                e.Canceled = True
            End Try
        ElseIf e.CommandName = "PerformInsert" Then

            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(5) As String

            valores(0) = CType(MyUserControl.FindControl("TxtNombre"), RadTextBox).Text
            valores(1) = CType(MyUserControl.FindControl("DdlValidaProm"), RadDropDownList).SelectedValue
            valores(2) = CType(MyUserControl.FindControl("DdlNombreProm"), RadDropDownList).SelectedValue
            valores(3) = CType(MyUserControl.FindControl("DdlValidaDiasMora"), RadDropDownList).SelectedValue
            valores(4) = CType(MyUserControl.FindControl("DDLSuperior"), RadDropDownList).SelectedValue


            Try
                If valores(0) = "" Or valores(1) = "" Or valores(2) = "" Or valores(3) = "" Then
                    Aviso("Falta capturar al menos un dato")
                    e.Canceled = True
                Else
                    Dim dtsguardado As DataTable = Class_Judicial.EditarCatalogosJudicial(valores(0), valores(1), valores(2), valores(3), valores(4), "", "", "", "", "", "", "", "", "", 8)
                    Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogos judiciales", "Cierre juicios: " & valores(0) & "|" & valores(1) & "|" & valores(2) & "|" & "" & "|" & valores(3) & "|" & valores(4) & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "8")
                    Aviso(dtsguardado.Rows.Item(0)("MSJ").ToString)
                    DdlTipo.Enabled = True
                End If
            Catch ex As Exception
                DdlTipo.Enabled = False
                Aviso("No se realizo la insercion debido a: " & ex.Message)
                e.Canceled = True
            End Try
        ElseIf e.CommandName = "Edit" Then
            DdlTipo.Enabled = False
            Session("VALORID") = e.Item.Cells.Item(3).Text
            VALORID.Value = e.Item.Cells.Item(3).Text
        ElseIf e.CommandName = "InitInsert" Then
            DdlTipo.Enabled = False
            Session("PostBack") = False
        ElseIf e.CommandName = "Cancel" Then
            DdlTipo.Enabled = True
        End If
    End Sub
    Private Sub RGVTramitesJudiciales_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RGVTramitesJudiciales.ItemCommand
        Session("PostBack") = True
        Session("LLenar") = True

        If e.CommandName = "Update" Then

            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(12) As String

            valores(0) = Session("tramite") 'CType(MyUserControl.FindControl("DdlTipoTramite"), RadDropDownList).SelectedValue
            valores(1) = CType(MyUserControl.FindControl("DdlTipoInscripcion"), RadDropDownList).SelectedValue
            valores(2) = CType(MyUserControl.FindControl("DdlBienEmbargado"), RadDropDownList).SelectedValue
            valores(3) = CType(MyUserControl.FindControl("DdlFolioInmobiliario"), RadDropDownList).SelectedValue
            valores(4) = CType(MyUserControl.FindControl("DdlFechaEvento"), RadDropDownList).SelectedValue
            valores(5) = CType(MyUserControl.FindControl("DdlHoraEvento"), RadDropDownList).SelectedValue
            valores(6) = CType(MyUserControl.FindControl("DdlPosicion"), RadDropDownList).SelectedValue
            valores(7) = CType(MyUserControl.FindControl("DdlObservaciones"), RadDropDownList).SelectedValue
            valores(8) = CType(MyUserControl.FindControl("DdlFechaAvaluo"), RadDropDownList).SelectedValue
            valores(9) = CType(MyUserControl.FindControl("DdlValorComercial"), RadDropDownList).SelectedValue
            valores(10) = CType(MyUserControl.FindControl("DdlGarantia"), RadDropDownList).SelectedValue
            valores(11) = CType(MyUserControl.FindControl("DdlOrigenGarantia"), RadDropDownList).SelectedValue

            Try
                If valores(0) = "" Then
                    Aviso("Falta capturar al menos un dato")
                    e.Canceled = True
                Else
                    Dim dtsguardado As DataTable = Class_Judicial.EditarCatalogosJudicial(valores(0), valores(1), valores(2), valores(3), valores(4), valores(5), valores(6), valores(7), valores(8), valores(9), valores(10), valores(11), Session("VALORID"), "", 23)
                    Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogos judiciales", "Cierre juicios: " & valores(0) & "|" & valores(1) & "|" & valores(2) & "|" & valores(3) & "|" & valores(4) & "|" & valores(5) & "|" & valores(6) & "|" & valores(7) & "|" & valores(8) & "|" & valores(9) & "|" & valores(10) & "|" & valores(11) & "|" & Session("VALORID") & "|" & "" & "|" & "23")
                    Aviso(dtsguardado.Rows.Item(0)("MSJ").ToString)
                    DdlTipo.Enabled = True
                End If
            Catch ex As Exception
                Aviso("No se realizo la actualizacion debido a: " & ex.Message)
                e.Canceled = True
                DdlTipo.Enabled = False
            End Try
        ElseIf e.CommandName = "PerformInsert" Then

            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(12) As String

            valores(0) = Session("tramite") 'CType(MyUserControl.FindControl("DdlTipoTramite"), RadDropDownList).SelectedValue
            valores(1) = CType(MyUserControl.FindControl("DdlTipoInscripcion"), RadDropDownList).SelectedValue
            valores(2) = CType(MyUserControl.FindControl("DdlBienEmbargado"), RadDropDownList).SelectedValue
            valores(3) = CType(MyUserControl.FindControl("DdlFolioInmobiliario"), RadDropDownList).SelectedValue
            valores(4) = CType(MyUserControl.FindControl("DdlFechaEvento"), RadDropDownList).SelectedValue
            valores(5) = CType(MyUserControl.FindControl("DdlHoraEvento"), RadDropDownList).SelectedValue
            valores(6) = CType(MyUserControl.FindControl("DdlPosicion"), RadDropDownList).SelectedValue
            valores(7) = CType(MyUserControl.FindControl("DdlObservaciones"), RadDropDownList).SelectedValue
            valores(8) = CType(MyUserControl.FindControl("DdlFechaAvaluo"), RadDropDownList).SelectedValue
            valores(9) = CType(MyUserControl.FindControl("DdlValorComercial"), RadDropDownList).SelectedValue
            valores(10) = CType(MyUserControl.FindControl("DdlGarantia"), RadDropDownList).SelectedValue
            valores(11) = CType(MyUserControl.FindControl("DdlOrigenGarantia"), RadDropDownList).SelectedValue

            Try
                If valores(0) = "" Then
                    Aviso("Falta capturar al menos un dato")
                    e.Canceled = True
                Else
                    Dim dtsguardado As DataTable = Class_Judicial.EditarCatalogosJudicial(valores(0), valores(1), valores(2), valores(3), valores(4), valores(5), valores(6), valores(7), valores(8), valores(9), valores(10), valores(11), "", "", 9)
                    Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogos judiciales", "Cierre juicios: " & valores(0) & "|" & valores(1) & "|" & valores(2) & "|" & valores(3) & "|" & valores(4) & "|" & valores(5) & "|" & valores(6) & "|" & valores(7) & "|" & valores(8) & "|" & valores(9) & "|" & valores(10) & "|" & valores(11) & "|" & "" & "|" & "" & "|" & "9")
                    Aviso(dtsguardado.Rows.Item(0)("MSJ").ToString)
                    DdlTipo.Enabled = True
                End If
            Catch ex As Exception
                Aviso("No se realizo la insercion debido a: " & ex.Message)
                e.Canceled = True
                DdlTipo.Enabled = False
            End Try
        ElseIf e.CommandName = "Edit" Then
            DdlTipo.Enabled = False
            Session("VALORID") = e.Item.Cells.Item(3).Text
            VALORID.Value = e.Item.Cells.Item(3).Text
        ElseIf e.CommandName = "InitInsert" Then
            DdlTipo.Enabled = False
            Session("PostBack") = False
        ElseIf e.CommandName = "Cancel" Then
            DdlTipo.Enabled = True
        End If
    End Sub
    Private Sub RGVAcuerdos_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RGVAcuerdos.ItemCommand
        Session("PostBack") = True
        If e.CommandName = "Update" Then

            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(9) As String

            valores(0) = CType(MyUserControl.FindControl("TxtNombre"), RadTextBox).Text
            valores(1) = CType(MyUserControl.FindControl("DdlIndicadorimpulso"), RadDropDownList).SelectedValue
            valores(2) = CType(MyUserControl.FindControl("DdlCambiaEtapa"), RadDropDownList).SelectedValue
            valores(3) = CType(MyUserControl.FindControl("DdlEtapaDestino"), RadDropDownList).SelectedValue
            valores(4) = CType(MyUserControl.FindControl("DdlPagoHonorarios"), RadDropDownList).SelectedValue
            valores(5) = CType(MyUserControl.FindControl("NtxtMontoPagar"), RadNumericTextBox).Text
            valores(6) = CType(MyUserControl.FindControl("DdlPagoExterno"), RadDropDownList).SelectedValue
            valores(7) = CType(MyUserControl.FindControl("DdlIncluyeIVA"), RadDropDownList).SelectedValue
            valores(8) = CType(MyUserControl.FindControl("txtConcepto"), RadTextBox).Text

            Try
                If valores(0) = "" Then
                    Aviso("Falta capturar al menos un dato")
                    e.Canceled = True
                Else
                    Dim dtsguardado As DataTable = Class_Judicial.EditarCatalogosJudicial(valores(0), valores(1), valores(2), valores(3), valores(4), valores(5), valores(6), valores(7), Session("VALORID"), valores(8), "", "", "", "", 27)
                    Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogos judiciales", "Acuerdos: " & valores(0) & "|" & valores(1) & "|" & valores(2) & "|" & valores(3) & "|" & valores(4) & "|" & valores(5) & "|" & valores(6) & "|" & valores(7) & "|" & Session("VALORID") & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "27")
                    If dtsguardado.Rows.Item(0)("MSJ").ToString = "EXITO" Then
                        Aviso("Acuerdo actualizado correctamente")
                        DdlTipo.Enabled = True
                    ElseIf dtsguardado.Rows.Item(0)("MSJ").ToString = "ERROR" Then
                        Aviso("Error al actualizar acuerdo. Ya existe un acuerdo registrado con este nombre.")
                        DdlTipo.Enabled = True
                    End If
                End If
            Catch ex As Exception
                Aviso("No se realizo la actualizacion debido a: " & ex.Message)
                e.Canceled = True
                DdlTipo.Enabled = False
            End Try
        ElseIf e.CommandName = "PerformInsert" Then

            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(9) As String

            valores(0) = CType(MyUserControl.FindControl("TxtNombre"), RadTextBox).Text
            valores(1) = CType(MyUserControl.FindControl("DdlIndicadorimpulso"), RadDropDownList).SelectedValue
            valores(2) = CType(MyUserControl.FindControl("DdlCambiaEtapa"), RadDropDownList).SelectedValue
            valores(3) = CType(MyUserControl.FindControl("DdlEtapaDestino"), RadDropDownList).SelectedValue
            valores(4) = CType(MyUserControl.FindControl("DdlPagoHonorarios"), RadDropDownList).SelectedValue
            valores(5) = CType(MyUserControl.FindControl("NtxtMontoPagar"), RadNumericTextBox).Text
            valores(6) = CType(MyUserControl.FindControl("DdlPagoExterno"), RadDropDownList).SelectedValue
            valores(7) = CType(MyUserControl.FindControl("DdlIncluyeIVA"), RadDropDownList).SelectedValue
            valores(8) = CType(MyUserControl.FindControl("txtConcepto"), RadTextBox).Text

            Try
                If valores(0) = "" Then
                    Aviso("Falta capturar al menos un dato")
                    e.Canceled = True
                Else
                    Dim dtsguardado As DataTable = Class_Judicial.EditarCatalogosJudicial(valores(0), valores(1), valores(2), valores(3), valores(4), valores(5), valores(6), valores(7), "", valores(8), "", "", "", "", 13)
                    If dtsguardado.Rows.Item(0)("MSJ").ToString = "EXITO" Then
                        Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogos judiciales", "Acuerdos: " & valores(0) & "|" & valores(1) & "|" & valores(2) & "|" & valores(3) & "|" & valores(4) & "|" & valores(5) & "|" & valores(6) & "|" & valores(7) & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "13")
                        Aviso("Acuerdo insertado correctamente")
                        DdlTipo.Enabled = True
                    ElseIf dtsguardado.Rows.Item(0)("MSJ").ToString = "ERROR" Then
                        Aviso("Error al insertar acuerdo. Ya existe un acuerdo registrado con este nombre.")
                        DdlTipo.Enabled = True
                    End If
                End If
            Catch ex As Exception
                DdlTipo.Enabled = False
                Aviso("No se realizo la insercion debido a: " & ex.Message)
                e.Canceled = True
            End Try
        ElseIf e.CommandName = "Edit" Then
            DdlTipo.Enabled = False
            Session("VALORID") = e.Item.Cells.Item(3).Text
            VALORID.Value = e.Item.Cells.Item(3).Text
            Session("LLenar") = True
        ElseIf e.CommandName = "InitInsert" Then
            DdlTipo.Enabled = False
            Session("PostBack") = True
            Session("LLenar") = False
        ElseIf e.CommandName = "Cancel" Then
            DdlTipo.Enabled = True
        ElseIf e.CommandName = "Desactivar" Then
            Class_Judicial.EditarCatalogosJudicial(e.Item.Cells(3).Text, 0, "", "", "", "", "", "", "", "", "", "", "", "", 36)
            Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogos judiciales", "Acuerdos: " & e.Item.Cells(3).Text & "|" & "0" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "36")
            RGVAcuerdos.Rebind()
        ElseIf e.CommandName = "Activar" Then
            Class_Judicial.EditarCatalogosJudicial(e.Item.Cells(3).Text, 1, "", "", "", "", "", "", "", "", "", "", "", "", 36)
            Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogos judiciales", "Acuerdos: " & e.Item.Cells(3).Text & "|" & "1" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "36")
            RGVAcuerdos.Rebind()
        End If
    End Sub
    Private Sub RGVDiligencias_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RGVDiligencias.ItemCommand
        Session("PostBack") = True
        If e.CommandName = "Update" Then

            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(10) As String

            valores(0) = CType(MyUserControl.FindControl("DdlDiligencia"), RadDropDownList).SelectedValue
            valores(1) = CType(MyUserControl.FindControl("DdlResultado"), RadDropDownList).SelectedValue
            valores(2) = CType(MyUserControl.FindControl("DdlSubresultado"), RadDropDownList).SelectedValue
            valores(3) = CType(MyUserControl.FindControl("DdlMarcarParticipante"), RadDropDownList).SelectedValue
            valores(4) = CType(MyUserControl.FindControl("DdlRembueble"), RadDropDownList).SelectedValue
            valores(5) = CType(MyUserControl.FindControl("DdlReinmueble"), RadDropDownList).SelectedValue
            valores(6) = CType(MyUserControl.FindControl("DdlRcdepositario"), RadDropDownList).SelectedValue
            valores(7) = CType(MyUserControl.FindControl("DdlRiprocesal"), RadDropDownList).SelectedValue
            valores(8) = CType(MyUserControl.FindControl("DdlPromocion"), RadDropDownList).SelectedValue
            valores(9) = CType(MyUserControl.FindControl("DdlNotifCorreo"), RadDropDownList).SelectedValue

            Try
                If valores(0) = "" Or valores(1) = "" Or valores(2) = "" Or valores(3) = "" Or valores(4) = "" Or valores(5) = "" Or valores(6) = "" Or valores(7) = "" Or valores(8) = "" Or valores(9) = "" Then
                    Aviso("Falta capturar al menos un dato")
                    e.Canceled = True
                Else
                    Dim dtsguardado As DataTable = Class_Judicial.EditarCatalogosJudicial(valores(0), valores(1), valores(2), valores(3), valores(4), valores(5), valores(6), valores(7), valores(8), valores(9), Session("VALORID"), "", "", "", 19)
                    Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogos judiciales", "Diligencias: " & valores(0) & "|" & valores(1) & "|" & valores(2) & "|" & valores(3) & "|" & valores(4) & "|" & valores(5) & "|" & valores(6) & "|" & valores(7) & "|" & valores(8) & "|" & valores(9) & "|" & Session("VALORID") & "|" & "" & "|" & "" & "|" & "" & "|" & "19")
                    Aviso(dtsguardado.Rows.Item(0)("MSJ").ToString)
                    DdlTipo.Enabled = True
                End If
            Catch ex As Exception
                DdlTipo.Enabled = False
                Aviso("No se realizo la actualizacion debido a: " & ex.Message)
                e.Canceled = True
            End Try
        ElseIf e.CommandName = "PerformInsert" Then

            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(10) As String

            valores(0) = CType(MyUserControl.FindControl("DdlDiligencia"), RadDropDownList).SelectedValue
            valores(1) = CType(MyUserControl.FindControl("DdlResultado"), RadDropDownList).SelectedValue
            valores(2) = CType(MyUserControl.FindControl("DdlSubresultado"), RadDropDownList).SelectedValue
            valores(3) = CType(MyUserControl.FindControl("DdlMarcarParticipante"), RadDropDownList).SelectedValue
            valores(4) = CType(MyUserControl.FindControl("DdlRembueble"), RadDropDownList).SelectedValue
            valores(5) = CType(MyUserControl.FindControl("DdlReinmueble"), RadDropDownList).SelectedValue
            valores(6) = CType(MyUserControl.FindControl("DdlRcdepositario"), RadDropDownList).SelectedValue
            valores(7) = CType(MyUserControl.FindControl("DdlRiprocesal"), RadDropDownList).SelectedValue
            valores(8) = CType(MyUserControl.FindControl("DdlPromocion"), RadDropDownList).SelectedValue
            valores(9) = CType(MyUserControl.FindControl("DdlNotifCorreo"), RadDropDownList).SelectedValue

            Try
                If valores(0) = "" Or valores(1) = "" Or valores(2) = "" Or valores(3) = "" Or valores(4) = "" Or valores(5) = "" Or valores(6) = "" Or valores(7) = "" Or valores(8) = "" Or valores(9) = "" Then
                    Aviso("Falta capturar al menos un dato")
                    e.Canceled = True
                Else
                    Dim dtsguardado As DataTable = Class_Judicial.EditarCatalogosJudicial(valores(0), valores(1), valores(2), valores(3), valores(4), valores(5), valores(6), valores(7), valores(8), valores(9), "", "", "", "", 5)
                    Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogos judiciales", "Diligencias: " & valores(0) & "|" & valores(1) & "|" & valores(2) & "|" & valores(3) & "|" & valores(4) & "|" & valores(5) & "|" & valores(6) & "|" & valores(7) & "|" & valores(8) & "|" & valores(9) & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "5")
                    Aviso(dtsguardado.Rows.Item(0)("MSJ").ToString)
                    DdlTipo.Enabled = True
                End If
            Catch ex As Exception
                DdlTipo.Enabled = False
                Aviso("No se realizo la insercion debido a: " & ex.Message)
                e.Canceled = True
            End Try
        ElseIf e.CommandName = "Edit" Then
            DdlTipo.Enabled = False
            Session("VALORID") = e.Item.Cells.Item(3).Text
            VALORID.Value = e.Item.Cells.Item(3).Text
        ElseIf e.CommandName = "InitInsert" Then
            DdlTipo.Enabled = False
            Session("PostBack") = False
        ElseIf e.CommandName = "Cancel" Then
            DdlTipo.Enabled = True
        End If
    End Sub
    Private Sub RGVCastigo_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RGVCastigo.ItemCommand
        Session("PostBack") = True
        If e.CommandName = "Update" Then

            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(4) As String

            valores(0) = CType(MyUserControl.FindControl("DdlInstancia"), RadDropDownList).SelectedValue
            valores(1) = CType(MyUserControl.FindControl("DdlUsuario"), RadDropDownList).SelectedValue
            valores(2) = CType(MyUserControl.FindControl("DdlOrden"), RadDropDownList).SelectedValue
            valores(3) = CType(MyUserControl.FindControl("RTBCorreos"), RadTextBox).Text

            Try
                If valores(0) = "" Or valores(1) = "" Or valores(2) = "" Or valores(3) = "" Then
                    Aviso("Falta capturar al menos un dato")
                    e.Canceled = True
                Else
                    Dim confirmacion As DataTable = Class_Judicial.EditarCatalogosJudicial(valores(0), valores(1), valores(2), Session("VALORID"), "", "", "", "", "", "", "", "", "", "", 29)
                    Dim orden As DataTable = Class_Judicial.EditarCatalogosJudicial(valores(0), valores(1), valores(2), Session("VALORID"), "", "", "", "", "", "", "", "", "", "", 30)
                    Dim usuario As DataTable = Class_Judicial.EditarCatalogosJudicial(valores(0), valores(1), valores(2), Session("VALORID"), "", "", "", "", "", "", "", "", "", "", 31)
                    If orden(0)(0) = 0 Then
                        If usuario(0)(0) = 0 Then
                            If confirmacion(0)(0) = 0 Then
                                Dim dtsguardado As DataTable = Class_Judicial.EditarCatalogosJudicial(valores(0), valores(1), valores(2), valores(3), Session("VALORID"), "", "", "", "", "", "", "", "", "", 24)
                                Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogos judiciales", "Castigo: " & valores(0) & "|" & valores(1) & "|" & valores(2) & "|" & valores(3) & "|" & Session("VALORID") & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "24")
                                Aviso(dtsguardado.Rows.Item(0)("MSJ").ToString)
                                DdlTipo.Enabled = True
                            Else
                                Aviso("Ya existe un Registro con la misma secuencia, elija otra")
                                e.Canceled = True
                            End If
                        Else
                            Aviso("Ya existe un Registro en esta instancia con el mismo usuario, elija otro")
                            e.Canceled = True
                        End If
                    Else
                        Aviso("Ya existe un Registro con el mismo orden, elija otro")
                        e.Canceled = True
                    End If


                End If
            Catch ex As Exception
                DdlTipo.Enabled = False
                Aviso("No se realizo la actualizacion debido a: " & ex.Message)
                e.Canceled = True
            End Try
        ElseIf e.CommandName = "PerformInsert" Then

            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(3) As String

            valores(0) = CType(MyUserControl.FindControl("DdlInstancia"), RadDropDownList).SelectedValue
            valores(1) = CType(MyUserControl.FindControl("DdlUsuario"), RadDropDownList).SelectedValue
            valores(2) = CType(MyUserControl.FindControl("DdlOrden"), RadDropDownList).SelectedValue
            valores(3) = CType(MyUserControl.FindControl("RTBCorreos"), RadTextBox).Text


            Try
                If valores(0) = "" Or valores(1) = "" Or valores(2) = "" Then
                    Aviso("Falta capturar al menos un dato")
                    e.Canceled = True
                Else
                    Dim confirmacion As DataTable = Class_Judicial.EditarCatalogosJudicial(valores(0), valores(1), valores(2), Session("VALORID"), "", "", "", "", "", "", "", "", "", "", 29)
                    Dim orden As DataTable = Class_Judicial.EditarCatalogosJudicial(valores(0), valores(1), valores(2), Session("VALORID"), "", "", "", "", "", "", "", "", "", "", 32)
                    Dim usuario As DataTable = Class_Judicial.EditarCatalogosJudicial(valores(0), valores(1), valores(2), Session("VALORID"), "", "", "", "", "", "", "", "", "", "", 33)
                    If orden(0)(0) = 0 Then
                        If usuario(0)(0) = 0 Then
                            If confirmacion(0)(0) = 0 Then
                                Dim dtsguardado As DataTable = Class_Judicial.EditarCatalogosJudicial(valores(0), valores(1), valores(2), valores(3), "", "", "", "", "", "", "", "", "", "", 10)
                                Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogos judiciales", "Castigo: " & valores(0) & "|" & valores(1) & "|" & valores(2) & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "10")
                                Aviso(dtsguardado.Rows.Item(0)("MSJ").ToString)
                                DdlTipo.Enabled = True
                            Else
                                Aviso("Ya existe un Registro con la misma secuencia, elija otra")
                                e.Canceled = True
                            End If
                        Else
                            Aviso("Ya existe un Registro en esta instancia con el mismo usuario, elija otro")
                            e.Canceled = True
                        End If
                    Else
                        Aviso("Ya existe un Registro con el mismo orden, elija otro")
                        e.Canceled = True
                    End If
                End If
            Catch ex As Exception
                DdlTipo.Enabled = False
                Aviso("No se realizo la insercion debido a: " & ex.Message)
                e.Canceled = True
            End Try
        ElseIf e.CommandName = "Edit" Then
            DdlTipo.Enabled = False
            Session("VALORID") = e.Item.Cells.Item(3).Text
            VALORID.Value = e.Item.Cells.Item(3).Text
        ElseIf e.CommandName = "InitInsert" Then
            DdlTipo.Enabled = False
            Session("PostBack") = True
        ElseIf e.CommandName = "Cancel" Then
            DdlTipo.Enabled = True
        ElseIf e.CommandName = "Cancelar" Then
            Class_Judicial.EditarCatalogosJudicial(e.Item.Cells.Item(3).Text, e.Item.Cells.Item(4).Text, "", "", "", "", "", "", "", "", "", "", "", "", 34)
            Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogos judiciales", "Castigo: " & e.Item.Cells(3).Text & "|" & e.Item.Cells.Item(4).Text & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "34")
            RGVCastigo.Rebind()
        ElseIf e.CommandName = "Activar" Then
            Class_Judicial.EditarCatalogosJudicial(e.Item.Cells.Item(3).Text, e.Item.Cells.Item(4).Text, "", "", "", "", "", "", "", "", "", "", "", "", 35)
            Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogos judiciales", "Castigo: " & e.Item.Cells(3).Text & "|" & e.Item.Cells.Item(4).Text & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "35")
            RGVCastigo.Rebind()
        End If
    End Sub
    Private Sub CatalogosSistema_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        If Not IsPostBack Then
            RGVEtapas.Visible = False
            gridTipoJuicio.Visible = False
            RGVDocumentosBase.Visible = False
            RGVDiligencias.Visible = False
            RGVDiasInhabiles.Visible = False
            RGVCierreJuicios.Visible = False
            RGVTramitesJudiciales.Visible = False
            RGVCastigo.Visible = False
            RGVMotivos.Visible = False
            RGVEtiquetas.Visible = False
            RGVPromociones.Visible = False
            RGVResultados.Visible = False
            RGVAcuerdos.Visible = False
        End If
    End Sub
    Private Function encadena(cambo As RadComboBox) As String
        Dim sali As String = ""
        For Each item In cambo.CheckedItems
            sali += item.Value & ","
        Next
        sali = sali.Substring(0, sali.Length - 1)
        Return sali
    End Function
End Class

