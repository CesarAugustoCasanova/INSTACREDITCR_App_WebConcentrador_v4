Imports Microsoft.VisualBasic
Imports Class_MasterPage
Public Class Gestion
    Private _credito As String
    Private _producto As String
    Private _usuario As String
    Private _codigoAccion As String
    Private _resultado As String
    Private _codigoResultado As String
    Private _codigoNoPago As String
    Private _comentario As String
    Private _inOutBound As String
    Private _telefono As String
    Private _agencia As String
    Private _fechaPromesa As String
    Private _montopp As String
    Private _aplicacionAccion As String
    Private _aplicacionNoPago As String
    Private _anterior As String
    Private _tipo As String
    Private _motivo As String
    Private _supervisor As String
    Private _filasTrabajo As String
    Private _callID As String
    Private _campanaMarcador As String
    Private _instancia As String
    Private _participante As String
    Private _tipoPago As String
    Private _parentesco As String

    Public Property Credito As String
        Get
            Return _credito
        End Get
        Set(value As String)
            _credito = value
        End Set
    End Property

    Public Property Producto As String
        Get
            Return _producto
        End Get
        Set(value As String)
            _producto = value
        End Set
    End Property

    Public Property Usuario As String
        Get
            Return _usuario
        End Get
        Set(value As String)
            _usuario = value
        End Set
    End Property

    Public Property CodigoAccion As String
        Get
            Return _codigoAccion
        End Get
        Set(value As String)
            _codigoAccion = value
        End Set
    End Property

    Public Property Resultado As String
        Get
            Return _resultado
        End Get
        Set(value As String)
            _resultado = value
        End Set
    End Property

    Public Property CodigoResultado As String
        Get
            Return _codigoResultado
        End Get
        Set(value As String)
            _codigoResultado = value
        End Set
    End Property

    Public Property CodigoNoPago As String
        Get
            Return _codigoNoPago
        End Get
        Set(value As String)
            _codigoNoPago = value
        End Set
    End Property

    Public Property Comentario As String
        Get
            Return _comentario
        End Get
        Set(value As String)
            _comentario = value
        End Set
    End Property

    Public Property InOutBound As String
        Get
            Return _inOutBound
        End Get
        Set(value As String)
            _inOutBound = value
        End Set
    End Property

    Public Property Telefono As String
        Get
            Return _telefono
        End Get
        Set(value As String)
            _telefono = value
        End Set
    End Property

    Public Property Agencia As String
        Get
            Return _agencia
        End Get
        Set(value As String)
            _agencia = value
        End Set
    End Property

    Public Property FechaPromesa As String
        Get
            Return _fechaPromesa
        End Get
        Set(value As String)
            _fechaPromesa = value
        End Set
    End Property

    Public Property Montopp As String
        Get
            Return _montopp
        End Get
        Set(value As String)
            _montopp = value
        End Set
    End Property

    Public Property AplicacionAccion As String
        Get
            Return _aplicacionAccion
        End Get
        Set(value As String)
            _aplicacionAccion = value
        End Set
    End Property

    Public Property AplicacionNoPago As String
        Get
            Return _aplicacionNoPago
        End Get
        Set(value As String)
            _aplicacionNoPago = value
        End Set
    End Property

    Public Property Anterior As String
        Get
            Return _anterior
        End Get
        Set(value As String)
            _anterior = value
        End Set
    End Property

    Public Property Tipo As String
        Get
            Return _tipo
        End Get
        Set(value As String)
            _tipo = value
        End Set
    End Property

    Public Property Motivo As String
        Get
            Return _motivo
        End Get
        Set(value As String)
            _motivo = value
        End Set
    End Property

    Public Property Supervisor As String
        Get
            Return _supervisor
        End Get
        Set(value As String)
            _supervisor = value
        End Set
    End Property

    Public Property FilasTrabajo As String
        Get
            Return _filasTrabajo
        End Get
        Set(value As String)
            _filasTrabajo = value
        End Set
    End Property

    Public Property CallID As String
        Get
            Return _callID
        End Get
        Set(value As String)
            _callID = value
        End Set
    End Property

    Public Property CampanaMarcador As String
        Get
            Return _campanaMarcador
        End Get
        Set(value As String)
            _campanaMarcador = value
        End Set
    End Property
    Public Property instancia As String
        Get
            Return _instancia
        End Get
        Set(value As String)
            _instancia = value
        End Set
    End Property
    Public Property Participante As String
        Get
            Return _participante
        End Get
        Set(value As String)
            _participante = value
        End Set
    End Property

    Public Property TipoPago As String
        Get
            Return _tipoPago
        End Get
        Set(value As String)
            _tipoPago = value
        End Set
    End Property

    Public Property parentesco As String
        Get
            Return _parentesco
        End Get
        Set(value As String)
            _parentesco = value
        End Set
    End Property

    Sub New()
    End Sub

    Public Function guardarGestion() As Object
        If AplicacionAccion = 1 And CodigoAccion = "Seleccione" Then
            Return "Seleccione Una Acción"
        ElseIf CodigoResultado = "Seleccione" Then
            Return "Seleccione Un Resultado"
        ElseIf (AplicacionNoPago = 1 And CodigoNoPago = "Seleccione" And CodigoResultado.Split(",")(1) = 1) Then
            Return "Seleccione Una Causa De No Pago"
        ElseIf Comentario.Length < 10 Then
            Return "Capture Un Comentario Valido"
        Else
            Return AddGestion(Credito, Producto, Usuario, CodigoAccion, Resultado, CodigoResultado.Split(",")(0), CodigoNoPago, Comentario, InOutBound, Telefono, Agencia, FechaPromesa, Anterior, FilasTrabajo, CallID, CampanaMarcador, instancia, Participante, "", parentesco)
        End If
    End Function

    Public Function guardarPromesa() As Object
        If AplicacionAccion = 1 And CodigoAccion = "Seleccione" Then
            Return "Seleccione Una Acción"
        ElseIf CodigoResultado = "Seleccione" Then
            Return "Seleccione Un Resultado"
        ElseIf (AplicacionNoPago = 1 And CodigoNoPago = "Seleccione" And CodigoResultado.Split(",")(1) = 1) Then
            Return "Seleccione Una Causa De No Pago"
        ElseIf Comentario.Length < 10 Then
            Return "Capture Un Comentario Valido"
        Else
            Return AddPromesa(Credito, Producto, Montopp, FechaPromesa, Usuario, "Parcial", 1, "Telefonica", Agencia, CodigoAccion, Resultado, CodigoResultado.Split(",")(0), CodigoNoPago, Comentario, "", Telefono, 0, Anterior, 1, FechaPromesa, FilasTrabajo, CallID, CampanaMarcador, instancia, Participante, TipoPago)
        End If
    End Function

    Public Function cancelarPromesa() As Object
        If AplicacionAccion = 1 And CodigoAccion = "Seleccione" Then
            Return "Seleccione Una Acción"
        ElseIf CodigoResultado = "Seleccione" Then
            Return "Seleccione Un Resultado"
        ElseIf (AplicacionNoPago = 1 And CodigoNoPago = "Seleccione" And CodigoResultado.Split(",")(1) = 1) Then
            Return "Seleccione Una Causa De No Pago"
        ElseIf Comentario.Length < 10 Then
            Return "Capture Un Comentario Valido"
        Else
            CancelarPP(Motivo & "," & Credito, Supervisor, 9)
            Return AddPromesa(Credito, Producto, Montopp, FechaPromesa, Usuario, "Parcial", 1, "Telefonica", Agencia, CodigoAccion, Resultado, CodigoResultado.Split(",")(0), CodigoNoPago, Comentario, "", Telefono, 0, Anterior, 1, FechaPromesa, FilasTrabajo, CallID, CampanaMarcador, instancia, Participante, TipoPago)
        End If
    End Function
End Class