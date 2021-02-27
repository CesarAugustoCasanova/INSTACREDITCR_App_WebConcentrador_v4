<%@ Page Title="" Language="VB" MasterPageFile="MasterPage.master" AutoEventWireup="false" CodeFile="Campanas.aspx.vb" Inherits="_Campanas" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPHMaster" runat="Server">
    <telerik:RadAjaxManager runat="server" ID="ajaxManager">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlCampana" />
                    <telerik:AjaxUpdatedControl ControlID="pnlCampanas" />
                    <telerik:AjaxUpdatedControl ControlID="RadNotification1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAgregarCampana">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlCampana" />
                    <telerik:AjaxUpdatedControl ControlID="pnlCampanas" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnCancelar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlCampana" />
                    <telerik:AjaxUpdatedControl ControlID="pnlCampanas" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="gridCampanas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadNotification1" />
                    <telerik:AjaxUpdatedControl ControlID="pnlCampana" />
                    <telerik:AjaxUpdatedControl ControlID="pnlCampanas" />
                    <telerik:AjaxUpdatedControl ControlID="pnlCargaMasiva" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnMasiva">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadNotification1" />
                    <telerik:AjaxUpdatedControl ControlID="pnlCargaMasiva" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
       
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel runat="server" ID="pnlLoading"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel runat="server" ID="pnlCampanas" LoadingPanelID="pnlLoading">
        <%--<telerik:RadButton runat="server" ID="btnAgregarCampana" Text="Agregar campaña"></telerik:RadButton>--%>
        <telerik:RadGrid runat="server" ID="gridCampanas" AutoGenerateColumns="false" AllowPaging="true" PageSize="5" AllowFilteringByColumn="true" OnItemDataBound="RadGrid1_ItemDataBound">
            <MasterTableView CommandItemDisplay="Top" DataKeyNames="Campana" EditMode="PopUp">
                <CommandItemSettings AddNewRecordText="Añadir Campaña" RefreshText="Recargar" />
                <Columns>
                    <telerik:GridButtonColumn Text="Delete" CommandName="Delete" ButtonType="FontIconButton" ConfirmDialogType="RadWindow" ConfirmTitle="Eliminar campaña" ConfirmTextFormatString="¿Eliminar '{0}'? Esta acción no se puede deshacer" ConfirmTextFields="Campana">
                        <HeaderStyle Width="36px" />
                    </telerik:GridButtonColumn>
                    <telerik:GridButtonColumn UniqueName="BtnEventos" Text="Ejecutar" CommandName="onEjecutar" ButtonType="FontIconButton" ConfirmDialogType="RadWindow" ConfirmTitle="Ejecutar campaña" ConfirmTextFormatString="¿Ejecutar '{0}'?" ConfirmTextFields="Campana">
                        <HeaderStyle Width="36px" />
                    </telerik:GridButtonColumn>
                    <telerik:GridEditCommandColumn ButtonType="FontIconButton" HeaderText="Editar"></telerik:GridEditCommandColumn>
                    <telerik:GridButtonColumn ButtonType="PushButton" Text="Cargar archivo" HeaderText="Carga Masiva" CommandName="onCargaMasiva" HeaderStyle-Width="100px"></telerik:GridButtonColumn>


                    <telerik:GridBoundColumn HeaderText="Campaña" DataField="Campana" CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Proveedor" DataField="Proveedor" CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Tipo" DataField="Tipo" CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="Estatus" HeaderText="Estatus" DataField="Estatus">
                        <FilterTemplate>
                            <telerik:RadComboBox runat="server" SelectedValue='<%# TryCast(Container, GridItem).OwnerTableView.GetColumn("Estatus").CurrentFilterValue %>' OnClientSelectedIndexChanged="EstatusChanged" EmptyMessage="Filtro">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Activa" Value="Activa" />
                                    <telerik:RadComboBoxItem Text="Inactiva" Value="Inactiva" />
                                </Items>
                            </telerik:RadComboBox>
                            <telerik:RadScriptBlock ID="RadScriptBlock2" runat="server">
                                <script type="text/javascript">
                                    function EstatusChanged(sender, args) {
                                        var tableView = $find("<%# TryCast(Container, GridItem).OwnerTableView.ClientID %>");
                                        tableView.filter("Estatus", args.get_item().get_value(), "EqualTo");
                                    }
                                </script>
                            </telerik:RadScriptBlock>
                        </FilterTemplate>
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn AllowFiltering="false">
                        <ItemTemplate>
                            <telerik:RadButton runat="server" CssClass='<%# "w3-text-white" & iif(Eval("Estatus")="Activa","w3-red","w3-green") %>' Text='<%# iif(Eval("Estatus")="Activa","Inactivar","Activar") %>' CommandName="Estatus"></telerik:RadButton>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridButtonColumn ButtonType="PushButton" Text="Simular" HeaderText="Simular" CommandName="onSaldo" HeaderStyle-Width="100px"></telerik:GridButtonColumn>
                </Columns>
                <EditFormSettings UserControlName="./gridCampanaMsj.ascx" EditFormType="WebUserControl">
                    <PopUpSettings CloseButtonToolTip="Cancelar cambios" KeepInScreenBounds="true" Modal="true" OverflowPosition="Center" Width="90%" Height="90%" ShowCaptionInEditForm="true" />
                    <EditColumn UniqueName="EditCommandColumn1">
                    </EditColumn>
                </EditFormSettings>
                <PagerStyle PageSizeLabelText="Elementos por página:" />
            </MasterTableView>
        </telerik:RadGrid>
        <telerik:RadWindowManager RenderMode="Lightweight" ID="RadWindowManager1" runat="server" Animation="Fade" KeepInScreenBounds="true" Modal="true" />
    </telerik:RadAjaxPanel>

    <telerik:RadAjaxPanel runat="server" ID="pnlCargaMasiva" Visible="false" CssClass="w3-text-black" PostBackControls="btnMasiva">
        <label>Carga masiva para la campaña </label>
        <telerik:RadLabel runat="server" ID="lblCampana"></telerik:RadLabel>
        <telerik:RadLabel runat="server" ID="LblProveedor"></telerik:RadLabel>
        <telerik:RadLabel runat="server" ID="LblTipo"></telerik:RadLabel>
        <br />
        <label>Seleccione el archivo (.csv) para la carga masiva</label>
        <telerik:RadAsyncUpload runat="server" ID="uploadMasiva" AllowedFileExtensions=".csv" MultipleFileSelection="Disabled"></telerik:RadAsyncUpload>
        <telerik:RadButton runat="server" ID="btnMasiva" Text="Ejecutar carga masiva"></telerik:RadButton>
    </telerik:RadAjaxPanel>
    <telerik:RadNotification RenderMode="Lightweight" ID="RadNotification1" runat="server" Height="140px" Animation="Fade" EnableRoundedCorners="true" EnableShadow="true" AutoCloseDelay="5500" Position="Center" OffsetX="-30" OffsetY="-70" ShowCloseButton="true" VisibleOnPageLoad="false" LoadContentOn="EveryShow" KeepOnMouseOver="false">
    </telerik:RadNotification>
</asp:Content>

