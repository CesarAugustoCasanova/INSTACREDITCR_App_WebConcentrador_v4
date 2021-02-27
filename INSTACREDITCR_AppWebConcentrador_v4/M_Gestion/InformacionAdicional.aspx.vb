'Imports System.Data.SqlClient
Imports System.Data
Imports Db
Imports Funciones
Imports System.Web.Services
Imports System.Globalization
Imports Busquedas
Imports Class_InformacionAdicional
Imports System.Web.Script.Serialization
Imports Telerik.Web.UI
Imports System.Data.SqlClient

Partial Class InformacionAdicional
    Inherits System.Web.UI.Page

    ''' <summary>
    ''' Contiene la información del usuario de la BD.
    ''' Ej. tmpUSUARIO("CAT_LO_NOMBRE")
    ''' </summary>
    ''' <returns>String</returns>
    Public Property tmpUSUARIO As IDictionary
        Get
            Return CType(Session("USUARIO"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("USUARIO") = value
        End Set
    End Property

    ''' <summary>
    ''' Contiene la informacion del credito de la BD.
    '''  Ej. tmpCredito("PR_CD_NOMBRE")
    ''' </summary>
    ''' <returns>String</returns>
    Public Property tmpCredito As IDictionary
        Get
            Return CType(Session("Credito"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("Credito") = value
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
        If Session("usuario") IsNot Nothing Then
            Try
                If Not tmpCredito Is Nothing And Not IsPostBack Then
                    RGTelefono.Rebind()
                    RGCorreo.Rebind()
                    RGDirecciones.Rebind()
                    'RGRelaciones.Rebind()
                    CreditoRetirado(tmpCredito("PR_MC_ESTATUS"))
                End If
            Catch ex As Exception
                EnviarCorreo("Administrador", "InformacionAdicional.ascx", "Page_Load", ex, "", "", tmpUSUARIO("CAT_LO_USUARIO"))
            End Try
        End If
    End Sub

    Private Sub Estilos_InformacionAdicional_Error(sender As Object, e As EventArgs) Handles Me.[Error]
        showModal(Notificacion, "warning", "Error", "gg")
    End Sub

    Sub CreditoRetirado(ByVal V_Estatus As String)
        If (V_Estatus = "Retirada") Or (V_Estatus = "Liquidada") Then
            'RGTelefono.Enabled = False
            RGCorreo.Enabled = False
            RGDirecciones.Enabled = False
            RGTelefono.EnableHeaderContextAggregatesMenu = False
        Else
            If tmpUSUARIO("CAT_LO_PGESTION").ToString.Substring(0, 1) = 0 Then ' Adicionales
                RGTelefono.Enabled = False
                RGCorreo.Enabled = False
                RGDirecciones.Enabled = False
            End If
        End If
    End Sub

    Private Sub RGCorreo_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RGCorreo.NeedDataSource
        Try
            RGCorreo.DataSource = Class_InformacionAdicional.LlenarElementosAgregar(tmpCredito("PR_MC_CREDITO"), 3)
        Catch ex As Exception
            RGCorreo.DataSource = Nothing
        End Try
    End Sub

    Private Sub RGDirecciones_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RGDirecciones.NeedDataSource
        Try
            RGDirecciones.DataSource = Class_InformacionAdicional.LlenarElementosAgregar(tmpCredito("PR_MC_CREDITO"), 8)
        Catch ex As Exception
            RGDirecciones.DataSource = Nothing
        End Try
    End Sub
    Private Sub RGTelefono_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RGTelefono.NeedDataSource
        Try
            RGTelefono.DataSource = Class_InformacionAdicional.LlenarElementosAgregar(tmpCredito("PR_MC_CREDITO"), 1)
        Catch ex As Exception
            RGTelefono.DataSource = Nothing
        End Try
    End Sub

    'Private Sub RGRelaciones_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RGRelaciones.NeedDataSource
    '    Try
    '        RGRelaciones.DataSource = Class_InformacionAdicional.LlenarElementosAgregar(tmpCredito("PR_MC_CREDITO"), 9)
    '    Catch ex As Exception
    '        RGRelaciones.DataSource = Nothing
    '    End Try
    'End Sub


    Private Function RGTelefonoToHash(editedItem As UserControl) As Hashtable
        Dim newValues As Hashtable = New Hashtable()
        Dim dias As String = ""
        newValues.Add("CONSECUTIVO", TryCast(editedItem.FindControl("LblConsecutivo"), RadLabel).Text)
        'newValues.Add("LADA", TryCast(editedItem.FindControl("TB0"), RadTextBox).Text)
        newValues.Add("TELEFONO", TryCast(editedItem.FindControl("TB2"), RadTextBox).Text)
        newValues.Add("EXTENSION", TryCast(editedItem.FindControl("TB1"), RadTextBox).Text)
        newValues.Add("TIPO", TryCast(editedItem.FindControl("DDTipo"), DropDownList).SelectedValue)
        newValues.Add("PARENTESCO", TryCast(editedItem.FindControl("DDParentesco"), DropDownList).SelectedValue)
        newValues.Add("NOMBRE", TryCast(editedItem.FindControl("TB12"), TextBox).Text)
        newValues.Add("CONTACTO", IIf(TryCast(editedItem.FindControl("RCBContacto"), RadCheckBox).Checked = "True", 1, 0))
        newValues.Add("PROPORCIONA", TryCast(editedItem.FindControl("TB13"), TextBox).Text)
        Try
            newValues.Add("HORA1", TryCast(editedItem.FindControl("TB14"), RadTimePicker).SelectedTime.Value.ToString.Substring(0, 5))
        Catch ex As Exception
            newValues.Add("HORA1", "00:00")
        End Try
        Try
            newValues.Add("HORA2", TryCast(editedItem.FindControl("TB15"), RadTimePicker).SelectedTime.Value.ToString.Substring(0, 5))
        Catch ex As Exception
            newValues.Add("HORA2", "00:00")
        End Try
        For i = 1 To 7
            If TryCast(editedItem.FindControl("RadCheckBox" & i), RadCheckBox).Checked Then
                dias = dias & "1"
            Else
                dias = dias & "0"
            End If
        Next
        newValues.Add("DIAS", dias)
        Return newValues
    End Function

    Protected Function RGCorreoToHash(editedItem As UserControl) As Hashtable
        Dim newValues As Hashtable = New Hashtable()
        Dim contacto As String = ""
        Dim dias As String = ""
        newValues.Add("TIPO", TryCast(editedItem.FindControl("DDTipo"), DropDownList).SelectedValue)
        newValues.Add("PARENTESCO", TryCast(editedItem.FindControl("DDParentesco"), DropDownList).SelectedValue)
        newValues.Add("NOMBRE", TryCast(editedItem.FindControl("TB12"), TextBox).Text)
        newValues.Add("PROPORCIONA", TryCast(editedItem.FindControl("TB13"), TextBox).Text)
        newValues.Add("CORREO", TryCast(editedItem.FindControl("TB3"), RadTextBox).Text)
        newValues.Add("CONTACTO", IIf(TryCast(editedItem.FindControl("RadCheckBox1"), RadCheckBox).Checked, "1", "0"))
        Return newValues
    End Function
    Private Function RGDireccionesToHash(editedItem As UserControl) As Hashtable
        Dim newValues As Hashtable = New Hashtable()
        Dim dias As String = ""
        newValues.Add("DIRECCIONID", TryCast(editedItem.FindControl("txtID"), RadLabel).Text)
        newValues.Add("Ciudad", TryCast(editedItem.FindControl("TBCiudad"), RadTextBox).Text)
        newValues.Add("Estado", TryCast(editedItem.FindControl("TBEstado"), RadTextBox).Text)
        'newValues.Add("Municipio", TryCast(editedItem.FindControl("TBMunicipio"), RadTextBox).Text)
        newValues.Add("CP", TryCast(editedItem.FindControl("TBCP"), RadTextBox).Text)
        newValues.Add("Calle", TryCast(editedItem.FindControl("TBCalle"), RadTextBox).Text)
        ' newValues.Add("NumInt", TryCast(editedItem.FindControl("TBNumInt"), RadTextBox).Text)
        ' newValues.Add("NumExt", TryCast(editedItem.FindControl("TBNumExt"), RadTextBox).Text)
        '  newValues.Add("MZ", TryCast(editedItem.FindControl("TBManzana"), RadTextBox).Text)
        '  newValues.Add("Lt", TryCast(editedItem.FindControl("TBLote"), RadTextBox).Text)
        newValues.Add("ENTRECALLE1", TryCast(editedItem.FindControl("TBCalle1"), RadTextBox).Text)
        ' newValues.Add("ENTRECALLE2", TryCast(editedItem.FindControl("TBCalle2"), RadTextBox).Text)
        newValues.Add("Vigente", IIf(TryCast(editedItem.FindControl("CBVigente"), RadCheckBox).Checked, "1", "0"))
        ' newValues.Add("TIPODOMICILIO", TryCast(editedItem.FindControl("CBTipoDomicilio"), RadComboBox).SelectedValue)
        ' newValues.Add("TIPOVIVIENDA", TryCast(editedItem.FindControl("CBTipoVivienda"), RadComboBox).SelectedValue)
        ' newValues.Add("VIVEENCASA", TryCast(editedItem.FindControl("CBViveEnCasa"), RadComboBox).SelectedValue)
        newValues.Add("Proporciona", TryCast(editedItem.FindControl("TBProporciona"), TextBox).Text)
        newValues.Add("NOMBRE", TryCast(editedItem.FindControl("TBNombre"), TextBox).Text)
        Dim ddl As RadDropDownList = TryCast(editedItem.FindControl("DDCol"), RadDropDownList)
        Dim txt As RadTextBox = TryCast(editedItem.FindControl("TBCol"), RadTextBox)
        newValues.Add("Colonia", IIf(ddl.SelectedValue = "", txt.Text, ddl.SelectedValue))
        newValues.Add("Parentesco", TryCast(editedItem.FindControl("DDParentesco"), RadComboBox).SelectedValue)
        Try
            newValues.Add("Horario1", TryCast(editedItem.FindControl("TB14"), RadTimePicker).SelectedTime.Value.ToString.Substring(0, 5))
        Catch ex As Exception
            newValues.Add("Horario1", "00:00")
        End Try
        Try
            newValues.Add("Horario2", TryCast(editedItem.FindControl("TB15"), RadTimePicker).SelectedTime.Value.ToString.Substring(0, 5))
        Catch ex As Exception
            newValues.Add("Horario2", "00:00")
        End Try
        newValues.Add("contacto", IIf(TryCast(editedItem.FindControl("RadCheckBox1"), RadCheckBox).Checked, "1", "0"))
        For i = 2 To 8
            If TryCast(editedItem.FindControl("RadCheckBox" & i), RadCheckBox).Checked Then
                dias = dias & "1"
            Else
                dias = dias & "0"
            End If
        Next
        newValues.Add("DIAS", dias)
        Return newValues
    End Function
    Function validaDominio(correo As String) As String
        Dim dominio As String = correo.Split("@")(1)
        Dim valida As String = ""
        Dim comand As New SqlCommand("select * from CAT_DOMINIOS_SEG")
        Dim tabla As DataTable = Consulta_Procedure(comand, "DOMINIOS")
        For i = 0 To tabla.Rows.Count - 1
            If dominio = (CType(tabla.Rows(i).Item(1), String)) Then
                valida = "OK"
            End If
        Next
        Return valida
    End Function
    Private Sub RGCorreo_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RGCorreo.ItemCommand
        'Insertar nuevo registro
        If e.CommandName = "PerformInsert" Then
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim hash As Hashtable = RGCorreoToHash(MyUserControl)
            If hash.Item("CORREO") = "" Then
                showModal(Notificacion, "warning", "Aviso", "Por favor agregue un correo valido")
                e.Canceled = True
            ElseIf validaDominio(hash.Item("CORREO")) <> "OK" Then
                showModal(Notificacion, "warning", "Aviso", "Por favor agregue un dominio valido")
                e.Canceled = True
            ElseIf hash.Item("TIPO") = "" Then
                showModal(Notificacion, "warning", "Aviso", "Por favor seleccione un tipo")
                e.Canceled = True
            ElseIf hash.Item("PARENTESCO") = "" Then
                showModal(Notificacion, "warning", "Aviso", "Por favor seleccione un parentesco")
                e.Canceled = True
            Else
                Dim resultado As String = Class_InformacionAdicional.AgregarCorreo(tmpCredito("PR_MC_CREDITO"), tmpCredito("PR_MC_PRODUCTO"), hash.Item("PARENTESCO"), hash.Item("NOMBRE"), hash.Item("CORREO"), hash.Item("CONTACTO"), "Agregar", tmpUSUARIO("CAT_LO_USUARIO"), tmpCredito("PR_MC_AGENCIA"), "Captura", hash.Item("TIPO"), hash.Item("PROPORCIONA"))
                If resultado = "1" Or resultado Like "*insertado correctamente*" Then
                    RGCorreo.Rebind()
                    showModal(Notificacion, "ok", "Exito", "Correo agregado correctamente")
                Else
                    showModal(Notificacion, "warning", "Aviso", resultado)
                    e.Canceled = True
                End If
            End If

            'Actualizar registro
        ElseIf e.CommandName = "Update" Then
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim hash As Hashtable = RGCorreoToHash(MyUserControl)
            If hash.Item("CORREO") = "" Then
                showModal(Notificacion, "warning", "Aviso", "Por favor agregue un correo valido")
                e.Canceled = True
            ElseIf hash.Item("TIPO") = "" Then
                showModal(Notificacion, "warning", "Aviso", "Por favor seleccione un tipo")
                e.Canceled = True
            ElseIf hash.Item("PARENTESCO") = "" Then
                showModal(Notificacion, "warning", "Aviso", "Por favor seleccione un parentesco")
                e.Canceled = True
            Else
                Dim resultado As String = Class_InformacionAdicional.AgregarCorreo(tmpCredito("PR_MC_CREDITO"), tmpCredito("PR_MC_PRODUCTO"), hash.Item("PARENTESCO"), hash.Item("NOMBRE"), hash.Item("CORREO"), hash.Item("CONTACTO"), "Actualizar", tmpUSUARIO("CAT_LO_USUARIO"), tmpCredito("PR_MC_AGENCIA"), "Captura", hash.Item("TIPO"), hash.Item("PROPORCIONA"))
                If resultado = "0" Or resultado Like "*insertado correctamente*" Or resultado Like "*actualizado correctamente*" Then
                    showModal(Notificacion, "ok", "Exito", "Correo actualizado correctamente")
                    RGCorreo.Rebind()
                    'Response.Redirect("MasterPage.aspx")
                Else
                    showModal(Notificacion, "warning", "Aviso", resultado)
                    e.Canceled = True
                End If
            End If

        End If
    End Sub

    Private Sub RGTelefono_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RGTelefono.ItemCommand
        If e.CommandName = "PerformInsert" Then
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim newValues As Hashtable = RGTelefonoToHash(MyUserControl)
            If newValues.Item("TIPO") = "N" Then
                showModal(Notificacion, "warning", "Aviso", "Por favor seleccione un tipo")
                e.Canceled = True
            ElseIf newValues.Item("PARENTESCO") = "" Then
                showModal(Notificacion, "warning", "Aviso", "Por favor seleccione un parentesco")
                e.Canceled = True
            ElseIf newValues.Item("TELEFONO").ToString.trim.Length <> 10 Then
                showModal(Notificacion, "warning", "Aviso", "El Telefono debe de ser a 10 digitos")
                e.Canceled = True
            Else
                Dim resultado As String = Class_InformacionAdicional.AgregarTelefono("-1", tmpCredito("PR_MC_CREDITO"), tmpCredito("PR_MC_CLIENTE"), tmpCredito("PR_MC_PRODUCTO"), newValues.Item("TELEFONO").ToString.Trim, newValues.Item("TIPO"), newValues.Item("PARENTESCO"), newValues.Item("NOMBRE"), newValues.Item("EXTENSION"), newValues.Item("HORA1"), newValues.Item("HORA2"), tmpUSUARIO("CAT_LO_USUARIO"), tmpCredito("PR_MC_AGENCIA"), "Captura", newValues.Item("DIAS"), newValues.Item("PROPORCIONA"), newValues.Item("CONTACTO"))
                If resultado = "1" Or resultado Like "*insertado correctamente*" Then
                    showModal(Notificacion, "ok", "Exito", "Telefono agregado correctamente")
                    ClientScript.RegisterStartupScript(Me.GetType(), "RefreshParent", "<script type='text/javascript'>var btn = window.parent.document.getElementById('BtnRecargar');if (btn) btn.click();</script>")
                    RGTelefono.Rebind()
                Else
                    showModal(Notificacion, "warning", "Aviso", resultado)
                    e.Canceled = True
                End If
            End If

        ElseIf e.CommandName = "Update" Then
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim newValues As Hashtable = RGTelefonoToHash(MyUserControl)
            If newValues.Item("TIPO") = "N" Then
                showModal(Notificacion, "warning", "Aviso", "Por favor seleccione un tipo")
                e.Canceled = True
            ElseIf newValues.Item("PARENTESCO") = "" Then
                showModal(Notificacion, "warning", "Aviso", "Por favor seleccione un parentesco")
                e.Canceled = True
            ElseIf newValues.Item("TELEFONO").ToString.Trim.Length <> 10 Then
                showModal(Notificacion, "warning", "Aviso", "Por favor seleccione un parentesco")
                e.Canceled = True
            Else
                Dim resultado As String = Class_InformacionAdicional.AgregarTelefono(newValues("CONSECUTIVO"), tmpCredito("PR_MC_CREDITO"), tmpCredito("PR_MC_CLIENTE"), tmpCredito("PR_MC_PRODUCTO"), newValues.Item("TELEFONO").ToString.Trim, newValues.Item("TIPO"), newValues.Item("PARENTESCO"), newValues.Item("NOMBRE"), newValues.Item("EXTENSION"), newValues.Item("HORA1"), newValues.Item("HORA2"), tmpUSUARIO("CAT_LO_USUARIO"), tmpCredito("PR_MC_AGENCIA"), "Captura", newValues.Item("DIAS"), newValues.Item("PROPORCIONA"), newValues.Item("CONTACTO"))
                If resultado = "1" Or resultado Like "*insertado correctamente*" Or resultado Like "*actualizado correctamente*" Then
                RGTelefono.Rebind()
                showModal(Notificacion, "ok", "Exito", "Telefono actualizado correctamente")
            Else
                showModal(Notificacion, "warning", "Aviso", resultado)
                    e.Canceled = True
                End If
            End If

        End If
    End Sub

    Private Sub RGDirecciones_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RGDirecciones.ItemCommand
        If e.CommandName = "PerformInsert" Then
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim newValues As Hashtable = RGDireccionesToHash(MyUserControl)
            newValues.Add("Credito", tmpCredito("PR_MC_CREDITO"))
            If newValues.Item("CP") = "" Or Len(newValues.Item("CP")) < 5 Then
                showModal(Notificacion, "warning", "Aviso", "Por favor capture un cp válido")
                e.Canceled = True
            ElseIf newValues.Item("Ciudad") = "" Or newValues.Item("Estado") = "" Then
                showModal(Notificacion, "warning", "Aviso", "Por favor haga clic en el boton validar CP")
                e.Canceled = True
            ElseIf newValues.Item("Colonia") = "" Then
                showModal(Notificacion, "warning", "Aviso", "Por favor seleccione una colonia")
                e.Canceled = True
            Else
                Dim resultado As String = Class_InformacionAdicional.AgregarDireccion(tmpUSUARIO("CAT_LO_USUARIO"), tmpCredito("PR_MC_CREDITO"),  tmpCredito("PR_MC_PRODUCTO"), newValues)
                If resultado = "1" Or resultado Like "*insertado correctamente*" Then
                    showModal(Notificacion, "ok", "Exito", "Dirección agregada correctamente")
                    RGDirecciones.Rebind()
                Else
                    showModal(Notificacion, "warning", "Aviso", resultado)
                    e.Canceled = True
                End If
            End If

        ElseIf e.CommandName = "Update" Then
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim newValues As Hashtable = RGDireccionesToHash(MyUserControl)
            newValues.Add("Credito", tmpCredito("PR_MC_CREDITO"))
            If newValues.Item("CP") = "" Or Len(newValues.Item("CP")) < 5 Then
                showModal(Notificacion, "warning", "Aviso", "Por favor capture un cp válido")
                e.Canceled = True
            ElseIf newValues.Item("Ciudad") = "" Or newValues.Item("Estado") = "" Then
                showModal(Notificacion, "warning", "Aviso", "Por favor haga clic en el boton validar CP")
                e.Canceled = True
            ElseIf newValues.Item("Colonia") = "" Then
                showModal(Notificacion, "warning", "Aviso", "Por favor seleccione una colonia")
                e.Canceled = True
            Else
                Dim resultado As String = Class_InformacionAdicional.AgregarDireccion(tmpUSUARIO("CAT_LO_USUARIO"), tmpCredito("PR_MC_CREDITO"), tmpCredito("PR_MC_PRODUCTO"), newValues)
                If resultado = "1" Or resultado Like "*insertado correctamente*" Or resultado Like "*actualizado correctamente*" Then
                    showModal(Notificacion, "ok", "Exito", "Dirección actualizada correctamente")
                    RGDirecciones.Rebind()
                Else
                    showModal(Notificacion, "warning", "Aviso", resultado)
                    e.Canceled = True
                End If
            End If
        End If
    End Sub

    Public Function revisa(ByRef seleccionado As Object) As String
        Dim selec As String = ""
        Try
            selec = Convert.ToString(seleccionado)
            If selec = "D" Or selec = "C" Then
                selec = ""
            End If
        Catch
            selec = ""
        End Try
        Return selec
    End Function

    Public Function revisa2(ByRef seleccionado As Object) As String
        Dim selec As String = ""
        Try
            selec = Convert.ToString(seleccionado)
            If selec = "Seleccione" Or selec = "" Or selec = " " Then
                selec = ""
            End If
        Catch
            selec = ""
        End Try
        Return selec
    End Function

    Public Function quitaNull(ByVal valor As Object) As Boolean
        If IsDBNull(valor) Then
            Return False
        End If
        If TryCast(valor, String) = "1" Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function quitaNull2(ByVal valor As Object) As String
        If IsDBNull(valor) Then
            Return False
        End If
        If TryCast(valor, String) = "True" Then
            Return "1"
        Else
            Return "0"
        End If
    End Function
End Class
