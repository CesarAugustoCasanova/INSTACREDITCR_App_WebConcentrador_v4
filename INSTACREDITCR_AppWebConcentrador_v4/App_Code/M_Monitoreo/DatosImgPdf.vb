Imports Microsoft.VisualBasic

Public Class DatosImgPdf
    Public Sub New(ByVal ruta As String, ByVal nombre As String)
        _rutaArchivo = ruta
        _nombreArchivo = nombre
        _extension = nombre.Substring(nombre.Length - 3).ToUpper
    End Sub

    Private _rutaArchivo As String
    Public Property rutaArchivo As String
        Get
            Return _rutaArchivo
        End Get
        Set(ByVal value As String)
            _rutaArchivo = value
        End Set
    End Property
    Private _nombreArchivo As String
    Public Property nombreArchivo As String
        Get
            Return _nombreArchivo
        End Get
        Set(ByVal value As String)
            _nombreArchivo = value
        End Set
    End Property
    Private _extension As String
    Public Property extension As String
        Get
            Return _extension
        End Get
        Set(ByVal value As String)
            _extension = value
        End Set
    End Property



End Class
