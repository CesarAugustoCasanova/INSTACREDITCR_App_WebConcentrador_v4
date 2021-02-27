<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Relacionados.aspx.vb" Inherits="Relacionados" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mc :: Modulo Gestión</title>
    <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
    <link rel="stylesheet" href="Estilos/w3.css" />
    <script src="JS/iFrameMejorado.js?v=2" type="text/javascript"></script>
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
        <%--<div class="w3-container w3-center w3-blue">
            <b>Participantes del Crédito</b>
        </div>
        <telerik:RadGrid ID="GvParticipantes" runat="server" OnNeedDataSource="GvParticipantes_NeedDataSource">
            <ClientSettings>
                <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="2"></Scrolling>
            </ClientSettings>
            <MasterTableView TableLayout="Fixed"></MasterTableView>
            <HeaderStyle Width="200px" />
        </telerik:RadGrid>--%>
        <br />
        <div class="w3-container w3-center w3-blue">
            <b>Creditos Relacionados</b>
        </div>
        <!-- Checar los creditos relacionas y participantes del crédito. Tiene que aparecer todos los creditos incluido él mismo. Lo mismo para los avales. -->
        <telerik:RadGrid ID="GvRelacionados" runat="server" OnNeedDataSource="GvRelacionados_NeedDataSource">
            <ClientSettings>
                <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" CountGroupSplitterColumnAsFrozen="true" FrozenColumnsCount="2"></Scrolling>
            </ClientSettings>
            <HeaderStyle Width="200px" />
            <MasterTableView TableLayout="Fixed">
                <Columns>
                    <telerik:GridTemplateColumn>
                        <ItemTemplate>
                            <telerik:RadButton ID="BtnSeleccionar" CssClass="w3-button w3-center w3-hover-green" runat="server" Text="Seleccionar" OnClick="SeleccionarCto">
                            </telerik:RadButton>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </form>
</body>
</html>
