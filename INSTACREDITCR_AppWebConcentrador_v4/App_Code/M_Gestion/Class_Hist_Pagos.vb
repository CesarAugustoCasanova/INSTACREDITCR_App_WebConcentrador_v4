Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports Db
Imports System.Globalization
Imports System.IO
Imports Funciones
Imports System.Web.SessionState.HttpSessionState
Imports System.Data
Public Class Class_Hist_Pagos
    Public Shared Function LlenarElementosHist_Pagos(ByVal V_Credito As String, ByVal V_Bandera As String, ByVal V_Producto As String) As Object

        Dim SSCommand As New SqlCommand("SP_HISTORICO_PAGOS")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Credito", SqlDbType.NVarChar).Value = V_Credito
        SSCommand.Parameters.Add("@V_Producto", SqlDbType.NVarChar).Value = V_Producto
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = V_Bandera
        Dim DtsTelefonos As DataTable = Consulta_Procedure(SSCommand, SSCommand.CommandText)
        Return DtsTelefonos
    End Function
    Public Shared Function AgregarPago(ByVal V_Hist_Pa_Credito As String, ByVal V_Hist_Pa_Producto As String, ByVal V_Hist_Pa_Usuario As String, ByVal V_Hist_Pa_Referencia As String, ByVal V_Hist_Pa_Montopago As String, ByVal V_Hist_Pa_Dtepago As String, ByVal V_Hist_Pa_Confirmacion As String, ByVal V_Hist_Pa_Lugarpago As String, ByVal V_Hist_Pa_Agencia As String) As String
        If V_Hist_Pa_Dtepago = "" Then
            Return "Seleccione La fecha De Pago"
        ElseIf Funciones.ValidaMonto(V_Hist_Pa_Montopago) = 1 Then
            Return "Monto Incorrecto"
        ElseIf Val(V_Hist_Pa_Montopago) <= 0 Then
            Return "El Monto Del Pago Debe De Ser Mayor A 0"
        ElseIf V_Hist_Pa_Montopago = "" Then
            Return "Capture Un Monto"
        ElseIf V_Hist_Pa_Lugarpago = "Seleccione" Then
            Return "Seleccione Lugar De Pago"
        ElseIf V_Hist_Pa_Referencia = "" Then
            Return "Capture Una Referencia"
        ElseIf V_Hist_Pa_Confirmacion = "Seleccione" Then
            Return "Seleccione Tipo De Confirmación"
        Else

            Dim SSCommand As New SqlCommand("SP_ADD_HIST_PAGOS")
            SSCommand.CommandType = CommandType.StoredProcedure
            SSCommand.Parameters.Add("@V_Hist_Pa_Credito", SqlDbType.NVarChar).Value = V_Hist_Pa_Credito
            'SSCommand.Parameters.Add("@V_Hist_Pa_Producto", SqlDbType.NVarChar).Value = V_Hist_Pa_Producto
            SSCommand.Parameters.Add("@V_Hist_Pa_Usuario", SqlDbType.NVarChar).Value = V_Hist_Pa_Usuario
            SSCommand.Parameters.Add("@V_Hist_Pa_Referencia", SqlDbType.NVarChar).Value = V_Hist_Pa_Referencia
            SSCommand.Parameters.Add("@V_Hist_Pa_Montopago", SqlDbType.Decimal).Value = V_Hist_Pa_Montopago
            SSCommand.Parameters.Add("@V_Hist_Pa_Dtepago", SqlDbType.NVarChar).Value = V_Hist_Pa_Dtepago
            SSCommand.Parameters.Add("@V_Hist_Pa_Confirmacion", SqlDbType.NVarChar).Value = V_Hist_Pa_Confirmacion
            SSCommand.Parameters.Add("@V_Hist_Pa_Lugarpago", SqlDbType.NVarChar).Value = V_Hist_Pa_Lugarpago
            'SSCommand.Parameters.Add("@V_Hist_Pa_Agencia", SqlDbType.NVarChar).Value = V_Hist_Pa_Agencia
            Dim DtsPagos As DataTable = Consulta_Procedure(SSCommand, "Pagos")
            If DtsPagos.TableName = "Exception" Then
                Throw New Exception(DtsPagos.Rows(0).Item(0).ToString)
            End If
            Return DtsPagos.Rows(0).Item("Pago")
        End If
    End Function
End Class
