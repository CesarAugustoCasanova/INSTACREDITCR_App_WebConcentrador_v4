
Imports System.Data
Imports System.Data.SqlClient
Imports Db

Partial Class Especiales
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

    Private Sub Especiales_Error(sender As Object, e As EventArgs) Handles Me.[Error]
        Dim err As String = ""
    End Sub

    Private Sub Especiales_Load(sender As Object, e As EventArgs) Handles Me.Load
        'If RCBCampoSaldo.Items.Count = 0 Then


        'End If
        'If DDlInstancia.Items.Count = 0 Then



        'End If

        If Session("DO") = "" Then
            Dim SSCommandAgencias As New SqlCommand
            SSCommandAgencias.CommandText = "SP_INSTANCIAS"
            SSCommandAgencias.CommandType = CommandType.StoredProcedure
            Dim DtsVarios As DataTable = Consulta_Procedure(SSCommandAgencias, "Especiales")
            'Dim orcommsaldos As New SqlCommand
            'orcommsaldos.CommandText = "SP_CAMPANAS_ESP"
            'orcommsaldos.CommandType = CommandType.StoredProcedure
            'orcommsaldos.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 9
            'orcommsaldos.Parameters.Add("@CV_1", OracleType.Cursor).Direction = ParameterDirection.Output
            'Dim Dtssaldos As DataSet = Consulta_Procedure(orcommsaldos, "Especiales")
            'RCBCampoSaldo.DataTextField = "CAT_SA_DESCRIPCION"
            'RCBCampoSaldo.DataValueField = "CAT_SA_CAMPO"
            'RCBCampoSaldo.DataSource = Dtssaldos.Tables(0)
            'RCBCampoSaldo.DataBind()

            Dim fecha As String = Session("FechaG")
            If fecha <> "" Then

            End If

        End If
    End Sub

    Private Sub Especiales_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        If Session("DO") = "" Then
            Try
                TxtNombre.Text = DataItem("Nombre")
                TxtDescripcion.Text = DataItem("Descripcion")
                NtxtBonificacion.Text = DataItem("Bonificacion")
                NtxtCondonacionN.Text = DataItem("CondonacionN")
                NtxtCondonacionM.Text = DataItem("CondonacionM")
                NtxtCononacionC.Text = DataItem("CondonacionC")
                NtxtExterno.Text = DataItem("externo")
                RCBCampoSaldo.FindItemByValue(DataItem("saldo")).Selected = True
            Catch ex As Exception

            End Try
            Try
                Dim fecha As Date = Convert.ToDateTime(DataItem("VIGENCIA"))
                RDPVigencia.MinDate = fecha
                RDPVigencia.SelectedDate = fecha.AddMinutes(2)
            Catch ex As Exception
                RDPVigencia.MinDate = Now
                RDPVigencia.SelectedDate = Nothing
            End Try
            Session("DO") = "x"
        End If
    End Sub
End Class
