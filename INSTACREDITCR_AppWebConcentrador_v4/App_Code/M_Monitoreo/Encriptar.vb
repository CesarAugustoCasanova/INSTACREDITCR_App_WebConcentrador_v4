Imports Microsoft.VisualBasic
Imports System.Security.Cryptography


Public Class Encriptar
    Public Enum AlgoritmoDeEncriptacion
        MD5 = 0
        SHA = 1
        TRIPLE_DESC
    End Enum

    Public Function Encriptar(ByVal valAlgoritmo As AlgoritmoDeEncriptacion,
    ByVal strCadena As String, Optional ByVal valIV As Byte = 0,
    Optional ByVal valKey As Byte = 0) As String
        Dim Codificacion As New UTF8Encoding
        Select Case valAlgoritmo
            Case AlgoritmoDeEncriptacion.MD5
                Dim md5Hasher As MD5 = MD5.Create()
                Dim data As Byte() = md5Hasher.ComputeHash(Encoding.Default.GetBytes(
                strCadena))
                Dim sBuilder As New StringBuilder()
                Dim i As Integer
                For i = 0 To data.Length - 1
                    sBuilder.Append(data(i).ToString("x2"))
                Next i
                Return sBuilder.ToString()
            Case AlgoritmoDeEncriptacion.SHA
                Dim data() As Byte = Codificacion.GetBytes(strCadena)
                Dim resultado() As Byte
                Dim sha As New SHA1CryptoServiceProvider()
                resultado = sha.ComputeHash(data)
                Dim sb As New StringBuilder
                For i As Integer = 0 To resultado.Length - 1
                    If resultado(i) < 16 Then
                        sb.Append("0")
                    End If
                    sb.Append(resultado(i).ToString("x"))
                Next
                Return sb.ToString() '<-
            Case AlgoritmoDeEncriptacion.TRIPLE_DESC
                Dim message As Byte() = Codificacion.GetBytes(strCadena)
                Dim criptoProvider As New TripleDESCryptoServiceProvider
                Dim criptoTransform As ICryptoTransform = criptoProvider.
                CreateEncryptor(criptoProvider.Key, criptoProvider.IV)
                Dim memorystream As New IO.MemoryStream
                Dim cryptoStream As New CryptoStream(memorystream, criptoTransform,
                CryptoStreamMode.Write)
                cryptoStream.FlushFinalBlock()
                Dim encriptado As Byte() = memorystream.ToArray
                Dim cadenaEncriptada = Codificacion.GetString(encriptado)
                Return cadenaEncriptada
            Case Else
                Return ""
        End Select
    End Function
End Class

