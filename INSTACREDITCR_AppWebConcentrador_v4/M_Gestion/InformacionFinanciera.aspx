<%@ Page Language="VB" AutoEventWireup="false" CodeFile="InformacionFinanciera.aspx.vb" Inherits="InformacionFinanciera" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mc :: Modulo Gestion</title>
    
    <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
    
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
        <telerik:RadLabel ID="LblCat_Lo_Usuario" runat="server" Visible="false"></telerik:RadLabel>
        <!-- Informaci�n financiera -->
        <div class="w3-container w3-blue w3-center">
            <b>Información Financiera</b>
        </div>
        <div class="w3-row-padding">
            <div class="w3-col s12 l6">
                <div class="w3-col s12 m4">
                    <label>Tasa Base Anualizada</label>
                    <telerik:RadLabel ID="TxtPR_BI_TASABASE" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4 w3-padding-small">
                    <label>Sobre Tasa</label>
                    <telerik:RadLabel ID="TxtPR_BI_SOBRETASA" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4">
                    <label>Piso Tasa</label>
                    <telerik:RadLabel ID="TxtPR_BI_PISOTASA" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
            </div>
            <div class="w3-col s12 l6">
                <div class="w3-col s12 m4">
                    <label>Techo Tasa</label>
                    <telerik:RadLabel ID="TxtPR_BI_TECHOTASA" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4 w3-padding-small">
                    <label>Tipo de Cobro Moratorio</label>
                    <telerik:RadLabel ID="TxtPR_BI_TIPCOBCOMMORATO" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4">
                    <label>Tasa Moratoria</label>
                    <telerik:RadLabel ID="TxtPR_BI_FACTORMORA" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
            </div>
        </div>
        <div class="w3-row-padding w3-margin-top">
            <div class="w3-col s12 l6">
                <div class="w3-col s12 m4">
                    <label>Saldo de Capital Vencido no Exigible</label>
                    <telerik:RadLabel ID="TxtPR_BI_SALCAPVENNOEXI" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4 w3-padding-small">
                    <label>Saldo de Interes Ordinario</label>
                    <telerik:RadLabel ID="TxtPR_BI_SALINTORDINARIO" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4">
                    <label>Saldo de Interes Atrasado</label>
                    <telerik:RadLabel ID="TxtPR_BI_SALINTATRASADO" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
            </div>
            <div class="w3-col s12 l6">
                <div class="w3-col s12 m4">
                    <label>Saldo de Interes Vencido</label>
                    <telerik:RadLabel ID="TxtPR_BI_SALINTVENCIDO" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4 w3-padding-small">
                    <label>Saldo de Provision</label>
                    <telerik:RadLabel ID="TxtPR_BI_SALINTPROVISION" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4">
                    <label>Saldo de Interés no Contabilizado</label>
                    <telerik:RadLabel ID="TxtPR_BI_SALINTNOCONTA" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
            </div>
        </div>
        <div class="w3-row-padding w3-margin-top">
            <div class="w3-col s12 l6">
                <div class="w3-col s12 m4">
                    <label>Saldo Moratorios</label>
                    <telerik:RadLabel ID="TxtPR_BI_SALMORATORIOS" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4 w3-padding-small">
                    <label>Saldo de Interes Moratorio en atraso</label>
                    <telerik:RadLabel ID="TxtPR_BI_SALDOMORAVENCIDO" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4">
                    <label>Saldo de Moratorios (Cartera Vencida)</label>
                    <telerik:RadLabel ID="TxtPR_BI_SALDOMORACARVEN" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
            </div>
            <div class="w3-col s12 l6">
                <div class="w3-col s12 m4">
                    <label>Saldo Comision Falta Pago</label>
                    <telerik:RadLabel ID="TxtPR_BI_SALCOMFALTAPAGO" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4 w3-padding-small">
                    <label>Saldo Comision Otras Comisiones</label>
                    <telerik:RadLabel ID="TxtPR_BI_SALOTRASCOMISI" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4">
                    <label>Saldo Iva Interés</label>
                    <telerik:RadLabel ID="TxtPR_BI_SALIVAINTERES" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
            </div>
        </div>
        <div class="w3-row-padding w3-margin-top">
            <div class="w3-col s12 l6">
                <div class="w3-col s12 m4">
                    <label>Saldo Iva Moratorios</label>
                    <telerik:RadLabel ID="TxtPR_BI_SALIVAMORATORIOS" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4 w3-padding-smallm4">
                    <label>Saldo Iva Comision Falta Pago</label>
                    <telerik:RadLabel ID="TxtPR_BI_SALIVACOMFALPAGO" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4">
                    <label>Saldo Iva Otras Comisiones</label>
                    <telerik:RadLabel ID="TxtPR_BI_SALIVACOMISI" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
            </div>
            <div class="w3-col s12 l6">
                <div class="w3-col s12 m4">
                    <label>Monto Capital que paso a Atrasado</label>
                    <telerik:RadLabel ID="TxtPR_BI_PASOCAPATRADIA" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4 w3-padding-small">
                    <label>Monto Capital que paso a Vencido</label>
                    <telerik:RadLabel ID="TxtPR_BI_PASOCAPVENDIA" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4">
                    <label>Monto Capital que paso a VNE</label>
                    <telerik:RadLabel ID="TxtPR_BI_PASOCAPVNEDIA" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
            </div>
        </div>
        <div class="w3-row-padding w3-margin-top">
            <div class="w3-col s12 l6">
                <div class="w3-col s12 m4">
                    <label>Monto Interes que paso a Atrasado</label>
                    <telerik:RadLabel ID="TxtPR_BI_PASOINTATRADIA" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4 w3-padding-small">
                    <label>Monto Interes que paso a Vencido</label>
                    <telerik:RadLabel ID="TxtPR_BI_PASOINTVENDIA" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4">
                    <label>Monto de interes Ordinario Devengado</label>
                    <telerik:RadLabel ID="TxtPR_BI_INTORDDEVENGADO" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
            </div>
            <div class="w3-col s12 l6">
                <div class="w3-col s12 m4">
                    <label>Monto de interes Moratorio Devengado</label>
                    <telerik:RadLabel ID="TxtPR_BI_INTMORDEVENGADO" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4 w3-padding-small">
                    <label>Monto de Comision por Falta de Pago</label>
                    <telerik:RadLabel ID="TxtPR_BI_COMISIDEVENGADO" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4">
                    <label>Pagos de Capital Vigente del Dia</label>
                    <telerik:RadLabel ID="TxtPR_BI_PAGOCAPVIGDIA" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
            </div>
        </div>
        <div class="w3-row-padding w3-margin-top">
            <div class="w3-col s12 l6">
                <div class="w3-col s12 m4">
                    <label>Pagos de Capital Atrasado del Dia</label>
                    <telerik:RadLabel ID="TxtPR_BI_PAGOCAPATRDIA" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4 w3-padding-small">
                    <label>Pagos de Capital Vencido del Dia</label>
                    <telerik:RadLabel ID="TxtPR_BI_PAGOCAPVENDIA" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4">
                    <label>Pagos de Capital VNE del Dia</label>
                    <telerik:RadLabel ID="TxtPR_BI_PAGOCAPVENNEXDIA" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
            </div>
            <div class="w3-col s12 l6">
                <div class="w3-col s12 m4">
                    <label>Pagos de Interes Ordinario del Dia</label>
                    <telerik:RadLabel ID="TxtPR_BI_PAGOINTORDDIA" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4 w3-padding-small">
                    <label>Pagos de Interes Vencido del Dia</label>
                    <telerik:RadLabel ID="TxtPR_BI_PAGOINTVENDIA" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4">
                    <label>Pagos de Interes Atrasado del Dia</label>
                    <telerik:RadLabel ID="TxtPR_BI_PAGOINTATRDIA" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
            </div>
        </div>
        <div class="w3-row-padding w3-margin-top">
            <div class="w3-col s12 l6">
                <div class="w3-col s12 m4">
                    <label>Pagos de Interes No Contabilizado</label>
                    <telerik:RadLabel ID="TxtPR_BI_PAGOINTCALNOCON" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4 w3-padding-small">
                    <label>Pagos de Interes No Comisiones</label>
                    <telerik:RadLabel ID="TxtPR_BI_PAGOCOMISIDIA" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4">
                    <label>Pagos de Moratorio del Dia</label>
                    <telerik:RadLabel ID="TxtPR_BI_PAGOMORATORIOS" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
            </div>
            <div class="w3-col s12 l6">
                <div class="w3-col s12 m4">
                    <label>Pagos de IVAS del Dia</label>
                    <telerik:RadLabel ID="TxtPR_BI_PAGOIVADIA" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4 w3-padding-small">
                    <label>Interes Codonado Del Dia</label>
                    <telerik:RadLabel ID="TxtPR_BI_INTCONDONADODIA" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4">
                    <label>Moratorios Condonados Del Dia</label>
                    <telerik:RadLabel ID="TxtPR_BI_MORCONDONADODIA" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
            </div>
        </div>
        <div class="w3-row-padding w3-margin-top">
            <div class="w3-col s12 l6">
                <div class="w3-col s12 m4">
                    <label>Numero de Dias de Mora</label>
                    <telerik:RadLabel ID="TxtPR_BI_DIASATRASO" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4 w3-padding-small">
                    <label>Numero de Cuotas</label>
                    <telerik:RadLabel ID="TxtPR_BI_NOCUOTASATRASO" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4">
                    <label>Numero de Maximo Dias de atraso</label>
                    <telerik:RadLabel ID="TxtPR_BI_MAXIMODIASATRASO" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
            </div>
            <div class="w3-col s12 l6">
                <div class="w3-col s12 m4">
                    <label>Fecha de Inicio del Credito</label>
                    <telerik:RadLabel ID="TxtPR_BI_FECHAINICIO" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
               <div class="w3-col s12 m4 w3-padding-small">
                    <label>Comision Condonado en el Dia</label>
                    <telerik:RadLabel ID="TxtPR_BI_COMCONDONADODIA" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4">
                    <label>Saldo Promedio del credito</label>
                    <telerik:RadLabel ID="TxtPR_BI_SALDOPROMEDIO" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
            </div>
        </div>
        <div class="w3-row-padding w3-margin-top">
            <div class="w3-col s12 l6">
                <div class="w3-col s12 m4">
                    <label>Interes Devengado</label>
                    <telerik:RadLabel ID="TxtPR_BI_INTDEVCTAORDEN" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4 w3-padding-small">
                    <label>Periodo de dias de Cuotas de Capital</label>
                    <telerik:RadLabel ID="TxtPR_BI_PERIODICIDADCAP" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4">
                    <label>Comision por Administración</label>
                    <telerik:RadLabel ID="TxtPR_BI_COMADMONPAGDIA" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
            </div>
            <div class="w3-col s12 l6">
                <div class="w3-col s12 m4">
                    <label>Periodo de dias de Cuotas de Interes</label>
                    <telerik:RadLabel ID="TxtPR_BI_PERIODICIDADINT" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4 w3-padding-small">
                    <label>Tipo de Pago de Capital</label>
                    <telerik:RadLabel ID="TxtPR_BI_TIPOPAGOCAPITAL" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4">
                    <label>Numero de Amortizaciones</label>
                    <telerik:RadLabel ID="TxtPR_BI_NUMAMORTIZACION" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
            </div>
        </div>
        <div class="w3-row-padding w3-margin-top">
            <div class="w3-col s12 l6">
                <div class="w3-col s12 m4 ">
                    <label>Capital Condonado del Dia</label>
                    <telerik:RadLabel ID="TxtPR_BI_CAPCONDONADODIA" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4 w3-padding-small">
                    <label>Frec. Pago de Amortizaciones de Capital</label>
                    <telerik:RadLabel ID="TxtPR_BI_FRECUENCIACAP" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
                
                 <div class="w3-col s12 m4">
                    <label>Frec. Pago de Amortizaciones de Interes</label>
                    <telerik:RadLabel ID="TxtPR_BI_FRECUENCIAINT" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
            </div>
            <div class="w3-col s12 l6">
                 <div class="w3-col s12 m4 w3-padding-small">
                    <label>Inicio de amortizaciones y cobro de intereses</label>
                    <telerik:RadLabel ID="TxtPR_BI_FECHAINICIOAMOR" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
                
            </div>
        </div>

                <!-- Informaci�n de agencia -->
        <div class="w3-container w3-blue w3-center w3-margin-top">
            <b>Información de agencia</b>
        </div>
        <div class="w3-row-padding">
            <div class="w3-col s12 l6">
                <div class="w3-col s12 m4">
                    <label>Numero de Referencia Generado</label>
                    <telerik:RadLabel ID="TxtPR_BI_REFERENCIA" runat="server" CssClass="w3-border w3-round" Width="100%">
                    </telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4 w3-padding-small">
                    <label>ID Plazo / Duración del Crédito</label>
                    <telerik:RadLabel ID="TxtPR_BI_PLAZOID" runat="server" CssClass="w3-border w3-round" Width="100%">
                    </telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4">
                    <label>Tipo de Cartera</label>
                    <telerik:RadLabel ID="TxtPR_BI_TIPOCARTERA" runat="server" CssClass="w3-border w3-round" Width="100%">
                    </telerik:RadLabel>
                </div>
            </div>
            <div class="w3-col s12 l6">
                <div class="w3-col s12 m4">
                    <label>Garantía Prendria</label>
                    <telerik:RadLabel ID="TxtPR_BI_CONGARPRENDA" runat="server" CssClass="w3-border w3-round" Width="100%">
                    </telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4 w3-padding-small">
                    <label>Garantía Liquida</label>
                    <telerik:RadLabel ID="TxtPR_BI_CONGARLIQ" runat="server" CssClass="w3-border w3-round" Width="100%">
                    </telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4">
                    <label>ID del tipo de moneda del crédito</label>
                    <telerik:RadLabel ID="TxtPR_BI_MONEDAIDCREDITO" runat="server" CssClass="w3-border w3-round" Width="100%">
                    </telerik:RadLabel>
                </div>
            </div>
        </div>
        <div class="w3-row-padding w3-margin-top">
            <div class="w3-col s12 l6">
                <div class="w3-col s12 m4">
                    <label>Descripcion de la moneda</label>
                    <telerik:RadLabel ID="TxtPR_BI_DESCRICORTAMONEDA" runat="server" CssClass="w3-border w3-round" Width="100%">
                    </telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4 w3-padding-small">
                    <label>ID de linea de Credito</label>
                    <telerik:RadLabel ID="TxtPR_BI_LINEACREDITOID" runat="server" CssClass="w3-border w3-round" Width="100%">
                    </telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4">
                    <label>ID de moneda</label>
                    <telerik:RadLabel ID="TxtPR_BI_MONEDAID" runat="server" CssClass="w3-border w3-round" Width="100%">
                    </telerik:RadLabel>
                </div>
            </div>
            <div class="w3-col s12 l6">
                <div class="w3-col s12 m4">
                    <label>Calendario Irregular</label>
                    <telerik:RadLabel ID="TxtPR_BI_CALENDIRREGULAR" runat="server" CssClass="w3-border w3-round" Width="100%">
                    </telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4 w3-padding-small">
                    <label>Comportamiento</label>
                    <telerik:RadLabel ID="TxtPR_BI_FECHAINHABIL" runat="server" CssClass="w3-border w3-round" Width="100%">
                    </telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4">
                    <label>Generación de Intereses</label>
                    <telerik:RadLabel ID="TxtPR_BI_DIAPAGOINTERES" runat="server" CssClass="w3-border w3-round" Width="100%">
                    </telerik:RadLabel>
                </div>
            </div>
        </div>
        <div class="w3-row-padding w3-margin-top">
            <div class="w3-col s12 l6">
                <div class="w3-col s12 m4">
                    <label>Generación de Capitales</label>
                    <telerik:RadLabel ID="TxtPR_BI_DIAPAGOCAPITAL" runat="server" CssClass="w3-border w3-round" Width="100%">
                    </telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4 w3-padding-small">
                    <label>Frecuencia Mensual</label>
                    <telerik:RadLabel ID="TxtPR_BI_DIAPAGOPROD" runat="server" CssClass="w3-border w3-round" Width="100%">
                    </telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4">
                    <label>Fecha de Traspaso a Vencido</label>
                    <telerik:RadLabel ID="TxtPR_BI_FECHTRASPASVENC" runat="server" CssClass="w3-border w3-round"
                        Width="100%">
                    </telerik:RadLabel>
                </div>
            </div>
            <div class="w3-col s12 l6">
                <div class="w3-col s12 m4">
                    <label>Fecha de autorizacion del credito</label>
                    <telerik:RadLabel ID="TxtPR_BI_FECHAUTORIZA" runat="server" CssClass="w3-border w3-round" Width="100%">
                    </telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4 w3-padding-small">
                    <label>Destino</label>
                    <telerik:RadLabel ID="TxtPR_BI_DESTINOCREID" runat="server" CssClass="w3-border w3-round"
                        Width="100%">
                    </telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4">
                    <label>Calificacion de Cartera</label>
                    <telerik:RadLabel ID="TxtPR_BI_CALIFICACION" runat="server" CssClass="w3-border w3-round" Width="100%">
                    </telerik:RadLabel>
                </div>
            </div>
        </div>
        <div class="w3-row-padding w3-margin-top">
            <div class="w3-col s12 l6">
                <div class="w3-col s12 m4">
                    <label>Porcentaje de Reserva</label>
                    <telerik:RadLabel ID="TxtPR_BI_PORCRESERVA" runat="server" CssClass="w3-border w3-round" Width="100%">
                    </telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4 w3-padding-small">
                    <label>Tipo de Fondeo</label>
                    <telerik:RadLabel ID="TxtPR_BI_TIPOFONDEO" runat="server" CssClass="w3-border w3-round"
                        Width="100%">
                    </telerik:RadLabel>
                </div>
                <div class="w3-col s12 m4">
                    <label>Institución de Fondeo</label>
                    <telerik:RadLabel ID="TxtPR_BI_INSTITFONDEOID" runat="server" CssClass="w3-border w3-round" Width="100%">
                    </telerik:RadLabel>
                </div>
            </div>
            <div class="w3-col s12 l6">
                <div class="w3-col s12 m4">
                    <label>Monto Retirado en Ventanilla</label>
                    <telerik:RadLabel ID="TxtPR_BI_DESEMBOLSOSDIA" runat="server" CssClass="w3-border w3-round" Width="100%">
                    </telerik:RadLabel>
                </div>
            </div>
        </div>
        
    </form>
    <script src="JS/iFrameMejorado.js?v=2" type="text/javascript"></script>
    <link rel="stylesheet" href="Estilos/w3.css" />
</body>
</html>
