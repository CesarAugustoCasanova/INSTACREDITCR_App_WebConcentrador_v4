<%@ Page Title="" Language="VB" MasterPageFile="~/M_Reportes/MasterPage.master" AutoEventWireup="false" CodeFile="RollBack.aspx.vb" Inherits="RollBack" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMaster" runat="Server">

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rcbSubProd">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ssRollBack" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <div class="container mb-2">
        <div class="row justify-content-center">
            <div class="col-md-4 my-1">
                <label>Subproducto</label>
                <telerik:RadComboBox runat="server" ID="rcbSubProd" Width="100%" EmptyMessage="Seleccione" AutoPostBack="true">
                    <Items>
                        <telerik:RadComboBoxItem Text="BF/CSB TDC" Value="0" Selected="true" />
                        <telerik:RadComboBoxItem Text="MI PAGUITOS" Value="1" />
                    </Items>
                </telerik:RadComboBox>
            </div>

            <div class="col-md-4 my-1">
                <label>Fecha</label>
                <telerik:RadDatePicker runat="server" ID="RPFechaReporte" Width="100%" ></telerik:RadDatePicker>
            </div>
            <div class="col-12"></div>
            <div class="col-md-3 my-1">
                <telerik:RadButton runat="server" ID="btnGenerarReporte" Text="Generar" CssClass="bg-success border-0 text-white btn-block" SingleClick="true" SingleClickText="Generando" >
                </telerik:RadButton>
            </div>

        </div>
    </div>
    <br />
    <telerik:RadSpreadsheet runat="server" ID="ssRollBack" Height="650px" Visible="false" ></telerik:RadSpreadsheet>
    <telerik:RadNotification runat="server" AutoCloseDelay="3000" EnableShadow="true" ID="notificacion1" KeepOnMouseOver="true" ShowCloseButton="true" Position="center"></telerik:RadNotification>
</asp:Content>
