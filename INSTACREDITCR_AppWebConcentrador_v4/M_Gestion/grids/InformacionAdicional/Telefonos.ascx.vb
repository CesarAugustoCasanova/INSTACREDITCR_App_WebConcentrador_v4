Imports Db
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports Funciones
Imports System.Data
Imports Telerik.Web.UI.Calendar
Partial Class M_Gestion_grids_InformacionAdicional_Telefonos
    Inherits System.Web.UI.UserControl
    Private _dataItem As Object = Nothing
    Public Property DataItem() As Object
        Get
            Return Me._dataItem
        End Get
        Set(ByVal value As Object)
            Me._dataItem = value
        End Set
    End Property

    Public Function LlenarDrops(ByVal flag As Integer) As String()
        Dim ds As DataTable = Nothing
        Dim valores As String()
        Dim cuantos As Integer = 0
        Select Case flag
            Case 1
                ds = Class_InformacionAdicional.LlenarElementosAgregar("", 5)
            Case 2
                ds = Class_InformacionAdicional.LlenarElementosAgregar("", 7)
            Case 3
                ds = Class_InformacionAdicional.LlenarElementosAgregar("", 6)
        End Select
        If ds.Rows.Count > 0 Then
            cuantos = ds.Rows.Count
            valores = New String(cuantos - 1) {}
        Else
            valores = New String(0) {}
        End If
        For i = 0 To cuantos - 1
            valores(i) = ds.Rows(i)(1)
        Next
        Return valores
    End Function

    Public Function revisa(ByRef seleccionado As Object) As String
        Dim selec As String = ""
        Try
            selec = Convert.ToString(seleccionado)
            If selec = "D" Or selec = "C" Then
                selec = ""
            End If
        Catch
            selec = ""
        End Try
        Return selec
    End Function

    Public Function revisa2(ByRef seleccionado As Object) As String
        Dim selec As String = ""
        Try
            selec = Convert.ToString(seleccionado)
            If selec = "Seleccione" Or selec = "" Or selec = " " Then
                selec = ""
            End If
        Catch
            selec = ""
        End Try
        Return selec
    End Function

    Public Function quitaNull(ByVal valor As Object) As Boolean
        If IsDBNull(valor) Then
            Return False
        End If
        If TryCast(valor, String) = "1" Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function quitaNull2(ByVal valor As Object) As String
        If IsDBNull(valor) Then
            Return False
        End If
        If TryCast(valor, String) = "True" Then
            Return "1"
        Else
            Return "0"
        End If
    End Function
End Class
