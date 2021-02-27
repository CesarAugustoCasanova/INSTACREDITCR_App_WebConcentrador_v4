<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Nota.aspx.vb" Inherits="Nota" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="JS/iFrameMejorado.js?v=2" type="text/javascript"></script>
    <title>Nota</title>
</head>
<body style="font-size:.7em">
    <form id="form1" runat="server" onmousemove="window.parent.movement();">
        <noscript>
            <div class="w3-modal" style="display: block">
                <div class="w3-modal-content">
                    <div class="w3-container w3-red w3-center w3-padding-24 w3-jumbo">
                        JavaScript deshabilitado
                    </div>
                    <div class="w3-container w3-center w3-xlarge">
                        Javascript está deshabilitado en su navegador web. Por favor, para ver correctamente este sitio, <b><i>habilite javascript</i></b>.<br />
                        <br />
                        Para ver las instrucciones para habilitar javascript en su navegador, haga click <a href="http://www.enable-javascript.com/es/">aquí</a>.
                    </div>
                </div>
            </div>
        </noscript>
        <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
        <telerik:RadAjaxManager runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="BtnGuardarnota">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="TBNota" />
                        <telerik:AjaxUpdatedControl ControlID="RadNotification1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <div class="w3-panel">
            <telerik:RadTextBox runat="server" ID="TBNota" TextMode="MultiLine" CssClass="w3-input" Width="100%" Height="300px" MaxLength="3999"></telerik:RadTextBox>
            <telerik:RadButton runat="server" ID="BtnGuardarnota" Text="Guardar"></telerik:RadButton>
        </div>
        <telerik:RadNotification RenderMode="Lightweight" ID="RadNotification1" runat="server" Height="140px"
            Animation="Fade" EnableRoundedCorners="true" EnableShadow="true" AutoCloseDelay="1500"
            Position="Center" ShowCloseButton="true" KeepOnMouseOver="false">
        </telerik:RadNotification>
    </form>
</body>
</html>
