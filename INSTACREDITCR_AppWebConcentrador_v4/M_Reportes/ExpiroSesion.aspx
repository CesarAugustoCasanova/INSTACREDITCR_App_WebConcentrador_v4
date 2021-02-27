<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ExpiroSesion.aspx.vb" Inherits="ExpiroSesion"
    UICulture="es" Culture="es-MX" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Estilos/HTML.css" rel="stylesheet" />
    <link href="Estilos/Modal.css" rel="stylesheet" />
    <link href="Estilos/ObjHtmlS.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManagerlogin" runat="server" EnableScriptGlobalization="true"
            EnableScriptLocalization="true">
        </asp:ToolkitScriptManager>
        <div class="Pagina">
            <table class="Table">
                <tr>
                    <td class="Izquierda" rowspan="2">
                        <asp:Image ID="ImgLogo_Cl" runat="server" ImageUrl="~/Imagenes/ImgLogo_Cl.png" Width="30%"/>
                    </td>
                    <td class="TituloP" rowspan="2">Módulo De Reportes
                    </td>
                    <td class="Derecha" style="width:30%">
                        <asp:Image ID="ImgLogo_Ca" runat="server" ImageUrl="~/Imagenes/ImgLogo_Ca.png"/></td>
                </tr>
            </table>
            <div class="Espacio">
            </div>
        </div>
        <asp:Button ID="BtnModalMsj" runat="server" Style="visibility: hidden" />
        <asp:Panel ID="PnlModalMsj" runat="server" CssClass="ModalMsj" Style="display: none">
            <div class="heading">
                Aviso
            </div>
            <div class="CuerpoMsj">
                <table class="Table">
                    <tr align="center">
                        <td>&nbsp;</td>
                    </tr>
                    <tr align="center">
                        <td>
                            <asp:Label ID="LblMsj" runat="server" CssClass="LblDesc" Text="Su Sesión Ha Caducado"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="BtnSalir" runat="server" CssClass="button green close" Text="Aceptar" />
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        <asp:Button ID="BtnCancelarMsj" runat="server" CssClass="button green close" Style="visibility: hidden" />
        <asp:ModalPopupExtender ID="MpuMensajes" runat="server" PopupControlID="PnlModalMsj" TargetControlID="BtnModalMsj"
            CancelControlID="BtnCancelarMsj" BackgroundCssClass="modalBackground">
        </asp:ModalPopupExtender>
    </form>
</body>
</html>
