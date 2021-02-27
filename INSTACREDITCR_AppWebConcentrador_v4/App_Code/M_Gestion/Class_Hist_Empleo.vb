Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports Db
Imports System.Data

Public Class Class_Hist_Empleo
    Public Shared Function LlenarInfoEmpleo(ByVal V_Credito As String, ByVal V_PRODUCTO As String) As DataTable
        Dim SSCommand As New SqlCommand("SP_INFORMACION_CREDITO")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Credito", SqlDbType.NVarChar).Value = V_Credito
        SSCommand.Parameters.Add("@V_PRODUCTO", SqlDbType.NVarChar).Value = V_PRODUCTO
        SSCommand.Parameters.Add("@v_bandera", SqlDbType.NVarChar).Value = 6
        Dim DtsHist_Act As DataTable = Consulta_Procedure(SSCommand, "EMPLEO")
        Return DtsHist_Act
    End Function
    Public Shared Function LlenarInfoDirecciones(ByVal V_Credito As String, ByVal V_PRODUCTO As String) As DataTable
        Dim SSCommand As New SqlCommand("SP_INFORMACION_CREDITO")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Credito", SqlDbType.NVarChar).Value = V_Credito
        SSCommand.Parameters.Add("@V_PRODUCTO", SqlDbType.NVarChar).Value = V_PRODUCTO
        SSCommand.Parameters.Add("@v_bandera", SqlDbType.NVarChar).Value = 9
        Dim DtsHist_Act As DataTable = Consulta_Procedure(SSCommand, "EMPLEO")
        Return DtsHist_Act
    End Function
    Public Shared Function LlenarInfoEmpleoAval(ByVal V_Credito As String, ByVal V_PRODUCTO As String) As DataTable
        Dim SSCommand As New SqlCommand("SP_INFORMACION_CREDITO")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Credito", SqlDbType.NVarChar).Value = V_Credito
        SSCommand.Parameters.Add("@V_PRODUCTO", SqlDbType.NVarChar).Value = V_PRODUCTO
        SSCommand.Parameters.Add("@v_bandera", SqlDbType.NVarChar).Value = 10
        Dim DtsHist_Act As DataTable = Consulta_Procedure(SSCommand, "EMPLEO")
        Return DtsHist_Act
    End Function
    Public Shared Function LlenarInfoDireccionesAval(ByVal V_Credito As String, ByVal V_PRODUCTO As String) As DataTable
        Dim SSCommand As New SqlCommand("SP_INFORMACION_CREDITO")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Credito", SqlDbType.NVarChar).Value = V_Credito
        SSCommand.Parameters.Add("@V_PRODUCTO", SqlDbType.NVarChar).Value = V_PRODUCTO
        SSCommand.Parameters.Add("@v_bandera", SqlDbType.NVarChar).Value = 11
        Dim DtsHist_Act As DataTable = Consulta_Procedure(SSCommand, "EMPLEO")
        Return DtsHist_Act
    End Function
End Class
