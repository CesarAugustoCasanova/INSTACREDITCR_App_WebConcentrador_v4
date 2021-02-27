
Imports System.IO
Imports System.Net
    Imports System.Collections.Generic
    Imports System.Xml
    Imports Telerik.Web.UI
    Imports Telerik.Web.UI.SkinReferenceCollection
    Imports System.Data.SqlClient
Imports System.Data
Imports Funciones
Public Class CreacionFlujos
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim Usr As String = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO


        Catch ex As Exception
            OffLine(HidenUrs.Value)
            AUDITORIA(HidenUrs.Value, "Administrador", "Creacion Flujos", "", "Page_Load", "", Request.ServerVariables("REMOTE_ADDR"), Request.ServerVariables("REMOTE_HOST"))
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/SesionExpirada.aspx")
        End Try
        Try
            If Not IsPostBack Then
                llenarboxEtapas()
                HidenUrs.Value = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
                If CType(Session("USUARIOADMIN"), USUARIO).CAT_LO_MADMINISTRADOR.ToString.Substring(0, 1) = 0 Then
                    OffLine(HidenUrs.Value)
                    Session.Clear()
                    Session.Abandon()
                    Response.Redirect("~/SesionExpirada.aspx")
                End If
            End If
        Catch ex As Exception
            SendMail("Page_Load", ex, "", "", HidenUrs.Value)
        End Try
    End Sub
    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Adminitrador", "CreacionFlujos.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub

    Private Sub llenarboxEtapas()
        Dim etapas As DataSet = Class_Judicial.Llenarpasos(3, "", "")
        RLBEtapa0.DataSource = etapas
        RLBEtapa0.DataBind()
        RLBEtapa1.DataSource = etapas
        RLBEtapa1.DataBind()
        RLBEtapa2.DataSource = etapas
        RLBEtapa2.DataBind()
        RdDlEtapaAvanza.DataSource = etapas
        RdDlEtapaAvanza.DataBind()
        RdDlEtapaAvanza.Items.Add("SELECCIONE")
        RdDlEtapaAvanza.SelectedText = "SELECCIONE"
    End Sub
    Protected Sub Aviso(ByVal MSJ As String)
        RadAviso.RadAlert(MSJ, 440, 155, "AVISO", Nothing)
    End Sub



    Private Sub RdBtnGuardar1_Click(sender As Object, e As EventArgs) Handles RdBtnGuardar1.Click
        Dim dtsresultado As DataSet
        Dim Promociones As String = ""
        For Each item_v As GridItem In RgdPromociones.MasterTableView.Items
            Dim dataitem As GridDataItem = DirectCast(item_v, GridDataItem)
            Dim cell As TableCell = dataitem("ClientSelectColumn")
            Dim checkBox As CheckBox = DirectCast(cell.Controls(0), CheckBox)
            If checkBox.Checked Then
                Promociones += dataitem.Cells(3).Text & ","

            End If
        Next

        'If Promociones = "" Then
        '    dtsresultado = Class_Judicial.GuardarCatalogos(8, RLBEtapa0.SelectedItem.Value, "", "", "", "", "", "", "", "", "", "")
        'End If
        If Promociones <> "" Then
            Promociones = Promociones.Substring(0, Promociones.Length - 1)
        End If
        dtsresultado = Class_Judicial.GuardarCatalogos(8, RLBEtapa0.SelectedItem.Value, "", Promociones, "", "", "", "", "", "", "", "", "")
        '  Dim dtsresultado As DataSet = Class_Judicial.GuardarCatalogos(8, RLBEtapa0.SelectedItem.Value, "", Promociones, "", "", "", "", "", "", "", "")
        Aviso(dtsresultado.Tables(0).Rows(0).Item(0))


    End Sub

    Private Sub RadWizard1_NavigationBarButtonClick(sender As Object, e As WizardEventArgs) Handles RadWizard1.NavigationBarButtonClick
        Limpiar()
    End Sub
    Private Sub Limpiar()
        LblPromocion.Visible = False
        RgdPromociones.Visible = False
        RdBtnGuardar1.Visible = False
        LblResultado2.Visible = False
        RgdResultados.Visible = False
        LblPromocion2.Visible = False
        RLBPromociones1.Visible = False
        LblAvanza.Visible = False
        RdDlAvanza.Visible = False
        LblEtapaAvanza.Visible = False
        RdDlEtapaAvanza.Visible = False
        LblPromocion3.Visible = False
        LblFecha.Visible = False
        RdDlFecha.Visible = False
        LblFechaOb.Visible = False
        RdDlFechaOb.Visible = False
        LblResultado3.Visible = False
        LblNota.Visible = False
        RcbNota.Visible = False
        LblNotaOb.Visible = False
        RcbNotaOb.Visible = False
        LblDocumentos.Visible = False
        RcbDocumentos.Visible = False
        LblDocumentosOb.Visible = False
        RcbDocumentosOb.Visible = False
        LblDocumentosNum.Visible = False
        RNTBCuantos.Visible = False
        RLBEtapa0.ClearSelection()
        RLBEtapa1.ClearSelection()
        RLBEtapa2.ClearSelection()
        RLBPromocion2.ClearSelection()
        RLBPromociones1.ClearSelection()
        RLBResultados.ClearSelection()
        RdBtnGuardar2.Visible = False
        RdBtnGuardar3.Visible = False
        RLBPromocion2.Visible = False
    End Sub

    Private Sub RLBEtapa0_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RLBEtapa0.SelectedIndexChanged
        Dim promociones As DataSet = Class_Judicial.Llenarpasos(4, "", "")
        If promociones.Tables(0).Rows.Count > 0 Then
            RgdPromociones.DataSource = promociones.Tables(0)
            RgdPromociones.DataBind()
            LblPromocion.Visible = True
            RgdPromociones.Visible = True
            RdBtnGuardar1.Visible = True
            Dim dtsprom As DataSet = Class_Judicial.Llenarpasos(6, RLBEtapa0.SelectedItem.Value, RLBEtapa0.SelectedItem.Text)
            If dtsprom.Tables(0).Rows.Count > 0 Then
                Dim promocioneselec(dtsprom.Tables(0).Rows.Count) As String
                For x As Integer = 0 To dtsprom.Tables(0).Rows.Count - 1
                    For Each item_v As GridItem In RgdPromociones.MasterTableView.Items
                        Dim dataitem As GridDataItem = DirectCast(item_v, GridDataItem)
                        If HttpUtility.HtmlDecode(item_v.Cells(3).Text) = dtsprom.Tables(0).Rows(x).Item(0).ToString Then
                            dataitem.Selected = True
                            Exit For
                        End If
                    Next
                Next
            End If
        Else
            Aviso("No existen Promociones para asociar a esta etapa")
        End If
    End Sub

    Private Sub RLBEtapa1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RLBEtapa1.SelectedIndexChanged
        RgdResultados.Visible = False
        RdBtnGuardar2.Visible = False
        Dim dset As DataSet = Class_Judicial.Llenarpasos(41, RLBEtapa1.SelectedItem.Value, "")
        RLBPromociones1.Visible = True
        LblPromocion2.Visible = True
        If dset.Tables(0).Rows.Count <> 0 Then
            RLBPromociones1.DataSource = dset
            RLBPromociones1.DataBind()
            RLBPromociones1.Enabled = True
        Else
            RLBPromociones1.Items.Clear()
            RLBPromociones1.Items.Add("NO EXISTEN PROMOCIONES")
            RLBPromociones1.Items.Add(" RELACIONADAS")
            RLBPromociones1.Enabled = False
        End If
    End Sub

    Private Sub RadWizard1_ActiveStepChanged(sender As Object, e As EventArgs) Handles RadWizard1.ActiveStepChanged
        Limpiar()
    End Sub

    Private Sub RLBPromociones1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RLBPromociones1.SelectedIndexChanged
        Dim dsetRes As DataSet = Class_Judicial.Llenarpasos(5, "", "")
        If dsetRes.Tables(0).Rows.Count <> 0 Then
            RgdResultados.DataSource = dsetRes.Tables(0)
            RgdResultados.DataBind()
            LblResultado3.Visible = True
            RgdResultados.Visible = True
            RdBtnGuardar2.Visible = True
            Dim dtsResSel As DataSet = Class_Judicial.Llenarpasos(7, RLBPromociones1.SelectedItem.Value, RLBEtapa1.SelectedItem.Value)
            If dtsResSel.Tables(0).Rows(0).Item(0) <> 0 Then
                Dim resultadoselec(dtsResSel.Tables(0).Rows.Count) As String
                For x As Integer = 0 To dtsResSel.Tables(0).Rows.Count - 1
                    For Each item_v As GridItem In RgdResultados.MasterTableView.Items
                        Dim dataitem As GridDataItem = DirectCast(item_v, GridDataItem)
                        If HttpUtility.HtmlDecode(item_v.Cells(3).Text) = dtsResSel.Tables(0).Rows(x).Item(0).ToString Then
                            dataitem.Selected = True
                            Exit For
                        End If
                    Next
                Next
            End If
        Else
            Aviso("No existen resultados para asociar a esta promocion")
        End If
    End Sub

    Private Sub RLBEtapa2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RLBEtapa2.SelectedIndexChanged
        Dim dset As DataSet = Class_Judicial.Llenarpasos(41, RLBEtapa2.SelectedItem.Value, "")
        RLBPromocion2.Visible = True
        LblPromocion3.Visible = True
        RLBResultados.Visible = False
        LblResultado3.Visible = False
        LblAvanza.Visible = False
        RdDlAvanza.Visible = False
        LblEtapaAvanza.Visible = False
        RdDlEtapaAvanza.Visible = False
        RdBtnGuardar3.Visible = False
        LblFecha.Visible = False
        RdDlFecha.Visible = False
        LblFechaOb.Visible = False
        RdDlFechaOb.Visible = False
        LblNota.Visible = False
        RcbNota.Visible = False
        LblNotaOb.Visible = False
        RcbNotaOb.Visible = False
        LblDocumentos.Visible = False
        RcbDocumentos.Visible = False
        LblDocumentosOb.Visible = False
        RcbDocumentosOb.Visible = False
        LblDocumentosNum.Visible = False
        RNTBCuantos.Visible = False
        If dset.Tables(0).Rows.Count <> 0 Then
            RLBPromocion2.DataSource = dset
            RLBPromocion2.DataBind()
            RLBPromocion2.Enabled = True
        Else
            RLBPromocion2.Items.Clear()
            RLBPromocion2.Items.Add("NO EXISTEN PROMOCIONES")
            RLBPromocion2.Items.Add(" RELACIONADAS")
            RLBPromocion2.Enabled = False

        End If

    End Sub

    Private Sub RLBPromocion2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RLBPromocion2.SelectedIndexChanged
        LblAvanza.Visible = False
        RdDlAvanza.Visible = False
        LblEtapaAvanza.Visible = False
        RdDlEtapaAvanza.Visible = False
        RdBtnGuardar3.Visible = False
        LblFecha.Visible = False
        RdDlFecha.Visible = False
        LblFechaOb.Visible = False
        RdDlFechaOb.Visible = False
        LblNota.Visible = False
        RcbNota.Visible = False
        LblNotaOb.Visible = False
        RcbNotaOb.Visible = False
        LblDocumentos.Visible = False
        RcbDocumentos.Visible = False
        LblDocumentosOb.Visible = False
        RcbDocumentosOb.Visible = False
        LblDocumentosNum.Visible = False
        RNTBCuantos.Visible = False
        Dim dset As DataSet = Class_Judicial.Llenarpasos(51, RLBPromocion2.SelectedItem.Value, RLBEtapa2.SelectedItem.Value)
        RLBResultados.Visible = True
        LblResultado3.Visible = True
        If dset.Tables(0).Rows(0).Item(0) <> 0 Then
            RLBResultados.DataSource = dset
            RLBResultados.DataBind()
            RLBResultados.Enabled = True
        Else
            RLBResultados.Items.Clear()
            RLBResultados.Items.Add("NO EXISTEN RESULTADOS")
            RLBResultados.Items.Add(" RELACIONADOS")
            RLBResultados.Enabled = False

        End If
    End Sub

    Private Sub RLBResultados_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RLBResultados.SelectedIndexChanged
        LblAvanza.Visible = True
        RdDlAvanza.Visible = True
        LblEtapaAvanza.Visible = True
        RdDlEtapaAvanza.Visible = True
        RdBtnGuardar3.Visible = True
        LblFecha.Visible = True
        RdDlFecha.Visible = True
        LblFechaOb.Visible = True
        RdDlFechaOb.Visible = True
        LblNota.Visible = True
        RcbNota.Visible = True
        LblNotaOb.Visible = True
        RcbNotaOb.Visible = True
        LblDocumentos.Visible = True
        RcbDocumentos.Visible = True
        LblDocumentosOb.Visible = True
        RcbDocumentosOb.Visible = True
        LblDocumentosNum.Visible = True
        RNTBCuantos.Visible = True
        Dim dset As DataSet = Class_Judicial.GuardarCatalogos(11, RLBEtapa2.SelectedItem.Value, RLBPromocion2.SelectedItem.Value, RLBResultados.SelectedItem.Value, "", "", "", "", "", "", "", "", "")
        RdDlAvanza.SelectedValue = dset.Tables(0).Rows(0).Item(0)
        RdDlEtapaAvanza.SelectedValue = dset.Tables(0).Rows(0).Item(1)

        RdDlFecha.SelectedValue = dset.Tables(0).Rows(0).Item(2)
        RdDlFechaOb.SelectedValue = dset.Tables(0).Rows(0).Item(3)
        RcbNota.SelectedValue = dset.Tables(0).Rows(0).Item(4)
        RcbNotaOb.SelectedValue = dset.Tables(0).Rows(0).Item(5)
        RcbDocumentos.SelectedValue = dset.Tables(0).Rows(0).Item(6)
        RcbDocumentosOb.SelectedValue = dset.Tables(0).Rows(0).Item(7)
        RNTBCuantos.Text = dset.Tables(0).Rows(0).Item(8)
        RNTBCuantos.Enabled = False
    End Sub

    Private Sub RdDlAvanza_SelectedIndexChanged(sender As Object, e As DropDownListEventArgs) Handles RdDlAvanza.SelectedIndexChanged
        If RdDlAvanza.SelectedText = "SI" Then
            RdDlEtapaAvanza.Enabled = True
        Else
            RdDlEtapaAvanza.Enabled = False
            RdDlEtapaAvanza.SelectedText = "SELECCIONE"
        End If
    End Sub

    Private Sub RdDlFecha_SelectedIndexChanged(sender As Object, e As DropDownListEventArgs) Handles RdDlFecha.SelectedIndexChanged
        If RdDlFecha.SelectedText = "SI" Then
            RdDlFechaOb.Enabled = True
        Else
            RdDlFechaOb.Enabled = False
            RdDlFechaOb.SelectedText = "NO"
        End If
    End Sub

    Private Sub RcbNota_SelectedIndexChanged(sender As Object, e As DropDownListEventArgs) Handles RcbNota.SelectedIndexChanged
        If RcbNota.SelectedText = "SI" Then
            RcbNotaOb.Enabled = True
        Else
            RcbNotaOb.Enabled = False
            RcbNotaOb.SelectedText = "NO"
        End If
    End Sub

    Private Sub RcbDocumentos_SelectedIndexChanged(sender As Object, e As DropDownListEventArgs) Handles RcbDocumentos.SelectedIndexChanged
        If RcbDocumentos.SelectedText = "SI" Then
            RcbDocumentosOb.Enabled = True
            RNTBCuantos.Enabled = True
            RNTBCuantos.MinValue = 1
            RNTBCuantos.Value = 1
        Else
            RcbDocumentosOb.Enabled = False
            RNTBCuantos.Enabled = False
            RNTBCuantos.MinValue = 0
            RcbDocumentosOb.SelectedText = "NO"
            RNTBCuantos.Value = 0
        End If
    End Sub

    Private Sub RdBtnGuardar2_Click(sender As Object, e As EventArgs) Handles RdBtnGuardar2.Click
        Dim Resultados As String = ""
        For Each item_v As GridItem In RgdResultados.MasterTableView.Items
            Dim dataitem As GridDataItem = DirectCast(item_v, GridDataItem)
            Dim cell As TableCell = dataitem("ClientSelectColumn")
            Dim checkBox As CheckBox = DirectCast(cell.Controls(0), CheckBox)
            If checkBox.Checked Then
                Resultados = Resultados + dataitem.Cells(3).Text + ","
            End If
        Next
        If Resultados <> "" Then
            Resultados = Resultados.Substring(0, Resultados.Length - 1)
        End If

        Dim dtsresultado As DataSet = Class_Judicial.GuardarCatalogos(9, RLBPromociones1.SelectedItem.Value, RLBEtapa1.SelectedItem.Value, Resultados, "", "", "", "", "", "", "", "", "")
        Aviso(dtsresultado.Tables(0).Rows(0).Item(0))

    End Sub

    Private Sub RdBtnGuardar3_Click(sender As Object, e As EventArgs) Handles RdBtnGuardar3.Click
        Dim dtsresultado As DataSet = Class_Judicial.GuardarCatalogos(10, RLBEtapa2.SelectedItem.Value, RLBPromocion2.SelectedItem.Value, RLBResultados.SelectedItem.Value, RdDlAvanza.SelectedValue, RdDlEtapaAvanza.SelectedValue, RdDlFecha.SelectedValue, RdDlFechaOb.SelectedValue, RcbNota.SelectedValue, RcbNotaOb.SelectedValue, RcbDocumentos.SelectedValue, RcbDocumentosOb.SelectedValue, RNTBCuantos.Text)
        Aviso(dtsresultado.Tables(0).Rows(0).Item(0))
    End Sub
End Class