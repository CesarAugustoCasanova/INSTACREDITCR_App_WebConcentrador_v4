<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DefaultNegociaciones.aspx.vb" Inherits="DefaultNegociaciones" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta name="format-detection" content="telephone=no" />
    <meta name="HandheldFriendly" content="true" />
    <title>MC Collect</title>
    <link href="https://fonts.googleapis.com/css?family=Prata|Roboto&display=swap" rel="stylesheet" />
</head>
<body>
    <form id="FrmLogin" runat="server">
        <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPnlGeneral" runat="server">
        </telerik:RadAjaxLoadingPanel>
        <div class="w3-row">
            <div class="w3-half">
                <img src="Imagenes/ImgLogo_Cliente.png" alt="MC Collect S.A. de C.V." class="img-fluid" style="max-height: 50px;" />
            </div>
            <div class="w3-half">
                <span style="font-family: 'Prata', serif; font-size: xx-large;">MC Collect</span>
            </div>
        </div>
        <asp:Label runat="server" ID="LblId" Visible="false"></asp:Label>
        <div class="w3-row">
            <div class="w3-half">
                <asp:Panel runat="server" ID="PnlCancelar" Visible="false">
                    <table>
                        <tr>
                            <td>Motivo de NO autorización</td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadTextBox runat="server" ID="TxtComentario" Height="300px" Width="200px" TextMode="MultiLine"></telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadButton runat="server" ID="RBtnAceptar" Text="Guardar"></telerik:RadButton>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
        </div>
        <div class="w3-row">
            <div class="w3-large">
                <asp:Label runat="server" ID="LblError" ForeColor="blue"></asp:Label>
            </div>
        </div>
    </form>
    <script src="Scripts/jQuery.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <link href="Estilos/w3.css" rel="stylesheet" />
    <link href="Estilos/bootstrap.min.css" rel="stylesheet" />
</body>
</html>


