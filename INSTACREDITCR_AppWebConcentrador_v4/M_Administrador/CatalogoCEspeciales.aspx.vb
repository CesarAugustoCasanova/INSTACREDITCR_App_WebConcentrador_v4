
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Diagnostics
Imports System.IO
Imports Db
Imports Telerik.Web.UI

Partial Class CatalogoCEspeciales
    Inherits System.Web.UI.Page
    Dim ERRORS As String = "1"
    Dim SKIP As String = "1"
    Dim ctlCarga As String
    Dim logCarga As String
    Dim badCarga As String

    Public Property tmpUSUARIO As IDictionary
        Get
            Return CType(Session("USUARIO"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("USUARIO") = value
        End Set
    End Property
    Sub llenar()
        LBCreditos.DataTextField = "Credito"
        LBCreditos.DataValueField = "Credito"
        LBCreditos.DataSource = consultar(4, "", "", "", "", 0, 0, 0, 0, 0)
        LBCreditos.DataBind()
        DDLCampo.DataTextField = "CAMPO"
        DDLCampo.DataValueField = "REAL"
        DDLCampo.DataSource = consultar(5, "", "", "", "", 0, 0, 0, 0, 0)
        DDLCampo.DataBind()
    End Sub

    Private Sub RGEspeciales_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RGEspeciales.ItemCommand
        Session("FechaG") = ""
        Session("InstanciaG") = ""
        If e.CommandName = "Update" Then
            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(10) As String


            valores(0) = CType(MyUserControl.FindControl("TxtNombre"), RadTextBox).Text
            valores(1) = CType(MyUserControl.FindControl("RDPVigencia"), RadDatePicker).DbSelectedDate
            valores(2) = tmpUSUARIO("CAT_LO_INSTANCIA")
            valores(3) = CType(MyUserControl.FindControl("TxtDescripcion"), RadTextBox).Text
            valores(4) = CType(MyUserControl.FindControl("NtxtBonificacion"), RadNumericTextBox).Text
            valores(5) = CType(MyUserControl.FindControl("NtxtCondonacionM"), RadNumericTextBox).Text
            valores(6) = CType(MyUserControl.FindControl("NtxtCondonacionN"), RadNumericTextBox).Text
            valores(7) = CType(MyUserControl.FindControl("NtxtCononacionC"), RadNumericTextBox).Text
            valores(8) = CType(MyUserControl.FindControl("NtxtExterno"), RadNumericTextBox).Text
            valores(9) = CType(MyUserControl.FindControl("RCBCampoSaldo"), RadComboBox).SelectedValue
            Dim falla As String = ""
            If valores(0) = "" Then
                falla += "Ingresa un NOMBRE <br/>"
            End If
            If valores(1) = "" Then

                falla += "Ingresa una Fecha <br/>"
            End If
            Dim fecha As Date = CType(CType(MyUserControl.FindControl("RDPVigencia"), RadDatePicker).SelectedDate, Date)
            If fecha.CompareTo(Now) < 0 Then
                falla += "Ingrese una fecha mayor a hoy <br/>"
            End If
            If valores(2) = "Seleccione" Or valores(2) = "" Then
                falla += "Ingresa una INSTANCIA <br/>"
            End If
            If valores(3) = "" Then
                falla += "Ingresa una DESCRIPCION <br/>"
            End If
            If valores(4) = "" Then
                falla += "Ingresa una BONIFICACION <br/>"
            End If
            If valores(5) = "" Then
                falla += "Ingresa una CONDONACION MORA <br/>"
            End If
            If valores(6) = "" Then
                falla += "Ingresa una CONDONACION NORMAL <br/>"
            End If
            If valores(7) = "" Then
                falla += "Ingresa una CONDONACION CAPITAL <br/>"
            End If
            If valores(8) = "" Then
                falla += "Ingresa HONORARIOS EXTERNOS <br/>"
            End If
            If valores(9) = "" Then
                falla += "Selecciona un Saldo <br/>"
            End If
            If falla <> "" Then
                RadAviso.RadAlert(falla, 375, 375, "Aviso", Nothing)
                e.Canceled = True
            Else
                valores(1) = CType(valores(1), DateTime).ToShortDateString
                consultar(1, valores(0), valores(1), valores(2), valores(3), valores(4), valores(5), valores(6), valores(7), valores(8), valores(9))
            End If
        ElseIf e.CommandName = "PerformInsert" Then
            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(10) As String


            valores(0) = CType(MyUserControl.FindControl("TxtNombre"), RadTextBox).Text
            valores(1) = CType(MyUserControl.FindControl("RDPVigencia"), RadDatePicker).SelectedDate.ToString
            valores(2) = tmpUSUARIO("CAT_LO_INSTANCIA")
            valores(3) = CType(MyUserControl.FindControl("TxtDescripcion"), RadTextBox).Text
            valores(4) = CType(MyUserControl.FindControl("NtxtBonificacion"), RadNumericTextBox).Text
            valores(5) = CType(MyUserControl.FindControl("NtxtCondonacionM"), RadNumericTextBox).Text
            valores(6) = CType(MyUserControl.FindControl("NtxtCondonacionN"), RadNumericTextBox).Text
            valores(7) = CType(MyUserControl.FindControl("NtxtCononacionC"), RadNumericTextBox).Text
            valores(8) = CType(MyUserControl.FindControl("NtxtExterno"), RadNumericTextBox).Text
            valores(9) = CType(MyUserControl.FindControl("RCBCampoSaldo"), RadComboBox).SelectedValue
            Dim falla As String = ""
            If valores(0) = "" Then
                falla += "Ingresa un NOMBRE <br/>"
            End If
            If valores(1) = "" Then
                falla += "Ingresa una Fecha <br/>"
            End If

            If valores(3) = "" Then
                falla += "Ingresa una DESCRIPCION <br/>"
            End If
            If valores(4) = "" Then
                falla += "Ingresa una BONIFICACION <br/>"
            End If
            If valores(5) = "" Then
                falla += "Ingresa una CONDONACION MORA <br/>"
            End If
            If valores(6) = "" Then
                falla += "Ingresa una CONDONACION NORMAL <br/>"
            End If
            If valores(7) = "" Then
                falla += "Ingresa una CONDONACION CAPITAL <br/>"
            End If
            If valores(8) = "" Then
                falla += "Ingresa HONORARIOS EXTERNOS <br/>"
            End If
            If valores(9) = "" Then
                falla += "Selecciona un Saldo <br/>"
            End If
            If falla <> "" Then
                RadAviso.RadAlert(falla, 375, 375, "Aviso", Nothing)
                e.Canceled = True
            Else
                valores(1) = CType(valores(1), DateTime).ToShortDateString
                consultar(2, valores(0), valores(1), valores(2), valores(3), valores(4), valores(5), valores(6), valores(7), valores(8), valores(9))
            End If
        ElseIf e.CommandName = "Edit" Then
            Session("FechaG") = e.Item.Cells.Item(5).Text
            Session("InstanciaG") = tmpUSUARIO("CAT_LO_INSTANCIA")
            Session("DO") = ""
        ElseIf e.CommandName = "Delete" Then
            consultar(3, e.Item.Cells.Item(4).Text, "", "", "", 0, 0, 0, 0, 0, 0)
        ElseIf e.CommandName = "Select" Then
            MostrarOcultarVentana(winManual, 1, 0)
            LblNombreCampaña.Text = e.Item.Cells.Item(4).Text
        ElseIf e.CommandName = "InitInsert" Then
            Session("DO") = ""
        End If
    End Sub
    Sub MostrarOcultarVentana(ByVal V_Ventana As Object, ByVal V_Bandera As String, ByVal V_Maximizada As String)
        If V_Bandera = 1 Then
            Dim script As String
            If V_Maximizada = 1 Then
                script = "function f(){$find(""" + V_Ventana.ClientID + """).show();$find(""" + V_Ventana.ClientID + """).maximize(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
            Else
                script = "function f(){$find(""" + V_Ventana.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
            End If
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
        Else
            Dim script As String = "function f(){$find(""" + V_Ventana.ClientID + """).hide(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
        End If
    End Sub
    Private Function consultar(ByVal V_BANDERA As Integer, ByVal V_NOMBRE As String, ByVal V_DTEVIGENCIA As String, ByVal V_INSTANCIA As String, ByVal V_DESCRIPCION As String, ByVal V_PRCBONIFICACION As Integer,
                               ByVal V_PRCCONDONACIONM As Integer, ByVal V_PRCCONDONACIONN As Integer, ByVal V_PRCCONDONACIONC As Integer, ByVal V_PRCEXTERNO As Integer, Optional ByVal v_saldo As String = "") As DataTable
        Dim SSCommand As New SqlCommand("SP_CAMPANAS_ESP")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = V_BANDERA
        SSCommand.Parameters.Add("@v_NOMBRE", SqlDbType.NVarChar).Value = V_NOMBRE
        SSCommand.Parameters.Add("@v_DTEVIGENCIA", SqlDbType.NVarChar).Value = V_DTEVIGENCIA
        SSCommand.Parameters.Add("@v_INSTANCIA", SqlDbType.NVarChar).Value = V_INSTANCIA
        SSCommand.Parameters.Add("@v_DESCRIPCION", SqlDbType.NVarChar).Value = V_DESCRIPCION
        SSCommand.Parameters.Add("@v_PRCBONIFICACION", SqlDbType.Decimal).Value = V_PRCBONIFICACION
        SSCommand.Parameters.Add("@v_PRCCONDONACIONM", SqlDbType.Decimal).Value = V_PRCCONDONACIONM
        SSCommand.Parameters.Add("@v_PRCCONDONACIONN", SqlDbType.Decimal).Value = V_PRCCONDONACIONN
        SSCommand.Parameters.Add("@v_PRCCONDONACIONC", SqlDbType.NVarChar).Value = V_PRCCONDONACIONC
        SSCommand.Parameters.Add("@v_PRCEXTERNO", SqlDbType.Decimal).Value = V_PRCEXTERNO
        SSCommand.Parameters.Add("@v_saldo", SqlDbType.NVarChar).Value = v_saldo
        Dim DtsVarios As DataTable = Consulta_Procedure(SSCommand, "Especiales")
        Return DtsVarios
    End Function

    Private Sub CatalogoCEspeciales_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            llenar()
        End If
    End Sub

    Private Sub DDLCampo_SelectedIndexChanged(sender As Object, e As DropDownListEventArgs) Handles DDLCampo.SelectedIndexChanged
        DDLOperador.Items.Clear()
        If DDLCampo.SelectedIndex = 0 Then

            TxtValores.Visible = False
            CBVAlores.Visible = False
            DPValores.Visible = False
            TxtValores2.Visible = False
            DPValores2.Visible = False
            DDLConector.Visible = False
            DDLOperador.Visible = False
            LblOperador.Visible = False
            LblValores.Visible = False
            LblConector.Visible = False
        Else
            Dim separadores As String() = Split(DDLCampo.SelectedValue, "$")
            If separadores(1) = 1 Then
                DDLOperador.Items.Add(New DropDownListItem("Seleccione", "0"))
                DDLOperador.Items.Add(New DropDownListItem("Que Contenga", "In"))
                DDLOperador.Items.Add(New DropDownListItem("Que No Contenga", "Not In"))

            Else
                DDLOperador.Items.Add(New DropDownListItem("Seleccione", "0"))
                DDLOperador.Items.Add(New DropDownListItem("Mayor Que", ">"))
                DDLOperador.Items.Add(New DropDownListItem("Menor Que", "<"))
                DDLOperador.Items.Add(New DropDownListItem(" Igual", "="))
                DDLOperador.Items.Add(New DropDownListItem(" Mayor O Igual", ">="))
                DDLOperador.Items.Add(New DropDownListItem("Menor O Igual", "<="))
                DDLOperador.Items.Add(New DropDownListItem(" Distinto", "<>"))
                DDLOperador.Items.Add(New DropDownListItem("Que Contenga", "In"))
                DDLOperador.Items.Add(New DropDownListItem("Que No Contenga", "Not In"))
                DDLOperador.Items.Add(New DropDownListItem("Entre", "Between"))

            End If
            LblOperador.Visible = True
            DDLOperador.Visible = True
        End If
    End Sub

    Private Sub DDLOperador_SelectedIndexChanged(sender As Object, e As DropDownListEventArgs) Handles DDLOperador.SelectedIndexChanged
        TxtValores.Visible = False
        CBVAlores.Visible = False
        DPValores.Visible = False
        TxtValores2.Visible = False
        DPValores2.Visible = False
        LblValores.Visible = False
        LblConector.Visible = False

        Dim separadores As String() = Split(DDLCampo.SelectedValue, "$")
        If DDLOperador.SelectedText = "Que Contenga" Or DDLOperador.SelectedText = "Que No Contenga" Then
            CBVAlores.Visible = True
            CBVAlores.DataTextField = "Texto"
            CBVAlores.DataValueField = "Texto"
            CBVAlores.DataSource = consultar(6, separadores(0), "", "", "", 0, 0, 0, 0, 0)
            CBVAlores.DataBind()
        ElseIf separadores(2) = "DATE" Then
            DPValores.Visible = True
            If DDLOperador.SelectedText = "Entre" Then
                DPValores2.Visible = True
            End If
        Else
            TxtValores.Visible = True
            If DDLOperador.SelectedText = "Entre" Then
                TxtValores2.Visible = True
            End If
        End If
        DDLConector.Visible = True
        LblValores.Visible = True
        LblConector.Visible = True
    End Sub

    Private Sub DDLConector_SelectedIndexChanged(sender As Object, e As DropDownListEventArgs) Handles DDLConector.SelectedIndexChanged
        Dim separadores As String() = Split(DDLCampo.SelectedValue, "$")
        Dim fmostrar As String = DDLCampo.SelectedText & " " & DDLOperador.SelectedText
        Dim fnuestro As String = separadores(0) & " " & DDLOperador.SelectedValue
        If ValidaFiltro() = "" Then
            If TxtValores2.Visible = True Then
                If separadores(2) = "VARCHAR2" Then
                    fmostrar += "'" & TxtValores.Text & "' y " & "'" & TxtValores2.Text & "'"
                    fnuestro += "'" & TxtValores.Text & "' AND " & "'" & TxtValores2.Text & "'"
                Else
                    fmostrar += TxtValores.Text & " y " & TxtValores2.Text
                    fnuestro += TxtValores.Text & " AND " & TxtValores2.Text
                End If
            ElseIf DPValores2.Visible = True Then
                fmostrar += CType(DPValores.AccessKey, DateTime).ToShortDateString & " y " & CType(DPValores2.AccessKey, DateTime).ToShortDateString
                fnuestro += "FN_DATE(" & CType(DPValores.AccessKey, DateTime).ToShortDateString & ") and FN_DATE(" & CType(DPValores2.AccessKey, DateTime).ToShortDateString & ")"
            ElseIf CBVAlores.Visible = True Then
                fmostrar += "(" & valoresCB(CBVAlores) & ")"
                fnuestro += "(" & valoresCB(CBVAlores) & ")"
            ElseIf TxtValores.Visible = True Then
                fmostrar += " " & TxtValores.Text
                fnuestro += " " & TxtValores.Text
            ElseIf DPValores.Visible = True Then
                fmostrar += CType(DPValores.AccessKey, DateTime).ToShortDateString
                fnuestro += CType(DPValores.AccessKey, DateTime).ToShortDateString
            End If
            fmostrar += " " & DDLConector.SelectedText
            fnuestro += " " & DDLConector.SelectedValue

            LBFiltro.Items.Add(New RadListBoxItem(fmostrar, fnuestro))

            TxtValores.Visible = False
            CBVAlores.Visible = False
            DPValores.Visible = False
            TxtValores2.Visible = False
            DPValores2.Visible = False
            DDLConector.Visible = False
            DDLOperador.Visible = False
            DDLCampo.SelectedIndex = 0
            DDLConector.SelectedIndex = 0
            TxtValores.Text = ""
            TxtValores2.Text = ""
            DPValores.SelectedDate = Nothing
            DPValores2.SelectedDate = Nothing
            LblOperador.Visible = False
            LblValores.Visible = False
            LblConector.Visible = False
        Else
            RadAviso.RadAlert(ValidaFiltro, 375, 245, "Aviso", Nothing)
            DDLConector.SelectedIndex = 0
        End If
    End Sub
    Private Function ValidaFiltro() As String
        Dim err As String = ""
        If DDLCampo.SelectedIndex = 0 Then
            err += "Seleccione un Campo <br/>"
        End If
        If DDLOperador.SelectedIndex = 0 Then
            err += "Seleccione un Operador <br/>"
        End If
        If TxtValores.Text = "" And DPValores.SelectedDate Is Nothing And CBVAlores.CheckedItems.Count = 0 Then
            err += "Selecciona un Valor <br/>"
        End If
        If TxtValores2.Text = "" And TxtValores.Text <> "" And TxtValores2.Visible = True Then
            err += "Seleccione un Rango valido <br/>"
        End If
        If DPValores2.SelectedDate Is Nothing And DPValores.SelectedDate IsNot Nothing And DPValores2.Visible = True Then
            err += "Seleccione un Rango de fecha Valido <br/>"
        End If
        Return err
    End Function
    Private Function valoresCB(Cb As RadComboBox) As String
        Dim valores As String = ""
        For i As Integer = 0 To Cb.Items.Count - 1
            If Cb.Items(i).Checked Then
                valores += "'" & (Cb.Items(i).Text) & "',"
            End If
        Next
        valores = valores.Substring(0, valores.Length - 1)
        Return valores
    End Function

    Private Sub LBFiltro_Deleted(sender As Object, e As RadListBoxEventArgs) Handles LBFiltro.Deleted
        For Each item As RadListBoxItem In LBFiltro.Items
            If item.Checked = True Then
                LBFiltro.Items.Remove(item)
            End If
        Next

    End Sub

    Private Sub BtnAplicar_Click(sender As Object, e As EventArgs) Handles BtnAplicar.Click
        Dim filtro As String = ""
        For Each item As RadListBoxItem In LBFiltro.Items

            filtro += item.Value & " "

        Next
        filtro = filtro.Trim()
        If filtro.Substring(filtro.Length - 2) = "or" Then
            filtro = filtro.Substring(0, filtro.Length - 2)
        ElseIf filtro.Substring(filtro.Length - 3) = "and" Then
            filtro = filtro.Substring(0, filtro.Length - 3)
        End If
        If filtro = "" Then
            Aviso("Ningún filtro que aplicar")
        Else
            LBCreditos.Items.Clear()
            LBCreditos.DataTextField = "Credito"
            LBCreditos.DataValueField = "Credito"
            Dim dset = consultar(7, filtro, "", "", "", 0, 0, 0, 0, 0)
            If dset.Rows.Count = 0 Then
                Aviso("La combinacion de filtros no produjo ningun resultado")
            Else
                LBCreditos.DataSource = dset
                LBCreditos.DataBind()
            End If
        End If
    End Sub
    Private Sub Aviso(ByVal MSJ As String)
        RadAviso.RadAlert(MSJ, 250, 175, "Aviso", Nothing)
    End Sub

    Private Sub BtnCargaManual_Click(sender As Object, e As EventArgs) Handles BtnCargaManual.Click
        Dim creditos As String = ""
        For Each item As RadListBoxItem In LBCreditos.Items

            If item.Checked = True Then
                creditos += item.Value & ","
            End If

        Next
        If creditos = "" Then
            Aviso("Ningun credito seleccionado")
        Else
            creditos.Substring(0, creditos.Length - 1)
            Try
                consultar(8, creditos, "", LblNombreCampaña.Text, "", 0, 0, 0, 0, 0)
                Aviso(LBCreditos.CheckedItems.Count & " Créditos Añadidos a la campaña " & LblNombreCampaña.Text)
            Catch ex As Exception
                Aviso(ex.Message)
            End Try
        End If

    End Sub
    Private Sub Import_To_oRACLE(ByVal FilePath As String, ByVal Extension As String, ByVal isHDR As String)

        Dim conStr As String = ""
        Select Case Extension
            Case ".xls"
                'Excel 97-03 
                'conStr = ConfigurationManager.ConnectionStrings("Excel03ConString").ConnectionString
                conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1};IMEX=1'"
                Exit Select
            Case ".xlsx"
                'Excel 07 
                'conStr = ConfigurationManager.ConnectionStrings("Excel07ConString").ConnectionString
                conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1};IMEX=1'"
                Exit Select
        End Select
        conStr = String.Format(conStr, FilePath, isHDR)

        Dim connExcel As New OleDbConnection(conStr)
        Dim cmdExcel As New OleDbCommand()
        Dim oda As New OleDbDataAdapter()
        Dim dt As New DataTable()

        cmdExcel.Connection = connExcel

        connExcel.Open()
        Dim dtExcelSchema As DataTable
        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
        Dim SheetName As String = dtExcelSchema.Rows(0)("TABLE_NAME").ToString()
        connExcel.Close()

        connExcel.Open()
        cmdExcel.CommandText = "SELECT * From [" & SheetName & "]"
        oda.SelectCommand = cmdExcel
        oda.Fill(dt)
        connExcel.Close()

        Dim msCadenaORCL As String = Conectando()
        Using loConexionSSERV As New SqlConnection(msCadenaORCL)
            gP_DataTableToTable(dt, "TMP_CAMPANAS_ESPECIALES", loConexionSSERV)
        End Using


    End Sub
    Private Sub gP_DataTableToTable(ByRef rdtDataTable As DataTable, ByVal vsTabla As String, ByVal voConexion As SqlConnection)
        For Each row As DataRow In rdtDataTable.Rows
            'Las filas recién agregadas se consideran filas insertadas
            row.SetAdded()
        Next
        Try
            Dim loDataAdapter As New SqlDataAdapter("Select * From " & vsTabla, voConexion)
            ' Dim cmdBuilder As New SqlCommandBuilder(CType(loDataAdapter, OracleDataAdapter))
            loDataAdapter.Update(rdtDataTable)
        Catch ex As Exception
            Aviso(ex.Message)
            Dim s As String = "asdsadsaf"
        End Try
    End Sub

    Private Sub BtnExcel_Click(sender As Object, e As EventArgs) Handles BtnExcel.Click
        MostrarOcultarVentana(WinExcel, 1, 0)
    End Sub

    Private Sub AUCarga_FileUploaded(sender As Object, e As FileUploadedEventArgs) Handles AUCarga.FileUploaded
        Dim lugar As String = StrRuta() & "Especiales\"
        cargaex(1)
        If AUCarga.UploadedFiles.Count <> 0 Then
            Try
                AUCarga.UploadedFiles(0).SaveAs(lugar & AUCarga.UploadedFiles(0).FileName, True)
                'Import_To_oRACLE2(lugar, AUCarga.UploadedFiles(0).GetExtension, "Si")
                Import_To_oRACLE2(lugar, AUCarga.UploadedFiles(0).FileName, 0)
            Catch ex As Exception
                Aviso("ERROR " & ex.Message)
            End Try
            Dim DtsVarios As DataTable = cargaex(0)
            GVResultado.DataSource = DtsVarios
            GVResultado.DataBind()
        Else
            Aviso("Archivo no cargado o no valido")
        End If
    End Sub
    Sub Import_To_oRACLE2(Ruta, archivo, bandera)
        Dim Directory As New IO.DirectoryInfo(Ruta)
        Dim allFiles As IO.FileInfo() = Directory.GetFiles.Where(Function(fi) fi.Extension = ".csv" OrElse fi.Extension = ".txt").ToArray
        Dim singleFile As IO.FileInfo
        Dim value As Long
        Dim FileCarga As String = ""

        For Each singleFile In allFiles
            value = singleFile.Length
            FileCarga = singleFile.Name
            If value = 0 Then
                My.Computer.FileSystem.DeleteFile(Ruta & FileCarga, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently, FileIO.UICancelOption.DoNothing)
            Else
                Dim TABLA As String = "TMP_CAMPANAS_ESPECIALES"
                ctlCarga = Ruta & "CTL_CAMPANAS_ES_" & TABLA & "_" & Now.ToShortDateString.Replace("/", "") & "" & ".ctl"
                logCarga = Ruta & "LOG_CAMPANAS_ES_" & TABLA & "_" & Now.ToShortDateString.Replace("/", "") & "" & ".log"
                badCarga = Ruta & "BAD_CAMPANAS_ES_" & TABLA & "_" & Now.ToShortDateString.Replace("/", "") & "" & ".bad"

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
                sw.WriteLine("infile '" & Ruta & "" & FileCarga & "'")
                sw.WriteLine("into table " & TABLA & "")
                sw.WriteLine("FIELDS TERMINATED BY ',' optionally enclosed by '""'")
                sw.WriteLine("TRAILING NULLCOLS")
                sw.WriteLine("(")
                sw.WriteLine("CREDITO,")
                sw.WriteLine("CAMPANA")
                sw.WriteLine(")")
                sw.Close()
                fs.Close()

                Dim comando As Process = New Process
                comando.StartInfo.FileName = "sqlldr"
                comando.StartInfo.Arguments = (StrConexion(1)) & "/" & (StrConexion(2)) & "@" & (StrConexion(3)) & " control= " & ctlCarga & ", bad=" & badCarga &
                ",log=" & logCarga & " errors=" & ERRORS & ",DISCARDMAX=0,ROWS=" & "10000" & ",direct=y,skip=" & SKIP & ""
                comando.Start()
                comando.WaitForExit()
                Dim num As Integer = comando.ExitCode
                If (num <> 0) Then
                    If (File.Exists(badCarga)) Then
                        Dim fic As New IO.StreamReader(badCarga, System.Text.Encoding.UTF8)
                        Dim linea As String = fic.ReadLine
                        fic.Close()
                        Aviso("Error en la cadena siguiente " & linea)
                    End If
                Else
                    Dim SSCommand As New SqlCommand("SP_CARGA_CAMPANAS_ESP")
                    SSCommand.CommandType = CommandType.StoredProcedure
                    SSCommand.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = bandera
                    SSCommand.Parameters.Add("@V_Instancia", SqlDbType.Decimal).Value = tmpUSUARIO("CAT_LO_INSTANCIA")
                    Dim DtsCarga As DataTable = Consulta_Procedure(SSCommand, "SP_CARGA_CAMPANAS_ESP")
                    GVResultado.DataSource = DtsCarga
                    GVResultado.DataBind()

                    SubExisteRuta(Ruta & "Historico")
                    FileCopy(Ruta & FileCarga, Ruta & "Historico/" & singleFile.Name.ToString.Substring(0, Len(singleFile.Name.ToString) - 4) & "_" & DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") & singleFile.Extension)
                    Kill(Ruta & FileCarga)

                    If (File.Exists(logCarga)) Then
                        FileCopy(logCarga, Ruta & "Historico/" & singleFile.Name.ToString.Substring(0, Len(singleFile.Name.ToString) - 4) & "_" & DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") & ".log")
                    End If

                    If (File.Exists(badCarga)) Then
                        FileCopy(badCarga, Ruta & "Historico/" & singleFile.Name.ToString.Substring(0, Len(singleFile.Name.ToString) - 4) & "_" & DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") & ".bad")
                    End If
                End If

            End If
        Next
    End Sub


    Private Sub BtnCarga_Click(sender As Object, e As EventArgs) Handles BtnCarga.Click
        If AUCarga.UploadedFiles.Count = 0 Then
            Aviso("Archivos inválidos")
        End If
    End Sub
    Function cargaex(ByVal v_bandera As Integer) As DataTable
        Dim SSCommand As New SqlCommand("SP_CARGA_CAMPANAS_ESP")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = v_bandera
        SSCommand.Parameters.Add("@V_Instancia", SqlDbType.Decimal).Value = tmpUSUARIO("CAT_LO_INSTANCIA")
        Dim DtsVarios As DataTable = Consulta_Procedure(SSCommand, "Especiales")
        Return DtsVarios
    End Function

    Private Sub RGEspeciales_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RGEspeciales.NeedDataSource
        RGEspeciales.DataSource = consultar(0, "", "", tmpUSUARIO("CAT_LO_INSTANCIA"), "", 0, 0, 0, 0, 0)
    End Sub
End Class
