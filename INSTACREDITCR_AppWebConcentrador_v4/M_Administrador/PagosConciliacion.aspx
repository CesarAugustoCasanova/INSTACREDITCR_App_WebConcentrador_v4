<%@ Page Title="" Language="VB" MasterPageFile="~/M_Administrador/MasterPage.master" AutoEventWireup="false" CodeFile="PagosConciliacion.aspx.vb" Inherits="M_Administrador_PagosConciliacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMaster" runat="Server">
    <telerik:RadAjaxLoadingPanel runat="server" ID="load1"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel runat="server" ID="PNLGEN" LoadingPanelID="load1">
        <div class="container">
            <div class="w3-row">
                <telerik:RadGrid runat="server" ID="GVPagos" AllowFilteringByColumn="true" AutoGenerateColumns="false" AllowPaging="true" PageSize="20">
                    <MasterTableView AllowFilteringByColumn="True">
                        <Columns>
                            <telerik:GridEditCommandColumn ButtonType="FontIconButton" HeaderTooltip="Editar" HeaderText="Editar"></telerik:GridEditCommandColumn>
                            <telerik:GridBoundColumn HeaderText="Credito" DataField="Credito" AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Monto" DataField="Monto" AllowFiltering="true" AutoPostBackOnFilter="true"></telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn HeaderText="Fecha Pago" DataField="FPago" AllowFiltering="true" AutoPostBackOnFilter="true" PickerType="DatePicker" EnableTimeIndependentFiltering="true" DataFormatString="{0:dd/MM/yyyy}"></telerik:GridDateTimeColumn>
                            <telerik:GridDateTimeColumn HeaderText="Fecha Posteo" DataField="FPosteo" AllowFiltering="true" AutoPostBackOnFilter="true" PickerType="DatePicker" EnableTimeIndependentFiltering="true" DataFormatString="{0:dd/MM/yyyy}"></telerik:GridDateTimeColumn>
                            <telerik:GridBoundColumn HeaderText="Comicionable" DataField="Comicionable" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Usuario" DataField="Usuario" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            </telerik:GridBoundColumn>
                          
                        </Columns>
                        <EditFormSettings UserControlName="Concilicacion.ascx" EditFormType="WebUserControl">
                            <EditColumn UniqueName="EditCommandColumn1">
                            </EditColumn>
                        </EditFormSettings>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
            <div class="w3-row">
            </div>
        </div>
    </telerik:RadAjaxPanel>
</asp:Content>

