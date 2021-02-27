Imports Microsoft.VisualBasic
Imports System.Net
Imports System.Security.Cryptography.X509Certificates

Public Class CustomCertificatePolicyHandler
    Implements ICertificatePolicy

    Private _ServerCertificateValidationCallback As System.Net.Security.RemoteCertificateValidationCallback

#Region "Properties"
    ''' <summary>
    ''' The code allows the client application to accept every certificate that the server provides.
    ''' and then accepts every request under SSL.
    ''' Refered From http://support.microsoft.com/kb/823177
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable ReadOnly Property ServerCertificateValidationCallback() As System.Net.Security.RemoteCertificateValidationCallback
        Get
            Return _ServerCertificateValidationCallback
        End Get
    End Property
#End Region

#Region "Methods"
    ''' <summary>
    ''' Check Validation Result and allows the client application to accept every certificate that the server provides
    ''' </summary>
    ''' <param name="srvPoint"></param>
    ''' <param name="cert"></param>
    ''' <param name="request"></param>
    ''' <param name="certificateProblem"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckValidationResult(ByVal srvPoint As ServicePoint, _
              ByVal cert As X509Certificate, ByVal request As WebRequest, _
              ByVal certificateProblem As Integer) _
          As Boolean Implements ICertificatePolicy.CheckValidationResult
        'Return True to allow the certificate to be accepted.
        Return True
    End Function

#End Region

#Region "Procedures"
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        MyBase.New()
    End Sub

#End Region
End Class
