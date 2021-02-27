<%@ WebHandler Language="VB" Class="Imagen" %>

Imports System
Imports System.Web
Imports System.Data
Imports db

Public Class Imagen : Implements IHttpHandler
    
    Public Sub ProcessRequest(context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Response.ContentType = "image/jpeg"
        Dim cuenta As String = context.Request.QueryString("cuenta")
        Dim visita As String = context.Request.QueryString("visita")
        Dim hora As String = context.Request.QueryString("hora")
        Dim query As String = "select HIST_VI_IMAGEN from HIST_VISITAS where HIST_VI_CREDITO='" & cuenta & "' and (HIST_VI_DTEVISITA)=to_date('" & visita & "','dd/mm/yyyy HH24:MI:SS')  and HIST_VI_IMAGEN is not null and rownum=1"
        Dim oDataset1 As DataTable = Consulta(query, "IMAGEN  ")
        Dim imgBytes As Byte() = DirectCast(oDataset1.Rows(0)("HIST_VI_IMAGEN"), Byte())
        context.Response.BinaryWrite(imgBytes)
    End Sub

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property
End Class