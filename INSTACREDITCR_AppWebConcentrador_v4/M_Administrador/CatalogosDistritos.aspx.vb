Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Diagnostics
Imports System.IO
Imports System.Web.Services
Imports Db
Imports Telerik.Web.UI

Partial Class M_Administrador_CatalogosDistritos
    Inherits System.Web.UI.Page
    Public Property tmpUSUARIO As IDictionary
        Get
            Return CType(Session("USUARIO"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("USUARIO") = value
        End Set
    End Property

    Private Sub llenarCombo(combo As RadComboBox, modulo As Integer, perfil As Integer)
        combo.Items.Clear()
        Dim dt As DataTable = SP.ADD_PERFILES(2, "", "", "", "", "", "", "", "", perfil, modulo)
        For Each row As DataRow In dt.Rows()
            Dim item As RadComboBoxItem = New RadComboBoxItem
            item.Text = row("PERMISO")
            item.Value = row("PERMISO")
            If perfil <> 0 Then
                item.Checked = IIf(row("VALOR") = "1", True, False)
            End If
            combo.Items.Add(item)
        Next
    End Sub
    Private Sub RGDistritos_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RGDistritos.ItemCommand
        Session("Editar") = False
        Session("Borrar") = False
        Dim cods As String = ""
        Dim tab As DataTable = Nothing
        If e.CommandName = "PerformInsert" Then
            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim Nombre As String = CType(MyUserControl.FindControl("TxtNombre"), RadTextBox).Text
            Dim CPI As String = CType(MyUserControl.FindControl("TxtCPI"), RadMaskedTextBox).Text
            Dim CPF As String = CType(MyUserControl.FindControl("TxtCPF"), RadMaskedTextBox).Text
            Dim prenumero As String = CType(MyUserControl.FindControl("TxtNumPlaza"), RadNumericTextBox).Text
            '   Dim Gerente As String = CType(MyUserControl.FindControl("txtGerente"), RadTextBox).Text
            '  Dim Supervisor As String = CType(MyUserControl.FindControl("txtSupervisor"), RadTextBox).Text
            Dim Region As String = CType(MyUserControl.FindControl("txtRegion"), RadTextBox).Text
            Dim Regional As String = CType(MyUserControl.FindControl("txtRegional"), RadTextBox).Text
            Dim JefePlaza As String = CType(MyUserControl.FindControl("txtJefePlaza"), RadTextBox).Text
            Dim Gestor As String = CType(MyUserControl.FindControl("txtGestor"), RadTextBox).Text
            Dim Auxiliar As String = CType(MyUserControl.FindControl("txtAuxiliar"), RadTextBox).Text
            'Dim Distrito As String = CType(MyUserControl.FindControl("txtDistrito"), RadTextBox).Text
            Dim Usuario As String = CType(MyUserControl.FindControl("txtUsuario"), RadTextBox).Text
            Dim ZONA As String = CType(MyUserControl.FindControl("txtZona"), RadTextBox).Text
            Dim numero As Integer
            If prenumero = "" Then
                numero = -1
            Else
                numero = Integer.Parse(prenumero)
            End If
            Dim codigossueltos As GridTableView = CType(MyUserControl.FindControl("RGCodigos"), RadGrid).MasterTableView

            If codigossueltos.Items.Count = 0 Then
                If Nombre = "" Then
                    showModal(RadNotification1, "deny", "Error", "Ingrese Nombre de plaza")
                    e.Canceled = True
                ElseIf numero = -1 Then
                    showModal(RadNotification1, "deny", "Error", "Ingrese Numero de plaza")
                    e.Canceled = True
                ElseIf CPI = "" And CPF = "" Then
                    showModal(RadNotification1, "deny", "Error", "Ingrese codigos")
                    e.Canceled = True
                ElseIf CPI = "" Then
                    showModal(RadNotification1, "deny", "Error", "Ingrese codigo Inicial")
                    e.Canceled = True
                ElseIf CPF = "" Then
                    showModal(RadNotification1, "deny", "Error", "Ingrese codigo Final")
                    e.Canceled = True
                ElseIf CType(CPI, Integer) > CType(CPF, Integer) Then
                    e.Canceled = True
                    showModal(RadNotification1, "deny", "Error", "El codigo postal FINAL no puede ser MENOR al INICIAL")
                Else
                    tab = SP.DISTRITOSINSERT(0, CPI, CPF, Nombre, numero, 0, Region, Regional, JefePlaza, Gestor, Auxiliar, Usuario, ZONA, 1)
                    If tab.Rows(0).Item("RESULTADO") = "OK" Then
                        RGDistritos.Rebind()
                        showModal(RadNotification1, "ok", "Exito", "Codigos Insertados")
                    Else
                        showModal(RadNotification1, "warning", "Codigo Repetido", "El CP " & tab.Rows(0).Item("RESULTADO") & "Ya pertenece a otra plaza. NINGUN CP Insertado")
                    End If
                End If
            Else
                For Each roe As GridDataItem In codigossueltos.Items
                    cods += roe.Cells(3).Text & ","
                Next
                cods = cods.Substring(0, cods.Length - 1)
                tab = SP.DISTRITOSINSERT(0, cods, "VARIOS", Nombre, numero, 0, Region, Regional, JefePlaza, Gestor, Auxiliar, Usuario, ZONA, 1)
                If tab.Rows(0).Item("RESULTADO") = "OK" Then
                    RGDistritos.Rebind()
                    showModal(RadNotification1, "ok", "Exito", "Codigos Insertados")
                Else
                    showModal(RadNotification1, "warning", "Codigo Repetido", "El CP " & tab.Rows(0).Item("RESULTADO") & "Ya pertenece a otra plaza. NINGUN CP Insertado")
                End If

            End If
        ElseIf e.CommandName = "Update" Then
            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim Nombre As String = CType(MyUserControl.FindControl("TxtNombre"), RadTextBox).Text
            Dim CPI As String = CType(MyUserControl.FindControl("TxtCPI"), RadMaskedTextBox).Text
            Dim CPF As String = CType(MyUserControl.FindControl("TxtCPF"), RadMaskedTextBox).Text
            Dim prenumero As String = CType(MyUserControl.FindControl("TxtNumPlaza"), RadNumericTextBox).Text
            'Dim Gerente As String = CType(MyUserControl.FindControl("txtGerente"), RadTextBox).Text
            'Dim Supervisor As String = CType(MyUserControl.FindControl("txtSupervisor"), RadTextBox).Text
            Dim Region As String = CType(MyUserControl.FindControl("txtRegion"), RadTextBox).Text
            Dim Regional As String = CType(MyUserControl.FindControl("txtRegional"), RadTextBox).Text
            Dim JefePlaza As String = CType(MyUserControl.FindControl("txtJefePlaza"), RadTextBox).Text
            Dim Gestor As String = CType(MyUserControl.FindControl("txtGestor"), RadTextBox).Text
            Dim Auxiliar As String = CType(MyUserControl.FindControl("txtAuxiliar"), RadTextBox).Text
            ' Dim Distrito As String = CType(MyUserControl.FindControl("txtDistrito"), RadTextBox).Text
            Dim Usuario As String = CType(MyUserControl.FindControl("txtUsuario"), RadTextBox).Text
            Dim ZONA As String = CType(MyUserControl.FindControl("txtZona"), RadTextBox).Text
            Dim numero As Integer
            If prenumero = "" Then
                numero = -1
            Else
                numero = Integer.Parse(prenumero)
            End If
            Dim codigossueltos As GridTableView = CType(MyUserControl.FindControl("RGCodigos"), RadGrid).MasterTableView
            Dim conservar As Integer = CType(MyUserControl.FindControl("CBConservar"), RadCheckBox).Checked
            If codigossueltos.Items.Count = 0 Or conservar <> 0 Then
                If Nombre = "" Then
                    showModal(RadNotification1, "deny", "Error", "Ingrese Nombre de plaza")
                    e.Canceled = True
                ElseIf numero = -1 Then
                    showModal(RadNotification1, "deny", "Error", "Ingrese Numero de plaza")
                    e.Canceled = True
                ElseIf CPI = "" And CPF = "" Then
                    showModal(RadNotification1, "deny", "Error", "Ingrese codigos")
                    e.Canceled = True
                ElseIf CPI = "" Then
                    showModal(RadNotification1, "deny", "Error", "Ingrese codigo Inicial")
                    e.Canceled = True
                ElseIf CPI.Length <> 5 Then
                    showModal(RadNotification1, "deny", "Error", "Ingrese codigo Inicial de 5 digitos")
                    e.Canceled = True
                ElseIf CPF.Length <> 5 Then
                    showModal(RadNotification1, "deny", "Error", "Ingrese codigo Final de 5 digitos")
                    e.Canceled = True
                ElseIf CPF = "" Then
                    showModal(RadNotification1, "deny", "Error", "Ingrese codigo Final")
                    e.Canceled = True
                ElseIf CType(CPI, Integer) > CType(CPF, Integer) Then
                    e.Canceled = True
                    showModal(RadNotification1, "deny", "Error", "El codigo postal FINAL debe ser MAYOR al INICIAL")
                Else
                    tab = SP.DISTRITOSINSERT(sender.items(e.Item.ItemIndex).cells(4).text, CPI, CPF, Nombre, numero, conservar, Region, Regional, JefePlaza, Gestor, Auxiliar, Usuario, ZONA, 2)
                    If tab.Rows(0).Item("RESULTADO") = "OK" Then
                        RGDistritos.Rebind()
                        showModal(RadNotification1, "ok", "Exito", "Codigos Insertados")
                    Else
                        showModal(RadNotification1, "warning", "Codigo Repetido", "El CP " & tab.Rows(0).Item("RESULTADO") & "Ya pertenece a otra plaza. NINGUN CP Insertado")
                    End If

                End If
            Else
                For Each roe As GridDataItem In codigossueltos.Items
                    cods += roe.Cells(3).Text & ","
                Next
                cods = cods.Substring(0, cods.Length - 1)
                tab = SP.DISTRITOSINSERT(sender.items(e.Item.ItemIndex).cells(4).text, cods, "VARIOS", Nombre, numero, conservar, Region, Regional, JefePlaza, Gestor, Auxiliar, Usuario, ZONA, 2)
                If tab.Rows(0).Item("RESULTADO") = "OK" Then
                    RGDistritos.Rebind()
                    showModal(RadNotification1, "ok", "Exito", "Codigos Insertados")
                Else
                    showModal(RadNotification1, "warning", "Codigo Repetido", "El CP " & tab.Rows(0).Item("RESULTADO") & "Ya pertenece a otra plaza.. NINGUN CP Insertado")
                End If
            End If

            RGDistritos.Rebind()
        ElseIf e.CommandName = "Edit" Then
            Session("Editar") = True
            Session("Borrar") = True
        ElseIf e.CommandName = "Delete" Then
            SP.DISTRITOS(e.Item.Cells(4).Text, "", "", "", 0, 0, 3)
            RGDistritos.Rebind()
        ElseIf e.CommandName = "InitInsert" Then
            Session("Borrar") = True

        End If
    End Sub

    Private Sub RGDistritos_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RGDistritos.NeedDataSource
        RGDistritos.DataSource = SP.DISTRITOS(0, "", "", "", 0, 0, 0)
    End Sub
    ''' <summary>
    ''' Establece los parametros para mostrar una notificacion
    ''' </summary>
    ''' <param name="Notificacion">Objeto RadNotification de Telerik</param>
    ''' <param name="icono">info - delete - deny - edit - ok - warning - none</param>
    ''' <param name="titulo">título de la notificación</param>
    ''' <param name="msg">mensaje de la notificación</param>
    Public Shared Sub showModal(ByRef Notificacion As Object, ByVal icono As String, ByVal titulo As String, ByVal msg As String)
        Dim radnot As RadNotification = TryCast(Notificacion, RadNotification)
        radnot.TitleIcon = icono
        radnot.ContentIcon = icono
        radnot.Title = titulo
        radnot.Text = msg
        radnot.Show()
    End Sub
    <WebMethod>
    Public Shared Function GetResults(context As SearchBoxContext) As SearchBoxItemData()
        Dim data As DataTable = SP.DISTRITOS(0, context.Text.ToUpper, "", "", 0, 0, 5)
        Dim result As New List(Of SearchBoxItemData)()
        If context.Text.Length >= 3 Then
            For i As Integer = 0 To data.Rows.Count - 1
                Dim itemData As New SearchBoxItemData()
                itemData.Text = data.Rows(i)("CODIGO").ToString()
                itemData.Value = data.Rows(i)("CODIGO").ToString()
                result.Add(itemData)
            Next
        End If
        Return result.ToArray()
    End Function
End Class
