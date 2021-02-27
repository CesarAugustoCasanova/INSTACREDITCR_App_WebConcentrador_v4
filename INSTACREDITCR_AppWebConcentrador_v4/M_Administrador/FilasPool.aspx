<%@ Page Title="Filas activas" Language="VB" MasterPageFile="./MasterPage.master" AutoEventWireup="false" CodeFile="FilasPool.aspx.vb" Inherits="FilasPool" EnableViewState="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>




<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <script>
        function confirmCallBackFn(arg) {
            alert("user chose: " + arg);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMaster" runat="Server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rdgrdEstatusFilas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rdgrdEstatusFilas" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>

    <div class="container align-content-center" runat="server" id="Filas">
        <div class="Titulos">Filas de trabajo</div>

        <telerik:RadWindowManager RenderMode="Lightweight" ID="RadWindowManager1" runat="server" EnableShadow="true">
        </telerik:RadWindowManager>
        <div class="w3-center">
            <telerik:RadTicker ID="rdtckrMensaje" runat="server" AutoStart="false" Loop="true" Visible="false" ForeColor="Red" Font-Size="XX-Large">
                <Items>
                    <telerik:RadTickerItem>No existen filas definidas</telerik:RadTickerItem>
                </Items>
            </telerik:RadTicker>
        </div>

        <telerik:RadGrid ID="rdgrdEstatusFilas" runat="server" AutoGenerateColumns="False" ClientSettings-EnableRowHoverStyle="true" OnItemCommand="rdgrdEstatusFilas_ItemCommand" OnItemCreated="rdgrdEstatusFilas_ItemCreated" OnItemDataBound="rdgrdEstatusFilas_ItemDataBound" RenderMode="Lightweight" Visible="False" Width="100%">
            <%-- <ClientSettings>
                                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                            </ClientSettings>--%>
            <MasterTableView>
                <Columns>
                    <telerik:GridBoundColumn DataField="NOM_FILA" HeaderStyle-HorizontalAlign="Center" HeaderText="Filas" ItemStyle-HorizontalAlign="Center" UniqueName="NOM_FILA">
                        <HeaderStyle Width="60px" />
                        <ItemStyle Width="60px" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="STATUS" HeaderStyle-HorizontalAlign="Center" HeaderText="Estado" ItemStyle-HorizontalAlign="Center" UniqueName="STATUS">
                        <HeaderStyle Width="30px" />
                        <ItemStyle Width="30px" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="SIN_GESTION" HeaderStyle-HorizontalAlign="Center" HeaderText="Sin gestión" ItemStyle-HorizontalAlign="Center">
                        <HeaderStyle Width="50px" />
                        <ItemStyle Width="50px" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="POR_GESTIONAR" HeaderStyle-HorizontalAlign="Center" HeaderText="En fila" ItemStyle-HorizontalAlign="Center">
                        <HeaderStyle Width="50px" />
                        <ItemStyle Width="50px" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="EN_GESTION" HeaderStyle-HorizontalAlign="Center" HeaderText="En gestión" ItemStyle-HorizontalAlign="Center">
                        <HeaderStyle Width="50px" />
                        <ItemStyle Width="50px" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="GESTIONADAS" HeaderStyle-HorizontalAlign="Center" HeaderText="Gestionadas" ItemStyle-HorizontalAlign="Center">
                        <HeaderStyle Width="50px" />
                        <ItemStyle Width="50px" />
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="60px" HeaderText="Gestores asignados" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px" UniqueName="TemplateEditColumn">
                        <ItemTemplate>
                            <telerik:RadComboBox ID="drpdwchbxGestores" runat="server" AutoPostBack="true" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" OnSelectedIndexChanged="drpdwchbxGestores_SelectedIndexChanged" RenderMode="Lightweight">
                            </telerik:RadComboBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="actualiza" HeaderStyle-HorizontalAlign="Center" HeaderText="Actualiza" ImageUrl="~/M_Administrador/Imagenes/refrehs.png" ItemStyle-HorizontalAlign="Center" UniqueName="actualiza">
                        <HeaderStyle Width="50px" />
                        <ItemStyle Width="50px" />
                    </telerik:GridButtonColumn>
                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="OnOff" HeaderStyle-HorizontalAlign="Center" HeaderText="On / Off" ItemStyle-HorizontalAlign="Center" UniqueName="OnOff">
                        <HeaderStyle Width="50px" />
                        <ItemStyle Width="50px" />
                    </telerik:GridButtonColumn>
                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="eliminar" HeaderStyle-HorizontalAlign="Center" HeaderText="Eliminar" ImageUrl="~/M_Administrador/Imagenes/Eliminar.png" ItemStyle-HorizontalAlign="Center" UniqueName="eliminar">
                        <HeaderStyle Width="50px" />
                        <ItemStyle Width="50px" />
                    </telerik:GridButtonColumn>
                </Columns>
            </MasterTableView>
            <FilterMenu RenderMode="Lightweight">
            </FilterMenu>
            <HeaderContextMenu RenderMode="Lightweight">
            </HeaderContextMenu>
        </telerik:RadGrid>
        <telerik:RadWindowManager runat="server" ID="WinAviso"></telerik:RadWindowManager>
        <asp:HiddenField ID="HidenUrs" runat="server" />
    </div>

</asp:Content>

