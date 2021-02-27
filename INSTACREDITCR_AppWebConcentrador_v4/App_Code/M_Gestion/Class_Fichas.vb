Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports Db
Imports System.Net
Imports Newtonsoft.Json
Imports System.IO

Public Class Class_Fichas
    Public Shared Function LlenarParticipantes(ByVal V_Credito As String, ByVal v_fecha As String, ByVal v_pago As String, ByVal v_comisiones As String, ByVal v_moratorios As String, ByVal v_intnormal As String, ByVal v_capital As String) As DataTable
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_FICHAS_NEGOCIACION"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Credito", SqlDbType.NVarChar).Value = V_Credito
        SSCommand.Parameters.Add("@v_fecha", SqlDbType.NVarChar).Value = v_fecha
        SSCommand.Parameters.Add("@v_pago", SqlDbType.NVarChar).Value = v_pago
        SSCommand.Parameters.Add("@v_comisiones", SqlDbType.NVarChar).Value = v_comisiones
        SSCommand.Parameters.Add("@v_moratorios", SqlDbType.NVarChar).Value = v_moratorios
        SSCommand.Parameters.Add("@v_intnormal", SqlDbType.NVarChar).Value = v_intnormal
        SSCommand.Parameters.Add("@v_capital", SqlDbType.NVarChar).Value = v_capital
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = 0

        Dim DtsHist_Fichas As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return DtsHist_Fichas
    End Function
    Public Shared Function LlenarSimulacion(ByVal V_Credito As String, ByVal v_fecha As String, ByVal v_pago As String, ByVal v_comisiones As String, ByVal v_moratorios As String, ByVal v_intnormal As String, ByVal v_capital As String) As DataTable
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_FICHAS_NEGOCIACION"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Credito", SqlDbType.NVarChar).Value = V_Credito
        SSCommand.Parameters.Add("@v_fecha", SqlDbType.NVarChar).Value = v_fecha
        SSCommand.Parameters.Add("@v_pago", SqlDbType.NVarChar).Value = v_pago
        SSCommand.Parameters.Add("@v_comisiones", SqlDbType.NVarChar).Value = v_comisiones
        SSCommand.Parameters.Add("@v_moratorios", SqlDbType.NVarChar).Value = v_moratorios
        SSCommand.Parameters.Add("@v_intnormal", SqlDbType.NVarChar).Value = v_intnormal
        SSCommand.Parameters.Add("@v_capital", SqlDbType.NVarChar).Value = v_capital
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = 1

        Dim DtsHist_Fichas As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return DtsHist_Fichas
    End Function
    Public Shared Function LlenarCampEsp(ByVal V_Credito As String, ByVal v_fecha As String, ByVal v_pago As String, ByVal v_comisiones As String, ByVal v_moratorios As String, ByVal v_intnormal As String, ByVal v_capital As String) As DataTable
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_FICHAS_NEGOCIACION"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Credito", SqlDbType.NVarChar).Value = V_Credito
        SSCommand.Parameters.Add("@v_fecha", SqlDbType.NVarChar).Value = v_fecha
        SSCommand.Parameters.Add("@v_pago", SqlDbType.NVarChar).Value = v_pago
        SSCommand.Parameters.Add("@v_comisiones", SqlDbType.NVarChar).Value = v_comisiones
        SSCommand.Parameters.Add("@v_moratorios", SqlDbType.NVarChar).Value = v_moratorios
        SSCommand.Parameters.Add("@v_intnormal", SqlDbType.NVarChar).Value = v_intnormal
        SSCommand.Parameters.Add("@v_capital", SqlDbType.NVarChar).Value = v_capital
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = 2

        Dim DtsHist_Fichas As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return DtsHist_Fichas
    End Function
    Public Shared Function LlenarBaseHono(ByVal V_Credito As String, ByVal v_fecha As String, ByVal v_pago As String, ByVal v_comisiones As String, ByVal v_moratorios As String, ByVal v_intnormal As String, ByVal v_capital As String) As String
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_FICHAS_NEGOCIACION"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Credito", SqlDbType.NVarChar).Value = V_Credito
        SSCommand.Parameters.Add("@v_fecha", SqlDbType.NVarChar).Value = v_fecha
        SSCommand.Parameters.Add("@v_pago", SqlDbType.NVarChar).Value = v_pago
        SSCommand.Parameters.Add("@v_comisiones", SqlDbType.NVarChar).Value = v_comisiones
        SSCommand.Parameters.Add("@v_moratorios", SqlDbType.NVarChar).Value = v_moratorios
        SSCommand.Parameters.Add("@v_intnormal", SqlDbType.NVarChar).Value = v_intnormal
        SSCommand.Parameters.Add("@v_capital", SqlDbType.NVarChar).Value = v_capital
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = 3

        Dim DtsHist_Fichas As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return DtsHist_Fichas.Rows(0).Item("BASEHONORARIO").ToString
    End Function
    Public Shared Function detallepagos(ByVal V_Credito As String, ByVal v_fecha As String, ByVal v_pago As String, ByVal v_comisiones As String, ByVal v_moratorios As String, ByVal v_intnormal As String, ByVal v_capital As String) As DataTable
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_FICHAS_NEGOCIACION"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Credito", SqlDbType.NVarChar).Value = V_Credito
        SSCommand.Parameters.Add("@v_fecha", SqlDbType.NVarChar).Value = v_fecha
        SSCommand.Parameters.Add("@v_pago", SqlDbType.NVarChar).Value = v_pago
        SSCommand.Parameters.Add("@v_comisiones", SqlDbType.NVarChar).Value = v_comisiones
        SSCommand.Parameters.Add("@v_moratorios", SqlDbType.NVarChar).Value = v_moratorios
        SSCommand.Parameters.Add("@v_intnormal", SqlDbType.NVarChar).Value = v_intnormal
        SSCommand.Parameters.Add("@v_capital", SqlDbType.NVarChar).Value = v_capital
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = 4

        Dim DtsHist_Fichas As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return DtsHist_Fichas
    End Function
    Public Shared Function LlenarSimulacionOrigianl(ByVal V_Credito As String) As DataTable
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_FICHAS_NEGOCIACION"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Credito", SqlDbType.NVarChar).Value = V_Credito
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = 5

        Dim DtsHist_Fichas As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return DtsHist_Fichas
    End Function

    Public Shared Function LlenarSimulacionPago(ByVal V_Credito As String, ByVal V_pago As String, ByVal v_comisiones As String, ByVal v_moratorios As String, ByVal v_intnormal As String, ByVal v_capital As String, ByVal v_fecha As String) As DataTable
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_FICHAS_NEGOCIACION"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Credito", SqlDbType.NVarChar).Value = V_Credito
        SSCommand.Parameters.Add("@V_PAGO", SqlDbType.NVarChar).Value = V_pago
        SSCommand.Parameters.Add("@v_comisiones", SqlDbType.NVarChar).Value = v_comisiones
        SSCommand.Parameters.Add("@v_moratorios", SqlDbType.NVarChar).Value = v_moratorios
        SSCommand.Parameters.Add("@v_intnormal", SqlDbType.NVarChar).Value = v_intnormal
        SSCommand.Parameters.Add("@v_capital", SqlDbType.NVarChar).Value = v_capital
        SSCommand.Parameters.Add("@v_fecha", SqlDbType.NVarChar).Value = v_fecha
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = 6

        Dim DtsHist_Fichas As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return DtsHist_Fichas
    End Function
    Public Shared Function LlenarPDF(ByVal V_Credito As String, ByVal v_Bandera As Integer) As DataTable
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_PDF_NEGOCIACION"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Credito", SqlDbType.NVarChar).Value = V_Credito
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = v_Bandera

        Dim DtsPDF_Fichas As DataTable = Consulta_Procedure(SSCommand, "PDF")
        Return DtsPDF_Fichas
    End Function
    Public Shared Function LlenarPDF2(ByVal V_Credito As String, ByVal v_Bandera As Integer) As DataTable
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_PDF_NEGOCIACION_ADJ"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Credito", SqlDbType.NVarChar).Value = V_Credito
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = v_Bandera

        Dim DtsPDF_Fichas As DataTable = Consulta_Procedure(SSCommand, "PDF")
        Return DtsPDF_Fichas
    End Function
    Public Shared Function LlenarSaldosCredito(ByVal V_Credito As String) As DataTable
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_FICHAS_NEGOCIACION"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Credito", SqlDbType.NVarChar).Value = V_Credito
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = 7

        Dim DtsHist_Fichas As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return DtsHist_Fichas
    End Function

    Public Shared Function LlenarSaldosCredito2(ByVal V_Credito As String, ByVal V_Bandera As Integer, ByVal V_valor1 As String, ByVal V_valor2 As String, ByVal V_valor3 As String) As DataTable
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_FICHAS_NEGOCIACION"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Credito", SqlDbType.NVarChar).Value = V_Credito
        SSCommand.Parameters.Add("@v_fecha", SqlDbType.NVarChar).Value = V_valor1
        SSCommand.Parameters.Add("@V_PAGO", SqlDbType.NVarChar).Value = V_valor2
        SSCommand.Parameters.Add("@v_comisiones", SqlDbType.NVarChar).Value = V_valor3
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = V_Bandera

        Dim DtsHist_Fichas As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return DtsHist_Fichas
    End Function
    Public Shared Function LlenarAutorizador(ByVal V_MORATORIO As String, ByVal V_IMPAGO As String, ByVal V_HONORARIOS As String, ByVal V_ORDINARIO As String, ByVal V_CAPITAL As String, ByVal V_PUESTO As String, ByVal V_Bandera As String) As DataTable
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_QUIEN_AUTORIZA"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_MORATORIO", SqlDbType.NVarChar).Value = V_MORATORIO
        SSCommand.Parameters.Add("@V_IMPAGO", SqlDbType.NVarChar).Value = V_IMPAGO
        SSCommand.Parameters.Add("@V_HONORARIOS", SqlDbType.NVarChar).Value = V_HONORARIOS
        SSCommand.Parameters.Add("@V_ORDINARIO", SqlDbType.NVarChar).Value = V_ORDINARIO
        SSCommand.Parameters.Add("@V_CAPITAL", SqlDbType.NVarChar).Value = V_CAPITAL
        SSCommand.Parameters.Add("@V_PUESTO", SqlDbType.NVarChar).Value = V_PUESTO
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = V_Bandera

        Dim DtsHist_Fichas As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return DtsHist_Fichas
    End Function
    Public Shared Function mandarficha(ByVal KEYMC As String, ByVal folioFicha As String, ByVal tipoFicha As String, ByVal usuarioEnvioID As String, ByVal usuarioEnvioNombre As String, ByVal usuarioIP As String, ByVal clienteID As String, ByVal creditoID As String, ByVal tipoPago As String, ByVal totalPagar As String, ByVal fechaPago As String, ByVal observacionesgest As String, ByVal valorNetoActivo As String, ByVal capital As String, ByVal interes As String, ByVal ivaInteres As String, ByVal moratorio As String, ByVal ivaMoratorio As String, ByVal comisiones As String, ByVal ivaComisiones As String, ByVal cC_comisiones As String, ByVal cC_moratorios As String, ByVal cC_interes As String, ByVal cC_capital As String, ByVal gastosCobranza As String, ByVal ivaGastosCob As String, ByVal honorario As String, ByVal despachoID As String, ByVal despachoNombre As String, ByVal tipoDespacho As String, ByVal abogadoID As String, ByVal abogadoNombre As String, ByVal supervisorID As String, ByVal supervisorNombre As String, ByVal folioDacion As String, ByVal descripcion As String, ByVal valorComercial As String, ByVal valorAplicar As String, ByVal numCuenta As String, ByVal monto As String, ByVal V_debe_A As String, ByVal porchonorario As String, ByVal observacionesficha As String, ByVal campa As String) As String
        Dim cliente As String = "0000000147"
        Dim credito As String = "100004437"
        Dim ahorro As String = "100018691"
        Dim montoahorro As String = "100"
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_GUARDA_FICHA"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@KEYMC", SqlDbType.NVarChar).Value = KEYMC
        SSCommand.Parameters.Add("@folioFicha", SqlDbType.NVarChar).Value = folioFicha
        SSCommand.Parameters.Add("@tipoFicha", SqlDbType.NVarChar).Value = tipoFicha
        SSCommand.Parameters.Add("@usuarioEnvioID", SqlDbType.NVarChar).Value = usuarioEnvioID
        SSCommand.Parameters.Add("@usuarioEnvioNombre", SqlDbType.NVarChar).Value = usuarioEnvioNombre
        SSCommand.Parameters.Add("@usuarioIP", SqlDbType.NVarChar).Value = usuarioIP
        SSCommand.Parameters.Add("@clienteID", SqlDbType.NVarChar).Value = clienteID
        SSCommand.Parameters.Add("@creditoID", SqlDbType.NVarChar).Value = creditoID
        SSCommand.Parameters.Add("@tipoPago", SqlDbType.NVarChar).Value = tipoPago
        SSCommand.Parameters.Add("@totalPagar", SqlDbType.NVarChar).Value = totalPagar
        SSCommand.Parameters.Add("@fechaPago", SqlDbType.NVarChar).Value = fechaPago
        SSCommand.Parameters.Add("@observaciones", SqlDbType.NVarChar).Value = observacionesgest
        SSCommand.Parameters.Add("@valorNetoActivo", SqlDbType.NVarChar).Value = valorNetoActivo
        SSCommand.Parameters.Add("@capital", SqlDbType.NVarChar).Value = capital
        SSCommand.Parameters.Add("@interes", SqlDbType.NVarChar).Value = interes
        SSCommand.Parameters.Add("@ivaInteres", SqlDbType.NVarChar).Value = ivaInteres
        SSCommand.Parameters.Add("@moratorio", SqlDbType.NVarChar).Value = moratorio
        SSCommand.Parameters.Add("@ivaMoratorio", SqlDbType.NVarChar).Value = ivaMoratorio
        SSCommand.Parameters.Add("@comisiones", SqlDbType.NVarChar).Value = comisiones
        SSCommand.Parameters.Add("@ivaComisiones", SqlDbType.NVarChar).Value = ivaComisiones
        SSCommand.Parameters.Add("@cC_comisiones", SqlDbType.NVarChar).Value = cC_comisiones
        SSCommand.Parameters.Add("@cC_moratorios", SqlDbType.NVarChar).Value = cC_moratorios
        SSCommand.Parameters.Add("@cC_interes", SqlDbType.NVarChar).Value = cC_interes
        SSCommand.Parameters.Add("@cC_capital", SqlDbType.NVarChar).Value = cC_capital
        SSCommand.Parameters.Add("@gastosCobranza", SqlDbType.NVarChar).Value = gastosCobranza
        SSCommand.Parameters.Add("@ivaGastosCob", SqlDbType.NVarChar).Value = ivaGastosCob
        SSCommand.Parameters.Add("@honorario", SqlDbType.NVarChar).Value = honorario
        SSCommand.Parameters.Add("@despachoID", SqlDbType.NVarChar).Value = despachoID
        SSCommand.Parameters.Add("@despachoNombre", SqlDbType.NVarChar).Value = despachoNombre
        SSCommand.Parameters.Add("@tipoDespacho", SqlDbType.NVarChar).Value = tipoDespacho
        SSCommand.Parameters.Add("@abogadoID", SqlDbType.NVarChar).Value = abogadoID
        SSCommand.Parameters.Add("@abogadoNombre", SqlDbType.NVarChar).Value = abogadoNombre
        SSCommand.Parameters.Add("@supervisorID", SqlDbType.NVarChar).Value = supervisorID
        SSCommand.Parameters.Add("@supervisorNombre", SqlDbType.NVarChar).Value = supervisorNombre
        SSCommand.Parameters.Add("@folioDacion", SqlDbType.NVarChar).Value = folioDacion
        SSCommand.Parameters.Add("@descripcion", SqlDbType.NVarChar).Value = descripcion
        SSCommand.Parameters.Add("@valorComercial", SqlDbType.NVarChar).Value = valorComercial
        SSCommand.Parameters.Add("@valorAplicar", SqlDbType.NVarChar).Value = valorAplicar
        SSCommand.Parameters.Add("@numCuenta", SqlDbType.NVarChar).Value = numCuenta
        SSCommand.Parameters.Add("@monto", SqlDbType.NVarChar).Value = monto
        SSCommand.Parameters.Add("@V_debe_A", SqlDbType.NVarChar).Value = V_debe_A
        SSCommand.Parameters.Add("@v_porchonorario", SqlDbType.NVarChar).Value = porchonorario
        SSCommand.Parameters.Add("@observacionesficha", SqlDbType.NVarChar).Value = observacionesficha
        SSCommand.Parameters.Add("@campaplicada", SqlDbType.NVarChar).Value = campa
        SSCommand.Parameters.Add("@v_bandera", SqlDbType.Decimal).Value = 0

        Dim DTFICHA As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return "ok"
    End Function
    Public Shared Function mandarficha(ByVal KEYMC As String, ByVal folioFicha As String, ByVal tipoFicha As String, ByVal usuarioEnvioID As String, ByVal usuarioEnvioNombre As String, ByVal usuarioIP As String, ByVal clienteID As String, ByVal creditoID As String, ByVal tipoPago As String, ByVal totalPagar As String, ByVal fechaPago As String, ByVal observaciones As String, ByVal valorNetoActivo As String, ByVal capital As String, ByVal interes As String, ByVal ivaInteres As String, ByVal moratorio As String, ByVal ivaMoratorio As String, ByVal comisiones As String, ByVal ivaComisiones As String, ByVal cC_comisiones As String, ByVal cC_moratorios As String, ByVal cC_interes As String, ByVal cC_capital As String, ByVal gastosCobranza As String, ByVal ivaGastosCob As String, ByVal honorario As String, ByVal despachoID As String, ByVal despachoNombre As String, ByVal tipoDespacho As String, ByVal abogadoID As String, ByVal abogadoNombre As String, ByVal supervisorID As String, ByVal supervisorNombre As String, ByVal folioDacion As String, ByVal descripcion As String, ByVal valorComercial As String, ByVal valorAplicar As String, ByVal numCuenta As String, ByVal monto As String, ByVal V_debe_A As String, ByVal porchonorario As String, ByVal descr As String, ByVal vcomercial As String, ByVal vaplicar As String) As String
        Dim cliente As String = "0000000147"
        Dim credito As String = "100004437"
        Dim ahorro As String = "100018691"
        Dim montoahorro As String = "100"
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_GUARDA_FICHA"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@KEYMC", SqlDbType.NVarChar).Value = KEYMC
        SSCommand.Parameters.Add("@folioFicha", SqlDbType.NVarChar).Value = folioFicha
        SSCommand.Parameters.Add("@tipoFicha", SqlDbType.NVarChar).Value = tipoFicha
        SSCommand.Parameters.Add("@usuarioEnvioID", SqlDbType.NVarChar).Value = usuarioEnvioID
        SSCommand.Parameters.Add("@usuarioEnvioNombre", SqlDbType.NVarChar).Value = usuarioEnvioNombre
        SSCommand.Parameters.Add("@usuarioIP", SqlDbType.NVarChar).Value = usuarioIP
        SSCommand.Parameters.Add("@clienteID", SqlDbType.NVarChar).Value = clienteID
        SSCommand.Parameters.Add("@creditoID", SqlDbType.NVarChar).Value = creditoID
        SSCommand.Parameters.Add("@tipoPago", SqlDbType.NVarChar).Value = tipoPago
        SSCommand.Parameters.Add("@totalPagar", SqlDbType.NVarChar).Value = totalPagar
        SSCommand.Parameters.Add("@fechaPago", SqlDbType.NVarChar).Value = fechaPago
        SSCommand.Parameters.Add("@observaciones", SqlDbType.NVarChar).Value = observaciones
        SSCommand.Parameters.Add("@valorNetoActivo", SqlDbType.NVarChar).Value = valorNetoActivo
        SSCommand.Parameters.Add("@capital", SqlDbType.NVarChar).Value = capital
        SSCommand.Parameters.Add("@interes", SqlDbType.NVarChar).Value = interes
        SSCommand.Parameters.Add("@ivaInteres", SqlDbType.NVarChar).Value = ivaInteres
        SSCommand.Parameters.Add("@moratorio", SqlDbType.NVarChar).Value = moratorio
        SSCommand.Parameters.Add("@ivaMoratorio", SqlDbType.NVarChar).Value = ivaMoratorio
        SSCommand.Parameters.Add("@comisiones", SqlDbType.NVarChar).Value = comisiones
        SSCommand.Parameters.Add("@ivaComisiones", SqlDbType.NVarChar).Value = ivaComisiones
        SSCommand.Parameters.Add("@cC_comisiones", SqlDbType.NVarChar).Value = cC_comisiones
        SSCommand.Parameters.Add("@cC_moratorios", SqlDbType.NVarChar).Value = cC_moratorios
        SSCommand.Parameters.Add("@cC_interes", SqlDbType.NVarChar).Value = cC_interes
        SSCommand.Parameters.Add("@cC_capital", SqlDbType.NVarChar).Value = cC_capital
        SSCommand.Parameters.Add("@gastosCobranza", SqlDbType.NVarChar).Value = gastosCobranza
        SSCommand.Parameters.Add("@ivaGastosCob", SqlDbType.NVarChar).Value = ivaGastosCob
        SSCommand.Parameters.Add("@honorario", SqlDbType.NVarChar).Value = honorario
        SSCommand.Parameters.Add("@despachoID", SqlDbType.NVarChar).Value = despachoID
        SSCommand.Parameters.Add("@despachoNombre", SqlDbType.NVarChar).Value = despachoNombre
        SSCommand.Parameters.Add("@tipoDespacho", SqlDbType.NVarChar).Value = tipoDespacho
        SSCommand.Parameters.Add("@abogadoID", SqlDbType.NVarChar).Value = abogadoID
        SSCommand.Parameters.Add("@abogadoNombre", SqlDbType.NVarChar).Value = abogadoNombre
        SSCommand.Parameters.Add("@supervisorID", SqlDbType.NVarChar).Value = supervisorID
        SSCommand.Parameters.Add("@supervisorNombre", SqlDbType.NVarChar).Value = supervisorNombre
        SSCommand.Parameters.Add("@folioDacion", SqlDbType.NVarChar).Value = folioDacion
        SSCommand.Parameters.Add("@descripcion", SqlDbType.NVarChar).Value = descripcion
        SSCommand.Parameters.Add("@valorComercial", SqlDbType.NVarChar).Value = valorComercial
        SSCommand.Parameters.Add("@valorAplicar", SqlDbType.NVarChar).Value = valorAplicar
        SSCommand.Parameters.Add("@numCuenta", SqlDbType.NVarChar).Value = numCuenta
        SSCommand.Parameters.Add("@monto", SqlDbType.NVarChar).Value = monto
        SSCommand.Parameters.Add("@V_debe_A", SqlDbType.NVarChar).Value = V_debe_A
        SSCommand.Parameters.Add("@v_porchonorario", SqlDbType.NVarChar).Value = porchonorario
        SSCommand.Parameters.Add("@v_bandera", SqlDbType.Decimal).Value = 0

        SSCommand.Parameters.Add("@v_descripcion", SqlDbType.NVarChar).Value = descr
        SSCommand.Parameters.Add("@v_vcomercial", SqlDbType.NVarChar).Value = vcomercial
        SSCommand.Parameters.Add("@v_vaplicar", SqlDbType.NVarChar).Value = vaplicar

        Dim DTFICHA As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return "ok"
    End Function

    Public Shared Function VerificaPuedeCambiarAutorizador(ByVal V_credito As String) As Boolean
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_QUIEN_AUTORIZA"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_MORATORIO", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@V_IMPAGO", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@V_HONORARIOS", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@V_ORDINARIO", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@V_CAPITAL", SqlDbType.NVarChar).Value = V_credito
        SSCommand.Parameters.Add("@V_PUESTO", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = 5

        Dim DtsHist_Fichas As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return IIf(DtsHist_Fichas.Rows(0).Item("CUENTA") = "0", False, True)
    End Function
    Public Shared Function InfMailCambAuotizador(ByVal identificador As String, ByVal bandera As Integer) As DataTable
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_INFCAMBIARAUTORIZADOR"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@identificador", SqlDbType.NVarChar).Value = identificador
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = bandera

        Dim DtsHist_Fichas As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return DtsHist_Fichas
    End Function
    Public Shared Function guardarvalorescorreo(ByVal v_usuario As String, ByVal v_comentario As String, ByVal v_variable1 As String, ByVal v_variable2 As String, ByVal v_variable3 As String, ByVal v_variable4 As String, ByVal v_variable5 As String, ByVal v_variable6 As String, ByVal v_variable7 As String, ByVal v_variable8 As String, ByVal v_variable9 As String, ByVal v_variable10 As String, ByVal v_variable11 As String, ByVal v_variable12 As String, ByVal v_variable13 As String, ByVal v_variable14 As String, ByVal v_variable15 As String, ByVal v_variable16 As String, ByVal v_variable17 As String, ByVal v_variable18 As String, ByVal v_variable19 As String, ByVal v_variable20 As String, ByVal v_variable21 As String, ByVal v_variable22 As String, ByVal v_variable23 As String, ByVal v_variable24 As String, ByVal v_variable25 As String, ByVal v_variable26 As String, ByVal v_variable27 As String, ByVal v_variable28 As String, ByVal v_variable29 As String, ByVal v_variable30 As String, ByVal v_variable31 As String, ByVal v_variable32 As String, ByVal v_variable33 As String, ByVal v_variable34 As String, ByVal v_variable35 As String, ByVal v_variable36 As String, ByVal key As String, ByVal tipocondonacion As Integer, ByVal basehonorario As String, ByVal porccomis As String, ByVal porcmora As String, ByVal porcint As String, ByVal porccap As String) As String
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_GUARDARVALORESCORREO"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_usuario", SqlDbType.NVarChar).Value = v_usuario
        SSCommand.Parameters.Add("@v_comentario", SqlDbType.NVarChar).Value = v_comentario
        SSCommand.Parameters.Add("@v_variable1", SqlDbType.NVarChar).Value = v_variable1
        SSCommand.Parameters.Add("@v_variable2", SqlDbType.NVarChar).Value = v_variable2
        SSCommand.Parameters.Add("@v_variable3", SqlDbType.NVarChar).Value = v_variable3
        SSCommand.Parameters.Add("@v_variable4", SqlDbType.NVarChar).Value = v_variable4
        SSCommand.Parameters.Add("@v_variable5", SqlDbType.NVarChar).Value = v_variable5
        SSCommand.Parameters.Add("@v_variable6", SqlDbType.NVarChar).Value = v_variable6
        SSCommand.Parameters.Add("@v_variable7", SqlDbType.NVarChar).Value = v_variable7
        SSCommand.Parameters.Add("@v_variable8", SqlDbType.NVarChar).Value = v_variable8
        SSCommand.Parameters.Add("@v_variable9", SqlDbType.NVarChar).Value = v_variable9
        SSCommand.Parameters.Add("@v_variable10", SqlDbType.NVarChar).Value = v_variable10
        SSCommand.Parameters.Add("@v_variable11", SqlDbType.NVarChar).Value = v_variable11
        SSCommand.Parameters.Add("@v_variable12", SqlDbType.NVarChar).Value = v_variable12
        SSCommand.Parameters.Add("@v_variable13", SqlDbType.NVarChar).Value = v_variable13
        SSCommand.Parameters.Add("@v_variable14", SqlDbType.NVarChar).Value = v_variable14
        SSCommand.Parameters.Add("@v_variable15", SqlDbType.NVarChar).Value = v_variable15
        SSCommand.Parameters.Add("@v_variable16", SqlDbType.NVarChar).Value = v_variable16
        SSCommand.Parameters.Add("@v_variable17", SqlDbType.NVarChar).Value = v_variable17
        SSCommand.Parameters.Add("@v_variable18", SqlDbType.NVarChar).Value = v_variable18
        SSCommand.Parameters.Add("@v_variable19", SqlDbType.NVarChar).Value = v_variable19
        SSCommand.Parameters.Add("@v_variable20", SqlDbType.NVarChar).Value = v_variable20
        SSCommand.Parameters.Add("@v_variable21", SqlDbType.NVarChar).Value = v_variable21
        SSCommand.Parameters.Add("@v_variable22", SqlDbType.NVarChar).Value = v_variable22
        SSCommand.Parameters.Add("@v_variable23", SqlDbType.NVarChar).Value = v_variable23
        SSCommand.Parameters.Add("@v_variable24", SqlDbType.NVarChar).Value = v_variable24
        SSCommand.Parameters.Add("@v_variable25", SqlDbType.NVarChar).Value = v_variable25
        SSCommand.Parameters.Add("@v_variable26", SqlDbType.NVarChar).Value = v_variable26
        SSCommand.Parameters.Add("@v_variable27", SqlDbType.NVarChar).Value = v_variable27
        SSCommand.Parameters.Add("@v_variable28", SqlDbType.NVarChar).Value = v_variable28
        SSCommand.Parameters.Add("@v_variable29", SqlDbType.NVarChar).Value = v_variable29
        SSCommand.Parameters.Add("@v_variable30", SqlDbType.NVarChar).Value = v_variable30
        SSCommand.Parameters.Add("@v_variable31", SqlDbType.NVarChar).Value = v_variable31
        SSCommand.Parameters.Add("@v_variable32", SqlDbType.NVarChar).Value = v_variable32
        SSCommand.Parameters.Add("@v_variable33", SqlDbType.NVarChar).Value = v_variable33
        SSCommand.Parameters.Add("@v_variable34", SqlDbType.NVarChar).Value = v_variable34
        SSCommand.Parameters.Add("@v_variable35", SqlDbType.NVarChar).Value = v_variable35
        SSCommand.Parameters.Add("@v_variable36", SqlDbType.NVarChar).Value = v_variable36
        SSCommand.Parameters.Add("@v_key", SqlDbType.NVarChar).Value = key
        SSCommand.Parameters.Add("@v_tipocondonacion", SqlDbType.Decimal).Value = tipocondonacion
        SSCommand.Parameters.Add("@v_basehonorario", SqlDbType.Decimal).Value = basehonorario
        SSCommand.Parameters.Add("@v_porccomis", SqlDbType.Decimal).Value = porccomis
        SSCommand.Parameters.Add("@v_porcmora", SqlDbType.Decimal).Value = porcmora
        SSCommand.Parameters.Add("@v_porcint", SqlDbType.Decimal).Value = porcint
        SSCommand.Parameters.Add("@v_porccap", SqlDbType.Decimal).Value = porccap
        SSCommand.Parameters.Add("@v_bandera", SqlDbType.Decimal).Value = 0

        Dim DTFICHA As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return "ok"
    End Function
    Public Shared Function guardarvalorescorreo(ByVal v_usuario As String, ByVal v_comentario As String, ByVal v_variable1 As String, ByVal v_variable2 As String, ByVal v_variable3 As String, ByVal v_variable4 As String, ByVal v_variable5 As String, ByVal v_variable6 As String, ByVal v_variable7 As String, ByVal v_variable8 As String, ByVal v_variable9 As String, ByVal v_variable10 As String, ByVal v_variable11 As String, ByVal v_variable12 As String, ByVal v_variable13 As String, ByVal v_variable14 As String, ByVal v_variable15 As String, ByVal v_variable16 As String, ByVal v_variable17 As String, ByVal v_variable18 As String, ByVal v_variable19 As String, ByVal v_variable20 As String, ByVal v_variable21 As String, ByVal v_variable22 As String, ByVal v_variable23 As String, ByVal v_variable24 As String, ByVal v_variable25 As String, ByVal v_variable26 As String, ByVal v_variable27 As String, ByVal v_variable28 As String, ByVal v_variable29 As String, ByVal v_variable30 As String, ByVal v_variable31 As String, ByVal v_variable32 As String, ByVal v_variable33 As String, ByVal v_variable34 As String, ByVal v_variable35 As String, ByVal v_variable36 As String, ByVal key As String, ByVal tipocondonacion As Integer, ByVal basehonorario As String, ByVal desc As String, vomercial As String, vaplicar As String) As String
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_GUARDARVALORESCORREO"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_usuario", SqlDbType.NVarChar).Value = v_usuario
        SSCommand.Parameters.Add("@v_comentario", SqlDbType.NVarChar).Value = v_comentario
        SSCommand.Parameters.Add("@v_variable1", SqlDbType.NVarChar).Value = v_variable1
        SSCommand.Parameters.Add("@v_variable2", SqlDbType.NVarChar).Value = v_variable2
        SSCommand.Parameters.Add("@v_variable3", SqlDbType.NVarChar).Value = v_variable3
        SSCommand.Parameters.Add("@v_variable4", SqlDbType.NVarChar).Value = v_variable4
        SSCommand.Parameters.Add("@v_variable5", SqlDbType.NVarChar).Value = v_variable5
        SSCommand.Parameters.Add("@v_variable6", SqlDbType.NVarChar).Value = v_variable6
        SSCommand.Parameters.Add("@v_variable7", SqlDbType.NVarChar).Value = v_variable7
        SSCommand.Parameters.Add("@v_variable8", SqlDbType.NVarChar).Value = v_variable8
        SSCommand.Parameters.Add("@v_variable9", SqlDbType.NVarChar).Value = v_variable9
        SSCommand.Parameters.Add("@v_variable10", SqlDbType.NVarChar).Value = v_variable10
        SSCommand.Parameters.Add("@v_variable11", SqlDbType.NVarChar).Value = v_variable11
        SSCommand.Parameters.Add("@v_variable12", SqlDbType.NVarChar).Value = v_variable12
        SSCommand.Parameters.Add("@v_variable13", SqlDbType.NVarChar).Value = v_variable13
        SSCommand.Parameters.Add("@v_variable14", SqlDbType.NVarChar).Value = v_variable14
        SSCommand.Parameters.Add("@v_variable15", SqlDbType.NVarChar).Value = v_variable15
        SSCommand.Parameters.Add("@v_variable16", SqlDbType.NVarChar).Value = v_variable16
        SSCommand.Parameters.Add("@v_variable17", SqlDbType.NVarChar).Value = v_variable17
        SSCommand.Parameters.Add("@v_variable18", SqlDbType.NVarChar).Value = v_variable18
        SSCommand.Parameters.Add("@v_variable19", SqlDbType.NVarChar).Value = v_variable19
        SSCommand.Parameters.Add("@v_variable20", SqlDbType.NVarChar).Value = v_variable20
        SSCommand.Parameters.Add("@v_variable21", SqlDbType.NVarChar).Value = v_variable21
        SSCommand.Parameters.Add("@v_variable22", SqlDbType.NVarChar).Value = v_variable22
        SSCommand.Parameters.Add("@v_variable23", SqlDbType.NVarChar).Value = v_variable23
        SSCommand.Parameters.Add("@v_variable24", SqlDbType.NVarChar).Value = v_variable24
        SSCommand.Parameters.Add("@v_variable25", SqlDbType.NVarChar).Value = v_variable25
        SSCommand.Parameters.Add("@v_variable26", SqlDbType.NVarChar).Value = v_variable26
        SSCommand.Parameters.Add("@v_variable27", SqlDbType.NVarChar).Value = v_variable27
        SSCommand.Parameters.Add("@v_variable28", SqlDbType.NVarChar).Value = v_variable28
        SSCommand.Parameters.Add("@v_variable29", SqlDbType.NVarChar).Value = v_variable29
        SSCommand.Parameters.Add("@v_variable30", SqlDbType.NVarChar).Value = v_variable30
        SSCommand.Parameters.Add("@v_variable31", SqlDbType.NVarChar).Value = v_variable31
        SSCommand.Parameters.Add("@v_variable32", SqlDbType.NVarChar).Value = v_variable32
        SSCommand.Parameters.Add("@v_variable33", SqlDbType.NVarChar).Value = v_variable33
        SSCommand.Parameters.Add("@v_variable34", SqlDbType.NVarChar).Value = v_variable34
        SSCommand.Parameters.Add("@v_variable35", SqlDbType.NVarChar).Value = v_variable35
        SSCommand.Parameters.Add("@v_variable36", SqlDbType.NVarChar).Value = v_variable36
        SSCommand.Parameters.Add("@v_key", SqlDbType.NVarChar).Value = key
        SSCommand.Parameters.Add("@v_tipocondonacion", SqlDbType.Decimal).Value = tipocondonacion
        SSCommand.Parameters.Add("@v_basehonorario", SqlDbType.Decimal).Value = basehonorario
        SSCommand.Parameters.Add("@v_porccomis", SqlDbType.Decimal).Value = 0
        SSCommand.Parameters.Add("@v_porcmora", SqlDbType.Decimal).Value = 0
        SSCommand.Parameters.Add("@v_porcint", SqlDbType.Decimal).Value = 0
        SSCommand.Parameters.Add("@v_porccap", SqlDbType.Decimal).Value = 0
        SSCommand.Parameters.Add("@v_bandera", SqlDbType.Decimal).Value = 1

        Dim DTFICHA As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return "ok"
    End Function
    Public Shared Function guardarespuesta(ByVal respuesta As String, ByVal folioFicha As String, ByVal ENVIO As String) As String
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_GUARDA_RESPUESTAFICHA"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_respuesta", SqlDbType.NVarChar).Value = respuesta
        SSCommand.Parameters.Add("@v_folioFicha", SqlDbType.NVarChar).Value = folioFicha
        SSCommand.Parameters.Add("@V_ENVIO", SqlDbType.NVarChar).Value = ENVIO

        Dim DTFICHA As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return "ok"
    End Function
End Class
