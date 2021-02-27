Imports System.Data.OracleClient
Imports System.Data
Imports Funciones
Imports System.Web.Services
Imports Db
Imports System.Diagnostics
Imports System.IO
Partial Class DescargaAPK
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim App = "E:\Cargas\Gabssa\AppMovil\gabssa.apk"
            Dim App2 = "E:\Cargas\Gabssa\AppMovil\gabssa2.apk"
            Dim AppPDF = "E:\Cargas\Gabssa\AppMovil\Manual.pdf"
            Dim AppPDF2 = "E:\Cargas\Gabssa\AppMovil\Manual2.pdf"


            lbmsjapk.Text = "Actualización: " & File.GetLastWriteTime(App).ToLocalTime
            lbmsjapk2.Text = "Actualización: " & File.GetLastWriteTime(App2).ToLocalTime
            lbmsjmanual.Text = "Actualización: " & File.GetCreationTime(AppPDF).ToLocalTime
            lbmsjmanual0.Text = "Actualización: " & File.GetCreationTime(AppPDF2).ToLocalTime
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ImgApp_Click(sender As Object, e As ImageClickEventArgs) Handles ImgApp.Click
        Dim App = "E:\Cargas\Gabssa\AppMovil\gabssa.apk"
        If File.Exists(App) Then
            Dim ioflujo As FileInfo = New FileInfo(App)
            Response.Clear()
            Response.AddHeader("Content-Disposition", "attachment; filename=" + ioflujo.Name)
            Response.AddHeader("Content-Length", ioflujo.Length.ToString())
            Response.ContentType = "application/octet-stream"
            Response.WriteFile(App)
            Response.End()
        End If
    End Sub


    Protected Sub ImageManual_Click(sender As Object, e As ImageClickEventArgs) Handles ImageManual.Click
        Dim App = "E:\Cargas\Gabssa\AppMovil\Manual.pdf"
        If File.Exists(App) Then
            Dim ioflujo As FileInfo = New FileInfo(App)
            Response.Clear()
            Response.AddHeader("Content-Disposition", "attachment; filename=" + ioflujo.Name)
            Response.AddHeader("Content-Length", ioflujo.Length.ToString())
            Response.ContentType = "application/octet-stream"
            Response.WriteFile(App)
            Response.End()
        End If
    End Sub
    Protected Sub ImageManual0_Click(sender As Object, e As ImageClickEventArgs) Handles ImageManual0.Click
        Dim App = "E:\Cargas\Gabssa\AppMovil\Manual2.pdf"
        If File.Exists(App) Then
            Dim ioflujo As FileInfo = New FileInfo(App)
            Response.Clear()
            Response.AddHeader("Content-Disposition", "attachment; filename=" + ioflujo.Name)
            Response.AddHeader("Content-Length", ioflujo.Length.ToString())
            Response.ContentType = "application/octet-stream"
            Response.WriteFile(App)
            Response.End()
        End If
    End Sub
    Protected Sub ImgApp2_Click(sender As Object, e As ImageClickEventArgs) Handles ImgApp2.Click
        Dim App = "E:\Cargas\Gabssa\AppMovil\gabssa2.apk"
        If File.Exists(App) Then
            Dim ioflujo As FileInfo = New FileInfo(App)
            Response.Clear()
            Response.AddHeader("Content-Disposition", "attachment; filename=" + ioflujo.Name)
            Response.AddHeader("Content-Length", ioflujo.Length.ToString())
            Response.ContentType = "application/octet-stream"
            Response.WriteFile(App)
            Response.End()
        End If
    End Sub
End Class
