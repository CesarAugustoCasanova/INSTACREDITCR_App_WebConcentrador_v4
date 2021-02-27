Imports System.Data
Imports System.Data.SqlClient
Imports Conexiones
Imports Funciones
Imports Db
Imports System.Net.Mail
Imports System.Net
Imports System.Net.Security
Imports Telerik.Web.UI

Partial Class EnvioCorreos
    Inherits System.Web.UI.Page

    Shared Plantillaoriginal As String
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                GVCorreosCredito.Rebind()
                LlenarPlantillas()
            Else
                If Not tmpCredito Is Nothing Then
                    Dim dts As DataTable = Class_InformacionAdicional.LlenarElementosAgregar(tmpCredito("PR_MC_CREDITO"), 4)
                    If dts.Rows.Count > 0 Then
                        GVCorreosCredito.DataSource = dts
                        GVCorreosCredito.DataBind()
                    End If
                End If
            End If
        Catch ex As Exception
            EnviarCorreo("Gestion", "EnvioCorreos.ascx", "Page_Load", ex, "", "", tmpUSUARIO("CAT_LO_USUARIO"))
        End Try
    End Sub

    Sub LlenarPlantillas()

        Dim SSCommand As New SqlCommand("SP_ADD_CAT_PLANTILLAS_CORREO")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Cat_Pc_Id", SqlDbType.Decimal).Value = 0
        SSCommand.Parameters.Add("@V_Cat_Pc_Nombre", SqlDbType.NVarChar).Value = ""
       ' SSCommand.Parameters.Add("@V_CAT_PC_CONFIGURACION", SqlDbType.Variant).Value = DBNull.Value
        SSCommand.Parameters.Add("@V_CAT_PC_PRODUCTO", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 4

        Dim DtsCorreo As DataTable = Consulta_Procedure(SSCommand, "Correo")
        If DtsCorreo.Rows.Count > 0 Then
            DdlPlantilla.DataTextField = "Nombre"
            DdlPlantilla.DataValueField = "Nombre"
            DdlPlantilla.DataSource = DtsCorreo
            DdlPlantilla.DataBind()
            DdlPlantilla.Items.Add("Seleccione")
            DdlPlantilla.SelectedText = "Seleccione"
        End If
        Dim SSCommandMail As New SqlCommand("SP_EMAILS")
        SSCommandMail.CommandType = CommandType.StoredProcedure
        SSCommandMail.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = 0
        Dim DTSMail As DataTable = Consulta_Procedure(SSCommandMail, "SP_EMAILS")
        If DTSMail.Rows.Count > 0 Then
            DDLPerfilMail.Items.Clear()
            DDLPerfilMail.DataTextField = "PERFIL"
            DDLPerfilMail.DataValueField = "PERFIL"
            DDLPerfilMail.DataSource = DTSMail
            DDLPerfilMail.DataBind()
            DDLPerfilMail.Items.Add("Seleccione")
            DDLPerfilMail.SelectedText = "Seleccione"
        End If

    End Sub

    Public Sub DdlPlantilla_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlPlantilla.SelectedIndexChanged
        Try
            TxtPlantilla.EditModes = EditModes.All
            If DdlPlantilla.SelectedText = "Seleccione" Then
                TxtPlantilla.Content = ""
                TxtPlantilla.EditModes = EditModes.Preview
            Else
                Dim SSCommand As New SqlCommand("SP_ADD_CAT_PLANTILLAS_CORREO")
                SSCommand.CommandType = CommandType.StoredProcedure
                SSCommand.Parameters.Add("@V_Cat_Pc_Id", SqlDbType.Decimal).Value = 0
                SSCommand.Parameters.Add("@V_Cat_Pc_Nombre", SqlDbType.NVarChar).Value = DdlPlantilla.SelectedValue
                ' SSCommand.Parameters.Add("@V_CAT_PC_CONFIGURACION", SqlDbType.Variant).Value = DBNull.Value
                SSCommand.Parameters.Add("@V_CAT_PC_PRODUCTO", SqlDbType.NVarChar).Value = ""
                SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 6

                Dim dtsdesc As DataTable = Consulta_Procedure(SSCommand, "mail")
                Dim StrRemplazar As String = dtsdesc.Rows(0)("CAT_PC_CONFIGURACION").ToString.Replace("&lt;", "<").Replace("&gt;", ">")


                Dim SSCommandEti As New SqlCommand("SP_CATALOGOS")
                SSCommandEti.CommandType = CommandType.StoredProcedure
                SSCommandEti.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 27
                Dim dtsReplace As DataTable = Consulta_Procedure(SSCommandEti, "remplazar")
                Dim sb As New StringBuilder("")

                For c As Integer = 0 To dtsReplace.Rows.Count - 1

                    Dim SSCommandVALOR As New SqlCommand("SP_ADD_CAT_PLANTILLAS_CORREO")
                    SSCommandVALOR.CommandType = CommandType.StoredProcedure
                    SSCommandVALOR.Parameters.Add("@V_Cat_Pc_Id", SqlDbType.Decimal).Value = 0
                    SSCommandVALOR.Parameters.Add("@V_Cat_Pc_Nombre", SqlDbType.NVarChar).Value = dtsReplace.Rows(c)("CAT_EC_DESCRIPCION")
                    'SSCommandVALOR.Parameters.Add("@V_CAT_PC_CONFIGURACION", SqlDbType.Variant).Value = DBNull.Value
                    SSCommandVALOR.Parameters.Add("@V_CAT_PC_PRODUCTO", SqlDbType.NVarChar).Value = tmpCredito("PR_MC_CREDITO")
                    SSCommandVALOR.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 7
                    Dim DtsBuscaValor As DataTable = Consulta_Procedure(SSCommandVALOR, "BuscaValor")
                    If CStr(DtsBuscaValor.Rows(0).Item(0).ToString) <> "" Then
                        StrRemplazar = StrRemplazar.Replace(dtsReplace.Rows(c)("CAT_EC_DESCRIPCION"), DtsBuscaValor.Rows(0)(0))
                    Else
                        sb.Append(dtsReplace.Rows(c)("CAT_EC_DESCRIPCION") & ",")
                    End If
                Next

                If sb.ToString <> "" Then
                    sb.Replace("<<", "&#8810;")
                    sb.Replace(">>", "&#8811;")
                    showModal(Notificacion, "warning", "Aviso", "No se pudieron encontrar algunas etiquetas de la plantilla: " & sb.ToString)
                End If
                Plantillaoriginal = StrRemplazar
                TxtPlantilla.Content = StrRemplazar
                TxtPlantilla.EditModes = EditModes.Preview
            End If
        Catch exa As Exception
            showModal(Notificacion, "deny", "Error", exa.Message)
        End Try
    End Sub

    Sub validar(ByRef Objeto As Object, ByVal Estado As Boolean)
        If Objeto.visible = True And Estado = True Then
            Objeto.checked = True
        ElseIf Objeto.visible = False Then
            Objeto.checked = False
        Else
            Objeto.checked = False
        End If
    End Sub

    Protected Sub GVCorreosCredito_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs)
        Try
            GVCorreosCredito.DataSource = Class_InformacionAdicional.LlenarElementosAgregar(tmpCredito("PR_MC_CREDITO"), 4)
        Catch
            GVCorreosCredito.DataSource = Nothing
        End Try
    End Sub

    Public Sub EnviarRes(sender As Object, e As ButtonClickEventArgs)
        Try
            If DdlPlantilla.SelectedText = "Seleccione" Then
                showModal(Notificacion, "warning", "Aviso", "Seleccione una Plantilla")
            ElseIf DDLPerfilMail.SelectedText = "Seleccione" Then
                showModal(Notificacion, "warning", "Aviso", "Seleccione una Perfil de envío")
            ElseIf TxtPlantilla.Content = "" Then
                showModal(Notificacion, "warning", "Aviso", "Plantilla sin Contenido, Seleccione una Plantilla Válida")
            Else
                Dim fila As GridDataItem = TryCast(TryCast(sender, Telerik.Web.UI.RadButton).NamingContainer, GridDataItem)
                Dim correoenviar As String = fila.Cells(3).Text

                'Dim SSCommand1 As New SqlCommand("SP_CONFIGURACION_MAIL")
                'SSCommand1.CommandType = CommandType.StoredProcedure
                'SSCommand1.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 3
                'Dim DtsMail As DataTable = Consulta_Procedure(SSCommand1, "Mail")
                'Dim USUARIO As String = DtsMail.Rows(0).Item("CAT_CONF_USER")
                'If USUARIO = "Sin Correo Predeterminado, Contacta A Tu Administrador De Sistema" Then
                '    showModal(Notificacion, "warning", "Error", "Sin Correo Predeterminado, Contacta A Tu Administrador De Sistema")
                'Else
                'Dim PASSWORD As String = DtsMail.Rows(0).Item("CAT_CONF_PWD")
                '    Dim SSL As Integer = DtsMail.Rows(0).Item("CAT_CONF_SSL")

                Dim msg As New System.Net.Mail.MailMessage()
                    'msg.[To].Add(correoenviar)
                    'msg.[To].Add("alejandro.miramontes@mccollect.com.mx")
                    'msg.From = New MailAddress(USUARIO, DtsMail.Rows(0).Item("CAT_CONF_RESPONSABLE"), System.Text.Encoding.UTF8)
                    'msg.Subject = DdlPlantilla.SelectedText
                    'msg.SubjectEncoding = System.Text.Encoding.UTF8


                    Dim SSCommandVALOR As New SqlCommand("SP_ADD_CAT_PLANTILLAS_CORREO")
                    SSCommandVALOR.CommandType = CommandType.StoredProcedure
                    SSCommandVALOR.Parameters.Add("@V_Cat_Pc_Id", SqlDbType.Decimal).Value = 0
                    SSCommandVALOR.Parameters.Add("@V_Cat_Pc_Nombre", SqlDbType.NVarChar).Value = "PR_Nombre"
                    '  SSCommandVALOR.Parameters.Add("@V_CAT_PC_CONFIGURACION", SqlDbType.Variant).Value = DBNull.Value
                    SSCommandVALOR.Parameters.Add("@V_CAT_PC_PRODUCTO", SqlDbType.NVarChar).Value = tmpCredito("PR_MC_CREDITO")
                    SSCommandVALOR.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 7

                    Dim DtsBuscaValo As DataTable = Consulta_Procedure(SSCommandVALOR, "BuscaValor")
                Dim StrEncabezado As String = ""

                'Dim StrEncabezado As String = "<table align=""right"">" &
                '                        "<tr align="" right"">" &
                '                            "<td style="" text-align Right();font-weight: bold"">" &
                '                                "MC Collect" &
                '                            "</td>" &
                '                        "</tr>" &
                '                    "</table>" &
                '                    "<table>" &
                '                        "<tr align=""left"">" &
                '                            "<td>" &
                '                                "<img src=cid:LogoAlivio>" &
                '                            "</td>" &
                '                        "</tr>" &
                '                        "<tr>" &
                '                            "<td>" &
                '                                "SR.(A) : " & DtsBuscaValo.Rows(0)("PR_Nombre") &
                '                            "</td>" &
                '                        "</tr>" &
                '                "</table>" &
                '                "<table align=""right"">" &
                '                    "<tr>" &
                '                        "<td style="" font-weight:bold;text-align: Right"" >" &
                '                            "Numero de Cuenta:" & tmpCredito("PR_MC_CREDITO") &
                '                        "</td>" &
                '                    "</tr>" &
                '                    "<tr><td>&nbsp;</td></tr>" &
                '                    "<tr><td>&nbsp;</td></tr>" &
                '                "</table> <p><br><br><br><br></p>"

                Dim StrFinalCorreo As String = ""

                'Dim StrFinalCorreo As String = "<p align = ""center"">Le recordamos que puede leer nuestro anuncio de privacidad en <a href=""#"">www.mccollect.com.mx </a> </p>" &
                '                                "<p align = ""center"" > <b>ATENTAMENTE</b> <br><br></p>" &
                '                                "<p align = ""center"" ><font color=""blue"">MC Collect</font><br><br><br></p>" &
                '                                "<table align = ""center"">"

                Try


                    Dim htmlview = AlternateView.CreateAlternateViewFromString(StrEncabezado + Plantillaoriginal, Nothing, "text/html")
                    Dim htmlcorreo As String = (StrEncabezado + Plantillaoriginal + StrFinalCorreo)

                    Dim StrRutaLogo As String = Server.MapPath(".") & "\Imagenes\ImgLogo_Cliente.png"
                    'Dim StrRutaCuadros As String = Server.MapPath(".") & "\Imagenes\RecuadrosFINEM.png"
                    Dim LRLogoMail As New LinkedResource(StrRutaLogo)
                    'Dim LRCuadroMail As New LinkedResource(StrRutaCuadros)
                    LRLogoMail.ContentId = "LogoATT"
                    'LRCuadroMail.ContentId = "CuadrosFinem"
                    htmlview.LinkedResources.Add(LRLogoMail)
                    'htmlview.LinkedResources.Add(LRCuadroMail)


                    msg.AlternateViews.Add(htmlview)

                    msg.BodyEncoding = System.Text.Encoding.UTF8
                    msg.IsBodyHtml = True

                    'Dim client As New SmtpClient()
                    'client.Credentials = New System.Net.NetworkCredential(USUARIO, PASSWORD)
                    'client.Port = DtsMail.Rows(0).Item("CAT_CONF_PUERTO")
                    'client.Host = DtsMail.Rows(0).Item("CAT_CONF_HOST")
                    'If SSL = 1 Then
                    '    client.EnableSsl = True
                    'Else
                    '    client.EnableSsl = False
                    'End If



                    Try


                        Dim oraCommanEvnioMails2 As New SqlCommand("SP_EMAILS")
                        oraCommanEvnioMails2.CommandType = CommandType.StoredProcedure
                        oraCommanEvnioMails2.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 8
                        oraCommanEvnioMails2.Parameters.Add("@MAIL_DESTINO", SqlDbType.NVarChar).Value = correoenviar.Split("@")(1)
                        Dim dtab As DataTable = Consulta_Procedure(oraCommanEvnioMails2, "MAILS")

                        If dtab.Rows(0).Item("Valido") = 1 Then


                            Dim oraCommanEvnioMails As New SqlCommand("SP_EMAILS")
                            oraCommanEvnioMails.CommandType = CommandType.StoredProcedure
                            oraCommanEvnioMails.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 4
                            oraCommanEvnioMails.Parameters.Add("@MAIL_NOMBRE", SqlDbType.NVarChar).Value = DDLPerfilMail.SelectedText
                            oraCommanEvnioMails.Parameters.Add("@MAIL_DESTINO", SqlDbType.NVarChar).Value = correoenviar
                            oraCommanEvnioMails.Parameters.Add("@MAIL_MENSAJE", SqlDbType.NVarChar).Value = htmlcorreo
                            oraCommanEvnioMails.Parameters.Add("@MAIL_DESCRIPCION", SqlDbType.NVarChar).Value = DdlPlantilla.SelectedText
                            Dim DtsVarios As DataTable = Consulta_Procedure(oraCommanEvnioMails, "MAILS")
                            If DtsVarios.Rows(0).Item(0) = "ok" Then
                                Dim oraCommanEvnioMailss As New SqlCommand("SP_ADD_HIST_CORREOS")
                                oraCommanEvnioMailss.CommandType = CommandType.StoredProcedure
                                oraCommanEvnioMailss.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 3
                                oraCommanEvnioMailss.Parameters.Add("@v_hist_co_credito", SqlDbType.NVarChar).Value = tmpCredito("PR_MC_CREDITO")
                                oraCommanEvnioMailss.Parameters.Add("@v_hist_co_producto", SqlDbType.NVarChar).Value = tmpCredito("PR_MC_PRODUCTO")
                                oraCommanEvnioMailss.Parameters.Add("@v_hist_co_usuario", SqlDbType.NVarChar).Value = tmpUSUARIO("CAT_LO_USUARIO")
                                oraCommanEvnioMailss.Parameters.Add("@v_hist_co_agencia", SqlDbType.NVarChar).Value = tmpUSUARIO("CAT_LO_NUM_AGENCIA")
                                oraCommanEvnioMailss.Parameters.Add("@v_hist_co_fuente", SqlDbType.NVarChar).Value = DdlPlantilla.SelectedText

                                Dim DtsVarioss As DataTable = Consulta_Procedure(oraCommanEvnioMailss, "MAILS")
                                showModal(Notificacion, "", "ok", "Correo enviado a " & correoenviar)

                            Else
                                showModal(Notificacion, "warning", "Aviso", DtsVarios.Rows(0).Item(0))
                            End If

                        Else
                            showModal(Notificacion, "warning", "Aviso", "Dominio NO Seguro")
                        End If
                        'client.Send(msg)

                        'Dim SSCommandVALOR2 As New SqlCommand
                        'SSCommandVALOR2.CommandText = "SP_ADD_GESTIONES_MASIVAS"
                        'SSCommandVALOR2.CommandType = CommandType.StoredProcedure
                        'SSCommandVALOR2.Parameters.Add("@V_HIST_GE_CREDITO", SqlDbType.NVarChar).Value = tmpCredito("PR_MC_CREDITO")
                        'SSCommandVALOR2.Parameters.Add("@V_HIST_GE_USUARIO", SqlDbType.NVarChar).Value = "ATT"
                        'SSCommandVALOR2.Parameters.Add("@V_HIST_GE_COMENTARIO", SqlDbType.NVarChar).Value = DdlPlantilla.SelectedValue
                        'SSCommandVALOR2.Parameters.Add("@V_HIST_GE_EMAIL", SqlDbType.NVarChar).Value = correoenviar
                        'SSCommandVALOR2.Parameters.Add("@V_HIST_GE_CAMPANA", SqlDbType.NVarChar).Value = "ATT"
                        ''SSCommandVALOR2.Parameters.Add("@V_HIST_GE_BUCKET", SqlDbType.NVarChar).Value = CType(Session("Credito"), credito).PR_CF_BUCKET
                        'Dim DtsBuscaValor As datatable = Consulta_Procedure(SSCommandVALOR2, "AgregadoAlHistMasivas")

                        DdlPlantilla.SelectedText = "Seleccione"
                        TxtPlantilla.Content = ""
                        DdlPlantilla.SelectedValue = "Seleccione"
                        DDLPerfilMail.SelectedText = "Seleccione"
                        DDLPerfilMail.SelectedValue = "Seleccione"
                        'showModal(Notificacion, "ok", "Exito", "Correo enviado a " & correoenviar)
                    Catch ex As System.Net.Mail.SmtpException
                        'Ejecuta("insert into hist_errores (ERRORS) values('" & ex.ToString & "')")
                        showModal(Notificacion, "deny", "Error", ex.Message)
                        'LblMsj.Text = "No Se Pudo Enviar El Correo, Configuración Del Correo Inválida."
                        'MpuMensajes.Show()
                    End Try
                Catch ex As Exception
                    'LblMsj.Text = "No Se Pudo Enviar El Correo, Configuración Del Correo Inválida."
                    'MpuMensajes.Show()
                    showModal(Notificacion, "deny", "Error", ex.Message)
                        'Ejecuta("insert into hist_errores (ERRORS) values('" & ex.ToString & "')")
                    End Try
                End If
            'End If

        Catch ex As Exception
            showModal(Notificacion, "deny", "Error", ex.Message)
        End Try

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
    Public Property tmpPermisos As IDictionary
        Get
            Return CType(Session("Permisos"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("Permisos") = value
        End Set
    End Property

End Class
