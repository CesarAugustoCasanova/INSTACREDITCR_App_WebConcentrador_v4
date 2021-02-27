Imports Conexiones
Imports System.Data.SqlClient
Imports System.Data
Imports System.Text
Imports Db
Imports Funciones
Imports Telerik.Web.UI
Imports System.Web.Services

Partial Class MasterPageR
    Inherits System.Web.UI.MasterPage
    Public Property tmpUSUARIO As IDictionary
        Get
            Return CType(Session("USUARIO"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("USUARIO") = value
        End Set

    End Property
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim sessionactive As Integer = GetSessionActive.Search(tmpUSUARIO("CAT_LO_USUARIO"), 1)

            If tmpUSUARIO Is Nothing Or sessionactive = 0 Then
                Response.Redirect("~/SesionExpirada.aspx")
            End If
            Try
                Llenar()
            Catch ex As Exception
                Dim es As String = ex.Message
            End Try

        Catch ex As System.Threading.ThreadAbortException

        Catch ex As Exception
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/SesionExpirada.aspx")
        End Try

    End Sub

    Protected Sub Llenar()

        If tmpUSUARIO("CAT_LO_MREPORTES") = "00" Then
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/SesionExpirada.aspx")
        ElseIf tmpUSUARIO("CAT_LO_MREPORTES") = "01" Then
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/SesionExpirada.aspx")
        ElseIf tmpUSUARIO("CAT_LO_MREPORTES") = "10" Then
            Menu_Configuracion_Reportes.Visible = False
        End If

        Dim DtsReportes As DataTable = Bloques(3, "")
        Dim Cuantos As Integer = 0
        Dim menuGenerator As New MenuGenerator()

        For indice As Integer = 0 To DtsReportes.Rows.Count - 1
            Dim title As String = DtsReportes.Rows(indice)("Cat_Red_Bloque")
            Dim itemid As String = title.Replace(" ", "").Replace(".", "").Replace(",", "").Replace("ñ", "n").Replace("Ñ", "N").Replace("$", "").Replace("/", "")
            Cuantos = indice + 3
            Dim dropdownitem As New DropDownItemGenerator(title, itemid, "&#xe8e5;")
            Dim DtsHijos As DataTable = Bloques(4, title)

            For indice2 As Integer = 0 To DtsHijos.Rows.Count - 1
                Dim childTitle As String = DtsHijos.Rows(indice2)("Cat_Red_Nombre")
                Dim childItemid As String = title.Replace(" ", "").Replace(".", "").Replace(",", "").Replace("ñ", "n").Replace("Ñ", "N").Replace("$", "").Replace("/", "")
                dropdownitem.AddItem(childTitle, "DashBoard.aspx?" & childTitle, childItemid)
            Next
            menuGenerator.AddDropdownItem(dropdownitem)
        Next
        If Cuantos = 0 Then
            Cuantos = 2
        End If

        Dim DtsReportesFijos As DataTable = Bloques(9, "")

        For indice As Integer = 0 To DtsReportesFijos.Rows.Count - 1
            Dim title As String = DtsReportesFijos.Rows(indice)("CAT_REF_PADRE")
            Dim itemid As String = title.Replace(" ", "").Replace(".", "").Replace(",", "").Replace("ñ", "n").Replace("Ñ", "N").Replace("$", "").Replace("/", "")
            Dim dropdownitem As New DropDownItemGenerator(title, itemid, "&#xe8e5;")


            Dim DtsHijosFijos As DataTable = Bloques(10, title)

            For indice2 As Integer = 0 To DtsHijosFijos.Rows.Count - 1
                Dim childTitle As String = DtsHijosFijos.Rows(indice2)("CAT_REF_HIJO")
                Dim childItemid As String = title.Replace(" ", "").Replace(".", "").Replace(",", "").Replace("ñ", "n").Replace("Ñ", "N").Replace("$", "").Replace("/", "")
                dropdownitem.AddItem(childTitle, DtsHijosFijos.Rows(indice2)("CAT_REF_URL"), childItemid)
            Next
            menuGenerator.AddDropdownItem(dropdownitem)
        Next
        DataBindMenu(menuGenerator.GetHTMLItems)
    End Sub
    Function Bloques(ByVal V_Bandera As Integer, ByVal V_valor As String) As DataTable
        Dim SSCommandId As New SqlCommand

        Dim HeredarReporte As String = "'" & tmpUSUARIO("CAT_LO_HEREDAR") & "'," & "'" & tmpUSUARIO("CAT_LO_NUM_AGENCIA") & "'"

        SSCommandId.CommandText = "Sp_Add_Cat_Reporte_Detalle"
        SSCommandId.CommandType = CommandType.StoredProcedure
        SSCommandId.Parameters.Add("@V_Cat_Red_Id", SqlDbType.Decimal).Value = 0
        SSCommandId.Parameters.Add("@V_Cat_Red_Nombre", SqlDbType.NVarChar).Value = ""
        SSCommandId.Parameters.Add("@V_Cat_Red_Descripcion", SqlDbType.NVarChar).Value = ""
        SSCommandId.Parameters.Add("@V_Cat_Red_Tabla_Desc", SqlDbType.NVarChar).Value = ""
        SSCommandId.Parameters.Add("@V_Cat_Red_Campo_Desc", SqlDbType.NVarChar).Value = ""
        SSCommandId.Parameters.Add("@V_Cat_Red_Condicion", SqlDbType.Decimal).Value = 0
        SSCommandId.Parameters.Add("@V_Cat_Red_Formato", SqlDbType.NVarChar).Value = ""
        SSCommandId.Parameters.Add("@V_Cat_Red_Orden", SqlDbType.Decimal).Value = 0
        SSCommandId.Parameters.Add("@V_Cat_Red_Tipo", SqlDbType.NVarChar).Value = ""
        SSCommandId.Parameters.Add("@V_Cat_Red_Bloque", SqlDbType.NVarChar).Value = V_valor
        SSCommandId.Parameters.Add("@V_Agencia", SqlDbType.NVarChar).Value = HeredarReporte 'CType(Session("USUARIO"), USUARIO).CAT_LO_AGENCIA
        SSCommandId.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = V_Bandera
        Dim DtsReportes As DataTable = Consulta_Procedure(SSCommandId, "Reporte")
        Return DtsReportes
    End Function

    Private Sub DataBindMenu(html As String)
        ltTable.Text = html
    End Sub
End Class

Class MenuGenerator
    Private items As StringBuilder

    Public Sub New()
        items = New StringBuilder
    End Sub

    Public Sub AddSingleItem(id As String, icon As String, text As String, url As String)
        Dim singleItem As String = "<li class='nav-item bottom-border' id='{0}' runat='server'><a class='nav-link' href='{1}'><i class='material-icons' style='font-size: 1em'>{2}</i>{3}</a></li>"
        items.Append(String.Format(singleItem, id, url, icon, text))
        '<li Class='nav-item bottom-border'>
        '  <a Class='nav-link' href='./Inicio.aspx'><i class='material-icons' style='font-size: 1em'>&#xe88a;</i> Inicio</a>
        '</li>
    End Sub

    Public Sub AddDropdownItem(ByRef item As DropDownItemGenerator)
        items.Append(item.GenerateDropDownItem())
    End Sub

    Public Function GetHTMLItems() As String
        Return items.ToString
    End Function
End Class

''' <summary>
''' Esta clase sirve para construir items para el menu
''' personalizado. Estos Items deben tener una lista de items
''' para que funcionen de manera correcta
''' </summary>
Class DropDownItemGenerator
    Private ListTexts As List(Of String)
    Private ListUrls As List(Of String)
    Private ListIDs As List(Of String)
    Private title As String
    Private itemID As String
    Private icon As String
    Private howManyItems As Integer

    ''' <summary>
    ''' Inicializa los campos para generar un item con dropdown en el menu
    ''' </summary>
    ''' <param name="title">Titulo que aparecerá en el menu</param>
    ''' <param name="itemID">ID que se le asignará al menu</param>
    ''' <param name="icon">codigo html hex para font icon de google</param>
    Public Sub New(title As String, itemID As String, icon As String)
        ListTexts = New List(Of String)
        ListUrls = New List(Of String)
        ListIDs = New List(Of String)
        Me.title = title
        Me.itemID = itemID
        Me.icon = icon
        Me.howManyItems = 0
    End Sub
    ''' <summary>
    ''' Este metodo agrega un item al dropdown. El id de este item es generado automaticamente por:
    ''' itemID + '_' + id
    ''' </summary>
    ''' <param name="text">Texto que tendrá el item</param>
    ''' <param name="url">Url a donde navegrá cuando se haga clic</param>
    ''' <param name="id">Id de este item</param>
    Public Sub AddItem(text As String, url As String, id As String)
        howManyItems += 1
        ListTexts.Add(text)
        ListUrls.Add(url)
        ListIDs.Add(id)
    End Sub

    Public Function GenerateDropDownItem() As String
        Dim nav_item As String = "<li class='nav-item dropdown bottom-border' id='{0}' runat='server'>"
        Dim dropdown_toggle As String = "<a class='nav-link dropdown-toggle' href='#' id='{0}Dropdown' role='button' data-toggle='dropdown' aria-haspopup='true' aria-expanded='false'>"
        Dim icon_Title As String = "<i class='material-icons' style='font-size: 1em'>{0}</i> {1}"
        Dim dropdown_menu As String = "<div class='dropdown-menu menu-item' aria-labelledby='{0}Dropdown'>"
        Dim dropdown_item As String = " <a Class='dropdown-item' runat='server' id='Menu_{0}_{1}' href='{2}'>{3}</a>"

        Dim menu_item As New StringBuilder
        menu_item.AppendFormat(nav_item, itemID)

        menu_item.AppendFormat(dropdown_toggle, itemID)
        menu_item.AppendFormat(icon_Title, icon, title)
        menu_item.Append("</a>")

        menu_item.AppendFormat(dropdown_menu, itemID)
        For i As Integer = 0 To howManyItems - 1
            menu_item.AppendFormat(dropdown_item, itemID, ListIDs(i), ListUrls(i), ListTexts(i))
        Next
        menu_item.Append("</div>")
        menu_item.Append("</li>")
        'Al final nos queeda un elemento parecido a este:
        '
        '<li Class= 'nav-item dropdown bottom-border' id='idMenu' runat='server'>
        '  <a Class='nav-link dropdown-toggle' href='#' id='idMenuDropdown' role='button' data-toggle='dropdown' aria-haspopup='true' aria-expanded='false'>
        '    <i Class='material-icons' style='font-size: 1em'>&#xe02f; <-- Icon </i> Title
        '  </a>
        '  <div Class='dropdown-menu menu-item' aria-labelledby='idMenuDropdown'>
        '    <a Class='dropdown-item' runat='server' id='Menu_idMenu_idItem' href='./url1.aspx'>Item 1</a>
        '     .
        '     .
        '     .
        '    <a Class='dropdown-item' runat='server' id='Menu_idMenu_idItem' href='./urlN.aspx'>Item N</a>
        '  </div>
        '</li>
        Return menu_item.ToString
    End Function
End Class
