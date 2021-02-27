
Imports System.Data
Imports System.Data.SqlClient
Imports Db
Imports Telerik.Web.UI

Partial Class Plantillas

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

    Private Sub Plantillas_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Session("LLenar") = True Then
            Session("LLenar") = False
            llenarlista()
        End If

    End Sub
    Sub llenarlista()
        RLBEtiquetas.Items.Clear()
        Dim tabla As DataTable = Llenar(0, "", "", "", "", 0)
        For i = 0 To tabla.Rows.Count - 1
            RLBEtiquetas.Items.Add(CType(tabla.Rows(i).Item(0), String))
        Next
        RLBEtiquetas.DataBind()

    End Sub


    Function Llenar(V_Cat_Sm_Id As Integer, V_Cat_Sm_Descripcion As String, V_Cat_Sm_Valor As String, V_Cat_Sm_Tabla As String, V_Cat_Sm_Camporeal As String, V_Bandera As Integer) As DataTable


        Dim SSCommandAgencias As New SqlCommand
        SSCommandAgencias.CommandText = "Sp_Add_Cat_Etiquetas"
        SSCommandAgencias.CommandType = CommandType.StoredProcedure
        SSCommandAgencias.Parameters.Add("@V_Cat_Sm_Id", SqlDbType.Decimal).Value = V_Cat_Sm_Id
        SSCommandAgencias.Parameters.Add("@V_Cat_Sm_Descripcion", SqlDbType.NVarChar).Value = V_Cat_Sm_Descripcion
        SSCommandAgencias.Parameters.Add("@V_Cat_Sm_Valor", SqlDbType.NVarChar).Value = V_Cat_Sm_Valor
        SSCommandAgencias.Parameters.Add("@V_Cat_Sm_Tabla", SqlDbType.NVarChar).Value = V_Cat_Sm_Tabla
        SSCommandAgencias.Parameters.Add("@V_Cat_Sm_Camporeal", SqlDbType.NVarChar).Value = V_Cat_Sm_Camporeal
        SSCommandAgencias.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = V_Bandera
        Dim DtsVarios As DataTable = Consulta_Procedure(SSCommandAgencias, "Etiqueta")
        Dim DtvVarios As DataView = DtsVarios.DefaultView

        Return DtsVarios

    End Function

    Protected Sub RLBEtiquetas_Dropped(sender As Object, e As RadListBoxDroppedEventArgs)
        For Each item As RadListBoxItem In e.SourceDragItems
            TxtCAT_PL_MENSAJE.Text += " [[" + item.Text + "]] "
        Next
    End Sub

    Private Sub RLBEtiquetas_ItemCheck(sender As Object, e As RadListBoxItemEventArgs) Handles RLBEtiquetas.ItemCheck
        TxtCAT_PL_MENSAJE.Text += " [[" + RLBEtiquetas.SelectedItem.Text + "]] "
        RLBEtiquetas.ClearChecked()
    End Sub


End Class
