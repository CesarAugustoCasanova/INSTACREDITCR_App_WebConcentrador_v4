<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CatalogoInstancia.aspx.vb" MasterPageFile="MasterPage.master" Inherits="CatalogoInstancia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHMaster" runat="Server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="UPNLGeneral" runat="server" UpdatePanelCssClass="w3-center" CssClass="w3-center" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
        <br />
        <div class="w3-container Titulos">
            <b>Asignación de instancias</b>
        </div>
        <asp:Panel runat="server" ID="PnlInicio">
            <div id="modalDelete" class="w3-modal" style="z-index: 999999">
                <div class="w3-modal-content">
                    <div class="w3-container">
                        <span onclick="document.getElementById('id01').style.display='none'"
                            class="w3-button w3-display-topright">&times;</span>
                        <p>Esta configuración de asignación borrará toda la configuracion anterior.</p>
                        <p>¿Deseas continuar?</p>
                        <telerik:RadButton runat="server" ID="btnAcpetarBorrar" Text="Continuar y borrar" CssClass="w3-hover-green w3-hover-text-white"></telerik:RadButton>
                        <telerik:RadButton runat="server" ID="btnCancelarBorrar" Text="Cancelar" CssClass="w3-hover-red w3-hover-text-white"></telerik:RadButton>
                    </div>
                </div>
            </div>

            <div class="w3-row-padding w3-text-black">
                <div class="w3-col s12 m3">
                    <asp:Panel runat="server" ID="PnlConfigirar">
                        <label>Seleccione una instancia</label>
                        <telerik:RadComboBox runat="server" ID="RDDLInstancia" EmptyMessage="Selecciona una Instancia" AutoPostBack="true" Width="100%">
                            <Items>
                                <%--<telerik:RadComboBoxItem Text="Seleccione" Value="Seleccione" />--%>
                                <%--<telerik:RadComboBoxItem Text="Preventiva" Value="0" />
                                <telerik:RadComboBoxItem Text="Administrativa" Value="1" />--%>
                                <%--<telerik:RadComboBoxItem Text="Extrajudicial" Value="2" />--%>
                                <%--<telerik:RadComboBoxItem Text="Judicial" Value="3" />--%>
                                <%--<telerik:RadComboBoxItem Text="Simular" Value="Simular" />--%>
                            </Items>
                        </telerik:RadComboBox>
                    </asp:Panel>
                </div>

                <asp:Panel ID="PnlExisten" runat="server" Visible="false" CssClass="w3-col s12 m9 w3-margin-top">
                    <div class="w3-row-padding">
                        <div class="w3-col s12 m6">
                            <telerik:RadButton runat="server" ID="btnNuevaRegla" Text="Crear regla"></telerik:RadButton>
                        </div>
                        <div class="w3-col s12 m6">
                            <telerik:RadGrid runat="server" ID="gridReglas" AutoGenerateColumns="false" Width="300px">
                                <MasterTableView>
                                    <Columns>
                                        <telerik:GridButtonColumn ButtonType="PushButton" CommandName="onSelect" Text="Modificar" ItemStyle-Width="100px" CommandArgument="NOMBRE"></telerik:GridButtonColumn>
                                        <telerik:GridBoundColumn HeaderText="Regla" DataField="Regla"></telerik:GridBoundColumn>
                                        <telerik:GridButtonColumn ButtonType="PushButton" CommandName="onBorrar" Text="Eliminar" ItemStyle-Width="100px" CommandArgument="Eliminar"></telerik:GridButtonColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>
                    </div>
                </asp:Panel>

                <asp:Panel runat="server" ID="PnlSimular" Visible="false" CssClass="w3-col s12 m9 w3-margin-top">
                    <div class="w3-row-padding w3-text-black">
                        <div class="w3-col s12 m6">
                            <telerik:RadComboBox runat="server" ID="RDDLInstancia2" EmptyMessage="Selecciona una Instancia" AutoPostBack="true" Width="100%">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Seleccione" Value="Seleccione" />
                                    <telerik:RadComboBoxItem Text="Preventiva" Value="0" />
                                    <telerik:RadComboBoxItem Text="Administrativa" Value="1" />
                                    <telerik:RadComboBoxItem Text="Extrajudicial" Value="2" />
                                    <telerik:RadComboBoxItem Text="Judicial" Value="3" />
                                    <telerik:RadComboBoxItem Text="Todas" Value="Todas" />
                                </Items>
                            </telerik:RadComboBox>
                        </div>
                        <div class="w3-col s12 m6">
                            <telerik:RadButton runat="server" ID="BtnPreviewInstancia" Text="Preview"></telerik:RadButton>
                        </div>
                    </div>
                    <telerik:RadGrid runat="server" ID="RadGvPreview" Visible="false" AllowSorting="True" AutoGenerateColumns="true" AllowPaging="True" PageSize="10">
                        <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true" OpenInNewWindow="true" UseItemStyles="false" FileName="Simulacion de Asignación">
                        </ExportSettings>
                        <MasterTableView Width="100%" CommandItemDisplay="Top">
                            <CommandItemSettings ShowExportToCsvButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" ShowExportToExcelButton="true" />
                        </MasterTableView>
                    </telerik:RadGrid>
                </asp:Panel>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="PnlConfiguracion" Visible="false">
            <div class="w3-panel Titulos">
                <b>Parametrización</b>
            </div>
            <div class="w3-panel">
                <telerik:RadTextBox runat="server" ID="RTNombre" Label="Nombre" Width="400px" MaxLength="100">
                </telerik:RadTextBox>
            </div>
            <div class="w3-panel w3-text-black" style="overflow: auto; max-height: 400px; max-width: 100%">
                <telerik:RadGrid runat="server" ID="gridInstancias">
                    <MasterTableView AutoGenerateColumns="false" CommandItemDisplay="top" EditMode="EditForms">
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
                        <EditFormSettings UserControlName="./grids/configReglas/Reglas.ascx" EditFormType="WebUserControl">
                            <EditColumn UniqueName="EditCommandColumn1">
                            </EditColumn>
                        </EditFormSettings>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
            <br />
        </asp:Panel>
        <telerik:RadButton runat="server" ID="btnSimularInstancias" Visible="false" Text="Simular"></telerik:RadButton>
        <telerik:RadButton runat="server" ID="RbtnRegresar" Text="Regresar" Visible="false"></telerik:RadButton>
        <br />
        <telerik:RadGrid runat="server" ID="gridAsignacion" Visible="false" AllowSorting="True" AutoGenerateColumns="true" AllowPaging="True" PageSize="10">
            <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true" OpenInNewWindow="true" UseItemStyles="false" FileName="Simulacion de Asignación">
            </ExportSettings>
            <MasterTableView Width="100%" CommandItemDisplay="Top">
                <CommandItemSettings ShowExportToCsvButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" ShowExportToExcelButton="true" />
            </MasterTableView>
        </telerik:RadGrid>
        <br />
        <telerik:RadButton runat="server" ID="btnAplicarAsignacion" Visible="false" Text="Aplicar asignacion"></telerik:RadButton>
        <br />
        <telerik:RadWindowManager ID="RadAviso" runat="server"></telerik:RadWindowManager>
    </telerik:RadAjaxPanel>
    <asp:HiddenField ID="HidenUrs" runat="server" />
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
</asp:Content>

