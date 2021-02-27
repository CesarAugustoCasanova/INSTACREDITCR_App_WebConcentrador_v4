<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReglasGlobales.aspx.vb" MasterPageFile="MasterPage.master" Inherits="ReglasGlobales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHMaster" runat="Server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="UPNLGeneral" runat="server" UpdatePanelCssClass="w3-center" CssClass="w3-center" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
        <br />
        <div class="w3-container Titulos">
            <b>Configuración de reglas globales</b>
        </div>

       
        <div class="w3-row-padding w3-text-black">
            <div class="w3-col s12 m2">
                <label>Nombre de la nueva regla</label>
                <telerik:RadTextBox ID="RTBNomRegla" runat="server" ></telerik:RadTextBox>                
                &nbsp;
                <asp:Button ID="btnCrearRegla" runat="server" Text="Crear Regla" />
            </div> 
            <div class="w3-col s12 m3">
                <label>Seleccione una regla global</label>
                <telerik:RadComboBox runat="server" ID="DDLRegla" EmptyMessage="Selecciona una Regla Global" AutoPostBack="true" Width="100%">
                </telerik:RadComboBox>
            </div>           
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
        </div>
        <br />
        <asp:Panel runat="server" ID="PnlDatos" Visible="false" CssClass="w3-container">
            <div class="w3-panel Titulos">
                <b>Parametrización</b>
            </div>
            <div class="w3-row w3-text-black" style="overflow: auto; max-height: 400px; max-width: 100%">
                <telerik:RadGrid runat="server" ID="gridReglasGlob">
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
        </asp:Panel>
        <asp:Button runat="server" ID="btnSimularCamp" Visible="true" Text="Simular creditos"></asp:Button>
        <br />
        <telerik:RadGrid runat="server" ID="gridAsignacion" Visible="true" AllowSorting="True" AutoGenerateColumns="true" AllowPaging="True" PageSize="10">
            <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true" OpenInNewWindow="true" UseItemStyles="false" FileName="Simulacion de Asignación">
            </ExportSettings>
            <MasterTableView Width="100%" CommandItemDisplay="Top">
                <CommandItemSettings ShowExportToCsvButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" ShowExportToExcelButton="true" />
            </MasterTableView>
        </telerik:RadGrid>
        <br />
        <telerik:RadWindowManager ID="RadAviso" runat="server"></telerik:RadWindowManager>
    </telerik:RadAjaxPanel>
    <asp:HiddenField ID="HidenUrs" runat="server" />
</asp:Content>
