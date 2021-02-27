Imports Microsoft.VisualBasic

Public Class GvHorarios

    Private Horario As String
    Public Property obtenerHorario() As String
        Get
            Return Horario
        End Get
        Set(ByVal value As String)
            Horario = value
        End Set
    End Property

    Private Nota As String
    Public Property obtenerNota() As String
        Get
            Return Nota
        End Get
        Set(ByVal value As String)
            Nota = value
        End Set
    End Property

    Public Sub New(ByVal nHorario As String, ByVal nNota As String)
        Horario = nHorario
        Nota = nNota
    End Sub

End Class
