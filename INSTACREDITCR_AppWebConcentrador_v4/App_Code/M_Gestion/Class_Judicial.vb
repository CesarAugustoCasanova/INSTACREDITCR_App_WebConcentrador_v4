Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports Db
Imports Telerik.Web.UI

Public Class Class_Judicial
    Public Shared Function Catalogos(v_catalogo As String, v_bandera As Integer) As Object
        Dim SSCommand As New SqlCommand("SP_CATALOGOS_JUDICIALES")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_bandera", SqlDbType.Decimal).Value = v_bandera
        SSCommand.Parameters.Add("@v_id", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@v_catalogo", SqlDbType.NVarChar).Value = v_catalogo
        SSCommand.Parameters.Add("@v_nombre", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@v_comodin", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@v_avanza", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@v_fecha", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@v_fechaob", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@v_nota", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@v_notaob", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@v_documentos", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@v_documentosob", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@v_cuantosdocumentos", SqlDbType.NVarChar).Value = ""
        Dim DtsJudicial As DataTable = Consulta_Procedure(SSCommand, "Telefonos")
        Return DtsJudicial
    End Function

    Public Shared Function GuardarCatalogos(v_bandera As String, v_id As String, v_catalogo As String, v_nombre As String, v_comodin As String, v_avanza As String, v_fecha As String, v_fechaob As String, v_nota As String, v_notaob As String, v_documentos As String, v_documentosob As String, v_cuantosdocumentos As String) As Object
        Dim SSCommand As New SqlCommand("SP_CATALOGOS_JUDICIALES")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_bandera", SqlDbType.Decimal).Value = v_bandera
        SSCommand.Parameters.Add("@v_id", SqlDbType.NVarChar).Value = v_id
        SSCommand.Parameters.Add("@v_catalogo", SqlDbType.NVarChar).Value = v_catalogo
        SSCommand.Parameters.Add("@v_nombre", SqlDbType.NVarChar).Value = v_nombre
        SSCommand.Parameters.Add("@v_comodin", SqlDbType.NVarChar).Value = v_comodin
        SSCommand.Parameters.Add("@v_avanza", SqlDbType.NVarChar).Value = v_avanza
        SSCommand.Parameters.Add("@v_fecha", SqlDbType.NVarChar).Value = v_fecha
        SSCommand.Parameters.Add("@v_fechaob", SqlDbType.NVarChar).Value = v_fechaob
        SSCommand.Parameters.Add("@v_nota", SqlDbType.NVarChar).Value = v_nota
        SSCommand.Parameters.Add("@v_notaob", SqlDbType.NVarChar).Value = v_notaob
        SSCommand.Parameters.Add("@v_documentos", SqlDbType.NVarChar).Value = v_documentos
        SSCommand.Parameters.Add("@v_documentosob", SqlDbType.NVarChar).Value = v_documentosob
        SSCommand.Parameters.Add("@v_cuantosdocumentos", SqlDbType.NVarChar).Value = v_cuantosdocumentos
        Dim DtsJudicial As DataTable = Consulta_Procedure(SSCommand, "Telefonos")
        Return DtsJudicial
    End Function

    Public Shared Function Llenarpasos(v_bandera As String, v_idetapa As String, v_etapa As String) As Object
        Dim SSCommand As New SqlCommand("SP_CATALOGOS_JUDICIALES")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_bandera", SqlDbType.Decimal).Value = v_bandera
        SSCommand.Parameters.Add("@v_id", SqlDbType.NVarChar).Value = v_idetapa
        SSCommand.Parameters.Add("@v_catalogo", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@v_nombre", SqlDbType.NVarChar).Value = v_etapa
        SSCommand.Parameters.Add("@v_comodin", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@v_avanza", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@v_fecha", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@v_fechaob", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@v_nota", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@v_notaob", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@v_documentos", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@v_documentosob", SqlDbType.NVarChar).Value = ""
        SSCommand.Parameters.Add("@v_cuantosdocumentos", SqlDbType.NVarChar).Value = ""
        Dim DtsCataloso As DataTable = Consulta_Procedure(SSCommand, "Telefonos")
        Return DtsCataloso
    End Function
    Public Shared Function llenarcatalogojudicial(ByVal v_bandera As String) As Object
        Dim SSCommand As New SqlCommand("SP_CATALOGOS_JUDICIALESV2")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_bandera", SqlDbType.NVarChar).Value = v_bandera
        Dim DtsCataloso As DataTable = Consulta_Procedure(SSCommand, "CatalogosJudiciales")
        Return DtsCataloso
    End Function
    Public Shared Function EditarCatalogosJudicial(ByVal v_dato1 As String, ByVal v_dato2 As String, ByVal v_dato3 As String, ByVal v_dato4 As String, ByVal v_dato5 As String, ByVal v_dato6 As String, ByVal v_dato7 As String, ByVal v_dato8 As String, ByVal v_dato9 As String, ByVal v_dato10 As String, ByVal v_dato11 As String, ByVal v_dato12 As String, ByVal v_dato13 As String, ByVal v_dato14 As String, ByVal v_bandera As Integer) As Object
        Dim SSCommand As New SqlCommand("SP_EDITA_CATALGOS_JUDICIALES")
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_dato1", SqlDbType.NVarChar).Value = v_dato1
        SSCommand.Parameters.Add("@v_dato2", SqlDbType.NVarChar).Value = v_dato2
        SSCommand.Parameters.Add("@v_dato3", SqlDbType.NVarChar).Value = v_dato3
        SSCommand.Parameters.Add("@v_dato4", SqlDbType.NVarChar).Value = v_dato4
        SSCommand.Parameters.Add("@v_dato5", SqlDbType.NVarChar).Value = v_dato5
        SSCommand.Parameters.Add("@v_dato6", SqlDbType.NVarChar).Value = v_dato6
        SSCommand.Parameters.Add("@v_dato7", SqlDbType.NVarChar).Value = v_dato7
        SSCommand.Parameters.Add("@v_dato8", SqlDbType.NVarChar).Value = v_dato8
        SSCommand.Parameters.Add("@v_dato9", SqlDbType.NVarChar).Value = v_dato9
        SSCommand.Parameters.Add("@v_dato10", SqlDbType.NVarChar).Value = v_dato10
        SSCommand.Parameters.Add("@v_dato11", SqlDbType.NVarChar).Value = v_dato11
        SSCommand.Parameters.Add("@v_dato12", SqlDbType.NVarChar).Value = v_dato12
        SSCommand.Parameters.Add("@v_dato13", SqlDbType.NVarChar).Value = v_dato13
        SSCommand.Parameters.Add("@v_dato14", SqlDbType.NVarChar).Value = v_dato14
        SSCommand.Parameters.Add("@v_bandera", SqlDbType.Decimal).Value = v_bandera
        Dim DtsJudicial As DataTable = Consulta_Procedure(SSCommand, "EditaCatalogos")
        Return DtsJudicial
    End Function
    Public Shared Function Rad_Tcadena(ByRef v_item As RadComboBox) As String
        Dim v_cadena As String = "'"
        Dim collection As IList(Of RadComboBoxItem) = v_item.CheckedItems

        For Each item As RadComboBoxItem In collection
            v_cadena = v_cadena & item.Value & "','"
        Next

        If collection.Count = 0 Then
            v_cadena = ""
        Else
            v_cadena = v_cadena.Substring(0, Len(v_cadena) - 2)
        End If

        Return v_cadena
    End Function
End Class
