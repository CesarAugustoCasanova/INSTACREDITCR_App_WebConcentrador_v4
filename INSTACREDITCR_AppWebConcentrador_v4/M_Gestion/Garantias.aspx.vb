Imports System.Data.SqlClient
Imports System.Data
Imports Db
Imports Conexiones
Imports Funciones
Imports System.Web.Services
Imports System.Globalization
Imports Busquedas
Imports Class_Hist_Act
Imports System.Web.Script.Serialization
Imports Telerik.Web.UI
Partial Class M_Gestion_Garantias
    Inherits System.Web.UI.Page
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
    Public Property TmpUSUARIO As IDictionary
        Get
            Return CType(Session("USUARIO"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("USUARIO") = value
        End Set
    End Property
    Dim combo As String
    'Private Sub BtnGuardarC_Click(sender As Object, e As EventArgs) Handles BtnGuardarC.Click



    '    Dim oraCommanVarios As New SqlCommand("SP_GARANTIAS")
    '    oraCommanVarios.CommandType = CommandType.StoredProcedure
    '    oraCommanVarios.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 3
    '    oraCommanVarios.Parameters.Add("@V_Credito", SqlDbType.NVarChar).Value = "2"
    '    oraCommanVarios.Parameters.Add("@V_Garantia", SqlDbType.NVarChar).Value = ComboBox1.SelectedValue.ToString()
    '    oraCommanVarios.Parameters.Add("@V_NombreB", SqlDbType.NVarChar).Value = RTxtBanco.Text
    'End Sub


    Private Sub M_Gestion_Garantias_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LLENABANCO()
            llenaTodo()
        End If


    End Sub
    Sub LLENABANCO()
        ddlBanco.DataSource = SP.GARANTIA(0)
        ddlBanco.DataTextField = "Nombre"
        ddlBanco.DataValueField = "ID"
        ddlBanco.DataBind()
    End Sub
    Sub llenaTodo()
        Dim oraCommanVarios As New SqlCommand("SP_GARANTIAS")
        oraCommanVarios.CommandType = CommandType.StoredProcedure
        oraCommanVarios.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 5
        oraCommanVarios.Parameters.Add("@V_Credito", SqlDbType.NVarChar).Value = tmpCredito("PR_MC_CREDITO")
        Dim DtsVarios As DataTable = Consulta_Procedure(oraCommanVarios, "GARANTIAS")
        If DtsVarios.Rows(0)(0).ToString <> "" Then
            If DtsVarios.Rows(0)(0).ToString = "Cuenta" Then
                ComboBox1.Visible = True
                ddlBanco.Visible = True
                Label3.Visible = True
                Label3.Text = "Su Banco "
                regis.Visible = True
                ComboBox1.SelectedValue = DtsVarios.Rows(0)(0).ToString
                ddlBanco.SelectedText = DtsVarios.Rows(0)(1).ToString
            Else
                ComboBox1.Visible = True
                ComboTipo.Visible = True
                Label5.Visible = True
                Label5.Text = "Tipo de Empresa"
                RTBDependencia.Visible = True
                regis.Visible = True
                Label6.Visible = True
                Label6.Text = "Dependencia"
                ComboBox1.SelectedValue = DtsVarios.Rows(0)(0).ToString
                ComboTipo.SelectedValue = DtsVarios.Rows(0)(3).ToString
                RTBDependencia.Text = DtsVarios.Rows(0)(2).ToString
            End If


        End If
    End Sub
    Protected Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        'BtnGuardarC.Visible = False
        RTxtBanco.Visible = False
        ddlBanco.Visible = False
        ComboTipo.Visible = False
        RTBDependencia.Visible = False
        Label1.Visible = False
        'Label2.Text = "seleccione una Garantia"
        Label3.Visible = False
        Label4.Visible = False
        Label5.Visible = False
        Label6.Visible = False
        'GridGarantia.Visible = True
        'GridGarantia2.Visible = False
        'ComboBanco.DataSource = SP.GARANTIA(0)
        'ComboBanco.DataTextField = "CAT_NOMBREB"
        'ComboBanco.DataValueField = "CAT_ID"
        'ComboBanco.DataBind()

        If ComboBox1.SelectedItem.Value = "Cuenta" Then
            'GridGarantia.Visible = True
            'GridGarantia2.Visible = False
            Label3.Visible = True
            Label3.Text = "Su Banco "
            ddlBanco.Visible = True
            regis.Visible = True
            ddlBanco.ClearSelection()

            Try
                'ComboBanco.DataSource = SP.GARANTIA(0, V_ID:=TmpUSUARIO("CAT_ID"), V_NOMBREB:=TmpUSUARIO("CAT_NOMBRE"))
                'ComboBanco.DataSource = SP.GARANTIA(0)

                'ComboBanco.DataTextField = "CAT_NOMBREB"
                'ComboBanco.DataValueField = "CAT_ID"
                'ComboBanco.DataBind()
                'ComboBanco.Items.Add("Seleccione")
                'ComboBanco.SelectedValue = "Seleccione"
            Catch

            End Try


        ElseIf ComboBox1.SelectedItem.Value = "Salario" Then
            'GridGarantia2.Visible = True
            'GridGarantia.Visible = False
            Label5.Visible = True
            Label5.Text = "Tipo de Empresa"
            ComboTipo.Visible = True
            ComboTipo.ClearSelection()
        Else

            showModal(Notificacion13, "warning", "Aviso", "ERROR: No ha Seleccionado una Garantia")
            regis.Visible = "False"

        End If

    End Sub




    Protected Sub SubmitBtn_Click(sender As Object, e As EventArgs)


        If ComboBox1.SelectedItem.Value = "Cuenta" Then

            Dim oraCommanVarios As New SqlCommand("SP_GARANTIAS")

            oraCommanVarios.CommandType = CommandType.StoredProcedure

            oraCommanVarios.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 1
            oraCommanVarios.Parameters.Add("@V_Credito", SqlDbType.NVarChar).Value = tmpCredito("PR_MC_CREDITO")
            oraCommanVarios.Parameters.Add("@V_USUARIO", SqlDbType.NVarChar).Value = TmpUSUARIO("CAT_LO_USUARIO")
            oraCommanVarios.Parameters.Add("@V_Garantia", SqlDbType.NVarChar).Value = ComboBox1.SelectedValue.ToString()
            Dim nombre As String = ddlBanco.SelectedText = "Otro"
            If ddlBanco.SelectedText <> "" Then
                If ddlBanco.SelectedText = "Otro" Then
                    oraCommanVarios.Parameters.Add("@V_NombreB", SqlDbType.NVarChar).Value = RTxtBanco.Text
                    If RTxtBanco.Text <> "" Then
                        Dim DtsVarios As DataTable = Consulta_Procedure(oraCommanVarios, "GARANTIAS")
                        showModal(Notificacion13, "warning", "Aviso", "La Garantia se Registro Corretamente")
                        ddlBanco.ClearSelection()
                        ComboBox1.ClearSelection()
                        ddlBanco.Visible = "false"

                        RTBDependencia.Visible = "False"
                        RTxtBanco.Text = ""
                        RTxtBanco.Visible = "false"
                        Label1.Visible = "False"
                        ' Label2.Text = "seleccione una Garantia"
                        Label3.Visible = "false"
                        Label4.Visible = "False"
                        Label5.Visible = "False"
                        Label6.Visible = "False"
                        regis.Visible = "False"
                        DtsVarios.Select("")
                        'GridGarantia.Rebind()
                        llenaTodo()
                    Else
                        showModal(Notificacion13, "warning", "Aviso", "Por Favor Ingrese el Nombre del Banco")
                        Label4.Visible = "true"

                    End If
                Else
                    oraCommanVarios.Parameters.Add("@V_NombreB", SqlDbType.NVarChar).Value = ddlBanco.SelectedText
                    Dim DtsVarios As DataTable = Consulta_Procedure(oraCommanVarios, "GARANTIAS")
                    showModal(Notificacion13, "warning", "Aviso", "La Garantia se Registro Correctamente")
                    ddlBanco.ClearSelection()
                    ComboBox1.ClearSelection()
                    ddlBanco.Visible = "false"
                    RTBDependencia.Visible = "False"
                    Label1.Visible = "False"
                    ' Label2.Text = "seleccione una Garantia"
                    Label3.Visible = "false"
                    Label5.Visible = "False"
                    Label6.Visible = "False"
                    regis.Visible = "False"
                    Label4.Visible = "False"
                    RTxtBanco.Visible = "False"
                    llenaTodo()
                End If
            Else
                '  Label1.Visible = True
                '    Label1.Text = "ERROR: NO HA SELECCIONADO UN BANCO"
                showModal(Notificacion13, "warning", "Aviso", "ERROR: No ha Seleccionado un Banco")
                ddlBanco.ClearSelection()
                Label1.Visible = "False"
                'Label2.Text = "seleccione una Garantia"
                Label3.Visible = "true"
                Label4.Visible = "true"
                Label5.Visible = "False"
                Label6.Visible = "False"



            End If



        ElseIf ComboBox1.SelectedItem.Value = "Salario" Then

            Dim oraCommanVarios As New SqlCommand("SP_GARANTIAS")
            oraCommanVarios.CommandType = CommandType.StoredProcedure
            oraCommanVarios.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 2
            oraCommanVarios.Parameters.Add("@V_Credito", SqlDbType.NVarChar).Value = tmpCredito("PR_MC_CREDITO")
            oraCommanVarios.Parameters.Add("@V_USUARIO", SqlDbType.NVarChar).Value = TmpUSUARIO("CAT_LO_USUARIO")
            oraCommanVarios.Parameters.Add("@V_Garantia", SqlDbType.NVarChar).Value = ComboBox1.SelectedValue.ToString()
            oraCommanVarios.Parameters.Add("@V_Empresa", SqlDbType.NVarChar).Value = ComboTipo.SelectedValue.ToString()
            oraCommanVarios.Parameters.Add("@V_Dependencia", SqlDbType.NVarChar).Value = RTBDependencia.Text
            Try
                If ComboTipo.SelectedText <> "SELECCIONE" Then
                    If RTBDependencia.Text <> "" Then
                        Dim DtsVarios As DataTable = Consulta_Procedure(oraCommanVarios, "CYR")
                        showModal(noti113, "warning", "Aviso", "La Garantia se Registro Correctamente")
                        ddlBanco.ClearSelection()
                        ComboBox1.ClearSelection()
                        ComboTipo.ClearSelection()
                        RTBDependencia.Text = ""
                        ddlBanco.Visible = "false"
                        ComboTipo.Visible = "false"
                        RTBDependencia.Visible = "False"
                        Label1.Visible = "False"
                        ' Label2.Text = "seleccione una Garantia"
                        Label3.Visible = "False"
                        Label4.Visible = "False"
                        Label5.Visible = "False"
                        Label6.Visible = "False"
                        regis.Visible = "False"
                        llenaTodo()
                        'GridGarantia2.Rebind()
                    Else
                        showModal(noti113, "warning", "Aviso", "Por Favor Ingrese una Dependencia")
                    End If
                Else
                    showModal(noti113, "warning", "Aviso", "ERROR: Seleccione un tipo de Empresa")
                End If
            Catch ex As Exception
                showModal(noti113, "warning", "Aviso", ex.Message)
            End Try
        Else

            showModal(noti113, "warning", "Aviso", "El credito ya Cuenta con una Garantia")
            ddlBanco.ClearSelection()
            ComboBox1.ClearSelection()
            ComboTipo.ClearSelection()
            RTBDependencia.Text = ""
            ddlBanco.Visible = "false"
            ComboTipo.Visible = "false"
            RTBDependencia.Visible = "False"
            Label1.Visible = "False"
            ' Label2.Text = "seleccione una Garantia"
            Label3.Visible = "False"
            Label4.Visible = "False"
            Label5.Visible = "False"
            Label6.Visible = "False"
            regis.Visible = "False"

        End If
        LLENABANCO()
    End Sub

    Protected Sub ddlBanco_SelectedIndexChanged(sender As Object, e As DropDownListEventArgs) Handles ddlBanco.SelectedIndexChanged
        Session("CAT_BANCO") = ddlBanco.SelectedText
        ' ddlBanco.Items.Add("Otro")
        If ddlBanco.SelectedText = "Otro" Then
            Label4.Visible = True
            Label4.Text = "Ingrese su Banco"
            RTxtBanco.Visible = True

        Else

        End If
    End Sub

    Protected Sub ComboTipo_SelectedIndexChanged(sender As Object, e As DropDownListEventArgs) Handles ComboTipo.SelectedIndexChanged

        Label6.Visible = True
        Label6.Text = "Ingrese Dependencia"
        RTBDependencia.Visible = True
        regis.Visible = True
    End Sub

    'Private Sub GridGarantia_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles GridGarantia.NeedDataSource

    '    Dim SSCommandUsuario As New SqlCommand
    '    SSCommandUsuario.CommandText = "SP_GARANTIAS"
    '   ' GridGarantia.DataSource = SP.GARANTIA(bandera:=3)
    'End Sub

    'Private Sub GridGarantia2_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles GridGarantia2.NeedDataSource
    '    Dim SSCommandUsuario As New SqlCommand
    '    SSCommandUsuario.CommandText = "SP_GARANTIAS"
    '    GridGarantia2.DataSource = SP.GARANTIA(bandera:=4)

    'End Sub

    'Private Sub GridGarantia_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles GridGarantia.ItemCommand

    'End Sub
End Class
