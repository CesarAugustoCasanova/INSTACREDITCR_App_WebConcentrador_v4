Imports System.Data.OracleClient
Imports System.Data
Imports Funciones
Imports System.Web.Services
Imports System.Web.SessionState.HttpSessionState
Imports AjaxControlToolkit
Imports System.Web.UI.WebControls.Label
Imports System.Drawing
Imports Subgurim.Controles
Imports System.IO
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D
Imports Db
Imports Telerik.Web.UI
Partial Class Inicio

    Inherits System.Web.UI.Page


    Public Property tmpUSUARIO As IDictionary
        Get
            Return CType(Session("USUARIO"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("USUARIO") = value
        End Set
    End Property

    Public Property tmpCredito As IDictionary
        Get
            Return CType(Session("Credito"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("Credito") = value
        End Set
    End Property

    Public Property tmpGarantias As IDictionary
        Get
            Return CType(Session("garantias"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("garantias") = value
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

    <WebMethod(EnableSession:=True)>
    Public Shared Function Cargar(ByVal V_Bandera As String) As String
        If V_Bandera = 1 Then
            Dim DtsInfo As DataTable = Class_VariosQueries.VARIOS_QUERIES("", V_Bandera)
            If DtsInfo.Rows.Count > 0 Then
                Dim sJSON As String = ""
                sJSON = DStoJSON(DtsInfo)
                If Len(sJSON) > 1 Then
                    sJSON = sJSON.Substring(0, Len(sJSON) - 1)
                End If
                Dim yourJson As String = "[" & sJSON & "]"
                Return yourJson
            Else
                Return "No"
            End If
        ElseIf V_Bandera = 0 Then
            Dim DtsInfo As DataTable = Class_VariosQueries.VARIOS_QUERIES("", V_Bandera)
            If DtsInfo.Rows.Count > 0 Then
                Dim sJSON As String = ""
                sJSON = DStoJSON(DtsInfo)
                If Len(sJSON) > 1 Then
                    sJSON = sJSON.Substring(0, Len(sJSON) - 1)
                End If
                Dim yourJson As String = "[" & sJSON & "]"
                Return yourJson
            Else
                Return "No"
            End If
        ElseIf V_Bandera = 5 Then
            Dim DtsInfo As DataTable = Class_VariosQueries.VARIOS_QUERIES("", V_Bandera)
            If DtsInfo.Rows.Count > 0 Then
                Dim sJSON As String = ""
                sJSON = DStoJSON(DtsInfo)
                If Len(sJSON) > 1 Then
                    sJSON = sJSON.Substring(0, Len(sJSON) - 1)
                End If
                Dim yourJson As String = "[" & sJSON & "]"
                Return yourJson
            Else
                Return "No"
            End If
        Else
            Return "No"
        End If
    End Function

    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Monitoreo", "InfoUsuarios.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If tmpPermisos("DASHBOARD_VISITADORES") = False Then
                Session.Clear()
                Session.Abandon()
                Response.Redirect("~/SesionExpirada.aspx")
            Else
                PnlInfo.Visible = True
            End If
        Catch ex As Exception
            SendMail("Page_Load Inicio Backoffice", ex, "", "", "")
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/SesionExpirada.aspx")
        End Try
    End Sub

    Protected Sub RadBtnGenerar_Click(sender As Object, e As EventArgs) Handles RadBtnGenerar.Click
        If TxtFechaI.DateInput.DisplayText = "" Then
            Mensaje("Seleccione Fecha De Inicio")
        ElseIf TxtFechaF.DateInput.DisplayText = "" Then
            Mensaje("Seleccione Fecha De Termino")
        Else
            Dim DtsInfo As DataTable = Class_VariosQueries.VARIOS_QUERIES(" and convert(date,isnull(HIST_AM_FECHA,HIST_AM_FECHA_SYNC),103) between convert(date,'" & TxtFechaI.DateInput.DisplayText & "',103) and convert(date,'" & TxtFechaF.DateInput.DisplayText & "',103)", 4)
            If DtsInfo.Rows.Count <> 0 Then
                Dim RUTA As String = StrRuta() & "Salida\"
                If Not Directory.Exists(RUTA) Then
                    Directory.CreateDirectory(RUTA)
                End If
                Dim ARCHIVO As String = RUTA & "GestionesTerreno.txt"
                Dim cuantos As Integer = DtsInfo.Rows.Count
                If File.Exists(ARCHIVO) Then
                    Kill(ARCHIVO)
                End If

                Dim fs As Stream
                fs = New System.IO.FileStream(ARCHIVO, IO.FileMode.OpenOrCreate)
                Dim sw As New System.IO.StreamWriter(fs)

                Dim v_encabezado As String = ""
                Dim v_delim As String = ","

                For h As Integer = 0 To DtsInfo.Columns.Count - 1
                    v_encabezado = v_encabezado & HttpUtility.HtmlDecode(DtsInfo.Columns(h).ColumnName) & v_delim
                Next

                sw.WriteLine(HttpUtility.HtmlDecode(v_encabezado))

                For a As Integer = 0 To cuantos - 1
                    Dim v_registros As String = ""
                    For r As Integer = 0 To DtsInfo.Columns.Count - 1
                        Dim v_valor As String = ""
                        If Not IsDBNull(DtsInfo.Rows(a)(DtsInfo.Columns(r).ColumnName)) Then
                            v_valor = HttpUtility.HtmlDecode(DtsInfo.Rows(a)(DtsInfo.Columns(r).ColumnName)).Replace(v_delim, "")
                        Else
                            v_valor = " "
                        End If
                        v_registros = v_registros & v_valor & v_delim
                    Next
                    sw.WriteLine(HttpUtility.HtmlDecode(v_registros))
                Next

                sw.Close()
                fs.Close()

                If File.Exists(ARCHIVO) Then
                    Dim ioflujo As FileInfo = New FileInfo(ARCHIVO)
                    Response.Clear()
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + ioflujo.Name)
                    Response.AddHeader("Content-Length", ioflujo.Length.ToString())
                    Response.ContentType = "application/octet-stream"
                    Response.WriteFile(ARCHIVO)
                    Response.End()
                End If
            Else
                Mensaje("No se Encontraron Resultados")
            End If
        End If

    End Sub

    Protected Sub Mensaje(ByVal v_mensaje As String)
        RamiWa.RadAlert(v_mensaje, 400, 150, Nothing, Nothing)
    End Sub

End Class
