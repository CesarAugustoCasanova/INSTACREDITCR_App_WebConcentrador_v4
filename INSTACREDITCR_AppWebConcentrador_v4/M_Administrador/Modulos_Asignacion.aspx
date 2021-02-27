<%@ Page Title="" Language="VB" MasterPageFile="~/M_Administrador/MasterPage.master" AutoEventWireup="false" CodeFile="Modulos_Asignacion.aspx.vb" Inherits="Modulos_Asignacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMaster" Runat="Server">
    <telerik:RadAjaxLoadingPanel runat="server" ID="pnlLoading"></telerik:RadAjaxLoadingPanel>
    <asp:Button runat="server" ID="btnRegresar" Text="Regresar" CssClass="btn btn-link"/>
    <div class="text-center">
    <h1>Asignacion</h1>
        <p>La regla <u><asp:Label runat="server" ID="lblRegla" Text="unknown" CssClass=""></asp:Label></u> engloba a <u><asp:Label runat="server" ID="lblCuantas" Text="0"></asp:Label></u> diferentes cuentas</p>
    </div>
    <telerik:RadAjaxPanel runat="server" cssclass="container" LoadingPanelID="pnlLoading">
        <div class="row">
            <div class="col-md-6"><label>Vigencia</label>
                <telerik:RadDatePicker runat="server" ID="dpVigencia" Width="100%"></telerik:RadDatePicker>
            </div>
            <div class="col-md-6"><label>Usuarios</label>
                <telerik:RadComboBox runat="server" ID="cbUsuarios" EmptyMessage="Seleccione" Width="100%" AllowCustomText="false" CheckBoxes="true" DropDownAutoWidth="Enabled"  MarkFirstMatch="true" ></telerik:RadComboBox>
            </div>
        </div>
        <div class="text-center my-2">
            <asp:Button runat="server" ID="btnAsignar" Text="Asignar cuentas" CssClass="btn btn-primary" />
        </div>
        <telerik:RadGrid runat="server" ID="gridResumen" AutoGenerateColumns="true" AllowPaging="true" PageSize="10" Visible="false" Width="100%"></telerik:RadGrid>
        <telerik:RadNotification runat="server" ID="not1" Position="Center" KeepOnMouseOver="true" AutoCloseDelay="10000" ShowCloseButton="true"></telerik:RadNotification>
    </telerik:RadAjaxPanel>
    
</asp:Content>

