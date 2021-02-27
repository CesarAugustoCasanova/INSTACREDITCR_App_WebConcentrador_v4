Imports System.Data
Imports System.Data.SqlClient
Imports Db
Imports Telerik.Web.UI

Public Class Class_MasterPage
    Public Shared Function Telefonos(ByVal V_Credito As String, ByVal V_Campo As String, ByVal V_CampoFecha As String, ByVal V_Tabla As String, ByVal V_Tipo As String, ByVal V_Calificacion As String, ByVal V_Telefono As String, ByVal V_Bandera As String) As Object
        Dim SSCommand As New SqlCommand("Sp_Telefonos")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Credito", SqlDbType.NVarChar).Value = V_Credito
        SSCommand.Parameters.Add("@V_Tabla", SqlDbType.NVarChar).Value = V_Tabla
        SSCommand.Parameters.Add("@V_Campo", SqlDbType.NVarChar).Value = V_Campo
        SSCommand.Parameters.Add("@V_CampoFecha", SqlDbType.NVarChar).Value = V_CampoFecha
        SSCommand.Parameters.Add("@V_Tipo", SqlDbType.NVarChar).Value = V_Tipo
        SSCommand.Parameters.Add("@V_Calificacion", SqlDbType.NVarChar).Value = V_Calificacion
        SSCommand.Parameters.Add("@V_Telefono", SqlDbType.NVarChar).Value = V_Telefono
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = V_Bandera
        Dim DtsTelefonos As DataTable = Consulta_Procedure(SSCommand, "Telefonos")
        Return DtsTelefonos
    End Function
    Public Shared Function Agenda(ByVal V_Usuario As String, ByVal V_Bandera As String) As Object
        Dim SSCommand As New SqlCommand("Sp_Filasagenda")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Valor", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@V_Usuario", SqlDbType.NVarChar).Value = V_Usuario
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = V_Bandera
        Dim DtsAgenda As DataTable = Consulta_Procedure(SSCommand, "Agenda")
        Return DtsAgenda
    End Function
    Public Shared Sub Tocar(ByVal V_Valor As String, ByVal V_Usuario As String, ByVal v_producto As String)
        Dim SSCommand As New SqlCommand("SP_VARIOS_QRS")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_USUARIO", SqlDbType.NVarChar).Value = V_Usuario
        SSCommand.Parameters.Add("@v_producto", SqlDbType.NVarChar).Value = v_producto
        SSCommand.Parameters.Add("@V_VALOR", SqlDbType.NVarChar).Value = V_Valor
        SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.NVarChar).Value = 1
        Dim DtsVariable As DataTable = Consulta_Procedure(SSCommand, SSCommand.CommandText)
    End Sub
    Public Shared Sub MarcarLeidos(ByVal V_Valor As String, ByVal V_Usuario As String)

        Dim SSCommand As New SqlCommand("SP_VARIOS_QRS")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_USUARIO", SqlDbType.NVarChar).Value = V_Usuario
        SSCommand.Parameters.Add("@V_VALOR", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.NVarChar).Value = V_Valor
        Dim DtsVariable As DataTable = Consulta_Procedure(SSCommand, SSCommand.CommandText)
    End Sub
    Public Shared Function LlenarElementosMaster(ByVal Objeto As RadDropDownList, ByVal V_Valor As String, ByVal V_Producto As String, ByVal V_Perfil As String, ByVal V_Tipo As String, V_Bandera As String, Optional ByVal V_Instancia As String = "") As Object
        Dim SSCommand As New SqlCommand("SP_CODIGOS_GESTION")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Valor", SqlDbType.NVarChar).Value = V_Valor
        SSCommand.Parameters.Add("@V_Producto", SqlDbType.NVarChar).Value = V_Producto
        SSCommand.Parameters.Add("@V_Instancia", SqlDbType.NVarChar).Value = V_Instancia
        SSCommand.Parameters.Add("@V_Perfil", SqlDbType.NVarChar).Value = V_Perfil
        SSCommand.Parameters.Add("@V_Tipo", SqlDbType.NVarChar).Value = V_Tipo
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = V_Bandera
        Dim Dts As DataTable = Consulta_Procedure(SSCommand, SSCommand.CommandText)
        Objeto.ClearSelection()
        Objeto.Items.Clear()

        If Objeto.ID <> "DdlHist_Ge_NoPago" Then
            Objeto.DataTextField = "Descripcion"
            Objeto.DataValueField = "Codigo"
            Objeto.DataSource = Dts
            Objeto.DataBind()
            Objeto.Items.Add("Seleccione")
            Objeto.SelectedText = "Seleccione"
        End If
        Return Dts
    End Function

    Public Shared Function LlenarElementosMaster(ByVal Objeto As RadComboBox, ByVal V_Valor As String, ByVal V_Producto As String, ByVal V_Perfil As String, ByVal V_Tipo As String, V_Bandera As String, Optional ByVal V_Instancia As String = "") As Object
        Dim SSCommand As New SqlCommand("SP_CODIGOS_GESTION")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Valor", SqlDbType.NVarChar).Value = V_Valor
        SSCommand.Parameters.Add("@V_Producto", SqlDbType.NVarChar).Value = V_Producto
        SSCommand.Parameters.Add("@V_Instancia", SqlDbType.NVarChar).Value = V_Instancia
        SSCommand.Parameters.Add("@V_Perfil", SqlDbType.NVarChar).Value = V_Perfil
        SSCommand.Parameters.Add("@V_Tipo", SqlDbType.NVarChar).Value = V_Tipo
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = V_Bandera
        Dim Dts As DataTable = Consulta_Procedure(SSCommand, SSCommand.CommandText)
        Objeto.ClearSelection()
        Objeto.Items.Clear()

        If Objeto.ID <> "DdlHist_Ge_NoPago" Then
            Objeto.DataTextField = "Descripcion"
            Objeto.DataValueField = "Codigo"
            Objeto.DataSource = Dts
            Objeto.DataBind()
        End If
        Return Dts
    End Function

    Public Shared Function AddPromesa(ByVal v_HIST_PR_CREDITO As String, ByVal v_HIST_PR_PRODUCTO As String, ByVal v_HIST_PR_MONTOPP As String, ByVal v_HIST_PR_DTEPROMESA As String, ByVal v_HIST_PR_USUARIO As String, ByVal v_HIST_PR_TIPO As String, ByVal v_HIST_PR_CONSECUTIVO As String, ByVal v_HIST_PR_ORIGEN As String, ByVal v_HIST_PR_AGENCIA As String, ByVal V_CODACCION As String, ByVal v_HIST_GE_RESULTADO As String, ByVal v_HIST_GE_CODIGO As String, ByVal V_CODNOPAGO As String, ByVal v_HIST_GE_COMENTARIO As String, ByVal v_HIST_VI_DTEVISITA As String, ByVal V_Hist_Ge_Telefono As String, ByVal v_HIST_GE_INOUTBOUND As String, ByVal V_ANTERIOR As String, ByVal V_ACTUALIZAR As String, ByVal V_FECHASESGUIMIENTO As String, ByVal V_FILA_T As String, ByVal V_HIST_GE_CALLID As String, ByVal V_HIST_GE_CAMPANAMARCADOR As String, ByVal V_INSTANCIA As String, ByVal v_participante As String, ByVal v_tipoPago As String) As DataTable
        Dim SSCommand As New SqlCommand("SP_ADD_HIST_PROMESAS")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Hist_Pr_Credito", SqlDbType.NVarChar).Value = v_HIST_PR_CREDITO
        SSCommand.Parameters.Add("@V_Hist_Pr_Producto", SqlDbType.NVarChar).Value = v_HIST_PR_PRODUCTO
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
        SSCommand.Parameters.Add("@V_HIST_GE_COMENTARIO", SqlDbType.NVarChar).Value = v_HIST_GE_COMENTARIO
        SSCommand.Parameters.Add("@v_HIST_VI_DTEVISITA", SqlDbType.NVarChar).Value = v_HIST_VI_DTEVISITA
        SSCommand.Parameters.Add("@V_Hist_Ge_Telefono", SqlDbType.NVarChar).Value = V_Hist_Ge_Telefono
        SSCommand.Parameters.Add("@v_HIST_GE_INOUTBOUND", SqlDbType.Decimal).Value = v_HIST_GE_INOUTBOUND
        SSCommand.Parameters.Add("@V_ANTERIOR", SqlDbType.NVarChar).Value = V_ANTERIOR
        SSCommand.Parameters.Add("@V_Actualizar", SqlDbType.NVarChar).Value = V_ACTUALIZAR
        SSCommand.Parameters.Add("@V_Fechasesguimiento", SqlDbType.NVarChar).Value = CType(V_FECHASESGUIMIENTO, DateTime).ToString("yyyy-MM-dd") + " 12:00:00"
        SSCommand.Parameters.Add("@V_FILA_T", SqlDbType.NVarChar).Value = V_FILA_T
        SSCommand.Parameters.Add("@V_HIST_GE_CALLID", SqlDbType.NVarChar).Value = V_HIST_GE_CALLID
        SSCommand.Parameters.Add("@V_HIST_GE_CAMPANAMARCADOR", SqlDbType.NVarChar).Value = V_HIST_GE_CAMPANAMARCADOR
        SSCommand.Parameters.Add("@V_INSTANCIA", SqlDbType.NVarChar).Value = V_INSTANCIA
        SSCommand.Parameters.Add("@V_HIST_GE_PARTICIPANTE", SqlDbType.NVarChar).Value = v_participante
        SSCommand.Parameters.Add("@V_tipoPago", SqlDbType.NVarChar).Value = v_tipoPago

        Dim DtsPromesa As DataTable = Consulta_Procedure(SSCommand, "Promesa")
        Return DtsPromesa
    End Function
    Public Shared Function AddGestion(ByVal V_Hist_Ge_Credito As String, ByVal V_Hist_Ge_Producto As String, ByVal V_Hist_Ge_Usuario As String, ByVal V_Codaccion As String, ByVal V_Hist_Ge_Resultado As String, ByVal V_Codresult As String, ByVal V_Codnopago As String, ByVal V_Hist_Ge_Comentario As String, ByVal V_Hist_Ge_Inoutbound As String, ByVal V_Hist_Ge_Telefono As String, ByVal V_Hist_Ge_Agencia As String, ByVal v_Hist_Pr_Dtepromesa As String, ByVal V_Anterior As String, ByVal V_FILA_T As String, ByVal V_HIST_GE_CALLID As String, ByVal V_HIST_GE_CAMPANAMARCADOR As String, ByVal V_INSTANCIA As String, ByVal V_Participante As String, v_tipopago As String, v_parentesco As String) As DataTable
        Dim SSCommand As New SqlCommand("SP_ADD_HIST_GESTIONES")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Hist_Ge_Credito", SqlDbType.NVarChar).Value = V_Hist_Ge_Credito
        SSCommand.Parameters.Add("@V_Hist_Ge_Producto", SqlDbType.NVarChar).Value = V_Hist_Ge_Producto
        SSCommand.Parameters.Add("@V_Hist_Ge_Usuario", SqlDbType.NVarChar).Value = V_Hist_Ge_Usuario
        SSCommand.Parameters.Add("@V_Codaccion", SqlDbType.NVarChar).Value = V_Codaccion
        SSCommand.Parameters.Add("@V_Hist_Ge_Resultado", SqlDbType.NVarChar).Value = V_Hist_Ge_Resultado
        SSCommand.Parameters.Add("@V_Codresult", SqlDbType.NVarChar).Value = V_Codresult
        SSCommand.Parameters.Add("@V_Codnopago", SqlDbType.NVarChar).Value = V_Codnopago
        SSCommand.Parameters.Add("@V_Hist_Ge_Comentario", SqlDbType.NVarChar).Value = V_Hist_Ge_Comentario
        SSCommand.Parameters.Add("@V_Hist_Ge_Inoutbound", SqlDbType.NVarChar).Value = V_Hist_Ge_Inoutbound
        SSCommand.Parameters.Add("@V_Hist_Ge_Telefono", SqlDbType.NVarChar).Value = V_Hist_Ge_Telefono
        SSCommand.Parameters.Add("@V_Hist_Ge_Agencia", SqlDbType.NVarChar).Value = V_Hist_Ge_Agencia
        SSCommand.Parameters.Add("@V_Dtecontacto", SqlDbType.NVarChar).Value = v_Hist_Pr_Dtepromesa
        SSCommand.Parameters.Add("@V_Anterior", SqlDbType.NVarChar).Value = V_Anterior
        SSCommand.Parameters.Add("@V_FILA_T", SqlDbType.NVarChar).Value = V_FILA_T
        SSCommand.Parameters.Add("@V_HIST_GE_CALLID", SqlDbType.NVarChar).Value = V_HIST_GE_CALLID
        SSCommand.Parameters.Add("@V_HIST_GE_CAMPANAMARCADOR", SqlDbType.NVarChar).Value = V_HIST_GE_CAMPANAMARCADOR
        SSCommand.Parameters.Add("@V_INSTANCIA", SqlDbType.NVarChar).Value = V_INSTANCIA
        SSCommand.Parameters.Add("@V_HIST_GE_PARTICIPANTE", SqlDbType.NVarChar).Value = V_Participante
        SSCommand.Parameters.Add("@V_tipoPago", SqlDbType.NVarChar).Value = v_tipopago
        SSCommand.Parameters.Add("@v_parentesco", SqlDbType.NVarChar).Value = v_parentesco
        Dim DtsGestion As DataTable = Consulta_Procedure(SSCommand, "Gestion")
        Return DtsGestion
    End Function

    Public Shared Sub CancelarPP(ByVal V_Valor As String, ByVal V_Usuario As String, ByVal V_Bandera As String)
        Dim SSCommand As New SqlCommand("SP_VARIOS_QRS")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_USUARIO", SqlDbType.NVarChar).Value = V_Usuario
        SSCommand.Parameters.Add("@V_VALOR", SqlDbType.NVarChar).Value = V_Valor
        SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.NVarChar).Value = V_Bandera
        SSCommand.Parameters.Add("@V_PRODUCTO", SqlDbType.NVarChar).Value = "CREDIFIEL"
        Dim DtsVariable As DataTable = Consulta_Procedure(SSCommand, "Tocar")
    End Sub

    Public Shared Function RegresaCredito(ByVal V_Valor As String, ByVal V_Usuario As String, ByVal V_Bandera As String) As DataTable
        Dim SSCommand As New SqlCommand("SP_VARIOS_QRS")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_USUARIO", SqlDbType.NVarChar).Value = V_Usuario
        SSCommand.Parameters.Add("@V_VALOR", SqlDbType.NVarChar).Value = V_Valor
        SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.NVarChar).Value = V_Bandera
        Dim DtsVariable As DataTable = Consulta_Procedure(SSCommand, "Credito")
        Return DtsVariable
    End Function
    Public Shared Function LlenarGridDirecciones(ByVal V_Credito As String, ByVal V_Bandera As String) As Object
        Dim SSCommand As New SqlCommand("SP_VARIOS_QRS")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_USUARIO", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@V_VALOR", SqlDbType.NVarChar).Value = V_Credito
        SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.NVarChar).Value = V_Bandera
        Dim DtsVariable As DataTable = Consulta_Procedure(SSCommand, "Direcciones")
        Return DtsVariable
    End Function
End Class

