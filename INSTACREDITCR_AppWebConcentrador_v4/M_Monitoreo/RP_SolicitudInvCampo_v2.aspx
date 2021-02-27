<%@ Page Title="" Language="VB" MasterPageFile="~/M_Monitoreo/MasterPage.master" AutoEventWireup="false" CodeFile="RP_SolicitudInvCampo_v2.aspx.vb" Inherits="M_Monitoreo_RP_SolicitudInvCampo_v2" %>

<%@ Register Assembly="GMaps" Namespace="Subgurim.Controles" TagPrefix="cc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Reporte Visitas</title>
    <script type='text/javascript' src='https://maps.googleapis.com/maps/api/js?key=AIzaSyB4XOxMMENmanihA9hOqfPVAcWPR3eUW3I'></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadFormDecorator ID="FormDecorator1" runat="server" DecoratedControls="all" DecorationZoneID="optionsDecoration"></telerik:RadFormDecorator>
    <telerik:RadAjaxPanel runat="server" CssClass="container" LoadingPanelID="RadAjaxLoadingPanel1">
        <div class="container" id="optionsDecoration">
            <h3>Reporte Investigaciones De Campo</h3>
            <fieldset>
                <legend>Configuraci&#243;n</legend>
                <div class="w-100 w3-center">
                    <label class="font-weight-bold">Tipo De Reporte: </label>
                    <telerik:RadRadioButtonList ID="RblReporte" runat="server" Direction="Horizontal" CssClass="mb-2">
                        <Items>
                            <telerik:ButtonListItem Text="Solicitudes" ToolTip="Solicitudes de Investigaciones" Value="Solicitudes" />
                            <telerik:ButtonListItem Text="Domicilio" ToolTip="Investigaciones Domicilio" Value="Domicilio" />
                            <telerik:ButtonListItem Text="Ingresos" ToolTip="Investigaciones de Ingresos" Value="Ingresos" />
                        </Items>
                    </telerik:RadRadioButtonList>
                </div>
                <div class="w3-row-padding">
                    <div class="w3-col m12 l4">
                        <label class="font-weight-bold">Estatus:</label>
                        <telerik:RadComboBox ID="RcbEstatus" runat="server" AutoPostBack="True" CheckBoxes="true" DropDownAutoWidth="Enabled" EnableCheckAllItemsCheckBox="true" Localization-AllItemsCheckedString="Todos los elementos seleccionados" Localization-CheckAllString="Todos" Enabled="false" Width="100%"></telerik:RadComboBox>
                    </div>
                    <div class="w3-col m12 l4">
                        <label class="font-weight-bold">Tipo(s):</label>
                        <telerik:RadComboBox ID="RcbTipo" runat="server" AutoPostBack="True" CheckBoxes="true" DropDownAutoWidth="Enabled" EnableCheckAllItemsCheckBox="true" Localization-AllItemsCheckedString="Todos los elementos seleccionados" Localization-CheckAllString="Todos" Enabled="False" Width="100%"></telerik:RadComboBox>
                    </div>
                    <div class="w3-col m12 l4">
                        <label class="font-weight-bold">Usuario(s):</label>
                        <telerik:RadComboBox ID="RcbUsuario" runat="server" CheckBoxes="true" DropDownAutoWidth="Enabled" EnableCheckAllItemsCheckBox="true" Localization-AllItemsCheckedString="Todos los elementos seleccionados" Localization-CheckAllString="Todos" Enabled="False" Width="100%"></telerik:RadComboBox>
                    </div>
                </div>
                <div class="w3-row-padding">
                    <div class="w3-col m12 l4">
                        <label class="font-weight-bold">Desde:</label>
                        <telerik:RadDatePicker ID="TxtFechaI" runat="server" Enabled="False" Width="100%"></telerik:RadDatePicker>
                    </div>
                    <div class="w3-col m12 l4">
                        <label class="font-weight-bold">Hasta:</label>
                        <telerik:RadDatePicker ID="TxtFechaF" runat="server" Enabled="False" Width="100%"></telerik:RadDatePicker>
                    </div>
                    <div class="w3-col m12 l4">
                        <label class="font-weight-bold">Generar:</label>
                        <telerik:RadCheckBoxList ID="rcbGenerar" runat="server" Direction="Horizontal" SelectedIndex="0" SelectedValue="1" Enabled="False" >
                            <Items>
                                <telerik:ButtonListItem Selected="True" Text="Informaci&#243;n" Value="1" />
                                <telerik:ButtonListItem Text="Mapa" Value="2" />
                                <telerik:ButtonListItem Text="Puntos de Monitoreo" Value="3" />
                            </Items>
                        </telerik:RadCheckBoxList>
                    </div>
                </div>
                <div class="w3-block w3-center mt-2">
                    <telerik:RadButton ID="BtnGenerar" runat="server" SingleClick="true" SingleClickText="Procesando" Text="Generar" Enabled="False" CssClass="btn-green"></telerik:RadButton>
                </div>
            </fieldset>
        </div>
    </telerik:RadAjaxPanel>
    <div class="w3-margin-top s12">
        <div>
            <div class="w3-container w3-center w3-white">
                <telerik:RadAjaxPanel ID="RadAjaxPanelGral" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
                    <div class="w3-container w3-center w3-white">
                        <telerik:RadAjaxPanel ID="RadAjaxPanelFiltros" runat="server" Visible="false">
                            <div class="w3-container w3-center w3-white">


                                <telerik:RadComboBox ID="RcbExportar" runat="server" AutoPostBack="True" CheckBoxes="True" DropDownAutoWidth="Enabled" EnableCheckAllItemsCheckBox="True" Localization-AllItemsCheckedString="Todos los elementos seleccionados" Localization-CheckAllString="Todos" Visible="False" Culture="es-ES">
                                    <Items>
                                        <telerik:RadComboBoxItem runat="server" Text="Solicitudes" Value="Solicitudes" />
                                        <telerik:RadComboBoxItem runat="server" Text="Domicilio" Value="Domicilio" />
                                        <telerik:RadComboBoxItem runat="server" Text="Ingresos" Value="Ingresos" />
                                    </Items>
                                    <Localization AllItemsCheckedString="Todos los elementos seleccionados" CheckAllString="Todos" />
                                </telerik:RadComboBox>
                                <telerik:RadButton ID="BtnExportar" runat="server" Text="Exportar" Visible="False">
                                </telerik:RadButton>

                            </div>

                            <div class="w3-row w3-panel" style="overflow: auto">
                                <telerik:RadGrid RenderMode="Lightweight" ID="GVSolInvCampo" runat="server" AllowPaging="True" AllowSorting="True" OnNeedDataSource="GVSolInvCampo_NeedDataSource" PageSize="50" Visible="false" HeaderStyle-HorizontalAlign="Center">
                                    <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                    <ClientSettings>
                                        <Scrolling AllowScroll="True" FrozenColumnsCount="1" SaveScrollPosition="true" UseStaticHeaders="True" />
                                    </ClientSettings>
                                    <MasterTableView Frame="VSides" Caption="Investigaciones">
                                        <RowIndicatorColumn Visible="False">
                                        </RowIndicatorColumn>
                                        <ExpandCollapseColumn Created="True">
                                        </ExpandCollapseColumn>
                                        <PagerStyle PageSizeControlType="RadDropDownList" />
                                    </MasterTableView>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <PagerStyle Mode="NextPrevAndNumeric" PageSizeControlType="RadDropDownList" />
                                    <FilterMenu RenderMode="Lightweight">
                                    </FilterMenu>
                                    <HeaderContextMenu RenderMode="Lightweight">
                                    </HeaderContextMenu>
                                </telerik:RadGrid>
                            </div>
                            <asp:Panel ID="PanelMapa" runat="server" Style="visibility: hidden">
                                <cc:GMap ID="GMap1" runat="server" enableGoogleBar="False" Height="500px" Libraries="none" Width="100%" />
                            </asp:Panel>
                        </telerik:RadAjaxPanel>
                    </div>
                </telerik:RadAjaxPanel>
            </div>



        </div>


    </div>
    <telerik:RadWindowManager ID="RamiWa" runat="server" EnableShadow="true"
        Animation="Resize" Modal="True" RenderMode="Lightweight"
        VisibleTitlebar="False" ShowContentDuringLoad="false">
        <Localization OK="Aceptar" />
    </telerik:RadWindowManager>
</asp:Content>


