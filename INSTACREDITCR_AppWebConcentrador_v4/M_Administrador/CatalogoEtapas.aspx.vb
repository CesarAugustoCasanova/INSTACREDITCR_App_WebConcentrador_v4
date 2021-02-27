Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports Funciones
Imports Telerik.Web.UI
'Imports Db
Partial Class CatalogoEtapas
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim Usr As String = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
        Catch ex As Exception
            OffLine("")
            AUDITORIA("", "Administrador", "Catalogo Etapas", "", "Page_Load", "", Request.ServerVariables("REMOTE_ADDR"), Request.ServerVariables("REMOTE_HOST"))
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/SesionExpirada.aspx")
        End Try
        Try
            If Not IsPostBack Then
                If CType(Session("USUARIOADMIN"), USUARIO).CAT_LO_MADMINISTRADOR.ToString.Substring(11, 1) = 0 Then
                    OffLine("")
                    Session.Clear()
                    Session.Abandon()
                    Response.Redirect("~/SesionExpirada.aspx")
                End If

            End If
        Catch ex As Exception
            SendMail("Page_Load", ex, "", "", "")
        End Try
    End Sub

    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Administrador", "CatalogoEtapaS.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub
    Protected Sub Aviso(ByVal MSJ As String)
        RadAviso.RadAlert(MSJ, 440, 155, "AVISO", Nothing)
    End Sub
    Protected Sub Confirma(ByVal MSJ As String)
        RadAviso.RadConfirm(MSJ, "confirmCallbackFn", 440, 155, Nothing, "Alerta")
    End Sub
    Public Function GetDataTable() As DataTable
        Dim table1 As New DataTable

        table1 = Class_EtapasJudiciales.LlenarElementos("", "", "", "", 0).Tables(0)
        Return table1
    End Function
    Public Sub RGVEtapas_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RGVEtapas.NeedDataSource
        RGVEtapas.DataSource = GetDataTable()
    End Sub

    Private Sub RGVEtapas_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RGVEtapas.ItemCommand
        If e.CommandName = "Update" Then
            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(3) As String


            valores(0) = CType(MyUserControl.FindControl("LblCAT_ET_ID"), RadLabel).Text
            valores(1) = CType(MyUserControl.FindControl("TxtCAT_ET_ETAPA"), RadTextBox).Text
            valores(2) = CType(MyUserControl.FindControl("LblCAT_ET_ETAPA0"), RadLabel).Text

            If valores(1) = "" Then
                Aviso("Capture una Etapa valido")
            ElseIf valores(2) = "" Then


                Try
                    Dim DtsEtapas As DataSet = Class_EtapasJudiciales.LlenarElementos(valores(1), "", "", "", 1)
                    If DtsEtapas.Tables(0).Rows(0).Item("CUANTOS") > 0 Then
                        Aviso("La Etapa Judicial Ya Existe, Valide")
                    Else
                        Class_EtapasJudiciales.LlenarElementos(valores(1), valores(0), valores(2), "", 3)
                        Aviso("Etapa Judicial Modificada")

                    End If

                Catch ex As Exception

                    Aviso("No Fue Posible Actualizar. Razon: " + ex.Message)
                    e.Canceled = True
                End Try
            End If
        ElseIf e.CommandName = "PerformInsert" Then
            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(3) As String


            valores(0) = CType(MyUserControl.FindControl("LblCAT_ET_ID"), RadLabel).Text
            valores(1) = CType(MyUserControl.FindControl("TxtCAT_ET_ETAPA"), RadTextBox).Text
            valores(2) = CType(MyUserControl.FindControl("LblCAT_ET_ETAPA0"), RadLabel).Text
            If valores(1) = "" Then
                Aviso("Capture una Etapa valido")

            Else

                Try
                    Dim DtsEtapas As DataSet = Class_EtapasJudiciales.LlenarElementos(valores(1), "", "", "", 1)
                    If DtsEtapas.Tables(0).Rows(0).Item("CUANTOS") > 0 Then
                        Aviso("La Etapa Judicial Ya Existe, Valide")
                    Else
                        Class_EtapasJudiciales.LlenarElementos(valores(1), "", "", "", 2)
                        Aviso("Etapa Judicial Agregada")

                    End If
                Catch ex As Exception
                    Aviso("No Fue Posible Insertar El  Usuario. Razon: " + ex.Message)
                End Try


            End If

        ElseIf e.CommandName = "Delete" Then

            Dim ID As String = (CType(e.Item, GridDataItem)).OwnerTableView.DataKeyValues(e.Item.ItemIndex)("Id").ToString
            Dim Tipo As String = e.Item.Cells.Item(4).Text
            Class_EtapasJudiciales.LlenarElementos(ID, Tipo, "", "", 4)
            Aviso("Etapa Judicial Eliminada")
        End If


    End Sub
End Class

