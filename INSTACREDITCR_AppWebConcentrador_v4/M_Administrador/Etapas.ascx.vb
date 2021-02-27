
Imports Telerik.Web.UI

Partial Class Etapas
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

    Private Sub DdlTipoJuicio_CheckAllCheck(sender As Object, e As RadComboBoxCheckAllCheckEventArgs) Handles DdlTipoJuicio.CheckAllCheck
        Dim aux As String = ""
        For Each item As RadComboBoxItem In DdlTipoJuicio.CheckedItems
            aux &= item.Value & ","
        Next
        Try
            aux = aux.Substring(0, aux.Length - 1)
        Catch ex As Exception
        End Try
        Session("Juicios") = aux
    End Sub

    Private Sub DdlTipoJuicio_ItemChecked(sender As Object, e As RadComboBoxItemEventArgs) Handles DdlTipoJuicio.ItemChecked
        Dim aux As String = ""
        For Each item As RadComboBoxItem In DdlTipoJuicio.CheckedItems
            aux &= item.Value & ","
        Next
        Try
            aux = aux.Substring(0, aux.Length - 1)
        Catch ex As Exception
        End Try
        Session("Juicios") = aux
    End Sub

    Private Sub Etapas_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("PostBack") = True Then
            DdlTipoJuicio.DataValueField = "ID"
            DdlTipoJuicio.DataTextField = "VALOR"
            DdlTipoJuicio.DataSource = Class_Judicial.Catalogos("", 13)
            DdlTipoJuicio.DataBind()
        End If
    End Sub

    Private Sub Etapas_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        If Session("PostBack") = True Then
            Session("PostBack") = False
            llenar()
        End If
    End Sub

    Private Sub llenar()
        Try
            TxtNombre.Text = DataItem.row.item(1).ToString
            Dim aux As String = ""
            For Each item As String In DataItem.row.item(2).ToString.Split(",")
                DdlTipoJuicio.FindItemByValue(item).Checked = True
                aux &= item & ","
            Next
            Try
                aux = aux.Substring(0, aux.Length - 1)
            Catch ex As Exception
            End Try
            Session("Juicios") = aux
            TxtDescripcion.Text = DataItem.row.item(3).ToString
            NtxtImporte.Text = DataItem.row.item(4).ToString
        Catch
        End Try
    End Sub
End Class
