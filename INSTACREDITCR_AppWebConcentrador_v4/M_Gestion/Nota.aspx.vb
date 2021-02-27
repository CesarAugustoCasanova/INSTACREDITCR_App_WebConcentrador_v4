
Imports System.Data
Imports System.Data.SqlClient
Imports Db

Partial Class Nota
    Inherits System.Web.UI.Page
    Private Shared note As NotaDB
    Public Property tmpUSUARIO As IDictionary
        Get
            Return CType(Session("USUARIO"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("USUARIO") = value
        End Set
    End Property

    Private Sub BtnGuardarnota_Click(sender As Object, e As EventArgs) Handles BtnGuardarnota.Click
        note.Nota = TBNota.Text
        Dim resultado As String = note.commit()
        If resultado = "OK" Then
            shownotificacion("Exito", "Nota guardada exitosamente.", "ok")
            note.load()
            TBNota.Text = note.Nota
        Else
            shownotificacion("Intenta de nuevo", resultado, "deny")
        End If
    End Sub

    Private Sub Nota_Error(sender As Object, e As EventArgs) Handles Me.[Error]
        shownotificacion("Error", "Error de sistema. Recarga la página.", "deny")
    End Sub

    Private Sub Nota_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            note = New NotaDB()
            note.User = tmpUSUARIO("CAT_LO_USUARIO")
            note.load()
            TBNota.Text = note.Nota
        End If
    End Sub

    Private Sub shownotificacion(ByVal title As String, ByVal text As String, ByVal icon As String)
        RadNotification1.Title = title
        RadNotification1.Text = text.Replace(Chr(10), "").Replace(Chr(13), "").Replace("""", "").Replace("'", "")
        RadNotification1.ContentIcon = icon
        RadNotification1.TitleIcon = icon
        RadNotification1.Show()
    End Sub
End Class

Class NotaDB
    Private _user As String
    Private _nota As String

    Public Property User As String
        Get
            Return _user
        End Get
        Set(value As String)
            _user = value
        End Set
    End Property

    Public Property Nota As String
        Get
            Return _nota
        End Get
        Set(value As String)
            _nota = value
        End Set
    End Property


    Public Function commit() As String

        Dim SSCommand As New SqlCommand("sp_gestion_notas")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_USUARIO", SqlDbType.NVarChar).Value = User
        SSCommand.Parameters.Add("@V_NOTA", SqlDbType.NVarChar).Value = Nota
        SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.NVarChar).Value = "0"
        Dim DtsHist_Act As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Return DtsHist_Act.Rows(0).Item(0)
    End Function

    Public Sub load()
        Dim SSCommand As New SqlCommand("sp_gestion_notas")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_USUARIO", SqlDbType.NVarChar).Value = User
        SSCommand.Parameters.Add("@V_NOTA", SqlDbType.NVarChar).Value = " "
        SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.NVarChar).Value = "1"
        Dim DtsHist_Act As DataTable = Consulta_Procedure(SSCommand, "ELEMENTOS")
        Nota = DtsHist_Act.Rows(0).Item(0)
    End Sub
End Class