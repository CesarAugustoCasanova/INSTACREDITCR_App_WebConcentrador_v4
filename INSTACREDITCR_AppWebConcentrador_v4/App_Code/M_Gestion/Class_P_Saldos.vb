Imports Microsoft.VisualBasic


Public Class Class_P_Saldos
    Public Property Ws As String
    Public Property Informacion() As List(Of Informacion_Saldos)
    Public Property Estatus As String
    Public Property Resultado As String
End Class

Public Class Informacion_Saldos
    Public Property credito As String
    Public Property diasMora As String
    Public Property saldoLiquidar As String
    Public Property saldoPCorriente As String
    Public Property baseHonorariosT As String
    Public Property baseHonorariosP As String
    Public Property codigoSafi As String
    Public Property mensajeSafi As String
End Class




