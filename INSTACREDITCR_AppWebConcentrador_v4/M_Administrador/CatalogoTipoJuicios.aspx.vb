Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports Funciones
Imports Telerik.Web.UI
'Imports Db
Partial Class CatalogoTipoJuicios
    Inherits System.Web.UI.Page

    Public Property tmpUSUARIO As IDictionary
        Get
            Return CType(Session("USUARIO"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("USUARIO") = value
        End Set
    End Property
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim Usr As String = tmpUSUARIO("CAT_LO_USUARIO")
        Catch ex As Exception
            OffLine("")
            AUDITORIA("", "Administrador", "Tipo De Juicios", "", "Page_Load", "", Request.ServerVariables("REMOTE_ADDR"), Request.ServerVariables("REMOTE_HOST"))
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/SesionExpirada.aspx")
        End Try
        Try
            If Not IsPostBack Then

                'If CType(Session("USUARIOADMIN"), USUARIO).CAT_LO_MADMINISTRADOR.ToString.Substring(10, 1) = 0 Then
                '    OffLine("")
                '    Session.Clear()
                '    Session.Abandon()
                '    Response.Redirect("~/SesionExpirada.aspx")
                'End If

            End If
        Catch ex As Exception
            SendMail("Page_Load", ex, "", "", "")
        End Try
    End Sub

    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Administrador", "CatalogoTipoJuicios.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub
    Protected Sub Aviso(ByVal MSJ As String)
        RadAviso.RadAlert(MSJ, 440, 155, "AVISO", Nothing)
    End Sub
    Protected Sub Confirma(ByVal MSJ As String)
        RadAviso.RadConfirm(MSJ, "confirmCallbackFn", 440, 155, Nothing, "Alerta")
    End Sub

    Public Function GetDataTable() As DataTable
        Dim table1 As New DataTable

        table1 = Class_TipoDeJuicios.LlenarElementos("", "", "", "", 0)
        Return table1
    End Function

    Public Sub RGVTipoJuicio_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RGVTipoJuicio.NeedDataSource
        Me.RGVTipoJuicio.DataSource = GetDataTable()
    End Sub

    Private Sub RGVTipoJuicio_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RGVTipoJuicio.ItemCommand
        If e.CommandName = "Update" Then
            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(3) As String


            valores(0) = CType(MyUserControl.FindControl("LblCAT_TJ_ID"), RadLabel).Text
            valores(1) = CType(MyUserControl.FindControl("TxtCAT_TJ_TIPO"), RadTextBox).Text
            valores(2) = CType(MyUserControl.FindControl("LblCAT_TJ_TIPO0"), RadLabel).Text

            If valores(1) = "" Then
                Aviso("Capture Un Tipo Valido")

            Else

                Try


                    'Dim DtsEtapas As DataSet = Class_TipoDeJuicios.LlenarElementos(valores(1), "", "", "", 1)
                    'If DtsEtapas.Tables(0).Rows(0).Item("CUANTOS") > 0 Then
                    '    Aviso("El Tipo De Juicio Ya Existe, Valide")
                    'Else
                    '    Class_TipoDeJuicios.LlenarElementos(valores(1), valores(0), valores(2), "", 3)
                    '    Aviso("Tipo De Juicio Modificado")

                    'End If


                Catch ex As Exception

                    Aviso("No Fue Posible Actualizar. Razon: " + ex.Message)
                    e.Canceled = True
                End Try
            End If
        ElseIf e.CommandName = "PerformInsert" Then
            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(3) As String


            valores(0) = CType(MyUserControl.FindControl("LblCAT_TJ_ID"), RadLabel).Text
            valores(1) = CType(MyUserControl.FindControl("TxtCAT_TJ_TIPO"), RadTextBox).Text
            valores(2) = CType(MyUserControl.FindControl("LblCAT_TJ_TIPO0"), RadLabel).Text

            If valores(1) = "" Then
                Aviso("Capture un Tipo valido")

            Else

                'Try
                '    Dim DtsEtapas As DataSet = Class_TipoDeJuicios.LlenarElementos(valores(1), "", "", "", 1)
                '    If DtsEtapas.Tables(0).Rows(0).Item("CUANTOS") > 0 Then
                '        Aviso("El Tipo De Juicio Ya Existe, Valide")
                '    Else
                '        Class_TipoDeJuicios.LlenarElementos(valores(1), "", "", "", 2)
                '        Aviso("Tipo De Juicio Agregado"
                '    )
                '    End If
                'Catch ex As Exception
                '    Aviso("No Fue Posible Insertar El  Usuario. Razon: " + ex.Message)
                'End Try


            End If

        ElseIf e.CommandName = "Delete" Then

            Dim ID As String = (CType(e.Item, GridDataItem)).OwnerTableView.DataKeyValues(e.Item.ItemIndex)("Id").ToString
            Dim Tipo As String = e.Item.Cells.Item(4).Text

            Class_TipoDeJuicios.LlenarElementos(ID, Tipo, "", "", 4)
            Aviso("Gasto Eliminado")



        End If
    End Sub
End Class

