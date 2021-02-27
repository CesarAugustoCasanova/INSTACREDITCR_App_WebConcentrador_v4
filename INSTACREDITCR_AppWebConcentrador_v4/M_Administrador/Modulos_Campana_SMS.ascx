<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Modulos_Campana_SMS.ascx.vb" Inherits="M_Administrador_Modulos_Campana_SMS" %>
<telerik:RadFormDecorator runat="server" DecoratedControls="Fieldset" DecorationZoneID="Data" />
<telerik:RadAjaxLoadingPanel runat="server" ID="pnlLoading"></telerik:RadAjaxLoadingPanel>
<div id="Data" class="container">
    <telerik:RadAjaxPanel runat="server" LoadingPanelID="pnlLoading">
        <fieldset>
            <legend>SMS -
            <asp:Label runat="server" ID="lblReglaNombre"></asp:Label>
                <small class="text-muted">(<asp:Label runat="server" ID="lblCuantasCuentas"></asp:Label>
                    cuentas)</small></legend>
            <div class="row justify-content-center">
                <div class="col-md-6">
                    <label>Selecciona la plantilla para generar los mensajes</label>
                    <telerik:RadDropDownList runat="server" ID="ddlPlantillas" Width="100%" DefaultMessage="Seleccione"></telerik:RadDropDownList>
                </div>
                <div class="col-12"></div>
                <div class="col-md-6 my-2 text-center">
                    <asp:Button runat="server" ID="btnGenerar" CssClass="btn btn-primary" Text="Generar SMS"/>
                </div>
            </div>
            <telerik:RadProgressManager ID="RadProgressManager1" runat="server"/>
        <telerik:RadProgressArea RenderMode="Lightweight" ID="RadProgressArea1" runat="server" Width="100%" ProgressIndicators="TotalProgressBar,TotalProgressBar,TotalProgress,TimeElapsed,CurrentFileName,FilesCount" DisplayCancelButton="true"/>
        </fieldset>
        <asp:Panel runat="server" ID="pnlResultados" Visible="false">
            <fieldset>
                <legend>Resultados</legend>
                <telerik:RadGrid runat="server" ID="gridResultados" PageSize="10" AllowPaging="true" AllowSorting="true" >
                    <MasterTableView CommandItemDisplay="top">
                        <CommandItemSettings  ShowAddNewRecordButton="false" ShowCancelChangesButton="false" ShowRefreshButton="false" ShowExportToExcelButton="true"  />
                    </MasterTableView>
                    <ExportSettings IgnorePaging="true" FileName="Resultados Campaña SMS" OpenInNewWindow="true" >
                    </ExportSettings>
                </telerik:RadGrid>
            </fieldset>
        </asp:Panel>
    </telerik:RadAjaxPanel>
</div>
