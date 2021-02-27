
Imports System.Data
Imports System.Web.Services
Imports Telerik.Web.UI

Partial Class M_Administrador_CodigosPostales
    Inherits System.Web.UI.UserControl
    Private _dataItem As Object = Nothing
    Public Property DataItem() As Object
        Get
            Return Me._dataItem
        End Get
        Set(ByVal value As Object)
            Me._dataItem = value
        End Set
    End Property
    Shared codigos As String = ""
    Private Sub M_Administrador_CodigosPostales_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("Borrar") = True Then
            Session("Borrar") = False
            codigos = ""
        End If
        If Session("Editar") Then
            Session("Editar") = False
            TxtNombre.Text = DataItem("PLAZA")
            'TxtCPI.Text = DataItem("CPI")
            txtRegion.Text = DataItem("REGION")
            txtZona.Text = DataItem("ZONA")
            txtUsuario.Text = DataItem("USUARIO")
            txtGestor.Text = DataItem("GESTOR")
            txtRegional.Text = DataItem("REGIONAL")
            txtJefePlaza.Text = DataItem("JEFEPLAZA")
            txtAuxiliar.Text = DataItem("AUXILIAR")
            txtNumPlaza.Text = DataItem("NUMPLAZA")
            codigos = DataItem("CPI") & ","

        End If
    End Sub

    Public Sub rsb1_Search(sender As Object, e As SearchBoxEventArgs)
        Dim s = e
        If e.Value IsNot Nothing Then
            codigos += e.Value & ","
            RGCodigos.Visible = True
            RGCodigos.Rebind()
            rsb1.Text = ""
        End If
    End Sub

    Private Sub RGCodigos_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RGCodigos.NeedDataSource
        Try
            RGCodigos.DataSource = SP.DISTRITOS(0, codigos.Substring(0, codigos.Length - 1), "", "", 0, 0, 4)
        Catch
            RGCodigos.DataSource = Nothing
        End Try
    End Sub

    Private Sub CBRango_CheckedChanged(sender As Object, e As EventArgs) Handles CBRango.CheckedChanged
        If CBRango.Checked = True Then
            DivMultiple.Visible = False
            DivRango.Visible = True
            If CBConservar.Checked = False Then
                codigos = ""
                RGCodigos.Rebind()
            End If
        Else
            DivMultiple.Visible = True
            DivRango.Visible = False
            TxtCPI.Text = ""
            TxtCPF.Text = ""
        End If
    End Sub

    Private Sub RGCodigos_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RGCodigos.ItemCommand
        If e.CommandName = "Delete" Then
            codigos = codigos.Replace(e.Item.Cells(3).Text & ",", "")
            'Dim s As String = "Hola"
            's.Replace("ol", "")
            's.Replace("Hola", "")
        End If
    End Sub
End Class
