Imports System.Data
Imports System.Data.SqlClient
Imports Conexiones
Imports Db
Imports Funciones
Imports Telerik.Web.UI

Partial Class Catalogo_Mail_v2
    Inherits System.Web.UI.Page

    Public Property TmpUSUARIO As IDictionary
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

    <Services.WebMethod>
    Public Shared Function GetResults(data As String) As String
        Dim etiquetas As String = getEtiquetas(data)
        Return EliminarEtiquetasRepetidas(etiquetas)
    End Function

    Private Shared Function getEtiquetas(data As String) As String
        Dim etiquetas As String = ""
        Dim chars() As Char = data.ToCharArray
        Dim etiquetaComienza As Integer = 0
        Dim etiquetaTermina As Integer = 0
        For Each caracter As Char In chars
            Select Case caracter
                Case "<"
                    etiquetaComienza += 1
                    If etiquetaComienza > 2 Then
                        etiquetaComienza -= 1
                    End If
                Case ">"
                    etiquetaTermina += 1
                    If etiquetaTermina = 2 Then
                        etiquetaTermina = 0
                        etiquetaComienza = 0
                        etiquetas &= ","
                    End If
                Case Else
                    If etiquetaTermina <> 2 Then
                        etiquetaTermina = 0
                    End If

                    If etiquetaComienza <> 2 Then
                        etiquetaComienza = 0
                    Else
                        etiquetas &= caracter
                    End If
            End Select
        Next
        Try
            etiquetas = etiquetas.Substring(0, etiquetas.Length - 1)
        Catch ex As Exception
        End Try
        Return etiquetas
    End Function

    Private Shared Function EliminarEtiquetasRepetidas(data As String) As String
        Dim hash As New Hashtable
        Dim etiquetas As String = ""
        For Each etiqueta As String In data.Split(",")
            If Not hash.ContainsKey(etiqueta) Then
                hash.Add(etiqueta, "gg")
                etiquetas &= etiqueta & ","
            End If
        Next
        Try
            etiquetas = etiquetas.Substring(0, etiquetas.Length - 1)
        Catch ex As Exception
        End Try
        Return etiquetas
    End Function
    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Administrador", "Catalogo_Mailv2.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim Usr As String = TmpUSUARIO("CAT_LO_USUARIO")
        Catch ex As Exception
            OffLine(HidenUrs.Value)
            AUDITORIA(HidenUrs.Value, "Administrador", "Catalogo_Mailv2", "", "Page_Load", "", Request.ServerVariables("REMOTE_ADDR"), Request.ServerVariables("REMOTE_HOST"))
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/SesionExpirada.aspx")
        End Try

        Try
            If Not IsPostBack Then
                Dim Facultad As String = 1
                If Facultad = "0" Then
                    OffLine(HidenUrs.Value)
                    Session.Clear()
                    Session.Abandon()
                    Response.Redirect("~/SesionExpirada.aspx")
                End If
                HidenUrs.Value = TmpUSUARIO("CAT_LO_USUARIO")
                'Llenar(0, "", "", "", "", 0)
            End If

        Catch ex As Exception
            SendMail("Page_Load", ex, "", "", HidenUrs.Value)
        End Try
    End Sub

    Sub Llenar(V_Cat_Ec_Id As Integer, V_Cat_Ec_Descripcion As String, V_Cat_Ec_Valor As String, V_Cat_Ec_Tabla As String, V_Cat_Ec_Camporeal As String, V_Bandera As Integer)
        Dim SSCommand As New SqlCommand("SP_ADD_CAT_ETIQUETAS_CORREO")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Cat_Ec_Id", SqlDbType.NVarChar).Value = V_Cat_Ec_Id
        SSCommand.Parameters.Add("@V_Cat_Ec_Descripcion", SqlDbType.NVarChar).Value = V_Cat_Ec_Descripcion
        SSCommand.Parameters.Add("@V_Cat_Ec_Valor", SqlDbType.NVarChar).Value = V_Cat_Ec_Valor
        SSCommand.Parameters.Add("@V_Cat_Ec_Tabla", SqlDbType.NVarChar).Value = V_Cat_Ec_Tabla
        SSCommand.Parameters.Add("@V_Cat_Ec_Camporeal", SqlDbType.NVarChar).Value = V_Cat_Ec_Camporeal
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = V_Bandera
        Dim DtsVarios As DataTable = Consulta_Procedure(SSCommand, "SP_ADD_CAT_ETIQUETAS_CORREO")

    End Sub

    Protected Sub Mensaje(ByVal v_mensaje As String)
        RadWindowManager1.RadAlert(v_mensaje, 400, 150, Nothing, Nothing)
    End Sub

    Protected Function DirectorioImg(ByVal V_Valor1 As String) As String
        Dim aux As Integer
        Dim diagonal As Integer
        Dim archivo As String
        For i As Integer = 0 To V_Valor1.Length - 1
            If i + 4 <= V_Valor1.Length Then
                If V_Valor1.Substring(i, 4) = "src=" Then
                    aux = InStr(i + 6, V_Valor1, """")
                    diagonal = InStrRev(V_Valor1, "/", aux)
                    archivo = V_Valor1.Substring(diagonal, aux - diagonal - 1)
                    'V_Valor1 = V_Valor1.Replace(V_Valor1.Substring(i, aux - i), "src=""https://mcnoc.com.mx/ATTImagenes/" & archivo & """")
                    V_Valor1 = V_Valor1.Replace(V_Valor1.Substring(i, aux - i), "src=""https://pruebasmc.com.mx/ATTImagenes/" & archivo & """")
                End If
            End If
        Next
        Return V_Valor1
    End Function

    Private Sub gridEtiquetas_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles gridEtiquetas.ItemCommand
        If e.CommandName = "PerformInsert" Then
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim hash As New Hashtable
            hash("Tabla") = CType(MyUserControl.FindControl("comboTablas"), RadComboBox).SelectedValue
            hash("CampoVal") = CType(MyUserControl.FindControl("comboCampos"), RadComboBox).SelectedValue
            hash("CampoText") = CType(MyUserControl.FindControl("comboCampos"), RadComboBox).SelectedItem.Text
            If hash("CampoText").ToString.Length >= 50 Then
                hash("CampoText") = hash("CampoText").ToString.Substring(0, 46) & "..."
            End If
            hash("Etiqueta") = CType(MyUserControl.FindControl("txtEtiqueta"), RadTextBox).Text
            Dim SSCommand As New SqlCommand("SP_ADD_CAT_ETIQUETAS_CORREO")
            SSCommand.CommandType = CommandType.StoredProcedure
            SSCommand.Parameters.Add("@V_Cat_Ec_Id", SqlDbType.NVarChar).Value = 0
            SSCommand.Parameters.Add("@V_Cat_Ec_Descripcion", SqlDbType.NVarChar).Value = hash("Etiqueta")
            SSCommand.Parameters.Add("@V_Cat_Ec_Valor", SqlDbType.NVarChar).Value = hash("CampoText")
            SSCommand.Parameters.Add("@V_Cat_Ec_Tabla", SqlDbType.NVarChar).Value = hash("Tabla")
            SSCommand.Parameters.Add("@V_Cat_Ec_Camporeal", SqlDbType.NVarChar).Value = hash("CampoVal")
            SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 4
            Dim DtsVarios As DataTable = Consulta_Procedure(SSCommand, "Fuente")
            If DtsVarios.Rows(0).Item("Cuantos") = 0 Then
                Llenar(1, hash("Etiqueta"), hash("CampoText"), hash("Tabla"), hash("CampoVal"), 1)
                Mensaje("Fuente de información creada")
            Else
                Mensaje("ERROR, EL NOMBRE O CAMPO DE LA ETIQUETA YA SE ENCUENTRA REGISTRADO")
                e.Canceled = True
            End If
        ElseIf e.CommandName = "Update" Then
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim hash As New Hashtable
            hash("Tabla") = CType(MyUserControl.FindControl("comboTablas"), RadComboBox).SelectedValue
            hash("CampoVal") = CType(MyUserControl.FindControl("comboCampos"), RadComboBox).SelectedValue
            hash("CampoText") = CType(MyUserControl.FindControl("comboCampos"), RadComboBox).SelectedItem.Text
            If hash("CampoText").ToString.Length >= 50 Then
                hash("CampoText") = hash("CampoText").ToString.Substring(0, 46) & "..."
            End If
            hash("Etiqueta") = CType(MyUserControl.FindControl("txtEtiqueta"), RadTextBox).Text
            hash("ID") = CType(MyUserControl.FindControl("txtID"), RadTextBox).Text
            Dim SSCommand As New SqlCommand("SP_ADD_CAT_ETIQUETAS_CORREO")
            SSCommand.CommandType = CommandType.StoredProcedure
            SSCommand.Parameters.Add("@V_Cat_Ec_Id", SqlDbType.NVarChar).Value = hash("ID")
            SSCommand.Parameters.Add("@V_Cat_Ec_Descripcion", SqlDbType.NVarChar).Value = hash("Etiqueta")
            SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 6
            Dim DtsVarios As DataTable = Consulta_Procedure(SSCommand, "Fuente")
            If DtsVarios.Rows(0).Item("Cuantos") = 0 Then
                Llenar(hash("ID"), hash("Etiqueta"), hash("CampoText"), hash("Tabla"), hash("CampoVal"), 2)
                Mensaje("Se Han Guardado Los Cambios")
            Else
                Mensaje("ERROR EL NOMBRE DE LA ETIQUEDA QUE DESEA ACTUALIZAR YA SE ENCUENTRA REGISTRADA")
                e.Canceled = True
            End If
        ElseIf e.CommandName = "Edit" Then
            Session("Edit") = True
        ElseIf e.CommandName = "Delete" Then
            Try
                Dim item As GridItem = CType(e.Item, GridItem)
                Dim idcell As GridTableCell = CType(item.Controls(4), GridTableCell)
                Llenar(Integer.Parse(idcell.Text), "", "", "", "", 3)
                Mensaje("Se ha eliminado la etiqueta")
                'Llenar(0, "", "", "", "", 0)
                'Limpiar()
            Catch ex As Exception
                SendMail("BtnEliminar_Click", ex, "", "", HidenUrs.Value)
            End Try
        End If
    End Sub

    Private Sub gridEtiquetas_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles gridEtiquetas.NeedDataSource
        Dim SSCommand As New SqlCommand("SP_ADD_CAT_ETIQUETAS_CORREO")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 0
        Dim DtsVarios As DataTable = Consulta_Procedure(SSCommand, "SP_ADD_CAT_ETIQUETAS_CORREO")

        gridEtiquetas.DataSource = DtsVarios
    End Sub

    Private Sub gridEtiquetas_PreRender(sender As Object, e As EventArgs) Handles gridEtiquetas.PreRender
        If Not IsPostBack Then
            Try
                gridEtiquetas.MasterTableView.GetColumn("Tabla").Visible = False
                gridEtiquetas.MasterTableView.GetColumn("Campo").Visible = False
                gridEtiquetas.Rebind()
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub gridPlantillas_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles gridPlantillas.NeedDataSource
        Dim SSCommand As New SqlCommand("SP_ADD_CAT_PLANTILLAS_CORREO")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Cat_Pc_Id", SqlDbType.NVarChar).Value = 0
        SSCommand.Parameters.Add("@V_Cat_Pc_Nombre", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@V_CAT_PC_CONFIGURACION", SqlDbType.NVarChar).Value = DBNull.Value
        SSCommand.Parameters.Add("@V_CAT_PC_PRODUCTO", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 4
        Dim DtsCorreo As DataTable = Consulta_Procedure(SSCommand, "Correo")

        gridPlantillas.DataSource = DtsCorreo
    End Sub

    Private Sub gridPlantillas_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles gridPlantillas.ItemCommand
        If e.CommandName = "InitInsert" Then
            Session("InitInsert") = True
        ElseIf e.CommandName = "Edit" Then
            Session("Edit") = True
        ElseIf e.CommandName = "Update" Then
            Try
                Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
                Dim hash As New Hashtable
                hash("ID") = CType(MyUserControl.FindControl("txtID"), RadTextBox).Text
                hash("Nombre") = CType(MyUserControl.FindControl("txtNombre"), RadTextBox).Text
                hash("Editor") = CType(MyUserControl.FindControl("editor"), RadEditor).Content

                Dim SSCommand As New SqlCommand("SP_ADD_CAT_PLANTILLAS_CORREO")
                SSCommand.CommandType = CommandType.StoredProcedure
                SSCommand.Parameters.Add("@V_Cat_Pc_Id", SqlDbType.NVarChar).Value = Val(hash("ID"))
                SSCommand.Parameters.Add("@V_Cat_Pc_Nombre", SqlDbType.NVarChar).Value = hash("Nombre")
                SSCommand.Parameters.Add("@V_CAT_PC_CONFIGURACION", SqlDbType.NVarChar).Value = hash("Editor")
                SSCommand.Parameters.Add("@V_CAT_PC_PRODUCTO", SqlDbType.NVarChar).Value = Session("CAT_LO_PRODUCTO") ' CType(Session("USUARIOADMIN"), USUARIO).CAT_LO_PRODUCTO
                SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 2
                Dim DtsCorreo As DataTable = Consulta_Procedure(SSCommand, "Correo")

                Mensaje(DtsCorreo.Rows(0).Item("Correo"))
                Class_Auditoria.GuardarValorAuditoria(TmpUSUARIO("CAT_LO_USUARIO"), "CAT Avisos", "Plantilla " & Val(hash("ID")) & " actualizada")
            Catch ex As Exception
                Mensaje(ex.Message)
                e.Canceled = True
            End Try
        ElseIf e.CommandName = "PerformInsert" Then
            Try
                Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
                Dim hash As New Hashtable
                hash("Nombre") = CType(MyUserControl.FindControl("txtNombre"), RadTextBox).Text
                hash("Editor") = CType(MyUserControl.FindControl("editor"), RadEditor).Content

                Dim SSCommand As New SqlCommand("SP_ADD_CAT_PLANTILLAS_CORREO")
                SSCommand.CommandType = CommandType.StoredProcedure
                SSCommand.Parameters.Add("@V_Cat_Pc_Id", SqlDbType.NVarChar).Value = 0
                SSCommand.Parameters.Add("@V_Cat_Pc_Nombre", SqlDbType.NVarChar).Value = hash("Nombre")
                SSCommand.Parameters.Add("@V_CAT_PC_CONFIGURACION", SqlDbType.NVarChar).Value = hash("Editor")
                SSCommand.Parameters.Add("@V_CAT_PC_PRODUCTO", SqlDbType.NVarChar).Value = Session("CAT_LO_PRODUCTO") ' CType(Session("USUARIO"), USUARIO).CAT_LO_PRODUCTO
                SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 1
                Dim DtsCorreo As DataTable = Consulta_Procedure(SSCommand, "Correo")

                Mensaje(DtsCorreo.Rows(0).Item("Correo"))
                Class_Auditoria.GuardarValorAuditoria(TmpUSUARIO("CAT_LO_USUARIO"), "CAT Avisos", "Plantilla " & Val(hash("ID")) & " insertada")
            Catch ex As Exception
                Mensaje(ex.Message)
                e.Canceled = True
            End Try
        ElseIf e.CommandName = "Delete" Then
            Try
                Dim item As GridItem = CType(e.Item, GridItem)
                Dim idcell As GridTableCell = CType(item.Controls(4), GridTableCell)
                Dim SSCommand As New SqlCommand("SP_ADD_CAT_PLANTILLAS_CORREO")
                SSCommand.CommandType = CommandType.StoredProcedure
                SSCommand.Parameters.Add("@V_Cat_Pc_Id", SqlDbType.NVarChar).Value = Val(idcell.Text)
                SSCommand.Parameters.Add("@V_Bandera", SqlDbType.Int).Value = 3
                Dim DtsCorreo As DataTable = Consulta_Procedure(SSCommand, "Correo")
                Mensaje(DtsCorreo.Rows(0).Item("Correo"))
                Class_Auditoria.GuardarValorAuditoria(TmpUSUARIO("CAT_LO_USUARIO"), "CAT Avisos", "Plantilla " & Val(idcell.Text) & " eliminda")
            Catch ex As Exception
                Mensaje(ex.Message)
            End Try
        End If
    End Sub
End Class
