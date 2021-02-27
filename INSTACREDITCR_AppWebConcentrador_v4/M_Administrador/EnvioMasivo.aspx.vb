Imports System.Data.SqlClient
Imports System.Data
Imports System.Diagnostics
Imports System.IO
Imports Db
Imports QParametros
Imports Quiubas
Imports Funciones
Imports QRegreso
Partial Class EnvioMasivo
    Inherits System.Web.UI.Page
    Dim delim As String
    Dim ERRORS As String = "100"
    Dim TABLA As String = "TMP_Masivo_SMS"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim Usr As String = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
        Catch ex As Exception
            OffLine(HidenUrs.Value)
            AUDITORIA(HidenUrs.Value, "Administrador", "Catalogo Etapas", "", "Page_Load", "", Request.ServerVariables("REMOTE_ADDR"), Request.ServerVariables("REMOTE_HOST"))
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/SesionExpirada.aspx")
        End Try
        Try
            If Not IsPostBack Then
                HidenUrs.Value = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
                If CType(Session("USUARIOADMIN"), USUARIO).CAT_LO_MADMINISTRADOR.ToString.Substring(22, 1) = 0 Then
                    OffLine(HidenUrs.Value)
                    Session.Clear()
                    Session.Abandon()
                    Response.Redirect("~/SesionExpirada.aspx")
                End If
                Dim USR As String = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
                HidenUrs.Value = USR
                'Dim Cadena As String = Ejecuta_SMS(SALDOSMS())
                'If QCampo(Cadena, "response", "action") = "error" Then
                '    BtnPre.Visible = False
                '    LblSaldo.Text = QCampo(Cadena, "data", "errormessage")
                'Else
                '    If QCampo(Cadena, "account", "balance") = "0" Then
                '        LlenarDatos(Session("Archivo"), "", "Validacion De Saldo", "0 Mensajes", "", "", "", "", "", 3)
                '        BtnPre.Visible = False
                '        PnlLayout.Visible = False
                '        LblSaldo.Text = "Saldo Insuficiente"
                '    Else
                BtnPre.Visible = True

                '        LblSaldo.Text = "Usted Puede Enviar " & QCampo(Cadena, "account", "balance") & " Mensaje(s)"
                '        LlenarDatos(Session("Archivo"), "", "Validacion De Saldo", QCampo(Cadena, "account", "balance"), "", "", "", "", "", 3)
                '    End If
                'End If
            End If
        Catch ex As Exception
            SendMail("Page_Load", ex, "", "", HidenUrs.Value)
        End Try
    End Sub


    Protected Sub BtnCargar_Click(sender As Object, e As System.EventArgs) Handles BtnCargar.Click
        Try
            Dim USR As String = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
            HidenUrs.Value = USR
        Catch ex As Exception
            OffLine(HidenUrs.Value)
            Session.Clear()
            Session.Abandon()
            Response.Redirect("Login.aspx")
        End Try
        Cargar()
    End Sub
    Public Sub ctl_Carga()

        Dim tmp_archivo As String = StrRuta() & "SMS\" & Path.GetFileName(RadAsyncUpload1.UploadedFiles(0).FileName)

        Session("Archivo") = RadAsyncUpload1.UploadedFiles(0).FileName
        If File.Exists(tmp_archivo) Then
            Kill(tmp_archivo)
        End If
        RadAsyncUpload1.UploadedFiles(0).SaveAs(tmp_archivo)
        Dim ctlCarga As String = StrRuta() & "SMS\" & "CTL_SMS" & TABLA & "_" & Now.ToShortDateString.Replace("/", "") & "" & ".ctl"
        Dim logCarga As String = StrRuta() & "SMS\" & "LOG_SMS" & TABLA & "_" & Now.ToShortDateString.Replace("/", "") & "" & ".log"
        Dim badCarga As String = StrRuta() & "SMS\" & "BAD_SMS" & TABLA & "_" & Now.ToShortDateString.Replace("/", "") & "" & ".bad"

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
        sw.WriteLine("infile '" & tmp_archivo & "'")
        sw.WriteLine("into table " & TABLA & "")
        sw.WriteLine("FIELDS TERMINATED BY '" & delim & "' ")
        sw.WriteLine("TRAILING NULLCOLS")
        sw.WriteLine("(")
        sw.WriteLine("TMP_SM_CUENTA CHAR(25),")
        sw.WriteLine("TMP_SM_TELEFONO CHAR(25),")
        sw.WriteLine("TMP_SM_PLANTILLA CHAR(100)")
        sw.WriteLine(")")
        sw.Close()
        fs.Close()
        Ejecuta("truncate table " & TABLA & "")
        Dim comando As Process = New Process
        comando.StartInfo.FileName = "sqlldr"
        'comando.StartInfo.Arguments = DesEncriptarCadena(Conexiones.StrConexion(1)) & "/" & DesEncriptarCadena(Conexiones.StrConexion(2)) & "@" & DesEncriptarCadena(Conexiones.StrConexion(3)) & " control=" & ctlCarga & ", bad=" & badCarga &
        comando.StartInfo.Arguments = (StrConexion(1)) & "/" & (StrConexion(2)) & "@" & (StrConexion(3)) & " control=" & ctlCarga & ", bad=" & badCarga &
        ",log=" & logCarga & " errors=" & ERRORS & ",DISCARDMAX=0,ROWS=" & ERRORS & " direct=y, SKIP =1"
        comando.Start()
        comando.WaitForExit()
    End Sub

    Protected Sub Cargar()
        Try
            Dim USR As String = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
            HidenUrs.Value = USR
        Catch ex As Exception
            OffLine(HidenUrs.Value)
            Session.Clear()
            Session.Abandon()
            Response.Redirect("Login.aspx")
        End Try

        Try
            Dim Tmpusr As String = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
            Dim SSCommandAgencias As New SqlCommand
            SSCommandAgencias.CommandText = "SP_ENVIO_MASIVO_SMS"
            SSCommandAgencias.CommandType = CommandType.StoredProcedure
            Dim DtsVarios As DataTable = Consulta_Procedure(SSCommandAgencias, "Enviar")
            Dim DtvVarios As DataView = DtsVarios.DefaultView
            If DtvVarios.Count > 0 Then
                For X As Integer = 0 To DtvVarios.Count - 1

                    'esta comentado'Dim Cadena As String = Ejecuta_SMS(EnviarSMS(DtsVarios.Rows(X).Item("TELEFONO"), DtsVarios.Rows(X).Item("MENSAJE")))

                    Dim Cadena As String = EnviarSMS_Donde(DtsVarios.Rows(X).Item("TELEFONO"), DtsVarios.Rows(X).Item("MENSAJE"))

                    If QCampo(Cadena, "mensajeResponse", "estadoMensaje") = "0" Then
                        LlenarDatos(Session("Archivo"), DtsVarios.Rows(X).Item("TELEFONO"), DtsVarios.Rows(X).Item("MENSAJE"), QCampo(Cadena, "mensajeResponse", "estadoMensaje"), Tmpusr, "Masivo", DtsVarios.Rows(X).Item("CREDITO"), DtsVarios.Rows(X).Item("PLANTILLA"), "", 3)
                    Else
                        LlenarDatos(Session("Archivo"), DtsVarios.Rows(X).Item("TELEFONO"), DtsVarios.Rows(X).Item("MENSAJE"), QCampo(Cadena, "mensajeResponse", "estadoMensaje"), Tmpusr, "Masivo", DtsVarios.Rows(X).Item("CREDITO"), DtsVarios.Rows(X).Item("PLANTILLA"), "", 3)
                    End If
                    ' se agrego por maap
                    Call LLenadatoshist(DtsVarios.Rows(X).Item("CREDITO"), (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO, DtsVarios.Rows(X).Item("MENSAJE"), DtsVarios.Rows(X).Item("TELEFONO"))
                Next
            End If
            aviso("MENSAJES ENVIADOS"
            )
        Catch ex As Exception
            Label1.Text = ex.Message
            SendMail("Cargar", ex, "", "", HidenUrs.Value)
        End Try
    End Sub
    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Administrador", "EnviosMasivos.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub



    Protected Sub BtnPre_Click(sender As Object, e As EventArgs) Handles BtnPre.Click
        Try
            Dim USR As String = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
            HidenUrs.Value = USR
        Catch ex As Exception
            OffLine(HidenUrs.Value)
            Session.Clear()
            Session.Abandon()
            Response.Redirect("Login.aspx")
        End Try

        If Len(RadAsyncUpload1.UploadedFiles(0).FileName) < 5 Then
            aviso("Debe elegir una archivo para visualizar"
            )
        ElseIf DdlDelimitador.SelectedValue = "Seleccione" Then
            aviso("Seleccione un Delimitador"
            )
        Else
            If RadAsyncUpload1.UploadedFiles(0).ContentType = "text/plain" Or RadAsyncUpload1.UploadedFiles(0).ContentType = "application/vnd.ms-excel" Then
                delim = DdlDelimitador.SelectedValue
                SubExisteRuta(StrRuta() & "\SMS\")
                ctl_Carga()
                Dim DtsPreview As DataTable = LlenarDatos("", "", "", "", "", "", "", "", "", 4)
                Grid_RESULTADO.DataSource = DtsPreview
                Grid_RESULTADO.DataBind()
                BtnCargar.Visible = True
            Else
                aviso("Solo puede cargar archivos con extensión txt o csv."
                )
            End If
        End If
    End Sub
    Public Sub LLenadatoshist(ByVal Credito As String, ByVal USUARIO As String, ByVal Comentario As String, ByVal Telefono As String)

        Dim SSCommand2 As New SqlCommand
        SSCommand2.CommandText = "SP_HIST_GEST_MAIL_SMS"
        SSCommand2.CommandType = CommandType.StoredProcedure
        SSCommand2.Parameters.Add("@V_Hist_Ge_Credito", SqlDbType.NVarChar).Value = Credito '1
        SSCommand2.Parameters.Add("@V_Hist_Ge_Usuario", SqlDbType.NVarChar).Value = USUARIO '2
        SSCommand2.Parameters.Add("@V_Codaccion", SqlDbType.NVarChar).Value = "MT" '3
        SSCommand2.Parameters.Add("@V_Hist_Ge_Resultado", SqlDbType.NVarChar).Value = "MEDIO ALTERNO" '4
        SSCommand2.Parameters.Add("@V_Codresult", SqlDbType.NVarChar).Value = "MT" '5
        SSCommand2.Parameters.Add("@V_HIST_GE_INOUTBOUND", SqlDbType.NVarChar).Value = "1" '6
        SSCommand2.Parameters.Add("@V_Hist_Ge_Comentario", SqlDbType.NVarChar).Value = Comentario '7
        SSCommand2.Parameters.Add("@V_Hist_Ge_Telefono", SqlDbType.NVarChar).Value = Telefono '8
        Dim DtsGestion As DataTable = Consulta_Procedure(SSCommand2, "Gestion")

    End Sub

    Function LlenarDatos(ByVal V_Valor1 As String, ByVal V_Valor2 As String, ByVal V_Valor3 As String, ByVal V_Valor4 As String, ByVal V_Valor5 As String, ByVal V_Valor6 As String, ByVal V_Valor7 As String, ByVal V_Valor8 As String, ByVal V_Valor9 As String, ByVal V_Bandera As String) As DataTable
        Dim SSCommandSms As New SqlCommand
        SSCommandSms.CommandText = "SP_VARIOS_SMS"
        SSCommandSms.CommandType = CommandType.StoredProcedure
        SSCommandSms.Parameters.Add("@V_Valor1", SqlDbType.NVarChar).Value = V_Valor1
        SSCommandSms.Parameters.Add("@V_Valor2", SqlDbType.NVarChar).Value = V_Valor2
        SSCommandSms.Parameters.Add("@V_Valor3", SqlDbType.NVarChar).Value = V_Valor3
        SSCommandSms.Parameters.Add("@V_Valor4", SqlDbType.NVarChar).Value = V_Valor4
        SSCommandSms.Parameters.Add("@V_Valor5", SqlDbType.NVarChar).Value = V_Valor5
        SSCommandSms.Parameters.Add("@V_Valor6", SqlDbType.NVarChar).Value = V_Valor6
        SSCommandSms.Parameters.Add("@V_Valor7", SqlDbType.NVarChar).Value = V_Valor7
        SSCommandSms.Parameters.Add("@V_Valor8", SqlDbType.NVarChar).Value = V_Valor8
        SSCommandSms.Parameters.Add("@V_Valor9", SqlDbType.NVarChar).Value = V_Valor9
        SSCommandSms.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = V_Bandera
        Dim DtsVarios As DataTable = Consulta_Procedure(SSCommandSms, "SMS")
        Return DtsVarios
    End Function
    Protected Sub aviso(ByVal MSJ As String)
        RadAviso.RadAlert(MSJ, 400, 150, "Aviso", Nothing)
    End Sub
End Class
