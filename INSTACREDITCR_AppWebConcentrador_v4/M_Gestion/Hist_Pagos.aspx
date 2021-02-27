<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Hist_Pagos.aspx.vb" Inherits="Hist_Pagos" %>

<!DOCTYPE html>

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
        <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1">
        </telerik:RadAjaxManager>

        <telerik:RadLabel ID="LblCat_Lo_Usuario" runat="server" Visible="false"></telerik:RadLabel>

        <div class="w3-container">
            <div class="w3-blue w3-center">
                <b>Historico de Pagos</b>
            </div>
            <asp:Panel ID="pnlPagos1" runat="server" CssClass=" w3-row-padding" Visible='<%# tmpPermisos("HIST_PAGOS_GUARDAR") %>'>
                <div class="w3-col l4 m6 s12">
                    <label>Fecha de Pago</label>
                    <telerik:RadDatePicker ID="TxtHist_Pa_Dtepago" runat="server" Width="100%" EnableTyping="false">
                        <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" Width="100%"
                            UseColumnHeadersAsSelectors="False" EnableWeekends="True"
                            FastNavigationNextText="&amp;lt;&amp;lt;">
                        </Calendar>
                        <DateInput ID="DateInput1" runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy">
                            <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                            <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                            <FocusedStyle Resize="None"></FocusedStyle>
                            <DisabledStyle Resize="None"></DisabledStyle>
                            <InvalidStyle Resize="None"></InvalidStyle>
                            <HoveredStyle Resize="None"></HoveredStyle>
                            <EnabledStyle Resize="None"></EnabledStyle>
                        </DateInput>
                    </telerik:RadDatePicker>
                </div>
                <div class="w3-col l4 m6 s12">
                    <label>Monto de Pago</label>
                    <telerik:RadNumericTextBox ID="TxtHist_Pa_Montopago" runat="server" MaxLength="10" CssClass="w3-input"
                        Width="100%">
                    </telerik:RadNumericTextBox>
                </div>
                <div class="w3-col l4 m6 s12">
                    <label>Lugar de Pago</label>
                    <telerik:RadDropDownList ID="DdlHist_Pa_Lugarpago" runat="server" Width="100%">
                        <Items>
                            <telerik:DropDownListItem Text="Seleccione" Selected="true" />
                            <telerik:DropDownListItem Text="Autoservicio OXXO" />
                            <telerik:DropDownListItem Text="Boleta de Pago" />
                            <telerik:DropDownListItem Text="Cobro en Castigado" />
                            <telerik:DropDownListItem Text="Reestructura" />
                        </Items>
                    </telerik:RadDropDownList>
                </div>
                <div class="w3-col l4 m6 s12">
                    <label>Referencia</label>
                    <telerik:RadTextBox ID="TxtHist_Pa_Referencia" runat="server" MaxLength="25" CssClass="w3-input"
                        Width="100%">
                    </telerik:RadTextBox>
                </div>
                <div class="w3-col l4 m6 s12">
                    <label>Tipo de Confirmacion</label>
                    <telerik:RadDropDownList ID="DdlHist_Pa_Confirmacion" runat="server" Width="100%">
                        <Items>
                            <telerik:DropDownListItem Text="Seleccione" Selected="true" />
                            <telerik:DropDownListItem Text="Telefono" />
                            <telerik:DropDownListItem Text="Correo" />
                        </Items>
                    </telerik:RadDropDownList>
                </div>
                <div class="w3-col l4 m6 s12">
                    <div class="w3-block w3-center w3-margin">
                        <br />
                        <telerik:RadButton ID="BtnGuardar" runat="server" Text="Guardar" CssClass="w3-button w3-green">
                        </telerik:RadButton>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlPagos2" runat="server" CssClass="w3-container w3-row" Visible='<%#Not tmpPermisos("HIST_PAGOS_GUARDAR") %>'>
                <div class="w3-block w3-center w3-margin">
                    Sin Permisos
                </div>
            </asp:Panel>
        </div>
        <div class="w3-container w3-blue w3-center">
            <b>Informaci�n de pagos</b>
        </div>
        <div class="w3-panel" style="overflow: auto">
            <telerik:RadGrid ID="RGInfoPagos" runat="server" HeaderStyle-HorizontalAlign="Center" Visible="true">
                <ClientSettings>
                    <Scrolling AllowScroll="true" EnableColumnClientFreeze="true" FrozenColumnsCount="3" />
                </ClientSettings>
            </telerik:RadGrid>
        </div>
        <div class="w3-container w3-blue w3-center">
            <b>Pagos Pendientes</b>
        </div>
        <div class="w3-panel" style="overflow: auto">
            <telerik:RadGrid ID="GVHist_PagP" runat="server" OnNeedDataSource="GVHist_PagP_NeedDataSource" Visible="true" Style="overflow: visible;" HeaderStyle-HorizontalAlign="Center">
                <MasterTableView></MasterTableView>
            </telerik:RadGrid>
        </div>
        <div class="w3-container w3-blue w3-center">
            <b>Pagos Validados</b>
        </div>
        <div class="w3-panel" style="overflow: auto">
            <telerik:RadGrid ID="GVHist_PagV" runat="server" HeaderStyle-HorizontalAlign="Center"
                OnNeedDataSource="GVHist_PagV_NeedDataSource" Visible="true" Style="overflow: visible;">
            </telerik:RadGrid>
        </div>
        <label>Pago total Validado: </label>
        <telerik:RadTextBox ID="TextPag" runat="server" ReadOnly="True"></telerik:RadTextBox>
        <br />
        <br />
        <div class="w3-container w3-blue w3-center">
            <b>Promesas de pago cumplidas / incumplidas</b>
        </div>
        <div class="w3-panel" style="overflow: auto">
            <telerik:RadGrid ID="GVPP_CI" runat="server" HeaderStyle-HorizontalAlign="Center"
                OnNeedDataSource="GVPP_CI_NeedDataSource" Visible="true" Style="overflow: visible;">
            </telerik:RadGrid>
        </div>
        <div class="w3-container w3-blue w3-center">
            <b>Promesas de pago pendientes</b>
        </div>
        <div class="w3-panel" style="overflow: auto">
            <telerik:RadGrid ID="GVPP_P" runat="server" HeaderStyle-HorizontalAlign="Center"
                OnNeedDataSource="GVPP_P_NeedDataSource" Visible="true" Style="overflow: visible;">
            </telerik:RadGrid>
        </div>
        <telerik:RadNotification ID="Notificacion" runat="server" Position="Center" Width="330" Height="160"
            Animation="Fade" EnableRoundedCorners="true" EnableShadow="true" Style="z-index: 100000">
        </telerik:RadNotification>
    </form>
</body>

</html>
