<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Promesas.aspx.vb" Inherits="M_Gestion_Promesas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="JS/iFrameMejorado.js?v=2" type="text/javascript"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
    <link rel="stylesheet" href="Estilos/w3.css"/>
</head>
<body style="font-size:.7em" class="scroll">
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
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="DdlCat_Ne_Nombre">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="PnlNeGociacion" LoadingPanelID="RadAjaxLoadingPnlGeneral" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="BtnGestion">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="PnlGestion" LoadingPanelID="RadAjaxLoadingPnlGeneral" />
                        <telerik:AjaxUpdatedControl ControlID="BtnGestion" />
                        
                        
                        <telerik:AjaxUpdatedControl ControlID="Notificacion" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="BtnGuardar">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="PnlNeGociacion" LoadingPanelID="RadAjaxLoadingPnlGeneral" />
                        
                        
                        <telerik:AjaxUpdatedControl ControlID="Notificacion" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="DdlHist_Ge_Accion">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="PnlNeGociacion" LoadingPanelID="RadAjaxLoadingPnlGeneral" />
                        
                        
                        <telerik:AjaxUpdatedControl ControlID="Notificacion" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="DdlHist_Ge_Resultados">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="PnlNeGociacion" LoadingPanelID="RadAjaxLoadingPnlGeneral" />
                        
                        
                        <telerik:AjaxUpdatedControl ControlID="Notificacion" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="DdlHist_Ge_NoPago">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="PnlNeGociacion" LoadingPanelID="RadAjaxLoadingPnlGeneral" />
                        
                        
                        <telerik:AjaxUpdatedControl ControlID="Notificacion" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="TxtFechaContacto">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="PnlNeGociacion" LoadingPanelID="RadAjaxLoadingPnlGeneral" />
                        
                        
                        <telerik:AjaxUpdatedControl ControlID="Notificacion" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="DdlHora">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="PnlNeGociacion" LoadingPanelID="RadAjaxLoadingPnlGeneral" />
                        
                        
                        <telerik:AjaxUpdatedControl ControlID="Notificacion" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPnlGeneral" runat="server">
        </telerik:RadAjaxLoadingPanel>
        <asp:Panel ID="PnlNegoVigente" runat="server">
            <div class="w3-row-padding">                
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
        <asp:Panel ID="PnlNeGociacion" runat="server" Visible="true">
            <!-- Título -->
            <div class="w3-container w3-blue w3-center">
                <label>Promesas de pagos</label>
            </div>
            <!-- PnlDetalle -->
            <asp:Panel runat="server" CssClass="w3-container w3-row" ID="PnlDetalle" Visible="true">
                <asp:Panel runat="server" CssClass="w3-container w3-row" ID="PnlSaldos" Visible="true">
                <!-- Información financiera -->
                <div class="w3-container">
                    <!-- Título -->
                    <div class="w3-container w3-blue w3-center">
                        <label>Saldos del credito</label>
                    </div>                    
                    <!-- Datos -->
                    <div class="w3-container">
                        <div class="w3-row-padding">
                            <div class="w3-col s12 m6 l3">
                                <label>Tasa normal</label> 
                                <telerik:RadNumericTextBox ID="RNTTasaNormal" runat="server" Type="Percent"   Width="100%" ReadOnly="true"></telerik:RadNumericTextBox>
                            </div>
                            <div class="w3-col s12 m6 l3">
                                <label>Tasa moratoria</label>
                                <telerik:RadNumericTextBox ID="RNTTasaMoratoria" runat="server" Type="Percent"   Width="100%" ReadOnly="true"></telerik:RadNumericTextBox>                                
                            </div>
                            <div class="w3-col s12 m6 l3">
                                <label>Plazo del crédito</label>
                                <telerik:RadTextBox ID="RTBPlazo" runat="server"   Width="100%" ReadOnly="true"></telerik:RadTextBox>
                            </div>
                            <div class="w3-col s12 m6 l3">
                                <label>Frecuencia de pago</label>
                                <telerik:RadTextBox ID="RTBFrecuenciaPago" runat="server"   Width="100%" ReadOnly="true"></telerik:RadTextBox>
                            </div>
                        </div>
                    </div>
                    <div class="w3-container">
                        <div class="w3-row-padding">
                            <div class="w3-col s12 m6 l3">
                                <label>Con garantía o sin garantía</label>                               
                                <telerik:RadTextBox ID="RTBCSGarantia" runat="server"   Width="100%" ReadOnly="true"></telerik:RadTextBox>
                            </div>
                            <div class="w3-col s12 m6 l3">
                                <label>Fecha de Vencimiento del crédito</label>                               
                                <telerik:RadTextBox ID="RTBDteVencCredito" runat="server"   Width="100%" ReadOnly="true"></telerik:RadTextBox>
                            </div>
                            <div class="w3-col s12 m6 l3">
                                <label>Fecha de otorgamiento</label>                               
                                <telerik:RadTextBox ID="RTBDteOtorgamientoFinan" runat="server"   Width="100%" ReadOnly="true"></telerik:RadTextBox>
                            </div>
                            <div class="w3-col s12 m6 l3">
                                <label>Saldo del crédito</label>                               
                                <telerik:RadNumericTextBox ID="RNTSdoCredito" runat="server" Type="Currency"   Width="100%" ReadOnly="true"></telerik:RadNumericTextBox>
                            </div>
                        </div>
                    </div>
                    <div class="w3-container">
                        <div class="w3-row-padding">
                            <div class="w3-col s12 m6 l3">
                                <label>Capital pendiente</label>
                                <telerik:RadNumericTextBox ID="RNTCapitalPendiente" runat="server" Type="Currency"   Width="100%" ReadOnly="true"></telerik:RadNumericTextBox>
                            </div>
                            <div class="w3-col s12 m6 l3">
                                <label>Capital vencido impagado</label>
                                <telerik:RadNumericTextBox ID="RNTCapVencImp" runat="server" Type="Currency"   Width="100%" ReadOnly="true"></telerik:RadNumericTextBox>
                            </div>
                            <div class="w3-col s12 m6 l3">
                                <label>Interés normal</label>
                                <telerik:RadNumericTextBox ID="RNTIntNormalFinan" runat="server" Type="Currency"   Width="100%" ReadOnly="true"></telerik:RadNumericTextBox>
                            </div>
                            <div class="w3-col s12 m6 l3">
                                <%--<label>IVA del interés normal</label>--%>
                                <telerik:RadNumericTextBox ID="RNTIVAIntNormal" runat="server" Type="Currency"   Width="100%" Visible="false" ReadOnly="true"></telerik:RadNumericTextBox>
                            </div>
                        </div>
                    </div>
                    <div class="w3-container">
                        <div class="w3-row-padding">
                            <div class="w3-col s12 m6 l3">
                                <label>Intereses moratorios</label>
                                <telerik:RadNumericTextBox ID="RNTIntMoratorios" runat="server" Type="Currency"   Width="100%" ReadOnly="true"></telerik:RadNumericTextBox>
                            </div>
                            <div class="w3-col s12 m6 l3">
                                <label>IVA del interés moratorio</label>
                                <telerik:RadNumericTextBox ID="RNTIVAIntMoratorio" runat="server" Type="Currency"   Width="100%" ReadOnly="true"></telerik:RadNumericTextBox>
                            </div>
                            <div class="w3-col s12 m6 l3">
                                <label>Comisiones</label>
                                <telerik:RadNumericTextBox ID="RNTComisionesFinan" runat="server" Type="Currency"   Width="100%" ReadOnly="true"></telerik:RadNumericTextBox>
                            </div>
                            <div class="w3-col s12 m6 l3">
                                <label>IVA de las comisiones</label>
                                <telerik:RadNumericTextBox ID="RNTIVAComisiones" runat="server" Type="Currency"   Width="100%" ReadOnly="true"></telerik:RadNumericTextBox>
                            </div>
                        </div>
                    </div>
                    <div class="w3-container">
                        <div class="w3-row-padding">
                            <div class="w3-col s12 m6 l3">
                                <label>Número de cuotas impagadas</label>
                                <telerik:RadNumericTextBox ID="RNTNumCuotasImp" runat="server" Type="Number"   Width="100%" NumberFormat-DecimalDigits="0" ReadOnly="true"></telerik:RadNumericTextBox>
                            </div>
                            <div class="w3-col s12 m6 l3">
                                <label>Fecha del último pago</label>
                                <telerik:RadTextBox ID="RTBDteUltPago" runat="server"   Width="100%" ReadOnly="true"></telerik:RadTextBox>
                            </div>
                            <div class="w3-col s12 m6 l3">
                                <label>Fecha castigo</label>
                                <telerik:RadNumericTextBox ID="RTBDteCastigo" runat="server"   Width="100%" ReadOnly="true"></telerik:RadNumericTextBox>
                            </div>
                            <div class="w3-col s12 m6 l3">
                                <label>Instancia de cobranza</label>
                                <telerik:RadTextBox ID="RTBInstanciaFinan" runat="server"   Width="100%" ReadOnly="true"></telerik:RadTextBox>
                            </div>
                        </div>
                    </div>
                    <div class="w3-container">
                        <div class="w3-row-padding">                            
                            <div class="w3-col s12 m6 l3">
                                <label>Gastos de cobranza</label>
                                <telerik:RadNumericTextBox ID="RNTGastosCobranza" runat="server" Type="Currency"   Width="100%" ReadOnly="true"></telerik:RadNumericTextBox>
                            </div>
                            <div class="w3-col s12 m6 l3">
                                
                            </div>
                            <div class="w3-col s12 m6 l3">
                                
                            </div>
                            <div class="w3-col s12 m6 l3">
                                
                            </div>
                        </div>
                    </div>
                    <div class="w3-container">
                        <div class="w3-row-padding">                            
                            <div class="w3-col s12 m6 l3">
                                <label>Estatus del credito</label>
                                <telerik:RadTextBox ID="RTBCalifCartera" runat="server"   Width="100%" ReadOnly="true"></telerik:RadTextBox>
                            </div>
                            <div class="w3-col s12 m6 l3">
                                <label>Días mora</label>
                                <telerik:RadTextBox ID="RTBDiasMora" runat="server"   Width="100%" ReadOnly="true"></telerik:RadTextBox>
                            </div>
                            <div class="w3-col s12 m6 l3">
                                <label>Capital vencido impagado</label>
                                <telerik:RadNumericTextBox ID="RNTCapVencImpFinan" runat="server" Type="Currency"   Width="100%" ReadOnly="true"></telerik:RadNumericTextBox>
                            </div>
                            <div class="w3-col s12 m6 l3">
                                <label>Rol</label>
                                <telerik:RadTextBox ID="RTBRol" runat="server"   Width="100%" ReadOnly="true"></telerik:RadTextBox>
                            </div>
                        </div>
                    </div>
                    <div class="w3-container w3-blue w3-center" id="DivActualiza" runat="server">
                        <label>Actualización de saldos</label>
                    </div>                    
                    <!-- Datos -->
                    <div class="w3-container" id="DivActualiza2" runat="server">
                        <div class="w3-row-padding">
                            <div class="w3-col s12 m6 l3">
                                <label>Capital</label> 
                                <telerik:RadNumericTextBox ID="RNTCapitalAct" runat="server" Type="Currency"   Width="100%" ReadOnly="true"></telerik:RadNumericTextBox>
                            </div>
                            <div class="w3-col s12 m6 l3">
                                <label>Interes Normal</label>
                                <telerik:RadNumericTextBox ID="RNTIntNormAct" runat="server" Type="Currency"   Width="100%" ReadOnly="true"></telerik:RadNumericTextBox>                               
                            </div>
                            <div class="w3-col s12 m6 l3">
                                <label>Interes Moratorio</label>
                                <telerik:RadNumericTextBox ID="RNTIntMoraAct" runat="server" Type="Currency"   Width="100%" ReadOnly="true"></telerik:RadNumericTextBox>
                            </div>
                            <div class="w3-col s12 m6 l3">
                                <label>Comisiones</label>
                                <telerik:RadNumericTextBox ID="RNTComisAct" runat="server" Type="Currency"   Width="100%" ReadOnly="true"></telerik:RadNumericTextBox>
                            </div>
                        </div>
                    </div>
                    <div class="w3-container">
                        <div class="w3-row-padding">                            
                            <div class="w3-col s12 m6 l3">
                                
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
              <asp:Panel runat="server" CssClass="w3-container w3-row" ID="PnlSaldosBloqueados" Visible="true">
            <br />
            <br />
            <div class="w3-container w3-center w3-blue">
                <b>Saldos de haberes</b>
            </div> 
            <telerik:RadGrid ID="GvSaldosCredito" runat="server"  RenderMode="Lightweight" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true"
                        ClientSettings-EnableRowHoverStyle="true" OnNeedDataSource="GvSaldosCredito_NeedDataSource" Culture="es-MX" AllowFilteringByColumn="false" Style="overflow: visible;" HeaderStyle-HorizontalAlign="Center" >
                       <MasterTableView Width="100%" CommandItemDisplay="Top" EditFormSettings-PopUpSettings-KeepInScreenBounds="true" DataKeyNames="NUMERO">
                            <CommandItemSettings ShowAddNewRecordButton="false" RefreshText="Refrescar"/>
                    <Columns>
                        <telerik:GridEditCommandColumn ItemStyle-Width="5px" UniqueName="Expandible">
                                </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn UniqueName="NUMERO" HeaderText="No." DataField="NUMERO"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="CUENTAID" HeaderText="Cuenta ID" DataField="CUENTAID"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="PRODUCTO" HeaderText="Rol" DataField="PRODUCTO"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="ESTATUS" HeaderText="Estatus" DataField="ESTATUS"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="SALDO_TOTAL" HeaderText="Saldo total" DataField="SALDO_TOTAL"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="SALDO_DISP" HeaderText="Saldo disp" DataField="SALDO_DISP"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="SALDO_DISPUESTO" HeaderText="Saldo dispuesto" DataField="SALDO_DISPUESTO"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="SALDO_BLOQUEADO" HeaderText="Saldo bloqueado" DataField="SALDO_BLOQUEADO"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="TIPO_DE_BLOQUEO" HeaderText="Tipo de bloqueo" DataField="TIPO_DE_BLOQUEO"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="DESCRIPCION" HeaderText="Descripción" DataField="DESCRIPCION"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="REFERENCIA" HeaderText="Referencia" DataField="REFERENCIA"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="TIPO_DE_CUENTA" HeaderText="TIpo de cuenta" DataField="TIPO_DE_CUENTA"></telerik:GridBoundColumn>
                    </Columns> 
                            <EditFormSettings UserControlName="Editar.ascx" EditFormType="WebUserControl">
                                <EditColumn UniqueName="EditCommandColumn1">
                                </EditColumn>
                            </EditFormSettings>                   
                </MasterTableView>
            </telerik:RadGrid>
                  <asp:HiddenField ID="HidenUrs" runat="server" />
                    <asp:Button runat="server" ID="BtnAceptarConfirmacion" Visible="true" Height="1px" Style="display: none;" />


                    <telerik:RadWindowManager ID="RadAviso" runat="server" >
                        <Localization OK="Aceptar" />
                    </telerik:RadWindowManager>
            <br />
            <br />
            </asp:Panel>
            <%--<asp:Panel runat="server" CssClass="w3-container w3-row" ID="PnlSaldosBloqueados" Visible="true">
                <!-- Información financiera -->
                <div class="w3-container">
                    <!-- Título -->
                    <div class="w3-container w3-blue w3-center">
                        <label>Saldos bloqueados</label>
                    </div>                    
                    <!-- Datos -->
                    <div class="w3-container">
                        <div class="w3-row-padding">
                            <div class="w3-col s12 m6 l3">
                                <label>ID Cuenta</label> 
                                <telerik:RadTextBox ID="RTBIDCuenta" runat="server"   Width="100%"></telerik:RadTextBox>
                            </div>
                            <div class="w3-col s12 m6 l3">
                                <label>Nombre del producto</label>
                                <telerik:RadTextBox ID="RTBNombreProducto" runat="server"   Width="100%"></telerik:RadTextBox>
                            </div>
                            <div class="w3-col s12 m6 l3">
                                <label>Estatus de la cuenta</label>
                                <telerik:RadTextBox ID="RTBEstatusCuenta" runat="server"   Width="100%"></telerik:RadTextBox>
                            </div>
                            <div class="w3-col s12 m6 l3">
                                <label>Saldo total</label>
                                <telerik:RadNumericTextBox ID="RNTSaldoTotal" runat="server" Type="Currency"   Width="100%"></telerik:RadNumericTextBox>
                            </div>
                        </div>
                    </div>
                    <div class="w3-container">
                        <div class="w3-row-padding">
                            <div class="w3-col s12 m6 l3">
                                <label>Saldo disponible</label>                               
                                <telerik:RadNumericTextBox ID="RNTSaldoDisponible" runat="server" Type="Currency"   Width="100%"></telerik:RadNumericTextBox>
                            </div>
                            <div class="w3-col s12 m6 l3">
                                <label>Saldo bloqueado</label>                               
                                <telerik:RadNumericTextBox ID="RNTSaldoBloqueado" runat="server" Type="Currency"   Width="100%"></telerik:RadNumericTextBox>
                            </div>
                            <div class="w3-col s12 m6 l3">
                                
                            </div>
                            <div class="w3-col s12 m6 l3">
                                
                            </div>
                        </div>
                    </div>
                </div>
                <div class="w3-container">
                    <!-- Título -->
                    <div class="w3-container w3-blue w3-center">
                        <label>Bloqueo de saldos</label>
                    </div>                    
                    <!-- Datos -->
                    <div class="w3-container">
                        <div class="w3-row-padding">
                            <div class="w3-col s12 m6 l3">
                                <label>ID Cuenta</label> 
                                <telerik:RadTextBox ID="RTBIDCuenta2" runat="server"   Width="100%"></telerik:RadTextBox>
                            </div>
                            <div class="w3-col s12 m6 l3">
                                <label>Tipo de bloqueo</label>
                                <telerik:RadTextBox ID="RTBTipobloqueo" runat="server"   Width="100%"></telerik:RadTextBox>
                            </div>
                            <div class="w3-col s12 m4 l2">
                                <label>Descripción</label>
                                <telerik:RadTextBox ID="RTBDescripcion" runat="server"   Width="100%"></telerik:RadTextBox>
                            </div>
                            <div class="w3-col s12 m4 l2">
                                <label>Referencia</label>
                                <telerik:RadTextBox ID="RTBReferencia" runat="server"   Width="100%"></telerik:RadTextBox>
                            </div>
                            <div class="w3-col s12 m4 l2">
                                <label>Monto bloqueado</label>
                                <telerik:RadNumericTextBox ID="RNTMontoBloqueado" runat="server" Type="Currency"   Width="100%"></telerik:RadNumericTextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>--%>
                <!-- Información financiera -->
                <div class="w3-container">
                    <!-- Título -->
                    <div class="w3-container w3-blue w3-center">
                        <label>Simulación de saldos y aplicación de pago</label>
                    </div>
                    <!-- Datos -->
                    <div class="w3-container">
                        <div class="w3-row-padding">
                            <div class="w3-col s12 m6 l3">
                                <label>Fecha de simulación:</label>
                                <telerik:RadDatePicker ID="RDPDteSimulacion" runat="server" CssClass="w3-input" Width="100%" ></telerik:RadDatePicker>                                
                            </div>
                            <div class="w3-col s12 m6 l3">
                                <label>Tipo de pago:</label>
                                <telerik:Radcombobox ID="RCBTipoPago" runat="server" AutoPostBack="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Value="PP" Text="Pago parcial"/>
                                        <telerik:RadComboBoxItem Value="PC" Text="Puesta al corriente"/>                                        
                                        <telerik:RadComboBoxItem Value="T" Text="Liquidacion"/>
                                    </Items>
                                </telerik:Radcombobox>
                            </div>
                            <div class="w3-col s12 m6 l3">
                                <telerik:RadButton ID="RBSimular" Text="Simular" runat="server" SingleClick="true" SingleClickText="Simulando..."></telerik:RadButton>
                            </div>
                            <div class="w3-col s12 m4 l2">
                                <label id="LAccion" runat="server" visible="true">Accion:</label>
                                <telerik:RadDropDownList ID="RDDLAccion" runat="server" DefaultMessage="Seleccione..." Width="100%" AutoPostBack="false" Enabled="true" visible="true"></telerik:RadDropDownList>
                            </div>                            
							<div class="w3-col s12 l3">
                                <label id="Label1" runat="server" visible="true">Observaciones gestión</label>                             
                                <telerik:radtextbox runat="server" ID="RTBObservacionesGest" MaxLength="500" Visible="true" TextMode="MultiLine"></telerik:radtextbox>
                                <%--<telerik:RadButton runat="server" ID="BtnDescargarDos" Text="Descargar PDF Dos" AutoPostBack="true" Visible="true"></telerik:RadButton>--%>
                            </div>
                        </div>
                    </div>
                    <div class="w3-container w3-blue w3-center">
                        <label>Campañas aplicadas actualmente</label>
                    </div>
                    <div class="w3-container">
                                <telerik:RadGrid ID="RGCampEspVig" runat="server" Width="100%" OnNeedDataSource="RGCampEspVig_NeedDataSource" Visible="true" Style="overflow: visible;" AllowSorting="True" AllowPaging="true" PageSize="3" HeaderStyle-HorizontalAlign="Center" GridLines="None" AllowFilteringByColumn="false" AutoGenerateColumns="false" Culture="es-MX">
                                    <MasterTableView AllowMultiColumnSorting="false">
                                        <PagerStyle AlwaysVisible="true" />
                                        <CommandItemSettings ShowAddNewRecordButton="false"/>
                                        <Columns>
                                            <telerik:GridBoundColumn UniqueName="NOMBRE" HeaderText="Nombre" DataField="NOMBRE"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="VIGENCIA" HeaderText="Vigencia" DataField="VIGENCIA"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="DESCRIPCION" HeaderText="Descripción" DataField="DESCRIPCION"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="BONIFICACION" HeaderText="% Cond Comisiones" DataField="BONIFICACION"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="CONDONACIONM" HeaderText="% Cond Interes Mora" DataField="CONDONACIONM"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="CONDONACIONN" HeaderText="% Cond Interes Normal" DataField="CONDONACIONN"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="CONDONACIONC" HeaderText="% Cond Capital" DataField="CONDONACIONC"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="PORCEXTERNO" HeaderText="% Honorarios Max Externo" DataField="PORCEXTERNO"></telerik:GridBoundColumn>
                                        </Columns>                    
                                    </MasterTableView>
                                </telerik:RadGrid>
                    </div>
                    <!-- Título -->
                    <div class="w3-container w3-blue w3-center">
                        <label>Aplicación del pago, condonaciones, honorarios y gastos de juicio</label>
                    </div>
                    <!-- Datos -->
                    <div class="w3-container">
                        <div class="w3-row-padding">
                            <div class="w3-col s12 m6 l3">
                                <label>Monto a pagar:</label>
                                <telerik:RadNumericTextBox ID="RNTMtoPagar" runat="server" CssClass="w3-input" Type="Currency" Width="100%"  Value="0" Enabled="true"></telerik:RadNumericTextBox>
                            </div>
                            <div class="w3-col s12 m6 l3">
                                <%--<label>¿El pago considera honorarios?</label>--%>
                                <telerik:RadCheckBox ID="RCBConsideraHonorarios" runat="server" AutoPostBack="false" Checked="true" Visible="false"></telerik:RadCheckBox>
                            </div>
                        </div>
                    </div>
                    <div class="w3-container">
                        <div class="w3-row-padding">
                            <div class="w3-col s12 m6 l3">
                                <label>Comisiones</label>
                                <telerik:RadNumericTextBox ID="RNTComisiones" runat="server" CssClass="w3-input" Type="Percent" Value="0" Width="100%" MaxValue="100" MinValue="0"></telerik:RadNumericTextBox>
                            </div>
                            <div class="w3-col s12 m6 l3">
                                <label>Moratorios</label>
                                <telerik:RadNumericTextBox ID="RNTMoratorios" runat="server" CssClass="w3-input" Type="Percent" Value="0"  Width="100%" MaxValue="100" MinValue="0"></telerik:RadNumericTextBox>
                            </div>
                            <div class="w3-col s12 m6 l3">
                                <label>Interes normal</label>
                                <telerik:RadNumericTextBox ID="RNTIntNormal" runat="server" CssClass="w3-input" Type="Percent" Value="0"  Width="100%" MaxValue="100" MinValue="0"></telerik:RadNumericTextBox>
                            </div>
                            <div class="w3-col s12 m6 l3">
                                <label>Capital</label>
                                <telerik:RadNumericTextBox ID="RNTCapital" runat="server" CssClass="w3-input" Type="Percent" Value="0"  Width="100%" MaxValue="100" MinValue="0"></telerik:RadNumericTextBox>
                            </div>
                        </div>
                    </div>
                    <div class="w3-container">
                        <div class="w3-row-padding">
                            <div class="w3-col s12 m6 l3">
                                <label>Porcentaje del honorario</label>
                                <telerik:RadNumericTextBox ID="RNTPorcHonorario" runat="server" CssClass="w3-input" Type="Percent" Width="100%"  AutoPostBack="false" MaxValue="100" MinValue="0"></telerik:RadNumericTextBox>
                            </div>
                            <div class="w3-col s12 m6 l3">
                                <label>Base del honorario</label>
                                <telerik:RadNumericTextBox ID="RNTBaseHonorario" runat="server" CssClass="w3-input" Type="Currency" Width="100%"  Enabled="false"></telerik:RadNumericTextBox>
                            </div>
                            <div class="w3-col s12 m4 l2">
                                <label>Importe del honorario</label>
                                <telerik:RadNumericTextBox ID="RNTImporteHonorario" runat="server" CssClass="w3-input" Type="Currency" Width="100%"  Enabled="false"></telerik:RadNumericTextBox>
                            </div>
                            <div class="w3-col s12 m4 l2">
                                <label>Impuesto del honorario</label>
                                <telerik:RadNumericTextBox ID="RNTImpuestoHonorario" runat="server" CssClass="w3-input" Type="Currency" Width="100%"  Enabled="false"></telerik:RadNumericTextBox>
                            </div>
                            <div class="w3-col s12 m4 l2">
                                <label>Total a pagar del honorario</label>
                                <telerik:RadNumericTextBox ID="RNTTotalHonorario" runat="server" CssClass="w3-input" Type="Currency" Width="100%"  Enabled="false"></telerik:RadNumericTextBox>
                            </div>
                        </div>
                    </div>
                    <div class="w3-container">
                        <div class="w3-row-padding">
                            <div class="w3-col s12 m6 l3">
                                <label>Gastos pendientes cobro</label>
                                <telerik:RadNumericTextBox ID="RNTGastosPend" runat="server" CssClass="w3-input" Type="Currency" Width="100%"  Enabled="false"></telerik:RadNumericTextBox>
                            </div>
                            <div class="w3-col s12 m6 l3">
                                <label>Monto total a pagar:</label>
                                <telerik:RadNumericTextBox ID="RNTMontoTotalPagar" runat="server" CssClass="w3-input" Type="Currency" Width="100%"  Enabled="false"></telerik:RadNumericTextBox>
                            </div>
                            <div class="w3-col s12 m6 l3">
                                <telerik:RADButton ID="RBAplicar" runat="server" Text="Aplicar" Width="100%" Visible="false" SingleClick="true" SingleClickText="Aplicando..."></telerik:RADButton>
                            </div>                                                      
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <br />
            <asp:Panel runat="server" CssClass="w3-container w3-row" ID="PnlAplicacionPago" Visible="true">
                <!-- Información financiera -->
                <div class="w3-container">
                    <!-- Título -->
                    <div class="w3-container w3-blue w3-center">
                        <label>Aplicación del pago</label>
                    </div>    
                    <div class="w3-container">
                        <div class="w3-row-padding">
                            <div class="w3-col s12 l5 w3-center">
                                <div class="w3-container w3-blue w3-center">
                                    <label>Deuda original</label>
                                </div>
                                <div>
                                    <table>
                                        <tr>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                Capital pendiente de vencimiento
                                            </td>
                                            <td colspan="3" style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLCPVenc" runat="server"></telerik:RadLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                Capital vencido impagado
                                            </td>
                                            <td colspan="3" style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLCVImp" runat="server"></telerik:RadLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                Total capital
                                            </td>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLTCap" runat="server"></telerik:RadLabel>
                                            </td>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                IVA
                                            </td>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                TOTALES
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                Int. Pend. Vento.
                                            </td>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLIPVento" runat="server"></telerik:RadLabel>
                                            </td>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLIPVentoIVA" runat="server"></telerik:RadLabel>
                                            </td>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLIPVentoTOTAL" runat="server"></telerik:RadLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                Int. Vencido
                                            </td>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLIVenc" runat="server"></telerik:RadLabel>
                                            </td>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLIVencIVA" runat="server"></telerik:RadLabel>
                                            </td>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLIVencTOTAL" runat="server"></telerik:RadLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                Total intereses
                                            </td>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLTInt" runat="server"></telerik:RadLabel>
                                            </td>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLTIntIVA" runat="server"></telerik:RadLabel>
                                            </td>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLTIntTOTAL" runat="server"></telerik:RadLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                Moratorios
                                            </td>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLMoratorios" runat="server"></telerik:RadLabel>
                                            </td>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLMoratoriosIVA" runat="server"></telerik:RadLabel>
                                            </td>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLMoratoriosTOTAL" runat="server"></telerik:RadLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                Comisiones
                                            </td>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLCom" runat="server"></telerik:RadLabel>
                                            </td>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLComIVA" runat="server"></telerik:RadLabel>
                                            </td>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLComTOTAL" runat="server"></telerik:RadLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                Totales
                                            </td>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLTot" runat="server"></telerik:RadLabel>
                                            </td>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLTotIVA" runat="server"></telerik:RadLabel>
                                            </td>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLTotTOTALES" runat="server"></telerik:RadLabel>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <%--<div class="w3-col s12 m6 l3 w3-center">
                                <div class="w3-container w3-blue w3-center">
                                    <label>Importe del pago</label>
                                </div>
                                <div>
                                    <table>
                                        <tr>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLIPCPVenc" runat="server"></telerik:RadLabel>
                                            </td>
                                            <td colspan="2">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLIPCVImp" runat="server"></telerik:RadLabel>
                                            </td>
                                            <td colspan="2">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLIpTCap" runat="server"></telerik:RadLabel>
                                            </td>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                IVA
                                            </td>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                TOTALES
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLIPIPVento" runat="server"></telerik:RadLabel>
                                            </td>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLIPIPVentoIVA" runat="server"></telerik:RadLabel>
                                            </td>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLIPIPVentoTOTAL" runat="server"></telerik:RadLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLIPIVenc" runat="server"></telerik:RadLabel>
                                            </td>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLIPIVencIVA" runat="server"></telerik:RadLabel>
                                            </td>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLIPIVencTOTAL" runat="server"></telerik:RadLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLIPTInt" runat="server"></telerik:RadLabel>
                                            </td>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLIPTIntIVA" runat="server"></telerik:RadLabel>
                                            </td>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLIPTIntTOTAL" runat="server"></telerik:RadLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLIPMora" runat="server"></telerik:RadLabel>
                                            </td>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLIPMoraIVA" runat="server"></telerik:RadLabel>
                                            </td>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLIPMoraTOTAL" runat="server"></telerik:RadLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLIPCom" runat="server"></telerik:RadLabel>
                                            </td>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLIPComIVA" runat="server"></telerik:RadLabel>
                                            </td>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLIPComTOTAL" runat="server"></telerik:RadLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLIPTotal" runat="server"></telerik:RadLabel>
                                            </td>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLIPTotalIVA" runat="server"></telerik:RadLabel>
                                            </td>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLIPTotalTOTAL" runat="server"></telerik:RadLabel>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div class="w3-col s12 l2 w3-center">
                                <div class="w3-container w3-blue w3-center">
                                    <label>Condonaciones</label>
                                </div>
                                <div>
                                    <table>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLCCVImp" runat="server"></telerik:RadLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLCTInt" runat="server"></telerik:RadLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLCMora" runat="server"></telerik:RadLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLCCom" runat="server"></telerik:RadLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLCTotal" runat="server"></telerik:RadLabel>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div class="w3-col s12 l2 w3-center">
                                <div class="w3-container w3-blue w3-center">
                                    <label>Nuevos saldos</label>
                                </div>
                                <div>
                                    <table>
                                        <tr>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLNSCPVenc" runat="server"></telerik:RadLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLNVCVImp" runat="server"></telerik:RadLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLNSTCap" runat="server"></telerik:RadLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLNSIPVento" runat="server"></telerik:RadLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLNSIVenc" runat="server"></telerik:RadLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLNSTInt" runat="server"></telerik:RadLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLNSMora" runat="server"></telerik:RadLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLNSCom" runat="server"></telerik:RadLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLNSTotal" runat="server"></telerik:RadLabel>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>--%>
                            <div class="w3-col s12 l12 w3-center">
                                <div>
                                    <table>
                                        <tr>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLDiasMoraSim" runat="server" Text="Dias de Mora Simulados"></telerik:RadLabel>
                                            </td>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLDiasMoraSimVal" runat="server"></telerik:RadLabel>
                                            </td>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLFactPagComp" runat="server" Text="Facturas Pagadas Completas"></telerik:RadLabel>
                                            </td>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLFactPagCompVal" runat="server"></telerik:RadLabel>
                                            </td>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLFactPagParc" runat="server" Text="Facturas Pagadas Parciales"></telerik:RadLabel>
                                            </td>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RLFactPagParcVal" runat="server"></telerik:RadLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RadLabel1" runat="server"></telerik:RadLabel>
                                            </td>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadLabel ID="RadLabel2" runat="server"></telerik:RadLabel>
                                            </td>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid" bgcolor="LightGray">
                                                <telerik:RadLabel ID="RLAdedudoTot" runat="server" Text="Adeudo Total" Font-Bold="true"></telerik:RadLabel>
                                            </td>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid" bgcolor="LightGray">
                                                <telerik:RadLabel ID="RLAdedudoTotVal" runat="server" Font-Bold="true"></telerik:RadLabel>
                                            </td>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <%--<telerik:RadButton ID="RBDetallePago" runat="server" Text="Detalle del Pago" OnClientClicked="<script>document.getElementById('PnlDetallePagos').style.display='block';return false;</script>"></telerik:RadButton>--%>
                                                <button class="w3-border-0 w3-white" onclick="document.getElementById('PnlDetallePagos').style.display='block';return false;">
                                                    Detalle del Pago
                                                </button>
                                            </td>
                                            <td style="border-color: #000000; border-width:1px; border-style: solid">
                                                <telerik:RadButton ID="RBGenFicha" runat="server" Text="Generar Promesa" AutoPostBack="true" Visible="false" SingleClick="true" SingleClickText="Enviando..."></telerik:RadButton>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="PnlDetallePagos" style="display: none" class="w3-modal">
                    <div class="w3-modal-content w3-card-4">
                        <div class="w3-panel w3-blue w3-center">
                            <span onclick="document.getElementById('PnlDetallePagos').style.display='none'"
                                class="w3-button w3-display-topright w3-hover-red" style="font-size: 150%">&times;</span>
                            <h2>
                                <label>
                                    Detalle de Facturas                                    
                                </label>
                            </h2>
                        </div>
                        <div class="w3-container w3-center w3-white">
                            <telerik:RadGrid ID="RGDetallePagos" AutoGenerateColumns="false" runat="server" AllowPaging="True" PageSize="10">
                                <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true" OpenInNewWindow="true" UseItemStyles="false" FileName="Detalle facturas"></ExportSettings>
                                <MasterTableView AllowMultiColumnSorting="false" CommandItemDisplay="Top">
                                        <PagerStyle AlwaysVisible="true" />
                                        <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="true" ShowExportToCsvButton="true"/>
                                        <Columns>
                                            <telerik:GridBoundColumn UniqueName="NOFACTURA" HeaderText="Numero factura" DataField="NOFACTURA"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="FECHAINICIO" HeaderText="Fecha inicio" DataField="FECHAINICIO"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="FECHAFIN" HeaderText="Fecha fin" DataField="FECHAFIN"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="DIAS" HeaderText="Dias" DataField="DIAS"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="INTERESES" HeaderText="Intereses" DataField="INTERESES"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="IVAINTERES" HeaderText="IVA Intereses" DataField="IVAINTERES"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="CAPITAL" HeaderText="Capital" DataField="CAPITAL"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="INSOLUTO" HeaderText="Insoluto" DataField="INSOLUTO"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="PAGO" HeaderText="Pago" DataField="PAGO"></telerik:GridBoundColumn>
                                        </Columns>                    
                                    </MasterTableView>
                            </telerik:RadGrid>
                        </div>
                    </div>
                </div>
            </asp:Panel>          
            <br />
            <telerik:RadNotification ID="Notificacion2" runat="server" Position="Center" Width="330" Height="160" Animation="Fade" EnableRoundedCorners="true" EnableShadow="true" Style="z-index: 100000">
        </telerik:RadNotification>
        </asp:Panel>
        

    </form>
</body>
</html>
