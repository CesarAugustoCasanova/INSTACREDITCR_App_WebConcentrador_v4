Imports System.Data.SqlClient
Imports System.Data
Imports System.Diagnostics
Imports System.IO
Imports Db
Imports Funciones
Imports Telerik.Web.UI
Imports System.Net
Imports Newtonsoft.Json

Partial Class MAdministrador_CargarAsignacionMovil
    Inherits System.Web.UI.Page
    Dim ERRORS As String = "1"
    Dim SKIP As String = "1"
    Dim ctlCarga As String
    Dim logCarga As String
    Dim badCarga As String
    Dim Ruta As String = StrRuta() & "Asignacion_M\"
    Dim d As EventArgs = EventArgs.Empty

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
                            Dim TABLA As String = "TMP_ASIGNACION_M_" & (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_AGENCIA
                            ctlCarga = Ruta & "CTL_ASIG_M_" & TABLA & "_" & Now.ToShortDateString.Replace("/", "") & "" & ".ctl"
                            logCarga = Ruta & "LOG_ASIG_M_" & TABLA & "_" & Now.ToShortDateString.Replace("/", "") & "" & ".log"
                            badCarga = "C:\Users\ccasanova\Documents\clientes\TUIIO\errorescargas.csv"

                            If File.Exists(ctlCarga) Then
                                Kill(ctlCarga)
                            End If
                            If File.Exists(logCarga) Then
                                Kill(logCarga)
                            End If
                            If File.Exists(badCarga) Then
                                Kill(badCarga)
                            End If

                            Dim bandera As Integer = 0
                            If (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_AGENCIA <> "MASTER" Then
                                bandera = 1
                            End If

                            Dim SSCommandT As New SqlCommand("SP_VALIDA_TABLA")
                            SSCommandT.CommandType = CommandType.StoredProcedure
                            SSCommandT.Parameters.Add("@V_TABLA", SqlDbType.NVarChar).Value = FileCarga
                            SSCommandT.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 4
                            SSCommandT.Parameters.Add("@V_Principal", SqlDbType.NVarChar).Value = 0
                            Ejecuta_Procedure(SSCommandT)


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
                            sw.WriteLine("TMP_ID,")
                            sw.WriteLine("TMP_UASIGNADO")
                            sw.WriteLine(")")
                            sw.Close()
                            fs.Close()

                            Dim SSCommand As New SqlCommand("SP_CARGA_TMP_ACTSALDOS")
                            SSCommand.CommandType = CommandType.StoredProcedure
                            SSCommand.Parameters.Add("@V_ARCHIVO", SqlDbType.NVarChar).Value = FileCarga
                            SSCommand.Parameters.Add("@V_ERROR", SqlDbType.NVarChar).Value = badCarga
                            SSCommand.Parameters.Add("@V_TABLA", SqlDbType.NVarChar).Value = TABLA
                            Ejecuta_Procedure(SSCommand)
                            Dim num As Integer = 0

                            If (File.Exists(badCarga)) Then
                                Dim fic As New IO.StreamReader(badCarga, System.Text.Encoding.UTF8)
                                Dim linea As String = fic.ReadLine
                                fic.Close()
                                LblMensaje.Text = "Error en la cadena siguiente " & linea
                            End If

                            LblMensaje.Text = ""
                            Dim SSCommand2 As New SqlCommand("SP_ASIGNACION_M")
                            SSCommand2.CommandType = CommandType.StoredProcedure
                            SSCommand2.Parameters.Add("@V_AGENCIA", SqlDbType.NVarChar).Value = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_AGENCIA
                            SSCommand2.Parameters.Add("@V_USUARIO_A", SqlDbType.NVarChar).Value = ""
                            SSCommand2.Parameters.Add("@V_USUARIO_DE", SqlDbType.NVarChar).Value = ""
                            SSCommand2.Parameters.Add("@V_TIPO", SqlDbType.NVarChar).Value = DdltipoAsig.SelectedValue
                            SSCommand2.Parameters.Add("@V_VALOR", SqlDbType.NVarChar).Value = ""
                            SSCommand2.Parameters.Add("@V_USUARIO_CARGA", SqlDbType.NVarChar).Value = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
                            SSCommand2.Parameters.Add("@v_bandera", SqlDbType.NVarChar).Value = 0
                            Dim DtsCarga As DataTable = Consulta_Procedure(SSCommand2, "SP_CARGAR_ASIGNACION_M")
                            GvCargaAsignacion2.DataSource = DtsCarga
                            GvCargaAsignacion2.DataBind()
                            GvCargaAsignacion2.Visible = True
                            LblMensaje.Text = "Carga Completada"
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
                    Next
                End If

            End If

        Catch ex As Exception
            aviso(ex.Message)
        End Try
        'Catch ex As Exception
        '    SendMail("BtnCargar_Click", ex, "", "", HidenUrs.Value)
        'End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try
            Dim Usr As String = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
        Catch ex As Exception
            OffLine(HidenUrs.Value)
            AUDITORIA(HidenUrs.Value, "Administrador", "CargarAsignacion", " ", "Page_Load", "", Request.ServerVariables("REMOTE_ADDR"), Request.ServerVariables("REMOTE_HOST"))
            Session.Clear()
            Session.Abandon()
            'Response.Redirect("~/SesionExpirada.aspx")
        End Try
        Try
            If Not IsPostBack Then
                HidenUrs.Value = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
                If CType(Session("USUARIOADMIN"), USUARIO).CAT_LO_MADMINISTRADOR.ToString.Substring(17, 1) = 0 Then
                    OffLine(HidenUrs.Value)
                    Session.Clear()
                    Session.Abandon()
                    'Response.Redirect("~/SesionExpirada.aspx")
                End If
                LLENAR_DROP_C(35, "", DdlInstanciaC, "V_VALOR", "T_VALOR")
            End If
        Catch ex As Exception
            SendMail("Page_Load", ex, "", "", HidenUrs.Value)
        End Try
    End Sub

    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Administrador", "CargarAsignacionMovilaspx", evento, ex, Cuenta, Captura, usr)
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
            PnlArchivo.Visible = False
            PnlCredito.Visible = False
        Else
            If DdltipoAsig.SelectedValue = "Crédito" Then
                PnlArchivo.Visible = False
                PnlCredito.Visible = True
                BtnCargar.Visible = False
                DdlUsuarioC.Visible = False
                LblUsuarioC.Visible = False
                BtnAcptar.Visible = False
            Else
                PnlArchivo.Visible = True
                BtnCargar.Visible = True
                PnlCredito.Visible = False
                DdlUsuarioC.Visible = False
                LblUsuarioC.Visible = False
                BtnAcptar.Visible = False
            End If
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
        Dim SSCommand As New SqlCommand("SP_CATALOGOS")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.NVarChar).Value = bandera
        SSCommand.Parameters.Add("@V_Valor", SqlDbType.NVarChar).Value = v_valor
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

    Protected Sub BtnAcptar_Click(sender As Object, e As EventArgs) Handles BtnAcptar.Click
        If DdlInstanciaC.SelectedValue = "Seleccione" Then
            aviso("Seleccione una instancia")
        ElseIf TextBox1.Text.Trim = "" Then
            aviso("Digite un número de crédito válido")
        Else

            Dim TABLA As String = "TMP_ASIGNACION_" & (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_AGENCIA

            Dim bandera As Integer = 0
            If (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_AGENCIA <> "MASTER" Then
                bandera = 1
            End If


            Dim SSCommandT As New SqlCommand("SP_VALIDA_TABLA")
            SSCommandT.CommandType = CommandType.StoredProcedure
            SSCommandT.Parameters.Add("@V_TABLA", SqlDbType.NVarChar).Value = TABLA
            SSCommandT.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 1
            SSCommandT.Parameters.Add("@V_Principal", SqlDbType.NVarChar).Value = bandera
            Ejecuta_Procedure(SSCommandT)

            Dim SSCommand As New SqlCommand("SP_ASIGNACION_M")
            SSCommand.CommandType = CommandType.StoredProcedure
            SSCommand.Parameters.Add("@V_AGENCIA", SqlDbType.NVarChar).Value = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_AGENCIA
            SSCommand.Parameters.Add("@V_USUARIO", SqlDbType.NVarChar).Value = DdlUsuarioC.SelectedValue
            SSCommand.Parameters.Add("@V_TIPO", SqlDbType.NVarChar).Value = DdltipoAsig.SelectedValue
            SSCommand.Parameters.Add("@V_VALOR", SqlDbType.NVarChar).Value = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_CADENAAGENCIAS
            SSCommand.Parameters.Add("@V_VALOR2", SqlDbType.NVarChar).Value = TextBox1.Text.Trim
            SSCommand.Parameters.Add("@V_USUARIO_CARGA", SqlDbType.NVarChar).Value = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
            Dim DtsCarga As DataTable = Consulta_Procedure(SSCommand, "SP_CARGAR_ASIGNACION")
            GvCargaAsignacion2.DataSource = DtsCarga
            GvCargaAsignacion2.DataBind()
            GvCargaAsignacion2.Visible = True
            LblMensaje.Text = "Asignación Completada"



        End If

    End Sub

    Protected Sub Limpia()
        LblMensaje.Text = ""
        'DdlInstanciaA.ClearSelection()
        'DdlInstanciaC.ClearSelection()
        GvCargaAsignacion2.DataSource = Nothing
        GvCargaAsignacion2.DataBind()
        GvCargaAsignacion2.Visible = False
    End Sub

    Protected Sub Safi_actualizaAsignacionCartera(ByVal info As DataTable)
        Try
            Dim usuarioIP As String = Request.ServerVariables("REMOTE_HOST")
            Dim mensaje As String = ""
            '------asignacionCartera-----------------------
            For i As Integer = 0 To info.Rows.Count - 1
                Dim creditoID As String = info.Rows(i).Item("TMP_PR_MC_CREDITO")
                Dim tipoActualizacion As String = "A"
                Dim tipoInstancia As String = info.Rows(i).Item("TMP_PR_MC_INSTANCIA")
                Dim fechaAsignacion As String = info.Rows(i).Item("FECHA")
                Dim conDemanda As String = info.Rows(i).Item("JUICIO")
                Dim despachoID As String = info.Rows(i).Item("IDDESPACHO")
                Dim despachoNombre As String = info.Rows(i).Item("TMP_PR_MC_AGENCIA")
                Dim tipoDespacho As String = info.Rows(i).Item("EXTINT")
                Dim abogadoID As String = info.Rows(i).Item("IDUSUARIO")
                Dim abogadoNombre As String = info.Rows(i).Item("NOMBREUSUARIO")
                Dim supervisorID As String = info.Rows(i).Item("IDSUP")
                Dim supervisorNombre As String = info.Rows(i).Item("NOMBRESUP")

                Dim v_endpoint As String = Db.StrEndPoint("SAFI", 1)
                Dim v_metodo As String = "cobranza/actualizaAsignacionCartera"

                Dim CUrl As WebRequest = WebRequest.Create(v_endpoint & v_metodo)

                Dim data As String = "{" & vbLf & vbTab &
                    """usuarioIP"":""" & usuarioIP & """," & vbLf &
                    "    ""actualizaAsignacionCartera"":[" & vbLf &
                         "{" & vbLf &
                              """creditoID"":""" & creditoID & """," & vbLf &
                              """tipoActualizacion"":""" & tipoActualizacion & """," & vbLf &
                              """tipoInstancia"":""" & tipoInstancia & """," & vbLf &
                              """fechaAsignacion"":""" & fechaAsignacion & """," & vbLf &
                              """conDemanda"":""" & conDemanda & """," & vbLf &
                              """despachoID"":""" & despachoID & """," & vbLf &
                              """despachoNombre"":""" & despachoNombre & """," & vbLf &
                              """tipoDespacho"":""" & tipoDespacho & """," & vbLf &
                              """abogadoID"":""" & abogadoID & """," & vbLf &
                              """abogadoNombre"":""" & abogadoNombre & """," & vbLf &
                              """supervisorID"":""" & supervisorID & """," & vbLf &
                              """supervisorNombre"":""" & supervisorNombre & """" & vbLf &
                              "}" & vbLf & " ]" & vbLf &
                              "}" & vbLf & "  "


                CUrl.Method = "POST"
                CUrl.ContentLength = data.Length
                CUrl.ContentType = "application/json; charset=UTF-8"
                Dim enc As New UTF8Encoding()
                CUrl.Headers.Add("Autentificacion", Convert.ToBase64String(enc.GetBytes(Db.StrEndPoint("SAFI", 2) & ":" & Db.StrEndPoint("SAFI", 3))))

                Using ds As Stream = CUrl.GetRequestStream()
                    ds.Write(enc.GetBytes(data), 0, data.Length)
                End Using

                Dim wr As WebResponse = CUrl.GetResponse()
                Dim receiveStream As Stream = wr.GetResponseStream()
                Dim reader As New StreamReader(receiveStream, Encoding.UTF8)

                Dim v_json_resp As String = reader.ReadToEnd()


                mensaje = mensaje
                'Response.Close()
                reader.Close()
            Next


        Catch ex As WebException
            Dim abd As String = ex.Message
        End Try


    End Sub
End Class
