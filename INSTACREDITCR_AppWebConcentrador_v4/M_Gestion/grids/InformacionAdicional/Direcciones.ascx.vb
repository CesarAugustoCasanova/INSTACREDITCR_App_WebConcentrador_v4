Imports Db
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports Funciones
Imports System.Data
Imports Telerik.Web.UI.Calendar

Partial Class M_Gestion_grids_InformacionAdicional_Direcciones
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

    Public Function LlenarDrops(ByVal flag As Integer) As String()
        Dim ds As DataTable = Nothing
        Dim valores As String()
        Dim cuantos As Integer = 0
        Select Case flag
            Case 1
                ds = Class_InformacionAdicional.LlenarElementosAgregar("", 5)
            Case 2
                ds = Class_InformacionAdicional.LlenarElementosAgregar("", 7)
            Case 3
                ds = Class_InformacionAdicional.LlenarElementosAgregar("", 6)
        End Select
        If ds.Rows.Count > 0 Then
            cuantos = ds.Rows.Count
            valores = New String(cuantos - 1) {}
        Else
            valores = New String(0) {}
        End If
        For i = 0 To cuantos - 1
            valores(i) = ds.Rows(i)(1)
        Next
        Return valores
    End Function

    Public Function revisa(ByRef seleccionado As Object) As String
        Dim selec As String = ""
        Try
            selec = Convert.ToString(seleccionado)
            If selec = "D" Or selec = "C" Then
                selec = ""
            End If
        Catch
            selec = ""
        End Try
        Return selec
    End Function

    Public Function revisa2(ByRef seleccionado As Object) As String
        Dim selec As String = ""
        Try
            selec = Convert.ToString(seleccionado)
            If selec = "Seleccione" Or selec = "" Or selec = " " Then
                selec = ""
            End If
        Catch
            selec = ""
        End Try
        Return selec
    End Function

    Public Function quitaNull(ByVal valor As Object) As Boolean
        If IsDBNull(valor) Then
            Return False
        End If
        If TryCast(valor, String) = "1" Then
            Return True
        Else
            Return False
        End If
    End Function
    Protected Sub actualizar_ddl(cp As String)
        Dim DtsDatosCp As DataTable
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_COMPLETA_BUSQUEDA"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_PATRON", SqlDbType.NVarChar).Value = cp
        SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.NVarChar).Value = 1

        DtsDatosCp = Consulta_Procedure(SSCommand, "BUSQUEDA")
        Dim cuantos As Integer = DtsDatosCp.Rows.Count
        'CAT_SE_COLONIA
        If cuantos <> 0 Then
            DDCol.DataTextField = "CAT_SE_COLONIA"
            DDCol.DataValueField = "CAT_SE_COLONIA"
            DDCol.DataSource = DtsDatosCp
            DDCol.DataBind()
            DDCol.Items.Insert(0, "Seleccione")

            TBCiudad.Text = DtsDatosCp.Rows(0)("CAT_SE_CIUDAD")
            TBEstado.Text = DtsDatosCp.Rows(0)("CAT_SE_ESTADO")
            ' TBMunicipio.Text = DtsDatosCp.Rows(0)("CAT_SE_MUNICIPIO")
        End If
    End Sub
    Protected Sub btnValidaCP_Click(sender As Object, e As EventArgs) Handles btnValidaCP.Click
        If TBCP.Text.Length = 5 Then
            actualizar_ddl(TBCP.Text)
        End If
    End Sub

    Private Sub TB14_SelectedDateChanged(sender As Object, e As SelectedDateChangedEventArgs) Handles TB14.SelectedDateChanged
        TB15.MinDate = TB14.SelectedDate
    End Sub

    Private Sub M_Gestion_grids_InformacionAdicional_Direcciones_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            btnCancel.Focus()
        End If
    End Sub
End Class
