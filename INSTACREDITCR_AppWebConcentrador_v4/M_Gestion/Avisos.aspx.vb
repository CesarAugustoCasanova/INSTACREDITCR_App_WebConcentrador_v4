Imports Conexiones
Imports System.Data.SqlClient
Imports System.Data
Imports Db
Imports Funciones
Imports Telerik.Web.UI
Imports System.Web.Services
Partial Class M_Gestion_Avisos
    Inherits System.Web.UI.Page
    Public Property tmpCredito As IDictionary
        Get
            Return CType(Session("Credito"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("Credito") = value
        End Set
    End Property
    Public Property tmpUSUARIO As IDictionary
        Get
            Return CType(Session("USUARIO"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("USUARIO") = value
        End Set
    End Property


    Private Sub BtnEnviar_Click(sender As Object, e As EventArgs) Handles BtnEnviar.Click
        Dim folio As String = "Aviso_" & tmpCredito("PR_MC_CREDITO") & "_" & Now.Month & Now.Day & Now.Second
        Try
            If DPFelimite.SelectedDate Is Nothing Then
                LblError.Text = "Seleccione una Fecha limite"
            Else
                Funciones.HIST_avisos(tmpCredito("PR_MC_CREDITO"), tmpUSUARIO("CAT_LO_USUARIO"), CBVisitadores.SelectedValue, folio, DPFelimite.SelectedDate.Value.ToShortDateString, 0)
                Dim tmp_archivo As String
                tmp_archivo = "https://pruebasmc.com.mx/GestionAvisosDescargar/" + Now.ToShortDateString.Replace("/", "_").Replace(" ", "") + "/" + folio + ".pdf"
                Dim aviso As New AvisosPDF(tmpCredito("PR_MC_CREDITO"), CBParticipantes.SelectedValue, CbTipoVivienda.SelectedValue, cbPlantillas.SelectedValue)
                 aviso.CreatePDF(CBVisitadores.SelectedValue, DPFelimite.SelectedDate.Value.ToShortDateString, folio, CBSimulacion.Checked, DPSimulacion.DateInput.DisplayText.ToString)
                Dim vtn As String = "window.open('" & tmp_archivo & "','M2','scrollbars=yes,resizable=yes','height=300', 'width=300')"
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup2", vtn, True)
                LblError.ForeColor = Drawing.Color.Green
                LblError.Text = "Aviso Generado"
            End If
        Catch ex As Exception
            LblError.Text = ex.Message
        End Try
    End Sub

    Private Sub CBParticipantes_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles CBParticipantes.SelectedIndexChanged
        ''LLENAR_DROP2(40, "", cbPlantillas, "id", "nombre", CBParticipantes.SelectedValue)
        dim SSCommand as new sqlcommand
        SSCommand.CommandText = "SP_CATALOGOS"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 39
        SSCommand.Parameters.Add("@V_Valor", SqlDbType.NVarChar).Value = tmpCredito("PR_MC_CREDITO")
        SSCommand.Parameters.Add("@V_Valor2", SqlDbType.NVarChar).Value = CBParticipantes.SelectedValue

        Dim objDSa As DataTable = Consulta_Procedure(SSCommand, "PROD")

        CbTipoVivienda.ClearSelection()
        CbTipoVivienda.Items.Clear()

        CbTipoVivienda.Visible = True
        If objDSa.Rows.Count >= 1 Then
            CbTipoVivienda.EmptyMessage = "Seleccione"
            CbTipoVivienda.DataTextField = "TipoDireccion"
            CbTipoVivienda.DataValueField = "ID"
            CbTipoVivienda.DataSource = objDSa
            CbTipoVivienda.DataBind()
        Else
            CbTipoVivienda.EmptyMessage = "Sin Direcciones disponibles"
            CbTipoVivienda.Enabled = False
        End If

    End Sub

    Private Sub CBSimulacion_CheckedChanged(sender As Object, e As EventArgs) Handles CBSimulacion.CheckedChanged
        DPSimulacion.Enabled = CBSimulacion.Checked
    End Sub

    Private Sub M_Gestion_Avisos_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LLENAR_DROP2(37, tmpCredito("PR_MC_CREDITO"), CBParticipantes, "ValPar", "TexPar")
            LLENAR_DROP2(38, "", CBVisitadores, "vvis", "tvis")
            'LLENAR_DROP2(39, tmpCredito("PR_MC_CLIENTE"), CbTipoVivienda, "valdir", "textdir", CBParticipantes.SelectedValue)
            LLENAR_DROP2(40, "", cbPlantillas, "id", "nombre")
            DPFelimite.MinDate = Today
            LblError.Text = ""
        End If
    End Sub


End Class
