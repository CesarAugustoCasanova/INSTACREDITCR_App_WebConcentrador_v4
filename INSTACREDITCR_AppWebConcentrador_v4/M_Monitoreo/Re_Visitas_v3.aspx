<%@ Page Title="" Language="VB" MasterPageFile="~/M_Monitoreo/MasterPage.master" AutoEventWireup="false" CodeFile="Re_Visitas_v3.aspx.vb" Inherits="M_Monitoreo_Re_Visitas_v3" %>

<%@ Register Assembly="GMaps" Namespace="Subgurim.Controles" TagPrefix="cc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Reporte Visitas</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadFormDecorator ID="FormDecorator1" runat="server" DecoratedControls="all" DecorationZoneID="optionsDecoration"></telerik:RadFormDecorator>
    <telerik:RadAjaxPanel runat="server" CssClass="container" LoadingPanelID="RadAjaxLoadingPanel1">
        <!-- CONFIGURACION -->
        <div class="container" id="optionsDecoration">
            <h3>Reporte de Visitas - Cobranza</h3>
            <fieldset>
                <legend>Configuraci&#243;n</legend>
                <div class="w3-row-padding">
                    <div class="w3-col m12 l6">
                        <label class="font-weight-bold">Usuario:</label>
                        <telerik:RadComboBox ID="RcbUsuario" runat="server" CheckBoxes="true" DropDownAutoWidth="Enabled" EnableCheckAllItemsCheckBox="true" Localization-AllItemsCheckedString="Todos los elementos seleccionados" Localization-CheckAllString="Todos" AutoPostBack="True" Width="100%"></telerik:RadComboBox>
                    </div>
                    <div class="w3-col m12 l6">
                        <label class="font-weight-bold">Resultado:</label>
                        <telerik:RadComboBox ID="RcbResultado" runat="server" CheckBoxes="true" DropDownAutoWidth="Enabled" EnableCheckAllItemsCheckBox="true" Localization-AllItemsCheckedString="Todos los elementos seleccionados" Localization-CheckAllString="Todos" Enabled="False" Width="100%"></telerik:RadComboBox>
                    </div>
                    <div class="w3-col m12 l6">
                        <label class="font-weight-bold">Desde:</label>
                        <telerik:RadDatePicker ID="TxtFechaI" runat="server" Enabled="False" Width="100%"></telerik:RadDatePicker>
                    </div>
                    <div class="w3-col m12 l6">
                        <label class="font-weight-bold">Hasta:</label>
                        <telerik:RadDatePicker ID="TxtFechaF" runat="server" Enabled="False" Width="100%"></telerik:RadDatePicker>
                    </div>
                </div>
                <div class="w3-block mt-2 w3-center">
                    <label class="font-weight-bold">Tipo de Reporte:</label>
                    <telerik:RadCheckBoxList ID="rcbGenerar" runat="server" Direction="Horizontal" SelectedIndex="0" SelectedValue="1" Enabled="False" AutoPostBack="false">
                        <Items>
                            <telerik:ButtonListItem Text="Información" Value="1" />
                            <telerik:ButtonListItem Text="Mapa" Value="2" />
                            <%--<telerik:ButtonListItem Text="Puntos de Monitoreo" Value="3" />--%>
                        </Items>
                    </telerik:RadCheckBoxList>
                </div>
                <div class="w3-block mt-2 w3-center">
                    <telerik:RadButton ID="BtnGenerar" runat="server" SingleClick="true" SingleClickText="Procesando" Text="Generar" Visible="false" CssClass="bg-success border-0 text-white mx-2"></telerik:RadButton>

                    <telerik:RadButton ID="BtnExportar" runat="server" Text="Exportar" Visible="False" CssClass="bg-primary border-0 text-white mx-2"></telerik:RadButton>

                     <telerik:RadButton ID="btnimagen" runat="server" Text="Exportarimagen" Visible="False" CssClass="bg-primary border-0 text-white mx-2"></telerik:RadButton>
                </div>
            </fieldset>

            <!-- TABLA -->
            <div class="container mt-2 w3-responsive">
                <telerik:RadGrid RenderMode="Lightweight" ID="GVSolInvCampo" runat="server" AllowPaging="True" AllowSorting="True" OnNeedDataSource="GVSolInvCampo_NeedDataSource" PageSize="50" Visible="false" HeaderStyle-HorizontalAlign="Center" Width="100%">
                    <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                    <ClientSettings>
                        <Scrolling AllowScroll="True" FrozenColumnsCount="1" SaveScrollPosition="true" UseStaticHeaders="True" />
                    </ClientSettings>
                    <MasterTableView>
                        <PagerStyle PageSizeControlType="RadDropDownList" />
                    </MasterTableView>
                    <HeaderStyle HorizontalAlign="Center" />
                    <PagerStyle Mode="NextPrevAndNumeric" PageSizeControlType="RadDropDownList" />
                </telerik:RadGrid>
            </div>

            <!-- MAPA -->
            <div class="container mt-2">
                <asp:Panel ID="PanelMapa" runat="server" Style="display: none" Width="100%">
                    <cc:GMap ID="GMap1" runat="server" enableGoogleBar="False" Height="500px" Libraries="none" Width="100%" Key="AIzaSyB4XOxMMENmanihA9hOqfPVAcWPR3eUW3I" />
                </asp:Panel>
            </div>
        </div>
        
    </telerik:RadAjaxPanel>
    <telerik:RadWindowManager ID="RamiWa" runat="server" EnableShadow="true"
        Animation="Resize" Modal="True" RenderMode="Lightweight"
        VisibleTitlebar="False" ShowContentDuringLoad="false">
        <Localization OK="Aceptar" />
    </telerik:RadWindowManager>
</asp:Content>
