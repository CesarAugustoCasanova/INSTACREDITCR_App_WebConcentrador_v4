<%@ Page Title="" Language="VB" MasterPageFile="~/M_Administrador/MasterPage.master" AutoEventWireup="false" CodeFile="PonderacionCodigos.aspx.vb" Inherits="M_Administrador_PrioridadReglas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMaster" Runat="Server">
      <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="UPNLGeneral" runat="server" UpdatePanelCssClass="w3-center" CssClass="w3-center" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
        <br />
        <div class="w3-container Titulos">
            <b>Ponderación Codigos</b>
        </div>
            <label> ID-MC | CODIGO ACCION | DESCRIPCION | CODIGO RESULTADO | DESCRIPCION  </label>
      
        
             <telerik:RadListBox RenderMode="Lightweight" ID="LBPonderacion" runat="server" AllowReorder="true" AutoPostBackOnReorder="true" 
                    Width="100%" DataTextField ="V_TEXT" DataValueField="V_VALUE" Height="450px" EnableDragAndDrop="true"  >
                    <buttonsettings ShowReorder="true" verticalalign="Middle"></buttonsettings>
                    <%--<itemtemplate>
                        <span class="product-title">
                            <%# DataBinder.Eval(Container, "Text")%></span> <span class="bearing">"Ponderacion" & <%# DataBinder.Eval(Container, "Value")%>'</span>
                    </itemtemplate>--%>
                 </telerik:RadListBox>
            <br />
        
        <asp:Button runat="server" ID="BTNAceptar" Visible="true" Text="Aceptar"></asp:Button>
        <asp:Button runat="server" ID="BTNCancelar" Visible="true" Text="Cancelar"></asp:Button>
        <br />
      </telerik:RadAjaxPanel>
</asp:Content>

