Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports Db
Imports System.Web.SessionState.HttpSessionState
Imports System.Data

Public Class LlenarAplicacion

    Public Shared Sub APLICACION()
        Dim SSCommand As New sqlcommand With {
            .CommandText = "SP_CATALOGOS",
            .CommandType = CommandType.StoredProcedure
        }
        SSCommand.Parameters.Add("@V_BANDERA", SqlDbType.Decimal).Value = 1
        SSCommand.Parameters.Add("@V_Valor", SqlDbType.NVarChar).Value = ""
        Try


            Dim DtsLlenarAplicacion As DataTable = Consulta_Procedure(SSCommand, "APLICACION")

            Dim TmpAplicacion As Aplicacion = HttpContext.Current.Session("Aplicacion")

            For a As Integer = 0 To DtsLlenarAplicacion.Rows.Count - 1
                Select Case DtsLlenarAplicacion.Rows(a)("CAT_VA_DESCRIPCION")
                    Case Is = "Letra"
                        TmpAplicacion.LETRA = DtsLlenarAplicacion.Rows(a)("CAT_VA_VALOR")
                    Case Is = "Consecutivo"
                        TmpAplicacion.CONSECUTIVO = DtsLlenarAplicacion.Rows(a)("CAT_VA_VALOR")
                    Case Is = "PROMESAS DE PAGO"
                        TmpAplicacion.PROMESAS_PAGO = DtsLlenarAplicacion.Rows(a)("CAT_VA_VALOR")
                    Case Is = "Negociaciones"
                        TmpAplicacion.NEGOCIACIONES = DtsLlenarAplicacion.Rows(a)("CAT_VA_VALOR")
                    Case Is = "Codigo Accion"
                        TmpAplicacion.ACCION = DtsLlenarAplicacion.Rows(a)("CAT_VA_VALOR")
                    Case Is = "Codigo Resultado"
                        TmpAplicacion.RESULTADO = DtsLlenarAplicacion.Rows(a)("CAT_VA_VALOR")
                    Case Is = "Codigo Causa No Pago"
                        TmpAplicacion.NOPAGO = DtsLlenarAplicacion.Rows(a)("CAT_VA_VALOR")
                End Select
            Next
        Catch ex As Exception
        End Try
    End Sub
End Class
