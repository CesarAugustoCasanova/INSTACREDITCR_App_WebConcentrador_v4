Imports System.Data.SqlClient
Imports System.Data
Imports AjaxControlToolkit
Imports System.Web.UI.WebControls.Label
Imports Funciones
Imports System.Drawing
Imports Subgurim.Controles
Imports System.IO
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D
Imports Db
Imports Telerik.Web.UI
Imports System.Net
Imports Spire.Xls

Partial Class M_Monitoreo_Re_Visitas_v3
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
        'Try
        '    If tmpPermisos("INGRESO") = False Then
        '        Session.Clear()
        '        Session.Abandon()
        '        Response.Redirect("~/SesionExpirada.aspx")
        '    End If
        'Catch ex As Exception
        '    Session.Clear()
        '    Session.Abandon()
        '    Response.Redirect("~/SesionExpirada.aspx")
        'End Try
        If Not IsPostBack Then
            'RadAjaxPanelFiltros.Visible = True
            Llena_Usuario()
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
            oraCommand.CommandText = "SP_RP_VISITAS_V3"
            oraCommand.CommandType = CommandType.StoredProcedure
            oraCommand.Parameters.Add("v_bandera", SqlDbType.Decimal).Value = bandera
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
        Dim valores As List(Of String) = New List(Of String)
        For Each item As RadComboBoxItem In v_item.CheckedItems
            valores.Add(item.Value)
        Next
        Return "'" & String.Join("','", valores.ToArray) & "'"
    End Function


    Protected Sub Llena_Usuario()
        Dim v_cond = " "
        Dim v_bandera As Integer = 0

        LLENAR_DROP(v_bandera, v_cond, RcbUsuario, "V_VALOR", "T_VALOR", "Sin Usuario")
    End Sub


    Protected Sub Llena_Resultado()
        Dim v_cond = " where HIST_VI_VISITADOR in (" & Rad_Tcadena(RcbUsuario) & ") "
        Dim v_bandera As Integer = 2

        LLENAR_DROP(v_bandera, v_cond, RcbResultado, "V_VALOR", "T_VALOR", "Sin Resultado")
    End Sub


    Protected Sub RcbUsuario_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles RcbUsuario.SelectedIndexChanged
        Limpia_grid()
        RcbResultado.Enabled = True
        TxtFechaI.Enabled = True
        TxtFechaF.Enabled = True
        BtnGenerar.Visible = True
        rcbGenerar.Enabled = True
        Llena_Resultado()
    End Sub


    Protected Sub Limpia_grid()
        Session("InvDom") = Nothing
        GVSolInvCampo.DataSource = Nothing
        GVSolInvCampo.DataBind()
        GVSolInvCampo.Visible = False
        BtnExportar.Visible = False
    End Sub

    Protected Sub Limpia_filtros()
        RcbUsuario.Visible = False
        RcbResultado.Enabled = False
        TxtFechaI.Enabled = False
        TxtFechaF.Enabled = False
        BtnGenerar.Visible = False
        rcbGenerar.Enabled = False
        BtnExportar.Visible = False
    End Sub

    Protected Sub BtnGenerar_Click(sender As Object, e As EventArgs) Handles BtnGenerar.Click

        If Rad_Tcadena(RcbResultado) = "" Then
            Mensaje("Seleccione Por Lo Menos Un Resultado")
        ElseIf TxtFechaI.DateInput.DisplayText = "" Then
            Mensaje("Seleccione Fecha De Inicio")
        ElseIf TxtFechaF.DateInput.DisplayText = "" Then
            Mensaje("Seleccione Fecha De Termino")
        ElseIf rcbGenerar.SelectedValue = "" Then
            Mensaje("Seleccione reporte a generar (Información / Mapa)")
        Else
            Dim v_where As String = ""
            v_where = " where convert(date,HIST_VI_FECHA_VISITA) between convert(date,'" & TxtFechaI.DateInput.Text.Substring(0, 10) & "',120) and convert(date,'" & TxtFechaF.DateInput.Text.Substring(0, 10) & "',120) " & IIf(RcbUsuario.Items.Count <> RcbUsuario.GetCheckedIndices.Count, " And HIST_VI_VISITADOR In (" & Rad_Tcadena(RcbUsuario) & ") ", " ") & IIf(RcbResultado.Items.Count <> RcbResultado.GetCheckedIndices.Count, " And HIST_VI_RESULTADO In (" & Rad_Tcadena(RcbResultado) & ") ", " ")

            Limpia_grid()
            PanelMapa.Style.Add("display", "none")
            BtnExportar.Visible = False
            For Each val As String In rcbGenerar.SelectedValues
                Select Case val
                    Case "1"
                        GeneraInfo(v_where)
                    Case "2"
                        GeneraMapa(v_where)
                End Select
            Next
        End If
    End Sub


    Protected Sub GeneraInfo(ByVal v_where As String)
        Dim v_url As String = GetUrl("WSImagenInv.ashx")

        Dim DtsInvCampo As DataTable = Class_Visitas.LlenarGridVisitasDT(4, " ", v_where, v_url, "", "")

        If DtsInvCampo.Rows.Count = 0 Then
            Limpia_grid()
            Mensaje("No se encontraron resultados")
            'PanelGrid.Visible = False
            BtnExportar.Visible = False
        ElseIf DtsInvCampo.Rows.Count >= 10000 Then
            'PanelGrid.Visible = False
            BtnExportar.Visible = False
            Limpia_grid()

            Dim RUTA As String = StrRuta() & "\Salida\"
            If Not Directory.Exists(RUTA) Then
                Directory.CreateDirectory(RUTA)
            End If
            Dim ARCHIVO As String = RUTA & "Re_Visitas" & ".txt"
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
        End If
    End Sub

    Protected Sub GeneraMapa(ByVal v_where As String)
        GMap1.reset()
        GMap1.resetMarkerManager()

        Dim DtsVisitadores As DataTable = Class_Visitas.LlenarGridVisitas(8, "", v_where, "", "", "")
        Dim bounds As New GLatLngBounds
        For i As Integer = 0 To DtsVisitadores.Rows.Count - 1
            Dim v_bandera As Integer = 12
            Dim v_dato1 As String = ""
            For Each item As ButtonListItem In rcbGenerar.Items
                If item.Selected And item.Value = 3 Then
                    v_bandera = 14
                    v_dato1 = " where HIST_MU_USUARIO= '" & DtsVisitadores.Rows(i)("V_VISITADOR") & "' and trunc(HIST_MU_DTEMONITOREO)= '" & TxtFechaF.DateInput.DisplayText & "' "
                End If
            Next

            Dim DtsUsuario As DataTable = Class_Visitas.LlenarGridVisitas(v_bandera, "", v_where & " and HIST_VI_VISITADOR='" & DtsVisitadores.Rows(i)("V_VISITADOR") & "'", v_dato1, "", "")
            Dim auxLocs = PintarPuntos(DtsUsuario)
            For Each loc As GLatLng In auxLocs
                bounds.extend(loc)
            Next
        Next

        GMap1.setCenter(bounds.getCenter(), GMap1.getBoundsZoomLevel(bounds))
    End Sub

    Protected Function PintarPuntos(ByVal DtsUsuario As DataTable) As List(Of GLatLng)
        Dim Coordenadas As List(Of Coordenada) = New List(Of Coordenada)
        Dim UFIJA As New GLatLng
        Dim puntos As New List(Of GLatLng)
        Dim Contar As Integer = 0
        ' Try
        For Each row As DataRow In DtsUsuario.Rows
            Try
                Dim coord As New Coordenada(row("Latitud"), row("Longitud"))
                If coord.Lat <> 0.0 And coord.Lng <> 0 Then
                    Coordenadas.Add(coord)
                    Dim Credito As String = row("Credito").ToString
                    Dim Producto As String = row("Producto").ToString
                    Dim Nombre As String = row("Nombre").ToString
                    'Dim Instancia As String = row("Instancia").ToString
                    'Dim Despacho As String = row("Despacho").ToString

                    Dim Visitador As String = row("Visitador").ToString
                    Dim NombreVisitador As String = row("NombreVisitador").ToString
                    Dim FechaVisita As String = row("FechaVisita").ToString
                    Dim Accion As String = row("Accion").ToString
                    Dim Resultado As String = row("Resultado").ToString
                    Dim MontoPP As String = row("MontoPP").ToString
                    Dim FechaPP As String = row("FechaPP").ToString
                    Dim Comentario As String = row("Comentario").ToString
                    Dim Latitud As String = row("Latitud").ToString
                    Dim Longitud As String = row("Longitud").ToString
                    ' Dim MontoSolidario As String = row("Monto Solidiario").ToString
                    'Dim IDSolidario As String = row("ID Solidario").ToString

                    Dim Id As String = row("Id").ToString
                    Dim Tipo As String = "VISITA"
                    Dim v_html As String = ""
                    Dim COLORVB As String = "Yellow"

                    Try
                        Tipo = row("TIPO").ToString
                    Catch ex As Exception

                    End Try
                    If Tipo = "VISITA" Then
                        Dim c As String = Chr(39)
                        'Dim infoWindowHTML As String = String.Format("")
                        Dim v_part1 As String = ""
                        Dim v_part2 As String = ""
                        Dim v_base64 As Byte()
                        Dim v_nombre As String = "Imagen1"
                        Dim v_no As String = ""
                        'img carrusel
                        Try
                            Dim DtsImagenes As DataTable = Class_Visitas.LlenarGridVisitas(13, "", "", Credito, Trim(Id), "")
                            If DtsImagenes.Rows.Count > 0 Then
                                For x As Integer = 0 To (DtsImagenes.Rows.Count - 1)
                                    v_base64 = DtsImagenes.Rows(x)("IMAGEN_B64")
                                    v_nombre = DtsImagenes.Rows(x)("NOMBRE").ToString
                                    'v_no = DtsImagenes.Rows(x)("NUM").ToString

                                    If x = 0 Then
                                        v_part1 = v_part1 & " <li data-target=""#myCarousel"" data-slide-to=""0"" class=""active""></li>"
                                        v_part2 = v_part2 & "      <div class=""item active"" >" _
    & "        <img src=""data:Image/ png;base64, " & Convert.ToBase64String(v_base64) & """ />" _
    & "      </div>"
                                    Else
                                        v_part1 = v_part1 & " <li data-target=""#myCarousel"" data-slide-to=""" & x & """></li>"
                                        v_part2 = v_part2 & "      <div  class=""item"" >" _
    & "        <img src=""data:Image/ png;base64, " & Convert.ToBase64String(v_base64) & """ />" _
    & "      </div>"
                                    End If
                                Next
                            End If
                        Catch ex As Exception
                        End Try
                        Dim infoWindow As New InfoWindowGenerator("Resumen de la visita", "(" & Credito & " - " & Resultado & ")")

                        Dim listGroup As New ListGruopGenerator("Información del crédito")
                        listGroup.AddItem("Credito", Credito)
                        listGroup.AddItem("Producto", Producto)
                        listGroup.AddItem("Nombre", Nombre)
                        infoWindow.ListGroups.Add(listGroup)

                        listGroup = New ListGruopGenerator("Visita")
                        listGroup.AddItem("Visitador", Visitador)
                        listGroup.AddItem("Nombre Visitador", NombreVisitador)
                        listGroup.AddItem("Fecha de Visita", FechaVisita)
                        listGroup.AddItem("Resultado", Resultado)
                        If Resultado = "PROMESA DE PAGO" Then
                            listGroup.AddItem("Monto de Promesa", MontoPP)
                            listGroup.AddItem("Fecha de Promesa", FechaPP)
                            COLORVB = "Green"
                        End If
                        listGroup.AddItem("Accion", Accion)
                        listGroup.AddItem("Comentario", Comentario)
                        'listGroup.AddItem("ID Solidiario", IDSolidario)
                        ' listGroup.AddItem("Monto Solidiario", MontoSolidario)
                        listGroup.AddItem("Latitud", Latitud)
                        listGroup.AddItem("Longitud", Longitud)
                        infoWindow.ListGroups.Add(listGroup)

                        listGroup = New ListGruopGenerator("Imagenes")
                        listGroup.AddCarrousel(v_part1, v_part2)
                        infoWindow.ListGroups.Add(listGroup)

                        v_html = infoWindow.getInfowindow()
                    Else
                        COLORVB = "Red"
                        Dim infoWindow As New InfoWindowGenerator("Punto de Monitoreo", "(" & Visitador & ")")

                        Dim listGroup As New ListGruopGenerator("Información")
                        listGroup.AddItem("Coordenadas", Latitud & "," & Longitud)
                        listGroup.AddItem("Internet", Resultado)
                        listGroup.AddItem("Bateria", Comentario)
                        listGroup.AddItem("Fecha", FechaVisita)
                        infoWindow.ListGroups.Add(listGroup)

                        v_html = infoWindow.getInfowindow()
                    End If


                    Dim infoWindowHTML As String = (v_html)

                    'If i = 0 Then
                    '    icon.markerIconOptions = New MarkerIconOptions(30, 30, Color.Green)
                    'ElseIf i = DtsUsuario.Rows.Count - 1 Then
                    '    icon.markerIconOptions = New MarkerIconOptions(30, 30, Color.Red)
                    'Else
                    '    icon.markerIconOptions = New MarkerIconOptions(30, 30, Color.Yellow)
                    'End If
                    Dim icon As GIcon = New GIcon With {
                        .markerIconOptions = New MarkerIconOptions(30, 30, Color.FromName(COLORVB))
                    }
                    Dim ubicacion As New GLatLng(coord.Lat, coord.Lng)
                    GMap1.Add(New GInfoWindow(New GMarker(ubicacion, icon), infoWindowHTML))
                    puntos.Add(ubicacion)
                End If
            Catch ex As Exception

                Dim s As String = ex.Message

            End Try
        Next
        Dim linea As New GPolyline(puntos, [String].Format("#{0:X6}", random.[Next](&H1000000)), 2)
        GMap1.Add(linea)

        'Establecemos alto y ancho en px
        'GMap1.Height = 500
        'GMap1.Width = 900

        PanelMapa.Style.Add("display", "initial")
        'Imagen1.Visible = True
        'Imagen2.Visible = True
        'Imagen3.Visible = True
        'Imagen4.Visible = True
        'RadLabel1.Visible = True
        'RadLabel2.Visible = True
        'RadLabel3.Visible = True
        'RadLabel4.Visible = True

        'Adiciona el control de la parte izq superior (moverse, ampliar y reducir)
        GMap1.Add(New GControl(GControl.preBuilt.LargeMapControl))
        'GControl.preBuilt.MapTypeControl: permite elegir un tipo de mapa y otro.
        GMap1.Add(New GControl(GControl.preBuilt.StreetViewControl))
        GMap1.Add(GMapType.GTypes.Hybrid)
        GMap1.Add(GMapType.GTypes.Physical)

        'Permite hacer zoom con la rueda del mouse
        GMap1.enableHookMouseWheelToZoom = True
        'Tipo de mapa a mostrar
        GMap1.mapType = GMapType.GTypes.Normal
        'GMap1.setCenter(New GLatLng(19.4326, -99.133158), 13)
        'Catch ex As Exception
        'End Try
        Return puntos
    End Function


    Protected Sub RblReporte_SelectedIndexChanged(sender As Object, e As EventArgs)
        'RadAjaxPanelFiltros.Visible = True
        Limpia_grid()
        Limpia_filtros()
        Llena_Usuario()
    End Sub


    Protected Sub BtnExportar_Click(sender As Object, e As EventArgs) Handles BtnExportar.Click
        Try
            Dim dataTable As DataTable = Session("InvDom")

            If dataTable.Rows.Count = 0 Then
                Mensaje("No hay resultados para la busqueda")
            Else
                Dim arch_Excel As String = StrRuta() & "Salida\" & "Re_Visitas" & "_" & DateTime.Now.ToString("ddMMyyyy_hhmm") & ".xlsx"
                Dim book As New Workbook()
                Dim sheet As Worksheet = book.Worksheets(0)

                sheet.InsertDataTable(dataTable, True, 1, 1)
                sheet.Name = "Visitas"
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

    Private Sub btnimagen_Click(sender As Object, e As EventArgs) Handles btnimagen.Click
        Dim oraCommand As New SqlCommand
        oraCommand.CommandText = "SP_RP_VISITAS_V3"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("v_bandera", SqlDbType.Decimal).Value = 16
        oraCommand.Parameters.Add("v_agencia", SqlDbType.NVarChar).Value = ""
        oraCommand.Parameters.Add("v_condiciones", SqlDbType.NVarChar).Value = ""
        Dim objDSa As DataTable = Consulta_Procedure(oraCommand, "Filtros")
        Dim s As Byte() = objDSa.Rows(1).Item("hist_me_media")
        If File.Exists("C:\Workspace\ccasanova\imagen.jpg") Then
            Kill("C:\Workspace\ccasanova\imagen.jpg")
        End If
        File.WriteAllBytes("C:\Workspace\ccasanova\imagen.jpg", s)
    End Sub
