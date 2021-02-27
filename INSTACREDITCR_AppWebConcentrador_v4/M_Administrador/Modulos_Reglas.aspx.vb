
Imports Telerik.Web.UI

Partial Class Modulos_Reglas
    Inherits System.Web.UI.Page

    Private Sub gridReglas_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles gridReglas.NeedDataSource
        gridReglas.DataSource = SP.REGLAS(bandera:=1)
    End Sub

    Private Sub gridReglas_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles gridReglas.ItemCommand
        Try

            Select Case e.CommandName
                Case "Edit"
                    Session("ModulosEdit") = True
                    Session("LoadData") = True
                Case "Delete"
                    Dim id = e.Item.Cells(5).Text
                    Dim res = SP.REGLAS(bandera:=11, v_regla_id:=id)
                    If res.Rows(0)(0).ToString = "OK" Then
                        Funciones.showModal(not1, "ok", "Correcto", "Regla eliminada correctamente")
                    End If
                Case "InitInsert"
                    Session("LoadData") = True
                    Session("ModulosEdit") = False
            End Select
        Catch ex As Exception
            e.Canceled = True
            Funciones.showModal(not1, "deny", "Error", ex.Message)
        End Try
    End Sub

    Private Sub gridReglas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gridReglas.SelectedIndexChanged
        Try
            'SE GUARDA EL ID DE LA REGLA. ESTE ID HACE REFERENCIA A LA CAT_REGLAS. 
            Dim selectedID As Integer = TryCast(sender, RadGrid).SelectedItems(0).Cells(5).Text
            Dim selectedRegla As String = TryCast(sender, RadGrid).SelectedItems(0).Cells(6).Text
            Session("ReglaID") = selectedID
            Session("ReglaNombre") = selectedRegla
            Response.Redirect("./Modulos_Acciones.aspx")
        Catch ex As Exception
            Funciones.showModal(not1, "deny", "Error", "No se ha podido seleccionar la regla deseada. Intentelo de nuevo más tarde")
        End Try
    End Sub
End Class
