Imports System.Data
Imports System.Data.SqlClient
Imports Db
Imports Funciones
Imports Telerik.Web.UI

Partial Class Administrador_CatalogoAgencias
    Inherits System.Web.UI.Page
    Public Property TmpUSUARIO As IDictionary
        Get
            Return CType(Session("USUARIO"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("USUARIO") = value
        End Set
    End Property
    Public Property TmpPermisos As IDictionary
        Get
            Return CType(Session("Permisos"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("Permisos") = value
        End Set
    End Property
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If TmpUSUARIO Is Nothing Then
            OffLine(HidenUrs.Value)
            AUDITORIA(HidenUrs.Value, "Administrador", "CatalogoAgencias", "", "Page_Load", "", Request.ServerVariables("REMOTE_ADDR"), Request.ServerVariables("REMOTE_HOST"))
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/SesionExpirada.aspx")
        Else
            Try
                HidenUrs.Value = TmpUSUARIO("CAT_LO_USUARIO")
                If Not IsPostBack Then
                    RGVAgencias.MasterTableView.EditMode = CType([Enum].Parse(GetType(GridEditMode), "EditForms"), GridEditMode)
                    SP.Add_AGENCIAS(0, "", "", "", "", "", 0)
                End If
            Catch ex As Exception
                SendMail("Page_Load", ex, "", "", HidenUrs.Value)
            End Try
        End If


    End Sub
    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Administrador", "CatalogoAgencias.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub


    Protected Sub Aviso(ByVal MSJ As String)
        RadAviso.RadAlert(MSJ.Replace(Chr(10), "").Replace(Chr(13), "").Replace("'", "").Replace("""", ""), 440, 155, "AVISO", Nothing)
    End Sub
    Private Sub RGVAgencias_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RGVAgencias.ItemCommand
        If e.CommandName = "Update" Then
            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(4) As String

            valores(0) = CType(MyUserControl.FindControl("TxtCat_Ag_Nombre"), RadTextBox).Text
            valores(1) = CType(MyUserControl.FindControl("TxtCat_Ag_Usuario"), RadTextBox).Text
            valores(2) = CType(MyUserControl.FindControl("DdlCat_Ag_Estatus"), RadDropDownList).SelectedItem.Text
            valores(3) = CType(MyUserControl.FindControl("TxtCat_Ag_Motivo"), RadTextBox).Text

            If valores(0) = "" Then
                Aviso("Captura un nombre de usuario valido")
                e.Canceled = True
            ElseIf valores(1) = "" Then
                Aviso("Captura un usuario valido")
                e.Canceled = True
            ElseIf valores(2) = "Seleccione" Then
                Aviso("Seleccione un estatus")
                e.Canceled = True
            ElseIf valores(3) = "" And valores(2) = "Baja" Then
                Aviso("Captura un motivo valido")
                e.Canceled = True
            Else
                Dim changedRows As DataRow()
                changedRows = Me.Agencias.Select(" USUARIO = '" & editedItem.OwnerTableView.DataKeyValues(editedItem.ItemIndex)("USUARIO") & "'")
                Try
                    If (Not changedRows.Length = 1) Then
                        e.Canceled = True
                        Return
                    End If
                    Dim newValues As Hashtable = New Hashtable
                    newValues("Nombre") = valores(0)
                    newValues("Usuario") = valores(1)
                    newValues("Estatus") = valores(2)
                    newValues("Motivo") = valores(3)

                    changedRows(0).BeginEdit()
                    Dim entry As DictionaryEntry
                    For Each entry In newValues
                        changedRows(0)(CType(entry.Key, String)) = entry.Value
                    Next
                    changedRows(0).EndEdit()
                    Me.Agencias.AcceptChanges()

                    Aviso(SP.Add_AGENCIAS(2, valores(1), valores(0), valores(2), valores(3), valores(1), 2)(0)(0).ToString)
                Catch ex As Exception
                    changedRows(0).CancelEdit()
                    Aviso("No fue posible actualizar el usuario. Razon: " + ex.Message)
                    e.Canceled = True
                End Try
            End If
        ElseIf e.CommandName = "PerformInsert" Then
            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(1) As String

            valores(0) = CType(MyUserControl.FindControl("TxtCat_Ag_Nombre"), RadTextBox).Text


            If valores(0) = "" Then
                Aviso("Captura un nombre valido")
                e.Canceled = True

            Else
                Try

                    Dim existe As String = SP.Add_AGENCIAS(0, valores(0), valores(0), "", "", TmpUSUARIO("CAT_LO_USUARIO"), 4)(0)(0).ToString

                    If existe = "0" Then
                        Aviso(SP.Add_AGENCIAS(0, UCase(valores(0)), UCase(valores(0)), "Activa", "-", TmpUSUARIO("CAT_LO_USUARIO"), 1)(0)(0).ToString)
                    Else
                        Aviso("La agencia ya existe")
                        e.Canceled = True
                    End If
                Catch ex As Exception
                    Aviso("No fue posible crear la agencia.</br> Razon: " + ex.Message)
                    e.Canceled = True
                End Try

            End If
            Session("initInsert") = False
        ElseIf e.CommandName = "InitInsert" Then
            Session("initInsert") = True
        End If
    End Sub
    Public Function GetDataTable() As DataTable
        Dim table1 As New DataTable
        table1 = SP.Add_AGENCIAS(0, "", "", "", "", "", 0)
        Return table1
    End Function

    Public Sub RGVAgencias_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RGVAgencias.NeedDataSource
        Me.RGVAgencias.DataSource = Me.Agencias
        Me.Agencias.PrimaryKey = New DataColumn() {Me.Agencias.Columns("Usario")}
    End Sub

    Public ReadOnly Property Agencias() As DataTable
        Get
            Dim obj As Object = Me.Session("Usuarios")
            If (Not obj Is Nothing) Then
                Return CType(obj, DataTable)
            End If

            Dim myDataTable As DataTable = New DataTable
            myDataTable = GetDataTable()
            Me.Session("Agencias") = myDataTable
            Return myDataTable
        End Get
    End Property

End Class
