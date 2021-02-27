Imports System.Data
Imports System.Data.SqlClient
Imports Db
Imports Telerik.Web.UI
Partial Class M_Administrador_ModuloMails
    Inherits System.Web.UI.UserControl


    'Dim myNewArrList As ArrayList
    Protected Sub Aviso(ByVal MSJ As String)
        RadAviso.RadAlert(MSJ, 440, 155, "AVISO", Nothing)
    End Sub



    Private Sub btnGuardar_Command(sender As Object, e As CommandEventArgs) Handles btnGuardar.Command
        Select Case e.CommandName
            Case "PerformInsert"

                Dim oraCommanVarios As New SqlCommand("SP_EMAILS")

                oraCommanVarios.CommandType = CommandType.StoredProcedure
                oraCommanVarios.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 3
                oraCommanVarios.Parameters.Add("@MAIL_NCUENTA", SqlDbType.NVarChar).Value = "Credi" + txtUsuario.Text
                oraCommanVarios.Parameters.Add("@MAIL_DESCRIPCION", SqlDbType.NVarChar).Value = txtdescripcion.Text
                oraCommanVarios.Parameters.Add("@MAIL_CORREO", SqlDbType.NVarChar).Value = txtCorreo.Text
                oraCommanVarios.Parameters.Add("@MAIL_NOMBRE", SqlDbType.NVarChar).Value = txtNombre.Text
                oraCommanVarios.Parameters.Add("@MAIL_PASS", SqlDbType.NVarChar).Value = txtContrasena.Text
                oraCommanVarios.Parameters.Add("@MAIL_NSERVER", SqlDbType.NVarChar).Value = txtnserver.Text
                oraCommanVarios.Parameters.Add("@MAIL_PORT", SqlDbType.NVarChar).Value = txtport.Text


                Try
                    If txtdescripcion.Text = "" Then
                        Aviso("FALTAN CAMPOS MARCADOS CON * , SON IMPORTANTES")
                        'e.CommandName = "Cancel"
                    Else

                        Dim DtsVarios As DataTable = Consulta_Procedure(oraCommanVarios, "MAILS")

                        Aviso("PERFIL Y CUENTA CREADOS CORRECTAMENTE")


                    End If
                Catch ex As Exception
                    Dim V_ERRRO As String = ex.Message
                    Aviso("EL PERFIL YA EXISTE")
                End Try
                Return
            Case "Update"


                Dim oraCommanVarios As New SqlCommand("SP_EMAILS")

                oraCommanVarios.CommandType = CommandType.StoredProcedure
                oraCommanVarios.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 1
                oraCommanVarios.Parameters.Add("@MAIL_ID", SqlDbType.NVarChar).Value = txtid.Text
                oraCommanVarios.Parameters.Add("@MAIL_NCUENTA", SqlDbType.NVarChar).Value = txtUsuario.Text
                oraCommanVarios.Parameters.Add("@MAIL_DESCRIPCION", SqlDbType.NVarChar).Value = txtdescripcion.Text
                oraCommanVarios.Parameters.Add("@MAIL_CORREO", SqlDbType.NVarChar).Value = txtCorreo.Text
                oraCommanVarios.Parameters.Add("@MAIL_NOMBRE", SqlDbType.NVarChar).Value = txtNombre.Text
                oraCommanVarios.Parameters.Add("@MAIL_PASS", SqlDbType.NVarChar).Value = txtContrasena.Text
                oraCommanVarios.Parameters.Add("@MAIL_NSERVER", SqlDbType.NVarChar).Value = txtnserver.Text
                oraCommanVarios.Parameters.Add("@MAIL_PORT", SqlDbType.NVarChar).Value = txtport.Text

                Try
                    If txtdescripcion Is Nothing Then
                        Aviso("FALTAN CAMPOS MARCADOS CON * , SON IMPORTANTES")
                    Else

                        Dim DtsVarios As DataTable = Consulta_Procedure(oraCommanVarios, "MAILS")

                        Aviso("SUS DATOS SE ACTUALIZARON CORRECTAMENTE")


                    End If



                Catch ex As Exception
                    Dim V_ERRRO As String = ex.Message
                    Aviso("FALTA INFORMACIÓN")
                End Try

        End Select
        '    Case "Update"

        '        Dim oraCommanVarios As New SqlCommand("SP_EMAILS")


        '        Dim valores(10) As String
        '        Dim lol As List(Of String) = New List(Of String)

        '        lol.Add(txtUsuario.Text)
        '        lol.Add(txtdescripcion.Text)
        '        lol.Add(txtCorreo.Text)
        '        lol.Add(txtNombre.Text)
        '        lol.Add(txtContrasena.Text)
        '        lol.Add(txtnserver.Text)
        '        lol.Add(txtport.Text)

        '        oraCommanVarios.CommandType = CommandType.StoredProcedure
        '        oraCommanVarios.Parameters.Add("@BAN", SqlDbType.Decimal).Value = 1
        '        oraCommanVarios.Parameters.Add("@MAIL_IID", SqlDbType.NVarChar).Value = txtid.Text
        '        oraCommanVarios.Parameters.Add("@MAIL_NCUENTA", SqlDbType.NVarChar).Value = lol.Item(0)
        '        oraCommanVarios.Parameters.Add("@MAIL_DESCRIPCION", SqlDbType.NVarChar).Value = lol.Item(1)
        '        oraCommanVarios.Parameters.Add("@MAIL_CORREO", SqlDbType.NVarChar).Value = lol.Item(2)
        '        oraCommanVarios.Parameters.Add("@MAIL_NOMBRE", SqlDbType.NVarChar).Value = lol.Item(3)
        '        oraCommanVarios.Parameters.Add("@MAIL_PASS", SqlDbType.NVarChar).Value = txtContrasena.Text
        '        oraCommanVarios.Parameters.Add("@MAIL_NSERVER", SqlDbType.NVarChar).Value = lol.Item(5)
        '        oraCommanVarios.Parameters.Add("@MAIL_POR", SqlDbType.NVarChar).Value = lol.Item(6)

        '        Try
        '            Dim DtsVarios As DataTable = Consulta_Procedure(oraCommanVarios, "MAILS")

        '            Dim algo As String



        '        Catch ex As Exception
        '            Dim V_ERRRO As String = ex.Message
        '        End Try
        'End Select
    End Sub

    Private Sub M_Administrador_PruebaModuloMails_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender

        If Session Is Nothing Then


        Else


            Dim mensaje As String = Session("id")
            Dim mensaje1 As String = Convert.ToString(Session("nombre"))
            Dim mensaje2 As String = Convert.ToString(Session("descripcion"))
            Dim mensaje3 As String = Convert.ToString(Session("cuenta"))
            Dim mensaje4 As String = Convert.ToString(Session("correo"))
            Dim mensaje5 As String = Convert.ToString(Session("smtp"))
            Dim mensaje6 As String = Session("port")

            txtid.Text = mensaje
            txtNombre.Text = mensaje1
            txtdescripcion.Text = mensaje2
            txtUsuario.Text = mensaje3
            txtCorreo.Text = mensaje4
            txtnserver.Text = mensaje5
            txtport.Text = mensaje6


        End If
    End Sub

    'Private Sub M_Administrador_PruebaModuloMails_Load(sender As Object, e As EventArgs) Handles Me.Load
    '    'Dim mensaje As String = Session("id")
    '    'Dim mensaje1 As String = Convert.ToString(Session("nombre"))
    '    'Dim mensaje2 As String = Convert.ToString(Session("descripcion"))
    '    'Dim mensaje3 As String = Convert.ToString(Session("cuenta"))
    '    'Dim mensaje4 As String = Convert.ToString(Session("correo"))
    '    'Dim mensaje5 As String = Convert.ToString(Session("smtp"))
    '    'Dim mensaje6 As String = Session("port")

    '    'txtid.Text = mensaje
    '    'txtNombre.Text = mensaje1
    '    'txtdescripcion.Text = mensaje2
    '    'txtUsuario.Text = mensaje3
    '    'txtCorreo.Text = mensaje4
    '    'txtnserver.Text = mensaje5
    '    'txtport.Text = mensaje6

    'End Sub
    ' When you get to your next page you just create a new ArrayList object and set it like this.


    ' Private Sub M_Administrador_PruebaModuloMails_Load(sender As Object, e As EventArgs) Handles Me.Load




    'Then to pull out your values you just do this.

    'Dim Datos As String = Convert.ToString(Session("Datos"))
    '  IIf(Session("MyArrayList") Is DBNull.Value, "", Convert.ToString(Session("MyArrayList")))
    'Dim id As String = Datos(0).ToString
    'Dim nn As String = Datos(1).ToString
    'Dim des As String = Datos(2).ToString
    'Dim usu As String = Datos(3).ToString
    'Dim cor As String = Datos(5).ToString
    'Dim ser As String = Datos(6).ToString
    'Dim por As String = Datos(7).ToString

    'txtid.Text = id
    'txtNombre.Text = nn
    'txtdescripcion.Text = des
    'txtUsuario.Text = usu
    'txtCorreo.Text = cor
    'txtnserver.Text = ser
    'txtport.Text = por





    'Context.Session("Id") = txtid.Text
    'Context.Session("Usuario") = txtUsuario.Text
    'Context.Session("Descripcion") = txtdescripcion.Text
    'Context.Session("Cuenta") = txtCorreo.Text
    'Context.Session("Nombre") = txtNombre.Text
    'Context.Session("SMTP") = txtnserver.Text
    'Context.Session("Puerto") = txtport.Text



    'Dim oraCommanVarios As New SqlCommand("SP_EMAILS")

    'oraCommanVarios.CommandType = CommandType.StoredProcedure
    'oraCommanVarios.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 5
    'oraCommanVarios.Parameters.Add("@MAIL_ID", SqlDbType.NVarChar).Value = Session("Id")
    'oraCommanVarios.Parameters.Add("@MAIL_NCUENTA", SqlDbType.NVarChar).Value = Session(1) = txtUsuario.Text
    'oraCommanVarios.Parameters.Add("@MAIL_DESCRIPCION", SqlDbType.NVarChar).Value = Session("DATOS") = txtdescripcion.Text
    'oraCommanVarios.Parameters.Add("@MAIL_CORREO", SqlDbType.NVarChar).Value = Session(3) = txtCorreo.Text
    'oraCommanVarios.Parameters.Add("@MAIL_NOMBRE", SqlDbType.NVarChar).Value = Session(4) = txtNombre.Text
    'oraCommanVarios.Parameters.Add("@MAIL_PASS", SqlDbType.NVarChar).Value = txtContrasena.Text
    'oraCommanVarios.Parameters.Add("@MAIL_NSERVER", SqlDbType.NVarChar).Value = Session(5) = txtnserver.Text
    'oraCommanVarios.Parameters.Add("@MAIL_PORT", SqlDbType.NVarChar).Value = Session(6) = txtport.Text

    'Try
    '    Dim DtsVarios As DataTable = Consulta_Procedure(oraCommanVarios, "MAILS")





    'Catch ex As Exception
    '    Dim V_ERRRO As String = ex.Message
    'End Try


    'End Sub
End Class



'id perfil
'nombre
'descrip
'ultima modificacion perfil y cuenta 
'correo
'id de la cuenta