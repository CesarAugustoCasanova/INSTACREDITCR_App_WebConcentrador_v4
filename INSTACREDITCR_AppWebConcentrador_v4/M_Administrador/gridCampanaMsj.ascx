<%@ Control Language="VB" AutoEventWireup="false" CodeFile="gridCampanaMsj.ascx.vb" Inherits="M_Administrador_gridCampanaMsj" %>

<table style="width: 100%">
    <tr class="w3-row-padding">
        <td>Nombre de la campaña
        </td>
        <td colspan="3">
            <telerik:RadTextBox runat="server" MaxLength="20" ID="txtNombre" Enabled='<%# IIf((TypeOf (Container) Is GridEditFormInsertItem), True, False)%>' Height="22px">
            </telerik:RadTextBox>

            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNombre" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr class="w3-row-padding">
        <td>Proveedores</td>
        <td colspan="3">
            <telerik:RadComboBox runat="server" ID="comboProveedor" CheckBoxes="false" EmptyMessage="Seleccione" Width="100%" AutoPostBack="true"></telerik:RadComboBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="comboProveedor" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>

        </td>
    </tr>
    <tr>
        <td colspan="3">
            <table id="TableCalixta" runat="server" visible="false">
                <tr>
                    <td>Tipo De Campaña</td>
                    <td>
                        <label>Reglas globales</label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadComboBox runat="server" ID="comboTipoCampana" EmptyMessage="Seleccione" Width="100%" AutoPostBack="true"></telerik:RadComboBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="comboTipoCampana" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <telerik:RadComboBox runat="server" ID="comboReglasGlobales" CheckBoxes="false" EmptyMessage="Seleccione" Width="100%"></telerik:RadComboBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="comboReglasGlobales" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>Tipo de Programacion</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadComboBox runat="server" ID="comboProgramacion" EmptyMessage="Seleccione" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="comboProgramacion_SelectedIndexChanged">
                            <Items>
                                <telerik:RadComboBoxItem Text="Manual" Value="M" />
                                <telerik:RadComboBoxItem Text="Recurrente" Value="A" />
                            </Items>
                        </telerik:RadComboBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="comboProgramacion" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <telerik:RadAjaxPanel ID="DIVFECHA" runat="server">
                            <label>
                                Fecha de programación</label>
                            <telerik:RadDateTimePicker ID="dteProgramacion" runat="server" DateInput-EmptyMessage="No aplica" DateInput-Enabled="false" Enabled="false" Width="100%">
                            </telerik:RadDateTimePicker>
                        </telerik:RadAjaxPanel>

                    </td>
                    <td>
                        <telerik:RadAjaxPanel ID="DIVVIGENCIA" runat="server">
                            <label>
                                Vigencia</label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtVigencia" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <telerik:RadDatePicker ID="txtVigencia" runat="server" Calendar-EnableMultiSelect="false" DateInput-Enabled="false" Width="100%">
                            </telerik:RadDatePicker>
                        </telerik:RadAjaxPanel>
                    </td>
                </tr>
                <tr>
                    <td>Tipo de mensaje</td>
                    <td>
                        <label>Mensaje</label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadComboBox ID="RDDLTipoMsj" runat="server" AutoPostBack="true" EmptyMessage="Seleccione">
                            <Items>
                                <telerik:RadComboBoxItem Text="Manual" Value="M" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        <telerik:RadTextBox runat="server" ID="txtMensaje" InputType="Text" TextMode="MultiLine" Width="100%" Enabled="false"></telerik:RadTextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtMensaje" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <div class="w3-col s12 m4" id="DIVRol" runat="server">
                            <label>Rol</label>
                            <telerik:RadComboBox ID="RCBRol" runat="server" EmptyMessage="Seleccione">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Titular" Value="T" />
                                    <telerik:RadComboBoxItem Text="Aval" Value="A" />
                                    <telerik:RadComboBoxItem Text="Codeudor" Value="C" />
                                    <telerik:RadComboBoxItem Text="Garante" Value="G" />
                                </Items>
                            </telerik:RadComboBox>
                        </div>
                    </td>
                    <td>
                        <div class="w3-col s12 m4" id="DIVTipoTEL" runat="server" visible="false">
                            <label>Tipo</label>
                            <telerik:RadComboBox ID="RDDLTipoTel" runat="server" EmptyMessage="Seleccione">
                            </telerik:RadComboBox>
                        </div>
                    </td>
                    <td>
                        <div class="w3-col s12 m4" id="DIV1" runat="server" visible="false">
                            <label>Prefijo</label>
                            <telerik:RadComboBox ID="RCBPrefijo" runat="server" EmptyMessage="Seleccione">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Sin prefijo" Value="0" />
                                    <telerik:RadComboBoxItem Text="Prefijo 1" Value="1" />
                                    <telerik:RadComboBoxItem Text="Mixto" Value="2" />
                                </Items>
                            </telerik:RadComboBox>
                        </div>
                    </td>
                </tr>
            </table>
            <table id="TableInconcert" runat="server" visible="false">
                <tr>
                    <td>Tipo De Campaña</td>
                    <td>
                        <telerik:RadComboBox runat="server" ID="comboTipoCampanaI" EmptyMessage="Seleccione" Width="100%" AutoPostBack="true"></telerik:RadComboBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="comboTipoCampanaI" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <label>Reglas globales</label>
                    </td>
                    <td>
                        <telerik:RadComboBox runat="server" ID="comboReglasGlobalesI" CheckBoxes="false" EmptyMessage="Seleccione" Width="100%"></telerik:RadComboBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="comboReglasGlobalesI" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <%--<div class="w3-col s12 m4" id="DIVRolI" runat="server">
                            <label>Rol</label>
                            <telerik:RadComboBox ID="RCBRolI" runat="server" EmptyMessage="Seleccione">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Titular" Value="T" />
                                    <telerik:RadComboBoxItem Text="Aval" Value="A" />
                                    <telerik:RadComboBoxItem Text="Codeudor" Value="C" />
                                    <telerik:RadComboBoxItem Text="Garante" Value="G" />
                                </Items>
                            </telerik:RadComboBox>
                        </div>--%>
                    </td>
                    <td>
                        <%--  <div class="w3-col s12 m4" id="DIVTipoTELI" runat="server" visible="false">
                            <label>Tipo</label>
                            <telerik:RadComboBox ID="RDDLTipoTelI" runat="server" EmptyMessage="Seleccione">
                            </telerik:RadComboBox>
                        </div>--%>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
            </table>
            <table id="TableLSC" runat="server" visible="false">
                <tr>
                    <td>
                        <label>Reglas globales</label>
                    </td>
                    <td>
                        <telerik:RadComboBox runat="server" ID="comboReglasGlobalesCarteo" CheckBoxes="false" EmptyMessage="Seleccione" Width="100%"></telerik:RadComboBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="comboReglasGlobalesCarteo" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </td>
                    <td>Simular</td>
                    <td>
                        <telerik:RadDatePicker ID="TxtSimulacion" runat="server" Calendar-EnableMultiSelect="false" DateInput-Enabled="false"  Width="100%">
                        </telerik:RadDatePicker>
                    </td>
                </tr>
                <tr>
                    <td>Mensaje</td>
                    <td>
                        <telerik:RadTextBox runat="server" ID="TxtMensajeC" InputType="Text" TextMode="MultiLine" Width="420px" Height="84px"></telerik:RadTextBox>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
</table>

<telerik:RadButton runat="server" ID="btnGuardar" Text="Guardar" SingleClick="true" SingleClickText="Guardando..." CommandName='<%# IIf((TypeOf (Container) Is GridEditFormInsertItem), RadGrid.PerformInsertCommandName, RadGrid.UpdateCommandName)%>'></telerik:RadButton>
<telerik:RadButton runat="server" ID="btnCancelar" Text="Cancelar" CommandName="Cancel" CausesValidation="false"></telerik:RadButton>
