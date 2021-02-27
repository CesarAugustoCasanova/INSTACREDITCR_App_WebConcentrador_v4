<%@ Control Language="VB" AutoEventWireup="false" CodeFile="CodigosPostales.ascx.vb" Inherits="M_Administrador_CodigosPostales" %>
<div>
    <telerik:RadAjaxPanel runat="server">
        <div class="w3-row-padding">
            <div>

                <telerik:RadCheckBox runat="server" ID="CBConservar" Text="Añadir" Visible='<%# IIf((TypeOf (Container) Is GridEditFormInsertItem), "false", "true")%>'></telerik:RadCheckBox>
                <telerik:RadCheckBox runat="server" ID="CBRango" Text="Ingresar Rango"></telerik:RadCheckBox>
            </div>
            <div class="w3-col s12 m3">
                <label>Nombre de la plaza:</label>
                <telerik:RadTextBox runat="server" ID="TxtNombre" EmptyMessage="Ingrese el nombre" Width="100%" CausesValidation="false"></telerik:RadTextBox>
            </div>
            <div class="w3-col s12 m3">
                <label>Region</label>
                <telerik:RadTextBox runat="server" ID="txtRegion" EmptyMessage="Region" Width="100%" CausesValidation="false" MinValue="0" NumberFormat-GroupSeparator="" NumberFormat-DecimalDigits="0" ShowSpinButtons="true"></telerik:RadTextBox>
            </div>
            <div class="w3-col s12 m3">
                <label>Jefe Regional</label>
                <telerik:RadTextBox runat="server" ID="txtRegional" EmptyMessage="Nombre del Jefe Regional" Width="100%" CausesValidation="false" MinValue="0" NumberFormat-GroupSeparator="" NumberFormat-DecimalDigits="0" ShowSpinButtons="true"></telerik:RadTextBox>
            </div>
            <div class="w3-col s12 m3">
                <label>Jefe de Plaza</label>
                <telerik:RadTextBox runat="server" ID="txtJefePlaza" EmptyMessage="Nombre del Jefe de Plaza" Width="100%" CausesValidation="false" MinValue="0" NumberFormat-GroupSeparator="" NumberFormat-DecimalDigits="0" ShowSpinButtons="true"></telerik:RadTextBox>
            </div>
            <div class="w3-col s12 m3">
                <label>Gestor</label>
                <telerik:RadTextBox runat="server" ID="txtGestor" EmptyMessage="Nombre del Gestor" Width="100%" CausesValidation="false" MinValue="0" NumberFormat-GroupSeparator="" NumberFormat-DecimalDigits="0" ShowSpinButtons="true"></telerik:RadTextBox>
            </div>
            <div class="w3-col s12 m3">
                <label>Nombre del Auxiliar</label>
                <telerik:RadTextBox runat="server" ID="txtAuxiliar" EmptyMessage="Nombre del Auxiliar" Width="100%" CausesValidation="false" MinValue="0" NumberFormat-GroupSeparator="" NumberFormat-DecimalDigits="0" ShowSpinButtons="true"></telerik:RadTextBox>
            </div>
            <div class="w3-col s12 m3">
                <label>Numero de plaza</label>
                <telerik:RadNumericTextBox runat="server" ID="txtNumPlaza" EmptyMessage="Numero de Plaza" Width="100%" CausesValidation="false" MinValue="0" NumberFormat-GroupSeparator="" NumberFormat-DecimalDigits="0" ShowSpinButtons="true"></telerik:RadNumericTextBox>
            </div>

        </div>
        <div class="w3-col s12 m3">
            <label>Usuario</label>
            <telerik:RadTextBox runat="server" ID="txtUsuario" EmptyMessage="Usuario" Width="100%" CausesValidation="false" MinValue="0" NumberFormat-GroupSeparator="" NumberFormat-DecimalDigits="0" ShowSpinButtons="true"></telerik:RadTextBox>
        </div>
        <div class="w3-col s12 m3">
            <label>Nombre Zona</label>
            <telerik:RadTextBox runat="server" ID="txtZona" EmptyMessage="Zona" Width="100%" CausesValidation="false" MinValue="0" NumberFormat-GroupSeparator="" NumberFormat-DecimalDigits="0" ShowSpinButtons="true"></telerik:RadTextBox>
        </div>
        <div runat="server" id="DivRango" visible="false">
            <div class="w3-col s12 m3">
                <label>Codigo Postal de Inicio</label>
                <telerik:RadMaskedTextBox runat="server" ID="TxtCPI" EmptyMessage="CP Inicio" Width="100%" CausesValidation="false" Mask="#####"></telerik:RadMaskedTextBox>
            </div>
            <div class="w3-col s12 m3">
                <label>Codigo Postal de Final</label>
                <telerik:RadMaskedTextBox runat="server" ID="TxtCPF" EmptyMessage="CP Final" Width="100%" CausesValidation="false" Mask="#####"></telerik:RadMaskedTextBox>
            </div>
        </div>
        <div runat="server" id="DivMultiple">
            <div class="w3-col s12 m3">
                <label>Ingrese el codigo postal</label>
                <telerik:RadSearchBox runat="server" ID="rsb1" EmptyMessage="Buscar..." OnSearch="rsb1_Search" Localization-ShowAllResults="Mostrar todos" MaxResultCount="25" Style="width: 100%" DropDownSettings-Width="100%" HighlightFirstMatch="true">
                    <WebServiceSettings Path="CatalogosDistritos.aspx" Method="GetResults" />
                    <DropDownSettings Width="350px" Height="450px" CssClass="w3-card-4">
                        <HeaderTemplate>
                            <table>
                                <tr>
                                    <td align="center">Codigo postal</td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%--<ul>
                                    <li>
                                        <%# DataBinder.Eval(Container.DataItem, "Credito") %></li>
                                    <li>
                                        <%# DataBinder.Eval(Container.DataItem, "Nombre") %></li>
                                </ul>--%>
                            <telerik:RadCheckBox runat="server" ID="CBP"></telerik:RadCheckBox>
                        </ItemTemplate>
                    </DropDownSettings>
                </telerik:RadSearchBox>
            </div>
            <div class="w3-col s12 m3">
                <label>Codigos Postales Seleccionados</label>
                <telerik:RadGrid runat="server" ID="RGCodigos">
                    <MasterTableView AllowPaging="true" PageSize="10">
                        <Columns>
                            <telerik:GridButtonColumn ItemStyle-Width="5px" HeaderText="Eliminar" CommandName="Delete"></telerik:GridButtonColumn>

                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
        </div>
    </telerik:RadAjaxPanel>
</div>
<br />
<div class="container mt-2">
    <telerik:RadButton ID="btnInsert" Text="Guardar" runat="server" CommandName='<%# IIf((TypeOf (Container) Is GridEditFormInsertItem), "PerformInsert", "Update")%>' AutoPostBack="true" CssClass="bg-primary text-white border-0" Width="150px"></telerik:RadButton>
    <telerik:RadButton ID="RBtnCacelar" runat="server" Text="Cancelar" CommandName="Cancel" CausesValidation="false" CssClass="bg-info text-white border-0" Width="150px"></telerik:RadButton>
</div>
<div class="container mt-2">
    <telerik:RadLabel ID="RadButton1" Text="Guardar" runat="server" Height="0px" Width="0px"></telerik:RadLabel>

</div>

