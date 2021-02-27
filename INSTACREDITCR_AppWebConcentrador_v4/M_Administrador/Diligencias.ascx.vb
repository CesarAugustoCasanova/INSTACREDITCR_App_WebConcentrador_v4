
Imports System.Data
Imports System.Data.SqlClient
Imports Db
Imports Telerik.Web.UI
Partial Class Productos
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

    Function LlenarSubresultado() As DataTable
        Dim oraCommand As New SqlCommand
        oraCommand.CommandText = "SP_CATALOGO_DILIGENCIAS"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_Diligencia", SqlDbType.VarChar).Value = ""
        oraCommand.Parameters.Add("V_Resultado", SqlDbType.VarChar).Value = ""
        oraCommand.Parameters.Add("V_Subresultado", SqlDbType.VarChar).Value = ""
        oraCommand.Parameters.Add("V_Mparticipante", SqlDbType.VarChar).Value = ""
        oraCommand.Parameters.Add("V_Remueble", SqlDbType.VarChar).Value = ""
        oraCommand.Parameters.Add("V_Reinmueble", SqlDbType.VarChar).Value = ""
        oraCommand.Parameters.Add("V_Rcdepositario", SqlDbType.VarChar).Value = ""
        oraCommand.Parameters.Add("V_Riprocesal", SqlDbType.VarChar).Value = ""
        oraCommand.Parameters.Add("V_Promocion", SqlDbType.VarChar).Value = ""
        oraCommand.Parameters.Add("V_Notifcorreo", SqlDbType.VarChar).Value = ""
        oraCommand.Parameters.Add("V_Bandera", SqlDbType.Decimal).Value = 2
        Dim DtsDiligencias As DataTable = Consulta_Procedure(oraCommand, "ELEMENTOS")
        Dim DtvDiligencias As DataView = DtsDiligencias.DefaultView

        If DtvDiligencias.Count > 0 Then
            DdlSubresultado.DataTextField = "CAT_SR_NOMBRE"
            DdlSubresultado.DataValueField = "CAT_SR_NOMBRE"
            DdlSubresultado.DataSource = DtsDiligencias
            'DdlSubresultado.Items.Add("Seleccione")
            DdlSubresultado.DataBind()

        End If
        Return DtsDiligencias
    End Function

    Function LlenarPromociones() As DataTable
        Dim oraCommand As New SqlCommand
        oraCommand.CommandText = "SP_CATALOGO_DILIGENCIAS"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_Diligencia", SqlDbType.VarChar).Value = ""
        oraCommand.Parameters.Add("V_Resultado", SqlDbType.VarChar).Value = ""
        oraCommand.Parameters.Add("V_Subresultado", SqlDbType.VarChar).Value = ""
        oraCommand.Parameters.Add("V_Mparticipante", SqlDbType.VarChar).Value = ""
        oraCommand.Parameters.Add("V_Remueble", SqlDbType.VarChar).Value = ""
        oraCommand.Parameters.Add("V_Reinmueble", SqlDbType.VarChar).Value = ""
        oraCommand.Parameters.Add("V_Rcdepositario", SqlDbType.VarChar).Value = ""
        oraCommand.Parameters.Add("V_Riprocesal", SqlDbType.VarChar).Value = ""
        oraCommand.Parameters.Add("V_Promocion", SqlDbType.VarChar).Value = ""
        oraCommand.Parameters.Add("V_Notifcorreo", SqlDbType.VarChar).Value = ""
        oraCommand.Parameters.Add("V_Bandera", SqlDbType.Decimal).Value = 3
        Dim DtsDiligencias As DataTable = Consulta_Procedure(oraCommand, "ELEMENTOS")
        Dim DtvDiligencias As DataView = DtsDiligencias.DefaultView

        If DtvDiligencias.Count > 0 Then
            DdlPromocion.DataTextField = "CAT_PJ_NOMBREPROMOCION"
            DdlPromocion.DataValueField = "CAT_PJ_NOMBREPROMOCION"
            DdlPromocion.DataSource = DtsDiligencias
            'DdlPromocion.Items.Add("Seleccione")
            DdlPromocion.DataBind()

        End If
        Return DtsDiligencias
    End Function
    Function llenaTodosDrop() As DataTable
        Dim oraCommand As New SqlCommand
        oraCommand.CommandText = "SP_CATALOGO_DILIGENCIAS"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_Diligencia", SqlDbType.VarChar).Value = ""
        oraCommand.Parameters.Add("V_Resultado", SqlDbType.VarChar).Value = Session("VALORID")
        oraCommand.Parameters.Add("V_Subresultado", SqlDbType.VarChar).Value = ""
        oraCommand.Parameters.Add("V_Mparticipante", SqlDbType.VarChar).Value = ""
        oraCommand.Parameters.Add("V_Remueble", SqlDbType.VarChar).Value = ""
        oraCommand.Parameters.Add("V_Reinmueble", SqlDbType.VarChar).Value = ""
        oraCommand.Parameters.Add("V_Rcdepositario", SqlDbType.VarChar).Value = ""
        oraCommand.Parameters.Add("V_Riprocesal", SqlDbType.VarChar).Value = ""
        oraCommand.Parameters.Add("V_Promocion", SqlDbType.VarChar).Value = ""
        oraCommand.Parameters.Add("V_Notifcorreo", SqlDbType.VarChar).Value = ""
        oraCommand.Parameters.Add("V_Bandera", SqlDbType.Decimal).Value = 6
        Dim DtsDiligencias As DataTable = Consulta_Procedure(oraCommand, "ELEMENTOS")
        Dim DtvDiligencias As DataView = DtsDiligencias.DefaultView


        Return DtsDiligencias
    End Function

    Private Sub Productos_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim valores As DataRow = llenaTodosDrop().Rows(0)
        LlenarSubresultado()
        LlenarPromociones()
        If Session("PostBack") = True Then
            Session("PostBack") = False



            DdlDiligencia.SelectedText = valores.Item("DILIGENCIA")
            DdlResultado.SelectedText = valores.Item("RESULTADO")
            DdlSubresultado.SelectedText = valores.Item("SUBRESULTADO")
            DdlMarcarParticipante.SelectedText = valores.Item("MPARTICIPANTE")
            DdlRembueble.SelectedText = valores.Item("REMUEBLE")
            DdlReinmueble.SelectedText = valores.Item("REINMUEBLE")
            DdlRcdepositario.SelectedText = valores.Item("RCDEPOSITARIO")
            DdlRiprocesal.SelectedText = valores.Item("RIPROCESAL")
            DdlPromocion.SelectedText = valores.Item("PROMOCION")
            DdlNotifCorreo.SelectedText = valores.Item("NOTIFCORREO")

            Dim tabla As String = Session("Tabla")

            If Session("Campo") <> "" Then
                'DdlMercado.SelectedValue = Session("Campo")
            Else
                'DdlMercado.SelectedValue = "Seleccione"
            End If

        End If
        ' If Session("Campo") <> "" Then
        'DdlMercado.SelectedValue = Session("Campo")

        ' Else
        'End If

    End Sub
End Class
