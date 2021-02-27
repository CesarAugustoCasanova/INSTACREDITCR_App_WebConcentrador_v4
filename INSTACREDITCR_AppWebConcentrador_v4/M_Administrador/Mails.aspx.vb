Imports System.Web.Services
Imports Class_Mail
'Imports System.Windows.Forms
Imports Telerik.Web.UI.PivotGrid.Core.Totals
Imports System.Web.UI.Control
Imports System.Data
Imports System.Data.SqlClient
Imports Db
Imports Funciones
Imports System.IO
Imports Telerik.Web.UI
Imports Spire.Xls

Partial Class Mails
    Inherits System.Web.UI.Page



    Public Property TmpUSUARIO As IDictionary
        Get
            Return CType(Session("MAILS"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("MAILS") = value
        End Set
    End Property

    Protected Sub Aviso(ByVal MSJ As String)
        RadAviso.RadAlert(MSJ, 440, 155, "AVISO", Nothing)
    End Sub

    Public Sub gridMails_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles gridMails.NeedDataSource
        Dim SSCommandUsuario As New SqlCommand
        SSCommandUsuario.CommandText = "SP_EMAILS"
        gridMails.DataSource = SP.EMAILS(bandera:=0)

    End Sub

    Public Sub gridMails_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles gridMails.ItemCommand
        Dim comando As String = e.CommandName
        Dim valores(100) As String
        Try
            valores(0) = e.Item.Cells.Item(4).Text
            valores(1) = e.Item.Cells.Item(5).Text
            valores(2) = e.Item.Cells.Item(6).Text
            valores(3) = e.Item.Cells.Item(7).Text
            valores(4) = e.Item.Cells.Item(8).Text
            valores(5) = e.Item.Cells.Item(9).Text
            valores(6) = e.Item.Cells.Item(10).Text
            valores(7) = e.Item.Cells.Item(11).Text
            valores(8) = e.Item.Cells.Item(12).Text

        Catch

        End Try


        Session("id") = Convert.ToInt32(valores(0))
        Session("nombre") = valores(1)
        Session("descripcion") = valores(2)
        Session("cuenta") = valores(3)
        Session("correo") = valores(5)
        Session("smtp") = valores(6)
        Session("port") = valores(7)


        If comando = "Delete" Then
            BorrarPerfil(valores(0), valores(3))
            Aviso("SE EILIMINO CORRECTAMENTE ")
        End If










    End Sub


    Private Sub GridEnvio_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles GridEnvio.NeedDataSource
        Dim SSCommandUsuario As New SqlCommand
        SSCommandUsuario.CommandText = "SP_EMAILS"
        GridEnvio.DataSource = SP.EMAILS(bandera:=5)
    End Sub

    Private Sub GridEnvio_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles GridEnvio.ItemCommand
        Dim comando As String = e.CommandName
        Dim valores(100) As String
        If comando = "Edit" Then
            Try
                valores(0) = e.Item.Cells.Item(3).Text
                valores(1) = e.Item.Cells.Item(5).Text
                valores(2) = e.Item.Cells.Item(6).Text
                valores(3) = e.Item.Cells.Item(7).Text
            Catch




            End Try

            Session("nombre") = valores(0)
            Session("descripcion") = valores(2)
            Session("cuenta") = valores(3)
            Session("correo") = valores(5)
            Session("smtp") = valores(6)
            Session("port") = valores(7)



        ElseIf comando = "Update" Then

            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores2 As Hashtable = GetGridValues(MyUserControl)
            Dim resultado As String = Validar(valores2, False)
            If resultado = "OK" Then
                Dim oraCommanVarios As New SqlCommand("SP_EMAILS")
                oraCommanVarios.CommandType = CommandType.StoredProcedure
                oraCommanVarios.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 6
                oraCommanVarios.Parameters.Add("@MAIL_NOMBRE", SqlDbType.NVarChar).Value = valores2("usuario")
                oraCommanVarios.Parameters.Add("@MAIL_DESTINO", SqlDbType.NVarChar).Value = valores2("correo")
                oraCommanVarios.Parameters.Add("@MAIL_MENSAJE", SqlDbType.NVarChar).Value = valores2("mensaje")
                oraCommanVarios.Parameters.Add("@MAIL_DESCRIPCION", SqlDbType.NVarChar).Value = valores2("asunto")

                Try
                    Dim DtsVarios As DataTable = Consulta_Procedure(oraCommanVarios, "MAILS")

                    If DtsVarios.TableName = "Exception" Then
                        Aviso(DtsVarios.Rows(0).Item(0).ToString)
                    Else
                        Aviso("Correo enviado correctamente")
                    End If


                Catch ex As Exception
                    Dim V_ERRRO As String = ex.Message
                    Aviso("")
                End Try


            Else
                e.Canceled = True
                Aviso(resultado)
            End If
        End If
    End Sub
    Public Shared Function GetGridValues(usrControl As UserControl) As Hashtable
        Dim valores As New Hashtable
        Dim V_Multiple As String = 0

        valores.Add("correo", CType(usrControl.FindControl("txtCorreo"), RadTextBox).Text)
        valores.Add("usuario", CType(usrControl.FindControl("txtUsuario"), RadTextBox).Text)
        valores.Add("mensaje", CType(usrControl.FindControl("txtdescripcion"), RadTextBox).Text)
        valores.Add("asunto", CType(usrControl.FindControl("txtAsunto"), RadTextBox).Text)


        Return valores
    End Function
    Private Function Validar(valores As Hashtable, isInsert As Boolean) As String
        Dim mensaje As String = ""

        If String.IsNullOrEmpty(valores("usuario")) Or String.IsNullOrWhiteSpace(valores("usuario")) Then
            mensaje = "Capture un usuario válido"
        ElseIf String.IsNullOrEmpty(valores("correo")) Or String.IsNullOrWhiteSpace(valores("correo")) Then
            mensaje = "Capture un correo válido"
        ElseIf String.IsNullOrEmpty(valores("mensaje")) Or String.IsNullOrWhiteSpace(valores("mensaje")) Then
            mensaje = "Capture un mensaje válido"
        ElseIf Not IsEmail(valores("correo")) Then
            mensaje = "Capture un correo válido"
        ElseIf String.IsNullOrEmpty(valores("asunto")) Or String.IsNullOrWhiteSpace(valores("asunto")) Then
            mensaje = "Capture un asunto válido"
        Else
            mensaje = "OK"
        End If
        Return mensaje
    End Function
    Function IsEmail(ByVal email As String) As Boolean
        Static emailExpression As New Regex("^[_a-z0-9-]+(.[a-z0-9-]+)@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$")

        Return emailExpression.IsMatch(email)
    End Function
End Class