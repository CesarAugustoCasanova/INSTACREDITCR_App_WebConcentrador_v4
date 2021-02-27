
Imports System.Data
Imports System.IO
Imports System.Net
Imports Telerik.Web.UI

Partial Class Modulos_FTP_FTPManager
    Inherits System.Web.UI.UserControl
    Private Shared ftpDirectory As String = ""
    Private Shared usuario As String = ""
    Private Shared contraseña As String = ""
    Private Shared allowedExtensions As String = ""
    Private Shared _showftpDirectory As Boolean = True

    Private Property directoryTree As List(Of String)
        Get
            Return CType(Session("directoryTree"), List(Of String))
        End Get
        Set(value As List(Of String))
            Session("directoryTree") = value
        End Set
    End Property

    Public Property DirectorioFTP As String
        Get
            Return ftpDirectory
        End Get
        Set(value As String)
            ftpDirectory = value
        End Set
    End Property
    Public Property UsuarioFTP As String
        Get
            Return usuario
        End Get
        Set(value As String)
            usuario = value
        End Set
    End Property
    Public Property ContrasenaFTP As String
        Get
            Return contraseña
        End Get
        Set(value As String)
            contraseña = value
        End Set
    End Property
    Public Property ExtensionesPermitidas As String
        Get
            Return allowedExtensions
        End Get
        Set(value As String)
            allowedExtensions = value
        End Set
    End Property

    Public Property ShowftpDirectory As Boolean
        Get
            Return _showftpDirectory
        End Get
        Set(value As Boolean)
            _showftpDirectory = value
        End Set
    End Property

    Private Sub Modulos_FTP_FTPManager_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblRuta.Visible = _showftpDirectory
            directoryTree = New List(Of String)
            directoryTree.Add("/")
            PrintIfo()
        End If
    End Sub

    Public Sub PrintIfo()
        Dim ex As New Exception
        Dim fullFTPDirectory As String = ftpDirectory & String.Join("", directoryTree)
        lblRuta.Text = "Ruta de acceso: " & ftpDirectory & String.Join(" ", directoryTree)

        Dim ls As List(Of String) = ListRemoteFiles(fullFTPDirectory, usuario, contraseña, ex)
        Dim data() As DataTable = processFilesString(ls)

        Session("gvFiles") = data(0)
        Session("gvDirectorio") = data(1)

        gvDirectorio.Rebind()
    End Sub

    Protected Sub DownloadFile(sender As Object, e As EventArgs)
        Dim fileName As String = TryCast(sender, LinkButton).CommandArgument
        Dim ex As New Exception
        Dim fullFTPDirectory As String = ftpDirectory & String.Join("", directoryTree)
        DownloadSingleFile(fullFTPDirectory, usuario, contraseña, fileName, ex)
    End Sub

    Protected Sub MoveDownDirectory(sender As Object, e As EventArgs)
        Dim directoryName As String = TryCast(sender, LinkButton).CommandArgument
        directoryTree.Add(directoryName & "/")
        PrintIfo()
    End Sub

    Protected Sub MoveUpDirectory(sender As Object, e As EventArgs)
        Dim isRoot As Boolean
        Dim lastDirectory As String = directoryTree.Item(directoryTree.Count - 1)
        isRoot = IIf(lastDirectory = "/", True, False)

        If Not isRoot Then
            directoryTree.RemoveAt(directoryTree.Count - 1)
        End If
        PrintIfo()
    End Sub

    Private Function processFilesString(entries As List(Of String)) As DataTable()
        Dim dtFiles As New DataTable()
        Dim dtDirectories As New DataTable()

        dtFiles.Columns.AddRange(New DataColumn(2) {
                                 New DataColumn("Name", GetType(String)),
                                 New DataColumn("Size", GetType(Decimal)),
                                 New DataColumn("Date", GetType(String))})

        dtDirectories.Columns.AddRange(New DataColumn(0) {
                                 New DataColumn("Name", GetType(String))})

        'Loop and add details of each File to the DataTable.
        For Each entry As String In entries
            Dim splits As String() = entry.Split(New String() {" "}, StringSplitOptions.RemoveEmptyEntries)

            'Determine whether entry is for File or Directory.
            Dim isFile As Boolean = splits(0).Substring(0, 1) <> "d"
            Dim isDirectory As Boolean = splits(0).Substring(0, 1) = "d"

            'If entry is for File, add details to DataTable.
            If isFile Then
                Dim size As Decimal = Decimal.Parse(splits(4)) / 1024
                Dim fecha As String = String.Join(" ", splits(5), splits(6), splits(7))
                Dim name As String = String.Empty
                For i As Integer = 8 To splits.Length - 1
                    name = String.Join(" ", name, splits(i))
                Next
                Dim extensiones() As String = allowedExtensions.Split(",")
                Dim extension As String
                Try
                    extension = name.Split(".")(1)
                Catch ex As Exception
                    extension = "txt"
                End Try
                If extensiones.Contains(extension) Or (allowedExtensions = "" Or allowedExtensions = "*") Then
                    dtFiles.Rows.Add()
                    dtFiles.Rows(dtFiles.Rows.Count - 1)("Size") = Decimal.Parse(splits(4)) / 1024
                    dtFiles.Rows(dtFiles.Rows.Count - 1)("Date") = String.Join(" ", splits(5), splits(6), splits(7))
                    dtFiles.Rows(dtFiles.Rows.Count - 1)("Name") = name.Trim()
                End If
            Else
                Dim name As String = String.Empty
                For i As Integer = 8 To splits.Length - 1
                    name = String.Join(" ", name, splits(i))
                Next
                'Excluimos las carpetas que hace referancia a la carpeta actual o a la carpeta anterior
                If name <> "" And name <> "." And name <> ".." Then
                    dtDirectories.Rows.Add()
                    dtDirectories.Rows(dtDirectories.Rows.Count - 1)("Name") = name.Trim()
                End If
            End If
        Next
        Return {dtFiles, dtDirectories}
    End Function
    Private Function ListRemoteFiles(ftpAddress As String, ftpUser As String, ftpPassword As String, ByRef ExceptionInfo As Exception) As List(Of String)

        Dim ListOfFilesOnFTPSite As New List(Of String)

        Dim ftpRequest As FtpWebRequest = Nothing
        Dim ftpResponse As FtpWebResponse = Nothing

        Dim strReader As StreamReader = Nothing
        Dim sline As String = ""

        Try
            ftpRequest = CType(WebRequest.Create(ftpAddress), FtpWebRequest)

            With ftpRequest
                .Credentials = New NetworkCredential(ftpUser, ftpPassword)
                .Method = WebRequestMethods.Ftp.ListDirectoryDetails
            End With

            ftpResponse = CType(ftpRequest.GetResponse, FtpWebResponse)

            strReader = New StreamReader(ftpResponse.GetResponseStream)

            If strReader IsNot Nothing Then sline = strReader.ReadLine

            While sline IsNot Nothing
                ListOfFilesOnFTPSite.Add(sline)
                sline = strReader.ReadLine
            End While

        Catch ex As Exception
            ExceptionInfo = ex

        Finally
            If ftpResponse IsNot Nothing Then
                ftpResponse.Close()
                ftpResponse = Nothing
            End If

            If strReader IsNot Nothing Then
                strReader.Close()
                strReader = Nothing
            End If
        End Try
        ListRemoteFiles = ListOfFilesOnFTPSite

        ListOfFilesOnFTPSite = Nothing
    End Function

    Private Sub DownloadSingleFile(ftpAddress As String, ftpUser As String, ftpPassword As String, fileToDownload As String, ExceptionInfo As Exception)
        Try
            'Create FTP Request.
            Dim request As FtpWebRequest = DirectCast(WebRequest.Create(ftpAddress & fileToDownload), FtpWebRequest)
            request.Method = WebRequestMethods.Ftp.DownloadFile

            'Enter FTP Server credentials.
            request.Credentials = New NetworkCredential(ftpUser, ftpPassword)
            request.UsePassive = True
            request.UseBinary = True
            request.EnableSsl = False

            'Fetch the Response and read it into a MemoryStream object.
            Dim resp As FtpWebResponse = DirectCast(request.GetResponse(), FtpWebResponse)
            Using stream As New MemoryStream()
                'Download the File.
                resp.GetResponseStream().CopyTo(stream)
                Response.AddHeader("content-disposition", "attachment;filename=" & fileToDownload)
                Response.Cache.SetCacheability(HttpCacheability.NoCache)
                Response.BinaryWrite(stream.ToArray())
                Response.End()
            End Using
        Catch ex As WebException
            'Throw New Exception(TryCast(ex.Response, FtpWebResponse).StatusDescription)
        End Try
    End Sub

    Private Sub gvDirectorio_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles gvDirectorio.NeedDataSource
        gvDirectorio.DataSource = Session("gvDirectorio")
        gvFiles.Rebind()
    End Sub

    Private Sub gvFiles_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles gvFiles.NeedDataSource
        gvFiles.DataSource = Session("gvFiles")
    End Sub

    Private Sub Modulos_FTP_FTPManager_DataBinding(sender As Object, e As EventArgs) Handles Me.DataBinding
        directoryTree = New List(Of String)
        directoryTree.Add("/")
        PrintIfo()
    End Sub
End Class
