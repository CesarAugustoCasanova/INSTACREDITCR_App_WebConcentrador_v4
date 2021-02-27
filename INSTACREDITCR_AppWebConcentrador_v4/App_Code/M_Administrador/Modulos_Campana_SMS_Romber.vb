Imports System.Data
Imports System.IO
Imports System.Net
Imports Microsoft.VisualBasic

Public Class Modulos_Campana_SMS_Romber
    '{"user""8186474300","password":"8186474300","number":"5527411362","message":"room","idmessage":"0"}
    ''http://sms.romber.com.mx:8080/http/enviaSMS?user=8186474300&password=186474300&number=5527411362&message=room&idmessage=0


    Public Shared Function enviaSMS(v_username As String, v_password As String, v_number As String, v_message As String, v_idmessage As String) As String
        'Dim strIdSMS As String = Date.Now.ToString("yyyyMMddHHmmss") & "_" & CType(Session("credito"), Credito).PR_MC_CREDITO
        Dim requestSMS As HttpWebRequest
        Dim response As HttpWebResponse = Nothing
        Dim reader As StreamReader
        Dim strData As String = "?" &
        "user=" & v_username &
        "&password=" & v_password &
        "&number=" & HttpUtility.UrlEncode(v_number) &
        "&message=" & HttpUtility.UrlEncode(v_message) &
        "&idmessage=" & HttpUtility.UrlEncode(v_idmessage)


        Try
            requestSMS = DirectCast(WebRequest.Create("http://sms.romber.com.mx:8080/http/enviaSMS" & strData), HttpWebRequest)
            response = DirectCast(requestSMS.GetResponse(), HttpWebResponse)
            reader = New StreamReader(response.GetResponseStream())
            Return reader.ReadToEnd()

        Catch ex As Exception
            Return ex.Message
        Finally
            If Not response Is Nothing Then response.Close()
        End Try
    End Function
End Class
