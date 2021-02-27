<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Modulos_Reglas_Grid.ascx.vb" Inherits="M_Administrador_Modulos_Reglas_Grid" %>
<%@ Register Namespace="CustomEditors" TagPrefix="custom" %>
<telerik:RadAjaxPanel runat="server" CssClass="container-fluid mb-5 pb-3">
    <div class="text-center">
        <h1>Configuraci&oacute;n de regla</h1>
        <p class="text-muted">Seleccione los campos que desea usar para posteriormente configurar el filtro y obtener la informaci&oacute;n necesaria</p>
    </div>
    <telerik:RadWizard runat="server" ID="wizard1" RenderedSteps="Active" ClickActiveStep="false" DisplayProgressBar="false" CssClass="bg-light border rounded m-2">
        <Localization Cancel="Cancelar" Finish="Guardar" Next="Siguiente" Previous="Anterior" />
        <WizardSteps>
            <telerik:RadWizardStep DisplayCancelButton="true" Title="Nombre de la regla" StepType="Start" >
                <p>Escoge un nombre para tu regla. Te recomendamos usar un nombre que sea f&aacute;cil de identificar.</p>
                <telerik:RadTextBox runat="server" ID="txtNombreRegla" Label="Nombre de la regla" Width="100%" MaxLength="50"></telerik:RadTextBox>
                <asp:Label runat="server" ID="lblReglaID" Visible="false" Text="-1"></asp:Label>
            </telerik:RadWizardStep>
            <telerik:RadWizardStep DisplayCancelButton="true" Title="Selección de datos" Enabled="false" StepType="Step">
                <p>Escoge una o varias tablas para poder ver sus campos disponibles. Posteiormente elige uno o varios campos. <u>Estos campos se usar&aacute;n para generar tus archivos, asignaciones, campañas, etc...</u></p>
                <div class="container-fluid">
                    <telerik:RadAjaxPanel runat="server" CssClass="row my-5">
                        <div class="col-md-6 mb-2 text-center">
                            <telerik:RadComboBox runat="server" ID="rcbTablas" Label="Tablas" Width="100%" EmptyMessage="Selecciona" EnableCheckAllItemsCheckBox="true" CheckBoxes="true" AllowCustomText="false" AutoPostBack="true" DropDownAutoWidth="Enabled" MarkFirstMatch="true" ZIndex="999999999">
                                <Localization AllItemsCheckedString="Todas" CheckAllString="Todo" />
                            </telerik:RadComboBox>
                        </div>
                        <div class="col-md-6 mb-2 text-center">
                            <telerik:RadComboBox runat="server" ID="rcbCampos" Label="Campos" Width="90%" EmptyMessage="Selecciona" EnableCheckAllItemsCheckBox="true" CheckBoxes="true" AllowCustomText="false" AutoPostBack="false" DropDownAutoWidth="Enabled" MarkFirstMatch="true" ZIndex="999999999" Enabled="false">
                                <Localization AllItemsCheckedString="Todas" CheckAllString="Todo" />
                            </telerik:RadComboBox>
                        </div>
                    </telerik:RadAjaxPanel>

                    <div class="text-right">
                        <div class="custom-control custom-switch">
                            <telerik:RadCheckBox runat="server" ID="switch1" Text="Mostrar toda la informacion" AutoPostBack="false">
                            </telerik:RadCheckBox>
                            <i class="material-icons" id="helpIcon" style="font-size: 1em">&#xe8fd;</i>
                            <telerik:RadToolTip runat="server" Position="BottomCenter" IsClientID="true" TargetControlID="helpIcon" ShowEvent="OnMouseOver" AutoCloseDelay="6000" HideEvent="LeaveTargetAndToolTip" Width="150px" RelativeTo="Element">
                                En algunos casos, un crédito no tendrá información
                                en un catálogo por lo que este crédito no será visible
                                a menos de que esta opción esté activada.
                            </telerik:RadToolTip>
                        </div>
                    </div>
                </div>
            </telerik:RadWizardStep>
            <telerik:RadWizardStep DisplayCancelButton="true" Title="Configuración de filtros" Enabled="false" StepType="Finish" AllowReturn="false">
                <telerik:RadFilter runat="server" ID="filtergg" AddExpressionToolTip="Añadir condicion" AddGroupToolTip="Añadir grupo de expresiones" AllowFilterOnBlur="false"  ApplyButtonText="Aplicar" BetweenDelimeterText="," FilterContainerID="gridInfoPreview" ExpressionPreviewPosition="Bottom" Width="100%" OperationMode="ServerAndClient" DefaultGroupOperation="And">
                    <FieldEditors>
                        <telerik:RadFilterBooleanFieldEditor DefaultFilterFunction="EqualTo" ToolTip="Verdadero o falso" />
                        <telerik:RadFilterDateFieldEditor DateFormat="dd/mm/yyyy" DefaultFilterFunction="DoesNotContain" />
                        <telerik:RadFilterNumericFieldEditor DefaultFilterFunction="DoesNotContain" />
                        <telerik:RadFilterTextFieldEditor DefaultFilterFunction="DoesNotContain" />                        
                    </FieldEditors>
                    <ClientSettings>
                        <ClientEvents OnFilterCreated="FilterCreated" />
                    </ClientSettings>
                    <Localization PreviewProviderBetweenText="Entre" PreviewProviderContainsText="Contiene" PreviewProviderDoesNotContainText="No Contiene" PreviewProviderEndsWithText="Termina con" PreviewProviderEqualToText="Igual a" PreviewProviderGreaterThanOrEqualToText="Mayor o igual que" PreviewProviderGreaterThanText="Mayor que" PreviewProviderIsEmptyText="Vacio" PreviewProviderIsNullText="Nulo" PreviewProviderLessThanOrEqualToText="Menor o igual que" PreviewProviderLessThanText="Menor que" PreviewProviderNotBetweenText="No entre" PreviewProviderNotEqualToText="Diferente a" PreviewProviderNotIsEmptyText="No vacio" PreviewProviderNotIsNullText="No nulo" PreviewProviderStartsWithText="Empieza con" GroupOperationAnd="Y" GroupOperationNotAnd="Y no" GroupOperationNotOr="O no" GroupOperationOr="O" FilterFunctionContains="Contiene" FilterFunctionDoesNotContain="No Contiene" FilterFunctionEndsWith="Termina Con" FilterFunctionEqualTo="Igual a" FilterFunctionGreaterThan="Mayor que" FilterFunctionGreaterThanOrEqualTo="Mayor o Igual a" FilterFunctionLessThan="Menor que" FilterFunctionLessThanOrEqualTo="Menor o Igual a" FilterFunctionNotEqualTo="Diferente a" FilterFunctionStartsWith="Empieza Con" />
                </telerik:RadFilter>
                <br />
                <div class="text-center">
                    <b>¿Tu informaci&oacute;n se muestra incompleta?</b> Prueba con "Actualizar Datos".
                </div>
                <div style="max-width: 100%; overflow-x: auto">

                    <telerik:RadGrid runat="server" ID="gridInfoPreview" Width="100%" AllowPaging="true" PageSize="10">
                        <MasterTableView CommandItemDisplay="Top" >
                            <CommandItemSettings ShowAddNewRecordButton="false" RefreshText="Actualizar Datos"/>
                            <NoRecordsTemplate>
                                <div class="text-center my-2">Los filtros seleccionados no proporcionan informacion.</div>
                            </NoRecordsTemplate>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                <p class="font-weight-bold">
                    Solamente se muestran los primeros 1,000 registros que coinciden con tus filtros. 
                </p>
            </telerik:RadWizardStep>
        </WizardSteps>
    </telerik:RadWizard>
    <script type="text/javascript">
        var filter;

        function FilterCreated(sender, args) {
            filter = sender;
            var menu = filter.get_contextMenu();
            menu.add_showing(FilterMenuShowing);
        }

        function FilterMenuShowing(sender, args) {
            var currentExpandedItem = sender.get_attributes()._data.ItemHierarchyIndex;
            var fieldName = filter._expressionItems[currentExpandedItem];
            var allFields = filter._dataFields;
            for (var i = 0, j = allFields.length; i < j; i++) {
                if (allFields[i].FieldName == fieldName) {
                    sender.findItemByValue("IsNull").set_visible(false);
                    sender.findItemByValue("NotIsNull").set_visible(false);
                    sender.findItemByValue("IsEmpty").set_visible(false);
                    sender.findItemByValue("NotIsEmpty").set_visible(false);
                    sender.findItemByValue("Between").set_visible(false);
                    sender.findItemByValue("NotBetween").set_visible(false);
                }
            }

        }
    </script>
</telerik:RadAjaxPanel>
