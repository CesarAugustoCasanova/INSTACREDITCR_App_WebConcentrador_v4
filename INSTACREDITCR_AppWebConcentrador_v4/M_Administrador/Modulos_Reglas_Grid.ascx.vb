
Imports System.Data
Imports CustomEditors
Imports Telerik.Web.UI

Partial Class M_Administrador_Modulos_Reglas_Grid
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
    Private Sub M_Administrador_Modulos_Reglas_Grid_Init(sender As Object, e As EventArgs) Handles Me.Init

        If Session("LoadData") Then
            Dim tablas As DataTable = SP.REGLAS(bandera:=2)
            rcbTablas.DataSource = tablas
            rcbTablas.DataTextField = "descripcion"
            rcbTablas.DataValueField = "tabla"
            rcbTablas.DataBind()
        End If

        If Session("ModulosEdit") Then
            lblReglaID.Text = DataItem("# Regla")
            txtNombreRegla.Text = DataItem("Nombre")
        End If
    End Sub

    Private Sub M_Administrador_Modulos_Reglas_Grid_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("LoadData") Then
            Session.Remove("LoadData")
            'SetRadFilterDropDownItems()
        End If
    End Sub

    Private Sub M_Administrador_Modulos_Reglas_Grid_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        If Session("ModulosEdit") Then
            Session.Remove("ModulosEdit")
            Dim datos As DataRow = SP.REGLAS(bandera:=7, v_regla_id:=DataItem("# Regla"))(0)

            Dim tablas As String = datos("CAT_RE_TABLAS").ToString
            Dim campos As String = datos("CAT_RE_CAMPOS").ToString
            Dim left_join As String = datos("CAT_RE_LEFT_JOIN").ToString
            Dim whereRadFilter As String = datos("CAT_RE_WHERE_B64").ToString

            If Not (String.IsNullOrEmpty(tablas) Or String.IsNullOrWhiteSpace(tablas)) Then
                For Each tabla As String In tablas.Split(",")
                    Dim item = rcbTablas.FindItemByValue(tabla)
                    If item IsNot Nothing Then
                        item.Checked = True
                    End If
                Next
            End If

            If Not (String.IsNullOrEmpty(campos) Or String.IsNullOrWhiteSpace(campos)) Then
                Session("campos") = campos
            End If

            If Not (String.IsNullOrEmpty(left_join) Or String.IsNullOrWhiteSpace(left_join)) Then
                switch1.Checked = IIf(left_join = "1", True, False)
            End If

            If Not (String.IsNullOrEmpty(whereRadFilter) Or String.IsNullOrWhiteSpace(whereRadFilter)) Then
                Session("filter") = whereRadFilter
            End If
        End If
    End Sub
    Private Sub rcbCampos_TextChanged(sender As Object, e As EventArgs) Handles rcbCampos.TextChanged

    End Sub

    '--------------------WIZARD CONTROL STEP CHANGES---------------------
    Private Sub wizard1_NextButtonClick(sender As Object, e As WizardEventArgs) Handles wizard1.NextButtonClick
        Select Case e.CurrentStepIndex
            Case 0
                If txtNombreRegla.Text = "" Then

                Else
                    'Si se crea una nueva regla, el lblReglaID.text vale -1 y por lo tanto va a insertar ese nombre como una nueva regla. Si estamos editando
                    ' el lblReglaId ya trae el id de la regla y va a actualizar el nombre de la regla.
                    lblReglaID.Text = SP.REGLAS(bandera:=4, v_regla_id:=lblReglaID.Text, v_nombre_regla:=txtNombreRegla.Text, v_usuario:=tmpUSUARIO("CAT_LO_USUARIO")).Rows(0)(0).ToString
                    Try
                        If Session("campos") IsNot Nothing Then
                            'En caso de que estemos editando, Session("campos") contiene los campos seleccionados.
                            Dim SelectedCampos As String = Session("campos").ToString
                            Session.Remove("campos")

                            'Traemos todos los campos de las tablas seleccionads
                            getCampos()

                            'Hacemos check en los campos seleccionados
                            For Each campo As String In SelectedCampos.Split(",")
                                Dim item = rcbCampos.FindItemByValue(campo)
                                If item IsNot Nothing Then
                                    item.Checked = True
                                End If
                            Next
                        End If

                    Catch ex As Exception

                    End Try
                    'Se habilita el paso para seleccionar las tablas y campos
                    ' y se deshabilita el paso para poner el nombre.
                    ' Basicamente es para que el usuario respete el flujo y no se ande saltando del paso 3 al paso 1 o cualquier otra locura xd
                    e.NextStep.Enabled = True
                    e.CurrentStep.Enabled = False
                End If
            Case 1
                If rcbCampos.CheckedItems.Count = 0 Then

                Else
                    SaveTablesAndColumns()
                    e.NextStep.Enabled = True
                    e.CurrentStep.Enabled = False
                    'Tratamos de cargar las reglas que se crearon anteriormente
                    'No siempre se pueden recuperar porque los campos seleccionados cambian
                    If Session("filter") IsNot Nothing Then
                        Try
                            filtergg.LoadSettings(Session("filter").ToString)
                            Session.Remove("filter")
                        Catch ex As Exception

                        End Try
                    End If
                    Try
                        LlenarGrid()
                    Catch ex As Exception

                    End Try
                End If
        End Select

    End Sub
    Private Sub wizard1_PreviousButtonClick(sender As Object, e As WizardEventArgs) Handles wizard1.PreviousButtonClick
        e.NextStep.Enabled = True
        e.CurrentStep.Enabled = False
    End Sub

    Private Sub wizard1_FinishButtonClick(sender As Object, e As WizardEventArgs) Handles wizard1.FinishButtonClick
        SP.REGLAS(bandera:=10, v_regla_id:=lblReglaID.Text)
        Response.Redirect("./Modulos_Reglas.aspx")
    End Sub

    Private Sub wizard1_CancelButtonClick(sender As Object, e As WizardEventArgs) Handles wizard1.CancelButtonClick
        Response.Redirect("./Modulos_Reglas.aspx")
    End Sub

    '--------------------WIZARD STEP 2-----------------------------------
    Private Sub rcbTablas_ItemChecked(sender As Object, e As RadComboBoxItemEventArgs) Handles rcbTablas.ItemChecked
        getCampos()
    End Sub

    Private Sub rcbTablas_CheckAllCheck(sender As Object, e As RadComboBoxCheckAllCheckEventArgs) Handles rcbTablas.CheckAllCheck
        getCampos()
    End Sub

    Private Sub getCampos()
        rcbCampos.Items.Clear()
        Dim data As New DataTable("Datos")
        data.Columns.Add("Campo_Nombre")
        data.Columns.Add("Campo")
        data.Columns.Add("Nombre")
        data.Columns.Add("Tabla")
        data.Columns.Add("Tipo")
        'Obtenemos la informacion de las tablas seleccionadas
        For Each item As RadComboBoxItem In rcbTablas.CheckedItems
            Dim aux As DataTable = SP.REGLAS(bandera:=3, v_tablas_regla:=item.Text)
            data.Merge(aux, True)
        Next
        rcbCampos.Enabled = True
        'Armamos los items con las propiedades para los campos
        For Each row As DataRow In data.Rows
            Dim item As New RadComboBoxItem("[" & row("Nombre") & "] " & row("Campo_Nombre"), row("Tabla") & "." & row("Campo"))
            item.Attributes.Add("tabla", row("Tabla"))
            item.Attributes.Add("campoNombre", row("Campo_Nombre"))
            item.Attributes.Add("campo", row("Campo"))
            item.Attributes.Add("nombre", row("Nombre"))
            item.Attributes.Add("tipo", row("Tipo"))
            rcbCampos.Items.Add(item)
        Next
        rcbCampos.DataBind()
    End Sub

    ''' <summary>
    ''' Se procesan los campos seleccionados para determinar cuales tablas se van a usar realmente
    '''  y para armar la parte del select y el from con el join o left join si lo requiere.
    ''' </summary>
    Private Sub SaveTablesAndColumns()
        Dim tablas As New List(Of String)
        Dim campos As New List(Of String)
        Dim CamposSelect As New List(Of String)
        Dim isLeftJoin As Integer = IIf(switch1.Checked, 1, 0)
        'Primero obtenemos las tablas que realmente se usaron,
        ' posteriormente, nos fijamos que no existan campos duplicados
        ' como el cliente, por ejemplo.

        'Al mismo tiempor que buscamos excluimos los campos repetidos, se genera
        ' la estructura del select

        'La estructura del From se genera al momento de guardar las cosas (SP_REGLAS bandera 5)

        For Each item As RadComboBoxItem In rcbCampos.CheckedItems
            Dim atributos = item.Attributes
            If Not tablas.Contains(atributos("tabla")) Then
                tablas.Add(atributos("tabla"))
            End If

            If Not campos.Contains(item.Value) Then
                campos.Add(item.Value)
                CamposSelect.Add(item.Value & " """ & atributos("campoNombre").Replace("""", "") & """")
            End If
        Next

        Dim res = SP.REGLAS(bandera:=5, v_regla_id:=lblReglaID.Text, V_LEFT_JOIN:=isLeftJoin, v_tablas_regla:=String.Join(",", tablas), V_SELECT:=String.Join(",", CamposSelect), v_campos_regla:=String.Join(",", campos))

        If res.Rows(0)(1) = "1" Then
            'Si se han detectado cambios en los campos, se elimina la session que
            ' contiene los filtros de antes para evitar que cargue filtros con
            ' campos que ya no existen
            Try
                Session.Remove("filter")
            Catch ex As Exception

            End Try
        End If
    End Sub

    ''' <summary>
    ''' Traemos los campos que van a ser DropDown y los configuramos.
    ''' </summary>
    Private Sub SetRadFilterDropDownItems()
        Try
            'Traemos los campos que serán DropDownList y los inicializamos
            Dim camposDDL As DataTable = SP.REGLAS(bandera:=12)

            For Each campo As DataRow In camposDDL.Rows
                'Generamos nuestro DropDown. El DataSource se genera en el evento ExpressionItemCreated
                Dim ddl As New RadFilterDropDownEditor()

                'Añadimos nuestro DropDown al RadFilter
                filtergg.FieldEditors.Add(ddl)

                ddl.FieldName = campo("CAMPO")
            Next
        Catch ex As Exception
            Dim e = ex.Message
        End Try
    End Sub

    Private Function FindFilterByFieldName(fieldName As String) As RadFilterDataFieldEditor
        For Each fielEditor As RadFilterDataFieldEditor In filtergg.FieldEditors
            If fielEditor.FieldName = fieldName Then
                Return fielEditor
            End If
        Next
        Return Nothing
    End Function

    '-----------------WIZARD FINAL STEP----------------------------------
    Private Sub LlenarGrid()
        Try
            gridInfoPreview.DataSource = SP.REGLAS(bandera:=6, v_regla_id:=lblReglaID.Text)
            gridInfoPreview.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub filtergg_ApplyExpressions(sender As Object, e As RadFilterApplyExpressionsEventArgs) Handles filtergg.ApplyExpressions
        'Obtenemos el estado del RadFilter
        Dim whereb64 As String = filtergg.SaveSettings()

        'Obtenemos el query que genrea el RadFilter
        Dim provider As New RadFilterSqlQueryProvider()
        provider.ProcessGroup(e.ExpressionRoot)
        Dim res = provider.Result

        'El query lo arma con las descripciones de los campos, entonces tenemos que sustituir esas descripciones
        ' por los nombres de los campos,
        For Each item As RadComboBoxItem In rcbCampos.CheckedItems
            res = res.Replace("[" & item.Attributes("campoNombre") & "]", item.Attributes("campo"))
        Next

        'Guardamos toda la informacion generada
        SP.REGLAS(bandera:=8, V_WHERE:=" where " & res, V_WHERE_B64:=whereb64, v_regla_id:=lblReglaID.Text)
        LlenarGrid()
    End Sub

    Private Sub filtergg_ExpressionItemCreated(sender As Object, e As RadFilterExpressionItemCreatedEventArgs) Handles filtergg.ExpressionItemCreated
        'Dim singleItem As RadFilterSingleExpressionItem = TryCast(e.Item, RadFilterSingleExpressionItem)
        'Try
        '    Dim dropDownList As RadDropDownList = TryCast(singleItem.InputControl, RadDropDownList)
        '    If singleItem IsNot Nothing And singleItem.IsSingleValue And dropDownList IsNot Nothing Then
        '        dropDownList.DefaultMessage = "Seleccione"
        '        Dim dt As DataTable = SP.REGLAS(bandera:=13, v_campos_regla:=singleItem.FieldName)
        '        dropDownList.DataTextField = dt.Columns(0).ColumnName
        '        dropDownList.DataValueField = dt.Columns(0).ColumnName
        '        dropDownList.DataSource = dt
        '        dropDownList.DataBind()
        '    End If
        'Catch ex As Exception
        '    Dim err = ex.Message
        'End Try
    End Sub


End Class
