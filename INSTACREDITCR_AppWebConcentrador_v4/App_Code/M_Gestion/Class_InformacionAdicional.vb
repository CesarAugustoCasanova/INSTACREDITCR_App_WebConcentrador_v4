Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports Db
Imports System.Globalization
Imports System.IO
Imports Funciones
Imports System.Web.SessionState.HttpSessionState
Imports System.Data
Public Class Class_InformacionAdicional
    Public Shared Function LlenarElementosAgregar(ByVal V_Credito As String, ByVal V_Bandera As String) As DataTable
        Dim SSCommand As New SqlCommand("SP_INFORMACION_ADICIONAL")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Credito", SqlDbType.NVarChar).Value = V_Credito
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = V_Bandera
        Dim DtsTelefonos As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return DtsTelefonos
    End Function
    Public Shared Function AgregarTelefono(ByVal V_HIST_TE_CONSECUTIVO As String, ByVal V_Hist_Te_Credito As String, ByVal V_Hist_Te_cliente As String, ByVal V_Hist_Te_Producto As String, ByVal V_Hist_Te_Numerotel As String, ByVal V_Hist_Te_Tipo As String, ByVal V_Hist_Te_Parentesco As String, ByVal V_Hist_Te_Nombre As String, ByVal V_Hist_Te_Extension As String, ByVal V_Hist_Te_Horario0 As String, ByVal V_Hist_Te_Horario1 As String, ByVal V_Hist_Te_Usuario As String, ByVal V_Hist_Te_Agencia As String, ByVal V_Hist_Te_Fuente As String, ByVal Dias As String, ByVal V_Hist_Te_Proporciona As String, ByVal V_Hist_Te_Contacto As String) As String
        Dim msg As String = ""
        If V_Hist_Te_Parentesco <> "Cliente" And V_Hist_Te_Nombre = "" Then
            msg = "Capture el nombre de " & V_Hist_Te_Parentesco
        ElseIf V_Hist_Te_Numerotel.Length <> 10 Then
            msg = "El número de télefono debe ser a 10 dígitos"
        ElseIf V_Hist_Te_Tipo.Contains("Oficina") And V_Hist_Te_Extension = "" Then
            msg = "Capture el número de extensión "
        ElseIf Val(V_Hist_Te_Horario0) > Val(V_Hist_Te_Horario1) Then
            msg = "Valide Horario"
        Else
            Dim SSCommand As New SqlCommand("SP_ADD_HIST_TELEFONOS")
            SSCommand.CommandType = CommandType.StoredProcedure
            SSCommand.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = 3
            SSCommand.Parameters.Add("@V_HIST_TE_CONSECUTIVO", SqlDbType.NVarChar).Value = V_HIST_TE_CONSECUTIVO
            SSCommand.Parameters.Add("@V_Hist_Te_Credito", SqlDbType.NVarChar).Value = V_Hist_Te_Credito
            SSCommand.Parameters.Add("@V_Hist_Te_cliente", SqlDbType.NVarChar).Value = V_Hist_Te_cliente
            SSCommand.Parameters.Add("@V_Hist_Te_Producto", SqlDbType.NVarChar).Value = V_Hist_Te_Producto
            SSCommand.Parameters.Add("@V_Hist_Te_Numerotel", SqlDbType.NVarChar).Value = V_Hist_Te_Numerotel
            SSCommand.Parameters.Add("@V_Hist_Te_Tipo", SqlDbType.NVarChar).Value = V_Hist_Te_Tipo
            SSCommand.Parameters.Add("@V_Hist_Te_Parentesco", SqlDbType.NVarChar).Value = V_Hist_Te_Parentesco
            SSCommand.Parameters.Add("@V_Hist_Te_Nombre", SqlDbType.NVarChar).Value = V_Hist_Te_Nombre
            SSCommand.Parameters.Add("@V_Hist_Te_Extension", SqlDbType.NVarChar).Value = V_Hist_Te_Extension
            SSCommand.Parameters.Add("@V_Hist_Te_Horario", SqlDbType.NVarChar).Value = V_Hist_Te_Horario0 & V_Hist_Te_Horario1
            SSCommand.Parameters.Add("@V_Hist_Te_Dias", SqlDbType.NVarChar).Value = Dias
            SSCommand.Parameters.Add("@V_Hist_Te_Usuario", SqlDbType.NVarChar).Value = V_Hist_Te_Usuario
            SSCommand.Parameters.Add("@V_Hist_Te_Agencia", SqlDbType.NVarChar).Value = V_Hist_Te_Agencia
            SSCommand.Parameters.Add("@V_Hist_Te_Fuente", SqlDbType.NVarChar).Value = V_Hist_Te_Fuente
            SSCommand.Parameters.Add("@V_Hist_Te_Proporciona", SqlDbType.NVarChar).Value = V_Hist_Te_Proporciona
            SSCommand.Parameters.Add("@V_Hist_Te_Contacto", SqlDbType.NVarChar).Value = V_Hist_Te_Contacto
            Dim DtsTelefonos As DataTable = Consulta_Procedure(SSCommand, "Telefonos")
            ' msg = DtsTelefonos.Rows(0).Item("telefono")
            If DtsTelefonos.Rows(0).Item("inserta") = 0 Then
                msg = "Teléfono " + V_Hist_Te_Numerotel + " actualizado correctamente"
            ElseIf DtsTelefonos.Rows(0).Item("inserta") = 1 Then
                msg = "Telefono " + V_Hist_Te_Numerotel + " insertado correctamente"
            End If
        End If
        Return msg
    End Function
    Public Shared Function AgregarDireccion(v_usuario As String, credito As String, V_Hist_d_producto As String, values As Hashtable) As String
        Dim DtsDireccion As DataTable
        Dim SSCommand As New SqlCommand("SP_ADD_HIST_DIRECCIONES")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_HIST_D_CALLE", SqlDbType.NVarChar).Value = values("Calle")
        SSCommand.Parameters.Add("@V_HIST_D_COLONIA", SqlDbType.NVarChar).Value = values("Colonia")
        SSCommand.Parameters.Add("@V_HIST_D_CONTACTO", SqlDbType.NVarChar).Value = values("contacto")
        SSCommand.Parameters.Add("@V_HIST_D_CP", SqlDbType.NVarChar).Value = values("CP")
        SSCommand.Parameters.Add("@V_HIST_D_DESCRIPCION", SqlDbType.NVarChar).Value = values("descripcion")
        SSCommand.Parameters.Add("@V_HIST_D_DIASCONTACTO", SqlDbType.NVarChar).Value = values("DIAS")
        SSCommand.Parameters.Add("@V_HIST_D_DIRECCIONID", SqlDbType.NVarChar).Value = IIf(values("DIRECCIONID").ToString = "", "-1", values("DIRECCIONID"))
        SSCommand.Parameters.Add("@V_HIST_D_ESTADONOMBRE", SqlDbType.NVarChar).Value = values("Estado")
        SSCommand.Parameters.Add("@V_HIST_D_FISCAL", SqlDbType.NVarChar).Value = values("Fiscal")
        SSCommand.Parameters.Add("@V_HIST_D_FUENTE", SqlDbType.NVarChar).Value = "Captura"
        SSCommand.Parameters.Add("@V_HIST_D_HORARIO", SqlDbType.NVarChar).Value = values("Horario1") & values("Horario2")
        SSCommand.Parameters.Add("@V_HIST_D_ID_VISITA", SqlDbType.NVarChar).Value = values("")
        SSCommand.Parameters.Add("@V_HIST_D_LOCALIDADNOMBRE", SqlDbType.NVarChar).Value = values("Ciudad")
        SSCommand.Parameters.Add("@V_HIST_D_LOTE", SqlDbType.NVarChar).Value = values("Lt")
        SSCommand.Parameters.Add("@V_HIST_D_MANZANA", SqlDbType.NVarChar).Value = values("MZ")
        SSCommand.Parameters.Add("@V_HIST_D_MUNICIPIONOMBRE", SqlDbType.NVarChar).Value = values("Municipio")
        SSCommand.Parameters.Add("@V_HIST_D_NOMBRE", SqlDbType.NVarChar).Value = values("NOMBRE")
        SSCommand.Parameters.Add("@V_HIST_D_NUMEROCASA", SqlDbType.NVarChar).Value = values("NumExt")
        SSCommand.Parameters.Add("@V_HIST_D_CREDITO", SqlDbType.NVarChar).Value = credito
        SSCommand.Parameters.Add("@V_HIST_D_NUMINTERIOR", SqlDbType.NVarChar).Value = values("NumInt")
        SSCommand.Parameters.Add("@V_HIST_D_OFICIAL", SqlDbType.NVarChar).Value = values("Oficial")
        SSCommand.Parameters.Add("@V_HIST_D_PARENTESCO", SqlDbType.NVarChar).Value = values("Parentesco")
        SSCommand.Parameters.Add("@V_HIST_D_PISO", SqlDbType.NVarChar).Value = values("Piso")
        SSCommand.Parameters.Add("@V_HIST_D_PRIMERAENTRECALLE", SqlDbType.NVarChar).Value = values("ENTRECALLE1")
        SSCommand.Parameters.Add("@V_HIST_D_PROPORCIONA", SqlDbType.NVarChar).Value = values("Proporciona")
        SSCommand.Parameters.Add("@V_HIST_D_SEGUNDAENTRECALLE", SqlDbType.NVarChar).Value = values("ENTRECALLE2")
        SSCommand.Parameters.Add("@V_HIST_D_TIPODIRECCIONID", SqlDbType.NVarChar).Value = values("TIPODOMICILIO")
        SSCommand.Parameters.Add("@V_HIST_D_TIPOVIVIENDA", SqlDbType.NVarChar).Value = values("TIPOVIVIENDA")
        SSCommand.Parameters.Add("@V_USUARIO", SqlDbType.NVarChar).Value = v_usuario
        SSCommand.Parameters.Add("@V_HIST_D_VIVE_EN", SqlDbType.NVarChar).Value = values("CaVIVEENCASAlle")
        SSCommand.Parameters.Add("@v_bandera", SqlDbType.Decimal).Value = 3
        SSCommand.Parameters.Add("@V_HIST_D_PRODUCTO", SqlDbType.NVarChar).Value = V_Hist_d_producto
        DtsDireccion = Consulta_Procedure(SSCommand, "Direccion")
        'Return DtsDireccion.Rows(0).Item("Direccion")
        If DtsDireccion.Rows(0).Item("inserta") = 0 Then
            Return ("Dirección actualizada correctamente")
        ElseIf DtsDireccion.Rows(0).Item("inserta") = 1 Then
            Return ("Dirección insertada correctamente")
        End If
    End Function

    Public Shared Function AgregarCorreo(ByVal V_Hist_Co_Credito As String, ByVal V_Hist_Co_Producto As String, ByVal V_Hist_Co_Parentesco As String, ByVal V_Hist_Co_Nombre As String, ByVal V_Hist_Co_Correo As String, ByVal V_Hist_Co_Contacto As String, ByVal mode As String, ByVal V_Hist_Co_Usuario As String, ByVal V_Hist_Co_Agencia As String, ByVal V_Hist_Co_Fuente As String, ByVal V_Hist_Co_Tipo As String, ByVal V_Hist_Co_Proporciona As String) As String
        Dim V_Bandera As Integer = 2

        If mode = "Agregar" Then
            V_Bandera = 1
        End If

        If V_Hist_Co_Parentesco <> "Cliente" And V_Hist_Co_Nombre = "" Then
            Return "Capture el nombre de " & V_Hist_Co_Parentesco
        ElseIf EmailValida(V_Hist_Co_Correo) = True Then
            Return "Correo no valido"
        Else
            Dim DtsCorreos As DataTable
            Dim SSCommand As New SqlCommand("SP_ADD_HIST_CORREOS")
            SSCommand.CommandType = CommandType.StoredProcedure
            SSCommand.Parameters.Add("@v_bandera", SqlDbType.Decimal).Value = V_Bandera
            SSCommand.Parameters.Add("@v_hist_co_credito", SqlDbType.NVarChar).Value = V_Hist_Co_Credito
            SSCommand.Parameters.Add("@v_hist_co_producto", SqlDbType.NVarChar).Value = V_Hist_Co_Producto
            SSCommand.Parameters.Add("@v_hist_co_parentesco", SqlDbType.NVarChar).Value = V_Hist_Co_Parentesco
            SSCommand.Parameters.Add("@v_hist_co_nombre", SqlDbType.NVarChar).Value = V_Hist_Co_Nombre
            SSCommand.Parameters.Add("@v_hist_co_correo", SqlDbType.NVarChar).Value = V_Hist_Co_Correo
            SSCommand.Parameters.Add("@v_hist_co_contacto", SqlDbType.NVarChar).Value = V_Hist_Co_Contacto
            SSCommand.Parameters.Add("@v_hist_co_usuario", SqlDbType.NVarChar).Value = V_Hist_Co_Usuario
            SSCommand.Parameters.Add("@v_hist_co_agencia", SqlDbType.NVarChar).Value = V_Hist_Co_Agencia
            SSCommand.Parameters.Add("@v_hist_co_fuente", SqlDbType.NVarChar).Value = V_Hist_Co_Fuente
            SSCommand.Parameters.Add("@v_hist_co_tipo", SqlDbType.NVarChar).Value = V_Hist_Co_Tipo
            SSCommand.Parameters.Add("@v_hist_co_proporciona", SqlDbType.NVarChar).Value = V_Hist_Co_Proporciona
            DtsCorreos = Consulta_Procedure(SSCommand, "correo")
            'Return DtsCorreos.Rows(0).Item("correo")
            If DtsCorreos.Rows(0).Item("correo") = 0 Then
                Return ("Correo " + V_Hist_Co_Correo + " actualizado correctamente")
            ElseIf DtsCorreos.Rows(0).Item("correo") = 1 Then
                Return ("Correo " + V_Hist_Co_Correo + " insertado correctamente")
            End If
        End If
    End Function
End Class
