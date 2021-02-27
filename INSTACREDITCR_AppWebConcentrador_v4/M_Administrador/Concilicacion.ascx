<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Concilicacion.ascx.vb" Inherits="M_Administrador_Concilicacion" %>
<div class="container">
    <telerik:RadComboBox runat="server" ID="CbPlaza" DataTextField="Plaza" DataValueField="ID" AutoPostBack="true"></telerik:RadComboBox>
    <telerik:RadComboBox runat="server" ID="CbUsuario" DataTextField="Nombre" DataValueField="Usuario"></telerik:RadComboBox>
    <telerik:RadTextBox runat="server" ID="txtMotivo" MaxLength="250"></telerik:RadTextBox>
    <telerik:RadButton runat="server" ID="BtnAceptar" Text="Aceptar" CommandName='<%# IIf((TypeOf(Container) is GridEditFormInsertItem), "PerformInsert", "Update")%>'></telerik:RadButton>
    <telerik:RadButton runat="server" ID="BtnCancelar" Text="Cancelar" CommandName="Cancel"></telerik:RadButton>
</div>
