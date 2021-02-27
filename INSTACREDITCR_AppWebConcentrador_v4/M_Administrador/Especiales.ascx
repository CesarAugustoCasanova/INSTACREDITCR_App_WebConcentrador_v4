<%@ Control Language="VB" AutoEventWireup="true" CodeFile="Especiales.ascx.vb" Inherits="Especiales" %>
<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
<%--<telerik:RadAjaxPanel ID="UPNLGen" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" BackColor="#999999">--%>
<div style="overflow: auto; width: 100%; max-width: 1200px; text-size-adjust: 150%; font-size: 12px;">
    <div class="w3-row-padding">
        <div class="w3-col s12 m6">
            <telerik:RadLabel ID="LblNombre" runat="server" Text="Nombre:"></telerik:RadLabel>
            <telerik:RadTextBox ID="TxtNombre" runat="server" MaxLength="99" Width="100%"></telerik:RadTextBox>
        </div>
        <div class="w3-col s12 m6">
            <telerik:RadLabel ID="LblVigencia" runat="server" Text="Fecha de vigencia:"></telerik:RadLabel>
            <telerik:RadDatePicker ID="RDPVigencia" runat="server" MaxLength="25" Width="100%"></telerik:RadDatePicker>
        </div>
    </div>
    <div class="w3-row-padding">
        <div class="w3-col s6 m3">
        </div>
        <div class="w3-col s6 m3">
            <telerik:RadLabel ID="LblCampoSaldo" runat="server" Text="Tipo De Pago:"></telerik:RadLabel>
            <telerik:RadComboBox ID="RCBCampoSaldo" runat="server" Width="100%" DropDownAutoWidth="Enabled" EmptyMessage="Seleccione">
                <Items>
                    <telerik:RadComboBoxItem Value="T" Text="Liquidacion"/>
                    <telerik:RadComboBoxItem Value="PC" Text="Puesta Al Corriente"/>
                </Items>
            </telerik:RadComboBox>
        </div>
        <div class="w3-col s12 m6">
            <telerik:RadLabel ID="LblDescripcion" runat="server" Text="Descripción:"></telerik:RadLabel>
            <telerik:RadTextBox ID="TxtDescripcion" runat="server" Height="58px" MaxLength="200" TextMode="MultiLine" Width="100%" ></telerik:RadTextBox>
        </div>
    </div>
    <div class="w3-row-padding">
        <div class="w3-col s6 m3">
            <telerik:RadLabel ID="LblBonificacion" runat="server" Text="% Condonacion Comisión:"></telerik:RadLabel>
            <telerik:RadNumericTextBox ID="NtxtBonificacion" runat="server" MinValue="0" MaxValue="100" Type="Percent" Width="100%" ></telerik:RadNumericTextBox>
        </div>
        <div class="w3-col s6 m3">
            <telerik:RadLabel ID="LblCondonacionM" runat="server" Text="% Condonación interés moratorio:"></telerik:RadLabel>
            <telerik:RadNumericTextBox ID="NtxtCondonacionM" runat="server" MinValue="0" MaxValue="100" Type="Percent" Width="100%" ></telerik:RadNumericTextBox>

        </div>
        <div class="w3-col s6 m3">
            <telerik:RadLabel ID="LblCondonacionN" runat="server" Text="% Condonación interés normal:"></telerik:RadLabel>
            <telerik:RadNumericTextBox ID="NtxtCondonacionN" runat="server" MinValue="0" MaxValue="100" Type="Percent" Width="100%"></telerik:RadNumericTextBox>
        </div>
        <div class="w3-col s6 m3">
            <telerik:RadLabel ID="LblCondonacionC" runat="server" Text="% Condonación capital:"></telerik:RadLabel>
            <telerik:RadNumericTextBox ID="NtxtCononacionC" runat="server" MinValue="0" MaxValue="100" Type="Percent" Width="100%" ></telerik:RadNumericTextBox>
        </div>
    </div>
    <div class="w3-row-padding">
        <div class="w3-col s4">
            <hr />
        </div>
        <div class="w3-col s4">
            <telerik:RadLabel ID="LblExterno" runat="server" Text="% Cobro de honorarios por despachos externos:"></telerik:RadLabel>
            <telerik:RadNumericTextBox ID="NtxtExterno" runat="server" MinValue="0" MaxValue="100" Type="Percent" Width="100%" ></telerik:RadNumericTextBox>
        </div>
        <div class="w3-col s4">
            <hr />
        </div>
    </div>
    <div class="w3-row-padding w3-margin">
        <div class="w3-col s6">
            <telerik:RadButton ID="BtnAccion" runat="server" Text="ACTUALIZAR" CommandName="Update" Visible='<%# Not (TypeOf DataItem Is Telerik.Web.UI.GridInsertionObject) %>' CssClass="w3-block"></telerik:RadButton>
            <telerik:RadButton ID="btnInsert" Text="INSERTAR" runat="server" CommandName="PerformInsert" Visible='<%# (TypeOf DataItem Is Telerik.Web.UI.GridInsertionObject) %>' CssClass="w3-block"></telerik:RadButton>
        </div>
        <div class="w3-col s6">
            <telerik:RadButton ID="Btncancelar" runat="server" Text="CANCELAR" CommandName="Cancel" CausesValidation="false" CssClass="w3-block"></telerik:RadButton>
        </div>
    </div>
</div>
