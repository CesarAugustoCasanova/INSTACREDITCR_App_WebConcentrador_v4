Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports Conexiones
Imports System.Globalization
Imports System.IO
Imports Funciones
Imports Db
Imports System.Web.SessionState.HttpSessionState
Imports System.Data
Public Class Class_Investigaciones

    Public Shared Function DataTableInv(ByVal V_Bandera As Integer, ByVal V_Usuario As String, ByVal V_Agencia As String, ByVal V_condiciones As String, ByVal RblReporte As String) As DataTable

        Dim DtsInvCampo As DataTable = SP.RP_SOLICITUDINVCAMPO(V_Bandera, V_Usuario, V_Agencia, V_condiciones)

        Try
            If RblReporte = "Solicitudes" Then
                DtsInvCampo.Columns(0).ColumnName = "Folio de Investigacion"
                DtsInvCampo.Columns(1).ColumnName = "Folio de Envio"
                DtsInvCampo.Columns(2).ColumnName = "Tipo de Investigacion"
                DtsInvCampo.Columns(3).ColumnName = "Credito"
                DtsInvCampo.Columns(4).ColumnName = "No. de Cliente"
                DtsInvCampo.Columns(5).ColumnName = "Monto Solicitado"
                DtsInvCampo.Columns(6).ColumnName = "Fecha de Respuesta"
                DtsInvCampo.Columns(7).ColumnName = "Nombre Titular"
                DtsInvCampo.Columns(8).ColumnName = "Nombre Integrante"
                DtsInvCampo.Columns(9).ColumnName = "Tipo de Integrante"
                DtsInvCampo.Columns(10).ColumnName = "Direccion"
                DtsInvCampo.Columns(11).ColumnName = "Latitud"
                DtsInvCampo.Columns(12).ColumnName = "Longitud"
                DtsInvCampo.Columns(13).ColumnName = "Horario de Verificacion"
                DtsInvCampo.Columns(14).ColumnName = "Actividad Giro"
                DtsInvCampo.Columns(15).ColumnName = "Puesto Id"
                DtsInvCampo.Columns(16).ColumnName = "Puesto"
                DtsInvCampo.Columns(17).ColumnName = "Analista Asignado"
                DtsInvCampo.Columns(18).ColumnName = "Comentario Analista"
                DtsInvCampo.Columns(19).ColumnName = "Usuario"
                DtsInvCampo.Columns(20).ColumnName = "Visitada"
                DtsInvCampo.Columns(21).ColumnName = "Requiere Visita"
                DtsInvCampo.Columns(22).ColumnName = "Resultado"
                DtsInvCampo.Columns(23).ColumnName = "Estatus MC"

            ElseIf RblReporte = "Domicilio" Then
                DtsInvCampo.Columns(0).ColumnName = "Folio de Investigacion"
                DtsInvCampo.Columns(1).ColumnName = "Folio de Envio"
                DtsInvCampo.Columns(2).ColumnName = "Tipo de Investigacion"
                DtsInvCampo.Columns(3).ColumnName = "Credito"
                DtsInvCampo.Columns(4).ColumnName = "Estatus MC"
                DtsInvCampo.Columns(5).ColumnName = "El Domicilio capturado es correcto"
                DtsInvCampo.Columns(6).ColumnName = "Comentarios Domicilio"
                DtsInvCampo.Columns(7).ColumnName = "El Domicilio fue localizado"
                DtsInvCampo.Columns(8).ColumnName = "Vecino1-Conoce a la persona a investigar"
                DtsInvCampo.Columns(9).ColumnName = "Vecino1-Vive la persona a investigar en domicilio declarado"
                DtsInvCampo.Columns(10).ColumnName = "Vecino1-Cuanto tiempo tiene el participante viviendo en el domicilio"
                DtsInvCampo.Columns(11).ColumnName = "Vecino1-Sabe a que se dedica la persona a investigar"
                DtsInvCampo.Columns(12).ColumnName = "Vecino2-Conoce a la persona a investigar"
                DtsInvCampo.Columns(13).ColumnName = "Vecino2-Vive la persona a investigar en domicilio declarad"
                DtsInvCampo.Columns(14).ColumnName = "Vecino2-Cuanto tiempo tiene el participante viviendo en el domicilio"
                DtsInvCampo.Columns(15).ColumnName = "Vecino2-Sabe a que se dedica la persona a investigar"
                DtsInvCampo.Columns(16).ColumnName = "Atendieron en Domicilio de la persona a investigar"
                DtsInvCampo.Columns(17).ColumnName = "Quien atiende"
                DtsInvCampo.Columns(18).ColumnName = "Nombre de quien atiende"
                DtsInvCampo.Columns(19).ColumnName = "Numero de contacto"
                DtsInvCampo.Columns(20).ColumnName = "Persona a investigar vive en el domicilio"
                DtsInvCampo.Columns(21).ColumnName = "Cuanto tiempo tiene la persona a investigar  viviendo en el domicilio"
                DtsInvCampo.Columns(22).ColumnName = "Tiene dependientes economicos"
                DtsInvCampo.Columns(23).ColumnName = "Cuantos"
                DtsInvCampo.Columns(24).ColumnName = "Sabe a que se dedica la persona a investigar"
                DtsInvCampo.Columns(25).ColumnName = "Cuanto tiempo tiene la persona a investigar dedicandose a esta actividad"
                DtsInvCampo.Columns(26).ColumnName = "Cual es la finalidad del credito"
                DtsInvCampo.Columns(27).ColumnName = "Observaciones Generales"
                DtsInvCampo.Columns(28).ColumnName = "Requiere Visita"
                DtsInvCampo.Columns(29).ColumnName = "Resultado"
                DtsInvCampo.Columns(30).ColumnName = "Motivo"
                DtsInvCampo.Columns(31).ColumnName = "Latitud"
                DtsInvCampo.Columns(32).ColumnName = "Longitud"
                DtsInvCampo.Columns(33).ColumnName = "Fecha de Visita"
                DtsInvCampo.Columns(34).ColumnName = "Usuario"

            ElseIf RblReporte = "Ingresos" Then
                DtsInvCampo.Columns(0).ColumnName = "Folio de Investigacion"
                DtsInvCampo.Columns(1).ColumnName = "Folio de Envio"
                DtsInvCampo.Columns(2).ColumnName = "Tipo de Investigacion"
                DtsInvCampo.Columns(3).ColumnName = "Credito"
                DtsInvCampo.Columns(4).ColumnName = "Estatus MC"
                DtsInvCampo.Columns(5).ColumnName = "El Domicilio capturado es correcto"
                DtsInvCampo.Columns(6).ColumnName = "El Domicilio fue localizado"
                DtsInvCampo.Columns(7).ColumnName = "Vecino1-Conoce al Sr@"
                DtsInvCampo.Columns(8).ColumnName = "Vecino1-Sabe que actividad realiza el Sr."
                DtsInvCampo.Columns(9).ColumnName = "Vecino1-Cuanto tiempo lleva el Sr. dedicandose a su actividad"
                DtsInvCampo.Columns(10).ColumnName = "Vecino2-Conoce al Sr@ "
                DtsInvCampo.Columns(11).ColumnName = "Vecino2-Sabe que actividad realiza el Sr."
                DtsInvCampo.Columns(12).ColumnName = "Vecino2-Cuanto tiempo lleva el Sr. dedicandose a su actividad"
                DtsInvCampo.Columns(13).ColumnName = "Atendieron en el Domicilio donde realiza su actividad economica"
                DtsInvCampo.Columns(14).ColumnName = "Quien atendio"
                DtsInvCampo.Columns(15).ColumnName = "Nombre de quien atendio"
                DtsInvCampo.Columns(16).ColumnName = "Dias que labora"
                DtsInvCampo.Columns(17).ColumnName = "Cuanto tiempo lleva en su actividad"
                DtsInvCampo.Columns(18).ColumnName = "Como recibe sus ingresos"
                DtsInvCampo.Columns(19).ColumnName = "Ingresos Libres al Mes"
                DtsInvCampo.Columns(20).ColumnName = "El cliente se dedica a lo que declara en solicitud"
                DtsInvCampo.Columns(21).ColumnName = "Observaciones Generales"
                DtsInvCampo.Columns(22).ColumnName = "Requiere Visita"
                DtsInvCampo.Columns(23).ColumnName = "Resultado"
                DtsInvCampo.Columns(24).ColumnName = "Motivo"
                DtsInvCampo.Columns(25).ColumnName = "Latitud"
                DtsInvCampo.Columns(26).ColumnName = "Longitud"
                DtsInvCampo.Columns(27).ColumnName = "Fecha de Visita"
                DtsInvCampo.Columns(28).ColumnName = "Usuario"
            End If


        Catch ex As Exception

        End Try

        Return DtsInvCampo

    End Function



End Class