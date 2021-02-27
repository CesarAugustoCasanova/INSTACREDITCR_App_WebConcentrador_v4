Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports Funciones
Imports Telerik.Web.UI
'Imports Db

Partial Class CatalogoGastos
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim Usr As String = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
        Catch ex As Exception

            AUDITORIA("", "Administrador", "Gastos", "", "Page_Load", "", Request.ServerVariables("REMOTE_ADDR"), Request.ServerVariables("REMOTE_HOST"))
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/SesionExpirada.aspx")
        End Try
        Try
            If Not IsPostBack Then

                If CType(Session("USUARIOADMIN"), USUARIO).CAT_LO_MADMINISTRADOR.ToString.Substring(9, 1) = 0 Then

                    Session.Clear()
                    Session.Abandon()
                    Response.Redirect("~/SesionExpirada.aspx")
                End If
                Llenar(0)
            End If
        Catch ex As Exception
            SendMail("Page_Load", ex, "", "", "")
        End Try
    End Sub

    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Administrador", "CatalogoGastos.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub

    Protected Function Llenar(ByVal V_Bandera As Integer) As DataSet
        Dim DtsEtapas As DataSet
        If V_Bandera = 0 Then
            Dim TmpUsr As USUARIO = CType(Session("USUARIOADMIN"), USUARIO)
            DtsEtapas = Class_GastosJudiciales.LlenarElementos("", "", "", "", 0)

        End If
        Return DtsEtapas
    End Function

    Protected Sub BtnAceptarConfirmacion_Click(sender As Object, e As EventArgs) Handles BtnAceptarConfirmacion.Click


    End Sub
    Protected Sub Aviso(ByVal MSJ As String)
        RadAviso.RadAlert(MSJ, 440, 155, "AVISO", Nothing)
    End Sub
    Protected Sub Confirma(ByVal MSJ As String)
        RadAviso.RadConfirm(MSJ, "confirmCallbackFn", 440, 155, Nothing, "Alerta")
    End Sub

    Public Sub RGVGastos_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RGVGastos.NeedDataSource
        Me.RGVGastos.DataSource = GetDataTable()
    End Sub
    Public Function GetDataTable() As DataTable
        Dim table1 As New DataTable

        table1 = Llenar(0).Tables(0)
        Return table1
    End Function

    Private Sub RGVGastos_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RGVGastos.ItemCommand

        If e.CommandName = "Update" Then
            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(3) As String


            valores(0) = CType(MyUserControl.FindControl("LblCAT_GA_ID"), RadLabel).Text
            valores(1) = CType(MyUserControl.FindControl("TxtCAT_GA_GASTO"), RadTextBox).Text
            valores(2) = CType(MyUserControl.FindControl("TxtCAT_GA_MONTO"), RadNumericTextBox).Text

            If valores(1) = "" Then
                Aviso("Capture un Gasto valido")
            ElseIf valores(2) = "" Then
                Aviso("Capture un Monto valido")
            Else

                Try


                    Class_GastosJudiciales.LlenarElementos(valores(1), valores(2), valores(0), "", 3)
                    Aviso("Gasto Agregado")


                Catch ex As Exception

                    Aviso("No Fue Posible Actualizar. Razon: " + ex.Message)
                    e.Canceled = True
                End Try
            End If
        ElseIf e.CommandName = "PerformInsert" Then
            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(3) As String


            valores(0) = CType(MyUserControl.FindControl("LblCAT_GA_ID"), RadLabel).Text
            valores(1) = CType(MyUserControl.FindControl("TxtCAT_GA_GASTO"), RadTextBox).Text
            valores(2) = CType(MyUserControl.FindControl("TxtCAT_GA_MONTO"), RadNumericTextBox).Text
            If valores(1) = "" Then
                Aviso("Capture un Gasto valido")
            ElseIf valores(2) = "" Then
                Aviso("Capture un Monto valido")
            Else

                Try
                    Dim DtsEtapas As DataSet = Class_GastosJudiciales.LlenarElementos(valores(1), valores(2), "", "", 1)
                    If DtsEtapas.Tables(0).Rows(0).Item("CUANTOS") > 0 Then
                        Aviso("El Gasto Ya Existe, Valide")
                    Else
                        Class_GastosJudiciales.LlenarElementos(valores(1), valores(2), "", "", 2)
                        Aviso("Gasto Modificado")
                    End If
                Catch ex As Exception
                    Aviso("No Fue Posible Insertar El  Usuario. Razon: " + ex.Message)
                End Try


            End If

        ElseIf e.CommandName = "Delete" Then

            Dim ID As String = (CType(e.Item, GridDataItem)).OwnerTableView.DataKeyValues(e.Item.ItemIndex)("Id").ToString
            Dim Tipo As String = e.Item.Cells.Item(4).Text
            Class_GastosJudiciales.LlenarElementos(ID, Tipo, "", "", 4)
            Aviso("Gasto Eliminado")



        End If

    End Sub
End Class

