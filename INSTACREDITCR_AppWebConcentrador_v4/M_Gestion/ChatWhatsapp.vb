Imports Conexiones
Imports System.Data.SqlClient
Imports System.Data
Imports Db
Imports Funciones
Imports System.Timers
Imports Telerik.Web.UI
Imports System.Web.Services
Imports System.Net

Partial Class M_Gestion_ChatWhatsapp
    Inherits System.Web.UI.Page
    Public Property tmpCredito As IDictionary
        Get
            Return CType(Session("Credito"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("Credito") = value
        End Set
    End Property
    Public Property tmpUSUARIO As IDictionary
        Get
            Return CType(Session("USUARIO"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("USUARIO") = value
        End Set
    End Property

    Protected Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim DtsChat As New DataTable
        'If Session("Credito") Is Nothing Then
        '    PnlChat.Visible = False

        'Else
        DtsChat = llenaGridChat(1, lblCredito.Text, Nothing, Nothing, Nothing, Nothing, Nothing, lblTelefono.Text)

        GrdWhatsapp.Rebind()
            GrdWhatsapp.DataSource = DtsChat
            GrdWhatsapp.DataBind()
            PnlChat.Visible = True
        'End If
    End Sub
    Protected Sub RGChats_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs)
        Try
            'RGChats.DataSource = llenaGridChat(3, Nothing, Nothing, tmpUSUARIO("CAT_LO_USUARIO"), Nothing, Nothing, Nothing)

            RGChats.DataSource = llenaGridChatsPendientes(tmpUSUARIO("CAT_LO_USUARIO"))
        Catch ex As Exception
            RGChats.DataSource = Nothing
        End Try
    End Sub
    Function llenaGridChatsPendientes(usuario As String) As Object
        Dim Dtspendientes As DataTable
        Dim Dts As New DataTable
        Dim Dtsresp As New DataTable
        Dts.Columns.Add("CREDITO")
        Dts.Columns.Add("FECHA")
        Dts.Columns.Add("TELEFONO")
        Dtsresp.Columns.Add("CREDITO")
        Dtsresp.Columns.Add("FECHA")
        Dtsresp.Columns.Add("TELEFONO")
        Dtsresp.Columns.Add("MENSAJE")
        Dim SSCommand2 As New SqlCommand("SP_CHAT_WHATSAPP")
        SSCommand2.CommandType = CommandType.StoredProcedure
        SSCommand2.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = 3
        SSCommand2.Parameters.Add("@V_GESTOR", SqlDbType.VarChar).Value = usuario
        Dtspendientes = Consulta_Procedure(SSCommand2, "SP_CHAT_WHATSAPP")
        For gbrow As Integer = 0 To Dtspendientes.Rows.Count - 1
            If Dtspendientes.Rows(gbrow).Item("CREDITO").ToString = "" Then
            Else
                If (Dtspendientes.Rows(gbrow).Item("ORIGEN")).ToString = "MC" Then
                    Dim Renglon As DataRow = Dts.NewRow()
                    Renglon("CREDITO") = Dtspendientes.Rows(gbrow).Item("CREDITO")
                    Renglon("FECHA") = Dtspendientes.Rows(gbrow).Item("FECHA")
                    Renglon("TELEFONO") = Dtspendientes.Rows(gbrow).Item("TELEFONO")
                    Dts.Rows.Add(Renglon)

                ElseIf (Dtspendientes.Rows(gbrow).Item("ORIGEN")).ToString = "WHATSAPP" Then
                    Dim Renglon As DataRow = Dtsresp.NewRow()
                    Renglon("CREDITO") = Dtspendientes.Rows(gbrow).Item("CREDITO").ToString + "*"
                    Renglon("FECHA") = Dtspendientes.Rows(gbrow).Item("FECHA")
                    Renglon("TELEFONO") = Dtspendientes.Rows(gbrow).Item("TELEFONO")
                    Renglon("MENSAJE") = "*Mensajes pendientes*"
                    Dtsresp.Rows.Add(Renglon)
                End If
            End If
        Next
        For gbrow As Integer = 0 To Dts.Rows.Count - 1
            Dim Renglon As DataRow = Dtsresp.NewRow()
            Renglon("CREDITO") = Dts.Rows(gbrow).Item("CREDITO")
            Renglon("FECHA") = Dts.Rows(gbrow).Item("FECHA")
            Renglon("TELEFONO") = Dts.Rows(gbrow).Item("TELEFONO")
            Dtsresp.Rows.Add(Renglon)
        Next

        Return Dtsresp
    End Function
    Private Sub RGChats_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RGChats.ItemCommand
        If e.CommandName = "irChat" Then
            PnlOtrosChats.Visible = False
            PnlChat.Visible = True
            RDLTelefonos.Visible = False
            Dim credito As String = e.Item.Cells.Item(3).Text
            lblTelefono.Text = e.Item.Cells.Item(5).Text
            lblCredito.Text = credito.Replace("*", "")
            Dim DtsChat As DataTable = llenaGridChat(1, lblCredito.Text, Nothing, Nothing, Nothing, Nothing, Nothing, lblTelefono.Text)

            GrdWhatsapp.Rebind()
            GrdWhatsapp.DataSource = DtsChat
            GrdWhatsapp.DataBind()
            llenarcombotelefonos()
            RDLPlWhatsapp.ClearSelection()
            TxtMensaje.Text = ""
        End If
    End Sub
    Protected Sub Grdwhatsapp_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs)
        Try
            GrdWhatsapp.DataSource = llenaGridChat(1, tmpCredito("PR_MC_CREDITO"), Nothing, Nothing, Nothing, Nothing, Nothing, lblTelefono.Text)
        Catch ex As Exception
            GrdWhatsapp.DataSource = Nothing
        End Try
    End Sub
    Function llenaGridChat(bandera As Integer, credito As String, mensaje As String, usuario As String, origen As String, id_p As String, id_h As String, telefono As String) As Object
        Dim DtsChat1 As DataTable = SP.CHAT_WHATSAPP(bandera, credito, mensaje, usuario, origen, id_p, id_h, telefono)
        Dim DtsChat As New DataTable
        DtsChat.Columns.Add("whats")
        DtsChat.Columns.Add("mc")

        For gbrow As Integer = 0 To DtsChat1.Rows.Count - 1
            If DtsChat1.Rows(gbrow).Item("MSJ").ToString = "" Then
            Else
                If (DtsChat1.Rows(gbrow).Item("MSJ")).ToString.Substring(0, 2) = "MC" Then
                    Dim Renglon As DataRow = DtsChat.NewRow()
                    Dim nmensaje As String = DtsChat1.Rows(gbrow).Item("MSJ")
                    nmensaje = nmensaje.ToString.Replace("MC", "")
                    Renglon("mc") = nmensaje
                    Renglon("whats") = ""
                    DtsChat.Rows.Add(Renglon)

                ElseIf (DtsChat1.Rows(gbrow).Item("MSJ")).ToString.Substring(0, 2) = "WH" Then
                    Dim Renglon As DataRow = DtsChat.NewRow()
                    Dim nmensaje As String = DtsChat1.Rows(gbrow).Item("MSJ")
                    nmensaje = nmensaje.ToString.Replace("WHATSAPP", "")
                    Renglon("mc") = ""
                    Renglon("whats") = nmensaje
                    DtsChat.Rows.Add(Renglon)
                End If

            End If
        Next

        Return DtsChat
    End Function


    Protected Sub RBEnviar_click(sender As Object, e As EventArgs) Handles RBEnviar.Click
        lblmsj.Text = ""
        If TxtMensaje.Text = "" Then
            lblmsj.Text = "Debes escribir un mensaje antes de enviar"
        Else
            Dim credito As String = tmpCredito("PR_MC_CREDITO")
            Dim telefono As String = "+52" + lblTelefono.Text
            'Dim DtsChat1 As DataTable = SP.CHAT_WHATSAPP(0, credito, TxtMensaje.Text, tmpUSUARIO("CAT_LO_USUARIO"), "MC")
            Dim myText As String = ""
            Try
                Dim myReq As HttpWebRequest
                Dim myResp As HttpWebResponse

                myReq = HttpWebRequest.Create("https://api-messaging.wavy.global/v1/whatsapp/send")
                myReq.Method = "POST"
                myReq.ContentType = "application/json"
                myReq.Headers.Add("userName", "wa_credifiel_mx")
                myReq.Headers.Add("authenticationToken", "--mmUXCnICHhcBeCSmxMT_SI_uLFxikXoVkhTM15")

                Dim myData As String = "{""destinations"":[{""correlationId"":""" + credito + """,""destination"": """ + telefono + """,""recipientType"":""individual""}],""message"": {""messageText"": """ + TxtMensaje.Text + """},""campaignAlias"":""campaignAlias""}"
                '"{""messages"":[{""destination"":" + telefono + ",""messageText"":" + TxtMensaje.Text + "}]}"
                myReq.GetRequestStream.Write(System.Text.Encoding.UTF8.GetBytes(myData), 0, System.Text.Encoding.UTF8.GetBytes(myData).Count)
                myResp = myReq.GetResponse

                Dim myreader As New System.IO.StreamReader(myResp.GetResponseStream)
                If myResp.StatusDescription = "OK" Then

                    myText = myreader.ReadToEnd
                    myText = myText.Replace("{", "").Replace("}", "").Replace("[", "").Replace("]", "").Replace(",", "").Replace("""", "").Replace("id", "").Replace("destinations", "").Replace("destination", "").Replace("correlationId", "")
                    Dim ID_P As String = myText.ToString.Split(":")(1)
                    Dim ID_H As String = myText.ToString.Split(":")(3)
                    Dim DtsChat As New DataTable
                    DtsChat = llenaGridChat(0, credito, TxtMensaje.Text, tmpUSUARIO("CAT_LO_USUARIO"), "MC", ID_P, ID_H, lblTelefono.Text)

                    GrdWhatsapp.DataSource = DtsChat
                    GrdWhatsapp.DataBind()
                    TxtMensaje.Text = ""
                End If

                myText = myreader.ReadToEnd
            Catch ax As WebException
                If ax.Response Is Nothing Then
                    myText = ax.Message
                Else
                    Dim myreader As New System.IO.StreamReader(ax.Response.GetResponseStream)
                    myText = myreader.ReadToEnd
                End If
            Catch ex As Exception
                myText = ex.Message.ToString
            End Try

            lblmsj.Text = myText
            TxtMensaje.Enabled = True
            llenarcomboplantilla()
        End If
    End Sub
    Private Sub RDLTelefonos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RDLTelefonos.SelectedIndexChanged
        lblTelefono.Text = RDLTelefonos.SelectedValue
        Dim DtsChat As DataTable = llenaGridChat(1, lblCredito.Text, Nothing, Nothing, Nothing, Nothing, Nothing, lblTelefono.Text)

        GrdWhatsapp.Rebind()
        GrdWhatsapp.DataSource = DtsChat
        GrdWhatsapp.DataBind()
        TxtMensaje.Enabled = False
    End Sub
    Private Sub RCBPlantillaWhats_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RDLPlWhatsapp.SelectedIndexChanged
        Dim plantilla As String = RDLPlWhatsapp.SelectedValue


        Dim oraCommanAgencias As New SqlCommand
        oraCommanAgencias.CommandText = "SP_ADD_CAT_ETIQUETAS_WHATSAPP"
        oraCommanAgencias.CommandType = CommandType.StoredProcedure
        oraCommanAgencias.Parameters.Add("@v_credito", SqlDbType.VarChar).Value = lblCredito.Text
        oraCommanAgencias.Parameters.Add("@v_plantilla", SqlDbType.VarChar).Value = plantilla
        oraCommanAgencias.Parameters.Add("@V_Bandera", SqlDbType.VarChar).Value = 16

        Dim DtsVarios As DataTable = Consulta_Procedure(oraCommanAgencias, "Etiqueta")
        If DtsVarios(0)(0).ToString = "" Then
            TxtMensaje.Text = "Error al consultar credito: " + lblCredito.Text
        Else
            TxtMensaje.Text = DtsVarios.Rows(0).Item(0)
            TxtMensaje.Enabled = False

        End If
    End Sub

    Function algo(objAlgo As Object) As Boolean
        If IsDBNull(algo) Then
            Return False
        Else
            Return True
        End If
    End Function
    Private Sub M_Gestion_ChatWhatsapp_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim DtsChat As New DataTable


        If Not IsPostBack Then
            If Session("Credito") Is Nothing Then
                lblCredito.Text = ""
                PnlChat.Visible = False
                PnlOtrosChats.Visible = True
            Else
                lblCredito.Text = tmpCredito("PR_MC_CREDITO")
                'DtsChat = llenaGridChat(1, lblCredito.Text, Nothing, Nothing, Nothing, Nothing, Nothing)

                'GrdWhatsapp.Rebind()
                'GrdWhatsapp.DataSource = DtsChat
                'GrdWhatsapp.DataBind()
                PnlChat.Visible = True
                PnlOtrosChats.Visible = False
            End If
            llenarcomboplantilla()
            llenarcombotelefonos()
            ' GrdWhatsapp.Rebind()
            'LLENAR_DROP2(37, tmpCredito("PR_MC_CREDITO"), CBParticipantes, "ValPar", "TexPar")
            'LLENAR_DROP2(38, "", CBVisitadores, "vvis", "tvis")
            ''LLENAR_DROP2(39, tmpCredito("PR_MC_CLIENTE"), CbTipoVivienda, "valdir", "textdir", CBParticipantes.SelectedValue)
            'LLENAR_DROP2(40, "", cbPlantillas, "id", "nombre")
            'DPFelimite.MinDate = Today
            'LblError.Text = ""
        End If
    End Sub
    Sub llenarcombotelefonos()
        RDLTelefonos.ClearSelection()
        Dim oraCommanEtiquetas As New SqlCommand
        oraCommanEtiquetas.CommandText = "SP_CHAT_WHATSAPP"
        oraCommanEtiquetas.CommandType = CommandType.StoredProcedure

        oraCommanEtiquetas.Parameters.Add("@V_Bandera", SqlDbType.VarChar).Value = 4
        oraCommanEtiquetas.Parameters.Add("@V_CREDITO", SqlDbType.VarChar).Value = lblCredito.Text
        Dim DtsVarios As DataTable = Consulta_Procedure(oraCommanEtiquetas, "Etiqueta")

        Dim DtvVarios As DataView = DtsVarios.DefaultView
        RDLTelefonos.DataTextField = "Telefono"
        RDLTelefonos.DataValueField = "Telefono"
        RDLTelefonos.DataSource = DtsVarios
        RDLTelefonos.DataBind()
        RDLTelefonos.SelectedText = "Seleccione"
    End Sub
    Sub llenarcomboplantilla()
        RDLPlWhatsapp.ClearSelection()
        Dim oraCommanEtiquetas As New SqlCommand
        oraCommanEtiquetas.CommandText = "SP_ADD_CAT_ETIQUETAS_WHATSAPP"
        oraCommanEtiquetas.CommandType = CommandType.StoredProcedure

        oraCommanEtiquetas.Parameters.Add("@V_Bandera", SqlDbType.VarChar).Value = 9

        Dim DtsVarios As DataTable = Consulta_Procedure(oraCommanEtiquetas, "Etiqueta")

        Dim DtvVarios As DataView = DtsVarios.DefaultView
        RDLPlWhatsapp.DataTextField = "Nombre"
        RDLPlWhatsapp.DataValueField = "Nombre"
        RDLPlWhatsapp.DataSource = DtsVarios
        RDLPlWhatsapp.DataBind()
        RDLPlWhatsapp.SelectedText = "Seleccione"
    End Sub
    Dim valido As Integer = 0
    Private Sub BtnOtrosChats_Click(sender As Object, e As EventArgs) Handles BtnOtrosChats.Click

        PnlChat.Visible = False
        PnlOtrosChats.Visible = True
            RGChats.Rebind()

    End Sub

    Private Sub BtnRegresar_Click(sender As Object, e As EventArgs) Handles BtnRegresar.Click
        PnlChat.Visible = True
        PnlOtrosChats.Visible = False
        RGChats.Rebind()
    End Sub
End Class
