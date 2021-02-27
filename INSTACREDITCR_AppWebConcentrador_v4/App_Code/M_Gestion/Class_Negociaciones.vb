Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports Db
Imports System.Globalization
Imports System.IO
Imports Funciones
Imports System.Web.SessionState.HttpSessionState
Imports System.Data
Public Class Class_Negociaciones
    Public Shared Function LlenarElementosNego(ByVal V_Valor As String, ByVal V_Valor2 As String, ByVal V_Valor3 As String, ByVal V_Bandera As String) As Object
        Dim SSCommand As New SqlCommand("SP_NEGOCIACIONES_RG")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Valor", SqlDbType.NVarChar).Value = V_Valor
        SSCommand.Parameters.Add("@V_Valor2", SqlDbType.NVarChar).Value = V_Valor2
        SSCommand.Parameters.Add("@V_Valor3", SqlDbType.NVarChar).Value = V_Valor3
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = V_Bandera
        Dim DtsNegociaciones As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return DtsNegociaciones
    End Function


    Public Shared Function GuardarNego(ByVal V_Hist_Pr_Credito As String, ByVal V_Hist_Pr_Producto As String, ByVal v_HIST_PR_MONTOPP As String, ByVal v_HIST_PR_DTEPROMESA As String, ByVal v_HIST_PR_USUARIO As String, ByVal v_HIST_PR_TIPO As String, ByVal v_HIST_PR_CONSECUTIVO As String, ByVal v_HIST_PR_ORIGEN As String, ByVal v_HIST_PR_AGENCIA As String, ByVal V_CODACCION As String, ByVal v_HIST_GE_RESULTADO As String, ByVal v_HIST_GE_CODIGO As String, ByVal V_CODNOPAGO As String, ByVal v_HIST_GE_COMENTARIO As String, ByVal v_HIST_VI_DTEVISITA As String, ByVal V_Hist_Ge_Telefono As String, ByVal v_HIST_GE_INOUTBOUND As String, ByVal V_Anterior As String, ByVal V_ACTUALIZAR As String, ByVal V_FECHASESGUIMIENTO As String, ByVal V_Hist_Pr_Tipoacuerdo As String, ByVal V_Hist_Pr_Tipodecontacto As String, ByVal V_Hist_Pr_Periodicidad As String, ByVal V_HIST_PR_SDONEGOCIADO As String, ByVal V_HIST_PR_SDODESCUENTO As String, ByVal V_Hist_Pr_Excepcion As String, ByVal V_FILA_T As String, ByVal V_HIST_GE_CALLID As String, ByVal V_HIST_GE_CAMPANAMARCADOR As String, ByVal V_INSTANCIA As String, ByVal v_hist_pr_key As String, ByVal v_folio_ficha As String) As String
        Dim SSCommand As New SqlCommand("SP_ADD_HIST_PROMESAS")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_HIST_PR_CREDITO", SqlDbType.NVarChar).Value = V_Hist_Pr_Credito
        SSCommand.Parameters.Add("@v_HIST_PR_PRODUCTO", SqlDbType.NVarChar).Value = V_Hist_Pr_Producto
        SSCommand.Parameters.Add("@v_HIST_PR_MONTOPP", SqlDbType.Decimal).Value = v_HIST_PR_MONTOPP
        SSCommand.Parameters.Add("@v_HIST_PR_DTEPROMESA", SqlDbType.NVarChar).Value = v_HIST_PR_DTEPROMESA
        SSCommand.Parameters.Add("@v_HIST_PR_USUARIO", SqlDbType.NVarChar).Value = v_HIST_PR_USUARIO
        SSCommand.Parameters.Add("@v_HIST_PR_TIPO", SqlDbType.NVarChar).Value = v_HIST_PR_TIPO
        SSCommand.Parameters.Add("@v_HIST_PR_CONSECUTIVO", SqlDbType.Decimal).Value = v_HIST_PR_CONSECUTIVO
        SSCommand.Parameters.Add("@v_HIST_PR_ORIGEN", SqlDbType.NVarChar).Value = v_HIST_PR_ORIGEN
        SSCommand.Parameters.Add("@v_HIST_PR_AGENCIA", SqlDbType.NVarChar).Value = v_HIST_PR_AGENCIA
        SSCommand.Parameters.Add("@V_CODACCION", SqlDbType.NVarChar).Value = V_CODACCION
        SSCommand.Parameters.Add("@v_HIST_GE_RESULTADO", SqlDbType.NVarChar).Value = v_HIST_GE_RESULTADO
        SSCommand.Parameters.Add("@v_HIST_GE_CODIGO", SqlDbType.NVarChar).Value = v_HIST_GE_CODIGO.Split(",")(0)
        SSCommand.Parameters.Add("@V_CODNOPAGO", SqlDbType.NVarChar).Value = V_CODNOPAGO
        SSCommand.Parameters.Add("@v_HIST_GE_COMENTARIO", SqlDbType.NVarChar).Value = v_HIST_GE_COMENTARIO
        SSCommand.Parameters.Add("@v_HIST_VI_DTEVISITA", SqlDbType.NVarChar).Value = v_HIST_VI_DTEVISITA
        SSCommand.Parameters.Add("@v_HIST_GE_TELEFONO", SqlDbType.NVarChar).Value = V_Hist_Ge_Telefono
        SSCommand.Parameters.Add("@v_HIST_GE_INOUTBOUND", SqlDbType.Decimal).Value = v_HIST_GE_INOUTBOUND
        SSCommand.Parameters.Add("@V_Anterior", SqlDbType.NVarChar).Value = V_Anterior
        SSCommand.Parameters.Add("@V_ACTUALIZAR", SqlDbType.NVarChar).Value = V_ACTUALIZAR
        SSCommand.Parameters.Add("@V_FECHASESGUIMIENTO", SqlDbType.NVarChar).Value = V_FECHASESGUIMIENTO
        SSCommand.Parameters.Add("@V_Hist_Pr_Tipoacuerdo", SqlDbType.NVarChar).Value = V_Hist_Pr_Tipoacuerdo
        SSCommand.Parameters.Add("@V_Hist_Pr_Tipodecontacto", SqlDbType.NVarChar).Value = V_Hist_Pr_Tipodecontacto
        SSCommand.Parameters.Add("@V_Hist_Pr_Periodicidad", SqlDbType.NVarChar).Value = V_Hist_Pr_Periodicidad
        SSCommand.Parameters.Add("@V_HIST_PR_SDONEGOCIADO", SqlDbType.NVarChar).Value = V_HIST_PR_SDONEGOCIADO
        SSCommand.Parameters.Add("@V_HIST_PR_SDODESCUENTO", SqlDbType.NVarChar).Value = V_HIST_PR_SDODESCUENTO
        SSCommand.Parameters.Add("@V_Hist_Pr_Excepcion", SqlDbType.NVarChar).Value = V_Hist_Pr_Excepcion
        SSCommand.Parameters.Add("@V_FILA_T", SqlDbType.NVarChar).Value = V_FILA_T
        SSCommand.Parameters.Add("@V_HIST_GE_CALLID", SqlDbType.NVarChar).Value = V_HIST_GE_CALLID
        SSCommand.Parameters.Add("@V_HIST_GE_CAMPANAMARCADOR", SqlDbType.NVarChar).Value = V_HIST_GE_CAMPANAMARCADOR
        SSCommand.Parameters.Add("@V_INSTANCIA", SqlDbType.NVarChar).Value = V_INSTANCIA
        SSCommand.Parameters.Add("@V_key", SqlDbType.NVarChar).Value = v_hist_pr_key
        SSCommand.Parameters.Add("@v_folio_ficha", SqlDbType.NVarChar).Value = v_folio_ficha
        Dim DtsPromesa As DataTable = Consulta_Procedure(SSCommand, SSCommand.CommandText)

        If DtsPromesa.TableName = "Exception" Then
            Throw New System.Exception(DtsPromesa.Rows(0).Item(0).ToString)
        Else
            Return "Ok"
        End If
    End Function


    Public Shared Function LlenarElementosNego_Fijos(ByVal V_Tabla As String, ByVal V_Tipo As String, ByVal V_Credito As String, ByVal V_Valor1 As String, ByVal V_Valor2 As String, ByVal V_Valor3 As String, ByVal V_Valor4 As String, ByVal V_Bandera As String) As DataTable
        Dim SSCommand As New SqlCommand("SP_NEGOCIACIONES_FIJAS")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Tabla", SqlDbType.NVarChar).Value = V_Tabla
        SSCommand.Parameters.Add("@V_Tipo", SqlDbType.NVarChar).Value = V_Tipo
        SSCommand.Parameters.Add("@V_Credito", SqlDbType.NVarChar).Value = V_Credito
        SSCommand.Parameters.Add("@V_Valor1", SqlDbType.NVarChar).Value = V_Valor1
        SSCommand.Parameters.Add("@V_Valor2", SqlDbType.NVarChar).Value = V_Valor2
        SSCommand.Parameters.Add("@V_Valor3", SqlDbType.NVarChar).Value = V_Valor3
        SSCommand.Parameters.Add("@V_Valor4", SqlDbType.NVarChar).Value = V_Valor4
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = V_Bandera
        Dim DtsNegociaciones As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return DtsNegociaciones
    End Function

    Public Shared Function AplazarNego(ByVal V_Hist_Pr_Credito As String, ByVal V_Hist_Pr_Producto As String, ByVal v_HIST_PR_MONTOPP As String, ByVal v_HIST_PR_DTEPROMESA As String, ByVal v_HIST_PR_USUARIO As String, ByVal v_HIST_PR_TIPO As String, ByVal v_HIST_PR_CONSECUTIVO As String, ByVal v_HIST_PR_ORIGEN As String, ByVal v_HIST_PR_AGENCIA As String, ByVal V_CODACCION As String, ByVal v_HIST_GE_RESULTADO As String, ByVal v_HIST_GE_CODIGO As String, ByVal V_CODNOPAGO As String, ByVal v_HIST_GE_COMENTARIO As String, ByVal v_HIST_VI_DTEVISITA As String, ByVal V_Hist_Ge_Telefono As String, ByVal v_HIST_GE_INOUTBOUND As String, ByVal V_Anterior As String, ByVal V_ACTUALIZAR As String, ByVal V_FECHASESGUIMIENTO As String, ByVal V_Hist_Pr_Tipoacuerdo As String, ByVal V_Hist_Pr_Tipodecontacto As String, ByVal V_Hist_Pr_Periodicidad As String, ByVal V_HIST_PR_SDONEGOCIADO As String, ByVal V_HIST_PR_SDODESCUENTO As String, ByVal V_Hist_Pr_Excepcion As String, ByVal V_FILA_T As String, ByVal V_HIST_GE_CALLID As String, ByVal V_HIST_GE_CAMPANAMARCADOR As String, ByVal V_INSTANCIA As String, ByVal v_hist_pr_key As String, ByVal v_folio_ficha As String) As String
        Dim SSCommand As New SqlCommand("SP_APLAZA_HIST_PROMESAS")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_HIST_PR_CREDITO", SqlDbType.NVarChar).Value = V_Hist_Pr_Credito
        SSCommand.Parameters.Add("@v_HIST_PR_PRODUCTO", SqlDbType.NVarChar).Value = V_Hist_Pr_Producto
        SSCommand.Parameters.Add("@v_HIST_PR_MONTOPP", SqlDbType.Decimal).Value = v_HIST_PR_MONTOPP
        SSCommand.Parameters.Add("@v_HIST_PR_DTEPROMESA", SqlDbType.NVarChar).Value = v_HIST_PR_DTEPROMESA
        SSCommand.Parameters.Add("@v_HIST_PR_USUARIO", SqlDbType.NVarChar).Value = v_HIST_PR_USUARIO
        SSCommand.Parameters.Add("@v_HIST_PR_TIPO", SqlDbType.NVarChar).Value = v_HIST_PR_TIPO
        SSCommand.Parameters.Add("@v_HIST_PR_CONSECUTIVO", SqlDbType.Decimal).Value = v_HIST_PR_CONSECUTIVO
        SSCommand.Parameters.Add("@v_HIST_PR_ORIGEN", SqlDbType.NVarChar).Value = v_HIST_PR_ORIGEN
        SSCommand.Parameters.Add("@v_HIST_PR_AGENCIA", SqlDbType.NVarChar).Value = v_HIST_PR_AGENCIA
        SSCommand.Parameters.Add("@V_CODACCION", SqlDbType.NVarChar).Value = V_CODACCION
        SSCommand.Parameters.Add("@v_HIST_GE_RESULTADO", SqlDbType.NVarChar).Value = v_HIST_GE_RESULTADO
        SSCommand.Parameters.Add("@v_HIST_GE_CODIGO", SqlDbType.NVarChar).Value = v_HIST_GE_CODIGO.Split(",")(0)
        SSCommand.Parameters.Add("@V_CODNOPAGO", SqlDbType.NVarChar).Value = V_CODNOPAGO
        SSCommand.Parameters.Add("@v_HIST_GE_COMENTARIO", SqlDbType.NVarChar).Value = v_HIST_GE_COMENTARIO
        SSCommand.Parameters.Add("@v_HIST_VI_DTEVISITA", SqlDbType.NVarChar).Value = v_HIST_VI_DTEVISITA
        SSCommand.Parameters.Add("@v_HIST_GE_TELEFONO", SqlDbType.NVarChar).Value = V_Hist_Ge_Telefono
        SSCommand.Parameters.Add("@v_HIST_GE_INOUTBOUND", SqlDbType.Decimal).Value = v_HIST_GE_INOUTBOUND
        SSCommand.Parameters.Add("@V_Anterior", SqlDbType.NVarChar).Value = V_Anterior
        SSCommand.Parameters.Add("@V_ACTUALIZAR", SqlDbType.NVarChar).Value = V_ACTUALIZAR
        SSCommand.Parameters.Add("@V_FECHASESGUIMIENTO", SqlDbType.NVarChar).Value = V_FECHASESGUIMIENTO
        SSCommand.Parameters.Add("@V_Hist_Pr_Tipoacuerdo", SqlDbType.NVarChar).Value = V_Hist_Pr_Tipoacuerdo
        SSCommand.Parameters.Add("@V_Hist_Pr_Tipodecontacto", SqlDbType.NVarChar).Value = V_Hist_Pr_Tipodecontacto
        SSCommand.Parameters.Add("@V_Hist_Pr_Periodicidad", SqlDbType.NVarChar).Value = V_Hist_Pr_Periodicidad
        SSCommand.Parameters.Add("@V_HIST_PR_SDONEGOCIADO", SqlDbType.NVarChar).Value = V_HIST_PR_SDONEGOCIADO
        SSCommand.Parameters.Add("@V_HIST_PR_SDODESCUENTO", SqlDbType.NVarChar).Value = V_HIST_PR_SDODESCUENTO
        SSCommand.Parameters.Add("@V_Hist_Pr_Excepcion", SqlDbType.NVarChar).Value = V_Hist_Pr_Excepcion
        SSCommand.Parameters.Add("@V_FILA_T", SqlDbType.NVarChar).Value = V_FILA_T
        SSCommand.Parameters.Add("@V_HIST_GE_CALLID", SqlDbType.NVarChar).Value = V_HIST_GE_CALLID
        SSCommand.Parameters.Add("@V_HIST_GE_CAMPANAMARCADOR", SqlDbType.NVarChar).Value = V_HIST_GE_CAMPANAMARCADOR
        SSCommand.Parameters.Add("@V_INSTANCIA", SqlDbType.NVarChar).Value = V_INSTANCIA
        SSCommand.Parameters.Add("@V_key", SqlDbType.NVarChar).Value = v_hist_pr_key
        SSCommand.Parameters.Add("@v_folio_ficha", SqlDbType.NVarChar).Value = v_folio_ficha
        Dim DtsPromesa As DataTable = Consulta_Procedure(SSCommand, "Promesa")
        Return " "
    End Function
End Class
