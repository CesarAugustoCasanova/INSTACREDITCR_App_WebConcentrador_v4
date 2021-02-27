Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls

Public Class WorkItem
    Public Sub New()
    End Sub

    Public Sub New(ByVal Id As Integer, ByVal Codigo As String, ByVal Descripcion As String, ByVal Ponderacion As Integer)
        _Id = Id
        _Codigo = Codigo
        _Descripcion = Descripcion
        _Ponderacion = Ponderacion
    End Sub

    Private _Id As Integer
    Public Property Id() As Integer
        Get
            Return _Id
        End Get
        Set(ByVal value As Integer)
            _Id = value
        End Set
    End Property

    Private _Codigo As String
    Public Property Codigo() As String
        Get
            Return _Codigo
        End Get
        Set(ByVal value As String)
            _Codigo = value
        End Set
    End Property

    Private _Descripcion As String
    Public Property Descripcion() As String
        Get
            Return _Descripcion
        End Get
        Set(ByVal value As String)
            _Descripcion = value
        End Set
    End Property
    Private _Ponderacion As Integer
    Public Property Ponderacion() As Integer
        Get
            Return _Ponderacion
        End Get
        Set(ByVal value As Integer)
            _Ponderacion = value
        End Set
    End Property

    Public Overrides Function ToString() As String
        Return String.Format("Item: ID:{0}" & vbTab & "Codigo:{1}" & vbTab & "Descripcion:{2}" & vbTab & "Ponderacion:{3}", _Id, _Codigo, _Descripcion, _Ponderacion)
    End Function

End Class
