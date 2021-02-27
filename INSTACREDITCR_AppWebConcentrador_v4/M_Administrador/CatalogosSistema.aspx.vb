Imports System.Data
Imports System.Data.SqlClient
Imports Db
Imports Funciones
Imports Telerik.Web.UI

Partial Class CatalogosSistema
    Inherits System.Web.UI.Page
    Public Property TmpUSUARIO As IDictionary
        Get
            Return CType(Session("USUARIO"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("USUARIO") = value
        End Set
    End Property
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("USUARIO") IsNot Nothing Then
            HidenUrs.Value = TmpUSUARIO("CAT_LO_USUARIO")
        Else
            OffLine(HidenUrs.Value)
            AUDITORIA(HidenUrs.Value, "Administrador", "CatalogosSistemas", "", "Page_Load", "", Request.ServerVariables("REMOTE_ADDR"), Request.ServerVariables("REMOTE_HOST"))
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/SesionExpirada.aspx")
        End If
    End Sub
    Private Sub CatalogosSistema_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        If Not IsPostBack Then
            UPNLCapacidad.Visible = False
            UPNLInstancia.Visible = False
            UPNLDominios.Visible = False
            UPNLPagos.Visible = False
            UPNLParentesco.Visible = False
            UPNLHolguras.Visible = False
        End If
    End Sub
    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Administrador", "CatalogosSistemas.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub
    Function Llenar(V_Cat_Id As Integer, V_Cat_Descripcion As String, V_Catalogo As String, V_Bandera As String) As DataTable
        Dim DtsVarios As DataTable
        Try
            Dim SSCommandAgencias As New SqlCommand
            SSCommandAgencias.CommandText = "Sp_Add_Catalogos"
            SSCommandAgencias.CommandType = CommandType.StoredProcedure
            SSCommandAgencias.Parameters.Add("@V_Cat_Id", SqlDbType.Decimal).Value = V_Cat_Id
            SSCommandAgencias.Parameters.Add("@V_Cat_Descripcion", SqlDbType.NVarChar).Value = V_Cat_Descripcion
            SSCommandAgencias.Parameters.Add("@V_Catalogo", SqlDbType.NVarChar).Value = V_Catalogo
            SSCommandAgencias.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = V_Bandera
            If V_Catalogo = "CAT_PERFILES" Then
                DtsVarios = Consulta_Procedure(SSCommandAgencias, "Perfiles")
                Dim DtvVarios As DataView = DtsVarios.DefaultView
            ElseIf V_Catalogo = "CAT_PRODUCTOS" Then
                DtsVarios = Consulta_Procedure(SSCommandAgencias, "Productos")
                Dim DtvVarios As DataView = DtsVarios.DefaultView
            ElseIf V_Catalogo = "CAT_PRODUCTOS_BIENESTAR" Then
                DtsVarios = Consulta_Procedure(SSCommandAgencias, "ProductosBienestar")
                Dim DtvVarios As DataView = DtsVarios.DefaultView
            ElseIf V_Catalogo = "CAT_LUGAR_PAGO" Then
                DtsVarios = Consulta_Procedure(SSCommandAgencias, "Pagos")
                Dim DtvVarios As DataView = DtsVarios.DefaultView
            ElseIf V_Catalogo = "CAT_GRUPO_REPORTES" Then
                DtsVarios = Consulta_Procedure(SSCommandAgencias, "Reportes")
                Dim DtvVarios As DataView = DtsVarios.DefaultView
            ElseIf V_Catalogo = "CAT_VARIABLES" Then
                DtsVarios = Consulta_Procedure(SSCommandAgencias, "CuentasContables")
                Dim DtvVarios As DataView = DtsVarios.DefaultView
            ElseIf V_Catalogo = "CAT_CAPACIDAD" Then
                DtsVarios = Consulta_Procedure(SSCommandAgencias, "capacidad")
                Dim DtvVarios As DataView = DtsVarios.DefaultView
            ElseIf V_Catalogo = "CAT_INSTANCIAS" Then
                DtsVarios = Consulta_Procedure(SSCommandAgencias, "instancias")
                Dim DtvVarios As DataView = DtsVarios.DefaultView
            ElseIf V_Catalogo = "CAT_DOMINIOS_SEG" Then
                DtsVarios = Consulta_Procedure(SSCommandAgencias, "dominios")
                Dim DtvVarios As DataView = DtsVarios.DefaultView
            ElseIf V_Catalogo = "CAT_PARENTESCO" Then
                DtsVarios = Consulta_Procedure(SSCommandAgencias, "parentesco")
                Dim DtvVarios As DataView = DtsVarios.DefaultView
            End If

            Return DtsVarios
        Catch ex As Exception
            Aviso(ex.Message)
        End Try

    End Function

    Function Llenar2(V_Cat_Id As String, V_Cat_Descripcion As String, V_Catalogo As String, V_Bandera As String) As DataTable
        Dim DtsVarios As DataTable
        Try
            Dim SSCommandAgencias As New SqlCommand
            SSCommandAgencias.CommandText = "Sp_Add_Catalogos"
            SSCommandAgencias.CommandType = CommandType.StoredProcedure
            SSCommandAgencias.Parameters.Add("@V_Cat_Id", SqlDbType.NVarChar).Value = 0
            SSCommandAgencias.Parameters.Add("@V_Cat_Id2", SqlDbType.NVarChar).Value = V_Cat_Id
            SSCommandAgencias.Parameters.Add("@V_Cat_Descripcion", SqlDbType.NVarChar).Value = V_Cat_Descripcion
            SSCommandAgencias.Parameters.Add("@V_Catalogo", SqlDbType.NVarChar).Value = V_Catalogo
            SSCommandAgencias.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = V_Bandera
            If V_Catalogo = "CAT_PERFILES" Then
                DtsVarios = Consulta_Procedure(SSCommandAgencias, "Perfiles")
                Dim DtvVarios As DataView = DtsVarios.DefaultView
            ElseIf V_Catalogo = "CAT_PRODUCTOS" Then
                DtsVarios = Consulta_Procedure(SSCommandAgencias, "Productos")
                Dim DtvVarios As DataView = DtsVarios.DefaultView
            ElseIf V_Catalogo = "CAT_PRODUCTOS_BIENESTAR" Then
                DtsVarios = Consulta_Procedure(SSCommandAgencias, "ProductosBienestar")
                Dim DtvVarios As DataView = DtsVarios.DefaultView
            ElseIf V_Catalogo = "CAT_LUGAR_PAGO" Then
                DtsVarios = Consulta_Procedure(SSCommandAgencias, "Pagos")
                Dim DtvVarios As DataView = DtsVarios.DefaultView
            ElseIf V_Catalogo = "CAT_GRUPO_REPORTES" Then
                DtsVarios = Consulta_Procedure(SSCommandAgencias, "Reportes")
                Dim DtvVarios As DataView = DtsVarios.DefaultView
            ElseIf V_Catalogo = "CAT_CAPACIDAD" Then
                DtsVarios = Consulta_Procedure(SSCommandAgencias, "capacidad")
                Dim DtvVarios As DataView = DtsVarios.DefaultView
            ElseIf V_Catalogo = "CAT_INSTANCIAS" Then
                DtsVarios = Consulta_Procedure(SSCommandAgencias, "instancias")
                Dim DtvVarios As DataView = DtsVarios.DefaultView
            ElseIf V_Catalogo = "CAT_DOMINIOS_SEG" Then
                DtsVarios = Consulta_Procedure(SSCommandAgencias, "dominios")
                Dim DtvVarios As DataView = DtsVarios.DefaultView
            ElseIf V_Catalogo = "CAT_PARENTESCO" Then
                DtsVarios = Consulta_Procedure(SSCommandAgencias, "parentesco")
                Dim DtvVarios As DataView = DtsVarios.DefaultView
            End If

            Return DtsVarios
        Catch ex As Exception
            Aviso(ex.Message)
        End Try

    End Function

    Protected Sub DdlTipo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlTipo.SelectedIndexChanged

        Select Case DdlTipo.SelectedValue

            Case "CAT_CAPACIDAD"
                UPNLHolguras.Visible = False
                UPNLParentesco.Visible = False
                UPNLPagos.Visible = False
                UPNLDominios.Visible = False
                UPNLInstancia.Visible = False
                UPNLCapacidad.Visible = True

                TxtMaxUsr.Text = traeCapacidadUSR()

                TxtMaxAGN.Text = traeCapacidadAGN()


            Case "CAT_INSTANCIAS"
                UPNLHolguras.Visible = False
                UPNLPagos.Visible = False
                UPNLCapacidad.Visible = False
                UPNLInstancia.Visible = True
                UPNLDominios.Visible = False
                UPNLParentesco.Visible = False

            Case "CAT_DOMINIOS_SEG"
                UPNLHolguras.Visible = False
                UPNLPagos.Visible = False
                UPNLInstancia.Visible = False
                UPNLCapacidad.Visible = False
                UPNLDominios.Visible = True
                UPNLParentesco.Visible = False

            Case "CAT_PARENTESCO"
                UPNLHolguras.Visible = False
                UPNLPagos.Visible = False
                UPNLCapacidad.Visible = False
                UPNLInstancia.Visible = False
                UPNLDominios.Visible = False
                UPNLParentesco.Visible = True

            Case "CAT_LUGAR_PAGO"
                UPNLHolguras.Visible = False
                UPNLPagos.Visible = True
                UPNLInstancia.Visible = False
                UPNLCapacidad.Visible = False
                UPNLDominios.Visible = False
                UPNLParentesco.Visible = False

            Case "CAT_GRUPO_REPORTES"
                'UPNLReportes.Visible = True

                'UPNLProductos.Visible = False

                'UPNLPagos.Visible = False

                'UPNLCuentaContable.Visible = False

                'UPNLVigenciaPrereestructura.Visible = False

                'UPNLDACION.Visible = False
            Case "CAT_VARIABLES"
                UPNLHolguras.Visible = True
                UPNLPagos.Visible = False
                UPNLCapacidad.Visible = False
                UPNLInstancia.Visible = False
                UPNLDominios.Visible = False
                UPNLParentesco.Visible = False

                TxtHolguraFecha.Text = traeHolguraFecha()

                TxtHolguraMonto.Text = traeHolguraMonto()

            Case "CAT_PRE"
                'UPNLReportes.Visible = False

                'UPNLProductos.Visible = False

                'UPNLPagos.Visible = False

                'UPNLCuentaContable.Visible = False

                'UPNLVigenciaPrereestructura.Visible = True

                'UPNLDACION.Visible = False

                'TxtVigenciaPre.Text = traevigencia()

            Case "CAT_DAC"
                'UPNLReportes.Visible = False

                'UPNLProductos.Visible = False

                'UPNLPagos.Visible = False

                'UPNLCuentaContable.Visible = False

                'UPNLVigenciaPrereestructura.Visible = False

                'UPNLDACION.Visible = True

                'TxtDacion.Text = traedacion()
            Case Else
                UPNLPagos.Visible = False
                UPNLCapacidad.Visible = False
                UPNLInstancia.Visible = False
                UPNLDominios.Visible = False
                UPNLParentesco.Visible = False
                UPNLHolguras.Visible = False

        End Select


    End Sub

    Protected Sub BtnAceptarConfirmacion_Click(sender As Object, e As EventArgs) Handles BtnAceptarConfirmacion.Click

    End Sub

    Protected Sub Aviso(ByVal MSJ As String)
        RadAviso.RadAlert(MSJ, 440, 155, "AVISO", Nothing)
    End Sub
    Protected Sub Confirma(ByVal MSJ As String)
        RadAviso.RadConfirm(MSJ, "confirmCallbackFn", 440, 155, Nothing, "Alerta")
    End Sub
    Public Sub RGVLugarPago_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RGVLugarPago.NeedDataSource
        Me.RGVLugarPago.DataSource = GetDataTablePago()
    End Sub

    Public Sub RadGInsEx_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RadGInsEx.NeedDataSource
        Me.RadGInsEx.DataSource = GetDataTableInstancia()
    End Sub

    Public Sub RGDominiosEx_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RGDominiosEx.NeedDataSource
        Me.RGDominiosEx.DataSource = GetDataTableDominios()
    End Sub

    Public Sub RGParentesco_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RGParentesco.NeedDataSource
        Me.RGParentesco.DataSource = GetDataTableParentesco()
    End Sub


    Public Function GetDataTablePerfil() As DataTable
        Dim table1 As New DataTable

        table1 = Llenar(0, "", "CAT_PERFILES", 0)
        Return table1
    End Function
    Public Function GetDataTableProductoBienestar() As DataTable
        Dim table1 As New DataTable

        table1 = Llenar(0, "", "CAT_PRODUCTOS_BIENESTAR", 0)
        Return table1
    End Function
    Public Function GetDataTableParentesco() As DataTable
        Dim table1 As New DataTable

        table1 = Llenar(0, "", "CAT_PARENTESCO", 0)
        Return table1
    End Function
    Public Function GetDataTableProducto() As DataTable
        Dim table1 As New DataTable

        table1 = Llenar(0, "", "CAT_PRODUCTOS", 0)
        Return table1
    End Function
    Public Function GetDataTableInstancia() As DataTable
        Dim table1 As New DataTable

        table1 = Llenar(0, "", "CAT_INSTANCIAS", 0)
        Return table1
    End Function
    Public Function GetDataTablePago() As DataTable
        Dim table1 As New DataTable

        table1 = Llenar(0, "", "CAT_LUGAR_PAGO", 0)
        Return table1
    End Function
    Public Function GetDataTableDominios() As DataTable
        Dim table1 As New DataTable

        table1 = Llenar(0, "", "CAT_DOMINIOS_SEG", 0)
        Return table1
    End Function
    Public Function GetDataTableReporte() As DataTable
        Dim table1 As New DataTable

        table1 = Llenar(0, "", "CAT_GRUPO_REPORTES", 0)
        Return table1
    End Function
    Public Function traecuentacontable() As String
        Return Llenar(0, "", "CAT_VARIABLES", 8).Rows(0).Item("VALOR")
    End Function

    Public Function traevigencia() As String
        Return Llenar(0, "", "CAT_VARIABLES", 10).Rows(0).Item("VALOR")
    End Function
    Public Function traeCapacidadUSR() As String
        Return Llenar(0, "", "CAT_VARIABLES", 14).Rows(0).Item("VALOR")
    End Function
    Public Function traeCapacidadAGN() As String
        Return Llenar(0, "", "CAT_VARIABLES", 15).Rows(0).Item("VALOR")
    End Function
    Public Function traeHolguraFecha() As String
        Return Llenar(0, "", "CAT_VARIABLES", 18).Rows(0).Item("VALOR")
    End Function
    Public Function traeHolguraMonto() As String
        Return Llenar(0, "", "CAT_VARIABLES", 19).Rows(0).Item("VALOR")
    End Function
    Public Function traedacion() As String
        Return Llenar(0, "", "CAT_VARIABLES", 12).Rows(0).Item("VALOR")
    End Function

    Private Sub BtnGrdCapacidad_Click(sender As Object, e As EventArgs) Handles BtnGrdCapacidad.Click

        Dim SSCommandAgencias As New SqlCommand
        SSCommandAgencias.CommandText = "SP_ADD_CATALOGOS"
        SSCommandAgencias.CommandType = CommandType.StoredProcedure
        SSCommandAgencias.Parameters.Add("@V_Cat_Id", SqlDbType.Decimal).Value = 0
        SSCommandAgencias.Parameters.Add("@V_Cat_Descripcion", SqlDbType.NVarChar).Value = TxtMaxUsr.Text
        SSCommandAgencias.Parameters.Add("@V_Catalogo", SqlDbType.NVarChar).Value = DdlTipo.SelectedValue
        SSCommandAgencias.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 16
        Dim DtsVarios As DataTable = Consulta_Procedure(SSCommandAgencias, "Catalogo")

        Dim SSCommandAgencias2 As New SqlCommand
        SSCommandAgencias2.CommandText = "SP_ADD_CATALOGOS"
        SSCommandAgencias2.CommandType = CommandType.StoredProcedure
        SSCommandAgencias2.Parameters.Add("@V_Cat_Id", SqlDbType.Decimal).Value = 0
        SSCommandAgencias2.Parameters.Add("@V_Cat_Descripcion", SqlDbType.NVarChar).Value = TxtMaxAGN.Text
        SSCommandAgencias2.Parameters.Add("@V_Catalogo", SqlDbType.NVarChar).Value = DdlTipo.SelectedValue
        SSCommandAgencias2.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 17
        Dim DtsVarios2 As DataTable = Consulta_Procedure(SSCommandAgencias2, "Catalogo")

        Aviso("Capacidades actualizadas")

        AUDITORIA(TmpUSUARIO("CAR_LO_USUARIO"), "Administrador", "CATALOGOS SISTEMA", "", "Cambio Limites", "usr " & TxtMaxUsr.Text & "- Agencia" & TxtMaxAGN.Text, "", "")

    End Sub
    Private Sub BtnGrdHolguras_Click(sender As Object, e As EventArgs) Handles BtnGrdHolguras.Click

        Dim SSCommandHolguras As New SqlCommand
        SSCommandHolguras.CommandText = "SP_ADD_CATALOGOS"
        SSCommandHolguras.CommandType = CommandType.StoredProcedure
        SSCommandHolguras.Parameters.Add("@V_Cat_Id", SqlDbType.Decimal).Value = 0
        SSCommandHolguras.Parameters.Add("@V_Cat_Descripcion", SqlDbType.NVarChar).Value = TxtHolguraFecha.Text
        SSCommandHolguras.Parameters.Add("@V_Catalogo", SqlDbType.NVarChar).Value = DdlTipo.SelectedValue
        SSCommandHolguras.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 20
        Dim DtsVarios As DataTable = Consulta_Procedure(SSCommandHolguras, "Catalogo")

        Dim SSCommandHolguras2 As New SqlCommand
        SSCommandHolguras2.CommandText = "SP_ADD_CATALOGOS"
        SSCommandHolguras2.CommandType = CommandType.StoredProcedure
        SSCommandHolguras2.Parameters.Add("@V_Cat_Id", SqlDbType.Decimal).Value = 0
        SSCommandHolguras2.Parameters.Add("@V_Cat_Descripcion", SqlDbType.NVarChar).Value = TxtHolguraMonto.Text
        SSCommandHolguras2.Parameters.Add("@V_Catalogo", SqlDbType.NVarChar).Value = DdlTipo.SelectedValue
        SSCommandHolguras2.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 21
        Dim DtsVarios2 As DataTable = Consulta_Procedure(SSCommandHolguras2, "Catalogo")

        Aviso("Holguras actualizadas")

        AUDITORIA(TmpUSUARIO("CAR_LO_USUARIO"), "Administrador", "CATALOGOS SISTEMA", "", "Cambio Limites", "Holgura Fecha " & TxtHolguraFecha.Text & "- Holgura Monto" & TxtHolguraMonto.Text, "", "")

    End Sub


    Private Sub RGVLugarPago_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RGVLugarPago.ItemCommand
        If e.CommandName = "Update" Then
            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(2) As String


            valores(0) = CType(MyUserControl.FindControl("TxtDescrpcion"), RadTextBox).Text
            valores(1) = CType(MyUserControl.FindControl("LblId"), RadLabel).Text

            If valores(0) = "" Then
                Aviso("CAPTURA UNA DESCRIPCIÓN")
                e.Canceled = True
            Else

                Try
                    Llenar(valores(1), valores(0), DdlTipo.SelectedValue, 2)
                    Aviso("Registro Actualizado")
                Catch ex As Exception

                    Aviso("No Fue Posible Actualizar. Razon: " + ex.Message)
                    e.Canceled = True
                End Try
            End If
        ElseIf e.CommandName = "PerformInsert" Then
            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(2) As String
            Dim newRow As DataRow = Me.Pagos.NewRow

            valores(0) = CType(MyUserControl.FindControl("TxtDescrpcion"), RadTextBox).Text
            valores(1) = CType(MyUserControl.FindControl("LblId"), RadLabel).Text
            If valores(0) = "" Then
                Aviso("CAPTURA UNA DESCRIPCIÓN")
                e.Canceled = True
            Else
                Dim SSCommandAgencias As New SqlCommand
                SSCommandAgencias.CommandText = "Sp_Add_Catalogos"
                SSCommandAgencias.CommandType = CommandType.StoredProcedure
                SSCommandAgencias.Parameters.Add("@V_Cat_Id", SqlDbType.Decimal).Value = 0
                SSCommandAgencias.Parameters.Add("@V_Cat_Descripcion", SqlDbType.NVarChar).Value = valores(0)
                SSCommandAgencias.Parameters.Add("@V_Catalogo", SqlDbType.NVarChar).Value = DdlTipo.SelectedValue
                SSCommandAgencias.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 4
                Dim DtsVarios As DataTable = Consulta_Procedure(SSCommandAgencias, "Catalogo")
                If DtsVarios.Rows(0).Item("Cuantos") = 0 Then
                    Try
                        Llenar(1, valores(0), DdlTipo.SelectedValue, 1)
                        Aviso("Registro Exitoso")
                    Catch ex As Exception
                        Aviso("No Fue Posible Insertar El  Usuario. Razon: " + ex.Message)

                    End Try
                Else
                    Aviso("Campo ya existente, Valide")
                End If
            End If
        ElseIf e.CommandName = "Delete" Then
            Dim ID As String = (CType(e.Item, GridDataItem)).OwnerTableView.DataKeyValues(e.Item.ItemIndex)("Id").ToString

            Llenar(ID, "", DdlTipo.SelectedValue, 3)
            Aviso("Se Elimino el registro")
        End If
    End Sub
    Private Sub RGDominiosEx_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RGDominiosEx.ItemCommand
        If e.CommandName = "Update" Then
            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(2) As String


            valores(0) = CType(MyUserControl.FindControl("TxtDescrpcion"), RadTextBox).Text
            valores(1) = CType(MyUserControl.FindControl("LblId"), RadLabel).Text

            If valores(0) = "" Then
                Aviso("CAPTURA UNA DESCRIPCIÓN")
                e.Canceled = True
            Else

                Try
                    Llenar(valores(1), valores(0), DdlTipo.SelectedValue, 2)
                    Aviso("Registro Actualizado")
                Catch ex As Exception

                    Aviso("No Fue Posible Actualizar. Razon: " + ex.Message)
                    e.Canceled = True
                End Try
            End If
        ElseIf e.CommandName = "PerformInsert" Then
            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(2) As String
            Dim newRow As DataRow = Me.Pagos.NewRow

            valores(0) = CType(MyUserControl.FindControl("TxtDescrpcion"), RadTextBox).Text
            valores(1) = CType(MyUserControl.FindControl("LblId"), RadLabel).Text
            If valores(0) = "" Then
                Aviso("CAPTURA UNA DESCRIPCIÓN")
                e.Canceled = True
            Else
                Dim SSCommandAgencias As New SqlCommand
                SSCommandAgencias.CommandText = "Sp_Add_Catalogos"
                SSCommandAgencias.CommandType = CommandType.StoredProcedure
                SSCommandAgencias.Parameters.Add("@V_Cat_Id", SqlDbType.Decimal).Value = 0
                SSCommandAgencias.Parameters.Add("@V_Cat_Descripcion", SqlDbType.NVarChar).Value = valores(0)
                SSCommandAgencias.Parameters.Add("@V_Catalogo", SqlDbType.NVarChar).Value = DdlTipo.SelectedValue
                SSCommandAgencias.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 4
                Dim DtsVarios As DataTable = Consulta_Procedure(SSCommandAgencias, "Catalogo")
                If DtsVarios.Rows(0).Item("Cuantos") = 0 Then
                    Try
                        Llenar(1, valores(0), DdlTipo.SelectedValue, 1)
                        Aviso("Registro Exitoso")
                    Catch ex As Exception
                        Aviso("No Fue Posible Insertar El  Usuario. Razon: " + ex.Message)

                    End Try
                Else
                    Aviso("Campo ya existente, Valide")
                End If
            End If
        ElseIf e.CommandName = "Delete" Then
            Dim ID As String = (CType(e.Item, GridDataItem)).OwnerTableView.DataKeyValues(e.Item.ItemIndex)("Id").ToString

            Llenar(ID, "", DdlTipo.SelectedValue, 3)
            Aviso("Se Elimino el registro")
        End If
    End Sub
    Private Sub RGParentesco_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RGParentesco.ItemCommand
        If e.CommandName = "Update" Then
            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(2) As String


            valores(0) = CType(MyUserControl.FindControl("TxtDescrpcion"), RadTextBox).Text
            valores(1) = CType(MyUserControl.FindControl("LblId"), RadLabel).Text

            If valores(0) = "" Then
                Aviso("CAPTURA UNA DESCRIPCIÓN")
                e.Canceled = True
            Else

                Try
                    Llenar(valores(1), valores(0), DdlTipo.SelectedValue, 2)
                    Aviso("Registro Actualizado")
                Catch ex As Exception

                    Aviso("No Fue Posible Actualizar. Razon: " + ex.Message)
                    e.Canceled = True
                End Try
            End If
        ElseIf e.CommandName = "PerformInsert" Then
            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(2) As String
            Dim newRow As DataRow = Me.Pagos.NewRow

            valores(0) = CType(MyUserControl.FindControl("TxtDescrpcion"), RadTextBox).Text
            valores(1) = CType(MyUserControl.FindControl("LblId"), RadLabel).Text
            If valores(0) = "" Then
                Aviso("CAPTURA UNA DESCRIPCIÓN")
                e.Canceled = True
            Else
                Dim SSCommandAgencias As New SqlCommand
                SSCommandAgencias.CommandText = "Sp_Add_Catalogos"
                SSCommandAgencias.CommandType = CommandType.StoredProcedure
                SSCommandAgencias.Parameters.Add("@V_Cat_Id", SqlDbType.Decimal).Value = 0
                SSCommandAgencias.Parameters.Add("@V_Cat_Descripcion", SqlDbType.NVarChar).Value = valores(0)
                SSCommandAgencias.Parameters.Add("@V_Catalogo", SqlDbType.NVarChar).Value = DdlTipo.SelectedValue
                SSCommandAgencias.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 4
                Dim DtsVarios As DataTable = Consulta_Procedure(SSCommandAgencias, "Catalogo")
                If DtsVarios.Rows(0).Item("Cuantos") = 0 Then
                    Try
                        Llenar(1, valores(0), DdlTipo.SelectedValue, 1)
                        Aviso("Registro Exitoso")
                    Catch ex As Exception
                        Aviso("No Fue Posible Insertar El  Usuario. Razon: " + ex.Message)

                    End Try
                Else
                    Aviso("Campo ya existente, Valide")
                End If
            End If
        ElseIf e.CommandName = "Delete" Then
            Dim ID As String = (CType(e.Item, GridDataItem)).OwnerTableView.DataKeyValues(e.Item.ItemIndex)("Id").ToString

            Llenar(ID, "", DdlTipo.SelectedValue, 3)
            Aviso("Se Elimino el registro")
        End If
    End Sub
    Private Sub RadGInsEx_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RadGInsEx.ItemCommand
        If e.CommandName = "Update" Then
            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(2) As String


            valores(0) = CType(MyUserControl.FindControl("TxtDescrpcion"), RadTextBox).Text
            valores(1) = CType(MyUserControl.FindControl("LblId"), RadLabel).Text

            If valores(0) = "" Then
                Aviso("CAPTURA UNA DESCRIPCIÓN")
                e.Canceled = True
            Else

                Try
                    Llenar(valores(1), valores(0), DdlTipo.SelectedValue, 2)
                    Aviso("Registro Actualizado")
                Catch ex As Exception

                    Aviso("No Fue Posible Actualizar. Razon: " + ex.Message)
                    e.Canceled = True
                End Try
            End If
        ElseIf e.CommandName = "PerformInsert" Then
            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(2) As String
            Dim newRow As DataRow = Me.Pagos.NewRow

            valores(0) = CType(MyUserControl.FindControl("TxtDescrpcion"), RadTextBox).Text
            valores(1) = CType(MyUserControl.FindControl("LblId"), RadLabel).Text
            If valores(0) = "" Then
                Aviso("CAPTURA UNA DESCRIPCIÓN")
                e.Canceled = True
            Else
                Dim SSCommandAgencias As New SqlCommand
                SSCommandAgencias.CommandText = "Sp_Add_Catalogos"
                SSCommandAgencias.CommandType = CommandType.StoredProcedure
                SSCommandAgencias.Parameters.Add("@V_Cat_Id", SqlDbType.Decimal).Value = 0
                SSCommandAgencias.Parameters.Add("@V_Cat_Descripcion", SqlDbType.NVarChar).Value = valores(0)
                SSCommandAgencias.Parameters.Add("@V_Catalogo", SqlDbType.NVarChar).Value = DdlTipo.SelectedValue
                SSCommandAgencias.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 4
                Dim DtsVarios As DataTable = Consulta_Procedure(SSCommandAgencias, "Catalogo")
                If DtsVarios.Rows(0).Item("Cuantos") = 0 Then
                    Try
                        Llenar(1, valores(0), DdlTipo.SelectedValue, 1)
                        Aviso("Registro Exitoso")
                    Catch ex As Exception
                        Aviso("No Fue Posible Insertar El  Usuario. Razon: " + ex.Message)

                    End Try
                Else
                    Aviso("Campo ya existente, Valide")
                End If
            End If
        ElseIf e.CommandName = "Delete" Then
            Dim ID As String = (CType(e.Item, GridDataItem)).OwnerTableView.DataKeyValues(e.Item.ItemIndex)("Id").ToString

            Llenar(ID, "", DdlTipo.SelectedValue, 3)
            Aviso("Se Elimino el registro")
        End If
    End Sub


    Public ReadOnly Property Perfil() As DataTable
        Get
            Dim obj As Object = Me.Session("Perfil")
            If (Not obj Is Nothing) Then
                Return CType(obj, DataTable)
            End If

            Dim myDataTable As DataTable = New DataTable
            myDataTable = GetDataTablePerfil()
            Me.Session("Perfil") = myDataTable
            Return myDataTable
        End Get
    End Property
    Public ReadOnly Property Productos() As DataTable
        Get
            Dim obj As Object = Me.Session("Usuarios")
            If (Not obj Is Nothing) Then
                Return CType(obj, DataTable)
            End If

            Dim myDataTable As DataTable = New DataTable
            myDataTable = GetDataTableProducto()
            Me.Session("Producto") = myDataTable
            Return myDataTable
        End Get
    End Property
    Public ReadOnly Property Pagos() As DataTable
        Get
            Dim obj As Object = Me.Session("Usuarios")
            If (Not obj Is Nothing) Then
                Return CType(obj, DataTable)
            End If

            Dim myDataTable As DataTable = New DataTable
            myDataTable = GetDataTablePago()
            Me.Session("Pagos") = myDataTable
            Return myDataTable
        End Get
    End Property

    Public ReadOnly Property Reportes() As DataTable
        Get
            Dim obj As Object = Me.Session("Usuarios")
            If (Not obj Is Nothing) Then
                Return CType(obj, DataTable)
            End If

            Dim myDataTable As DataTable = New DataTable
            myDataTable = GetDataTableReporte()
            Me.Session("Reportes") = myDataTable
            Return myDataTable
        End Get
    End Property
End Class
