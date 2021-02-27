Imports System.Data
Imports System.Data.SqlClient
Imports Funciones
Imports Db
Imports Conexiones
Imports System
Imports System.Net
Imports System.IO
Imports Newtonsoft.Json
Imports RestSharp
Imports Class_Fichas

Partial Class _DefaultGastos
    Inherits System.Web.UI.Page
    Public Shared Function NuloAVacio(valor As Object) As String
        If Not IsDBNull(valor) Then
            Return valor.ToString()
        Else
            Return ""
        End If
    End Function

    Public Sub guardardato(ByVal bandera As String, ByVal dato As String)
        dim SSCommand as new sqlcommand
        SSCommand.CommandText = "SP_WS_GUARDAVALORESWS"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@v_bandera", SqlDbType.Decimal).Value = bandera
        SSCommand.Parameters.Add("@v_valor", SqlDbType.NVarChar).Value = dato

        Dim DS_Recibe As DataTable = Db.Consulta_Procedure(SSCommand, "WS_QUERIES")
    End Sub
    Function ContaE(ByVal NumeroCredito As String, ByVal GastoId As String, ByVal FechaMovimiento As String, ByVal MontoCancelado As String) As Integer
        Dim v_endpoint As String = "http://10.150.102.76:8686/ws_rest-0.0.1/"
        'Dim v_endpoint As String = "https://10.150.102.76/ws_rest-0.0.1/"
        Dim v_metodo As String = "RecepcionPagosGastoCobranza"

        Dim CUrl As WebRequest = WebRequest.Create(v_endpoint & v_metodo)

        Dim data As String = "[" & vbCrLf & "  {" & vbCrLf &
                                    "    ""numeroCredito"": """ & NumeroCredito & """," & vbCrLf &
                                    "    ""gastosCobranza"": [" & vbCrLf & "      {" & vbCrLf &
                                       "        ""gastoId"": """ & GastoId & """," & vbCrLf &
                                       "        ""estatus"": """ & "Cancelado" & """," & vbCrLf &
                                       "        ""fechaMovimiento"": """ & FechaMovimiento & """," & vbCrLf &
                                       "        ""fechaCancelacion"": """ & "" & """," & vbCrLf &
                                       "        ""fechaCondonacion"": """ & "" & """," & vbCrLf &
                                       "        ""montoPagado"": """ & "0" & """," & vbCrLf &
                                       "        ""montoCondonado"": """ & "0" & """," & vbCrLf &
                                       "        ""montoCancelado"": """ & MontoCancelado & """," & vbCrLf &
                                       "        ""personaCancela"": """ & "" & """," & vbCrLf &
                                       "        ""personaCondona"": """ & "" & """," & vbCrLf &
                                       "        ""motivoCancelaCondona"": """ & "" & """" & vbCrLf &
                                       "      }" & vbCrLf & "    ]," & vbCrLf & "    " & vbCrLf &
                                    "    ""folioFicha"": """ & "" & """," & vbCrLf &
                                    "    ""numeroExpediente"": """ & "" & """," & vbCrLf &
                                    "    ""ciudadRadicacion"": """ & "" & """," & vbCrLf &
                                    "    ""juzgadoRadicacion"": """ & "" & """," & vbCrLf &
                                    "    ""abogadoAsignado"": """ & "" & """," & vbCrLf &
                                    "    ""despachoAsignado"": """ & "" & """" & vbCrLf &
                                    "  }" & vbCrLf & "]" & vbCrLf

        Dim client = New RestClient(v_endpoint & v_metodo)
        Dim request = New RestRequest(Method.POST)
        request.AddHeader("cache-control", "no-cache")
        request.AddHeader("Connection", "keep-alive")
        request.AddHeader("Content-Length", data.Length)
        request.AddHeader("Accept", "*/*")
        request.AddHeader("Content-Type", "application/json")
        request.AddHeader("Autorizacion", "dXN1YXJpb1BydWViYVdTOjEyMw==")
        request.AddParameter("undefined", data, ParameterType.RequestBody)

        guardardato(1, data)

        Dim response2 As IRestResponse = client.Execute(request)

        guardardato(2, response2.Content)

        If response2.StatusCode.ToString = "OK" Then

        Else
            Return "1111111"
        End If

    End Function
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1))
        Response.Cache.SetNoStore()
        Try
            LblError.Text = ""
            PnlCancelar.Visible = False
            TxtComentario.Text = ""
            Dim v_key As String = Request.QueryString("key")
            Dim V_Bandera As String = Request.QueryString("Id_solicitud")

            If V_Bandera <> "2" And V_Bandera <> "1" Then
                LblError.Text = "Error: Solicitud De Cancelación No Existente"
            Else
                If V_Bandera = "1" Then
                    If CondonaGastos("", "", v_key, "", 4).Rows(0).Item("Resultado") = "0" Then
                        LblError.Text = "Error: Solicitud De Cancelación No Existente"
                    Else
                        Dim DtsGastos As DataTable = CondonaGastos("", "", v_key, "", 5)
                        For x As Integer = 0 To DtsGastos.Rows.Count - 1
                            Dim V_Aceptar As String = ContaE(DtsGastos.Rows(x).Item("HIST_GC_CREDITO"), DtsGastos.Rows(x).Item("HIST_GC_GASTOID"), DtsGastos.Rows(x).Item("HIST_GC_SOLICITUD"), DtsGastos.Rows(x).Item("HIST_GC_MONTOGASTO"))
                            If Val(V_Aceptar) = 0 Then
                                CondonaGastos("", DtsGastos.Rows(x).Item("HIST_GC_GASTOID"), "", "", 6)
                            Else
                                Exit For
                                LblError.Text = "Al parecer Hubo Un Problema De Comunicacion(" & V_Aceptar & "), Intente De Nuevo Desde Su Correo"
                            End If
                        Next
                    End If
                Else
                    If CondonaGastos("", "", v_key, "", 4).Rows(0).Item("Resultado") = "0" Then
                        LblError.Text = "Error: Solicitud De Cancelación No Existente"
                    Else
                        PnlCancelar.Visible = True
                        LblId.Text = v_key
                    End If
                End If
            End If
        Catch ex As Exception
            LblError.Text = "Error: Solicitud No Existente"
        End Try
    End Sub

    Protected Sub RBtnAceptar_Click(sender As Object, e As EventArgs) Handles RBtnAceptar.Click
        'Cancela Ficha
        Dim DtsFicha As DataTable = CondonaGastos("", "", LblId.Text, "", 7)
        PnlCancelar.Visible = False
        LblError.Text = "autorizacion Cancelada"
        'Else
        'LblError.Text = V_Estatus
        'End If
    End Sub
    Public Function CondonaGastos(ByVal V_Credito As String, ByVal V_Id_Gasto As String, ByVal V_Key As String, ByVal V_Comentario_S As String, ByVal V_Bandera As Integer) As DataTable
        Dim SSCommand As New SqlCommand
        SSCommand.CommandText = "SP_CONDONACION_GASTOS"
        SSCommand.CommandType = CommandType.StoredProcedure
        SSCommand.Parameters.Add("@V_Credito", SqlDbType.NVarChar).Value = V_Credito
        SSCommand.Parameters.Add("@V_Id_Gasto", SqlDbType.NVarChar).Value = V_Id_Gasto
        SSCommand.Parameters.Add("@V_Key", SqlDbType.NVarChar).Value = V_Key
        SSCommand.Parameters.Add("@V_Comentario_S", SqlDbType.NVarChar).Value = V_Comentario_S
        SSCommand.Parameters.Add("@V_Bandera", SqlDbType.Decimal).Value = V_Bandera
        Dim DtsGastos As DataTable = Consulta_Procedure(SSCommand, "SP_CONDONACION_GASTOS")
        Return DtsGastos
    End Function
End Class
