Imports Microsoft.VisualBasic

Public Class ClassGraficaGoogle


    'Opciones de la Grafica

    Public Property pieHole() As String
        Get
            Return m_pieHole
        End Get
        Set(value As String)
            m_pieHole = value
        End Set
    End Property
    Private m_pieHole As String

    Public Property pointSize() As String
        Get
            Return m_pointSize
        End Get
        Set(value As String)
            m_pointSize = value
        End Set
    End Property
    Private m_pointSize As String

    Public Property height() As String
        Get
            Return m_height
        End Get
        Set(value As String)
            m_height = value
        End Set
    End Property
    Private m_height As String

    Public Property width() As String
        Get
            Return m_width
        End Get
        Set(value As String)
            m_width = value
        End Set
    End Property
    Private m_width As String


    Public Property title() As String
        Get
            Return m_title
        End Get
        Set(value As String)
            m_title = value
        End Set
    End Property
    Private m_title As String

    Public Property series() As Object
        Get
            Return m_series
        End Get
        Set(value As Object)
            m_series = value
        End Set
    End Property
    Private m_series As Object


    Public Property type() As String
        Get
            Return m_type
        End Get
        Set(value As String)
            m_type = value
        End Set
    End Property
    Private m_type As String


    Public Property seriesType() As String
        Get
            Return m_seriesType
        End Get
        Set(value As String)
            m_seriesType = value
        End Set
    End Property
    Private m_seriesType As String


    Public Property is3D() As Boolean
        Get
            Return m_is3D
        End Get
        Set(value As Boolean)
            m_is3D = value
        End Set
    End Property
    Private m_is3D As Boolean

    Public Property colors() As List(Of String)
        Get
            Return m_colors
        End Get
        Set(value As List(Of String))
            m_colors = value
        End Set
    End Property
    Private m_colors As List(Of String)
    'Roles de la Grafica
    Public Property role() As String
        Get
            Return m_role
        End Get
        Set(value As String)
            m_role = value
        End Set
    End Property
    Private m_role As String

    Public Property tooltip() As Object
        Get
            Return m_tooltip
        End Get
        Set(value As Object)
            m_tooltip = value
        End Set
    End Property
    Private m_tooltip As Object

    Public Property trigger() As String
        Get
            Return m_trigger
        End Get
        Set(value As String)
            m_trigger = value
        End Set
    End Property
    Private m_trigger As String

    Public Property legend() As String
        Get
            Return m_legend
        End Get
        Set(value As String)
            m_legend = value
        End Set
    End Property
    Private m_legend As String
    Public Property pieSliceText() As String
        Get
            Return m_pieSliceText
        End Get
        Set(value As String)
            m_pieSliceText = value
        End Set
    End Property
    Private m_pieSliceText As String
End Class
