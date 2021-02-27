Imports System.Data.SqlClient
Imports System.Data
Imports System.Diagnostics
Imports System.IO
Imports Db
Imports Funciones
Imports Telerik.Web.UI
Imports System.Net
Imports Newtonsoft.Json
Partial Class M_Administrador_CargaExclusiones
    Inherits System.Web.UI.Page
    Dim ERRORS As String = "1"
    Dim SKIP As String = "1"
    Dim ctlCarga As String
    Dim logCarga As String
    Dim badCarga As String
    Shared nombre As String
    Dim Ruta As String = StrRuta() & "Exclusiones\"
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
            AUDITORIA(HidenUrs.Value, "Administrador", "CargarExclusiones", " ", "Page_Load", "", Request.ServerVariables("REMOTE_ADDR"), Request.ServerVariables("REMOTE_HOST"))
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
        EnviarCorreo("Administrador", "CargarExclusiones.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub


    Private Sub RAUVigencia_FileUploaded(sender As Object, e As FileUploadedEventArgs) Handles RAUVigencia.FileUploaded
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
    'Private Function FileToDataTableA(path As String) As DataTable
    '    Dim lines = IO.File.ReadAllLines(path)
    '    Dim tbl = New DataTable, nameCols As DataTable, tabla = New DataTable
    '    tabla.Columns.Add("TMP_CE_CREDITO")
    '    tabla.Columns.Add("TMP_CE_CONVENIO")
    '    Dim SSCommand2 As New SqlCommand("SP_CARGA_EXCLUSIONES")
    '    SSCommand2.CommandType = CommandType.StoredProcedure
    '    SSCommand2.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = 0
    '    nameCols = Consulta_Procedure(SSCommand2, "SP_CARGA_ASIGNACION_V3")

    '    Dim encabezado = True
    '    Dim separador As Char = Chr(44) 'Coma

    '    Dim numLinea = 1, numCols = nameCols.Rows.Count
    '    For Each line In lines
    '        If encabezado Then
    '            Dim campos As String() = line.Split(separador)
    '            If campos.Length > numCols Then
    '                Throw New Exception("El encabezado es incorrecto. Tiene campos extra")
    '            ElseIf campos.Length < numCols Then
    '                Throw New Exception("El encabezado es incorecto. Le faltan campos")
    '            End If
    '            For Each name As DataRow In nameCols.Rows
    '                tbl.Columns.Add(New DataColumn(name(0).ToString, GetType(String)))
    '            Next
    '            encabezado = False
    '        Else
    '            Dim objFields = From field In line.Split(separador)
    '                            Select field
    '            numLinea += 1

    '            Dim objetos = objFields.ToArray()

    '            If objetos.Length > numCols Then
    '                Throw New Exception("La linea #" & numLinea & " tiene campos extra. Valide.")
    '            ElseIf objetos.Length < numCols Then
    '                Throw New Exception("La linea #" & numLinea & " tiene campos faltantes. Valide.")
    '            End If
    '            Dim newRow = tbl.Rows.Add()
    '            newRow.ItemArray = objetos
    '            Dim Renglon As DataRow = tabla.NewRow()
    '            Renglon("TMP_CE_CREDITO") = objetos(0)
    '            Renglon("TMP_CE_CONVENIO") = objetos(1)
    '            tabla.Rows.Add(Renglon)
    '        End If
    '    Next

    '    Return tabla
    'End Function
    'Private Function FileToDataTableAC(path As String) As DataTable
    '    Dim lines = IO.File.ReadAllLines(path)
    '    Dim tbl = New DataTable, nameCols As DataTable

    '    Dim SSCommand2 As New SqlCommand("SP_CARGA_ASIGNACION_V3")
    '    SSCommand2.CommandType = CommandType.StoredProcedure
    '    SSCommand2.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = 4
    '    nameCols = Consulta_Procedure(SSCommand2, "SP_CARGA_ASIGNACION_V3")

    '    Dim encabezado = True
    '    Dim separador As Char = Chr(44) 'Coma

    '    Dim numLinea = 1, numCols = nameCols.Rows.Count
    '    For Each line In lines
    '        If encabezado Then
    '            Dim campos As String() = line.Split(separador)
    '            If campos.Length > numCols Then
    '                Throw New Exception("El encabezado es incorrecto. Tiene campos extra")
    '            ElseIf campos.Length < numCols Then
    '                Throw New Exception("El encabezado es incorecto. Le faltan campos")
    '            End If
    '            For Each name As DataRow In nameCols.Rows
    '                tbl.Columns.Add(New DataColumn(name(0).ToString, GetType(String)))
    '            Next
    '            encabezado = False
    '        Else
    '            Dim objFields = From field In line.Split(separador)
    '                            Select field
    '            numLinea += 1

    '            Dim objetos = objFields.ToArray()

    '            If objetos.Length > numCols Then
    '                Throw New Exception("La linea #" & numLinea & " tiene campos extra. Valide.")
    '            ElseIf objetos.Length < numCols Then
    '                Throw New Exception("La linea #" & numLinea & " tiene campos faltantes. Valide.")
    '            End If
    '            Dim newRow = tbl.Rows.Add()
    '            newRow.ItemArray = objetos

    '        End If
    '    Next

    '    Return tbl
    'End Function
    Protected Sub DdltipoAsig_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdltipoAsig.SelectedIndexChanged

        Limpia()

        If DdltipoAsig.SelectedValue = "Seleccione" Then
            PnlVigencia.Visible = False
        Else
            PnlVigencia.Visible = True
        End If

        If DdltipoAsig.SelectedValue = "Dependencia" Then
            DepeC.Visible = True
            DepeD.Visible = True
            CrediC.Visible = False
            CrediD.Visible = False
            NoDomC.Visible = False
            NoDomD.Visible = False
        ElseIf DdltipoAsig.SelectedValue = "Credito" Then
            DepeC.Visible = False
            DepeD.Visible = False
            CrediC.Visible = True
            CrediD.Visible = True
            NoDomC.Visible = False
            NoDomD.Visible = False
        ElseIf DdltipoAsig.SelectedValue = "NoDomiciliar" Then
            DepeC.Visible = False
            DepeD.Visible = False
            CrediC.Visible = False
            CrediD.Visible = False
            NoDomC.Visible = True
            NoDomD.Visible = True
        Else
            DepeC.Visible = False
            DepeD.Visible = False
            CrediC.Visible = False
            CrediD.Visible = False
            NoDomC.Visible = False
            NoDomD.Visible = False
        End If

    End Sub
    'Protected Sub DdlInstanciaA_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlInstanciaA.SelectedIndexChanged
    '    Limpia()
    '    If DdlInstanciaA.SelectedItem.Text.ToString = "Seleccione" Then
    '        DdlUsuarioA.Visible = False
    '        LblUsuarioA.Visible = False
    '        BtnCargar.Visible = False
    '    Else
    '        LLENAR_DROP_C(36, " And CAT_LO_INSTANCIA = '" & DdlInstanciaA.SelectedValue & "' ", DdlUsuarioA, "V_VALOR", "T_VALOR")
    '        BtnCargar.Visible = True
    '        LblUsuarioA.Visible = True
    '    End If

    'End Sub
    'Protected Sub DdlInstanciaC_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlInstanciaC.SelectedIndexChanged
    '    Limpia()
    '    If DdlInstanciaC.SelectedItem.Text.ToString = "Seleccione" Then
    '        DdlUsuarioC.Visible = False
    '        LblUsuarioC.Visible = False
    '        BtnAcptar.Visible = False
    '    Else
    '        LLENAR_DROP_C(36, " and CAT_LO_INSTANCIA = '" & DdlInstanciaC.SelectedValue & "' ", DdlUsuarioC, "V_VALOR", "T_VALOR")
    '        LblUsuarioC.Visible = True
    '        BtnAcptar.Visible = True
    '    End If

    'End Sub


    'Public Shared Sub LLENAR_DROP_C(ByVal bandera As String, ByVal v_valor As String, ByVal ITEM As RadComboBox, ByVal DataValueField As String, ByVal DataTextField As String)
    '    Dim SSCommand2 As New SqlCommand("SP_CATALOGOS")
    '    SSCommand2.CommandType = CommandType.StoredProcedure
    '    SSCommand2.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = bandera
    '    SSCommand2.Parameters.Add("@V_Valor", SqlDbType.NVarChar).Value = v_valor
    '    Dim objDSa As DataTable = Consulta_Procedure(SSCommand2, "PROD")
    '    ITEM.Visible = True
    '    ITEM.ClearSelection()
    '    ITEM.Items.Clear()

    '    If objDSa.Rows.Count >= 1 Then
    '        ITEM.DataTextField = DataTextField
    '        ITEM.DataValueField = DataValueField
    '        ITEM.DataSource = objDSa
    '        ITEM.DataBind()
    '        ITEM.Items.Add("Seleccione")
    '        ITEM.SelectedValue = "Seleccione"
    '    Else
    '        ITEM.DataTextField = DataTextField
    '        ITEM.DataValueField = DataValueField
    '        ITEM.Items.Add("Seleccione")
    '        ITEM.SelectedValue = "Seleccione"
    '    End If

    'End Sub

    'Protected Sub BtnAcptar_Click(sender As Object, e As EventArgs) Handles BtnAcptar.Click
    '    Try
    '        If DdlInstanciaC.SelectedValue = "Seleccione" Then
    '            aviso("Seleccione una instancia")
    '        ElseIf TxtCredito.Text.Trim = "" Then
    '            aviso("Digite un número de crédito válido")
    '        Else
    '            Dim tabla As New DataTable
    '            Try
    '                'tabla = FileToDataTableAC(Ruta & nombre)
    '                tabla.Columns.Add("TMP_PR_MC_CREDITO")
    '                tabla.Columns.Add("TMP_PR_MC_USUARIO")
    '                Dim SSCommand2 As New SqlCommand("SP_CARGA_ASIGNACION_V3")
    '                SSCommand2.CommandType = CommandType.StoredProcedure
    '                SSCommand2.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = 1
    '                Consulta_Procedure(SSCommand2, SSCommand2.CommandText)

    '                Dim Renglon As DataRow = tabla.NewRow()
    '                Renglon("TMP_PR_MC_CREDITO") = TxtCredito.Text
    '                Renglon("TMP_PR_MC_USUARIO") = DdlUsuarioC.SelectedValue
    '                tabla.Rows.Add(Renglon)
    '                Dim fakebulk As New SqlBulkCopy(Conectando())
    '                'If DDLTipo.SelectedValue = 1 Then
    '                fakebulk.DestinationTableName = "dbo.TMP_ASIGNACION_CREDIFIEL"
    '                fakebulk.ColumnMappings.Add("TMP_PR_MC_CREDITO", "TMP_PR_MC_CREDITO")
    '                fakebulk.ColumnMappings.Add("TMP_PR_MC_USUARIO", "TMP_PR_MC_USUARIO")
    '                fakebulk.WriteToServer(tabla)

    '                Dim SSCommand3 As New SqlCommand("SP_CARGA_ASIGNACION_V3")
    '                SSCommand3.CommandType = CommandType.StoredProcedure
    '                SSCommand3.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = 2
    '                Dim dtss As DataTable = Consulta_Procedure(SSCommand3, SSCommand3.CommandText)

    '                Dim SSCommand4 As New SqlCommand("SP_CARGA_ASIGNACION_V3")
    '                SSCommand4.CommandType = CommandType.StoredProcedure
    '                SSCommand4.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = 3
    '                Dim carga As DataTable = Consulta_Procedure(SSCommand4, SSCommand4.CommandText)
    '                If carga.Rows(1)(0).ToString = 1 Then
    '                    LblMensaje.ForeColor = Drawing.Color.Green
    '                    LblMensaje.Text = " Credito Asignado "
    '                Else
    '                    LblMensaje.ForeColor = Drawing.Color.Red
    '                    LblMensaje.Text = " Credito Inexistente "
    '                End If

    '            Catch ex As Exception
    '                LblMensaje.ForeColor = Drawing.Color.Red
    '                LblMensaje.Text = ex.Message
    '            End Try


    '        End If
    '    Catch ex As Exception
    '        aviso(ex.Message)
    '    End Try

    'End Sub

    Protected Sub Limpia()
        LblMensaje.Text = ""
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
                Dim Directory As New IO.DirectoryInfo(Ruta)
                Dim allFiles As IO.FileInfo() = Directory.GetFiles.Where(Function(fi) fi.Extension = ".csv" OrElse fi.Extension = ".txt").ToArray
                If allFiles.Length = 0 Then
                    aviso("Archivo No Seleccionado o No Valido ")
                Else

                    Dim tabla As DataTable
                    Try
                        If DdltipoAsig.SelectedValue = "Credito" Then
                            tabla = FileToDataTableB(Ruta + nombre)
                        Else
                            tabla = FileToDataTableA(Ruta + nombre)
                        End If

                        Dim SSCommand2 As New SqlCommand("SP_CARGA_EXCLUSIONES")
                        SSCommand2.CommandType = CommandType.StoredProcedure
                        SSCommand2.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = 2
                        Consulta_Procedure(SSCommand2, SSCommand2.CommandText)

                        Dim fakebulk As New SqlBulkCopy(Conectando())
                        fakebulk.DestinationTableName = "dbo.TMP_CARGA_EXCLUSIONES"
                        If DdltipoAsig.SelectedValue = "Credito" Then
                            fakebulk.ColumnMappings.Add("TMP_CE_CREDITO", "TMP_CE_CREDITO")
                        ElseIf DdltipoAsig.SelectedValue = "Dependencia" Or DdltipoAsig.SelectedValue = "NoDomiciliar" Then
                            fakebulk.ColumnMappings.Add("TMP_CE_CREDITO", "TMP_CE_CREDITO")
                            fakebulk.ColumnMappings.Add("TMP_CE_CONVENIO", "TMP_CE_CONVENIO")
                        End If
                        fakebulk.WriteToServer(tabla)

                        Dim SSCommand3 As New SqlCommand("SP_CARGA_EXCLUSIONES")
                        SSCommand3.CommandType = CommandType.StoredProcedure
                        If DdltipoAsig.SelectedValue = "Dependencia" Or DdltipoAsig.SelectedValue = "NoDomiciliar" Then
                            SSCommand3.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = 3
                        ElseIf DdltipoAsig.SelectedValue = "Credito" Then
                            SSCommand3.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = 4
                        End If

                        Dim dtss As DataTable = Consulta_Procedure(SSCommand3, SSCommand3.CommandText)

                        Dim SSCommand4 As New SqlCommand("SP_CARGA_EXCLUSIONES")
                        SSCommand4.CommandType = CommandType.StoredProcedure
                        SSCommand4.Parameters.Add("@V_TIPO", SqlDbType.VarChar).Value = DdltipoAsig.SelectedValue

                        If (DdltipoAsig.SelectedValue = "Dependencia" Or DdltipoAsig.SelectedValue = "NoDomiciliar") And RCBAccion.SelectedValue = "1" Then
                            SSCommand4.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = 5
                        ElseIf DdltipoAsig.SelectedValue = "Credito" And RCBAccion.SelectedValue = "1" Then
                            SSCommand4.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = 6
                        ElseIf (DdltipoAsig.SelectedValue = "Dependencia" Or DdltipoAsig.SelectedValue = "NoDomiciliar") And RCBAccion.SelectedValue = "0" Then
                            SSCommand4.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = 7
                        ElseIf DdltipoAsig.SelectedValue = "Credito" And RCBAccion.SelectedValue = "0" Then
                            SSCommand4.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = 8
                        End If
                        Dim carga As DataTable = Consulta_Procedure(SSCommand4, SSCommand4.CommandText)

                        If carga.TableName = "Exception" Then
                            Throw New Exception(carga.Rows(0).Item(0).ToString)
                        End If

                        LblMensaje.ForeColor = Drawing.Color.Green
                        LblMensaje.Text = carga.Rows(0)(0).ToString + " Creditos Correctos " + carga.Rows(1)(0).ToString + " Convenios Correcto " + carga.Rows(2)(0).ToString + "Creditos/convenios incorrectos"

                    Catch ex As Exception
                        LblMensaje.ForeColor = Drawing.Color.Red
                        LblMensaje.Text = ex.Message
                    End Try
                    FileCopy(Ruta + nombre, Ruta + "Historico/" & DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") & "_" & nombre)
                    Kill(Ruta + nombre)
                End If
            End If

        Catch ex As Exception
            aviso(ex.Message)
            SendMail("BtnCargar_Click", ex, "", "", HidenUrs.Value)
        End Try
    End Sub
    Private Function FileToDataTableA(path As String) As DataTable
        Dim lines = IO.File.ReadAllLines(path)
        Dim tbl = New DataTable, nameCols As DataTable

        Dim SSCommand2 As New SqlCommand("SP_CARGA_EXCLUSIONES")
        SSCommand2.CommandType = CommandType.StoredProcedure
        SSCommand2.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = 0
        nameCols = Consulta_Procedure(SSCommand2, "SP_CARGA_EXCLUSIONES")

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
    Private Function FileToDataTableB(path As String) As DataTable
        Dim lines = IO.File.ReadAllLines(path)
        Dim tbl = New DataTable, nameCols As DataTable

        Dim SSCommand2 As New SqlCommand("SP_CARGA_EXCLUSIONES")
        SSCommand2.CommandType = CommandType.StoredProcedure
        SSCommand2.Parameters.Add("@V_BANDERA", SqlDbType.Int).Value = 1
        nameCols = Consulta_Procedure(SSCommand2, "SP_CARGA_EXCLUSIONES")

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
