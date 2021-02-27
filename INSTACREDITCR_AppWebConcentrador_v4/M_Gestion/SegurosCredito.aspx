<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SegurosCredito.aspx.vb" Inherits="SegurosCredito" %>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Mc :: Modulo Gestion</title>
    <script src="JS/iFrameMejorado.js?v=2" type="text/javascript"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
    <link rel="stylesheet" href="Estilos/w3.css" />
</head>

<body style="font-size: .7em" class="scroll">
    <form id="form1" runat="server" onmousemove="window.parent.movement();">
        <noscript>
            <div class="w3-modal" style="display: block">
                <div class="w3-modal-content">
                    <div class="w3-container w3-red w3-center w3-padding-24 w3-jumbo">
                        JavaScript deshabilitado
                    </div>
                    <div class="w3-container w3-center w3-xlarge">
                        Javascript está deshabilitado en su navegador web. Por favor, para ver correctamente este sitio,
                        <b><i>habilite javascript</i></b>.<br />
                        <br />
                        Para ver las instrucciones para habilitar javascript en su navegador, haga click <a
                            href="http://www.enable-javascript.com/es/">aquí</a>.
                    </div>
                </div>
            </div>
        </noscript>
        <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
        
            <telerik:RadLabel ID="LblCat_Lo_Usuario" runat="server"></telerik:RadLabel>
            <div class="w3-container w3-blue w3-center">
                <b>Seguros de cr�dito</b>
            </div>
                       
         <div class="w3-row-padding">
                <div class="w3-col s12 m3">
                    <label>Tipo de seguro</label>
                    <telerik:radLabel ID="TxtHIST_SC_TIPOSEGURO" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:radLabel>
                </div>
                <div class="w3-col s12 m3">
                    <label>Poliza de seguro</label>
                    <telerik:radLabel ID="TxtHIST_SC_POLIZASEGURO" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:radLabel>
                </div>
                <div class="w3-col s12 m3">
                    <label>Monto del seguro</label>
                    <telerik:radLabel ID="TxtHIST_SC_MONTOASEGURADO" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:radLabel>
                </div>
                <div class="w3-col s12 m3">
                    <label>Vigencia</label>
                    <telerik:radLabel ID="TxtHIST_SC_VIGENCIA" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:radLabel>
                </div>
            </div>
         <div class="w3-row-padding">
                <div class="w3-col s12 m3">
                    <label>Aseguradora</label>
                    <telerik:radLabel ID="TxtHIST_SC_NOMBREASEGRDORA" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:radLabel>
                </div>
                <div class="w3-col s12 m3">
                    <label>Beneficiario</label>
                    <telerik:radLabel ID="TxtHIST_SC_BENEFICIARIO" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:radLabel>
                </div>
                <div class="w3-col s12 m3">
                    <label>Porcentaje</label>
                    <telerik:radLabel ID="TxtHIST_SC_PROCENTAJE" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:radLabel>
                </div>
                <div class="w3-col s12 m3">
                    <label> &nbsp;</label>
                    <telerik:radLabel ID="TXt24" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:radLabel>
                </div>
            </div>
    </form>
</body>

</html>
