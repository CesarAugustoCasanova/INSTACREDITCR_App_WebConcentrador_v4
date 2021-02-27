Imports System.Data
Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports Db
Imports System.Globalization
Imports System.IO
Imports Funciones
Imports Telerik.Web.UI
Partial Class Inventarios
    Inherits System.Web.UI.Page
    Public Property tmpCredito As IDictionary
        Get
            Return CType(Session("Credito"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("Credito") = value
        End Set
    End Property

    Private Sub Garantias_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        Try
            Dim tabla As DataTable = Class_Hist_Garantias.LlenarInfoInventarios(tmpCredito("PR_MC_CREDITO"), 0)
            Dim generator As New RadAjaxPanelGenerator(4, 3)
            Dim pnl As RadAjaxPanel = generator.generarPanel(tabla)
            form1.Controls.Add(pnl)
        Catch ex As Exception

        End Try

    End Sub
End Class
