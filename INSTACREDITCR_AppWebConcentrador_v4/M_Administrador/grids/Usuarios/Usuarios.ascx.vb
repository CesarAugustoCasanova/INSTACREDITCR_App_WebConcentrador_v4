Imports System.Data
Imports System.Data.OracleClient
Imports System.Data.SqlClient
Imports System.Web.Services
Imports Conexiones
Imports Db
Imports Funciones
Imports Telerik.Web.UI

Partial Class Usuarios
    Inherits System.Web.UI.UserControl
    Private _dataItem As Object = Nothing
    Public Property DataItem() As Object
        Get
            Return Me._dataItem
        End Get
        Set(ByVal value As Object)
            Me._dataItem = value
        End Set
    End Property
    Public Property tmpUSUARIO As IDictionary
        Get
            Return CType(Session("USUARIO"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("USUARIO") = value
        End Set
    End Property
    Private Sub Usuarios_Init(sender As Object, e As EventArgs) Handles Me.Init
        If Session("Edit") Then
            initDrops()
            initPwd()
        End If

        If Session("initInsert") Then
            Session.Remove("initInsert")
            lblFechaAlta.Text = "Fecha de alta: " + Now.ToShortDateString + ", " + Now.ToShortTimeString
            initDrops()
            initPwd()
        End If
    End Sub

    Private Sub initDrops()
        ddlAgencia.DataSource = getData(0)
        ddlAgencia.DataValueField = "Id"
        ddlAgencia.DataTextField = "Agencias"
        ddlAgencia.DataBind()

        ddlPerfil.DataSource = getData(3)
        ddlPerfil.DataValueField = "Id"
        ddlPerfil.DataTextField = "Nombre"
        ddlPerfil.DataBind()

        cbVerAgencias.DataSource = getData(0)
        cbVerAgencias.DataValueField = "Id"
        cbVerAgencias.DataTextField = "Agencias"
        cbVerAgencias.DataBind()

        rDdlCat_Lo_CoberturaCobranza.DataSource = getData(13)
        rDdlCat_Lo_CoberturaCobranza.DataValueField = "Cat_co_Region"
        rDdlCat_Lo_CoberturaCobranza.DataTextField = "Cat_co_Region"
        rDdlCat_Lo_CoberturaCobranza.DataBind()

        cbVerProductos.DataSource = getData(10)
        cbVerProductos.DataValueField = "V_PRODUCTO"
        cbVerProductos.DataTextField = "T_PRODUCTO"
        cbVerProductos.DataBind()

        RCBAppsBloqueadas.DataSource = getData(12)
        RCBAppsBloqueadas.DataValueField = "IDENTIFICADOR"
        RCBAppsBloqueadas.DataTextField = "NOMBRE"
        RCBAppsBloqueadas.DataBind()


        Try
            Dim agn As String = DataItem("AGENCIA")
            initSupervisor(agn)
        Catch ex As Exception
            Dim s As String = ex.Message
        End Try
    End Sub

    Private Sub initPwd()
        Dim SSCommandAgencias As New SqlCommand
        SSCommandAgencias.CommandText = "SP_ADD_POLITICA"
        SSCommandAgencias.CommandType = CommandType.StoredProcedure
        SSCommandAgencias.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 0
        Dim DtsVarios As DataTable = Consulta_Procedure(SSCommandAgencias, "Politicas")
        For Each row As DataRow In DtsVarios.Rows
            Select Case row("CAT_DESC")
                Case "Caracteres Especiales"
                    pwdSpecialLbl.Text = row("CAT_VALOR")
                Case "Longitud Minima"
                    pwdMinLongLbl.Text = row("CAT_VALOR")
                Case "Mayusculas"
                    pwdMayusLbl.Text = row("CAT_VALOR")
                Case "Minusculas"
                    pwdMinusLbl.Text = row("CAT_VALOR")
                Case "Numeros"
                    pwdNumsLbl.Text = row("CAT_VALOR")
            End Select
        Next
    End Sub

    Private Sub Llenar()
        Dim oraCommanVarios As New SqlCommand
        oraCommanVarios.CommandText = "SP_CREAR_USUARIO"
        oraCommanVarios.CommandType = CommandType.StoredProcedure
        oraCommanVarios.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 12
        oraCommanVarios.Parameters.Add("@v_USUARIO", SqlDbType.VarChar).Value = DataItem("USUARIO")
        Try
            Dim DtsVarios As DataTable = Consulta_Procedure(oraCommanVarios, "Usuarios")
            lblFechaAlta.Text = DtsVarios(0)("CAT_LO_DTEALTA")
            Session("CAT_LO_ID") = DtsVarios(0)("CAT_LO_ID")

            txtNombre.Text = DtsVarios(0)("CAT_LO_NOMBRE").ToString
            txtUsuario.Text = DataItem("USUARIO").ToString
            txtUsuario.Enabled = False
            dtpHoraEntrada.DbSelectedDate = DtsVarios(0)("CAT_LO_HENTRADA") & ":00:00"
            dtpHoraSalida.DbSelectedDate = DtsVarios(0)("CAT_LO_HSALIDA") & ":00:00"
            ddlAgencia.SelectedValue = DtsVarios(0)("CAT_LO_AGENCIA").ToString
            Dim estatus As String = DtsVarios(0)("CAT_LO_ESTATUS").ToString
            ddlEstatus.SelectedValue = estatus
            rChbxInterno.Checked = If(DtsVarios(0)("CAT_LO_INTERNOEXTERNO").ToString = "1", True, False)
            rDdlInstancia.SelectedValue = DtsVarios(0)("CAT_LO_INSTANCIA").ToString
            If estatus = "Cancelado" Then
                LblMotivo.Visible = True
                txtMotivo.Visible = True
                txtMotivo.Text = DtsVarios(0)("CAT_LO_MOTIVO").ToString
            End If
            ddlPerfil.SelectedValue = DtsVarios(0)("CAT_LO_PERFIL")

            txtContrasena.Text = DtsVarios(0)("CAT_LO_CONTRASENA")
            txtRepiteContrasena.Text = DtsVarios(0)("CAT_LO_CONTRASENA")
            LBLoldpassword.Text = DtsVarios(0)("CAT_LO_CONTRASENA")
            TxtCat_Lo_EMail.Text = DtsVarios(0).Item("CAT_LO_EMAIL")
            TxtCat_Lo_NumEmpleado.Text = DtsVarios(0).Item("Cat_Lo_NumEmpleado")

            'CBProductos.SelectedValue = CBProductos.FindItemByText(DtsVarios(0)("CAT_LO_PRODUCTO")).Value
            Dim productos As String() = Split(DtsVarios(0)("CAT_LO_PRODUCTO"), ",")
            For i As Integer = 0 To productos.Length - 1
                Try
                    Dim item1 As RadComboBoxItem = New RadComboBoxItem()
                    item1 = cbVerProductos.Items.FindItemByValue(productos(i))
                    item1.Checked = True
                Catch ex As Exception
                End Try
            Next


            CBAgencia.Checked = IIf(DtsVarios(0)("CAT_LO_SUCURSAL") = "0", False, True)

            Dim agencias As String() = Split(DtsVarios(0)("CAT_LO_AGENCIASVER"), ",")
            For i As Integer = 0 To agencias.Length - 1
                Try
                    Dim item1 As RadComboBoxItem = New RadComboBoxItem()
                    item1 = cbVerAgencias.Items.FindItemByValue(agencias(i))
                    item1.Checked = True
                Catch ex As Exception
                End Try
            Next

            Dim sql As New SqlCommand
            sql.CommandText = "Sp_Llenar_Usuario"
            sql.CommandType = CommandType.StoredProcedure
            sql.Parameters.Add("@V_Agencia", SqlDbType.NVarChar).Value = DtsVarios(0)("CAT_LO_AGENCIA")
            sql.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = 2
            Dim dt As DataTable = Consulta_Procedure(sql, "Usuarios")
            ddlSupervisor.DataSource = dt
            ddlSupervisor.DataValueField = "Id"
            ddlSupervisor.DataTextField = "Nombre"
            ddlSupervisor.DataBind()
            ddlSupervisor.SelectedText = DtsVarios(0)("CAT_LO_SUPERVISOR")


            rDdlTipoUsuario.SelectedText = DtsVarios(0)("CAT_LO_TIPO_USR")
            TxtMeta.Text = DtsVarios(0)("CAT_LO_META")
            rTxtCapacidadCuentas.Text = DtsVarios(0)("CAT_LO_CAPACIDAD_CUENTAS")
            Dim apps As String() = Split(DtsVarios(0)("CAT_LO_APPS_DENEGADAS").ToString, ",")
            For Each app In apps
                Try
                    Dim item = RCBAppsBloqueadas.Items.FindItemByValue(app)
                    If item IsNot Nothing Then
                        item.Checked = True
                    End If
                Catch
                End Try
            Next

            Dim iCCs As String() = Split(DtsVarios(0).Item("CAT_LO_CoberturaCobranza").ToString, ",")
            Dim CoberturaCobranza As String() = Split(DtsVarios(0).Item("CAT_LO_CoberturaCobranza").ToString, ",")
            For Each iCC In iCCs
                Try
                    Dim item = rDdlCat_Lo_CoberturaCobranza.Items.FindItemByValue(iCC)
                    If item IsNot Nothing Then
                        item.Checked = True
                    End If
                Catch
                End Try
            Next

            Try
                dllNivelQuitas.SelectedValue = DtsVarios(0).Item("CAT_LO_NIVELQUITAS")
            Catch ex As Exception

            End Try

        Catch ex As Exception
            Dim V_ERRRO As String = ex.Message
            EnviarCorreo("Administrador", "Usuarios.ascx", "Llenar()", ex, "", "", tmpUSUARIO("CAT_LO_USUARIO"))
        End Try

    End Sub

    Private Sub ddlEstatus_SelectedIndexChanged(sender As Object, e As DropDownListEventArgs) Handles ddlEstatus.SelectedIndexChanged
        LblMotivo.Visible = (e.Text = "Cancelado")
        txtMotivo.Visible = (e.Text = "Cancelado")
        txtMotivo.Text = ""
    End Sub

    Function getData(v_bandera As String, Optional usuario As String = "") As DataTable
        Dim oraCommanVarios As New SqlCommand
        oraCommanVarios.CommandText = "Sp_Llenar_Usuario"
        oraCommanVarios.CommandType = CommandType.StoredProcedure
        oraCommanVarios.Parameters.Add("@V_Usuario", SqlDbType.NVarChar).Value = usuario
        oraCommanVarios.Parameters.Add("@V_Agencia", SqlDbType.NVarChar).Value = ""
        oraCommanVarios.Parameters.Add("@V_Producto", SqlDbType.NVarChar).Value = ""
        oraCommanVarios.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = v_bandera
        Dim DtsVarios As DataTable = Consulta_Procedure(oraCommanVarios, "Usuarios")
        Return DtsVarios
    End Function

    Private Sub ddlAgencia_SelectedIndexChanged(sender As Object, e As DropDownListEventArgs) Handles ddlAgencia.SelectedIndexChanged
        initSupervisor(e.Value)
    End Sub

    Private Sub initSupervisor(ByVal v_valor As String)
        Dim oraCommanVarios As New SqlCommand
        oraCommanVarios.CommandText = "Sp_Llenar_Usuario"
        oraCommanVarios.CommandType = CommandType.StoredProcedure
        oraCommanVarios.Parameters.Add("V_Usuario", SqlDbType.NVarChar).Value = ""
        oraCommanVarios.Parameters.Add("V_Agencia", SqlDbType.NVarChar).Value = v_valor
        oraCommanVarios.Parameters.Add("V_Producto", SqlDbType.NVarChar).Value = ""
        oraCommanVarios.Parameters.Add("V_Bandera", SqlDbType.NVarChar).Value = 2

        Dim DtsVarios As DataTable = Consulta_Procedure(oraCommanVarios, "Usuarios")
        ddlSupervisor.DataSource = DtsVarios
        ddlSupervisor.DataValueField = "Id"
        ddlSupervisor.DataTextField = "Nombre"
        ddlSupervisor.DataBind()

    End Sub

    Private Sub Usuarios_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        If Session("Edit") Then
            Session.Remove("Edit")
            Llenar()
        End If
    End Sub
End Class