Imports System.Collections.Generic
Imports System.Linq
Imports System.Web

Public Class CalendarEvent

    Public Property id() As Integer
        Get
            Return m_id
        End Get
        Set(value As Integer)
            m_id = value
        End Set
    End Property
    Private m_id As Integer

    Public Property title() As String
        Get
            Return m_title
        End Get
        Set(value As String)
            m_title = value
        End Set
    End Property
    Private m_title As String
    Public Property description() As String
        Get
            Return m_description
        End Get
        Set(value As String)
            m_description = value
        End Set
    End Property
    Private m_description As String
    Public Property start() As DateTime
        Get
            Return m_start
        End Get
        Set(value As DateTime)
            m_start = value
        End Set
    End Property
    Private m_start As DateTime
    Public Property [end]() As DateTime
        Get
            Return m_end
        End Get
        Set(value As DateTime)
            m_end = value
        End Set
    End Property
    Private m_end As DateTime

End Class
