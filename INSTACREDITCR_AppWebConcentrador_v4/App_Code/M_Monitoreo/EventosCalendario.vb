Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports Db
Imports System.Data


Public Class EventosCalendario

    Public Shared Function getEvents(start As DateTime, [end] As DateTime) As List(Of CalendarEvent)
        Dim events As New List(Of CalendarEvent)()
        Dim oraCommand As New SqlCommand
        oraCommand.CommandText = "SP_AGENDA"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_USUARIO", SqlDbType.NVarChar).Value = "YO" 'CType(Session("Usuario"), USUARIO).CAT_LO_USUARIO
        oraCommand.Parameters.Add("V_INICIO", SqlDbType.DateTime).Value = start
        oraCommand.Parameters.Add("V_FIN", SqlDbType.DateTime).Value = [end]
        Dim DtsAgenda As DataTable = Consulta_Procedure(oraCommand, "AGENDA")
        If DtsAgenda.Rows.Count() = 0 Then
            Dim rtn As Date = "01/01/2000"

            Dim cevent As New CalendarEvent()
            cevent.id = CInt(0)
            cevent.title = "INICIO MES"
            cevent.description = "INICIO MES"
            cevent.start = rtn
            cevent.[end] = rtn
            events.Add(cevent)
        Else
            For A As Integer = 0 To DtsAgenda.Rows.Count - 1
                Dim cevent As New CalendarEvent()
                cevent.id = CInt(DtsAgenda.Rows(A)("HIST_AG_ID"))
                cevent.title = DirectCast(DtsAgenda.Rows(A)("HIST_AG_TITULO"), String)
                cevent.description = DirectCast(DtsAgenda.Rows(A)("HIST_AG_COMENTARIO"), String)
                cevent.start = DirectCast(DtsAgenda.Rows(A)("HIST_AG_INICIO"), DateTime)
                cevent.[end] = DirectCast(DtsAgenda.Rows(A)("HIST_AG_FIN"), DateTime)
                events.Add(cevent)
            Next
        End If
        Return events
    End Function

    Public Shared Sub updateEvent(id As Integer, title As [String], description As [String])
        Dim oraCommand As New SqlCommand
        oraCommand.CommandText = "SP_ADD_HIST_AGENDA"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_BANDERA", SqlDbType.Decimal).Value = 1
        oraCommand.Parameters.Add("V_ID", SqlDbType.Decimal).Value = id
        oraCommand.Parameters.Add("V_INICIO", SqlDbType.DateTime).Value = Now
        oraCommand.Parameters.Add("V_FIN", SqlDbType.DateTime).Value = Now
        oraCommand.Parameters.Add("V_USUARIO", SqlDbType.NVarChar).Value = "YO" 'CType(Session("Usuario"), USUARIO).CAT_LO_USUARIO
        oraCommand.Parameters.Add("V_COMENTARIO", SqlDbType.NVarChar).Value = description
        oraCommand.Parameters.Add("V_TITULO", SqlDbType.NVarChar).Value = title
        oraCommand.Parameters.Add("V_CREDITO", SqlDbType.NVarChar).Value = "501086065965" 'CType(Session("Credito"), Credito).PR_KT_CREDITO
        Dim DtsAddAgenda As DataTable = Consulta_Procedure(oraCommand, "Agenda")
    End Sub

    Public Shared Sub updateEventTime(id As Integer, start As DateTime, [end] As DateTime)
        Dim oraCommand As New SqlCommand
        oraCommand.CommandText = "SP_ADD_HIST_AGENDA"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_BANDERA", SqlDbType.Decimal).Value = 2
        oraCommand.Parameters.Add("V_ID", SqlDbType.Decimal).Value = id
        oraCommand.Parameters.Add("V_INICIO", SqlDbType.DateTime).Value = start
        oraCommand.Parameters.Add("V_FIN", SqlDbType.DateTime).Value = [end]
        oraCommand.Parameters.Add("V_USUARIO", SqlDbType.NVarChar).Value = "YO" 'CType(Session("Usuario"), USUARIO).CAT_LO_USUARIO
        oraCommand.Parameters.Add("V_COMENTARIO", SqlDbType.NVarChar).Value = ""
        oraCommand.Parameters.Add("V_TITULO", SqlDbType.NVarChar).Value = ""
        oraCommand.Parameters.Add("V_CREDITO", SqlDbType.NVarChar).Value = "501086065965" 'CType(Session("Credito"), Credito).PR_KT_CREDITO
        Dim DtsAddAgenda As DataTable = Consulta_Procedure(oraCommand, "Agenda")
    End Sub

    Public Shared Sub deleteEvent(id As Integer)
        Dim oraCommand As New SqlCommand
        oraCommand.CommandText = "SP_ADD_HIST_AGENDA"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_BANDERA", SqlDbType.Decimal).Value = 3
        oraCommand.Parameters.Add("V_ID", SqlDbType.Decimal).Value = id
        oraCommand.Parameters.Add("V_INICIO", SqlDbType.DateTime).Value = Now
        oraCommand.Parameters.Add("V_FIN", SqlDbType.DateTime).Value = Now
        oraCommand.Parameters.Add("V_USUARIO", SqlDbType.NVarChar).Value = "YO" 'CType(Session("Usuario"), USUARIO).CAT_LO_USUARIO
        oraCommand.Parameters.Add("V_COMENTARIO", SqlDbType.NVarChar).Value = ""
        oraCommand.Parameters.Add("V_TITULO", SqlDbType.NVarChar).Value = ""
        oraCommand.Parameters.Add("V_CREDITO", SqlDbType.NVarChar).Value = "501086065965" 'CType(Session("Credito"), Credito).PR_KT_CREDITO
        Dim DtsAddAgenda As DataTable = Consulta_Procedure(oraCommand, "Agenda")
    End Sub

    Public Shared Function addEvent(cevent As CalendarEvent) As Integer
        Dim oraCommand As New SqlCommand
        oraCommand.CommandText = "SP_ADD_HIST_AGENDA"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_BANDERA", SqlDbType.Decimal).Value = 0
        oraCommand.Parameters.Add("V_INICIO", SqlDbType.DateTime).Value = cevent.start
        oraCommand.Parameters.Add("V_FIN", SqlDbType.DateTime).Value = cevent.[end]
        oraCommand.Parameters.Add("V_USUARIO", SqlDbType.NVarChar).Value = "YO" 'CType(Session("Usuario"), USUARIO).CAT_LO_USUARIO
        oraCommand.Parameters.Add("V_COMENTARIO", SqlDbType.NVarChar).Value = cevent.description
        oraCommand.Parameters.Add("V_TITULO", SqlDbType.NVarChar).Value = cevent.title
        oraCommand.Parameters.Add("V_CREDITO", SqlDbType.NVarChar).Value = "501086065965" 'CType(Session("Credito"), Credito).PR_KT_CREDITO
        Dim DtsAddAgenda As DataTable = Consulta_Procedure(oraCommand, "Agenda")
        Return Val(DtsAddAgenda.Rows(0).Item("IDENTIFICADOR"))
    End Function

End Class
