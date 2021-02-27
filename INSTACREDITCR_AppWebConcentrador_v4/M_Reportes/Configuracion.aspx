<%@ Page Title="" Language="VB" MasterPageFile="./MasterPage.master" AutoEventWireup="false" CodeFile="Configuracion.aspx.vb" Inherits="Configuracion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHMaster" runat="Server">
    <telerik:RadAjaxLoadingPanel runat="server" ID="pnlLoading"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel runat="server" LoadingPanelID="pnlLoading" CssClass="container">
        <h4>Lista de reportes</h4>
        <telerik:RadGrid runat="server" ID="gridReportes" AllowPaging="true" PageSize="10" AutoGenerateColumns="true">
            <MasterTableView Caption=""  CommandItemDisplay="top">
                <CommandItemSettings AddNewRecordText="Nuevo reporte" RefreshText="Actualizar lista" />
                <Columns>
                    <telerik:GridButtonColumn ConfirmText="¿Seguro que deseas eliminar el reporte? Esta acción no se puede deshacer" ConfirmTitle="Eliminar" ConfirmDialogType="RadWindow" CommandName="Delete" ButtonCssClass="bg-danger text-white" HeaderStyle-Width="40px"></telerik:GridButtonColumn>
                    <telerik:GridEditCommandColumn ButtonType="FontIconButton" HeaderStyle-Width="40px"></telerik:GridEditCommandColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
        <telerik:RadWindowManager runat="server" Width="700px"></telerik:RadWindowManager>
    </telerik:RadAjaxPanel>
</asp:Content>

