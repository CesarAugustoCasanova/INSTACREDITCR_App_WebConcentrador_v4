Imports System.Web.Services
Imports Telerik.Web.UI
Imports System.Data
Imports Funciones

Partial Class CreacionJuicios
    Inherits System.Web.UI.Page

    Public Property tmpUSUARIO As IDictionary
        Get
            Return CType(Session("USUARIO"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("USUARIO") = value
        End Set
    End Property
    Public Property tmpPermisos As IDictionary
        Get
            Return CType(Session("Permisos"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("Permisos") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If tmpPermisos("CONFIG_JUICIOS") = False Then
                Session.Clear()
                Session.Abandon()
                Response.Redirect("~/SesionExpirada.aspx")
            Else

                Try
                    If Not IsPostBack Then
                        ConfigureListBoxSource()
                        ConfigureListBoxDestination()
                        llenarJuicios()
                        Session("currentStep") = 0
                        HidenUrs.Value = (CType(Session("USUARIOADMIN"), USUARIO)).CAT_LO_USUARIO
                    End If
                Catch ex As Exception
                    SendMail("Page_Load", ex, "", "", HidenUrs.Value)
                End Try
            End If
    End Sub

    Private Sub ConfigureListBoxSource()
        RadListBoxSource.AllowTransfer = True
        RadListBoxSource.TransferMode = ListBoxTransferMode.Move
        RadListBoxSource.AllowTransferDuplicates = False
        RadListBoxSource.AllowReorder = True
        RadListBoxSource.ClearSelection()
        RadListBoxSource.SelectionMode = ListBoxSelectionMode.[Single]
        RadListBoxSource2.AllowTransfer = True
        RadListBoxSource2.TransferMode = ListBoxTransferMode.Move
        RadListBoxSource2.AllowTransferDuplicates = False
        RadListBoxSource2.AllowReorder = True
        RadListBoxSource2.ClearSelection()
        RadListBoxSource2.SelectionMode = ListBoxSelectionMode.[Single]
        RadListBoxSource3.AllowTransfer = True
        RadListBoxSource3.TransferMode = ListBoxTransferMode.Move
        RadListBoxSource3.AllowTransferDuplicates = False
        RadListBoxSource3.AllowReorder = True
        RadListBoxSource3.ClearSelection()
        RadListBoxSource3.SelectionMode = ListBoxSelectionMode.[Single]
    End Sub
    Private Sub ConfigureListBoxDestination()
        RadListBoxDestination.AllowReorder = True
        RadListBoxDestination.AllowDelete = True
        RadListBoxDestination2.AllowReorder = True
        RadListBoxDestination2.AllowDelete = True
        RadListBoxDestination3.AllowReorder = True
        RadListBoxDestination3.AllowDelete = True
    End Sub
    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Administrador", "CreacionJuicios.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub
    Protected Sub Aviso(ByVal MSJ As String)
        RadAviso.RadAlert(MSJ, 440, 155, "AVISO", Nothing)
    End Sub

    Private Sub RadWizard1_FinishButtonClick(sender As Object, e As WizardEventArgs) Handles RadWizard1.FinishButtonClick
        RLBNombres.SelectedIndex = -1
        RadWizard1.ActiveStepIndex = 0
        Aviso("Juicio guardado correctamente")
        Limpiar()
    End Sub

    Private Sub RadWizard1_ActiveStepChanged(sender As Object, e As EventArgs) Handles RadWizard1.ActiveStepChanged
        Dim wizard As RadWizard = TryCast(sender, RadWizard)
        Dim nextStep As Integer = wizard.ActiveStepIndex
        Dim currentStep As Integer = Session("currentStep")
        Select Case nextStep
            Case 1
                If RLBNombres.SelectedIndex = -1 Then
                    RadWizard1.ActiveStepIndex = 0
                    Aviso("Selecciona un juicio")
                End If
            Case 2
                If RLBNombres.SelectedIndex = -1 Then
                    RadWizard1.ActiveStepIndex = 0
                    Aviso("Selecciona un juicio")
                ElseIf RadListBoxDestination.Items.Count = 0 Then
                    RadWizard1.ActiveStepIndex = Session("currentStep")
                    Aviso("Asigna etapas para poder continuar con las promociones")
                End If
            Case 3
                If RLBNombres.SelectedIndex = -1 Then
                    RadWizard1.ActiveStepIndex = 0
                    Aviso("Selecciona un juicio")
                ElseIf RadListBoxDestination.Items.Count = 0 Then
                    RadWizard1.ActiveStepIndex = Session("currentStep")
                    Aviso("Asigna etapas para poder continuar con los acuerdos")
                End If

            Case 4
                If RLBNombres.SelectedIndex = -1 Then
                    RadWizard1.ActiveStepIndex = 0
                    Aviso("Selecciona un juicio")
                ElseIf Session("currentStep") <> 3 Then
                    If RadListBoxDestination.Items.Count = 0 Then
                        RadWizard1.ActiveStepIndex = 1
                        nextStep = 1
                        Aviso("Asigna etapas para poder continuar")
                    Else
                        RadWizard1.ActiveStepIndex = 3
                        nextStep = 3
                        Aviso("Revisa los acuerdos antes de continuar con los resultados")
                    End If
                End If
        End Select

        Session("currentStep") = nextStep
    End Sub
    Private Sub Limpiar()
        RadListBoxDestination.ClearSelection()
        RadListBoxDestination.Items.Clear()
        RadListBoxDestination2.ClearSelection()
        RadListBoxDestination2.Items.Clear()
        RadListBoxDestination3.ClearSelection()
        RadListBoxDestination3.Items.Clear()
        RadListBoxDestination4.ClearSelection()
        RadListBoxDestination4.Items.Clear()

        RadListBoxSource.ClearSelection()
        RadListBoxSource.Items.Clear()
        RadListBoxSource2.ClearSelection()
        RadListBoxSource2.Items.Clear()
        RadListBoxSource3.ClearSelection()
        RadListBoxSource3.Items.Clear()
        RadListBoxSource4.ClearSelection()
        RadListBoxSource4.Items.Clear()
    End Sub
    Private Sub llenarJuicios()
        Dim juicios As DataTable = Class_Judicial.Llenarpasos(15, "", "")
        RLBNombres.DataSource = juicios
        RLBNombres.DataBind()
    End Sub
    Private Sub RLBNombres_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RLBNombres.SelectedIndexChanged
        Dim Seleccionado As String = RLBNombres.SelectedValue.ToString
        Dim Existentes As DataTable = Class_Judicial.Llenarpasos(18, "", Seleccionado)
        If Existentes.Rows(0).Item(0) = 0 Then
            Class_Judicial.Llenarpasos(17, "", Seleccionado)
            Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Creacion de Jucicios", "Juicio Introducido: " & "17" & "|" & "" & "|" & Seleccionado)
        End If
        llenarEtapas()
        RadWizard1.ActiveStepIndex = 1
        Session("currentStep") = 1
    End Sub

    Private Sub llenarEtapas()
        Dim dsetRes As DataTable = Class_Judicial.Llenarpasos(3, "", "")
        RadListBoxSource.DataSource = dsetRes
        RadListBoxSource.DataBind()
        Dim NDataTable As New DataTable
        Dim NDataTableResultados As New DataTable
        Dim NDataTableResultados2 As New DataTable
        Dim dsetExist As DataTable = Class_Judicial.Llenarpasos(16, "", RLBNombres.SelectedValue.ToString)
        For Each numero As String In dsetExist.Rows(0).Item(0).ToString.Split(",")
            Dim Llenado As DataTable = Class_Judicial.Llenarpasos(21, "", numero)
            NDataTable.Merge(Llenado)
        Next
        RadListBoxDestination.DataSource = NDataTable
        RadListBoxDestination.DataBind()
        RLBEtapas.DataSource = NDataTable
        RLBEtapas.DataBind()
        'RLBEtapas2.DataSource = NDataTable

        Dim EtapaPromo As String
        Dim Existentes2 As DataTable = Class_Judicial.Llenarpasos(40, "", RLBNombres.SelectedValue.ToString)
        If Existentes2.Rows(0).Item(0) <> 0 Then

            Dim dsetAcuerdo As DataTable = Class_Judicial.Llenarpasos(39, "", RLBNombres.SelectedValue.ToString)
            For Each registros As DataRow In dsetAcuerdo.Rows
                Session("ID") = registros(1).ToString
                Session("IDInicial") = Session("ID")
                Dim Sesion As DataTable = Class_Judicial.Llenarpasos(52, "", Session("ID"))
                Try
                    EtapaPromo = " - " & Sesion(0)(0)
                Catch ex As Exception

                End Try
                For Each numero As String In registros(0).ToString.Split(",")
                    If numero <> 0 Then
                        Dim Llenado2 As DataTable = Class_Judicial.Llenarpasos(53, "", numero)
                        Llenado2(0)(0) &= EtapaPromo
                        NDataTableResultados.Merge(Llenado2)
                    End If
                Next
            Next

            RLBEtapas2.DataSource = NDataTableResultados
            RLBEtapas2.DataBind()
        End If

        'RLBEtapas2.DataBind()
        Dim EtapaResultado As String
        Dim Existentes As DataTable = Class_Judicial.Llenarpasos(49, "", RLBNombres.SelectedValue.ToString)
        If Existentes.Rows(0).Item(0) <> 0 Then

            Dim dsetAcuerdo As DataTable = Class_Judicial.Llenarpasos(50, "", RLBNombres.SelectedValue.ToString)
            For Each registros As DataRow In dsetAcuerdo.Rows
                Session("ID") = registros(1).ToString
                Session("IDInicial") = Session("ID")
                'Dim NumeroID = Mid((registros(1).ToString), 3, 2)
                Dim Sesion As DataTable = Class_Judicial.Llenarpasos(99, "", registros(1).ToString)
                Dim Sesion2 As DataTable = Class_Judicial.Llenarpasos(55, "", registros(1).ToString)
                Try
                    EtapaResultado = " - " & Sesion2(0)(0) & " - " & Sesion(0)(0)
                Catch ex As Exception

                End Try
                For Each numero As String In registros(0).ToString.Split(",")
                    If numero <> 0 Then
                        Dim Llenado2 As DataTable = Class_Judicial.Llenarpasos(42, "", numero)
                        Llenado2(0)(0) &= EtapaResultado
                        NDataTableResultados2.Merge(Llenado2)
                    End If
                Next
            Next

            RLBEtapas3.DataSource = NDataTableResultados2
            RLBEtapas3.DataBind()
        End If

    End Sub

    'Private Sub RadListBoxDestination_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RadListBoxDestination.SelectedIndexChanged
    'guardaAsignarEtapas()
    'End Sub

    Private Sub guardaAsignarEtapas()
        Dim Resultados As String = ""
        For Each Item In RadListBoxDestination.Items
            Resultados = Resultados + Item.Value + ","
        Next
        If Resultados <> "" Then
            Resultados = Resultados.Substring(0, Resultados.Length - 1)
        End If
        Dim dtsresultado As DataTable = Class_Judicial.Llenarpasos(19, Resultados, RLBNombres.SelectedValue.ToString)
        Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Creacion de Jucicios", "Juicio Actualizado: " & "19" & "|" & Resultados & "|" & RLBNombres.SelectedValue.ToString)
        'Aviso(dtsresultado.Rows(0).Item(0))
        llenarEtapas()
    End Sub

    Private Sub RLBEtapas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RLBEtapas.SelectedIndexChanged
        'guardarPromociones()
        Session("IDJuicioEtapa") = RLBNombres.SelectedValue.ToString & "~" & RLBEtapas.SelectedValue.ToString
        Dim Existentes As DataTable = Class_Judicial.Llenarpasos(26, "", Session("IDJuicioEtapa"))
        If Existentes.Rows(0).Item(0) = 0 Then
            Class_Judicial.Llenarpasos(24, "", Session("IDJuicioEtapa"))
            Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Creacion de Jucicios", "Juicio Introducido: " & "24" & "|" & "" & "|" & Session("IDJuicioEtapa"))
        End If

        Dim dsetRes As DataTable = Class_Judicial.Llenarpasos(22, "", "")
        RadListBoxSource2.DataSource = dsetRes
        RadListBoxSource2.DataBind()

        Dim NDataTable As New DataTable
        Dim dsetExist As DataTable = Class_Judicial.Llenarpasos(23, "", Session("IDJuicioEtapa"))
        For Each numero As String In dsetExist.Rows(0).Item(0).ToString.Split(",")
            Dim Llenado As DataTable = Class_Judicial.Llenarpasos(28, "", numero)
            NDataTable.Merge(Llenado)
        Next
        RadListBoxDestination2.DataSource = NDataTable
        RadListBoxDestination2.DataBind()
    End Sub

    'Private Sub RadListBoxDestination2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RadListBoxDestination2.SelectedIndexChanged
    '    guardarPromociones()
    'End Sub

    Private Sub guardarPromociones()
        Dim Resultados As String = ""
        For Each Item In RadListBoxDestination2.Items
            Resultados = Resultados + Item.Value + ","
        Next
        If Resultados <> "" Then
            Resultados = Resultados.Substring(0, Resultados.Length - 1)
        End If
        Dim dtsresultado As DataTable = Class_Judicial.Llenarpasos(27, Resultados, Session("IDJuicioEtapa"))
        Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Creacion de Jucicios", "Juicio Actualizado: " & "27" & "|" & Resultados & "|" & Session("IDJuicioEtapa"))
        'Aviso(dtsresultado.Rows(0).Item(0))
        llenarEtapas()
    End Sub

    Private Sub RLBEtapas2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RLBEtapas2.SelectedIndexChanged
        RadListBoxSource3.ClearSelection()
        RadListBoxDestination3.ClearSelection()
        Dim TextoID = Split(RLBEtapas2.SelectedItem.Text, " - ")
        Dim NoID As DataTable = Class_Judicial.Llenarpasos(54, "", TextoID(1))
        Session("IDJuicioEtapaAcuerdo") = RLBNombres.SelectedValue.ToString & "~" & NoID(0)(0).ToString & "~" & RLBEtapas2.SelectedValue.ToString
        Dim Existentes As DataTable = Class_Judicial.Llenarpasos(33, "", Session("IDJuicioEtapaAcuerdo"))
        If Existentes.Rows(0).Item(0) = 0 Then
            Class_Judicial.Llenarpasos(31, "", Session("IDJuicioEtapaAcuerdo"))
            Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Creacion de Jucicios", "Juicio Introducido: " & "31" & "|" & "" & "|" & Session("IDJuicioEtapaAcuerdo"))
        End If


        Dim dsetRes As DataTable = Class_Judicial.Llenarpasos(29, "", "")
        RadListBoxSource3.DataSource = dsetRes
        RadListBoxSource3.DataBind()

        Dim NDataTable As New DataTable
        Dim dsetExist As DataTable = Class_Judicial.Llenarpasos(30, "", Session("IDJuicioEtapaAcuerdo"))
        For Each numero As String In dsetExist.Rows(0).Item(0).ToString.Split(",")
            Dim Llenado As DataTable = Class_Judicial.Llenarpasos(35, "", numero)
            NDataTable.Merge(Llenado)
        Next
        RadListBoxDestination3.DataSource = NDataTable
        RadListBoxDestination3.DataBind()
    End Sub

    'Private Sub RadListBoxDestination3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RadListBoxDestination3.SelectedIndexChanged
    '    guardarAcuerdos()
    'End Sub

    Private Sub guardarAcuerdos()
        Dim Resultados As String = ""
        For Each Item In RadListBoxDestination3.Items
            Resultados = Resultados + Item.Value + ","
        Next
        If Resultados <> "" Then
            Resultados = Resultados.Substring(0, Resultados.Length - 1)
        End If
        Dim dtsresultado As DataTable = Class_Judicial.Llenarpasos(34, Resultados, Session("IDJuicioEtapaAcuerdo"))
        Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Creacion de Jucicios", "Juicio Actualizado: " & "34" & "|" & Resultados & "|" & Session("IDJuicioEtapaAcuerdo"))
        'Aviso(dtsresultado.Rows(0).Item(0))
        llenarEtapas()
    End Sub

    Private Sub RLBEtapas3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RLBEtapas3.SelectedIndexChanged
        RadListBoxSource4.ClearSelection()
        RadListBoxDestination4.ClearSelection()
        Dim TextoID = Split(RLBEtapas3.SelectedItem.Text, " - ")
        Dim NoID As DataTable = Class_Judicial.Llenarpasos(54, "", TextoID(2))
        Dim NoID2 As DataTable = Class_Judicial.Llenarpasos(56, "", TextoID(1))
        Session("IDJuicioEtapaResultado") = RLBNombres.SelectedValue.ToString & "~" & NoID(0)(0).ToString & "~" & NoID2(0)(0).ToString & "~" & RLBEtapas3.SelectedValue.ToString
        Dim Existentes As DataTable = Class_Judicial.Llenarpasos(43, "", Session("IDJuicioEtapaResultado"))
        If Existentes.Rows(0).Item(0) = 0 Then
            Class_Judicial.Llenarpasos(44, "", Session("IDJuicioEtapaResultado"))
            Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Creacion de Jucicios", "Juicio Actualizado: " & "44" & "|" & "" & "|" & Session("IDJuicioEtapaResultado"))
        End If


        Dim dsetRes As DataTable = Class_Judicial.Llenarpasos(45, "", "")
        RadListBoxSource4.DataSource = dsetRes
        RadListBoxSource4.DataBind()

        Dim NDataTable As New DataTable
        Dim dsetExist As DataTable = Class_Judicial.Llenarpasos(46, "", Session("IDJuicioEtapaResultado"))
        For Each numero As String In dsetExist.Rows(0).Item(0).ToString.Split(",")
            Dim Llenado As DataTable = Class_Judicial.Llenarpasos(47, "", numero)
            NDataTable.Merge(Llenado)
        Next
        RadListBoxDestination4.DataSource = NDataTable
        RadListBoxDestination4.DataBind()
    End Sub

    'Private Sub RadListBoxDestination4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RadListBoxDestination4.SelectedIndexChanged
    '    guardarResultados()
    'End Sub

    Private Sub guardarResultados()
        Dim Resultados As String = ""
        For Each Item In RadListBoxDestination4.Items
            Resultados = Resultados + Item.Value + ","
        Next
        If Resultados <> "" Then
            Resultados = Resultados.Substring(0, Resultados.Length - 1)
        End If
        Dim dtsresultado As DataTable = Class_Judicial.Llenarpasos(48, Resultados, Session("IDJuicioEtapaResultado"))
        Class_Auditoria.GuardarValorAuditoria(tmpUSUARIO("CAT_LO_USUARIO"), "Creacion de Jucicios", "Juicio Actualizado: " & "48" & "|" & Resultados & "|" & Session("IDJuicioEtapaResultado"))
    End Sub

    Private Sub RadListBoxSource2_Transferred(sender As Object, e As RadListBoxTransferredEventArgs) Handles RadListBoxSource2.Transferred
        guardarPromociones()
    End Sub

    Private Sub RadListBoxDestination2_Deleted(sender As Object, e As RadListBoxEventArgs) Handles RadListBoxDestination2.Deleted
        guardarPromociones()
    End Sub

    Private Sub RadListBoxDestination2_Reordered(sender As Object, e As RadListBoxEventArgs) Handles RadListBoxDestination2.Reordered
        guardarPromociones()
    End Sub

    Private Sub RadListBoxDestination3_Deleted(sender As Object, e As RadListBoxEventArgs) Handles RadListBoxDestination3.Deleted
        guardarAcuerdos()
    End Sub
    Private Sub RadListBoxSource3_Transferred(sender As Object, e As RadListBoxTransferredEventArgs) Handles RadListBoxSource3.Transferred
        guardarAcuerdos()
    End Sub

    Private Sub RadListBoxDestination3_Reordered(sender As Object, e As RadListBoxEventArgs) Handles RadListBoxDestination3.Reordered
        guardarAcuerdos()
    End Sub

    Private Sub RadListBoxDestination4_Deleted(sender As Object, e As RadListBoxEventArgs) Handles RadListBoxDestination4.Deleted
        guardarResultados()
    End Sub


    Private Sub RadListBoxSource4_Transferred(sender As Object, e As RadListBoxTransferredEventArgs) Handles RadListBoxSource4.Transferred
        guardarResultados()
    End Sub

    Private Sub RadListBoxDestination4_Reordered(sender As Object, e As RadListBoxEventArgs) Handles RadListBoxDestination4.Reordered
        guardarResultados()
    End Sub

    Private Sub RadListBoxSource_Transferred(sender As Object, e As RadListBoxTransferredEventArgs) Handles RadListBoxSource.Transferred
        guardaAsignarEtapas()
    End Sub

    Private Sub RadListBoxDestination_Deleted(sender As Object, e As RadListBoxEventArgs) Handles RadListBoxDestination.Deleted
        guardaAsignarEtapas()
    End Sub

    Private Sub RadListBoxDestination_Reordered(sender As Object, e As RadListBoxEventArgs) Handles RadListBoxDestination.Reordered
        guardaAsignarEtapas()
    End Sub

    'Private Sub llenarPromocionesAcuerdos()
    '    Dim dsetRes As DataTable = Class_Judicial.Llenarpasos(22, "", "")
    '    RadListBoxSource2.DataSource = dsetRes
    '    RadListBoxSource2.DataBind()
    '    Dim NDataTable As New DataTable
    '    Dim dsetExist As DataTable = Class_Judicial.Llenarpasos(23, "", RLBNombres.SelectedValue.ToString)
    '    For Each numero As String In dsetExist.Rows(0).Item(0).ToString.Split(",")
    '        Dim Llenado As DataTable = Class_Judicial.Llenarpasos(21, "", numero)
    '        NDataTable.Merge(Llenado)
    '    Next
    '    RadListBoxDestination.DataSource = NDataTable
    '    RadListBoxDestination.DataBind()
    'End Sub

    'Private Sub llenarResultados()
    '    Dim dtsResSel As DataTable = Class_Judicial.Llenarpasos(20, "", RLBNombres.SelectedValue.ToString)
    '    Dim aux As New DataTable
    '    Dim i As Integer = 0
    '    'RLBEtapa1.DataSource = dtsResSel
    '    'RLBEtapa1.DataBind()
    '    If dtsResSel.Rows(0).Item(0) <> "" Then
    '        For Each numero As String In dtsResSel.Rows(0).Item(0).ToString.Split(",")
    '            Dim Llenado As DataTable = Class_Judicial.Llenarpasos(21, "", numero)
    '            RLBEtapa1.DataSource = Llenado
    '            RLBEtapa1.DataBind()
    '        Next
    '        'RLBEtapa1.DataSource = Llenado
    '        'RLBEtapa1.DataBind()
    '    End If

    '    '        For Each item_v As GridItem In RGEtapas.MasterTableView.Items
    '    '            Dim dataitem As GridDataItem = DirectCast(item_v, GridDataItem)
    '    '            If HttpUtility.HtmlDecode(item_v.Cells(3).Text) = numero Then
    '    '                dataitem.Selected = True
    '    '                Exit For
    '    '            End If
    '    '        Next
    '    '    Next
    '    'End If
    '    'Aviso("No existen resultados para asociar a esta etapa")
    'End Sub




    'Private Sub RLBEtapa1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RLBEtapa1.SelectedIndexChanged
    '    RgdResultados.Visible = False
    '    RdBtnGuardar2.Visible = False
    '    Dim dset As DataTable = Class_Judicial.Llenarpasos(41, RLBEtapa1.SelectedItem.Value, "")
    '    RLBPromociones1.Visible = True
    '    LblPromocion2.Visible = True
    '    If dset.Rows.Count <> 0 Then
    '        RLBPromociones1.DataSource = dset
    '        RLBPromociones1.DataBind()
    '        RLBPromociones1.Enabled = True
    '    Else
    '        RLBPromociones1.Items.Clear()
    '        RLBPromociones1.Items.Add("NO EXISTEN PROMOCIONES")
    '        RLBPromociones1.Items.Add(" RELACIONADAS")
    '        RLBPromociones1.Enabled = False
    '    End If
    'End Sub

    'Private Sub RLBPromociones1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RLBPromociones1.SelectedIndexChanged
    '    Dim dsetRes As DataTable = Class_Judicial.Llenarpasos(5, "", "")
    '    If dsetRes.Rows.Count <> 0 Then
    '        RgdResultados.DataSource = dsetRes
    '        RgdResultados.DataBind()
    '        RgdResultados.Visible = True
    '        RdBtnGuardar2.Visible = True
    '        Dim dtsResSel As DataTable = Class_Judicial.Llenarpasos(7, RLBPromociones1.SelectedItem.Value, RLBEtapa1.SelectedItem.Value)
    '        If dtsResSel.Rows(0).Item(0) <> 0 Then

    '            Dim resultadoselec(dtsResSel.Rows.Count) As String
    '            For x As Integer = 0 To dtsResSel.Rows.Count - 1
    '                For Each item_v As GridItem In RgdResultados.MasterTableView.Items
    '                    Dim dataitem As GridDataItem = DirectCast(item_v, GridDataItem)
    '                    If HttpUtility.HtmlDecode(item_v.Cells(3).Text) = dtsResSel.Rows(x).Item(0).ToString Then
    '                        dataitem.Selected = True
    '                        Exit For
    '                    End If
    '                Next
    '            Next
    '        End If
    '    Else
    '        Aviso("No existen resultados para asociar a esta promocion")
    '    End If
    'End Sub

    'Private Sub RLBResultados_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RLBResultados.SelectedIndexChanged
    '    LblAvanza.Visible = True
    '    RdDlAvanza.Visible = True
    '    LblEtapaAvanza.Visible = True
    '    RdDlEtapaAvanza.Visible = True
    '    RdBtnGuardar3.Visible = True
    '    LblFecha.Visible = True
    '    RdDlFecha.Visible = True
    '    LblFechaOb.Visible = True
    '    RdDlFechaOb.Visible = True
    '    LblNota.Visible = True
    '    RcbNota.Visible = True
    '    LblNotaOb.Visible = True
    '    RcbNotaOb.Visible = True
    '    LblDocumentos.Visible = True
    '    RcbDocumentos.Visible = True
    '    LblDocumentosOb.Visible = True
    '    RcbDocumentosOb.Visible = True
    '    LblDocumentosNum.Visible = True
    '    RNTBCuantos.Visible = True
    '    Dim dset As DataTable = Class_JudicialRLBEtapas.GuardarCatalogos(11, RLBEtapa2.SelectedItem.Value, RLBPromocion2.SelectedItem.Value, RLBResultados.SelectedItem.Value, "", "", "", "", "", "", "", "", "")
    '    RdDlAvanza.SelectedValue = dset.Rows(0).Item(0)
    '    RdDlEtapaAvanza.SelectedValue = dset.Rows(0).Item(1)

    '    RdDlFecha.SelectedValue = dset.Rows(0).Item(2)
    '    RdDlFechaOb.SelectedValue = dset.Rows(0).Item(3)
    '    RcbNota.SelectedValue = dset.Rows(0).Item(4)
    '    RcbNotaOb.SelectedValue = dset.Rows(0).Item(5)
    '    RcbDocumentos.SelectedValue = dset.Rows(0).Item(6)
    '    RcbDocumentosOb.SelectedValue = dset.Rows(0).Item(7)
    '    RNTBCuantos.Text = dset.Rows(0).Item(8)
    '    RNTBCuantos.Enabled = False
    'End Sub

    'Private Sub RdBtnGuardar2_Click(sender As Object, e As EventArgs) Handles RdBtnGuardar2.Click
    '    Dim Resultados As String = ""
    '    For Each item_v As GridItem In RgdResultados.MasterTableView.Items
    '        Dim dataitem As GridDataItem = DirectCast(item_v, GridDataItem)
    '        Dim cell As TableCell = dataitem("ClientSelectColumn")
    '        Dim checkBox As CheckBox = DirectCast(cell.Controls(0), CheckBox)
    '        If checkBox.Checked Then
    '            Resultados = Resultados + dataitem.Cells(3).Text + ","
    '        End If
    '    Next
    '    If Resultados <> "" Then
    '        Resultados = Resultados.Substring(0, Resultados.Length - 1)
    '    End If

    '    Dim dtsresultado As DataTable = Class_Judicial.GuardarCatalogos(9, RLBPromociones1.SelectedItem.Value, RLBEtapa1.SelectedItem.Value, Resultados, "", "", "", "", "", "", "", "", "")
    '    Aviso(dtsresultado.Rows(0).Item(0))

    'End Sub

    'Private Sub RGEtapas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RGEtapas.SelectedIndexChanged
    '    Dim Resultados As String = ""
    '    For Each item_v As GridItem In RGEtapas.MasterTableView.Items
    '        Dim dataitem As GridDataItem = DirectCast(item_v, GridDataItem)
    '        Dim cell As TableCell = dataitem("ClientSelectColumn")
    '        Dim checkBox As CheckBox = DirectCast(cell.Controls(0), CheckBox)
    '        If checkBox.Checked Then
    '            Resultados = Resultados + dataitem.Cells(3).Text + ","
    '        End If
    '    Next
    '    If Resultados <> "" Then
    '        Resultados = Resultados.Substring(0, Resultados.Length - 1)
    '    End If

    '    Dim dtsresultado As DataTable = Class_Judicial.Llenarpasos(19, Resultados, RLBNombres.SelectedValue.ToString)
    '    Aviso(dtsresultado.Rows(0).Item(0))
    '    llenarResultados()
    'End Sub

End Class