Imports Db
Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Class M_Administrador_grids_configReglas_Reglas
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

    Private Sub comboTablas_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles comboTablas.SelectedIndexChanged
        llenarComboCampo(e.Text)
        comboCampo.Enabled = True
    End Sub

    Private Sub llenarComboCampo(tabla As String)
        comboCampo.ClearSelection()
        comboCampo.Items.Clear()
        Dim ds As DataTable = DatosComboBox(3, tabla)

        For Each row As DataRow In ds.Rows
            Dim item As New RadComboBoxItem(row("Campo_Nombre"), row("Campo"))
            item.Attributes.Add("Tipo", row("Tipo"))
            comboCampo.Items.Add(item)
        Next

    End Sub

    Private Sub comboTablas_DataBinding(sender As Object, e As EventArgs) Handles comboTablas.DataBinding
        Dim ds As DataTable = DatosComboBox(2)
        comboTablas.DataTextField = "Cat_Ta_Desc"
        comboTablas.DataValueField = "Cat_Ta_Tabla"
        comboTablas.DataSource = ds
    End Sub

    Private Sub comboCampo_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles comboCampo.SelectedIndexChanged
        Dim tipo As String
        Dim DTsValidaCatalogo As DataTable
        DTsValidaCatalogo = ValidaCatalogo(comboCampo.SelectedValue)
        If DTsValidaCatalogo.TableName <> "Exception" Then

            tipo = "COMBO"
        Else
            DTsValidaCatalogo = Nothing
            tipo = comboCampo.SelectedItem.Attributes.Item("Tipo")
        End If


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
            Case "numeric", "decimal", "int", "number", "bigint"
                DdlOperador.DataSource = Valores_Seleccion(1)
                DdlOperador.DataValueField = "Value"
                DdlOperador.DataTextField = "Text"
                DdlOperador.DataBind()
                NumValores.Visible = True
            Case "date", "datetime"
                DdlOperador.DataSource = Valores_Seleccion(1)
                DdlOperador.DataValueField = "Value"
                DdlOperador.DataTextField = "Text"
                DdlOperador.DataBind()
                DteValores.Visible = True
            Case "varchar", "nvarchar"
                DdlOperador.DataSource = Valores_Seleccion(1)
                DdlOperador.DataValueField = "Value"
                DdlOperador.DataTextField = "Text"
                DdlOperador.DataBind()
                TxtValores.Visible = True
            Case "COMBO"
                DdlOperador.DataSource = Valores_Seleccion(2)
                DdlOperador.DataValueField = "Value"
                DdlOperador.DataTextField = "Text"
                DdlOperador.DataBind()
                RadMultiple.Visible = True
                RadMultiple.ClearSelection()
                RadMultiple.Items.Clear()
                For Each row As DataRow In DTsValidaCatalogo.Rows
                    Dim item As New RadComboBoxItem(row("Campo"), row("Campo"))
                    item.Attributes.Add("Tipo", "COMBO")
                    RadMultiple.Items.Add(item)
                Next
        End Select
    End Sub
    Function Valores_Seleccion(ByVal V_Bandera As String) As ArrayList
        Dim Valores As ArrayList = New ArrayList()
        If V_Bandera = 1 Then
            Valores.Add(New ListItem("Mayor Que", ">"))
            Valores.Add(New ListItem("Menor Que", "<"))
            Valores.Add(New ListItem("Igual", "="))
            Valores.Add(New ListItem("Mayor O Igual", ">="))
            Valores.Add(New ListItem("Menor O Igual", "<="))
            Valores.Add(New ListItem("Distinto", "!="))
            Valores.Add(New ListItem("Que Contenga", "In"))
            Valores.Add(New ListItem("Que No Contenga", "Not In"))
        Else
            Valores.Add(New ListItem("Que Contenga", "In"))
            Valores.Add(New ListItem("Que No Contenga", "Not In"))
        End If
        Return Valores
    End Function
    Private Function V_Validar(ByVal V_Campo As String) As Integer
        Dim V_Respuesta As Integer

        Return V_Respuesta
    End Function

    Private Function ValidaCatalogo(ByVal V_Campo As String) As DataTable
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_CONFIG_REGLAS"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_bandera", SqlDbType.NVarChar).Value = 1
        SSCommand.Parameters.Add("@v_aux", SqlDbType.NVarChar).Value = V_Campo

        Dim DtsDatos As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return DtsDatos
    End Function

    Private Function DatosComboBox(ByVal v_bandera As String, Optional v_aux As String = "") As DataTable
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_CONFIG_REGLAS"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_bandera", SqlDbType.NVarChar).Value = v_bandera
        SSCommand.Parameters.Add("@v_aux", SqlDbType.NVarChar).Value = v_aux

        Dim DtsDatos As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return DtsDatos
    End Function

    Private Sub M_Administrador_grids_configReglas_Reglas_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("Edit") Then
            Try
                lblConsecutivo.Text = DataItem("ORDEN")

                llenarComboCampo(DataItem("DESCRIPCIONTABLA"))
                comboCampo.Enabled = True
                comboCampo.FindItemByText(DataItem("DESCRIPCIONCAMPO")).Selected = True

                Dim tipo As String
                Dim DTsValidaCatalogo As DataTable
                DTsValidaCatalogo = ValidaCatalogo(comboCampo.SelectedValue)
                If DTsValidaCatalogo.TableName <> "Exception" Then

                    tipo = "COMBO"
                Else
                    DTsValidaCatalogo = Nothing
                    tipo = comboCampo.SelectedItem.Attributes.Item("Tipo")
                End If


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
                    Case "numeric", "decimal", "int", "number", "money", "bigint"
                        DdlOperador.DataSource = Valores_Seleccion(1)
                        DdlOperador.DataValueField = "Value"
                        DdlOperador.DataTextField = "Text"
                        DdlOperador.DataBind()
                        NumValores.Visible = True
                        NumValores.Text = DataItem("Valor")
                    Case "date", "datetime"
                        DdlOperador.DataSource = Valores_Seleccion(1)
                        DdlOperador.DataValueField = "Value"
                        DdlOperador.DataTextField = "Text"
                        DdlOperador.DataBind()
                        DteValores.Visible = True
                        Try
                            DteValores.SelectedDate = CType(DataItem("Valor"), Date)
                        Catch ex As Exception
                            DteValores.SelectedDate = Now
                        End Try
                    Case "varchar", "nvarchar"
                        DdlOperador.DataSource = Valores_Seleccion(1)
                        DdlOperador.DataValueField = "Value"
                        DdlOperador.DataTextField = "Text"
                        DdlOperador.DataBind()
                        TxtValores.Visible = True
                        TxtValores.Text = DataItem("Valor")
                    Case "COMBO"
                        DdlOperador.DataSource = Valores_Seleccion(2)
                        DdlOperador.DataValueField = "Value"
                        DdlOperador.DataTextField = "Text"
                        DdlOperador.DataBind()
                        RadMultiple.Visible = True
                        RadMultiple.ClearSelection()
                        RadMultiple.Items.Clear()
                        For Each row As DataRow In DTsValidaCatalogo.Rows
                            Dim item As New RadComboBoxItem(row("Campo"), row("Campo"))
                            item.Attributes.Add("Tipo", "COMBO")
                            RadMultiple.Items.Add(item)
                            Try
                                Dim valores() As String = DataItem("Valor").ToString.Replace("(", "").Replace(")", "").Replace("'", "").Split(",")
                                For Each valor As String In valores
                                    RadMultiple.FindItemByText(valor).Checked = True
                                Next
                            Catch ex As Exception
                            End Try
                        Next

                End Select
                Try
                    DdlConector.FindItemByText(DataItem("DESCRIPCIONCONECTOR")).Selected = True
                Catch ex As Exception
                End Try
            Catch ex As Exception
                Dim abc As String = ex.Message
            End Try
        End If
    End Sub

    Private Sub M_Administrador_grids_configReglas_Reglas_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        If Session("Edit") Then
            Session.Remove("Edit")
            Try
                comboTablas.FindItemByText(DataItem("DESCRIPCIONTABLA")).Selected = True
                DdlOperador.FindItemByText(DataItem("DESCRIPCIONOPERADOR")).Selected = True
            Catch ex As Exception
            End Try
        End If
    End Sub
End Class