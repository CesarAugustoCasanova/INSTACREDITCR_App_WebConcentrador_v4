Imports System.Data.SqlClient
Imports System.Data
Imports Db
Imports Funciones
Imports System.Web.Services
Imports Telerik.Web.UI

Partial Class Mantenimiento
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Llenar()
                pnlFilaTrabajo.DataBind()
            End If
        Catch ex As Exception
            EnviarCorreo("Gestion", "Mantenimiento.ascx", "Page_Load", ex, "", "", tmpUSUARIO("CAT_LO_USUARIO"))
        End Try
    End Sub

    Sub Llenar()
        Try
            Dim cuantasDia As DataTable = Class_Agenda.LlenarElementosAgenda("", tmpUSUARIO("CAT_LO_USUARIO"), 10)
            GvRDia.DataSource = cuantasDia
            Dim cuantasMes As DataTable = Class_Agenda.LlenarElementosAgenda("", tmpUSUARIO("CAT_LO_USUARIO"), 11)
            GvRmes.DataSource = cuantasMes
            GvAsignacion.Rebind()

            Dim DtsPromesasD As DataTable = Class_Agenda.LlenarElementosAgenda("", tmpUSUARIO("CAT_LO_USUARIO"), 12)

            LblPPPD.Text = DtsPromesasD.Rows(0).Item("Cuantas") & " Promesas De Pago"
            LblMtoPPPD.Text = DtsPromesasD.Rows(0).Item("Monto")

            Dim DtsPromesasM As DataTable = Class_Agenda.LlenarElementosAgenda("", tmpUSUARIO("CAT_LO_USUARIO"), 13)
            LblPPPM.Text = DtsPromesasM.Rows(0).Item("Cuantas") & " Promesas De Pago"
            LblMtoPPPM.Text = DtsPromesasM.Rows(0).Item("Monto")

            Dim i As Integer = 0
            Dim numfilasDia As Integer = cuantasDia.Rows.Count
            For index = 0 To numfilasDia - 1
                i = i + cuantasMes.Rows(index).Item("Cuantas")
            Next
            LblTrabajadas.Text = i

            i = 0
            Dim numfilasMes As Integer = cuantasMes.Rows.Count
            For index = 0 To numfilasMes - 1
                i = i + cuantasMes.Rows(index).Item("Cuantas")
            Next
            LblTrabajadasM.Text = i

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub DrlFila_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DrlFila.SelectedIndexChanged
        Try
            GridFilas.DataSource = Nothing
            GridFilas.DataBind()
            Dim Bandera As Integer

            If DrlFila.SelectedText <> "Seleccione" Then
                Select Case DrlFila.SelectedText
                    Case Is = "Código Resultado"
                        Bandera = 0
                    Case Is = "Código Resultado Ponderado"
                        Bandera = 1
                    Case Is = "Promesas de Pago"
                        Bandera = 2
                    Case Is = "Semaforo"
                        Bandera = 3
                    Case Is = "Contactar Hoy"
                        Bandera = 4
                End Select
                Dim DTSFila As DataTable = Class_Agenda.LlenarElementosAgenda("", tmpUSUARIO("CAT_LO_USUARIO"), Bandera)
                Dim DTVFila As DataView = DTSFila.DefaultView
                If DTVFila.Count > 0 Then
                    GridFilas.DataSource = DTSFila
                    GridFilas.DataBind()
                    BtnCrear.Visible = True
                    LblMsjFilas.Visible = False
                Else
                    GridFilas.DataSource = Nothing
                    GridFilas.DataBind()
                    BtnCrear.Visible = False
                    LblMsjFilas.Visible = True
                    LblMsjFilas.Text = "No Se Encontraron Resultados"
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Property tmpUSUARIO As IDictionary
        Get
            Return CType(Session("USUARIO"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("USUARIO") = value
        End Set
    End Property

    Public Property tmpCredito As IDictionary
        Get
            Return CType(Session("Credito"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("Credito") = value
        End Set
    End Property
    Public Property tmpPermisos As IDictionary
        Get
            Return CType(Session("Permisos"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("Permisos") = value
        End Set
    End Property
    Protected Sub GridFilas_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs)
        Try
            'GridFilas.DataSource = Class_InformacionAdicional.LlenarElementosAgregar(tmpCredito("PR_MC_CREDITO"), 1)
        Catch ex As Exception
            GridFilas.DataSource = Nothing
        End Try
    End Sub

    Protected Sub GvRDia_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs)
        Try
            'GvRDia.DataSource = Class_Agenda.LlenarElementosAgenda("", tmpUSUARIO("CAT_LO_USUARIO"), 10)
        Catch ex As Exception
            GvRDia.DataSource = Nothing
        End Try
    End Sub

    Protected Sub GvRmes_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs)
        Try
            'GvRmes.DataSource = Class_Agenda.LlenarElementosAgenda("", tmpUSUARIO("CAT_LO_USUARIO"), 11)
        Catch ex As Exception
            GvRmes.DataSource = Nothing
        End Try
    End Sub

    Protected Sub GvAsignacion_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs)
        Try

            GvAsignacion.DataSource = Class_Agenda.LlenarElementosAgenda("", tmpUSUARIO("CAT_LO_USUARIO"), 8)
        Catch ex As Exception
            GvAsignacion.DataSource = Nothing
        End Try
    End Sub

    Private Sub BtnCrear_Click(sender As Object, e As EventArgs) Handles BtnCrear.Click
        Dim Resultados As String = ""
        Dim BANDERA As Integer = 0
        Dim Cuantos As Integer = 0
        For Each item_v As GridItem In GridFilas.MasterTableView.Items
            Dim dataitem As GridDataItem = DirectCast(item_v, GridDataItem)
            Dim cell As TableCell = dataitem("ClientSelectColumn")
            Dim checkBox As CheckBox = DirectCast(cell.Controls(0), CheckBox)
            If checkBox.Checked Then
                BANDERA = BANDERA + 1
                Resultados = Resultados + dataitem.Cells(3).Text + ","
                Cuantos = Cuantos + dataitem.Cells(4).Text
            End If
        Next

        If Resultados <> "" Then
            Resultados = Resultados.Substring(0, Resultados.Length - 1)
        End If
        If BANDERA > 0 Then
            If Cuantos > 0 Then
                Try
                    Dim Cadena As String = ""
                    Dim SSCommand As New SqlCommand("SP_CREAR_FILA")
                    SSCommand.CommandType = CommandType.StoredProcedure
                    SSCommand.Parameters.Add("@V_USUARIO", SqlDbType.NVarChar).Value = tmpUSUARIO("CAT_LO_USUARIO")
                    Select Case DrlFila.SelectedText
                        Case Is = "Código Resultado"
                            SSCommand.Parameters.Add("@V_Cadena", SqlDbType.NVarChar).Value = Resultados.Substring(0, Len(Resultados) - 0)
                            SSCommand.Parameters.Add("@v_Bandera", SqlDbType.NVarChar).Value = 1
                        Case Is = "Promesas de Pago"
                            If Resultados.Contains("Promesas Incumplidas") Then
                                Cadena = Cadena + "SELECT HIST_PR_CREDITO FROM HIST_PROMESAS WHERE HIST_PR_USUARIO ='" & tmpUSUARIO("CAT_LO_USUARIO") & "' AND HIST_PR_ESTATUS ='Incumplida' AND TRUNC(HIST_PR_DTEPROMESA) < TRUNC(SYSDATE) And Hist_Pr_Credito Not In (Select Pr_Mc_Credito From Pr_Mc_Gral Where Pr_Mc_Modal=0 And Pr_Mc_Secfila != 0) UNION "
                            End If
                            If Resultados.Contains("Promesas Pendientes") Then
                                Cadena = Cadena + "SELECT HIST_PR_CREDITO FROM HIST_PROMESAS WHERE HIST_PR_USUARIO ='" & tmpUSUARIO("CAT_LO_USUARIO") & "' AND HIST_PR_ESTATUS ='Pendiente' AND TRUNC(HIST_PR_DTEPROMESA) >= TRUNC(SYSDATE) And Hist_Pr_Credito Not In (Select Pr_Mc_Credito From Pr_Mc_Gral Where Pr_Mc_Modal=0 And Pr_Mc_Secfila != 0) UNION "
                            End If
                            If Resultados.Contains("Promesas Vencen Hoy") Then
                                Cadena = Cadena + "SELECT HIST_PR_CREDITO FROM HIST_PROMESAS WHERE HIST_PR_USUARIO ='" & tmpUSUARIO("CAT_LO_USUARIO") & "' AND HIST_PR_ESTATUS ='Pendiente' AND TRUNC(HIST_PR_DTEPROMESA)=TRUNC(SYSDATE) And Hist_Pr_Credito Not In (Select Pr_Mc_Credito From Pr_Mc_Gral Where Pr_Mc_Modal=0 And Pr_Mc_Secfila != 0) UNION "
                            End If
                            If Resultados.Contains("Promesas Vencen Mañana") Then
                                Cadena = Cadena + "SELECT HIST_PR_CREDITO FROM HIST_PROMESAS WHERE HIST_PR_USUARIO ='" & tmpUSUARIO("CAT_LO_USUARIO") & "' AND HIST_PR_ESTATUS ='Pendiente' AND TRUNC(HIST_PR_DTEPROMESA)=TRUNC(SYSDATE + 1) And Hist_Pr_Credito Not In (Select Pr_Mc_Credito From Pr_Mc_Gral Where Pr_Mc_Modal=0 And Pr_Mc_Secfila != 0) UNION "
                            End If
                            If Resultados.Contains("Promesas Vencieron Ayer") Then
                                Cadena = Cadena + "SELECT HIST_PR_CREDITO FROM HIST_PROMESAS WHERE HIST_PR_USUARIO ='" & tmpUSUARIO("CAT_LO_USUARIO") & "' AND HIST_PR_ESTATUS ='Pendiente' AND TRUNC(HIST_PR_DTEPROMESA)=TRUNC(SYSDATE - 1) And Hist_Pr_Credito Not In (Select Pr_Mc_Credito From Pr_Mc_Gral Where Pr_Mc_Modal=0 And Pr_Mc_Secfila != 0) UNION "
                            Else
                                Cadena = Cadena + ""
                            End If
                            SSCommand.Parameters.Add("@V_Cadena", SqlDbType.NVarChar).Value = Cadena.Substring(0, Len(Cadena) - 7)
                            SSCommand.Parameters.Add("@v_Bandera", SqlDbType.NVarChar).Value = 2
                        Case Is = "Contactar Hoy"
                            SSCommand.Parameters.Add("@V_Cadena", SqlDbType.NVarChar).Value = ""
                            SSCommand.Parameters.Add("@v_Bandera", SqlDbType.NVarChar).Value = 3
                        Case Is = "Semaforo"
                            SSCommand.Parameters.Add("@V_Cadena", SqlDbType.NVarChar).Value = Resultados.Substring(0, Len(Resultados) - 0)
                            SSCommand.Parameters.Add("@v_Bandera", SqlDbType.NVarChar).Value = 4
                        Case Is = "Código Resultado Ponderado"
                            SSCommand.Parameters.Add("@V_Cadena", SqlDbType.NVarChar).Value = Resultados.Substring(0, Len(Resultados) - 0)
                            SSCommand.Parameters.Add("@v_Bandera", SqlDbType.NVarChar).Value = 5
                    End Select
                    Ejecuta_Procedure(SSCommand)
                    Session.Remove("LatestLoadedControlName")
                    showModal(Notificacion, "ok", "Exito", "Fila creada correctamente")
                    Response.Redirect("InformacionGeneral.aspx")
                Catch ex As System.Threading.ThreadAbortException
                Catch ex As Exception
                    LblMsjFilas.Visible = True
                    LblMsjFilas.Text = ex.ToString
                End Try
            Else
                showModal(Notificacion, "warning", "Aviso", "No Existen Créditos En La(s) Opcion(es) Seleccionada(s)")
            End If
        Else
            showModal(Notificacion, "warning", "Aviso", "Seleccione Una Opción Del Listado Para Crear La Fila De Trabajo")
        End If
    End Sub

End Class