End Class

Class Coordenada
    Private _lat As Double
    Private _lng As Double

    Public Sub New(lat As String, lng As String)
        Dim auxLat As Double
        Dim auxLng As Double
        Try
            auxLat = Double.Parse(lat)
            auxLng = Double.Parse(lng)

            If auxLat > 90 Or auxLat < -90 Then
                auxLat = 0.0
            End If

            If auxLng > 180 Or auxLng < -180 Then
                auxLng = 0.0
            End If
        Catch ex As Exception
            auxLat = 0.0
            auxLng = 0.0
        Finally
            Me.Lat = auxLat
            Me.Lng = auxLng
        End Try
    End Sub

    Public Property Lat As String
        Get
            Return _lat
        End Get
        Set(value As String)
            _lat = value
        End Set
    End Property

    Public Property Lng As String
        Get
            Return _lng
        End Get
        Set(value As String)
            _lng = value
        End Set
    End Property
End Class


Class ListGruopGenerator
    Private ListTitle As String = "<div class='list-group-item active'>{0}</div>"
    Private ListItem As String = "<div class='list-group-item'><b>{0}:</b> {1}</div>"
    Private ListGruop As List(Of String)

    Public Sub New(title As String)
        ListGruop = New List(Of String) From {
            String.Format(ListTitle, title)
        }
    End Sub

    Public Sub AddItem(name As String, description As String)
        ListGruop.Add(String.Format(ListItem, name, description))
    End Sub

    Public Sub AddCarrousel(v_part1 As String, v_part2 As String)
        Dim Carrousel As String = "<div class=""list-group-item"" style=""width:100%""> " _
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
        & "</div>"
        ListGruop.Add(Carrousel)
    End Sub

    Public Function getGruop() As String
        Dim infowindow = "<div class='list-group my-2'>"
        For Each item As String In ListGruop
            infowindow &= item
        Next
        infowindow &= "</div>"
        Return infowindow
    End Function
End Class

Class InfoWindowGenerator
    Private ReadOnly Head As String = "<head>" &
    "<link rel=""stylesheet"" href=""https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css"">" &
    "<script src=""https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js""></script>" &
    "<script src=""https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js""></script>" &
    "</head><body>"
    Private _ListGroups As List(Of ListGruopGenerator)
    Private Encabezado As String = ""
    Public Sub New(title As String, subtitle As String)
        Encabezado = "<div class=""container"" style=""width:500px"">" &
    "<h4>" & title & "</h4>" &
    "<h6><strong>" & subtitle & "</strong></h6> </div>"
        _ListGroups = New List(Of ListGruopGenerator)
    End Sub

    Friend Property ListGroups As List(Of ListGruopGenerator)
        Get
            Return _ListGroups
        End Get
        Set(value As List(Of ListGruopGenerator))
            _ListGroups = value
        End Set
    End Property

    Public Function getInfowindow() As String
        Dim infoWindow As String = Head & Encabezado
        For Each group As ListGruopGenerator In _ListGroups
            infoWindow &= group.getGruop()
        Next
        infoWindow &= "</body>"
        Return infoWindow
    End Function
End Class