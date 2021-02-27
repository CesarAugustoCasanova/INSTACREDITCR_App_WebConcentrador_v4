Imports System.Data.SqlClient
Imports Db
Imports System.Data
Imports Microsoft.VisualBasic

Public Class AvisoGestion
    Private _ID As String
    Private _Mensaje As String
    Private _Usuario As String
    Private _DteVigencia As String
    Private _DteCreacion As String
    Private _visto As String
    Private _Tipo As String

    Public Sub New(_Mensaje As String, _Usuario As String, _DteVigencia As String, _Tipo As String)
        Me._Mensaje = _Mensaje
        Me._Usuario = _Usuario
        Me._DteVigencia = _DteVigencia
        Me._Tipo = _Tipo
    End Sub

    Public Sub New(_ID As String, _Mensaje As String, _Usuario As String, _DteVigencia As String, _DteCreacion As String, _visto As String, _Tipo As String)
        Me._ID = _ID
        Me._Mensaje = _Mensaje
        Me._Usuario = _Usuario
        Me._DteVigencia = _DteVigencia
        Me._DteCreacion = _DteCreacion
        Me._visto = _visto
        Me._Tipo = _Tipo
    End Sub

    Public Property ID As String
        Get
            Return _ID
        End Get
        Set(value As String)
            _ID = value
        End Set
    End Property

    Public Property Mensaje As String
        Get
            Return _Mensaje
        End Get
        Set(value As String)
            _Mensaje = value
        End Set
    End Property

    Public Property Usuario As String
        Get
            Return _Usuario
        End Get
        Set(value As String)
            _Usuario = value
        End Set
    End Property

    Public Property DteVisto As String
        Get
            Return _DteVigencia
        End Get
        Set(value As String)
            _DteVigencia = value
        End Set
    End Property

    Public Property Tipo As String
        Get
            Return _Tipo
        End Get
        Set(value As String)
            _Tipo = value
        End Set
    End Property

    Public Property DteVigencia As String
        Get
            Return _DteVigencia
        End Get
        Set(value As String)
            _DteVigencia = value
        End Set
    End Property

    Public Property DteCreacion As String
        Get
            Return _DteCreacion
        End Get
        Set(value As String)
            _DteCreacion = value
        End Set
    End Property

    Public Property Visto As String
        Get
            Return _visto
        End Get
        Set(value As String)
            _visto = value
        End Set
    End Property


    Public Function Actualizar() As String
        Return sendData(2)
    End Function

    Public Function Insertar() As String
        Return sendData(1)
    End Function

    Private Function sendData(ByVal v_bandera As Integer) As String
        dim SSCommand as new sqlcommand
        SSCommand.CommandText = "SP_ALERTAS_GESTION"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_USUARIO", SqlDbType.NVarChar).Value = _Usuario
        SSCommand.Parameters.Add("@V_mensaje", SqlDbType.NVarChar).Value = _Mensaje
        SSCommand.Parameters.Add("@V_tipo", SqlDbType.NVarChar).Value = _Tipo
        SSCommand.Parameters.Add("@V_vigencia", SqlDbType.NVarChar).Value = _DteVigencia
        SSCommand.Parameters.Add("@V_id", SqlDbType.Decimal).Value = _ID
        SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = v_bandera

        Return Consulta_Procedure(SSCommand, "Tocar")(0)(0).ToString
    End Function

    Public Shared Function getAllAvisos(ByVal v_usuario As String) As List(Of AvisoGestion)
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_ALERTAS_GESTION"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_usuario", SqlDbType.NVarChar).Value = v_usuario
        SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 3

        Dim table As DataTable = Consulta_Procedure(SSCommand, "Tocar")
        Dim avisos As List(Of AvisoGestion) = New List(Of AvisoGestion)
        For Each row As DataRow In table.Rows
            Dim aviso As New AvisoGestion(row("ID"), row("Mensaje"), v_usuario, row("Fecha de vigencia"), row("Fecha de creacion"), row("Visto"), row("Tipo"))
            avisos.Add(aviso)
        Next
        Return avisos
    End Function

    Public Shared Function getAllAvisosDataTable(ByVal v_usuario As String) As DataTable
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_ALERTAS_GESTION"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_usuario", SqlDbType.NVarChar).Value = v_usuario
        SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 3

        Return Consulta_Procedure(SSCommand, "Tocar")
    End Function

    Public Shared Function getAllUnseen(ByVal v_usuario As String) As Integer
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_ALERTAS_GESTION"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_usuario", SqlDbType.NVarChar).Value = v_usuario
        SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 4

        Return Consulta_Procedure(SSCommand, "Tocar")(0)(0)
    End Function

    Public Shared Function setAllSeen(ByVal v_usuario As String) As String
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_ALERTAS_GESTION"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_usuario", SqlDbType.NVarChar).Value = v_usuario
        SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 2

        Return Consulta_Procedure(SSCommand, "Tocar")(0)(0)
    End Function
    Public Shared Function traealertasgral(ByVal usuario As String, ByVal tipo As String, ByVal fechaini As String, ByVal fechafin As String) As DataTable
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_ALERTAS_GESTION"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_USUARIO", SqlDbType.NVarChar).Value = usuario
        SSCommand.Parameters.Add("@V_TIPO", SqlDbType.NVarChar).Value = tipo
        SSCommand.Parameters.Add("@V_MENSAJE", SqlDbType.NVarChar).Value = fechaini
        SSCommand.Parameters.Add("@V_VIGENCIA", SqlDbType.NVarChar).Value = fechafin
        SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 5

        Return Consulta_Procedure(SSCommand, "Tocar")
    End Function
    Public Shared Function traetiposalertas(ByVal usuario As String) As DataTable
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_ALERTAS_GESTION"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_USUARIO", SqlDbType.NVarChar).Value = usuario
        SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 7

        Return Consulta_Procedure(SSCommand, "Tocar")
    End Function
End Class
