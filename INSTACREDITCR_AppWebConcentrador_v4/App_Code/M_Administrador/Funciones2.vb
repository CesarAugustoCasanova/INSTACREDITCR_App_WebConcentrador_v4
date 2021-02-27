Imports System.Data
Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports Db
Imports System.Globalization
Imports System.IO
Imports System

Public Class Funciones2
    Public Shared Function DStoJSON(ds As DataTable) As String
        Dim json As New StringBuilder()

        For Each dr As DataRow In ds.Rows
            json.Append("{")

            Dim i As Integer = 0
            Dim colcount As Integer = dr.Table.Columns.Count

            For Each dc As DataColumn In dr.Table.Columns
                json.Append("""")
                json.Append(HttpUtility.HtmlDecode(dc.ColumnName))
                json.Append("""")
                json.Append(":")
                json.Append("""")
                json.Append(HttpUtility.HtmlDecode(NuloAVacio(dr(dc))).Replace(Chr(34), "").Replace(Chr(10), "").Replace(Chr(92), "/"))
                json.Append("""")

                i += 1
                If i < colcount Then
                    json.Append(",")
                End If
            Next
            json.Append("}")
            json.Append(",")
        Next

        Return json.ToString()

    End Function

    Public Shared Function NuloAVacio(valor As Object) As String
        If Not IsDBNull(valor) Then
            Return valor.ToString()
        Else
            Return " "
        End If
    End Function
    Public Shared Sub EnviarCorreo(ByVal Pantalla As String, ByVal Evento As String, ByVal ex As Exception, ByVal Mgcta As String, ByVal Captura As String, ByVal usr As String)
        dim SSCommand as new sqlcommand
        SSCommand.CommandText = "SP_ENVIAR_CORREO"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_Subject", SqlDbType.NVarChar).Value = "Administrador WEB"
        SSCommand.Parameters.Add("@V_Pantalla", SqlDbType.NVarChar).Value = Pantalla
        SSCommand.Parameters.Add("@V_Evento", SqlDbType.NVarChar).Value = Evento
        SSCommand.Parameters.Add("@V_Error", SqlDbType.NVarChar).Value = ex.ToString
        SSCommand.Parameters.Add("@V_CREDITO", SqlDbType.NVarChar).Value = Mgcta
        SSCommand.Parameters.Add("@V_Captura", SqlDbType.NVarChar).Value = Captura
        SSCommand.Parameters.Add("@V_Usuario", SqlDbType.NVarChar).Value = usr
        Ejecuta_Procedure(SSCommand)
    End Sub
    Public Shared Sub EnviarCorreoSinEx(ByVal Pantalla As String, ByVal Evento As String, ByVal ex As String, ByVal Mgcta As String, ByVal Captura As String, ByVal usr As String)
        dim SSCommand as new sqlcommand
        SSCommand.CommandText = "SP_ENVIAR_CORREO"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_Subject", SqlDbType.NVarChar).Value = "Administrador WEB"
        SSCommand.Parameters.Add("@V_Pantalla", SqlDbType.NVarChar).Value = Pantalla
        SSCommand.Parameters.Add("@V_Evento", SqlDbType.NVarChar).Value = Evento
        SSCommand.Parameters.Add("@V_Error", SqlDbType.NVarChar).Value = ex
        SSCommand.Parameters.Add("@V_CREDITO", SqlDbType.NVarChar).Value = Mgcta
        SSCommand.Parameters.Add("@V_Captura", SqlDbType.NVarChar).Value = Captura
        SSCommand.Parameters.Add("@V_Usuario", SqlDbType.NVarChar).Value = usr
        Ejecuta_Procedure(SSCommand)
    End Sub

    Public Shared Sub OffLine(ByVal Usuario As String)
        Dim DtsVariable As DataTable
        Dim SSCommand as new sqlcommand
        SSCommand.CommandText = "SP_LICENCIA"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_USUARIO", SqlDbType.NVarChar).Value = Usuario
        SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 4

        DtsVariable = Consulta_Procedure(SSCommand, "Licencias")
    End Sub

    Public Shared Sub AUDITORIA(ByVal USUARIO As String, ByVal MODULO As String, ByVal PAGINA As String, ByVal CREDITO As String, ByVal EVENTO As String, ByVal VALOR As String, ByVal IPPUBLICA As String, ByVal IPPRIVADA As String)
        dim SSCommand as new sqlcommand
        SSCommand.CommandText = "SP_AUDITORIA"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_USUARIO", SqlDbType.NVarChar).Value = USUARIO
        SSCommand.Parameters.Add("@V_MODULO", SqlDbType.NVarChar).Value = MODULO
        SSCommand.Parameters.Add("@V_PAGINA", SqlDbType.NVarChar).Value = PAGINA
        SSCommand.Parameters.Add("@V_CREDITO", SqlDbType.NVarChar).Value = CREDITO
        SSCommand.Parameters.Add("@V_EVENTO", SqlDbType.NVarChar).Value = EVENTO
        SSCommand.Parameters.Add("@V_VALOR", SqlDbType.NVarChar).Value = VALOR
        SSCommand.Parameters.Add("@V_IPPUBLICA", SqlDbType.NVarChar).Value = IPPUBLICA
        SSCommand.Parameters.Add("@V_IPPRIVADA", SqlDbType.NVarChar).Value = IPPRIVADA
        Ejecuta_Procedure(SSCommand)
    End Sub

    Public Shared Function EmailValida(ByVal correo As String) As Boolean
        Dim atCnt As String
        Dim revCorreo As Boolean = 0
        If Len(correo) < 5 Then
            revCorreo = True
        ElseIf InStr(correo, "@") = 0 Then
            revCorreo = True
        ElseIf InStr(correo, ".") = 0 Then
            revCorreo = True
        ElseIf Len(correo) - InStrRev(correo, ".") > 4 Then
            revCorreo = True
        Else
            atCnt = 0
            For i = 1 To Len(correo)
                If Mid(correo, i, 1) = "@" Then
                    atCnt = atCnt + 1
                End If
            Next
            If atCnt > 1 Then
                revCorreo = True
            End If
            For i = 1 To Len(correo)
                If Not IsNumeric(Mid(correo, i, 1)) And
          (LCase(Mid(correo, i, 1)) < "a" Or
          LCase(Mid(correo, i, 1)) > "z") And
          Mid(correo, i, 1) <> "_" And
          Mid(correo, i, 1) <> "." And
          Mid(correo, i, 1) <> "@" And
          Mid(correo, i, 1) <> "-" Then
                    revCorreo = True
                End If
            Next
        End If
        Return revCorreo
    End Function

    Public Shared Function Boleano(ByVal valor As String) As Integer
        Dim bin As Integer
        If valor = True Then
            bin = 1
        Else
            bin = 0
        End If
        Return bin
    End Function

    Public Shared Function Boleano2(ByVal valor As Integer) As Boolean
        Dim bin As Boolean
        If valor = 1 Then
            bin = True
        Else
            bin = False
        End If
        Return bin
    End Function

    Public Shared Function ValidaMonto(ByVal cadena As String) As Integer
        Dim CuantosCaracteres As Integer
        Dim MontoValido As Integer = 0
        CuantosCaracteres = 0
        For i = 1 To Len(cadena)
            If Mid(cadena, i, 1) = "." Then
                CuantosCaracteres = CuantosCaracteres + 1
            End If
        Next
        If CuantosCaracteres > 1 Then
            MontoValido = 1
        Else
            MontoValido = 0
        End If
        Return MontoValido
    End Function

    Public Shared Function ppvigente(ByVal cuenta As String) As Integer
        dim SSCommand as new sqlcommand
        SSCommand.CommandText = "SP_VALIDAPP"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_CREDITO", SqlDbType.NVarChar).Value = cuenta

        Dim oDataset As DataTable = Consulta_Procedure(SSCommand, "VALIDAPP")
        Dim VALIDA As Integer = oDataset.Rows(0)("EXISTE")
        Return VALIDA
    End Function

    Public Shared Function to_money(ByRef numero As String) As String
        Dim valor As Double = CDbl(Val(numero))
        to_money = "$ " & (valor.ToString("N", CultureInfo.InvariantCulture))
        Return to_money
    End Function

    Public Shared Function to_codigos(ByVal VALOR As String, ByVal campo As Integer) As String
        Dim extrae() As String = VALOR.Split("|")
        Return extrae(campo)
    End Function

    Public Shared Sub to_excel(ByRef pagina As Page, ByVal control As Control, ByVal file As String)
        Dim sb As New StringBuilder()
        Dim sw As New StringWriter(sb)
        Dim htw As New HtmlTextWriter(sw)
        Dim Page As New Page()
        Dim Form As New HtmlForm()
        control.EnableViewState = False
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(Form)
        Form.Controls.Add(control)
        Page.RenderControl(htw)
        pagina.Response.Clear()
        pagina.Response.Clear()
        pagina.Response.Buffer = True
        pagina.Response.ContentType = "text/plain"
        Dim fecha As String = Now.ToString("ddMMyyyy")
        pagina.Response.AddHeader("Content-Disposition", "attachment;filename=" & file + fecha & ".xls")
        pagina.Response.Charset = "UTF-8"
        pagina.Response.ContentEncoding = Encoding.Default
        pagina.Response.Write(sb.ToString())
        pagina.Response.End()
    End Sub

    Public Shared Sub LLENAR_DROP(ByVal bandera As String, ByVal ITEM As DropDownList, ByVal DataValueField As String, ByVal DataTextField As String)
        dim SSCommand as new sqlcommand
        SSCommand.CommandText = "SP_CATALOGOS"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = bandera
        'SSCommand.Parameters.Add("@V_Valor", SqlDbType.NVarChar).Value = ""

        Dim objDSa As DataTable = Consulta_Procedure(SSCommand, "PROD")
        ITEM.Visible = True
        If objDSa.Rows.Count >= 1 Then
            ITEM.DataTextField = objDSa.Columns(0).ColumnName
            ITEM.DataValueField = objDSa.Columns(0).ColumnName
            ITEM.DataSource = objDSa
            ITEM.DataBind()
            If bandera = 9 Then

                ITEM.Items.Add("AZUL 2")
            End If
            ITEM.Items.Add("Seleccione")
            ITEM.SelectedValue = "Seleccione"
        Else
            ITEM.Visible = False
        End If
    End Sub

    Public Shared Function GuardarGestion(ByVal v_HIST_GE_CREDITO As String, ByVal v_HIST_GE_PRODUCTO As String, ByVal v_HIST_GE_USUARIO As String, ByVal V_CODACCION As String, ByVal v_HIST_GE_RESULTADO As String, ByVal v_HIST_GE_CODIGO As String, ByVal V_CODNOPAGO As String, ByVal v_HIST_GE_COMENTARIO As String, ByVal v_HIST_GE_INOUTBOUND As Integer, ByVal v_HIST_GE_TELEFONO As String, ByVal v_HIST_GE_AGENCIA As String, ByVal V_DTECONTACTO As String, ByVal V_CONFIGURACION As String) As DataTable
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_ADD_HIST_GESTIONES"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_HIST_GE_CREDITO", SqlDbType.NVarChar).Value = v_HIST_GE_CREDITO
        SSCommand.Parameters.Add("@v_HIST_GE_PRODUCTO", SqlDbType.NVarChar).Value = "%" & v_HIST_GE_PRODUCTO & "%"
        SSCommand.Parameters.Add("@v_HIST_GE_USUARIO", SqlDbType.NVarChar).Value = v_HIST_GE_USUARIO
        SSCommand.Parameters.Add("@V_CODACCION", SqlDbType.NVarChar).Value = V_CODACCION
        SSCommand.Parameters.Add("@v_HIST_GE_RESULTADO", SqlDbType.NVarChar).Value = v_HIST_GE_RESULTADO
        SSCommand.Parameters.Add("@v_HIST_GE_CODIGO", SqlDbType.NVarChar).Value = v_HIST_GE_CODIGO
        SSCommand.Parameters.Add("@V_CODNOPAGO", SqlDbType.NVarChar).Value = V_CODNOPAGO
        SSCommand.Parameters.Add("@v_HIST_GE_COMENTARIO", SqlDbType.NVarChar).Value = v_HIST_GE_COMENTARIO
        SSCommand.Parameters.Add("@v_HIST_GE_INOUTBOUND", SqlDbType.Decimal).Value = v_HIST_GE_INOUTBOUND
        SSCommand.Parameters.Add("@v_HIST_GE_TELEFONO", SqlDbType.NVarChar).Value = v_HIST_GE_TELEFONO
        SSCommand.Parameters.Add("@v_HIST_GE_AGENCIA", SqlDbType.NVarChar).Value = v_HIST_GE_AGENCIA
        SSCommand.Parameters.Add("@V_DTECONTACTO", SqlDbType.NVarChar).Value = V_DTECONTACTO
        SSCommand.Parameters.Add("@V_CONFIGURACION", SqlDbType.NVarChar).Value = V_CONFIGURACION

        Dim DtsGestion As DataTable = Consulta_Procedure(SSCommand, "Gestion")
        Return DtsGestion
    End Function

    Public Shared Function GuardarPromesa(ByVal v_HIST_PR_CREDITO As String, ByVal v_HIST_PR_PRODUCTO As String, ByVal v_HIST_PR_MONTOPP As Double, ByVal v_HIST_PR_DTEPROMESA As String, ByVal v_HIST_PR_USUARIO As String, ByVal v_HIST_PR_TIPO As String, ByVal v_HIST_PR_CONSECUTIVO As Integer, ByVal v_HIST_PR_ORIGEN As String, ByVal v_HIST_PR_AGENCIA As String, ByVal V_CODACCION As String, ByVal v_HIST_GE_RESULTADO As String, ByVal v_HIST_GE_CODIGO As String, ByVal v_HIST_GE_COMENTARIO As String, ByVal v_HIST_VI_DTEVISITA As String, ByVal v_HIST_GE_TELEFONO As String, ByVal v_HIST_GE_INOUTBOUND As Integer, ByVal V_CONFIGURACION As String) As DataTable

        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_ADD_HIST_PROMESAS"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_HIST_PR_CREDITO", SqlDbType.NVarChar).Value = v_HIST_PR_CREDITO
        SSCommand.Parameters.Add("@v_HIST_PR_PRODUCTO", SqlDbType.NVarChar).Value = "%" & v_HIST_PR_PRODUCTO & "%"
        SSCommand.Parameters.Add("@v_HIST_PR_MONTOPP", SqlDbType.Decimal).Value = v_HIST_PR_MONTOPP
        SSCommand.Parameters.Add("@v_HIST_PR_DTEPROMESA", SqlDbType.NVarChar).Value = v_HIST_PR_DTEPROMESA
        SSCommand.Parameters.Add("@v_HIST_PR_USUARIO", SqlDbType.NVarChar).Value = v_HIST_PR_USUARIO
        SSCommand.Parameters.Add("@v_HIST_PR_TIPO", SqlDbType.NVarChar).Value = v_HIST_PR_TIPO
        SSCommand.Parameters.Add("@v_HIST_PR_CONSECUTIVO", SqlDbType.Decimal).Value = v_HIST_PR_CONSECUTIVO
        SSCommand.Parameters.Add("@v_HIST_PR_ORIGEN", SqlDbType.NVarChar).Value = v_HIST_PR_ORIGEN
        SSCommand.Parameters.Add("@v_HIST_PR_AGENCIA", SqlDbType.NVarChar).Value = v_HIST_PR_AGENCIA
        SSCommand.Parameters.Add("@V_CODACCION", SqlDbType.NVarChar).Value = V_CODACCION
        SSCommand.Parameters.Add("@v_HIST_GE_RESULTADO", SqlDbType.NVarChar).Value = v_HIST_GE_RESULTADO
        SSCommand.Parameters.Add("@v_HIST_GE_CODIGO", SqlDbType.NVarChar).Value = v_HIST_GE_CODIGO
        SSCommand.Parameters.Add("@v_HIST_GE_COMENTARIO", SqlDbType.NVarChar).Value = v_HIST_GE_COMENTARIO
        SSCommand.Parameters.Add("@v_HIST_VI_DTEVISITA", SqlDbType.NVarChar).Value = v_HIST_VI_DTEVISITA
        SSCommand.Parameters.Add("@v_HIST_GE_TELEFONO", SqlDbType.NVarChar).Value = v_HIST_GE_TELEFONO
        SSCommand.Parameters.Add("@v_HIST_GE_INOUTBOUND", SqlDbType.Decimal).Value = v_HIST_GE_INOUTBOUND
        SSCommand.Parameters.Add("@V_CONFIGURACION", SqlDbType.NVarChar).Value = V_CONFIGURACION

        Dim DtsPromesa As DataTable = Consulta_Procedure(SSCommand, "Gestion")
        Return DtsPromesa
    End Function


    Public Shared Function ValidaLicencias(ByVal Cuantas As Integer, ByVal Usuario As String, ByVal Ip As String, ByVal Hora As String, ByVal V_bandera As Integer, ByVal V_ID_Session As String) As Integer
        Try
            Dim Cuantos As Integer = 0
            If V_bandera = 1 Then ' Esta Conectado?
                Dim DtsConectado As DataTable = Class_Sesion.LlenarElementos(Usuario, "", "", "", 3, "", "", "", "")
                If DtsConectado.Rows(0).Item("Cuantas") <> "0" Then
                    Return 1
                Else
                    Return 0
                End If
            ElseIf V_bandera = 2 Then ' Insertar Usuario Nuevo
                Dim DtsDisponibles As DataTable = Class_Sesion.LlenarElementos(Usuario, Cuantas, Ip, Hora, 6, "", "", "", "")
                Return 0
            ElseIf V_bandera = 3 Then 'Existen licencias Disponibles
                Dim DtsDisponibles As DataTable = Class_Sesion.LlenarElementos(Usuario, Cuantas, "", "", 4, "", "", "", "")
                If Val(DtsDisponibles.Rows(0).Item("Cuantas")) < Val(Cuantas) Then
                    Return 1
                Else
                    Return 0
                End If
            ElseIf V_bandera = 4 Then 'Desconectar
                Dim DtsDisponibles As DataTable = Class_Sesion.LlenarElementos(Usuario, Cuantas, "Administrador", "Cierre De Sesion Manual", 5, "", "", "Cierre De Sesion", V_ID_Session)
                Return 0
            End If
        Catch ex As Exception
        End Try
    End Function

    Public Shared Function EsOrdenable(ByVal campo As String, ByVal tipo As String) As Boolean
        Dim SSCommandUsuario As New SqlCommand
        SSCommandUsuario.CommandText = "SP_FILAS_TRABAJO"
        SSCommandUsuario.CommandType = CommandType.StoredProcedure
        SSCommandUsuario.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = "32"
        SSCommandUsuario.Parameters.Add("@V_Campo", SqlDbType.NVarChar).Value = campo
        SSCommandUsuario.Parameters.Add("@V_Tipo", SqlDbType.NVarChar).Value = tipo
        Dim DtsUsuario As DataTable = Consulta_Procedure(SSCommandUsuario, "FILAS")

        If DtsUsuario.Rows(0).Item("RESPUESTA") = 1 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function moneyToText(ByVal value As String) As String
        Dim parte1 As String = Num2Text(Double.Parse(value.Split(".")(0)))
        Dim parte2 As String
        Try
            parte2 = " CON " & value.Split(".")(1) & "/100 M.N."
        Catch ex As Exception
            parte2 = " M.N."
        End Try
        Return parte1 & parte2
    End Function

    Public Shared Function Num2Text(ByVal value As Double) As String
        Select Case value
            Case 0 : Num2Text = "CERO"
            Case 1 : Num2Text = "UN"
            Case 2 : Num2Text = "DOS"
            Case 3 : Num2Text = "TRES"
            Case 4 : Num2Text = "CUATRO"
            Case 5 : Num2Text = "CINCO"
            Case 6 : Num2Text = "SEIS"
            Case 7 : Num2Text = "SIETE"
            Case 8 : Num2Text = "OCHO"
            Case 9 : Num2Text = "NUEVE"
            Case 10 : Num2Text = "DIEZ"
            Case 11 : Num2Text = "ONCE"
            Case 12 : Num2Text = "DOCE"
            Case 13 : Num2Text = "TRECE"
            Case 14 : Num2Text = "CATORCE"
            Case 15 : Num2Text = "QUINCE"
            Case Is < 20 : Num2Text = "DIECI" & Num2Text(value - 10)
            Case 20 : Num2Text = "VEINTE"
            Case Is < 30 : Num2Text = "VEINTI" & Num2Text(value - 20)
            Case 30 : Num2Text = "TREINTA"
            Case 40 : Num2Text = "CUARENTA"
            Case 50 : Num2Text = "CINCUENTA"
            Case 60 : Num2Text = "SESENTA"
            Case 70 : Num2Text = "SETENTA"
            Case 80 : Num2Text = "OCHENTA"
            Case 90 : Num2Text = "NOVENTA"
            Case Is < 100 : Num2Text = Num2Text(Int(value \ 10) * 10) & " Y " & Num2Text(value Mod 10)
            Case 100 : Num2Text = "CIEN"
            Case Is < 200 : Num2Text = "CIENTO " & Num2Text(value - 100)
            Case 200, 300, 400, 600, 800 : Num2Text = Num2Text(Int(value \ 100)) & "CIENTOS"
            Case 500 : Num2Text = "QUINIENTOS"
            Case 700 : Num2Text = "SETECIENTOS"
            Case 900 : Num2Text = "NOVECIENTOS"
            Case Is < 1000 : Num2Text = Num2Text(Int(value \ 100) * 100) & " " & Num2Text(value Mod 100)
            Case 1000 : Num2Text = "MIL"
            Case Is < 2000 : Num2Text = "MIL " & Num2Text(value Mod 1000)
            Case Is < 1000000 : Num2Text = Num2Text(Int(value \ 1000)) & " MIL"
                If value Mod 1000 Then Num2Text = Num2Text & " " & Num2Text(value Mod 1000)
            Case 1000000 : Num2Text = "UN MILLON"
            Case Is < 2000000 : Num2Text = "UN MILLON " & Num2Text(value Mod 1000000)
            Case Is < 1000000000000.0# : Num2Text = Num2Text(Int(value / 1000000)) & " MILLONES"
                If (value - Int(value / 1000000) * 1000000) Then Num2Text = Num2Text & " " & Num2Text(value - Int(value / 1000000) * 1000000)
            Case 1000000000000.0# : Num2Text = "UN BILLON"
            Case Is < 2000000000000.0# : Num2Text = "UN BILLON " & Num2Text(value - Int(value / 1000000000000.0#) * 1000000000000.0#)
            Case Else : Num2Text = Num2Text(Int(value / 1000000000000.0#)) & " BILLONES"
                If (value - Int(value / 1000000000000.0#) * 1000000000000.0#) Then Num2Text = Num2Text & " " & Num2Text(value - Int(value / 1000000000000.0#) * 1000000000000.0#)
        End Select

        Return Num2Text
    End Function

End Class

