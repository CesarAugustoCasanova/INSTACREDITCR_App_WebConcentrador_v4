Imports Funciones
Imports System.Data
Imports System.Data.SqlClient
Imports Db

Partial Class Editar
    Inherits System.Web.UI.UserControl
    Private _dataItem As Object = Nothing

    Private Sub Editar_Init(sender As Object, e As EventArgs) Handles Me.Init


        If Session("initInsert") Then

            TxtCat_Ag_Usuario.Visible = False
            TxtCat_Ag_Nombre.ReadOnly = False
            LblCat_Ag_Usuario.Visible = False


        End If
    End Sub
    Protected Sub DdlCat_Ag_Estatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlCat_Ag_Estatus.SelectedIndexChanged
        Try
            If DdlCat_Ag_Estatus.SelectedValue = "Baja" Then
                LblCat_Ag_Motivo.Visible = True
                TxtCat_Ag_Motivo.Visible = True
            Else
                LblCat_Ag_Motivo.Visible = False
                TxtCat_Ag_Motivo.Visible = False
            End If
        Catch ex As Exception
            SendMail("DdlCat_Ag_Estatus_SelectedIndexChanged", ex, "", "", "")
        End Try

    End Sub

    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Administrador", "Editar.ascx", evento, ex, Cuenta, Captura, usr)
    End Sub
    Public Property DataItem() As Object
        Get
            Return Me._dataItem
        End Get
        Set(ByVal value As Object)
            Me._dataItem = value
        End Set
    End Property
End Class
