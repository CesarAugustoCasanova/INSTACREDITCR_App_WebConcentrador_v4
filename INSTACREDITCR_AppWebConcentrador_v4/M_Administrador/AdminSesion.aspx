<%@ Page Title="" Language="VB" MasterPageFile="~/M_Administrador/MasterPage.master"
    AutoEventWireup="false" CodeFile="AdminSesion.aspx.vb" Inherits="AdminSesion" %>


<asp:Content ID="CAvisos" ContentPlaceHolderID="CPHMaster" runat="Server">

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxPanel ID="UPNLGeneral" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" CssClass="container">
        <div class="Titulos">Usuarios Conectados</div>
        <telerik:RadGrid ID="RGVUsuarios" runat="server" AllowPaging="true" PageSize="10" AllowSorting="True" AutoGenerateColumns="true" ClientSettings-EnableRowHoverStyle="true">
            <MasterTableView Width="100%" CommandItemDisplay="Top" EditFormSettings-PopUpSettings-KeepInScreenBounds="true">
                <CommandItemTemplate>
                                <div class="w3-col s10 w3-right" style="height: 100%;">
                                    <asp:LinkButton ID="LinkButton4" runat="server" CommandName="RebindGrid" Style="text-decoration: none" CssClass="w3-hover-shadow w3-padding w3-round">Recargar lista</asp:LinkButton>
                                </div>
                            </CommandItemTemplate>
                <Columns>
                    <telerik:GridButtonColumn UniqueName="DESCONECTAR" HeaderText="DESCONECTAR" ItemStyle-Width="10px" Text="DESCONECTAR" CommandName="DESCONECTAR" ButtonType="ImageButton" ImageUrl="~/Imagenes/close-session.png"></telerik:GridButtonColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
        <asp:Button ID="BtnAceptar" runat="server" CssClass="mx-auto btn bg-success text-light" Text="Actualizar" Visible="False" />
        <asp:HiddenField ID="HidenUrs" runat="server" />
    </telerik:RadAjaxPanel>
    <telerik:RadWindowManager ID="RadAviso" runat="server">
        <Localization OK="Aceptar" />
    </telerik:RadWindowManager>
</asp:Content>
