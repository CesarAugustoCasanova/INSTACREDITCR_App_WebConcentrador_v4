Imports Microsoft.VisualBasic

Public Class Encriptador

    Private patron_busqueda As String = "qpwoeirutyQPWOEIRUTYañsld1234567890kfjghAÑSLDKFJGHzmxncbvZMXNCBV."
    Private Patron_encripta As String = "zmxncbvZMXNCBVañsldkfjghAÑ.SLDKFJGHqpwoeirutyQPWOEIRUTY0987654321"

    Private Function EncriptarCaracter(ByVal caracter As String, ByVal variable As Integer, ByVal a_indice As Integer) As String
        Dim indice As Integer
        If patron_busqueda.IndexOf(caracter) <> -1 Then
            indice = (patron_busqueda.IndexOf(caracter) + variable + a_indice) Mod patron_busqueda.Length
            Return Patron_encripta.Substring(indice, 1)
        End If
        Return caracter
    End Function

    Public Function EncriptarCadena(ByVal cadena As String) As String
        Dim idx As Integer
        Dim result As String = ""

        For idx = 0 To cadena.Length - 1
            result += EncriptarCaracter(cadena.Substring(idx, 1), cadena.Length, idx)
        Next
        Return result
    End Function
    Public Function DesEncriptarCadena(ByVal cadena As String) As String
        Dim idx As Integer
        Dim result As String = ""
        For idx = 0 To cadena.Length - 1
            result += DesEncriptarCaracter(cadena.Substring(idx, 1), cadena.Length, idx)
        Next
        Return result
    End Function

    Private Function DesEncriptarCaracter(ByVal caracter As String, ByVal variable As Integer, ByVal a_indice As Integer) As String
        Dim indice As Integer
        If Patron_encripta.IndexOf(caracter) <> -1 Then
            If (Patron_encripta.IndexOf(caracter) - variable - a_indice) > 0 Then
                indice = (Patron_encripta.IndexOf(caracter) - variable - a_indice) Mod Patron_encripta.Length
            Else
                indice = (patron_busqueda.Length) + ((Patron_encripta.IndexOf(caracter) - variable - a_indice) Mod Patron_encripta.Length)
            End If
            indice = indice Mod Patron_encripta.Length
            Return patron_busqueda.Substring(indice, 1)
        Else
            Return caracter
        End If
    End Function


End Class
