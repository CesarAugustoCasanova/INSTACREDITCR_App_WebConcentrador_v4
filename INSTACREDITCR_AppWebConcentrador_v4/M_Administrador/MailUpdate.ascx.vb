Imports System.Data
Imports System.Data.SqlClient
Imports Db
Imports Telerik.Web.UI
Partial Class M_Administrador_MailUpdate
    Inherits System.Web.UI.UserControl
    Private _dataItem As Object = Nothing

    Protected Sub Aviso1(ByVal MSJ As String)
        RadAviso1.RadAlert(MSJ.Replace(Chr(13), "").Replace(Chr(10), "").Replace("""", "").Replace("'", ""), 440, 155, "AVISO", Nothing)
    End Sub
    Private Sub M_Administrador_PruebaMailUpdate_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim mensaje3 As String = Convert.ToString(Session("nombre"))
        txtUsuario.Text = mensaje3
    End Sub




    Private Sub btnEnviar_Command(sender As Object, e As CommandEventArgs) Handles btnEnviar.Command

        'Dim oraCommanVarios As New SqlCommand("SP_EMAILS")
        '    oraCommanVarios.CommandType = CommandType.StoredProcedure
        '    oraCommanVarios.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 6
        '    oraCommanVarios.Parameters.Add("@MAIL_NOMBRE", SqlDbType.NVarChar).Value = txtUsuario.Text
        '    oraCommanVarios.Parameters.Add("@MAIL_DESTINO", SqlDbType.NVarChar).Value = txtCorreo.Text
        '    oraCommanVarios.Parameters.Add("@MAIL_MENSAJE", SqlDbType.NVarChar).Value = txtdescripcion.Text
        '    oraCommanVarios.Parameters.Add("@MAIL_DESCRIPCION", SqlDbType.NVarChar).Value = txtAsunto.Text

        '    Try
        '    ' Dim DtsVarios As DataTable = Consulta_Procedure(oraCommanVarios, "MAILS")

        '    'If DtsVarios.TableName = "Exception" Then
        '    '        Aviso1(DtsVarios.Rows(0).Item(0).ToString)
        '    '    Else
        '    '        Aviso1("SU CORREO SE ENVIO CORRECTAMENTE")
        '    '    End If


        'Catch ex As Exception
        '        Dim V_ERRRO As String = ex.Message
        '        Aviso1("ERROR AL ENVIAR CORREO INVALIDO O CUENTA NO EXISTE ")
        '    End Try


        'Select Case e.CommandName
        '    Case "Enviar"
        'Dim valores(10) As String
        'Dim lol As List(Of String) = New List(Of String)
        'lol.Add(txtUsuario.Text)



        'End Select

    End Sub

    Private Sub M_Administrador_PruebaMailUpdate_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        If Session Is Nothing Then


        Else


            Dim mensaje3 As String = Convert.ToString(Session("nombre"))



            txtUsuario.Text = mensaje3




        End If
    End Sub
End Class
