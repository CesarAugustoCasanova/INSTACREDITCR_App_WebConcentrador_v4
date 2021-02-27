<%@ Page Title="" Language="VB" MasterPageFile="~/M_Reportes/MasterPage.master" AutoEventWireup="false" CodeFile="HighRisk.aspx.vb" Inherits="HighRisk" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPHMaster" runat="Server">
    <h1 class="text-center">Reporte High Risk</h1>

    <div class="container mb-2">
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

    <div class="container-fluid">
        <telerik:RadSpreadsheet runat="server" ID="ssHighRisk" Width="100%" Height="800px" Visible="false"></telerik:RadSpreadsheet>
    </div>
</asp:Content>

