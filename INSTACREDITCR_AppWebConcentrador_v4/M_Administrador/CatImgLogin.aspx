<%@ Page Language="VB" AutoEventWireup="false" Title="MC :: Imagenes Login" MasterPageFile="~/M_Administrador/MasterPage.master" CodeFile="CatImgLogin.aspx.vb" Inherits="M_Administrador_CatImgLogin" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPHMaster" runat="Server">
    <telerik:RadAjaxLoadingPanel runat="server" ID="Up1"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel runat="server" ID="PNLGEN" LoadingPanelID="Up1">
        <div class="w3-container">
            <div class="w3-row">
                <div class="w3-col s12 m6 l3">
                    <asp:Image runat="server" ID="imagen1" ImageUrl="~/M_Administrador/Imagenes/ImgDefault.png" Width="350" />
                    <div class="w3-panel" style="padding-left: 64px;">
                        <telerik:RadAsyncUpload runat="server" ID="Upload1" AllowedFileExtensions=".jpg,.jpeg,.png" RenderMode="Lightweight"></telerik:RadAsyncUpload>
                        <br />
                        <telerik:RadButton runat="server" ID="BtnCarga1" Text="Cargar"></telerik:RadButton>
                        <telerik:RadButton runat="server" ID="BtnElimina1" Text="Elminar"></telerik:RadButton>
                    </div>
                </div>
                <div class="w3-col s12 m6 l3">
                    <asp:Image runat="server" ID="imagen2" ImageUrl="~/M_Administrador/Imagenes/ImgDefault.png" Width="350" />
                    <div class="w3-panel" style="padding-left: 64px;">
                        <telerik:RadAsyncUpload runat="server" ID="Upload2" AllowedFileExtensions=".jpg,.jpeg,.png" RenderMode="Lightweight"></telerik:RadAsyncUpload>
                        <br />
                        <telerik:RadButton runat="server" ID="BtnCarga2" Text="Cargar"></telerik:RadButton>
                        <telerik:RadButton runat="server" ID="BtnElimina2" Text="Elminar"></telerik:RadButton>
                    </div>
                </div>
                <div class="w3-col s12 m6 l3">
                    <asp:Image runat="server" ID="imagen3" ImageUrl="~/M_Administrador/Imagenes/ImgDefault.png" Width="350" />
                    <div class="w3-panel" style="padding-left: 64px;">
                        <telerik:RadAsyncUpload runat="server" ID="Upload3" AllowedFileExtensions=".jpg,.jpeg,.png" RenderMode="Lightweight"></telerik:RadAsyncUpload>
                        <br />
                        <telerik:RadButton runat="server" ID="BtnCarga3" Text="Cargar"></telerik:RadButton>
                        <telerik:RadButton runat="server" ID="BtnElimina3" Text="Elminar"></telerik:RadButton>
                    </div>
                </div>
                <div class="w3-col s12 m6 l3">
                    <asp:Image runat="server" ID="imagen4" ImageUrl="~/M_Administrador/Imagenes/ImgDefault.png" Width="350" />
                    <div class="w3-panel" style="padding-left: 64px;">
                        <telerik:RadAsyncUpload runat="server" ID="Upload4" AllowedFileExtensions=".jpg,.jpeg,.png" RenderMode="Lightweight"></telerik:RadAsyncUpload>
                        <br />
                        <telerik:RadButton runat="server" ID="BtnCarga4" Text="Cargar"></telerik:RadButton>
                        <telerik:RadButton runat="server" ID="BtnElimina4" Text="Elminar"></telerik:RadButton>
                    </div>
                </div>
            </div>
        </div>
    </telerik:RadAjaxPanel>
</asp:Content>

