Imports Telerik.Web.UI

Partial Class VisorExpedientes
    Inherits System.Web.UI.Page

    Public Property tmpUSUARIO As IDictionary
        Get
            Return CType(Session("USUARIO"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("USUARIO") = value
        End Set
    End Property

    Private Sub VisorExpedientes_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            modulo_ftp.Visible = True
            modulo_ftp.ShowftpDirectory = False
            modulo_ftp.DirectorioFTP = "ftp://cspmexftp01/"
            modulo_ftp.UsuarioFTP = "ftpmc"
            modulo_ftp.ContrasenaFTP = "C0nsup4g001"
            modulo_ftp.ExtensionesPermitidas = "doc,docx,csv,xls,xlsx,pps,ppsx,ppt,pptx,bmp,gif,jpg,jpge,tif,png,pdf,msg"
            modulo_ftp.DataBind()
        End If
    End Sub
End Class