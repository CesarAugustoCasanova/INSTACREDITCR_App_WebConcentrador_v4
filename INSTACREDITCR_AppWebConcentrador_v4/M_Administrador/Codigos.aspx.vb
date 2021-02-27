Imports System.Data
Imports System.Data.SqlClient
Imports Db
Imports Funciones
Imports System.IO
Imports Telerik.Web.UI

Partial Class Codigos
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
            Response.Redirect("~/SesionExpirada.aspx")
        End If
        Try
            If Not IsPostBack Then
                HidenUrs.Value = TmpUSUARIO("CAT_LO_USUARIO")
            End If
        Catch ex As Exception
            SendMail("Page_Load", ex, "", "", HidenUrs.Value)
        End Try
    End Sub

    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        AUDITORIA(usr, "Administrador", "Codigos.aspx", Cuenta, evento, ex.Message, Captura, "")
    End Sub

    '-----------------------------------------------------------------------------------------------------------------
    'NUEVO DISEÑO CODIGOS
    '-----------------------------------------------------------------------------------------------------------------

    Private Sub gridCodAccion_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles gridCodAccion.NeedDataSource
        Try
            Dim SSCommand As New SqlCommand
            SSCommand.CommandText = "SP_ADD_CODIGOS"
            SSCommand.CommandType = CommandType.StoredProcedure
            SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 101

            Dim ds = Consulta_Procedure(SSCommand, "Catalogos")
            gridCodAccion.DataSource = ds
            ViewState("gridCodAccion") = ds
        Catch ex As Exception
            gridCodAccion.DataSource = Nothing
        End Try
    End Sub

    Private Sub gridResultados_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles gridResultados.NeedDataSource
        Try
            Dim SSCommand As New SqlCommand
            SSCommand.CommandText = "SP_ADD_CODIGOS"
            SSCommand.CommandType = CommandType.StoredProcedure
            SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 102

            Dim ds = Consulta_Procedure(SSCommand, "Catalogos")
            gridResultados.DataSource = ds
            ViewState("gridResultados") = ds
        Catch ex As Exception
            gridResultados.DataSource = Nothing
        End Try
    End Sub

    Private Sub gridNoPago_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles gridNoPago.NeedDataSource
        Try
            Dim SSCommand As New SqlCommand
            SSCommand.CommandText = "SP_ADD_CODIGOS"
            SSCommand.CommandType = CommandType.StoredProcedure
            SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 103

            Dim ds = Consulta_Procedure(SSCommand, "Catalogos")
            gridNoPago.DataSource = ds
            ViewState("gridNoPago") = ds
        Catch ex As Exception
            gridNoPago.DataSource = Nothing
        End Try
    End Sub

    Private Sub gridAsociacion_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles gridAsociacion.NeedDataSource
        Try
            Dim SSCommand As New SqlCommand
            SSCommand.CommandText = "SP_ADD_CODIGOS"
            SSCommand.CommandType = CommandType.StoredProcedure
            SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 104

            Dim ds = Consulta_Procedure(SSCommand, "Catalogos")
            gridAsociacion.DataSource = ds
            ViewState("gridAsociacion") = ds
        Catch ex As Exception
            gridAsociacion.DataSource = Nothing
        End Try
    End Sub
    Protected Sub grid_FilterCheckListItemsRequested(sender As Object, e As GridFilterCheckListItemsRequestedEventArgs)
        Dim DataField As String = TryCast(e.Column, IGridDataColumn).GetActiveDataField()

        Dim idSender = TryCast(sender, RadGrid).ID

        Dim dt As DataTable = ViewState(idSender)

        Dim ds = From rows As DataRow In dt
                 Select valor = rows(1)

        e.ListBox.DataSource = ds
        e.ListBox.DataBind()
    End Sub

    Private Function GetGridValuesGeneric(MyUserControl As UserControl) As Hashtable
        Dim newValues As New Hashtable
        For Each control As Object In MyUserControl.Controls
            If TryCast(control, RadLabel) IsNot Nothing Then
                Dim ctrl = TryCast(control, RadLabel)
                Try
                    newValues.Add(ctrl.ID, ctrl.Text)
                Catch ex As Exception
                    newValues.Add(ctrl.ID, "")

                End Try
            ElseIf TryCast(control, RadTextBox) IsNot Nothing Then
                Dim ctrl = TryCast(control, RadTextBox)
                Try
                    newValues.Add(ctrl.ID, ctrl.Text)
                Catch ex As Exception
                    newValues.Add(ctrl.ID, "")

                End Try
            End If
        Next
        Return newValues
    End Function

    Private Function ExistsCode_Generic(newValues As Hashtable, dt As DataTable) As Boolean
        Dim otherData = From rows As DataRow In dt
                        Where rows(0).ToString <> newValues("lblID").ToString

        Dim existCode = Aggregate rows In otherData
                            Into Cuantos = Count(rows(1).ToString.ToUpper = newValues("txtCod").ToString.ToUpper)

        Return (existCode > 0)
    End Function

    Private Function ExistsDesc_Generic(newValues As Hashtable, dt As DataTable) As Boolean
        Dim otherData = From rows As DataRow In dt
                        Where rows(0).ToString <> newValues("lblID").ToString

        Dim existDescription = Aggregate rows In otherData
                            Into Cuantos = Count(rows(2).ToString.ToUpper.Replace(" ", "") = newValues("txtDesc").ToString.ToUpper.Replace(" ", ""))
        Return (existDescription > 0)
    End Function

    Private Function IsCodeValid(code As String) As Boolean
        Dim rg = New Regex("(\w{2}(\w)?(\w)?)")
        Dim matches = rg.Match(code)
        Return (matches.Value = code)
    End Function

    Private Sub gridCodAccion_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles gridCodAccion.ItemCommand
        If RadGrid.UpdateCommandName = e.CommandName Or RadGrid.PerformInsertCommandName = e.CommandName Then
            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)

            Dim newValues As Hashtable = GetGridValuesGeneric(MyUserControl)
            newValues("lblID") = IIf(newValues("lblID") = "", -1, newValues("lblID"))
            newValues("txtCod") = newValues("txtCod").ToString.ToUpper
            ' newValues("txtAliasCod") = newValues("txtAliasCod").ToString.ToUpper

            'If Not IsCodeValid(newValues("txtCod")) Then
            '    e.Canceled = True
            '    showModal(AccionNotify, "deny", "Código Invalido", "El código de acción debe contener 2-4 caracteres alfanumericos sin caracteres especiales.")
            '    Return
            'Else
            If newValues("txtDesc") = "" Or newValues("txtDesc").ToString.Length < 4 Then
                e.Canceled = True
                showModal(AccionNotify, "deny", "Descripción Invalida", "La descripción del código de acción proporcionado debe contener mínimo 4 caracteres.")
                Return
            End If

            'Nos evitamos ir a la base a verificar si el codigo que estamos agregando existe
            Dim Existe = ExistsCode_Generic(newValues, ViewState("gridCodAccion"))

            If ExistsCode_Generic(newValues, ViewState("gridCodAccion")) Then
                e.Canceled = True
                showModal(AccionNotify, "deny", "Error", "El código de acción ya existe en el sistema. Válide e intente de nuevo.")
            ElseIf ExistsDesc_Generic(newValues, ViewState("gridCodAccion")) Then
                e.Canceled = True
                showModal(AccionNotify, "deny", "Error", "La descripción ya existe en el sistema. Válide e intente de nuevo.")
            Else
                SP.ADD_CODIGOS(V_Bandera:=105, V_Cat_Co_Accion:=newValues("txtCod"), V_Cat_Co_Adescripcion:=newValues("txtDesc"), V_Cat_Co_Id:=newValues("lblID"))
                showModal(AccionNotify, "ok", "Correcto", "Informacion actualizada correctamente.")
            End If

        ElseIf RadGrid.DeleteCommandName = e.CommandName Then
            Dim id = e.Item.Cells(3).Text
            Dim codigo = e.Item.Cells(4).Text

            SP.ADD_CODIGOS(V_Bandera:=109, V_Cat_Co_Id:=id)
            SP.ADD_CODIGOS(V_Bandera:=110, V_Cat_Co_Accion:=codigo)
            showModal(AccionNotify, "ok", "Correcto", "Informacion actualizada correctamente.")
        End If
    End Sub

    Private Sub gridNoPago_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles gridNoPago.ItemCommand
        If RadGrid.UpdateCommandName = e.CommandName Or RadGrid.PerformInsertCommandName = e.CommandName Then
            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)

            Dim newValues As Hashtable = GetGridValuesGeneric(MyUserControl)
            newValues("lblID") = IIf(newValues("lblID") = "", -1, newValues("lblID"))
            newValues("txtCod") = newValues("txtCod").ToString.ToUpper
            ' newValues("txtAliasCod") = newValues("txtAliasCod").ToString.ToUpper

            'If Not IsCodeValid(newValues("txtCod")) Then
            '    e.Canceled = True
            '    showModal(NoPagoNotify, "deny", "Código Invalido", "El código de acción debe contener 2-4 caracteres alfanumericos sin caracteres especiales.")
            '    Return
            'Else
            If newValues("txtDesc") = "" Or newValues("txtDesc").ToString.Length < 4 Then
                    e.Canceled = True
                    showModal(NoPagoNotify, "deny", "Descripción Invalida", "La descripción del código de acción proporcionado debe contener mínimo 4 caracteres.")
                    Return
                End If

                'Nos evitamos ir a la base a verificar si el codigo que estamos agregando existe
                Dim Existe = ExistsCode_Generic(newValues, ViewState("gridNoPago"))

            If ExistsCode_Generic(newValues, ViewState("gridNoPago")) Then
                showModal(NoPagoNotify, "deny", "Error", "El código de acción ya existe en el sistema. Válide e intente de nuevo.")
            ElseIf ExistsDesc_Generic(newValues, ViewState("gridCodAccion")) Then
                showModal(NoPagoNotify, "deny", "Error", "La descripción ya existe en el sistema. Válide e intente de nuevo.")
            Else
                SP.ADD_CODIGOS(V_Bandera:=107, V_Cat_Co_Accion:=newValues("txtCod"), V_Cat_Co_Adescripcion:=newValues("txtDesc"), V_Cat_Co_Id:=newValues("lblID"))
                showModal(NoPagoNotify, "ok", "Correcto", "Informacion actualizada correctamente.")
            End If
        ElseIf RadGrid.DeleteCommandName = e.CommandName Then
            Dim id = e.Item.Cells(3).Text
            Dim codigo = e.Item.Cells(4).Text

            SP.ADD_CODIGOS(V_Bandera:=113, V_Cat_Co_Id:=id)
            SP.ADD_CODIGOS(V_Bandera:=114, V_Cat_Co_Accion:=codigo)
            showModal(NoPagoNotify, "ok", "Correcto", "Informacion actualizada correctamente.")

        End If
    End Sub

    Private Sub gridResultados_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles gridResultados.ItemCommand
        If RadGrid.UpdateCommandName = e.CommandName Or RadGrid.PerformInsertCommandName = e.CommandName Then
            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)

            Dim newValues As Hashtable = GetGridValuesGeneric(MyUserControl)
            newValues("lblID") = IIf(newValues("lblID") = "", -1, newValues("lblID"))
            newValues("txtCod") = newValues("txtCod").ToString.ToUpper
            'newValues("txtAliasCod") = newValues("txtAliasCod").ToString.ToUpper

            'If Not IsCodeValid(newValues("txtCod")) Then
            '    e.Canceled = True
            '    showModal(ResultadosNotify, "deny", "Código Invalido", "El código de acción debe contener 2-4 caracteres alfanumericos sin caracteres especiales.")
            '    Return
            'Else
            If newValues("txtDesc") = "" Or newValues("txtDesc").ToString.Length < 4 Then
                e.Canceled = True
                showModal(ResultadosNotify, "deny", "Descripción Invalida", "La descripción del código de acción proporcionado debe contener mínimo 4 caracteres.")
                Return
            End If

            'Nos evitamos ir a la base a verificar si el codigo que estamos agregando existe
            Dim Existe = ExistsCode_Generic(newValues, ViewState("gridResultados"))

            If ExistsCode_Generic(newValues, ViewState("gridResultados")) Then
                showModal(ResultadosNotify, "deny", "Error", "El código de acción ya existe en el sistema. Válide e intente de nuevo.")
            ElseIf ExistsDesc_Generic(newValues, ViewState("gridCodAccion")) Then
                showModal(ResultadosNotify, "deny", "Error", "La descripción ya existe en el sistema. Válide e intente de nuevo.")
            Else
                SP.ADD_CODIGOS(V_Bandera:=106, V_Cat_Co_Accion:=newValues("txtCod"), V_Cat_Co_Adescripcion:=newValues("txtDesc"), V_Cat_Co_Id:=newValues("lblID"))
                showModal(ResultadosNotify, "ok", "Correcto", "Informacion actualizada correctamente.")
            End If
        ElseIf RadGrid.DeleteCommandName = e.CommandName Then
            Dim id = e.Item.Cells(3).Text
            Dim codigo = e.Item.Cells(4).Text

            SP.ADD_CODIGOS(V_Bandera:=111, V_Cat_Co_Id:=id)
            SP.ADD_CODIGOS(V_Bandera:=112, V_Cat_Co_Accion:=codigo)
            showModal(ResultadosNotify, "ok", "Correcto", "Informacion actualizada correctamente.")

        End If
    End Sub

    Private Sub gridAsociacion_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles gridAsociacion.ItemCommand
        If RadGrid.DeleteCommandName = e.CommandName Then
            Dim id = e.Item.Cells(3).Text

            SP.ADD_CODIGOS(V_Bandera:=115, V_Cat_Co_Id:=id)
            showModal(ResultadosNotify, "ok", "Correcto", "Informacion actualizada correctamente.")
        ElseIf RadGrid.EditCommandName = e.CommandName Then
            Session("EditAsociacion") = True
        ElseIf RadGrid.InitInsertCommandName = e.CommandName Then
            Session("NewAsociacion") = True
        ElseIf RadGrid.UpdateCommandName = e.CommandName Or RadGrid.PerformInsertCommandName = e.CommandName Then
            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim newValues As New Hashtable
            Try
                newValues("id") = TryCast(MyUserControl.FindControl("lblID"), RadLabel).Text
            Catch ex As Exception
                newValues("id") = -1
            End Try
            newValues("id") = IIf(newValues("id") = "", "-1", newValues("id"))

            newValues("btnAccionResultados") = If(TryCast(MyUserControl.FindControl("btnAccionResultados"), RadButton).Checked, "1", "0")

            Try
                newValues("AccionCodigo") = TryCast(MyUserControl.FindControl("lblSelectedAccion"), RadLabel).Attributes("Codigo").ToString
                newValues("AccionDescripcion") = TryCast(MyUserControl.FindControl("lblSelectedAccion"), RadLabel).Attributes("Descripcion").ToString
            Catch ex As Exception
                Dim mensaje As String
                If newValues("btnAccionResultados") = 1 Then
                    mensaje = "Selecciona un código de acción."
                Else
                    mensaje = "Selecciona un código de resultado."
                End If
                showModal(AsociacionNotify, "deny", "Error", mensaje)
                e.Canceled = True
                Return
            End Try

            Dim resultados = TryCast(MyUserControl.FindControl("comboResultados"), RadComboBox)
            If resultados.CheckedItems.Count = 0 Then
                Dim mensaje As String
                If newValues("btnAccionResultados") = 1 Then
                    mensaje = "Selecciona al menos un código de acción."
                Else
                    mensaje = "Selecciona al menos un código de resultado."
                End If
                showModal(AsociacionNotify, "deny", "Error", mensaje)
                e.Canceled = True
                Return
            End If

            newValues("cbPromesa") = IIf(TryCast(MyUserControl.FindControl("cbPromesa"), HtmlInputCheckBox).Checked, 1, 0)
            newValues("cbSignificativo") = IIf(TryCast(MyUserControl.FindControl("cbSignificativo"), HtmlInputCheckBox).Checked, 1, 0)
            newValues("cbTelefonico") = IIf(TryCast(MyUserControl.FindControl("cbTelefonico"), HtmlInputCheckBox).Checked, 1, 0)
            newValues("cbVisitador") = IIf(TryCast(MyUserControl.FindControl("cbVisitador"), HtmlInputCheckBox).Checked, 2, 0)

            Try
                newValues("txtSemaforoVerde") = TryCast(MyUserControl.FindControl("txtSemaforoVerde"), RadNumericTextBox).Text
            Catch ex As Exception
                showModal(AsociacionNotify, "deny", "Error", "Establece los días para el semaforo verde.")
                e.Canceled = True
                Return
            End Try
            Try
                newValues("txtSemaforoAmarillo") = TryCast(MyUserControl.FindControl("txtSemaforoAmarillo"), RadNumericTextBox).Text
            Catch ex As Exception
                showModal(AsociacionNotify, "deny", "Error", "Establece los días para el semaforo amarillo.")
                e.Canceled = True
                Return
            End Try

            Dim comboPerfiles = TryCast(MyUserControl.FindControl("comboPerfiles"), RadComboBox)
            Dim bits = ""
            Dim valido = False
            For Each perfil As RadComboBoxItem In comboPerfiles.Items
                bits &= IIf(perfil.Checked, "1", "0")
                valido = valido Or perfil.Checked
            Next

            If Not valido Then
                showModal(AsociacionNotify, "deny", "Error", "Selecciona al menos un perfil.")
                e.Canceled = True
                Return
            End If
            newValues("comboPerfiles") = bits

            Dim comboProductos = TryCast(MyUserControl.FindControl("comboProductos"), RadComboBox)
            bits = ""
            valido = False
            For Each producto As RadComboBoxItem In comboProductos.Items
                bits &= IIf(producto.Checked, "1", "0")
                valido = valido Or producto.Checked
            Next

            If Not valido Then
                showModal(AsociacionNotify, "deny", "Error", "Selecciona al menos un producto.")
                e.Canceled = True
                Return
            End If
            newValues("comboProductos") = bits


            For Each item As RadComboBoxItem In resultados.CheckedItems
                newValues("Resultado") = item.Attributes("Codigo").ToString
                newValues("RDescripcion") = item.Attributes("Descripcion").ToString
                If newValues("btnAccionResultados") = 1 Then
                    SP.ADD_CODIGOS(V_Bandera:=108, V_Cat_Co_Id:=newValues("id"), V_Cat_Co_Accion:=newValues("AccionCodigo"), V_Cat_Co_Adescripcion:=newValues("AccionDescripcion"), V_Cat_Co_Amarillo:=newValues("txtSemaforoAmarillo"), V_Cat_Co_Configuracion:=newValues("cbPromesa"), V_Cat_Co_Perfiles:=newValues("comboPerfiles"), V_Cat_Co_Producto:=newValues("comboProductos"), V_Cat_Co_Rdescripcion:=newValues("RDescripcion"), V_Cat_Co_Resultado:=newValues("Resultado"), V_Cat_Co_Significativo:=newValues("cbSignificativo"), V_Cat_Co_Tipo:=newValues("cbTelefonico") + newValues("cbVisitador"), V_Cat_Co_Verde:=newValues("txtSemaforoVerde"))
                Else
                    SP.ADD_CODIGOS(V_Bandera:=108, V_Cat_Co_Id:=newValues("id"), V_Cat_Co_Accion:=newValues("Resultado"), V_Cat_Co_Adescripcion:=newValues("RDescripcion"), V_Cat_Co_Amarillo:=newValues("txtSemaforoAmarillo"), V_Cat_Co_Configuracion:=newValues("cbPromesa"), V_Cat_Co_Perfiles:=newValues("comboPerfiles"), V_Cat_Co_Producto:=newValues("comboProductos"), V_Cat_Co_Rdescripcion:=newValues("AccionDescripcion"), V_Cat_Co_Resultado:=newValues("AccionCodigo"), V_Cat_Co_Significativo:=newValues("cbSignificativo"), V_Cat_Co_Tipo:=newValues("cbTelefonico") + newValues("cbVisitador"), V_Cat_Co_Verde:=newValues("txtSemaforoVerde"))
                End If
            Next
            showModal(AsociacionNotify, "ok", "Correcto", "Asociaciones creadas correctamente")

        End If
    End Sub


End Class