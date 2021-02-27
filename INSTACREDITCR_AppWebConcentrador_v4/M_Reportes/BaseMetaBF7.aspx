<%@ Page Title="" Language="VB" MasterPageFile="~/M_Reportes/MasterPage.master" AutoEventWireup="false" CodeFile="BaseMetaBF7.aspx.vb" Inherits="M_Reportes_BaseMetaBF7" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMaster" runat="Server">
    <h1 class="text-center">Reporte Base Meta BF7</h1>
    <telerik:RadFormDecorator runat="server" DecoratedControls="Fieldset" DecorationZoneID="porcentajesMeta" />
    <telerik:RadNotification runat="server" AutoCloseDelay="3000" EnableShadow="true" ID="notificacion1" KeepOnMouseOver="true" ShowCloseButton="true" Position="center"></telerik:RadNotification>
    <telerik:RadAjaxManager runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="pnlAplicar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gridBF2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div class="container my-2" id="FechaFoto">
        <fieldset>
            <legend>Fecha Foto</legend>
            <div class="row justify-content-center">
                <div class="col-md-4 my-1">
                    <label>Fecha</label>
                    <telerik:RadDatePicker runat="server" ID="RPFechaReporte" Width="100%"></telerik:RadDatePicker>
                </div>
                <div class="col-12"></div>
                <div class="col-md-3 my-1">
                    <telerik:RadButton runat="server" ID="btnGenerarReporte" Text="Generar" CssClass="bg-success border-0 text-white btn-block" SingleClick="true" SingleClickText="Generando">
                    </telerik:RadButton>
                </div>

            </div>
        </fieldset>
    </div>
    <div class="container my-2" id="porcentajesMeta">
        <fieldset>
            <legend>% Meta por Bucket</legend>
            <div class="row justify-content-center">
                <div class="col-md-2 my-1">
                    <label>PRIME</label><asp:TextBox runat="server" Width="100%" ID="bucket0"></asp:TextBox>
                </div>
                <div class="col-md-2 my-1">
                    <label>PRIME-MED</label><asp:TextBox runat="server" Width="100%" ID="bucket1"></asp:TextBox>
                </div>
                <div class="col-md-2 my-1">
                    <label>SECOND</label><asp:TextBox runat="server" Width="100%" ID="bucket2"></asp:TextBox>
                </div>
                <div class="col-md-2 my-1">
                    <label>TERCH</label><asp:TextBox runat="server" Width="100%" ID="bucket3"></asp:TextBox>
                </div>
                <div class="col-md-2 my-1">
                    <label>FOURTH</label><asp:TextBox runat="server" Width="100%" ID="bucket4"></asp:TextBox>
                </div>
                <div class="col-12 my-1"></div>
                <div class="col-md-2 my-1">
                    <telerik:RadAjaxPanel runat="server" ID="pnlAplicar">
                        <asp:Button runat="server" ID="btnAplicar" Text="Aplicar porcentajes" CssClass="btn btn-block btn-success" />
                    </telerik:RadAjaxPanel>
                </div>
            </div>
        </fieldset>
    </div>
    <telerik:RadInputManager runat="server">
        <telerik:NumericTextBoxSetting AllowRounding="false" MinValue="0" MaxValue="100" DecimalDigits="2" EmptyMessage="%" Type="Percent">
            <TargetControls>
                <telerik:TargetInput ControlID="bucket0" />
                <telerik:TargetInput ControlID="bucket1" />
                <telerik:TargetInput ControlID="bucket2" />
                <telerik:TargetInput ControlID="bucket3" />
                <telerik:TargetInput ControlID="bucket4" />
            </TargetControls>
        </telerik:NumericTextBoxSetting>
    </telerik:RadInputManager>
    <br />
    <telerik:RadSpreadsheet runat="server" ID="ssBF7" Visible="false"></telerik:RadSpreadsheet>

</asp:Content>

