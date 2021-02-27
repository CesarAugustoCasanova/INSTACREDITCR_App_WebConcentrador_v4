
Imports Telerik.Web.UI
Imports System.Data
Imports Model.ReadWrite.Telerik

Partial Class CatalogoAplicaciones
    Inherits System.Web.UI.Page
    Const MaxTotalBytes As Integer = 1048576
    ' 1 MB
    Private totalBytes As Int64

    Public Property IsRadAsyncValid() As System.Nullable(Of Boolean)
        Get
            If Session("IsRadAsyncValid") Is Nothing Then
                Session("IsRadAsyncValid") = True
            End If

            Return Convert.ToBoolean(Session("IsRadAsyncValid").ToString())
        End Get
        Set(value As System.Nullable(Of Boolean))
            Session("IsRadAsyncValid") = value
        End Set
    End Property

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)

        IsRadAsyncValid = Nothing
    End Sub

    Protected Sub RadGrid1_ItemCreated(sender As Object, e As GridItemEventArgs)
        If TypeOf e.Item Is GridEditableItem AndAlso e.Item.IsInEditMode Then
            'Dim upload As RadAsyncUpload = TryCast(DirectCast(e.Item, GridEditableItem)("Upload").FindControl("AsyncUpload1"), RadAsyncUpload)
            'Dim cell As TableCell = DirectCast(upload.Parent, TableCell)

            'Dim validator As New CustomValidator()
            'validator.ErrorMessage = "Please select file to be uploaded"
            'validator.ClientValidationFunction = "validateRadUpload"
            'validator.Display = ValidatorDisplay.Dynamic
            'cell.Controls.Add(validator)
        End If
    End Sub

    Protected Function TrimDescription(description As String) As String
        If Not String.IsNullOrEmpty(description) AndAlso description.Length > 200 Then
            Return String.Concat(description.Substring(0, 200), "...")
        End If
        Return description
    End Function

    Protected Sub RadGrid1_NeedDataSource(source As Object, e As GridNeedDataSourceEventArgs)
        Try
            RadGrid1.DataSource = SP.CATALOGO_APLICACIONES(V_BANDERA:=0)

        Catch ex As Exception
            RadGrid1.DataSource = Nothing
        End Try
    End Sub
    Protected Sub RadGrid1_InsertCommand(source As Object, e As GridCommandEventArgs)
        If Not IsRadAsyncValid.Value Then
            e.Canceled = True
            RadAjaxManager1.Alert("The length of the uploaded file must be less than 1 MB")
            Return
        End If

        Dim insertItem As GridEditFormInsertItem = TryCast(e.Item, GridEditFormInsertItem)
        Dim txtNombre As String = TryCast(insertItem("AppName").FindControl("txtNombre"), RadTextBox).Text
        Dim txtPaquete As String = TryCast(insertItem("AppPakage").FindControl("txtPaquete"), RadTextBox).Text
        Dim radAsyncUpload As RadAsyncUpload = TryCast(insertItem("AppIcon").FindControl("AsyncUpload1"), RadAsyncUpload)
        Dim bloqueo As Boolean = TryCast(insertItem("Bloqueo").FindControl("ChbBloqueoE"), RadCheckBox).Checked

        Dim file As UploadedFile = radAsyncUpload.UploadedFiles(0)
        Dim fileData As Byte() = New Byte(file.InputStream.Length - 1) {}
        file.InputStream.Read(fileData, 0, CInt(file.InputStream.Length))

        SP.CATALOGO_APLICACIONES(V_BANDERA:=2, V_ICONO:=fileData, V_NOMBRE:=txtNombre, V_PAQUETE:=txtPaquete, V_GRUPO:=IIf(bloqueo, 1, 0))
    End Sub

    Protected Sub RadGrid1_UpdateCommand(source As Object, e As GridCommandEventArgs)

        Dim editedItem As GridEditableItem = TryCast(e.Item, GridEditableItem)
        Dim ID As Integer = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues(editedItem.ItemIndex)("ID").ToString())
        Dim txtNombre As String = TryCast(editedItem("AppName").FindControl("txtNombre"), RadTextBox).Text
        Dim txtPaquete As String = TryCast(editedItem("AppPakage").FindControl("txtPaquete"), RadTextBox).Text
        Dim bloqueo As Boolean = TryCast(editedItem("Bloqueo").FindControl("ChbBloqueoE"), RadCheckBox).Checked

        SP.CATALOGO_APLICACIONES(V_BANDERA:=1, V_ID:=ID, V_NOMBRE:=txtNombre, V_PAQUETE:=txtPaquete, V_GRUPO:=IIf(bloqueo, 1, 0))

    End Sub

    Protected Sub RadGrid1_ItemCommand(source As Object, e As GridCommandEventArgs)
        If e.CommandName = RadGrid.EditCommandName Then
            ScriptManager.RegisterStartupScript(Page, Page.[GetType](), "SetEditMode", "isEditMode = true;", True)
        End If
    End Sub

    Protected Sub AsyncUpload1_FileUploaded(sender As Object, e As FileUploadedEventArgs)
        If (totalBytes < MaxTotalBytes) AndAlso (e.File.ContentLength < MaxTotalBytes) Then
            e.IsValid = True
            totalBytes += e.File.ContentLength
            IsRadAsyncValid = True
        Else
            e.IsValid = False
            IsRadAsyncValid = False
        End If
    End Sub
End Class
