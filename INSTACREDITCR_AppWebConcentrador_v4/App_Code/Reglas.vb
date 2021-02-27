Imports Microsoft.VisualBasic
Imports Telerik.Web.UI

Public Class Reglas
    Public Shared Function getGridValues(usrControl As UserControl) As Hashtable
        Dim valores As New Hashtable
        Dim V_Multiple As String = 0

        If CType(usrControl.FindControl("RadMultiple"), RadComboBox).Visible = True Then
            V_Multiple = 1
        End If

        Try
            valores.Add("tablaValue", CType(usrControl.FindControl("comboTablas"), RadComboBox).SelectedValue)
            valores.Add("tablaText", CType(usrControl.FindControl("comboTablas"), RadComboBox).SelectedItem.Text)
        Catch ex As Exception
            valores.Add("tablaText", "")
        End Try

        Try
            valores.Add("campoValue", CType(usrControl.FindControl("comboCampo"), RadComboBox).SelectedValue)
            valores.Add("campoText", CType(usrControl.FindControl("comboCampo"), RadComboBox).SelectedItem.Text)
        Catch ex As Exception
            valores.Add("campoText", "")
        End Try

        Try
            valores.Add("operadorValue", CType(usrControl.FindControl("DdlOperador"), RadComboBox).SelectedValue)
            valores.Add("operadorText", CType(usrControl.FindControl("DdlOperador"), RadComboBox).SelectedItem.Text)
        Catch ex As Exception
            valores.Add("operadorText", "")
        End Try

        Try
            valores.Add("conectorValue", CType(usrControl.FindControl("DdlConector"), RadComboBox).SelectedValue)
            valores.Add("conectorText", CType(usrControl.FindControl("DdlConector"), RadComboBox).SelectedItem.Text)
        Catch ex As Exception
            valores.Add("conectorText", "")
        End Try

        valores.Add("consecutivo", CType(usrControl.FindControl("lblConsecutivo"), RadLabel).Text)

        Dim tipo As String = CType(usrControl.FindControl("comboCampo"), RadComboBox).SelectedItem.Attributes.Item("Tipo")
        valores.Add("tipo", tipo)
        Select Case tipo
            Case "numeric", "decimal", "int", "number", "bigint"
                If V_Multiple = 1 Then
                    valores.Add("valor", "(" & Rad_Ncadena(CType(usrControl.FindControl("RadMultiple"), RadComboBox)) & ")")
                Else
                    valores.Add("valor", CType(usrControl.FindControl("NumValores"), RadNumericTextBox).Text)
                End If
            Case "date", "datetime"
                If V_Multiple = 1 Then
                    valores.Add("valor", "(" & Rad_Tcadena(CType(usrControl.FindControl("RadMultiple"), RadComboBox)) & ")")
                Else
                    valores.Add("valor", CType(usrControl.FindControl("DteValores"), RadDatePicker).DbSelectedDate)
                End If
            Case Else
                If V_Multiple = 1 Then
                    valores.Add("valor", "(" & Rad_Tcadena(CType(usrControl.FindControl("RadMultiple"), RadComboBox)) & ")")
                Else
                    valores.Add("valor", CType(usrControl.FindControl("TxtValores"), RadTextBox).Text)
                End If
        End Select
        Return valores
    End Function

    Private Shared Function Rad_Tcadena(ByRef v_item As RadComboBox) As String
        Dim v_cadena As String = ""
        Dim collection As IList(Of RadComboBoxItem) = v_item.CheckedItems

        If collection.Count > 0 Then
            For Each item As RadComboBoxItem In collection
                v_cadena &= "'" & item.Value & "',"
            Next
            v_cadena = v_cadena.Substring(0, v_cadena.Length - 1)
        End If

        Return v_cadena
    End Function
    Private Shared Function Rad_Ncadena(ByRef v_item As RadComboBox) As String
        Dim v_cadena As String = ""
        Dim collection As IList(Of RadComboBoxItem) = v_item.CheckedItems

        If collection.Count > 0 Then
            For Each item As RadComboBoxItem In collection
                v_cadena &= item.Value & ","
            Next

            v_cadena = v_cadena.Substring(0, v_cadena.Length - 1)
        End If

        Return v_cadena
    End Function
End Class
