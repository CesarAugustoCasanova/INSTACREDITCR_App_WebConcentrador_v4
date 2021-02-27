<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Historicos_De_Actividades.aspx.vb" Inherits="Default3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mc :: Modulo Gestión</title>
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
                        Javascript está deshabilitado en su navegador web. Por favor, para ver correctamente este sitio, <b><i>habilite javascript</i></b>.<br />
                        <br />
                        Para ver las instrucciones para habilitar javascript en su navegador, haga click <a href="http://www.enable-javascript.com/es/">aquí</a>.
                    </div>
                </div>
            </div>
        </noscript>
        <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
        <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1">
        </telerik:RadAjaxManager>

        <!-- Informacion general -->
        <div class="w3-container w3-center w3-blue">
            <b>Historico de Actividades</b>
        </div>
        

        <telerik:RadAjaxPanel runat="server" CssClass="w3-container">
             <telerik:RadGrid ID="GvHistActMasivo" runat="server" OnNeedDataSource="GvHistActMasivo_NeedDataSource" AllowSorting="True" AllowPaging="true" HeaderStyle-HorizontalAlign="Center" GridLines="None">
                 <ClientSettings>
                     <Scrolling AllowScroll="true" FrozenColumnsCount="2" EnableColumnClientFreeze="true" UseStaticHeaders="true"/>
                 </ClientSettings>
                <MasterTableView AllowMultiColumnSorting="false">
                    <PagerStyle AlwaysVisible="true" />
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
