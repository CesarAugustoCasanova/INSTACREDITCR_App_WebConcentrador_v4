Imports System.Data
Imports System.Data.SqlClient
Imports Funciones
Imports Db
Imports Conexiones
Imports System
Imports System.Net
Imports System.IO
Imports Newtonsoft.Json
Imports System.Net.Mail
Partial Class DefaultNegociaciones
    Inherits System.Web.UI.Page



    Public Shared Function NuloAVacio(valor As Object) As String
        If Not IsDBNull(valor) Then
            Return valor.ToString()
        Else
            Return ""
        End If
    End Function

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1))
        Response.Cache.SetNoStore()
        Try
            LblError.Text = ""
            PnlCancelar.Visible = False
            If Not IsPostBack Then
                TxtComentario.Text = ""
            End If
            Dim infoCastigo = Request.QueryString("castigo")
            Dim json = "{""Table"":" & DecodeBase64ToString(infoCastigo) & "}"
            Dim table = JsonConvert.DeserializeObject(Of RootObject(Of DataTable))(json).Table

            'Se valida que se pueda aceptar o rechazar el castigo


            Dim V_Bandera As String = table(0)("Id_solicitud")

            If V_Bandera <> "2" And V_Bandera <> "1" Then
                LblError.Text = "Error: Solicitud No Existente"
            Else

                RBtnAceptar.Attributes("id_castigo") = table(0)("id_castigo").ToString
                RBtnAceptar.Attributes("usuario") = table(0)("usuario").ToString
                RBtnAceptar.Attributes("mail") = table(0)("mail").ToString

                If V_Bandera = "1" Then
                    LblError.Text = "Proceso terminado Correctamente. Negociacion Aceptada"
                    enviarautorizacion(Class_Negociaciones.LlenarElementosNego(table(0)("usuario").ToString, "", "", 8).rows(0).item(0))
                Else
                    'PnlCancelar.Visible = True
                    LblError.Text = "Proceso terminado Correctamente. Negociacion Aceptada"
                    enviarDenegacion(Class_Negociaciones.LlenarElementosNego(table(0)("usuario").ToString, "", "", 8).rows(0).item(0))
                End If
            End If

        Catch ex As Exception
            LblError.Text = "Error: Solicitud No Existente"
        End Try
    End Sub

    'Protected Sub RBtnAceptar_Click(sender As Object, e As EventArgs) Handles RBtnAceptar.Click
    '    ' Dim DtsCancelaFicha As DataTable = Solicitud_Castigo(LblId.Text, 2, TxtComentario.Text)
    '    PnlCancelar.Visible = False
    '    LblError.Text = "Solicitud no autorizada"
    '    enviarDenegacion()
    '    'Else
    '    'LblError.Text = V_Estatus
    '    'End If
    'End Sub

    Public Function DecodeBase64ToString(valor As String) As String
        Dim myBase64ret As Byte() = Convert.FromBase64String(valor)
        Dim myStr As String = System.Text.Encoding.UTF8.GetString(myBase64ret)
        Return myStr
    End Function
    Private Sub enviarautorizacion(destinatario As String)
        Dim usuario = RBtnAceptar.Attributes("usuario").ToString
        Dim ID_REGLA = RBtnAceptar.Attributes("id_castigo").ToString
        Dim mailAutoriza = RBtnAceptar.Attributes("mail").ToString
        Dim Random As String = System.Guid.NewGuid().ToString.Substring(0, 15)
        'usuario = "CALCAZAR"        



        Class_Negociaciones.LlenarElementosNego(RBtnAceptar.Attributes("id_castigo").ToString, "", "", 9).rows(0).item(0)


        Dim SmtpServer As New SmtpClient()
        Dim mail As New MailMessage()
        SmtpServer.Credentials = New Net.NetworkCredential("soporte@mccollect.com.mx", "Adalesperra2")
        SmtpServer.Port = 587
        SmtpServer.Host = "smtp.gmail.com"
        SmtpServer.EnableSsl = True
        mail = New MailMessage()
        mail.From = New MailAddress("soporte@mccollect.com.mx")
        mail.To.Add(destinatario) '
        'mail.To.Add("cesar.casanova@mccollect.com.mx") '
        mail.Subject = "Correo de negociaciones - Autorizado"
        mail.IsBodyHtml = True
        mail.Body = "<html><body> <p>La Necociacion fue AUTORIZADO por " & mailAutoriza & "</p>  </body></html>"


        SmtpServer.Send(mail)

    End Sub
    Private Sub enviarDenegacion(destinatario As String)
        Dim usuario = RBtnAceptar.Attributes("usuario").ToString
        Dim mailAutoriza = RBtnAceptar.Attributes("mail").ToString

        Class_Negociaciones.LlenarElementosNego(RBtnAceptar.Attributes("id_castigo").ToString, "", "", 10).rows(0).item(0)

        Dim SmtpServer As New SmtpClient()
        Dim mail As New MailMessage()
        SmtpServer.Credentials = New Net.NetworkCredential("soporte@mccollect.com.mx", "Adalesperra2")
        SmtpServer.Port = 587
        SmtpServer.Host = "smtp.gmail.com"
        SmtpServer.EnableSsl = True
        mail = New MailMessage()
        mail.From = New MailAddress("soporte@mccollect.com.mx")
        mail.To.Add(destinatario) '
        'mail.To.Add("ricardo.torres@mccollect.com.mx") '
        mail.Subject = "Correo de negociaciones - NO Autorizado"
        mail.IsBodyHtml = True
        mail.Body = "<html><body> <p>Necociacion NO AUTORIZADA por " & mailAutoriza & " </p>  </body></html>"


        SmtpServer.Send(mail)

    End Sub

    Class RootObject(Of T)
        Public Property Table As T
    End Class
End Class
