<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CatalogoDispercion.aspx.vb" MasterPageFile="MasterPage.master" Inherits="CatalogoDispercion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHMaster" runat="Server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="UPNLGeneral" runat="server" UpdatePanelCssClass="w3-center" CssClass="w3-center" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
        <br />
        <div class="w3-container Titulos w3-margin-bottom">
            <b>Dispersión</b>
        </div>

        <div class="w3-row-padding w3-text-black">
            <div class="w3-col s12 m3">
                <asp:Panel ID="PNLDispersiones" runat="server" Width="100%">
                    <telerik:RadGrid ID="RGDispersiones" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true" ClientSettings-EnableRowHoverStyle="true" Culture="es-MX" AllowFilteringByColumn="true" AllowMultiRowSelection="false" Style="margin: auto;">
                        <MasterTableView Width="100%" CommandItemDisplay="Top" EditFormSettings-PopUpSettings-KeepInScreenBounds="true" DataKeyNames="Id" Caption="Dispersiones" EditFormSettings-FormCaptionStyle-CssClass="w3-blue w3-text-white">
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
                                <telerik:GridBoundColumn UniqueName="INSTANCIA" HeaderText="Instancia" DataField="INSTANCIA" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                </telerik:GridBoundColumn>
                               <%-- <telerik:GridBoundColumn UniqueName="RESPONSABLE" HeaderText="Resp Gestión" DataField="RESPONSABLE" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="DISPERSION" HeaderText="Dispersión" DataField="DISPERSION">
                                    <FilterTemplate>
                                        <telerik:RadComboBox runat="server" SelectedValue='<%# TryCast(Container, GridItem).OwnerTableView.GetColumn("DISPERSION").CurrentFilterValue %>' OnClientSelectedIndexChanged="DispersionChanged" EmptyMessage="Filtro">
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
                                </telerik:GridBoundColumn>--%>
                                <telerik:GridBoundColumn UniqueName="Usuarios" HeaderText="Usuarios" DataField="Usuarios" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Interno" HeaderText="Interno/Externo" DataField="Interno" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                </telerik:GridBoundColumn>
                                <telerik:GridButtonColumn ButtonType="PushButton" ConfirmTextFormatString="¿Seguro que deseas eliminar la dispersión #{0}? Esta acción no se puede deshacer" ConfirmTextFields="ID" ConfirmTitle="Eliminar Dispersión" CommandName="Delete" Text="Eliminar" ShowInEditForm="false" ConfirmDialogType="RadWindow" ButtonCssClass="w3-red w3-text-white w3-btn w3-round-medium"></telerik:GridButtonColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </asp:Panel>
            </div>
            <div class="w3-col s12 m3">
                <asp:Panel ID="PnlInstancia" runat="server" Visible="false">
                    <label>Instancia</label>
                    <telerik:RadComboBox runat="server" ID="DDLInstancia" EmptyMessage="Selecciona una Instancia" AutoPostBack="true" Width="100%">
                    </telerik:RadComboBox>
                </asp:Panel>
            </div>
            <div class="w3-col s12 m3">
                <asp:Panel ID="PnlResp" runat="server" Visible="false">
                    <label>Interno/Externo</label>
                    <telerik:RadComboBox runat="server" ID="rDdlInterno" EmptyMessage="Selecciona Interno/Externo" AutoPostBack="true" Width="100%" Enabled="false">
                        <Items>
                            <telerik:RadComboBoxItem Text="Interno" Value="1" />
                            <telerik:RadComboBoxItem Text="Externo" Value="0" />
                        </Items>
                    </telerik:RadComboBox>
                </asp:Panel>
            </div>
            <div class="w3-col s12 m3">
                <%--<asp:Panel ID="PnlDisp" runat="server" Visible="false">
                    <label>Dispersión</label>
                    <telerik:RadComboBox runat="server" ID="DDLDispersion" EmptyMessage="Selecciona Dispersión" AutoPostBack="true" Width="100%" Enabled="false">
                        <Items>
                            <telerik:RadComboBoxItem Text="SI" Value="1" />
                            <telerik:RadComboBoxItem Text="NO" Value="0" />
                        </Items>
                    </telerik:RadComboBox>
                </asp:Panel>--%>
            </div>
            <div class="w3-col s12 m3">
                <telerik:RadButton ID="RBCancelar" Text="Cancelar" runat="server" Visible="false"></telerik:RadButton>
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
        <br />
        <asp:Panel runat="server" ID="PnlDatos" Visible="false" CssClass="w3-container">
            <div class="w3-panel Titulos">
                <b>Parametrización</b>
            </div>
            <div class="w3-row w3-text-black" style="overflow: auto; max-height: 400px; max-width: 100%">
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
            </div>
            <br />
            <div class="w3-panel Titulos">
                    <b>Selección de usuarios</b><br />                    
                </div>
            <div class="w3-col s12 m6">
                <label>CALL CENTER</label>
            </div>
            
            <div class="w3-col s12 m6">
                <label>VISITAS</label>
            </div>

            <div class="w3-col s12 m6">
                <telerik:RadComboBox runat="server" ID="RCBUsuarios" EmptyMessage="Selecciona al menos un usuario" AutoPostBack="true" Width="100%" CheckBoxes="true"></telerik:RadComboBox>
            </div>
            <div class="w3-col s12 m6">
                <telerik:RadComboBox runat="server" ID="RCBUsuarios2" EmptyMessage="Selecciona al menos un usuario" AutoPostBack="true" Width="100%" CheckBoxes="true"></telerik:RadComboBox>
            </div>
        </asp:Panel>
        <asp:Button runat="server" ID="btnSimularDispersion" Visible="false" Text="Simular asignación"></asp:Button>
        <br />
        <telerik:RadGrid runat="server" ID="gridAsignacion" Visible="false" AllowSorting="True" AutoGenerateColumns="true" AllowPaging="True" PageSize="10">
            <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true" OpenInNewWindow="true" UseItemStyles="false" FileName="Simulacion de Asignación">
            </ExportSettings>

            <MasterTableView Width="100%" CommandItemDisplay="Top">
                <CommandItemSettings ShowExportToCsvButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" ShowExportToExcelButton="true" />
                <Columns>
                    <telerik:GridEditCommandColumn ItemStyle-Width="5px" UniqueName="Expandible"></telerik:GridEditCommandColumn>
                </Columns>
                <EditFormSettings UserControlName="gridEditaDispMan.ascx" EditFormType="WebUserControl">
                    <EditColumn UniqueName="EditCommandColumn1">
                    </EditColumn>
                </EditFormSettings>
            </MasterTableView>
        </telerik:RadGrid>
        <br />
        <telerik:RadButton runat="server" ID="btnAplicarAsignacion" Visible="false" Text="Aplicar asignacion"></telerik:RadButton>
        <br />
        <telerik:RadWindowManager ID="RadAviso" runat="server"></telerik:RadWindowManager>
    </telerik:RadAjaxPanel>
    <asp:HiddenField ID="HidenUrs" runat="server" />
</asp:Content>
