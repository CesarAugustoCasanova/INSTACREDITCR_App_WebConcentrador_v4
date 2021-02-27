
Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports Db
Imports Class_Negociaciones


Partial Class Negociaciones


    Inherits System.Web.UI.UserControl
    Private _dataItem As Object = Nothing
    'Public DdlTabla As DropDownList = New DropDownList()
    Public DdlCampo As DropDownList = New DropDownList()
    'Public DdlOperador As DropDownList = New DropDownList()
    'Public TxtValores As TextBox = New TextBox()
    Public DdlConector As DropDownList = New DropDownList()
    'Public GvCatalogos As GridView
    'Public pnl As pnl
    'Public DatePick As RadDatePicker = New RadDatePicker()    ' Public'Calendario As AjaxControlToolkit.CalendarExtender
    'Public DdlTipo As DropDownList = New DropDownList()
    Private filtro As String
    Dim class_neg As Class_Negociaciones
    Private Sub Negociaciones_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Session("PostBack") = True Then
            Session("PostBack") = False

            Dim SSCommand2 As New SqlCommand
            SSCommand2.CommandText = "SP_CATALOGO_NEGO"
            SSCommand2.CommandType = CommandType.StoredProcedure
            SSCommand2.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 12
            Dim dtsDrops As DataTable = Consulta_Procedure(SSCommand2, "Campos_Drops")

            DdlCampoNego3.DataSource = dtsDrops
            DdlCampoNego3.DataBind()

            DdlCampoNego2.DataSource = dtsDrops
            DdlCampoNego2.DataBind()

            DdlCampoNego1.DataSource = dtsDrops
            DdlCampoNego1.DataBind()

            TxtID.Text = Session("ID_NEGO")
            Dim Negociacion As String = Session("Negociacion")
            If Session("Negociacion") <> "" Then
                Dim SSCommand1 As New SqlCommand
                SSCommand1.CommandText = "SP_CATALOGO_NEGO"
                SSCommand1.CommandType = CommandType.StoredProcedure
                'SSCommand1.Parameters.Add("@V_USUARIO", SqlDbType.NVarChar).Value = DesEncriptarCadena(Conexiones.StrConexion(1))
                SSCommand1.Parameters.Add("@V_VALOR", SqlDbType.NVarChar).Value = Negociacion
                SSCommand1.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 3
                Dim DtsLlenar As DataTable = Consulta_Procedure(SSCommand1, "Campos")
                TxtCAT_NE_DESC_MAX1.Text = DtsLlenar.Rows(0).Item("CAT_NE_DESC_MAX1")
                TxtCAT_NE_DESC_MAX2.Text = DtsLlenar.Rows(0).Item("CAT_NE_DESC_MAX2")
                TxtCAT_NE_DESC_MAX3.Text = DtsLlenar.Rows(0).Item("CAT_NE_DESC_MAX3")
                TxtCAT_NE_NUM_PAGOS.Text = DtsLlenar.Rows(0).Item("CAT_NE_NUM_PAG")
                NTxtNivel.Text = DtsLlenar.Rows(0).Item("CAT_NE_NIVEL")
                Try
                    DdlCampoNego3.SelectedValue = DtsLlenar.Rows(0).Item("CAT_NE_CAMPO_DESC_MAX3")
                Catch ex As Exception

                End Try

                Try
                    DdlCampoNego2.SelectedValue = DtsLlenar.Rows(0).Item("CAT_NE_CAMPO_DESC_MAX2")
                Catch ex As Exception

                End Try
                Try
                    DdlCampoNego1.SelectedValue = DtsLlenar.Rows(0).Item("CAT_NE_CAMPO_DESC_MAX1")
                Catch ex As Exception

                End Try
                Dim SSCommand As New SqlCommand
                SSCommand.CommandText = "SP_CATALOGO_NEGO"
                SSCommand.CommandType = CommandType.StoredProcedure
                ' SSCommand.Parameters.Add("@V_USUARIO", SqlDbType.NVarChar).Value = DesEncriptarCadena(Conexiones.StrConexion(1))
                SSCommand.Parameters.Add("@V_VALOR", SqlDbType.NVarChar).Value = Negociacion
                SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 4

                Dim DtsVariable As DataTable = Consulta_Procedure(SSCommand, "Campos")
                Dim Dtsgrid As DataTable = DtsVariable
                gridNego.DataSource = Dtsgrid
                gridNego.DataBind()
                Dim DtvVariable As DataView = DtsVariable.DefaultView


            Else
                Session("name_Nego") = ""
            End If
        End If

    End Sub



    Protected Sub LLENAR_DROP(ByVal Item As DropDownList, Tabla As String)
        Try
            'Item.Items.Clear()

            Dim SSCommand As New SqlCommand
            SSCommand.CommandText = "SP_CATALOGO_NEGO"
            SSCommand.CommandType = CommandType.StoredProcedure
            'SSCommand.Parameters.Add("@V_USUARIO", SqlDbType.NVarChar).Value = DesEncriptarCadena(Conexiones.StrConexion(1))
            SSCommand.Parameters.Add("@V_VALOR", SqlDbType.NVarChar).Value = Tabla
            SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 4

            Dim DtsVariable As DataTable = Consulta_Procedure(SSCommand, "Campos")
            Item.DataTextField = "COMMENTS"
            Item.DataValueField = "COLUMN_NAME"
            Item.DataSource = DtsVariable
            Item.DataBind()
            Item.Items.Add("Seleccione")
            Item.SelectedValue = "Seleccione"
        Catch ex As Exception
            '    'MENSJAE.Text = ex.ToString
            '    'SendMail("LLENAR_DROP", ex, "", "", HidenUrs.Value)
        End Try
    End Sub

    Function SP_CATALOGO_NEGO(ByVal V_BANDERA As String, id As String, NOMBRE As String, TABLA As String, CAMPO As String, VALOR As String, OPERADOR As String, CONECTOR As String, DESCRIPCIONCAMPO As String, DESCRIPCIONTABLA As String, DESCRIPCIONCONECTOR As String, DESCRIPCIONOPERADOR As String, ORDEN As String, QUERY As String, DESC_MAX1 As String, CAMPO_DESC_MAX1 As String, DESC_MAX2 As String, CAMPO_DESC_MAX2 As String, DESC_MAX3 As String, CAMPO_DESC_MAX3 As String, NUM_PAG As String, ESTATUS As String, NIVEL As String) As DataTable
        Dim Cmmn As New SqlCommand
        Cmmn.CommandText = "SP_CATALOGO_NEGO"
        Cmmn.CommandType = CommandType.StoredProcedure
        Cmmn.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = V_BANDERA
        Cmmn.Parameters.Add("@v_ID", SqlDbType.NVarChar).Value = id
        Cmmn.Parameters.Add("@v_NOMBRE", SqlDbType.NVarChar).Value = NOMBRE
        Cmmn.Parameters.Add("@v_TABLA", SqlDbType.NVarChar).Value = TABLA
        Cmmn.Parameters.Add("@v_CAMPO", SqlDbType.NVarChar).Value = CAMPO
        Cmmn.Parameters.Add("@v_VALOR", SqlDbType.NVarChar).Value = VALOR
        Cmmn.Parameters.Add("@v_OPERADOR", SqlDbType.NVarChar).Value = OPERADOR
        Cmmn.Parameters.Add("@v_CONECTOR", SqlDbType.NVarChar).Value = CONECTOR
        Cmmn.Parameters.Add("@v_DESCRIPCIONCAMPO", SqlDbType.NVarChar).Value = DESCRIPCIONCAMPO
        Cmmn.Parameters.Add("@v_DESCRIPCIONTABLA", SqlDbType.NVarChar).Value = DESCRIPCIONTABLA
        Cmmn.Parameters.Add("@v_DESCRIPCIONCONECTOR", SqlDbType.NVarChar).Value = DESCRIPCIONCONECTOR
        Cmmn.Parameters.Add("@v_DESCRIPCIONOPERADOR", SqlDbType.NVarChar).Value = DESCRIPCIONOPERADOR
        Cmmn.Parameters.Add("@v_ORDEN", SqlDbType.NVarChar).Value = ORDEN
        Cmmn.Parameters.Add("@v_QUERY", SqlDbType.NVarChar).Value = QUERY
        Cmmn.Parameters.Add("@v_DESC_MAX1", SqlDbType.NVarChar).Value = DESC_MAX1
        Cmmn.Parameters.Add("@v_CAMPO_DESC_MAX1", SqlDbType.NVarChar).Value = CAMPO_DESC_MAX1
        Cmmn.Parameters.Add("@v_DESC_MAX2", SqlDbType.NVarChar).Value = DESC_MAX2
        Cmmn.Parameters.Add("@v_CAMPO_DESC_MAX2", SqlDbType.NVarChar).Value = CAMPO_DESC_MAX2
        Cmmn.Parameters.Add("@v_DESC_MAX3", SqlDbType.NVarChar).Value = DESC_MAX3
        Cmmn.Parameters.Add("@v_CAMPO_DESC_MAX3", SqlDbType.NVarChar).Value = CAMPO_DESC_MAX3
        Cmmn.Parameters.Add("@v_NUM_PAG", SqlDbType.NVarChar).Value = NUM_PAG
        Cmmn.Parameters.Add("@v_ESTATUS", SqlDbType.NVarChar).Value = ESTATUS
        Cmmn.Parameters.Add("@v_nivel", SqlDbType.NVarChar).Value = NIVEL

        Dim DtsDatos As DataTable = Consulta_Procedure(Cmmn, Cmmn.CommandText)
        Return DtsDatos
    End Function

    Function SP_CATALOGO_NEGO(ByVal V_BANDERA As String, ByVal V_id As String) As DataTable
        Dim Cmmn As New SqlCommand
        Cmmn.CommandText = "SP_CATALOGO_NEGO"
        Cmmn.CommandType = CommandType.StoredProcedure
        Cmmn.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = V_BANDERA
        Cmmn.Parameters.Add("@V_NOMBRE", SqlDbType.VarChar).Value = V_id
        Dim DtsDatos As DataTable = Consulta_Procedure(Cmmn, Cmmn.CommandText)
        Return DtsDatos
    End Function

    Private Sub gridNego_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles gridNego.NeedDataSource
        'If Session("Negociacion") = "" Then
        '    Dim DtsDatos As DataTable = SP_CATALOGO_NEGO(1, "")
        '    gridNego.DataSource = DtsDatos
        'ElseIf Session("name_nego") <> "" Then
        Dim DtsDatos As DataTable = SP_CATALOGO_NEGO(1, Session("name_Nego"))
        gridNego.DataSource = DtsDatos
        'End If
    End Sub
    Protected Sub Aviso(ByVal MSJ As String)
        RLBError.Text = MSJ.Replace("""", "").Replace("'", "").Replace(Chr(13), "").Replace(Chr(10), "")
    End Sub
    Function Valida(where As String) As String
        Dim SSCommandValidaQ As New SqlCommand
        SSCommandValidaQ.CommandText = "Sp_Valida_Query"
        SSCommandValidaQ.CommandType = CommandType.StoredProcedure
        SSCommandValidaQ.Parameters.Add("@V_Valor", SqlDbType.NVarChar).Value = where
        Dim DtsExiste As DataTable = Consulta_Procedure(SSCommandValidaQ, "Sp_Valida_Query")
        Return DtsExiste.Rows(0).Item("RESULTADO")
    End Function


    Private Sub gridNego_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles gridNego.ItemCommand
        Try
            Dim comando As String = e.CommandName
            If comando = "PerformInsert" Then
                Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
                Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)

                Dim valores As Hashtable = Reglas.getGridValues(MyUserControl)
                Dim abc As String = ""
                Dim DtsDatos As DataTable = SP_CATALOGO_NEGO(2, Session("ID_NEGO"), TxtNombre.Text, valores("tablaValue"), valores("campoValue"), valores("valor"), valores("operadorValue"), valores("conectorValue"), valores("campoText"), valores("tablaText"), valores("conectorText"), valores("operadorText"), Nothing, Nothing, TxtCAT_NE_DESC_MAX1.Text, DdlCampoNego1.SelectedValue, TxtCAT_NE_DESC_MAX2.Text, DdlCampoNego2.SelectedValue, TxtCAT_NE_DESC_MAX3.Text, DdlCampoNego3.SelectedValue, TxtCAT_NE_NUM_PAGOS.Text, Nothing, NTxtNivel.Text)
                If DtsDatos.TableName = "Exception" Then
                    Throw New Exception(DtsDatos.Rows(0).Item(0).ToString)
                End If
                Session("name_Nego") = DtsDatos.Rows(0)(0)
                    gridNego.Rebind()
                    'Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogo dispersión", "Insertar dispersion: " & DDLInstancia.SelectedValue & "|" & ddlsubclasificacion.SelectedValue & "|" & rDdlInterno.SelectedValue & "|" & Nothing & "|" & valores("operadorText") & "|" & valores("conectorText") & "|" & valores("tablaText") & "|" & valores("campoText") & "|" & valores("valor") & "|" & valores("campoValue") & "|" & valores("tablaValue") & "|" & valores("operadorValue") & "|" & valores("conectorValue") & "|" & valores("tipo") & "|" & Session("DispDUMMY"))
                    '
                    ElseIf comando = "Edit" Then
                Session("Edit") = True
                Session("name_Nego") = TxtNombre.Text
            ElseIf comando = "Update" Then
                Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
                Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
                Dim valores As Hashtable = Reglas.getGridValues(MyUserControl)
                Dim Cmmn As New SqlCommand
                Cmmn.CommandText = "SP_CATALOGO_NEGO"
                Cmmn.CommandType = CommandType.StoredProcedure
                Cmmn.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = 11
                Cmmn.Parameters.Add("@v_ID", SqlDbType.VarChar).Value = TxtID.Text
                Cmmn.Parameters.Add("@v_DESCRIPCIONTABLA", SqlDbType.VarChar).Value = valores("tablaText")
                Cmmn.Parameters.Add("@v_OPERADOR", SqlDbType.VarChar).Value = valores("operadorValue")
                Cmmn.Parameters.Add("@v_TABLA", SqlDbType.VarChar).Value = valores("tablaValue")
                Cmmn.Parameters.Add("@v_DESCRIPCIONOPERADOR", SqlDbType.VarChar).Value = valores("operadorText")
                Cmmn.Parameters.Add("@v_CONECTOR", SqlDbType.VarChar).Value = valores("conectorValue")
                Cmmn.Parameters.Add("@v_VALOR", SqlDbType.VarChar).Value = valores("valor")
                Cmmn.Parameters.Add("@v_ORDEN", SqlDbType.VarChar).Value = valores("consecutivo")
                Cmmn.Parameters.Add("@v_DESCRIPCIONCAMPO", SqlDbType.VarChar).Value = valores("campoText")
                Cmmn.Parameters.Add("@v_CAMPO", SqlDbType.VarChar).Value = valores("campoValue")
                Cmmn.Parameters.Add("@v_DESCRIPCIONCONECTOR", SqlDbType.VarChar).Value = valores("conectorText")
                Dim DtsDatos As DataTable = Consulta_Procedure(Cmmn, Cmmn.CommandText)
                If DtsDatos.TableName = "Exception" Then
                    Throw New Exception(DtsDatos.Rows(0).Item(0).ToString)
                End If
                gridNego.Rebind()
                'ActualizarParametro(valores("operadorText"), valores("conectorText"), valores("tablaText"), valores("campoText"), valores("valor"), valores("campoValue"), valores("tablaValue"), valores("operadorValue"), valores("conectorValue"), valores("tipo"), Session("DispDUMMY"), valores("consecutivo"))
                'asignaUsusarios()
                'Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogo dispersión", "Actualizar dispersion: " & valores("operadorText") & "|" & valores("conectorText") & "|" & valores("tablaText") & "|" & valores("campoText") & "|" & valores("valor") & "|" & valores("campoValue") & "|" & valores("tablaValue") & "|" & valores("operadorValue") & "|" & valores("conectorValue") & "|" & valores("tipo") & "|" & Session("DispDUMMY") & "|" & valores("consecutivo"))
                'gridDispersion.Rebind()
                Aviso("Dispersion Actualizada")
            ElseIf comando = "onDelete" Then
                Dim valores(7) As String

                valores(1) = e.Item.Cells.Item(3).Text
                Dim Cmmn As New SqlCommand
                Cmmn.CommandText = "SP_CATALOGO_NEGO"
                Cmmn.CommandType = CommandType.StoredProcedure
                Cmmn.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = 10
                Cmmn.Parameters.Add("@v_ID", SqlDbType.VarChar).Value = TxtID.Text
                Cmmn.Parameters.Add("@v_ORDEN", SqlDbType.VarChar).Value = valores(1)
                Dim DtsDatos As DataTable = Consulta_Procedure(Cmmn, Cmmn.CommandText)
                If DtsDatos.TableName = "Exception" Then
                    Throw New Exception(DtsDatos.Rows(0).Item(0).ToString)
                End If
                Session("name_Nego") = TxtNombre.Text
                gridNego.Rebind()
                'valores(2) = e.Item.Cells.Item(4).Text
                'valores(3) = e.Item.Cells.Item(5).Text
                'valores(4) = e.Item.Cells.Item(6).Text
                'valores(5) = e.Item.Cells.Item(7).Text
                'valores(6) = e.Item.Cells.Item(8).Text
                'BorrarParametro(valores(0), ddlsubclasificacion.SelectedValue, valores(1), valores(4), valores(6), valores(2), valores(3), valores(5), Session("DispDUMMY"))
                'Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogo dispersión", "Borrar dispersion: " & valores(0) & "|" & ddlsubclasificacion.SelectedValue & "|" & valores(1) & "|" & valores(4) & "|" & valores(6) & "|" & valores(2) & "|" & valores(3) & "|" & valores(5) & "|" & Session("DispDUMMY"))
                'gridDispersion.Rebind()
                Aviso("Dispersion Actualizada")
            ElseIf e.CommandName = "InitInsert" Or e.CommandName = "Cancel" Then
                Session("name_Nego") = TxtNombre.Text
            End If
        Catch ex As Exception
            e.Canceled = True
            Aviso(ex.Message)
        End Try
    End Sub



    'Private Sub Btnconfirmar_Click(sender As Object, e As EventArgs) Handles Btnconfirmar.Click
    '    filtroconsulta()

    '    If TxtNombre.Text = "" Then
    '        Aviso("Escriba Un Nombre Para La Negociación")
    '    ElseIf TxtCAT_NE_DESC_MAX.Text = "" Then
    '        Aviso("Escriba Un Descuento Máximo Para La Negociación")
    '    ElseIf TxtCAT_NE_NUM_PAGOS.Text = "0" Then
    '        Aviso("Seleccione El Número De Pagos Para La Negociación")
    '    Else

    '        Dim SSCommandExiste As New SqlCommand
    '        SSCommandExiste.CommandText = "Sp_Varios_Negociaciones"
    '        SSCommandExiste.CommandType = CommandType.StoredProcedure
    '        SSCommandExiste.Parameters.Add("@V_USUARIO", SqlDbType.NVarChar).Value = ""
    '        SSCommandExiste.Parameters.Add("@V_VALOR", SqlDbType.NVarChar).Value = TxtNombre.Text
    '        SSCommandExiste.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 9

    '        Dim DtsExiste As DataTable = Consulta_Procedure(SSCommandExiste, "Sp_Varios_Negociaciones")

    '        Dim Resultado As String = Valida(filtro)
    '            If Resultado.Length > 15 Then
    '            Aviso(Resultado)
    '        Else
    '                Dim SSCommandNEG As New SqlCommand
    '                SSCommandNEG.CommandText = "SP_GENERAR_NEGOCIACIONES"
    '                SSCommandNEG.CommandType = CommandType.StoredProcedure
    '                Ejecuta_Procedure(SSCommandNEG)
    '            'BorrarFilas(LblSeguro.Text)
    '            CrearFila(filtro, TxtNombre.Text, 1)
    '            End If

    '    End If

    'End Sub

    Public Property DataItem() As Object
        Get
            Return Me._dataItem
        End Get
        Set(ByVal value As Object)
            Me._dataItem = value
        End Set
    End Property
End Class
