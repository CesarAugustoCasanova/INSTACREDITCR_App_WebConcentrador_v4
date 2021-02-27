
Imports System.Data
Imports Telerik.Web.UI
Imports Telerik.Web.UI.Upload
Imports Modulos_Campana_SMS_Romber
Partial Class M_Administrador_Modulos_Campana_SMS
    Inherits System.Web.UI.UserControl

    Private Sub M_Administrador_Modulos_Campana_SMS_Load(sender As Object, e As EventArgs) Handles Me.Load
        lblReglaNombre.Text = Session("ReglaNombre")
        Try
            lblCuantasCuentas.Text = SP.MODULOS_CAMAPANA_SMS(0, Session("ReglaID")).Rows(0)(0).ToString
        Catch ex As Exception
            lblCuantasCuentas.Text = 0
        End Try

        ddlPlantillas.DataSource = SP.MODULOS_CAMAPANA_SMS(1)
        ddlPlantillas.DataTextField = "TEXTO"
        ddlPlantillas.DataValueField = "VALOR"
        ddlPlantillas.DataBind()
    End Sub

    Private Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click

        'Obtenemos el texto de la plantilla
        Dim plantillaBase As String = SP.MODULOS_CAMAPANA_SMS(bandera:=4, plantilla_id:=ddlPlantillas.SelectedText).Rows(0)(0).ToString

        'Rescatamos todas las etiquetas presentes en la plantilla
        Dim etiquetas() As String = EliminarEtiquetasRepetidas(getAllEtiquetas(plantillaBase)).Split(",")

        'Obtenemos los datos del cliente
        Dim DatosCliente As DataTable = SP.MODULOS_CAMAPANA_SMS(3, Session("ReglaID"))

        Dim Total As Integer = DatosCliente.Rows.Count

        Dim progress As RadProgressContext = RadProgressContext.Current
        progress.Speed = "N/A"

        'Preparamos la tabla para guardar los errores
        Dim dtResultados As New DataTable("RESULTADOS") 'Se guardan los creditos que tuvieron error al enviarse
        dtResultados.Columns.Add("Estatus")
        dtResultados.Columns.Add("Credito")
        dtResultados.Columns.Add("Telefono")
        dtResultados.Columns.Add("Mensaje")
        'Funcion anonima para facilitar el insertar filas
        Dim InsertRow = Sub(estatus As String, credito As String, telefono As String, mensaje As String)
                            Dim row As DataRow = dtResultados.NewRow()
                            row("Estatus") = estatus
                            row("Credito") = credito
                            row("Telefono") = telefono
                            row("Mensaje") = mensaje
                            dtResultados.Rows.Add(row)
                        End Sub

        Dim plantillaConDatos As String = ""

        Dim value As Integer = 1
        'enviaSMS(v_username:="5555199553", v_password:="5555199553", v_number:="5551643219", v_message:="Buenas Tardes", v_idmessage:="20200518185300")

        'Recorremos la lista de cuentas
        For Each cliente As DataRow In DatosCliente.Rows

            If Not Response.IsClientConnected Then
                'Cancel button was clicked or the browser was closed, so stop processing
                Exit For
            End If

            progress.CurrentOperationText = "Sending SMS to '" & cliente("CREDITO").ToString & "'"
            progress.PrimaryTotal = Total
            progress.PrimaryValue = value
            progress.PrimaryPercent = value * 100 / Total

            plantillaConDatos = plantillaBase
            Dim resultado As String = "ok"
            progress.SecondaryPercent = 0
            If IsNumeric(cliente("TELEFONO").ToString) Then
                'Para cada etiqueta traemos, extraemos sus datos y los sustituimos en la plantilla
                For Each etiqueta In etiquetas

                    Dim valor As String = SP.MODULOS_CAMAPANA_SMS(bandera:=5, v_credito:=cliente("CREDITO").ToString, v_etiqueta:=etiqueta).Rows(0)(0).ToString
                    plantillaConDatos = plantillaConDatos.Replace("[[" & etiqueta & "]]", valor)
                Next
                Dim strIdSMS As String = Date.Now.ToString("yyyyMMddHHmmss") & "_" & cliente("CREDITO").ToString

                'Enviar el mensaje
                enviaSMS(v_username:="5555199553", v_password:="5555199553", v_number:=cliente("TELEFONO").ToString, v_message:=plantillaConDatos, v_idmessage:=strIdSMS)
            Else
                resultado = "Formato incorrecto"
            End If
            InsertRow(resultado, cliente("CREDITO").ToString, cliente("TELEFONO").ToString, plantillaConDatos)
            value += 1
        Next

        pnlResultados.Visible = True
        Session("dtResultados") = dtResultados
        gridResultados.Rebind()
    End Sub

    Private Sub gridResultados_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles gridResultados.NeedDataSource
        gridResultados.DataSource = Session("dtResultados")
    End Sub

    Private Function getAllEtiquetas(data As String) As String
        Dim etiquetas As String = ""
        Dim chars() As Char = data.ToCharArray
        Dim etiquetaComienza As Integer = 0
        Dim etiquetaTermina As Integer = 0
        For Each caracter As Char In chars
            Select Case caracter
                Case "["
                    etiquetaComienza += 1
                    If etiquetaComienza > 2 Then
                        etiquetaComienza -= 1
                    End If
                Case "]"
                    etiquetaTermina += 1
                    If etiquetaTermina = 2 Then
                        etiquetaTermina = 0
                        etiquetaComienza = 0
                        etiquetas &= ","
                    End If
                Case Else
                    If etiquetaTermina <> 2 Then
                        etiquetaTermina = 0
                    End If

                    If etiquetaComienza <> 2 Then
                        etiquetaComienza = 0
                    Else
                        etiquetas &= caracter
                    End If
            End Select
        Next
        Try
            etiquetas = etiquetas.Substring(0, etiquetas.Length - 1)
        Catch ex As Exception
        End Try
        Return etiquetas
    End Function

    Private Function EliminarEtiquetasRepetidas(data As String) As String
        Dim filtro As New List(Of String)
        For Each etiqueta As String In data.Split(",")
            If Not filtro.Contains(etiqueta) Then
                filtro.Add(etiqueta)
            End If
        Next
        Return String.Join(",", filtro)
    End Function
End Class
