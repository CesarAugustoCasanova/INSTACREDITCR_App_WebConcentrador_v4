Imports System.Data
Imports System.Data.OracleClient
Imports Microsoft.VisualBasic
Imports Db
Imports System.Net
Imports System.IO
Imports RestSharp
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class M_DONDE
    Public Shared Function EnviarRequestGet(ByVal url As String) As String
        Try
            Dim v_endpoint As String = ""

            v_endpoint = url


            Dim client = New RestClient(v_endpoint)

            Dim report1 As report1
            client.RemoteCertificateValidationCallback = Function(sender, certificate, chain, sslPolicyErrors) True
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 Or SecurityProtocolType.Tls12

            Dim request = New RestRequest(Method.GET)
            'request.AddHeader("cache-control", "no-cache")
            'request.AddHeader("Content-Type", "application/json")
            ' request.AddHeader("Content-Type", "application/x-www-form-urlencoded")
            Dim response As IRestResponse = client.Execute(request)
            Dim invDom As String = response.Content

            If response.StatusCode.ToString = "OK" Then
                Return invDom
                ' report1 = JsonConvert.DeserializeObject(Of report1)(response.Content.Replace("[", "").Replace("]", ""))
            Else
                Dim errorData As String = "{""codigoRespuesta"": """ & response.StatusCode.ToString & """, ""mensajeRespuesta"": """ & response.ErrorMessage.ToString & """}"
                report1 = JsonConvert.DeserializeObject(Of report1)(errorData)
            End If


            Dim codigoRespuesta As String = report1.name
            Dim mensajeRespuesta As String = report1.login_time

            'Response.Close()
            'reader.Close()


            Return "OK"
        Catch ex As WebException
            Dim V_error As String = ex.Message
            Return V_error
        End Try
    End Function



    Public Shared Function ConsumeWS_DONDE(ByVal v_url As String, ByVal v_metodo As String, ByVal v_xml As String) As String

        Dim myReq As System.Net.HttpWebRequest = CType(HttpWebRequest.Create(v_url), HttpWebRequest)

        Dim objCertificatePolicy As New CustomCertificatePolicyHandler
        ServicePointManager.CertificatePolicy = objCertificatePolicy

        Dim proxy As IWebProxy = CType(myReq.Proxy, IWebProxy)
        Dim proxyaddress As String
        Dim myProxy As New WebProxy()
        Dim encoding As New ASCIIEncoding
        Dim buffer() As Byte = encoding.GetBytes(v_xml)
        Dim response As String

        myReq.AllowWriteStreamBuffering = False
        myReq.Method = v_metodo
        myReq.ContentType = "text/xml; charset=UTF-8"
        myReq.ContentLength = buffer.Length
        myReq.Headers.Add("SOAPAction", v_metodo)
        myReq.Headers.Add("SOAPAction", ":http://frd.nimbuscc.mx/ccs/servicios.php/CAMPANAS_LIST")
        proxyaddress = proxy.GetProxy(myReq.RequestUri).ToString

        Dim newUri As New Uri(proxyaddress)
        myProxy.Address = newUri
        Dim post As Stream = myReq.GetRequestStream

        post.Write(buffer, 0, buffer.Length)
        post.Close()


        Try
            Dim myResponse As HttpWebResponse = myReq.GetResponse
            Dim responsedata As Stream = myResponse.GetResponseStream
            Dim responsereader As New StreamReader(responsedata)
            response = responsereader.ReadToEnd
        Catch ex As WebException
            Dim myResponse As HttpWebResponse = ex.Response
            Dim responsedata As Stream = myResponse.GetResponseStream
            Dim responsereader As New StreamReader(responsedata)
            response = responsereader.ReadToEnd
            response = XElement.Parse(response).ToString
            Dim xmlresponse As String = XElement.Parse(response.ToString).ToString

        End Try
        Return response

    End Function
End Class
