Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Collections.Generic
Imports Db
Imports System.Data.SqlClient
Imports System.Web.SessionState.HttpSessionState

Public Class WorkItemsHelper

    Public Sub New()
    End Sub

    'Loads the Work items to the Application Scope
    Public Shared Sub LoadWorkItems()
        HttpContext.Current.Session("WorkItems") = WorkItemsHelper.GetWorkItems()
    End Sub
    Public Shared Function GetWorkItems() As List(Of WorkItem)

        If HttpContext.Current.Session("WorkItems") Is Nothing Then
            Dim workItems As New List(Of WorkItem)()
            Dim oraCommanCodigos As New SqlCommand
            oraCommanCodigos.CommandText = "Sp_Ponderar"
            oraCommanCodigos.CommandType = CommandType.StoredProcedure
            oraCommanCodigos.Parameters.Add("V_Producto", SqlDbType.Decimal).Value = 0
            oraCommanCodigos.Parameters.Add("V_Id", SqlDbType.Decimal).Value = 0
            oraCommanCodigos.Parameters.Add("V_Orden", SqlDbType.Decimal).Value = 0
            oraCommanCodigos.Parameters.Add("V_Bandera", SqlDbType.Decimal).Value = 1
            Dim DtsVarios As DataTable = Consulta_Procedure(oraCommanCodigos, "Codigos")
            For Each row As DataRow In DtsVarios.Rows
                workItems.Add(New WorkItem(Integer.Parse(row("Id").ToString()), row("Codigo").ToString(), row("Descripcion").ToString(), Integer.Parse(row("Ponderacion").ToString())))
            Next
            Return workItems
        End If
        Return DirectCast(HttpContext.Current.Session("WorkItems"), List(Of WorkItem))
    End Function
    'Save the workItems to the ApplicationScope
    Public Shared Sub SaveWorkItems(ByVal workItems As List(Of WorkItem))
        HttpContext.Current.Session("WorkItems") = workItems
    End Sub

End Class
