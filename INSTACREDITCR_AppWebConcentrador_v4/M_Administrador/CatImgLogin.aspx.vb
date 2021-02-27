Imports System
Imports System.Web
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports System.Net
Imports Db
Imports Funciones
Imports Class_Documentos
Imports Conexiones
Imports Telerik.Web.UI
Partial Class M_Administrador_CatImgLogin
    Inherits System.Web.UI.Page
    Private Sub M_Administrador_CatImgLogin_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim dtab As DataTable = SP.IMAGENES_LOGIN(0, 0)
        llenar(dtab)
    End Sub

    Private Sub Upload1_FileUploaded(sender As Object, e As FileUploadedEventArgs) Handles Upload1.FileUploaded, Upload2.FileUploaded, Upload3.FileUploaded, Upload4.FileUploaded
        Dim identi As Integer = sender.id.ToString.Substring(6)
        Dim subido As UploadedFile = CType(sender, RadAsyncUpload).UploadedFiles(0)
        Dim subidoBytes As Byte() = New Byte(subido.InputStream.Length - 1) {}
        subido.InputStream.Read(subidoBytes, 0, subidoBytes.Length)
        llenar(SP.IMAGENES_LOGIN(identi, 1, subidoBytes))
    End Sub
    Private Sub BtnElimina1_Click(sender As Object, e As EventArgs) Handles BtnElimina1.Click, BtnElimina2.Click, BtnElimina3.Click, BtnElimina4.Click
        Dim identi As Integer = sender.id.ToString.Substring(10)
        llenar(SP.IMAGENES_LOGIN(identi, 2))
    End Sub

    Private Sub llenar(dtab As DataTable)
        imagen1.ImageUrl = "~/M_Administrador/Imagenes/ImgDefault.png"
        imagen2.ImageUrl = "~/M_Administrador/Imagenes/ImgDefault.png"
        imagen3.ImageUrl = "~/M_Administrador/Imagenes/ImgDefault.png"
        imagen4.ImageUrl = "~/M_Administrador/Imagenes/ImgDefault.png"
        For Each row As DataRow In dtab.Rows
            Select Case row.Item("ID")
                Case 1 : imagen1.ImageUrl = "data:image;base64," + Convert.ToBase64String(row.Item("IMAGEN"))
                Case 2 : imagen2.ImageUrl = "data:image;base64," + Convert.ToBase64String(row.Item("IMAGEN"))
                Case 3 : imagen3.ImageUrl = "data:image;base64," + Convert.ToBase64String(row.Item("IMAGEN"))
                Case 4 : imagen4.ImageUrl = "data:image;base64," + Convert.ToBase64String(row.Item("IMAGEN"))

            End Select
        Next
    End Sub
End Class
