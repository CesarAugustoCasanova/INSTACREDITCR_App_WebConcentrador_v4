<%@ Application Language="VB" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.OracleClient" %>
<script RunAt="server">
    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        'dim SSCommand as new sqlcommand
        'SSCommand.CommandText = "SP_VARIOS_QRS"
        'SSCommand.CommandType = CommandType.StoredProcedure
        'SSCommand.Parameters.Add("@V_USUARIO", SqlDbType.NVarChar).Value = ""
        'SSCommand.Parameters.Add("@V_VALOR", SqlDbType.Decimal).Value = 0
        'SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 4
        '
        'Dim DtsVariable As DataSet = Conexiones.Consulta_Procedure(SSCommand, "Licencias")
        'Dim Licencias(DtsVariable.Tables(0).Rows(0).Item("Cuantas"), 2) As String
        'Application("UsersLoggedIn") =  Licencias
    End Sub
    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        'Dim userLoggedIn As String = Session("UserLoggedIn")
        'dim SSCommand as new sqlcommand
        'SSCommand.CommandText = "SP_LICENCIA"
        'SSCommand.CommandType = CommandType.StoredProcedure
        'SSCommand.Parameters.Add("@V_USUARIO", SqlDbType.NVarChar).Value = userLoggedIn
        'SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 3
        '
        'Dim DtsVariable As DataSet = Conexiones.Consulta_Procedure(SSCommand, "Licencias")
    End Sub
        
    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Código que se ejecuta al producirse un error no controlado   
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        Dim userLoggedIn As String = Session("UserLoggedIn")
    End Sub
    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        Dim DtsDisponibles As DataSet = Class_Sesion.LlenarElementos(Session("UserLoggedIn"), "Planfia", "Administrador", "Inactividad Por 3 Minutos", 5, "", "", "Cierre De Sesion")
    End Sub
</script>
