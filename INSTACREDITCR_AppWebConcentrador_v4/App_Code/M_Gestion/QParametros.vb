Imports Microsoft.VisualBasic

Public Class QParametros
    Public Sub New(
   ByVal response_Tel As String,
   ByVal response_msg As String)

        Me._response_Tel = response_Tel
        Me._response_msg = response_msg

    End Sub

    Private _response_Tel As String
    Property response_Tel() As String
        Get
            Return _response_Tel
        End Get
        Set(ByVal value As String)
            _response_Tel = value
        End Set
    End Property

    Private _response_msg As String
    Property response_msg() As String
        Get
            Return _response_msg
        End Get
        Set(ByVal value As String)
            _response_msg = value
        End Set
    End Property


End Class


