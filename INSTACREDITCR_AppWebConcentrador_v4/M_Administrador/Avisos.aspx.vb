Imports System.Data
Imports System.Data.SqlClient
Imports Db
Imports Funciones
Imports System.IO
Imports Telerik.Web.UI
Imports Telerik.Web.UI.Calendar

Partial Class Administrador_Avisos
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
            RDPHist_Av_Dteexpira.MinDate = Today
            Dim Usr As String = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
        Catch ex As Exception
            OffLine(HidenUrs.Value)
            AUDITORIA(HidenUrs.Value, "Administrador", "Avisos", "", "Page_Load", "", Request.ServerVariables("REMOTE_ADDR"), Request.ServerVariables("REMOTE_HOST"))
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/SesionExpirada.aspx")
        End Try
        Try
            If Not IsPostBack Then
                HidenUrs.Value = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
                If CType(Session("USUARIOADMIN"), USUARIO).CAT_LO_MADMINISTRADOR.ToString.Substring(7, 1) = 0 Then
                    OffLine(HidenUrs.Value)
                    Session.Clear()
                    Session.Abandon()
                    Response.Redirect("~/SesionExpirada.aspx")
                End If
                If CType(Session("USUARIOADMIN"), USUARIO).cat_Lo_Num_Agencia = "0" Then
                    GVAgencias.DataSource = Llenar(" ORDER BY 1", "Agencias", 5)
                    GVAgencias.DataBind()

                Else
                    GVAgencias.DataSource = Llenar(" where CAT_AG_ID in (" & CType(Session("USUARIOADMIN"), USUARIO).CAT_LO_CADENAAGENCIAS & ") ORDER BY 1", "Agencias", 5)
                    GVAgencias.DataBind()

                End If
                Llenar_avisos()
            End If
        Catch ex As Exception
            SendMail("Page_Load", ex, "", "", HidenUrs.Value)
        End Try
    End Sub
    Function Llenar(Valor As String, Quien As String, Bandera As Integer) As DataTable
        Dim SSCommand As New SqlCommand("Sp_Avisos")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = Bandera
        SSCommand.Parameters.Add("@V_Valor", SqlDbType.NVarChar).Value = Valor
        Dim DtsVarios As DataTable = Consulta_Procedure(SSCommand, "Campos")
        Dim DtvVarios As DataView = DtsVarios.DefaultView
        If Quien = "MAviso" Then
            LblAccion.Visible = False
            DdlAccion.Visible = False
            BtnAceptar.Visible = False
            LblHist_Av_Dteexpira2.Visible = False
            RDPHist_AvDteexpira2.Visible = False
            If DtvVarios.Count > 0 Then
                GvAvisos.Visible = True
                DdlAccion.Visible = True
                LblAccion.Visible = True
            End If
        End If
        Return DtsVarios
    End Function

    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Administrador", "Avisos.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub

    Protected Sub chkSelectAll_CheckedChangedA(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim USR As String = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
            HidenUrs.Value = USR
        Catch ex As Exception
            OffLine(HidenUrs.Value)
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/SesionExpirada.aspx")
        End Try

        Try
            Dim Cuantas As Integer = 0
            Dim Agencias As String = "'"
            Dim chkAll As CheckBox = DirectCast(GVAgencias.HeaderRow.FindControl("chkSelectAll"), CheckBox)
            If chkAll.Checked = True Then
                For Each gvRow As GridViewRow In GVAgencias.Rows
                    Dim chkSel As CheckBox = DirectCast(gvRow.FindControl("chkSelect"), CheckBox)
                    chkSel.Checked = True
                    Cuantas = Cuantas + 1
                    Agencias = Agencias + HttpUtility.HtmlDecode(gvRow.Cells(2).Text) + "','"
                Next
            Else
                For Each gvRow As GridViewRow In GVAgencias.Rows
                    Dim chkSel As CheckBox = DirectCast(gvRow.FindControl("chkSelect"), CheckBox)
                    chkSel.Checked = False
                Next
            End If
            If Cuantas <> 0 Then
                Agencias = Agencias.Substring(0, Len(Agencias) - 2)
                GridViewUsrs.DataSource = Llenar("  CAT_LO_AGENCIA IN (" & Agencias & ") ORDER BY 1", "Usuarios", 6)
                GridViewUsrs.DataBind()
                LblUsr.Visible = True
                BtnUsrs.Visible = True

            Else
                GridViewUsrs.DataSource = Nothing
                GridViewUsrs.DataBind()
                LblUsr.Visible = False
                BtnAceptar.Visible = False

                BtnUsrs.Visible = False
            End If
        Catch ex As Exception
            SendMail("chkSelectAll_CheckedChangedA", ex, "", "", HidenUrs.Value)
        End Try
    End Sub
    Protected Sub chkSelect_CheckedChangedA(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim USR As String = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
            HidenUrs.Value = USR
        Catch ex As Exception
            OffLine(HidenUrs.Value)
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/SesionExpirada.aspx")
        End Try
        Try
            Dim Cuantas As Integer = 0
            Dim Agencias As String = "'"
            For Each gvRow As GridViewRow In GVAgencias.Rows
                Dim chkSel As CheckBox = DirectCast(gvRow.FindControl("chkSelect"), CheckBox)
                If chkSel.Checked = True Then
                    Cuantas = Cuantas + 1
                    Agencias = Agencias + gvRow.Cells(2).Text + "','"
                End If
            Next
            If Cuantas <> 0 Then
                Agencias = Agencias.Substring(0, Len(Agencias) - 2)
                TdGVUsers.Visible = True
                GridViewUsrs.DataSource = Llenar("  CAT_LO_AGENCIA IN (" & Agencias & ") ORDER BY 1", "Usuarios", 6)
                GridViewUsrs.DataBind()
                LblUsr.Visible = True
                BtnUsrs.Visible = True

            Else
                GridViewUsrs.DataSource = Nothing
                GridViewUsrs.DataBind()
                LblUsr.Visible = False
                RBtnCrear.Visible = False

                BtnUsrs.Visible = False
            End If
        Catch ex As Exception
            SendMail("chkSelectAll_CheckedChangedA", ex, "", "", HidenUrs.Value)
        End Try
    End Sub
    Protected Sub chkSelectAll_CheckedChangedU(ByVal sender As Object, ByVal e As EventArgs)
        DdlHist_Av_Prioridad.Visible = True
        LblHist_Av_Prioridad.Visible = True
        Try
            Dim USR As String = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
            HidenUrs.Value = USR
        Catch ex As Exception
            OffLine(HidenUrs.Value)
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/SesionExpirada.aspx")
        End Try
        Try
            Dim chkAll As CheckBox = DirectCast(GridViewUsrs.HeaderRow.FindControl("chkSelectAll"), CheckBox)
            If chkAll.Checked = True Then
                For Each gvRow As GridViewRow In GridViewUsrs.Rows
                    Dim chkSel As CheckBox = DirectCast(gvRow.FindControl("chkSelect"), CheckBox)
                    chkSel.Checked = True
                Next
            Else
                For Each gvRow As GridViewRow In GridViewUsrs.Rows
                    Dim chkSel As CheckBox = DirectCast(gvRow.FindControl("chkSelect"), CheckBox)
                    chkSel.Checked = False
                Next
            End If
        Catch ex As Exception

            SendMail("chkSelectAll_CheckedChangedU", ex, "", "", HidenUrs.Value)
        End Try
    End Sub
    Protected Sub Llenar_avisos()
        DdlAvisos.DataTextField = "Mensaje"
        DdlAvisos.DataValueField = "Mensaje"
        DdlAvisos.DataSource = Llenar(CType(Session("USUARIOADMIN"), USUARIO).CAT_LO_USUARIO, "Avisos", 7)
        DdlAvisos.DataBind()
        DdlAvisos.Items.Add("Seleccione")
        DdlAvisos.SelectedText = "Seleccione"
        LblPreAvisos.Visible = True
        DdlAvisos.Visible = True
    End Sub

    Protected Sub chkSelectAllA_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim USR As String = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
            HidenUrs.Value = USR
        Catch ex As Exception
            OffLine(HidenUrs.Value)
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/SesionExpirada.aspx")
        End Try
        Try
            Dim chkAll As CheckBox = DirectCast(GvAvisos.HeaderRow.FindControl("chkSelectAllA"), CheckBox)
            If chkAll.Checked = True Then
                For Each gvRow As GridViewRow In GvAvisos.Rows
                    Dim chkSel As CheckBox = DirectCast(gvRow.FindControl("chkSelectA"), CheckBox)
                    chkSel.Checked = True
                Next
            Else
                For Each gvRow As GridViewRow In GvAvisos.Rows
                    Dim chkSel As CheckBox = DirectCast(gvRow.FindControl("chkSelectA"), CheckBox)
                    chkSel.Checked = False
                Next
            End If
        Catch ex As Exception
            SendMail("chkSelectAllA_CheckedChanged", ex, "", "", HidenUrs.Value)
        End Try
    End Sub

    Protected Sub Limpiar()
        TdGVAgencias.Visible = False
        TdGVUsers.Visible = False
        LblUsr.Visible = False
        BtnUsrs.Visible = False
        LblHist_Av_Prioridad.Visible = False
        DdlHist_Av_Prioridad.Visible = False
        LblHist_Av_Dteexpira.Visible = False
        RDPHist_Av_Dteexpira.Visible = False
        LblMsjAvisos.Visible = False
        RTxtHist_Av_Mensaje.Visible = False
        DdlHist_Av_Prioridad.SelectedText = "Seleccione"
        RDPHist_Av_Dteexpira.SelectedDate = Nothing
        RTxtHist_Av_Mensaje.Text = ""
        DdlAccion.SelectedText = "Seleccione"
        DdlAccion.Visible = False
        LblAccion.Visible = False
        RDPHist_AvDteexpira2.SelectedDate = Nothing
        RDPHist_AvDteexpira2.Visible = False
        LblHist_Av_Dteexpira2.Visible = False
        GvAvisos.Visible = False
        BtnAceptar.Visible = False
        DdlAvisos.SelectedText = "Seleccione"
    End Sub




    Sub ModificarAviso(V_HIST_AV_DTEEXPIRA As String, V_HIST_AV_ESTATUS As String, V_HIST_AV_MENSAJE As String, V_HIST_AV_PRIORIDAD As String, V_HIST_AV_RECEPTOR As String, V_BANDERA As Integer)
        Dim SSCommand As New SqlCommand("SP_ADD_AVISOS")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_HIST_AV_DTEEXPIRA", SqlDbType.NVarChar).Value = V_HIST_AV_DTEEXPIRA
        SSCommand.Parameters.Add("@V_HIST_AV_ESTATUS", SqlDbType.NVarChar).Value = V_HIST_AV_ESTATUS
        SSCommand.Parameters.Add("@V_HIST_AV_MENSAJE", SqlDbType.NVarChar).Value = V_HIST_AV_MENSAJE
        SSCommand.Parameters.Add("@V_HIST_AV_PRIORIDAD", SqlDbType.NVarChar).Value = V_HIST_AV_PRIORIDAD
        SSCommand.Parameters.Add("@V_HIST_AV_EMISOR", SqlDbType.NVarChar).Value = CType(Session("USUARIOADMIN"), USUARIO).CAT_LO_USUARIO
        SSCommand.Parameters.Add("@V_HIST_AV_RECEPTOR", SqlDbType.NVarChar).Value = V_HIST_AV_RECEPTOR
        SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.NVarChar).Value = V_BANDERA
        Ejecuta_Procedure(SSCommand)
    End Sub


    Protected Sub Aviso(ByVal MSJ As String)
        RadAviso.RadAlert(MSJ, 440, 155, "AVISO", Nothing)
    End Sub


    Private Sub DdlHist_Av_Prioridad_SelectedIndexChanged(sender As Object, e As DropDownListEventArgs) Handles DdlHist_Av_Prioridad.SelectedIndexChanged
        If DdlHist_Av_Prioridad.SelectedIndex <> 0 Then
            RDPHist_Av_Dteexpira.Visible = True
            LblHist_Av_Dteexpira.Visible = True
        Else
            RDPHist_Av_Dteexpira.Visible = False
            LblHist_Av_Dteexpira.Visible = False
            RDPHist_Av_Dteexpira.SelectedDate = Nothing
        End If
    End Sub

    Private Sub RDPHist_Av_Dteexpira_SelectedDateChanged(sender As Object, e As SelectedDateChangedEventArgs) Handles RDPHist_Av_Dteexpira.SelectedDateChanged
        RBtnCrear.Visible = True
        RTxtHist_Av_Mensaje.Visible = True
        LblMsjAvisos.Visible = True

    End Sub

    Private Sub RBtnCrear_Click(sender As Object, e As EventArgs) Handles RBtnCrear.Click
        Try

            Dim USR As String = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
            HidenUrs.Value = USR
        Catch ex As Exception
            OffLine(HidenUrs.Value)
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/SesionExpirada.aspx")
        End Try
        If RTxtHist_Av_Mensaje.Text.Length < 5 Then
            Aviso("Capture Un Aviso Valido")
        ElseIf DdlHist_Av_Prioridad.SelectedValue = "Seleccione" Then
            Aviso("Seleccione Prioridad Del Mensaje")
        ElseIf RDPHist_Av_Dteexpira.SelectedDate.ToString = "" Then
            Aviso("Seleccione Fecha En Que Expirara El Aviso")
        Else

            For Each gvRow As GridViewRow In GridViewUsrs.Rows
                Dim chkSel As CheckBox = DirectCast(gvRow.FindControl("chkSelect"), CheckBox)
                If chkSel.Checked = True Then
                    ModificarAviso(RDPHist_Av_Dteexpira.SelectedDate.Value, 0, RTxtHist_Av_Mensaje.Text, DdlHist_Av_Prioridad.SelectedValue, HttpUtility.HtmlDecode(gvRow.Cells(1).Text), 1)
                End If
            Next
            Aviso("Aviso Creado")
            Limpiar()
            Llenar_avisos()
        End If

    End Sub

    Public Sub GridViewUsrs_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridViewUsrs.SelectedIndexChanged
        DdlHist_Av_Prioridad.Visible = True
        LblHist_Av_Prioridad.Visible = True
    End Sub

    Private Sub DdlAvisos_SelectedIndexChanged(sender As Object, e As DropDownListEventArgs) Handles DdlAvisos.SelectedIndexChanged
        Try
            Dim USR As String = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
            HidenUrs.Value = USR
        Catch ex As Exception
            OffLine(HidenUrs.Value)
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/SesionExpirada.aspx")
        End Try
        Try
            GvAvisos.DataSource = Llenar(DdlAvisos.SelectedValue, "MAviso", 8)
            GvAvisos.DataBind()
            GvAvisos.Visible = True
        Catch ex As Exception
            SendMail("CbAvisos_SelectedIndexChanged", ex, "", "", HidenUrs.Value)
        End Try
    End Sub

    Private Sub DdlAccion_SelectedIndexChanged(sender As Object, e As DropDownListEventArgs) Handles DdlAccion.SelectedIndexChanged
        Try
            Dim USR As String = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
            HidenUrs.Value = USR
        Catch ex As Exception
            OffLine(HidenUrs.Value)
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/SesionExpirada.aspx")
        End Try

        Try
            BtnAceptar.Visible = False
            RDPHist_AvDteexpira2.Visible = False
            LblHist_Av_Dteexpira2.Visible = False
            If DdlAccion.SelectedText = "Reactivar" Then
                RDPHist_AvDteexpira2.Visible = True
                LblHist_Av_Dteexpira2.Visible = True
                BtnAceptar.Visible = True
            ElseIf DdlAccion.SelectedText = "Expirar" Then
                BtnAceptar.Visible = True
            ElseIf DdlAccion.SelectedText = "Eliminar" Then
                BtnAceptar.Visible = True
            End If
        Catch ex As Exception
            SendMail("DdlAccion_SelectedIndexChanged", ex, "", "", HidenUrs.Value)
        End Try
    End Sub
    Sub MostrarOcultarVentana(ByVal V_Ventana As Object, ByVal V_Bandera As String, ByVal V_Maximizada As String)
        If V_Bandera = 1 Then
            Dim script As String
            If V_Maximizada = 1 Then
                script = "function f(){$find(""" + V_Ventana.ClientID + """).show();$find(""" + V_Ventana.ClientID + """).maximize(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
            Else
                script = "function f(){$find(""" + V_Ventana.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
            End If
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
        Else
            Dim script As String = "function f(){$find(""" + V_Ventana.ClientID + """).hide(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
        End If
    End Sub

    Private Sub RBtnAvisosPrevios_Click(sender As Object, e As EventArgs) Handles RBtnAvisosPrevios.Click
        MostrarOcultarVentana(WindowMostrar, 1, 0)
    End Sub

    Private Sub BtnSucursales_Click(sender As Object, e As ImageClickEventArgs) Handles BtnSucursales.Click
        TdGVAgencias.Visible = True
    End Sub

    Private Sub BtnAceptar_Click(sender As Object, e As EventArgs) Handles BtnAceptar.Click
        Try
            Dim USR As String = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
            HidenUrs.Value = USR
        Catch ex As Exception
            OffLine(HidenUrs.Value)
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/SesionExpirada.aspx")
        End Try
        Try
            If DdlAccion.SelectedText = "Reactivar" And RDPHist_AvDteexpira2.SelectedDate.ToString = "" Then
                Aviso("Seleccione La Fecha En La Que Expirara El Aviso"
                )
            Else
                If DdlAccion.SelectedText = "Reactivar" Then

                    For Each gvRow As GridViewRow In GvAvisos.Rows
                        Dim chkSel As CheckBox = DirectCast(gvRow.FindControl("chkSelectA"), CheckBox)
                        If chkSel.Checked = True Then
                            ModificarAviso(RDPHist_AvDteexpira2.SelectedDate.Value, "", HttpUtility.HtmlDecode(gvRow.Cells(1).Text), "", HttpUtility.HtmlDecode(gvRow.Cells(8).Text), 2)
                        End If
                    Next
                    Aviso("Aviso(s) Reactivado(s)")
                    Limpiar()
                    Llenar_avisos()
                    MostrarOcultarVentana(WindowMostrar, 0, 0)
                ElseIf DdlAccion.SelectedText = "Expirar" Then
                    For Each gvRow As GridViewRow In GvAvisos.Rows
                        Dim chkSel As CheckBox = DirectCast(gvRow.FindControl("chkSelectA"), CheckBox)
                        If chkSel.Checked = True Then
                            ModificarAviso("", "", HttpUtility.HtmlDecode(gvRow.Cells(1).Text), "", HttpUtility.HtmlDecode(gvRow.Cells(8).Text), 3)
                        End If
                    Next
                    Aviso("Aviso(s) Expirado(s)")
                    Limpiar()
                    Llenar_avisos()
                    MostrarOcultarVentana(WindowMostrar, 0, 0)
                ElseIf DdlAccion.SelectedText = "Eliminar" Then
                    For Each gvRow As GridViewRow In GvAvisos.Rows
                        Dim chkSel As CheckBox = DirectCast(gvRow.FindControl("chkSelectA"), CheckBox)
                        If chkSel.Checked = True Then
                            ModificarAviso("", "", HttpUtility.HtmlDecode(gvRow.Cells(1).Text), "", HttpUtility.HtmlDecode(gvRow.Cells(8).Text), 4)
                        End If
                    Next
                    Aviso("Aviso(s) Eliminado(s)")
                    Limpiar()
                    Llenar_avisos()
                    MostrarOcultarVentana(WindowMostrar, 0, 0)
                End If
            End If

        Catch ex As System.Threading.ThreadAbortException
            Aviso(ex.Message)
        Catch ex As Exception
            Aviso(ex.Message)
            SendMail("BtnAceptar_Click", ex, "", "", HidenUrs.Value)
        End Try
    End Sub

    Private Sub BtnCancelar_Click(sender As Object, e As EventArgs) Handles BtnCancelar.Click
        Limpiar()
        MostrarOcultarVentana(WindowMostrar, 0, 0)
    End Sub
End Class

