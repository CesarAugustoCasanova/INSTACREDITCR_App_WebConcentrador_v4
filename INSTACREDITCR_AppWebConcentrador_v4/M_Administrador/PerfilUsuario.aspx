<%@ Page Title="" Language="VB" MasterPageFile="~/M_Administrador/MasterPage.master" AutoEventWireup="false" CodeFile="PerfilUsuario.aspx.vb" Inherits="M_Administrador_PerfilUsuario" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPHMaster" Runat="Server">
    <telerik:radajaxpanel runat="server"  cssclass="container" ID="pnlPerfil">
        <div class="Titulos">Perfil De Usuario</div>
        <div class="w3-row-padding">
            <div class="w3-col s12 m6 l4"><b>Usuario: </b><telerik:RadLabel runat="server" Text='<%# tmpUSUARIO("CAT_LO_USUARIO") %>'></telerik:RadLabel></div>
            <div class="w3-col s12 m6 l4"><b>Nombre: </b><telerik:RadLabel runat="server" Text='<%# tmpUSUARIO("CAT_LO_NOMBRE") %>'></telerik:RadLabel></div>
            <div class="w3-col s12 m6 l4"><b>Rol: </b><telerik:RadLabel runat="server" Text='<%# tmpUSUARIO("CAT_PE_PERFIL") %>'></telerik:RadLabel></div>
            <div class="w3-col s12 m6 l4"><b>Horario: </b><telerik:RadLabel runat="server" Text='<%# tmpUSUARIO("CAT_LO_HENTRADA") & " - " & tmpUSUARIO("CAT_LO_HSALIDA") & " hrs"%>'></telerik:RadLabel></div>
            <div class="w3-col s12 m6 l4"><b>Estatus: </b><telerik:RadLabel runat="server" Text='<%# tmpUSUARIO("CAT_LO_ESTATUS") %>'></telerik:RadLabel></div>
            <div class="w3-col s12 m6 l4"><b>Supervisor: </b><telerik:RadLabel runat="server" Text='<%# tmpUSUARIO("CAT_LO_SUPERVISOR") %>'></telerik:RadLabel></div>
        </div>
    </telerik:radajaxpanel>
</asp:Content>

