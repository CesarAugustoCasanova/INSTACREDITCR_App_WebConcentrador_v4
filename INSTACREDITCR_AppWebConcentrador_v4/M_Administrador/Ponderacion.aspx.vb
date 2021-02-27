Imports System.Data
Imports System.Data.SqlClient
Imports Db
Imports Funciones
Partial Class Ponderacion
    Inherits System.Web.UI.Page
    Dim ArregloBotonesDesde(100) As String
    Dim ArregloBotonesHasta(100) As String
    Dim TmpArreglo(100) As String
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim Usr As String = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
        Catch ex As Exception
            OffLine(HidenUrs.Value)
            AUDITORIA(HidenUrs.Value, "Administrador", "Ponderacion", "", "Page_Load", "", Request.ServerVariables("REMOTE_ADDR"), Request.ServerVariables("REMOTE_HOST"))
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/SesionExpirada.aspx")
        End Try
        Try
            If Not IsPostBack Then
                HidenUrs.Value = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
                If CType(Session("USUARIOADMIN"), USUARIO).CAT_LO_MADMINISTRADOR.ToString.Substring(3, 1) = 0 Then
                    OffLine(HidenUrs.Value)
                    Session.Clear()
                    Session.Abandon()
                    Response.Redirect("~/SesionExpirada.aspx")
                End If
                Session("ArregloBotonesDesde") = Nothing
                Session("ArregloBotonesHasta") = Nothing
                If CType(Session("Aplicacion"), Aplicacion).ACCION = 1 Then
                    LlenarDrop(14, DdlProducto, "")
                Else
                    LlenarDrop(14, DdlProducto, "")
                    DdlAccion.Visible = False
                    LblAccion.Visible = False
                End If
            End If
        Catch ex As Exception
            SendMail("Page_Load", ex, "", "", HidenUrs.Value)
        End Try
    End Sub
    Protected Sub DdlProducto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlProducto.SelectedIndexChanged
        Session("ArregloBotonesDesde") = Nothing
        Session("ArregloBotonesHasta") = Nothing
        Limpiar(1)
        If DdlProducto.SelectedValue <> "Seleccione" Then
            If CType(Session("Aplicacion"), Aplicacion).ACCION = 1 Then
                LlenarDrop(13, DdlAccion, DdlProducto.SelectedValue)
                DdlAccion.Visible = True
                LblAccion.Visible = True
            Else
                'LlenarDrop(9, DdlAccion, "")
                GvPonderacionHasta.DataSource = Nothing
                GvPonderacionHasta.DataBind()
                Session("ArregloBotonesDesde") = Nothing
                Session("ArregloBotonesHasta") = Nothing
                ImgArriba.Visible = False
                ImgAbajo.Visible = False

                dim SSCommand as new sqlcommand
                SSCommand.CommandText = "SP_CATALOGOS"
                SSCommand.CommandType = CommandType.StoredProcedure
                SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 22
                SSCommand.Parameters.Add("@V_Valor", SqlDbType.NVarChar).Value = DdlProducto.SelectedValue

                Dim DtsCodigos As DataTable = Consulta_Procedure(SSCommand, "Codigos")

                For indice As Integer = 0 To DtsCodigos.Rows.Count - 1
                    ArregloBotonesDesde(indice) = DtsCodigos.Rows(indice)("Descripcion")
                Next
                Session("ArregloBotonesDesde") = ArregloBotonesDesde
                Reconstruir(GvPonderacionDesde, Session("ArregloBotonesDesde"))
            End If
        Else
            Limpiar(1)
        End If
    End Sub
    Protected Sub Bnt_clik_Desde(sender As Object, e As System.EventArgs)
        Try
            Try
                Dim USR As String = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
                HidenUrs.Value = USR
            Catch ex As Exception
                OffLine(HidenUrs.Value)
                Session.Clear()
                Session.Abandon()
                Response.Redirect("~/LogOn.aspx")
            End Try
            If Not IsNothing(Session("ArregloBotonesHasta")) Then
                ArregloBotonesHasta = Session("ArregloBotonesHasta")
            End If
            ArregloBotonesDesde = Session("ArregloBotonesDesde")

            Dim Quien As Button = DirectCast(sender, Button)
            Dim grdRow As GridViewRow = DirectCast(Quien.NamingContainer, GridViewRow)
            Dim Codigo As String = Quien.Text.ToString
            For indice As Integer = 0 To 99
                If IsNothing(ArregloBotonesHasta(indice)) Then
                    ArregloBotonesHasta(indice) = Codigo
                    Exit For
                End If
            Next
            Dim Posicion As Integer
            For indice As Integer = 0 To 99
                If ArregloBotonesDesde(indice) <> Codigo Then
                    TmpArreglo(Posicion) = ArregloBotonesDesde(indice)
                    Posicion = Posicion + 1
                End If
            Next
            ImgAbajo.Visible = False
            ImgArriba.Visible = False
            BtnPonderarDesde.Visible = False
            BtnPonderarHasta.Visible = False
            Session("ArregloBotonesDesde") = TmpArreglo
            Session("ArregloBotonesHasta") = ArregloBotonesHasta
            Reconstruir(GvPonderacionDesde, Session("ArregloBotonesDesde"))
            Reconstruir(GvPonderacionHasta, Session("ArregloBotonesHasta"))
        Catch ex As Exception
            Aviso(ex.Message
            )
        End Try
    End Sub
    Protected Sub Bnt_clik_Hasta(sender As Object, e As System.EventArgs)
        Try
            If Not IsNothing(Session("ArregloBotonesHasta")) Then
                ArregloBotonesHasta = Session("ArregloBotonesHasta")
            End If
            ArregloBotonesDesde = Session("ArregloBotonesDesde")

            Dim Quien As Button = DirectCast(sender, Button)
            Dim grdRow As GridViewRow = DirectCast(Quien.NamingContainer, GridViewRow)
            Dim Codigo As String = Quien.Text.ToString
            For indice As Integer = 0 To 99
                If IsNothing(ArregloBotonesDesde(indice)) Then
                    ArregloBotonesDesde(indice) = Codigo
                    Exit For
                End If
            Next
            Dim Posicion As Integer
            For indice As Integer = 0 To 99
                If ArregloBotonesHasta(indice) <> Codigo Then
                    TmpArreglo(Posicion) = ArregloBotonesHasta(indice)
                    Posicion = Posicion + 1
                End If
            Next
            ImgAbajo.Visible = False
            ImgArriba.Visible = False
            BtnPonderarDesde.Visible = False
            BtnPonderarHasta.Visible = False
            Session("ArregloBotonesDesde") = ArregloBotonesDesde
            Session("ArregloBotonesHasta") = TmpArreglo
            Reconstruir(GvPonderacionDesde, Session("ArregloBotonesDesde"))
            Reconstruir(GvPonderacionHasta, Session("ArregloBotonesHasta"))
        Catch ex As Exception
            Aviso(ex.Message
            )
        End Try
    End Sub
    Sub Reconstruir(ByVal Quien As GridView, ByVal Sesion As Object)
        Dim dtDatos As DataTable = New DataTable()
        dtDatos.Columns.Add("1")
        Dim Lleno As Integer = 0
        Dim cuantasC As Integer = 0
        Dim dtRow As DataRow
        dtRow = dtDatos.NewRow()
        For indice As Integer = 0 To 100
            If Sesion(indice) Is Nothing Then
                Exit For
            Else
                Lleno = 1
                dtRow.Item(cuantasC) = Sesion(indice).ToString
                dtDatos.Rows.Add(dtRow)
                cuantasC = 0
                dtRow = dtDatos.NewRow()
            End If
        Next
        If Lleno = 1 Then
            Quien.DataSource = dtDatos
            Quien.DataBind()
            Quien.HeaderRow.Cells(0).Visible = False
            Ocultar(Quien)
            If Quien.ID = "GvPonderacionDesde" Then
                BtnPonderarHasta.Visible = False

            Else
                BtnPonderarDesde.Visible = False
            End If
        Else
            If Quien.ID = "GvPonderacionHasta" Then
                BtnPonderarDesde.Visible = True
                BtnPonderarHasta.Visible = False

            Else
                BtnPonderarDesde.Visible = False
                BtnPonderarHasta.Visible = True

            End If
            Quien.DataSource = Nothing
            Quien.DataBind()
        End If
    End Sub
    Sub LlenarDrop(ByVal Opcion As Integer, ByVal Drop As DropDownList, ByVal Valor As String)
        dim SSCommand as new sqlcommand
        SSCommand.CommandText = "SP_CATALOGOS"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = Opcion
        SSCommand.Parameters.Add("@V_Valor", SqlDbType.NVarChar).Value = Valor

        Dim DtsCodigo As DataTable = Consulta_Procedure(SSCommand, "Drop")
        If DtsCodigo.Rows.Count <= 0 Then
            Aviso("No Existen Resultados"
            )
            Drop.Items.Clear()
        Else
            Drop.DataTextField = "Descripcion"
            Drop.DataValueField = "Valor"
            Drop.DataSource = DtsCodigo
            Drop.DataBind()
            Drop.Items.Add("Seleccione")
            Drop.SelectedValue = "Seleccione"
        End If
    End Sub
    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Administrador", "Ponderacion.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub
    Sub Ocultar(ByVal Quien As GridView)
        Dim Cbx1 As CheckBox
        For Each gvRow As GridViewRow In Quien.Rows
            Dim Button1 As Button = DirectCast(gvRow.FindControl("Button1"), Button)
            If Quien.ID = "GvPonderacionDesde" Then
                Cbx1 = DirectCast(gvRow.FindControl("Cbx1"), CheckBox)
            End If
            If Button1.Text.ToString = "" Then
                Button1.Visible = False
                If Quien.ID = "GvPonderacionDesde" Then
                    Cbx1.Visible = False
                End If
            Else
                If Quien.ID = "GvPonderacionDesde" Then
                    Cbx1.Text = Button1.Text
                End If
            End If
        Next
    End Sub
    Sub LLenarVariable()
        dim SSCommand as new sqlcommand
        SSCommand.CommandText = "SP_CATALOGOS"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 12
        SSCommand.Parameters.Add("@V_Valor", SqlDbType.NVarChar).Value = DdlAccion.SelectedValue & DdlProducto.SelectedValue

        Dim DtsCodigos As DataTable = Consulta_Procedure(SSCommand, "Codigos")
        For indice As Integer = 0 To DtsCodigos.Rows.Count - 1
            ArregloBotonesDesde(indice) = DtsCodigos.Rows(indice)("Descripcion")
        Next
        Session("ArregloBotonesDesde") = ArregloBotonesDesde
    End Sub
    Protected Sub DdlAccion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlAccion.SelectedIndexChanged
        GvPonderacionHasta.DataSource = Nothing
        GvPonderacionHasta.DataBind()
        Session("ArregloBotonesDesde") = Nothing
        Session("ArregloBotonesHasta") = Nothing
        ImgArriba.Visible = False
        ImgAbajo.Visible = False
        If DdlAccion.SelectedValue <> "Seleccione" Then
            LLenarVariable()
            Reconstruir(GvPonderacionDesde, Session("ArregloBotonesDesde"))
        Else
            Limpiar(1)
        End If
    End Sub
    Sub Limpiar(ByVal Opcion As Integer)
        If Opcion = 1 Then
            Session("ArregloBotonesHasta") = Nothing
            Session("ArregloBotonesDesde") = Nothing
            GvPonderacionDesde.DataSource = Nothing
            GvPonderacionHasta.DataSource = Nothing
            GvPonderacionDesde.DataBind()
            GvPonderacionHasta.DataBind()
            DdlAccion.Visible = False
            LblAccion.Visible = False
            BtnPonderarDesde.Visible = False
            BtnPonderarHasta.Visible = False
            ImgArriba.Visible = False
            ImgAbajo.Visible = False
        ElseIf Opcion = 2 Then
            GvPonderacionDesde.DataSource = Nothing
            GvPonderacionHasta.DataSource = Nothing
            GvPonderacionDesde.DataBind()
            GvPonderacionHasta.DataBind()
            DdlAccion.Visible = False
            LblAccion.Visible = False
            DdlProducto.SelectedValue = "Seleccione"
            BtnPonderarDesde.Visible = False
            BtnPonderarHasta.Visible = False
            ImgArriba.Visible = False
            ImgAbajo.Visible = False
        End If
    End Sub
    Protected Sub BtnPonderar_Click(sender As Object, e As EventArgs) Handles BtnPonderarHasta.Click
        Dim Bandera As Integer = 0
        Dim Orden As Integer = 1
        If CType(Session("Aplicacion"), Aplicacion).ACCION = 1 Then
            Bandera = 1
        End If
        ArregloBotonesHasta = Session("ArregloBotonesHasta")
        If Not IsNothing(ArregloBotonesHasta) Then
            For indice As Integer = 0 To 99
                dim SSCommand as new sqlcommand
                SSCommand.CommandText = "SP_Pondera"
                SSCommand.CommandType = CommandType.StoredProcedure
                SSCommand.Parameters.Add("@V_Producto", SqlDbType.NVarChar).Value = DdlProducto.Text
                SSCommand.Parameters.Add("@V_Accion", SqlDbType.NVarChar).Value = DdlAccion.Text
                SSCommand.Parameters.Add("@V_Resultado", SqlDbType.NVarChar).Value = ArregloBotonesHasta(indice)
                SSCommand.Parameters.Add("@V_Orden", SqlDbType.Decimal).Value = Orden
                SSCommand.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = Bandera
                Ejecuta_Procedure(SSCommand)
                Orden = Orden + 1
            Next
        End If
        Aviso("Códigos Ponderados"
        )
        Limpiar(2)
    End Sub
    Protected Sub Cbx_(sender As Object, e As System.EventArgs)
        Dim Posicion As Integer = 0
        ArregloBotonesDesde = Session("ArregloBotonesDesde")
        ArregloBotonesHasta = Session("ArregloBotonesHasta")

        If Not IsNothing(ArregloBotonesHasta) Then
            For indice As Integer = 0 To 99
                If IsNothing(ArregloBotonesDesde(indice)) Then
                    Posicion = indice
                    Exit For
                End If
            Next

            For indice As Integer = 0 To 99
                If IsNothing(ArregloBotonesHasta(indice)) Then
                    Exit For
                Else
                    ArregloBotonesDesde(Posicion) = ArregloBotonesHasta(indice)
                    Posicion = Posicion + 1
                End If
            Next
            If Not IsNothing(ArregloBotonesHasta(0)) Then
                Session("ArregloBotonesDesde") = ArregloBotonesDesde
                Reconstruir(GvPonderacionDesde, Session("ArregloBotonesDesde"))
            End If
        End If

        GvPonderacionHasta.DataSource = Nothing
        GvPonderacionHasta.DataBind()
        Session("ArregloBotonesHasta") = Nothing
        Dim Quien As CheckBox = DirectCast(sender, CheckBox)
        Session("Chb") = Quien.Text
        For Each gvRow As GridViewRow In GvPonderacionDesde.Rows
            For indice As Integer = 0 To 0
                Dim chkSelect As CheckBox = DirectCast(gvRow.FindControl("cbx" & indice + 1), CheckBox)
                If chkSelect.Text <> Quien.Text Then
                    chkSelect.Checked = False
                End If
            Next
        Next
        If Quien.Checked = True Then
            ImgAbajo.Visible = True
            ImgArriba.Visible = True
            BtnPonderarDesde.Visible = True
        Else
            ImgAbajo.Visible = False
            ImgArriba.Visible = False
            BtnPonderarHasta.Visible = False
        End If
    End Sub

    Protected Sub ImgAbajo_Click(sender As Object, e As ImageClickEventArgs) Handles ImgAbajo.Click
        If Session("Chb") = Nothing Then
            Aviso("Seleccione Elemento A Mover"
            )
        Else
            ArregloBotonesDesde = Session("ArregloBotonesDesde")
            Dim CodTemporal1 As String = ""
            Dim CodTemporal2 As String = ""
            For indice As Integer = 0 To 99
                If ArregloBotonesDesde(indice) = Session("Chb") Then
                    If IsNothing(ArregloBotonesDesde(indice + 1)) Then
                        Aviso("Imposible Mover,Se Encuentra En El Último Nivel"
                        )
                        Exit For
                    Else
                        CodTemporal1 = ArregloBotonesDesde(indice)
                        CodTemporal2 = ArregloBotonesDesde(indice + 1)

                        ArregloBotonesDesde(indice) = CodTemporal2
                        ArregloBotonesDesde(indice + 1) = CodTemporal1
                        Exit For
                    End If
                    Exit For
                End If
            Next
            Session("ArregloBotonesDesde") = ArregloBotonesDesde
            Reconstruir(GvPonderacionDesde, Session("ArregloBotonesDesde"))
            For Each gvRow As GridViewRow In GvPonderacionDesde.Rows
                For indice As Integer = 0 To 0
                    Dim chkSelect As CheckBox = DirectCast(gvRow.FindControl("cbx" & indice + 1), CheckBox)
                    If chkSelect.Text = Session("Chb") Then
                        chkSelect.Checked = True
                    End If
                Next
            Next
        End If
    End Sub

    Protected Sub ImgArriba_Click(sender As Object, e As ImageClickEventArgs) Handles ImgArriba.Click
        If Session("Chb") = Nothing Then
            Aviso("Seleccione Elemento A Mover"
            )
        Else
            ArregloBotonesDesde = Session("ArregloBotonesDesde")
            Dim CodTemporal1 As String = ""
            Dim CodTemporal2 As String = ""
            For indice As Integer = 0 To 99
                If ArregloBotonesDesde(indice) = Session("Chb") Then
                    If indice = 0 Then
                        Aviso("Imposible Mover,Se Encuentra En El Primer Nivel"
                        )
                        Exit For
                    Else
                        CodTemporal1 = ArregloBotonesDesde(indice)
                        CodTemporal2 = ArregloBotonesDesde(indice - 1)

                        ArregloBotonesDesde(indice) = CodTemporal2
                        ArregloBotonesDesde(indice - 1) = CodTemporal1
                        Exit For
                    End If
                    Exit For
                End If
            Next
            Session("ArregloBotonesDesde") = ArregloBotonesDesde
            Reconstruir(GvPonderacionDesde, Session("ArregloBotonesDesde"))
            For Each gvRow As GridViewRow In GvPonderacionDesde.Rows
                For indice As Integer = 0 To 0
                    Dim chkSelect As CheckBox = DirectCast(gvRow.FindControl("cbx" & indice + 1), CheckBox)
                    If chkSelect.Text = Session("Chb") Then
                        chkSelect.Checked = True
                    End If
                Next
            Next
        End If
    End Sub

    Protected Sub BtnPonderarDesde_Click(sender As Object, e As EventArgs) Handles BtnPonderarDesde.Click
        Try
            Dim Bandera As Integer = 0
            Dim Orden As Integer = 1
            If CType(Session("Aplicacion"), Aplicacion).ACCION = 1 Then
                Bandera = 1
            End If
            ArregloBotonesDesde = Session("ArregloBotonesDesde")
            For indice As Integer = 0 To 99
                If Not IsNothing(ArregloBotonesDesde(indice)) Then
                    dim SSCommand as new sqlcommand
                    SSCommand.CommandText = "SP_Pondera"
                    SSCommand.CommandType = CommandType.StoredProcedure
                    SSCommand.Parameters.Add("@V_Producto", SqlDbType.NVarChar).Value = DdlProducto.SelectedValue
                    SSCommand.Parameters.Add("@V_Accion", SqlDbType.NVarChar).Value = DdlAccion.SelectedValue
                    SSCommand.Parameters.Add("@V_Resultado", SqlDbType.NVarChar).Value = ArregloBotonesDesde(indice)
                    SSCommand.Parameters.Add("@V_Orden", SqlDbType.Decimal).Value = Orden
                    SSCommand.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = Bandera
                    Ejecuta_Procedure(SSCommand)
                    Orden = Orden + 1
                Else
                    Exit For
                End If
            Next
            Aviso("Códigos Ponderados"
            )
            Limpiar(2)
        Catch ex As Exception
            Aviso(ex.Message
            )
        End Try
    End Sub
    Protected Sub Aviso(ByVal MSJ As String)
        RadAviso.RadAlert(MSJ, 440, 155, "AVISO", Nothing)
    End Sub
End Class
