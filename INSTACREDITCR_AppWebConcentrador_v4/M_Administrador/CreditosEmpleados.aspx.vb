Imports System.Data.SqlClient
Imports System.Data
Imports System.Diagnostics
Imports System.IO
Imports Db
Imports Funciones
Imports System.Net.Mail
Imports Telerik.Web.UI
Imports System.Threading.Tasks
Imports System.Threading
Imports System.Runtime.CompilerServices

Partial Class M_Administrador_CreditosEmpleados
    Inherits System.Web.UI.Page

    Dim Ruta As String = Db.StrRuta() & "CreditosEmpleados\"
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

    Protected Sub BtnCargar_Click(sender As Object, e As EventArgs) Handles BtnCargar.Click
        ProcesaArchivo_Thread()
        'ejecutacoordenadas()
        'correos()
        'disponeCP()
        'asigna()
    End Sub

    Protected Sub ProcesaArchivo_Thread()
        Db.SubExisteRuta(Ruta)

        Dim Directory As New IO.DirectoryInfo(Ruta)
        Dim allFiles As IO.FileInfo() = Directory.GetFiles.Where(Function(fi) fi.Extension = ".csv" OrElse fi.Extension = ".txt" OrElse fi.Extension = ".unl").ToArray
        If allFiles.Length = 0 Then
            aviso("Archivo No Seleccionado o No Valido ")
        Else
            Dim singleFile As IO.FileInfo
            Dim value As Long
            Dim FileCarga As String = ""
            Dim nuevas As Integer = 0, actualizadas As Integer = 0


            For Each singleFile In allFiles
                value = singleFile.Length
                FileCarga = singleFile.Name
                If value = 0 Then
                    My.Computer.FileSystem.DeleteFile(Ruta & FileCarga, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently, FileIO.UICancelOption.DoNothing)
                Else
                    Try

                        'Dim progress As RadProgressContext = RadProgressContext.Current
                        'progress.Speed = "N/A"
                        'progress.PrimaryTotal = 100
                        'progress.PrimaryValue = 0
                        'progress.PrimaryPercent = 5

                        ''Convierte el archivo a datatable y verifica que tenga el numero de campos correcto

                        'Dim dt As DataTable = FileToDataTable(Ruta & FileCarga)
                        'Dim totalDisplay As Integer = dt.Rows.Count

                        ''Rompe el datatable en varios más pequeños de máximo 4000 rows c/u
                        'Dim tables = dt.AsEnumerable().ToChunks_CreditosEmpleados(4000).[Select](Function(rows) rows.CopyToDataTable())

                        'Dim tablesCount = tables.Count, porcentaje As Double = 0

                        'Dim res As New DataTable
                        ''SUBIMOS LA CARGA A LA PR_Cliente
                        'For i As Integer = 0 To tablesCount - 1

                        '    porcentaje = (i + 1) / tablesCount
                        '    progress.PrimaryTotal = totalDisplay
                        '    progress.PrimaryValue += tables(i).Rows.Count
                        '    progress.PrimaryPercent = Math.Floor(porcentaje * 100)
                        '    res = UploadToPR(tables(i))
                        '    nuevas += Integer.Parse(res(0)(3).ToString)
                        '    actualizadas += Integer.Parse(res(0)(2).ToString)
                        'Next
                        'LblMensaje.ForeColor = Drawing.Color.Green
                        'LblMensaje.Text = nuevas & " Cuentas Nuevas y " & actualizadas & " Cuentas Actualizadas"






                        Dim tabla As DataTable = FileToDataTable(Ruta + FileCarga)


                        Dim SSCommand2 As New SqlCommand("SP_CARGA_CREDITOSEMPLEADOS")
                        SSCommand2.CommandType = CommandType.StoredProcedure
                        SSCommand2.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = 2
                        Consulta_Procedure(SSCommand2, SSCommand2.CommandText)

                        Dim fakebulk As New SqlBulkCopy(Conectando())
                        fakebulk.DestinationTableName = "dbo.TMP_CARGA_CREDITOSEMPLEADOS"
                        fakebulk.ColumnMappings.Add("TMP_Credito", "TMP_Credito")
                        fakebulk.ColumnMappings.Add("TMP_Nombre", "TMP_Nombre")
                        fakebulk.ColumnMappings.Add("TMP_DTE_INGRESO", "TMP_DTE_INGRESO")
                        fakebulk.ColumnMappings.Add("TMP_DTE_BAJA", "TMP_DTE_BAJA")
                        fakebulk.ColumnMappings.Add("TMP_MONTO_CREDITO", "TMP_MONTO_CREDITO")
                        fakebulk.ColumnMappings.Add("TMP_PLAZO", "TMP_PLAZO")
                        fakebulk.ColumnMappings.Add("TMP_VALORDESCUENTO", "TMP_VALORDESCUENTO")
                        fakebulk.ColumnMappings.Add("TMP_Pagare", "TMP_Pagare")
                        fakebulk.ColumnMappings.Add("TMP_FRECUENCIA", "TMP_FRECUENCIA")
                        fakebulk.ColumnMappings.Add("TMP_SALDO_PAGADO", "TMP_SALDO_PAGADO")
                        fakebulk.ColumnMappings.Add("TMP_SaldoActual", "TMP_SaldoActual")
                        fakebulk.ColumnMappings.Add("TMP_SaldoVencido", "TMP_SaldoVencido")
                        fakebulk.ColumnMappings.Add("TMP_DTE_INCUMPLIMINETO", "TMP_DTE_INCUMPLIMINETO")
                        fakebulk.ColumnMappings.Add("TMP_EXPEDIENTE", "TMP_EXPEDIENTE")
                        fakebulk.ColumnMappings.Add("TMP_JUZGADO", "TMP_JUZGADO")
                        fakebulk.WriteToServer(tabla)

                        'Dim SSCommand3 As New SqlCommand("SP_CARGA_EXCLUSIONES")
                        'SSCommand3.CommandType = CommandType.StoredProcedure
                        'SSCommand3.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = 4
                        'Dim dtss As DataTable = Consulta_Procedure(SSCommand3, SSCommand3.CommandText)

                        Dim SSCommand4 As New SqlCommand("SP_CARGA_CREDITOSEMPLEADOS")
                        SSCommand4.CommandType = CommandType.StoredProcedure
                        SSCommand4.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = 1

                        Dim carga As DataTable = Consulta_Procedure(SSCommand4, SSCommand4.CommandText)

                        If carga.TableName = "Exception" Then
                            Throw New Exception(carga.Rows(0).Item(0).ToString)
                        End If

                        LblMensaje.ForeColor = Drawing.Color.Green
                        LblMensaje.Text = carga.Rows(0).Item(3) & " Cuentas Nuevas y <br/>" & carga.Rows(0).Item(2) & " Cuentas Actualizadas"
                        'LblMensaje.Text = carga.Rows(0)(0).ToString + " Creditos Correctos " + carga.Rows(1)(0).ToString + " Convenios Correcto " + carga.Rows(2)(0).ToString + "Creditos/convenios incorrectos"


                    Catch ex As Exception
                        LblMensaje.ForeColor = Drawing.Color.Red
                        LblMensaje.Text = ex.Message
                    Finally
                        SubExisteRuta(Ruta & "Historico")
                        FileCopy(Ruta & FileCarga, Ruta & "Historico/" & singleFile.Name.ToString.Substring(0, Len(singleFile.Name.ToString) - 4) & "_" & DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") & singleFile.Extension)
                        Kill(Ruta & FileCarga)
                        SP.AUDITORIA_GLOBAL(0, tmpUSUARIO("CAT_LO_USUARIO"), "Modulo Administracion", "Cartera Cargada")

                    End Try
                End If
            Next
        End If
    End Sub
    Private Function UploadToPR(ByRef dt As DataTable) As DataTable
        Dim res As DataTable = SP.CARGA_CREDITOS_EMPLEADOS(1, dt)
        If res.TableName = "Exception" Then
            Throw New Exception("Error: " & res(0)(0).ToString)
        End If
        Return res
    End Function


    Private Function FileToDataTable(path As String) As DataTable
        Dim lines = IO.File.ReadAllLines(path)
        Dim tbl = New DataTable, nameCols = SP.CARGA_CREDITOS_EMPLEADOS(0, Nothing)
        If nameCols.TableName = "Exception" Then
            Throw New Exception(nameCols.Rows(0)(0).ToString)
        End If
        Dim encabezado = True
        Dim separador As Char

        Select Case DDLSeparador.SelectedValue
            Case 0
                separador = Chr(9) 'Tabulador
            Case 1
                separador = Chr(44) 'Coma
            Case 2
                separador = Chr(124) 'pipe
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
                If objetos.Length > numCols Then
                    Throw New Exception("La linea #" & numLinea & " tiene campos extra. Valide.")
                ElseIf objetos.Length < numCols Then
                    Throw New Exception("La linea #" & numLinea & " tiene campos faltantes. Valide.")
                End If
                Dim newRow = tbl.Rows.Add()
                newRow.ItemArray = objetos
            End If
        Next
        Return tbl
    End Function
    'Private Sub ejecutacoordenadas()
    '    Dim startInfo As System.Diagnostics.ProcessStartInfo
    '    Dim pStart As New System.Diagnostics.Process
    '    startInfo = New System.Diagnostics.ProcessStartInfo("C:\SitiosDesarrollo\DONDE\CARGA_COORDENADAS.exe")

    '    pStart.StartInfo = startInfo
    '    pStart.Start()
    '    pStart.WaitForExit()
    'End Sub
    Private Sub correos()
        Dim res As DataTable = SP.CARGA_CARTERA(2, tmpUSUARIO("CAT_LO_USUARIO"), Nothing, "")
        Dim cuerpo As String = ""
        For Each datar As DataRow In res.Rows
            cuerpo += datar.Item(0) & vbLf
        Next
        Dim email As New MailMessage("Soporte@mccollect.com.mx", "ricardo.coyote@mccollect.com.mx", "Cuentas no encontradas", cuerpo)

        Dim enviador As New SmtpClient("smtp.gmail.com", 587)
        enviador.Credentials = New System.Net.NetworkCredential("Soporte@mccollect.com.mx", "Adalesperra2")
        enviador.EnableSsl = True

        Try
            enviador.Send(email)
            email.Dispose()
        Catch e As Exception

        End Try
    End Sub
    Private Sub asigna()
        Dim SSCommand2 As New SqlCommand("SP_ASIGNACION_XCENTROS") With {
                     .CommandType = CommandType.StoredProcedure
                 }
        SSCommand2.Parameters.Add("@V_USUARIO", SqlDbType.VarChar).Value = tmpUSUARIO("CAT_LO_USUARIO")
        Consulta_Procedure(SSCommand2, "SP_ASIGNACION_XCENTROS")


        Dim SSCommand3 As New SqlCommand("SP_REORDENA_XCENTROS") With {
                    .CommandType = CommandType.StoredProcedure
                }
        Consulta_Procedure(SSCommand3, "SP_REORDENA_XCENTROS")
    End Sub
    Private Sub disponeCP()
        Dim SSCommand2 As New SqlCommand("SP_DISTRITOS") With {
                     .CommandType = CommandType.StoredProcedure
                 }
        SSCommand2.Parameters.Add("@Bandera", SqlDbType.Int).Value = 6
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If tmpUSUARIO Is Nothing Then
            OffLine(HidenUrs.Value)
            AUDITORIA(HidenUrs.Value, "Administrador", "CargaCartera", " ", "Page_Load", "", Request.ServerVariables("REMOTE_ADDR"), Request.ServerVariables("REMOTE_HOST"))
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/SesionExpirada.aspx")
        Else
            HidenUrs.Value = tmpUSUARIO("Cat_lo_usuario")
            If Not IsPostBack Then
                RadProgressArea1.Localization.Uploaded = "Subiendo datos:"
                RadProgressArea1.Localization.UploadedFiles = "Progreso:"
                RadProgressArea1.Localization.CurrentFileName = "Procesando datos: "
            End If
        End If

    End Sub

    'Sub llenarProd()
    '    Dim SSCommand2 As New SqlCommand("SP_CATALOGOS") With {
    '                .CommandType = CommandType.StoredProcedure
    '            }
    '    SSCommand2.Parameters.Add("@v_Bandera", SqlDbType.Decimal).Value = 9
    '    Dim objDSa As DataTable = Consulta_Procedure(SSCommand2, SSCommand2.CommandText)

    '    DDLProducto.DataTextField = "productos"
    '    DDLProducto.DataValueField = "productos"
    '    DDLProducto.DataSource = objDSa
    '    DDLProducto.DataBind()
    'End Sub

    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Administrador", "CargaCartera.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub

    Private Sub RadAsyncUpload1_FileUploaded(sender As Object, e As FileUploadedEventArgs) Handles RadAsyncUpload1.FileUploaded
        Try
            SubExisteRuta(Ruta)
            For Each s In System.IO.Directory.GetFiles(Ruta)
                System.IO.File.Delete(s)
            Next
            Dim nom As String = e.File.FileName
            Dim ruta_archivo As String = Ruta + nom
            e.File.SaveAs(ruta_archivo)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub aviso(ByVal MSJ As String)
        RadAviso.RadAlert(MSJ, 400, 150, "Aviso", Nothing)
    End Sub

    Private Sub BtnCargar_Command(sender As Object, e As CommandEventArgs) Handles BtnCargar.Command

    End Sub


End Class

Module ext_CreditosEmpleados
    <Extension()>
    Public Iterator Function ToChunks_CreditosEmpleados(Of T)(ByVal enumerable As IEnumerable(Of T), ByVal chunkSize As Integer) As IEnumerable(Of IEnumerable(Of T))
        Dim itemsReturned As Integer = 0
        Dim list = enumerable.ToList()
        Dim count As Integer = list.Count

        While itemsReturned < count
            Dim currentChunkSize As Integer = Math.Min(chunkSize, count - itemsReturned)
            Yield list.GetRange(itemsReturned, currentChunkSize)
            itemsReturned += currentChunkSize
        End While
    End Function
End Module

