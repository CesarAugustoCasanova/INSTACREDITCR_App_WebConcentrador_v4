<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Avisos.aspx.vb" Inherits="M_Gestion_Avisos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="JS/iFrameMejorado.js?v=2" type="text/javascript"></script>
    <link rel="stylesheet" href="Estilos/w3.css" />
    <title></title>
    <meta charset="utf-8" />
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
        <telerik:RadAjaxLoadingPanel runat="server" ID="uno"></telerik:RadAjaxLoadingPanel>
        <telerik:RadAjaxPanel runat="server" ID="pnlgen" LoadingPanelID="uno">
            <div class="w3-half">
                <telerik:RadLabel runat="server" Text="Participante"></telerik:RadLabel>
            </div>
            <div class="w3-half">
                <telerik:RadComboBox runat="server" ID="CBParticipantes" Width="100%" AutoPostBack="true" EmptyMessage="Seleccione"></telerik:RadComboBox>
            </div>
            <div class="w3-half">
                <telerik:RadLabel runat="server" Text="Visitadores"></telerik:RadLabel>
            </div>
            <div class="w3-half">
                <telerik:RadComboBox runat="server" ID="CBVisitadores" Width="100%"></telerik:RadComboBox>
            </div>
            <div class="w3-half">
                <telerik:RadLabel runat="server" Text="Tipo Domicilio"></telerik:RadLabel>
            </div>
            <div class="w3-half">
                <telerik:RadComboBox runat="server" ID="CbTipoVivienda" Width="100%" EmptyMessage="Seleccione">
                    <ItemTemplate>
                        <div class="w3-card">
                            <div class="w3-panel w3-blue w3-center">
                                <%# Eval("ID") %> - <%# Eval("TipoDireccion")%>
                            </div>
                            <div>
                                <%# Eval("Direccion") %>
                            </div>
                        </div>
                    </ItemTemplate>
                </telerik:RadComboBox>
            </div>
            <div class="w3-half">
                <telerik:RadLabel runat="server" Text="Plantilla"></telerik:RadLabel>
            </div>
            <div class="w3-half">
                <telerik:RadComboBox runat="server" ID="cbPlantillas" Width="100%"></telerik:RadComboBox>
            </div>
            <div class="w3-half">
                <telerik:RadLabel runat="server" Text="Fecha limite para obtener resultado" Width="100%"></telerik:RadLabel>
            </div>
            <div class="w3-half">
                <telerik:RadDatePicker runat="server" ID="DPFelimite" Width="100%"></telerik:RadDatePicker>
            </div>
            <div class="w3-half">
                <telerik:RadCheckBox runat="server" ID="CBSimulacion" Text="¿Simular saldos?" Width="100%" AutoPostBack="true"></telerik:RadCheckBox>
            </div>
            <div class="w3-half">
                <label>Fecha de simulacion</label>
                <telerik:RadDatePicker runat="server" ID="DPSimulacion" Width="100%" Enabled="False" Culture="es-ES">
		                    <Calendar Culture="es-ES" EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
		                    </Calendar>
		                    <DateInput DateFormat="yyyy-MM-dd" DisplayDateFormat="yyyy-MM-dd" LabelWidth="40%">
		                        <EmptyMessageStyle Resize="None" />
		                        <ReadOnlyStyle Resize="None" />
		                        <FocusedStyle Resize="None" />
		                        <DisabledStyle Resize="None" />
		                        <InvalidStyle Resize="None" />
		                        <HoveredStyle Resize="None" />
		                        <EnabledStyle Resize="None" />
		                    </DateInput>
		                    <DatePopupButton CssClass="rcCalPopup rcDisabled" HoverImageUrl="" ImageUrl="" />
                </telerik:RadDatePicker>
            </div>
            <div class="w3-half">
                <telerik:RadButton runat="server" ID="BtnEnviar" Text="Enviar" SingleClick="true" SingleClickText="Generando"></telerik:RadButton>
            </div>
            <div class="w3-half">
                <telerik:RadLabel runat="server" ID="LblError" ForeColor="Red"></telerik:RadLabel>

            </div>

        </telerik:RadAjaxPanel>

    </form>
</body>
</html>
