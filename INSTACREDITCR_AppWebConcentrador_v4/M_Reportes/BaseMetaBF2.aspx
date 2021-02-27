<%@ Page Title="" Language="VB" MasterPageFile="~/M_Reportes/MasterPage.master" AutoEventWireup="false" CodeFile="BaseMetaBF2.aspx.vb" Inherits="BaseMetaBF2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMaster" runat="Server">
    <h1 class="text-center">Reporte Base Meta BF2</h1>
    <telerik:RadFormDecorator runat="server" DecoratedControls="Fieldset" DecorationZoneID="porcentajesMeta" />
    <telerik:RadNotification runat="server" AutoCloseDelay="3000" EnableShadow="true" ID="notificacion1" KeepOnMouseOver="true" ShowCloseButton="true" Position="center"></telerik:RadNotification>
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
                <div runat="server" id="pnlDetalle" class="col-md-3 my-1" visible="false">
                    <telerik:RadButton runat="server" ID="btnGenerarDetalle" Text="Ver detalle" CssClass="bg-info border-0 text-white btn-block" SingleClick="true" SingleClickText="Generando">
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
                    <label>Bucket 0</label><asp:TextBox runat="server" Width="100%" ID="bucket0" Text="16"></asp:TextBox>
                </div>
                <div class="col-md-2 my-1">
                    <label>Bucket 1</label><asp:TextBox runat="server" Width="100%" ID="bucket1" Text="16"></asp:TextBox>
                </div>
                <div class="col-md-2 my-1">
                    <label>Bucket 2</label><asp:TextBox runat="server" Width="100%" ID="bucket2" Text="14"></asp:TextBox>
                </div>
                <div class="col-md-2 my-1">
                    <label>Bucket 3</label><asp:TextBox runat="server" Width="100%" ID="bucket3" Text="12"></asp:TextBox>
                </div>
                <div class="col-md-2 my-1">
                    <label>Bucket 4</label><asp:TextBox runat="server" Width="100%" ID="bucket4" Text="10"></asp:TextBox>
                </div>
                <div class="col-md-2 my-1">
                    <label>Bucket 5</label><asp:TextBox runat="server" Width="100%" ID="bucket5" Text="10"></asp:TextBox>
                </div>
                <div class="col-md-2 my-1">
                    <label>Bucket 6</label><asp:TextBox runat="server" Width="100%" ID="bucket6" Text="10"></asp:TextBox>
                </div>
                <div class="col-md-2 my-1">
                    <label>Bucket 7</label><asp:TextBox runat="server" Width="100%" ID="bucket7" Text="2"></asp:TextBox>
                </div>
                <div class="col-md-2 my-1">
                    <label>Bucket 8</label><asp:TextBox runat="server" Width="100%" ID="bucket8" Text="2"></asp:TextBox>
                </div>
                <div class="col-md-2 my-1">
                    <label>Bucket 9</label><asp:TextBox runat="server" Width="100%" ID="bucket9" Text="2"></asp:TextBox>
                </div>
                <div class="col-12">
                </div>
                <div class="col-md-2 my-1">
                    <asp:Button runat="server" ID="btnAplicarPorcentaje" Text="Aplicar porcentajes" CssClass="btn btn-success" />
                </div>
            </div>
        </fieldset>
    </div>

    <telerik:RadInputManager runat="server">
        <telerik:NumericTextBoxSetting AllowRounding="false" MinValue="0" MaxValue="100" DecimalDigits="2" EmptyMessage="%" Type="Percent" SelectionOnFocus="SelectAll">
            <TargetControls>
                <telerik:TargetInput ControlID="bucket0" />
                <telerik:TargetInput ControlID="bucket1" />
                <telerik:TargetInput ControlID="bucket2" />
                <telerik:TargetInput ControlID="bucket3" />
                <telerik:TargetInput ControlID="bucket4" />
                <telerik:TargetInput ControlID="bucket5" />
                <telerik:TargetInput ControlID="bucket6" />
                <telerik:TargetInput ControlID="bucket7" />
                <telerik:TargetInput ControlID="bucket8" />
                <telerik:TargetInput ControlID="bucket9" />
            </TargetControls>
        </telerik:NumericTextBoxSetting>
    </telerik:RadInputManager>
    <telerik:RadSpreadsheet runat="server" ID="ssBF2" Visible="false"></telerik:RadSpreadsheet>

</asp:Content>

