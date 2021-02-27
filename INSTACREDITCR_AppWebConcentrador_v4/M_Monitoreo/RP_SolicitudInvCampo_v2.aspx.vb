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
Partial Class M_Monitoreo_RP_SolicitudInvCampo_v2
    Inherits System.Web.UI.Page
    Dim random = New Random()

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

            Dim objDSa As DataTable = SP.RP_SOLICITUDINVCAMPO(bandera, "", "", condiciones)

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
        Select Case RblReporte.SelectedValue
            Case "Solicitudes"
                v_bandera = 0
            Case "Domicilio"
                v_bandera = 4
            Case "Ingresos"
                v_bandera = 8
        End Select
RcbEstatus.Enabled = true
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
        rcbGenerar.Visible = False
        Llena_Tipo()
    End Sub

    Protected Sub RcbTipo_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles RcbTipo.SelectedIndexChanged
        'RcbTipo.Visible = True
        RcbUsuario.Enabled = True
        TxtFechaI.Enabled = True
        TxtFechaF.Enabled = True
        rcbGenerar.Visible = True
        BtnGenerar.Enabled = True

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
        RcbTipo.Enabled = False
        RcbTipo.ClearCheckedItems()
        RcbTipo.Items.Clear()

        RcbUsuario.Enabled = False
        RcbUsuario.ClearCheckedItems()
        RcbUsuario.Items.Clear()

        TxtFechaI.Enabled = False
        TxtFechaI.Clear()

        TxtFechaF.Enabled = False
        TxtFechaF.Clear()

        BtnGenerar.Enabled = False

        rcbGenerar.Enabled = False
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

            Limpia_grid()
            PanelMapa.Style.Add("visibility", "hidden")
            BtnExportar.Visible = False

            If rcbGenerar.SelectedValue = "" Then
                Mensaje("Seleccione reporte a generar (Información / Mapa)")
            Else
                For Each item As ButtonListItem In rcbGenerar.Items
                    If item.Selected And item.Value = 1 Then
                        GeneraInfo(v_datos)
                    End If
                    If item.Selected And item.Value = 2 Then
                        GeneraMapa(v_datos)
                    End If
                Next
            End If

        End If

    End Sub

    Protected Sub GeneraInfo(ByVal v_datos() As String)
        Dim v_url As String = GetUrl("WSImagenInv.ashx")

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

    End Sub

    Protected Sub Inicio()
        RadAjaxPanelFiltros.Visible = True
        Limpia_grid()
        Limpia_filtros()
        Llena_Estatus()

        GMap1.reset()
        GMap1.resetMarkerManager()
        PanelMapa.Style.Add("visibility", "hidden")

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


    Protected Sub GeneraMapa(ByVal v_datos() As String)
        GMap1.reset()
        GMap1.resetMarkerManager()

        '--------------------------------
        Dim v_bandera As Integer = 0
        Dim v_campUser As String = ""
        Dim v_url As String = GetUrl("WSImagenInv.ashx")

        If RblReporte.SelectedValue = "Solicitudes" Then
            v_bandera = 1
        ElseIf RblReporte.SelectedValue = "Domicilio" Then
            v_bandera = 5
        ElseIf RblReporte.SelectedValue = "Ingresos" Then
            v_bandera = 9
        End If


        Dim DtsVisitadores As DataTable = SP.RP_SOLICITUDINVCAMPO(v_bandera, "", v_url, v_datos(0))



        '-------------------------------------------
        For i As Integer = 0 To DtsVisitadores.Rows.Count - 1
            If RblReporte.SelectedValue = "Solicitudes" Then
                v_campUser = " and HIST_SIC_USUARIO='" & DtsVisitadores.Rows(i)("V_VALOR") & "'"
            ElseIf RblReporte.SelectedValue = "Domicilio" Then
                v_campUser = " and HIST_ID_USUARIO='" & DtsVisitadores.Rows(i)("V_VALOR") & "'"
            ElseIf RblReporte.SelectedValue = "Ingresos" Then
                v_campUser = " and HIST_II_USUARIO='" & DtsVisitadores.Rows(i)("V_VALOR") & "'"
            End If


            Dim v_datos1 As Integer = v_datos(1)
            Dim v_condMonitoreo As String = v_datos(0)

            For Each item As ButtonListItem In rcbGenerar.Items
                If item.Selected And item.Value = 3 Then
                    v_condMonitoreo = " where HIST_MU_USUARIO= '" & DtsVisitadores.Rows(i)("V_VALOR") & "' and trunc(HIST_MU_DTEMONITOREO)= '" & TxtFechaF.DateInput.DisplayText & "' "
                    If RblReporte.SelectedValue = "Solicitudes" Then

                    ElseIf RblReporte.SelectedValue = "Domicilio" Then
                        v_datos1 = 15
                    ElseIf RblReporte.SelectedValue = "Ingresos" Then
                        v_datos1 = 16
                    End If
                End If
            Next


            Dim DtsUsuario As DataTable = SP.RP_SOLICITUDINVCAMPO(v_datos1, v_condMonitoreo, v_url, v_datos(0) & v_campUser)

            PintarPuntos(DtsUsuario)
        Next
    End Sub

    Protected Sub PintarPuntos(ByVal DtsUsuario As DataTable)
        Dim CADENA1(DtsUsuario.Rows.Count) As String
        Dim CADENA2(DtsUsuario.Rows.Count) As String
        Dim UFIJA As New GLatLng
        Session("CADENA1") = CADENA1
        Session("CADENA2") = CADENA2
        Dim Contar As Integer = 0
        ' Try
        For i As Integer = 0 To (DtsUsuario.Rows.Count - 1)
            Try
                If DtsUsuario.Rows(i)("Latitud") <> "0.0" And DtsUsuario.Rows(i)("Longitud") <> "0.0" Then

                    Dim Id As String = " "
                    Dim c As String = Chr(39)
                    Dim v_Tipo As String = RblReporte.SelectedValue
                    Dim v_html As String = ""
                    Dim COLORVB As String = "Yellow"

                    Dim Credito As String = ""
                    Dim Latitud As String = ""
                    Dim Longitud As String = ""

                    Dim v_part1 As String = ""
                    Dim v_part2 As String = ""
                    Dim v_base64 As String = ""
                    Dim v_nombre As String = "Imagen1"
                    Dim v_no As String = ""


                    If v_Tipo = "Solicitudes" Then
                        Credito = DtsUsuario.Rows(i)("Credito").ToString
                        Latitud = DtsUsuario.Rows(i)("Latitud").ToString
                        Longitud = DtsUsuario.Rows(i)("Longitud").ToString

                        Dim FOLIO_INVESTIGACION As String = DtsUsuario.Rows(i)("FOLIO_INVESTIGACION").ToString
                        Dim FOLIO_ENVIO As String = DtsUsuario.Rows(i)("FOLIO_ENVIO").ToString
                        Dim TIPO As String = DtsUsuario.Rows(i)("TIPO").ToString
                        'Dim CREDITO As String = DtsUsuario.Rows(i)("CREDITO").ToString
                        Dim NO_CLIENTE As String = DtsUsuario.Rows(i)("NO_CLIENTE").ToString
                        Dim MONTO As String = DtsUsuario.Rows(i)("MONTO").ToString
                        Dim FECHA_RESPUESTA As String = DtsUsuario.Rows(i)("FECHA_RESPUESTA").ToString
                        Dim TITULAR As String = DtsUsuario.Rows(i)("TITULAR").ToString
                        Dim INTEGRANTE As String = DtsUsuario.Rows(i)("INTEGRANTE").ToString
                        Dim TIPO_INTEGRANTE As String = DtsUsuario.Rows(i)("TIPO_INTEGRANTE").ToString
                        Dim DIRECCION As String = DtsUsuario.Rows(i)("DIRECCION").ToString
                        'Dim LATITUD As String = DtsUsuario.Rows(i)("LATITUD").ToString
                        'Dim LONGITUD As String = DtsUsuario.Rows(i)("LONGITUD").ToString
                        Dim HORARIO_VERIFICACION As String = DtsUsuario.Rows(i)("HORARIO_VERIFICACION").ToString
                        Dim ACTIVIDAD_GIRO As String = DtsUsuario.Rows(i)("ACTIVIDAD_GIRO").ToString
                        Dim PUESTO_ID As String = DtsUsuario.Rows(i)("PUESTO_ID").ToString
                        Dim PUESTO As String = DtsUsuario.Rows(i)("PUESTO").ToString
                        Dim ANALISTA As String = DtsUsuario.Rows(i)("ANALISTA").ToString
                        Dim COMENTARIO_ANALISTA As String = DtsUsuario.Rows(i)("COMENTARIO_ANALISTA").ToString
                        Dim USUARIO As String = DtsUsuario.Rows(i)("USUARIO").ToString
                        Dim VISITADA As String = IIf(DtsUsuario.Rows(i)("VISITADA").ToString = "1", "Si", "No")
                        Dim REQUIERE_VISITA As String = DtsUsuario.Rows(i)("REQUIERE_VISITA").ToString
                        Dim RESULTADO As String = DtsUsuario.Rows(i)("RESULTADO").ToString
                        Dim ESTATUS As String = DtsUsuario.Rows(i)("ESTATUS").ToString

                        If VISITADA = "Si" Then
                            COLORVB = "Green"
                        ElseIf VISITADA = "Si" And USUARIO = "" Then
                            COLORVB = "Yellow"
                        Else
                            COLORVB = "Red"
                        End If


                        v_html = "<head> " _
    & "  <link rel=""stylesheet"" href=""https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css"">" _
    & "  <script src=""https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js""></script>" _
    & "  <script src=""https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js""></script>" _
    & "</head>" _
    & "<body>" _
    & "<div class=""container"" style=""width:500px"">" _
    & "  <h4>" & TIPO & "</h4>" _
    & "  <h6><strong>(" & FOLIO_INVESTIGACION & " - " & INTEGRANTE & ")</strong></h6>" _
    & "  <div class=""panel-group"" id=""accordion"">" _
    & "    <div class=""panel panel-default"">" _
    & "      <div class=""panel-heading"">" _
    & "        <h4 class=""panel-title"">" _
    & "          <a data-toggle=""collapse"" data-parent=""#accordion"" href=""#collapse1"">Investigación</a>" _
    & "        </h4>" _
    & "      </div>" _
    & "      <div id=""collapse1"" class=""panel-collapse collapse"">" _
    & "        <div class=""panel-body""><strong>Folio de Investigación:</strong> " & FOLIO_INVESTIGACION & "</div>" _
    & "        <div class=""panel-body""><strong>Folio de Envio:</strong> " & FOLIO_ENVIO & "</div>" _
    & "        <div class=""panel-body""><strong>Tipo de Investigacion</strong> " & TIPO & "</div>" _
    & "      </div>" _
    & "    </div>" _
    & "    <div class=""panel panel-default"">" _
    & "      <div class=""panel-heading"">" _
    & "        <h4 class=""panel-title"">" _
    & "          <a data-toggle=""collapse"" data-parent=""#accordion"" href=""#collapse2"">Datos Persona Investigar</a>" _
    & "        </h4>" _
    & "      </div>" _
    & "      <div id=""collapse2"" class=""panel-collapse collapse"">" _
    & "        <div class=""panel-body""><strong>Credito:</strong> " & Credito & "</div>" _
    & "        <div class=""panel-body""><strong>No. de Cliente:</strong> " & NO_CLIENTE & "</div>" _
    & "        <div class=""panel-body""><strong>Monto Solicitado:</strong> " & MONTO & "</div>" _
    & "        <div class=""panel-body""><strong>Fecha de Respuesta:</strong> " & FECHA_RESPUESTA & "</div>" _
    & "        <div class=""panel-body""><strong>Nombre Titular:</strong> " & TITULAR & "</div>" _
    & "        <div class=""panel-body""><strong>Nombre Integrante:</strong> " & INTEGRANTE & "</div>" _
    & "        <div class=""panel-body""><strong>Tipo de Integrante:</strong> " & TIPO_INTEGRANTE & "</div>" _
    & "      </div>" _
    & "    </div>" _
    & "    <div class=""panel panel-default"">" _
    & "      <div class=""panel-heading"">" _
    & "        <h4 class=""panel-title"">" _
    & "          <a data-toggle=""collapse"" data-parent=""#accordion"" href=""#collapse3"">Datos Generales</a>" _
    & "        </h4>" _
    & "      </div>" _
    & "      <div id=""collapse3"" class=""panel-collapse collapse"">" _
    & "        <div class=""panel-body""><strong>Direccion:</strong> " & DIRECCION & "</div>" _
    & "        <div class=""panel-body""><strong>Coordenadas:</strong> " & Latitud & "," & Longitud & "</div>" _
    & "        <div class=""panel-body""><strong>Horario de Verificación:</strong> " & HORARIO_VERIFICACION & "</div>" _
    & "        <div class=""panel-body""><strong>Actividad / Giro:</strong> " & ACTIVIDAD_GIRO & "</div>" _
    & "        <div class=""panel-body""><strong>Puesto Id:</strong> " & PUESTO_ID & "</div>" _
    & "        <div class=""panel-body""><strong>Puesto:</strong> " & PUESTO & "</div>" _
    & "      </div>" _
    & "    </div>" _
    & "    <div class=""panel panel-default"">" _
    & "      <div class=""panel-heading"">" _
    & "        <h4 class=""panel-title"">" _
    & "          <a data-toggle=""collapse"" data-parent=""#accordion"" href=""#collapse4"">Asignación / Estatus</a>" _
    & "        </h4>" _
    & "      </div>" _
    & "      <div id=""collapse4"" class=""panel-collapse collapse"">" _
    & "        <div class=""panel-body""><strong>Analista Asignado:</strong> " & ANALISTA & "</div>" _
    & "        <div class=""panel-body""><strong>Comentario Analista:</strong> " & COMENTARIO_ANALISTA & "</div>" _
    & "        <div class=""panel-body""><strong>Usuario Asignado:</strong> " & USUARIO & "</div>" _
    & "        <div class=""panel-body""><strong>Visitada:</strong> " & VISITADA & "</div>" _
    & "        <div class=""panel-body""><strong>¿Requiere Visita?:</strong> " & REQUIERE_VISITA & "</div>" _
    & "        <div class=""panel-body""><strong>Resultado:</strong> " & RESULTADO & "</div>" _
    & "        <div class=""panel-body""><strong>Estatus MC:</strong> " & ESTATUS & "</div>" _
    & "      </div>" _
    & "    </div>" _
    & "  </div> " _
    & "</div>" _
    & "</body>"


                    ElseIf v_Tipo = "Domicilio" Then
                        Credito = DtsUsuario.Rows(i)("Credito").ToString
                        Latitud = DtsUsuario.Rows(i)("Latitud").ToString
                        Longitud = DtsUsuario.Rows(i)("Longitud").ToString

                        Dim FOLIO_INVESTIGACION As String = DtsUsuario.Rows(i)("FOLIO_INVESTIGACION").ToString
                        Dim FOLIO_ENVIO As String = DtsUsuario.Rows(i)("FOLIO_ENVIO").ToString
                        Dim TIPO As String = DtsUsuario.Rows(i)("TIPO").ToString
                        'Dim CREDITO As String = DtsUsuario.Rows(i)(" CREDITO").ToString
                        Dim ESTATUS As String = DtsUsuario.Rows(i)("ESTATUS").ToString
                        Dim HIST_ID_CORRECTO As String = DtsUsuario.Rows(i)("HIST_ID_CORRECTO").ToString
                        Dim HIST_ID_CAPTURADOC As String = DtsUsuario.Rows(i)("HIST_ID_CAPTURADOC").ToString
                        Dim HIST_ID_LOCALIZADO As String = DtsUsuario.Rows(i)("HIST_ID_LOCALIZADO").ToString
                        Dim HIST_ID_V1_CONOCE As String = DtsUsuario.Rows(i)("HIST_ID_V1_CONOCE").ToString
                        Dim HIST_ID_V1_VIVE As String = DtsUsuario.Rows(i)("HIST_ID_V1_VIVE").ToString
                        Dim HIST_ID_V1_TIEMPO As String = DtsUsuario.Rows(i)("HIST_ID_V1_TIEMPO").ToString
                        Dim HIST_ID_V1_DEDICA As String = DtsUsuario.Rows(i)("HIST_ID_V1_DEDICA").ToString
                        Dim HIST_ID_V2_CONOCE As String = DtsUsuario.Rows(i)("HIST_ID_V2_CONOCE").ToString
                        Dim HIST_ID_V2_VIVE As String = DtsUsuario.Rows(i)("HIST_ID_V2_VIVE").ToString
                        Dim HIST_ID_V2_TIEMPO As String = DtsUsuario.Rows(i)("HIST_ID_V2_TIEMPO").ToString
                        Dim HIST_ID_V2_DEDICA As String = DtsUsuario.Rows(i)("HIST_ID_V2_DEDICA").ToString
                        Dim HIST_ID_ATENDIERON As String = DtsUsuario.Rows(i)("HIST_ID_ATENDIERON").ToString
                        Dim HIST_ID_QUIENATIENDE As String = DtsUsuario.Rows(i)("HIST_ID_QUIENATIENDE").ToString
                        Dim HIST_ID_NOMBREQA As String = DtsUsuario.Rows(i)("HIST_ID_NOMBREQA").ToString
                        Dim HIST_ID_TELEFONOQA As String = DtsUsuario.Rows(i)("HIST_ID_TELEFONOQA").ToString
                        Dim HIST_ID_VIVE As String = DtsUsuario.Rows(i)("HIST_ID_VIVE").ToString
                        Dim HIST_ID_TIEMPO As String = DtsUsuario.Rows(i)("HIST_ID_TIEMPO").ToString
                        Dim HIST_ID_DEPECONO As String = DtsUsuario.Rows(i)("HIST_ID_DEPECONO").ToString
                        Dim HIST_ID_CUANTOS As String = DtsUsuario.Rows(i)("HIST_ID_CUANTOS").ToString
                        Dim HIST_ID_DEDICA As String = DtsUsuario.Rows(i)("HIST_ID_DEDICA").ToString
                        Dim HIST_ID_TIEMPODE As String = DtsUsuario.Rows(i)("HIST_ID_TIEMPODE").ToString
                        Dim HIST_ID_FINALIDAD As String = DtsUsuario.Rows(i)("HIST_ID_FINALIDAD").ToString
                        Dim HIST_ID_OBSERVACIONESG As String = DtsUsuario.Rows(i)("HIST_ID_OBSERVACIONESG").ToString
                        Dim HIST_ID_REQUIEREVISITA As String = DtsUsuario.Rows(i)("HIST_ID_REQUIEREVISITA").ToString
                        Dim HIST_ID_RESULTADO As String = DtsUsuario.Rows(i)("HIST_ID_RESULTADO").ToString
                        Dim HIST_ID_MOTIVO As String = DtsUsuario.Rows(i)("HIST_ID_MOTIVO").ToString
                        'Dim LATITUD As String = DtsUsuario.Rows(i)(" LATITUD").ToString
                        'Dim LONGITUD As String = DtsUsuario.Rows(i)(" LONGITUD").ToString
                        Dim HIST_ID_DTEVISITA As String = DtsUsuario.Rows(i)("HIST_ID_DTEVISITA").ToString
                        Dim HIST_ID_USUARIO As String = DtsUsuario.Rows(i)("HIST_ID_USUARIO").ToString


                        If TIPO = "Domicilio" Then
                            Id = DtsUsuario.Rows(i)("IdFoto").ToString

                            'img carrusel
                            Try

                                Dim DtsImagenes As DataTable = SP.RP_SOLICITUDINVCAMPO(14, Credito, Id)

                                If DtsImagenes.Rows.Count > 0 Then
                                    For x As Integer = 0 To (DtsImagenes.Rows.Count - 1)
                                        v_base64 = DtsImagenes.Rows(x)("IMAGEN_B64").ToString
                                        v_nombre = DtsImagenes.Rows(x)("NOMBRE").ToString
                                        v_no = DtsImagenes.Rows(x)("NUM").ToString

                                        If x = 0 Then
                                            v_part1 = v_part1 & " <li data-target=""#myCarousel"" data-slide-to=""0"" class=""active""></li>"
                                            v_part2 = v_part2 & "      <div class=""item active"" >" _
        & "        <img src=""data:Image/ png;base64, " & v_base64 & """ />" _
        & "      </div>"
                                        Else
                                            v_part1 = v_part1 & " <li data-target=""#myCarousel"" data-slide-to=""" & x & """></li>"
                                            v_part2 = v_part2 & "      <div  class=""item"" >" _
        & "        <img src=""data:Image/ png;base64, " & v_base64 & """ />" _
        & "      </div>"
                                        End If
                                    Next
                                End If
                            Catch ex As Exception
                            End Try

                            Dim v_carrusel As String = " <div class=""panel panel-default"">" _
        & "      <div class=""panel-heading"">" _
        & "        <h4 class=""panel-title"">" _
        & "          <a data-toggle=""collapse"" data-parent=""#accordion"" href=""#collapse3"">Imagenes</a>" _
        & "        </h4>" _
        & "      </div>" _
        & "      <div id=""collapse3"" class=""panel-collapse collapse"">" _
        & "<div class=""container"" style=""width:100%""> " _
        & "  <div id=""myCarousel"" Class=""carousel slide"" data-ride=""carousel"">" _
        & "    <ol class=""carousel-indicators"">" _
        & v_part1 _
        & "   </ol>" _
        & "    <div class=""carousel-inner"">" _
        & v_part2 _
        & "    </div>" _
        & "    <a class=""left carousel-control"" href=""#myCarousel"" data-slide=""prev"">" _
        & "      <span class=""glyphicon glyphicon-chevron-left""></span>" _
        & "      <span class=""sr-only"">Previous</span>" _
        & "    </a>" _
        & "    <a class=""right carousel-control"" href=""#myCarousel"" data-slide=""next"">" _
        & "      <span class=""glyphicon glyphicon-chevron-right""></span>" _
        & "      <span class=""sr-only"">Next</span>" _
        & "    </a>" _
        & "  </div>" _
        & "</div>" _
        & "      </div>" _
        & "    </div>"


                            'Dim Foto2 As String = DtsUsuario.Rows(i)(" Foto2").ToString
                            'Dim Foto3 As String = DtsUsuario.Rows(i)(" Foto3").ToString
                            'Dim Foto4 As String = DtsUsuario.Rows(i)(" Foto4").ToString
                            'Dim Foto5 As String = DtsUsuario.Rows(i)(" Foto5").ToString
                            'Dim Foto6 As String = DtsUsuario.Rows(i)(" Foto6").ToString
                            Dim CORREOS As String = DtsUsuario.Rows(i)("CORREOS").ToString
                            Dim TELEFONOS As String = DtsUsuario.Rows(i)("TELEFONOS").ToString
                            Dim DIRECCIONES As String = DtsUsuario.Rows(i)("DIRECCIONES").ToString

                            v_html = "<head> " _
        & "  <link rel=""stylesheet"" href=""https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css"">" _
        & "  <script src=""https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js""></script>" _
        & "  <script src=""https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js""></script>" _
        & "</head>" _
        & "<body>" _
        & "<div class=""container"" style=""width:500px"">" _
        & "  <h4>" & TIPO & "</h4>" _
        & "  <h6><strong>(" & FOLIO_INVESTIGACION & " - " & Credito & ")</strong></h6>" _
        & "  <div class=""panel-group"" id=""accordion"">" _
        & "    <div class=""panel panel-default"">" _
        & "      <div class=""panel-heading"">" _
        & "        <h4 class=""panel-title"">" _
        & "          <a data-toggle=""collapse"" data-parent=""#accordion"" href=""#collapse1"">Investigación</a>" _
        & "        </h4>" _
        & "      </div>" _
        & "      <div id=""collapse1"" class=""panel-collapse collapse"">" _
        & "        <div class=""panel-body""><strong>Folio de Investigación:</strong> " & FOLIO_INVESTIGACION & "</div>" _
        & "        <div class=""panel-body""><strong>Folio de Envio:</strong> " & FOLIO_ENVIO & "</div>" _
        & "        <div class=""panel-body""><strong>Tipo de Investigacion</strong> " & TIPO & "</div>" _
        & "        <div class=""panel-body""><strong>Credito</strong> " & Credito & "</div>" _
        & "        <div class=""panel-body""><strong>Estatus</strong> " & ESTATUS & "</div>" _
        & "        <div class=""panel-body""><strong>Latitud </strong> " & Latitud & "</div>" _
        & "        <div class=""panel-body""><strong>Longitud </strong> " & Longitud & "</div>" _
        & "        <div class=""panel-body""><strong>Fecha de Visita </strong> " & HIST_ID_DTEVISITA & "</div>" _
        & "        <div class=""panel-body""><strong>Usuario </strong> " & HIST_ID_USUARIO & "</div>" _
        & "      </div>" _
        & "    </div>" _
        & "    <div class=""panel panel-default"">" _
        & "      <div class=""panel-heading"">" _
        & "        <h4 class=""panel-title"">" _
        & "          <a data-toggle=""collapse"" data-parent=""#accordion"" href=""#collapse2"">Formulario</a>" _
        & "        </h4>" _
        & "      </div>" _
        & "      <div id=""collapse2"" class=""panel-collapse collapse"">" _
        & "        <div class=""panel-body""><strong>¿El Domicilio capturado es correcto? </strong> " & Credito & "</div>" _
        & "        <div class=""panel-body""><strong>Comentarios Domicilio:</strong> " & HIST_ID_CORRECTO & "</div>" _
        & "        <div class=""panel-body""><strong>Monto Solicitado:</strong> " & HIST_ID_CAPTURADOC & "</div>" _
        & "        <div class=""panel-body""><strong>¿El Domicilio fue localizado? </strong> " & HIST_ID_LOCALIZADO & "</div>" _
        & "        <div class=""panel-body""><strong>¿Vecino1-Conoce a la persona a investigar?:</strong> " & HIST_ID_V1_CONOCE & "</div>" _
        & "        <div class=""panel-body""><strong>¿Vecino1-Vive la persona a investigar en domicilio declarado? </strong> " & HIST_ID_V1_VIVE & "</div>" _
        & "        <div class=""panel-body""><strong>¿Vecino1-Cuanto tiempo tiene el participante viviendo en el domicilio? </strong> " & HIST_ID_V1_TIEMPO & "</div>" _
        & "        <div class=""panel-body""><strong>¿Vecino1-Sabe a que se dedica la persona a investigar? </strong> " & HIST_ID_V1_DEDICA & "</div>" _
        & "        <div class=""panel-body""><strong>¿Vecino2-Conoce a la persona a investigar? </strong> " & HIST_ID_V2_CONOCE & "</div>" _
        & "        <div class=""panel-body""><strong>¿Vecino2-Vive la persona a investigar en domicilio declarad? </strong> " & HIST_ID_V2_VIVE & "</div>" _
        & "        <div class=""panel-body""><strong>¿Vecino2-Cuanto tiempo tiene el participante viviendo en el domicilio? </strong> " & HIST_ID_V2_TIEMPO & "</div>" _
        & "        <div class=""panel-body""><strong>¿Vecino2-Sabe a que se dedica la persona a investigar? </strong> " & HIST_ID_V2_DEDICA & "</div>" _
        & "        <div class=""panel-body""><strong>¿Atendieron en Domicilio de la persona a investigar? </strong> " & HIST_ID_ATENDIERON & "</div>" _
    & "        <div class=""panel-body""><strong>¿Quien atiende? </strong> " & HIST_ID_QUIENATIENDE & "</div>" _
    & "        <div class=""panel-body""><strong>¿Nombre de quien atiende? </strong> " & HIST_ID_NOMBREQA & "</div>" _
    & "        <div class=""panel-body""><strong>¿Numero de contacto? </strong> " & HIST_ID_TELEFONOQA & "</div>" _
    & "        <div class=""panel-body""><strong>¿Persona a investigar vive en el domicilio? </strong> " & HIST_ID_VIVE & "</div>" _
    & "        <div class=""panel-body""><strong>¿Cuanto tiempo tiene la persona a investigar  viviendo en el domicilio? </strong> " & HIST_ID_TIEMPO & "</div>" _
    & "        <div class=""panel-body""><strong>¿Tiene dependientes economicos? </strong> " & HIST_ID_DEPECONO & "</div>" _
    & "        <div class=""panel-body""><strong>¿Cuantos? </strong> " & HIST_ID_CUANTOS & "</div>" _
    & "        <div class=""panel-body""><strong>¿Sabe a que se dedica la persona a investigar? </strong> " & HIST_ID_DEDICA & "</div>" _
    & "        <div class=""panel-body""><strong>¿Cuanto tiempo tiene la persona a investigar dedicandose a esta actividad? </strong> " & HIST_ID_TIEMPODE & "</div>" _
    & "        <div class=""panel-body""><strong>¿Cual es la finalidad del credito? </strong> " & HIST_ID_FINALIDAD & "</div>" _
    & "        <div class=""panel-body""><strong>Observaciones Generales </strong> " & HIST_ID_OBSERVACIONESG & "</div>" _
    & "        <div class=""panel-body""><strong>¿Requiere Visita? </strong> " & HIST_ID_REQUIEREVISITA & "</div>" _
    & "        <div class=""panel-body""><strong>Resultado </strong> " & HIST_ID_RESULTADO & "</div>" _
    & "        <div class=""panel-body""><strong>Motivo </strong> " & HIST_ID_MOTIVO & "</div>" _
        & "      </div>" _
        & "    </div>" _
        & v_carrusel _
        & "  </div> " _
        & "</div>" _
        & "</body>"

                        Else

                            COLORVB = "Red"
                            v_html = "<head> " _
    & "  <link rel=""stylesheet"" href=""https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css"">" _
    & "  <script src=""https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js""></script>" _
    & "  <script src=""https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js""></script>" _
    & "</head>" _
    & "<body>" _
    & "<div class=""container"" style=""width:500px"">" _
    & "  <h4>Punto de Monitoreo</h4>" _
    & "  <h6><strong>(" & HIST_ID_USUARIO & ")</strong></h6>" _
    & "  <div class=""panel-group"" id=""accordion"">" _
    & "    <div class=""panel panel-default"">" _
    & "      <div class=""panel-heading"">" _
    & "        <h4 class=""panel-title"">" _
    & "          <a data-toggle=""collapse"" data-parent=""#accordion"" href=""#collapse1"">Información</a>" _
    & "        </h4>" _
    & "      </div>" _
    & "      <div id=""collapse1"" class=""panel-collapse collapse in"">" _
    & "        <div class=""panel-body""><strong>Coordenadas:</strong> " & Latitud & "," & Longitud & "</div>" _
    & "        <div class=""panel-body""><strong>Internet:</strong> " & HIST_ID_RESULTADO & "</div>" _
    & "        <div class=""panel-body""><strong>Bateria:</strong> " & HIST_ID_REQUIEREVISITA & "</div>" _
    & "        <div class=""panel-body""><strong>Fecha:</strong> " & HIST_ID_DTEVISITA & "</div>" _
    & "      </div>" _
    & "    </div>" _
    & "  </div> " _
    & "</div>" _
    & "</body>"



                        End If



                    ElseIf v_Tipo = "Ingresos" Then
                        Credito = DtsUsuario.Rows(i)("Credito").ToString
                        Latitud = DtsUsuario.Rows(i)("Latitud").ToString
                        Longitud = DtsUsuario.Rows(i)("Longitud").ToString

                        Dim HIST_II_FOLIOINVESTIGACION As String = DtsUsuario.Rows(i)("HIST_II_FOLIOINVESTIGACION").ToString
                        Dim HIST_II_FOLIOENVIO As String = DtsUsuario.Rows(i)("HIST_II_FOLIOENVIO").ToString
                        Dim HIST_II_TIPOINVESTIGACION As String = DtsUsuario.Rows(i)("HIST_II_TIPOINVESTIGACION").ToString
                        'Dim CREDITO As String = DtsUsuario.Rows(i)("CREDITO").ToString
                        Dim ESTATUS As String = DtsUsuario.Rows(i)("ESTATUS").ToString
                        Dim HIST_II_CORRECTO As String = DtsUsuario.Rows(i)("HIST_II_CORRECTO").ToString
                        Dim HIST_II_LOCALIZADO As String = DtsUsuario.Rows(i)("HIST_II_LOCALIZADO").ToString
                        Dim HIST_II_V1_CONOCE As String = DtsUsuario.Rows(i)("HIST_II_V1_CONOCE").ToString
                        Dim HIST_II_V1_DEDICA As String = DtsUsuario.Rows(i)("HIST_II_V1_DEDICA").ToString
                        Dim HIST_II_V1_TIEMPO As String = DtsUsuario.Rows(i)("HIST_II_V1_TIEMPO").ToString
                        Dim HIST_II_V2_CONOCE As String = DtsUsuario.Rows(i)("HIST_II_V2_CONOCE").ToString
                        Dim HIST_II_V2_DEDICA As String = DtsUsuario.Rows(i)("HIST_II_V2_DEDICA").ToString
                        Dim HIST_II_V2_TIEMPO As String = DtsUsuario.Rows(i)("HIST_II_V2_TIEMPO").ToString
                        Dim HIST_II_ATENDIERON As String = DtsUsuario.Rows(i)("HIST_II_ATENDIERON").ToString
                        Dim HIST_II_QUIENATIENDE As String = DtsUsuario.Rows(i)("HIST_II_QUIENATIENDE").ToString
                        Dim HIST_II_NOMBREQA As String = DtsUsuario.Rows(i)("HIST_II_NOMBREQA").ToString
                        Dim HIST_II_DIASLABORA As String = DtsUsuario.Rows(i)("HIST_II_DIASLABORA").ToString
                        Dim HIST_II_TIEMPO As String = DtsUsuario.Rows(i)("HIST_II_TIEMPO").ToString
                        Dim HIST_II_METODOPAGO As String = DtsUsuario.Rows(i)("HIST_II_METODOPAGO").ToString
                        Dim HIST_II_INGRESOMES As String = DtsUsuario.Rows(i)("HIST_II_INGRESOMES").ToString
                        Dim HIST_II_CONCUERDALABOR As String = DtsUsuario.Rows(i)("HIST_II_CONCUERDALABOR").ToString
                        Dim HIST_II_OBSERVACIONESG As String = DtsUsuario.Rows(i)("HIST_II_OBSERVACIONESG").ToString
                        Dim HIST_II_REQUIEREVISITA As String = DtsUsuario.Rows(i)("HIST_II_REQUIEREVISITA").ToString
                        Dim HIST_II_RESULTADO As String = DtsUsuario.Rows(i)("HIST_II_RESULTADO").ToString
                        Dim HIST_II_MOTIVO As String = DtsUsuario.Rows(i)("HIST_II_MOTIVO").ToString
                        'Dim LATITUD As String = DtsUsuario.Rows(i)("LATITUD").ToString
                        'Dim LONGITUD As String = DtsUsuario.Rows(i)("LONGITUD").ToString
                        Dim HIST_II_DTEVISITA As String = DtsUsuario.Rows(i)("HIST_II_DTEVISITA").ToString
                        Dim HIST_II_USUARIO As String = DtsUsuario.Rows(i)("HIST_II_USUARIO").ToString


                        If HIST_II_TIPOINVESTIGACION = "MONITOREO" Then

                            COLORVB = "Red"
                            v_html = "<head> " _
    & "  <link rel=""stylesheet"" href=""https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css"">" _
    & "  <script src=""https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js""></script>" _
    & "  <script src=""https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js""></script>" _
    & "</head>" _
    & "<body>" _
    & "<div class=""container"" style=""width:500px"">" _
    & "  <h4>Punto de Monitoreo</h4>" _
    & "  <h6><strong>(" & HIST_II_USUARIO & ")</strong></h6>" _
    & "  <div class=""panel-group"" id=""accordion"">" _
    & "    <div class=""panel panel-default"">" _
    & "      <div class=""panel-heading"">" _
    & "        <h4 class=""panel-title"">" _
    & "          <a data-toggle=""collapse"" data-parent=""#accordion"" href=""#collapse1"">Información</a>" _
    & "        </h4>" _
    & "      </div>" _
    & "      <div id=""collapse1"" class=""panel-collapse collapse in"">" _
    & "        <div class=""panel-body""><strong>Coordenadas:</strong> " & Latitud & "," & Longitud & "</div>" _
    & "        <div class=""panel-body""><strong>Internet:</strong> " & HIST_II_RESULTADO & "</div>" _
    & "        <div class=""panel-body""><strong>Bateria:</strong> " & HIST_II_REQUIEREVISITA & "</div>" _
    & "        <div class=""panel-body""><strong>Fecha:</strong> " & HIST_II_DTEVISITA & "</div>" _
    & "      </div>" _
    & "    </div>" _
    & "  </div> " _
    & "</div>" _
    & "</body>"

                        Else

                            Id = DtsUsuario.Rows(i)("IdFoto").ToString

                            'img carrusel
                            Try

                                Dim DtsImagenes As DataTable = SP.RP_SOLICITUDINVCAMPO(14, Credito, Id)

                                If DtsImagenes.Rows.Count > 0 Then
                                    For x As Integer = 0 To (DtsImagenes.Rows.Count - 1)
                                        v_base64 = DtsImagenes.Rows(x)("IMAGEN_B64").ToString
                                        v_nombre = DtsImagenes.Rows(x)("NOMBRE").ToString
                                        v_no = DtsImagenes.Rows(x)("NUM").ToString

                                        If x = 0 Then
                                            v_part1 = v_part1 & " <li data-target=""#myCarousel"" data-slide-to=""0"" class=""active""></li>"
                                            v_part2 = v_part2 & "      <div class=""item active"" >" _
        & "        <img src=""data:Image/ png;base64, " & v_base64 & """ />" _
        & "      </div>"
                                        Else
                                            v_part1 = v_part1 & " <li data-target=""#myCarousel"" data-slide-to=""" & x & """></li>"
                                            v_part2 = v_part2 & "      <div  class=""item"" >" _
        & "        <img src=""data:Image/ png;base64, " & v_base64 & """ />" _
        & "      </div>"
                                        End If
                                    Next
                                End If
                            Catch ex As Exception
                            End Try

                            Dim v_carrusel As String = " <div class=""panel panel-default"">" _
        & "      <div class=""panel-heading"">" _
        & "        <h4 class=""panel-title"">" _
        & "          <a data-toggle=""collapse"" data-parent=""#accordion"" href=""#collapse3"">Imagenes</a>" _
        & "        </h4>" _
        & "      </div>" _
        & "      <div id=""collapse3"" class=""panel-collapse collapse"">" _
        & "<div class=""container"" style=""width:100%""> " _
        & "  <div id=""myCarousel"" Class=""carousel slide"" data-ride=""carousel"">" _
        & "    <ol class=""carousel-indicators"">" _
        & v_part1 _
        & "   </ol>" _
        & "    <div class=""carousel-inner"">" _
        & v_part2 _
        & "    </div>" _
        & "    <a class=""left carousel-control"" href=""#myCarousel"" data-slide=""prev"">" _
        & "      <span class=""glyphicon glyphicon-chevron-left""></span>" _
        & "      <span class=""sr-only"">Previous</span>" _
        & "    </a>" _
        & "    <a class=""right carousel-control"" href=""#myCarousel"" data-slide=""next"">" _
        & "      <span class=""glyphicon glyphicon-chevron-right""></span>" _
        & "      <span class=""sr-only"">Next</span>" _
        & "    </a>" _
        & "  </div>" _
        & "</div>" _
        & "      </div>" _
        & "    </div>"

                            v_html = "<head> " _
        & "  <link rel=""stylesheet"" href=""https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css"">" _
        & "  <script src=""https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js""></script>" _
        & "  <script src=""https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js""></script>" _
        & "</head>" _
        & "<body>" _
        & "<div class=""container"" style=""width:500px"">" _
        & "  <h4>" & HIST_II_TIPOINVESTIGACION & "</h4>" _
        & "  <h6><strong>(" & HIST_II_FOLIOINVESTIGACION & " - " & Credito & ")</strong></h6>" _
        & "  <div class=""panel-group"" id=""accordion"">" _
        & "    <div class=""panel panel-default"">" _
        & "      <div class=""panel-heading"">" _
        & "        <h4 class=""panel-title"">" _
        & "          <a data-toggle=""collapse"" data-parent=""#accordion"" href=""#collapse1"">Investigación</a>" _
        & "        </h4>" _
        & "      </div>" _
        & "      <div id=""collapse1"" class=""panel-collapse collapse"">" _
        & "        <div class=""panel-body""><strong>Folio de Investigación:</strong> " & HIST_II_FOLIOINVESTIGACION & "</div>" _
    & "        <div class=""panel-body""><strong>Folio de Envio </strong> " & HIST_II_FOLIOENVIO & "</div>" _
    & "        <div class=""panel-body""><strong>Tipo de Investigacion </strong> " & HIST_II_TIPOINVESTIGACION & "</div>" _
    & "        <div class=""panel-body""><strong>Credito </strong> " & Credito & "</div>" _
    & "        <div class=""panel-body""><strong>Estatus MC </strong> " & ESTATUS & "</div>" _
    & "        <div class=""panel-body""><strong>Coordeandas </strong> " & Latitud & "," & Longitud & "</div>" _
    & "        <div class=""panel-body""><strong>Fecha de Visita </strong> " & HIST_II_DTEVISITA & "</div>" _
    & "        <div class=""panel-body""><strong>Usuario </strong> " & HIST_II_USUARIO & "</div>" _
        & "      </div>" _
        & "    </div>" _
        & "    <div class=""panel panel-default"">" _
        & "      <div class=""panel-heading"">" _
        & "        <h4 class=""panel-title"">" _
        & "          <a data-toggle=""collapse"" data-parent=""#accordion"" href=""#collapse2"">Formulario</a>" _
        & "        </h4>" _
        & "      </div>" _
        & "      <div id=""collapse2"" class=""panel-collapse collapse"">" _
    & "        <div class=""panel-body""><strong>¿El Domicilio capturado es correcto? </strong> " & HIST_II_CORRECTO & "</div>" _
    & "        <div class=""panel-body""><strong>¿El Domicilio fue localizado? </strong> " & HIST_II_LOCALIZADO & "</div>" _
    & "        <div class=""panel-body""><strong>¿Vecino1-Conoce al Sr@? </strong> " & HIST_II_V1_CONOCE & "</div>" _
    & "        <div class=""panel-body""><strong>¿Vecino1-Sabe que actividad realiza el Sr.? </strong> " & HIST_II_V1_DEDICA & "</div>" _
    & "        <div class=""panel-body""><strong>¿Vecino1-Cuanto tiempo lleva el Sr. dedicandose a su actividad? </strong> " & HIST_II_V1_TIEMPO & "</div>" _
    & "        <div class=""panel-body""><strong>¿Vecino2-Conoce al Sr@ ? </strong> " & HIST_II_V2_CONOCE & "</div>" _
    & "        <div class=""panel-body""><strong>¿Vecino2-Sabe que actividad realiza el Sr.? </strong> " & HIST_II_V2_DEDICA & "</div>" _
    & "        <div class=""panel-body""><strong>¿Vecino2-Cuanto tiempo lleva el Sr. dedicandose a su actividad? </strong> " & HIST_II_V2_TIEMPO & "</div>" _
    & "        <div class=""panel-body""><strong>¿Atendieron en el Domicilio donde realiza su actividad economica? </strong> " & HIST_II_ATENDIERON & "</div>" _
    & "        <div class=""panel-body""><strong>¿Quien atendio? </strong> " & HIST_II_QUIENATIENDE & "</div>" _
    & "        <div class=""panel-body""><strong>¿Nombre de quien atendio? </strong> " & HIST_II_NOMBREQA & "</div>" _
    & "        <div class=""panel-body""><strong>¿Dias que labora? </strong> " & HIST_II_DIASLABORA & "</div>" _
    & "        <div class=""panel-body""><strong>¿Cuanto tiempo lleva en su actividad? </strong> " & HIST_II_TIEMPO & "</div>" _
    & "        <div class=""panel-body""><strong>¿Como recibe sus ingresos? </strong> " & HIST_II_METODOPAGO & "</div>" _
    & "        <div class=""panel-body""><strong>¿Ingresos Libres al Mes? </strong> " & HIST_II_INGRESOMES & "</div>" _
    & "        <div class=""panel-body""><strong>¿El cliente se dedica a lo que declara en solicitud? </strong> " & HIST_II_CONCUERDALABOR & "</div>" _
    & "        <div class=""panel-body""><strong>Observaciones Generales </strong> " & HIST_II_OBSERVACIONESG & "</div>" _
    & "        <div class=""panel-body""><strong>¿Requiere Visita? </strong> " & HIST_II_REQUIEREVISITA & "</div>" _
    & "        <div class=""panel-body""><strong>Resultado </strong> " & HIST_II_RESULTADO & "</div>" _
    & "        <div class=""panel-body""><strong>Motivo </strong> " & HIST_II_MOTIVO & "</div>" _
        & "      </div>" _
        & "    </div>" _
        & v_carrusel _
        & "  </div> " _
        & "</div>" _
        & "</body>"

                        End If


                    End If


                    Dim ubicacion As New GLatLng(Latitud, Longitud)
                    Dim infoWindowHTML As String = (v_html)

                    Dim icon As GIcon = New GIcon

                    'If i = 0 Then
                    '    icon.markerIconOptions = New MarkerIconOptions(30, 30, Color.Green)
                    'ElseIf i = DtsUsuario.Rows.Count - 1 Then
                    '    icon.markerIconOptions = New MarkerIconOptions(30, 30, Color.Red)
                    'Else
                    '    icon.markerIconOptions = New MarkerIconOptions(30, 30, Color.Yellow)
                    'End If

                    icon.markerIconOptions = New MarkerIconOptions(30, 30, Color.FromName(COLORVB))

                    Dim marker As GMarker = New GMarker(ubicacion, icon)
                    GMap1.Add(New GInfoWindow(New GMarker(ubicacion, icon), infoWindowHTML))

                    Session("CADENA1")(Contar) = Latitud
                    Session("CADENA2")(Contar) = Longitud

                    Contar = Contar + 1
                End If
            Catch ex As Exception
                Dim fff As String = ex.Message
            End Try
        Next

        Dim Cuantos As Integer = Contar - 1

        Dim puntos As New List(Of GLatLng)()

        For i As Integer = -1 To (Cuantos - 1)
            Dim latlng As New GLatLng(Session("CADENA1")(i + 1), Session("CADENA2")(i + 1))
            puntos.Add(latlng)
        Next

        Dim latlng2 As New GLatLng(Session("CADENA1")(Cuantos / 2), Session("CADENA2")(Cuantos / 2))
        GMap1.setCenter(latlng2, 12)

        If RblReporte.SelectedValue <> "Solicitudes" Then
            Dim linea As New GPolyline(puntos, [String].Format("#{0:X6}", random.[Next](&H1000000)), 2)
            GMap1.Add(linea)
        End If
        'Establecemos alto y ancho en px
        'GMap1.Height = 500
        'GMap1.Width = 900
        PanelMapa.Style.Add("visibility", "visible")
        'Imagen1.Visible = True
        'Imagen2.Visible = True
        'Imagen3.Visible = True
        'Imagen4.Visible = True
        'RadLabel1.Visible = True
        'RadLabel2.Visible = True
        'RadLabel3.Visible = True
        'RadLabel4.Visible = True
        'Adiciona el control de la parte izq superior (moverse, ampliar y reducir)
        GMap1.addControl(New GControl(GControl.preBuilt.LargeMapControl))
        'GControl.preBuilt.MapTypeControl: permite elegir un tipo de mapa y otro.
        GMap1.addControl(New GControl(GControl.preBuilt.StreetViewControl))
        GMap1.Add(GMapType.GTypes.Hybrid)
        GMap1.Add(GMapType.GTypes.Physical)

        'Permite hacer zoom con la rueda del mouse
        GMap1.enableHookMouseWheelToZoom = True
        'Tipo de mapa a mostrar
        GMap1.mapType = GMapType.GTypes.Normal
        'GMap1.setCenter(New GLatLng(19.4326, -99.133158), 13)
        'Catch ex As Exception
        'End Try

    End Sub

    Private Sub RblReporte_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RblReporte.SelectedIndexChanged
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
End Class
