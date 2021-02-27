Imports System.Web.Script.Serialization
Imports ClassGraficaGoogle
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports Conexiones
Imports Funciones
Imports System
Imports Db
'Imports Spire.Xls
Imports Telerik.Web.UI
Imports ClosedXML.Excel
Imports DocumentFormat


Partial Class DashBoard
    Inherits System.Web.UI.Page
    Public DdlTipo As DropDownList
    Public DdlCampo As DropDownList
    Public DdlOperador As DropDownList
    Public TxtValores As TextBox
    Public DdlConector As DropDownList = New DropDownList()
    Public GvCatalogos As GridView
    Public Panel As Panel
    Private campoCredito As String = "Credito"

    Public Calendario As AjaxControlToolkit.CalendarExtender
    Dim random = New Random()
    Shared tab As DataTable = Nothing
    Public Property tmpUSUARIO As IDictionary
        Get
            Return CType(Session("USUARIO"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("USUARIO") = value
        End Set
    End Property

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        ScriptManager.GetCurrent(Page).RegisterPostBackControl(BtnToCsv)
        ScriptManager.GetCurrent(Page).RegisterPostBackControl(BtnToXlsx)




        If tmpUSUARIO Is Nothing Then
            Response.Redirect("~/SesionExpirada.aspx")
        End If
        Try
            If Not IsPostBack Then
                inicializarGrid()
                LblReporte.Text = Request.Url.ToString().Split("?")(1)
                LlenarGV()
                Where(LblReporte.Text, tmpUSUARIO("CAT_LO_USUARIO"), " ", " ", " ", " ", " ", "", 0, 0, 2)

                Dim relativePath = "~/PDFs"
                RadClientExportManager1.PdfSettings.ProxyURL = ResolveUrl(relativePath)
                RadClientExportManager1.PdfSettings.Author = "MCCollect"


                Dim tabla As DataTable = Session("TablaCondiciones")
                tabla.Clear()


            End If
        Catch ex As System.Threading.ThreadAbortException
        Catch ex As Exception
            SendMail("Page_Load", ex, "", "", HidenUrs.Value)
        End Try
    End Sub
    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Reportes", "Reporteador.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub

    Sub LlenarGV()
        Dim DtsCampo As DataTable = GenerarReporte("", "", LblReporte.Text, "", "", 2)

        Session("Agrupar") = DtsCampo(0)(0)
        Session("SumCount") = DtsCampo(0)(1)

        Select Case DtsCampo(0)(2).ToString
            Case "Area"
                RBLGrafica.SelectedValue = 1
            Case "Linea"
                RBLGrafica.SelectedValue = 2
            Case "Barra"
                RBLGrafica.SelectedValue = 3
            Case "BarraLateral"
                RBLGrafica.SelectedValue = 4
            Case "Dona"
                RBLGrafica.SelectedValue = 5
            Case "Circular"
                RBLGrafica.SelectedValue = 6
        End Select

        btnSumarContar.SetSelectedToggleStateByText(IIf(DtsCampo(0)(3).ToString = "Sum", "Sumar", "Contar"))
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
        Return DtsObjetos

    End Function

    Protected Sub BtnGenerar_Click(sender As Object, e As EventArgs) Handles BtnGenerar.Click
        If ddlCampoAgrupar.SelectedText = "" Then
            showModal(RadNotification1, "deny", "Error", "Selecciona un campo para agrupar")
        ElseIf ddlSumarContar.SelectedText = "" Then
            showModal(RadNotification1, "deny", "Error", "Selecciona un campo para sumar o contar")
        ElseIf btnCondiciones.Checked And gridCondiciones.MasterTableView.Items.Count = 0 Then
            showModal(RadNotification1, "deny", "Error", "Agrega al menos una condicion")
        ElseIf btnArchivo.Checked And hfCreditos.Value.Contains("Error") Then 'Si seleccionaron la opcion para subir archivo y subieron un archivo pero no se ha procesado o generó un error al procesarse
            showModal(RadNotification1, "deny", "Error al procesar archivo", hfCreditos.Value)
        Else
            Generar()
            Session("gotoEndPage") = True
        End If
    End Sub
    Function Verificar_Condicion(ByVal Condicion As String) As Integer
        Dim Existe As Integer = 0
        Try
            If Condicion.Length < 5 Then
                Condicion = ""
            Else
                Condicion = " where " & Condicion
            End If
            Dim DtsDatos As DataTable = GenerarReporte("", LblReporte.Text, Condicion, IIf(btnSumarContar.Text = "Sumar", "Sum", "Count"), ddlSumarContar.SelectedText, 4)
            Existe = DtsDatos.Rows(0).Item("Existe")
        Catch ex As Exception
            Existe = 0
        End Try
        Return Existe
    End Function

    Sub Generar()
        '  Dim resultado As String = actualizaetiquetas() & " AND PR_MC_AGENCIA IN (" & tmpUSUARIO("CAT_LO_CADENAAGENCIAS") & ")"
        Dim resultado As String = actualizaetiquetas("0") & " AND PR_MC_AGENCIA IN (" & tmpUSUARIO("CAT_LO_CADENAAGENCIAS") & ")"
        resultado = resultado.Replace(" AND AND ", " AND ")
        If resultado = "-1" Then
            showModal(RadNotification1, "deny", "Error", "Error al generar el reporte. Intente más tarde")
        Else
            If LblTipo.Text <> "0" Then
                Dim con_datos As Integer = Verificar_Condicion(resultado)
                If con_datos = 0 Then
                    showModal(RadNotification1, "deny", "Error", ".No se encontraron datos con la configuracion seleccionada")
                    chartPrint.DataSource = Nothing
                    pnlGrafica.Visible = False
                Else
                    GenerarGrafica(resultado)
                End If
            Else
                Dim con_datos As Integer = Verificar_Condicion(resultado)
                If con_datos = 0 Then
                    showModal(RadNotification1, "deny", "Error", "..No se encontraron datos con la configuracion seleccionada")
                    chartPrint.DataSource = Nothing
                    pnlGrafica.Visible = False
                Else
                    GenerarGrafica(resultado)
                End If
            End If
        End If
    End Sub

    Protected Sub ButtonDescargar_Click()
        'Try
        '    Try
        '        Dim Quien As String = Session("Condiciones")(LblExportar.Text)
        '        Dim DtsDatos As DataTable = GenerarReporte(LblX.Text, LblReporte.Text, LblX.Text, "", "", 6)
        '        Dim Campo As String = DtsDatos.Rows(0).Item("Campo")
        '        Dim SSCommand As New SqlCommand
        '        SSCommand.CommandText = "Sp_Generar_Salida_Reporte"
        '        SSCommand.CommandType = CommandType.StoredProcedure
        '        SSCommand.Parameters.Add("@V_Reporteagenerar", SqlDbType.NVarChar).Value = LblReporte.Text
        '        If DtsDatos.Rows(0).Item("Tipo").ToString.ToUpper = "FECHA" Then
        '            If Quien = " " Then
        '                SSCommand.Parameters.Add("@V_CONDICIONES", SqlDbType.NVarChar).Value = " WHERE " & Fn_Condicion() & " And " & Campo & " IS NULL"
        '            Else
        '                SSCommand.Parameters.Add("@V_CONDICIONES", SqlDbType.NVarChar).Value = " WHERE " & Fn_Condicion() & " And to_char(" & Campo & ",'DD/MM/YYYY')='" & Quien & "'"
        '            End If
        '        Else
        '            SSCommand.Parameters.Add("@V_CONDICIONES", SqlDbType.NVarChar).Value = " WHERE " & Fn_Condicion() & " And " & Campo & "='" & Quien & "'"
        '        End If
        '        SSCommand.Parameters.Add("@V_Agencia", SqlDbType.NVarChar).Value = ""

        '        Dim DtsGenerar As DataTable = Consulta_Procedure(SSCommand, "Archs")
        '        Dim archivo As String = StrRuta() & "Salida\" & DtsGenerar.Rows(0).Item("Archs")
        '        Dim ioflujo As FileInfo = New FileInfo(archivo)
        '        Response.Clear()
        '        Response.AddHeader("Content-Disposition", "attachment; filename=" + ioflujo.Name)
        '        Response.AddHeader("Content-Length", ioflujo.Length.ToString())
        '        Response.ContentType = "application/octet-stream"
        '        Response.WriteFile(archivo)
        '        Response.End()
        '    Catch ex As Exception
        '        LblMsj.Text = "Seleccione Una Condición"
        '        MpuMensajes.Show()
        '    End Try
        '    GenerarGrafica(Fn_Condicion())
        'Catch ex As Exception
        '    BtnDetalles.Visible = False
        '    Mensaje.Text = ex.ToString
        'End Try
    End Sub

    Sub GenerarGrafica(ByVal Condicion As String)
        pnlGrafica.Visible = True
        Todas(Condicion)

    End Sub
    Sub Todas(ByVal Condicion As String)
        Dim Cuantos As Double = 0
        'hiddenTitulo.Value = HttpUtility.HtmlDecode(" ")
        Dim DatosGrafica = New List(Of Object)

        Dim GrafRoles As New List(Of ClassGraficaGoogle)()
        Dim RolesGrafica As New ClassGraficaGoogle()


        Dim DtsCircular As DataTable = GenerarReporte(ddlCampoAgrupar.SelectedText, LblReporte.Text, Condicion, IIf(btnSumarContar.Text = "Sumar", "Sum", "Count"), ddlSumarContar.SelectedText, 5)

        Try
            Dim tipoGrafica As Object
            Select Case RBLGrafica.SelectedValue
                Case 1
                    tipoGrafica = New AreaSeries
                Case 2
                    tipoGrafica = New LineSeries
                Case 3
                    tipoGrafica = New ColumnSeries
                Case 4
                    tipoGrafica = New BarSeries
                Case 5
                    tipoGrafica = New DonutSeries
                Case 6
                    tipoGrafica = New PieSeries
                Case Else
                    tipoGrafica = New LineSeries
            End Select
            Dim template As String = ddlSumarContar.SelectedText & ": #=value# \n" & ddlCampoAgrupar.SelectedText & ": #=dataItem.descripcion#"
            If DtsCircular.Rows.Count > 20 Or RBLGrafica.SelectedValue = 5 Then
                tipoGrafica.LabelsAppearance.Visible = False
                tipoGrafica.TooltipsAppearance.ClientTemplate = template
            Else
                tipoGrafica.LabelsAppearance.ClientTemplate = template
                tipoGrafica.TooltipsAppearance.Visible = False
            End If
            tipoGrafica.Name = LblReporte.Text
            tipoGrafica.DataFieldY = "cuantos"
            Try
                tipoGrafica.MissingValues = HtmlChart.MissingValuesBehavior.Zero
            Catch ex As Exception

            End Try

            chartPrint.PlotArea.Series.Clear()
            chartPrint.PlotArea.Series.Add(tipoGrafica)

            chartPrint.PlotArea.XAxis.TitleAppearance.Text = ddlCampoAgrupar.SelectedText
            chartPrint.PlotArea.XAxis.DataLabelsField = "descripcion"
            chartPrint.PlotArea.XAxis.LabelsAppearance.RotationAngle = 45

            chartPrint.Legend.Appearance.Visible = False

            chartPrint.PlotArea.YAxis.TitleAppearance.Text = ddlSumarContar.SelectedText

            chartPrint.DataSource = DtsCircular
            chartPrint.DataBind()
            Session("gotoEndPage") = True

            gridDetalles.Visible = False
        Catch ex As Exception
            pnlGrafica.Visible = False
            showModal(RadNotification1, "deny", "Error", "Imposible generar la gráfica. Intente más tarde.")
        End Try


        'Dim dtGrafica As DataTable = New DataTable()
        'dtGrafica.Columns.Add("Cuantos")
        'dtGrafica.Columns.Add("Descripcion")
        'dtGrafica.Columns.Add("Color")
        'Dim dtRowg As DataRow
        'dtRowg = dtGrafica.NewRow()

        'Dim list As New ArrayList



        'RolesGrafica.role = "Colors"
        'DatosGrafica.Add(New Object() {"Grupo", "Valor", RolesGrafica})
        'For A As Integer = 0 To DtsCircular.Rows.Count - 1
        '    dtRowg.Item(0) = DtsCircular.Rows(A).Item("Cuantos")
        '    Try
        '        Cuantos = Cuantos + DtsCircular.Rows(A).Item("Cuantos")
        '    Catch ex As Exception
        '        Cuantos = Cuantos + 0
        '    End Try
        '    dtRowg.Item(1) = DtsCircular.Rows(A).Item("descripcion")
        '    dtRowg.Item(2) = random.[Next](&H1000000)
        '    DatosGrafica.Add(New Object() {DtsCircular.Rows(A).Item("descripcion").ToString.ToLower, Convert.ToDecimal(DtsCircular.Rows(A).Item("Cuantos")), random.[Next](&H1000000)})
        '    list.Add(DtsCircular.Rows(A).Item("descripcion"))
        '    dtGrafica.Rows.Add(dtRowg)
        '    dtRowg = dtGrafica.NewRow()
        'Next

        'LblCuantosG.Text = LblY.Text & " " & Separador(Cuantos, DdlCAT_REG_OPCION.SelectedValue) & " Reporte Generado A Las " & Now.ToShortTimeString

        'MostrarGrafica(LblReporte.Text, DatosGrafica, "Pie")

        'If DdlGrafica.SelectedValue = "Pie" Then
        '    MostrarGrafica(LblReporte.Text, DatosGrafica, "Pie")
        'ElseIf DdlGrafica.SelectedValue = "Barra" Then
        '    MostrarGrafica(LblReporte.Text, DatosGrafica, "Barra")
        'ElseIf DdlGrafica.SelectedValue = "BarraLateral" Then
        '    MostrarGrafica(LblReporte.Text, DatosGrafica, "BarraLateral")
        'ElseIf DdlGrafica.SelectedValue = "Linea" Then
        '    MostrarGrafica(LblReporte.Text, DatosGrafica, "Linea")
        'ElseIf DdlGrafica.SelectedValue = "Dona" Then
        '    MostrarGrafica(LblReporte.Text, DatosGrafica, "Dona")
        'ElseIf DdlGrafica.SelectedValue = "Area" Then
        '    MostrarGrafica(LblReporte.Text, DatosGrafica, "Area")
        'End If

        'Session("Condiciones") = list
        BtnDetalles.Visible = True
        BtnToCsv.Visible = True
        BtnToXlsx.Visible = True

    End Sub

    Function Fn_Condicion() As String
        'Dim TmpUsr As USUARIO = CType(Session("USUARIO"), USUARIO)
        Dim Where As String = ""
        Dim MError As Integer = 0
        Dim TablaCondiciones As DataTable = Session("TablaCondiciones")
        Try
            For Each row As DataRow In TablaCondiciones.Rows
                Dim Tipo As String = row("TIPO")

                If Tipo = "FECHA" Then
                    Where &= " " & row("VALORCAMPO") & " " & row("VALOROPERADOR") & " CONVERT(date,'" & row("Valor") & "')"
                ElseIf Tipo = "CARACTER" Then
                    If row("VALOROPERADOR") = "In" Or row("VALOROPERADOR") = "Not In" Then
                        Where &= " " & row("VALORCAMPO") & " " & row("VALOROPERADOR") & row("Valor")
                    Else
                        Where &= " UPPER(" & row("VALORCAMPO") & ") " & row("VALOROPERADOR") & " UPPER('" & row("Valor") & "')"
                    End If
                Else
                    Where = Where & " " & row("VALORCAMPO") & " " & row("VALOROPERADOR") & " " & row("Valor")
                End If
                Where &= " " & row("VALORCONECTOR")
            Next
        Catch ex As Exception
            MError = -1
        End Try
        If MError = 0 Then
            Return Where
        Else
            Return MError
        End If
    End Function

    Protected Sub BtnDetalles_Click(sender As Object, e As EventArgs) Handles BtnDetalles.Click
        Try
            gridDetalles.Visible = True
            Exportar()
            Session("gotoEndPage") = True
        Catch ex As Exception
            gridDetalles.Visible = False
            showModal(RadNotification1, "deny", "error", ex.Message.ToString.Replace(Chr(13), "").Replace(Chr(10), "").Replace("""", "").Replace("'", ""))
        End Try
    End Sub

    Protected Sub BtnToCsv_Click(sender As Object, e As EventArgs) Handles BtnToCsv.Click
        Try
            llenaInfo()
            tocsv()
        Catch ex As Exception
            showModal(RadNotification1, "deny", "error", ex.Message.ToString.Replace(Chr(13), "").Replace(Chr(10), "").Replace("""", "").Replace("'", ""))
        End Try
    End Sub
    Protected Sub BtnToXlsx_Click(sender As Object, e As EventArgs) Handles BtnToXlsx.Click
        Try
            llenaInfo()
            toexcel()
        Catch ex As Exception
            showModal(RadNotification1, "deny", "error", ex.Message.ToString.Replace(Chr(13), "").Replace(Chr(10), "").Replace("""", "").Replace("'", ""))
        End Try
    End Sub

    Sub llenaInfo(Optional aditionalCondition As String = "")
        Dim Condicion As String = actualizaetiquetas("0")
        Condicion = Condicion.Replace(" AND AND ", " AND ")
        If Condicion = "-1" Then
            Throw New Exception("No se encontraron resultados")
        Else
            If Condicion <> "" And aditionalCondition <> "" Then
                Condicion = " where " & Condicion & " and " & aditionalCondition
            ElseIf Condicion <> "" And aditionalCondition = "" Then
                Condicion = " where " & Condicion
            ElseIf Condicion = "" And aditionalCondition <> "" Then
                Condicion = " where " & aditionalCondition
            End If

            Condicion = Condicion & " AND PR_MC_AGENCIA IN (" & tmpUSUARIO("CAT_LO_CADENAAGENCIAS") & ")"
            Condicion = Condicion.Replace(" AND AND ", " AND ")

            Dim SSCommand As New SqlCommand
            SSCommand.CommandText = "Sp_Generar_Salida_Reporte"
            SSCommand.CommandType = CommandType.StoredProcedure
            SSCommand.Parameters.Add("@V_Reporteagenerar", SqlDbType.NVarChar).Value = LblReporte.Text
            SSCommand.Parameters.Add("@V_CONDICIONES", SqlDbType.NVarChar).Value = Condicion
            SSCommand.Parameters.Add("@V_Agencia", SqlDbType.NVarChar).Value = tmpUSUARIO("CAT_LO_AGENCIASVER")
            Dim DtsGenerar As DataTable = Consulta_Procedure(SSCommand, "Archs")
            If DtsGenerar.TableName = "Exception" Then
                Throw New Exception(DtsGenerar.Rows(0)(0).ToString.Replace(Chr(13), "").Replace(Chr(10), "").Replace("""", "").Replace("'", ""))
            Else
                tab = DtsGenerar
            End If
        End If
    End Sub

    Sub Exportar(Optional aditionalCondition As String = "")
        Dim Condicion As String = actualizaetiquetas("1")
        If Condicion = "-1" Then
            Throw New Exception("No se encontraron resultados")
        Else
            If Condicion <> "" And aditionalCondition <> "" Then
                Condicion = " where " & Condicion & " and " & aditionalCondition
            ElseIf Condicion <> "" And aditionalCondition = "" Then
                Condicion = " where " & Condicion
            ElseIf Condicion = "" And aditionalCondition <> "" Then
                Condicion = " where " & aditionalCondition
            End If

            'Condicion = Condicion & " AND PR_MC_AGENCIA IN (" & tmpUSUARIO("CAT_LO_CADENAAGENCIAS") & ")"

            Dim SSCommand As New SqlCommand
            SSCommand.CommandText = "Sp_Generar_Salida_Reporte"
            SSCommand.CommandType = CommandType.StoredProcedure
            SSCommand.Parameters.Add("@V_Reporteagenerar", SqlDbType.NVarChar).Value = LblReporte.Text
            SSCommand.Parameters.Add("@V_CONDICIONES", SqlDbType.NVarChar).Value = Condicion
            SSCommand.Parameters.Add("@V_Agencia", SqlDbType.NVarChar).Value = tmpUSUARIO("CAT_LO_AGENCIASVER")

            Dim DtsGenerar As DataTable = Consulta_Procedure(SSCommand, "Archs")


            gridDetalles.DataSource = DtsGenerar
            gridDetalles.Rebind()
            tab = DtsGenerar
            Session("gotoEndPage") = True

        End If
    End Sub

    Protected Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click
        GuardarWhere()
        showModal(RadNotification2, "ok", "Correcto", "Se han guardado las condiciones")
    End Sub

    Sub GuardarWhere()
        '        Where(LblReporte.Text, tmpUSUARIO("CAT_LO_USUARIO"), " ", " ", " ", " ", " ", "", 0, 0, 0)

        Dim TablaCondiciones As DataTable = Session("TablaCondiciones")
        'Le quitamos el ultimo conector a las condiciones para evitar conflictos
        ' If TablaCondiciones.Rows.Count > 0 Then
        'Dim lastrow As Integer = TablaCondiciones.Rows.Count - 1
        'TablaCondiciones.Rows(lastrow)("VALORCONECTOR") = ""
        'TablaCondiciones.AcceptChanges()
        'End If

        For Each row As DataRow In TablaCondiciones.Rows
            Where(LblReporte.Text, tmpUSUARIO("CAT_LO_USUARIO"), row("VALORCAMPO"), row("DESCRIPCIONCAMPO"), row("VALOROPERADOR"), row("Valor"), row("VALORCONECTOR"), row("TIPO"), row("AGRUPADOR"), row("NIVEL"), 1)
        Next
        'TablaCondiciones.Clear()

    End Sub
    Sub Where(ByVal V_Cat_We_Reporte As String, ByVal V_Cat_We_Usuario As String, ByVal V_Cat_We_Campo As String, ByVal V_CAT_WE_CAMPO_DISPLAY As String, ByVal V_Cat_We_Operador As String, ByVal V_Cat_We_Valor As String, ByVal V_Cat_We_Conector As String, ByVal V_Cat_We_Tipo As String, ByVal v_cat_we_Agrupador As Integer, ByVal v_cat_we_Nivel As Integer, ByVal V_Bandera As Integer)
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "Sp_Add_Where"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Cat_We_Reporte", SqlDbType.NVarChar).Value = V_Cat_We_Reporte
        SSCommand.Parameters.Add("@V_Cat_We_Usuario", SqlDbType.NVarChar).Value = V_Cat_We_Usuario
        SSCommand.Parameters.Add("@V_Cat_We_Campo", SqlDbType.NVarChar).Value = V_Cat_We_Campo
        SSCommand.Parameters.Add("@V_CAT_WE_CAMPO_DISPLAY", SqlDbType.NVarChar).Value = V_CAT_WE_CAMPO_DISPLAY
        SSCommand.Parameters.Add("@V_Cat_We_Operador", SqlDbType.NVarChar).Value = V_Cat_We_Operador
        SSCommand.Parameters.Add("@V_Cat_We_Valor", SqlDbType.NVarChar).Value = V_Cat_We_Valor
        SSCommand.Parameters.Add("@V_Cat_We_Conector", SqlDbType.NVarChar).Value = V_Cat_We_Conector
        SSCommand.Parameters.Add("@V_Cat_We_Tipo", SqlDbType.NVarChar).Value = V_Cat_We_Tipo
        SSCommand.Parameters.Add("@V_Cat_We_Agrupador", SqlDbType.NVarChar).Value = v_cat_we_Agrupador
        SSCommand.Parameters.Add("@V_Cat_We_Nivel", SqlDbType.NVarChar).Value = v_cat_we_Nivel
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.Int).Value = V_Bandera

        Dim DtsGenerar As DataTable = Consulta_Procedure(SSCommand, "Where")
        If V_Bandera = 2 Then
            Dim TablaCondiciones As DataTable = Session("TablaCondiciones")
            For Each row As DataRow In DtsGenerar.Rows
                Dim valores As New Hashtable
                valores.Add("campoValue", row("Cat_We_Campo"))
                valores.Add("campoText", row("CAT_WE_CAMPO_DISPLAY"))
                valores.Add("operadorValue", row("Cat_We_Operador"))
                valores.Add("valor", row("Cat_We_Valor"))
                valores.Add("conectorValue", row("Cat_We_Conector"))
                valores.Add("tipo", row("Cat_We_Tipo"))
                valores.Add("agrupador", row("cat_we_Agrupador"))
                valores.Add("nivel", row("cat_we_nivel"))

                Dim operador As String = ""
                Dim conector As String = ""

                Select Case row("Cat_We_Operador")
                    Case ">"
                        operador = "Mayor Que"
                    Case "<"
                        operador = "Menor Que"
                    Case ">="
                        operador = "Mayor O Igual"
                    Case "<="
                        operador = "Menor O Igual"
                    Case "!="
                        operador = "Distinto"
                    Case "="
                        operador = "Igual"
                    Case "Not In"
                        operador = "Que No Contenga"
                    Case "In"
                        operador = "Que Contenga"
                End Select
                valores.Add("operadorText", operador)

                Select Case row("Cat_We_Conector")
                    Case "AND"
                        conector = "Y"
                    Case "OR"
                        conector = "O"
                End Select
                valores.Add("conectorText", conector)

                insertHashToDataTable(valores)
                ' actualizaetiquetas()
            Next
        End If
    End Sub
    Protected Sub BtnLimpiar_Click(sender As Object, e As EventArgs) Handles BtnLimpiar.Click
        Where(LblReporte.Text, tmpUSUARIO("CAT_LO_USUARIO"), " ", " ", " ", " ", " ", "", 0, 0, 0)
        inicializarGrid()
        gridCondiciones.Rebind()
    End Sub

    Private Sub gridCondiciones_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles gridCondiciones.ItemCommand
        Dim comando As String = e.CommandName
        If comando = "InitInsert" Then
            Session("Reporte") = LblReporte.Text
        ElseIf comando = "PerformInsert" Then
            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)

            Dim valores As Hashtable = getGridValues(MyUserControl)
            insertHashToDataTable(valores)
            gridCondiciones.DataBind()

            ' actualizaetiquetas()
        ElseIf comando = "Edit" Then
            Session("Reporte") = LblReporte.Text
            Session("Edit") = True
        ElseIf comando = "Update" Then

            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)

            Dim valores As Hashtable = getGridValues(MyUserControl)
            Dim val As String = valores("consecutivo")
          
            Dim SSCommandId As New SqlCommand
            SSCommandId.CommandText = "SP_ADD_WHERE"
            SSCommandId.CommandType = CommandType.StoredProcedure
            SSCommandId.Parameters.Add("@V_Cat_We_Usuario", SqlDbType.NVarChar).Value = tmpUSUARIO("CAT_LO_USUARIO")
            SSCommandId.Parameters.Add("@V_Cat_We_Reporte", SqlDbType.NVarChar).Value = LblReporte.Text
            SSCommandId.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = 5
            Dim DtsObjetos As DataTable = Consulta_Procedure(SSCommandId, "Reporte")

            Dim TablaCondiciones As DataTable = Session("TablaCondiciones")
            TablaCondiciones.AcceptChanges()
            Session("TablaCondiciones") = TablaCondiciones

            Dim TablaCondiciones2 As DataTable = DtsObjetos
            Dim columna As DataColumn = New DataColumn()
            columna.DataType = System.Type.GetType("System.String")
            columna.AllowDBNull = False
            columna.Caption = "CONSECUTIVO"
            columna.ColumnName = "CONSECUTIVO"
            TablaCondiciones2.Columns.Add(columna)
            Dim totalRows2 As Integer = TablaCondiciones2.Rows.Count
            For index2 As Integer = 0 To totalRows2 - 1
                TablaCondiciones2(index2)("CONSECUTIVO") = index2
            Next

            Dim TablaCondiciones3 As DataTable

            TablaCondiciones3 = TablaCondiciones2.Copy()
            TablaCondiciones3.Merge(TablaCondiciones)
            ' TablaCondiciones.Clear()

            Dim totalRows3 As Integer = TablaCondiciones3.Rows.Count
            For index3 As Integer = 0 To totalRows3 - 1
                TablaCondiciones3(index3)("CONSECUTIVO") = index3
            Next


            Try
                For Each row As DataRow In TablaCondiciones3.Rows
                    If row("CONSECUTIVO") = val Then
                        If IsDBNull(row("CONSECUTIVO2")) Then

                            updateHashToDataTable(valores)
                            gridCondiciones.DataBind()

                            '  actualizaetiquetas()
                        Else
                            Dim SSCommandId2 As New SqlCommand
                            SSCommandId2.CommandText = "SP_ADD_WHERE"
                            SSCommandId2.CommandType = CommandType.StoredProcedure
                            SSCommandId2.Parameters.Add("@V_Cat_We_ID", SqlDbType.NVarChar).Value = row("CONSECUTIVO2")
                            SSCommandId2.Parameters.Add("@V_CAT_WE_CAMPO_DISPLAY", SqlDbType.NVarChar).Value = valores("campoText")
                            SSCommandId2.Parameters.Add("@V_Cat_We_Campo", SqlDbType.NVarChar).Value = valores("campoValue")
                            SSCommandId2.Parameters.Add("@V_Cat_We_Operador", SqlDbType.NVarChar).Value = valores("operadorValue")
                            SSCommandId2.Parameters.Add("@V_Cat_We_Valor", SqlDbType.NVarChar).Value = valores("valor")
                            SSCommandId2.Parameters.Add("@V_Cat_We_Conector", SqlDbType.NVarChar).Value = valores("conectorValue")
                            SSCommandId2.Parameters.Add("@V_Cat_We_Tipo", SqlDbType.NVarChar).Value = valores("tipo")
                            SSCommandId2.Parameters.Add("@V_Cat_We_Agrupador", SqlDbType.NVarChar).Value = valores("agrupador")
                            SSCommandId2.Parameters.Add("@V_Cat_We_Nivel", SqlDbType.NVarChar).Value = valores("nivel")
                            SSCommandId2.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = 4
                            Dim DtsObjetos2 As DataTable = Consulta_Procedure(SSCommandId2, "Reporte")
                            updateHashToDataTable(valores)
                            gridCondiciones.DataBind()
                            '   actualizaetiquetas()
                        End If

                        Exit For
                    End If
                Next
            Catch ex As Exception
                Dim mensaje As Exception = ex
            End Try

        ElseIf comando = "Delete" Then
            Dim RowID As String = e.Item.Cells(4).Text

            Dim SSCommandId As New SqlCommand
            SSCommandId.CommandText = "SP_ADD_WHERE"
            SSCommandId.CommandType = CommandType.StoredProcedure
            SSCommandId.Parameters.Add("@V_Cat_We_Usuario", SqlDbType.NVarChar).Value = tmpUSUARIO("CAT_LO_USUARIO")
            SSCommandId.Parameters.Add("@V_Cat_We_Reporte", SqlDbType.NVarChar).Value = LblReporte.Text
            SSCommandId.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = 5
            Dim DtsObjetos As DataTable = Consulta_Procedure(SSCommandId, "Reporte")

            Dim TablaCondiciones As DataTable = Session("TablaCondiciones")
            TablaCondiciones.AcceptChanges()
            Session("TablaCondiciones") = TablaCondiciones

            Dim TablaCondiciones2 As DataTable = DtsObjetos
            Dim columna As DataColumn = New DataColumn()
            columna.DataType = System.Type.GetType("System.String")
            columna.AllowDBNull = False
            columna.Caption = "CONSECUTIVO"
            columna.ColumnName = "CONSECUTIVO"
            TablaCondiciones2.Columns.Add(columna)
            Dim totalRows2 As Integer = TablaCondiciones2.Rows.Count
            For index2 As Integer = 0 To totalRows2 - 1
                TablaCondiciones2(index2)("CONSECUTIVO") = index2
            Next

            'Dim totalRows2 As Integer = TablaCondiciones2.Rows.Count
            'For index2 As Integer = 0 To totalRows2 - 1
            '    TablaCondiciones2(index2)("CONSECUTIVO") = DtsObjetos(index2)("CONSECUTIVO").ToString()
            'Next

            'TablaCondiciones2.AcceptChanges()
            'DtsObjetos = TablaCondiciones2

            Dim TablaCondiciones3 As DataTable

            TablaCondiciones3 = TablaCondiciones2.Copy()
            TablaCondiciones3.Merge(TablaCondiciones)

            Dim totalRows3 As Integer = TablaCondiciones3.Rows.Count
            For index3 As Integer = 0 To totalRows3 - 1
                TablaCondiciones3(index3)("CONSECUTIVO") = index3
            Next

            Dim totalRows0 As Integer = TablaCondiciones.Rows.Count
            Dim uno As Int32 = 0
            For index3 As Integer = 0 To totalRows0 - 1
                TablaCondiciones(index3)("CONSECUTIVO") = (totalRows3 - 1) + uno
                uno = uno + 1
            Next



            'Dim totalRows As Integer = TablaCondiciones3.Rows.Count
            'For index As Integer = 0 To totalRows - 1
            '    TablaCondiciones(index)("CONSECUTIVO") = index
            'Next


            ' TablaCondiciones.AcceptChanges()
            ' Session("TablaCondiciones") = TablaCondiciones



            For Each row As DataRow In TablaCondiciones3.Rows
                If row("CONSECUTIVO") = RowID Then
                    If IsDBNull(row("CONSECUTIVO2")) Then
                        For Each row2 As DataRow In TablaCondiciones.Rows
                            If row2("CONSECUTIVO") = row("CONSECUTIVO") Then
                                row2.Delete()
                                ' TablaCondiciones.AcceptChanges()
                                Session("TablaCondiciones") = TablaCondiciones
                                'updateIndexTablaCondiciones()
                                ' actualizaetiquetas()
                            End If
                        Next


                    Else

                        Dim valor As String = row("CONSECUTIVO2")
                        Dim SSCommandId2 As New SqlCommand
                        SSCommandId2.CommandText = "SP_ADD_WHERE"
                        SSCommandId2.CommandType = CommandType.StoredProcedure
                        SSCommandId2.Parameters.Add("@V_Cat_We_ID", SqlDbType.NVarChar).Value = valor
                        SSCommandId2.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = 3
                        Dim DtsObjetos2 As DataTable = Consulta_Procedure(SSCommandId2, "Reporte")
                        ' updateIndexTablaCondiciones()
                        'actualizaetiquetas()
                    End If

                    Exit For
                End If
            Next

        End If
    End Sub



    Private Sub updateHashToDataTable(valores As Hashtable)
        Dim SSCommandId As New SqlCommand
        SSCommandId.CommandText = "SP_ADD_WHERE"
        SSCommandId.CommandType = CommandType.StoredProcedure
        SSCommandId.Parameters.Add("@V_Cat_We_Usuario", SqlDbType.NVarChar).Value = tmpUSUARIO("CAT_LO_USUARIO")
        SSCommandId.Parameters.Add("@V_Cat_We_Reporte", SqlDbType.NVarChar).Value = LblReporte.Text
        SSCommandId.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = 5
        Dim DtsObjetos As DataTable = Consulta_Procedure(SSCommandId, "Reporte")

        Dim TablaCondiciones As DataTable = Session("TablaCondiciones")
        TablaCondiciones.AcceptChanges()
        Session("TablaCondiciones") = TablaCondiciones

        Dim TablaCondiciones2 As DataTable = DtsObjetos
        Dim columna As DataColumn = New DataColumn()
        columna.DataType = System.Type.GetType("System.String")
        columna.AllowDBNull = False
        columna.Caption = "CONSECUTIVO"
        columna.ColumnName = "CONSECUTIVO"
        TablaCondiciones2.Columns.Add(columna)
        Dim totalRows2 As Integer = TablaCondiciones2.Rows.Count
        For index2 As Integer = 0 To totalRows2 - 1
            TablaCondiciones2(index2)("CONSECUTIVO") = index2
        Next

        Dim TablaCondiciones3 As DataTable

        TablaCondiciones3 = TablaCondiciones2.Copy()
        TablaCondiciones3.Merge(TablaCondiciones)

        Dim totalRows3 As Integer = TablaCondiciones3.Rows.Count
        For index3 As Integer = 0 To totalRows3 - 1
            TablaCondiciones3(index3)("CONSECUTIVO") = index3
        Next


        Dim totalRows0 As Integer = TablaCondiciones.Rows.Count
        Dim uno As Int32 = 0
        For index3 As Integer = 0 To totalRows0 - 1
            TablaCondiciones(index3)("CONSECUTIVO") = (totalRows3 - 1) + uno
            uno = uno + 1
        Next

        For Each row As DataRow In TablaCondiciones.Rows
            If row("CONSECUTIVO") = valores("consecutivo") Then
                row("DESCRIPCIONCAMPO") = valores("campoText")
                row("VALORCAMPO") = valores("campoValue")
                row("DESCRIPCIONOPERADOR") = valores("operadorText")
                row("VALOROPERADOR") = valores("operadorValue")
                row("Valor") = valores("valor")
                row("TIPO") = valores("tipo")
                row("DESCRIPCIONCONECTOR") = valores("conectorText")
                row("VALORCONECTOR") = valores("conectorValue")
                row("AGRUPADOR") = valores("agrupador")
                row("NIVEL") = valores("nivel")
                TablaCondiciones.AcceptChanges()
                Exit For
            End If
        Next
        Session("TablaCondiciones") = TablaCondiciones
    End Sub

    Private Sub updateIndexTablaCondiciones()

        'Dim TablaCondiciones As DataTable = Session("TablaCondiciones")

        'Dim totalRows As Integer = TablaCondiciones.Rows.Count
        'For index As Integer = 0 To totalRows - 1
        '    TablaCondiciones(index)("CONSECUTIVO") = index
        'Next
        'TablaCondiciones.AcceptChanges()
        'Session("TablaCondiciones") = TablaCondiciones


        Dim SSCommandId As New SqlCommand
        SSCommandId.CommandText = "SP_ADD_WHERE"
        SSCommandId.CommandType = CommandType.StoredProcedure
        SSCommandId.Parameters.Add("@V_Cat_We_Usuario", SqlDbType.NVarChar).Value = tmpUSUARIO("CAT_LO_USUARIO")
        SSCommandId.Parameters.Add("@V_Cat_We_Reporte", SqlDbType.NVarChar).Value = LblReporte.Text
        SSCommandId.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = 5
        Dim DtsObjetos As DataTable = Consulta_Procedure(SSCommandId, "Reporte")

        Dim TablaCondiciones As DataTable = Session("TablaCondiciones")
        TablaCondiciones.Merge(DtsObjetos)
        Dim totalRows As Integer = TablaCondiciones.Rows.Count
        For index As Integer = 0 To totalRows - 1
            TablaCondiciones(index)("CONSECUTIVO") = index
        Next

        TablaCondiciones.AcceptChanges()
        ' Session("TablaCondiciones") = TablaCondiciones



        'Dim totalRows2 As Integer = TablaCondiciones2.Rows.Count
        'For index2 As Integer = 0 To totalRows2 - 1
        '    TablaCondiciones2(index2)("CONSECUTIVO") = DtsObjetos(index2)("CONSECUTIVO").ToString()
        'Next

        'TablaCondiciones2.AcceptChanges()
        'DtsObjetos = TablaCondiciones2




    End Sub

    Private Sub gridCondiciones_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles gridCondiciones.NeedDataSource

        Dim SSCommandId As New SqlCommand
        SSCommandId.CommandText = "SP_ADD_WHERE"
        SSCommandId.CommandType = CommandType.StoredProcedure
        SSCommandId.Parameters.Add("@V_Cat_We_Usuario", SqlDbType.NVarChar).Value = tmpUSUARIO("CAT_LO_USUARIO")
        SSCommandId.Parameters.Add("@V_Cat_We_Reporte", SqlDbType.NVarChar).Value = LblReporte.Text
        SSCommandId.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = 5
        Dim DtsObjetos As DataTable = Consulta_Procedure(SSCommandId, "Reporte")


        Dim TablaCondiciones2 As DataTable = DtsObjetos
        Dim columna As DataColumn = New DataColumn()
        columna.DataType = System.Type.GetType("System.String")
        columna.AllowDBNull = False
        columna.Caption = "CONSECUTIVO"
        columna.ColumnName = "CONSECUTIVO"
        TablaCondiciones2.Columns.Add(columna)
        Dim TablaCondiciones As DataTable = Session("TablaCondiciones")



        '  If DtsObjetos.Rows.Count >= 1 Then

        ' TablaCondiciones = Session("TablaCondiciones")
        'End If

        If TablaCondiciones.Rows.Count <= 1 Then
            TablaCondiciones.AcceptChanges()
            Session("TablaCondiciones") = TablaCondiciones
            ' 
        Else
            Dim totalRows As Integer = TablaCondiciones.Rows.Count
            For index As Integer = 0 To totalRows - 1
                TablaCondiciones(index)("CONSECUTIVO") = index
            Next
            TablaCondiciones.AcceptChanges()
            Session("TablaCondiciones") = TablaCondiciones
            'BtnGuardarTablaCondiciones.Clear()
        End If

        Dim totalRows2 As Integer = TablaCondiciones2.Rows.Count
        For index2 As Integer = 0 To totalRows2 - 1
            TablaCondiciones2(index2)("CONSECUTIVO") = index2
        Next



        TablaCondiciones2.AcceptChanges()
        DtsObjetos = TablaCondiciones2




        Dim TablaCondiciones3 As DataTable

        TablaCondiciones3 = TablaCondiciones2.Copy()
        TablaCondiciones3.Merge(TablaCondiciones)

        Dim totalRows3 As Integer = TablaCondiciones3.Rows.Count
        For index3 As Integer = 0 To totalRows3 - 1
            TablaCondiciones3(index3)("CONSECUTIVO") = index3
        Next

        gridCondiciones.DataSource = TablaCondiciones3




    End Sub


    Private Sub insertHashToDataTable(valores As Hashtable)
        Dim TablaCondiciones As DataTable = Session("TablaCondiciones")

        Dim row As DataRow = TablaCondiciones.NewRow

        row("CONSECUTIVO") = TablaCondiciones.Rows.Count

        row("DESCRIPCIONCAMPO") = valores("campoText")
        row("VALORCAMPO") = valores("campoValue")
        row("DESCRIPCIONOPERADOR") = valores("operadorText")
        row("VALOROPERADOR") = valores("operadorValue")
        row("Valor") = valores("valor")
        row("TIPO") = valores("tipo")
        row("DESCRIPCIONCONECTOR") = valores("conectorText")
        row("VALORCONECTOR") = valores("conectorValue")
        row("AGRUPADOR") = valores("agrupador")
        row("NIVEL") = valores("nivel")


        TablaCondiciones.Rows.Add(row)

        Session("TablaCondiciones") = TablaCondiciones



    End Sub

    Private Sub inicializarGrid()
        Dim TablaCondiciones As New DataTable("TablaCondiciones")
        TablaCondiciones.Columns.Add("CONSECUTIVO")
        TablaCondiciones.Columns.Add("DESCRIPCIONCAMPO")
        TablaCondiciones.Columns.Add("VALORCAMPO")
        TablaCondiciones.Columns.Add("DESCRIPCIONOPERADOR")
        TablaCondiciones.Columns.Add("VALOROPERADOR")
        TablaCondiciones.Columns.Add("Valor")
        TablaCondiciones.Columns.Add("TIPO")
        TablaCondiciones.Columns.Add("DESCRIPCIONCONECTOR")
        TablaCondiciones.Columns.Add("VALORCONECTOR")
        TablaCondiciones.Columns.Add("AGRUPADOR")
        TablaCondiciones.Columns.Add("NIVEL")
        Session("TablaCondiciones") = TablaCondiciones
    End Sub

    Public Shared Function getGridValues(usrControl As UserControl) As Hashtable
        Dim valores As New Hashtable
        Dim V_Multiple As String = 0

        If CType(usrControl.FindControl("RadMultiple"), RadComboBox).Visible = True Then
            V_Multiple = 1
        End If

        Try
            valores.Add("tablaValue", CType(usrControl.FindControl("comboTablas"), RadComboBox).SelectedValue)
            valores.Add("tablaText", CType(usrControl.FindControl("comboTablas"), RadComboBox).SelectedItem.Text)
        Catch ex As Exception
            valores.Add("tablaText", "")
        End Try

        Try
            valores.Add("campoValue", CType(usrControl.FindControl("comboCampo"), RadComboBox).SelectedValue)
            valores.Add("campoText", CType(usrControl.FindControl("comboCampo"), RadComboBox).SelectedItem.Text)
        Catch ex As Exception
            valores.Add("campoText", "")
        End Try

        Try
            valores.Add("operadorValue", CType(usrControl.FindControl("DdlOperador"), RadComboBox).SelectedValue)
            valores.Add("operadorText", CType(usrControl.FindControl("DdlOperador"), RadComboBox).SelectedItem.Text)
        Catch ex As Exception
            valores.Add("operadorText", "")
        End Try

        Try
            valores.Add("conectorValue", CType(usrControl.FindControl("DdlConector"), RadComboBox).SelectedValue)
            valores.Add("conectorText", CType(usrControl.FindControl("DdlConector"), RadComboBox).SelectedItem.Text)
        Catch ex As Exception
            valores.Add("conectorText", "")
        End Try

        Try
            valores.Add("agrupador", CType(usrControl.FindControl("BtnInicioFin"), RadButton).SelectedToggleState.Value)
        Catch ex As Exception
            valores.Add("agrupador", "")
        End Try

        Try
            valores.Add("nivel", CType(usrControl.FindControl("NumNivel"), RadNumericTextBox).Text)
        Catch ex As Exception
            valores.Add("nivel", "")
        End Try

        valores.Add("consecutivo", CType(usrControl.FindControl("lblConsecutivo"), RadLabel).Text)

        Dim tipo As String = CType(usrControl.FindControl("comboCampo"), RadComboBox).SelectedItem.Attributes.Item("Tipo")
        valores.Add("tipo", tipo)
        Select Case tipo
            Case "NUMERO"
                If V_Multiple = 1 Then
                    valores.Add("valor", "(" & Rad_Ncadena(CType(usrControl.FindControl("RadMultiple"), RadComboBox)) & ")")
                Else
                    valores.Add("valor", CType(usrControl.FindControl("NumValores"), RadNumericTextBox).Text)
                End If
            Case "FECHA"
                If V_Multiple = 1 Then
                    valores.Add("valor", "(" & Rad_Tcadena(CType(usrControl.FindControl("RadMultiple"), RadComboBox)) & ")")
                Else
                    Dim control = CType(usrControl.FindControl("DteValores"), RadDatePicker)
                    Dim fecha As String = control.DateInput.Text.Substring(0, 10)
                    valores.Add("valor", fecha)
                End If
            Case Else
                If V_Multiple = 1 Then
                    valores.Add("valor", "(" & Rad_Tcadena(CType(usrControl.FindControl("RadMultiple"), RadComboBox)) & ")")
                Else
                    valores.Add("valor", CType(usrControl.FindControl("TxtValores"), RadTextBox).Text)
                End If
        End Select
        Return valores
    End Function

    Public Shared Function Rad_Tcadena(ByRef v_item As RadComboBox) As String
        Dim v_cadena As String = ""
        Dim collection As List(Of String) = New List(Of String)

        For Each item As RadComboBoxItem In v_item.CheckedItems
            collection.Add(item.Value)
        Next

        v_cadena = "'" & String.Join("','", collection) & "'"
        Return v_cadena
    End Function

    Public Shared Function Rad_Ncadena(ByRef v_item As RadComboBox) As String
        Dim v_cadena As String = ""
        Dim collection As List(Of String) = New List(Of String)

        For Each item As RadComboBoxItem In v_item.CheckedItems
            collection.Add(item.Value)
        Next

        v_cadena = String.Join(",", collection)
        Return v_cadena
    End Function

    Private Sub ddlCampoAgrupar_PreRender(sender As Object, e As EventArgs) Handles ddlCampoAgrupar.PreRender
        If ddlCampoAgrupar.Items.Count = 0 Then
            Dim DtsVariable As DataTable = GenerarReporte("", "", LblReporte.Text, "", "", 1)
            For Each row As DataRow In DtsVariable.Rows
                Dim item As New DropDownListItem(row("DESC").ToString, row("VALOR").ToString)
                item.Attributes.Add("Tipo", row("TIPO").ToString.Trim(" "))
                ddlCampoAgrupar.Items.Add(item)
            Next
            ddlCampoAgrupar.DataBind()
            Dim ddlitem As DropDownListItem = ddlCampoAgrupar.FindItemByText(Session("Agrupar"))
            Try
                ddlitem.Selected = True
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub ddlSumarContar_PreRender(sender As Object, e As EventArgs) Handles ddlSumarContar.PreRender
        If ddlSumarContar.Items.Count = 0 Then
            Dim DtsVariable As DataTable = GenerarReporte("", "", LblReporte.Text, "", "", 1)
            For Each row As DataRow In DtsVariable.Rows
                Dim item As New DropDownListItem(row("DESC").ToString, row("VALOR").ToString)
                item.Attributes.Add("Tipo", row("TIPO").ToString.Trim(" "))
                ddlSumarContar.Items.Add(item)
            Next
            ddlSumarContar.DataBind()
            Try
                Dim ddlitem As DropDownListItem = ddlSumarContar.FindItemByText(Session("SumCount"))
                If ddlitem.Attributes("Tipo") = "CARACTER" And btnSumarContar.SelectedToggleStateIndex = 0 Then
                    showModal(RadNotification1, "deny", "Imposible sumar", "Campo '" & ddlitem.Text & "' no se puede sumar")
                    ddlSumarContar.Focus()
                Else
                    ddlitem.Selected = True
                    btnSumarContar.Text = "Contar"
                End If
            Catch ex As Exception

            End Try
            Try
                Dim DtsReporteCount As DataTable = GenerarReporte("", "", LblReporte.Text, "", "", 7)

                If DtsReporteCount.Rows.Count > 0 Then
                    LblTipo.Text = DtsReporteCount.Rows(0)("Campo")
                Else
                    LblTipo.Text = 0
                End If
                Dim disableditem = ddlSumarContar.FindItemByText(LblTipo.Text)
                'disableditem.Enabled = False
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub ChartExample_PreRender(sender As Object, e As EventArgs) Handles ChartExample.PreRender
        Dim item As New Object
        Select Case RBLGrafica.SelectedValue
            Case 1
                item = New AreaSeries
            Case 2
                item = New LineSeries
            Case 3
                item = New ColumnSeries
            Case 4
                item = New BarSeries
            Case 5
                item = New DonutSeries
            Case 6
                item = New PieSeries
            Case Else
                item = New LineSeries
        End Select
        item.Name = "Ejemplo"
        Dim SItem = New SeriesItem(5, 50)
        item.Items.Add(SItem)
        For i As Integer = 1 To 9
            SItem = New SeriesItem(i, i * 2)
            item.Items.Add(SItem)
        Next
        Try
            item.MissingValues = HtmlChart.MissingValuesBehavior.Zero
        Catch ex As Exception

        End Try
        ChartExample.PlotArea.Series.Clear()
        ChartExample.PlotArea.Series.Add(item)
    End Sub

    Private Sub btnInfoChart_Click(sender As Object, e As EventArgs) Handles btnInfoChart.Click
        Try
            gridDetalles.Visible = True
            Dim extraCondicion As String = ""
            If ddlCampoAgrupar.SelectedItem.Attributes("Tipo") = "FECHA" Then
                extraCondicion = " convert(varchar," & ddlCampoAgrupar.SelectedItem.Value & ",103) = Convert(varchar,convert(date,'" & lblGroupValue.Text & "',103),103)"
            Else
                extraCondicion = " replace(" & ddlCampoAgrupar.SelectedItem.Value & ",'""','') = '" & lblGroupValue.Text & "'"
            End If
            Exportar(extraCondicion)
        Catch ex As Exception
            gridDetalles.Visible = False
            showModal(RadNotification1, "deny", "error", "Error al mostrar los detalles. Intente más tarde")
        End Try
    End Sub

    Private Sub gridDetalles_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles gridDetalles.ItemCommand
        If e.CommandName = Telerik.Web.UI.RadGrid.ExportToCsvCommandName Then
            gridDetalles.DataSource = tab
            'gridDetalles.MasterTableView.ExportToCSV()
            tocsv()
        ElseIf e.CommandName = Telerik.Web.UI.RadGrid.ExportToExcelCommandName Then
            gridDetalles.DataSource = tab
            'gridDetalles.MasterTableView.ExportToExcel()
            toexcel()
        End If
    End Sub
    Private Sub toexcel()
        'Dim arch_Excel As String = StrRuta() & "Salida\" & "Reporte Automatico MC Collect" & "_" & DateTime.Now.ToString("ddMMyyyy_hhmm") & ".xlsx"
        'Dim book As New Workbook()
        'Dim sheet As Worksheet = book.Worksheets(0)

        'sheet.InsertDataTable(tab, True, 1, 1)
        'sheet.Name = "Detalle"
        'book.SaveToFile(arch_Excel, ExcelVersion.Version2016)

        'If File.Exists(arch_Excel) Then
        '    Dim ioflujo As FileInfo = New FileInfo(arch_Excel)
        '    Response.Clear()
        '    Response.AddHeader("Content-Disposition", "attachment; filename=" + ioflujo.Name)
        '    Response.AddHeader("Content-Length", ioflujo.Length.ToString())
        '    Response.ContentType = "application/octet-stream"
        '    Response.WriteFile(arch_Excel)
        '    Response.End()
        'End If

        If tab.Rows.Count >= 1000000 Then
            showModal(RadNotification1, "deny", "error", "Error al exportar a xlsx. Excede el numero de registros permitidos.")
        Else
            ExportToXcel_SomeReport(tab, "Rep_" & Request.Url.ToString().Split("?")(1) & "_" & DateTime.Now.ToString("ddMMyyyy_hhmm"), Me)
        End If

    End Sub
    Private Sub tocsv()
        'Dim arch_Excel As String = StrRuta() & "Salida\" & "Reporte Automatico MC Collect" & "_" & DateTime.Now.ToString("ddMMyyyy_hhmm") & ".csv"
        'Dim book As New Workbook()
        'Dim sheet As Worksheet = book.Worksheets(0)
        'sheet.InsertDataTable(tab, True, 1, 1)
        'sheet.Name = "Detalle"
        'book.SaveToFile(arch_Excel, FileFormat.CSV)

        'If File.Exists(arch_Excel) Then
        '    Dim ioflujo As FileInfo = New FileInfo(arch_Excel)
        '    Response.Clear()
        '    Response.AddHeader("Content-Disposition", "attachment; filename=" + ioflujo.Name)
        '    Response.AddHeader("Content-Length", ioflujo.Length.ToString())
        '    Response.ContentType = "application/octet-stream"
        '    Response.WriteFile(arch_Excel)
        '    Response.End()
        'End If


        If tab.Rows.Count > 0 Then
            Dim StrNomArhc As String = "Rep_" & Request.Url.ToString().Split("?")(1) & "_" & DateTime.Now.ToString("ddMMyyyy_hhmm") & ".csv"
            Dim v_archivo As String = StrRuta() & "Salida\" & StrNomArhc
            SubExisteRuta(StrRuta() & "Salida\")
            Dim fs As Stream
            fs = New System.IO.FileStream(v_archivo, IO.FileMode.OpenOrCreate)
            Dim sw As New System.IO.StreamWriter(fs)


            Dim v_encabezados As String = ""
            For r As Integer = 0 To tab.Columns.Count - 1
                Dim v_valor As String = ""

                v_valor = Chr(34) & tab.Columns(r).ColumnName & Chr(34)

                If tab.Columns.Count - 1 <> r Then
                    v_valor = v_valor & ","
                End If

                v_encabezados = v_encabezados & v_valor
            Next
            sw.WriteLine(v_encabezados)

            Dim cuantos1 As Integer = tab.Rows.Count
            For e As Integer = 0 To cuantos1 - 1
                Dim v_registros As String = ""
                For r As Integer = 0 To tab.Columns.Count - 1
                    Dim v_valor As String = ""

                    If Not IsDBNull(tab.Rows(e)(tab.Columns(r).ColumnName)) Then
                        v_valor = Chr(34) & tab.Rows(e)(tab.Columns(r).ColumnName) & Chr(34)
                    Else
                        v_valor = Chr(34) & " " & Chr(34)
                    End If

                    If tab.Columns.Count - 1 <> r Then
                        v_valor = v_valor & ","
                    End If

                    v_registros = v_registros & v_valor
                Next
                sw.WriteLine(v_registros)
            Next

            sw.Close()
            fs.Close()

            Dim ioflujo As FileInfo = New FileInfo(v_archivo)
            Response.Clear()
            Response.AddHeader("Content-Disposition", "attachment; filename=" + ioflujo.Name)
            Response.AddHeader("Content-Length", ioflujo.Length.ToString())
            Response.ContentType = "application/octet-stream"
            Response.WriteFile(v_archivo)
            Response.End()


        End If



    End Sub
    Private Sub gridDetalles_PreRender(sender As Object, e As EventArgs) Handles gridDetalles.PreRender
        If Session("gotoEndPage") Then
            Session.Remove("gotoEndPage")
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "godown", "gotoEndPage();", True)
        End If
    End Sub

    Private Sub pnlGrafica_PreRender(sender As Object, e As EventArgs) Handles pnlGrafica.PreRender
        If Session("gotoEndPage") Then
            Session.Remove("gotoEndPage")
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "godown", "gotoEndPage();", True)
        End If
    End Sub
    Private Function actualizaetiquetas(valor As String) As String
        LblCondicion.Text = ""
        LblCondicionChida.Text = ""


        Dim SSCommandId As New SqlCommand
        SSCommandId.CommandText = "SP_ADD_WHERE"
        SSCommandId.CommandType = CommandType.StoredProcedure
        SSCommandId.Parameters.Add("@V_Cat_We_Usuario", SqlDbType.NVarChar).Value = tmpUSUARIO("CAT_LO_USUARIO")
        SSCommandId.Parameters.Add("@V_Cat_We_Reporte", SqlDbType.NVarChar).Value = LblReporte.Text
        SSCommandId.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = 6
        Dim DtsObjetos As DataTable = Consulta_Procedure(SSCommandId, "Reporte")

        Dim TablaCondiciones As DataTable = Session("TablaCondiciones")
        TablaCondiciones.AcceptChanges()


        Dim TablaCondiciones2 As DataTable = DtsObjetos


        '  Dim totalRows2 As Integer = TablaCondiciones2.Rows.Count
        'For index2 As Integer = 0 To totalRows2 - 1
        '    TablaCondiciones2(index2)("CONSECUTIVO") = DtsObjetos(index2)("CONSECUTIVO").ToString()
        'Next

        TablaCondiciones2.AcceptChanges()
        DtsObjetos = TablaCondiciones2

        Dim TablaCondiciones3 As DataTable

        TablaCondiciones3 = TablaCondiciones2.Copy()
        TablaCondiciones3.Merge(TablaCondiciones)







        Dim MError As Integer = 0
        Dim Donde As String = ""
        If btnCondiciones.Checked Then
            Try
                For Each fila As DataRow In TablaCondiciones3.Rows
                    Dim parentesis As String = ""
                    For i = 1 To CType(fila.Item("NIVEL"), Integer)
                        If fila.Item("AGRUPADOR") = 1 Then
                            parentesis += "("
                        ElseIf fila.Item("AGRUPADOR") = 2 Then
                            parentesis += ")"
                        End If
                    Next

                    Dim Tipo As String = fila("TIPO")


                    If Tipo = "FECHA" Then
                        Donde = " CONVERT(DATE," & fila("VALORCAMPO") & ") " & fila("VALOROPERADOR") & " CONVERT(date,'" & fila("Valor") & "')"
                    ElseIf Tipo = "CARACTER" Then
                        If fila("VALOROPERADOR") = "In" Or fila("VALOROPERADOR") = "Not In" Then
                            Donde = " " & fila("VALORCAMPO") & " " & fila("VALOROPERADOR") & fila("Valor")
                        Else
                            Donde = " UPPER(" & fila("VALORCAMPO") & ") " & fila("VALOROPERADOR") & " UPPER('" & fila("Valor") & "')"
                        End If
                    Else
                        Donde = " " & fila("VALORCAMPO") & " " & fila("VALOROPERADOR") & " " & fila("Valor")
                    End If

                    If valor = "0" Then
                        If fila.Item("AGRUPADOR") = 0 Then
                            LblCondicion.Text += " " & fila.Item("DESCRIPCIONCAMPO") & " " & fila.Item("DESCRIPCIONOPERADOR") & " " & fila.Item("Valor") & " " & fila.Item("DESCRIPCIONCONECTOR")
                            LblCondicionChida.Text += " " & Donde & " " & fila.Item("VALORCONECTOR")
                        ElseIf fila.Item("AGRUPADOR") = 1 Then
                            LblCondicion.Text += " " & parentesis & fila.Item("DESCRIPCIONCAMPO") & " " & fila.Item("DESCRIPCIONOPERADOR") & " " & fila.Item("Valor") & " " & fila.Item("DESCRIPCIONCONECTOR")
                            LblCondicionChida.Text += " " & parentesis & Donde & " " & fila.Item("VALORCONECTOR")
                        Else
                            LblCondicion.Text += " " & fila.Item("DESCRIPCIONCAMPO") & " " & fila.Item("DESCRIPCIONOPERADOR") & " " & fila.Item("Valor") & " " & parentesis & fila.Item("DESCRIPCIONCONECTOR")
                            LblCondicionChida.Text += " " & Donde & parentesis & " " & fila.Item("VALORCONECTOR")
                        End If
                    Else
                        If fila.Item("AGRUPADOR") = 0 Then
                            LblCondicion.Text += " " & fila.Item("DESCRIPCIONCAMPO") & " " & fila.Item("DESCRIPCIONOPERADOR") & " " & fila.Item("Valor")
                            LblCondicionChida.Text += " " & Donde
                        ElseIf fila.Item("AGRUPADOR") = 1 Then
                            LblCondicion.Text += " " & parentesis & fila.Item("DESCRIPCIONCAMPO") & " " & fila.Item("DESCRIPCIONOPERADOR") & " " & fila.Item("Valor")
                            LblCondicionChida.Text += " " & parentesis & Donde
                        Else
                            LblCondicion.Text += " " & fila.Item("DESCRIPCIONCAMPO") & " " & fila.Item("DESCRIPCIONOPERADOR") & " " & fila.Item("Valor") & " " & parentesis
                            LblCondicionChida.Text += " " & Donde & parentesis
                        End If
                    End If


                Next
            Catch ex As Exception
                MError = -1
            End Try
        End If
        If btnArchivo.Checked Then
            Try
                LblCondicionChida.Text = " _CREDITO_ in "
                LblCondicionChida.Text &= "(SELECT [TMP_CREDITO] FROM TMP_REPORTE_CREDITOS WHERE [TMP_NOMBRE_REPORTE] = '" & LblReporte.Text & "' and [TMP_USUARIO_ID] = '" & tmpUSUARIO("CAT_LO_ID") & "' )"
            Catch ex As Exception
                MError = -1
            End Try
        End If
        If MError = 0 Then
            Return LblCondicionChida.Text
        Else
            Return MError
        End If

    End Function

    Private Sub btnCondiciones_CheckedChanged(sender As Object, e As EventArgs) Handles btnCondiciones.CheckedChanged
        If btnCondiciones.Checked = True Then

            pnlArchivo.Visible = False
            pnlCondiciones.Visible = True
        End If
    End Sub

    Private Sub btnArchivo_CheckedChanged(sender As Object, e As EventArgs) Handles btnArchivo.CheckedChanged
        If btnArchivo.Checked = True Then
            pnlArchivo.Visible = True
            pnlCondiciones.Visible = False
        End If
    End Sub

    Private Sub btnProcesarArchivo_Click(sender As Object, e As EventArgs) Handles btnProcesarArchivo.Click
        If auCreditos.UploadedFiles.Count = 0 Then
            showModal(RadNotification2, "deny", "Error", "Selecciona un archivo csv o txt")
        Else
            ResetTMP_REPORTE_CREDITO()
            Dim archivo = auCreditos.UploadedFiles(0)
            Dim streamRead As New StreamReader(archivo.InputStream)

            hfCreditos.Value = ""

            'Expresion regular que detecta creditos
            Dim regrexCredito As New Regex("(\d+){1}", RegexOptions.IgnoreCase Or RegexOptions.Compiled)
            Dim valido As Boolean = False
            Dim creditos As New List(Of String)
            While Not streamRead.EndOfStream
                Dim linea As String = streamRead.ReadLine

                'Nos aseguramos que las lineas no estén vacías
                If Not (String.IsNullOrEmpty(linea) Or String.IsNullOrWhiteSpace(linea)) Then

                    'Procesamos la linea para detectar que solo exista un credito por linea
                    Dim cuantosMatches = regrexCredito.Matches(linea)
                    valido = IIf(cuantosMatches.Count = 1, True, False)

                    If valido Then
                        creditos.Add(linea)
                    Else
                        hfCreditos.Value = "Error en """ & linea & """"
                        showModal(RadNotification2, "deny", "Error al procesar el archivo", "Error en """ & linea & """")
                        Exit While
                    End If
                End If

            End While

            If valido Then
                hfCreditos.Value = "ok"
                GuardarCreditos(String.Join(",", creditos))
                showModal(RadNotification2, "ok", "Correcto", "Archivo procesado correctamente (" & creditos.Count & " creditos).")
            End If

        End If
    End Sub

    Private Sub GuardarCreditos(credito As String)
        Dim SSCommandId As New SqlCommand
        SSCommandId.CommandText = "SP_GENERAR_REPORTE"
        SSCommandId.CommandType = CommandType.StoredProcedure
        SSCommandId.Parameters.Add("@V_Campo", SqlDbType.NVarChar).Value = tmpUSUARIO("CAT_LO_ID")
        SSCommandId.Parameters.Add("@V_Reporte", SqlDbType.NVarChar).Value = LblReporte.Text
        SSCommandId.Parameters.Add("@V_Valor", SqlDbType.NVarChar).Value = credito
        SSCommandId.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = 10
        Dim DtsObjetos As DataTable = Consulta_Procedure(SSCommandId, "Reporte")
    End Sub

    Private Sub ResetTMP_REPORTE_CREDITO()
        Dim SSCommandId As New SqlCommand
        SSCommandId.CommandText = "SP_GENERAR_REPORTE"
        SSCommandId.CommandType = CommandType.StoredProcedure
        SSCommandId.Parameters.Add("@V_Campo", SqlDbType.NVarChar).Value = tmpUSUARIO("CAT_LO_ID")
        SSCommandId.Parameters.Add("@V_Reporte", SqlDbType.NVarChar).Value = LblReporte.Text
        SSCommandId.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = 9
        Dim DtsObjetos As DataTable = Consulta_Procedure(SSCommandId, "Reporte")
    End Sub

    Protected Sub ExportToXcel_SomeReport(dt As DataTable, fileName As String, page As Page)
        'dt.Columns.RemoveAt(0)
        Dim recCount = dt.Rows.Count
        fileName = String.Format(fileName, DateTime.Now.ToString("MMddyyyy_hhmmss"))
        Dim xlsx = New XLWorkbook()
        'Dim ws = xlsx.Worksheets.Add(fileName)
        Dim ws = xlsx.Worksheets.Add("Datos")
        ws.Style.Font.Bold = False

        'ws.Cell(1, 1).InsertTable(dt.AsEnumerable)

        'For co As Integer = 1 To dt.Columns.Count + 1
        '    ws.Column(co).AdjustToContents()
        'Next

        ''For co As Integer = 1 To dt.Columns.Count + 1
        ''    Dim Com As String = ws.Column(co).Cell(1).Value.ToString
        ''    If ws.Column(co).Cell(1).Value.ToString Like "[Mm][Oo][Nn][Tt][Oo]*" Or ws.Column(co).Cell(1).Value.ToString Like "[Ss][Aa][Ll][Dd][Oo]*" Or ws.Column(co).Cell(1).Value.ToString Like "[Ss][Vv][+][Ll][Ff]*" Or ws.Column(co).Cell(1).Value.ToString Like "[Ll][Aa][Tt][Ee]*" Then  'Formato para montos
        ''        For i = 1 To ws.Rows.Count
        ''            Dim Com2 As String = ws.Column(co).Cell(i).Value.ToString
        ''            If ws.Column(co).Cell(i).Value.ToString Like "[$]*" Then
        ''                ws.Column(co).Cell(i).Value = ws.Column(co).Cell(i).Value.ToString.Substring(1)
        ''            End If
        ''        Next
        ''        For i = 1 To ws.Rows.Count
        ''            ws.Columns(co).Cells(i).SetDataType(XLCellValues.Number)
        ''        Next
        ''        ws.Columns(co).Style.NumberFormat.Format = "$ #,##0.00"
        ''    End If

        ''    If ws.Column(co).Cell(1).Value.ToString Like "[Ff][Ee][Cc][Hh][Aa]*" Then  'Formato para Fechas
        ''        Dim j As Integer
        ''        For j = 1 To ws.Rows.Count

        ''            Try
        ''                ws.Columns(co).Cells(j).SetDataType(XLCellValues.DateTime)
        ''            Catch ex As Exception
        ''                ws.Columns(co).Cells(j).Value = Nothing
        ''                ws.Columns(co).Cells(j).SetDataType(XLCellValues.DateTime)
        ''            End Try
        ''        Next

        ''    End If
        ''    ws.Column(co).AdjustToContents()
        ''Next

        'ws.Tables.Table(0).ShowAutoFilter = False
        'ws.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.General

        '''''''''''''''' CICLO ''''''''''''''''''''''''''''''''
        For r As Integer = 0 To tab.Columns.Count - 1
            ws.Cell(1, r + 1).Value = tab.Columns(r).ColumnName
            ws.Cell(1, r + 1).SetDataType(XLCellValues.Text)
            ws.Cell(1, r + 1).Style.Fill.BackgroundColor = XLColor.BlueGray
            ws.Cell(1, r + 1).Style.Font.FontColor = XLColor.White
            ws.Cell(1, r + 1).Style.Font.Bold = True
            ws.Column(r + 1).AdjustToContents()
        Next

        For e As Integer = 0 To tab.Rows.Count - 1

            For r As Integer = 0 To tab.Columns.Count - 1
                Dim v_valor As String = ""

                If Not IsDBNull(tab.Rows(e)(tab.Columns(r).ColumnName)) Then
                    v_valor = tab.Rows(e)(tab.Columns(r).ColumnName)
                Else
                    v_valor = Nothing
                End If

                ws.Cell(e + 2, r + 1).Value = v_valor
                If tab.Columns(r).ColumnName.ToLower Like "*credito*" Or tab.Columns(r).ColumnName = "Referencia Bancaria" Then
                    ws.Cell(e + 2, r + 1).SetDataType(XLCellValues.Text)
                End If
            Next
        Next
        ''''''''''''''' CICLO ''''''''''''''''''''''''''''''''
        'ws.Workbook.SaveAs("E:\Cargas\AvonMX\Salida\archivo.xlsx")

        DynaGenExcelFile(fileName, page, xlsx)

    End Sub

    Protected Sub DynaGenExcelFile(fileName As String, page As Page, xlsx As XLWorkbook)
        Try
            page.Response.ClearContent()
            page.Response.ClearHeaders()
            page.Response.ContentType = "application/vnd.ms-excel"
            page.Response.AppendHeader("Content-Disposition", String.Format("attachment;filename={0}.xlsx", fileName))

            Using memoryStream As New MemoryStream()
                xlsx.SaveAs(memoryStream)
                memoryStream.WriteTo(page.Response.OutputStream)
            End Using
            page.Response.Flush()
            page.Response.[End]()
        Catch ex As Exception
            Dim msj As String = ex.Message
        End Try


    End Sub
End Class
