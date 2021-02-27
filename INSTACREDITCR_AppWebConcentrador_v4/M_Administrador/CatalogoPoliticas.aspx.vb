Imports System.Data
Imports System.Data.SqlClient
Imports Db
Imports Funciones

Partial Class CatalogoPoliticas
    Inherits System.Web.UI.Page
    Public Property TmpUSUARIO As IDictionary
        Get
            Return CType(Session("USUARIO"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("USUARIO") = value
        End Set
    End Property
    Public Property TmpPermisos As IDictionary
        Get
            Return CType(Session("Permisos"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("Permisos") = value
        End Set
    End Property

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If TmpUSUARIO Is Nothing Then
            OffLine(HidenUrs.Value)
            AUDITORIA(HidenUrs.Value, "Administrador", "Catalogos", "", "Page_Load", "", Request.ServerVariables("REMOTE_ADDR"), Request.ServerVariables("REMOTE_HOST"))
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/SesionExpirada.aspx")
        Else
            Try
                If Not IsPostBack Then
                    HidenUrs.Value = TmpUSUARIO("CAT_LO_USUARIO")
                    Llenar(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
                End If
            Catch ex As Exception
                SendMail("Page_Load", ex, "", "", HidenUrs.Value)
            End Try
        End If
    End Sub
    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Administrador", "CatalogosPolitica.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub
    Sub Llenar(V_CAT_DESC_LONG_MIN As Integer, V_CAT_DESC_LONG_MAX As Integer, V_CAT_DESC_MINUSC As Integer, V_CAT_DESC_MAYUSC As Integer, V_CAT_DESC_NUMEROS As Integer, V_CAT_DESC_ESPECIALES As Integer, V_CAT_DESC_HISTORIA As Integer, V_CAT_DESC_VIGENCIA As Integer, V_CAT_DESC_INACTIVIDAD As Integer, V_CAT_DESC_INTENTOS As Integer, V_CAT_USUARIO As String, V_Bandera As String)
        Dim SSCommandAgencias As New SqlCommand
        SSCommandAgencias.CommandText = "SP_ADD_POLITICA"
        SSCommandAgencias.CommandType = CommandType.StoredProcedure
        SSCommandAgencias.Parameters.Add("@V_CAT_DESC_LONG_MIN", SqlDbType.Decimal).Value = V_CAT_DESC_LONG_MIN
        SSCommandAgencias.Parameters.Add("@V_CAT_DESC_LONG_MAX", SqlDbType.Decimal).Value = V_CAT_DESC_LONG_MAX
        SSCommandAgencias.Parameters.Add("@V_CAT_DESC_MAYUSC", SqlDbType.Decimal).Value = V_CAT_DESC_MAYUSC
        SSCommandAgencias.Parameters.Add("@V_CAT_DESC_MINUSC", SqlDbType.Decimal).Value = V_CAT_DESC_MINUSC
        SSCommandAgencias.Parameters.Add("@V_CAT_DESC_NUMEROS", SqlDbType.Decimal).Value = V_CAT_DESC_NUMEROS
        SSCommandAgencias.Parameters.Add("@V_CAT_DESC_ESPECIALES", SqlDbType.Decimal).Value = V_CAT_DESC_ESPECIALES
        SSCommandAgencias.Parameters.Add("@V_CAT_DESC_HISTORIA", SqlDbType.Decimal).Value = V_CAT_DESC_HISTORIA
        SSCommandAgencias.Parameters.Add("@V_CAT_DESC_VIGENCIA", SqlDbType.Decimal).Value = V_CAT_DESC_VIGENCIA
        SSCommandAgencias.Parameters.Add("@V_CAT_DESC_INACTIVIDAD", SqlDbType.Decimal).Value = V_CAT_DESC_INACTIVIDAD
        SSCommandAgencias.Parameters.Add("@V_CAT_DESC_INTENTOS", SqlDbType.Decimal).Value = V_CAT_DESC_INTENTOS
        SSCommandAgencias.Parameters.Add("@V_CAT_USUARIO", SqlDbType.NVarChar).Value = V_CAT_USUARIO
        SSCommandAgencias.Parameters.Add("@V_CAT_IP", SqlDbType.NVarChar).Value = Request.ServerVariables("REMOTE_HOST")
        SSCommandAgencias.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = V_Bandera
        Dim DtsVarios As DataTable = Consulta_Procedure(SSCommandAgencias, "Politicas")
        If V_Bandera = 0 Then
            For Each row As DataRow In DtsVarios.Rows
                Select Case row("CAT_DESC")
                    Case "Cancelar Por Inactividad"
                        TXTCAT_DESC_INACTIVIDAD.Text = row("CAT_VALOR")
                    Case "Cancelar Por Intentos"
                        TXTCAT_DESC_INTENTOS.Text = row("CAT_VALOR")
                    Case "Caracteres Especiales"
                        TXTCAT_DESC_ESPECIALES.Text = row("CAT_VALOR")
                    Case "Historial"
                        TXTCAT_DESC_HISTORIA.Text = row("CAT_VALOR")
                    Case "Longitud Maxima"
                        TXTCAT_DESC_LONG_MAX.Text = row("CAT_VALOR")
                    Case "Longitud Minima"
                        TXTCAT_DESC_LONG_MIN.Text = row("CAT_VALOR")
                    Case "Mayusculas"
                        TXTCAT_DESC_MAYUSC.Text = row("CAT_VALOR")
                    Case "Minusculas"
                        TXTCAT_DESC_MINUSC.Text = row("CAT_VALOR")
                    Case "Numeros"
                        TXTCAT_DESC_NUMEROS.Text = row("CAT_VALOR")
                    Case "Vigencia"
                        TXTCAT_DESC_VIGENCIA.Text = row("CAT_VALOR")
                End Select
            Next
        End If
    End Sub


    Protected Sub BtnAceptar_Click(sender As Object, e As EventArgs) Handles BtnAceptar.Click
        Try
            Try
                If TXTCAT_DESC_LONG_MIN.Text = "" Then
                    Aviso("Capture La Longitud Minima")

                ElseIf Val(TXTCAT_DESC_LONG_MIN.Text) < 8 Then
                    Aviso("La Longitud Minima No Puede Ser Menor A 8")

                ElseIf TXTCAT_DESC_LONG_MAX.Text = "" Then
                    Aviso("Capture La Longitud Maxima")

                ElseIf Val(TXTCAT_DESC_LONG_MAX.Text) > 50 Then
                    Aviso("La Longitud Maxima No Puede Ser Mayor A 50")

                ElseIf TXTCAT_DESC_MINUSC.Text = "" Then
                    Aviso("Capture Cuantas Minusculas")

                ElseIf Integer.Parse(TXTCAT_DESC_LONG_MIN.Text) > Integer.Parse(TXTCAT_DESC_LONG_MAX.Text) Then
                    Aviso("La Longitud Mínima No Puede Ser Mayor a La Longitud Máxima")

                ElseIf TXTCAT_DESC_MAYUSC.Text = "" Then
                    Aviso("Capture Cuantas Mayusculas")

                ElseIf TXTCAT_DESC_NUMEROS.Text = "" Then
                    Aviso("Capture Cuantos Números")

                ElseIf TXTCAT_DESC_ESPECIALES.Text = "" Then
                    Aviso("Capture Cuantos Caracteres Especiales")

                ElseIf TXTCAT_DESC_HISTORIA.Text = "" Then
                    Aviso("Capture Historial De Contraseñas")

                ElseIf TXTCAT_DESC_VIGENCIA.Text = "" Then
                    Aviso("Capture Vigencia De La Contraseña")

                ElseIf TXTCAT_DESC_INACTIVIDAD.Text = "" Then
                    Aviso("Capture Cancelar Por Inactividad")

                ElseIf TXTCAT_DESC_INTENTOS.Text = "" Then
                    Aviso("Capture Número De Intentos")
                ElseIf (Integer.Parse(TXTCAT_DESC_MAYUSC.Text) + Integer.Parse(TXTCAT_DESC_ESPECIALES.Text) + Integer.Parse(TXTCAT_DESC_NUMEROS.Text) + Integer.Parse(TXTCAT_DESC_MINUSC.Text)) > Integer.Parse(TXTCAT_DESC_LONG_MIN.Text) Then
                    Aviso("La suma de las Mayúsculas, Minúsculas, Caracteres Especiales y Números tiene que ser menor o igual a la Longitud Mínima")
                Else
                    Confirma("¿Guardar Los Cambios?")

                End If
            Catch ex As Exception
                Aviso(ex.Message)

            End Try
        Catch ex As Exception
            SendMail("BtnAceptar_Click", ex, "", "", HidenUrs.Value)
        End Try
    End Sub
    Protected Sub Aviso(ByVal MSJ As String)
        RadAviso.RadAlert(MSJ, 440, 155, "Aviso", Nothing)
    End Sub
    Protected Sub Confirma(ByVal MSJ As String)
        RadAviso.RadConfirm(MSJ, "confirmCallbackFn", 440, 155, Nothing, "Alerta")

    End Sub

    Protected Sub BtnAceptarConfirmacion_Click(sender As Object, e As EventArgs) Handles BtnAceptarConfirmacion.Click

        Try
            Llenar(TXTCAT_DESC_LONG_MIN.Text, TXTCAT_DESC_LONG_MAX.Text, TXTCAT_DESC_MINUSC.Text, TXTCAT_DESC_MAYUSC.Text, TXTCAT_DESC_NUMEROS.Text, TXTCAT_DESC_ESPECIALES.Text, TXTCAT_DESC_HISTORIA.Text, TXTCAT_DESC_VIGENCIA.Text, TXTCAT_DESC_INACTIVIDAD.Text, TXTCAT_DESC_INTENTOS.Text, TmpUSUARIO("CAT_LO_USUARIO"), 1)
            Aviso("Se Han Guardado Los Cambios")

        Catch ex As Exception
            Aviso("Error al guardar")
        End Try
    End Sub
End Class
