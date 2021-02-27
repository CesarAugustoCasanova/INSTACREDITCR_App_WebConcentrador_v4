Imports System.Data
Imports System.Data.SqlClient
Imports Db
Imports Funciones2
Imports System.IO
Imports Telerik.Web.UI


Partial Class CrearFilas
    Inherits System.Web.UI.Page
    Public Property tmpUSUARIO As IDictionary
        Get
            Return CType(Session("USUARIO"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("USUARIO") = value
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
    Private Function isSessionIDValid() As Boolean
        Try
            If Application.Get(tmpUSUARIO("CAT_LO_USUARIO")) = Session.SessionID Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
        End Try
        Return False
    End Function

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If tmpUSUARIO("CAT_LO_USUARIO") Is Nothing Then
            OffLine(HidenUrs.Value)
            AUDITORIA(HidenUrs.Value, "Administrador", "CrearFilas", "", "Page_Load", "", Request.ServerVariables("REMOTE_ADDR"), Request.ServerVariables("REMOTE_HOST"))
            Session.Clear()
            Session.Abandon()
            Response.Redirect("../SesionExpirada.aspx")
        Else
            HidenUrs.Value = tmpUSUARIO("CAT_LO_USUARIO")
            Try
                If Not IsPostBack Then
                    Iniciar()
                End If
            Catch ex As Exception
                SendMail("Page_Load", ex, "", "", HidenUrs.Value)
            End Try
        End If
    End Sub
    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("CrearFilas.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub
    Private Sub Iniciar()
        Session("DispDUMMY") = Nothing
        Filas2.Visible = False
        Dim dtsFila As DataTable = ConsultarFila(1)
        If dtsFila.TableName = "Exception" Then
            lblMensaje.Text = dtsFila.Rows(0)(0).ToString
            GrVwFilasCreadas.Visible = False
        ElseIf dtsFila.rows.Count > 0 Then
            GrVwFilasCreadas.DataSource = dtsFila
            GrVwFilasCreadas.DataBind()
            GrVwFilasCreadas.Visible = True
            lblMensaje.Text = ""
        Else
            GrVwFilasCreadas.Visible = False
            lblMensaje.Text = "No hay filas de trabajo definidas aun"
        End If
    End Sub

    Function ConsultarFila(ByVal V_Bandera As Integer, Optional V_ID As Integer = Nothing) As DataTable
        Dim oraCommand As New SqlCommand
        oraCommand.CommandText = "SP_FILAS_TRABAJO2"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_Bandera", SqlDbType.Int).Value = V_Bandera
        oraCommand.Parameters.Add("V_ID", SqlDbType.Int).Value = V_ID
        Dim DtsDatos As DataTable = Consulta_Procedure(oraCommand, oraCommand.CommandText)
        Return DtsDatos
    End Function

    Protected Sub CrearFila_Click(sender As Object, e As EventArgs)
        Filas.Visible = False
        Filas2.Visible = True
        llenarGrid(0)
        Dim dtsFila As DataTable = ConsultarFila(5, Nothing)
        If dtsFila.TableName <> "Exception" Then
            lblConteo2.Text = dtsFila.Rows(0).Item(0).ToString
            'imgbtnSave.Visible = True

            dtsFila = Nothing
            dtsFila = ConsultarFila(15, Nothing)


            CbGestores.DataTextField = dtsFila.Columns(1).ColumnName
            CbGestores.DataValueField = dtsFila.Columns(0).ColumnName
            CbGestores.DataSource = dtsFila
            CbGestores.DataBind()

            PnlDatos.Visible = True
            gridDispersion.Rebind()
        Else
            Alerta(dtsFila.Rows(0).Item(0).ToString)
        End If

    End Sub
    Protected Sub llenarGrid(ByVal flag As Integer)
        'RLBFilasOrigen.Items.Clear()

        If flag = 0 Then
            Dim dtsFila As DataTable = ConsultarFila(2, Nothing)

            Dim tabla As DataTable = dtsFila
            For i = 0 To tabla.Rows.Count - 1
                '       RLBFilasOrigen.Items.Add(CType(tabla.Rows(i).Item(0), String))
            Next
            '   RLBFilasOrigen.DataBind()
        End If
    End Sub
    Private Sub gridDispersion_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles gridDispersion.NeedDataSource
        Dim DtsDatos As DataTable = ConsultarFila(36, Session("DispDUMMY"))
        gridDispersion.DataSource = DtsDatos
    End Sub

    Public Shared Function Rad_Tcadena(ByRef v_item As RadComboBox) As String
        Dim v_cadena As String = ""
        Dim collection As IList(Of RadComboBoxItem) = v_item.CheckedItems

        For Each item As RadComboBoxItem In collection
            v_cadena = v_cadena & "'" & item.Value & "',"
        Next

        If collection.Count = 0 Then
            v_cadena = ""
        Else
            v_cadena = v_cadena.Substring(0, Len(v_cadena) - 1)
        End If

        Return v_cadena
    End Function
    Private Sub gridDispersion_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles gridDispersion.ItemCommand
        Dim comando As String = e.CommandName
        If comando = "PerformInsert" Then
            Dim user As String = Rad_Tcadena(CbGestores)

            If txtBxNombreFila.Text.Trim = "" Then
                Alerta("Ingresa el nombre de la Fila")
                e.Canceled = True
            ElseIf txtBx1DescrpcionFila.Text.Trim = "" Then
                Alerta("Ingresa la descripcion de la Fila")
                e.Canceled = True
            ElseIf user = "" Then
                Alerta("Selecciona un Usuario")
                e.Canceled = True
            Else

                Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
                Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
                Dim valores As Hashtable = Reglas.getGridValues(MyUserControl)

                Dim DtsDatos As DataTable = InsertarParametro(valores("operadorText"), valores("conectorText"), valores("tablaText"), valores("campoText"), valores("valor"), valores("campoValue"), valores("tablaValue"), valores("operadorValue"), valores("conectorValue"), valores("tipo"), Session("DispDUMMY"), txtBxNombreFila.Text, txtBx1DescrpcionFila.Text, user, RCPColorfila.SelectedColor.ToArgb)
                If DtsDatos.TableName = "Exception" Then
                    Alerta(DtsDatos.Rows(0).Item(0).ToString)
                    e.Canceled = True
                Else
                    Session("DispDUMMY") = DtsDatos.Rows(0).Item(0).ToString
                    LblId.Text = DtsDatos.Rows(0)(0).ToString
                    'Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogo dispersión", "Insertar dispersion: " & DDLInstancia.SelectedValue & "|" & ddlsubclasificacion.SelectedValue & "|" & rDdlInterno.SelectedValue & "|" & Nothing & "|" & valores("operadorText") & "|" & valores("conectorText") & "|" & valores("tablaText") & "|" & valores("campoText") & "|" & valores("valor") & "|" & valores("campoValue") & "|" & valores("tablaValue") & "|" & valores("operadorValue") & "|" & valores("conectorValue") & "|" & valores("tipo") & "|" & Session("DispDUMMY"))
                    actualizacontador(Integer.Parse(LblId.Text))
                    gridDispersion.Rebind()
                    Alerta("Actualizacion Correcta")
                End If
            End If
        ElseIf comando = "Edit" Then
            Session("Edit") = True
        ElseIf comando = "Update" Then
            Dim user As String = Rad_Tcadena(CbGestores)
            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores As Hashtable = Reglas.getGridValues(MyUserControl)

            Dim DtsDatos As DataTable = ActualizarParametro(valores("operadorText"), valores("conectorText"), valores("tablaText"), valores("campoText"), valores("valor"), valores("campoValue"), valores("tablaValue"), valores("operadorValue"), valores("conectorValue"), valores("tipo"), Session("DispDUMMY"), txtBxNombreFila.Text, txtBx1DescrpcionFila.Text, user, RCPColorfila.SelectedColor.ToArgb, IIf(chbxHabilitado.Checked = True, 1, 0), gridDispersion.MasterTableView.Items(e.Item.ItemIndex).Cells(3).Text, 2)

            'Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogo dispersión", "Actualizar dispersion: " & valores("operadorText") & "|" & valores("conectorText") & "|" & valores("tablaText") & "|" & valores("campoText") & "|" & valores("valor") & "|" & valores("campoValue") & "|" & valores("tablaValue") & "|" & valores("operadorValue") & "|" & valores("conectorValue") & "|" & valores("tipo") & "|" & Session("DispDUMMY") & "|" & valores("consecutivo"))
            If DtsDatos.TableName = "Exception" Then
                Alerta(DtsDatos.Rows(0).Item(0).ToString)
                e.Canceled = True
            Else
                'Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogo dispersión", "Insertar dispersion: " & DDLInstancia.SelectedValue & "|" & ddlsubclasificacion.SelectedValue & "|" & rDdlInterno.SelectedValue & "|" & Nothing & "|" & valores("operadorText") & "|" & valores("conectorText") & "|" & valores("tablaText") & "|" & valores("campoText") & "|" & valores("valor") & "|" & valores("campoValue") & "|" & valores("tablaValue") & "|" & valores("operadorValue") & "|" & valores("conectorValue") & "|" & valores("tipo") & "|" & Session("DispDUMMY"))
                actualizacontador(Integer.Parse(LblId.Text))
                gridDispersion.Rebind()
                Alerta("Actualizacion Correcta")
            End If
        ElseIf comando = "onDelete" Then
            Dim valores(7) As String

            'BorrarParametro(valores(0), ddlsubclasificacion.SelectedValue, valores(1), valores(4), valores(6), valores(2), valores(3), valores(5), Session("DispDUMMY"))
            Alerta(borrarParametro(LblId.Text, e.Item.Cells(3).Text))
            'Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Catalogo dispersión", "Borrar dispersion: " & valores(0) & "|" & ddlsubclasificacion.SelectedValue & "|" & valores(1) & "|" & valores(4) & "|" & valores(6) & "|" & valores(2) & "|" & valores(3) & "|" & valores(5) & "|" & Session("DispDUMMY"))
            'gridDispersion.Rebind()
            gridDispersion.Rebind()
            actualizacontador(Integer.Parse(LblId.Text))
        End If

    End Sub

    Public Shared Function borrarParametro(ByVal id As String, ByVal orden As String) As String
        Dim oraCommand As New SqlCommand
        oraCommand.CommandText = "SP_FILAS_TRABAJO2"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_Bandera", SqlDbType.VarChar).Value = 37
        oraCommand.Parameters.Add("V_ID", SqlDbType.Int).Value = id
        oraCommand.Parameters.Add("V_ORDEN", SqlDbType.VarChar).Value = orden
        Dim DtsDatos As DataTable = Consulta_Procedure(oraCommand, oraCommand.CommandText)
        Return DtsDatos.Rows(0).Item(0)
    End Function
    Public Shared Function InsertarParametro(ByVal V_CAT_DIS_DESCRIPCIONOPERADOR As String, ByVal V_CAT_DIS_DESCRIPCIONCONECTOR As String, ByVal V_CAT_DIS_DESCRIPCIONTABLA As String, ByVal V_CAT_DIS_DESCRIPCIONCAMPO As String, ByVal V_Cat_DIS_Valor As String, ByVal V_Cat_DIS_Campo As String, ByVal V_CAT_DIS_TABLA As String, ByVal V_Cat_DIS_Operador As String, ByVal V_Cat_DIS_Conector As String, v_tipo As String, v_id As String, v_FILA As String, v_DescripcionFILA As String, v_USUARIOS As String, v_color As String) As Object
        Dim oraCommand As New SqlCommand
        oraCommand.CommandText = "SP_FILAS_TRABAJO2"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_Bandera", SqlDbType.VarChar).Value = 8
        oraCommand.Parameters.Add("V_ID", SqlDbType.Int).Value = v_id
        oraCommand.Parameters.Add("V_FILA", SqlDbType.VarChar).Value = v_FILA
        oraCommand.Parameters.Add("V_DescripcionFila", SqlDbType.VarChar).Value = v_DescripcionFILA
        oraCommand.Parameters.Add("V_DESCRIPCIONOPERADOR", SqlDbType.VarChar).Value = V_CAT_DIS_DESCRIPCIONOPERADOR
        oraCommand.Parameters.Add("V_DESCRIPCIONCONECTOR", SqlDbType.VarChar).Value = V_CAT_DIS_DESCRIPCIONCONECTOR
        oraCommand.Parameters.Add("V_DESCRIPCIONTABLA", SqlDbType.VarChar).Value = V_CAT_DIS_DESCRIPCIONTABLA
        oraCommand.Parameters.Add("V_DESCRIPCIONCAMPO", SqlDbType.VarChar).Value = V_CAT_DIS_DESCRIPCIONCAMPO
        oraCommand.Parameters.Add("V_Valor", SqlDbType.VarChar).Value = V_Cat_DIS_Valor
        oraCommand.Parameters.Add("V_Campo", SqlDbType.VarChar).Value = V_Cat_DIS_Campo
        oraCommand.Parameters.Add("V_TABLA", SqlDbType.VarChar).Value = V_CAT_DIS_TABLA
        oraCommand.Parameters.Add("V_Operador", SqlDbType.VarChar).Value = V_Cat_DIS_Operador
        oraCommand.Parameters.Add("V_Conector", SqlDbType.VarChar).Value = V_Cat_DIS_Conector
        oraCommand.Parameters.Add("v_tipo", SqlDbType.VarChar).Value = v_tipo
        oraCommand.Parameters.Add("v_USUARIOS", SqlDbType.VarChar).Value = v_USUARIOS
        oraCommand.Parameters.Add("V_ORDEN", SqlDbType.VarChar).Value = ""
        oraCommand.Parameters.Add("V_PRIORIDAD", SqlDbType.VarChar).Value = ""
        oraCommand.Parameters.Add("V_ON_OFF", SqlDbType.VarChar).Value = 0
        oraCommand.Parameters.Add("V_COLOR", SqlDbType.VarChar).Value = v_color
        Dim DtsDatos As DataTable = Consulta_Procedure(oraCommand, oraCommand.CommandText)
        Return DtsDatos
    End Function

    Public Shared Function ActualizarParametro(ByVal V_CAT_DIS_DESCRIPCIONOPERADOR As String, ByVal V_CAT_DIS_DESCRIPCIONCONECTOR As String, ByVal V_CAT_DIS_DESCRIPCIONTABLA As String, ByVal V_CAT_DIS_DESCRIPCIONCAMPO As String, ByVal V_Cat_DIS_Valor As String, ByVal V_Cat_DIS_Campo As String, ByVal V_CAT_DIS_TABLA As String, ByVal V_Cat_DIS_Operador As String, ByVal V_Cat_DIS_Conector As String, v_tipo As String, v_id As String, v_FILA As String, v_DescripcionFILA As String, v_USUARIOS As String, v_color As String, v_onoff As String, v_orden As String, v_bandera As String) As Object
        Dim oraCommand As New SqlCommand
        oraCommand.CommandText = "SP_FILAS_TRABAJO2"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_ID", SqlDbType.Int).Value = v_id
        oraCommand.Parameters.Add("V_FILA", SqlDbType.VarChar).Value = v_FILA
        oraCommand.Parameters.Add("V_DescripcionFila", SqlDbType.VarChar).Value = v_DescripcionFILA
        oraCommand.Parameters.Add("V_DESCRIPCIONOPERADOR", SqlDbType.VarChar).Value = V_CAT_DIS_DESCRIPCIONOPERADOR
        oraCommand.Parameters.Add("V_DESCRIPCIONCONECTOR", SqlDbType.VarChar).Value = V_CAT_DIS_DESCRIPCIONCONECTOR
        oraCommand.Parameters.Add("V_DESCRIPCIONTABLA", SqlDbType.VarChar).Value = V_CAT_DIS_DESCRIPCIONTABLA
        oraCommand.Parameters.Add("V_DESCRIPCIONCAMPO", SqlDbType.VarChar).Value = V_CAT_DIS_DESCRIPCIONCAMPO
        oraCommand.Parameters.Add("V_Valor", SqlDbType.VarChar).Value = V_Cat_DIS_Valor
        oraCommand.Parameters.Add("V_Campo", SqlDbType.VarChar).Value = V_Cat_DIS_Campo
        oraCommand.Parameters.Add("V_TABLA", SqlDbType.VarChar).Value = V_CAT_DIS_TABLA
        oraCommand.Parameters.Add("V_Operador", SqlDbType.VarChar).Value = V_Cat_DIS_Operador
        oraCommand.Parameters.Add("V_Conector", SqlDbType.VarChar).Value = V_Cat_DIS_Conector
        oraCommand.Parameters.Add("v_tipo", SqlDbType.VarChar).Value = v_tipo
        oraCommand.Parameters.Add("v_USUARIOS", SqlDbType.VarChar).Value = v_USUARIOS
        oraCommand.Parameters.Add("V_ORDEN", SqlDbType.VarChar).Value = v_orden
        oraCommand.Parameters.Add("V_PRIORIDAD", SqlDbType.VarChar).Value = ""
        oraCommand.Parameters.Add("V_ON_OFF", SqlDbType.VarChar).Value = v_onoff
        oraCommand.Parameters.Add("V_COLOR", SqlDbType.VarChar).Value = v_color
        oraCommand.Parameters.Add("V_Bandera", SqlDbType.VarChar).Value = v_bandera
        Dim DtsDatos As DataTable = Consulta_Procedure(oraCommand, oraCommand.CommandText)
        Return DtsDatos
    End Function

    Sub actualizacontador(v_id As String)
        Dim oraCommand As New SqlCommand
        oraCommand.CommandText = "SP_FILAS_TRABAJO2"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_ID", SqlDbType.Int).Value = v_id
        oraCommand.Parameters.Add("V_Bandera", SqlDbType.VarChar).Value = 39
        Dim DtsDatos As DataTable = Consulta_Procedure(oraCommand, oraCommand.CommandText)
        lblConteo2.Text = DtsDatos.Rows(0).Item("CONS")
    End Sub

    Protected Sub Alerta(ByVal MSJ As String)
        RadAviso.RadAlert(MSJ.Replace("""", "").Replace("'", "").Replace(Chr(10), "").Replace(Chr(10), ""), 440, 155, "AVISO", Nothing)
    End Sub

    Private Sub RCPColorfila_ColorChanged(sender As Object, e As EventArgs) Handles RCPColorfila.ColorChanged
        lblcolor.BackColor = RCPColorfila.SelectedColor

    End Sub
    Protected Sub CBListaDe_ItemChecked(sender As Object, e As RadComboBoxItemEventArgs)
        Dim Cbox As RadComboBox = DirectCast(sender, RadComboBox)
        Dim row As GridViewRow = DirectCast(Cbox.Parent.Parent, GridViewRow)
        Dim TExto As String = ""
        Dim collection As IList(Of RadComboBoxItem) = Cbox.CheckedItems

        For Each item As RadComboBoxItem In collection
            TExto += "'" + item.Text + "', "
        Next
        Dim txt As TextBox = row.FindControl("txtbxFiltroCheckbx")
        txt.Text = TExto
    End Sub

    Private Sub GrVwFilasCreadas_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles GrVwFilasCreadas.ItemCommand
        If e.CommandName = "Editar" Then
            Dim dtstFilas As DataTable = ConsultarFila(9, e.Item.Cells(2).Text) ', "", "", "", "", "", "", "", 0, "", 0, "", "", "")

            If dtstFilas.Rows.Count > 0 Then
                txtBxNombreFila.Text = dtstFilas.Rows(0)(1).ToString
                Dim dtsFila As DataTable = ConsultarFila(15, Nothing) ', "", " AND CAT_LO_AGENCIA = " & tmpUSUARIO("CAT_LO_AGENCIA"), "", "", "", "", "", 0, "", 0, "", "", "")

                CbGestores.DataTextField = dtsFila.Columns(1).ColumnName
                CbGestores.DataValueField = dtsFila.Columns(0).ColumnName
                CbGestores.DataSource = dtsFila
                CbGestores.DataBind()

                RCPColorfila.SelectedColor = System.Drawing.Color.FromArgb(dtstFilas.Rows(0).Item(5))
                Dim gestores As String() = CType(dtstFilas.Rows(0).Item(4), String).Split(",")
                For i = 0 To gestores.Count - 1
                    If CbGestores.Items.Contains(CbGestores.Items.FindItemByValue(gestores(i).Replace("'", "").Trim())) Then
                        CbGestores.Items.FindItemByValue(gestores(i).Replace("'", "").Trim()).Checked = True
                    End If
                Next

                chbxHabilitado.Visible = True
                txtBxNombreFila.ReadOnly = True
                txtBx1DescrpcionFila.Text = dtstFilas.Rows(0)(2).ToString

                If dtstFilas.Rows(0)(3) = 0 Then
                    chbxHabilitado.Checked = False
                Else
                    chbxHabilitado.Checked = True
                End If


                LblId.Text = dtstFilas.Rows(0).Item("IDS")
                Filas2.Visible = True
                Filas.Visible = False
                Session("DispDUMMY") = dtstFilas.Rows(0).Item("IDS")
                actualizacontador(Integer.Parse(LblId.Text))
                gridDispersion.Rebind()
            End If
        End If
    End Sub

    Private Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        Response.Redirect("CrearFilas.aspx")
    End Sub

    Private Sub BtnAceptar_Click(sender As Object, e As EventArgs) Handles BtnAceptar.Click
        Dim user As String = Rad_Tcadena(CbGestores)
        Dim DtsDatos As DataTable = ActualizarParametro("", "", "", "", "", "", "", "", "", "", Session("DispDUMMY"), txtBxNombreFila.Text, txtBx1DescrpcionFila.Text, User, RCPColorfila.SelectedColor.ToArgb, IIf(chbxHabilitado.Checked = True, 1, 0), "", 38)
        If DtsDatos.TableName = "Exception" Then
            Alerta(DtsDatos.Rows(0).Item(0).ToString)
        Else
            Response.Redirect("CrearFilas.aspx")
        End If
    End Sub



End Class
