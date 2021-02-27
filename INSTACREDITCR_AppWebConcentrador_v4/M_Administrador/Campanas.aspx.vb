Imports Telerik.Web.UI
Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports Db
Imports System.Web.SessionState.HttpSessionState
Imports System.Data
Imports Funciones
Imports System.Web.Services
Imports ClosedXML.Excel
Imports System.IO
Imports System.Net
Imports System.Diagnostics
Imports RestSharp
Imports Newtonsoft.Json
Imports Microsoft.Win32.TaskScheduler

Partial Class _Campanas
    Inherits System.Web.UI.Page
    Dim estado As String
    Dim ERRORS As String = "1"
    Dim SKIP As String = "1"
    Dim ctlCarga As String
    Dim logCarga As String
    Dim badCarga As String
    Dim Ruta As String = StrRuta() & "Campanas\"
    Dim api As New calixtaAPI.clsCalixtaAPI("www.calixtaondemand.com", "80", "jose.rojo@mccollect.com.mx", "45862", "f92341408245de375053e64d0c94c08b0de80aae50372965bef65ee6c177870b")
    ''' <summary>
    ''' Establece los parametros para mostrar una notificacion
    ''' </summary>
    ''' <param name="Notificacion">Objeto RadNotification de Telerik</param>
    ''' <param name="icono">info - delete - deny - edit - ok - warning - none</param>
    ''' <param name="titulo">título de la notificación</param>
    ''' <param name="msg">mensaje de la notificación</param>
    Public Shared Sub showModal(ByRef Notificacion As Object, ByVal icono As String, ByVal titulo As String, ByVal msg As String)
        Dim radnot As RadNotification = TryCast(Notificacion, RadNotification)
        radnot.TitleIcon = icono
        radnot.ContentIcon = icono
        radnot.Title = titulo
        radnot.Text = msg
        radnot.Show()
    End Sub
    Public Property tmpUSUARIO As IDictionary
        Get
            Return CType(Session("USUARIO"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("USUARIO") = value
        End Set
    End Property
    Private Function Guardar(hash As Hashtable) As Boolean
        Dim result As Boolean = False
        Try
            Dim V_Hora_Aceptable As Integer = 0
            Dim V_txtVigencia As String
            Dim V_dteProgramacion As String
            If hash("comboProgramacion") = "A" Then
                Dim SSCommandCFEC As New SqlCommand("SP_Valida_horagestion")
                SSCommandCFEC.CommandType = CommandType.StoredProcedure
                SSCommandCFEC.Parameters.Add("@V_FECHA", SqlDbType.NVarChar).Value = hash("dteProgramacion").ToString.Substring(11, 2)
                V_Hora_Aceptable = Consulta_Procedure(SSCommandCFEC, "Catalogo").Rows(0).Item("RESULTADO")
                V_dteProgramacion = hash("dteProgramacion").ToString.Replace(".", "").Replace(" m", "m")
                V_txtVigencia = hash("txtVigencia").ToString.Replace(".", "").Replace(" m", "m")
            End If

            If V_Hora_Aceptable = 0 Then
                Dim V_comboTipoCampana As String
                Dim V_comboReglasGlobales As String
                Dim V_Mensaje As String
                If hash("comboProveedor") = "CALIXTA" Then
                    V_comboTipoCampana = hash("comboTipoCampana")
                    V_comboReglasGlobales = hash("comboReglasGlobales")
                    V_Mensaje = hash("txtMensaje")
                ElseIf hash("comboProveedor") = "INCONCERT" Then
                    V_comboTipoCampana = hash("comboTipoCampanaI")
                    V_comboReglasGlobales = hash("comboReglasGlobalesI")
                    V_Mensaje = ""
                ElseIf hash("comboProveedor") = "LSC Communications" Then
                    V_comboTipoCampana = " "
                    V_Mensaje = hash("txtMensaje")
                    V_comboReglasGlobales = hash("comboReglasGlobalesCarteo")
                    Try
                        V_txtVigencia = hash("TxtSimulacion").ToString.Replace(".", "").Replace(" m", "m")
                    Catch ex As Exception
                    End Try
                End If


                Dim SSCommandCat As New SqlCommand("SP_CAMPANA_MSJ")
                SSCommandCat.CommandType = CommandType.StoredProcedure
                SSCommandCat.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 5
                SSCommandCat.Parameters.Add("@v_nombre", SqlDbType.NVarChar).Value = hash("txtNombre")
                SSCommandCat.Parameters.Add("@v_tipo", SqlDbType.NVarChar).Value = V_comboTipoCampana
                SSCommandCat.Parameters.Add("@v_reglasGlobales", SqlDbType.NVarChar).Value = V_comboReglasGlobales
                SSCommandCat.Parameters.Add("@v_proveedores", SqlDbType.NVarChar).Value = hash("comboProveedor")
                SSCommandCat.Parameters.Add("@v_tpProgramacion", SqlDbType.NVarChar).Value = hash("comboProgramacion")
                SSCommandCat.Parameters.Add("@v_dteProgramacion", SqlDbType.NVarChar).Value = V_dteProgramacion.Substring(0, 10) & " " & V_dteProgramacion.Substring(11, 8).Replace("-", ":")
                SSCommandCat.Parameters.Add("@v_vigencia", SqlDbType.NVarChar).Value = V_txtVigencia
                SSCommandCat.Parameters.Add("@v_mensaje", SqlDbType.NVarChar).Value = V_Mensaje
                SSCommandCat.Parameters.Add("@V_TIPOMSJ", SqlDbType.NVarChar).Value = hash("RDDLTipoMsj")
                SSCommandCat.Parameters.Add("@V_TIPOTEL", SqlDbType.NVarChar).Value = hash("RDDLTipoTel")
                SSCommandCat.Parameters.Add("@v_rol", SqlDbType.NVarChar).Value = hash("RCBRol")
                SSCommandCat.Parameters.Add("@v_prefijo", SqlDbType.NVarChar).Value = hash("RCBPrefijo")
                SSCommandCat.Parameters.Add("@v_plantilla", SqlDbType.NVarChar).Value = hash("NombrePlantilla")
                SSCommandCat.Parameters.Add("@v_Usuario", SqlDbType.NVarChar).Value = tmpUSUARIO("CAT_LO_USUARIO")
                SSCommandCat.Parameters.Add("@V_instancia", SqlDbType.NVarChar).Value = tmpUSUARIO("CAT_LO_INSTANCIA")
                Dim dtab As DataTable = Consulta_Procedure(SSCommandCat, "Catalogo")
                If dtab.TableName = "Exception" Then
                    showModal(RadNotification1, "deny", "Error de sistema", dtab.Rows(0).Item("Mensaje"))
                Else
                    showModal(RadNotification1, "ok", "Correcto", "Camapaña guardada correctamente")
                    result = True
                End If



                'End If
            Else
                showModal(RadNotification1, "deny", "Datos erroneos", "Por favor seleccione un horario laborable")
            End If

        Catch ex As Exception
            showModal(RadNotification1, "deny", "Error de sistema", ex.Message)
        End Try
        Return result
    End Function

    Private Sub gridCampanas_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles gridCampanas.ItemCommand
        If e.CommandName = "Delete" Then
            Dim item As GridItem = CType(e.Item, GridItem)
            Dim campana As GridTableCell = CType(item.Controls(6), GridTableCell)
            Dim SSCommandCat As New SqlCommand("SP_CAMPANA_MSJ")
            SSCommandCat.CommandType = CommandType.StoredProcedure
            SSCommandCat.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 7
            SSCommandCat.Parameters.Add("@v_nombre", SqlDbType.NVarChar).Value = campana.Text
            Consulta_Procedure(SSCommandCat, "Catalogo")
        ElseIf e.CommandName = "onCargaMasiva" Then
            Dim item As GridItem = CType(e.Item, GridItem)
            Dim V_Campana As GridTableCell = CType(item.Controls(6), GridTableCell)
            Dim V_Proveedor As GridTableCell = CType(item.Controls(7), GridTableCell)
            Dim V_Tipo As GridTableCell = CType(item.Controls(8), GridTableCell)
            pnlCargaMasiva.Visible = True
            lblCampana.Text = V_Campana.Text
            LblProveedor.Text = V_Proveedor.Text
            LblTipo.Text = V_Tipo.Text
        ElseIf e.CommandName = "onEjecutar" Then
            Dim item As GridItem = CType(e.Item, GridItem)
            Dim V_Campana As GridTableCell = CType(item.Controls(6), GridTableCell)
            Dim V_Proveedor As GridTableCell = CType(item.Controls(7), GridTableCell)
            Dim V_Tipo As GridTableCell = CType(item.Controls(8), GridTableCell)
            taskea(V_Campana.Text)
            Generar(V_Proveedor.Text, V_Campana.Text, V_Tipo.Text)

        ElseIf e.CommandName = "Edit" Then
            Session("Edit") = True
        ElseIf e.CommandName = "InitInsert" Then
            Session("InitInsert") = True
        ElseIf e.CommandName = RadGrid.PerformInsertCommandName Or e.CommandName = RadGrid.UpdateCommandName Then
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores As New Hashtable
            valores = getControlsHash(MyUserControl.Controls)
            If valores("comboProveedor") = "INCONCERT" Then
                valores.Add("comboTipoCampanaI", CType(MyUserControl.FindControl("comboTipoCampanaI"), RadComboBox).SelectedValue)
                valores.Add("comboReglasGlobalesI", CType(MyUserControl.FindControl("comboReglasGlobalesI"), RadComboBox).SelectedValue)
            ElseIf valores("comboProveedor") = "CALIXTA" Then
                valores.Add("NombrePlantilla", CType(MyUserControl.FindControl("RDDLTipoMsj"), RadComboBox).SelectedItem.Text)
                valores.Add("RDDLTipoTel", CType(MyUserControl.FindControl("RDDLTipoTel"), RadComboBox).SelectedValue)
                valores.Add("comboTipoCampana", CType(MyUserControl.FindControl("comboTipoCampana"), RadComboBox).SelectedValue)
                valores.Add("comboReglasGlobales", CType(MyUserControl.FindControl("comboReglasGlobales"), RadComboBox).SelectedValue)
                valores.Add("comboProgramacion", CType(MyUserControl.FindControl("comboProgramacion"), RadComboBox).SelectedValue)
                valores.Add("dteProgramacion", CType(MyUserControl.FindControl("dteProgramacion"), RadDateTimePicker).ValidationDate)
                valores.Add("txtVigencia", CType(MyUserControl.FindControl("txtVigencia"), RadDatePicker).SelectedDate)
                valores.Add("RDDLTipoMsj", CType(MyUserControl.FindControl("RDDLTipoMsj"), RadComboBox).SelectedValue)
                valores.Add("txtMensaje", CType(MyUserControl.FindControl("txtMensaje"), RadTextBox).Text)
                valores.Add("RCBRol", CType(MyUserControl.FindControl("RCBRol"), RadComboBox).SelectedValue)
                valores.Add("RCBPrefijo", CType(MyUserControl.FindControl("RCBPrefijo"), RadComboBox).SelectedValue)
            ElseIf valores("comboProveedor") = "LSC Communications" Then
                valores.Add("txtMensaje", CType(MyUserControl.FindControl("TxtMensajeC"), RadTextBox).Text)
                valores.Add("TxtSimulacion", CType(MyUserControl.FindControl("TxtSimulacion"), RadDatePicker).SelectedDate)
                valores.Add("comboReglasGlobalesCarteo", CType(MyUserControl.FindControl("comboReglasGlobalesCarteo"), RadComboBox).SelectedValue)
            End If
            If Not Guardar(valores) Then
                e.Canceled = True
            End If
        ElseIf e.CommandName = "onSaldo" Then
            Dim saldos As calixtaAPI.Saldo() = api.obtieneSaldo()
            Dim LtrlSaldo As String
            LtrlSaldo = "<table>"
            For Each saldo As calixtaAPI.Saldo In saldos
                Try
                    LtrlSaldo += "<tr class=""Izquierda""><td>" & ConceptoSaldo(saldo.id) & "</td><td>" & saldo.disponible & "</td></tr>"
                Catch ex As Exception
                End Try
            Next
            LtrlSaldo += "</table>"

            Dim item As GridItem = CType(e.Item, GridItem)
            Dim V_Campana As GridTableCell = CType(item.Controls(6), GridTableCell)
            Dim V_Proveedor As GridTableCell = CType(item.Controls(7), GridTableCell)
            Dim V_Tipo As GridTableCell = CType(item.Controls(8), GridTableCell)
            Dim V_Bandera As String
            If V_Proveedor.Text = "CALIXTA" Then
                If V_Tipo.Text = "SMS" Then
                    V_Bandera = 1
                ElseIf V_Tipo.Text = "BLASTER" Then
                    V_Bandera = 2
                ElseIf V_Tipo.Text = "CORREO" Then
                    V_Bandera = 3
                End If
                Dim SSCommandCat As New SqlCommand("SP_ENVIO_CAMPANA")
                SSCommandCat.CommandType = CommandType.StoredProcedure
                SSCommandCat.Parameters.Add("@V_Campana", SqlDbType.NVarChar).Value = V_Campana.Text
                SSCommandCat.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = V_Bandera
                Dim DtsInfo As DataTable = Consulta_Procedure(SSCommandCat, "Catalogo")
                showModal(RadNotification1, "ok", "Simulacion", "Se Enviaran " & DtsInfo.Rows.Count & " " & V_Tipo.Text & " Y Usted Cuenta Con " & LtrlSaldo & " De Saldo")
            Else
                showModal(RadNotification1, "deny", "Simulacion", "No Se Pude Generar Simulacion De Este Proveedor")
            End If


        ElseIf e.CommandName = "Estatus" Then
            Dim item As GridItem = CType(e.Item, GridItem)
            Dim campana As GridTableCell = CType(item.Controls(6), GridTableCell)
            Dim estatus As GridTableCell = CType(item.Controls(7), GridTableCell)
            Dim SSCommandCat As New SqlCommand("SP_CAMPANA_MSJ")
            SSCommandCat.CommandType = CommandType.StoredProcedure
            SSCommandCat.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 10
            SSCommandCat.Parameters.Add("@v_nombre", SqlDbType.NVarChar).Value = campana.Text
            SSCommandCat.Parameters.Add("@v_activa", SqlDbType.NVarChar).Value = estatus.Text
            Consulta_Procedure(SSCommandCat, "Catalogo")
            gridCampanas.Rebind()
        End If
    End Sub

    Sub Generar(ByVal V_Provedor As String, V_Campana As String, V_Tipo As String)
        If V_Provedor = "CALIXTA" Then
            If V_Tipo = "SMS" Then
                Calixta(V_Campana, 1, V_Tipo)
            ElseIf V_Tipo = "BLASTER" Then
                Calixta(V_Campana, 2, V_Tipo)
            ElseIf V_Tipo = "CORREO" Then
                Calixta(V_Campana, 3, V_Tipo)
            End If
        ElseIf V_Provedor = "INCONCERT" Then
            Dim SSCommandCat As New SqlCommand("SP_RP_INCONCERT")
            SSCommandCat.CommandType = CommandType.StoredProcedure
            SSCommandCat.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = 1
            SSCommandCat.Parameters.Add("@V_Campana", SqlDbType.NVarChar).Value = V_Campana
            Dim DtsInconcert As DataTable = Consulta_Procedure(SSCommandCat, "Reporte")
            If DtsInconcert.Rows.Count > 0 Then
                Dim V_Nombre As String = ExportToXcel_SomeReport(DtsInconcert, "inconcert", Me)
                If File.Exists(V_Nombre) Then
                    Dim ioflujo As FileInfo = New FileInfo(V_Nombre)
                    Response.Clear()
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + ioflujo.Name)
                    Response.AddHeader("Content-Length", ioflujo.Length.ToString())
                    Response.ContentType = "application/octet-stream"
                    Response.WriteFile(V_Nombre)
                    Response.End()
                End If
            End If
        ElseIf V_Provedor = "LSC Communications" Then
            Dim SSCommandSim As New SqlCommand("SP_RP_LSC")
            SSCommandSim.CommandType = CommandType.StoredProcedure
            SSCommandSim.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = 0
            SSCommandSim.Parameters.Add("@V_Campana", SqlDbType.NVarChar).Value = V_Campana
            Dim DtsLSCSim As DataTable = Consulta_Procedure(SSCommandSim, "Reporte")
            Dim V_Error As String

            Dim SSCommandCat As New SqlCommand("SP_RP_LSC")
            SSCommandCat.CommandType = CommandType.StoredProcedure
            SSCommandCat.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = 1
            SSCommandCat.Parameters.Add("@V_Campana", SqlDbType.NVarChar).Value = V_Campana
            Dim DtsLSC As DataTable = Consulta_Procedure(SSCommandCat, "Reporte")


            If DtsLSCSim.Rows(0).Item("Fecha") <> "No" Then

                For Each rowx As DataRow In DtsLSC.Rows
                    Dim objeto As Class_P_Saldos = Consultasaldoscredito(rowx("CREDITO"), rowx("cliente"), DtsLSCSim.Rows(0).Item("Fecha"))
                    If objeto.Estatus = "Error" Then
                        If objeto.Informacion(0).mensajeSafi.ToString() = "No es posible conectar con el servidor remoto" Then
                            V_Error = objeto.Informacion(0).mensajeSafi.ToString & "No es posible conectar con el servidor remoto"
                        Else
                            V_Error = objeto.Informacion(0).mensajeSafi.ToString
                        End If
                        Exit For
                    End If
                    Ejecuta("update tmp_LSC set Imp_ponerse_corriente=to_number('" & objeto.Informacion(0).saldoPCorriente & "','999999999.9999999'), Imp_ponerse_corriente_1=to_number('" & objeto.Informacion(0).saldoPCorriente & "','999999999.9999999'), Monto_con_Letra=FN_NUMERO_TO_LETRA('" & objeto.Informacion(0).saldoPCorriente & "')  where Num_Crdto='" & rowx("credito") & "'")
                Next
            End If


            Dim SSCommandRes As New SqlCommand
            SSCommandRes.CommandText = "SP_RP_LSC"
            SSCommandRes.CommandType = CommandType.StoredProcedure
            SSCommandRes.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = 3
            SSCommandRes.Parameters.Add("@V_Campana", SqlDbType.NVarChar).Value = ""
            Dim DtsLSCRes As DataTable = Consulta_Procedure(SSCommandRes, "Reporte")
            If DtsLSCRes.Rows.Count > 0 Then
                Dim V_Nombre As String = ExportToXcel_SomeReportLSC(DtsLSCRes, "LSC", Me)
                If File.Exists(V_Nombre) Then
                    Dim ioflujo As FileInfo = New FileInfo(V_Nombre)
                    Response.Clear()
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + ioflujo.Name)
                    Response.AddHeader("Content-Length", ioflujo.Length.ToString())
                    Response.ContentType = "application/octet-stream"
                    Response.WriteFile(V_Nombre)
                    Response.End()
                End If
            End If
        End If
    End Sub

    Protected Function ExportToXcel_SomeReportLSC(dt As DataTable, fileName As String, page As Page) As String
        fileName = String.Format(fileName, DateTime.Now.ToString("MMddyyyy_hhmmss"))
        Dim xlsx = New XLWorkbook()
        Dim ws = xlsx.Worksheets.Add(fileName)
        Dim frow As Integer = dt.Rows.Count
        Dim cuenta As Integer = 2
        ws.Style.Font.SetFontSize(11)
        ws.Style.Font.SetFontName("Calibri")
        ws.Rows().Height = 15
        ws.Cell("A1").Value = "Nombre"
        ws.Cell("B1").Value = "condonacion"
        ws.Cell("C1").Value = "Tot Fact Impg"
        ws.Cell("D1").Value = "Imp_ponerse_corriente"
        ws.Cell("E1").Value = "fecha de generacion"
        ws.Cell("F1").Value = "Fecha Sdo Sim"
        ws.Cell("G1").Value = "Nombre"
        ws.Cell("H1").Value = "Num_Crdto"
        ws.Cell("I1").Value = "Imp ponersecorriente"
        ws.Cell("J1").Value = "Monto con Letra"
        ws.Cell("K1").Value = "Leyenda"
        ws.Cell("L1").Value = "Direccion"
        ws.Cell("M1").Value = "Colonia"
        ws.Cell("N1").Value = "COD POSTAL AG"
        ws.Cell("O1").Value = "Nom Municipio"

        For Each row As DataRow In dt.Rows
            ws.Cell(cuenta, 1).Value = row("NOMBRE").ToString
            ws.Cell(cuenta, 2).Value = row("CONDONACION").ToString
            ws.Cell(cuenta, 3).Value = row("TOT_FACT_IMPG").ToString
            ws.Cell(cuenta, 4).Value = row("IMP_PONERSE_CORRIENTE").ToString
            ws.Cell(cuenta, 5).Value = row("FECHA_DE_GENERACION").ToString
            ws.Cell(cuenta, 6).Value = row("FECHA_SDO_SIM").ToString
            ws.Cell(cuenta, 7).Value = row("NOMBRE_1").ToString
            ws.Cell(cuenta, 8).Value = row("NUM_CRDTO").ToString
            ws.Cell(cuenta, 9).Value = row("IMP_PONERSE_CORRIENTE_1").ToString
            ws.Cell(cuenta, 10).Value = row("MONTO_CON_LETRA").ToString
            ws.Cell(cuenta, 11).Value = row("LEYENDA").ToString
            ws.Cell(cuenta, 12).Value = row("DIRECCION").ToString
            ws.Cell(cuenta, 13).Value = row("COLONIA").ToString
            ws.Cell(cuenta, 14).Value = row("COD_POSTAL_AG").ToString
            ws.Cell(cuenta, 15).Value = row("NOM_MUNICIPIO").ToString

            cuenta = cuenta + 1
        Next
        For co As Integer = 1 To 15
            ws.Column(co).AdjustToContents()
        Next
        'For co As Integer = 1 To 15
        '    If co <> 2 Then
        '        For ro As Integer = 1 To dt.Rows.Count + 1
        '            ws.Cell(ro, co).SetDataType(XLCellValues.Text)
        '        Next
        '    End If
        'Next
        ws.PageSetup.PageOrientation = XLPageOrientation.Landscape
        ws.PageSetup.AdjustTo(36)
        ws.PageSetup.PaperSize = XLPaperSize.LetterPaper
        ws.PageSetup.VerticalDpi = 600
        ws.PageSetup.HorizontalDpi = 600
        Dim V_Nombre As String = StrRuta() & "Salida\" & "LSC" & DateTime.Now.ToString("yyyyMMdd") & ".xlsx"
        xlsx.SaveAs(V_Nombre)
        Return V_Nombre
    End Function
    Protected Function ExportToXcel_SomeReport(dt As DataTable, fileName As String, page As Page) As String
        fileName = String.Format(fileName, DateTime.Now.ToString("MMddyyyy_hhmmss"))
        Dim xlsx = New XLWorkbook()
        Dim ws = xlsx.Worksheets.Add(fileName)
        Dim frow As Integer = dt.Rows.Count
        Dim cuenta As Integer = 2
        ws.Style.Font.SetFontSize(11)
        ws.Style.Font.SetFontName("Calibri")
        ws.Rows().Height = 15
        ws.Cell("A1").Value = "NUM_CRDTO"
        ws.Cell("B1").Value = "NUM_CLNT_T"
        ws.Cell("C1").Value = "NOMBRE_TITULAR"
        ws.Cell("D1").Value = "ROL_T"
        ws.Cell("E1").Value = "CALLE_T"
        ws.Cell("F1").Value = "COLONIA_T"
        ws.Cell("G1").Value = "NOM_MUNICIPIO_T"
        ws.Cell("H1").Value = "NOM_EDO_PROVICIA_T"
        ws.Cell("I1").Value = "CP_T"
        ws.Cell("J1").Value = "CASA_1_T"
        ws.Cell("K1").Value = "CASA_2_T"
        ws.Cell("L1").Value = "CASA_3_T"
        ws.Cell("M1").Value = "CELULAR_1_T"
        ws.Cell("N1").Value = "CELULAR_2_T"
        ws.Cell("O1").Value = "CELULAR_3_T"
        ws.Cell("P1").Value = "OFICINA_T"
        ws.Cell("Q1").Value = "FAMILIAR_T"
        ws.Cell("R1").Value = "RECADOS_T"
        ws.Cell("S1").Value = "CORREO_ELECTRONICO_T"
        ws.Cell("T1").Value = "ACTIVIDAD_ECONOMICA_T"
        ws.Cell("U1").Value = "PUESTO_T"
        ws.Cell("V1").Value = "EMPRESA_LABORA_T"
        ws.Cell("W1").Value = "DESCR_OCUPACION_SCCB_T"
        ws.Cell("X1").Value = "DESCRIP_PUESTO_SCCB_T"
        ws.Cell("Y1").Value = "NUM_CLNT_CD"
        ws.Cell("Z1").Value = "NOMBRE_CODEUDOR"
        ws.Cell("AA1").Value = "ROL_CD"
        ws.Cell("AB1").Value = "DIRECCION_CD"
        ws.Cell("AC1").Value = "COLONIA_CD"
        ws.Cell("AD1").Value = "NOM_MUNICIPIO_CD"
        ws.Cell("AE1").Value = "NOM_EDO_PROVICIA_CD"
        ws.Cell("AF1").Value = "CP_CD"
        ws.Cell("AG1").Value = "CASA_1_CD"
        ws.Cell("AH1").Value = "CASA_2_CD"
        ws.Cell("AI1").Value = "CASA_3_CD"
        ws.Cell("AJ1").Value = "CELULAR_1_CD"
        ws.Cell("AK1").Value = "CELULAR_2_CD"
        ws.Cell("AL1").Value = "CELULAR_3_CD"
        ws.Cell("AM1").Value = "OFICINA_CD"
        ws.Cell("AN1").Value = "FAMILIAR_CD"
        ws.Cell("AO1").Value = "RECADOS_CD"
        ws.Cell("AP1").Value = "NUM_CLNT_AV1"
        ws.Cell("AQ1").Value = "NOMBRE_AVAL1"
        ws.Cell("AR1").Value = "ROL_AV1"
        ws.Cell("AS1").Value = "DIRECCION_AV1"
        ws.Cell("AT1").Value = "COLONIA_AV1"
        ws.Cell("AU1").Value = "NOM_MUNICIPIO_AV1"
        ws.Cell("AV1").Value = "NOM_EDO_PROVICIA_AV1"
        ws.Cell("AW1").Value = "CP_AV1"
        ws.Cell("AX1").Value = "CASA_1_AV1"
        ws.Cell("AY1").Value = "CASA_2_AV1"
        ws.Cell("AZ1").Value = "CASA_3_AV1"
        ws.Cell("BA1").Value = "CELULAR_1_AV1"
        ws.Cell("BB1").Value = "CELULAR_2_AV1"
        ws.Cell("BC1").Value = "CELULAR_3_AV1"
        ws.Cell("BD1").Value = "OFICINA_AV1"
        ws.Cell("BE1").Value = "FAMILIAR_AV1"
        ws.Cell("BF1").Value = "RECADOS_AV1"
        ws.Cell("BG1").Value = "NUM_CLNT_AV2"
        ws.Cell("BH1").Value = "NOMBRE_AVAL2"
        ws.Cell("BI1").Value = "ROL_AV2"
        ws.Cell("BJ1").Value = "DIRECCION_AV2"
        ws.Cell("BK1").Value = "COLONIA_AV2"
        ws.Cell("BL1").Value = "NOM_MUNICIPIO_AV2"
        ws.Cell("BM1").Value = "NOM_EDO_PROVICIA_AV2"
        ws.Cell("BN1").Value = "CP_AV2"
        ws.Cell("BO1").Value = "CASA_1_AV2"
        ws.Cell("BP1").Value = "CASA_2_AV2"
        ws.Cell("BQ1").Value = "CASA_3_AV2"
        ws.Cell("BR1").Value = "CELULAR_1_AV2"
        ws.Cell("BS1").Value = "CELULAR_2_AV2"
        ws.Cell("BT1").Value = "CELULAR_3_AV2"
        ws.Cell("BU1").Value = "OFICINA_AV2"
        ws.Cell("BV1").Value = "FAMILIAR_AV2"
        ws.Cell("BW1").Value = "RECADOS_AV2"
        ws.Cell("BX1").Value = "NUM_CLNT_AV3"
        ws.Cell("BY1").Value = "NOMBRE_AVAL3"
        ws.Cell("BZ1").Value = "ROL_AV3"
        ws.Cell("CA1").Value = "DIRECCION_AV3"
        ws.Cell("CB1").Value = "COLONIA_AV3"
        ws.Cell("CC1").Value = "NOM_MUNICIPIO_AV3"
        ws.Cell("CD1").Value = "NOM_EDO_PROVICIA_AV3"
        ws.Cell("CE1").Value = "CP_AV3"
        ws.Cell("CF1").Value = "CASA_1_AV3"
        ws.Cell("CG1").Value = "CASA_2_AV3"
        ws.Cell("CH1").Value = "CASA_3_AV3"
        ws.Cell("CI1").Value = "CELULAR_1_AV3"
        ws.Cell("CJ1").Value = "CELULAR_2_AV3"
        ws.Cell("CK1").Value = "CELULAR_3_AV3"
        ws.Cell("CL1").Value = "OFICINA_AV3"
        ws.Cell("CM1").Value = "FAMILIAR_AV3"
        ws.Cell("CN1").Value = "RECADOS_AV3"
        ws.Cell("CO1").Value = "NUM_CLNT_AV4"
        ws.Cell("CP1").Value = "NOMBRE_AVAL4"
        ws.Cell("CQ1").Value = "ROL_AV4"
        ws.Cell("CR1").Value = "DIRECCION_AV4"
        ws.Cell("CS1").Value = "COLONIA_AV4"
        ws.Cell("CT1").Value = "NOM_MUNICIPIO_AV4"
        ws.Cell("CU1").Value = "NOM_EDO_PROVICIA_AV4"
        ws.Cell("CV1").Value = "CP_AV4"
        ws.Cell("CW1").Value = "CASA_1_AV4"
        ws.Cell("CX1").Value = "CASA_2_AV4"
        ws.Cell("CY1").Value = "CASA_3_AV4"
        ws.Cell("CZ1").Value = "CELULAR_1_AV4"
        ws.Cell("DA1").Value = "CELULAR_2_AV4"
        ws.Cell("DB1").Value = "CELULAR_3_AV4"
        ws.Cell("DC1").Value = "OFICINA_AV4"
        ws.Cell("DD1").Value = "FAMILIAR_AV4"
        ws.Cell("DE1").Value = "RECADOS_AV4"
        ws.Cell("DF1").Value = "CENTRO"
        ws.Cell("DG1").Value = "MONTO_OTORGADO"
        ws.Cell("DH1").Value = "FECHA_OTORGAMIENTO"
        ws.Cell("DI1").Value = "PLAZO"
        ws.Cell("DJ1").Value = "FECHA_VNCNTO"
        ws.Cell("DK1").Value = "FREC_PAGO_DIAS"
        ws.Cell("DL1").Value = "RANGO"
        ws.Cell("DM1").Value = "DIAS_MORA"
        ws.Cell("DN1").Value = "SDO_TOTAL"
        ws.Cell("DO1").Value = "EPRC_CONST"
        ws.Cell("DP1").Value = "CAPITAL_VIGENTE"
        ws.Cell("DQ1").Value = "CAPITAL_VENCIDO"
        ws.Cell("DR1").Value = "IMP_SDO_VECIDO"
        ws.Cell("DS1").Value = "FECHA_PROX_AMORTZCN"
        ws.Cell("DT1").Value = "IMP_PROX_AMORTZCN"
        ws.Cell("DU1").Value = "FECHA_ULT_FACT"
        ws.Cell("DV1").Value = "IMP_ULT_FACT"
        ws.Cell("DW1").Value = "CTA_OPE"
        ws.Cell("DX1").Value = "IMP_CTA_OPE"
        ws.Cell("DY1").Value = "FECHA_ULT_MOVTO_CTA_OPE"
        ws.Cell("DZ1").Value = "DEPSTOS_DINERO_RECIPROCIDAD"
        ws.Cell("EA1").Value = "TIPOCARTERA_CRDTO"
        ws.Cell("EB1").Value = "EMPRESA"
        ws.Cell("EC1").Value = "NOMB_EMPR_CTRL"
        ws.Cell("ED1").Value = "NOMB_CMRCL_CRDTO"
        ws.Cell("EE1").Value = "CLASIFICACION"
        ws.Cell("EF1").Value = "TIPO_CARTERA"
        ws.Cell("EG1").Value = "CLASIFICACION_SCCB"
        ws.Cell("EH1").Value = "COD_FORMA_PAGO"
        ws.Cell("EI1").Value = "STCN_CRDTO"
        ws.Cell("EJ1").Value = "NUM_EXP_JUDICIAL1"
        ws.Cell("EK1").Value = "NEGOCIADOR_ULT_ASIG_EXTRA"
        ws.Cell("EL1").Value = "NOM_SUPERVISOR"
        ws.Cell("EM1").Value = "NOM_DESPACHO"
        ws.Cell("EN1").Value = "NOMB_ABOGADO"
        ws.Cell("EO1").Value = "MERCADO"
        ws.Cell("EP1").Value = "RESP_GEST"
        For Each row As DataRow In dt.Rows
            ws.Cell(cuenta, 1).Value = row("CREDITO").ToString
            ws.Cell(cuenta, 2).Value = row("CLIE_T1").ToString
            ws.Cell(cuenta, 3).Value = row("NOMBRE_T").ToString
            ws.Cell(cuenta, 4).Value = "T"
            ws.Cell(cuenta, 5).Value = ""
            ws.Cell(cuenta, 6).Value = ""
            ws.Cell(cuenta, 7).Value = ""
            ws.Cell(cuenta, 8).Value = ""
            ws.Cell(cuenta, 9).Value = ""
            ws.Cell(cuenta, 10).Value = row("T1_T").ToString
            ws.Cell(cuenta, 11).Value = row("T2_T").ToString
            ws.Cell(cuenta, 12).Value = row("T3_T").ToString
            ws.Cell(cuenta, 13).Value = row("T4_T").ToString
            ws.Cell(cuenta, 14).Value = row("T5_T").ToString
            ws.Cell(cuenta, 15).Value = row("T6_T").ToString
            ws.Cell(cuenta, 16).Value = row("T7_T").ToString
            ws.Cell(cuenta, 17).Value = row("T8_T").ToString
            ws.Cell(cuenta, 18).Value = row("T9_T").ToString
            ws.Cell(cuenta, 19).Value = ""
            ws.Cell(cuenta, 20).Value = ""
            ws.Cell(cuenta, 21).Value = ""
            ws.Cell(cuenta, 22).Value = ""
            ws.Cell(cuenta, 23).Value = ""
            ws.Cell(cuenta, 24).Value = ""
            ws.Cell(cuenta, 25).Value = row("CLIE_C1").ToString
            ws.Cell(cuenta, 26).Value = row("NOMBRE_C").ToString
            ws.Cell(cuenta, 27).Value = "C"
            ws.Cell(cuenta, 28).Value = ""
            ws.Cell(cuenta, 29).Value = ""
            ws.Cell(cuenta, 30).Value = ""
            ws.Cell(cuenta, 31).Value = ""
            ws.Cell(cuenta, 32).Value = ""
            ws.Cell(cuenta, 33).Value = row("TC1_T").ToString
            ws.Cell(cuenta, 34).Value = row("TC2_T").ToString
            ws.Cell(cuenta, 35).Value = row("TC3_T").ToString
            ws.Cell(cuenta, 36).Value = row("TC4_T").ToString
            ws.Cell(cuenta, 37).Value = row("TC5_T").ToString
            ws.Cell(cuenta, 38).Value = row("TC6_T").ToString
            ws.Cell(cuenta, 39).Value = row("TC7_T").ToString
            ws.Cell(cuenta, 40).Value = row("TC8_T").ToString
            ws.Cell(cuenta, 41).Value = row("TC9_T").ToString
            ws.Cell(cuenta, 42).Value = row("CLIE_A1").ToString
            ws.Cell(cuenta, 43).Value = row("NOMBRE_A1").ToString
            ws.Cell(cuenta, 44).Value = "A1"
            ws.Cell(cuenta, 45).Value = ""
            ws.Cell(cuenta, 46).Value = ""
            ws.Cell(cuenta, 47).Value = ""
            ws.Cell(cuenta, 48).Value = ""
            ws.Cell(cuenta, 49).Value = ""
            ws.Cell(cuenta, 50).Value = row("T1A1_T").ToString
            ws.Cell(cuenta, 51).Value = row("T2A1_T").ToString
            ws.Cell(cuenta, 52).Value = row("T3A1_T").ToString
            ws.Cell(cuenta, 53).Value = row("T4A1_T").ToString
            ws.Cell(cuenta, 54).Value = row("T5A1_T").ToString
            ws.Cell(cuenta, 55).Value = row("T6A1_T").ToString
            ws.Cell(cuenta, 56).Value = row("T7A1_T").ToString
            ws.Cell(cuenta, 57).Value = row("T8A1_T").ToString
            ws.Cell(cuenta, 58).Value = row("T9A1_T").ToString
            ws.Cell(cuenta, 59).Value = row("CLIE_A2").ToString
            ws.Cell(cuenta, 60).Value = row("NOMBRE_A2").ToString
            ws.Cell(cuenta, 61).Value = "A2"
            ws.Cell(cuenta, 62).Value = ""
            ws.Cell(cuenta, 63).Value = ""
            ws.Cell(cuenta, 64).Value = ""
            ws.Cell(cuenta, 65).Value = ""
            ws.Cell(cuenta, 66).Value = ""
            ws.Cell(cuenta, 67).Value = row("T1A2_T").ToString
            ws.Cell(cuenta, 68).Value = row("T2A2_T").ToString
            ws.Cell(cuenta, 69).Value = row("T3A2_T").ToString
            ws.Cell(cuenta, 70).Value = row("T4A2_T").ToString
            ws.Cell(cuenta, 71).Value = row("T5A2_T").ToString
            ws.Cell(cuenta, 72).Value = row("T6A2_T").ToString
            ws.Cell(cuenta, 73).Value = row("T7A2_T").ToString
            ws.Cell(cuenta, 74).Value = row("T8A2_T").ToString
            ws.Cell(cuenta, 75).Value = row("T9A2_T").ToString
            ws.Cell(cuenta, 76).Value = row("CLIE_A3").ToString
            ws.Cell(cuenta, 77).Value = row("NOMBRE_A3").ToString
            ws.Cell(cuenta, 78).Value = "A3"
            ws.Cell(cuenta, 79).Value = ""
            ws.Cell(cuenta, 80).Value = ""
            ws.Cell(cuenta, 81).Value = ""
            ws.Cell(cuenta, 82).Value = ""
            ws.Cell(cuenta, 83).Value = ""
            ws.Cell(cuenta, 84).Value = row("T1A3_T").ToString
            ws.Cell(cuenta, 85).Value = row("T2A3_T").ToString
            ws.Cell(cuenta, 86).Value = row("T3A3_T").ToString
            ws.Cell(cuenta, 87).Value = row("T4A3_T").ToString
            ws.Cell(cuenta, 88).Value = row("T5A3_T").ToString
            ws.Cell(cuenta, 89).Value = row("T6A3_T").ToString
            ws.Cell(cuenta, 90).Value = row("T7A3_T").ToString
            ws.Cell(cuenta, 91).Value = row("T8A3_T").ToString
            ws.Cell(cuenta, 92).Value = row("T9A3_T").ToString
            ws.Cell(cuenta, 93).Value = row("CLIE_A4").ToString
            ws.Cell(cuenta, 94).Value = row("NOMBRE_A4").ToString
            ws.Cell(cuenta, 95).Value = "A4"
            ws.Cell(cuenta, 96).Value = ""
            ws.Cell(cuenta, 97).Value = ""
            ws.Cell(cuenta, 98).Value = ""
            ws.Cell(cuenta, 99).Value = ""
            ws.Cell(cuenta, 100).Value = ""
            ws.Cell(cuenta, 101).Value = ""
            ws.Cell(cuenta, 102).Value = ""
            ws.Cell(cuenta, 103).Value = row("T3A4_T").ToString
            ws.Cell(cuenta, 104).Value = row("T4A4_T").ToString
            ws.Cell(cuenta, 105).Value = row("T5A4_T").ToString
            ws.Cell(cuenta, 106).Value = row("T6A4_T").ToString
            ws.Cell(cuenta, 107).Value = row("T7A4_T").ToString
            ws.Cell(cuenta, 108).Value = row("T8A4_T").ToString
            ws.Cell(cuenta, 109).Value = row("T9A4_T").ToString
            ws.Cell(cuenta, 110).Value = ""
            ws.Cell(cuenta, 111).Value = ""
            ws.Cell(cuenta, 112).Value = ""
            ws.Cell(cuenta, 113).Value = ""
            ws.Cell(cuenta, 114).Value = ""
            ws.Cell(cuenta, 115).Value = ""
            ws.Cell(cuenta, 116).Value = ""
            ws.Cell(cuenta, 117).Value = ""
            ws.Cell(cuenta, 118).Value = ""
            ws.Cell(cuenta, 119).Value = ""
            ws.Cell(cuenta, 120).Value = ""
            ws.Cell(cuenta, 121).Value = ""
            ws.Cell(cuenta, 122).Value = ""
            ws.Cell(cuenta, 123).Value = ""
            ws.Cell(cuenta, 124).Value = ""
            ws.Cell(cuenta, 125).Value = ""
            ws.Cell(cuenta, 126).Value = ""
            ws.Cell(cuenta, 127).Value = ""
            ws.Cell(cuenta, 128).Value = ""
            ws.Cell(cuenta, 129).Value = ""
            ws.Cell(cuenta, 130).Value = ""
            ws.Cell(cuenta, 131).Value = ""
            ws.Cell(cuenta, 132).Value = ""
            ws.Cell(cuenta, 133).Value = ""
            ws.Cell(cuenta, 134).Value = ""
            ws.Cell(cuenta, 135).Value = ""
            ws.Cell(cuenta, 136).Value = ""
            ws.Cell(cuenta, 137).Value = ""
            ws.Cell(cuenta, 138).Value = ""
            ws.Cell(cuenta, 139).Value = ""
            ws.Cell(cuenta, 140).Value = ""
            ws.Cell(cuenta, 141).Value = ""
            ws.Cell(cuenta, 142).Value = ""
            ws.Cell(cuenta, 143).Value = ""
            ws.Cell(cuenta, 144).Value = ""
            ws.Cell(cuenta, 145).Value = ""
            ws.Cell(cuenta, 146).Value = ""
            cuenta = cuenta + 1
        Next
        For co As Integer = 1 To 12
            ws.Column(co).AdjustToContents()
        Next
        For co As Integer = 1 To 12
            If co <> 2 Then
                For ro As Integer = 1 To dt.Rows.Count + 1
                    ws.Cell(ro, co).SetDataType(XLCellValues.Text)
                Next
            End If
        Next
        ws.PageSetup.PageOrientation = XLPageOrientation.Landscape
        ws.PageSetup.AdjustTo(36)
        ws.PageSetup.PaperSize = XLPaperSize.LetterPaper
        ws.PageSetup.VerticalDpi = 600
        ws.PageSetup.HorizontalDpi = 600
        Dim V_Nombre As String = StrRuta() & "Salida\" & "Inconcert" & DateTime.Now.ToString("yyyyMMdd") & ".xlsx"
        xlsx.SaveAs(V_Nombre)
        Return V_Nombre
    End Function
    Sub Calixta(V_Campana As String, V_Bandera As String, V_Tipo As String)
        Dim SSCommandCat As New SqlCommand("SP_ENVIO_CAMPANA")
        SSCommandCat.CommandType = CommandType.StoredProcedure
        SSCommandCat.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = V_Bandera
        SSCommandCat.Parameters.Add("@V_Campana", SqlDbType.NVarChar).Value = V_Campana
        Dim DtsInfo As DataTable = Consulta_Procedure(SSCommandCat, "Catalogo")
        If DtsInfo.Rows.Count = 0 Then
            showModal(RadNotification1, "ok", "Correcto", "No Se Encontraron Resultados Para El Envio")
        Else
            If V_Tipo = "SMS" Then
                EnviarSMS(DtsInfo)
                showModal(RadNotification1, "ok", "Envio", "Fin Del Envio")
            ElseIf V_Tipo = "BLASTER" Then
                EnviarBlaster(DtsInfo)
                showModal(RadNotification1, "ok", "Envio", "Fin Del Envio")
            ElseIf V_Tipo = "CORREO" Then
                EnviarCorreo(DtsInfo)
                showModal(RadNotification1, "ok", "Envio", "Fin Del Envio")
            End If
            gridCampanas.Rebind()
        End If
        gridCampanas.Rebind()
    End Sub
    Sub EnviarSMS(ByVal V_Resultados As DataTable)
        For i As Integer = 0 To V_Resultados.Rows.Count - 1
            SMS(V_Resultados.Rows(i).Item("PR_MC_CREDITO").ToString, V_Resultados.Rows(i).Item("PR_MC_AGENCIA").ToString, V_Resultados.Rows(i).Item("PR_MC_PRODUCTO").ToString, CType(Session("USUARIOADMIN"), USUARIO).CAT_LO_USUARIO, V_Resultados.Rows(i).Item("MENSAJE").ToString, V_Resultados.Rows(i).Item("HIST_TE_NUMEROTEL").ToString, V_Resultados.Rows(i).Item("PR_MC_INSTANCIA").ToString, V_Resultados.Rows(i).Item("HIST_TE_TIPOREGISTRO").ToString)
        Next
    End Sub
    Sub EnviarBlaster(ByVal V_Resultados As DataTable)
        For i As Integer = 0 To V_Resultados.Rows.Count - 1
            TexVoz(V_Resultados.Rows(i).Item("PR_MC_CREDITO").ToString, V_Resultados.Rows(i).Item("PR_MC_AGENCIA").ToString, V_Resultados.Rows(i).Item("PR_MC_PRODUCTO").ToString, CType(Session("USUARIOADMIN"), USUARIO).CAT_LO_USUARIO, V_Resultados.Rows(i).Item("MENSAJE").ToString, V_Resultados.Rows(i).Item("HIST_TE_NUMEROTEL").ToString, V_Resultados.Rows(i).Item("PR_MC_INSTANCIA").ToString, V_Resultados.Rows(i).Item("HIST_TE_TIPOREGISTRO").ToString)
        Next
    End Sub
    Sub EnviarCorreo(ByVal V_Resultados As DataTable)
        For i As Integer = 0 To V_Resultados.Rows.Count - 1
            Correo(V_Resultados.Rows(i).Item("PR_MC_CREDITO").ToString, V_Resultados.Rows(i).Item("PR_MC_AGENCIA").ToString, V_Resultados.Rows(i).Item("PR_MC_PRODUCTO").ToString, CType(Session("USUARIOADMIN"), USUARIO).CAT_LO_USUARIO, V_Resultados.Rows(i).Item("HIST_CO_CORREO").ToString, V_Resultados.Rows(i).Item("MENSAJE").ToString, V_Resultados.Rows(i).Item("PR_MC_INSTANCIA").ToString, V_Resultados.Rows(i).Item("HIST_TE_TIPOREGISTRO").ToString)
        Next
    End Sub
    Sub SMS(ByVal V_Credito As String, ByVal V_Agencia As String, ByVal V_Producto As String, ByVal V_Usuario As String, ByVal V_Comentario As String, ByVal V_Telefono As String, ByVal V_Instancia As String, ByVal V_Participante As String)
        api.setPropiedades(Nothing)
        estado = api.enviaMensajeOL(V_Telefono, V_Comentario)
        Ejecuta("insert into hist_Gestiones (HIST_GE_CREDITO,HIST_GE_AGENCIA,HIST_GE_PRODUCTO,HIST_GE_USUARIO,HIST_GE_CODIGOS,HIST_GE_ACCION,HIST_GE_RESULTADO,HIST_GE_PONDERACION,HIST_GE_COMENTARIO, HIST_GE_TELEFONO,HIST_GE_DTEACTIVIDAD, HIST_GE_INOUTBOUND,HIST_GE_INSTANCIA, HIST_GE_CODACCION, HIST_GE_CODRESULT, HIST_GE_PARTICIPANTE) VALUES ('" & V_Credito & "','" & V_Agencia & "','" & V_Producto & "','" & V_Usuario & "','SMCM','SM','MENSAJE DE TEXTO COMPLETADO',0,'" & V_Comentario & "','" & V_Telefono & "',SYSDATE,0,'" & V_Instancia & "','SM','CM','" & V_Participante & "')")
    End Sub
    Sub TexVoz(ByVal V_Credito As String, ByVal V_Agencia As String, ByVal V_Producto As String, ByVal V_Usuario As String, ByVal V_Comentario As String, ByVal V_Telefono As String, ByVal V_Instancia As String, ByVal V_Participante As String)
        api.setPropiedades(Nothing)
        estado = api.enviaLlamadaOL(V_Telefono, "<check id=""fin"" valor=""0""/><texto voz=""Carlos"">" & V_Comentario & "</texto><check id=""fin"" valor=""1""/>")
        Ejecuta("insert into hist_Gestiones (HIST_GE_CREDITO,HIST_GE_AGENCIA,HIST_GE_PRODUCTO,HIST_GE_USUARIO,HIST_GE_CODIGOS,HIST_GE_ACCION,HIST_GE_RESULTADO,HIST_GE_PONDERACION,HIST_GE_COMENTARIO, HIST_GE_TELEFONO,HIST_GE_DTEACTIVIDAD, HIST_GE_INOUTBOUND,HIST_GE_INSTANCIA, HIST_GE_CODACCION, HIST_GE_CODRESULT, HIST_GE_PARTICIPANTE) VALUES ('" & V_Credito & "','" & V_Agencia & "','" & V_Producto & "','" & V_Usuario & "','BLEV','EV','BLASTER ENVIADO',0,'" & V_Comentario & "','" & V_Telefono & "',SYSDATE,0,'" & V_Instancia & "','BL','EV','" & V_Participante & "')")
    End Sub
    Sub Correo(ByVal V_Credito As String, ByVal V_Agencia As String, ByVal V_Producto As String, ByVal V_Usuario As String, ByVal V_Correo As String, ByVal V_Cuerpo_Correo As String, ByVal V_Instancia As String, ByVal V_Participante As String)
        estado = api.enviaEmailOL(V_Correo, "jose.rojo@mailix.mx", "Jose Rojo", "jose.rojo@mccollect.com.mx", "Mensaje de prueba", "Contenido del correo en texto plano", "<html><body>" & V_Cuerpo_Correo & "</body></html>")
        Ejecuta("insert into hist_Gestiones (HIST_GE_CREDITO,HIST_GE_AGENCIA,HIST_GE_PRODUCTO,HIST_GE_USUARIO,HIST_GE_CODIGOS,HIST_GE_ACCION,HIST_GE_RESULTADO,HIST_GE_PONDERACION,HIST_GE_COMENTARIO, HIST_GE_TELEFONO,HIST_GE_DTEACTIVIDAD, HIST_GE_INOUTBOUND,HIST_GE_INSTANCIA, HIST_GE_CODACCION, HIST_GE_CODRESULT, HIST_GE_PARTICIPANTE) VALUES ('" & V_Credito & "','" & V_Agencia & "','" & V_Producto & "','" & V_Usuario & "','EMCE','EM','BLASTER ENVIADO',0,'" & "CORREO ELECTRONICO (E MAIL) CORREO ENVIADO" & "','',SYSDATE,0,'" & V_Instancia & "','EM','CE','" & V_Participante & "')")
    End Sub
    Function ConceptoSaldo(ByVal V_IdSaldo As String) As String
        Dim V_Concepto As String = ""
        If V_IdSaldo = 0 Then
            V_Concepto = "Saldo"
        ElseIf V_IdSaldo = 1 Then
            V_Concepto = "SMS"
        ElseIf V_IdSaldo = 2 Then
            V_Concepto = "Plus"
        ElseIf V_IdSaldo = 3 Then
            V_Concepto = "A"
        ElseIf V_IdSaldo = 4 Then
            V_Concepto = "B"
        ElseIf V_IdSaldo = 5 Then
            V_Concepto = "C"
        ElseIf V_IdSaldo = 6 Then
            V_Concepto = "Celular"
        ElseIf V_IdSaldo = 7 Then
            V_Concepto = "Universal"
        ElseIf V_IdSaldo = 16 Then
            V_Concepto = "Cel.Plus"
        ElseIf V_IdSaldo = 17 Then
            V_Concepto = "Cel.A"
        ElseIf V_IdSaldo = 18 Then
            V_Concepto = "Cel.B"
        ElseIf V_IdSaldo = 19 Then
            V_Concepto = "Cel.C"
        ElseIf V_IdSaldo = 38 Then
            V_Concepto = "SMS Internacional"
        ElseIf V_IdSaldo = 41 Then
            V_Concepto = "Mail (Correo Transmitido)"
        ElseIf V_IdSaldo = 42 Then
            V_Concepto = "Mail(MB)"
        ElseIf V_IdSaldo = 43 Then
            V_Concepto = "Push Notification"
        ElseIf V_IdSaldo = 44 Then
            V_Concepto = "MMS"
        End If
        Return V_Concepto
    End Function

    Private Function getControlsHash(controles As ControlCollection) As Hashtable
        Dim valores As New Hashtable
        Dim value As String = ""
        For Each aux As Control In controles
            If TypeOf (aux) Is RadTextBox Then
                Dim c As RadTextBox = CType(aux, RadTextBox)
                valores.Add(c.ID, c.Text)
            ElseIf TypeOf (aux) Is RadComboBox Then
                Dim c As RadComboBox = CType(aux, RadComboBox)
                Try
                    If c.CheckBoxes Then
                        value = ""
                        For Each comboboxitem As RadComboBoxItem In c.CheckedItems
                            value &= comboboxitem.Value & ","
                        Next
                        value = value.Substring(0, value.Length() - 1)
                    Else
                        value = c.SelectedValue
                    End If
                Catch ex As Exception
                    value = "NA"
                End Try
                valores.Add(c.ID, value)
            ElseIf TypeOf (aux) Is RadDateTimePicker Then
                Dim c As RadDateTimePicker = CType(aux, RadDateTimePicker)
                Try
                    value = c.DbSelectedDate
                Catch ex As Exception
                    value = "NA"
                End Try
                valores.Add(c.ID, value)
            ElseIf TypeOf (aux) Is RadDatePicker Then
                Dim c As RadDatePicker = CType(aux, RadDatePicker)
                Try
                    value = c.DbSelectedDate
                Catch ex As Exception
                    value = "NA"
                End Try
                valores.Add(c.ID, value)
            ElseIf TypeOf (aux) Is RadAjaxPanel Then
                Dim c As Hashtable = getControlsHash(CType(aux, RadAjaxPanel).Controls)
                valores = mergeHashTables(valores, c)
            End If
        Next
        Return valores
    End Function

    Private Function mergeHashTables(ByVal h1 As Hashtable, ByVal h2 As Hashtable) As Hashtable
        For Each entry As DictionaryEntry In h2
            If Not h1.ContainsKey(entry.Key) Then
                h1.Add(entry.Key, entry.Value)
            End If
        Next
        Return h1
    End Function

    Private Sub gridCampanas_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles gridCampanas.NeedDataSource
        Dim SSCommandCat As New SqlCommand("SP_CAMPANA_MSJ")
        SSCommandCat.CommandType = CommandType.StoredProcedure
        SSCommandCat.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = 4
        SSCommandCat.Parameters.Add("@v_Usuario", SqlDbType.NVarChar).Value = tmpUSUARIO("CAT_LO_USUARIO")
        gridCampanas.DataSource = Consulta_Procedure(SSCommandCat, "Catalogo")
    End Sub

    Private Sub btnMasiva_Click(sender As Object, e As EventArgs) Handles btnMasiva.Click
        If uploadMasiva.UploadedFiles.Count > 0 Then
            If LblProveedor.Text = "CALIXTA" Then
                If LblTipo.Text = "SMS" Then
                    Calixta(lblCampana.Text, 1, LblTipo.Text)
                ElseIf LblTipo.Text = "BLASTER" Then
                    Calixta(lblCampana.Text, 2, LblTipo.Text)
                ElseIf LblTipo.Text = "CORREO" Then
                    Calixta(lblCampana.Text, 3, LblTipo.Text)
                End If
            ElseIf LblProveedor.Text = "INCONCERT" Then
                Dim SSCommandCat As New SqlCommand("SP_RP_INCONCERT")
                SSCommandCat.CommandType = CommandType.StoredProcedure
                SSCommandCat.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = 2
                SSCommandCat.Parameters.Add("@V_Campana", SqlDbType.NVarChar).Value = lblCampana.Text
                Dim DtsInconcert As DataTable = Consulta_Procedure(SSCommandCat, "Reporte")
                If DtsInconcert.Rows.Count > 0 Then
                    Dim V_Nombre As String = ExportToXcel_SomeReport(DtsInconcert, "inconcert", Me)
                    If File.Exists(V_Nombre) Then
                        Dim ioflujo As FileInfo = New FileInfo(V_Nombre)
                        Response.Clear()
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + ioflujo.Name)
                        Response.AddHeader("Content-Length", ioflujo.Length.ToString())
                        Response.ContentType = "application/octet-stream"
                        Response.WriteFile(V_Nombre)
                        Response.End()
                    End If
                End If
            ElseIf LblProveedor.Text = "LSC Communications" Then
                Dim SSCommandSim As New SqlCommand("SP_RP_LSC")
                SSCommandSim.CommandType = CommandType.StoredProcedure
                SSCommandSim.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = 0
                SSCommandSim.Parameters.Add("@V_Campana", SqlDbType.NVarChar).Value = lblCampana.Text
                Dim DtsLSCSim As DataTable = Consulta_Procedure(SSCommandSim, "Reporte")
                Dim V_Error As String

                Dim SSCommandCat As New SqlCommand("SP_RP_LSC")
                SSCommandCat.CommandType = CommandType.StoredProcedure
                SSCommandCat.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = 2
                SSCommandCat.Parameters.Add("@V_Campana", SqlDbType.NVarChar).Value = lblCampana.Text
                Dim DtsLSC As DataTable = Consulta_Procedure(SSCommandCat, "Reporte")


                If DtsLSCSim.Rows(0).Item("Fecha") <> "No" Then

                    For Each rowx As DataRow In DtsLSC.Rows
                        Dim objeto As Class_P_Saldos = Consultasaldoscredito(rowx("CREDITO"), rowx("cliente"), DtsLSCSim.Rows(0).Item("Fecha"))
                        If objeto.Estatus = "Error" Then
                            If objeto.Informacion(0).mensajeSafi.ToString() = "No es posible conectar con el servidor remoto" Then
                                V_Error = objeto.Informacion(0).mensajeSafi.ToString & "No es posible conectar con el servidor remoto"
                            Else
                                V_Error = objeto.Informacion(0).mensajeSafi.ToString
                            End If
                            Exit For
                        End If
                        Ejecuta("update tmp_LSC set Imp_ponerse_corriente=to_number('" & objeto.Informacion(0).saldoPCorriente & "','999999999.9999999'), Imp_ponerse_corriente_1=to_number('" & objeto.Informacion(0).saldoPCorriente & "','999999999.9999999'), Monto_con_Letra=FN_NUMERO_TO_LETRA('" & objeto.Informacion(0).saldoPCorriente & "')  where Num_Crdto='" & rowx("credito") & "'")
                    Next
                End If

                Dim SSCommandRes As New SqlCommand("SP_RP_LSC")
                SSCommandRes.CommandType = CommandType.StoredProcedure
                SSCommandRes.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = 3
                SSCommandRes.Parameters.Add("@V_Campana", SqlDbType.NVarChar).Value = ""
                Dim DtsLSCRes As DataTable = Consulta_Procedure(SSCommandRes, "Reporte")
                If DtsLSCRes.Rows.Count > 0 Then
                    Dim V_Nombre As String = ExportToXcel_SomeReportLSC(DtsLSCRes, "LSC", Me)
                    If File.Exists(V_Nombre) Then
                        Dim ioflujo As FileInfo = New FileInfo(V_Nombre)
                        Response.Clear()
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + ioflujo.Name)
                        Response.AddHeader("Content-Length", ioflujo.Length.ToString())
                        Response.ContentType = "application/octet-stream"
                        Response.WriteFile(V_Nombre)
                        Response.End()
                    End If
                End If
            End If
            showModal(RadNotification1, "ok", "Correcto", "Carga masiva para la campaña '" & lblCampana.Text & "' Ejecutada correctamente")
        Else
            showModal(RadNotification1, "deny", "Error", "Carga masiva para la campaña '" & lblCampana.Text & "' no pudo ser ejecutada, valide su archivo")
        End If
        pnlCargaMasiva.Visible = False
    End Sub

    Sub cargarTMP(ByVal FileCarga As String)
        Dim TABLA As String = "TMP_CAMPANAS"
        ctlCarga = Ruta & "CTL_CAMPANA_" & Now.ToShortDateString.Replace("/", "") & "" & ".ctl"
        logCarga = Ruta & "LOG_CAMPANA_" & Now.ToShortDateString.Replace("/", "") & "" & ".log"
        badCarga = Ruta & "BAD_CAMPANA_" & Now.ToShortDateString.Replace("/", "") & "" & ".bad"

        If File.Exists(ctlCarga) Then
            Kill(ctlCarga)
        End If
        If File.Exists(logCarga) Then
            Kill(logCarga)
        End If
        If File.Exists(badCarga) Then
            Kill(badCarga)
        End If

        Dim fs As Stream
        fs = New System.IO.FileStream(ctlCarga, IO.FileMode.OpenOrCreate)
        Dim sw As New System.IO.StreamWriter(fs)
        sw.AutoFlush = True
        sw.WriteLine("load data")
        sw.WriteLine("infile '" & FileCarga & "'")
        sw.WriteLine("into table " & TABLA & "")
        sw.WriteLine("truncate")
        sw.WriteLine("FIELDS TERMINATED BY ',' optionally enclosed by '""'")
        sw.WriteLine("TRAILING NULLCOLS")
        sw.WriteLine("(")
        sw.WriteLine("TMP_CREDITO")
        sw.WriteLine(")")
        sw.Close()
        fs.Close()

        Dim comando As Process = New Process
        comando.StartInfo.FileName = "sqlldr"
        'comando.StartInfo.Arguments = DesEncriptarCadena(Conexiones.StrConexion(1)) & "/" & DesEncriptarCadena(Conexiones.StrConexion(2)) & "@" & DesEncriptarCadena(Conexiones.StrConexion(3)) & " control=" & ctlCarga & ", bad=" & badCarga &
        comando.StartInfo.Arguments = (StrConexion(1)) & "/" & (StrConexion(2)) & "@" & (StrConexion(3)) & " control=" & ctlCarga & ", bad=" & badCarga &
        ",log=" & logCarga & " errors=" & ERRORS & ",DISCARDMAX=0,ROWS=" & "10000" & ",direct=y,skip=" & SKIP & ""
        comando.Start()
        comando.WaitForExit()

    End Sub
    Protected Sub RadGrid1_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs)
        If TypeOf e.Item Is GridDataItem Then
            Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(item("BtnEventos").Controls(0))
        End If
    End Sub

    Private Sub RadAsyncUpload1_FileUploaded(sender As Object, e As FileUploadedEventArgs) Handles uploadMasiva.FileUploaded
        Try
            SubExisteRuta(Ruta)
            For Each s In System.IO.Directory.GetFiles(Ruta)
                System.IO.File.Delete(s)
            Next
            Dim nom As String = e.File.FileName
            Dim ruta_archivo As String = Ruta + nom
            e.File.SaveAs(ruta_archivo)

            Try
                cargarTMP(Ruta + nom)
            Catch ex As Exception

            End Try
        Catch ex As Exception
        End Try
    End Sub
    Function Llenar(V_Cat_Sm_Id As Integer, V_Cat_Sm_Descripcion As String, V_Cat_Sm_Valor As String, V_Cat_Sm_Tabla As String, V_Cat_Sm_Camporeal As String, V_Bandera As Integer) As DataTable
        Dim SSCommandAgencias As New SqlCommand("Sp_Add_Cat_Etiquetas")
        SSCommandAgencias.CommandType = CommandType.StoredProcedure
        SSCommandAgencias.Parameters.Add("@V_Cat_Sm_Id", SqlDbType.Decimal).Value = V_Cat_Sm_Id
        SSCommandAgencias.Parameters.Add("@V_Cat_Sm_Descripcion", SqlDbType.NVarChar).Value = V_Cat_Sm_Descripcion
        SSCommandAgencias.Parameters.Add("@V_Cat_Sm_Valor", SqlDbType.Decimal).Value = V_Cat_Sm_Valor
        SSCommandAgencias.Parameters.Add("@V_Cat_Sm_Tabla", SqlDbType.NVarChar).Value = V_Cat_Sm_Tabla
        SSCommandAgencias.Parameters.Add("@V_Cat_Sm_Camporeal", SqlDbType.Decimal).Value = V_Cat_Sm_Camporeal
        SSCommandAgencias.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = V_Bandera
        Dim DtsVarios As DataTable = Consulta_Procedure(SSCommandAgencias, "Etiqueta")
        Return DtsVarios
    End Function

    'Private Sub _Campanas_Load(sender As Object, e As EventArgs) Handles Me.Load
    '    ScriptManager.GetCurrent(Page).RegisterPostBackControl(btnMasiva)
    'End Sub
    Public Shared Function Consultasaldoscredito(ByVal V_creditoID As String, ByVal V_cliente As String, ByVal V_fecha As String) As Class_P_Saldos

        Dim Class_P_Saldos As Class_P_Saldos
        Dim v_endpoint As String = "https://pruebasmc.com.mx/WsMovilBienestar/api/"
        Dim v_metodo As String = "SafiproyeccionSaldos"

        Dim data As String = "{" & vbCrLf &
                  "  ""creditoID"": """ & V_creditoID & """," & vbCrLf &
                  "  ""cliente"": """ & V_cliente & """," & vbCrLf &
                  "  ""fecha"": """ & V_fecha & """" & vbCrLf &
                              "}" & vbCrLf

        Dim v_credential As String = "dXN1YXJpb1BydWViYVdTOjEyMw=="
        Dim client = New RestClient(v_endpoint & v_metodo)
        Dim request = New RestRequest(Method.POST)
        request.AddHeader("cache-control", "no-cache")
        request.AddHeader("Content-Type", "application/json")
        request.AddParameter("undefined", data, ParameterType.RequestBody)

        Dim response As IRestResponse = client.Execute(request)
        Dim invDom As String = response.Content

        If response.StatusCode.ToString = "OK" Then
            Class_P_Saldos = JsonConvert.DeserializeObject(Of Class_P_Saldos)(response.Content)
        End If

        Return Class_P_Saldos

    End Function
    Private Sub taskea(ByVal nombre As String)
        Dim SSCommandCat As New SqlCommand("SP_CAMPANA_MSJ")
        SSCommandCat.CommandType = CommandType.StoredProcedure
        SSCommandCat.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = 14
        SSCommandCat.Parameters.Add("@v_nombre", SqlDbType.NVarChar).Value = nombre
        Dim DTHorario As DataTable = Consulta_Procedure(SSCommandCat, "Horario")
        Dim servicio As New TaskService() ' probar la sobrecarga por aquello de los permisos
        Dim definicion As TaskDefinition = servicio.NewTask()
        definicion.RegistrationInfo.Description = "Primera Prueba"
        Dim trigerdiario As New DailyTrigger
        trigerdiario.DaysInterval = 1
        trigerdiario.StartBoundary = Convert.ToDateTime(DTHorario.Rows(0).Item("INICIO"))
        trigerdiario.EndBoundary = Convert.ToDateTime(DTHorario.Rows(0).Item("FIN"))
        definicion.Triggers.Add(trigerdiario)
        definicion.Actions.Add(New ExecAction("C:\Program Files\internet explorer\iexplore.exe"))
        servicio.RootFolder.RegisterTaskDefinition("Prueba", definicion)
    End Sub

End Class
