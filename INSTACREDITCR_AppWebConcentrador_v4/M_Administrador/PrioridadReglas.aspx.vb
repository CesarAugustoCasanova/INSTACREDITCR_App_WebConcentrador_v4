
Imports Telerik.Web.UI

Partial Class M_Administrador_PrioridadReglas
    Inherits System.Web.UI.Page

    Private Sub BTNAceptar_Click(sender As Object, e As EventArgs) Handles BTNAceptar.Click
        For Each item As RadListBoxItem In LBPonderacion.Items
            Dim NuevaPonderacion As Integer = item.Index + 1
            Dim ID As String = item.Text.Split("|")(0).Trim
            SP.PonderaReglas(20, ID, NuevaPonderacion)
        Next
        LBPonderacion.DataSource = SP.PonderaReglas(19)
        LBPonderacion.DataBind()
    End Sub

    Private Sub BTNCancelar_Click(sender As Object, e As EventArgs) Handles BTNCancelar.Click
        LBPonderacion.DataSource = SP.PonderaReglas(19)
        LBPonderacion.DataBind()
    End Sub



    Private Sub M_Administrador_PrioridadReglas_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LBPonderacion.DataSource = SP.PonderaReglas(19)
            LBPonderacion.DataBind()

        End If

    End Sub
End Class
