Imports System.Web.Services
Imports Class_Mail
Imports System.Windows.Forms
Imports Telerik.Web.UI.PivotGrid.Core.Totals
Imports System.Web.UI.Control
Imports System.Data
Imports System.Data.SqlClient
Imports Db
Imports Funciones
Imports System.IO
Imports Telerik.Web.UI
Imports Spire.Xls

Partial Class CatalogoMail
    Inherits System.Web.UI.Page



    Public Property TmpUSUARIO As IDictionary
        Get
            Return CType(Session("MAILS"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("MAILS") = value
        End Set
    End Property

    Public Sub gridMails_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles gridMails.NeedDataSource
        Dim SSCommandUsuario As New SqlCommand
        SSCommandUsuario.CommandText = "SP_EMAILS"
        gridMails.DataSource = SP.EMAILS(bandera:=0)

    End Sub

    Public Sub gridMails_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles gridMails.ItemCommand

        Dim valores(10) As String
        Try
            valores(0) = e.Item.Cells.Item(3).Text '
            valores(1) = e.Item.Cells.Item(5).Text 'descripcion
            valores(2) = e.Item.Cells.Item(6).Text '
            valores(3) = e.Item.Cells.Item(7).Text 'ncuenta
            valores(4) = e.Item.Cells.Item(8).Text 'correo
            valores(5) = e.Item.Cells.Item(9).Text 'smtp
            valores(6) = e.Item.Cells.Item(10).Text 'puerto
            valores(7) = e.Item.Cells.Item(11).Text 'fecha
            valores(8) = e.Item.Cells.Item(4).Text '
        Catch

        End Try
        'Try
        '    Dim myArrList As ArrayList = New ArrayList
        '    myArrList.Insert(0, e.Item.Cells.Item(4).Text) ' The 0 here represents your index.
        '    myArrList.Insert(1, e.Item.Cells.Item(5).Text)
        '    myArrList.Insert(2, e.Item.Cells.Item(6).Text)
        '    myArrList.Insert(3, e.Item.Cells.Item(7).Text)
        '    myArrList.Insert(4, e.Item.Cells.Item(8).Text)
        '    myArrList.Insert(5, e.Item.Cells.Item(9).Text)
        '    myArrList.Insert(6, e.Item.Cells.Item(10).Text)
        '    myArrList.Insert(7, e.Item.Cells.Item(11).Text)
        '    myArrList.Insert(8, e.Item.Cells.Item(12).Text)
        '    ' Then all you do is this.
        '    If myArrList.Count <> 0 Then
        '        Session.Add("MyArrayList", myArrList)
        '    End If
        'Catch

        'End Try



        Select Case e.CommandName
            Case "Edit"




            Case "Delete"
                BorrarPerfil(valores(0), valores(8))


            Case "send"


            Case "Update"

                '  Dim aditedItem As GridEditableItem = CType(e.Item, GridEditableItem)
                '  Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
                '  valores as





        End Select



    End Sub

    Private Sub gridMails_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gridMails.SelectedIndexChanged




    End Sub

    Private Sub gridMails_Load(sender As Object, e As EventArgs) Handles gridMails.Load

        Dim valores As New Hashtable
        gridMails.Rebind()




    End Sub
End Class