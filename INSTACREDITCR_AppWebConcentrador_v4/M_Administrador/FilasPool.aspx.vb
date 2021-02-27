
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports Db
Imports Funciones
Imports System.Diagnostics
Imports Telerik.Web.UI
Imports AjaxControlToolkit
Imports System.Collections.Generic
Imports System.Globalization


Partial Class FilasPool
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
    Private Function isSessionIDValid() As Boolean
        Try
            If Application.Get(tmpUSUARIO("CAT_LO_USUARIO")) = Session.SessionID Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
        End Try
        Return False
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim Usr As String = tmpUSUARIO("CAT_LO_USUARIO")
        Catch ex As Exception
            OffLine(HidenUrs.Value)
            AUDITORIA(HidenUrs.Value, "Administrador", "CargaBlockCuentas", " ", "Page_Load", "", Request.ServerVariables("REMOTE_ADDR"), Request.ServerVariables("REMOTE_HOST"))
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/SesionExpirada.aspx")
        End Try
        Try
            If Not IsPostBack Then
                If True = False Then ' posible validacion de permiso filas
                    OffLine(HidenUrs.Value)
                    Session.Clear()
                    Session.Abandon()
                    Response.Redirect("~/SesionExpirada.aspx")
                End If
                HidenUrs.Value = tmpUSUARIO("CAT_LO_USUARIO")
                Consultafilas()
            End If
        Catch ex As Exception
            SendMail("Page_Load", ex, "", "", HidenUrs.Value)
        End Try
    End Sub

    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Administrador", "FilasPool.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub

    Sub Consultafilas()
        Dim dtstFilas As DataTable = ConsultaFilasCalendario(21, "", "", " CAT_LO_AGENCIA= " & tmpUSUARIO("CAT_LO_AGENCIA"), 0, "")
        If dtstFilas.Rows.Count > 0 Then
            rdtckrMensaje.AutoStart = False
            rdtckrMensaje.Visible = False
            rdgrdEstatusFilas.DataSource = dtstFilas
            rdgrdEstatusFilas.DataBind()
            rdgrdEstatusFilas.Visible = True
        Else
            rdtckrMensaje.AutoStart = True
            rdtckrMensaje.Visible = True
            rdgrdEstatusFilas.Visible = False
        End If
    End Sub
    Function ConsultaFilasCalendario(ByVal V_Bandera As String, ByVal V_Comentario As String, ByVal V_DteInicio As String, ByVal V_DteFin As String, ByVal V_ID As Integer, ByVal V_Nombre As String) As DataTable

        Dim SSCommandUsuario As New SqlCommand
        SSCommandUsuario.CommandText = "SP_FILAS_TRABAJO2"
        SSCommandUsuario.CommandType = CommandType.StoredProcedure
        SSCommandUsuario.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = V_Bandera
        SSCommandUsuario.Parameters.Add("@V_Tipo", SqlDbType.NVarChar).Value = V_Comentario
        SSCommandUsuario.Parameters.Add("@V_Campo", SqlDbType.NVarChar).Value = V_DteInicio
        SSCommandUsuario.Parameters.Add("@V_Donde", SqlDbType.NVarChar).Value = V_DteFin
        SSCommandUsuario.Parameters.Add("@V_ID", SqlDbType.Decimal).Value = V_ID
        SSCommandUsuario.Parameters.Add("@V_CAMPOS", SqlDbType.NVarChar).Value = V_Nombre
        Dim DtsUsuario As DataTable = Consulta_Procedure(SSCommandUsuario, "FILAS")
        Return DtsUsuario

    End Function



    Protected Sub rdgrdEstatusFilas_ItemCreated(sender As Object, e As GridItemEventArgs)
        If TypeOf e.Item Is GridDataItem Then

            'Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
            'Dim cell As TableCell = dataItem("NOM_FILA")
            'dataItem(
            'Dim fila As String = dataItem("NOM_FILA").Text
            'Dim drpdwnchbxGEstores As RadComboBox = DirectCast(e.Item.FindControl("drpdwchbxGestores"), RadComboBox)
            'Dim nomfila As String = cell.Text

        End If
    End Sub

    Protected Sub rdgrdEstatusFilas_ItemDataBound(sender As Object, e As GridItemEventArgs)

        If TypeOf e.Item Is GridDataItem Then

            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
            Dim nomfila As String = dataItem("NOM_FILA").Text
            Dim edo As String = dataItem("STATUS").Text
            Dim cell As TableCell = dataItem("TemplateEditColumn")
            Dim drpdwnchbxGEstores As RadComboBox = CType(dataItem.FindControl("drpdwchbxGestores"), RadComboBox)

            Dim dtsFilas As DataTable = ConsultaFilasCalendario(15, nomfila, "", tmpUSUARIO("CAT_LO_AGENCIA"), 0, "")

            drpdwnchbxGEstores.DataTextField = dtsFilas.Columns(0).ColumnName
            drpdwnchbxGEstores.DataValueField = dtsFilas.Columns(0).ColumnName
            drpdwnchbxGEstores.DataSource = dtsFilas
            drpdwnchbxGEstores.DataBind()

            dtsFilas = Nothing
            dtsFilas = ConsultaFilasCalendario(22, nomfila, "", "", 0, "")

            Dim strGestores2 As String = dtsFilas.Rows(0)(0)
            Dim Cont As Integer = 0
            For Each elemtos As RadComboBoxItem In drpdwnchbxGEstores.Items
                If InStr(strGestores2, elemtos.Value) Then
                    elemtos.Checked = True
                    Cont += 1
                End If
            Next
            Dim img As ImageButton = dataItem("OnOff").Controls(0)
            If edo = "On" Then
                'Dim img As ImageButton = dataItem("OnOff").Controls(0)
                img.ImageUrl = "~/M_Administrador/Imagenes/On.png"
                img.ToolTip = "Apagar"
            Else
                img.ImageUrl = "~/M_Administrador/Imagenes/Off.png"
                img.ToolTip = "Encender"
            End If

        End If

    End Sub

    Protected Sub rdgrdEstatusFilas_ItemCommand(sender As Object, e As GridCommandEventArgs)

        Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
        Dim nomfila As String = dataItem("NOM_FILA").Text
        Dim onoff As String = dataItem("STATUS").Text
        Dim Usr As String = tmpUSUARIO("CAT_LO_USUARIO")

        If e.CommandName = "actualiza" Then

            If onoff = "On" Then
                Dim dtsFilas As DataTable = ConsultaFilasCalendario(24, nomfila, "", "", 1, Usr)
                Response.Redirect("FilasPool.aspx", True)
            Else
                'RadWindowManager1.RadAlert("La fila no se encuentra trabajando actualmente", 250, 100, "Fila Off", "")
                ALERTA("La fila no se encuentra trabajando actualmente")
            End If

        ElseIf e.CommandName = "OnOff" Then

            If onoff = "Off" Then '----------- Enciende la fila de tabajo seleccionada

                Dim dteFinDiaLaboral As Date
                Dim bln As Boolean = DateTime.TryParse(Date.Now.Date & " 22:00", dteFinDiaLaboral)
                Dim dteIniDiaLaboral As Date
                Dim bln2 As Boolean = DateTime.TryParse(Date.Now.Date & " 07:00", dteIniDiaLaboral)

                Dim dtsFilas As DataTable = ConsultaFilasCalendario(27, nomfila, "", "", 1, Usr)
                If dtsFilas.Rows(0)(0) <= 0 And (Date.Now.TimeOfDay < dteFinDiaLaboral.TimeOfDay Or Date.Now.TimeOfDay > dteIniDiaLaboral.TimeOfDay) Then
                    If validaTieneCuentas(nomfila) Then
                        dtsFilas = Nothing
                        dtsFilas = ConsultaFilasCalendario(25, nomfila, "", "", 2, Usr)
                        Dim img As ImageButton = dataItem("OnOff").Controls(0)
                        img.ImageUrl = "~/M_Administrador/Imagenes/On.png"
                        img.ToolTip = "Apagar"

                        Response.Redirect("FilasPool.aspx", True)
                    Else
                        ALERTA("No se puede encender la fila debido a que actualmente no existen cuentas que cumplan los parametros establecidos")
                    End If
                Else
                    'RadWindowManager1.RadAlert("La fila ya se encuentra trabajando actualmente", 250, 100, "Fila trabajando", "")
                    ALERTA("La fila ya se encuentra trabajando actualmente")
                End If

            Else '----------- Apaga la fila de tabajo seleccionada

                Dim dtsFilas As DataTable = ConsultaFilasCalendario(26, nomfila, "", "", 2, Usr)

                Dim img As ImageButton = dataItem("OnOff").Controls(0)
                img.ImageUrl = "~/M_Administrador/Imagenes/Off.png"
                img.ToolTip = "Encender"

                Response.Redirect("FilasPool.aspx", True)

            End If

        ElseIf e.CommandName = "eliminar" Then
            Dim dtsFilas As DataTable = ConsultaFilasCalendario(31, nomfila, "", "", 2, Usr)
            Consultafilas()

        End If

    End Sub

    Protected Function validaTieneCuentas(ByVal nomfila As String) As Boolean
        Dim dtsFilas As DataTable = ConsultaFilasCalendario(35, nomfila, "", "", 0, "")
        If dtsFilas.Rows(0).Item("CUANTAS") > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Protected Sub drpdwchbxGestores_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)

        Dim rdcmdbxGEstores As RadComboBox = DirectCast(sender, RadComboBox)
        Dim dataItem As GridDataItem = TryCast(rdcmdbxGEstores.NamingContainer, GridDataItem)
        Dim Usr As String = tmpUSUARIO("CAT_LO_USUARIO")

        Dim onoff As String = dataItem("STATUS").Text
        Dim nomfila As String = dataItem("NOM_FILA").Text

        If onoff = "On" Then
            Dim gest As String
            Dim Cont As Integer = 0

            For Each elemtos As RadComboBoxItem In rdcmdbxGEstores.Items
                If elemtos.Checked Then
                    gest = gest & elemtos.Value & ","
                    Cont += 1
                End If
            Next

            If Cont > 0 Then
                gest = gest.Substring(0, gest.LastIndexOf(","))
                Dim dtsFilas As DataTable = ConsultaFilasCalendario(23, nomfila, gest, "", 0, Usr)
                Response.Redirect("FilasPool.aspx", True)
            Else
                'RadWindowManager1.RadAlert("Seleccione por lo menos un gestor para la fila", 250, 100, "Sin gestores", "")
                ALERTA("Seleccione por lo menos un gestor para la fila")
            End If
        Else
            'RadWindowManager1.RadAlert("La fila no se encuentra trabajando actualmente", 250, 100, "Fila Off", "")
            ALERTA("La fila no se encuentra trabajando actualmente")
        End If

    End Sub


    Sub ALERTA(ByVal MENSAJE As String)
        WinAviso.RadAlert(MENSAJE, 450, 250, "Aviso", Nothing)
        'Dim sb As New System.Text.StringBuilder()
        'sb.Append("<script type = 'text/javascript'>")
        'sb.Append("functionallle(){")
        'sb.Append("alert('")
        'sb.Append(MENSAJE)
        'sb.Append("')};")
        'sb.Append("</script>")
        'ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", sb.ToString())
    End Sub

End Class
