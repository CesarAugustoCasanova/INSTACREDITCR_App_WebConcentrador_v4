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

Partial Class M_Administrador_CargaMasivaGestiones
    Inherits System.Web.UI.Page
    Dim Ruta As String = Db.StrRuta() & "GESTIONES\"
    Shared nombre As String

    Public Property tmpUSUARIO As IDictionary
        Get
            Return CType(Session("USUARIO"), IDictionary) ' 
        End Get
        Set(value As IDictionary)
            Session("USUARIO") = value
        End Set
    End Property
    Private Sub M_Administrador_CargaMasivaGestiones_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            nombre = ""
            LblMensaje.Text = ""
            LBLResultado.Text = ""
        End If
    End Sub
    Private Sub BtnCargar_Click(sender As Object, e As EventArgs) Handles BtnCargar.Click

        Dim DtsPreview As DataTable = SP.CARGA_GESTION(6, Nothing, Nothing, tmpUSUARIO("CAT_LO_NUM_AGENCIA"))
        If DtsPreview.TableName = "Exception" Then
            LBLResultado.Text = DtsPreview.Rows(0)(0).ToString
        Else
            LBLResultado.Text = DtsPreview(0)(0).ToString + " Credito(s) Cargado(s) "
            Grid_RESULTADO.DataSource = Nothing
            Grid_RESULTADO.DataBind()
        End If

    End Sub

    Private Sub BtnPreview_Click(sender As Object, e As EventArgs) Handles BtnPreview.Click

        Db.SubExisteRuta(Ruta)
        Dim Directory As New IO.DirectoryInfo(Ruta)
        Dim allFiles As IO.FileInfo() = Directory.GetFiles.Where(Function(fi) fi.Extension = ".csv" OrElse fi.Extension = ".txt" OrElse fi.Extension = ".unl").ToArray

        LblMensaje.Text = ""
        If DDLTipo.SelectedText = "Seleccione" Then
            LBLResultado.Text = "Seleccione un tipo"

        ElseIf DDLSeparador.SelectedText = "Seleccione" Then
            LBLResultado.Text = "Seleccione un separador"
        ElseIf allFiles.Length = 0 Then
            LBLResultado.Text = "Archivo No Seleccionado o No Valido "
        Else
            Dim tabla As DataTable
            Try
                tabla = FileToDataTable(Ruta & nombre)

                Dim dts As DataTable = SP.CARGA_GESTION(5, Nothing, Nothing, Nothing)
                If dts.TableName = "Exception" Then
                    Throw New Exception(dts.Rows(0).Item(0).ToString)
                End If
                Dim fakebulk As New SqlBulkCopy(Conectando())
                If DDLTipo.SelectedValue = 1 Then
                    fakebulk.DestinationTableName = "dbo.TMP_HIST_GEST_MASIVAS"
                    fakebulk.ColumnMappings.Add("TMP_HIST_GM_CREDITO", "TMP_HIST_GM_CREDITO")
                    fakebulk.ColumnMappings.Add("TMP_HIST_GM_USUARIO", "TMP_HIST_GM_USUARIO")
                    fakebulk.ColumnMappings.Add("TMP_HIST_GM_CODIGO", "TMP_HIST_GM_CODIGO")
                    fakebulk.ColumnMappings.Add("TMP_HIST_GM_COMENTARIO", "TMP_HIST_GM_COMENTARIO")
                    fakebulk.ColumnMappings.Add("TMP_HIST_GM_DTEACTIVIDAD", "TMP_HIST_GM_DTEACTIVIDAD")

                ElseIf DDLTipo.SelectedValue = 2 Then
                    fakebulk.DestinationTableName = "dbo.HIST_VISITAS_MASIVAS"

                Else
                    LBLResultado.Text = "Seleccione un tipo"
                End If

                fakebulk.WriteToServer(tabla)


                If DDLTipo.SelectedValue = 1 Then
                    'SP.CARGA_GESTION(2, Nothing, Nothing, Nothing)

                    Dim DtsPreview As DataTable = SP.CARGA_GESTION(4, Nothing, Nothing, tmpUSUARIO("CAT_LO_NUM_AGENCIA"))
                    Grid_RESULTADO.DataSource = DtsPreview
                    Grid_RESULTADO.DataBind()
                    BtnCargar.Enabled = True
                ElseIf DDLTipo.SelectedValue = 2 Then
                    SP.CARGA_GESTION(3, Nothing, Nothing, Nothing)
                Else
                    LBLResultado.Text = "Seleccione un tipo"
                End If
                LBLResultado.Text = "Preview Gestiones"
                'LBLResultado.Text = "Cargadas " & tabla.Rows.Count & " Gestiones"
            Catch ex As Exception
                LBLResultado.Text = ex.Message
            End Try
            Kill(Ruta & nombre)
        End If
    End Sub
    Private Sub DDLTipo_SelectedIndexChanged(sender As Object, e As DropDownListEventArgs) Handles DDLTipo.SelectedIndexChanged
        Grid_RESULTADO.DataSource = Nothing
        Grid_RESULTADO.DataBind()

        LblReglasNegocio.Text = ""
        LOGestion.Visible = False
        LOVisitas.Visible = False
        LOAsignacion.Visible = False
        If DDLTipo.SelectedValue = 1 Then
            LOGestion.Visible = True
            LOVisitas.Visible = False
            LOAsignacion.Visible = False
            LblReglasNegocio.Text = SP.CARGA_GESTION(7, Nothing, Nothing, DDLTipo.SelectedValue).Rows(0).Item(0).ToString
        ElseIf DDLTipo.SelectedValue = 2 Then
            LOGestion.Visible = False
            LOVisitas.Visible = True
            LOAsignacion.Visible = False
            LblReglasNegocio.Text = SP.CARGA_GESTION(7, Nothing, Nothing, DDLTipo.SelectedValue).Rows(0).Item(0).ToString
        ElseIf DDLTipo.SelectedValue = 3 Then
            LOGestion.Visible = False
            LOVisitas.Visible = False
            LOAsignacion.Visible = True
            LblReglasNegocio.Text = SP.CARGA_GESTION(7, Nothing, Nothing, DDLTipo.SelectedValue).Rows(0).Item(0).ToString
        End If
    End Sub
    Private Function FileToDataTable(path As String) As DataTable
        Dim lines = IO.File.ReadAllLines(path)
        Dim tbl = New DataTable, nameCols As DataTable = Nothing
        If DDLTipo.SelectedValue = 1 Then
            nameCols = SP.CARGA_GESTION(0, Nothing, Nothing, Nothing)
        ElseIf DDLTipo.SelectedValue = 2 Then
            nameCols = SP.CARGA_GESTION(1, Nothing, Nothing, Nothing)
        ElseIf DDLTipo.SelectedValue = 3 Then
            nameCols = SP.CARGA_GESTION(2, Nothing, Nothing, Nothing)
        Else
            LBLResultado.Text = "Seleccione un tipo"
        End If
        If nameCols.TableName = "Exception" Then
            Throw New Exception(nameCols.Rows(0).Item(0).ToString)
        End If
        Dim encabezado = True
        Dim separador As Char

        Select Case DDLSeparador.SelectedValue
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

End Class
