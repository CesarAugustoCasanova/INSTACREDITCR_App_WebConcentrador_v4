Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports Db
Imports Funciones
Imports Telerik.Web.UI
Partial Class M_Administrador_CargaActualizacion
    Inherits System.Web.UI.Page
    Dim ERRORS As String = "1"
    Dim SKIP As String = "1"
    Dim ctlCarga As String
    Dim logCarga As String
    Dim badCarga As String
    Shared nombre As String
    Dim Ruta As String = StrRuta() & "Actualizacion\"
    Dim d As EventArgs = EventArgs.Empty
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
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If tmpUSUARIO Is Nothing Then
            OffLine(HidenUrs.Value)
            AUDITORIA(HidenUrs.Value, "Administrador", "Actualizacion", " ", "Page_Load", "", Request.ServerVariables("REMOTE_ADDR"), Request.ServerVariables("REMOTE_HOST"))
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/SesionExpirada.aspx")
        Else
            Try
                If Not IsPostBack Then
                    HidenUrs.Value = tmpUSUARIO("CAT_LO_USUARIO")
                End If
            Catch ex As Exception
                SendMail("Page_Load", ex, "", "", HidenUrs.Value)
            End Try
        End If

    End Sub


    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Administrador", "Actualizacion.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub


    Private Sub RAUActualizacion_FileUploaded(sender As Object, e As FileUploadedEventArgs) Handles RAUActualizacion.FileUploaded
        ' Try
        SubExisteRuta(Ruta)
        Dim nom As String = e.File.FileName
        If File.Exists(Ruta + nom) Then
            Kill(Ruta + nom)
        End If
        nombre = nom
        Dim ruta_archivo As String = Ruta + nom
        e.File.SaveAs(ruta_archivo)
        'Catch ex As Exception
        'End Try
    End Sub

    Protected Sub Limpia()
        LblMensaje.Text = ""
        GvCargaAsignacion2.DataSource = Nothing
        GvCargaAsignacion2.DataBind()
        GvCargaAsignacion2.Visible = False
    End Sub

    Sub aviso(ByVal MSJ As String)
        RadAviso.RadAlert(MSJ.Replace("""", "").Replace("'", "").Replace(Chr(10), "").Replace(Chr(13), ""), 400, 150, "Aviso", Nothing)
    End Sub
    Protected Sub RBCargarActualizacion_Click(sender As Object, e As EventArgs) Handles RBCargarActualizacion.Click
        Dim script As String = "<script type=text/javascript>modal();</script>"

        Try
            Dim Directory As New IO.DirectoryInfo(Ruta)
            Dim allFiles As IO.FileInfo() = Directory.GetFiles.Where(Function(fi) fi.Extension = ".csv" OrElse fi.Extension = ".txt").ToArray
            If allFiles.Length = 0 Then
                aviso("Archivo No Seleccionado o No Valido ")
            Else

                Dim tabla As DataTable
                Try
                    tabla = FileToDataTableVi(Ruta + nombre)

                    Dim SSCommand2 As New SqlCommand("SP_CARGA_ACTUALIZACION")
                    SSCommand2.CommandType = CommandType.StoredProcedure
                    SSCommand2.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = 1
                    Consulta_Procedure(SSCommand2, SSCommand2.CommandText)

                    Dim fakebulk As New SqlBulkCopy(Conectando())
                    'If DDLTipo.SelectedValue = 1 Then
                    fakebulk.DestinationTableName = "dbo.TMP_CARGA_ACTUALIZACION"
                    fakebulk.ColumnMappings.Add("TMP_CA_CREDITO", "TMP_CA_CREDITO")
                    fakebulk.ColumnMappings.Add("TMP_CA_MOTIVO", "TMP_CA_MOTIVO")
                    fakebulk.ColumnMappings.Add("TMP_CA_SUBMOTIVO", "TMP_CA_SUBMOTIVO")
                    fakebulk.ColumnMappings.Add("TMP_CA_APLICA", "TMP_CA_APLICA")

                    fakebulk.WriteToServer(tabla)

                    Dim SSCommand3 As New SqlCommand("SP_CARGA_ACTUALIZACION")
                    SSCommand3.CommandType = CommandType.StoredProcedure
                    SSCommand3.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = 2
                    Dim dtss As DataTable = Consulta_Procedure(SSCommand3, SSCommand3.CommandText)

                    Dim SSCommand4 As New SqlCommand("SP_CARGA_ACTUALIZACION")
                    SSCommand4.CommandType = CommandType.StoredProcedure
                    SSCommand4.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = 3
                    Dim carga As DataTable = Consulta_Procedure(SSCommand4, SSCommand4.CommandText)

                    LblMensaje.ForeColor = Drawing.Color.Green
                    LblMensaje.Text = carga.Rows(1)(0).ToString + " Creditos Actulizados " + carga.Rows(0)(0).ToString + " Creditos Inexistentes "

                Catch ex As Exception
                    LblMensaje.ForeColor = Drawing.Color.Red
                    LblMensaje.Text = ex.Message
                End Try
                FileCopy(Ruta + nombre, Ruta + "Historico/" & DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") & "_" & nombre)
                Kill(Ruta + nombre)
            End If

        Catch ex As Exception
            aviso(ex.Message)
            SendMail("BtnCargar_Click", ex, "", "", HidenUrs.Value)
        End Try
    End Sub
    Private Function FileToDataTableVi(path As String) As DataTable
        Dim lines = IO.File.ReadAllLines(path)
        Dim tbl = New DataTable, nameCols As DataTable

        Dim SSCommand2 As New SqlCommand("SP_CARGA_ACTUALIZACION")
        SSCommand2.CommandType = CommandType.StoredProcedure
        SSCommand2.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = 0
        nameCols = Consulta_Procedure(SSCommand2, "SP_CARGA_ACTUALIZACION")

        Dim encabezado = True
        Dim separador As Char = Chr(44) 'Coma

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
End Class
