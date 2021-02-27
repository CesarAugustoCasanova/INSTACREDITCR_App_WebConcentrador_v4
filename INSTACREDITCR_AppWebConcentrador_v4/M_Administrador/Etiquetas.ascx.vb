
Imports System.Data
Imports System.Data.SqlClient
Imports Db
Imports Telerik.Web.UI

Partial Class Etiquetas
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
        If DtvVarios.Count > 0 Then
            DdlCampo.DataTextField = "Comments"
            DdlCampo.DataValueField = "COLUMN_NAME"
            DdlCampo.DataSource = DtsVarios
            DdlCampo.DataBind()
        End If
        Return DtsVarios

    End Function
    Private Sub Etiquetas_Load(sender As Object, e As EventArgs) Handles Me.Load
        'If Session("PostBack") = True Then
        ' Session("PostBack") = False
        Dim SSCommandAgencias As New SqlCommand
            SSCommandAgencias.CommandText = "Sp_Add_Cat_Etiquetas"
            SSCommandAgencias.CommandType = CommandType.StoredProcedure
            SSCommandAgencias.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 15
            DdlTabla.DataSource = Consulta_Procedure(SSCommandAgencias, "Etiqueta")
            DdlTabla.DataValueField = "VALOR"
            DdlTabla.DataTextField = "TEXTO"
        DdlTabla.DataBind()
        Session("Tablatext") = DdlTabla.SelectedText.ToString

        Dim tabla As String = Session("Tabla")
            Llenar(0, "", "", tabla, "", 5)
            If Session("Campo") <> "" Then
                DdlCampo.SelectedValue = Session("Campo")
            Else
                DdlCampo.SelectedIndex = -1
            End If

        'End If
    End Sub

    Private Sub DdlTabla_SelectedIndexChanged(sender As Object, e As DropDownListEventArgs) Handles DdlTabla.SelectedIndexChanged
        If DdlTabla.SelectedIndex <> 0 Then
            Llenar(0, "", "", DdlTabla.SelectedValue, "", 5)
        End If
    End Sub

    Private Sub DdlCampo_SelectedIndexChanged(sender As Object, e As DropDownListEventArgs) Handles DdlCampo.SelectedIndexChanged
        Llenar(0, "", "", Session("Tablatext"), "", 5)
        Session("CampoText") = e.Text.ToString
        Session("CampoValue") = e.Value.ToString
        DdlCampo.SelectedValue = Session("CampoValue")
    End Sub
End Class
