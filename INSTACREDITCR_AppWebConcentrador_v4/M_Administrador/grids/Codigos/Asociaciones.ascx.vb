
Imports System.Data
Imports System.Web.Services
Imports Telerik.Web.UI

Partial Class M_Administrador_grids_Codigos_Asociaciones
    Inherits System.Web.UI.UserControl
    Private _dataItem As Object = Nothing
    Public Property DataItem() As Object
        Get
            Return Me._dataItem
        End Get
        Set(ByVal value As Object)
            Me._dataItem = value
        End Set
    End Property

    Private Sub acbAccion_EntryAdded(sender As Object, e As AutoCompleteEntryEventArgs) Handles acbAccion.EntryAdded
        lblSelectedAccion.Text = e.Entry.Text
        lblSelectedAccion.Attributes.Add("Codigo", e.Entry.Value)
        lblSelectedAccion.Attributes.Add("Descripcion", e.Entry.Text)
        lblSelectedAccion.Visible = True
        acbAccion.Visible = False
        pnlSelectedAccion.Visible = True
        pnlTipoAsociacion.Visible = False

        comboResultados.Enabled = True
        comboResultados.ClearCheckedItems()
        comboResultados.ClearSelection()
        comboResultados.Items.Clear()

        Dim datos As DataTable
        If btnAccionResultados.Checked = True Then
            datos = SP.ADD_CODIGOS(V_Bandera:=117, V_Cat_Co_Accion:=e.Entry.Value)
        Else
            datos = SP.ADD_CODIGOS(V_Bandera:=118, V_Cat_Co_Accion:=e.Entry.Value)
        End If
        For Each row As DataRow In datos.Rows
            Dim item As New RadComboBoxItem(row("TEXTO"), row("CODIGO")) With {
                .Enabled = (row("USADO") = 0)
            }
            item.Attributes.Add("Codigo", row("CODIGO"))
            item.Attributes.Add("Descripcion", row("DESCRIPCION"))

            comboResultados.Items.Add(item)
        Next
        comboResultados.DataBind()
    End Sub

    Private Sub btnCancelSelectedAccion_Click(sender As Object, e As EventArgs) Handles btnCancelSelectedAccion.Click
        lblSelectedAccion.Text = ""
        lblSelectedAccion.Attributes.Clear()
        acbAccion.Visible = True
        acbAccion.Entries.Clear()
        pnlSelectedAccion.Visible = False
        pnlTipoAsociacion.Visible = True

        comboResultados.ClearCheckedItems()
        comboResultados.ClearSelection()
        comboResultados.Items.Clear()
        comboResultados.Enabled = False
    End Sub

    Private Sub M_Administrador_grids_Codigos_Asociaciones_Init(sender As Object, e As EventArgs) Handles Me.Init
        If Session("EditAsociacion") Then
            Session.Remove("EditAsociacion")

            lblSelectedAccion.Visible = True
            acbAccion.Visible = False
            acbAccion.Enabled = False
            btnCancelSelectedAccion.Visible = False
            pnlSelectedAccion.Visible = True
            pnlTipoAsociacion.Visible = False
            Dim allData = SP.ADD_CODIGOS(V_Bandera:=116, V_Cat_Co_Id:=DataItem("id")).Rows(0)

            lblSelectedAccion.Text = allData("CAT_CO_ACCION").ToString() & ": " & allData("CAT_CO_ADESCRIPCION").ToString()
            lblSelectedAccion.Attributes.Add("Codigo", allData("CAT_CO_ACCION").ToString())
            lblSelectedAccion.Attributes.Add("Descripcion", allData("CAT_CO_ADESCRIPCION").ToString())

            Dim selectedItem As New RadComboBoxItem(allData("CAT_CO_RDESCRIPCION").ToString(), allData("CAT_CO_RESULTADO").ToString()) With {
                .Checked = True
            }
            selectedItem.Attributes.Add("Codigo", allData("CAT_CO_RESULTADO").ToString())
            selectedItem.Attributes.Add("Descripcion", allData("CAT_CO_RDESCRIPCION").ToString())

            comboResultados.Items.Add(selectedItem)

            'Item facherito. NO BORRAR
            comboResultados.Items.Add(New RadComboBoxItem("", ""))
            comboResultados.DataBind()


            cbPromesa.Checked = (allData("CAT_CO_CONFIGURACION").ToString = "1")
            cbSignificativo.Checked = (allData("CAT_CO_SIGNIFICATIVO").ToString = "1")
            cbTelefonico.Checked = (allData("CAT_CO_TIPO").ToString = "1") Or (allData("CAT_CO_TIPO").ToString = "3")
            cbVisitador.Checked = (allData("CAT_CO_TIPO").ToString = "2") Or (allData("CAT_CO_TIPO").ToString = "3")

            txtSemaforoVerde.Text = IIf(allData("CAT_CO_VERDE").ToString = "", 0, allData("CAT_CO_VERDE").ToString)
            txtSemaforoAmarillo.Text = IIf(allData("CAT_CO_AMARILLO").ToString = "", 0, allData("CAT_CO_AMARILLO").ToString)

            Dim allPerfiles = SP.CATALOGOS(v_bandera:=8)
            Dim perfilesBits = allData("CAT_CO_PERFILES").ToString

            For numPerfil = 0 To allPerfiles.Rows().Count - 1
                Dim item As New RadComboBoxItem(allPerfiles.Rows(numPerfil)(0).ToString, allPerfiles.Rows(numPerfil)(0).ToString)
                Try
                    Dim value = perfilesBits.Substring(numPerfil, 1)
                    item.Checked = (value = "1")
                Catch ex As Exception

                End Try
                comboPerfiles.Items.Add(item)
            Next

            Dim allProductos = SP.CATALOGOS(v_bandera:=9)
            Dim productosBits = allData("CAT_CO_PRODUCTO").ToString

            For numProducto = 0 To allProductos.Rows().Count - 1
                Dim item As New RadComboBoxItem(allProductos.Rows(numProducto)(0).ToString, allProductos.Rows(numProducto)(0).ToString)
                Try
                    Dim value = perfilesBits.Substring(numProducto, 1)
                    item.Checked = (value = "1")
                Catch ex As Exception

                End Try
                comboProductos.Items.Add(item)
            Next

            comboPerfiles.DataBind()
            comboProductos.DataBind()


        ElseIf Session("NewAsociacion") Then
            Session.Remove("NewAsociacion")


            comboProductos.DataSource = SP.CATALOGOS(v_bandera:=9)
            comboProductos.DataTextField = "Productos"
            comboProductos.DataValueField = "Productos"
            comboProductos.DataBind()

            comboPerfiles.DataSource = SP.CATALOGOS(v_bandera:=8)
            comboPerfiles.DataTextField = "Perfiles"
            comboPerfiles.DataValueField = "Perfiles"
            comboPerfiles.DataBind()
        End If
    End Sub

    Private Sub btnAccionResultados_ToggleStateChanged(sender As Object, e As ButtonToggleStateChangedEventArgs) Handles btnAccionResultados.ToggleStateChanged
        Dim auxText = lblTipoCodigo1.Text
        lblTipoCodigo1.Text = lblTipoCodigo2.Text
        lblTipoCodigo2.Text = auxText
    End Sub

    Private Sub acbAccion_Load(sender As Object, e As EventArgs) Handles acbAccion.Load
        Dim data As DataTable
        If btnAccionResultados.Checked Then
            data = SP.ADD_CODIGOS(V_Bandera:=101)
        Else
            data = SP.ADD_CODIGOS(V_Bandera:=102)
        End If
        acbAccion.DataSource = data
        acbAccion.DataTextField = "Descripcion"
        acbAccion.DataValueField = "Codigo"

        'Dim result As New List(Of AutoCompleteBoxItemData)()
        'For Each row As DataRow In data.Rows
        '    Dim entrie As New AutoCompleteBoxItemData()
        '    entrie.Text = row("Codigo") & ": " & row("Descripcion")
        '    entrie.Value = row("Codigo")
        '    entrie.Attributes.Add("Descripcion", row("Descripcion"))
        '    entrie.Attributes.Add("Codigo", row("Codigo"))
        'Next
        'Try
        '    acbAccion.DataSource = result.ToArray()

        'Catch ex As Exception
        '    acbAccion.DataSource = result

        'End Try
        'acbAccion.DataTextField = "Text"
        'acbAccion.DataValueField = "Value"
        'acbAccion.DataBind()
    End Sub

End Class
