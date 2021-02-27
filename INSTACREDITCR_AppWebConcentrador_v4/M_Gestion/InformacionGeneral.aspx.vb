Imports System.Data
Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports Db
Imports System.Globalization
Imports System.IO
Imports Funciones
Imports Telerik.Web.UI
Imports System.Net.Mail
Imports System.Net
Imports RestSharp

Partial Class M_Gestion_InformacionGeneral
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("Usuario") IsNot Nothing Then
                LblCat_Lo_Usuario.Text = tmpUSUARIO("cat_lo_usuario")
                Try
                    If Not tmpCredito Is Nothing Then
                        GvHistActResumido.Rebind()
                        Llenar()
                    End If
                Catch ex As Exception
                    EnviarCorreo("Gestion", "InformacionGeneral.ascx", "Page_Load", ex, "", "", LblCat_Lo_Usuario.Text)
                End Try
            End If
        End If

    End Sub

    Sub Llenar()
        If tmpCredito("PR_MC_CREDITO") Is Nothing Then

        ElseIf tmpCredito("PR_MC_CREDITO") <> "" Then
            Try

                If tmpCredito("PR_MC_CREDITOCONTACTADO").ToString = "0" Then
                    ImgNoContacto.ImageUrl = "Imagenes/ImgNoContacto.png"
                Else
                    ImgNoContacto.ImageUrl = "Imagenes/ImgContacto.png"
                End If

                TxtPr_Mc_Dteprimeragestion.Text = tmpCredito("PR_MC_DTEPRIMERAGESTION").ToString.Substring(0, 10)
                TxtPr_Mc_Primeragestion.Text = tmpCredito("PR_MC_PRIMERAGESTION")
                TxtPr_Mc_Usuario.Text = tmpCredito("PR_MC_USUARIO")
                TxtPr_Mc_Uasignado.Text = tmpCredito("PR_MC_UASIGNADO")
                TxtPr_Mc_Resultado.Text = tmpCredito("PR_MC_RESULTADO")
                TxtPr_Mc_Dtegestion.Text = tmpCredito("PR_MC_DTEGESTION")
                TxtPr_Mc_Resultadorelev.Text = tmpCredito("PR_MC_RESULTADORELEV")
                TxtPr_Mc_Dteresultadorelev.Text = tmpCredito("PR_MC_DTERESULTADORELEV")
                LblVi_Dias_Semaforo_Gestion.Text = tmpCredito("VI_DIAS_SEMAFORO_GESTION")

                TxtUsrExtrajudicial.Text = tmpCredito("PR_MC_UEXTRA")
                TxtProm_PG.Text = tmpCredito("PR_MC_NOPROMPARCIALES")
                ' TxtProm_NG.Text = tmpCredito("PR_MC_NOPROMNEGOCIACION")
                TxtProm_PC.Text = tmpCredito("PR_MC_NOPROMPARCUMP")
                'TxtProm_NC.Text = tmpCredito("PR_MC_NOPROMNEGCUMP")
                TxtProm_PI.Text = tmpCredito("PR_MC_NOPROMPARINC")
                'TxtProm_NI.Text = tmpCredito("PR_MC_NOPROMNEGINC")

                GvHistActResumido.Visible = True

                ImgSemaforo.ToolTip = tmpCredito("VI_DIAS_SEMAFORO_GESTION")
                If tmpCredito("VI_SEMAFORO_GESTION").ToString.ToUpper = "AMARILLO" Then
                    ImgSemaforo.ImageUrl = "Imagenes/ImgSemaforoAmarillo.png"
                ElseIf tmpCredito("VI_SEMAFORO_GESTION").ToString.ToUpper = "VERDE" Then
                    ImgSemaforo.ImageUrl = "Imagenes/ImgSemaforoVerde.png"
                Else
                    ImgSemaforo.ImageUrl = "Imagenes/ImgSemaforoRojo.png"
                End If

                'If CondonaGastos(tmpCredito("PR_MC_CREDITO"), "", "", "", 0).Rows(0).Item("Cuantos") <> 0 Then
                '    PnlGastos.Visible = False
                '    RLblMsj.Text = "Existe Una Solicitud Pendiente Por Autorizar"
                'Else
                '    PnlGastos.Visible = True
                'End If

            Catch ex As Exception
                SendMail("Llenar()", ex, tmpCredito("PR_MC_CREDITO"), "", LblCat_Lo_Usuario.Text)
            End Try
        End If
    End Sub
    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Gestion", "InformacionGeneral.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub

    Private Sub GvHistActResumido_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles GvHistActResumido.NeedDataSource
        Try
            GvHistActResumido.DataSource = Class_Hist_Act.LlenarElementosHistAct(tmpCredito("PR_MC_CREDITO"), "Hist_Ge_Dteactividad", "DESC", 4, tmpCredito("PR_MC_PRODUCTO"))
        Catch ex As Exception
            Dim s As String = ex.Message
        End Try
    End Sub

    Private Sub _Default_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        If Not IsPostBack And tmpCredito IsNot Nothing Then
            Dim tabla As DataTable = LlenarInfo(tmpCredito("PR_MC_CREDITO"), 1, tmpCredito("PR_MC_PRODUCTO"))
            Dim generator As New RadAjaxPanelGenerator(4, 3)
            Dim pnl As RadAjaxPanel = generator.generarPanel(tabla)
            pnlInfoBasica.Controls.Add(pnl)

            tabla = LlenarInfo(tmpCredito("PR_MC_CREDITO"), 2, tmpCredito("PR_MC_PRODUCTO"))
            generator = New RadAjaxPanelGenerator(4, 3)
            pnl = generator.generarPanel(tabla)
            pnlAvales.Controls.Add(pnl)

            tabla = LlenarInfo(tmpCredito("PR_MC_CREDITO"), 3, tmpCredito("PR_MC_PRODUCTO"))
            generator = New RadAjaxPanelGenerator(4, 3)
            pnl = generator.generarPanel(tabla)
            pnlInfoGeneral.Controls.Add(pnl)

            tabla = LlenarInfo(tmpCredito("PR_MC_CREDITO"), 4, tmpCredito("PR_MC_PRODUCTO"))
            rGvAvales.DataSource = tabla
            'rGvAvales.DataBind()

            'tabla = LlenarInfo(tmpCredito("PR_MC_CREDITO"), 4, tmpCredito("PR_MC_PRODUCTO")) 'Informacion judicial
            'Dim tablaGastos As DataTable = LlenarInfo(tmpCredito("PR_MC_CREDITO"), 14, tmpCredito("PR_MC_PRODUCTO")) 'Gastos de cobranza
            'tabla.Columns.Add(New DataColumn("Gastos de cobranza")) 'Añadimos la columna de gastos a la tabla de juicios

            ''Si las dos tablas traen informacion. Las juntamos
            'If tabla.Rows.Count > 0 And tablaGastos.Rows.Count > 0 Then
            '    tabla.Rows(0)("Gastos de cobranza") = tablaGastos(0)(0).ToString

            '    'Si judicial no trae filas pero la de gastos sí, creamos una fila vacia y añadimos los gastos 
            'ElseIf tabla.Rows.Count = 0 And tablaGastos.Rows.Count > 0 Then
            '    tabla.Rows.Add(tabla.NewRow)
            '    tabla.Rows(0)("Gastos de cobranza") = tablaGastos(0)(0).ToString
            'End If
            'generator = New RadAjaxPanelGenerator(4, 3)
            'pnl = generator.generarPanel(tabla)
            'pnlJuicio.Controls.Add(pnl)

            tabla = LlenarInfo(tmpCredito("PR_MC_CREDITO"), 5, tmpCredito("PR_MC_PRODUCTO"))
            generator = New RadAjaxPanelGenerator(4, 3)
            pnl = generator.generarPanel(tabla)
            pnlCamapanasEspeciales.Controls.Add(pnl)

            tabla = LlenarInfo(tmpCredito("PR_MC_CREDITO"), 12, tmpCredito("PR_MC_PRODUCTO"))
            generator = New RadAjaxPanelGenerator(4, 3)
            pnl = generator.generarPanel(tabla)
            pnlInfoFinan.Controls.Add(pnl)

            tabla = LlenarInfo(tmpCredito("PR_MC_CREDITO"), 13, tmpCredito("PR_MC_PRODUCTO"))
            generator = New RadAjaxPanelGenerator(4, 3)
            pnl = generator.generarPanel(tabla)
            pnlCondonaciones.Controls.Add(pnl)


        End If
    End Sub

    Public Property tmpUSUARIO As IDictionary
        Get
            Return CType(Session("USUARIO"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("USUARIO") = value
        End Set
    End Property

    Public Property tmpCredito As IDictionary
        Get
            Return CType(Session("Credito"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("Credito") = value
        End Set
    End Property

    Private Function LlenarInfo(ByVal V_Credito As String, ByVal V_Bandera As Integer, ByVal V_PRODUCTO As String) As DataTable
        Dim SSCommand As New SqlCommand("SP_INFORMACION_CREDITO")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Credito", SqlDbType.NVarChar).Value = V_Credito
        SSCommand.Parameters.Add("@V_PRODUCTO", SqlDbType.NVarChar).Value = V_PRODUCTO
        SSCommand.Parameters.Add("@v_bandera", SqlDbType.Decimal).Value = V_Bandera
        Dim DtsHist_Act As DataTable = Consulta_Procedure(SSCommand, SSCommand.CommandText)
        Return DtsHist_Act
    End Function
    Public Function CondonaGastos(ByVal V_Credito As String, ByVal V_Id_Gasto As String, ByVal V_Key As String, ByVal V_Comentario_S As String, ByVal V_Bandera As Integer) As DataTable
        Dim SSCommand As New SqlCommand("SP_INFORMACION_CREDITO")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Credito", SqlDbType.NVarChar).Value = V_Credito
        SSCommand.Parameters.Add("@v_bandera", SqlDbType.Decimal).Value = V_Bandera
        SSCommand.Parameters.Add("@V_Id_Gasto", SqlDbType.NVarChar).Value = V_Id_Gasto
        SSCommand.Parameters.Add("@V_Key", SqlDbType.NVarChar).Value = V_Key
        SSCommand.Parameters.Add("@V_Comentario_S", SqlDbType.NVarChar).Value = V_Comentario_S
        Dim DtsGastos As DataTable = Consulta_Procedure(SSCommand, "SP_CONDONACION_GASTOS")
        Return DtsGastos
    End Function

    'Private Sub RBSolicitar_Click(sender As Object, e As EventArgs) Handles RBSolicitar.Click
    '    Dim v_valor As Integer = 0
    '    For Each item_v As GridItem In RGGastos.MasterTableView.Items

    '        Dim dataitem As GridDataItem = DirectCast(item_v, GridDataItem)
    '        Dim cell As TableCell = dataitem("ClientSelectColumn")
    '        Dim checkBox As CheckBox = DirectCast(cell.Controls(0), CheckBox)
    '        If checkBox.Checked Then
    '            v_valor = 1
    '        End If
    '    Next

    '    If RtxtComentario.Text.Length < 10 Then
    '        Aviso("Capture Un Comentario Valido")
    '    ElseIf v_valor = 0 Then
    '        Aviso("Seleccione Por Lo Menos Un Gasto")
    '    Else
    '        Dim dt = New DataTable()
    '        Dim dcUsuario = New DataColumn("Usuario", GetType(String))
    '        Dim dcID = New DataColumn("ID", GetType(Int32))
    '        Dim dcDesc = New DataColumn("DESCRIPCION", GetType(String))
    '        Dim dcMto = New DataColumn("MONTO", GetType(String))
    '        Dim dcNombre = New DataColumn("NOMBRE", GetType(String))
    '        dt.Columns.Add(dcUsuario)
    '        dt.Columns.Add(dcID)
    '        dt.Columns.Add(dcDesc)
    '        dt.Columns.Add(dcMto)
    '        dt.Columns.Add(dcNombre)
    '        For Each item_v As GridItem In RGGastos.MasterTableView.Items
    '            Dim dataitem As GridDataItem = DirectCast(item_v, GridDataItem)
    '            Dim cell As TableCell = dataitem("ClientSelectColumn")
    '            Dim checkBox As CheckBox = DirectCast(cell.Controls(0), CheckBox)
    '            If checkBox.Checked Then
    '                dt.Rows.Add(tmpUSUARIO("CAT_LO_NOMBRE"), dataitem.Cells(3).Text, dataitem.Cells(4).Text, dataitem.Cells(5).Text, dataitem.Cells(6).Text)
    '            End If
    '        Next

    '        Dim V_Mensaje As String = EnviaAutoGasto(dt)
    '        Dim V_Subject As String = "Solicitud De Autorizacion De Gastos"
    '        Dim htmlView = AlternateView.CreateAlternateViewFromString(V_Mensaje, Nothing, "text/html")
    '        Dim msg As New System.Net.Mail.MailMessage()
    '        msg.[To].Add("mflores@ahorrosbienestar.com")
    '        msg.[To].Add("jose.rojo@mccollect.com.mx")
    '        msg.From = New MailAddress("soporte@mccollect.com.mx", V_Subject, System.Text.Encoding.UTF8)
    '        msg.Subject = V_Subject
    '        msg.SubjectEncoding = System.Text.Encoding.UTF8
    '        msg.AlternateViews.Add(htmlView)
    '        msg.BodyEncoding = System.Text.Encoding.UTF8
    '        msg.IsBodyHtml = True
    '        Dim client As New SmtpClient()
    '        client.Credentials = New System.Net.NetworkCredential("soporte@mccollect.com.mx", "Adalesperra2")
    '        client.Port = "587"
    '        client.Host = "smtp.gmail.com"
    '        client.EnableSsl = True
    '        Try
    '            client.Send(msg)
    '        Catch ex As System.Net.Mail.SmtpException
    '            'Ejecuta("insert into hist_errores (ERRORS) values('" & ex.ToString & "')")
    '        End Try
    '    End If
    'End Sub
    Private Function EnviaAutoGasto(ByVal V_Tabla As DataTable) As String

        Dim V_Registros As String = ""
        Dim key As String = CondonaGastos(tmpCredito("PR_MC_CREDITO"), "", "", "", 2).Rows(0).Item("Key")


        For Each rowx As DataRow In V_Tabla.Rows
            V_Registros = V_Registros & "<tr><td width = 59 valign=top style='width:52.9pt;border:solid windowtext 1.0pt;border-top:none;padding:0cm 5.4pt 0cm 5.4pt'><p class=MsoNormal><span lang=ES>" & rowx("ID") & "<o:p></o:p></span></p></td><td width=163 valign=top style='width:167.8pt;border-top:none;border-left:none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;padding:0cm 5.4pt 0cm 5.4pt'><p class=MsoNormal><span lang=ES>" & rowx("DESCRIPCION") & "<o:p></o:p></span></p></td><td width=110 valign=top style='width:110.35pt;border-top:none;border-left:none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;padding:0cm 5.4pt 0cm 5.4pt'><p class=MsoNormal><span lang=ES>" & rowx("MONTO") & "<o:p></o:p></span></p></td><td width=191 valign=top style='width:203.8pt;border-top:none;border-left:none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;padding:0cm 5.4pt 0cm 5.4pt'><p class=MsoNormal><span lang=ES>" & rowx("NOMBRE") & "<o:p></o:p></span></p></td></tr>"
            CondonaGastos(tmpCredito("PR_MC_CREDITO"), rowx("ID"), key, RtxtComentario.Text, 3)
        Next

        Dim v_html As String = "<html xmlns:v='urn:schemas-Microsoft - com: vml' xmlns:o='urn: schemas-Microsoft - com: office : office' xmlns:w='urn: schemas-Microsoft - com: office : word' xmlns:m='http: //schemas.microsoft.com/office/2004/12/omml' xmlns='http://www.w3.org/TR/REC-html40'><head><meta http-equiv=Content-Type content='text/html; charset=iso-8859-1'><meta name=Generator content='Microsoft Word 15 (filtered medium)'><style> </head><body lang=EN-US link='#0563C1' vlink='#954F72'><div class=WordSection1><p class=MsoNormal><b><span lang=ES style='font-size:14.0pt'>Condonación De Gastos.<o:p></o:p></span></b></p><p class=MsoNormal><span lang=ES><o:p>&nbsp;</o:p></span></p><p class=MsoNormal><span lang=ES>Solicita: <u>" & V_Tabla.Rows(0).Item("Usuario") & "</u><o:p></o:p></span></p><p class=MsoNormal style='mso-margin-top-alt:auto;mso-margin-bottom-alt:auto'><span lang=ES>Crédito:" & tmpCredito("PR_MC_CREDITO") & "<o:p></o:p></span></p><p class=MsoNormal><span lang=ES><o:p>&nbsp;</o:p></span></p><p class=MsoNormal><span lang=ES>Detalle<o:p></o:p></span></p><table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 style='margin-left:31.85pt;border-collapse:collapse'><tr><td width=59 valign=top style='width:52.9pt;border:solid windowtext 1.0pt;background:#8496B0;padding:0cm 5.4pt 0cm 5.4pt'><p class=MsoNormal align=center style='text-align:center'><span lang=ES style='color:white'>Id<o:p></o:p></span></p></td><td width=163 valign=top style='width:167.8pt;border:solid windowtext 1.0pt;border-left:none;background:#8496B0;padding:0cm 5.4pt 0cm 5.4pt'><p class=MsoNormal align=center style='text-align:center'><span lang=ES style='color:white'>Descripción<o:p></o:p></span></p></td><td width=110 valign=top style='width:110.35pt;border:solid windowtext 1.0pt;border-left:none;background:#8496B0;padding:0cm 5.4pt 0cm 5.4pt'><p class=MsoNormal align=center style='text-align:center'><span lang=ES style='color:white'>Monto<o:p></o:p></span></p></td><td width=191 valign=top style='width:203.8pt;border:solid windowtext 1.0pt;border-left:none;background:#8496B0;padding:0cm 5.4pt 0cm 5.4pt'><p class=MsoNormal align=center style='text-align:center'><span lang=ES style='color:white'>Registro<o:p></o:p></span></p></td></tr>" & V_Registros & "</table><p class=MsoNormal><span lang=ES style='color:#1F497D'><o:p>&nbsp;</o:p></span></p><p class=MsoNormal><span lang=ES>Motivo De Cancelación:<o:p></o:p></span></p><table class=MsoTableGrid border=1 cellspacing=0 cellpadding=0 style='border-collapse:collapse;border:none'><tr style='height:55.9pt'><td width=566 valign=top style='width:564.1pt;border:solid windowtext 1.0pt;padding:0cm 5.4pt 0cm 5.4pt;height:55.9pt'><p class=MsoNormal><span lang=ES><o:p>" & RtxtComentario.Text & "</o:p></span></p></td></tr></table><p class=MsoNormal><span lang=ES><o:p>&nbsp;</o:p></span></p></div><p class='MsoNormal'><b><span lang='ES' style='color:#1f497d'> </span></b></p><p class='MsoNormal'><span lang='ES' style='color:#1f497d'> </span></p><p class='MsoNormal'><span lang='ES' style='color:#1f497d'> </span></p><table class='MsoNormalTable' border='0' cellspacing='0' cellpadding='0' style='border-collapse:collapse'><tr><td width='67' valign='top' style='width: 49.35pt;border:solid windowtext 1.0pt;background: #00b0f0;padding:0cm 5.4pt 0cm 5.4pt'><p class='MsoNormal' align='center' style='text-align:center'><b><span style='font-size: 12.0pt;color:white'><a href='https://pruebasmc.com.mx/AhorrosBienestarDesarrollo/DefaultGastos.aspx?Key=" & key & "&Id_solicitud=1'><span style='color:white'>Aceptar</span></a></span></b></p></td></tr></table><p class='MsoNormal'> </p><table class='MsoNormalTable' border='0' cellspacing='0' cellpadding='0' style='border-collapse: collapse'><tr><td width='75' valign='top' style='width: 49.35pt;border:solid windowtext 1.0pt;background: red;padding:0cm 5.4pt 0cm 5.4pt'><p class='MsoNormal' align='center' style='text-align:center'><b><span style='font-size: 12.0pt;color:white'><a href='https://pruebasmc.com.mx/AhorrosBienestarDesarrollo/DefaultGastos.aspx?Key=" & key & "&Id_solicitud=2'><span style='color:white'>Rechazar</span></a></span></b></p></td></tr></table><p class='MsoNormal'> </p></div></body></html>"
        Return v_html
    End Function
    Protected Sub Aviso(ByVal MSJ As String)
        RadAviso.RadAlert(MSJ, 440, 155, "Aviso", Nothing)
    End Sub
    Sub RGGastos_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RGGastos.NeedDataSource
        Try
            Dim DtsGastos As DataTable = CondonaGastos(tmpCredito("PR_MC_CREDITO"), "", "", "", 1)
            RGGastos.DataSource = DtsGastos
        Catch ex As Exception
            Dim Errors As String = ex.ToString
        End Try
    End Sub


End Class
