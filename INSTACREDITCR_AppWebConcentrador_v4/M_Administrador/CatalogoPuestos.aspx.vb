
Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports Db

Partial Class CatalogoPuestos
    Inherits System.Web.UI.Page
    Dim OraDATASOURCE As SqlDataSource

    Public Property tmpUSUARIO As IDictionary
        Get
            Return CType(Session("USUARIO"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("USUARIO") = value
        End Set
    End Property
    Private Sub CatalogoPuestos_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            RadTreeList1.ExpandedIndexes.Add(New TreeListHierarchyIndex() With {
             .NestedLevel = 0,
             .LevelIndex = 0
            })

            ' RadTreeList1.DataSource = puestos(0, 0, "", 0)
            'RadTreeList1.DataBind()
        End If
    End Sub
    Protected Sub RadTreeList1_ItemDeleted(ByVal sender As Object, ByVal e As TreeListDeletedEventArgs)

        If e.Exception IsNot Nothing Then
            e.ExceptionHandled = True
            If e.Item IsNot Nothing Then
                Aviso("Location with ID " + TryCast(e.Item, TreeListDataItem)("LocationID").Text + " cannot be deleted. Reason: " + e.Exception.Message)
            End If
        Else
            If e.Item IsNot Nothing Then
                Aviso("Location with ID " + TryCast(e.Item, TreeListDataItem)("LocationID").Text + " is deleted!")
            End If
        End If
    End Sub

    Protected Sub RadTreeList1_ItemInserted(ByVal sender As Object, ByVal e As TreeListInsertedEventArgs)
        If e.Exception IsNot Nothing Then
            e.ExceptionHandled = True
            Aviso("Location cannot be inserted. Reason: " + e.Exception.Message)
        Else
            Aviso("New location is inserted!")
        End If
    End Sub

    Protected Sub RadTreeList1_ItemUpdated(ByVal sender As Object, ByVal e As TreeListUpdatedEventArgs)
        If e.Exception IsNot Nothing Then
            e.ExceptionHandled = True
            If RadTreeList1.EditMode = TreeListEditMode.InPlace Then
                Aviso("Location with ID " + TryCast(e.Item, TreeListDataItem)("LocationID").Text + " cannot be updated. Reason: " + e.Exception.Message)
            Else
                Aviso("Location with ID " + TryCast(e.Item, TreeListEditFormItem).ParentItem("LocationID").Text + " cannot be updated. Reason: " + e.Exception.Message)
            End If
        Else
            If RadTreeList1.EditMode = TreeListEditMode.InPlace Then
                Aviso("Location with ID " + TryCast(e.Item, TreeListDataItem)("LocationID").Text + "  is updated!")
            Else
                Aviso("Location with ID " + TryCast(e.Item, TreeListEditFormItem).ParentItem("LocationID").Text + "  is updated!")
            End If
        End If
    End Sub
    Function puestos(ByVal v_bandera As Integer, ByVal v_puestoid As Integer, ByVal v_puestonombre As String, ByVal v_puestopadre As Integer) As Object
        Dim SSCommandVarios As New SqlCommand
        SSCommandVarios.CommandText = "SP_PUESTOS"
        SSCommandVarios.CommandType = CommandType.StoredProcedure
        SSCommandVarios.Parameters.Add("@v_bandera", SqlDbType.NVarChar).Value = v_bandera
        SSCommandVarios.Parameters.Add("@v_puestoid", SqlDbType.NVarChar).Value = v_puestoid
        SSCommandVarios.Parameters.Add("@v_puestonombre", SqlDbType.NVarChar).Value = v_puestonombre
        SSCommandVarios.Parameters.Add("@v_puestopadre", SqlDbType.Decimal).Value = v_puestopadre
        Dim DtsVarios As DataTable = Consulta_Procedure(SSCommandVarios, "Usuarios")
        Return DtsVarios
    End Function
    Protected Sub Aviso(ByVal MSJ As String)
        RadAviso.RadAlert(MSJ, 440, 155, "AVISO", Nothing)
    End Sub

    Protected Sub RadTreeList1_NeedDataSource(sender As Object, e As TreeListNeedDataSourceEventArgs)
        RadTreeList1.DataSource = puestos(0, 0, "", 0)
    End Sub


    Private Sub RadTreeList1_ItemCommand(sender As Object, e As TreeListCommandEventArgs) Handles RadTreeList1.ItemCommand
        If e.CommandName = "PerformInsert" Then
            Dim table As New Hashtable()
            Dim item As TreeListEditableItem = TryCast(e.Item, TreeListEditableItem)
            item.ExtractValues(table)
            If table.Item("Puesto") <> "" Then
                puestos(1, 0, table.Item("Puesto"), table.Item("ParentPuestoID"))
                Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "CatalogoPuestos", "Adicion de puesto: " & table.Item("Puesto"))
            Else
                Aviso("Es Necesario Indicar El Nombre del Puesto")
            End If
        ElseIf e.CommandName = "Delete" Then
            Dim item As TreeListDataItem = TryCast(e.Item, TreeListDataItem)
            Dim borrar As String = item.GetDataKeyValue("PuestoID").ToString()
            Dim nombrepuesto As String = item.GetDataKeyValue("Puesto").ToString()
            If Not existenusr(borrar) Then
                If Not existenhijos(borrar) Then
                    puestos(2, borrar, "", 0)
                    Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "CatalogoPuestos", "Eliminacion de puesto: " & nombrepuesto)
                    Aviso("El puesto ha sido eliminado satisfactoriamente")
                Else
                    Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "CatalogoPuestos", "Intento de eliminacion de puesto: " & nombrepuesto)
                    Aviso("El puesto tiene puesto(s) bajo su cargo; elimínelos primero")
                End If
            Else
                Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "CatalogoPuestos", "Intento de eliminacion de puesto: " & nombrepuesto)
                Aviso("El puesto aún se encuentra asignado a al menos un usuario")
            End If

        ElseIf e.CommandName = "Update" Then
            Dim table As New Hashtable()
            Dim item As TreeListEditableItem = TryCast(e.Item, TreeListEditableItem)
            item.ExtractValues(table)
            puestos(3, table.Item("PuestoID"), table.Item("Puesto"), 0)
            Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "CatalogoPuestos", "Actualizacion de puesto: " & table.Item("Puesto"))
        End If
    End Sub

    Private Function existenusr(borrar As String) As Boolean
        Dim SSCommandVarios As New SqlCommand
        SSCommandVarios.CommandText = "SP_PUESTOS"
        SSCommandVarios.CommandType = CommandType.StoredProcedure
        SSCommandVarios.Parameters.Add("@v_bandera", SqlDbType.NVarChar).Value = 4
        SSCommandVarios.Parameters.Add("@v_puestoid", SqlDbType.NVarChar).Value = borrar
        Dim DtsVarios As DataTable = Consulta_Procedure(SSCommandVarios, "Usuarios")
        If DtsVarios(0)(0) = 0 Then
            Return 0
        Else
            Return 1
        End If
    End Function

    Private Function existenhijos(borrar As String) As Boolean
        Dim SSCommandVarios As New SqlCommand
        SSCommandVarios.CommandText = "SP_PUESTOS"
        SSCommandVarios.CommandType = CommandType.StoredProcedure
        SSCommandVarios.Parameters.Add("@v_bandera", SqlDbType.NVarChar).Value = 5
        SSCommandVarios.Parameters.Add("@v_puestoid", SqlDbType.NVarChar).Value = borrar
        Dim DtsVarios As DataTable = Consulta_Procedure(SSCommandVarios, "Usuarios")
        If DtsVarios(0)(0) = 0 Then
            Return 0
        Else
            Return 1
        End If
    End Function
End Class
