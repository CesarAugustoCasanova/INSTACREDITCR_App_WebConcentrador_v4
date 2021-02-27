Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Security.Cryptography
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Web

Public Class SignatureGoogle
    Public Shared Function Sign(ByVal url As String, ByVal keyString As String) As String
        Dim encoding As ASCIIEncoding = New ASCIIEncoding()
        Dim usablePrivateKey As String = keyString.Replace("-", "+").Replace("_", "/")
        Dim privateKeyBytes As Byte() = Convert.FromBase64String(usablePrivateKey)
        Dim uri As Uri = New Uri(url)
        Dim encodedPathAndQueryBytes As Byte() = encoding.GetBytes(uri.LocalPath + uri.Query)
        Dim algorithm As HMACSHA1 = New HMACSHA1(privateKeyBytes)
        Dim hash As Byte() = algorithm.ComputeHash(encodedPathAndQueryBytes)
        Dim signature As String = Convert.ToBase64String(hash).Replace("+", "-").Replace("/", "_")
        Return url & "&signature=" & signature
    End Function
End Class
