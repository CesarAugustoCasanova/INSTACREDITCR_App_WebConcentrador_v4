Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports Db
Imports Funciones
Imports Telerik.Web.UI

Partial Class Configuracion
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
                If tmpUSUARIO Is Nothing Then
                    Response.Redirect("~/SesionExpirada.aspx")
                ElseIf tmpUSUARIO("CAT_LO_MREPORTES") = "00" Or tmpUSUARIO("CAT_LO_MREPORTES") = "10" Then
                    Response.Redirect("~/SesionExpirada.aspx")
                End If
            End If
        Catch ex As Exception
            SendMail("Page_Load", ex, "", "", "")
        End Try
    End Sub

    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Reportes", "Configuracion.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub

    Private Sub gridReportes_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles gridReportes.NeedDataSource
        Dim SSCommandDC As New SqlCommand
        SSCommandDC.CommandText = "Sp_Cat_Reportes"
        SSCommandDC.CommandType = CommandType.StoredProcedure
        SSCommandDC.Parameters.Add("@V_BANDERA", SqlDbType.NVarChar).Value = 4
        SSCommandDC.Parameters.Add("@V_Agencia", SqlDbType.NVarChar).Value = tmpUSUARIO("CAT_LO_NUM_AGENCIA")
        Dim DtsDataCenter As DataTable = Consulta_Procedure(SSCommandDC, "CATALOGO")
        gridReportes.DataSource = DtsDataCenter

    End Sub

    Private Sub gridReportes_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles gridReportes.ItemCommand
        Select Case e.CommandName
            Case "Delete"
                Dim nombreReporte As String = e.Item.Cells(4).Text
                Dim SSCommandDC As New SqlCommand
                SSCommandDC.CommandText = "Sp_Cat_Reportes"
                SSCommandDC.CommandType = CommandType.StoredProcedure
                SSCommandDC.Parameters.Add("@V_BANDERA", SqlDbType.NVarChar).Value = 12
                SSCommandDC.Parameters.Add("@V_Valor", SqlDbType.NVarChar).Value = nombreReporte
                SSCommandDC.Parameters.Add("@V_Agencia", SqlDbType.NVarChar).Value = ""
                Dim DtsDataCenter As DataTable = Consulta_Procedure(SSCommandDC, "CATALOGO")
            Case "Edit"
                Dim nombreReporte As String = e.Item.Cells(4).Text
                Session("Nuevo") = nombreReporte
                Response.Redirect("Datos.aspx")
            Case "InitInsert"
                Session("Nuevo") = "Nuevo"
                Response.Redirect("Datos.aspx")
        End Select
    End Sub
End Class