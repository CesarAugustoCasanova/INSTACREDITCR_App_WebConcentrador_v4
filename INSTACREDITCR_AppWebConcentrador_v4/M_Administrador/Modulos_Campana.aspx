<%@ Page Title="" Language="VB" MasterPageFile="~/M_Administrador/MasterPage.master" AutoEventWireup="false" CodeFile="Modulos_Campana.aspx.vb" Inherits="Modulos_Campana" %>

<%@ Register Src="~/M_Administrador/Modulos_Campana_SMS.ascx" TagName="SMS" TagPrefix="ModulosCamapana" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CPHMaster" runat="Server">
    <telerik:RadFormDecorator runat="server" DecorationZoneID="Options" DecoratedControls="Fieldset" />
    <asp:Button runat="server" ID="btnRegresar" Text="Regresar" CssClass="btn btn-link" />

    <h1 class="text-center">Generación de campañas</h1>
    <div class="container mb-2" id="Options">
        <fieldset>
            <legend>Configuración</legend>

            <div class="row justify-content-center">
                <div class="col-md-6">
                    <label>Seleccione el tipo de campaña que desea generar</label>
                    <telerik:RadDropDownList runat="server" ID="ddlCampana" Width="100%" DefaultMessage="Seleccione" AutoPostBack="true">
                        <Items>
                            <telerik:DropDownListItem Value="0" Text="SMS" />
                        </Items>
                    </telerik:RadDropDownList>
                </div>
            </div>
        </fieldset>
    </div>
    <asp:PlaceHolder runat="server" ID="phTipoCamapana"></asp:PlaceHolder>

</asp:Content>

