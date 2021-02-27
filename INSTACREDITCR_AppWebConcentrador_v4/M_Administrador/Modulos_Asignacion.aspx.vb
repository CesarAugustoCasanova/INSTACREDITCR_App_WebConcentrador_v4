
Imports System.Data
Imports Telerik.Web.UI

Partial Class Modulos_Asignacion
    Inherits System.Web.UI.Page
    Private Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        Response.Redirect("./Modulos_Acciones.aspx")
    End Sub

    Private Sub M_Administrador_Default_Init(sender As Object, e As EventArgs) Handles Me.Init
        If Session("ReglaID") Is Nothing Then
            Response.Redirect("./Modulos_Reglas.aspx")
        Else
            LoadUsuarios()
            SetLabels()
        End If
    End Sub

    Private Sub SetLabels()
        lblRegla.Text = Session("ReglaNombre")
        lblCuantas.Text = SP.MODULOS_ASIGNACION(bandera:=1, v_regla_id:=Session("ReglaID")).Rows(0)(0).ToString
    End Sub

    Private Sub LoadUsuarios()
        cbUsuarios.DataTextField = "NOMBRE"
        cbUsuarios.DataValueField = "USUARIO"
        cbUsuarios.DataSource = SP.MODULOS_ASIGNACION(bandera:=2)
        cbUsuarios.DataBind()
    End Sub

    Private Sub btnAsignar_Click(sender As Object, e As EventArgs) Handles btnAsignar.Click
        Dim usuarios As New List(Of String)
        Dim vigencia As String = dpVigencia.DbSelectedDate.ToString.Substring(0, 10)

        For Each usuario As RadComboBoxItem In cbUsuarios.CheckedItems
            usuarios.Add(usuario.Value)
        Next

        Dim resumen As DataTable = SP.MODULOS_ASIGNACION(bandera:=3, v_regla_id:=Session("ReglaID"), v_usuarios_asignados:=String.Join(",", usuarios), v_vigencia:=vigencia)
        If resumen.TableName = "Exception" Then
            Funciones.showModal(not1, "deny", "Error", "No se ha podido realizar la asignacion. Intente de nuevo más tarde.")
        Else
            gridResumen.Visible = True
            gridResumen.DataSource = resumen
            gridResumen.DataBind()
        End If
    End Sub
End Class
