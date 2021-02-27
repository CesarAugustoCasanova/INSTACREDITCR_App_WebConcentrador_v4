<%@ Page Title="" Language="VB" MasterPageFile="./MasterPage.master" AutoEventWireup="false" CodeFile="CargaCartera.aspx.vb" Inherits="CargaCartera" %>


<asp:Content ID="Content1" ContentPlaceHolderID="CPHMaster" runat="Server">

    <telerik:RadAjaxPanel ID="UPNLGeneral" runat="server">
        <div class="Titulos">Carga Cartera</div>
        <div class="Lista">
            <label>Producto:</label>
            <telerik:RadDropDownList runat="server" ID="DDLProducto" DefaultMessage="Seleccione" AutoPostBack="true">
                    
                </telerik:RadDropDownList>
        </div>
        
            <div class="container">
                
                <div class="w-100">
                    Layout para carga de cartera. El archivo debe ser CSV o TXT con encabezados.
                </div>
                
                <div class="table-responsive">
                    <asp:panel runat="server" ID="PnlCredifiel" Visible="true">
                    <table class="table">
                        <tr>
                            <th>Campo</th>
                            <td>IdCredito</td>
                            <td>Nombre</td>
                            <td>RFC</td>
                            <td>Genero</td>
                            <td>Edad</td>
                            <td>Domicilio</td>
                            <td>Ciudad</td>
                            <td>TelCelular</td>
                            <td>TelDomicilio</td>
                            <td>TelEmpleo</td>
                            <td>TelReferencia1</td>
                            <td>TelReferencia2</td>
                            <td>CorreoElectronico</td>
                            <td>IdCliente</td>
                            <td>IdSolicitud</td>
                            <td>DAP</td>
                            <td>idempleado</td>
                            <td>FechaUltStatusSolicitud</td>
                            <td>EstatusDispersion</td>
                            <td>Producto</td>
                            <td>DescuentoOrigen</td>
                            <td>FondeadorActual</td>
                            <td>Capital</td>
                            <td>MontoDispersado</td>
                            <td>Interes</td>
                            <td>IVA</td>
                            <td>Pagare</td>
                            <td>NumeroPagos</td>
                            <td>Parcialidad</td>
                            <td>Periodicidad</td>
                            <td>DependenciaActual</td>
                            <td>DependenciaHomologado</td>
                            <td>TotalAplicado</td>
                            <td>MontoEnTransito</td>
                            <td>PagosEnTransito</td>
                            <td>SaldoCapital</td>
                            <td>SaldoInteres</td>
                            <td>SaldoIVA</td>
                            <td>SaldoActual</td>
                            <td>CuotasVencidasCompletas</td>
                            <td>SaldoVencido</td>
                            <td>InactividadMensual</td>
                            <td>PagoCero</td>
                            <td>PeriodoUltimoPagoOrigen</td>
                            <td>FechaUltimoPagoAplicado</td>
                            <td>Quebranto</td>
                            <td>EstatusCredito</td>
                            <td>UltGestionExtj</td>
                            <td>FechaUltGestionExtj</td>
                            <td>MejorGestionExtj</td>
                            <td>ScoreNivel</td>
                            <td>NumeroCreditos</td>
                            <td>SectorEconomico</td>
                            <td>EstadoDependencia</td>
                            <td>FinEstimadoCredito</td>
                            <td>FinRealCredito</td>
                            <td>FolioSolicitud</td>
                            <td>ClabeInterbancaria</td>
                            <td>Banco</td>
                            <td>CuentaBancaria</td>
                            <td>DueMensual</td>
                            <td>AsignaExtJud</td>
                            <td>RespuestaDomiciliacion</td>
                            <td>referencia646</td>
                            <td>convenio012cobranza</td>
                            <td>referencia012</td>
                            <td>referencia012l</td>
                            <td>convenio072cobranza</td>
                            <td>referencia072</td>
                            <td>referencia072l</td>
                            <td>convenio127cobranza</td>
                            <td>referencia127p</td>
                            <td>referencia127l</td>
                            <td>referenciapaynet</td>
                            <td>NumAfiliacion</td>
                            <td>ZonaPagadora</td>
                            <td>Pagaduria</td>
                            <td>Distribuidor</td>
                            <td>Promocion</td>
                            <td>ParcialidadCapital</td>
                            <td>ParcialidadInteres</td>
                            <td>ParcialidadIVA</td>
                            <td>Gracia</td>
                            <td>MontoPagosNomina</td>
                            <td>MontoPagosDomiciliacion</td>
                            <td>MontoPagosOtros</td>
                            <td>EstatusCobranza</td>
                            <td>SubEstatusCobranza</td>
                            <td>Dictaminacion</td>
                            <td>CreditoRefinanciamiento</td>
                            <td>FechaRespuestaDomiciliacion</td>
                            <td>MontoDomiciliacion</td>
                            <td>SucursalCF</td>
                            <td>nombreempleado</td>
                            <td>CuentaBancariaDispersion</td>
                            <td>PeriodoGracia</td>
                            <td>CapitalAplicado</td>
                            <td>InteresAplicado</td>
                            <td>IVAAplicado</td>
                            <td>PagoCeroHistorico</td>
                            <td>UltimoPagoOrigen</td>
                            <td>UltimoPagoAplicado</td>
                            <td>DiasSinPago</td>
                            <td>MontoUltPFN</td>
                            <td>FechaUltPFN</td>
                            <td>PeriodoUltDescuentoNomina</td>
                            <td>EstatusUltDescuentoNomina</td>
                            <td>FechaUltimoPago</td>
                            <td>DescuentoNominaMes</td>
                            <td>DomiciliacionMes</td>
                            <td>CobroPFNMes</td>
                            <td>CobroPFNTotal</td>
                            <td>MedioCobroPFN</td>
                            <td>UltGestionJudicial</td>
                            <td>EstatusPagoRelacionesPublicas</td>
                            <td>ADCMotivo</td>
                            <td>ADCSubMotivo</td>
                            <td>DueRiesgos</td>
                            <td>IndicadorDUE</td>
                            <td>NivelAtencion</td>
                            <td>iListId</td>
                            <td>nPayment</td>
                            <td>fPaymentDate</td>
                            <td>nPaid</td>
                            <td>EstadoUltLista</td>
                            <td>iCollect_iYear</td>
                            <td>fStatusDate</td>
                            <td>vReference</td>
                            <td>opt_Refinance_PicId</td>
                            <td>nAmount</td>
                            <td>fApplyDate</td>
                            <td>DependenciaOrigen</td>
                            <td>iOpStatus</td>

                        </tr>
                        <tr>
                            <th>Descripcion</th>
                            <td>Hasta 8 números</td>
                            <td>Hasta 120 caracteres</td>
                            <td>Hasta 20 caracteres</td>
                            <td>Hasta 50 caracteres</td>
                            <td>Hasta 4 números</td>
                            <td>Hasta 232 caracteres</td>
                            <td>Hasta 62 caracteres</td>
                            <td>Hasta 10 caracteres</td>
                            <td>Hasta 10 caracteres</td>
                            <td>Hasta 10 caracteres</td>
                            <td>Hasta 10 caracteres</td>
                            <td>Hasta 10 caracteres</td>
                            <td>Hasta 50 caracteres</td>
                            <td>Hasta 8 números</td>
                            <td>Hasta 8 números</td>
                            <td>Hasta 20 caracteres</td>
                            <td>Hasta 50 caracteres</td>
                            <td>Hasta 30 caracteres</td>
                            <td>Hasta 50 caracteres</td>
                            <td>Hasta 100 caracteres</td>
                            <td>Hasta 50 caracteres</td>
                            <td>Hasta 50 caracteres</td>
                            <td>Hasta 17 números con decimales</td>
                            <td>Hasta 9 números con decimales</td>
                            <td>Hasta 9 números con decimales</td>
                            <td>Hasta 9 números con decimales</td>
                            <td>Hasta 17 números con decimales</td>
                            <td>Hasta 4 números</td>
                            <td>Hasta 9 números con decimales</td>
                            <td>Hasta 50 caracteres</td>
                            <td>Hasta 50 caracteres</td>
                            <td>Hasta 100 caracteres</td>
                            <td>Hasta 9 números con decimales</td>
                            <td>Hasta 17 números con decimales</td>
                            <td>Hasta 4 números</td>
                            <td>Hasta 9 números con decimales</td>
                            <td>Hasta 9 números con decimales</td>
                            <td>Hasta 9 números con decimales</td>
                            <td>Hasta 9 números con decimales</td>
                            <td>Hasta 4 números</td>
                            <td>Hasta 17 números con decimales</td>
                            <td>Hasta 17 números con decimales</td>
                            <td>Hasta 2 caracteres</td>
                            <td>Hasta 25 caracteres</td>
                            <td>Hasta 40 ncaracteres</td>
                            <td>Hasta 2 caracteres</td>
                            <td>Hasta 50 caracteres</td>
                            <td>Hasta 50 caracteres</td>
                            <td>Hasta 40 ncaracteres</td>
                            <td>Hasta 10 caracteres</td>
                            <td>Hasta 20 caracteres</td>
                            <td>Hasta 4 números</td>
                            <td>Hasta 50 caracteres</td>
                            <td>Hasta 50 caracteres</td>
                            <td>Hasta 30 caracteres</td>
                            <td>Hasta 10 caracteres</td>
                            <td>Hasta 20 caracteres</td>
                            <td>Hasta 20 caracteres</td>
                            <td>Hasta 50 caracteres</td>
                            <td>Hasta 11 caracteres</td>
                            <td>Hasta 9 números con decimales</td>
                            <td>Hasta 50 caracteres</td>
                            <td>Hasta 50 caracteres</td>
                            <td>Hasta 50 caracteres</td>
                            <td>Hasta 50 caracteres</td>
                            <td>Hasta 50 caracteres</td>
                            <td>Hasta 50 caracteres</td>
                            <td>Hasta 50 caracteres</td>
                            <td>Hasta 50 caracteres</td>
                            <td>Hasta 50 caracteres</td>
                            <td>Hasta 50 caracteres</td>
                            <td>Hasta 50 caracteres</td>
                            <td>Hasta 50 caracteres</td>
                            <td>Hasta 50 caracteres</td>
                            <td>Hasta 20 caracteres</td>
                            <td>Hasta 100 caracteres</td>
                            <td>Hasta 50 caracteres</td>
                            <td>Hasta 10 caracteres</td>
                            <td>Hasta 10 caracteres</td>
                            <td>Hasta 9 números con decimales</td>
                            <td>Hasta 9 números con decimales</td>
                            <td>Hasta 9 números con decimales</td>
                            <td>Hasta 4 números</td>
                            <td>Hasta 17 números con decimales</td>
                            <td>Hasta 17 números con decimales</td>
                            <td>Hasta 17 números con decimales</td>
                            <td>Hasta 50 caracteres</td>
                            <td>Hasta 50 caracteres</td>
                            <td>Hasta 50 caracteres</td>
                            <td>Hasta 8 números</td>
                            <td>Hasta 40 ncaracteres</td>
                            <td>Hasta 9 números con decimales</td>
                            <td>Hasta 50 caracteres</td>
                            <td>Hasta 200 caracteres</td>
                            <td>Hasta 20 caracteres</td>
                            <td>Hasta 10 caracteres</td>
                            <td>Hasta 9 números con decimales</td>
                            <td>Hasta 9 números con decimales</td>
                            <td>Hasta 9 números con decimales</td>
                            <td>Hasta 2 caracteres</td>
                            <td>Hasta 17 números con decimales</td>
                            <td>Hasta 9 números con decimales</td>
                            <td>Hasta 4 números</td>
                            <td>Hasta 9 números con decimales</td>
                            <td>Hasta 40 ncaracteres</td>
                            <td>Hasta 25 caracteres</td>
                            <td>Hasta 2 smallnúmeros</td>
                            <td>Hasta 40 ncaracteres</td>
                            <td>Hasta 17 números con decimales</td>
                            <td>Hasta 17 números con decimales</td>
                            <td>Hasta 17 números con decimales</td>
                            <td>Hasta 17 números con decimales</td>
                            <td>Hasta 50 caracteres</td>
                            <td>Hasta 10 caracteres</td>
                            <td>Hasta 50 caracteres</td>
                            <td>Hasta 50 caracteres</td>
                            <td>Hasta 50 caracteres</td>
                            <td>Hasta 10 caracteres</td>
                            <td>Hasta 4 números</td>
                            <td>Hasta 4 números</td>
                            <td>Hasta 8 números</td>
                            <td>Hasta 9 números con decimales</td>
                            <td>Hasta 4 números</td>
                            <td>Hasta 9 números con decimales</td>
                            <td>Hasta 50 caracteres</td>
                            <td>Hasta 4 números</td>
                            <td>Hasta 4 números</td>
                            <td>Hasta 40 caracteres</td>
                            <td>Hasta 8 números</td>
                            <td>Hasta 9 números con decimales</td>
                            <td>Hasta 4 números</td>
                            <td>Hasta 50 caracteres</td>
                            <td>Hasta 4 números</td>

                        </tr>
                    </table>
                    </asp:panel>
                   
                </div>
           
                <div class="d-flex justify-content-center mt-2">
                    <h4>Subir archivo CSV, TXT o UNL</h4>
                </div>
                <div class="d-flex justify-content-center mt-2">
                    <label>Separador:</label><br />
                    <telerik:RadDropDownList runat="server" ID="DDLSeparador" DefaultMessage="Seleccione" AutoPostBack="false">
                        <Items>
                            <telerik:DropDownListItem Text="Tabulador" Value="0" Selected="true" />
                            <telerik:DropDownListItem Text="Coma" Value="1" />
                            <telerik:DropDownListItem Text="Pipe" Value="2" />
                        </Items>
                    </telerik:RadDropDownList>
                </div>
                <div class="d-flex justify-content-center mt-2">
                    <telerik:RadAsyncUpload ID="RadAsyncUpload1" runat="server" AllowedFileExtensions=".CSV,.txt,.unl" MultipleFileSelection="Disabled" OnClientFileUploading="focusDown">
                    </telerik:RadAsyncUpload>
                </div>
                <div class="d-flex justify-content-center mt-2">
                    <telerik:RadButton ID="BtnCargar" runat="server" Text="Cargar" SingleClick="true" SingleClickText="Procesando..." OnClientClicking="focusDown" />
                </div>
                <div class="d-flex justify-content-center my-2">
                    <telerik:RadProgressManager ID="RadProgressManager1" runat="server" />
                    <telerik:RadProgressArea RenderMode="Lightweight" ID="RadProgressArea1" runat="server" Width="50%" />
                </div>
                <div class="d-flex justify-content-center mt-2">
                    <asp:Label ID="LblMensaje" runat="server"></asp:Label>
                </div>
                <div class="d-flex justify-content-center my-2">
                    <telerik:RadGrid ID="GvCargaAsignacion" runat="server" Visible="false"></telerik:RadGrid>
                </div>
            </div>

    </telerik:RadAjaxPanel>
    <telerik:RadWindowManager ID="RadAviso" runat="server">
        <Localization OK="Aceptar" />
    </telerik:RadWindowManager>
    <asp:HiddenField ID="HidenUrs" runat="server" />

    <!-- Truco para ir hasta abajo de la pagina ;) -->
    <div id="down"></div>
    <script>
        focusDown = () => {
            $([document.documentElement, document.body]).animate({
                scrollTop: $("#down").offset().top
            }, 1000);
        }

    </script>
</asp:Content>

