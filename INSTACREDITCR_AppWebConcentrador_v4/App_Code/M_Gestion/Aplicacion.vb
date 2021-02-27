Imports Microsoft.VisualBasic

Public Class Aplicacion
    Public Sub New(ByVal LETRA As Integer,
                   ByVal CONSECUTIVO As Integer,
                   ByVal PROMESAS_PAGO As Integer,
                   ByVal NEGOCIACIONES As Integer,
                   ByVal ACCION As Integer,
                   ByVal RESULTADO As Integer, ByVal NOPAGO As Integer)
        Me._LETRA = LETRA
        Me._CONSECUTIVO = CONSECUTIVO
        Me._PROMESAS_PAGO = PROMESAS_PAGO
        Me._NEGOCIACIONES = NEGOCIACIONES
        Me._ACCION = ACCION
        Me._RESULTADO = RESULTADO
        Me._NOPAGO = NOPAGO
    End Sub

    Private _LETRA As String
    Property LETRA() As String
        Get
            Return _LETRA
        End Get
        Set(ByVal value As String)
            _LETRA = value
        End Set
    End Property

    Private _CONSECUTIVO As String
    Property CONSECUTIVO() As String
        Get
            Return _CONSECUTIVO
        End Get
        Set(ByVal value As String)
            _CONSECUTIVO = value
        End Set
    End Property

    Private _PROMESAS_PAGO As String
    Property PROMESAS_PAGO() As String
        Get
            Return _PROMESAS_PAGO
        End Get
        Set(ByVal value As String)
            _PROMESAS_PAGO = value
        End Set
    End Property

    Private _NEGOCIACIONES As String
    Property NEGOCIACIONES() As String
        Get
            Return _NEGOCIACIONES
        End Get
        Set(ByVal value As String)
            _NEGOCIACIONES = value
        End Set
    End Property

    Private _ACCION As String
    Property ACCION() As String
        Get
            Return _ACCION
        End Get
        Set(ByVal value As String)
            _ACCION = value
        End Set
    End Property

    Private _RESULTADO As String
    Property RESULTADO() As String
        Get
            Return _RESULTADO
        End Get
        Set(ByVal value As String)
            _RESULTADO = value
        End Set
    End Property
    Private _NOPAGO As String
    Property NOPAGO() As String
        Get
            Return _NOPAGO
        End Get
        Set(ByVal value As String)
            _NOPAGO = value
        End Set
    End Property
End Class
