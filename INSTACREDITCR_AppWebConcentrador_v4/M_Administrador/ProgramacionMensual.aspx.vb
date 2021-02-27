
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports Db
Imports Funciones
Imports System.Diagnostics
Imports Telerik.Web.UI
Imports AjaxControlToolkit
Imports System.Collections.Generic
Imports System.Globalization

Partial Class ProgramacionMensual
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
    Private Function isSessionIDValid() As Boolean
        Try
            If Application.Get(tmpUSUARIO("CAT_LO_USUARIO")) = Session.SessionID Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
        End Try
        Return False
    End Function
    Shared nombre As String
    '----------------------------------------------------------------------------------------------------
    '                   Carga de layaut de calendario
    '-----------------------------------------------------------------------------------------------------


    Dim ERRORS As String = "100"
    Dim SKIP As String = "1"
    Dim ctlCarga As String
    Dim logCarga As String
    Dim badCarga As String
    Dim Ruta As String = StrRuta() & "CalendarioFilas\"

    Protected Sub BtnCargar_Click(sender As Object, e As EventArgs) Handles BtnCargar.Click
        Db.SubExisteRuta(Ruta)
        Dim Directory As New IO.DirectoryInfo(Ruta)
        Dim allFiles As IO.FileInfo() = Directory.GetFiles.Where(Function(fi) fi.Extension = ".csv" OrElse fi.Extension = ".txt" OrElse fi.Extension = ".unl").ToArray

        Dim tabla As DataTable
        Try
            tabla = FileToDataTable(Ruta & nombre)

            ConsultaFilasCalendario(40, "", "", "", 0, "")
            Dim fakebulk As New SqlBulkCopy(Conectando())

            fakebulk.DestinationTableName = "dbo.TMP_FILAS_TRABAJO"



            fakebulk.WriteToServer(tabla)


            'ConsultaFilasCalendario(2, "", "", "", 0, "")

            ' Dim DtsPreview As DataTable = SP.CARGA_GESTION(4, Nothing, Nothing, tmpUSUARIO("CAT_LO_NUM_AGENCIA"))


            'LBLResultado.Text = "Cargadas " & tabla.Rows.Count & " Gestiones"
        Catch ex As Exception
            Alerta(ex.Message)
        End Try
        'Kill(Ruta & nombre)

        Dim V_PAPA As String = tmpUSUARIO("CAT_LO_USUARIO")
        Dim dtstFilasCal As DataTable = ConsultaFilasCalendario(13, V_PAPA, "", "", 0, "") 'ejecuta el procedimiento para validar las nuevas filas del documento cargado




        Carga.Visible = False
        Calendario.Visible = True



        Response.Redirect("ProgramacionMensual.aspx", True)



    End Sub
    Private Function FileToDataTable(path As String) As DataTable
        Dim lines = IO.File.ReadAllLines(path)
        Dim tbl = New DataTable, nameCols As DataTable

        nameCols = ConsultaFilasCalendario(41, "", "", "", 0, "")


        Dim encabezado = True
        Dim separador As Char


        separador = Chr(44) 'Coma



        Dim numLinea = 1, numCols = nameCols.Rows.Count
        For Each line In lines
            If encabezado Then
                Dim campos As String() = line.Split(separador)
                If campos.Length > numCols Then
                    Throw New Exception("El encabezado es incorrecto. Tiene campos extra")
                ElseIf campos.Length < numCols Then
                    Throw New Exception("El encabezado es incorecto. Le faltan campos")
                End If
                For Each name As DataRow In nameCols.Rows
                    tbl.Columns.Add(New DataColumn(name(0).ToString, GetType(String)))
                Next
                encabezado = False
            Else
                Dim objFields = From field In line.Split(separador)
                                Select field
                numLinea += 1

                Dim objetos = objFields.ToArray()

                If objetos.Length > numCols Then
                    Throw New Exception("La linea #" & numLinea & " tiene campos extra. Valide.")
                ElseIf objetos.Length < numCols Then
                    Throw New Exception("La linea #" & numLinea & " tiene campos faltantes. Valide.")
                End If
                Dim newRow = tbl.Rows.Add()
                newRow.ItemArray = objetos
            End If
        Next
        Return tbl
    End Function
    Private Sub Alerta(Msj As String)
        Aviso.RadAlert(Msj.Replace("""", "").Replace("'", "").Replace(Chr(10), "").Replace(Chr(10), ""), 440, 155, "AVISO", Nothing)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim Usr As String = tmpUSUARIO("CAT_LO_USUARIO")
        Catch ex As Exception
            OffLine(HidenUrs.Value)
            AUDITORIA(HidenUrs.Value, "Administrador", "ProgramacionMensual", " ", "Page_Load", "", Request.ServerVariables("REMOTE_ADDR"), Request.ServerVariables("REMOTE_HOST"))
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/SesionExpirada.aspx")
        End Try
        Try
            If Not IsPostBack Then

                If True = False Then ' posible permiso fichas
                    OffLine(HidenUrs.Value)
                    Session.Clear()
                    Session.Abandon()
                    Response.Redirect("~/SesionExpirada.aspx")
                End If
                HidenUrs.Value = tmpUSUARIO("CAT_LO_USUARIO")
                nombre = ""
                'LLENAR_DROP(16, DdlDelimitador, "Delimitador", "Delimitador")
                validacal()
            End If
        Catch ex As Exception
            SendMail("Page_Load", ex, "", "", HidenUrs.Value)
        End Try
    End Sub
    Sub validacal()

        Dim dtsFilas As DataTable = ConsultaFilasCalendario(14, "", "", "", 0, "")

        If dtsFilas.Rows(0)(0) > 0 Then
            Calendario.Visible = True
        Else
            Carga.Visible = True
        End If



    End Sub

    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Administrador", "ProgramacionMensual.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub

    'Protected Sub AsyncFileUpload1_UploadedComplete(ByVal sender As Object, ByVal e As AjaxControlToolkit.AsyncFileUploadEventArgs)
    '    Try
    '        SubExisteRuta(Ruta)
    '        For Each s In System.IO.Directory.GetFiles(Ruta)
    '            System.IO.File.Delete(s)
    '        Next
    '        Dim nom As String = e.FileName.Substring(e.FileName.LastIndexOf("\") + 1)
    '        Dim ruta_archivo As String = Ruta + nom
    '        Me.AsyncFileUpload1.SaveAs(ruta_archivo)
    '        LblMsj.Text = "Archivo listo para ser subido"
    '    Catch ex As Exception
    '    End Try
    'End Sub

    'Protected Sub FileUploadComplete(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim filename As String = System.IO.Path.GetFileName(AsyncFileUpload1.FileName)
    '    AsyncFileUpload1.SaveAs(Server.MapPath("Uploads/") + filename)
    'End Sub

    Protected Sub AsyncUpload1_FileUploaded(sender As Object, e As FileUploadedEventArgs)


        Dim Archivo As String = e.File.FileName.ToString
        nombre = Archivo
        Dim pacth As String = Ruta & Archivo
        e.File.SaveAs(pacth)

    End Sub

    '----------------------------------------------------------------------------------------------------
    '                   Calendario
    '-----------------------------------------------------------------------------------------------------


    Function ConsultaFilasCalendario(ByVal V_Bandera As String, ByVal V_Comentario As String, ByVal V_DteInicio As String, ByVal V_DteFin As String, ByVal V_ID As Integer, ByVal V_Nombre As String) As DataTable

        Dim SSCommandUsuario As New SqlCommand
        SSCommandUsuario.CommandText = "SP_FILAS_TRABAJO2"
        SSCommandUsuario.CommandType = CommandType.StoredProcedure
        SSCommandUsuario.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = V_Bandera
        SSCommandUsuario.Parameters.Add("@V_Tipo", SqlDbType.NVarChar).Value = V_Comentario
        SSCommandUsuario.Parameters.Add("@V_Campo", SqlDbType.NVarChar).Value = V_DteInicio
        SSCommandUsuario.Parameters.Add("@V_Donde", SqlDbType.NVarChar).Value = V_DteFin
        SSCommandUsuario.Parameters.Add("@V_ID", SqlDbType.Decimal).Value = V_ID
        SSCommandUsuario.Parameters.Add("@V_CAMPOS", SqlDbType.NVarChar).Value = V_Nombre
        Dim DtsUsuario As DataTable = Consulta_Procedure(SSCommandUsuario, "FILAS")
        Return DtsUsuario

    End Function


    Protected Sub RadScheduler1_Load(sender As Object, e As EventArgs)


        Dim dtstFilasCal As DataTable = ConsultaFilasCalendario(12, "", "", "", 0, "")
        RadScheduler1.DataKeyField = dtstFilasCal.Columns(0).ColumnName
        RadScheduler1.DataSubjectField = dtstFilasCal.Columns(1).ColumnName
        RadScheduler1.DataDescriptionField = dtstFilasCal.Columns(2).ColumnName
        RadScheduler1.DataStartField = dtstFilasCal.Columns(3).ColumnName
        RadScheduler1.DataEndField = dtstFilasCal.Columns(4).ColumnName

        RadScheduler1.DataSource = dtstFilasCal.DefaultView
        RadScheduler1.DataBind()

        RadScheduler1.SelectedView = SchedulerViewType.WeekView
        RadScheduler1.SelectedDate = Date.Now


    End Sub



    'Protected Sub RadScheduler1_DataBound(sender As Object, e As EventArgs)

    '    For Each ele As ResourceType In RadScheduler1.ResourceTypes
    '        ele.AllowMultipleValues = False
    '    Next

    'End Sub



    Protected Sub RadScheduler1_AppointmentCreated(sender As Object, e As AppointmentCreatedEventArgs)

        Dim pr As Panel = New Panel
        Dim Fila As String = e.Appointment.Subject

        Dim dtsFilasCal As DataTable = ConsultaFilasCalendario(30, Fila, "", "", 0, "")
        Dim col As Integer = dtsFilasCal.Rows(0)(0).ToString
        'col = col.Substring(1)
        Dim Color As Drawing.Color = System.Drawing.Color.FromArgb(col)
        If e.Appointment.Start <= System.DateTime.Now And e.Appointment.End >= System.DateTime.Now Then
            pr.CssClass = "FILAACTIVA"
            e.Appointment.BackColor = Drawing.Color.GreenYellow
            e.Appointment.AllowEdit = False
            e.Appointment.AllowDelete = False
        ElseIf e.Appointment.End <= System.DateTime.Now Then
            pr.CssClass = "FILAinACTIVA"
            e.Appointment.AllowEdit = False
            e.Appointment.AllowDelete = False
            e.Appointment.BackColor = Drawing.Color.Gray
        Else
            pr.CssClass = "FILAinACTIVA"
            e.Appointment.BackColor = Color
        End If


        e.Container.Controls.Add(pr) 'AddAt(0, pr)


    End Sub


    Protected Sub RadScheduler1_TimeSlotCreated(sender As Object, e As TimeSlotCreatedEventArgs)

        Dim scheduler As RadScheduler = DirectCast(sender, RadScheduler)
        If e.TimeSlot.Start.Date <= System.DateTime.Now And e.TimeSlot.End.Date >= System.DateTime.Now Then

            e.TimeSlot.CssClass = "FILAACTIVA"

        End If
    End Sub



    Protected Sub RadScheduler1_AppointmentUpdate(sender As Object, e As AppointmentUpdateEventArgs)


        Dim strNomFila As String = e.Appointment.Subject
        Dim strNomFila2 As String = e.ModifiedAppointment.Subject

        Dim id As Integer = e.Appointment.ID


        Dim dteFechaInicial As Date
        Dim dteFechaFinal As Date
        Dim dteFechaInicial2 As Date
        Dim dteFechaFinal2 As Date

        If Date.TryParse(e.Appointment.Start.Date & " " & e.Appointment.Start.Hour & ":" & e.Appointment.Start.Minute, dteFechaInicial) Then 'Fecha y hora inicial original
            If Date.TryParse(e.Appointment.End.Date & " " & e.Appointment.End.Hour & ":" & e.Appointment.End.Minute, dteFechaFinal) Then 'Fecha y hora final original
                If Date.TryParse(e.ModifiedAppointment.Start.Date & " " & e.ModifiedAppointment.Start.Hour & ":" & e.ModifiedAppointment.Start.Minute, dteFechaInicial2) Then 'Fecha y hora inical nueva original
                    If Date.TryParse(e.ModifiedAppointment.End.Date & " " & e.ModifiedAppointment.End.Hour & ":" & e.ModifiedAppointment.End.Minute, dteFechaFinal2) Then 'Fecha y hora final nueva original

                        If strNomFila <> strNomFila2 Then
                            RadWindowManager1.RadAlert("El nombre de la fila no puede ser modificado", 300, 100, "Nombre de fila", "")
                        ElseIf dteFechaInicial2.DayOfYear < Date.Now.DayOfYear Then
                            RadWindowManager1.RadAlert("La fila no puede ser ubicada antes del dia de hoy", 300, 100, "Fecha no valida", "")
                        ElseIf dteFechaInicial2.TimeOfDay < Date.Now.TimeOfDay And dteFechaFinal2.Hour <> 0 And dteFechaInicial2.DayOfYear = Date.Now.DayOfYear Then
                            RadWindowManager1.RadAlert("La fila no puede ser ubicada antes de ahora", 300, 100, "Fecha no valida", "")
                        ElseIf dteFechaFinal2 <= dteFechaInicial2 Then
                            RadWindowManager1.RadAlert("La fila no puede terminar antes de que comience", 300, 100, "Fecha no valida", "")
                        Else
                            Dim V_DteInicio As String
                            Dim V_DteFin As String
                            If dteFechaInicial2.TimeOfDay.Hours = 0 And dteFechaInicial2.TimeOfDay.Minutes = 0 Then
                                V_DteInicio = dteFechaInicial.Year  & "/" & dteFechaInicial.Month & "/" & dteFechaInicial.Day & " " & dteFechaInicial.TimeOfDay.Hours & ":" & dteFechaInicial.TimeOfDay.Minutes & ":00"
                                V_DteFin = dteFechaInicial.Day & "/" & dteFechaInicial.Month & "/" & dteFechaInicial.Year & " " & "22" & ":" & "00" & ":00"
                            Else
                                V_DteInicio = dteFechaInicial2.Year & "/" & dteFechaInicial2.Month & "/" & dteFechaInicial2.Day & " " & dteFechaInicial2.TimeOfDay.Hours & ":" & dteFechaInicial2.TimeOfDay.Minutes & ":00"
                                V_DteFin = dteFechaFinal2.Year & "/" & dteFechaFinal2.Month & "/" & dteFechaFinal2.Day & " " & dteFechaFinal2.TimeOfDay.Hours & ":" & dteFechaFinal2.TimeOfDay.Minutes & ":00"
                            End If

                            Dim Usr As String = tmpUSUARIO("CAT_LO_USUARIO")


                            Dim dstFilas As DataTable = ConsultaFilasCalendario(17, e.ModifiedAppointment.Description.ToString, V_DteInicio, V_DteFin, id, e.ModifiedAppointment.Subject.ToString)

                            dstFilas = Nothing
                            dstFilas = ConsultaFilasCalendario(29, Usr, "Programacion mensual", "Actualizacion de fila de trabajo", id, 0)

                            Response.Redirect("ProgramacionMensual.aspx", True)
                        End If

                    Else
                        RadWindowManager1.RadAlert("Error de fecha final 2", 300, 100, "Fecha final 2", "")
                    End If
                Else
                    RadWindowManager1.RadAlert("Error de fecha inicial 2", 300, 100, "Fecha inicial 2", "")
                End If
            Else
                RadWindowManager1.RadAlert("Error de fecha final 1", 300, 100, "Fecha final 1", "")
            End If
        Else
            RadWindowManager1.RadAlert("Error de fecha inicial 1", 300, 100, "Fecha inicial 1", "")
        End If





    End Sub

    Protected Sub RadScheduler1_AppointmentDelete(sender As Object, e As AppointmentDeleteEventArgs)

        Dim dstFilas As DataTable = ConsultaFilasCalendario(18, "", "", "", e.Appointment.ID, e.Appointment.Subject.ToString)

        Dim Usr As String = tmpUSUARIO("CAT_LO_USUARIO")
        dstFilas = ConsultaFilasCalendario(29, Usr, "Programacion mensual", "Eliminacion de fila de trabajo al calendario manualmente", e.Appointment.ID, 2)

        Response.Redirect("ProgramacionMensual.aspx", True)
    End Sub

    Protected Sub RadScheduler1_AppointmentInsert(sender As Object, e As AppointmentInsertEventArgs)

        Dim Subject As String = e.Appointment.Subject
        Dim V_dteInicio As String = e.Appointment.Start.Year & "/" & e.Appointment.Start.Month & "/" & e.Appointment.Start.Day & " " & e.Appointment.Start.TimeOfDay.Hours & ":" & e.Appointment.Start.TimeOfDay.Minutes & ":00"
        Dim V_dteFin As String = e.Appointment.End.Year & "/" & e.Appointment.End.Month & "/" & e.Appointment.End.Day & " " & e.Appointment.End.TimeOfDay.Hours & ":" & e.Appointment.End.TimeOfDay.Minutes & ":00"

        Dim dstFilas As DataTable = ConsultaFilasCalendario(19, Subject, V_dteInicio, V_dteFin, 0, "")


        If dstFilas.Rows(0)(1) > 0 Then

            Dim V_ID As Integer = dstFilas.Rows(0)(0) + 1
            Dim Descripcion As String = e.Appointment.Description
            Dim dteFinDiaLaboral As Date
            Dim bln As Boolean = DateTime.TryParse(Date.Now.Date & " 22:00", dteFinDiaLaboral)

            If e.Appointment.Start.DayOfYear < Date.Now.DayOfYear Then
                RadWindowManager1.RadAlert("La nueva fila no puede ser agendada antes del dia de hoy", 300, 100, "Fecha no valida", "")
            ElseIf e.Appointment.Start.DayOfYear = Date.Now.DayOfYear And e.Appointment.Start.TimeOfDay <= Date.Now.TimeOfDay Then
                RadWindowManager1.RadAlert("La nueva fila no puede ser agendada antes de la hora actual", 300, 100, "Hora no valida", "")
            ElseIf e.Appointment.Start.TimeOfDay.Hours < 7 And e.Appointment.Start.TimeOfDay.Hours <> 0 Then
                RadWindowManager1.RadAlert("La nueva fila no puede ser agendada antes de las 7:00 A.M.", 300, 100, "Hora no valida", "")
            ElseIf e.Appointment.End.TimeOfDay > dteFinDiaLaboral.TimeOfDay And e.Appointment.End.TimeOfDay.Hours <> 0 Then
                RadWindowManager1.RadAlert("La nueva fila no puede ser agendada despues de las 10:00 P.M.", 300, 100, "Hora no valida", "")
            ElseIf dstFilas.Rows(0)(2) > 0 Then
                RadWindowManager1.RadAlert("Ya existe una fila agendada a la misma hora. Solo se puede agendar la misma fila una sola vez al mismo tiempo", 400, 150, "Fila duplicada", "")
            ElseIf dstFilas.Rows(0)(3) > 0 Then
                RadWindowManager1.RadAlert("Ya existe una fila agendada a la misma hora. Solo se puede agendar la misma fila una sola vez al mismo tiempo", 400, 150, "Fila duplicada", "")
            ElseIf e.Appointment.End.TimeOfDay.Hours = 0 And e.Appointment.Start.TimeOfDay.Hours = 0 And e.Appointment.Start.DayOfYear < e.Appointment.End.DayOfYear Then

                V_dteInicio = e.Appointment.Start.Year & "/" & e.Appointment.Start.Month & "/" & e.Appointment.Start.Day & " 7:00:00"
                V_dteFin = e.Appointment.End.Year & "/" & e.Appointment.End.Month & "/" & e.Appointment.End.Day & " 22:00:00"
                dstFilas = Nothing
                dstFilas = ConsultaFilasCalendario(20, Descripcion, V_dteInicio, V_dteFin, V_ID, Subject)

                Dim Usr As String = tmpUSUARIO("CAT_LO_USUARIO")
                dstFilas = ConsultaFilasCalendario(29, Usr, "Programacion mensual", "Insercción de fila de trabajo al calendario manualmente", V_ID, 1)
                Response.Redirect("ProgramacionMensual.aspx", True)
            Else
                dstFilas = Nothing
                dstFilas = ConsultaFilasCalendario(20, Descripcion, V_dteInicio, V_dteFin, V_ID, Subject)

                dstFilas = Nothing
                Dim Usr As String = tmpUSUARIO("CAT_LO_USUARIO")
                dstFilas = ConsultaFilasCalendario(29, Usr, "Programacion mensual", "Insercción de fila de trabajo al calendario manualmente", V_ID, 1)

                Response.Redirect("ProgramacionMensual.aspx", True)
            End If

        Else
            RadWindowManager1.RadAlert("La fila " & Subject & " no esta definida", 250, 100, "Nombre no valido", "")
        End If





    End Sub
End Class
