Imports System.Data.SqlClient
Imports System.Data
Imports Funciones
Imports System.Web.Services
Imports Telerik.Web.UI
Imports Db
Imports Telerik.Web.UI.Calendar
Partial Class M_Monitoreo_VisualizarAsignacion
    Inherits System.Web.UI.Page
    Public Property tmpUSUARIO As IDictionary
        Get
            Return CType(Session("USUARIO"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("USUARIO") = value
        End Set
    End Property

    Private usr As String
    <WebMethod(EnableSession:=True)>
    Public Shared Function ObtenerMonitoreo(ByVal V_Usuario As String, ByVal V_solicitante As String) As String
        Dim DtsMonitoreo As DataTable = SP.MONITOREO(V_Usuario, V_solicitante, 2)
        If DtsMonitoreo.Rows.Count > 0 Then
            Dim dtDatos As DataTable = New DataTable()
            dtDatos.Columns.Add("Usuario")
            dtDatos.Columns.Add("Latitud")
            dtDatos.Columns.Add("Longitud")
            dtDatos.Columns.Add("Accion")
            dtDatos.Columns.Add("Fecha")
            dtDatos.Columns.Add("Credito")
            dtDatos.Columns.Add("Resultado")
            dtDatos.Columns.Add("Nombre")
            dtDatos.Columns.Add("Bloque")
            dtDatos.Columns.Add("Bateria")
            dtDatos.Columns.Add("Velocidad")
            Dim dtRow As DataRow
            For i As Integer = 0 To DtsMonitoreo.Rows.Count - 1
                dtRow = dtDatos.NewRow()
                dtRow.Item("Usuario") = DtsMonitoreo.Rows(i)("USUARIO")
                dtRow.Item("Latitud") = DtsMonitoreo.Rows(i)("LATITUD")
                dtRow.Item("Longitud") = DtsMonitoreo.Rows(i)("LONGITUD")
                dtRow.Item("Accion") = DtsMonitoreo.Rows(i)("ACCION")
                dtRow.Item("Fecha") = DtsMonitoreo.Rows(i)("FECHA")
                dtRow.Item("Credito") = DtsMonitoreo.Rows(i)("CREDITO")
                dtRow.Item("Resultado") = DtsMonitoreo.Rows(i)("RESULTADO")
                dtRow.Item("Nombre") = DtsMonitoreo.Rows(i)("NOMBRE")
                dtRow.Item("Bloque") = DtsMonitoreo.Rows(i)("BLOQUE")
                dtRow.Item("Bateria") = DtsMonitoreo.Rows(i)("BATERIA")
                dtRow.Item("Velocidad") = DtsMonitoreo.Rows(i)("VELOCIDAD")
                dtDatos.Rows.Add(dtRow)
            Next
            Dim ds As New DataTable
            ds = dtDatos
            Dim sJSON As String = ""
            sJSON = DStoJSON(ds)
            If Len(sJSON) > 1 Then
                sJSON = sJSON.Substring(0, Len(sJSON) - 1)
            End If
            Dim yourJson As String = "[" & sJSON & "]"
            Return yourJson
        Else
            Return "No Se Localizo El Dispositivo"
        End If

    End Function

    <WebMethod(EnableSession:=True)>
    Public Shared Function ObtenerCartera(ByVal V_Agrupacion As String, ByVal V_Tipo As String, ByVal v_Filtro As String, ByVal v_Tipotope As String) As String

        Dim DtsMonitoreo As DataTable = SP.RP_MONITOREO_V2(V_Agrupacion, V_Tipo, v_Filtro, v_Tipotope, 4)
        If DtsMonitoreo.Rows.Count > 0 Then
            Dim dtDatos As DataTable = New DataTable()
            dtDatos.Columns.Add("Cuenta")
            dtDatos.Columns.Add("Resultado")
            dtDatos.Columns.Add("Gestor")
            dtDatos.Columns.Add("Fecha")
            dtDatos.Columns.Add("Asignacion")
            dtDatos.Columns.Add("Latitud")
            dtDatos.Columns.Add("Longitud")
            dtDatos.Columns.Add("Campana")
            dtDatos.Columns.Add("Bloque")
            dtDatos.Columns.Add("Tipo")
            dtDatos.Columns.Add("Estatus")
            dtDatos.Columns.Add("Mes")
            Dim dtRow As DataRow
            For i As Integer = 0 To DtsMonitoreo.Rows.Count - 1
                dtRow = dtDatos.NewRow()
                dtRow.Item("Cuenta") = DtsMonitoreo.Rows(i)("CUENTA")
                dtRow.Item("Resultado") = DtsMonitoreo.Rows(i)("MUNICIPIO")
                dtRow.Item("Gestor") = DtsMonitoreo.Rows(i)("GESTOR")
                dtRow.Item("Fecha") = ""
                dtRow.Item("Asignacion") = DtsMonitoreo.Rows(i)("CP")
                dtRow.Item("Latitud") = DtsMonitoreo.Rows(i)("LATITUD")
                dtRow.Item("Longitud") = DtsMonitoreo.Rows(i)("LONGITUD")
                'dtRow.Item("Campana") = DtsMonitoreo.Rows(i)("CAMPANA")
                dtRow.Item("Bloque") = " " ' DtsMonitoreo.Rows(i)("RIESGO")
                ' dtRow.Item("Tipo") = DtsMonitoreo.Rows(i)("TIPO")
                dtRow.Item("Estatus") = " " 'DtsMonitoreo.Rows(i)("TIENDA")
                ' dtRow.Item("Mes") = DtsMonitoreo.Rows(i)("MES")
                dtDatos.Rows.Add(dtRow)
            Next
            Dim ds As New DataTable()
            ds = dtDatos
            Dim sJSON As String = ""



            sJSON = DStoJSON(ds)
            If Len(sJSON) > 1 Then
                sJSON = sJSON.Substring(0, Len(sJSON) - 1)
            End If
            Dim yourJson As String = "[" & sJSON & "]"
            Return yourJson
        Else
            Return "No se encontraron cuentas"
        End If

    End Function
    <WebMethod(EnableSession:=True)>
    Public Shared Function ObtenerRuta(ByVal V_Usuario As String, ByVal V_Fecha As String) As String
        Try
            Dim DtsMonitoreo As DataTable = SP.RP_MONITOREO_V2(V_Usuario, CType(V_Fecha, DateTime).ToShortDateString, "", "", 5)

            If DtsMonitoreo.Rows.Count > 0 Then
                Dim dtDatos As DataTable = New DataTable()
                dtDatos.Columns.Add("Visitador")
                dtDatos.Columns.Add("Resultado")
                dtDatos.Columns.Add("Fecha")
                dtDatos.Columns.Add("Latitud")
                dtDatos.Columns.Add("Longitud")
                dtDatos.Columns.Add("Efectivo")
                Dim dtRow As DataRow
                For i As Integer = 0 To DtsMonitoreo.Rows.Count - 1
                    dtRow = dtDatos.NewRow()
                    dtRow.Item("Visitador") = DtsMonitoreo.Rows(i)("VISITADOR")
                    dtRow.Item("Resultado") = DtsMonitoreo.Rows(i)("RESULTADO")
                    dtRow.Item("Fecha") = DtsMonitoreo.Rows(i)("FECHA")
                    dtRow.Item("Latitud") = DtsMonitoreo.Rows(i)("LATITUD")
                    dtRow.Item("Longitud") = DtsMonitoreo.Rows(i)("LONGITUD")
                    dtRow.Item("Efectivo") = DtsMonitoreo.Rows(i)("EFECTIVO")
                    dtDatos.Rows.Add(dtRow)
                Next
                Dim ds As New DataTable()
                ds = dtDatos
                Dim sJSON As String = ""
                sJSON = DStoJSON(ds)
                If Len(sJSON) > 1 Then
                    sJSON = sJSON.Substring(0, Len(sJSON) - 1)
                End If
                Dim yourJson As String = "[" & sJSON & "]"
                Return yourJson
            Else
                Return "0"
            End If
        Catch ex As Exception

        End Try
        Return "0"
    End Function
    Public Property tmpPermisos As IDictionary
        Get
            Return CType(Session("Permisos"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("Permisos") = value
        End Set
    End Property

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                HidenUrs.Value = tmpUSUARIO("CAT_LO_USUARIO")
            End If
        Catch ex As Exception
            HidenUrs.Value = "ADAL"
            Dim algo As String = ex.Message
        End Try
    End Sub

    Public Shared Function Rad_Tcadena(ByRef v_item As RadComboBox) As String
        Dim v_cadena As String = ""
        Dim collection As IList(Of RadComboBoxItem) = v_item.CheckedItems

        For Each item As RadComboBoxItem In collection
            v_cadena &= "'" & item.Value & "',"
        Next
        Try
            v_cadena = v_cadena.Substring(0, Len(v_cadena) - 1)
        Catch ex As Exception
            v_cadena = ""
        End Try

        Return v_cadena
    End Function
    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Monitoreo", "InfoUsuarios.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub

    Protected Sub LLENAR_DROP(ByVal bandera As Integer, ByVal condiciones As String, ByVal ITEM As Object, ByVal DataValueField As String, ByVal DataTextField As String, ByVal V_Nohay As String)
        Try
            ITEM.Items.Clear()

            Dim SSCommand As New SqlCommand
            SSCommand.CommandText = "SP_RP_VISITAS_V3"
            SSCommand.CommandType = CommandType.StoredProcedure
            SSCommand.Parameters.Add("@v_bandera", SqlDbType.Decimal).Value = bandera
            SSCommand.Parameters.Add("@v_agencia", SqlDbType.NVarChar).Value = ""
            SSCommand.Parameters.Add("@v_condiciones", SqlDbType.NVarChar).Value = condiciones
            Dim objDSa As DataTable = Consulta_Procedure(SSCommand, "Filtros")

            If objDSa.Rows.Count = 0 Then
                to_limpia_ddl(ITEM, V_Nohay)
            Else
                RcbCat_lo_usuario.DataTextField = DataTextField
                RcbCat_lo_usuario.DataValueField = DataValueField
                RcbCat_lo_usuario.DataSource = objDSa
                RcbCat_lo_usuario.DataBind()
            End If

        Catch ex As Exception
            Dim ALGO As String = ex.ToString
        End Try
    End Sub
    Protected Sub to_limpia_ddl(ByVal v_item As RadComboBox, ByVal v_NoHay As String)
        v_item.Items.Clear()
    End Sub

    Private Sub RcbCat_lo_usuario_Init(sender As Object, e As EventArgs) Handles RcbCat_lo_usuario.Init
        If Not IsPostBack Then
            LLENAR_DROP(17, " ", sender, "V_VALOR", "T_VALOR", "Sin Usuario")
        End If
    End Sub
End Class

