<%@ Page Title="" Language="VB" MasterPageFile="~/M_Administrador/MasterPage.master" AutoEventWireup="false" CodeFile="VisorExpedientes.aspx.vb" Inherits="VisorExpedientes" %>

<%@ Register Src="~/Modulos/FTP/FTPManager.ascx" TagName="FTP" TagPrefix="MC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMaster" runat="Server">
    <div class="text-center">
        <h1>Expedientes</h1>
        <%--<div class="text-muted">
            A continuación, puede elegir uno de los siguientes créditos para visualizar los documentos del mes en curso.
        </div>--%>
        <hr />
    </div>
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-md-6">
                <label>Selecciona un crédito</label>
                <telerik:RadComboBox runat="server" ID="rcbCréditos" AutoPostBack="true" DropDownAutoWidth="Enabled" EmptyMessage="Seleccione" MarkFirstMatch="true" Width="100%"></telerik:RadComboBox>
            </div>
        </div>
    </div>
    <MC:FTP runat="server" ID="modulo_ftp" Visible="false"></MC:FTP>
</asp:Content>

