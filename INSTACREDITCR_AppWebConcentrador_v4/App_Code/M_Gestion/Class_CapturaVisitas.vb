Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports Db
Imports System.Globalization
Imports System.IO
Imports Funciones
Imports System.Web.SessionState.HttpSessionState
Imports System.Data
Public Class Class_CapturaVisitas
    Public Shared Function LlenarElementosVisitas(ByVal V_Credito As String, ByVal V_Bandera As String) As Object
        Dim SSCommand As New SqlCommand("SP_HISTORICO_VISITAS")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Credito", SqlDbType.NVarChar).Value = V_Credito
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = V_Bandera

        Dim DtsElemento As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")

        Return DtsElemento
    End Function
    Public Shared Function LlenarElementosCodigos(ByVal Objeto As DropDownList, ByVal V_Valor As String, ByVal V_Producto As String, ByVal V_Perfil As String, ByVal V_Tipo As String, V_Bandera As String) As Object
        Dim SSCommand As New SqlCommand("SP_CODIGOS_GESTION")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Valor", SqlDbType.NVarChar).Value = V_Valor
        SSCommand.Parameters.Add("@V_Producto", SqlDbType.NVarChar).Value = V_Producto
        SSCommand.Parameters.Add("@V_Perfil", SqlDbType.NVarChar).Value = V_Perfil
        SSCommand.Parameters.Add("@V_Tipo", SqlDbType.NVarChar).Value = V_Tipo
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = V_Bandera
        Dim DtsTelefonos As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")

        If Objeto.ID <> "DdlHist_Ge_NoPago" Then
            Objeto.DataTextField = "Descripcion"
            Objeto.DataValueField = "Codigo"
            Objeto.DataSource = DtsTelefonos
            Objeto.DataBind()
            Objeto.Items.Add("Seleccione")
            Objeto.SelectedValue = "Seleccione"
        End If
        Return DtsTelefonos
    End Function

    Public Shared Function VariosQ(ByVal V_Valor As String, ByVal V_Usuario As String, ByVal V_Bandera As String, V_Producto As String) As String
        Dim SSCommand As New SqlCommand("SP_VARIOS_QRS")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_USUARIO", SqlDbType.NVarChar).Value = V_Usuario
        SSCommand.Parameters.Add("@V_VALOR", SqlDbType.NVarChar).Value = V_Valor
        SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = V_Bandera
        SSCommand.Parameters.Add("@V_PRODUCTO", SqlDbType.NVarChar).Value = v_Producto
        Dim DtsVariable As DataTable = Consulta_Procedure(SSCommand, "Tocar")
        If V_Bandera > 5 Then
            Return DtsVariable.Rows(0).Item("Valor")
        Else
            Return ""
        End If
    End Function

    Public Shared Function GuardarVisita(ByVal V_Hist_Vi_Credito As String, ByVal V_Hist_Vi_Producto As String, ByVal V_Hist_Vi_Visitador As String, ByVal V_Hist_Vi_Capturista As String, ByVal V_Hist_Vi_Dtevisita As String, ByVal V_Codaccion As String, ByVal V_Hist_Vi_Codigo As String, ByVal V_Hist_Vi_Resultado As String, ByVal V_Codnopago As String, ByVal V_Hist_Vi_Comentario As String, ByVal V_Hist_Vi_Nombrec As String, ByVal V_Hist_Vi_Parentesco As String, ByVal V_Hist_Vi_Tipodomicilio As String, ByVal V_Hist_Vi_Nivelsocio As String, ByVal V_Hist_Vi_Niveles As String, ByVal V_Hist_Vi_Caracteristicas As String, ByVal V_Hist_Vi_Colorf As String, ByVal V_Hist_Vi_Colorp As String, ByVal V_Hist_Vi_Hcontacto As String, ByVal V_Hist_Vi_Dcontacto As Object, ByVal V_Hist_Vi_Referencia As String, ByVal V_Hist_Vi_Fuente As String, ByVal V_Hist_Vi_Agencia As String, ByVal V_Hist_Vi_Entrecalle1 As String, ByVal V_Hist_Vi_Entrecalle2 As String, ByVal V_Anterior As String, ByVal V_Aplicacion_Accion As String, ByVal V_Aplicacion_NoPago As String, ByVal Tipo As Integer, ByVal v_HIST_PR_MONTOPP As String, ByVal v_HIST_PR_DTEPROMESA As String, ByVal v_Hist_Pr_Motivo As String, ByVal v_Hist_Pr_Supervisor As String, ByVal V_Hist_Vi_Folio As String) As String

        If V_Aplicacion_Accion = 1 And V_Codaccion = "Seleccione" Then
            Return "Seleccione Una Acción"
        ElseIf V_Hist_Vi_Visitador = "Seleccione" Then
            Return "Seleccione Un Visitador"
        ElseIf V_Hist_Vi_Resultado = "Seleccione" Then
            Return "Seleccione Un Resultado"
        ElseIf (V_Aplicacion_NoPago = 1 And V_Codnopago = "Seleccione" And V_Hist_Vi_Codigo.Split(",")(1) = 1) Then
            Return "Seleccione Una Causa De No Pago"
        ElseIf V_Hist_Vi_Comentario.Length < 10 Then
            Return "Capture Un Comentario Valido"
        ElseIf V_Hist_Vi_Dtevisita.Length < 10 Then
            Return "Capture La Fecha De La Visita"
        ElseIf (V_Hist_Vi_Parentesco <> "Cliente" And V_Hist_Vi_Parentesco <> "Ninguno") And V_Hist_Vi_Nombrec = "" Then
            Return "Capture El Nombre Del " & V_Hist_Vi_Parentesco
        ElseIf VariosQ(V_Hist_Vi_Dtevisita.Split(" ")(0), "", 6, "CREDIFIEL") = "NO VALIDA" Then
            Return "La Fecha De Visita No Puede Ser Mayor A " & V_Hist_Vi_Dtevisita
        Else
            Dim Dias As String = ""
            For Each gvRow As GridViewRow In V_Hist_Vi_Dcontacto.Rows
                Dim chkSel As CheckBox = DirectCast(gvRow.FindControl("chkSelect"), CheckBox)
                Dias = Dias & Boleano(chkSel.Checked).ToString
            Next
            If Tipo = 1 Then
                Promesa(V_Hist_Vi_Credito, V_Hist_Vi_Producto, v_HIST_PR_MONTOPP, v_HIST_PR_DTEPROMESA, V_Hist_Vi_Visitador, "Parcial", 1, "Terreno", V_Hist_Vi_Agencia, V_Codaccion, V_Hist_Vi_Resultado, V_Hist_Vi_Codigo, V_Codnopago, V_Hist_Vi_Comentario, V_Hist_Vi_Dtevisita, "", 0, V_Anterior)

                Visitas(V_Hist_Vi_Credito, V_Hist_Vi_Producto, V_Hist_Vi_Visitador, V_Hist_Vi_Capturista, V_Hist_Vi_Dtevisita, V_Codaccion, V_Hist_Vi_Codigo, V_Hist_Vi_Resultado, V_Codnopago, V_Hist_Vi_Comentario, V_Hist_Vi_Nombrec, V_Hist_Vi_Parentesco, V_Hist_Vi_Tipodomicilio, V_Hist_Vi_Nivelsocio, V_Hist_Vi_Niveles, V_Hist_Vi_Caracteristicas, V_Hist_Vi_Colorf, V_Hist_Vi_Colorp, V_Hist_Vi_Hcontacto, Dias, V_Hist_Vi_Referencia, V_Hist_Vi_Agencia, V_Hist_Vi_Entrecalle1, V_Hist_Vi_Entrecalle2, V_Hist_Vi_Folio, V_Anterior)
                Return "Visita Capturada"
            ElseIf Tipo = 3 Then
                VariosQ(v_Hist_Pr_Motivo & "," & V_Hist_Vi_Credito, v_Hist_Pr_Supervisor, 9, "CREDIFIEL")

                Promesa(V_Hist_Vi_Credito, V_Hist_Vi_Producto, v_HIST_PR_MONTOPP, v_HIST_PR_DTEPROMESA, V_Hist_Vi_Visitador, "Parcial", 1, "Terreno", V_Hist_Vi_Agencia, V_Codaccion, V_Hist_Vi_Resultado, V_Hist_Vi_Codigo, V_Codnopago, V_Hist_Vi_Comentario, V_Hist_Vi_Dtevisita, "", 0, V_Anterior)

                Visitas(V_Hist_Vi_Credito, V_Hist_Vi_Producto, V_Hist_Vi_Visitador, V_Hist_Vi_Capturista, V_Hist_Vi_Dtevisita, V_Codaccion, V_Hist_Vi_Codigo, V_Hist_Vi_Resultado, V_Codnopago, V_Hist_Vi_Comentario, V_Hist_Vi_Nombrec, V_Hist_Vi_Parentesco, V_Hist_Vi_Tipodomicilio, V_Hist_Vi_Nivelsocio, V_Hist_Vi_Niveles, V_Hist_Vi_Caracteristicas, V_Hist_Vi_Colorf, V_Hist_Vi_Colorp, V_Hist_Vi_Hcontacto, Dias, V_Hist_Vi_Referencia, V_Hist_Vi_Agencia, V_Hist_Vi_Entrecalle1, V_Hist_Vi_Entrecalle2, V_Hist_Vi_Folio, V_Anterior)

                Return "Visita Capturada"
            Else
                Visitas(V_Hist_Vi_Credito, V_Hist_Vi_Producto, V_Hist_Vi_Visitador, V_Hist_Vi_Capturista, V_Hist_Vi_Dtevisita, V_Codaccion, V_Hist_Vi_Codigo, V_Hist_Vi_Resultado, V_Codnopago, V_Hist_Vi_Comentario, V_Hist_Vi_Nombrec, V_Hist_Vi_Parentesco, V_Hist_Vi_Tipodomicilio, V_Hist_Vi_Nivelsocio, V_Hist_Vi_Niveles, V_Hist_Vi_Caracteristicas, V_Hist_Vi_Colorf, V_Hist_Vi_Colorp, V_Hist_Vi_Hcontacto, Dias, V_Hist_Vi_Referencia, V_Hist_Vi_Agencia, V_Hist_Vi_Entrecalle1, V_Hist_Vi_Entrecalle2, V_Hist_Vi_Folio, V_Anterior)
                Return "Visita Capturada"
            End If
        End If
    End Function
    Public Shared Sub Promesa(ByVal v_HIST_PR_CREDITO As String, ByVal v_HIST_PR_PRODUCTO As String, ByVal v_HIST_PR_MONTOPP As String, ByVal v_HIST_PR_DTEPROMESA As String, ByVal v_HIST_PR_USUARIO As String, ByVal v_HIST_PR_TIPO As String, ByVal v_HIST_PR_CONSECUTIVO As String, ByVal v_HIST_PR_ORIGEN As String, ByVal v_HIST_PR_AGENCIA As String, ByVal V_CODACCION As String, ByVal v_HIST_GE_RESULTADO As String, ByVal v_HIST_GE_CODIGO As String, ByVal V_CODNOPAGO As String, ByVal v_HIST_GE_COMENTARIO As String, ByVal v_HIST_VI_DTEVISITA As String, ByVal v_HIST_GE_TELEFONO As String, ByVal v_HIST_GE_INOUTBOUND As String, ByVal V_Anterior As String)
        Dim SSCommand As New SqlCommand("SP_ADD_HIST_PROMESAS")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_HIST_PR_CREDITO", SqlDbType.NVarChar).Value = v_HIST_PR_CREDITO
        SSCommand.Parameters.Add("@v_HIST_PR_PRODUCTO", SqlDbType.NVarChar).Value = v_HIST_PR_PRODUCTO
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
        SSCommand.Parameters.Add("@v_HIST_VI_DTEVISITA", SqlDbType.NVarChar).Value = v_HIST_VI_DTEVISITA & ":00"
        SSCommand.Parameters.Add("@v_HIST_GE_TELEFONO", SqlDbType.NVarChar).Value = v_HIST_GE_TELEFONO
        SSCommand.Parameters.Add("@v_HIST_GE_INOUTBOUND", SqlDbType.Decimal).Value = v_HIST_GE_INOUTBOUND
        SSCommand.Parameters.Add("@V_Anterior", SqlDbType.NVarChar).Value = V_Anterior
        Dim DtsPromesa As DataTable = Consulta_Procedure(SSCommand, "Promesa")
    End Sub
    Public Shared Sub Visitas(ByVal V_Hist_Vi_Credito As String, ByVal V_Hist_Vi_Producto As String, ByVal V_Hist_Vi_Visitador As String, ByVal V_Hist_Vi_Capturista As String, ByVal V_Hist_Vi_Dtevisita As String, ByVal V_Codaccion As String, ByVal V_Hist_Vi_Codigo As String, ByVal V_Hist_Vi_Resultado As String, ByVal V_Codnopago As String, ByVal V_Hist_Vi_Comentario As String, ByVal V_Hist_Vi_Nombrec As String, ByVal V_Hist_Vi_Parentesco As String, ByVal V_Hist_Vi_Tipodomicilio As String, ByVal V_Hist_Vi_Nivelsocio As String, ByVal V_Hist_Vi_Niveles As String, ByVal V_Hist_Vi_Caracteristicas As String, ByVal V_Hist_Vi_Colorf As String, ByVal V_Hist_Vi_Colorp As String, ByVal V_Hist_Vi_Hcontacto As String, ByVal Dias As String, ByVal V_Hist_Vi_Referencia As String, ByVal V_Hist_Vi_Agencia As String, ByVal V_Hist_Vi_Entrecalle1 As String, ByVal V_Hist_Vi_Entrecalle2 As String, ByVal V_Hist_Vi_Folio As String, ByVal V_Anterior As String)
        Dim SSCommand As New SqlCommand("SP_ADD_HIST_VISITAS")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Hist_Vi_Credito", SqlDbType.NVarChar).Value = V_Hist_Vi_Credito
        SSCommand.Parameters.Add("@V_Hist_Vi_Producto", SqlDbType.NVarChar).Value = V_Hist_Vi_Producto
        SSCommand.Parameters.Add("@V_Hist_Vi_Visitador", SqlDbType.NVarChar).Value = V_Hist_Vi_Visitador
        SSCommand.Parameters.Add("@V_Hist_Vi_Capturista", SqlDbType.NVarChar).Value = V_Hist_Vi_Capturista
        SSCommand.Parameters.Add("@V_Hist_Vi_Dtevisita", SqlDbType.NVarChar).Value = V_Hist_Vi_Dtevisita
        SSCommand.Parameters.Add("@V_Codaccion", SqlDbType.NVarChar).Value = V_Codaccion
        SSCommand.Parameters.Add("@V_Hist_Vi_Codigo", SqlDbType.NVarChar).Value = V_Hist_Vi_Codigo.Split(",")(0)
        SSCommand.Parameters.Add("@V_Hist_Vi_Resultado", SqlDbType.NVarChar).Value = V_Hist_Vi_Resultado
        SSCommand.Parameters.Add("@V_Codnopago", SqlDbType.NVarChar).Value = V_Codnopago
        SSCommand.Parameters.Add("@V_Hist_Vi_Comentario", SqlDbType.NVarChar).Value = V_Hist_Vi_Comentario
        SSCommand.Parameters.Add("@V_Hist_Vi_Nombrec", SqlDbType.NVarChar).Value = V_Hist_Vi_Nombrec
        SSCommand.Parameters.Add("@V_Hist_Vi_Parentesco", SqlDbType.NVarChar).Value = V_Hist_Vi_Parentesco
        SSCommand.Parameters.Add("@V_Hist_Vi_Tipodomicilio", SqlDbType.NVarChar).Value = V_Hist_Vi_Tipodomicilio
        SSCommand.Parameters.Add("@V_Hist_Vi_Nivelsocio", SqlDbType.NVarChar).Value = V_Hist_Vi_Nivelsocio
        SSCommand.Parameters.Add("@V_Hist_Vi_Niveles", SqlDbType.NVarChar).Value = V_Hist_Vi_Niveles
        SSCommand.Parameters.Add("@V_Hist_Vi_Caracteristicas", SqlDbType.NVarChar).Value = V_Hist_Vi_Caracteristicas
        SSCommand.Parameters.Add("@V_Hist_Vi_Colorf", SqlDbType.NVarChar).Value = V_Hist_Vi_Colorf
        SSCommand.Parameters.Add("@V_Hist_Vi_Colorp", SqlDbType.NVarChar).Value = V_Hist_Vi_Colorp
        SSCommand.Parameters.Add("@V_Hist_Vi_Hcontacto", SqlDbType.NVarChar).Value = V_Hist_Vi_Hcontacto
        SSCommand.Parameters.Add("@V_Hist_Vi_Dcontacto", SqlDbType.NVarChar).Value = Dias
        SSCommand.Parameters.Add("@V_Hist_Vi_Referencia", SqlDbType.NVarChar).Value = V_Hist_Vi_Referencia
        SSCommand.Parameters.Add("@V_Hist_Vi_Fuente", SqlDbType.NVarChar).Value = "Cliente"
        SSCommand.Parameters.Add("@V_Hist_Vi_Agencia", SqlDbType.NVarChar).Value = V_Hist_Vi_Agencia
        SSCommand.Parameters.Add("@V_Hist_Vi_Entrecalle1", SqlDbType.NVarChar).Value = V_Hist_Vi_Entrecalle1
        SSCommand.Parameters.Add("@V_Hist_Vi_Entrecalle2", SqlDbType.NVarChar).Value = V_Hist_Vi_Entrecalle2
        SSCommand.Parameters.Add("@V_Hist_Vi_Folio", SqlDbType.NVarChar).Value = V_Hist_Vi_Folio
        SSCommand.Parameters.Add("@V_Anterior", SqlDbType.NVarChar).Value = V_Anterior

        Dim odatatableF As DataTable = Consulta_Procedure(SSCommand, "SP_ADD_HIST_VISITAS")
    End Sub
End Class
