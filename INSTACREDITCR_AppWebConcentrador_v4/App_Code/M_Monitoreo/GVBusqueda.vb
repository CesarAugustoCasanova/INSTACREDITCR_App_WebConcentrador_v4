Imports Microsoft.VisualBasic

Public Class GVBusqueda
    Private NumCredito As String
    Public Property obtenerNumCredito() As String
        Get
            Return NumCredito
        End Get
        Set(ByVal value As String)
            NumCredito = value
        End Set
    End Property
    Private Nombre As String
    Public Property ObtenerNombre() As String
        Get
            Return Nombre
        End Get
        Set(ByVal value As String)
            Nombre = value
        End Set
    End Property

    Private Folio As String
    Public Property ObtenerFolio() As String
        Get
            Return Folio
        End Get
        Set(ByVal value As String)
            Folio = value
        End Set
    End Property

    Private Telefono As String
    Public Property ObtenerTelefono() As String
        Get
            Return Telefono
        End Get
        Set(ByVal value As String)
            Telefono = value
        End Set
    End Property
    Private Rfc As String
    Public Property obtenerRfc() As String
        Get
            Return Rfc
        End Get
        Set(ByVal value As String)
            Rfc = value
        End Set
    End Property

    Public Sub New(ByVal NuCredito As String, ByVal Nom As String, ByVal Fol As String, ByVal Rf As String, ByVal Tel As String)
        NumCredito = NuCredito
        Nombre = Nom
        Folio = Fol
        Rfc = Rf
        Telefono = Tel

    End Sub
End Class
