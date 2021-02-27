Imports Funciones
Imports Db
Imports Conexiones
Imports System.Data
Imports System.Data.SqlClient

Partial Class Modulos
    Inherits System.Web.UI.Page
    Public Property tmpPermisos As IDictionary
        Get
            Return CType(Session("Permisos"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("Permisos") = value
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

    Private Sub Modulos_Init(sender As Object, e As EventArgs) Handles Me.Init
        If Not IsPostBack Then
            If Session("USUARIO") Is Nothing Then
                Response.Redirect("~/Login.aspx")
            Else
                identificarPermisos()
            End If
        End If
    End Sub

    Private Sub Modulos_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        pnlModulos.DataBind()
    End Sub

    Public Function saludo() As String
        If Now.Hour >= 0 And Now.Hour < 12 Then
            Return "Buenos días"
        ElseIf Now.Hour >= 12 And Now.Hour < 19 Then
            Return "Buenas tardes"
        Else
            Return "Buenas noches"
        End If

    End Function
    Protected Sub imgAd_Click(sender As Object, e As ImageClickEventArgs)
        Try
            Dim sessionactive As Integer = GetSessionActive.Search(tmpUSUARIO("CAT_LO_USUARIO"), 1)
            If sessionactive = 0 Then

                Session.Clear()
                Session.Abandon()
                Response.Redirect("~/SesionExpirada.aspx")
            Else
                Session.Remove("Permisos")
                cargarPermisos(2)
                SP.AUDITORIA_GLOBAL(0, tmpUSUARIO("CAT_LO_USUARIO"), "Modulo Administrador", "Ingreso")
                Response.Redirect("~/M_Administrador/Inicio.aspx")
            End If

        Catch ex As System.Threading.ThreadAbortException

        Catch ex As Exception
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/SesionExpirada.aspx")
        End Try

    End Sub

    Protected Sub imgBo_Click(sender As Object, e As ImageClickEventArgs)
        Try
            Dim sessionactive As Integer = GetSessionActive.Search(tmpUSUARIO("CAT_LO_USUARIO"), 1)
            If sessionactive = 0 Then

                Session.Clear()
                Session.Abandon()
                Response.Redirect("~/SesionExpirada.aspx")
            Else
                Session.Remove("Permisos")
                cargarPermisos(4)
                SP.AUDITORIA_GLOBAL(0, tmpUSUARIO("CAT_LO_USUARIO"), "Modulo Monitoreo", "Ingreso")
                Response.Redirect("~/M_Monitoreo/Inicio.aspx")
            End If

        Catch ex As System.Threading.ThreadAbortException

        Catch ex As Exception
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/SesionExpirada.aspx")
        End Try

    End Sub
    Protected Sub imgRe_Click(sender As Object, e As ImageClickEventArgs)
        Try
            Dim sessionactive As Integer = GetSessionActive.Search(tmpUSUARIO("CAT_LO_USUARIO"), 1)
            If sessionactive = 0 Then

                Session.Clear()
                Session.Abandon()
                Response.Redirect("~/SesionExpirada.aspx")
            Else
                Session.Remove("Permisos")
                cargarPermisos(3)
                SP.AUDITORIA_GLOBAL(0, tmpUSUARIO("CAT_LO_USUARIO"), "Modulo Reportes", "Ingreso")
                Response.Redirect("~/M_Reportes/Inicio.aspx")
            End If

        Catch ex As System.Threading.ThreadAbortException

        Catch ex As Exception
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/SesionExpirada.aspx")
        End Try

    End Sub
    Protected Sub imgLe_Click(sender As Object, e As ImageClickEventArgs)
        Try
            Dim sessionactive As Integer = GetSessionActive.Search(tmpUSUARIO("CAT_LO_USUARIO"), 1)
            If sessionactive = 0 Then

                Session.Clear()
                Session.Abandon()
                Response.Redirect("~/SesionExpirada.aspx")
            Else
                Session.Remove("Permisos")
                cargarPermisos(6)
                SP.AUDITORIA_GLOBAL(0, tmpUSUARIO("CAT_LO_USUARIO"), "Modulo Judicial", "Ingreso")
                Response.Redirect("~/M_Judicial/Inicio.aspx")
            End If

        Catch ex As System.Threading.ThreadAbortException

        Catch ex As Exception
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/SesionExpirada.aspx")
        End Try

    End Sub
    Protected Sub imgGe_Click(sender As Object, e As ImageClickEventArgs)
        Try
            Dim sessionactive As Integer = GetSessionActive.Search(tmpUSUARIO("CAT_LO_USUARIO"), 1)
            If sessionactive = 0 Then

                Session.Clear()
                Session.Abandon()
                Response.Redirect("~/SesionExpirada.aspx")
            Else
                Session.Remove("Permisos")
                cargarPermisos(1)
                SP.AUDITORIA_GLOBAL(0, tmpUSUARIO("CAT_LO_USUARIO"), "Modulo Gestion", "Ingreso")
                Response.Redirect("~/M_Gestion/MasterPage.aspx")
            End If

        Catch ex As System.Threading.ThreadAbortException

        Catch ex As Exception
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/SesionExpirada.aspx")
        End Try

    End Sub

    Protected Sub identificarPermisos()
        Session.Remove("ModuloUnico")
        If tmpUSUARIO("CAT_PE_PERFIL") = "Default" Then
            'Si solo tiene permisos para gestion, se manda gestión automaticamente
            LBLSinPermisos.Visible = True
        ElseIf (tmpUSUARIO("CAT_PE_A_GESTION").ToString.Contains("1") And
        tmpUSUARIO("CAT_PE_A_ADMINISTRADOR").ToString = "0" And
        tmpUSUARIO("CAT_PE_A_BACKOFFICE").ToString = "0" And
        tmpUSUARIO("CAT_PE_A_JUDICIAL").ToString = "0" And
        tmpUSUARIO("CAT_PE_A_REPORTES").ToString = "0"
        ) Then
            Session.Remove("Permisos")
            cargarPermisos(1)
            Session("ModuloUnico") = True
            SP.AUDITORIA_GLOBAL(0, tmpUSUARIO("CAT_LO_USUARIO"), "Modulo Gestion", "Ingreso")
            Response.Redirect("M_Gestion/MasterPage.aspx")

            'Si solo tiene permisos para administrador, se manda administrador automaticamente
        ElseIf (Not tmpUSUARIO("CAT_PE_A_GESTION").ToString.Contains("1") And
            tmpUSUARIO("CAT_PE_A_ADMINISTRADOR").ToString = "1" And
            tmpUSUARIO("CAT_PE_A_BACKOFFICE").ToString = "0" And
            tmpUSUARIO("CAT_PE_A_JUDICIAL").ToString = "0" And
            tmpUSUARIO("CAT_PE_A_REPORTES").ToString = "0"
            ) Then
            Session.Remove("Permisos")
            cargarPermisos(2)
            Session("ModuloUnico") = True
            SP.AUDITORIA_GLOBAL(0, tmpUSUARIO("CAT_LO_USUARIO"), "Modulo Administrador", "Ingreso")
            Response.Redirect("M_Administrador/inicio.aspx")

            'Si solo tiene permisos para backoffice, se manda backoffice automaticamente
        ElseIf (Not tmpUSUARIO("CAT_PE_A_GESTION").ToString.Contains("1") And
            tmpUSUARIO("CAT_PE_A_ADMINISTRADOR").ToString = "0" And
            tmpUSUARIO("CAT_PE_A_BACKOFFICE").ToString = "1" And
            tmpUSUARIO("CAT_PE_A_JUDICIAL").ToString = "0" And
            tmpUSUARIO("CAT_PE_A_REPORTES").ToString = "0"
            ) Then
            Session.Remove("Permisos")
            cargarPermisos(4)
            Session("ModuloUnico") = True
            'Response.Redirect("M_Gestion/MasterPage.aspx")

            'Si solo tiene permisos para judicial, se manda judicial automaticamente
        ElseIf (Not tmpUSUARIO("CAT_PE_A_GESTION").ToString.Contains("1") And
            tmpUSUARIO("CAT_PE_A_ADMINISTRADOR").ToString = "0" And
            tmpUSUARIO("CAT_PE_A_BACKOFFICE").ToString = "0" And
            tmpUSUARIO("CAT_PE_A_JUDICIAL").ToString = "1" And
            tmpUSUARIO("CAT_PE_A_REPORTES").ToString = "0"
            ) Then
            Session.Remove("Permisos")
            cargarPermisos(6)
            Session("ModuloUnico") = True
            SP.AUDITORIA_GLOBAL(0, tmpUSUARIO("CAT_LO_USUARIO"), "Modulo Judicial", "Ingreso")
            Response.Redirect("M_Judicial/Inicio.aspx")

            'Si solo tiene permisos para reportes, se manda gestión reportes
        ElseIf (Not tmpUSUARIO("CAT_PE_A_GESTION").ToString.Contains("1") And
            tmpUSUARIO("CAT_PE_A_ADMINISTRADOR").ToString = "0" And
            tmpUSUARIO("CAT_PE_A_BACKOFFICE").ToString = "0" And
            tmpUSUARIO("CAT_PE_A_JUDICIAL").ToString = "0" And
            tmpUSUARIO("CAT_PE_A_REPORTES").ToString = "1"
            ) Then
            Session.Remove("Permisos")
            cargarPermisos(3)
            Session("ModuloUnico") = True
            SP.AUDITORIA_GLOBAL(0, tmpUSUARIO("CAT_LO_USUARIO"), "Modulo Reportes", "Ingreso")
            Response.Redirect("M_Reportes/Inicio.aspx")
        Else
            Session("ModuloUnico") = False
        End If

    End Sub

    Private Sub btnCerrarSesion_Click(sender As Object, e As EventArgs) Handles btnCerrarSesion.Click
        Try
            CerrarSesion.cerrar(tmpUSUARIO("CAT_LO_USUARIO"))
            SP.AUDITORIA_GLOBAL(0, tmpUSUARIO("CAT_LO_USUARIO"), "Modulos", "Cierre de sesión")
            Session.Abandon()
            Response.Redirect("~/Login")

        Catch ex As System.Threading.ThreadAbortException

        Catch ex As Exception
            Response.Redirect("~/Login")
        End Try

    End Sub
    ''' <summary>
    ''' Inicializa Session("Permisos") con los permisos correspondientes para el portal seleccionado
    ''' </summary>
    ''' <param name="modulo">1:Gestion - 2:Administrador - 3:Reportes - 4:BackOffice - 5:Móvil - 6:Judicial</param>
    Private Sub cargarPermisos(modulo As Integer)
        Try

            Dim DtsPermisoA As DataTable = SP.PERMISOS(tmpUSUARIO("CAT_LO_USUARIO"), tmpUSUARIO("CAT_LO_CONTRASENA"), modulo)

            If DtsPermisoA.TableName = "Exception" Then
                'Response.Redirect("FinDeSesion")
                Session.Clear()
                Session.Abandon()
                Response.Redirect("~/SesionExpirada.aspx")
            Else
                Dim permisos As IDictionary(Of String, Boolean) = New Dictionary(Of String, Boolean)()

                For Each row As DataRow In DtsPermisoA.Rows
                    permisos.Add(row(0), IIf(row(1) = "1", True, False))
                Next
                tmpPermisos = permisos

                Dim Dt As DataTable = SP.INGRESO(2, tmpUSUARIO("CAT_LO_USUARIO"), GetIPv4Address(), moduloIntegerToString(modulo))
            End If
        Catch ex As System.Threading.ThreadAbortException
        Catch ex As Exception
            'Response.Redirect("FinDeSesion")
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/SesionExpirada.aspx")
        End Try
    End Sub

    Private Function moduloIntegerToString(modulo As Integer) As String
        Dim moduloString As String = ""
        Select Case modulo
            Case 1
                moduloString = "Gestión"
            Case 2
                moduloString = "Administrador"
            Case 3
                moduloString = "Reportes"
            Case 4
                moduloString = "BackOffice"
            Case 5
                moduloString = "Móvil"
            Case 6
                moduloString = "Judicial"
            Case Else
                moduloString = "N/A"
        End Select
        Return moduloString
    End Function

    Private Function GetIPv4Address() As String
        GetIPv4Address = String.Empty
        Dim strHostName As String = System.Net.Dns.GetHostName()
        Dim iphe As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(strHostName)

        For Each ipheal As System.Net.IPAddress In iphe.AddressList
            If ipheal.AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork Then
                GetIPv4Address = ipheal.ToString()
            End If
        Next

    End Function
End Class
