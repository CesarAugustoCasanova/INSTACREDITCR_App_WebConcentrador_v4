Imports System.Data
Imports System.Data.SqlClient
Imports Funciones
Imports Db
Imports Conexiones
Imports System
Imports System.Net
Imports System.IO
Imports Newtonsoft.Json
Partial Class _Default
    Inherits System.Web.UI.Page
    Public Shared Function NuloAVacio(valor As Object) As String
        If Not IsDBNull(valor) Then
            Return valor.ToString()
        Else
            Return ""
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
                LblError.Text = "Error: Solicitud No Existente"
            Else
                If V_Bandera = "1" Then
                    If Ficha_Nego(v_key, 0, "").Rows(0).Item("Resultado") = "0" Then
                        LblError.Text = "Error: Solicitud No Existente"
                    Else
                        Dim DtsFicha As DataTable = Ficha_Nego(v_key, 1, "")
                        Dim V_Aceptar As String = Safi_fichaNegociacionRegistro(NuloAVacio(DtsFicha.Rows(0).Item("tmp_fi_folioFicha")), NuloAVacio(DtsFicha.Rows(0).Item("tmp_fi_tipoFicha")), NuloAVacio(DtsFicha.Rows(0).Item("tmp_fi_usuarioEnvioID")), NuloAVacio(DtsFicha.Rows(0).Item("tmp_fi_usuarioEnvioNombre")), NuloAVacio(DtsFicha.Rows(0).Item("tmp_fi_usuarioIP")), NuloAVacio(DtsFicha.Rows(0).Item("tmp_fi_clienteID")), NuloAVacio(DtsFicha.Rows(0).Item("tmp_fi_creditoID")), NuloAVacio(DtsFicha.Rows(0).Item("tmp_fi_tipoPago")), NuloAVacio(DtsFicha.Rows(0).Item("tmp_fi_totalPagar")), NuloAVacio(DtsFicha.Rows(0).Item("tmp_fi_fechaPago")), NuloAVacio(DtsFicha.Rows(0).Item("tmp_fi_observaciones")), NuloAVacio(DtsFicha.Rows(0).Item("tmp_fi_valorNetoActivo")), NuloAVacio(DtsFicha.Rows(0).Item("tmp_fi_capital")), NuloAVacio(DtsFicha.Rows(0).Item("tmp_fi_interes")), NuloAVacio(DtsFicha.Rows(0).Item("tmp_fi_ivaInteres")), NuloAVacio(DtsFicha.Rows(0).Item("tmp_fi_moratorio")), NuloAVacio(DtsFicha.Rows(0).Item("tmp_fi_ivaMoratorio")), NuloAVacio(DtsFicha.Rows(0).Item("tmp_fi_comisiones")), NuloAVacio(DtsFicha.Rows(0).Item("tmp_fi_ivaComisiones")), NuloAVacio(DtsFicha.Rows(0).Item("tmp_fi_cC_comisiones")), NuloAVacio(DtsFicha.Rows(0).Item("tmp_fi_cC_moratorios")), NuloAVacio(DtsFicha.Rows(0).Item("tmp_fi_cC_interes")), NuloAVacio(DtsFicha.Rows(0).Item("tmp_fi_cC_capital")), NuloAVacio(DtsFicha.Rows(0).Item("tmp_fi_gastosCobranza")), NuloAVacio(DtsFicha.Rows(0).Item("tmp_fi_ivaGastosCob")), NuloAVacio(DtsFicha.Rows(0).Item("tmp_fi_honorario")), NuloAVacio(DtsFicha.Rows(0).Item("tmp_fi_despachoID")), NuloAVacio(DtsFicha.Rows(0).Item("tmp_fi_despachoNombre")), NuloAVacio(DtsFicha.Rows(0).Item("tmp_fi_tipoDespacho")), NuloAVacio(DtsFicha.Rows(0).Item("tmp_fi_abogadoID")), NuloAVacio(DtsFicha.Rows(0).Item("tmp_fi_abogadoNombre")), NuloAVacio(DtsFicha.Rows(0).Item("tmp_fi_supervisorID")), NuloAVacio(DtsFicha.Rows(0).Item("tmp_fi_supervisorNombre")), NuloAVacio(DtsFicha.Rows(0).Item("tmp_fi_folioDacion")), NuloAVacio(DtsFicha.Rows(0).Item("tmp_fi_descripcion")), NuloAVacio(DtsFicha.Rows(0).Item("tmp_fi_valorComercial")), NuloAVacio(DtsFicha.Rows(0).Item("tmp_fi_valorAplicar")), NuloAVacio(DtsFicha.Rows(0).Item("tmp_fi_numCuenta")), NuloAVacio(DtsFicha.Rows(0).Item("tmp_fi_monto")))
                        If V_Aceptar.Split(",")(0).Trim = "000000" Then
                            Ficha_Nego(v_key, 2, "")
                            LblError.Text = "Solicitud Autorizada"
                        Else
                            LblError.Text = V_Aceptar
                        End If
                    End If
                Else
                    If Ficha_Nego(v_key, 0, "").Rows(0).Item("Resultado") = "0" Then
                        LblError.Text = "Error: Solicitud No Existente"
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
        Dim DtsFicha As DataTable = Ficha_Nego(LblId.Text, 1, "")
        'Dim V_Estatus As String = Safi_cancelarFichaNegociacion(DtsFicha.Rows(0).Item("tmp_fi_folioFicha"), DtsFicha.Rows(0).Item("tmp_fi_usuarioEnvioID"), DtsFicha.Rows(0).Item("tmp_fi_usuarioEnvioNombre"), DtsFicha.Rows(0).Item("tmp_fi_usuarioIP"))
        ' If V_Estatus.Split(",")(0).Trim = "000000" Then
        Dim DtsCancelaFicha As DataTable = Ficha_Nego(LblId.Text, 3, TxtComentario.Text)
        PnlCancelar.Visible = False
        LblError.Text = "Ficha Cancelada"
        'Else
        'LblError.Text = V_Estatus
        'End If
    End Sub
    Protected Function Ficha_Nego(ByVal V_KEY As String, ByVal V_BANDERA As String, ByVal V_COMENTARIO As String) As DataTable
        Dim SSCommandP As New SqlCommand
        SSCommandP.CommandText = "SP_FICHA_NEGO"
        SSCommandP.CommandType = CommandType.StoredProcedure
        SSCommandP.Parameters.Add("@V_KEY", SqlDbType.NVarChar).Value = V_KEY
        SSCommandP.Parameters.Add("@V_COMENTARIO", SqlDbType.NVarChar).Value = V_COMENTARIO
        SSCommandP.Parameters.Add("@V_BANDERA", SqlDbType.NVarChar).Value = V_BANDERA
        Dim DtsFicha As DataTable = Consulta_Procedure(SSCommandP, "SP_FICHA_NEGO")
        Return DtsFicha
    End Function
    'Protected Function Safi_cancelarFichaNegociacion(ByVal folioFicha As String, ByVal usuarioEnvioID As String, ByVal usuarioEnvioNombre As String, ByVal usuarioIP As String) As String
    '    Try
    '        Dim v_endpoint As String = Db.StrEndPoint("SAFI", 1)
    '        Dim v_metodo As String = "cartera/cancelarFichaNegociacion"

    '        Dim CUrl As WebRequest = WebRequest.Create(v_endpoint & v_metodo)

    '        Dim data As String = "{" & vbLf & vbTab &
    '                        """folioFicha"":""" & folioFicha & """," & vbLf &
    '                        """usuarioEnvioID"":""" & usuarioEnvioID & """," & vbLf &
    '                        """usuarioEnvioNombre"":""" & usuarioEnvioNombre & """," & vbLf &
    '                        """usuarioIP"":""" & usuarioIP & """" & vbLf &
    '                              "}" & vbCrLf

    '        CUrl.Method = "POST"
    '        CUrl.ContentLength = data.Length
    '        CUrl.ContentType = "application/json; charset=UTF-8"
    '        Dim enc As New UTF8Encoding()
    '        CUrl.Headers.Add("Autentificacion", Convert.ToBase64String(enc.GetBytes(Db.StrEndPoint("SAFI", 2) & ":" & Db.StrEndPoint("SAFI", 3))))

    '        Using ds As Stream = CUrl.GetRequestStream()
    '            ds.Write(enc.GetBytes(data), 0, data.Length)
    '        End Using

    '        Dim wr As WebResponse = CUrl.GetResponse()
    '        Dim receiveStream As Stream = wr.GetResponseStream()
    '        Dim reader As New StreamReader(receiveStream, Encoding.UTF8)

    '        Dim v_json_resp As String = reader.ReadToEnd()


    '        Dim safi_cancelarFichaNegociacions As safi_cancelarFichaNegociacion
    '        safi_cancelarFichaNegociacions = JsonConvert.DeserializeObject(Of safi_cancelarFichaNegociacion)(v_json_resp)

    '        Dim codigoRespuesta As String = safi_cancelarFichaNegociacions.codigoRespuesta
    '        Dim mensajeRespuesta As String = safi_cancelarFichaNegociacions.mensajeRespuesta

    '        'Response.Close()
    '        reader.Close()
    '        Return codigoRespuesta & "," & mensajeRespuesta
    '    Catch ex As WebException
    '        Dim V_error As String = ex.Message
    '        Return V_error
    '    End Try
    'End Function

    Protected Function Safi_fichaNegociacionRegistro(ByVal folioFicha As String, ByVal tipoFicha As String, ByVal usuarioEnvioID As String, ByVal usuarioEnvioNombre As String, ByVal usuarioIP As String, ByVal clienteID As String, ByVal creditoID As String, ByVal tipoPago As String, ByVal totalPagar As String, ByVal fechaPago As String, ByVal observaciones As String, ByVal valorNetoActivo As String, ByVal capital As String, ByVal interes As String, ByVal ivaInteres As String, ByVal moratorio As String, ByVal ivaMoratorio As String, ByVal comisiones As String, ByVal ivaComisiones As String, ByVal cC_comisiones As String, ByVal cC_moratorios As String, ByVal cC_interes As String, ByVal cC_capital As String, ByVal gastosCobranza As String, ByVal ivaGastosCob As String, ByVal honorario As String, ByVal despachoID As String, ByVal despachoNombre As String, ByVal tipoDespacho As String, ByVal abogadoID As String, ByVal abogadoNombre As String, ByVal supervisorID As String, ByVal supervisorNombre As String, ByVal folioDacion As String, ByVal descripcion As String, ByVal valorComercial As String, ByVal valorAplicar As String, ByVal numCuenta As String, ByVal monto As String) As String
        Try
            Dim v_endpoint As String = Db.StrEndPoint("SAFI", 1)
            Dim v_metodo As String = "cobranza/fichaNegociacionRegistro"

            Dim dtsahorro As DataTable = Class_Fichas.LlenarSaldosCredito2(creditoID, 12, "", "", "")
            Dim cuentasahorro As String = ""
            If dtsahorro.Rows.Count = 0 Then
                cuentasahorro = "{" & vbCrLf &
                     " ""numCuenta"" : """"," & vbCrLf &
                     " ""monto"" : """"" & vbCrLf &
                          "    }"
            Else
                For i = 0 To dtsahorro.Rows.Count - 1
                    cuentasahorro = cuentasahorro & "{" & vbCrLf & " ""numCuenta"" : """ & dtsahorro.Rows(i).Item("CUENTAID") & """," & vbCrLf &
                         " ""monto"" : """ & dtsahorro.Rows(i).Item("SALDO_DISPUESTO") & """" & vbCrLf &
                          "    },"
                Next
                cuentasahorro = cuentasahorro.Substring(0, cuentasahorro.Length - 1)
            End If

            Dim CUrl As WebRequest = WebRequest.Create(v_endpoint & v_metodo)

            Dim data As String = "{ " & vbCrLf &
                " ""folioFicha"":""" & folioFicha & """" & vbCrLf &
                " ,""tipoFicha"":""" & tipoFicha & """" & vbCrLf &
                " ,""usuarioEnvioID"":""" & usuarioEnvioID & """" & vbCrLf &
                " ,""usuarioEnvioNombre"":""" & usuarioEnvioNombre & """" & vbCrLf &
                " ,""usuarioIP"":""" & usuarioIP & """" & vbCrLf &
                " ,""clienteID"":""" & clienteID & """" & vbCrLf &
                " ,""creditoID"":""" & creditoID & """" & vbCrLf &
                " ,""tipoPago"":""" & tipoPago & """" & vbCrLf &
                " ,""totalPagar"":""" & totalPagar & """" & vbCrLf &
                " ,""fechaPago"":""" & fechaPago & """" & vbCrLf &
                " ,""observaciones"" :  """ & observaciones & """" & vbCrLf &
                " ,""valorNetoActivo"" : """ & valorNetoActivo & """" & vbCrLf &
                " ,""saldoSimulado""  : {" & vbCrLf &
                    " ""capital""  :  """ & capital & """," & vbCrLf &
                    " ""interes""  :  """ & interes & """," & vbCrLf &
                    " ""ivaInteres"" :""" & ivaInteres & """," & vbCrLf &
                    " ""moratorio""  : """ & moratorio & """," & vbCrLf &
                    " ""ivaMoratorio"" : """ & ivaMoratorio & """," & vbCrLf &
                    " ""comisiones""   : """ & comisiones & """, " & vbCrLf &
                    " ""ivaComisiones"": """ & ivaComisiones & """" & vbCrLf &
                         " }," & vbCrLf &
                " ""condonacionCob"" : {" & vbCrLf &
                     " ""comisiones""  : """ & cC_comisiones & """," & vbCrLf &
                     " ""moratorios""  :  """ & cC_moratorios & """," & vbCrLf &
                     " ""interes""     :  """ & cC_interes & """," & vbCrLf &
                     " ""capital""     :  """ & cC_capital & """" & vbCrLf &
                          " }," & vbCrLf &
                " ""gastosCobranza""  : """ & gastosCobranza & """," & vbCrLf &
                " ""ivaGastosCob""   :  """ & ivaGastosCob & """," & vbCrLf &
                " ""honorario""      :  """ & honorario & """," & vbCrLf &
                " ""despachoCob"": {" & vbCrLf &
                     " ""despachoID"":""" & despachoID & """" & vbCrLf &
                     " ,""despachoNombre"":""" & despachoNombre & """" & vbCrLf &
                     " ,""tipoDespacho"":""" & tipoDespacho & """" & vbCrLf &
                     " ,""abogadoID"":""" & abogadoID & """" & vbCrLf &
                     " ,""abogadoNombre"":""" & abogadoNombre & """" & vbCrLf &
                     " ,""supervisorID"":""" & supervisorID & """" & vbCrLf &
                     " ,""supervisorNombre"":""" & supervisorNombre & """" & vbCrLf &
                          "    }," & IIf(tipoFicha = "03" Or tipoFicha = "04", vbCrLf &
                 " ""dacionAdjudicacion"": [{" & vbCrLf &
                     " ""folioDacion"" : """ & folioDacion & """," & vbCrLf &
                     " ""descripcion"" : """ & descripcion & """," & vbCrLf &
                     " ""valorComercial"" : """ & valorComercial & """," & vbCrLf &
                     " ""valorAplicar"" : """ & valorAplicar & """" & vbCrLf &
                          "    }],", "") & vbCrLf &
                  " ""pagoCargoCta"": [" & cuentasahorro &
                          "]" & vbCrLf & "}" & vbCrLf

            CUrl.Method = "POST"
            CUrl.ContentLength = data.Length
            CUrl.ContentType = "application/json; charset=UTF-8"
            'CUrl.Timeout = 3000
            Dim enc As New UTF8Encoding()
            CUrl.Headers.Add("Autentificacion", Convert.ToBase64String(enc.GetBytes(Db.StrEndPoint("SAFI", 2) & ":" & Db.StrEndPoint("SAFI", 3))))

            Using ds As Stream = CUrl.GetRequestStream()
                ds.Write(enc.GetBytes(data), 0, data.Length)
            End Using

            Dim wr As WebResponse = CUrl.GetResponse()
            Dim receiveStream As Stream = wr.GetResponseStream()
            Dim reader As New StreamReader(receiveStream, Encoding.UTF8)

            Dim v_json_resp As String = reader.ReadToEnd()




            Class_Fichas.guardarespuesta(v_json_resp, folioFicha, data)

            ' Response.Close()
            Return ""
        Catch ex As WebException
            Dim V_Error As String = ex.Message
            Return V_Error
        End Try
    End Function
End Class
