Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data
Imports System.Data.OracleClient
Imports Db
Imports System.IO
Imports System.Collections.Generic

<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class SessionV
    Inherits System.Web.Services.WebService
    <WebMethod(EnableSession:=True)> _
    Public Shared Function KeepActiveSession() As Boolean
        If HttpContext.Current.Session("Vive") IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function
End Class