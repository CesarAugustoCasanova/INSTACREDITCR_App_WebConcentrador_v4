<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DatosEmpleo.aspx.vb" Inherits="M_Gestion_DatosEmpleo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="JS/iFrameMejorado.js?v=2" type="text/javascript"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
    <link rel="stylesheet" href="Estilos/w3.css" />
    <title></title>
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
                        Javascript está deshabilitado en su navegador web. Por favor, para ver correctamente este sitio, <b><i>habilite javascript</i></b>.<br />
                        <br />
                        Para ver las instrucciones para habilitar javascript en su navegador, haga click <a href="http://www.enable-javascript.com/es/">aquí</a>.
                    </div>
                </div>
            </div>
        </noscript>
        <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
        <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1"></telerik:RadAjaxManager>
        <div class="w3-container w3-center w3-blue">
            <b>Historico de Asignaciones</b>
        </div>
        <telerik:RadAjaxPanel runat="server" CssClass="w3-panel" Style="overflow: auto;">
            <telerik:RadGrid ID="GvHistAsigna" runat="server" AllowSorting="True" AllowPaging="true" HeaderStyle-HorizontalAlign="Center" GridLines="None">
                <MasterTableView AllowMultiColumnSorting="false">
                    <PagerStyle AlwaysVisible="true" />
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadAjaxPanel>

        <telerik:RadLabel ID="LblCat_Lo_Usuario" runat="server" Visible="false"></telerik:RadLabel>

        <div class="w3-panel w3-center w3-blue">
            <b>Dirección de títular y empleo</b>
        </div>
        <telerik:RadGrid runat="server" ID="gridEmpleos" AutoGenerateColumns="true" AllowPaging="true" MasterTableView-Caption="Direcciones particulares" PageSize="5">
            <ClientSettings>
                <Scrolling AllowScroll="true" EnableColumnClientFreeze="true" FrozenColumnsCount="3" />
            </ClientSettings>
        </telerik:RadGrid>
        <br />
        <telerik:RadGrid runat="server" ID="gridDirecciones" AutoGenerateColumns="true" AllowPaging="true" MasterTableView-Caption="Direcciones de empleo"  PageSize="5">
            <ClientSettings>
                <Scrolling AllowScroll="true" EnableColumnClientFreeze="true" FrozenColumnsCount="3" />
            </ClientSettings>
        </telerik:RadGrid>        
    </form>
</body>
</html>

