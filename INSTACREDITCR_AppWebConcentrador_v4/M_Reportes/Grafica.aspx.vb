Imports System.Data.SqlClient
Imports System.Data
Imports AjaxControlToolkit
Imports System.Web.UI.WebControls.Label
Imports Conexiones
Imports Funciones
Imports Db
Imports Telerik.Web.UI

Partial Class Grafica
    Inherits System.Web.UI.Page
    Public Property tmpUSUARIO As IDictionary
        Get
            Return CType(Session("USUARIO"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("USUARIO") = value
        End Set
    End Property

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                LlenarDdls()
                If Session("Nuevo") <> "Nuevo" Then
                    LlenarCampos()
                End If
            End If
        Catch ex As Exception
            'SendMail("Page_Load", ex, "", "", HidenUrs.Value)
        End Try
    End Sub
    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Reportes", "Grafica.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub

    Private Sub LlenarDdls()
        Dim SSCommandF As New SqlCommand
        SSCommandF.CommandText = "Sp_Cat_Reportes"
        SSCommandF.CommandType = CommandType.StoredProcedure
        SSCommandF.Parameters.Add("@V_BANDERA", SqlDbType.NVarChar).Value = 3
        SSCommandF.Parameters.Add("@V_Agencia", SqlDbType.NVarChar).Value = tmpUSUARIO("CAT_LO_NUM_AGENCIA")
        SSCommandF.Parameters.Add("@V_Valor", SqlDbType.NVarChar).Value = Session("ID")
        Dim DtsCatalogos As DataTable = Consulta_Procedure(SSCommandF, "CATALOGO")
        For Each row In DtsCatalogos.Rows
            Dim item1 As New DropDownListItem(row("Text").ToString(), row("Value").ToString())
            item1.Attributes.Add("Tabla", row("Tabla").ToString())
            ddlCampoAgrupar.Items.Add(item1)

            Dim item2 As New DropDownListItem(row("Text").ToString(), row("Value").ToString())
            item1.Attributes.Add("Tabla", row("Tabla").ToString())
            ddlSumarContar.Items.Add(item2)
        Next
        ddlSumarContar.DataBind()
        ddlCampoAgrupar.DataBind()
    End Sub

    Private Sub LlenarCampos()
        Dim SSCommandId As New SqlCommand
        SSCommandId.CommandText = "Sp_Add_Cat_Reporte_Grafica"
        SSCommandId.CommandType = CommandType.StoredProcedure
        SSCommandId.Parameters.Add("@V_Cat_Reg_Id", SqlDbType.Decimal).Value = Session("ID")
        SSCommandId.Parameters.Add("@V_Cat_Reg_Tipo", SqlDbType.NVarChar).Value = ""
        SSCommandId.Parameters.Add("@V_Cat_Reg_CaampoAdesc", SqlDbType.NVarChar).Value = ""
        SSCommandId.Parameters.Add("@V_Cat_Reg_Tabla_A", SqlDbType.NVarChar).Value = ""
        SSCommandId.Parameters.Add("@V_Cat_Reg_CaampoCdesc", SqlDbType.NVarChar).Value = ""
        SSCommandId.Parameters.Add("@V_Cat_Reg_Tabla_CS", SqlDbType.NVarChar).Value = ""
        SSCommandId.Parameters.Add("@V_CAT_REG_OPCION", SqlDbType.NVarChar).Value = ""
        SSCommandId.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = 2
        Dim DtsId As DataTable = Consulta_Procedure(SSCommandId, "Reporte")

        'ddlCampoAgrupar.SelectedText = DtsId.Rows(0).Item("CAT_REG_CAAMPOADESC").ToString
        'ddlSumarContar.SelectedText = DtsId.Rows(0).Item("CAT_REG_CAAMPOCDESC").ToString
        Select Case DtsId.Rows(0).Item("Cat_Reg_Tipo").ToString
            Case "Area"
                btnAreaChart.Checked = True
            Case "BarraLateral"
                btnBarChart.Checked = True
            Case "Barra"
                btnColumnChart.Checked = True
            Case "Dona"
                btnDonutChart.Checked = True
            Case "Linea"
                btnLineChart.Checked = True
            Case "Circular"
                btnPieChart.Checked = True
        End Select
        btnSumarContar.Value = DtsId.Rows(0).Item("CAT_REG_OPCION").ToString
    End Sub

    Private Function getSelectedChart() As String
        Dim selected As String = ""
        If btnAreaChart.Checked Then
            selected = "Area"
        ElseIf btnBarChart.Checked Then
            selected = "BarraLateral"
        ElseIf btnColumnChart.Checked Then
            selected = "Barra"
        ElseIf btnDonutChart.Checked Then
            selected = "Dona"
        ElseIf btnLineChart.Checked Then
            selected = "Linea"
        ElseIf btnPieChart.Checked Then
            selected = "Circular"
        End If
        Return selected
    End Function

    Private Sub btnTerminar_Click(sender As Object, e As EventArgs) Handles btnTerminar.Click
        Dim selectedChart = getSelectedChart()
        Dim selectedGroup = ddlCampoAgrupar.SelectedItem
        Dim selectedSum = ddlSumarContar.SelectedItem
        If selectedChart = "" Then
            showModal(RadNotification1, "deny", "Error", "Seleccione Un Tipo De Grafica")
        ElseIf selectedGroup.Text = "" Then
            showModal(RadNotification1, "deny", "Error", "Seleccione El Campo A Agrupar")
        ElseIf selectedSum.Text = "" Then
            showModal(RadNotification1, "deny", "Error", "Seleccione El Campo A Contar/Sumar")
        Else
            Dim SSCommandId As New SqlCommand
            SSCommandId.CommandText = "Sp_Add_Cat_Reporte_Grafica"
            SSCommandId.CommandType = CommandType.StoredProcedure
            SSCommandId.Parameters.Add("@V_Cat_Reg_Id", SqlDbType.Decimal).Value = Session("ID")
            SSCommandId.Parameters.Add("@V_Cat_Reg_Tipo", SqlDbType.NVarChar).Value = selectedChart
            SSCommandId.Parameters.Add("@V_Cat_Reg_CaampoAdesc", SqlDbType.NVarChar).Value = selectedGroup.Text
            SSCommandId.Parameters.Add("@V_Cat_Reg_Tabla_A", SqlDbType.NVarChar).Value = selectedGroup.Attributes("Tabla")
            SSCommandId.Parameters.Add("@V_Cat_Reg_CaampoCdesc", SqlDbType.NVarChar).Value = selectedSum.Text
            SSCommandId.Parameters.Add("@V_Cat_Reg_Tabla_CS", SqlDbType.NVarChar).Value = selectedSum.Attributes("Tabla")
            SSCommandId.Parameters.Add("@V_CAT_REG_OPCION", SqlDbType.NVarChar).Value = IIf(btnSumarContar.Text = "Contar", "Count", "Sum")
            SSCommandId.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = 1
            Dim DtsId As DataTable = Consulta_Procedure(SSCommandId, "Reporte")
            Response.Redirect("Inicio.aspx")
        End If
    End Sub
End Class