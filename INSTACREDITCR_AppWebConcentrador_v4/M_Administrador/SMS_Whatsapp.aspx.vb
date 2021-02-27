Imports System.Data
Imports System.Data.SqlClient
Imports Db
Imports Funciones
Imports Telerik.Web.UI

Partial Class SMS_Whatsapp
    Inherits System.Web.UI.Page
    Public Property tmpUSUARIO As IDictionary
        Get
            Return CType(Session("USUARIO"), IDictionary)
        End Get
        Set(value As IDictionary)
            Session("USUARIO") = value
        End Set
    End Property
    Function Llenar(V_Cat_Sm_Id As Integer, V_Cat_Sm_Descripcion As String, V_Cat_Sm_Valor As String, V_Cat_Sm_Tabla As String, V_Cat_Sm_Camporeal As String, V_Bandera As Integer) As DataTable
        Dim SSCommandAgencias As New SqlCommand
        SSCommandAgencias.CommandText = "SP_ADD_CAT_ETIQUETAS_WHATSAPP"
        SSCommandAgencias.CommandType = CommandType.StoredProcedure
        SSCommandAgencias.Parameters.Add("@V_Cat_Sm_Id", SqlDbType.Decimal).Value = V_Cat_Sm_Id
        SSCommandAgencias.Parameters.Add("@V_Cat_Sm_Descripcion", SqlDbType.NVarChar).Value = V_Cat_Sm_Descripcion
        SSCommandAgencias.Parameters.Add("@V_Cat_Sm_Valor", SqlDbType.NVarChar).Value = V_Cat_Sm_Valor
        SSCommandAgencias.Parameters.Add("@V_Cat_Sm_Tabla", SqlDbType.NVarChar).Value = V_Cat_Sm_Tabla
        SSCommandAgencias.Parameters.Add("@V_Cat_Sm_Camporeal", SqlDbType.NVarChar).Value = V_Cat_Sm_Camporeal
        SSCommandAgencias.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = V_Bandera

        Dim DtsVarios As DataTable = Consulta_Procedure(SSCommandAgencias, "Etiqueta")


        Return DtsVarios
    End Function
    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Administrador", "CatalogoEtiquetasSMS_Whatsapp.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub

    Protected Sub Aviso(ByVal MSJ As String)
        RadAviso.RadAlert(MSJ, 440, 155, "AVISO", Nothing)
    End Sub


    Public Sub RGVEtiquetas_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RGVEtiquetas.NeedDataSource
        RGVEtiquetas.DataSource = Llenar(0, "", "", "", "", 0)
    End Sub

    Private Sub RGVEtiquetas_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RGVEtiquetas.ItemCommand
        Session("PostBack") = True
        Session("Tabla") = ""
        Session("Campo") = ""
        If e.CommandName = "Update" Then



            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(5) As String


            valores(0) = CType(MyUserControl.FindControl("TxtCat_Sm_Descripcion"), RadTextBox).Text
            valores(1) = CType(MyUserControl.FindControl("DdlTabla"), RadDropDownList).SelectedValue
            valores(2) = CType(MyUserControl.FindControl("DdlCampo"), RadDropDownList).SelectedValue
            valores(2) = Session("CampoValue")

            valores(3) = CType(MyUserControl.FindControl("LblId"), RadLabel).Text
            valores(4) = CType(MyUserControl.FindControl("DdlCampo"), RadDropDownList).SelectedText
            valores(4) = Session("CampoText")
            If valores(4).Length > 199 Then
                valores(4) = valores(4).Substring(0, 199)
            End If
            If valores(0) = "" Then
                Aviso("Escriba El Nombre La Etiqueta")
                e.Canceled = True
            ElseIf valores(1) = "Seleccione" Then
                Aviso("Seleccione Una Tabla")
                e.Canceled = True
            ElseIf valores(2) = "Seleccione" Then
                Aviso("Seleccione Un Campo")
                e.Canceled = True
            Else
                Try
                    Dim SSCommandAgencias As New SqlCommand
                    SSCommandAgencias.CommandText = "SP_ADD_CAT_ETIQUETAS_WHATSAPP"
                    SSCommandAgencias.CommandType = CommandType.StoredProcedure
                    SSCommandAgencias.Parameters.Add("@V_Cat_Sm_Id", SqlDbType.Decimal).Value = valores(3)
                    SSCommandAgencias.Parameters.Add("@V_Cat_Sm_Descripcion", SqlDbType.NVarChar).Value = valores(0)
                    SSCommandAgencias.Parameters.Add("@V_Cat_Sm_Valor", SqlDbType.NVarChar).Value = ""
                    SSCommandAgencias.Parameters.Add("@V_Cat_Sm_Tabla", SqlDbType.NVarChar).Value = valores(1)
                    SSCommandAgencias.Parameters.Add("@V_Cat_Sm_Camporeal", SqlDbType.NVarChar).Value = valores(2)
                    SSCommandAgencias.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 13

                    Dim DtsVarios As DataTable = Consulta_Procedure(SSCommandAgencias, "Fuente")
                    If DtsVarios.Rows(0).Item("Cuantos") = 0 Then
                        Llenar(valores(3), valores(0), valores(4), valores(1), valores(2), 2)
                        Aviso("Se Han Guardado Los Cambios")
                    Else
                        Aviso("ERROR. El nombre de la etiqueta ya existe o ya existe una etiqueta con la misma tabla y campo pero con diferente nombre.")
                    End If
                Catch ex As Exception

                    Aviso("No Fue Posible Actualizar. Razon: " + ex.Message)
                    e.Canceled = True
                End Try
            End If
        ElseIf e.CommandName = "PerformInsert" Then
            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(5) As String


            valores(0) = CType(MyUserControl.FindControl("TxtCat_Sm_Descripcion"), RadTextBox).Text
            valores(1) = CType(MyUserControl.FindControl("DdlTabla"), RadDropDownList).SelectedValue
            valores(2) = CType(MyUserControl.FindControl("Ddlcampo"), RadDropDownList).SelectedValue
            valores(2) = Session("CampoValue")
            valores(3) = CType(MyUserControl.FindControl("LblId"), RadLabel).Text
            valores(4) = CType(MyUserControl.FindControl("Ddlcampo"), RadDropDownList).SelectedText
            valores(4) = Session("CampoText")
            If valores(4).Length > 199 Then
                valores(4) = valores(4).Substring(0, 199)
            End If
            If valores(0) = "" Then
                Aviso("Escriba El Nombre La Etiqueta")
                e.Canceled = True

            ElseIf valores(1) = "Seleccione" Then
                Aviso("Seleccione Una Tabla")
                e.Canceled = True
            ElseIf valores(2) = "Seleccione" Then
                Aviso("Seleccione Un Campo")
                e.Canceled = True
            Else
                Try
                    Dim SSCommandAgencias As New SqlCommand
                    SSCommandAgencias.CommandText = "SP_ADD_CAT_ETIQUETAS_WHATSAPP"
                    SSCommandAgencias.CommandType = CommandType.StoredProcedure
                    SSCommandAgencias.Parameters.Add("@V_Cat_Sm_Id", SqlDbType.Decimal).Value = 0
                    SSCommandAgencias.Parameters.Add("@V_Cat_Sm_Descripcion", SqlDbType.NVarChar).Value = valores(0)
                    SSCommandAgencias.Parameters.Add("@V_Cat_Sm_Valor", SqlDbType.NVarChar).Value = ""
                    SSCommandAgencias.Parameters.Add("@V_Cat_Sm_Tabla", SqlDbType.NVarChar).Value = valores(1)
                    SSCommandAgencias.Parameters.Add("@V_Cat_Sm_Camporeal", SqlDbType.NVarChar).Value = valores(2)
                    SSCommandAgencias.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 4

                    Dim DtsVarios As DataTable = Consulta_Procedure(SSCommandAgencias, "Fuente")
                    If DtsVarios.Rows(0).Item("Cuantos") = 0 Then
                        Llenar(1, valores(0), valores(4), valores(1), valores(2), 1)
                        Aviso("Etiqueta Creada")
                    Else
                        Aviso("La Etiqueta Ya Existe Valide")
                        e.Canceled = True
                    End If
                Catch ex As Exception
                    e.Canceled = True
                    Aviso("No Fue Posible Insertar El Usuario. Razon: " + ex.Message)
                End Try


            End If

        ElseIf e.CommandName = "Delete" Then

            Dim ID As String = (CType(e.Item, GridDataItem)).OwnerTableView.DataKeyValues(e.Item.ItemIndex)("Id").ToString
            Dim Tipo As String = e.Item.Cells.Item(4).Text
            Llenar(ID, "", "", "", "", 3)
            Aviso("Se Ha Eliminado La Etiqueta ")

        ElseIf e.CommandName = "InitInsert" Then
            'Session("PostBack") = False
        ElseIf e.CommandName = "Edit" Then
            Dim ID As String = (CType(e.Item, GridDataItem)).OwnerTableView.DataKeyValues(e.Item.ItemIndex)("Id").ToString
            Session("Tabla") = Llenar(ID, "", "", "", "", 11).Rows(0).Item(0)
            Session("Campo") = Llenar(ID, "", "", "", "", 12).Rows(0).Item(0)
        End If
    End Sub

    Public Sub RGVPlantillas_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RGVPlantillas.NeedDataSource
        RGVPlantillas.DataSource = Llenar(0, "", "", "", "", 9)
    End Sub



    Private Sub RGVPlantillas_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RGVPlantillas.ItemCommand
        Session("LLenar") = True
        RGVEtiquetas.Enabled = False
        If e.CommandName = "Update" Then
            RGVEtiquetas.Enabled = True
            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(2) As String


            valores(0) = CType(MyUserControl.FindControl("TxtCAT_PL_NOMBRE"), RadTextBox).Text
            valores(1) = CType(MyUserControl.FindControl("TxtCAT_PL_MENSAJE"), RadTextBox).Text

            If valores(0) = "" Then
                Aviso("Escriba El Nombre De La Plantilla")
                e.Canceled = True
            ElseIf valores(1).Length < 20 Then
                Aviso("Escriba El Mensaje De La Plantilla mayor a 20 caracteres")
                e.Canceled = True
            ElseIf valores(1).Length > 480 Then
                Aviso("El Largo Del Mensaje No Puede Exceder Los 480 Caracteres ")
                e.Canceled = True
            Else
                Try
                    Llenar(1, valores(1), "", valores(0), "", 8)
                    Aviso("Se Han Guardado Los Cambios")

                    RGVPlantillas.Rebind()
                Catch ex As Exception
                    SendMail("BtnAccion_Click", ex, "", "", HidenUrs.Value)
                    e.Canceled = True
                    Aviso("No fue posible actualizar la plantilla")
                End Try
            End If


        ElseIf e.CommandName = "PerformInsert" Then
            RGVEtiquetas.Enabled = True
            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim MyUserControl As UserControl = CType(e.Item.FindControl(GridEditFormItem.EditFormUserControlID), UserControl)
            Dim valores(2) As String


            valores(0) = CType(MyUserControl.FindControl("TxtCAT_PL_NOMBRE"), RadTextBox).Text
            valores(1) = CType(MyUserControl.FindControl("TxtCAT_PL_MENSAJE"), RadTextBox).Text

            If valores(0) = "" Then
                Aviso("Escriba El Nombre De La Plantilla")
                e.Canceled = True
            ElseIf valores(1).Length < 20 Then
                Aviso("Escriba El Mensaje De La Plantilla mayor a 20 caracteres")
                e.Canceled = True
            ElseIf valores(1).Length > 480 Then
                Aviso("El Largo Del Mensaje No Puede Exceder Los 480 Caracteres ")
                e.Canceled = True
            Else
                Try
                    Dim SSCommandAgencias As New SqlCommand
                    SSCommandAgencias.CommandText = "SP_ADD_CAT_ETIQUETAS_WHATSAPP"
                    SSCommandAgencias.CommandType = CommandType.StoredProcedure
                    SSCommandAgencias.Parameters.Add("@V_Cat_Sm_Id", SqlDbType.Decimal).Value = 0
                    SSCommandAgencias.Parameters.Add("@V_Cat_Sm_Descripcion", SqlDbType.NVarChar).Value = valores(0)
                    SSCommandAgencias.Parameters.Add("@V_Cat_Sm_Valor", SqlDbType.NVarChar).Value = ""
                    SSCommandAgencias.Parameters.Add("@V_Cat_Sm_Tabla", SqlDbType.NVarChar).Value = ""
                    SSCommandAgencias.Parameters.Add("@V_Cat_Sm_Camporeal", SqlDbType.NVarChar).Value = ""
                    SSCommandAgencias.Parameters.Add("@V_Bandera", SqlDbType.NVarChar).Value = 6

                    Dim DtsVarios As DataTable = Consulta_Procedure(SSCommandAgencias, "Fuente")
                    If DtsVarios.Rows(0).Item("Cuantos") = 0 Then
                        Llenar(1, valores(1), "", valores(0), "", 7)
                        Aviso("Plantilla Creada")

                        RGVPlantillas.Rebind()
                    Else
                        e.Canceled = True
                        Aviso("La Plantilla Ya Existe Valide")
                    End If
                Catch ex As Exception
                    SendMail("BtnAccion_Click", ex, "", "", HidenUrs.Value)
                    e.Canceled = True
                    Aviso("No fue posible insertar la plantilla")
                End Try
            End If

        ElseIf e.CommandName = "Delete" Then
            Llenar(0, e.Item.Cells.Item(3).Text, "", "", "", 10)
            Llenar(0, e.Item.Cells.Item(3).Text, "", "", "", 9)
            RGVEtiquetas.Enabled = True
            Aviso("Se Ha Eliminado La Plantilla")
        ElseIf e.CommandName = "InitInsert" Then
            RGVEtiquetas.Enabled = False
        ElseIf e.CommandName = "Cancel" Then
            RGVEtiquetas.Enabled = True
        End If
    End Sub



End Class
