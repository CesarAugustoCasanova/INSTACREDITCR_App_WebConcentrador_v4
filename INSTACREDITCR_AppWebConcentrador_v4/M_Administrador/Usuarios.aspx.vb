Imports System.Data
Imports System.Data.SqlClient
Imports Db
Imports Funciones
Imports System.IO
Imports Telerik.Web.UI
Imports Spire.Xls
Partial Class M_Administrador_Usuarios
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

    ''' <summary>
    ''' Establece los parametros para mostrar una notificacion
    ''' </summary>
    ''' <param name="Notificacion">Objeto RadNotification de Telerik</param>
    ''' <param name="icono">info - delete - deny - edit - ok - warning - none</param>
    ''' <param name="titulo">título de la notificación</param>
    ''' <param name="msg">mensaje de la notificación</param>
    Public Shared Sub ShowModal(ByRef Notificacion As Object, ByVal icono As String, ByVal titulo As String, ByVal msg As String)
        Dim radnot As RadNotification = TryCast(Notificacion, RadNotification)
        radnot.TitleIcon = icono
        radnot.ContentIcon = icono
        radnot.Title = titulo
        radnot.Text = msg
        radnot.Show()
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If TmpUSUARIO Is Nothing Then
            'OffLine(HidenUrs.Value)
            'AUDITORIA(HidenUrs.Value, "Administrador", "Usuarios", "", "Page_Load", "", Request.ServerVariables("REMOTE_ADDR"), Request.ServerVariables("REMOTE_HOST"))
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/SesionExpirada.aspx")
        Else
            Try
                If Not IsPostBack Then
                    'gridUsuarios.MasterTableView.CommandItemSettings.ShowAddNewRecordButton = IIf(TmpUSUARIO("CAT_PE_PERFIL").ToUpper = "TI", True, False)
                    gridUsuarios.Focus()
                    If Session("UsuarioMsg") IsNot Nothing Then
                        ShowModal(RadNotification1, "ok", "Correcto", Session("UsuarioMsg"))
                        Session.Remove("UsuarioMsg")
                    End If
                End If
            Catch ex As Exception
                SendMail("Page_Load", ex, "", "", TmpUSUARIO("CAT_LO_USUARIO"))
            End Try
        End If
    End Sub
    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Administrador", "Usuarios.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub

    Function OpcionesUsr(V_Usuario As String, V_Agencia As String, V_Producto As String, V_Bandera As String) As String
        Dim SSCommandVarios As New SqlCommand
        SSCommandVarios.CommandText = "Sp_Llenar_Usuario"
        SSCommandVarios.CommandType = CommandType.StoredProcedure
        SSCommandVarios.Parameters.Add("@V_Usuario", SqlDbType.NVarChar).Value = V_Usuario
        SSCommandVarios.Parameters.Add("@V_Agencia", SqlDbType.NVarChar).Value = V_Agencia
        SSCommandVarios.Parameters.Add("@V_Producto", SqlDbType.NVarChar).Value = V_Producto
        SSCommandVarios.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = V_Bandera
        Dim DtsVarios As DataTable = Consulta_Procedure(SSCommandVarios, "Usuarios")
        Return DtsVarios.Rows(0).Item("Mensaje")
    End Function


    Protected Sub Aviso(ByVal MSJ As String)
        RadAviso.RadAlert(MSJ.Replace("""", "").Replace("'", "").Replace(Chr(10), "").Replace(Chr(13), ""), 440, 155, "AVISO", Nothing)
    End Sub
    Protected Sub Confirma(ByVal MSJ As String)
        RadAviso.RadConfirm(MSJ, "confirmCallbackFn", 440, 155, Nothing, "Alerta")

    End Sub
    Sub SetCUsuario(usr As String)
        If TmpUSUARIO.Contains("CUsuario") Then
            TmpUSUARIO("CUsuario") = usr
        Else
            TmpUSUARIO.Add("CUsuario", usr)
        End If
    End Sub
    Private Sub GridUsuarios_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles gridUsuarios.ItemCommand

        Dim carpetaGrids As String = "./grids/Usuarios/"
        Dim controlUsuario As String = "Usuarios.ascx"


        Select Case e.CommandName
            Case "Edit"
                Session("Edit") = True
            Case "InitInsert"
                Session("initInsert") = True
                Session("CAT_LO_ID") = "n/a"
            Case "Expirar"
                Dim item As GridItem = e.Item
                Try
                    OpcionesUsr(item.Cells(6).Text, TmpUSUARIO("CAT_LO_AGENCIA"), "", 5)
                    ShowModal(RadNotification1, "ok", "Correcto", "Usuario " & item.Cells(6).Text & " expirado")
                Catch ex As Exception
                    ShowModal(RadNotification1, "delete", "Error", ex.Message)
                End Try
            Case "Cancelar"
                Dim item As GridItem = e.Item
                Try
                    OpcionesUsr(item.Cells(6).Text, TmpUSUARIO("CAT_LO_AGENCIA"), "", 11)
                    ShowModal(RadNotification1, "ok", "Correcto", "Usuario " & item.Cells(6).Text & " cancelado")
                Catch ex As Exception
                    ShowModal(RadNotification1, "delete", "Error", ex.Message)
                End Try
            Case "Update"
                Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
                Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
                Dim valores As Hashtable = GetGridValues(MyUserControl)
                Dim resultado As String = Validar(valores, False)

                If resultado = "OK" Then
                    Dim res2 As String = Guardar(valores)
                    If res2 Like "*correctamente*" Then
                        Session.Remove("CAT_LO_ID")
                        Aviso("Usuario '" + valores("usuario") + "' actualizado correctamente")
                    Else
                        Aviso(res2)
                        e.Canceled = True
                    End If
                Else
                    Aviso(resultado)
                    e.Canceled = True
                End If
            Case "PerformInsert"
                Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
                Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
                Dim valores As Hashtable = GetGridValues(MyUserControl)
                Dim resultado As String = Validar(valores, True)

                If resultado = "OK" Then
                    Dim res2 As String = ""
                    res2 = Guardar(valores)
                    If res2 Like "*correctamente*" Then
                        Session.Remove("CAT_LO_ID")
                        Aviso("Usuario '" + valores("usuario") + "' dado de alta correctamente")
                    Else
                        Aviso("Error al dar de alta nuevo usuario, intente más tarde</br>" & res2)
                        e.Canceled = True
                    End If
                Else
                    Aviso(resultado)
                    e.Canceled = True
                End If
        End Select

        gridUsuarios.MasterTableView.EditFormSettings.UserControlName = carpetaGrids & controlUsuario

    End Sub

    Private Sub gridUsuarios_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles gridUsuarios.NeedDataSource
        Dim SSCommandUsuario As New SqlCommand
        SSCommandUsuario.CommandText = "SP_BUSQUEDA_USUARIO"
        SSCommandUsuario.CommandType = CommandType.StoredProcedure
        SSCommandUsuario.Parameters.Add("@V_Agencia", SqlDbType.NVarChar).Value = IIf(TmpUSUARIO("CAT_LO_AGENCIA") Is Nothing, 0, TmpUSUARIO("CAT_LO_AGENCIA"))
        SSCommandUsuario.Parameters.Add("@V_Usuario", SqlDbType.NVarChar).Value = TmpUSUARIO("CAT_LO_USUARIO")
        SSCommandUsuario.Parameters.Add("@V_Patron", SqlDbType.NVarChar).Value = ""
        SSCommandUsuario.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 3
        Dim DtsUsuario As DataTable = Consulta_Procedure(SSCommandUsuario, "BUSQUEDA")
        If DtsUsuario.Columns(0).ColumnName.ToUpper = "MENSAJE" Then
            ShowModal(RadNotification1, "delete", "Error", DtsUsuario(0)(0))
            gridUsuarios.DataSource = Nothing
        Else
            gridUsuarios.DataSource = DtsUsuario
        End If
    End Sub

    Public Shared Function GetGridValues(usrControl As UserControl) As Hashtable
        Dim valores As New Hashtable
        Dim V_Multiple As String = 0

        valores.Add("usuario", CType(usrControl.FindControl("txtUsuario"), TextBox).Text)
        valores.Add("nombre", CType(usrControl.FindControl("txtNombre"), TextBox).Text)
        valores.Add("contrasena", CType(usrControl.FindControl("txtContrasena"), RadTextBox).Text)
        valores.Add("contrasena2", CType(usrControl.FindControl("txtRepiteContrasena"), RadTextBox).Text)
        valores.Add("supervisor", CType(usrControl.FindControl("ddlSupervisor"), RadDropDownList).SelectedValue)
        valores.Add("rol", CType(usrControl.FindControl("ddlPerfil"), RadDropDownList).SelectedValue)
        valores.Add("estatus", CType(usrControl.FindControl("ddlEstatus"), RadDropDownList).SelectedValue)
        valores.Add("motivo", CType(usrControl.FindControl("txtMotivo"), TextBox).Text)
        valores.Add("tipousr", CType(usrControl.FindControl("rDdlTipoUsuario"), RadDropDownList).SelectedValue)
        valores.Add("capacidadCuentas", CType(usrControl.FindControl("rTxtCapacidadCuentas"), TextBox).Text)
        valores.Add("instancia", CType(usrControl.FindControl("rDdlInstancia"), RadDropDownList).SelectedValue)
        valores.Add("eMail", CType(usrControl.FindControl("TxtCat_Lo_EMail"), RadTextBox).Text)
        valores.Add("numEmpleado", CType(usrControl.FindControl("TxtCat_Lo_NumEmpleado"), RadTextBox).Text)
        Dim inter As Integer = If(CType(usrControl.FindControl("rChbxInterno"), RadCheckBox).Checked = True, 1, 0)
        valores.Add("interno", inter)

        Dim rcbApps = CType(usrControl.FindControl("RCBAppsBloqueadas"), RadComboBox)
        Dim appsLock As New List(Of String)
        For Each item As RadComboBoxItem In rcbApps.CheckedItems
            appsLock.Add(item.Value)
        Next
        valores.Add("bloqapps", String.Join(",", appsLock))



        Dim rcbCCs = CType(usrControl.FindControl("rDdlCat_Lo_CoberturaCobranza"), RadComboBox)
        Dim rcbCC As New List(Of String)
        For Each item As RadComboBoxItem In rcbCCs.CheckedItems
            rcbCC.Add(item.Value)
        Next
        valores.Add("CoberturaCobranza", String.Join(",", rcbCC))




        If CType(usrControl.FindControl("dtpHoraEntrada"), RadTimePicker).SelectedDate.HasValue Then
            valores.Add("hora1", CType(usrControl.FindControl("dtpHoraEntrada"), RadTimePicker).SelectedTime.Value.Hours)
        Else
            valores.Add("hora1", "7")
        End If

        If CType(usrControl.FindControl("dtpHoraSalida"), RadTimePicker).SelectedDate.HasValue Then
            valores.Add("hora2", CType(usrControl.FindControl("dtpHoraSalida"), RadTimePicker).SelectedTime.Value.Hours)
        Else
            valores.Add("hora2", "19")
        End If

        valores.Add("agencia", CType(usrControl.FindControl("ddlAgencia"), RadDropDownList).SelectedValue)
        valores.Add("verAgencias", Rad_Vcadena(CType(usrControl.FindControl("cbVerAgencias"), RadComboBox)))
        valores.Add("esAgencia", CType(usrControl.FindControl("CBAgencia"), RadCheckBox).Checked)

        valores.Add("producto", Rad_Vcadena(CType(usrControl.FindControl("cbVerProductos"), RadComboBox)))
        'valores.Add("plaza", CType(usrControl.FindControl("CBPlazas"), RadComboBox).SelectedValue)
        valores.Add("meta", CType(usrControl.FindControl("TxtMeta"), RadNumericTextBox).Value)
        valores.Add("oldpassword", CType(usrControl.FindControl("LBLoldpassword"), Label).Text)
        valores.Add("NivelQuitas", CType(usrControl.FindControl("dllNivelQuitas"), RadDropDownList).SelectedValue)

        Return valores
    End Function

    Private Function Validar(valores As Hashtable, isInsert As Boolean) As String
        Dim mensaje As String = ""
        Try
            If isInsert Then
                If ValidarUsr(valores("usuario")) = "El Usuario Ya Existe Valide" Then
                    mensaje = "El Usuario Ya Existe Valide"
                End If
            End If
            If String.IsNullOrEmpty(valores("usuario")) Or String.IsNullOrWhiteSpace(valores("usuario")) Then
                mensaje = "Capture un usuario válido"
            ElseIf String.IsNullOrEmpty(valores("nombre")) Or String.IsNullOrWhiteSpace(valores("nombre")) Then
                mensaje = "Capture un nombre válido"
            ElseIf String.IsNullOrEmpty(valores("agencia")) Or String.IsNullOrWhiteSpace(valores("agencia")) Then
                mensaje = "Seleccione la agencia a la que pertenece el usuario"
            ElseIf valores("contrasena").ToString.Length < 7 And valores("oldpassword") = "" Then
                mensaje = "No existe contraseña previa. La longitud mínima de la contraseña es de 8 caracteres"
            ElseIf valores("contrasena") <> valores("contrasena2") Then
                mensaje = "Las contraseñas no coinciden"
            ElseIf String.IsNullOrEmpty(valores("supervisor")) Or String.IsNullOrWhiteSpace(valores("supervisor")) Then
                mensaje = "Seleccione un supervisor"
            ElseIf String.IsNullOrEmpty(valores("estatus")) Or String.IsNullOrWhiteSpace(valores("estatus")) Then
                mensaje = "Seleccione un estatus"
            ElseIf Val(valores("hora1")) > Val(valores("hora2")) Then
                mensaje = "La hora de entrada no puede ser mayor a la hora de salida"
            ElseIf String.IsNullOrEmpty(valores("rol")) Or String.IsNullOrWhiteSpace(valores("rol")) Then
                mensaje = "Seleccione un rol"
            ElseIf valores("estatus") = "Cancelado" And (String.IsNullOrEmpty(valores("motivo")) Or String.IsNullOrWhiteSpace(valores("motivo"))) Then
                mensaje = "Capture un motivo"
            ElseIf String.IsNullOrEmpty(valores("tipousr")) Or String.IsNullOrWhiteSpace(valores("tipousr")) Then
                mensaje = "Seleccione un tipo de usuario"
            ElseIf String.IsNullOrEmpty(valores("capacidadCuentas")) Or String.IsNullOrWhiteSpace(valores("capacidadCuentas")) Then
                mensaje = "Seleccione capacidad de cuentas"
            ElseIf String.IsNullOrEmpty(valores("instancia")) Or String.IsNullOrWhiteSpace(valores("instancia")) Then
                mensaje = "Seleccione Una Instancia"
            ElseIf valores("NivelQuitas") = "" Then
                mensaje = "Seleccione Nivel Quitas"
            Else
                validaMail(valores("eMail"))
                ValidaCapacidadCuentasMax(valores("capacidadCuentas"))

                mensaje = "OK"
            End If
            Return mensaje
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Sub ValidaCapacidadCuentasMax(valor As Integer)
        Dim dts = variosQrs(23)
        If valor > Val(dts.Rows(0).Item("CapacidadMaxUser")) Then
            Throw New Exception("La capacida Maxima que puede recibir un usuario es de " & dts.Rows(0).Item("CapacidadMaxUser").ToString)
        End If
    End Sub
    Sub validaMail(mail As String)
        If mail.ToString.Trim.Length > 0 Then
            If mail.ToString Like "*@*" And mail.ToString Like "*.com*" Then
            Else
                Throw New Exception("El E-Mail debe contener [@] y [.com]")
            End If
        End If
    End Sub

    Function variosQrs(V_Bandera As Integer, Optional V_Valor As String = "") As DataTable
        Dim SSCommand As New SqlCommand("SP_VARIOS_QRS")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_VALOR", SqlDbType.NVarChar).Value = V_Valor
        SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = V_Bandera
        Dim dts As DataTable = Consulta_Procedure(SSCommand, SSCommand.CommandText)
        If dts.TableName = "Exception" Then
            Throw New Exception(dts.Rows(0).Item(0).ToString)
        End If
        Return dts
    End Function
    Function ValidarUsr(Usuario As String) As String
        Dim SSCommandUsuario As New SqlCommand
        SSCommandUsuario.CommandText = "Sp_Llenar_Usuario"
        SSCommandUsuario.CommandType = CommandType.StoredProcedure
        SSCommandUsuario.Parameters.Add("@V_Usuario", SqlDbType.NVarChar).Value = Usuario
        SSCommandUsuario.Parameters.Add("@V_Agencia", SqlDbType.NVarChar).Value = ""
        SSCommandUsuario.Parameters.Add("@V_Producto", SqlDbType.NVarChar).Value = ""
        SSCommandUsuario.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 7
        Dim DtsUsuario As DataTable = Consulta_Procedure(SSCommandUsuario, "BUSQUEDA")
        Return DtsUsuario.Rows(0).Item("Mensaje")
    End Function

    Private Shared Function Rad_Vcadena(ByRef v_item As RadComboBox) As String
        Dim v_cadena As String = ""
        Dim collection As IList(Of RadComboBoxItem) = v_item.CheckedItems

        For Each item As RadComboBoxItem In collection
            v_cadena = v_cadena & item.Value & ","
        Next

        If collection.Count = 0 Then
            v_cadena = ""
        Else
            v_cadena = v_cadena.Substring(0, Len(v_cadena) - 1)
        End If

        Return v_cadena
    End Function


    Private Function Guardar(valores As Hashtable) As String

        Try
            Dim oraCommanVarios As New SqlCommand
            oraCommanVarios.CommandText = "SP_CREAR_USUARIO"
            oraCommanVarios.CommandType = CommandType.StoredProcedure
            oraCommanVarios.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 11
            oraCommanVarios.Parameters.Add("@V_ID", SqlDbType.Decimal).Value = IIf(Session("CAT_LO_ID").ToString = "n/a", -1, Session("CAT_LO_ID"))
            oraCommanVarios.Parameters.Add("@V_NOMBRE", SqlDbType.NVarChar).Value = valores("nombre")
            oraCommanVarios.Parameters.Add("@V_USUARIO", SqlDbType.NVarChar).Value = valores("usuario")
            oraCommanVarios.Parameters.Add("@V_CONTRASENA", SqlDbType.NVarChar).Value = IIf(valores("contrasena") = "", valores("oldpassword"), valores("contrasena"))
            oraCommanVarios.Parameters.Add("@V_ESTATUS", SqlDbType.NVarChar).Value = valores("estatus")
            oraCommanVarios.Parameters.Add("@v_AGENCIA", SqlDbType.NVarChar).Value = valores("agencia")
            oraCommanVarios.Parameters.Add("@v_VERAGENCIA", SqlDbType.NVarChar).Value = valores("verAgencias")
            oraCommanVarios.Parameters.Add("@V_CPP", SqlDbType.NVarChar).Value = valores("bloqapps")
            oraCommanVarios.Parameters.Add("@V_CAT_LO_CAPACIDAD_CUENTAS", SqlDbType.NVarChar).Value = valores("capacidadCuentas")
            oraCommanVarios.Parameters.Add("@V_CAT_LO_TIPO_USR", SqlDbType.NVarChar).Value = valores("tipousr")
            oraCommanVarios.Parameters.Add("@V_MOTIVO", SqlDbType.NVarChar).Value = valores("motivo")
            oraCommanVarios.Parameters.Add("@V_PERFIL", SqlDbType.NVarChar).Value = valores("rol")
            oraCommanVarios.Parameters.Add("@v_supervisor", SqlDbType.NVarChar).Value = valores("supervisor")
            oraCommanVarios.Parameters.Add("@V_HENTRADA", SqlDbType.NVarChar).Value = valores("hora1")
            oraCommanVarios.Parameters.Add("@v_HSALIDA", SqlDbType.NVarChar).Value = valores("hora2")
            oraCommanVarios.Parameters.Add("@v_PRODUCTO", SqlDbType.NVarChar).Value = valores("producto")
            oraCommanVarios.Parameters.Add("@v_email", SqlDbType.NVarChar).Value = valores("eMail")
            oraCommanVarios.Parameters.Add("@v_noEmpleado", SqlDbType.NVarChar).Value = valores("numEmpleado")
            oraCommanVarios.Parameters.Add("@v_META", SqlDbType.NVarChar).Value = IIf(valores("meta") Is Nothing, "", valores("meta"))
            oraCommanVarios.Parameters.Add("@V_QUIENMODIFICA", SqlDbType.NVarChar).Value = TmpUSUARIO("CAT_LO_USUARIO")
            oraCommanVarios.Parameters.Add("@v_instancia", SqlDbType.NVarChar).Value = valores("instancia")
            oraCommanVarios.Parameters.Add("@v_INTERNOEXTERNO", SqlDbType.Int).Value = valores("interno")
            oraCommanVarios.Parameters.Add("@v_NIVELQUITAS", SqlDbType.Int).Value = valores("NivelQuitas")
            oraCommanVarios.Parameters.Add("@v_CoberturaCobranza", SqlDbType.NVarChar).Value = valores("CoberturaCobranza")
            'Session.Remove("CAT_LO_ID")
            If valores("esAgencia") Then
                oraCommanVarios.Parameters.Add("@v_isAgencia", SqlDbType.NVarChar).Value = 1
            Else
                oraCommanVarios.Parameters.Add("@v_isAgencia", SqlDbType.NVarChar).Value = 0
            End If

            Dim DtsUsuario As DataTable = Consulta_Procedure(oraCommanVarios, oraCommanVarios.CommandText)
            If DtsUsuario.TableName = "Exception" Then
                Throw New Exception(DtsUsuario.Rows(0).Item(0))
            ElseIf DtsUsuario.Columns.Contains("EXISTE") Then
                If DtsUsuario(0)("EXISTE") = 0 Then
                    Return ("Usuario '" & valores("usuario") & "' creado correctamente")
                Else
                    Return ("Usuario '" & valores("usuario") & "' actualizado correctamente")
                End If
            Else
                Return (DtsUsuario(0)(0).ToString)
            End If
            'AUDITORIA(tmpUSUARIO("CAT_LO_USUARIO"), "Administrador", IIf(bandera = 0, "Crear Usuario", "Actualizar Usuario"), "", bandera, valores("usuario"), "", "")

            SP.AUDITORIA_GLOBAL(0, TmpUSUARIO("CAT_LO_USUARIO"), "Modulo Administracion", "Usuario Guardado: " & valores("usuario"))

        Catch ex As Exception
            SendMail("guardar User", ex, "", "", TmpUSUARIO("CAT_LO_USUARIO"))
            Return ex.ToString.Replace(Chr(10), "").Replace(Chr(13), "").Replace("""", "").Replace("'", "")
        End Try

    End Function

End Class