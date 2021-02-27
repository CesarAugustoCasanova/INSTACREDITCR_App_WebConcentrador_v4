Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports System.DirectoryServices
Imports Db


Public Class Class_LDAP


    Public Shared Function GetPropertyAD(ByRef SearchResult As SearchResult, ByVal PropertyName As String) As String
        If SearchResult.Properties.Contains(PropertyName) Then
            Return SearchResult.Properties(PropertyName)(0).ToString()
        Else
            Return String.Empty
        End If
    End Function

    Public Shared Function GetVarSistema(ByVal Valor As String) As String
        Try
            Dim SSCommand As New SqlCommand
            SSCommand.CommandText = "SP_CATALOGOS"
            SSCommand.CommandType = CommandType.StoredProcedure
            SSCommand.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = 42
            SSCommand.Parameters.Add("@V_Valor", SqlDbType.NVarChar).Value = Valor
            Return Consulta_Procedure(SSCommand, "CATALOGOS")(0)(0).ToString
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function

    Public Shared Function ValidaUsuarioLDAP(ByVal strUsuario As String, ByVal strContrasena As String) As String

        Dim strLDAPRuta As String = GetVarSistema("LDAP_Ruta")
        Dim strDominio As String = GetVarSistema("LDAP_Dominio")
        Dim strLDAP As String = GetVarSistema("LDAP")
        Dim blnExito As String = "Error"

        If strLDAP = "0" Then
            blnExito = "OK"
        Else
            Dim strDominioUsuario As String = strDominio & "\" & strUsuario
            Dim deEntrada As DirectoryEntry = New DirectoryEntry(strLDAPRuta, strDominioUsuario, strContrasena)

            Try
                Dim objObjecto As Object = deEntrada.NativeObject

                Dim dsBuscador As New DirectorySearcher(deEntrada)
                dsBuscador.Filter = "(SAMAccountName=" & strUsuario & ")"

                Dim srResultado As SearchResult = dsBuscador.FindOne()

                If Not srResultado Is Nothing Then
                    blnExito = "OK"

                    Dim v_title As String = GetPropertyAD(srResultado, "title")
                    Dim v_displayname As String = GetPropertyAD(srResultado, "displayname")
                    Dim v_givenname As String = GetPropertyAD(srResultado, "givenname")
                    Dim v_samaccountname As String = GetPropertyAD(srResultado, "samaccountname")

                    '---------------------------------------------------------------------------------------------------
                    Dim valores As New Hashtable
                    valores.Add("usuario", v_samaccountname)
                    valores.Add("nombre", v_displayname)
                    valores.Add("contrasena", strContrasena)
                    valores.Add("contrasena2", strContrasena)
                    valores.Add("supervisor", "Supervisor")
                    valores.Add("rol", v_title)
                    valores.Add("estatus", "Activo")
                    valores.Add("motivo", "LDAP")
                    valores.Add("hora1", "7")
                    valores.Add("hora2", "22")
                    valores.Add("agencia", "1")
                    valores.Add("verAgencias", "1")
                    valores.Add("esAgencia", "0")
                    valores.Add("producto", "1")
                    valores.Add("meta", "0")
                    valores.Add("oldpassword", "")
                    '---------------------------------------------------------------------------------------------------

                    If ValidarUsrB(valores("usuario")) = "El Usuario Ya Existe Valide" Then
                        'Dim mensaje As String = "El Usuario Ya Existe Valide"
                        GuardarU(valores, 36)
                    Else
                        GuardarU(valores, 37)
                    End If
                Else
                    blnExito = "Error"
                End If

                dsBuscador.Dispose()

            Catch ex As Exception
                Dim v_error As String = ex.Message()
                blnExito = v_error
            Finally
                deEntrada.Dispose()
            End Try
        End If

        Return blnExito

    End Function


    Public Shared Function ValidarUsrB(Usuario As String) As String
        Dim SSCommandUsuario As New SqlCommand
        SSCommandUsuario.CommandText = "Sp_Llenar_Usuario"
        SSCommandUsuario.CommandType = CommandType.StoredProcedure
        SSCommandUsuario.Parameters.Add("@V_Usuario", SqlDbType.NVarChar).Value = Usuario
        SSCommandUsuario.Parameters.Add("@V_Agencia", SqlDbType.NVarChar).Value = ""
        SSCommandUsuario.Parameters.Add("@V_Producto", SqlDbType.NVarChar).Value = ""
        SSCommandUsuario.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 7
        Dim DtsUsuario As DataTable = Consulta_Procedure(SSCommandUsuario, "BUSQUEDA")
        Return DtsUsuario.Rows(0).Item("Mensaje")
    End Function
    Public Shared Function GuardarU(valores As Hashtable, ByVal v_bandera As Decimal) As Boolean
        Dim res As Boolean = False
        Try
            Dim oraCommanVarios As New SqlCommand
            oraCommanVarios.CommandText = "SP_CREAR_USUARIO"
            oraCommanVarios.CommandType = CommandType.StoredProcedure
            oraCommanVarios.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = v_bandera
            oraCommanVarios.Parameters.Add("@V_ID", SqlDbType.Decimal).Value = -1
            oraCommanVarios.Parameters.Add("@V_NOMBRE", SqlDbType.NVarChar).Value = valores("nombre")
            oraCommanVarios.Parameters.Add("@V_USUARIO", SqlDbType.NVarChar).Value = valores("usuario")
            oraCommanVarios.Parameters.Add("@V_CONTRASENA", SqlDbType.NVarChar).Value = valores("contrasena")
            oraCommanVarios.Parameters.Add("@V_ESTATUS", SqlDbType.NVarChar).Value = valores("estatus")
            oraCommanVarios.Parameters.Add("@v_AGENCIA", SqlDbType.NVarChar).Value = valores("agencia")
            oraCommanVarios.Parameters.Add("@v_VERAGENCIA", SqlDbType.NVarChar).Value = valores("verAgencias")

            oraCommanVarios.Parameters.Add("@V_MOTIVO", SqlDbType.NVarChar).Value = valores("motivo")
            oraCommanVarios.Parameters.Add("@V_PERFIL", SqlDbType.NVarChar).Value = valores("rol")
            oraCommanVarios.Parameters.Add("@v_supervisor", SqlDbType.NVarChar).Value = valores("supervisor")
            oraCommanVarios.Parameters.Add("@V_HENTRADA", SqlDbType.NVarChar).Value = valores("hora1")
            oraCommanVarios.Parameters.Add("@v_HSALIDA", SqlDbType.NVarChar).Value = valores("hora2")
            oraCommanVarios.Parameters.Add("@v_PRODUCTO", SqlDbType.NVarChar).Value = valores("producto")
            oraCommanVarios.Parameters.Add("@v_PLAZA", SqlDbType.Int).Value = 0
            oraCommanVarios.Parameters.Add("@v_META", SqlDbType.NVarChar).Value = "0"
            oraCommanVarios.Parameters.Add("@V_QUIENMODIFICA", SqlDbType.NVarChar).Value = "LDAP"
            'Session.Remove("CAT_LO_ID")

            oraCommanVarios.Parameters.Add("@v_isAgencia", SqlDbType.NVarChar).Value = valores("esAgencia")

            Dim DtsUsuario As DataTable = Consulta_Procedure(oraCommanVarios, "BUSQUEDA")
            'If DtsUsuario.Columns.Contains("EXISTE") Then
            '    If DtsUsuario(0)("EXISTE") = 0 Then
            '        Aviso("Usuario '" & valores("usuario") & "' creado correctamente")
            '    Else
            '        Aviso("Usuario '" & valores("usuario") & "' actualizado correctamente")
            '    End If
            'Else
            '    Aviso(DtsUsuario(0)(0))
            'End If
            'AUDITORIA(tmpUSUARIO("CAT_LO_USUARIO"), "Administrador", IIf(bandera = 0, "Crear Usuario", "Actualizar Usuario"), "", bandera, valores("usuario"), "", "")

            res = True

            SP.AUDITORIA_GLOBAL(0, "LDAP", "Inicio de Sesion", "Usuario Guardado: " & valores("usuario"))

        Catch ex As Exception
            'SendMail("guardar", ex, "", "", TmpUSUARIO("CAT_LO_USUARIO"))
        End Try
        Return res
    End Function

End Class
