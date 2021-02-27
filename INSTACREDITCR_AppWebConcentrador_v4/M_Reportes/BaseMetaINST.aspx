<%@ Page Title="" Language="VB" MasterPageFile="~/M_Reportes/MasterPage.master" AutoEventWireup="false" CodeFile="BaseMetaINST.aspx.vb" Inherits="M_Reportes_BaseMetaINST" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMaster" runat="Server">
    <telerik:RadFormDecorator runat="server" DecoratedControls="Fieldset" DecorationZoneID="FechaFoto" />
    <telerik:RadNotification runat="server" AutoCloseDelay="3000" EnableShadow="true" ID="notificacion1" KeepOnMouseOver="true" ShowCloseButton="true" Position="center"></telerik:RadNotification>
    <h1 class="text-center">Reporte Base Meta Institucional</h1>
    <div class="container my-2" id="FechaFoto">
        <fieldset>
            <legend>Configuración</legend>
            <div class="row justify-content-center">
                <div class="col-md-4 my-1">
                    <label>Fecha foto:</label>
                    <telerik:RadDatePicker runat="server" ID="RPFechaReporte" Width="100%"></telerik:RadDatePicker>
                </div>
                <div class="col-12"></div>
                <div class="col-md-3 my-1">
                    <telerik:RadButton runat="server" ID="btnGenerarReporte" Text="Generar" CssClass="bg-success border-0 text-white btn-block" SingleClick="true" SingleClickText="Generando">
                    </telerik:RadButton>
                </div>
                <div runat="server" id="pnlDetalles" cssclass="col-md-3 my-1" visible="false">
                    <telerik:RadButton runat="server" ID="btnGenerarDetalle" Text="Ver detalle" CssClass="bg-info border-0 text-white btn-block" SingleClick="true" SingleClickText="Generando">
                    </telerik:RadButton>
                </div>
            </div>
        </fieldset>
    </div>
    <div class="m-2">
        <telerik:RadSpreadsheet runat="server" ID="ssINST" Visible="false"></telerik:RadSpreadsheet>
    </div>

</asp:Content>


