<%@ WebHandler Language="VB" Class="Imagen" %>

Imports System
Imports System.Web
Imports System.Data
Imports Conexiones
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Drawing.Drawing2D

Public Class Imagen : Implements IHttpHandler

    Public Sub ProcessRequest(context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Response.ContentType = "image/jpeg"
        Dim v_id As String = context.Request.QueryString("id")
        Dim v_credito As String = context.Request.QueryString("credito")
        Dim v_no As String = context.Request.QueryString("no")

        If v_no = "" Then
            v_no = "1"
        End If

        Dim DtsFotos As DataTable = Class_Visitas.LlenarGridVisitas(5, "", "", v_id, v_credito, v_no)

        If DtsFotos.Rows.Count > 0 Then
            If Not IsDBNull(DtsFotos.Rows(0)("IMAGEN")) Then
                Dim imgBytes As Byte() = DirectCast(DtsFotos.Rows(0)("IMAGEN"), Byte())
                'context.Response.BinaryWrite(imgBytes)
                Dim height As Integer = 240
                Dim width As Integer = 320

                'Get image from database here, put into a stream
                Dim stream = New MemoryStream(imgBytes)
                'this would represent the stream from your database image
                Using original = Image.FromStream(stream)
                    Using resized = New Bitmap(width, height, PixelFormat.Format24bppRgb)
                        Using g = Graphics.FromImage(resized)
                            g.SmoothingMode = SmoothingMode.AntiAlias
                            g.InterpolationMode = InterpolationMode.HighQualityBicubic
                            g.PixelOffsetMode = PixelOffsetMode.HighQuality
                            g.DrawImage(original, New Rectangle(New Point(0, 0), New Size(width, height)))
                            Dim resizedStream = New MemoryStream()
                            resized.Save(resizedStream, ImageFormat.Jpeg)
                            context.Response.ContentType = "image/jpeg"
                            context.Response.BinaryWrite(resizedStream.GetBuffer())
                            context.Response.[End]()
                        End Using
                    End Using
                End Using

            End If
        End If

    End Sub

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property
End Class