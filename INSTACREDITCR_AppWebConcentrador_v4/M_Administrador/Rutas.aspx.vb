Imports System.Threading.Tasks
Imports Telerik.Web.UI
Imports System.Data
Imports System.Data.SqlClient
Imports Db

Partial Class Rutas
    Inherits System.Web.UI.Page
    Protected Sub Aviso(ByVal MSJ As String)
        RadAviso.RadAlert(MSJ, 440, 155, "AVISO", Nothing)
    End Sub

    Private Function llenarGridRutas() As DataTable
        dim SSCommand as new sqlcommand
        SSCommand.CommandText = "Sp_rutas"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 1

        Dim Ds As DataTable = Consulta_Procedure(SSCommand, "Campo")
        Return Ds
    End Function

    Private Function AnadirRuta(ByVal nombreRuta As String) As Boolean
        Try
            dim SSCommand as new sqlcommand
            SSCommand.CommandText = "Sp_rutas"
            SSCommand.CommandType = CommandType.StoredProcedure
            SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 2
            SSCommand.Parameters.Add("@V_Nombre_Ruta", SqlDbType.NVarChar).Value = nombreRuta

            Dim Ds As DataTable = Consulta_Procedure(SSCommand, "Campo")
            Dim res = Ds(0)(0).ToString
            Return IIf(res = "ok", True, False)
        Catch ex As Exception
        End Try
        Return False
    End Function

    Private Function AnadirCP(ByVal nombreRuta As String, cp As String) As String
        Try
            dim SSCommand as new sqlcommand
            SSCommand.CommandText = "Sp_rutas"
            SSCommand.CommandType = CommandType.StoredProcedure
            SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 3
            SSCommand.Parameters.Add("@V_CP", SqlDbType.NVarChar).Value = cp
            SSCommand.Parameters.Add("@V_Nombre_Ruta", SqlDbType.NVarChar).Value = nombreRuta

            Dim Ds As DataTable = Consulta_Procedure(SSCommand, "Campo")
            Dim res = Ds(0)(0).ToString
            Return res
        Catch ex As Exception
        End Try
        Return False
    End Function

    Private Sub gridRutas_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles gridRutas.ItemCommand
        Select Case e.CommandName
            Case "PerformInsert"
                Dim item As GridEditFormInsertItem = e.Item
                Dim nombreRuta As String = TryCast(item.FindControl("tbNombre"), RadTextBox).Text
                If AnadirRuta(nombreRuta) Then
                    Aviso("Ruta añadida")
                Else
                    Aviso("Ruta duplicada. Inserción fallida")
                    e.Canceled = True
                End If
        End Select

    End Sub

    Private Sub gridRutas_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles gridRutas.NeedDataSource
        Try
            gridRutas.DataSource = llenarGridRutas()
        Catch ex As Exception
            gridRutas.DataSource = Nothing
        End Try
    End Sub

    Private Sub uploadArchivo_FileUploaded(sender As Object, e As FileUploadedEventArgs) Handles uploadArchivo.FileUploaded
        ProcesarArchivo(e.File.InputStream)
    End Sub

    Protected Function ProcesarArchivo(file As IO.Stream) As Boolean
        Dim rd As IO.StreamReader = New IO.StreamReader(file)
        Dim dt As DataTable = New DataTable()
        Dim campos(13) As String
        'Informacion tabla
        campos(0) = "msg"
        For Each campo As String In campos
            Dim dc As DataColumn = New DataColumn(campo)
            dt.Columns.Add(dc)
        Next
        'leemos la primer línea que solo trae el titulo de los campos
        Dim linea As String = rd.ReadLine
        Dim actualizados As Integer = 0
        Dim insertados As Integer = 0
        Dim errores As Integer = 0
        Dim dr As DataRow = dt.NewRow()

        While Not rd.EndOfStream
            linea = rd.ReadLine
            Dim cp As String = linea.Split(",")(0)
            Dim ruta As String = linea.Split(",")(1)
            Dim res As String = AnadirCP(ruta, cp)
            If res = "ok" Then
                insertados += 1
            ElseIf res.Contains("NO") Then
                errores += 1
                dr = dt.NewRow()
                dr("msg") = res
                dt.Rows.Add(dr)
            ElseIf res.Contains("C.P") Then
                actualizados += 1
                dr = dt.NewRow()
                dr("msg") = res
                dt.Rows.Add(dr)
            End If
        End While
        dr = dt.NewRow()
        dr("msg") = " "
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("msg") = "Registros insertados: " & insertados
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("msg") = "Registros actualizados: " & actualizados
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("msg") = "Registros erroneos: " & errores
        dt.Rows.Add(dr)

        dt.AcceptChanges()
        Session("rl") = dt
        gridRutas.Rebind()
        rl1.Rebind()
        Return True
    End Function

    Private Sub rl1_NeedDataSource(sender As Object, e As RadListViewNeedDataSourceEventArgs) Handles rl1.NeedDataSource
        Try
            rl1.DataSource = Session("rl")
        Catch ex As Exception
            rl1.DataSource = Nothing
        End Try
    End Sub

    Private Sub Rutas_Init(sender As Object, e As EventArgs) Handles Me.Init
        If Not IsPostBack Then
            Session.Remove("rl")
        End If
    End Sub
End Class
