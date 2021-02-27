Imports System.Data.SqlClient
Imports System.Data
Imports System.Diagnostics
Imports System.IO
Imports Db
Imports Funciones
Imports Telerik.Web.UI
Imports System.Net
Imports Newtonsoft.Json

Partial Class MAdministrador_CargarAsignacion
    Inherits System.Web.UI.Page
    Dim ERRORS As String = "1"
    Dim SKIP As String = "1"
    Dim ctlCarga As String
    Dim logCarga As String
    Dim badCarga As String
    Shared nombre As String
    Dim Ruta As String = StrRuta() & "Asignacion\"
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
            AUDITORIA(HidenUrs.Value, "Administrador", "CargarAsignacion", " ", "Page_Load", "", Request.ServerVariables("REMOTE_ADDR"), Request.ServerVariables("REMOTE_HOST"))
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/SesionExpirada.aspx")
        Else
            Try
                If Not IsPostBack Then
                    HidenUrs.Value = tmpUSUARIO("CAT_LO_USUARIO")
                    LLENAR_DROP_C(35, "", DdlInstanciaA, "V_VALOR", "T_VALOR")
                    LLENAR_DROP_C(35, "", DdlInstanciaC, "V_VALOR", "T_VALOR")
                End If
            Catch ex As Exception
                SendMail("Page_Load", ex, "", "", HidenUrs.Value)
            End Try
        End If

    End Sub
    Protected Sub BtnCargar_Click(sender As Object, e As EventArgs) Handles BtnCargar.Click
        Dim script As String = "<script type=text/javascript>modal();</script>"

        Try
            If DdltipoAsig.SelectedValue = "Seleccione" Then
                aviso("Seleccione un tipo de asignación ")
                Limpia()
            Else
                Dim Directory As New IO.DirectoryInfo(Ruta)
                Dim allFiles As IO.FileInfo() = Directory.GetFiles.Where(Function(fi) fi.Extension = ".csv" OrElse fi.Extension = ".txt").ToArray
                If allFiles.Length = 0 Then
                    aviso("Archivo No Seleccionado o No Valido ")
                ElseIf DdlInstanciaA.SelectedValue = "Seleccione" And DdltipoAsig.SelectedValue <> "ArchivoC" Then
                    aviso("Seleccione una instancia")
                Else
                    If DdltipoAsig.SelectedValue = "Archivo" Then
                        Dim tabla As DataTable
                        Try
                            tabla = FileToDataTableA(Ruta & nombre)

                            Dim SSCommand2 As New SqlCommand("SP_CARGA_ASIGNACION_V3")
                            SSCommand2.CommandType = CommandType.StoredProcedure
                            SSCommand2.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = 1
                            Consulta_Procedure(SSCommand2, SSCommand2.CommandText)

                            Dim fakebulk As New SqlBulkCopy(Conectando())
                            'If DDLTipo.SelectedValue = 1 Then
                            fakebulk.DestinationTableName = "dbo.TMP_ASIGNACION"
                            fakebulk.ColumnMappings.Add("TMP_PR_MC_CREDITO", "TMP_PR_MC_CREDITO")
                            'fakebulk.ColumnMappings.Add("TMP_PR_MC_USUARIO", "TMP_PR_MC_USUARIO")
                            'fakebulk.ColumnMappings.Add("TMP_PR_MC_INSTANCIA", "TMP_PR_MC_INSTANCIA")

                            fakebulk.WriteToServer(tabla)

                            Dim SSCommand3 As New SqlCommand("SP_CARGA_ASIGNACION_V3")
                            SSCommand3.CommandType = CommandType.StoredProcedure
                            SSCommand3.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = 2
                            Dim dtss As DataTable = Consulta_Procedure(SSCommand3, SSCommand3.CommandText)

                            Dim SSCommand4 As New SqlCommand("SP_CARGA_ASIGNACION_V3")
                            SSCommand4.CommandType = CommandType.StoredProcedure
                            SSCommand4.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = 3
                            SSCommand4.Parameters.Add("@V_USUARIO", SqlDbType.VarChar).Value = DdlUsuarioA.SelectedValue
                            Dim carga As DataTable = Consulta_Procedure(SSCommand4, SSCommand4.CommandText)

                            LblMensaje.ForeColor = Drawing.Color.Green
                            LblMensaje.Text = carga.Rows(1)(0).ToString + " Creditos Asignados " + carga.Rows(0)(0).ToString + " Creditos Inexistente(s)"

                        Catch ex As Exception
                            LblMensaje.ForeColor = Drawing.Color.Red
                            LblMensaje.Text = ex.Message
                        End Try
                        Kill(Ruta & nombre)
                    ElseIf DdltipoAsig.SelectedValue = "ArchivoC" Then
                        Dim tabla As DataTable
                        Try
                            tabla = FileToDataTableAC(Ruta & nombre)

                            Dim SSCommand2 As New SqlCommand("SP_CARGA_ASIGNACION_V3")
                            SSCommand2.CommandType = CommandType.StoredProcedure
                            SSCommand2.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = 1
                            Consulta_Procedure(SSCommand2, SSCommand2.CommandText)

                            Dim fakebulk As New SqlBulkCopy(Conectando())
                            'If DDLTipo.SelectedValue = 1 Then
                            fakebulk.DestinationTableName = "dbo.TMP_ASIGNACION"
                            fakebulk.ColumnMappings.Add("TMP_PR_MC_CREDITO", "TMP_PR_MC_CREDITO")
                            fakebulk.ColumnMappings.Add("TMP_PR_MC_USUARIO", "TMP_PR_MC_USUARIO")
                            'fakebulk.ColumnMappings.Add("TMP_PR_MC_INSTANCIA", "TMP_PR_MC_INSTANCIA")

                            fakebulk.WriteToServer(tabla)

                            Dim SSCommand3 As New SqlCommand("SP_CARGA_ASIGNACION_V3")
                            SSCommand3.CommandType = CommandType.StoredProcedure
                            SSCommand3.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = 5
                            Dim dtss As DataTable = Consulta_Procedure(SSCommand3, SSCommand3.CommandText)

                            Dim SSCommand4 As New SqlCommand("SP_CARGA_ASIGNACION_V3")
                            SSCommand4.CommandType = CommandType.StoredProcedure
                            SSCommand4.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = 10
                            Dim carga As DataTable = Consulta_Procedure(SSCommand4, SSCommand4.CommandText)

                            LblMensaje.ForeColor = Drawing.Color.Green
                            LblMensaje.Text = carga.Rows(1)(0).ToString + " Creditos Asignados <br/>" + carga.Rows(0)(0).ToString + " Creditos Inexistentes <br/>" + carga.Rows(2)(0).ToString + " Creditos con Usuario o Instancia Invalidos"

                        Catch ex As Exception
                            LblMensaje.ForeColor = Drawing.Color.Red
                            LblMensaje.Text = ex.Message
                        End Try
                        Kill(Ruta & nombre)

                    End If

                End If

            End If

            'End If

        Catch ex As Exception
            aviso(ex.Message)
            SendMail("BtnCargar_Click", ex, "", "", HidenUrs.Value)
        End Try
    End Sub


    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Administrador", "CargarAsignacion.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub

    'Protected Sub AsyncFileUpload1_UploadedComplete(ByVal sender As Object, ByVal e As AjaxControlToolkit.AsyncFileUploadEventArgs) Handles AsyncFileUpload1.UploadedComplete
    ' End Sub    
    Private Sub RadAsyncUpload1_FileUploaded(sender As Object, e As FileUploadedEventArgs) Handles RadAsyncUpload1.FileUploaded
        ' Try
        SubExisteRuta(Ruta)
        For Each s In System.IO.Directory.GetFiles(Ruta)
            System.IO.File.Delete(s)
        Next
        Dim nom As String = e.File.FileName
        nombre = nom
        Dim ruta_archivo As String = Ruta + nom
        e.File.SaveAs(ruta_archivo)
        'Catch ex As Exception
        'End Try
    End Sub

    Private Sub RAUVigencia_FileUploaded(sender As Object, e As FileUploadedEventArgs) Handles RAUVigencia.FileUploaded
        ' Try
        SubExisteRuta(Ruta + "Vigencia\")
        Dim nom As String = e.File.FileName
        If File.Exists(Ruta + "Vigencia\" + nom) Then
            Kill(Ruta + "Vigencia\" + nom)
        End If
        nombre = nom
        Dim ruta_archivo As String = Ruta + "Vigencia\" + nom
        e.File.SaveAs(ruta_archivo)
        'Catch ex As Exception
        'End Try
    End Sub
    Private Function FileToDataTableA(path As String) As DataTable
        Dim lines = IO.File.ReadAllLines(path)
        Dim tbl = New DataTable, nameCols As DataTable, tabla = New DataTable
        tabla.Columns.Add("TMP_PR_MC_CREDITO")
        tabla.Columns.Add("TMP_PR_MC_USUARIO")
        tabla.Columns.Add("TMP_PR_MC_INSTANCIA")
        Dim SSCommand2 As New SqlCommand("SP_CARGA_ASIGNACION_V3")
        SSCommand2.CommandType = CommandType.StoredProcedure
        SSCommand2.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = 0
        nameCols = Consulta_Procedure(SSCommand2, "SP_CARGA_ASIGNACION_V3")

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
                Dim Renglon As DataRow = tabla.NewRow()
                Renglon("TMP_PR_MC_CREDITO") = line
                Renglon("TMP_PR_MC_USUARIO") = DdlUsuarioA.SelectedValue
                Renglon("TMP_PR_MC_INSTANCIA") = DdlInstanciaA.SelectedValue
                tabla.Rows.Add(Renglon)
            End If
        Next

        Return tabla
    End Function
    Private Function FileToDataTableAC(path As String) As DataTable
        Dim lines = IO.File.ReadAllLines(path)
        Dim tbl = New DataTable, nameCols As DataTable

        Dim SSCommand2 As New SqlCommand("SP_CARGA_ASIGNACION_V3")
        SSCommand2.CommandType = CommandType.StoredProcedure
        SSCommand2.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = 4
        nameCols = Consulta_Procedure(SSCommand2, "SP_CARGA_ASIGNACION_V3")

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
    Protected Sub DdltipoAsig_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdltipoAsig.SelectedIndexChanged

        Limpia()

        If DdltipoAsig.DataTextField = "Seleccione" Then
            PnlArchivo.Visible = False
            PnlCredito.Visible = False
            PnlVigencia.Visible = False
            LblMensaje.Text = ""
        Else
            If DdltipoAsig.SelectedValue = "Credito" Then
                PnlArchivo.Visible = False
                PnlCredito.Visible = True
                PnlVigencia.Visible = False
                'DdlInstanciaC.SelectedItem.Text = "Seleccione"

                DdlUsuarioA.Visible = False
                LblUsuarioA.Visible = False
                BtnCargar.Visible = False
                DdlUsuarioC.Visible = False
                LblUsuarioC.Visible = False
                BtnAcptar.Visible = False
                LblMensaje.Text = ""
            Else
                LblMensaje.Text = ""
                PnlArchivo.Visible = True
                PnlCredito.Visible = False
                PnlVigencia.Visible = False
                'DdlInstanciaA.SelectedItem.Text = "Seleccione"
                DdlUsuarioA.Visible = False
                LblUsuarioA.Visible = False
                BtnCargar.Visible = False
                DdlUsuarioC.Visible = True
                LblUsuarioC.Visible = true
                BtnAcptar.Visible = False
                RLayoutI.Visible = False
                RLayoutID.Visible = False
                RLayoutU.Visible = False
                RLayoutUD.Visible = False
                DdlInstanciaA.Visible = True
                LblInstancia.Visible = True
                If DdltipoAsig.SelectedValue = "ArchivoC" Then
                    BtnCargar.Visible = True
                    DdlInstanciaA.Visible = False
                    LblInstancia.Visible = False
                    RLayoutI.Visible = True
                    RLayoutID.Visible = True
                    RLayoutU.Visible = True
                    RLayoutUD.Visible = True
                ElseIf DdltipoAsig.SelectedValue = "ArchivoV" Then
                    PnlVigencia.Visible = True
                    PnlArchivo.Visible = False
                    PnlCredito.Visible = False
                End If
            End If
        End If
    End Sub
    Protected Sub DdlInstanciaA_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlInstanciaA.SelectedIndexChanged
        Limpia()
        If DdlInstanciaA.SelectedItem.Text.ToString = "Seleccione" Then
            DdlUsuarioA.Visible = False
            LblUsuarioA.Visible = False
            BtnCargar.Visible = False
        Else
            LLENAR_DROP_C(36, " And CAT_LO_INSTANCIA = '" & DdlInstanciaA.SelectedValue & "' ", DdlUsuarioA, "V_VALOR", "T_VALOR")
            BtnCargar.Visible = True
            LblUsuarioA.Visible = True
        End If

    End Sub
    Protected Sub DdlInstanciaC_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlInstanciaC.SelectedIndexChanged
        Limpia()
        If DdlInstanciaC.SelectedItem.Text.ToString = "Seleccione" Then
            DdlUsuarioC.Visible = False
            LblUsuarioC.Visible = False
            BtnAcptar.Visible = False
        Else
            LLENAR_DROP_C(36, " and CAT_LO_INSTANCIA = '" & DdlInstanciaC.SelectedValue & "' ", DdlUsuarioC, "V_VALOR", "T_VALOR")
            LblUsuarioC.Visible = True
            BtnAcptar.Visible = True
        End If

    End Sub


    Public Shared Sub LLENAR_DROP_C(ByVal bandera As String, ByVal v_valor As String, ByVal ITEM As RadComboBox, ByVal DataValueField As String, ByVal DataTextField As String)
        Dim SSCommand2 As New SqlCommand("SP_CATALOGOS")
        SSCommand2.CommandType = CommandType.StoredProcedure
        SSCommand2.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = bandera
        SSCommand2.Parameters.Add("@V_Valor", SqlDbType.NVarChar).Value = v_valor
        Dim objDSa As DataTable = Consulta_Procedure(SSCommand2, "PROD")
        ITEM.Visible = True
        ITEM.ClearSelection()
        ITEM.Items.Clear()

        If objDSa.Rows.Count >= 1 Then
            ITEM.DataTextField = DataTextField
            ITEM.DataValueField = DataValueField
            ITEM.DataSource = objDSa
            ITEM.DataBind()
            ITEM.Items.Add("Seleccione")
            ITEM.SelectedValue = "Seleccione"
        Else
            ITEM.DataTextField = DataTextField
            ITEM.DataValueField = DataValueField
            ITEM.Items.Add("Seleccione")
            ITEM.SelectedValue = "Seleccione"
        End If

    End Sub

    Protected Sub BtnAcptar_Click(sender As Object, e As EventArgs) Handles BtnAcptar.Click
        Try
            If DdlInstanciaC.SelectedValue = "Seleccione" Then
                aviso("Seleccione una instancia")
            ElseIf TxtCredito.Text.Trim = "" Then
                aviso("Digite un número de crédito válido")
            Else
                Dim tabla As New DataTable
                Try
                    'tabla = FileToDataTableAC(Ruta & nombre)
                    tabla.Columns.Add("TMP_PR_MC_CREDITO")
                    tabla.Columns.Add("TMP_PR_MC_USUARIO")
                    Dim SSCommand2 As New SqlCommand("SP_CARGA_ASIGNACION_V3")
                    SSCommand2.CommandType = CommandType.StoredProcedure
                    SSCommand2.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = 1
                    Consulta_Procedure(SSCommand2, SSCommand2.CommandText)

                    Dim Renglon As DataRow = tabla.NewRow()
                    Renglon("TMP_PR_MC_CREDITO") = TxtCredito.Text
                    Renglon("TMP_PR_MC_USUARIO") = DdlUsuarioC.SelectedValue
                    tabla.Rows.Add(Renglon)
                    Dim fakebulk As New SqlBulkCopy(Conectando())
                    'If DDLTipo.SelectedValue = 1 Then
                    fakebulk.DestinationTableName = "dbo.TMP_ASIGNACION"
                    fakebulk.ColumnMappings.Add("TMP_PR_MC_CREDITO", "TMP_PR_MC_CREDITO")
                    fakebulk.ColumnMappings.Add("TMP_PR_MC_USUARIO", "TMP_PR_MC_USUARIO")
                    fakebulk.WriteToServer(tabla)

                    Dim SSCommand3 As New SqlCommand("SP_CARGA_ASIGNACION_V3")
                    SSCommand3.CommandType = CommandType.StoredProcedure
                    SSCommand3.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = 2
                    Dim dtss As DataTable = Consulta_Procedure(SSCommand3, SSCommand3.CommandText)

                    Dim SSCommand4 As New SqlCommand("SP_CARGA_ASIGNACION_V3")
                    SSCommand4.CommandType = CommandType.StoredProcedure
                    SSCommand4.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = 3
                    Dim carga As DataTable = Consulta_Procedure(SSCommand4, SSCommand4.CommandText)
                    If carga.Rows(1)(0).ToString = 1 Then
                        LblMensaje.ForeColor = Drawing.Color.Green
                        LblMensaje.Text = " Credito Asignado "
                    Else
                        LblMensaje.ForeColor = Drawing.Color.Red
                        LblMensaje.Text = " Credito Inexistente "
                    End If

                Catch ex As Exception
                    LblMensaje.ForeColor = Drawing.Color.Red
                    LblMensaje.Text = ex.Message
                End Try


            End If


            'End If
        Catch ex As Exception
            aviso(ex.Message)
        End Try

    End Sub

    Protected Sub Limpia()
        LblMensaje.Text = ""
        'DdlInstanciaA.ClearSelection()
        'DdlInstanciaC.ClearSelection()
        GvCargaAsignacion2.DataSource = Nothing
        GvCargaAsignacion2.DataBind()
        GvCargaAsignacion2.Visible = False
    End Sub

    Sub aviso(ByVal MSJ As String)
        RadAviso.RadAlert(MSJ.Replace("""", "").Replace("'", "").Replace(Chr(10), "").Replace(Chr(13), ""), 400, 150, "Aviso", Nothing)
    End Sub
    Protected Sub RBCargarVigencia_Click(sender As Object, e As EventArgs) Handles RBCargarVigencia.Click
        Dim script As String = "<script type=text/javascript>modal();</script>"

        Try
            If DdltipoAsig.SelectedValue = "Seleccione" Then
                aviso("Seleccione un tipo de asignación ")
                Limpia()
            Else
                Dim Directory As New IO.DirectoryInfo(Ruta + "Vigencia\")
                Dim allFiles As IO.FileInfo() = Directory.GetFiles.Where(Function(fi) fi.Extension = ".csv" OrElse fi.Extension = ".txt").ToArray
                If allFiles.Length = 0 Then
                    aviso("Archivo No Seleccionado o No Valido ")
                Else

                    Dim tabla As DataTable
                    Try
                        tabla = FileToDataTableVi(Ruta + "Vigencia\" + nombre)

                        Dim SSCommand2 As New SqlCommand("SP_CARGA_ASIGNACION_V3")
                        SSCommand2.CommandType = CommandType.StoredProcedure
                        SSCommand2.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = 7
                        Consulta_Procedure(SSCommand2, SSCommand2.CommandText)

                        Dim fakebulk As New SqlBulkCopy(Conectando())
                        'If DDLTipo.SelectedValue = 1 Then
                        fakebulk.DestinationTableName = "dbo.TMP_ASIGNACION_VIGENCIA"
                        fakebulk.ColumnMappings.Add("TMP_AV_CREDITO", "TMP_AV_CREDITO")
                        fakebulk.ColumnMappings.Add("TMP_AV_USUARIO", "TMP_AV_USUARIO")
                        fakebulk.ColumnMappings.Add("TMP_AV_VIGENCIA", "TMP_AV_VIGENCIA")

                        fakebulk.WriteToServer(tabla)

                        Dim SSCommand3 As New SqlCommand("SP_CARGA_ASIGNACION_V3")
                        SSCommand3.CommandType = CommandType.StoredProcedure
                        SSCommand3.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = 8
                        Dim dtss As DataTable = Consulta_Procedure(SSCommand3, SSCommand3.CommandText)

                        Dim SSCommand4 As New SqlCommand("SP_CARGA_ASIGNACION_V3")
                        SSCommand4.CommandType = CommandType.StoredProcedure
                        SSCommand4.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = 9
                        Dim carga As DataTable = Consulta_Procedure(SSCommand4, SSCommand4.CommandText)

                        LblMensaje.ForeColor = Drawing.Color.Green
                        LblMensaje.Text = carga.Rows(1)(0).ToString + " Creditos Asignados " + carga.Rows(0)(0).ToString + " Creditos Inexistentes " + carga.Rows(2)(0).ToString + " Creditos con Vigencia anteriores"

                    Catch ex As Exception
                        LblMensaje.ForeColor = Drawing.Color.Red
                        LblMensaje.Text = ex.Message
                    End Try
                    FileCopy(Ruta + "Vigencia\" + nombre, Ruta + "Vigencia\" + "Historico/" & DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") & "_" & nombre)
                    Kill(Ruta + "Vigencia\" + nombre)
                End If
            End If

        Catch ex As Exception
            aviso(ex.Message)
            SendMail("BtnCargar_Click", ex, "", "", HidenUrs.Value)
        End Try
    End Sub
    Private Function FileToDataTableVi(path As String) As DataTable
        Dim lines = IO.File.ReadAllLines(path)
        Dim tbl = New DataTable, nameCols As DataTable

        Dim SSCommand2 As New SqlCommand("SP_CARGA_ASIGNACION_V3")
        SSCommand2.CommandType = CommandType.StoredProcedure
        SSCommand2.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = 6
        nameCols = Consulta_Procedure(SSCommand2, "SP_CARGA_ASIGNACION_V3")

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
