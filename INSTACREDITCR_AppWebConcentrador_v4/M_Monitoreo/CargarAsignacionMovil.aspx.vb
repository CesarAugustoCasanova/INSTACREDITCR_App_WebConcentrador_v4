Imports System.Data.SqlClient
Imports System.Data
Imports System.Diagnostics
Imports System.IO
Imports Db
Imports Funciones
Imports Telerik.Web.UI
Imports System.Net
Imports Newtonsoft.Json

Partial Class M_Monitoreo_CargarAsignacionMovil
    Inherits System.Web.UI.Page

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



    Dim Ruta As String = StrRuta() & "Asignacion_M\"
    Dim d As EventArgs = EventArgs.Empty

    Protected Sub BtnCargar_Click(sender As Object, e As EventArgs) Handles BtnCargar.Click
        Dim script As String = "<script type=text/javascript>modal();</script>"
        If DdltipoAsig.SelectedValue = "Seleccione" Then
            aviso("Seleccione un tipo de asignación ")
            Limpia()
        Else
            Dim Directory As New IO.DirectoryInfo(Ruta)
            Dim allFiles As IO.FileInfo() = Directory.GetFiles.Where(Function(fi) fi.Extension.ToLower = ".csv" OrElse fi.Extension.ToLower = ".txt").ToArray
            If allFiles.Length = 0 Then
                aviso("Archivo No Seleccionado o No Valido ")
            Else
                Dim singleFile As IO.FileInfo
                Dim value As Long
                Dim FileCarga As String = ""
                For Each singleFile In allFiles
                    value = singleFile.Length
                    FileCarga = singleFile.Name
                    If value = 0 Then
                        My.Computer.FileSystem.DeleteFile(Ruta & FileCarga, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently, FileIO.UICancelOption.DoNothing)
                    Else

                        Try
                            Dim dt = FileToDataTable(Ruta & FileCarga)

                            GvCargaAsignacion2.DataSource = UploadToPR(dt)
                            GvCargaAsignacion2.DataBind()
                            GvCargaAsignacion2.Visible = True

                            LblMensaje.ForeColor = Drawing.Color.Green
                            LblMensaje.Text = "Archivo Cargado Correctamente"
                        Catch ex As Exception
                            LblMensaje.ForeColor = Drawing.Color.Red
                            LblMensaje.Text = ex.Message
                        Finally
                            SubExisteRuta(Ruta & "Historico")
                            FileCopy(Ruta & FileCarga, Ruta & "Historico/" & singleFile.Name.ToString.Substring(0, Len(singleFile.Name.ToString) - 4) & "_" & DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") & singleFile.Extension)
                            Kill(Ruta & FileCarga)
                        End Try
                    End If
                Next
            End If

        End If
    End Sub
    Private Function UploadToPR(ByRef dt As DataTable) As DataTable
        Dim res As DataTable = SP.CARGA_ASIGNACION_MOVIL(1, tmpUSUARIO("CAT_LO_USUARIO"), dt)
        If res.TableName = "Exception" Then
            Throw New Exception("Error: " & res(0)(0).ToString)
        End If
        Return res
    End Function


    Private Function FileToDataTable(path As String) As DataTable
        Dim lines = IO.File.ReadAllLines(path)
        Dim tbl = New DataTable, nameCols = SP.CARGA_ASIGNACION_MOVIL(0, Nothing, Nothing)
        Dim encabezado = True
        Dim separador As Char = Chr(44) 'Coma

        'Select Case DDLSeparador.SelectedValue
        '    Case 0
        '        separador = Chr(9) 'Tabulador
        '    Case 1
        '        separador = Chr(44) 'Coma
        'End Select


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
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        'Try
        '    If tmpPermisos("INGRESO") = False Then
        '        Session.Clear()
        '        Session.Abandon()
        '        Response.Redirect("~/SesionExpirada.aspx")
        '    End If
        'Catch ex As Exception
        '    Session.Clear()
        '    Session.Abandon()
        '    Response.Redirect("~/SesionExpirada.aspx")
        'End Try
        Try
            If Not IsPostBack Then
                'HidenUrs.Value = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
                'If CType(Session("USUARIOADMIN"), USUARIO).CAT_LO_MADMINISTRADOR.ToString.Substring(17, 1) = 0 Then
                '    OffLine(HidenUrs.Value)
                '    Session.Clear()
                '    Session.Abandon()
                '    'Response.Redirect("~/SesionExpirada.aspx")
                'End If

                'LLENAR_DROP_C(35, "", DdlInstanciaC, "V_VALOR", "T_VALOR")
            End If
        Catch ex As Exception
            SendMail("Page_Load", ex, "", "", HidenUrs.Value)
        End Try
    End Sub

    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Monitoreo", "CargarAsignacionMovilaspx", evento, ex, Cuenta, Captura, usr)
    End Sub

    'Protected Sub AsyncFileUpload1_UploadedComplete(ByVal sender As Object, ByVal e As AjaxControlToolkit.AsyncFileUploadEventArgs) Handles AsyncFileUpload1.UploadedComplete
    ' End Sub    
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
        WinMsj.RadAlert(MSJ, 400, 150, "Aviso", Nothing)
    End Sub
    Protected Sub DdltipoAsig_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdltipoAsig.SelectedIndexChanged

        Limpia()

        If DdltipoAsig.DataTextField = "Seleccione" Then
            'PnlArchivo.Visible = False
            'PnlCredito.Visible = False
        Else
            If DdltipoAsig.SelectedValue = "Crédito" Then
                'PnlArchivo.Visible = False
                'PnlCredito.Visible = True
                BtnCargar.Enabled = False
                'DdlUsuarioC.Visible = False
                'LblUsuarioC.Visible = False
                'BtnAcptar.Visible = False
            Else
                'PnlArchivo.Visible = True
                BtnCargar.Enabled = True
                'PnlCredito.Visible = False
                'DdlUsuarioC.Visible = False
                'LblUsuarioC.Visible = False
                'BtnAcptar.Visible = False
            End If
        End If
    End Sub

    'Protected Sub DdlInstanciaC_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlInstanciaC.SelectedIndexChanged
    '    Limpia()
    '    'If DdlInstanciaC.SelectedItem.Text.ToString = "Seleccione" Then
    '    '    DdlUsuarioC.Visible = False
    '    '    LblUsuarioC.Visible = False
    '    '    BtnAcptar.Visible = False
    '    'Else
    '    '    LLENAR_DROP_C(36, " and CAT_LO_INSTANCIA = '" & DdlInstanciaC.SelectedValue & "' ", DdlUsuarioC, "V_VALOR", "T_VALOR")
    '    '    LblUsuarioC.Visible = True
    '    '    BtnAcptar.Visible = True
    '    'End If

    'End Sub


    Public Shared Sub LLENAR_DROP_C(ByVal bandera As String, ByVal v_valor As String, ByVal ITEM As RadComboBox, ByVal DataValueField As String, ByVal DataTextField As String)
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_CATALOGOS"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("V_BANDERA", SqlDbType.Decimal).Value = bandera
        SSCommand.Parameters.Add("V_Valor", SqlDbType.NVarChar).Value = v_valor
        Dim objDSa As DataTable = Consulta_Procedure(SSCommand, "PROD")
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

    'Protected Sub BtnAcptar_Click(sender As Object, e As EventArgs) Handles BtnAcptar.Click
    '    If DdlInstanciaC.SelectedValue = "Seleccione" Then
    '        aviso("Seleccione una instancia")
    '    ElseIf TextBox1.Text.Trim = "" Then
    '        aviso("Digite un número de crédito válido")
    '    Else

    '        Dim TABLA As String = "TMP_ASIGNACION_" & tmpUSUARIO("CAT_LO_AGENCIA")

    '        Dim bandera As Integer = 0
    '        If tmpUSUARIO("CAT_LO_AGENCIA") <> "MASTER" Then
    '            bandera = 1
    '        End If

    '        Dim SSCommandT As New SqlCommand
    '        SSCommandT.CommandText = "SP_VALIDA_TABLA"
    '        SSCommandT.CommandType = CommandType.StoredProcedure
    '        SSCommandT.Parameters.Add("V_TABLA", SqlDbType.NVarChar).Value = TABLA
    '        SSCommandT.Parameters.Add("V_Bandera", SqlDbType.Decimal).Value = 1
    '        SSCommandT.Parameters.Add("V_Principal", SqlDbType.Decimal).Value = bandera
    '        Ejecuta_Procedure(SSCommandT)

    '        Dim SSCommand As New SqlCommand
    '        SSCommand.CommandText = "SP_ASIGNACION_M"
    '        SSCommand.CommandType = CommandType.StoredProcedure
    '        SSCommand.Parameters.Add("V_AGENCIA", SqlDbType.NVarChar).Value = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_AGENCIA
    '        SSCommand.Parameters.Add("V_USUARIO", SqlDbType.NVarChar).Value = DdlUsuarioC.SelectedValue
    '        SSCommand.Parameters.Add("V_TIPO", SqlDbType.NVarChar).Value = DdltipoAsig.SelectedValue
    '        SSCommand.Parameters.Add("V_VALOR", SqlDbType.NVarChar).Value = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_CADENAAGENCIAS
    '        SSCommand.Parameters.Add("V_VALOR2", SqlDbType.NVarChar).Value = TextBox1.Text.Trim
    '        SSCommand.Parameters.Add("V_USUARIO_CARGA", SqlDbType.NVarChar).Value = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
    '        Dim DtsCarga As DataTable = Consulta_Procedure(SSCommand, "SP_CARGAR_ASIGNACION")
    '        GvCargaAsignacion2.DataSource = DtsCarga
    '        GvCargaAsignacion2.DataBind()
    '        GvCargaAsignacion2.Visible = True
    '        LblMensaje.Text = "Asignación Completada"

    '    End If

    'End Sub

    Protected Sub Limpia()
        LblMensaje.Text = ""
        'DdlInstanciaA.ClearSelection()
        'DdlInstanciaC.ClearSelection()
        GvCargaAsignacion2.DataSource = Nothing
        GvCargaAsignacion2.DataBind()
        GvCargaAsignacion2.Visible = False
    End Sub


End Class