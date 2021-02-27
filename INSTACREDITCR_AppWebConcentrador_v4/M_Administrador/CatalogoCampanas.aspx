<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CatalogoCampanas.aspx.vb" MasterPageFile="MasterPage.master" Inherits="CatalogoCampanas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHMaster" runat="Server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadFormDecorator runat="server" DecoratedControls="Fieldset" DecorationZoneID="General" />
    <telerik:RadAjaxPanel ID="UPNLGeneral" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <h2 class="text-center">Campañas</h2>
        <hr />
        <div id="General" class="container">
            <asp:Panel ID="PNLDispersiones" runat="server" Width="100%">
                <telerik:RadGrid ID="RGDispersiones" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true" ClientSettings-EnableRowHoverStyle="true" Culture="es-MX" AllowFilteringByColumn="true" AllowMultiRowSelection="false" Style="margin: auto;">
                    <MasterTableView Width="100%" CommandItemDisplay="Top" EditFormSettings-PopUpSettings-KeepInScreenBounds="true" DataKeyNames="Id" Caption="">
                        <CommandItemSettings ShowAddNewRecordButton="true" />
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" AllowFiltering="false">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox0" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox0_CheckedChanged" />
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="headerChkbox" runat="server" AutoPostBack="True" />
                                </HeaderTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="ID" HeaderText="ID" DataField="ID" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PLANTILLA" HeaderText="Plantilla o Campaña" DataField="PLANTILLA" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="NOMBRE" HeaderText="Nombre" DataField="NOMBRE" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="ACTIVO" HeaderText="Activo" DataField="ACTIVO">
                                <FilterTemplate>
                                    <telerik:RadComboBox runat="server" SelectedValue='<%# TryCast(Container, GridItem).OwnerTableView.GetColumn("ACTIVO").CurrentFilterValue %>' OnClientSelectedIndexChanged="DispersionChanged" EmptyMessage="Filtro">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="SI" Value="SI" />
                                            <telerik:RadComboBoxItem Text="NO" Value="NO" />
                                        </Items>
                                    </telerik:RadComboBox>
                                    <telerik:RadScriptBlock ID="RadScriptBlock2" runat="server">
                                        <script type="text/javascript">
                                            function DispersionChanged(sender, args) {
                                                var tableView = $find("<%# TryCast(Container, GridItem).OwnerTableView.ClientID %>");
                                                tableView.filter("DISPERSION", args.get_item().get_value(), "EqualTo");
                                            }
                                        </script>
                                    </telerik:RadScriptBlock>
                                </FilterTemplate>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="Usuarios" HeaderText="Usuarios" DataField="Usuarios" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ItemStyle-Wrap="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridButtonColumn ButtonType="PushButton" ConfirmTextFormatString="¿Seguro que deseas eliminar la dispersión #{0}? Esta acción no se puede deshacer" ConfirmTextFields="ID" ConfirmTitle="Eliminar Dispersión" CommandName="Delete" Text="Eliminar" ShowInEditForm="false" ConfirmDialogType="RadWindow" ButtonCssClass="w3-red w3-text-white w3-btn w3-round-medium"></telerik:GridButtonColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlOpciones" Visible="false">
                <div class="row">
                    <div class="w3-col s12 m3">
                        <asp:Panel ID="PnlInstancia" runat="server" Visible="false">
                            <label>Instancia</label>
                            <telerik:RadComboBox runat="server" ID="DDLInstancia" EmptyMessage="Selecciona una Instancia" AutoPostBack="true" Width="100%">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Extrajudicial" Value="2" Selected="true" />
                                </Items>
                            </telerik:RadComboBox>
                        </asp:Panel>
                    </div>
                    <div class="w3-col s12 m3">
                        <asp:Panel ID="PnlResp" runat="server" Visible="false">
                            <label>Responsable de gestión</label>
                            <telerik:RadComboBox runat="server" ID="DDLRespGestion" EmptyMessage="Selecciona responsable de gestion" AutoPostBack="true" Width="100%" Enabled="false">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Jose Rojo" Value="4" Selected="true" />
                                </Items>
                            </telerik:RadComboBox>

                        </asp:Panel>
                    </div>
                    <div class="w3-col s12 m3">
                        <asp:Panel ID="PnlDisp" runat="server" Visible="false">
                            <label>Dispersión</label>
                            <telerik:RadComboBox runat="server" ID="DDLDispersion" EmptyMessage="Selecciona Dispersión" AutoPostBack="true" Width="100%" Enabled="false">
                                <Items>
                                    <telerik:RadComboBoxItem Text="SI" Value="1" Selected="true" />
                                    <telerik:RadComboBoxItem Text="NO" Value="0" />
                                </Items>
                            </telerik:RadComboBox>
                        </asp:Panel>
                    </div>
                    <div class="w3-col s12 m3">
                    </div>

                    <div class="w3-col s12 m3" style="visibility: hidden">
                        <label>Sub Clasificación</label>
                        <telerik:RadComboBox runat="server" ID="ddlsubclasificacion" EmptyMessage="Selecciona Sub Clasificación" AutoPostBack="true" Width="100%" Enabled="false">
                            <Items>
                                <telerik:RadComboBoxItem Selected="true" Text="Normal" Value="1" />
                                <telerik:RadComboBoxItem Text="Reestructurada" Value="2" />
                                <telerik:RadComboBoxItem Text="Renovada" Value="3" />
                            </Items>
                        </telerik:RadComboBox>
                    </div>

                    <div id="modalDelete" class="w3-modal" style="z-index: 999999">
                        <div class="w3-modal-content">
                            <div class="w3-container">
                                <span onclick="document.getElementById('id01').style.display='none'"
                                    class="w3-button w3-display-topright">&times;</span>
                                <p>Esta configuración de dispersión borrará toda la configuracion anterior.</p>
                                <p>¿Deseas continuar?</p>
                                <telerik:RadButton runat="server" ID="btnAcpetarBorrar" Text="Continuar y borrar" CssClass="w3-hover-green w3-hover-text-white"></telerik:RadButton>
                                <telerik:RadButton runat="server" ID="btnCancelarBorrar" Text="Cancelar" CssClass="w3-hover-red w3-hover-text-white"></telerik:RadButton>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel runat="server" ID="PnlDatos" Visible="false" CssClass="w3-container">
                <telerik:RadButton ID="RBCancelar" Text="Cancelar" runat="server" Visible="false" CssClass="bg-danger text-white border-0"></telerik:RadButton>

                <fieldset>
                    <legend>Parametrización</legend>
                    <telerik:RadAjaxPanel runat="server">
                        <telerik:RadGrid runat="server" ID="gridDispersion">
                            <MasterTableView AutoGenerateColumns="false" CommandItemDisplay="top">
                                <CommandItemSettings AddNewRecordText="Agregar Parametro" CancelChangesText="Cancelar" SaveChangesText="Guardar" RefreshText="Actualizar" />
                                <Columns>
                                    <telerik:GridTemplateColumn HeaderText="Eliminar">
                                        <ItemTemplate>
                                            <telerik:RadButton runat="server" Text="Eliminar" CommandName="onDelete"></telerik:RadButton>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn UniqueName="No" HeaderText="#" DataField="ORDEN">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="Tabla" HeaderText="Tabla" DataField="DESCRIPCIONTABLA">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="Campo" HeaderText="Campo" DataField="DESCRIPCIONCAMPO">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="Condicion" HeaderText="Condicion" DataField="DESCRIPCIONOPERADOR">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="Valor" HeaderText="Valor" DataField="Valor">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="Conector" HeaderText="Conector" DataField="DESCRIPCIONCONECTOR">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridEditCommandColumn ButtonType="FontIconButton" HeaderText="Editar"></telerik:GridEditCommandColumn>
                                </Columns>
                                <EditFormSettings UserControlName="grids/configReglas/Reglas.ascx" EditFormType="WebUserControl">
                                    <EditColumn UniqueName="EditCommandColumn1">
                                    </EditColumn>
                                </EditFormSettings>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </telerik:RadAjaxPanel>
                </fieldset>
                <fieldset>
                    <legend>Tipo de Campaña</legend>
                    <div class="container">
                        <div class="row justify-content-center">
                            <div class="col-md-6">
                                <label>Tipo Campañas</label>
                                <telerik:RadComboBox runat="server" ID="RCBTipoCampana" EmptyMessage="Seleccione..." AutoPostBack="true" Width="100%">
                                    <Items>
                                        <telerik:RadComboBoxItem Value="0" Text="Envio de SMS" />
                                        <telerik:RadComboBoxItem Value="1" Text="Envio de correo" />
                                        <telerik:RadComboBoxItem Value="2" Text="Envio de Whatsapp" />
                                    </Items>
                                </telerik:RadComboBox>
                            </div>
                            <div class="col-md-6">
                                <label>Campañas</label>
                                <telerik:RadComboBox runat="server" ID="RCBCampana" EmptyMessage="Selecciona Campaña" AutoPostBack="true" Width="100%" CheckBoxes="false" Enabled="false">
                                </telerik:RadComboBox>
                            </div>
                            <asp:Panel runat="server" ID="PNLPerfilMail" Visible="false" Width="100%">
                                <div class="col-md-6">
                                    <label>Perfil de envío</label>
                                    <telerik:RadComboBox runat="server" ID="RCBPerfilMail" EmptyMessage="Selecciona Perfil" AutoPostBack="true" Width="100%" CheckBoxes="false" Enabled="false">
                                    </telerik:RadComboBox>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </fieldset>
                 <fieldset>
                   <legend>Programaci&oacute;n</legend>
                    <div class="container">
                        <div class="row justify-content-center">
                            <div class="col-md-6">
                                <label>Tipo de envio</label>
                                <telerik:RadComboBox runat="server" ID="RCBTipoEnvio" EmptyMessage="Seleccione..." AutoPostBack="true" Width="30%">
                                    <Items>
                                        <telerik:RadComboBoxItem Value="4" Text="Diario" />
                                        <telerik:RadComboBoxItem Value="8" Text="Semanal" />
                                        <telerik:RadComboBoxItem Value="16" Text="Mensual" />
                                    </Items>
                                </telerik:RadComboBox>
                              <%--  <telerik:RadComboBox runat="server" ID="CBPeriodo" EmptyMessage="Seleccione..." AutoPostBack="true" Width="100%">
                                    <Items>
                                        <telerik:RadComboBoxItem Value="D" Text="Dias" />
                                        <telerik:RadComboBoxItem Value="M" Text="Meses" />
                                    </Items>
                                </telerik:RadComboBox>--%>
                            </div>
                            <div class="col-md-6">
                               <%-- <telerik:RadNumericTextBox runat="server" ID="TxtDuracion" MinValue="1" NumberFormat-DecimalDigits="0" Width="100%" AutoPostBack="true" ></telerik:RadNumericTextBox>--%>
                                
                                <label>Fecha inicial</label>
                                <telerik:RadDateTimePicker runat="server" ID="TPFechaI" DateInput-DateFormat="dd/MMM/yyyy" Width="30%" EnableTyping="false" DateInput-Visible="true" TimePopupButton-Visible="false" AutoPostBackControl="Calendar" ></telerik:RadDateTimePicker>
                                <label>Fecha final</label>
                                <telerik:RadDateTimePicker runat="server" ID="TPFechaF" enabled="false" DateInput-DateFormat="dd/MMM/yyyy" Width="30%" EnableTyping="false" DateInput-Visible="true" TimePopupButton-Visible="false" AutoPostBackControl="Calendar" ></telerik:RadDateTimePicker>
                               <br /><br />
                                <label>Hora</label>
                                 <telerik:RadNumericTextBox runat="server" ID="TPHora" InputType="Number" AutoPostBack="true" MaxValue="24" MaxLength="2" ></telerik:RadNumericTextBox>
                                <label>24/h</label>
                            </div>
                        </div>
                    </div>
                </fieldset>
            </asp:Panel>
            <div class=" d-flex justify-content-center my-3 ">
                <asp:Button runat="server" ID="btnSimularDispersion" Visible="false" Text="Simular" CssClass="btn btn-primary"></asp:Button>
                <asp:Button runat="server" ID="btnGuardar" Visible="false" Text="Guardar" CssClass="btn btn-green"></asp:Button>

            </div>
            <asp:Panel runat="server" ID="pnlResultadoAsignacion" Visible="false">
                <fieldset>
                    <legend>Resultado simulacion</legend>
                    <telerik:RadGrid runat="server" ID="gridAsignacion" AllowSorting="True" AutoGenerateColumns="true" AllowPaging="True" PageSize="10">
                        <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true" OpenInNewWindow="true" UseItemStyles="false" FileName="Simulacion de Asignación">
                        </ExportSettings>

                        <MasterTableView Width="100%" CommandItemDisplay="Top">
                            <CommandItemSettings ShowExportToCsvButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" ShowExportToExcelButton="true" />
                            <Columns>
                              <%--  <telerik:GridEditCommandColumn ItemStyle-Width="5px" UniqueName="Expandible"></telerik:GridEditCommandColumn>--%>
                            </Columns>
                            <EditFormSettings UserControlName="gridEditaDispMan.ascx" EditFormType="WebUserControl">
                                <EditColumn UniqueName="EditCommandColumn1">
                                </EditColumn>
                            </EditFormSettings>
                        </MasterTableView>
                    </telerik:RadGrid>
                    <telerik:RadButton runat="server" ID="btnAplicarAsignacion" Visible="false" Text="Aplicar asignacion" CssClass="btn btn-success my-2"></telerik:RadButton>
                </fieldset>
            </asp:Panel>
            <telerik:RadWindowManager ID="RadAviso" runat="server"></telerik:RadWindowManager>
        </div>
    </telerik:RadAjaxPanel>
    <asp:HiddenField ID="HidenUrs" runat="server" />
</asp:Content>
