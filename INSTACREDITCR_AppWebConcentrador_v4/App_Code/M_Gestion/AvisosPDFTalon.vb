Imports System.Data
Imports Microsoft.VisualBasic
Imports Funciones

Public Class AvisosPDFTalon
    Public Shared Function generarTalon(drow As DataRow) As String
        Dim estilosCelda As String = "width:33%;padding: 0px 5px 0px 0px;"
        Dim row As String = "<tr>" &
            "<td style=""" & estilosCelda & """><b>{0}:</b> {1}</td>" &
            "<td style=""" & estilosCelda & """><b>{2}:</b> {3}</td>" &
            "<td style=""" & estilosCelda & """><b>{4}:</b> {5}</td>" &
            "</tr>"

        Dim row2 As String = "<tr>" &
            "<td><b>{0}:</b> {1}</td>" &
            "<td style=""colspan:2""><b>{2}:</b> {3}</td>" &
            "</tr>"

        Dim estilosTabla As String = "width:100%;height: 4.3cm;border-collapse:collapse;border-style:none none dashed none;text-align:justify;"
        Dim talon As New StringBuilder
        talon.AppendFormat("<table style=""{0}"">", estilosTabla)

        talon.AppendFormat(row, "CLIENTE", drow.Item("ID"), "SALDO VENCIDO", to_money(drow.Item("SALDO_VENCIDO")), "NÚMERO DE EMPLEADO", drow.Item("NOEMPLEADO"))
        talon.AppendFormat(row, "NOMBRE", drow.Item("NOMBRE"), "DÍAS MORA", drow.Item("MORA"), "FECHA IMPRESION", Now.ToShortDateString)
        talon.AppendFormat(row, "ROL", drow.Item("ROL"), "FACTURAS", drow.Item("FACTURAS"), "NOMBRE USUARIO", drow.Item("USUARIO"))
        talon.AppendFormat(row2, "# CRÉDITO", drow.Item("ACUERDO"), "FRECUENCIA DE FACTURA", drow.Item("FRECUENCIA"))
        talon.AppendFormat(row, "DOMICILIO", drow.Item("DOMICILIO"), "MONTO PRÓXIMA FACTURA", to_money(drow.Item("MPROXPAGO")), "ÚLTIMA GESTION", drow.Item("FULTIMAGESTION"))
        talon.AppendFormat(row, "TITULAR", drow.Item("TITULAR"), "FECHA PROXIMA FACTURA", drow.Item("FPROXPAGO").ToString.Split(" ")(0), "PAGOS VENCIDOS", drow.Item("ATRASO").ToString)
        talon.AppendFormat(row2, "ÚLTIMO PAGO", drow.Item("ultimopago"), "VISITADOR", drow.Item("VISITADOR"))
        talon.Append("</table></br>")

        Return talon.ToString
    End Function
End Class
