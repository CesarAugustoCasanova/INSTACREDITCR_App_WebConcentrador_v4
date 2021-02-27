Imports Db
Imports System.Data
Imports System.Data.OracleClient
Imports Telerik.Web.UI
Imports System.Data.SqlClient

Partial Class M_Monitoreo_Reglas
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

    Private Sub llenarComboCampo()
        Try
            If comboCampo.Items.Count = 0 Then
                Dim DtsVariable As DataTable = GenerarReporte("", "", Session("Reporte"), "", "", 0)
                For Each row As DataRow In DtsVariable.Rows
                    Dim item As New RadComboBoxItem(row("DESC").ToString, row("VALOR").ToString)
                    item.Attributes.Add("Tipo", row("TIPO").ToString.Trim(" "))
                    comboCampo.Items.Add(item)
                Next
                comboCampo.DataBind()
            End If
        Catch ex As Exception
            Dim msg = ex.ToString
        End Try

    End Sub

    Private Sub comboCampo_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles comboCampo.SelectedIndexChanged
        Dim tipo As String = ""
        Dim SSCommandCat As New SqlCommand
        SSCommandCat.CommandText = "SP_VALIDA_CATALOGO"
        SSCommandCat.CommandType = CommandType.StoredProcedure
        SSCommandCat.Parameters.Add("@V_Campo", SqlDbType.NVarChar).Value = comboCampo.SelectedValue
        Dim DtsObjetos As DataTable = Consulta_Procedure(SSCommandCat, "Catalogo")
        If DtsObjetos.Rows(0).Item("CAMPO") <> "1" Then
            TxtValores.Visible = False
            RadMultiple.Visible = True
            RadMultiple.DataSource = DtsObjetos
            RadMultiple.DataBind()
            TxtValores.ReadOnly = True
        Else
            TxtValores.ReadOnly = False
            tipo = comboCampo.SelectedItem.Attributes.Item("Tipo")
        End If

        AplicaTipo(tipo)
    End Sub
    Private Sub AplicaTipo(tipo As String)
        DteValores.Clear()
        DteValores.Visible = False
        TxtValores.Text = ""
        TxtValores.Visible = False
        NumValores.Text = ""
        TxtValores.Text = ""
        TxtValores.Visible = False
        DteValores.Clear()
        NumValores.Text = ""
        NumValores.Visible = False
        RadMultiple.Visible = False
        Select Case tipo
            Case "NUMERO"
                DdlOperador.DataSource = Valores_Seleccion(1)
                DdlOperador.DataBind()
                NumValores.Visible = True
                Try
                    NumValores.Text = DataItem("Valor")
                Catch ex As Exception
                    NumValores.Text = 0
                End Try
            Case "FECHA"
                DdlOperador.DataSource = Valores_Seleccion(1)
                DdlOperador.DataBind()
                DteValores.Visible = True
                Try
                    DteValores.SelectedDate = CType(DataItem("Valor"), Date)
                Catch ex As Exception
                    DteValores.SelectedDate = Now
                End Try
            Case "CARACTER"
                DdlOperador.DataSource = Valores_Seleccion(1)
                DdlOperador.DataBind()
                TxtValores.Visible = True
                Try
                    TxtValores.Text = DataItem("Valor")
                Catch ex As Exception
                    TxtValores.Text = ""
                End Try
            Case Else
                DdlOperador.DataSource = Valores_Seleccion(2)
                DdlOperador.DataBind()
                RadMultiple.Visible = True
                RadMultiple.ClearSelection()
                'RadMultiple.Items.Clear()
                If Session("Edit") Then
                    RadMultiple.Items.Clear()
                    Dim DtsLlenar As DataTable = Session("LLENAR")
                    Dim tipoin = comboCampo.SelectedItem.Attributes.Item("Tipo")
                    Dim valores() As String
                    If tipoin = "NUMERO" Then
                        valores = DataItem("Valor").ToString.Replace("(", "").Replace(")", "").Split(",")
                    Else
                        valores = DataItem("Valor").ToString.Replace("(", "").Replace(")", "").Split(",")
                    End If


                    For Each row As DataRow In DtsLlenar.Rows
                        Dim item As New RadComboBoxItem(row("Campo").ToString, row("Campo").ToString)
                        item.Selected = True
                        item.Attributes.Add("Tipo", "COMBO")
                        Dim texto As String = "'" + row("Campo") + "'"


                        item.Checked = IIf(valores.Contains(texto), True, False)
                        RadMultiple.Items.Add(item)
                    Next
                    RadMultiple.DataBind()
                    'For Each row As String In valores
                    '    Dim item As New RadComboBoxItem(row, row)
                    '    Try
                    '        Dim algo As RadComboBoxItem = RadMultiple.FindItemByText(row)
                    '        'RadMultiple.FindItemByText(row).Selected = True
                    '        'RadMultiple.SelectedItem.Checked = True
                    '        algo.Checked = True

                    '    Catch ex As Exception
                    '    End Try
                    'Next
                End If

        End Select
        Try
            DdlConector.FindItemByText(DataItem("DESCRIPCIONCONECTOR")).Selected = True
        Catch ex As Exception
        End Try
    End Sub
    Function Valores_Seleccion(ByVal V_Bandera As String) As DataTable
        Dim Valores As DataTable = New DataTable("VALORES")
        Valores.Columns.Add("Text")
        Valores.Columns.Add("Value")
        Dim row As DataRow = Valores.NewRow()
        If V_Bandera = 1 Then
            row("Text") = "Mayor Que"
            row("Value") = ">"
            Valores.Rows.Add(row)

            row = Valores.NewRow()
            row("Text") = "Menor Que"
            row("Value") = "<"
            Valores.Rows.Add(row)

            row = Valores.NewRow()
            row("Text") = "Igual"
            row("Value") = "="
            Valores.Rows.Add(row)

            row = Valores.NewRow()
            row("Text") = "Mayor O Igual"
            row("Value") = ">="
            Valores.Rows.Add(row)

            row = Valores.NewRow()
            row("Text") = "Menor O Igual"
            row("Value") = "<="
            Valores.Rows.Add(row)

            row = Valores.NewRow()
            row("Text") = "Distinto"
            row("Value") = "!="
            Valores.Rows.Add(row)
        End If
        row = Valores.NewRow()
        row("Text") = "Que Contenga"
        row("Value") = "In"
        Valores.Rows.Add(row)

        row = Valores.NewRow()
        row("Text") = "Que No Contenga"
        row("Value") = "Not In"
        Valores.Rows.Add(row)
        Return Valores
    End Function
    Private Function V_Validar(ByVal V_Campo As String) As Integer
        Dim V_Respuesta As Integer

        Return V_Respuesta
    End Function

    Private Function ValidaCatalogo(ByVal V_Campo As String) As DataTable
        Dim oraCommand As New SqlCommand
        oraCommand.CommandText = "SP_CONFIG_REGLAS"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("@v_bandera", SqlDbType.VarChar).Value = 1
        oraCommand.Parameters.Add("@v_aux", SqlDbType.VarChar).Value = V_Campo
        Dim DtsDatos As DataTable = Consulta_Procedure(oraCommand, "ELEMENTOS")
        Return DtsDatos
    End Function

    Private Function DatosComboBox(ByVal v_bandera As String, Optional v_aux As String = "") As Object
        Dim oraCommand As New SqlCommand
        oraCommand.CommandText = "SP_CONFIG_REGLAS"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("@v_bandera", SqlDbType.VarChar).Value = v_bandera
        oraCommand.Parameters.Add("@v_aux", SqlDbType.VarChar).Value = v_aux
        Dim DtsDatos As DataTable = Consulta_Procedure(oraCommand, "ELEMENTOS")
        Return DtsDatos
    End Function


    Private Sub M_Administrador_grids_configReglas_Reglas_Load(sender As Object, e As EventArgs) Handles Me.Load
        llenarComboCampo()

        If Session("Edit") Then

            Try

                lblConsecutivo.Text = DataItem("CONSECUTIVO")
            Catch ex As Exception

            End Try



            BtnInicioFin.SetSelectedToggleStateByValue(DataItem("AGRUPADOR"))
            Try
                NumNivel.Value = CType(DataItem("NIVEL"), Integer)
                NumNivel.DisplayText = DataItem("NIVEL")
            Catch
                NumNivel.Value = 1
                NumNivel.DisplayText = "1"
            End Try

            'llenarComboCampo(DataItem("DESCRIPCIONTABLA"))
            comboCampo.Enabled = True
                comboCampo.FindItemByText(DataItem("DESCRIPCIONCAMPO")).Selected = True
                Dim tipo As String = ""
                Dim SSCommandCat As New SqlCommand
                SSCommandCat.CommandText = "SP_VALIDA_CATALOGO"
                SSCommandCat.CommandType = CommandType.StoredProcedure
                SSCommandCat.Parameters.Add("@V_Campo", SqlDbType.NVarChar).Value = comboCampo.SelectedValue
                Dim DtsObjetos As DataTable = Consulta_Procedure(SSCommandCat, "Catalogo")
                If DtsObjetos.Rows(0).Item("CAMPO") <> "1" Then
                    TxtValores.Visible = False
                    RadMultiple.Visible = True
                    'RadMultiple.DataSource = DtsObjetos
                    'RadMultiple.DataBind()
                    TxtValores.ReadOnly = True
                    Session("LLENAR") = DtsObjetos
                Else
                    TxtValores.ReadOnly = False
                    tipo = comboCampo.SelectedItem.Attributes.Item("Tipo")
                End If

                AplicaTipo(tipo)
            End If
    End Sub

    Private Sub Reglas_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        If Session("Edit") Then
            Session.Remove("Edit")
            Try
                'comboTablas.FindItemByText(DataItem("DESCRIPCIONTABLA")).Selected = True
                DdlOperador.FindItemByText(DataItem("DESCRIPCIONOPERADOR")).Selected = True
            Catch ex As Exception
            End Try
        End If
    End Sub

    Function GenerarReporte(ByVal V_Campo As String, ByVal V_Reporte As String, ByVal V_Valor As String, ByVal V_Opcion As String, ByVal V_CampoCS As String, ByVal V_Bandera As String) As DataTable
        If V_Valor <> "" And V_Bandera = 5 Then
            V_Valor = " where " & V_Valor
        End If
        Dim HeredarAgencia As String = ""
        If V_Bandera = "4" Or V_Bandera = "5" Then
            HeredarAgencia = tmpUSUARIO("CAT_LO_AGENCIASVER")
        Else
            HeredarAgencia = "'" & tmpUSUARIO("CAT_LO_NUM_AGENCIA") & "','" & tmpUSUARIO("CAT_LO_HEREDAR") & "'"
        End If

        Dim SSCommandId As New SqlCommand
        SSCommandId.CommandText = "SP_GENERAR_REPORTE"
        SSCommandId.CommandType = CommandType.StoredProcedure
        SSCommandId.Parameters.Add("@V_Campo", SqlDbType.NVarChar).Value = V_Campo
        SSCommandId.Parameters.Add("@V_Reporte", SqlDbType.NVarChar).Value = V_Reporte
        SSCommandId.Parameters.Add("@V_Valor", SqlDbType.NVarChar).Value = V_Valor
        SSCommandId.Parameters.Add("@V_Agencia", SqlDbType.NVarChar).Value = HeredarAgencia
        SSCommandId.Parameters.Add("@V_Opcion", SqlDbType.NVarChar).Value = V_Opcion
        SSCommandId.Parameters.Add("@V_CampoCS", SqlDbType.NVarChar).Value = V_CampoCS
        SSCommandId.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = V_Bandera
        Dim DtsObjetos As DataTable = Consulta_Procedure(SSCommandId, "Reporte")
        If V_Bandera = 0 Then
            Return DtsObjetos
        Else
            Return DtsObjetos
        End If
    End Function

End Class
