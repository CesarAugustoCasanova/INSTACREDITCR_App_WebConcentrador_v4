
Imports Telerik.Web.UI
Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports Db
Imports System.Web.SessionState.HttpSessionState
Imports System.Data
Imports Funciones
Imports System.Web.Services


Partial Class _CatalogoSucursales
    Inherits System.Web.UI.Page
    ''' <summary>
    ''' Establece los parametros para mostrar una notificacion
    ''' </summary>
    ''' <param name="Notificacion">Objeto RadNotification de Telerik</param>
    ''' <param name="icono">info - delete - deny - edit - ok - warning - none</param>
    ''' <param name="titulo">título de la notificación</param>
    ''' <param name="msg">mensaje de la notificación</param>
    Public Shared Sub showModal(ByRef Notificacion As Object, ByVal icono As String, ByVal titulo As String, ByVal msg As String)
        Dim radnot As RadNotification = TryCast(Notificacion, RadNotification)
        radnot.TitleIcon = icono
        radnot.ContentIcon = icono
        radnot.Title = titulo
        radnot.Text = msg
        radnot.Show()
    End Sub

    <WebMethod()>
    Public Shared Function webServiceDummy(context As Object) As AutoCompleteBoxData
        Dim result As New List(Of AutoCompleteBoxItemData)()
        Dim res As New AutoCompleteBoxData()
        res.Items = result.ToArray()

        Return res
    End Function

    <WebMethod()>
    Public Shared Function webServiceCP(context As Object) As AutoCompleteBoxData
        Dim searchString As String = DirectCast(context, Dictionary(Of String, Object))("Text").ToString()
        Dim data As DataTable = obtenerCPs(searchString)
        Dim result As New List(Of AutoCompleteBoxItemData)()

        For Each row As DataRow In data.Rows
            Dim childNode As New AutoCompleteBoxItemData()
            childNode.Text = row("CAT_SE_CODIGO").ToString()
            childNode.Value = row("CAT_SE_CODIGO").ToString()
            result.Add(childNode)
        Next

        Dim res As New AutoCompleteBoxData()
        res.Items = result.ToArray()

        Return res
    End Function

    Private Shared Function obtenerCPs(searchString As String) As DataTable
        Dim SSCommandCat As New SqlCommand
        SSCommandCat.CommandText = "SP_CAT_SUCURSALES"
        SSCommandCat.CommandType = CommandType.StoredProcedure
        SSCommandCat.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 6
        SSCommandCat.Parameters.Add("@V_CAT_SUC_CP", SqlDbType.NVarChar).Value = searchString
        Return Consulta_Procedure(SSCommandCat, "Catalogo")
    End Function

    Private Sub limpiar()
        tbClave.Text = ""
        tbDireccion.Text = ""
        tbEstado.Text = ""
        tbLocalidad.Text = ""
        tbMunicipio.Text = ""
        tbNombre.Text = ""
        acbCP.Entries.Clear()
        acbTelefonos.Entries.Clear()
    End Sub

    Private Sub eliminarSucursal(id As Integer)
        Dim SSCommandCat As New SqlCommand
        SSCommandCat.CommandText = "SP_CAT_SUCURSALES"
        SSCommandCat.CommandType = CommandType.StoredProcedure
        SSCommandCat.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 2
        SSCommandCat.Parameters.Add("@V_CAT_SUC_ID", SqlDbType.Decimal).Value = id
        Consulta_Procedure(SSCommandCat, "Catalogo")
    End Sub

    Private Sub cargarSucursal(id As Integer)
        Dim SSCommandCat As New SqlCommand
        SSCommandCat.CommandText = "SP_CAT_SUCURSALES"
        SSCommandCat.CommandType = CommandType.StoredProcedure
        SSCommandCat.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 4
        SSCommandCat.Parameters.Add("@V_CAT_SUC_ID", SqlDbType.Decimal).Value = id
        Dim row As DataRow = Consulta_Procedure(SSCommandCat, "Catalogo")(0)
        tbClave.Text = row("CAT_SUC_CLAVE")
        tbDireccion.Text = row("CAT_SUC_DIRECCION")
        tbEstado.Text = row("CAT_SUC_ESTADO")
        tbLocalidad.Text = row("CAT_SUC_LOCALIDAD")
        tbMunicipio.Text = row("CAT_SUC_MUNICIPIO")
        tbNombre.Text = row("CAT_SUC_NOMBRE")
        For Each entry As String In row("CAT_SUC_TELEFONOS").ToString.Split("|")
            acbTelefonos.Entries.Add(New AutoCompleteBoxEntry(entry))
        Next
        acbCP.Entries.Add(New AutoCompleteBoxEntry(row("CAT_SUC_CP")))
    End Sub

    Private Function formatoTelefonos() As String
        Dim telefonos = acbTelefonos.Text
        telefonos = telefonos.Replace(" ", "")
        telefonos = telefonos.Substring(0, telefonos.Length - 1)
        Return telefonos
    End Function

    Private Sub gridSucursales_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles gridSucursales.NeedDataSource
        Dim SSCommandCat As New SqlCommand
        SSCommandCat.CommandText = "SP_CAT_SUCURSALES"
        SSCommandCat.CommandType = CommandType.StoredProcedure
        SSCommandCat.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 3
        gridSucursales.DataSource = Consulta_Procedure(SSCommandCat, "Catalogo")
    End Sub



    Private Sub _CatalogoSucursales_Init(sender As Object, e As EventArgs) Handles Me.Init
        If Not IsPostBack Then
            gridSucursales.Rebind()
        End If
    End Sub

    Private Sub btnAgregarSucursal_Click(sender As Object, e As EventArgs) Handles btnAgregarSucursal.Click
        pnlSucursales.Visible = False
        pnlSucursal.Visible = True
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If Page.IsValid Then
            Try
                Dim SSCommandCat As New SqlCommand
                SSCommandCat.CommandText = "SP_CAT_SUCURSALES"
                SSCommandCat.CommandType = CommandType.StoredProcedure
                SSCommandCat.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 1
                SSCommandCat.Parameters.Add("@V_CAT_SUC_ID", SqlDbType.NVarChar).Value = Session("V_ID")
                SSCommandCat.Parameters.Add("@V_CAT_SUC_NOMBRE", SqlDbType.NVarChar).Value = tbNombre.Text
                SSCommandCat.Parameters.Add("@V_CAT_SUC_CLAVE", SqlDbType.NVarChar).Value = tbClave.Text
                SSCommandCat.Parameters.Add("@V_CAT_SUC_DIRECCION", SqlDbType.NVarChar).Value = tbDireccion.Text
                SSCommandCat.Parameters.Add("@V_CAT_SUC_CP", SqlDbType.NVarChar).Value = acbCP.Text
                SSCommandCat.Parameters.Add("@V_CAT_SUC_ESTADO", SqlDbType.NVarChar).Value = tbEstado.Text
                SSCommandCat.Parameters.Add("@V_CAT_SUC_MUNICIPIO", SqlDbType.NVarChar).Value = tbMunicipio.Text
                SSCommandCat.Parameters.Add("@V_CAT_SUC_LOCALIDAD", SqlDbType.NVarChar).Value = tbLocalidad.Text
                SSCommandCat.Parameters.Add("@V_CAT_SUC_TELEFONOS", SqlDbType.NVarChar).Value = formatoTelefonos()
                Consulta_Procedure(SSCommandCat, "Catalogo")
                limpiar()
            Catch ex As Exception

            End Try
            pnlSucursal.Visible = False
            pnlSucursales.Visible = True
            gridSucursales.Rebind()
            limpiar()
        End If
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        pnlSucursal.Visible = False
        pnlSucursales.Visible = True
        gridSucursales.Rebind()
        limpiar()
    End Sub

    Private Sub acbCP_TextChanged(sender As Object, e As AutoCompleteTextEventArgs) Handles acbCP.TextChanged
        Dim SSCommandCat As New SqlCommand
        SSCommandCat.CommandText = "SP_CAT_SUCURSALES"
        SSCommandCat.CommandType = CommandType.StoredProcedure
        SSCommandCat.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 5
        SSCommandCat.Parameters.Add("@V_CAT_SUC_CP", SqlDbType.NVarChar).Value = acbCP.Text
        Try
            Dim row As DataRow = Consulta_Procedure(SSCommandCat, "Catalogo")(0)
            tbLocalidad.Text = row("CAT_SE_CIUDAD")
            tbEstado.Text = row("CAT_SE_ESTADO")
            tbMunicipio.Text = row("CAT_SE_MUNICIPIO")
        Catch ex As Exception
            tbLocalidad.Text = ""
            tbEstado.Text = ""
            tbMunicipio.Text = ""
        End Try
    End Sub

    Private Sub gridSucursales_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles gridSucursales.ItemCommand
        Dim item As GridDataItem = CType(e.Item, GridDataItem)
        Dim idCell As GridTableCell = CType(item.Controls(4), GridTableCell)
        Dim SucursalCell As GridTableCell = CType(item.Controls(5), GridTableCell)
        If e.CommandName = "onEdit" Then
            pnlSucursales.Visible = False
            pnlSucursal.Visible = True
            Session("V_ID") = idCell.Text
            cargarSucursal(idCell.Text)
        ElseIf e.CommandName = "onDelete" Then
            eliminarSucursal(idCell.Text)
            gridSucursales.Rebind()
        End If
    End Sub
End Class
