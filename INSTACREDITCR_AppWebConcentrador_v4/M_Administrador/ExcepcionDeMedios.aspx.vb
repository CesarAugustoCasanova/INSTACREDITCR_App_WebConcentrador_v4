Imports System.Data.SqlClient
Imports System.Data
Imports System.Diagnostics
Imports System.IO
Imports Db
Imports Funciones
Imports Telerik.Web.UI

Partial Class ExcepcionDeMedios
    Inherits System.Web.UI.Page
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

            End If
        Catch ex As Exception
            SendMail("Page_Load", ex, "", "", HidenUrs.Value)
        End Try
    End Sub

    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Administrador", "exceptiondeMedios.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub

    Protected Sub aviso(ByVal MSJ As String)
        WinMsj.RadAlert(MSJ, 400, 150, "Aviso", Nothing)
    End Sub
    Protected Sub Limpiar()
        RLblTipo.Text = ""
        RTxtValor.Text = ""
        PnlElementos.Visible = False
    End Sub
    Private Sub RdElemento_SelectedIndexChanged(sender As Object, e As DropDownListEventArgs) Handles RdElemento.SelectedIndexChanged
        Limpiar()
        RDTipo.Visible = False
        RDTipo.SelectedValue = "Seleccione"
        RLblTipo.Visible = False
        If RdElemento.SelectedValue <> "Seleccione" Then
            RDTipo.Visible = True
            RLblTipo.Visible = True
        End If
    End Sub
    Private Sub RDTipo_SelectedIndexChanged(sender As Object, e As DropDownListEventArgs) Handles RDTipo.SelectedIndexChanged
        Limpiar()
        If RDTipo.SelectedValue <> "Seleccione" Then
            PnlElementos.Visible = True
            RLblTipoS.Text = RdElemento.SelectedText
        End If
    End Sub

    Protected Sub RBtnAceptar_Click(sender As Object, e As EventArgs) Handles RBtnAceptar.Click
        If RTxtValor.Text.Length = 0 Then
            aviso("Capture Un Valor Para El Tipo " & RLblTipoS.Text)
        Else
            Dim V_Validacion As String = "0"
            If RdElemento.SelectedValue = "Credito" Then
                V_Validacion = Valida("Credito", RTxtValor.Text)
            ElseIf RdElemento.SelectedValue = "Correo" Then
                V_Validacion = Valida("Correo", RTxtValor.Text)
            ElseIf RdElemento.SelectedValue = "Rol" Then
                V_Validacion = Valida("Rol", RTxtValor.Text)
            ElseIf RdElemento.SelectedValue = "Telefono" Then
                V_Validacion = Valida("Telefono", RTxtValor.Text)
            End If
            If V_Validacion = "0" Then
                Dim SSCommandBloqueos As New SqlCommand
                SSCommandBloqueos.CommandText = "SP_ADD_CAT_BLOQUEO_MEDIOS"
                SSCommandBloqueos.CommandType = CommandType.StoredProcedure
                SSCommandBloqueos.Parameters.Add("@V_Accion", SqlDbType.NVarChar).Value = RDTipo.SelectedValue
                SSCommandBloqueos.Parameters.Add("@V_Cat_Bm_Tipo", SqlDbType.NVarChar).Value = RdElemento.SelectedValue
                SSCommandBloqueos.Parameters.Add("@V_Cat_Bm_Valor", SqlDbType.NVarChar).Value = RTxtValor.Text
                SSCommandBloqueos.Parameters.Add("@V_Cat_Bm_Usuario", SqlDbType.NVarChar).Value = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
                SSCommandBloqueos.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 0
                Dim DtsVarios As DataTable = Consulta_Procedure(SSCommandBloqueos, "Bloqueos")
                If DtsVarios.Rows(0).Item("Estatus") = "1" Then

                    aviso(RdElemento.SelectedValue & " " & IIf(RDTipo.SelectedValue = "Bloqueo", "Bloqueado", "Desbloqueado"))
                    RdElemento.SelectedValue = "Seleccione"
                    RDTipo.Visible = False
                    Limpiar()
                Else
                    aviso(DtsVarios.Rows(0).Item("Mensaje"))
                End If
            Else
                    aviso(V_Validacion)
            End If
        End If
    End Sub


    Function Valida(ByVal V_Tipo As String, ByVal V_Valor As String) As String
        Dim V_Resultado As String = "0"
        If V_Tipo = "Credito" Then
            If V_Valor.ToUpper <> V_Valor.ToLower Then
                V_Resultado = "EL Número De Credito No Es Valido"
            End If
            If validabd(V_Tipo, V_Valor) = 0 Then
                V_Resultado = "EL Número De Credito No Existe"
            End If
        ElseIf V_Tipo = "Correo" Then
            If Funciones.EmailValida(V_Valor) = True Then
                V_Resultado = "EL Correo No Es Valido"
            End If
            If validabd(V_Tipo, V_Valor) = 0 Then
                V_Resultado = "EL Correo No Existe"
            End If
        ElseIf V_Tipo = "Rol" Then
                If V_Valor <> "T" And V_Valor <> "A" And V_Valor <> "G" And V_Valor <> "C" Then
                    V_Resultado = "El Rol No Existe"
                End If
            ElseIf V_Tipo = "Telefono" Then
            If V_Valor.ToUpper <> V_Valor.ToLower Then
                V_Resultado = "EL Teléfono No Es Valido"
            ElseIf V_Valor.Length <> 10 Then
                V_Resultado = "EL Teléfono No Es Valido, La Longitud No Puede Ser Distinta De 10 Caracteres"
            End If
            If validabd(V_Tipo, V_Valor) = 0 Then
                V_Resultado = "EL Telefono No Existe"
            End If
        End If
        Return V_Resultado
    End Function
    Private Function validabd(ByVal V_Tipo As String, ByVal V_Valor As String) As String
        Dim SSCommandBloqueos As New SqlCommand
        SSCommandBloqueos.CommandText = "SP_ADD_CAT_BLOQUEO_MEDIOS"
        SSCommandBloqueos.CommandType = CommandType.StoredProcedure
        SSCommandBloqueos.Parameters.Add("@V_Accion", SqlDbType.NVarChar).Value = "Validar"
        SSCommandBloqueos.Parameters.Add("@V_Cat_Bm_Tipo", SqlDbType.NVarChar).Value = RdElemento.SelectedValue
        SSCommandBloqueos.Parameters.Add("@V_Cat_Bm_Valor", SqlDbType.NVarChar).Value = RTxtValor.Text
        SSCommandBloqueos.Parameters.Add("@V_Cat_Bm_Usuario", SqlDbType.NVarChar).Value = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
        SSCommandBloqueos.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 0
        Dim DtsVarios As DataTable = Consulta_Procedure(SSCommandBloqueos, "Bloqueos")
        Return DtsVarios.Rows(0).Item(0)
    End Function
End Class
