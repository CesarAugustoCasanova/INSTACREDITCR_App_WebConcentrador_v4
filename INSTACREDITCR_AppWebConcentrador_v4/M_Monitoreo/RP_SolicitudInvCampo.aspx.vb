Imports System.Data.SqlClient
Imports System.Data
Imports AjaxControlToolkit
Imports System.Web.UI.WebControls.Label
Imports Funciones
Imports System.Drawing
Imports Subgurim.Controles
Imports System.IO
Imports Db
Imports Telerik.Web.UI
Imports System.Net
Imports Spire.Xls

Partial Class M_Monitoreo_RP_SolicitudInvCampo
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
        ScriptManager.GetCurrent(Page).RegisterPostBackControl(BtnExportar)
        Try
            If tmpPermisos("INGRESO") = False Then
                Session.Clear()
                Session.Abandon()
                Response.Redirect("~/SesionExpirada.aspx")
            End If
        Catch ex As Exception
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/SesionExpirada.aspx")
        End Try
        If Not IsPostBack Then
            'Inicio()
            Limpia_grid()
        End If
    End Sub

    Protected Sub Mensaje(ByVal v_mensaje As String)
        RamiWa.RadAlert(v_mensaje, 400, 150, Nothing, Nothing)
    End Sub

    Protected Sub GVSolInvCampo_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs)
        Try
            GVSolInvCampo.DataSource = Session("InvDom")
        Catch
            GVSolInvCampo.DataSource = Nothing
        End Try
    End Sub

    Protected Sub LLENAR_DROP(ByVal bandera As Integer, ByVal condiciones As String, ByVal ITEM As Object, ByVal DataValueField As String, ByVal DataTextField As String, ByVal V_Nohay As String)
        Try
            ITEM.Items.Clear()

            Dim oraCommand As New SqlCommand
            oraCommand.CommandText = "SP_RP_SOLICITUDINVCAMPO"
            oraCommand.CommandType = CommandType.StoredProcedure
            oraCommand.Parameters.Add("v_bandera", SqlDbType.Decimal).Value = bandera
            oraCommand.Parameters.Add("v_usuario", SqlDbType.NVarChar).Value = ""
            oraCommand.Parameters.Add("v_agencia", SqlDbType.NVarChar).Value = ""
            oraCommand.Parameters.Add("v_condiciones", SqlDbType.NVarChar).Value = condiciones
            Dim objDSa As DataTable = Consulta_Procedure(oraCommand, "Filtros")

            If objDSa.Rows.Count = 0 Then
                to_limpia_ddl(ITEM, V_Nohay)
            Else

                ITEM.DataTextField = DataTextField
                ITEM.DataValueField = DataValueField
                ITEM.DataSource = objDSa
                ITEM.DataBind()
            End If

        Catch ex As Exception
            Dim ALGO As String = ex.ToString
            'LblMsj.Text = ex.Message
            'MpuMensajes.Show()
        End Try
    End Sub

    Protected Sub to_limpia_ddl(ByVal v_item As RadComboBox, ByVal v_NoHay As String)
        v_item.Items.Clear()
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


    Protected Sub Llena_Estatus()

        Dim v_cond = " "
        Dim v_bandera As Integer = 0

        If RblReporte.SelectedValue = "Solicitudes" Then
            v_cond = " "
            v_bandera = 0
        ElseIf RblReporte.SelectedValue = "Domicilio" Then
            v_cond = " "
            v_bandera = 4
        ElseIf RblReporte.SelectedValue = "Ingresos" Then
            v_cond = " "
            v_bandera = 8
        End If



        LLENAR_DROP(v_bandera, v_cond, RcbEstatus, "V_VALOR", "T_VALOR", "Sin Estatus")
    End Sub

    Protected Sub Llena_Tipo()
        Dim v_cond = " "
        Dim v_bandera As Integer = 0

        If RblReporte.SelectedValue = "Solicitudes" Then
            v_cond = " where HIST_SIC_ESTATUS in (" & Rad_Tcadena(RcbEstatus) & ") "
            v_bandera = 2
        ElseIf RblReporte.SelectedValue = "Domicilio" Then
            v_cond = " where ESTATUS in (" & Rad_Tcadena(RcbEstatus) & ") "
            v_bandera = 6
        ElseIf RblReporte.SelectedValue = "Ingresos" Then
            v_cond = " where ESTATUS in (" & Rad_Tcadena(RcbEstatus) & ") "
            v_bandera = 10
        End If

        LLENAR_DROP(v_bandera, v_cond, RcbTipo, "V_VALOR", "T_VALOR", "Sin Tipo")

    End Sub

    Protected Sub Llena_Usuario()
        Dim v_cond = " "
        Dim v_bandera As Integer = 0

        If RblReporte.SelectedValue = "Solicitudes" Then
            v_cond = " where HIST_SIC_ESTATUS in (" & Rad_Tcadena(RcbEstatus) & ") and HIST_SIC_TIPOINVESTIGACION in (" & Rad_Tcadena(RcbTipo) & ") "
            v_bandera = 1
        ElseIf RblReporte.SelectedValue = "Domicilio" Then
            v_cond = " where ESTATUS in (" & Rad_Tcadena(RcbEstatus) & ") and HIST_ID_WS_TIPOINVESTIGACION in (" & Rad_Tcadena(RcbTipo) & ") "
            v_bandera = 5
        ElseIf RblReporte.SelectedValue = "Ingresos" Then
            v_cond = " where ESTATUS in (" & Rad_Tcadena(RcbEstatus) & ") and HIST_II_TIPOINVESTIGACION in (" & Rad_Tcadena(RcbTipo) & ") "
            v_bandera = 9
        End If

        LLENAR_DROP(v_bandera, v_cond, RcbUsuario, "V_VALOR", "T_VALOR", "Sin Usuarios")
    End Sub


    Protected Sub RcbEstatus_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles RcbEstatus.SelectedIndexChanged
        Limpia_grid()
        RcbTipo.Visible = True
        RcbUsuario.Visible = False
        TxtFechaI.Visible = False
        TxtFechaF.Visible = False
        BtnGenerar.Visible = False
        Llena_Tipo()
    End Sub

    Protected Sub RcbTipo_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles RcbTipo.SelectedIndexChanged
        RcbTipo.Visible = True
        RcbUsuario.Visible = True
        TxtFechaI.Visible = True
        TxtFechaF.Visible = True
        BtnGenerar.Visible = True

        Llena_Usuario()
    End Sub

    Protected Sub Limpia_grid()
        Session("InvDom") = Nothing
        GVSolInvCampo.DataSource = Nothing
        GVSolInvCampo.DataBind()
        GVSolInvCampo.Visible = False
        BtnExportar.Visible = False
        RcbExportar.Visible = False
    End Sub

    Protected Sub Limpia_filtros()
        RcbTipo.Visible = False
        RcbUsuario.Visible = False
        TxtFechaI.Visible = False
        TxtFechaF.Visible = False
        BtnGenerar.Visible = False
        BtnExportar.Visible = False
        RcbExportar.Visible = False
    End Sub

    Protected Sub BtnGenerar_Click(sender As Object, e As EventArgs) Handles BtnGenerar.Click

        If Rad_Tcadena(RcbEstatus) = "" Then
            Mensaje("Seleccione Por Lo Menos Un Estatus")
        ElseIf Rad_Tcadena(RcbUsuario) = "" Then
            Mensaje("Seleccione Por Lo Menos Un Usuario")
        ElseIf TxtFechaI.DateInput.DisplayText = "" Then
            Mensaje("Seleccione Fecha De Inicio")
        ElseIf TxtFechaF.DateInput.DisplayText = "" Then
            Mensaje("Seleccione Fecha De Termino")
        Else


            Dim v_datos() As String = v_condicionesRep(RblReporte.SelectedValue).Split("|")
            Dim v_url As String = GetUrl("WSImagenInv.ashx")

            'Dim DtsInvCampo As DataTable = Class_Investigaciones.LlenarGridInvestigaciones(v_bandera, "", v_url, v_where).Tables(0)

            Dim DtsInvCampo As DataTable = Class_Investigaciones.DataTableInv(v_datos(1), "", v_url, v_datos(0), RblReporte.SelectedValue)

            If DtsInvCampo.Rows.Count = 0 Then
                Limpia_grid()
                Mensaje("No se encontraron resultados")
                'PanelGrid.Visible = False
                BtnExportar.Visible = False
                RcbExportar.Visible = False
            ElseIf DtsInvCampo.Rows.Count >= 10000 Then
                'PanelGrid.Visible = False
                BtnExportar.Visible = False
                RcbExportar.Visible = False
                Limpia_grid()

                Dim RUTA As String = StrRuta() & "\Salida\"
                If Not Directory.Exists(RUTA) Then
                    Directory.CreateDirectory(RUTA)
                End If
                Dim ARCHIVO As String = RUTA & RblReporte.SelectedValue.ToString & ".txt"
                Dim cuantos As Integer = DtsInvCampo.Rows.Count
                If File.Exists(ARCHIVO) Then
                    Kill(ARCHIVO)
                End If

                Dim fs As Stream
                fs = New System.IO.FileStream(ARCHIVO, IO.FileMode.OpenOrCreate)
                Dim sw As New System.IO.StreamWriter(fs)

                Dim v_encabezado As String = ""
                Dim v_delim As String = ","

                For h As Integer = 0 To DtsInvCampo.Columns.Count - 1
                    v_encabezado = v_encabezado & HttpUtility.HtmlDecode(DtsInvCampo.Columns(h).ColumnName) & v_delim
                Next

                sw.WriteLine(HttpUtility.HtmlDecode(v_encabezado))

                For a As Integer = 0 To cuantos - 1
                    Dim v_registros As String = ""
                    For r As Integer = 0 To DtsInvCampo.Columns.Count - 1
                        Dim v_valor As String = ""
                        If Not IsDBNull(DtsInvCampo.Rows(a)(DtsInvCampo.Columns(r).ColumnName)) Then
                            v_valor = HttpUtility.HtmlDecode(DtsInvCampo.Rows(a)(DtsInvCampo.Columns(r).ColumnName)).Replace(v_delim, "")
                        Else
                            v_valor = " "
                        End If
                        v_registros = v_registros & v_valor & v_delim
                    Next
                    sw.WriteLine(HttpUtility.HtmlDecode(v_registros))
                Next

                sw.Close()
                fs.Close()

                If File.Exists(ARCHIVO) Then
                    Dim ioflujo As FileInfo = New FileInfo(ARCHIVO)
                    Response.Clear()
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + ioflujo.Name)
                    Response.AddHeader("Content-Length", ioflujo.Length.ToString())
                    Response.ContentType = "application/octet-stream"
                    Response.WriteFile(ARCHIVO)
                    Response.End()
                End If
            Else
                Session("InvDom") = DtsInvCampo
                GVSolInvCampo.DataSource = DtsInvCampo
                GVSolInvCampo.DataBind()
                GVSolInvCampo.Visible = True
                BtnExportar.Visible = True
                RcbExportar.Visible = True
            End If


        End If

    End Sub




    Protected Sub RblReporte_SelectedIndexChanged(sender As Object, e As EventArgs)
        RcbExportar.ClearCheckedItems()
        If RblReporte.SelectedValue = "Solicitudes" Then
            RcbExportar.Items(0).Checked = True
        ElseIf RblReporte.SelectedValue = "Domicilio" Then
            RcbExportar.Items(1).Checked = True
        ElseIf RblReporte.SelectedValue = "Ingresos" Then
            RcbExportar.Items(2).Checked = True
        End If

        Inicio()
    End Sub

    Protected Sub Inicio()
        RadAjaxPanelFiltros.Visible = True
        Limpia_grid()
        Limpia_filtros()
        Llena_Estatus()
    End Sub

    Protected Sub BtnExportar_Click(sender As Object, e As EventArgs) Handles BtnExportar.Click
        Try


            Dim v_url As String = GetUrl("WSImagenInv.ashx")
            Dim collection As IList(Of RadComboBoxItem) = RcbExportar.CheckedItems
            Dim v_cadena As String = ""
            Dim v_datos() As String = Nothing

            For Each item As RadComboBoxItem In collection
                v_cadena = v_cadena & item.Value & "|"
            Next
            If collection.Count = 0 Then
                v_cadena = ""
            Else
                v_cadena = v_cadena.Substring(0, Len(v_cadena) - 1)
            End If
            Dim v_rep() As String = v_cadena.Split("|")


            If v_cadena = "" Then
                Mensaje("Selecione un reporte para exportar")
            Else
                'Dim arch_Excel As String = StrRuta() & "Salida\" & RblReporte.SelectedValue.ToString & "_" & DateTime.Now.ToString("ddMMyyyy_hhmm") & ".xlsx"
                Dim arch_Excel As String = StrRuta() & "Salida\" & "Investigaciones" & "_" & DateTime.Now.ToString("ddMMyyyy_hhmm") & ".xlsx"

                Dim book As New Workbook()

                If v_rep.Count > 0 Then
                    v_datos = v_condicionesRep(v_rep(0)).Split("|")
                    Dim dataTable0 As DataTable = Class_Investigaciones.DataTableInv(v_datos(1), "", v_url, v_datos(0), v_rep(0))

                    Dim sheet1 As Worksheet = book.Worksheets(0)
                    If dataTable0.Rows.Count = 0 Then
                        sheet1.Range("A1").Text = "No se encontraron resultados "
                    Else
                        sheet1.InsertDataTable(dataTable0, True, 1, 1)
                    End If
                    sheet1.Name = v_rep(0)
                End If
                If v_rep.Count > 1 Then
                    v_datos = v_condicionesRep(v_rep(1)).Split("|")
                    Dim dataTable1 As DataTable = Class_Investigaciones.DataTableInv(v_datos(1), "", v_url, v_datos(0), v_rep(1))

                    Dim sheet2 As Worksheet = book.Worksheets(1)
                    If dataTable1.Rows.Count = 0 Then
                        sheet2.Range("A1").Text = "No se encontraron resultados "
                    Else
                        sheet2.InsertDataTable(dataTable1, True, 1, 1)
                    End If
                    sheet2.Name = v_rep(1)
                End If
                If v_rep.Count > 2 Then
                    v_datos = v_condicionesRep(v_rep(2)).Split("|")
                    Dim dataTable2 As DataTable = Class_Investigaciones.DataTableInv(v_datos(1), "", v_url, v_datos(0), v_rep(2))

                    Dim sheet3 As Worksheet = book.Worksheets(2)
                    If dataTable2.Rows.Count = 0 Then
                        sheet3.Range("A1").Text = "No se encontraron resultados "
                    Else
                        sheet3.InsertDataTable(dataTable2, True, 1, 1)
                    End If
                    sheet3.Name = v_rep(2)
                End If

                book.SaveToFile(arch_Excel, ExcelVersion.Version2010)

                If File.Exists(arch_Excel) Then
                    Dim ioflujo As FileInfo = New FileInfo(arch_Excel)
                    Response.Clear()
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + ioflujo.Name)
                    Response.AddHeader("Content-Length", ioflujo.Length.ToString())
                    Response.ContentType = "application/octet-stream"
                    Response.WriteFile(arch_Excel)
                    Response.End()
                End If
            End If

        Catch ex As System.Threading.ThreadAbortException
        Catch ex As Exception
            Dim er As String = ex.ToString
        End Try


    End Sub

    Protected Function GetUrl(ByVal page As String) As String
        Dim splits As String() = Request.Url.AbsoluteUri.Split("/"c)
        If splits.Length >= 2 Then
            Dim url As String = splits(0) & "//"
            For i As Integer = 2 To splits.Length - 2
                url += splits(i)
                url += "/"
            Next
            Return url + page
        End If
        Return page
    End Function

    Protected Function v_condicionesRep(ByVal tipo As String) As String
        Dim v_where As String = ""
        Dim v_bandera As String = "0"

        If tipo = "Solicitudes" Then
            v_where = " where HIST_SIC_ESTATUS In (" & Rad_Tcadena(RcbEstatus) & ")" & IIf(RcbTipo.Items.Count <> RcbTipo.GetCheckedIndices.Count, " And HIST_SIC_TIPOINVESTIGACION In (" & Rad_Tcadena(RcbTipo) & ") ", " ") & IIf(RcbUsuario.Items.Count <> RcbUsuario.GetCheckedIndices.Count, " And HIST_SIC_USUARIO In (" & Rad_Tcadena(RcbUsuario) & ") ", " ") & "And HIST_SIC_DTECARGA between to_date('" & TxtFechaI.DateInput.DisplayText & " 00:00:00','dd/mm/rrrr hh24:mi:ss') and to_date('" & TxtFechaF.DateInput.DisplayText & " 23:59:59','dd/mm/rrrr hh24:mi:ss') "
            v_bandera = 3
        ElseIf tipo = "Domicilio" Then
            v_where = " where ESTATUS In (" & Rad_Tcadena(RcbEstatus) & ")" & IIf(RcbTipo.Items.Count <> RcbTipo.GetCheckedIndices.Count, " And HIST_ID_WS_TIPOINVESTIGACION In (" & Rad_Tcadena(RcbTipo) & ") ", " ") & IIf(RcbUsuario.Items.Count <> RcbUsuario.GetCheckedIndices.Count, " And HIST_ID_USUARIO In (" & Rad_Tcadena(RcbUsuario) & ") ", " ") & "And HIST_ID_DTEVISITA between to_date('" & TxtFechaI.DateInput.DisplayText & " 00:00:00','dd/mm/rrrr hh24:mi:ss') and to_date('" & TxtFechaF.DateInput.DisplayText & " 23:59:59','dd/mm/rrrr hh24:mi:ss') "
            v_bandera = 7
        ElseIf tipo = "Ingresos" Then
            v_where = " where ESTATUS In (" & Rad_Tcadena(RcbEstatus) & ")" & IIf(RcbTipo.Items.Count <> RcbTipo.GetCheckedIndices.Count, " And HIST_II_TIPOINVESTIGACION In (" & Rad_Tcadena(RcbTipo) & ") ", " ") & IIf(RcbUsuario.Items.Count <> RcbUsuario.GetCheckedIndices.Count, " And HIST_II_USUARIO In (" & Rad_Tcadena(RcbUsuario) & ") ", " ") & "And HIST_II_DTEVISITA between to_date('" & TxtFechaI.DateInput.DisplayText & " 00:00:00','dd/mm/rrrr hh24:mi:ss') and to_date('" & TxtFechaF.DateInput.DisplayText & " 23:59:59','dd/mm/rrrr hh24:mi:ss') "
            v_bandera = 11
        End If


        Return v_where & "|" & v_bandera

    End Function

End Class
