Imports System.Data
Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports Db
Imports System.Globalization
Imports System.IO
Imports System.Net
Imports System.Xml
Public Class Push
    Public Shared Function SP_GET_KEY_MOVIL(ByVal V_Valor As String) As String
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_GET_KEY_MOVIL"
        SSCommand.CommandType = CommandType.StoredProcedure
        Dim v_dataset As DataTable = Db.Consulta_Procedure(SSCommand, "SP_GET_KEY_MOVIL")

        Return v_dataset.Rows(0)("CAT_VA_VALOR")
    End Function
    Public Shared Function Envia_Push(ByVal v_token As String, ByVal v_titulo As String, ByVal v_mensaje As String, ByVal v_prioridad As String) As String
        Dim v_color As String = "#3366FF"
        Dim V_KEY As String = SP_GET_KEY_MOVIL("")
        If v_prioridad = "Alta" Then
            v_color = "#FF3000"
        End If
        Try


            Dim Uri As String = "https://fcm.googleapis.com/fcm/send"
            Dim data As String = ""

            data = "{ ""data"": {""Tipo"": ""Ubicacion"",""Accion"": ""Regresar Cordenadas""}, ""to"": """ & v_token & """,""notification"": {""title"": """ & v_titulo & """,""body"": """ & v_mensaje & """,""priority"": """ & v_prioridad & """,""sound"": ""Default"",""color"": """ & v_color & """}}"

            'data = "{ ""data"": {""Tipo"": ""Ubicacion"",""Accion"": ""Regresar Cordenadas""},""to"" : """ & v_token & """}"

            Dim CUrl As WebRequest = WebRequest.Create(Uri)
            CUrl.Method = "POST"
            CUrl.ContentLength = data.Length
            CUrl.ContentType = "application/json"
            Dim enc As New UTF8Encoding()
            'CUrl.Headers.Add("Authorization", "key=AIzaSyDzbM5t5RroudXALucYuXjBB85UzKIMsdc") ' Coyote
            '                                       AIzaSyCFxCu_b3GJ-Mm_CQYa3g9oyNvK16xFBhM") ' Manuel
            CUrl.Headers.Add("Authorization", "key=" & V_KEY.ToString.Trim) ' Manuel
            'CUrl.Headers.Add("Authorization", "key=AIzaSyDYkdz2uxfAM3599rW4R3YhURNnYAqQgw0")

            Using ds As Stream = CUrl.GetRequestStream()
                ds.Write(enc.GetBytes(data), 0, data.Length)
            End Using

            Dim wr As WebResponse = CUrl.GetResponse()
            Dim receiveStream As Stream = wr.GetResponseStream()
            Dim reader As New StreamReader(receiveStream, Encoding.UTF8)
            Return reader.ReadToEnd()
        Catch ex As Exception
            Return ex.Message
        End Try

    End Function
End Class
