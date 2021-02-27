Imports System.Data
Imports System.Data.SqlClient
    Imports Db
    Imports Funciones
    Imports System.IO
    Imports Telerik.Web.UI

Partial Class M_Administrador_CatalogoRespGestion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim Usr As String = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
        Catch ex As Exception
            OffLine(HidenUrs.Value)
            AUDITORIA(HidenUrs.Value, "Administrador", "Catalogos Judiciales", "", "Page_Load", "", Request.ServerVariables("REMOTE_ADDR"), Request.ServerVariables("REMOTE_HOST"))
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/SesionExpirada.aspx")
        End Try
        Try
            If Not IsPostBack Then
                HidenUrs.Value = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
                If CType(Session("USUARIOADMIN"), USUARIO).CAT_LO_MADMINISTRADOR.ToString.Substring(0, 1) = 0 Then
                    OffLine(HidenUrs.Value)
                    Session.Clear()
                    Session.Abandon()
                    Response.Redirect("~/SesionExpirada.aspx")
                End If
                Session("gridAnterior") = Nothing
            End If
        Catch ex As Exception
            SendMail("Page_Load", ex, "", "", HidenUrs.Value)
        End Try
    End Sub
    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Administrador", "Usuarios.aspx", evento, ex, Cuenta, Captura, usr)
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

    Private Sub llenarcatalogos()
        For Each item As GridDataItem In gridRespGestion.MasterTableView.Items
            item.Edit = False
        Next
        gridRespGestion.Rebind()
    End Sub

    Protected Sub gridRespGestion_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles gridRespGestion.NeedDataSource
        dim SSCommand as new sqlcommand
        SSCommand.CommandText = "SP_EDITA_RESPONSABLES_GESTION"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_dato1", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@v_dato2", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@v_dato3", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@v_dato4", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@v_dato5", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@v_dato6", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@v_dato7", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@v_dato8", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@v_dato9", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@v_dato10", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@v_bandera", SqlDbType.Decimal).Value = 0

        Dim objDSa As DataTable = Consulta_Procedure(SSCommand, "Filtros")

        If objDSa.Rows.Count > 0 Then
            gridRespGestion.DataSource = objDSa
        End If

    End Sub

    Private Sub gridTipoJuicio_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles gridRespGestion.ItemCommand
        Session("PostBack") = True
        If e.CommandName = "Update" Then

            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(3) As String

            valores(0) = CType(MyUserControl.FindControl("TxtNombre"), RadTextBox).Text
            valores(1) = CType(MyUserControl.FindControl("RCBUsuarioResp"), RadComboBox).SelectedValue
            valores(2) = CType(MyUserControl.FindControl("RTBEmailResp"), RadTextBox).Text


            Try
                If valores(0) = "" Or valores(1) = "" Or valores(2) = "" Then
                    Aviso("Falta capturar al menos un dato")
                    e.Canceled = True
                Else
                    Dim dtsguardado As DataSet = EditarResponsablesGestion(valores(0), valores(1), valores(2), Session("VALORID"), "", "", "", "", "", "", 2)
                    Aviso(dtsguardado.Tables(0).Rows.Item(0)("MSJ").ToString)
                End If
            Catch ex As Exception
                e.Canceled = True
                Aviso("No se realizo la actualizacion debido a: " & ex.Message)
            End Try
        ElseIf e.CommandName = "Edit" Then
            Session("VALORID") = e.Item.Cells.Item(3).Text
            Session("Responsable") = e.Item.Cells.Item(4).Text
        End If
    End Sub
    Public Function EditarResponsablesGestion(ByVal V_Valor1 As String, ByVal V_Valor2 As String, ByVal V_Valor3 As String, ByVal V_Valor4 As String, ByVal V_Valor5 As String, ByVal V_Valor6 As String, ByVal V_Valor7 As String, ByVal V_Valor8 As String, ByVal V_Valor9 As String, ByVal V_Valor10 As String, ByVal V_Bandera As Integer) As Object
        dim SSCommand as new sqlcommand
        SSCommand.CommandText = "SP_EDITA_RESPONSABLES_GESTION"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_dato1", SqlDbType.NVarChar).Value = V_Valor1
        SSCommand.Parameters.Add("@v_dato2", SqlDbType.NVarChar).Value = V_Valor2
        SSCommand.Parameters.Add("@v_dato3", SqlDbType.NVarChar).Value = V_Valor3
        SSCommand.Parameters.Add("@v_dato4", SqlDbType.NVarChar).Value = V_Valor4
        SSCommand.Parameters.Add("@v_dato5", SqlDbType.NVarChar).Value = V_Valor5
        SSCommand.Parameters.Add("@v_dato6", SqlDbType.NVarChar).Value = V_Valor6
        SSCommand.Parameters.Add("@v_dato7", SqlDbType.NVarChar).Value = V_Valor7
        SSCommand.Parameters.Add("@v_dato8", SqlDbType.NVarChar).Value = V_Valor8
        SSCommand.Parameters.Add("@v_dato9", SqlDbType.NVarChar).Value = V_Valor9
        SSCommand.Parameters.Add("@v_dato10", SqlDbType.NVarChar).Value = V_Valor10
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = V_Bandera

        Dim DtsNegociaciones As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return DtsNegociaciones
    End Function
End Class


