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
Partial Class M_Administrador_CargaMasivaSMS
    Inherits System.Web.UI.Page
    Dim Ruta As String = Db.StrRuta() & "SMS\"
    Dim delim As String
    Dim ERRORS As String = "100"
    Dim TABLA As String = "tmp_masivo_sms"
    Shared nombre As String
    Public Property tmpUSUARIO As IDictionary
        Get
            Return CType(Session("USUARIO"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("USUARIO") = value
        End Set
    End Property
    Private Sub BtnCargar_Click(sender As Object, e As EventArgs) Handles BtnCargar.Click

        Dim tbl = New DataTable, campos = SP.CARGA_SMS(2, Nothing, "")
        Dim filas As Integer = campos.Rows.Count

        Dim dtsT As DataTable = New DataTable("DtsTRes")
        dtsT.Columns.Add("Credito")
        dtsT.Columns.Add("Estatus")
        For i As Integer = 0 To filas - 1
            Dim plantilla As String = campos(i)(0)
            Dim oraCommanPlantilla As New SqlCommand
            oraCommanPlantilla.CommandText = "SP_SMS"
            oraCommanPlantilla.CommandType = CommandType.StoredProcedure
            oraCommanPlantilla.Parameters.Add("@v_credito", SqlDbType.VarChar).Value = campos(i)(2)
            oraCommanPlantilla.Parameters.Add("@v_plantilla", SqlDbType.VarChar).Value = plantilla
            oraCommanPlantilla.Parameters.Add("@V_Bandera", SqlDbType.VarChar).Value = 9
            Dim DtsPlantilla As DataTable = Consulta_Procedure(oraCommanPlantilla, "Plantilla")
            Dim msj As String = DtsPlantilla(0)(0)

            Dim oracmjsonsms As New SqlCommand
            oracmjsonsms.CommandText = "Sp_getdatos"
            oracmjsonsms.CommandType = CommandType.StoredProcedure
            oracmjsonsms.Parameters.Add("@plantilla", SqlDbType.VarChar).Value = campos(i)(0)
            oracmjsonsms.Parameters.Add("@mensaje", SqlDbType.VarChar).Value = msj
            oracmjsonsms.Parameters.Add("@telefono", SqlDbType.VarChar).Value = "52" + campos(i)(1)
            oracmjsonsms.Parameters.Add("@Origen", SqlDbType.VarChar).Value = "Administrador"
            oracmjsonsms.Parameters.Add("@Credito", SqlDbType.VarChar).Value = campos(i)(2)
            oracmjsonsms.Parameters.Add("@usuario", SqlDbType.VarChar).Value = tmpUSUARIO("CAT_LO_USUARIO")
            oracmjsonsms.Parameters.Add("@Agencia", SqlDbType.VarChar).Value = tmpUSUARIO("CAT_LO_NUM_AGENCIA")
            oracmjsonsms.Parameters.Add("@producto", SqlDbType.VarChar).Value = "Credifiel"
            Dim DtsVarios As DataTable = Consulta_Procedure(oracmjsonsms, "Envio")

            Dim GRID = New DataTable, CONSULTA = SP.CARGA_SMS(3, Nothing, campos(i)(2))

            Dim Renglon As DataRow = dtsT.NewRow()

            Renglon("Credito") = campos(i)(2)
            If CONSULTA(0)(0) = "[id" Then
                Renglon("Estatus") = "Enviado correctamente"
            Else
                Renglon("Estatus") = CONSULTA(0)(0)
            End If


            dtsT.Rows.Add(Renglon)

        Next
        Grid_RESULTADO.DataSource = dtsT
        Grid_RESULTADO.DataBind()


    End Sub
    Private Function Plantilla() As String

    End Function
    Private Function FileToDataTable(path As String) As DataTable
        Dim lines = IO.File.ReadAllLines(path)
        Dim tbl = New DataTable, nameCols = SP.CARGA_SMS(0, Nothing, "")
        Dim encabezado = True
        Dim separador As Char

        Select Case DDLSeparador.SelectedValue
            Case 0
                separador = Chr(9) 'Tabulador
            Case 1
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

    Function LlenarDatos(ByVal V_Valor1 As String, ByVal V_Valor2 As String, ByVal V_Valor3 As String, ByVal V_Valor4 As String, ByVal V_Valor5 As String, ByVal V_Valor6 As String, ByVal V_Valor7 As String, ByVal V_Valor8 As String, ByVal V_Valor9 As String, ByVal V_Bandera As String) As DataTable
        Dim oraCommanSms As New SqlCommand
        oraCommanSms.CommandText = "SP_VARIOS_SMS"
        oraCommanSms.CommandType = CommandType.StoredProcedure
        oraCommanSms.Parameters.Add("V_Valor1", SqlDbType.VarChar).Value = V_Valor1
        oraCommanSms.Parameters.Add("V_Valor2", SqlDbType.VarChar).Value = V_Valor2
        oraCommanSms.Parameters.Add("V_Valor3", SqlDbType.VarChar).Value = V_Valor3
        oraCommanSms.Parameters.Add("V_Valor4", SqlDbType.VarChar).Value = V_Valor4
        oraCommanSms.Parameters.Add("V_Valor5", SqlDbType.VarChar).Value = V_Valor5
        oraCommanSms.Parameters.Add("V_Valor6", SqlDbType.VarChar).Value = V_Valor6
        oraCommanSms.Parameters.Add("V_Valor7", SqlDbType.VarChar).Value = V_Valor7
        oraCommanSms.Parameters.Add("V_Valor8", SqlDbType.VarChar).Value = V_Valor8
        oraCommanSms.Parameters.Add("V_Valor9", SqlDbType.VarChar).Value = V_Valor9
        oraCommanSms.Parameters.Add("V_Bandera", SqlDbType.VarChar).Value = V_Bandera
        Dim DtsVarios As DataTable = Consulta_Procedure(oraCommanSms, "SMS")
        Return DtsVarios
    End Function

    Sub llenarProd()
        Dim SSCommand2 As New SqlCommand("SP_CATALOGOS") With {
                        .CommandType = CommandType.StoredProcedure
                    }
        SSCommand2.Parameters.Add("@v_Bandera", SqlDbType.Decimal).Value = 9
        Dim objDSa As DataTable = Consulta_Procedure(SSCommand2, SSCommand2.CommandText)

        DDLProducto.DataTextField = "productos"
        DDLProducto.DataValueField = "productos"
        DDLProducto.DataSource = objDSa
        DDLProducto.DataBind()
    End Sub
    Public Sub ctl_Carga()
        Db.SubExisteRuta(Ruta)
        Dim Directory As New IO.DirectoryInfo(Ruta)
        Dim allFiles As IO.FileInfo() = Directory.GetFiles.Where(Function(fi) fi.Extension = ".csv" OrElse fi.Extension = ".txt" OrElse fi.Extension = ".unl").ToArray

        If DDLProducto.SelectedValue = "" Then
            aviso("Selecciona un producto")
        ElseIf allFiles.Length = 0 Then
            aviso("Archivo No Seleccionado o No Valido ")
        ElseIf DDLSeparador.SelectedValue = "Seleccione" Then
            aviso("Seleccione un Delimitador")
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
                        'Convierte el archivo a datatable y verifica que tenga el numero de campos correcto

                        Dim dt As DataTable = FileToDataTable(Ruta & FileCarga)
                        Dim totalDisplay As Integer = dt.Rows.Count

                        'Rompe el datatable en varios más pequeños de máximo 4000 rows c/u
                        Dim tables = dt.AsEnumerable().ToChunks_CargaMasivaSms(4000).[Select](Function(rows) rows.CopyToDataTable())

                        Dim tablesCount = tables.Count, porcentaje As Double = 0

                        Dim res As New DataTable
                        'SUBIMOS LA CARGA A LA PR_Cliente
                        For i As Integer = 0 To tablesCount - 1
                            res = UploadToPR(tables(i))

                        Next
                        Dim DtsPreview As DataTable = LlenarDatos("", "", "", "", "", "", "", "", "", 4)
                        Grid_RESULTADO.DataSource = DtsPreview
                        Grid_RESULTADO.DataBind()
                        BtnCargar.Enabled = True
                    Catch ex As Exception
                        aviso(ex.Message)
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
        Dim res As DataTable = SP.CARGA_SMS(1, dt, "")
        If res.TableName = "Exception" Then
            Throw New Exception("Error: " & res(0)(0).ToString)
        End If
        Return res
    End Function
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
        RadAviso.RadAlert(MSJ.Replace(Chr(10),"").Replace(Chr(13),"").Replace("'","").Replace("""",""), 400, 150, "Aviso", Nothing)
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        llenarProd()
    End Sub
    Protected Sub BtnPre_Click(sender As Object, e As EventArgs) Handles BtnPre.Click
        Grid_RESULTADO.DataSource = Nothing
        Grid_RESULTADO.DataBind()
        BtnCargar.Enabled = False
        ctl_Carga()


    End Sub
    Private Sub M_Administrador_CargaMasivaSMS_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            nombre = ""
        End If
    End Sub
End Class
Module extCargaMasivaSms
    <Extension()>
    Public Iterator Function ToChunks_CargaMasivaSms(Of T)(ByVal enumerable As IEnumerable(Of T), ByVal chunkSize As Integer) As IEnumerable(Of IEnumerable(Of T))
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