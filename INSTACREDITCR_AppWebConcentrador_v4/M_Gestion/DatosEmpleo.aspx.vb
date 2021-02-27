Imports System.Data
Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports Db
Imports System.Globalization
Imports System.IO
Imports Funciones
Imports Telerik.Web.UI
Partial Class M_Gestion_DatosEmpleo
    Inherits System.Web.UI.Page
    Private Sub GvHistAsigna_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles GvHistAsigna.NeedDataSource
        Try
            GvHistAsigna.DataSource = Class_Hist_Act.LlenarElementosHistAct(tmpCredito("PR_MC_CREDITO"), "HIST_AS_DTEASIG", "DESC", 2, tmpCredito("PR_MC_PRODUCTO"))
        Catch ex As Exception
            GvHistAsigna.DataSource = Nothing
        End Try
    End Sub

    Private Sub gridEmpleos_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles gridEmpleos.NeedDataSource
        Try
            gridEmpleos.DataSource = Class_Hist_Empleo.LlenarInfoEmpleo(tmpCredito("PR_MC_CREDITO"), tmpCredito("PR_MC_PRODUCTO"))
        Catch ex As Exception
            gridEmpleos.DataSource = Nothing
        End Try
    End Sub

    Private Sub gridDirecciones_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles gridDirecciones.NeedDataSource
        Try
            gridDirecciones.DataSource = Class_Hist_Empleo.LlenarInfoDirecciones(tmpCredito("PR_MC_CREDITO"), tmpCredito("PR_MC_PRODUCTO"))
        Catch ex As Exception
            gridDirecciones.DataSource = Nothing
        End Try
    End Sub

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load

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
End Class
