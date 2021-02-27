Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports Db
Imports System.Globalization
Imports System.IO
Imports Funciones
Imports System.Web.SessionState.HttpSessionState
Imports System.Data

Public Class Class_Documentos
    Public Shared Function LlenaGridDocumentos(ByRef V_Credito As String) As DataTable
        Dim SSCommandT As New SqlCommand
        SSCommandT.CommandText = "SP_HISTORICO_ARCHIVO"
        SSCommandT.CommandType = CommandType.StoredProcedure
        SSCommandT.Parameters.Add("@V_HIST_DO_CREDITO", SqlDbType.NVarChar).Value = V_Credito
        'SSCommandT.Parameters.Add("@V_HIST_DO_PRODUCTO", SqlDbType.NVarChar).Value = V_Producto
        SSCommandT.Parameters.Add("@V_HIST_DO_USUARIO", SqlDbType.NVarChar).Value = ""
        SSCommandT.Parameters.Add("@V_HIST_DO_NOMBRE_DOCUMENTO", SqlDbType.NVarChar).Value = ""
        SSCommandT.Parameters.Add("@V_HIST_DO_FECHA", SqlDbType.NVarChar).Value = ""
        SSCommandT.Parameters.Add("@V_HIST_DESC_DOCUMENTO", SqlDbType.NVarChar).Value = ""
        SSCommandT.Parameters.Add("@v_bandera", SqlDbType.Decimal).Value = 3
        Dim odatatable1 As DataTable = Consulta_Procedure(SSCommandT, "documento")
        Return odatatable1
    End Function

    Public Shared Function LlenaGridConvenios(ByRef V_Credito As String) As DataTable
        Dim SSCommandT As New SqlCommand
        SSCommandT.CommandText = "SP_HISTORICO_ARCHIVO"
        SSCommandT.CommandType = CommandType.StoredProcedure
        SSCommandT.Parameters.Add("@V_HIST_DO_CREDITO", SqlDbType.NVarChar).Value = V_Credito
        ' SSCommandT.Parameters.Add("@V_HIST_DO_PRODUCTO", SqlDbType.NVarChar).Value = v_producto
        SSCommandT.Parameters.Add("@V_HIST_DO_USUARIO", SqlDbType.NVarChar).Value = ""
        SSCommandT.Parameters.Add("@V_HIST_DO_NOMBRE_DOCUMENTO", SqlDbType.NVarChar).Value = ""
        SSCommandT.Parameters.Add("@V_HIST_DO_FECHA", SqlDbType.NVarChar).Value = ""
        SSCommandT.Parameters.Add("@V_HIST_DESC_DOCUMENTO", SqlDbType.NVarChar).Value = ""
        SSCommandT.Parameters.Add("@v_bandera", SqlDbType.Decimal).Value = 4
        Dim objDN As DataTable = Consulta_Procedure(SSCommandT, "Convenios")
        Return objDN
    End Function
End Class
