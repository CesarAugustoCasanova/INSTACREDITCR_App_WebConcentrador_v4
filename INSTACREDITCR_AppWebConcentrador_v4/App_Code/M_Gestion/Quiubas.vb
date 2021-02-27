Imports Microsoft.VisualBasic
Imports Db
Imports QParametros
Imports System.IO
Imports System.Xml
Imports System.Data
Imports System.Net

Public Class Quiubas
    Public Shared Function Ejecuta_SMS(ByVal URL As String) As String
        Dim ASCII As New System.Text.ASCIIEncoding
        Dim netWeb As New System.Net.WebClient
        Dim lsWeb As String
        Dim laWeb As Byte()
        Try
            laWeb = netWeb.DownloadData(URL)
            lsWeb = ASCII.GetString(laWeb)
            Return lsWeb
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString + ex.ToString)
        End Try
    End Function
    Public Shared Function EnviarSMS(ByVal V_Telefono As String, ByVal V_MSJ As String) As String
        Dim DtsParametros As DataSet = Class_SMS.LlenarElementos("", "", "", "", "", "", "", "", "", 0)
        Dim v_username As String = DtsParametros.Tables(0).Rows(0).Item(0)
        Dim v_password As String = DtsParametros.Tables(0).Rows(1).Item(0)
        Dim procesa_url As String = "https://api.quiubas.com:9443/api?" & _
                                    "action=sendmessage" & _
                                    "&username=" & v_username & _
                                    "&password=" & v_password & _
                                    "&recipient=+52" & V_Telefono & _
                                    "&messagedata=" & V_MSJ & _
                                    "&responseformat=xml"
        Return procesa_url
    End Function
    Public Shared Function SALDOSMS() As String
        Dim DtsParametros As DataSet = Class_SMS.LlenarElementos("", "", "", "", "", "", "", "", "", 0)
        Dim v_username As String = DtsParametros.Tables(0).Rows(0).Item(0)
        Dim v_password As String = DtsParametros.Tables(0).Rows(1).Item(0)
        Dim procesa_url As String = "https://api.quiubas.com:9443/api?" & _
                                    "action=getcredits" & _
                                    "&username=" & v_username & _
                                    "&password=" & v_password
        Return procesa_url
    End Function

    Public Shared Function EnviarSMS_Donde(ByVal V_Telefono As String, ByVal V_MSJ As String) As String

        Dim xml As String
        xml = "<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:ser=""http://service.centralnotificaciones.spirit.com/"">" & _
              "   <soapenv:Header/>" & _
              "   <soapenv:Body>" & _
              "      <ser:enviaNotificacion>" & _
              "         <mensaje>" & _
              "            <destinatario>" & V_Telefono & "</destinatario>" & _
              "            <estadoMensaje>" & "" & "</estadoMensaje>" & _
              "            <fechaMensaje>" & "" & "</fechaMensaje>" & _
              "            <idMensaje>" & "" & "</idMensaje>" & _
              "            <mensaje>" & V_MSJ & "</mensaje>" & _
              "            <referencias>" & "" & "</referencias>" & _
              "            <remitente>" & "" & "</remitente>" & _
              "            <tipoMensaje>" & "1" & "</tipoMensaje>" & _
              "         </mensaje>" & _
              "      </ser:enviaNotificacion>" & _
              "   </soapenv:Body>" & _
              "</soapenv:Envelope>"

        Dim data As String = xml
        'Dim url As String = "http://172.16.5.19:8080/CentralNotificaciones/NotificacionesService?WSDL"
        Dim url As String = "http://172.16.5.26:8080/CentralNotificaciones/NotificacionesService?WSDL"
        Dim responsestring As String = ""

        Dim myReq As HttpWebRequest = WebRequest.Create(url)
        Dim proxy As IWebProxy = CType(myReq.Proxy, IWebProxy)
        Dim proxyaddress As String
        Dim myProxy As New WebProxy()
        Dim encoding As New ASCIIEncoding
        Dim buffer() As Byte = encoding.GetBytes(xml)
        Dim response As String

        myReq.AllowWriteStreamBuffering = False
        myReq.Method = "POST"
        myReq.ContentType = "text/xml; charset=UTF-8"
        myReq.ContentLength = buffer.Length
        myReq.Headers.Add("SOAPAction", "http://tempuri.org/SaveIncomingFile")
        'myReq.Credentials = New NetworkCredential("abc", "123")
        'myReq.PreAuthenticate = True
        proxyaddress = proxy.GetProxy(myReq.RequestUri).ToString

        Dim newUri As New Uri(proxyaddress)
        myProxy.Address = newUri
        myReq.Proxy = myProxy
        Dim post As Stream = myReq.GetRequestStream

        post.Write(buffer, 0, buffer.Length)
        post.Close()

        Dim myResponse As HttpWebResponse = myReq.GetResponse
        Dim responsedata As Stream = myResponse.GetResponseStream
        Dim responsereader As New StreamReader(responsedata)

        response = responsereader.ReadToEnd

        'Dim response As String = "<env:Envelope xmlns:env='http://schemas.xmlsoap.org/soap/envelope/'><env:Header></env:Header><env:Body><ns2:enviaNotificacionResponse xmlns:ns2=""http://service.centralnotificaciones.spirit.com/""><mensajeResponse><destinatario>5537144998</destinatario><estadoMensaje>0</estadoMensaje><fechaMensaje>2015-07-17T11:51:42.938-05:00</fechaMensaje><idMensaje>146096</idMensaje><mensaje>mensaje sms de prueba</mensaje><observaciones>26390388</observaciones><referencias></referencias><remitente></remitente><tipoMensaje>1</tipoMensaje></mensajeResponse></ns2:enviaNotificacionResponse></env:Body></env:Envelope>"

        Return response

    End Function

End Class