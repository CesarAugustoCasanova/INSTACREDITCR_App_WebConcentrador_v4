Imports System.Data
Imports System.Data.SqlClient
Imports Conexiones
Imports Db
Imports Funciones
Imports Telerik.Web.UI

Partial Class ConfigurarCorreo
    Inherits System.Web.UI.Page
    Public Property tmpUSUARIO As IDictionary
        Get
            Return CType(Session("USUARIO"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("USUARIO") = value
        End Set
    End Property
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'Try
        '    Dim USR As String = (CType(Session("USUARIO"), USUARIO)).CAT_LO_USUARIO
        '    HidenUrs.Value = USR
        'Catch ex As Exception
        '    Session.Clear()
        '    Session.Abandon()
        '    Response.Redirect("~/SesionExpirada.aspx")
        'End Try
        'Try
        If Session("Usuario") IsNot Nothing Then
            HidenUrs.Value = tmpUSUARIO("CAT_LO_USUARIO")
            Try
                If CType(GetSessionActive.Search(tmpUSUARIO("CAT_LO_USUARIO"), 1), Integer) = 0 Then
                    Session.Clear()
                    Session.Abandon()
                    Response.Redirect("~/SesionExpirada.aspx")
                Else
                    If Not IsPostBack Then
                        llenar()
                    End If
                End If
            Catch ex As System.Threading.ThreadAbortException
                Aviso(ex.ToString)
            Catch ex As Exception
                Aviso(ex.ToString)
                SendMail("Page_Load", ex, "", "", HidenUrs.Value)
            End Try
        Else
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/SesionExpirada.aspx")
        End If
    End Sub

    Private Function llenar() As DataTable


        Dim SSCommand1 As New SqlCommand
        SSCommand1.CommandText = "SP_CONFIGURACION_MAIL"
        SSCommand1.CommandType = CommandType.StoredProcedure
        SSCommand1.Parameters.Add("@v_bandera", SqlDbType.Decimal).Value = 2
        Dim objDS As DataTable = Consulta_Procedure(SSCommand1, "TIPO")

        Return objDS

    End Function


    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Administrador", "CatalogoCorreos.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub
    Protected Sub Aviso(ByVal MSJ As String)
        RadAviso.RadAlert(MSJ, 440, 155, "AVISO", Nothing)
    End Sub
    Protected Sub Confirma(ByVal MSJ As String)
        RadAviso.RadConfirm(MSJ, "confirmCallbackFn", 440, 155, Nothing, "Alerta")
    End Sub

    Public Sub RGVCorreoSalida_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RGVCorreoSalida.NeedDataSource
        RGVCorreoSalida.DataSource = llenar()
    End Sub

    Private Sub RGVCorreoSalida_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RGVCorreoSalida.ItemCommand

        If e.CommandName = "Update" Then
            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(9) As String

            valores(0) = CType(MyUserControl.FindControl("TxtCAT_CONF_USER"), RadTextBox).Text
            valores(1) = CType(MyUserControl.FindControl("TxtCAT_CONF_PWD"), RadTextBox).Text
            valores(2) = CType(MyUserControl.FindControl("TxtCAT_CONF_HOST"), RadTextBox).Text
            valores(3) = CType(MyUserControl.FindControl("TxtCAT_CONF_PUERTO"), RadTextBox).Text
            valores(4) = CType(MyUserControl.FindControl("DdlCAT_CONF_SSL"), RadDropDownList).SelectedValue
            valores(5) = CType(MyUserControl.FindControl("TxtCAT_CONF_RESPONSABLE"), RadTextBox).Text
            valores(6) = CType(MyUserControl.FindControl("DdlSalidaGestion"), RadDropDownList).SelectedValue
            valores(7) = CType(MyUserControl.FindControl("LblId"), RadLabel).Text

            If valores(0) = "" Then
                Aviso("Escriba el correo")
            ElseIf valores(1) = "" Then
                Aviso("Escriba el password")
            ElseIf valores(2) = "" Then
                Aviso("Escriba el host")
            ElseIf valores(3) = "" Then
                Aviso("Escriba el puerto")
            ElseIf valores(4) = "-1" Then
                Aviso("Seleccione una opcion para SSL")
            ElseIf valores(5) = "" Then
                Aviso("Escriba el nombre del responsable")
            ElseIf valores(6) = "-1" Then
                Aviso("Seleccione una opcion para Salida Gestion")
            Else
                Try
                    Dim SSCommand1 As New SqlCommand
                    SSCommand1.CommandText = "SP_CONFIGURAR_MAIL"
                    SSCommand1.CommandType = CommandType.StoredProcedure
                    SSCommand1.Parameters.Add("@V_CAT_CONF_USER0", SqlDbType.NVarChar).Value = valores(7)
                    SSCommand1.Parameters.Add("@V_CAT_CONF_USER", SqlDbType.NVarChar).Value = valores(0)
                    SSCommand1.Parameters.Add("@V_CAT_CONF_PWD", SqlDbType.NVarChar).Value = valores(1)
                    SSCommand1.Parameters.Add("@V_CAT_CONF_HOST", SqlDbType.NVarChar).Value = valores(2)
                    SSCommand1.Parameters.Add("@V_CAT_CONF_PUERTO", SqlDbType.NVarChar).Value = valores(3)
                    SSCommand1.Parameters.Add("@V_CAT_CONF_SSL", SqlDbType.NVarChar).Value = valores(4)
                    SSCommand1.Parameters.Add("@V_CAT_CONF_MAIL", SqlDbType.NVarChar).Value = "CONF"
                    SSCommand1.Parameters.Add("@V_CAT_CONF_RESPONSABLE", SqlDbType.NVarChar).Value = valores(5)
                    SSCommand1.Parameters.Add("@V_CAT_CONF_SALIDAGESTION", SqlDbType.NVarChar).Value = valores(6)
                    SSCommand1.Parameters.Add("@v_bandera", SqlDbType.Decimal).Value = 2
                    Ejecuta_Procedure(SSCommand1)

                    Aviso("Correo Actualizado")
                Catch exxs As Exception
                    Aviso("No su pudo Actualizar, Error: " & exxs.Message)
                End Try
            End If
        ElseIf e.CommandName = "PerformInsert" Then

            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(9) As String


            valores(0) = CType(MyUserControl.FindControl("TxtCAT_CONF_USER"), RadTextBox).Text
            valores(1) = CType(MyUserControl.FindControl("TxtCAT_CONF_PWD"), RadTextBox).Text
            valores(2) = CType(MyUserControl.FindControl("TxtCAT_CONF_HOST"), RadTextBox).Text
            valores(3) = CType(MyUserControl.FindControl("TxtCAT_CONF_PUERTO"), RadTextBox).Text
            valores(4) = CType(MyUserControl.FindControl("DdlCAT_CONF_SSL"), RadDropDownList).SelectedValue
            valores(5) = CType(MyUserControl.FindControl("TxtCAT_CONF_RESPONSABLE"), RadTextBox).Text
            valores(6) = CType(MyUserControl.FindControl("DdlSalidaGestion"), RadDropDownList).SelectedValue
            valores(7) = CType(MyUserControl.FindControl("LblId"), RadLabel).Text

            If valores(0) = "" Then
                Aviso("Escriba el correo")
            ElseIf valores(1) = "" Then
                Aviso("Escriba el password")
            ElseIf valores(2) = "" Then
                Aviso("Escriba el host")
            ElseIf valores(3) = "" Then
                Aviso("Escriba el puerto")
            ElseIf valores(4) = "-1" Then
                Aviso("Seleccione una opcion para SSL")
            ElseIf valores(5) = "" Then
                Aviso("Escriba el nombre del responsable")
            ElseIf valores(6) = "-1" Then
                Aviso("Seleccione una opcion para Salida Gestion")
            Else
                Try
                    Dim SSCommand1 As New SqlCommand
                    SSCommand1.CommandText = "SP_CONFIGURAR_MAIL"
                    SSCommand1.CommandType = CommandType.StoredProcedure
                    SSCommand1.Parameters.Add("@V_CAT_CONF_USER0", SqlDbType.NVarChar).Value = valores(7)
                    SSCommand1.Parameters.Add("@V_CAT_CONF_USER", SqlDbType.NVarChar).Value = valores(0)
                    SSCommand1.Parameters.Add("@V_CAT_CONF_PWD", SqlDbType.NVarChar).Value = valores(1)
                    SSCommand1.Parameters.Add("@V_CAT_CONF_HOST", SqlDbType.NVarChar).Value = valores(2)
                    SSCommand1.Parameters.Add("@V_CAT_CONF_PUERTO", SqlDbType.NVarChar).Value = valores(3)
                    SSCommand1.Parameters.Add("@V_CAT_CONF_SSL", SqlDbType.NVarChar).Value = valores(4)
                    SSCommand1.Parameters.Add("@V_CAT_CONF_MAIL", SqlDbType.NVarChar).Value = "CONF"
                    SSCommand1.Parameters.Add("@V_CAT_CONF_RESPONSABLE", SqlDbType.NVarChar).Value = valores(5)
                    SSCommand1.Parameters.Add("@V_CAT_CONF_SALIDAGESTION", SqlDbType.NVarChar).Value = valores(6)
                    SSCommand1.Parameters.Add("@v_bandera", SqlDbType.Decimal).Value = 1
                    Ejecuta_Procedure(SSCommand1)

                    Aviso("Correo Creado")
                Catch exxs As Exception
                    Aviso("No su pudo crear, Error: " & exxs.Message)
                End Try
            End If
        ElseIf e.CommandName = "Delete" Then

            Dim ID As String = (CType(e.Item, GridDataItem)).OwnerTableView.DataKeyValues(e.Item.ItemIndex)("ID").ToString
            Dim Tipo As String = e.Item.Cells.Item(4).Text
            Try
                Dim SSCommand1 As New SqlCommand
                SSCommand1.CommandText = "SP_CONFIGURAR_MAIL"
                SSCommand1.CommandType = CommandType.StoredProcedure
                SSCommand1.Parameters.Add("@V_CAT_CONF_USER", SqlDbType.NVarChar).Value = e.Item.Cells.Item(3).Text
                SSCommand1.Parameters.Add("@V_CAT_CONF_PWD", SqlDbType.NVarChar).Value = e.Item.Cells.Item(4).Text
                SSCommand1.Parameters.Add("@V_CAT_CONF_HOST", SqlDbType.NVarChar).Value = e.Item.Cells.Item(5).Text
                SSCommand1.Parameters.Add("@V_CAT_CONF_PUERTO", SqlDbType.NVarChar).Value = e.Item.Cells.Item(6).Text
                SSCommand1.Parameters.Add("@V_CAT_CONF_SSL", SqlDbType.NVarChar).Value = e.Item.Cells.Item(7).Text
                SSCommand1.Parameters.Add("@V_CAT_CONF_MAIL", SqlDbType.NVarChar).Value = ""
                SSCommand1.Parameters.Add("@V_CAT_CONF_RESPONSABLE", SqlDbType.NVarChar).Value = e.Item.Cells.Item(9).Text
                SSCommand1.Parameters.Add("@v_bandera", SqlDbType.Decimal).Value = 3
                Ejecuta_Procedure(SSCommand1)
                Aviso("Correo Eliminado")

            Catch esx As Exception
                Aviso("Imposible eliminar, Error: " & esx.Message)
            End Try
        End If

    End Sub
End Class
