<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Negociaciones.aspx.vb" Inherits="Negociaciones" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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

        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script type="text/javascript">

                function ValidaLongitud(campo, longitudMaxima) {
                    //valor = document.getElementById('RGTelefono_ctl00_ctl02_ctl03_EditFormControl_TB2').value;
                    try {
                        if (campo.value.length > (longitudMaxima - 1))
                            return false;
                        else
                            return true;
                    } catch (e) {
                        return false;
                    }
                }


                function calculaUltMonto(campo) {
                    try {
                        var dropDown = $find("<%= RddlNumPagos.ClientID %>");
                 var numPagos = dropDown.get_selectedItem().get_text();
                 var txt1 = 0.00, txt2 = 0.00, txt3 = 0.00, txt4 = 0.00, txt5 = 0.00, txt6 = 0.00, txt7 = 0.00, txt8 = 0.00, txt9 = 0.00, txt10 = 0.00;
                 var valorSdoNego = 0.00;
                 var cal = 0.00;
                 valorSdoNego = parseFloat(nvl(document.getElementById('TxtSdoNegociado').value.replace(',', ''), 0));

                 txt1 = parseFloat(nvl(document.getElementById('RTxtMonto1').value.replace(',', ''), 0));

                 if (document.getElementById('RTxtMonto2')) {
                     txt2 = parseFloat(nvl(document.getElementById('RTxtMonto2').value.replace(',', ''), 0));
                 }
                 if (document.getElementById('RTxtMonto3')) {
                     txt3 = parseFloat(nvl(document.getElementById('RTxtMonto3').value.replace(',', ''), 0));
                 }
                 if (document.getElementById('RTxtMonto4')) {
                     txt4 = parseFloat(nvl(document.getElementById('RTxtMonto4').value.replace(',', ''), 0));
                 }
                 if (document.getElementById('RTxtMonto5')) {
                     txt5 = parseFloat(nvl(document.getElementById('RTxtMonto5').value.replace(',', ''), 0));
                 }





                 if (campo == 1 && numPagos == 2) {
                         cal = valorSdoNego - txt1;
                         document.getElementById("RTxtMonto2").value = cal;
                 }else if(campo == 2 && numPagos == 2) {
                     if (txt1 == 0) {
                         alert('Primero Captura El Monto 1');
                         document.getElementById("RTxtMonto2").value = 0
                     }
                 }else if (campo == 2 && numPagos == 3) {
                     if (txt1 == 0) {
                         alert('Captura EL Monto Anterior');
                         document.getElementById("RTxtMonto2").value = 0
                     } else {
                         cal = valorSdoNego - (txt1 + txt2);
                         document.getElementById("RTxtMonto3").value = cal;
                     }
                 } else if (campo == 3 && numPagos == 4) {
                     if (txt2 == 0 || txt3 == 0) {
                         alert('Captura Los Monto Anteriores');
                         document.getElementById("RTxtMonto3").value = cal;
                     } else {
                         cal = valorSdoNego - (txt1 + txt2 + txt3);
                         document.getElementById("RTxtMonto4").value = cal;
                     }
                 } else if (campo == 4 && numPagos == 5) {
                     cal = valorSdoNego - (txt1 + txt2 + txt3 + txt4);
                     document.getElementById("RTxtMonto5").value = cal;
                 } else if (campo == 5 && numPagos == 6) {
                     cal = valorSdoNego - (txt1 + txt2 + txt3 + txt4 + txt5);
                     document.getElementById("RTxtMonto6").value = cal;
                 } else if (campo == 6 && numPagos == 7) {
                     cal = valorSdoNego - (txt1 + txt2 + txt3 + txt4 + txt5 + txt6);
                     document.getElementById("RTxtMonto7").value = cal;
                 } else if (campo == 7 && numPagos == 8) {
                     cal = valorSdoNego - (txt1 + txt2 + txt3 + txt4 + txt5 + txt6 + txt7);
                     document.getElementById("RTxtMonto8").value = cal;
                 } else if (campo == 8 && numPagos == 9) {
                     cal = valorSdoNego - (txt1 + txt2 + txt3 + txt4 + txt5 + txt6 + txt7 + txt8);
                     document.getElementById("RTxtMonto9").value = cal;
                 } else if (campo == 9 && numPagos == 10) {
                     cal = valorSdoNego - (txt1 + txt2 + txt3 + txt4 + txt5 + txt6 + txt7 + txt8 + txt9);
                     document.getElementById("RTxtMonto10").value = cal;
                 }

             } catch (e) {
                 //alert(e.message);
             }
         }

         function nvl(value1, value2) {
             if (value1 == null || value1 == '')
                 return value2;

             return value1;
         }
            </script>
        </telerik:RadCodeBlock>

        <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPnlGeneral" runat="server">
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadAjaxPanel runat="server" ID="RPNLGral">


            <asp:Panel ID="PnlNeGociacion" runat="server" Visible="false">
                <!-- Título -->
                <div class="w3-container w3-blue w3-center">
                    <label>Negociaciones</label>
                </div>


                <!-- PnlDetalle -->
                <asp:Panel runat="server" CssClass="w3-container" ID="PnlDetalle" Visible="false">
                    <!-- Información financiera -->
                    <div class="w3-container">
                        <!-- Título -->
                        <div class="w3-container w3-blue w3-center">
                            <label>Información financiera</label>
                        </div>
                    </div>
                    <!-- Datos -->
                    <div class="w3-row-padding">
                        <div class="w3-col s2">
                            <label>Parcialidad</label>
                            <telerik:RadNumericTextBox ID="TxtPR_Parcialidad" runat="server" Type="Currency"  CssClass="w3-input" ReadOnly="True" Width="100%"></telerik:RadNumericTextBox>
                        </div>
                        <div class="w3-col s2">
                            <label>Total Aplicado</label>
                            <telerik:RadNumericTextBox ID="TxtPR_TotalAplicado" runat="server" Type="Currency" CssClass="w3-input" ReadOnly="True" Width="100%"></telerik:RadNumericTextBox>
                        </div>
                        <div class="w3-col s2">
                            <telerik:RadLabel runat="server" ID="LblPr_SaldoCapital" Text="Saldo Capital"/>
                            <telerik:RadNumericTextBox ID="TxtPR_SaldoCapital" runat="server" Type="Currency" CssClass="w3-input" ReadOnly="True" Width="100%"></telerik:RadNumericTextBox>
                        </div>
                        <div class="w3-col s2">
                            <label>Parcialidad Capital</label>
                            <telerik:RadNumericTextBox ID="TxtPR_ParcialidadCapital" runat="server" Type="Currency" CssClass="w3-input" ReadOnly="True" Width="100%"></telerik:RadNumericTextBox>
                        </div>
                        <div class="w3-col s2">
                            <label>Parcialidad Interes</label>
                            <telerik:RadNumericTextBox ID="TxtPR_ParcialidadInteres" Type="Currency" runat="server" CssClass="w3-input" MaxLength="7" ReadOnly="True" Width="100%"></telerik:RadNumericTextBox>
                        </div>
                        <div class="w3-col s2">
                            <label>Interes</label>
                            <telerik:RadNumericTextBox ID="TxtPR_Interes" runat="server" ReadOnly="True" CssClass="w3-input" Width="100%" Type="Currency" />
                        </div>
                    </div>

                    <div class="w3-row-padding">
                        <div class="w3-col s2">
                            <telerik:RadLabel runat="server" ID="LblPr_SaldoActual" Text="Saldo Actual"/>
                            <telerik:RadNumericTextBox ID="TxtPR_SaldoActual" runat="server" ReadOnly="True" CssClass="w3-input" Width="100%" Type="Currency" />
                        </div>
                        <div class="w3-col s2">
                            <label>Saldo Vencido</label>
                            <telerik:RadNumericTextBox ID="TxtPR_SaldoVencido" runat="server" ReadOnly="True" CssClass="w3-input" Width="100%" Type="Currency" />
                        </div>
                         <div class="w3-col s2">
                            <label>Credito IRR</label>
                            <telerik:RadTextBox ID="TxtVI_IRR" runat="server" ReadOnly="True" CssClass="w3-input" Width="100%" />
                        </div>
                        <div class="w3-col s2">
                            <label>Credito Sostenido</label>
                            <telerik:RadTextBox ID="TxtVI_SOSTENIDO" runat="server" ReadOnly="True" CssClass="w3-input" Width="100%" />
                        </div>
                        <div class="w3-col s2">
                            <label>Credito Terminado</label>
                            <telerik:RadTextBox ID="TxtVI_TERMINADO" runat="server" ReadOnly="True" CssClass="w3-input" Width="100%" />
                        </div>
                        <div class="w3-col s2">
                            <label>Avance del Credito</label>
                            <telerik:RadNumericTextBox ID="TxtVI_AVANCE_CREDITO" runat="server" ReadOnly="True" CssClass="w3-input" Width="100%" Type="Percent" />
                        </div>
                    </div>
                    
                    <div class="w3-row-padding">
                        <div class="w3-col s2">
                                <label>Capital</label>
                            <telerik:RadNumericTextBox ID="TxtPR_Capital" runat="server" ReadOnly="True" CssClass="w3-input" Width="100%" Type="Currency" />
                        </div>
                        <div class="w3-col s2">
                                <label>Capital Aplicado</label>
                            <telerik:RadNumericTextBox ID="TxtPr_CapitalAplicado" runat="server" ReadOnly="True" CssClass="w3-input" Width="100%" Type="Currency" />
                        </div>
                        <div class="w3-col s2">
                                <label>-</label>
                        </div>
                        <div class="w3-col s2">
                                <label>-</label>
                        </div>
                        <div class="w3-col s2">
                                <label>-</label>
                        </div>
                        <div class="w3-col s2">
                                <label>-</label>
                        </div>
                    </div>
                    <div class="w3-row-padding">
                        <div class="w3-col s2">
                            <label>Cuotas Vencidas</label>
                            <telerik:RadNumericTextBox ID="TxtVI_CUOTAS_VENCIDAS" runat="server" ReadOnly="True" CssClass="w3-input" Width="100%" Type="Number" />
                        </div>
                        <div class="w3-col s2">
                            <label>Clabe STP</label>
                            <telerik:RadTextBox ID="rTxtPR_referencia646" runat="server" ReadOnly="True" CssClass="w3-input" Width="100%" />
                        </div>
                        <div class="w3-col s2">
                            <label>Ref. Liquidacion BBVA</label>
                            <telerik:RadTextBox ID="rTxtPR_referencia012" runat="server" ReadOnly="True" CssClass="w3-input" Width="100%" />
                        </div>
                        <div class="w3-col s2">
                            <label>Ref. Liquidacion BANORTE</label>
                            <telerik:RadTextBox ID="rTxtPR_referencia072" runat="server" ReadOnly="True" CssClass="w3-input" Width="100%" />
                        </div>
                        <div class="w3-col s2">
                            <label>Ref. Liquidacion BANCO AZTECA</label>
                            <telerik:RadTextBox ID="rTxtPR_referencia127l" runat="server" ReadOnly="True" CssClass="w3-input" Width="100%" />
                        </div>
                        <div class="w3-col s2">
                            <label>Paynet</label>
                            <telerik:RadTextBox ID="rTxtPR_referenciapaynet" runat="server" ReadOnly="True" CssClass="w3-input" Width="100%" />
                        </div>
                    </div>
                     <div class="w3-row-padding">
                        <div class="w3-col s12">
                            <telerik:RadLabel runat="server" ID="LblMsjLlenar" Font-Size="X-Small"/>
                        </div>
                     </div>
                </asp:Panel>
                <br />
                <asp:Panel runat="server" CssClass="w3-container" ID="PanelInfoNego" Visible="false">
                    <!-- Información Negociacion -->
                    <div class="w3-container">
                        <!-- Título -->
                        <div class="w3-container w3-blue w3-center">
                            <label>Información Negociacion</label>
                        </div>
                    </div>
                    <!-- Datos -->
                    <div class="w3-row-padding">
                        <div class="w3-col s3">
                            <telerik:RadLabel runat="server" ID="LBlMontocampo1"></telerik:RadLabel>
                            <telerik:RadTextBox ID="TxtMontocampo1" runat="server" CssClass="w3-input" ReadOnly="True" Width="100%"></telerik:RadTextBox>
                        </div>
                        <div class="w3-col s3">
                            <telerik:RadLabel runat="server" ID="LBlMontocampo2"></telerik:RadLabel>
                            <telerik:RadTextBox ID="TxtMontocampo2" runat="server" CssClass="w3-input" ReadOnly="True" Width="100%"></telerik:RadTextBox>
                        </div>
                        <div class="w3-col s3">
                            <telerik:RadLabel runat="server" ID="LBlMontocampo3"></telerik:RadLabel>
                            <telerik:RadTextBox ID="TxtMontocampo3" runat="server" CssClass="w3-input" ReadOnly="True" Width="100%"></telerik:RadTextBox>
                        </div>
                        <div class="w3-col s3">
                            <label>Monto Calculado</label>
                            <telerik:RadNumericTextBox Type="Currency" ID="TxtMontoCalculado" runat="server" CssClass="w3-input" ReadOnly="True" Width="100%"></telerik:RadNumericTextBox>
                        </div>
                    </div>


                    <div class="w3-row-padding">
                        <div class="w3-col s12 l12">
                            <label>Regla</label>

                        </div>
                    </div>

                    <div class="w3-row-padding">
                        <div class="w3-col s12 l12">
                            <telerik:RadTextBox ID="TxtRegla" runat="server" ReadOnly="True" CssClass="w3-input" Width="100%" Type="Currency"></telerik:RadTextBox><br />
                            <telerik:RadLabel runat="server" ID="lblNivel" Visible="false"></telerik:RadLabel>
                        </div>
                    </div>
                </asp:Panel>
                <br />
                <asp:Panel runat="server" ID="PnlConfiguracion" CssClass="w3-container w3-row" Visible="false">
                    <!-- Negociación -->
                    <div class="w3-container w3-half">
                        <asp:Panel runat="server" ID="PnlPrev">
                            <div class="w3-container w3-half">
                                <div class="w3-container w3-blue w3-center">
                                    <label>Configuracion de la negociación</label>
                                </div>
                                <!-- Datos -->
                                <div class="w3-padding">
                                    <div class="w3-col m6">
                                        <label>Tipo de negociación:</label>
                                        <telerik:RadDropDownList ID="RddlTipoNego" runat="server" CssClass="w3-input" DataTextField="TEXTO" DataValueField="VALOR" Width="100%" SelectedText="DropDownListItem1" AutoPostBack="true">
                                            <Items>
                                                <telerik:DropDownListItem runat="server" Selected="True" Text="Seleccione" />
                                                <telerik:DropDownListItem runat="server" Text="Generico" />
                                                <%--<telerik:DropDownListItem runat="server" Text="Liquidacion" />--%>
                                                <%--<telerik:DropDownListItem runat="server" Text="Puesta al corriente" />--%>
                                            </Items>

                                        </telerik:RadDropDownList>
                                    </div>
                                    <div class="w3-col m6">
                                        <label>Número de pagos:</label>
                                        <telerik:RadDropDownList ID="RddlNumPagos" runat="server" CssClass="w3-input" Width="100%" SelectedText="DropDownListItem1" AutoPostBack="true">
                                            <%--   <Items>
                                                <telerik:DropDownListItem runat="server" Selected="True" Text="Seleccione" Value="0" />
                                                <telerik:DropDownListItem runat="server" Text="1" Value="1" />
                                                <telerik:DropDownListItem runat="server" Text="2" Value="2"/>
                                                <telerik:DropDownListItem runat="server" Text="3" Value="3"/>
                                                <telerik:DropDownListItem runat="server" Text="4" Value="4"/>
                                                <telerik:DropDownListItem runat="server" Text="5" Value="5"/>
                                                <telerik:DropDownListItem runat="server" Text="6" Value="6"/>
                                                <telerik:DropDownListItem runat="server" Text="7" Value="7"/>
                                                <telerik:DropDownListItem runat="server" Text="8" Value="8"/>
                                                <telerik:DropDownListItem runat="server" Text="9" Value="9"/>
                                                <telerik:DropDownListItem runat="server" Text="10" Value="10"/>
                                                <telerik:DropDownListItem runat="server" Text="11" Value="11"/>
                                                <telerik:DropDownListItem runat="server" Text="12" Value="12"/>
                                            </Items>--%>
                                        </telerik:RadDropDownList>
                                    </div>
                                </div>

                                <div class="w3-padding">
                                    <div class="w3-col m12">
                                        <label>Saldo a negociar:</label>
                                        <telerik:RadNumericTextBox runat="server" ID="TxtSdoNegociado" Type="Number" MaxLength="8" Enabled="false" />
                                    </div>
                                </div>
                                <div class="w3-padding">
                                    <div class="w3-padding">
                                        <div class="w3-col m6">
                                            <%-- <label>Pago minimo:</label>--%>
                                            <telerik:RadLabel ID="RlblSaldoMinimo" runat="server" ForeColor="Red" Visible="false"></telerik:RadLabel>
                                        </div>
                                        <div class="w3-col m6">
                                            <%--<label>Pago maximo:</label>--%>
                                            <telerik:RadLabel ID="RlblSaldoMaximo" runat="server" ForeColor="Green" Visible="false"></telerik:RadLabel>
                                        </div>
                                    </div>
                                    <div class="w3-row-padding">
                                        <div class="w3-col s12 l4">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td>
                                                        <telerik:RadLabel ID="LblMonto1" runat="server" Visible="False" Width="100">Monto 1</telerik:RadLabel>
                                                    </td>
                                                    <td>
                                                        <telerik:RadNumericTextBox onblur="calculaUltMonto(1);" runat="server" ID="RTxtMonto1" Visible="False" />
                                                    </td>
                                                    <td>
                                                        <telerik:RadLabel ID="LblFecha1" Width="50" runat="server" Visible="False">Fecha 1</telerik:RadLabel>
                                                    </td>
                                                    <td>
                                                        <telerik:RadDatePicker ID="RdFecha1" runat="server" CssClass="w3-input" Width="100" DateInput-DateFormat="dd/MM/yyyy" EnableTyping="false" Visible="False"></telerik:RadDatePicker>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <telerik:RadLabel ID="LblMonto2" runat="server" Visible="False" Width="100">Monto 2</telerik:RadLabel>
                                                    </td>
                                                    <td>
                                                        <telerik:RadNumericTextBox onblur="calculaUltMonto(2);" runat="server" ID="RTxtMonto2" Visible="False"></telerik:RadNumericTextBox>
                                                    </td>
                                                    <td>
                                                        <telerik:RadLabel ID="LblFecha2" runat="server" Visible="False">Fecha 2</telerik:RadLabel>
                                                    </td>
                                                    <td>
                                                        <telerik:RadDatePicker ID="RdFecha2" runat="server" CssClass="w3-input" DateInput-DateFormat="dd/MM/yyyy" Width="100" EnableTyping="false" Visible="false"></telerik:RadDatePicker>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <telerik:RadLabel ID="LblMonto3" runat="server" Visible="False" Width="100">Monto 3</telerik:RadLabel>
                                                    </td>
                                                    <td>
                                                        <telerik:RadNumericTextBox onblur="calculaUltMonto(3);" runat="server" ID="RTxtMonto3" Visible="False"></telerik:RadNumericTextBox>
                                                    </td>
                                                    <td>
                                                        <telerik:RadLabel ID="LblFecha3" runat="server" Visible="false">Fecha 3</telerik:RadLabel>
                                                    </td>
                                                    <td>
                                                        <telerik:RadDatePicker ID="RdFecha3" runat="server" CssClass="w3-input" DateInput-DateFormat="dd/MM/yyyy" Width="100" EnableTyping="false" Visible="False"></telerik:RadDatePicker>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <telerik:RadLabel ID="LblMonto4" runat="server" Visible="False" Width="100">Monto 4</telerik:RadLabel>
                                                    </td>
                                                    <td>
                                                        <telerik:RadNumericTextBox onblur="calculaUltMonto(4);" runat="server" ID="RTxtMonto4" Visible="False"></telerik:RadNumericTextBox>
                                                    </td>
                                                    <td>
                                                        <telerik:RadLabel ID="LblFecha4" runat="server" Visible="False">Fecha 4</telerik:RadLabel>
                                                    </td>
                                                    <td>
                                                        <telerik:RadDatePicker ID="RdFecha4" runat="server" CssClass="w3-input" DateInput-DateFormat="dd/MM/yyyy" Width="100" EnableTyping="false" Visible="False"></telerik:RadDatePicker>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <telerik:RadLabel ID="LblMonto5" runat="server" Visible="False" Width="100">Monto 5</telerik:RadLabel>
                                                    </td>
                                                    <td>
                                                        <telerik:RadNumericTextBox onblur="calculaUltMonto(5);" runat="server" ID="RTxtMonto5" Visible="False"></telerik:RadNumericTextBox>
                                                    </td>
                                                    <td>
                                                        <telerik:RadLabel ID="LblFecha5" runat="server" Visible="False">Fecha 5</telerik:RadLabel>
                                                    </td>
                                                    <td>
                                                        <telerik:RadDatePicker ID="RdFecha5" runat="server" CssClass="w3-input" DateInput-DateFormat="dd/MM/yyyy" Width="100" EnableTyping="false" Visible="False"></telerik:RadDatePicker>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>
                                                        <telerik:RadLabel ID="LblMonto6" runat="server" Visible="False" Width="100">Monto 6</telerik:RadLabel>
                                                    </td>
                                                    <td>
                                                        <telerik:RadNumericTextBox onblur="calculaUltMonto(6);" runat="server" ID="RTxtMonto6" Visible="False"></telerik:RadNumericTextBox>
                                                    </td>
                                                    <td>
                                                        <telerik:RadLabel ID="LblFecha6" runat="server" Visible="False">Fecha 6</telerik:RadLabel>
                                                    </td>
                                                    <td>
                                                        <telerik:RadDatePicker ID="RdFecha6" runat="server" CssClass="w3-input" DateInput-DateFormat="dd/MM/yyyy" Width="100" EnableTyping="false" Visible="False"></telerik:RadDatePicker>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <telerik:RadLabel ID="LblMonto7" runat="server" Visible="False" Width="100">Monto 7</telerik:RadLabel>
                                                    </td>
                                                    <td>
                                                        <telerik:RadNumericTextBox onblur="calculaUltMonto(7);" runat="server" ID="RTxtMonto7" Visible="False"></telerik:RadNumericTextBox>
                                                    </td>
                                                    <td>
                                                        <telerik:RadLabel ID="LblFecha7" runat="server" Visible="False">Fecha 7</telerik:RadLabel>
                                                    </td>
                                                    <td>
                                                        <telerik:RadDatePicker ID="RdFecha7" runat="server" CssClass="w3-input" DateInput-DateFormat="dd/MM/yyyy" Width="100" EnableTyping="false" Visible="False"></telerik:RadDatePicker>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <telerik:RadLabel ID="LblMonto8" runat="server" Visible="False" Width="100">Monto 8</telerik:RadLabel>
                                                    </td>
                                                    <td>
                                                        <telerik:RadNumericTextBox onblur="calculaUltMonto(8);" runat="server" ID="RTxtMonto8" Visible="False"></telerik:RadNumericTextBox>
                                                    </td>
                                                    <td>
                                                        <telerik:RadLabel ID="LblFecha8" runat="server" Visible="False">Fecha 8</telerik:RadLabel>
                                                    </td>
                                                    <td>
                                                        <telerik:RadDatePicker ID="RdFecha8" runat="server" CssClass="w3-input" DateInput-DateFormat="dd/MM/yyyy" Width="100" EnableTyping="false" Visible="False"></telerik:RadDatePicker>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>
                                                        <telerik:RadLabel ID="LblMonto9" runat="server" Visible="False" Width="100">Monto 9</telerik:RadLabel>
                                                    </td>
                                                    <td>
                                                        <telerik:RadNumericTextBox onblur="calculaUltMonto(9);" runat="server" ID="RTxtMonto9" Visible="False"></telerik:RadNumericTextBox>
                                                    </td>
                                                    <td>
                                                        <telerik:RadLabel ID="LblFecha9" runat="server" Visible="False">Fecha 9</telerik:RadLabel>
                                                    </td>
                                                    <td>
                                                        <telerik:RadDatePicker ID="RdFecha9" runat="server" CssClass="w3-input" DateInput-DateFormat="dd/MM/yyyy" Width="100" EnableTyping="false" Visible="False"></telerik:RadDatePicker>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <telerik:RadLabel ID="LblMonto10" runat="server" Visible="False" Width="100">Monto 10</telerik:RadLabel>
                                                    </td>
                                                    <td>
                                                        <telerik:RadNumericTextBox onblur="calculaUltMonto(10);" runat="server" ID="RTxtMonto10" Visible="False"></telerik:RadNumericTextBox>
                                                    </td>
                                                    <td>
                                                        <telerik:RadLabel ID="LblFecha10" runat="server" Visible="False">Fecha 10</telerik:RadLabel>
                                                    </td>
                                                    <td>
                                                        <telerik:RadDatePicker ID="RdFecha10" runat="server" CssClass="w3-input" DateInput-DateFormat="dd/MM/yyyy" Width="100" EnableTyping="false" Visible="False"></telerik:RadDatePicker>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <telerik:RadLabel ID="LblMonto11" runat="server" Visible="False" Width="100">Monto 11</telerik:RadLabel>
                                                    </td>
                                                    <td>
                                                        <telerik:RadNumericTextBox runat="server" ID="RTxtMonto11" Visible="False"></telerik:RadNumericTextBox>
                                                    </td>
                                                    <td>
                                                        <telerik:RadLabel ID="LblFecha11" runat="server" Visible="False">Fecha 11</telerik:RadLabel>
                                                    </td>
                                                    <td>
                                                        <telerik:RadDatePicker ID="RdFecha11" runat="server" CssClass="w3-input" DateInput-DateFormat="dd/MM/yyyy" Width="100" EnableTyping="false" Visible="False"></telerik:RadDatePicker>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>
                                                        <telerik:RadLabel ID="LblMonto12" runat="server" Visible="False" Width="100">Monto 12</telerik:RadLabel>
                                                    </td>
                                                    <td>
                                                        <telerik:RadNumericTextBox runat="server" ID="RTxtMonto12" Visible="False"></telerik:RadNumericTextBox>
                                                    </td>
                                                    <td>
                                                        <telerik:RadLabel ID="LblFecha12" runat="server" Visible="False">Fecha 12</telerik:RadLabel>
                                                    </td>
                                                    <td>
                                                        <telerik:RadDatePicker ID="RdFecha12" runat="server" CssClass="w3-input" DateInput-DateFormat="dd/MM/yyyy" Width="100" EnableTyping="false" Visible="False"></telerik:RadDatePicker>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4"></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                        <div class="w3-container w3-half">
                            <div class="w3-container w3-blue w3-center">
                                <label>Confirmacion negociación</label>
                            </div>
                            <div class="w3-row">

                                <div class="w3-col s12">
                                    <telerik:RadButton ID="BtnVisualizar" Width="100%" runat="server" Text="Confirmar"></telerik:RadButton>
                                </div>

                                <div class="w3-col s12">
                                    <telerik:RadButton ID="BtnCancelarVis" runat="server" Width="100%" Text="Cancelar" Visible="false"></telerik:RadButton>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="w3-row" style="overflow: auto">
                        <telerik:RadGrid ID="GvCalendario" runat="server" CssClass="mGrid" Font-Names="Tahoma" Visible="false" Font-Size="Small" Width="200PX">
                        </telerik:RadGrid>
                    </div>


                    <!-- Captura de gestión -->
                    <div class="w3-container w3-rest">
                        <!-- Título -->
                        <div class="w3-container w3-blue w3-center">
                            <label>Captura de gestión</label>
                        </div>
                        <!-- Datos -->
                        <div class="w3-container">
                            <div class="w3-row-padding">
                                <div class="w3-col s12 l4">
                                    <label>Acción:</label>
                                    <telerik:RadDropDownList ID="DdlHist_Ge_Accion" runat="server" Enabled="false" AutoPostBack="true" CssClass="w3-select" Width="100%"></telerik:RadDropDownList>
                                </div>
                                <div class="w3-col s12 l4">
                                    <label>Resultado:</label>
                                    <telerik:RadDropDownList ID="DdlHist_Ge_Resultado" runat="server" AutoPostBack="true" CssClass="w3-select" Width="100%" Enabled="false"></telerik:RadDropDownList>
                                </div>
                                <%-- <div class="w3-col s12 l4">
                                    <label>No Pago:</label>
                                    <telerik:RadDropDownList ID="DdlHist_Ge_NoPago" runat="server" CssClass="w3-input" Width="100%"></telerik:RadDropDownList>
                                </div>--%>
                            </div>
                            <div class="w3-row-padding">
                                <div class="w3-col l6 s12">
                                    <div class="w3-row">
                                        <label>Fecha de seguimiento:</label>
                                        <telerik:RadDateTimePicker ID="TxtFechaSeguimiento" runat="server" CssClass="w3-input" DateInput-DateFormat="dd MMM hh:mm tt" Width="100%" EnableTyping="false"></telerik:RadDateTimePicker>
                                    </div>
                                    <div class="w3-row">

                                        <telerik:RadTextBox Label="Supervisor" ID="TxtHist_Pr_SupervisorAuto" runat="server" MaxLength="25" Width="100%"></telerik:RadTextBox>
                                    </div>
                                    <div class="w3-row">
                                        <telerik:RadTextBox Label="Contraseña" ID="TxtContrasenaAuto" runat="server" MaxLength="20" TextMode="Password" Width="100%"></telerik:RadTextBox>
                                    </div>
                                </div>
                                <div class="w3-col s12 l6">
                                    <label>Comentario:</label>
                                    <telerik:RadTextBox ID="TxtHist_Ge_Comentario" runat="server" MaxLength="500" TextMode="MultiLine" Width="100%" CssClass="w3-input" Style="min-height: 150px;" Resize="Vertical"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="w3-row w3-padding w3-center">
                                <telerik:RadButton ID="BtnGuardar" runat="server" Text="Guardar negociación" Visible="false" />
                            </div>
                        </div>
                    </div>
                    <telerik:RadNotification ID="Notificacion2" runat="server" Position="Center" Width="330" Height="160" Animation="Fade" EnableRoundedCorners="true" EnableShadow="true" Style="z-index: 100000">
                    </telerik:RadNotification>
                </asp:Panel>
            </asp:Panel>
            <asp:Panel ID="PnlNegoVigente" runat="server">
                <div class="w3-row-padding">
                    <!-- Promesa -->
                    <div class="w3-half">
                        <!-- Título -->
                        <div class="w3-row w3-blue w3-center">
                            <telerik:RadLabel ID="LblPromesa" runat="server"></telerik:RadLabel>
                        </div>
                        <!-- Datos -->
                        <div class="w3-container">
                            <div class="w3-row-padding">
                                <div class="w3-half">
                                    <label>Supervisor:</label>
                                    <telerik:RadTextBox ID="TxtHist_Pr_Supervisor" runat="server" MaxLength="25" CssClass="w3-input" Width="100%" AutoCompleteType="None"></telerik:RadTextBox>
                                </div>
                                <div class="w3-half">
                                    <label>Contraseña:</label>
                                    <telerik:RadTextBox ID="TxtContrasena" runat="server" MaxLength="10" TextMode="Password" Width="100%"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="w3-row">
                                <div class="w3-col">
                                    <label>Motivo:</label>
                                    <telerik:RadTextBox ID="TxtHist_Pr_Motivo" runat="server" MaxLength="200" Width="100%" TextMode="MultiLine" CssClass="w3-input" Resize="Vertical" Style="min-height: 100px"></telerik:RadTextBox>
                                    <asp:RegularExpressionValidator runat="server" ControlToValidate="TxtHist_Pr_Motivo" ValidationExpression="[abcdefghijklmnopqrstuvwxyzñ@1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZÑ .]*" ErrorMessage="Carácteres inválidos." Display="Dynamic" CssClass="w3-text-red"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <br />
                            <div class="w3-row">
                                <div class="w3-col">
                                    <telerik:RadButton ID="BtnAceptarPromesa" runat="server" Text="Aceptar" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="w3-half">
                        <!-- Título -->
                        <div class="w3-row w3-blue w3-center">
                            <label>Calendario de promesas</label>
                        </div>
                        <!-- Datos -->
                        <div class="w3-row">
                            <div class="w3-col" style="overflow: auto">
                                <telerik:RadGrid ID="GvCalendarioVig" runat="server" CssClass="w3-table" Font-Names="Tahoma" Font-Size="Small" Width="100%"></telerik:RadGrid>
                            </div>
                        </div>
                    </div>
                    <telerik:RadNotification ID="Notificacion" runat="server" Position="Center" Width="330" Height="160" Animation="Fade" EnableRoundedCorners="true" EnableShadow="true" Style="z-index: 100000">
                    </telerik:RadNotification>
                </div>
            </asp:Panel>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
