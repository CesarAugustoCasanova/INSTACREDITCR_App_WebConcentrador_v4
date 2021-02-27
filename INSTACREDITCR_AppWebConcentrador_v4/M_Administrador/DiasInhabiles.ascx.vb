
Partial Class DiasInhabiles
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
    Shared llenaras As Boolean = False

    Private Sub llenar()

        For Each texto As String In Split(DataItem.row.item(2).ToString, ",")
            Try
                DdlEstado.Items.FindItemByValue(texto).Checked = True

            Catch ex As Exception
            End Try
        Next

        TxtNombre.Text = DataItem.row.item(3).ToString
        TxtNombre.Enabled = False
        RDPFechaInicio.DateInput.DisplayText = DataItem.row.item(1).ToString
        RDPFechaInicio.SelectedDate = CType(DataItem.row.item(1).ToString, Date)
        RDPFechaFin.DateInput.DisplayText = DataItem.row.item(0).ToString
        RDPFechaFin.SelectedDate = CType(DataItem.row.item(0).ToString, Date)
        '  DdlEstado.SelectedValue = DataItem.row.item(2).ToString
        ' DdlEstado.Enabled = False


    End Sub

    Private Sub DiasInhabiles_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender


        DdlEstado.DataValueField = "ESTADO"
            DdlEstado.DataTextField = "ESTADO"
        DdlEstado.DataSource = Class_Judicial.llenarcatalogojudicial("ESTADOS")
        DdlEstado.DataBind()
            If Session("PostBack") = True Then
                Session("PostBack") = False
                llenar()
            Else
                llenaras = True
                TxtNombre.Enabled = True
                DdlEstado.Enabled = True
            End If

    End Sub
End Class
