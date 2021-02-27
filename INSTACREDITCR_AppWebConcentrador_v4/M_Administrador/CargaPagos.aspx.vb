Imports System.Data.SqlClient
Imports System.Data
Imports System.Diagnostics
Imports System.IO
Imports Db
Imports Funciones
Imports Telerik.Web.UI

Partial Class MAdministrador_CargaPagos
    Inherits System.Web.UI.Page

    Dim Ruta As String = Db.StrRuta() & "PAGOS\"
    Shared nombre As String
    'Dim TABLA As String = "TMP_HIST_PAGOS"
    'Dim ERRORS As String = "100"
    'Dim SKIP As String = "1"
    'Dim ctlCarga As String
    'Dim logCarga As String
    'Dim badCarga As String
    'Dim Ruta As String = StrRuta() & "Pagos\"

    Protected Sub BtnCargar_Click(sender As Object, e As EventArgs) Handles BtnCargar.Click
        'Try

        Db.SubExisteRuta(Ruta)
            Dim Directory As New IO.DirectoryInfo(Ruta)
            Dim allFiles As IO.FileInfo() = Directory.GetFiles.Where(Function(fi) fi.Extension = ".csv" OrElse fi.Extension = ".txt").ToArray
        If allFiles.Length = 0 Then
            aviso("Archivo No Seleccionado o No Valido ")
            LblMensaje.Text = ""
        ElseIf DdlDelimitador.SelectedText = "Seleccione" Then
            LblMensaje.Text = ""
            aviso("Seleccione un separador")
        Else

                LblMensaje.Text = ""

                If DdlDelimitador.SelectedText = "Seleccione" Then
                    LblMensaje.Text = "Seleccione un separador"
                ElseIf allFiles.Length = 0 Then
                    LblMensaje.Text = "Archivo No Seleccionado o No Valido "
                Else
                    Dim tabla As DataTable
                Try
                    tabla = FileToDataTable(Ruta & nombre)
                    Dim SSCommand2 As New SqlCommand("SP_CARGA_PAGOS_MASIVA")
                    SSCommand2.CommandType = CommandType.StoredProcedure
                    SSCommand2.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = 2
                    Consulta_Procedure(SSCommand2, SSCommand2.CommandText) 'truncar tabla temporal

                    Dim fakebulk As New SqlBulkCopy(Conectando())
                    'If DDLTipo.SelectedValue = 1 Then
                    fakebulk.DestinationTableName = "dbo.TMP_HIST_PAGOS"
                    fakebulk.ColumnMappings.Add("TMP_PAG_ID_PAGO", "TMP_PAG_ID_PAGO")
                    fakebulk.ColumnMappings.Add("TMP_PAG_FECHA", "TMP_PAG_FECHA")
                    fakebulk.ColumnMappings.Add("TMP_PAG_CUENTA", "TMP_PAG_CUENTA")
                    fakebulk.ColumnMappings.Add("TMP_PAG_CONTO_1", "TMP_PAG_CONTO_1")
                    fakebulk.ColumnMappings.Add("TMP_PAG_CONTO_2", "TMP_PAG_CONTO_2")
                    fakebulk.ColumnMappings.Add("TMP_PAG_CONTO_3", "TMP_PAG_CONTO_3")
                    fakebulk.ColumnMappings.Add("TMP_PAG_REFERENCIA_PANET", "TMP_PAG_REFERENCIA_PANET")
                    fakebulk.ColumnMappings.Add("TMP_PAG_DV", "TMP_PAG_DV")
                    fakebulk.ColumnMappings.Add("TMP_PAG_IMPORTE", "TMP_PAG_IMPORTE")
                    fakebulk.ColumnMappings.Add("TMP_PAG_RFC", "TMP_PAG_RFC")
                    fakebulk.ColumnMappings.Add("TMP_PAG_DAP", "TMP_PAG_DAP")
                    fakebulk.ColumnMappings.Add("TMP_PAG_FOLIO", "TMP_PAG_FOLIO")
                    fakebulk.ColumnMappings.Add("TMP_PAG_CREDITO", "TMP_PAG_CREDITO")
                    fakebulk.ColumnMappings.Add("TMP_PAG_NOMBRE", "TMP_PAG_NOMBRE")
                    fakebulk.ColumnMappings.Add("TMP_PAG_PAGO_CERO_360", "TMP_PAG_PAGO_CERO_360")
                    fakebulk.ColumnMappings.Add("TMP_PAG_PAGO_CERO_INDICADORES", "TMP_PAG_PAGO_CERO_INDICADORES")
                    fakebulk.ColumnMappings.Add("TMP_PAG_PRODUCTO", "TMP_PAG_PRODUCTO")
                    fakebulk.ColumnMappings.Add("TMP_PAG_DEPENDENCIA", "TMP_PAG_DEPENDENCIA")
                    fakebulk.ColumnMappings.Add("TMP_PAG_LINEA_CREDITO", "TMP_PAG_LINEA_CREDITO")
                    fakebulk.ColumnMappings.Add("TMP_PAG_ESTATUS_CREDITO", "TMP_PAG_ESTATUS_CREDITO")
                    fakebulk.ColumnMappings.Add("TMP_PAG_TIPO_PAGO", "TMP_PAG_TIPO_PAGO")
                    fakebulk.ColumnMappings.Add("TMP_PAG_TIPO_PAGO_EXTJ", "TMP_PAG_TIPO_PAGO_EXTJ")
                    fakebulk.ColumnMappings.Add("TMP_PAG_OBSERVACIONES", "TMP_PAG_OBSERVACIONES")
                    fakebulk.ColumnMappings.Add("TMP_PAG_SUB_ESTATUS", "TMP_PAG_SUB_ESTATUS")
                    fakebulk.ColumnMappings.Add("TMP_PAG_ASIGNACION", "TMP_PAG_ASIGNACION")
                    fakebulk.ColumnMappings.Add("TMP_PAG_DETALLE", "TMP_PAG_DETALLE")
                    fakebulk.ColumnMappings.Add("TMP_PAG_CAMPANA", "TMP_PAG_CAMPANA")
                    fakebulk.ColumnMappings.Add("TMP_PAG_CAMPANA_AVANCES", "TMP_PAG_CAMPANA_AVANCES")
                    fakebulk.ColumnMappings.Add("TMP_PAG_EJECUTIVO", "TMP_PAG_EJECUTIVO")
                    fakebulk.ColumnMappings.Add("TMP_PAG_TURNO", "TMP_PAG_TURNO")
                    fakebulk.ColumnMappings.Add("TMP_PAG_JEFE_INMEDIATO", "TMP_PAG_JEFE_INMEDIATO")
                    fakebulk.ColumnMappings.Add("TMP_PAG_TIPO_GESTION", "TMP_PAG_TIPO_GESTION")
                    fakebulk.ColumnMappings.Add("TMP_PAG_REASIGNAR_PAGO", "TMP_PAG_REASIGNAR_PAGO")
                    fakebulk.ColumnMappings.Add("TMP_PAG_OBSERVACION", "TMP_PAG_OBSERVACION")
                    fakebulk.ColumnMappings.Add("TMP_PAG_BANCO", "TMP_PAG_BANCO")
                    fakebulk.ColumnMappings.Add("TMP_PAG_HOMOLOGADA", "TMP_PAG_HOMOLOGADA")
                    fakebulk.ColumnMappings.Add("TMP_PAG_ESTADO_DEPENDENCIA", "TMP_PAG_ESTADO_DEPENDENCIA")
                    fakebulk.ColumnMappings.Add("TMP_PAG_SUCURSAL", "TMP_PAG_SUCURSAL")
                    fakebulk.ColumnMappings.Add("TMP_PAG_BASE_EXTJ", "TMP_PAG_BASE_EXTJ")

                    'ElseIf DDLTipo.SelectedValue = 2 Then
                    ' fakebulk.DestinationTableName = "dbo.HIST_VISITAS_MASIVAS"

                    ' Else
                    'LBLResultado.Text = "Seleccione un tipo"
                    'End If

                    fakebulk.WriteToServer(tabla)

                    Dim SSCommand3 As New SqlCommand("SP_CARGA_PAGOS_MASIVA")
                    SSCommand3.CommandType = CommandType.StoredProcedure
                    SSCommand3.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = 1
                    Dim dtss As DataTable = Consulta_Procedure(SSCommand3, SSCommand3.CommandText)
                    If dtss.TableName = "Exception" Then
                        Throw New Exception(dtss.Rows(0).Item(0).ToString)
                    End If

                    Dim SSCommand4 As New SqlCommand("SP_CARGA_PAGOS_MASIVA")
                    SSCommand4.CommandType = CommandType.StoredProcedure
                    SSCommand4.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = 3
                    Dim carga As DataTable = Consulta_Procedure(SSCommand4, SSCommand4.CommandText)
                    If carga.TableName = "Exception" Then
                        Throw New Exception(carga.Rows(0).Item(0).ToString)
                    End If

                    LblMensaje.ForeColor = Drawing.Color.Green
                    LblMensaje.Text = carga.Rows(0)(0).ToString + " Pago(s) cargado(s) " + carga.Rows(1)(0).ToString + " Pago(s) duplicado(s)"
                    'LBLResultado.Text = "Cargadas " & tabla.Rows.Count & " Gestiones"
                Catch ex As Exception
                    LblMensaje.ForeColor = Drawing.Color.Red
                    LblMensaje.Text = ex.Message
                End Try
                Kill(Ruta & nombre)
            End If
            'Dim singleFile As IO.FileInfo
            'Dim value As Long
            'Dim FileCarga As String = ""

            'For Each singleFile In allFiles
            '    value = singleFile.Length
            '    FileCarga = singleFile.Name
            '    If value = 0 Then
            '        My.Computer.FileSystem.DeleteFile(Ruta & FileCarga, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently, FileIO.UICancelOption.DoNothing)
            '    Else
            '        ctlCarga = Ruta & "CTL_ASIG_" & TABLA & "_" & Now.ToShortDateString.Replace("/", "") & "" & ".ctl"
            '        logCarga = Ruta & "LOG_ASIG" & TABLA & "_" & Now.ToShortDateString.Replace("/", "") & "" & ".log"
            '        badCarga = "C:\Users\ccasanova\Documents\clientes\TUIIO\errorescargas.csv"

            '        If File.Exists(ctlCarga) Then
            '            Kill(ctlCarga)
            '        End If
            '        If File.Exists(logCarga) Then
            '            Kill(logCarga)
            '        End If
            '        If File.Exists(badCarga) Then
            '            Kill(badCarga)
            '        End If

            '        Dim SSCommandT As New SqlCommand("SP_VALIDA_TABLA")
            '        SSCommandT.CommandType = CommandType.StoredProcedure
            '        SSCommandT.Parameters.Add("@V_TABLA", SqlDbType.NVarChar).Value = TABLA
            '        SSCommandT.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 4
            '        SSCommandT.Parameters.Add("@V_Principal", SqlDbType.NVarChar).Value = 0
            '        Ejecuta_Procedure(SSCommandT)

            '        Dim fs As Stream
            '        fs = New System.IO.FileStream(ctlCarga, IO.FileMode.OpenOrCreate)
            '        Dim sw As New System.IO.StreamWriter(fs)
            '        sw.AutoFlush = True
            '        sw.WriteLine("load data")
            '        sw.WriteLine("infile '" & Ruta & "" & FileCarga & "'")
            '        sw.WriteLine("into table " & TABLA & "")
            '        sw.WriteLine("FIELDS TERMINATED BY '" & DdlDelimitador.SelectedValue & "' optionally enclosed by '""'")
            '        sw.WriteLine("TRAILING NULLCOLS")
            '        sw.WriteLine("(")
            '        sw.WriteLine("HIST_PA_CREDITO CHAR(25),")
            '        sw.WriteLine("HIST_PA_DTEPAGO CHAR(25),")
            '        sw.WriteLine("HIST_PA_MONTOPAGO CHAR(25)")
            '        sw.WriteLine(")")
            '        sw.Close()
            '        fs.Close()
            '        'Ejecuta("TRUNCATE TABLE " & TABLA)
            '        Dim SSCommand As New SqlCommand("SP_CARGA_TMP_ACTSALDOS")
            '        SSCommand.CommandType = CommandType.StoredProcedure
            '        SSCommand.Parameters.Add("@V_ARCHIVO", SqlDbType.NVarChar).Value = FileCarga
            '        SSCommand.Parameters.Add("@V_ERROR", SqlDbType.NVarChar).Value = badCarga
            '        SSCommand.Parameters.Add("@V_TABLA", SqlDbType.NVarChar).Value = TABLA
            '        Ejecuta_Procedure(SSCommand)
            '        Dim num As Integer = 0
            '        If (num <> 0) Then
            '            If (File.Exists(badCarga)) Then
            '                Dim fic As New IO.StreamReader(badCarga, System.Text.Encoding.UTF8)
            '                Dim linea As String = fic.ReadLine
            '                fic.Close()
            '                LblMensaje.Text = "Error en la cadena siguiente " & linea
            '                'LnkLog.Visible = True
            '                'LnkBad.Visible = True
            '            Else

            '                LblMensaje.Text = "Error Num " & num
            '            End If
            '        Else
            '            LblMensaje.Text = ""
            '            'LnkLog.Visible = False
            '            'LnkBad.Visible = False
            '            Dim SSCommand2 As New SqlCommand("SP_CARGA_TMP_HIST_PAGOS")
            '            SSCommand2.CommandType = CommandType.StoredProcedure
            '            SSCommand2.Parameters.Add("@V_USUARIO", SqlDbType.NVarChar).Value = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
            '            SSCommand2.Parameters.Add("@V_TIPO", SqlDbType.NVarChar).Value = 0
            '            Dim DtsCarga As DataTable = Consulta_Procedure(SSCommand2, "SP_CARGA_TMP_PAGOS")
            '            'GvCargaAsignacion.DataSource = DtsCarga
            '            'GvCargaAsignacion.DataBind()
            '            LblMensaje.Text = "Carga Completada"
            '        End If

            '        SubExisteRuta(Ruta & "Historico")
            '        FileCopy(Ruta & FileCarga, Ruta & "Historico/" & singleFile.Name.ToString.Substring(0, Len(singleFile.Name.ToString) - 4) & "_" & DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") & singleFile.Extension)
            '        Kill(Ruta & FileCarga)

            '        If (File.Exists(logCarga)) Then
            '            FileCopy(logCarga, Ruta & "Historico/" & singleFile.Name.ToString.Substring(0, Len(singleFile.Name.ToString) - 4) & "_" & DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") & ".log")
            '        End If

            '        If (File.Exists(badCarga)) Then
            '            FileCopy(badCarga, Ruta & "Historico/" & singleFile.Name.ToString.Substring(0, Len(singleFile.Name.ToString) - 4) & "_" & DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") & ".bad")
            '        End If
            '    End If
            'Next
        End If
        'Catch ex As Exception
        '    aviso(ex.Message)
        '    ' SendMail("BtnCargar_Click", ex, "", "", HidenUrs.Value)
        'End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Try
        '    Dim Usr As String = (CType(Session("usuario"), USUARIO)).CAT_LO_USUARIO
        'Catch ex As Exception
        '    OffLine(HidenUrs.Value)
        '    AUDITORIA(HidenUrs.Value, "Administrador", "CargaPagos", " ", "Page_Load", "", Request.ServerVariables("REMOTE_ADDR"), Request.ServerVariables("REMOTE_HOST"))
        '    Session.Clear()
        '    Session.Abandon()
        '    Response.Redirect("~/SesionExpirada.aspx")
        'End Try
        nombre = ""
        Try
            If Not IsPostBack Then
                HidenUrs.Value = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
                'If CType(Session("USUARIOADMIN"), USUARIO).CAT_LO_MADMINISTRADOR.ToString.Substring(24, 1) = 0 Then
                '    OffLine(HidenUrs.Value)
                '    Session.Clear()
                '    Session.Abandon()
                '    Response.Redirect("~/SesionExpirada.aspx")
                'End If
                'LLENAR_DROP(16, DdlDelimitador, "Delimitador", "Delimitador")
            End If
        Catch ex As Exception
            SendMail("Page_Load", ex, "", "", HidenUrs.Value)
        End Try
    End Sub

    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Administrador", "CargaPagos.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub

    Private Sub RadAsyncUpload1_FileUploaded(sender As Object, e As FileUploadedEventArgs) Handles RadAsyncUpload1.FileUploaded
        Try
            SubExisteRuta(Ruta)
            For Each s In System.IO.Directory.GetFiles(Ruta)
                System.IO.File.Delete(s)
            Next
            Dim nom As String = e.File.FileName
            nombre = nom
            Dim ruta_archivo As String = Ruta + nom
            e.File.SaveAs(ruta_archivo)
        Catch ex As Exception
        End Try
    End Sub
    Private Function FileToDataTable(path As String) As DataTable
        Dim lines = IO.File.ReadAllLines(path)
        Dim tbl = New DataTable, nameCols As DataTable
        'If DDLTipo.SelectedValue = 1 Then
        '    nameCols = SP.CARGA_GESTION(0, Nothing, Nothing, Nothing)
        'ElseIf DDLTipo.SelectedValue = 2 Then
        '    nameCols = SP.CARGA_GESTION(1, Nothing, Nothing, Nothing)
        'ElseIf DDLTipo.SelectedValue = 3 Then
        If DdlDelimitador.SelectedText = "Seleccione" Then
            LblMensaje.Text = "Seleccione un delimitador"
        Else
            Dim SSCommand2 As New SqlCommand("SP_CARGA_PAGOS_MASIVA")
            SSCommand2.CommandType = CommandType.StoredProcedure
            SSCommand2.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = 0
            nameCols = Consulta_Procedure(SSCommand2, SSCommand2.CommandText)
            If nameCols.TableName = "Exception" Then
                Throw New Exception(nameCols.Rows(0).Item(0).ToString)
            End If
        End If

        Dim encabezado = True
        Dim separador As Char

        Select Case DdlDelimitador.SelectedValue
            Case 1
                separador = Chr(9) 'Tabulador
            Case 2
                separador = Chr(44) 'Coma
        End Select


        Dim numLinea = 1, numCols = nameCols.Rows.Count
        For Each line In lines
            If encabezado Then
                Dim campos As String() = line.Split(separador)
                If campos.Length > numCols Then
                    Throw New Exception("El encabezado es incorrecto. Tiene campos extra")
                ElseIf campos.Length < numCols Then
                    Throw New Exception("El encabezado es incorecto. Le faltan campos")
                End If
                For Each name As DataRow In nameCols.Rows
                    tbl.Columns.Add(New DataColumn(name(0).ToString, GetType(String)))
                Next
                encabezado = False
            Else
                Dim objFields = From field In line.Split(separador)
                                Select field
                numLinea += 1

                Dim objetos = objFields.ToArray()
                Dim CRED As String = objetos(12).ToString
                If objetos.Length > numCols Then
                    Throw New Exception("La linea #" & numLinea & " tiene campos extra. Valide.(" & CRED & ")</br>" & line.ToString)
                ElseIf objetos.Length < numCols Then
                    Throw New Exception("La linea #" & numLinea & " tiene campos faltantes. Valide." & CRED & ")</br>" & line.ToString)
                End If
                Dim newRow = tbl.Rows.Add()
                    newRow.ItemArray = objetos
                End If
            Next
        'End If
        Return tbl
    End Function
    Protected Sub aviso(ByVal MSJ As String)
        RadAviso.RadAlert(MSJ, 400, 150, "Aviso", Nothing)
    End Sub
End Class
