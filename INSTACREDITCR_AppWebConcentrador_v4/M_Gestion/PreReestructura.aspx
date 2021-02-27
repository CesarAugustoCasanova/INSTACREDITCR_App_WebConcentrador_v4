<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PreReestructura.aspx.vb" Inherits="M_Gestion_PreReestructura" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Mc :: Modulo Gestion</title>
    <script src="JS/iFrameMejorado.js?v=2" type="text/javascript"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
    <link rel="stylesheet" href="Estilos/w3.css" />
    <script type="text/javascript">
        function realizarPostBack(dato) {
            __doPostBack('KA', dato);
        }
        
        function AlphabetOnly(sender, args)
    {
            var c = args.get_keyCode();
            console.log(c);
            if ((c < 32) || (c >=33 && c<=39) || (c >= 40 && c <= 45) || (c >= 47 && c < 65) || (c > 90 && c < 97) || (c > 122 && c < 209) )
            args.set_cancel(true);
        }

        //function Sinco(sender, args) {
        //    var c = args.get_keyCode();
        //    console.log(c);
        //    if (c == 34 )
        //        args.set_cancel(true);
        //}
</script>

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
                        Javascript estÃƒÂ¡ deshabilitado en su navegador web. Por favor, para ver correctamente este sitio,
                        <b><i>habilite javascript</i></b>.<br />
                        <br />
                        Para ver las instrucciones para habilitar javascript en su navegador, haga click <a
                            href="http://www.enable-javascript.com/es/">aquÃƒÂ­</a>.
                    </div>
                </div>
            </div>
        </noscript>
        <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
       <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="BtnGuardar">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="pnlReestructura" />
                        <telerik:AjaxUpdatedControl ControlID="pnlGrids" />
                        <telerik:AjaxUpdatedControl ControlID="Notificacion" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
               
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel runat="server" ID="pnlLoading"></telerik:RadAjaxLoadingPanel>
        <br />
        <telerik:RadButton runat="server" ID="btnGenerarPreReestructura" Text="Generar Pre-Reestructura"></telerik:RadButton>
         <asp:Button runat="server" ID="KA" Text="Â¿?" style="display:none;"></asp:Button>
        <telerik:RadAjaxPanel runat="server" ID="pnlGrids" LoadingPanelID="pnlLoading">
            <div class="w3-container w3-blue w3-center">
                <b>Creditos Pre-Reestructurados</b>
            </div>
            <div class="w3-panel w3-blue w3-center">
                <b>Creditos Pre-Reestructurados Internos</b>
            </div>
            <telerik:RadGrid runat="server" ID="gridPreReestructurados" AllowFilteringByColumn="true" AllowSorting="true" AutoGenerateColumns="false" AllowPaging="true" >
                <ClientSettings>
                    <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="2"></Scrolling>
                </ClientSettings>
                
                <MasterTableView CommandItemDisplay="Top">
                     <CommandItemTemplate>
                          <div class="w3-col s10 w3-right" style="height: 100%;">
                        <asp:LinkButton ID="LinkButton4" runat="server" CommandName="RebindGrid" Style="text-decoration: none" CssClass="w3-hover-shadow w3-padding w3-round"><img style="border:0px;vertical-align:middle;height:20px;" alt="" src="Imagenes/recargar.png?v=1.2"/>Recargar </asp:LinkButton>
                            </div>
                 </CommandItemTemplate>
                    <Columns>
                        <telerik:GridTemplateColumn HeaderText="Cancelar Pre-Reestructura" AllowFiltering="false">
                            <ItemTemplate>
                                <telerik:RadButton runat="server" CommandName="onCancel" Text="Cancelar"  Enabled='<%# IIf(Eval("Estatus") <> "CANCELADA" And Eval("instancia") = tmpUSUARIO("CAT_LO_INSTANCIA"), True, False) %>' >
                                </telerik:RadButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderText="Fecha Cancelacion" DataField="DTECANCELA" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"></telerik:GridBoundColumn> 
                          <telerik:GridTemplateColumn HeaderText="Aprobar Pre-Reestructura" AllowFiltering="false">
                            <ItemTemplate>
                                <telerik:RadButton runat="server" CommandName="onAprobar" Text="Concluir" Enabled='<%# IIf(Eval("Estatus") = "PENDIENTE" And Eval("instancia") = tmpUSUARIO("CAT_LO_INSTANCIA"), True, False) %>'>
                                </telerik:RadButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderText="Fecha Conclucion" DataField="DTECONCLUYE" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"></telerik:GridBoundColumn> 
                        <telerik:GridTemplateColumn HeaderText="Visualizar Pre-Reestructura" AllowFiltering="false">
                            <ItemTemplate>
                                <telerik:RadButton runat="server" CommandName="onVizualizar" Text="Visualizar" >
                                </telerik:RadButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderText="Fecha Captura" DataField="DTECAPTURA" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"></telerik:GridBoundColumn> 
                        <telerik:GridBoundColumn HeaderText="Credito" DataField="Credito" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"></telerik:GridBoundColumn> 
                        <telerik:GridBoundColumn HeaderText="Estatus" DataField="estatus" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"></telerik:GridBoundColumn> 
                        <telerik:GridBoundColumn HeaderText="Identificador" DataField="IDENTIFICADOR" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="instancia" DataField="instancia" UniqueName="INSTANCIA" AllowFiltering="false"></telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
            <br />
            <div class="w3-pnl w3-blue w3-center">
                <b>Creditos Pre-Reestructurados Externos</b>
            </div>
            <telerik:RadGrid runat="server" ID="gridAjenos" AllowFilteringByColumn="true" AllowSorting="true" AutoGenerateColumns="false" AllowPaging="true">
                <ClientSettings>
                    <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="2"></Scrolling>
                </ClientSettings>
                <MasterTableView CommandItemDisplay="Top">
                    <CommandItemTemplate>
                          <div class="w3-col s10 w3-right" style="height: 100%;">
                        <asp:LinkButton ID="LinkButton4" runat="server" CommandName="RebindGrid" Style="text-decoration: none" CssClass="w3-hover-shadow w3-padding w3-round"><img style="border:0px;vertical-align:middle;height:20px;" alt="" src="Imagenes/recargar.png?v=1.2"/>Recargar </asp:LinkButton>
                            </div>
                 </CommandItemTemplate>
                    <Columns>
                        <telerik:GridTemplateColumn HeaderText="Cancelar Pre-Reestructura" ShowFilterIcon="false" AllowFiltering="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            <ItemTemplate>
                                <telerik:RadButton runat="server" CommandName="onCancel" Text="Cancelar" Enabled='<%# IIf(Eval("Estatus") <> "CANCELADA" And Eval("instancia") = tmpUSUARIO("CAT_LO_INSTANCIA"), True, False) %>' >
                                </telerik:RadButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderText="Fecha Cancelacion" DataField="DTECANCELA" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"></telerik:GridBoundColumn> 
                        <telerik:GridTemplateColumn HeaderText="Aprobar Pre-Reestructura" AllowFiltering="false">
                            <ItemTemplate>
                                <telerik:RadButton runat="server" CommandName="onAprobar" Text="Concluir"  Enabled='<%# IIf(Eval("Estatus") = "PENDIENTE" And Eval("instancia") = tmpUSUARIO("CAT_LO_INSTANCIA"), True, False) %>'>
                                </telerik:RadButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderText="Fecha Conclucion" DataField="DTECONCLUYE" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"></telerik:GridBoundColumn> 
                        <telerik:GridTemplateColumn HeaderText="Visualizar Pre-Reestructura" AllowFiltering="false">
                            <ItemTemplate>
                                <telerik:RadButton runat="server" CommandName="onVizualizar" Text="Visualizar" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                </telerik:RadButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderText="Fecha Captura" DataField="DTECAPTURA" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"></telerik:GridBoundColumn> 
                        <telerik:GridBoundColumn HeaderText="Credito" DataField="Credito" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"></telerik:GridBoundColumn> 
                        <telerik:GridBoundColumn HeaderText="Nombre" DataField="Nombre" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"></telerik:GridBoundColumn> 
                        <telerik:GridBoundColumn HeaderText="Estatus" DataField="estatus" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"></telerik:GridBoundColumn> 
                        <telerik:GridBoundColumn HeaderText="Identificador" DataField="IDENTIFICADOR" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"></telerik:GridBoundColumn> 
                        <telerik:GridBoundColumn HeaderText="instancia" DataField="instancia" UniqueName="INSTANCIA"  AllowFiltering="false"></telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
            <telerik:RadWindow runat="server" ID="WinCancelar" Modal="true" Behaviors="Close" Height="200px" Width="400px" Title="Cancelar">
                <ContentTemplate>
                    <telerik:RadAjaxPanel runat="server" ID="pnlito">
                    <div>
                        <label>Motivo Cancelación</label>
                        <telerik:RadTextBox runat="server" ID="txtmotivo" Width="80%" TextMode="MultiLine" Resize="Both"  EmptyMessage="Escribe el motivo de cancelacion" AutoPostBack="true"></telerik:RadTextBox><br /></div>
                    <telerik:RadLabel runat="server" ID="lbloc" Visible="false"></telerik:RadLabel>
                    <br />
                    <br />
                    <br />
                    <telerik:RadButton runat="server" ID="btnCancelar" Text="Cancelar" Enabled="false"></telerik:RadButton>
                        </telerik:RadAjaxPanel>
                </ContentTemplate>
            </telerik:RadWindow>
             <telerik:RadWindowManager ID="RadAviso" runat="server"></telerik:RadWindowManager>
             <telerik:RadNotification ID="Notificacion2" runat="server" Position="Center" Width="330" Height="160"
            Animation="Fade" EnableRoundedCorners="true" EnableShadow="true" Style="z-index: 100000">
        </telerik:RadNotification>
        </telerik:RadAjaxPanel>
        <telerik:RadAjaxPanel runat="server" ID="pnlReestructura" LoadingPanelID="pnlLoading" Visible="false">
           
            <telerik:RadLabel ID="LblCat_Lo_Usuario" runat="server"></telerik:RadLabel>
            <div class="w3-container w3-blue w3-center">
                <b>Pre-Solicitud de Reestructura</b>
            </div>
            <br />
            <div class="w3-row-padding">
                <div class="w3-col s12 m6">
                    <label>Numero(s) de cliente</label>
                    <telerik:RadSearchBox runat="server" ID="rsb1" EmptyMessage="Buscar..." OnSearch="rsb1_Search" MaxResultCount="25" Style="width: 100%" DropDownSettings-Width="100%" HighlightFirstMatch="true" >
                        <WebServiceSettings Path="PreReestructura.aspx" Method="GetResults" />
                        <DropDownSettings Width="350px" Height="450px" CssClass="w3-card-4">
                            <HeaderTemplate>
                                <table>
                                    <tr>
                                        <td align="center">Credito</td>
                                        <td align="center">Nombre</td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%--<ul>
                                    <li>
                                        <%# DataBinder.Eval(Container.DataItem, "Credito") %></li>
                                    <li>
                                        <%# DataBinder.Eval(Container.DataItem, "Nombre") %></li>
                                </ul>--%>
                                <telerik:RadCheckBox runat="server" ID="CBP"></telerik:RadCheckBox>
                            </ItemTemplate>
                        </DropDownSettings>
                    </telerik:RadSearchBox>

                    <%--<telerik:RadComboBox ID="DDLNumero_cliente" DataTextField="stex" DataValueField="sval" Filter="Contains" ShowMoreResultsBox="true" EnableVirtualScrolling="true" runat="server" Width="100%" CheckBoxes="true" AutoPostBack="true" EmptyMessage="Seleccione" Localization-CheckAllString="Todos" Localization-AllItemsCheckedString="Todos los items seleccionados">
                    </telerik:RadComboBox>--%>

                </div>
                <div class="w3-col s12 m6">

                    <label>Numero de Cliente</label>
                    <telerik:RadnumericTextBox ID="TxtNumero_cliente" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" runat="server" CssClass="w3-input" AutoPostBack="true" Width="100%" Enabled="false">
                    </telerik:RadnumericTextBox>
                </div>
            </div>

            <div class="w3-row-padding">
                <div class="w3-col s12 m6 w3-center">
                    <label>¿Crédito Externo?</label>
                    <telerik:RadCheckBox runat="server" ID="CBCreditoExterno"></telerik:RadCheckBox>
                </div>
                <div class="w3-col s12 m6">
                    <label>Nombre</label>
                    <telerik:RadTextBox ID="TxtNombre_cliente" runat="server" Width="100%" CssClass="w3-input" AutoPostBack="true" Enabled="false">
                         <ClientEvents OnKeyPress="AlphabetOnly" />
                    </telerik:RadTextBox>
                </div>
            </div>
            <br />
            <div style="width: 100%; max-width: 100%; overflow: auto;">
                <telerik:RadGrid runat="server" ID="gridInformacion" Visible="false">
                    <MasterTableView>
                        <Columns>
                            <telerik:GridButtonColumn HeaderText="Quitar"  CommandName="Quitar" Text="X"></telerik:GridButtonColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                <telerik:RadTextBox runat="server" ID="TxtTitular" Visible="false" Label="Titular" Width="40%" ReadOnly="true" ></telerik:RadTextBox>
            </div>
            <br />
            <div class="w3-row-padding">
                <div class="w3-col s12 m3">
                    <label>Sucursal</label>
                    <telerik:RadDropDownList ID="DDLSucursal" runat="server"
                        Width="100%">
                    </telerik:RadDropDownList>
                </div>
                <div class="w3-col s12 m3">
                    <label>Monto</label>
                    <telerik:RadNumericTextBox ID="TxtMonto" runat="server" Width="100%" CssClass="w3-input">
                    </telerik:RadNumericTextBox>
                </div>
                <div class="w3-col s12 m3" runat="server" visible="false">
                    <label>ClasificaciÃ³n</label>
                    <telerik:RadDropDownList ID="DDLClasificacion" runat="server" Width="100%" DefaultMessage="Seleccione" >
                        <Items>
                            <telerik:DropDownListItem Text="CONSUMO" Value="CONSUMO" />
                            <telerik:DropDownListItem Text="COMERCIAL" Value="COMERCIAL" />
                            <telerik:DropDownListItem Text="VIVIENDA " Value="VIVIENDA " />
                        </Items>
                    </telerik:RadDropDownList>
                </div>
                <div class="w3-col s12 m3">
                    <label>Plazo (Meses)</label>
                    <telerik:RadDropDownList ID="DDL_plazo" runat="server" Width="100%" >
                    </telerik:RadDropDownList>
                </div>
            </div>
            <div class="w3-row-padding">
                <div class="w3-col s12 m3">
                    <label>Tasa</label>
                    <telerik:RadNumericTextBox ID="TxtTaza" runat="server" Width="100%" CssClass="w3-input">
                    </telerik:RadNumericTextBox>
                </div>
                <div class="w3-col s12 m3">
                    <label>Frecuencia de pago</label>
                    <telerik:RadDropDownList ID="DDLFrecuenciaPago" runat="server" Width="100%" DefaultMessage="Seleccione">
                        <Items>
                            <telerik:DropDownListItem Text="Mensual" Value="M" />
                            <telerik:DropDownListItem Text="Quincenal" Value="Q" />
                            <telerik:DropDownListItem Text="Catorcenal" Value="C" />
                            <telerik:DropDownListItem Text="Semanal" Value="S" />
                             <telerik:DropDownListItem Text="Periodo" Value="P" />
                            <telerik:DropDownListItem Text="Bimestral" Value="B" />
                            <telerik:DropDownListItem Text="Trimestral" Value="T" />
                            <telerik:DropDownListItem Text="TetraMestral" Value="R" />
                            <telerik:DropDownListItem Text="Semestral" Value="E" />
                            <telerik:DropDownListItem Text="Anual" Value="A" />
                            <telerik:DropDownListItem Text="Libres" Value="L" />

                        </Items>
                    </telerik:RadDropDownList>
                </div>
                <div class="w3-col s12 m3">
                    <label>Requiere Garantía Liquida</label>
                    <telerik:RadDropDownList ID="DDLGarantiaLiquida" runat="server" Width="100%" AutoPostBack="true" DefaultMessage="Seleccione">
                        <Items>
                            <telerik:DropDownListItem Text="SI" Value="S" />
                            <telerik:DropDownListItem Text="NO" Value="N" />
                        </Items>
                    </telerik:RadDropDownList>
                </div>
                <div class="w3-col s12 m3">
                    <asp:Panel runat="server" ID="pnlMontoLiquido" Visible="false">
                        <label>Monto</label>
                        <telerik:RadNumericTextBox ID="TxtMontoLiquido" runat="server" Width="100%" CssClass="w3-input">
                        </telerik:RadNumericTextBox>
                    </asp:Panel>
                </div>
            </div>
            <div class="w3-row-padding">
                <div class="w3-col s12 m3">
                    <label>Requiere Garantía Prendaria</label>
                    <telerik:RadDropDownList ID="DDLGarantiaPrendataria" runat="server" Width="100%" DefaultMessage="Seleccione">
                        <Items>
                            <telerik:DropDownListItem Text="SI" Value="S" />
                            <telerik:DropDownListItem Text="NO" Value="N" />
                        </Items>
                    </telerik:RadDropDownList>
                </div>
                <div class="w3-col s12 m3">
                    <label>Requiere Garantía Hipotecaria</label>
                    <telerik:RadDropDownList ID="DDLGarantiaHipotecaria" runat="server" Width="100%" DefaultMessage="Seleccione">
                        <Items>
                            <telerik:DropDownListItem Text="SI" Value="S" />
                            <telerik:DropDownListItem Text="NO" Value="N" />
                        </Items>
                    </telerik:RadDropDownList>
                </div>
                <div class="w3-col s12 m3">
                    <label>Tiene tratamiento de nómina</label>
                    <telerik:RadDropDownList ID="DDLNomina" runat="server" Width="100%" AutoPostBack="true" DefaultMessage="Seleccione">
                        <Items>
                            <telerik:DropDownListItem Text="SI" Value="S" />
                            <telerik:DropDownListItem Text="NO" Value="N" />
                        </Items>
                    </telerik:RadDropDownList>
                </div>
                <div class="w3-col s12 m3">
                    <asp:Panel runat="server" ID="pnlNumeroEmpleado" Visible="false">
                        <label>Número de Empleado</label>
                        <telerik:RadTextBox ID="TxtNumeroEmpleado" runat="server" Width="100%" CssClass="w3-input">
                        </telerik:RadTextBox>
                    </asp:Panel>
                </div>
            </div>
            <div class="w3-row-padding">
                <div class="w3-col s12 m3">
                    <asp:Panel runat="server" ID="pnlNumeroConvenio" Visible="false">
                        <label>Número Convenio</label>
                        <telerik:RadTextBox ID="TxtNumeroConvenio" runat="server" Width="100%" CssClass="w3-input">
                        </telerik:RadTextBox>
                    </asp:Panel>
                </div>
                <div class="w3-col s12 m3">
                    <label>Observaciones</label>
                    <telerik:RadTextBox ID="TxtObservaciones" runat="server" Width="100%" CssClass="w3-input" TextMode="MultiLine" Resize="Vertical" MaxLength="500">
                        
                    </telerik:RadTextBox>
                </div>
                <div class="w3-col s12 m3">
                    <label>Tiene Convenio Judicial</label>
                    <telerik:RadDropDownList ID="DDLConvenio" runat="server" Width="100%" AutoPostBack="true" DefaultMessage="Seleccione">
                        <Items>
                            <telerik:DropDownListItem Text="Seleccione" />
                            <telerik:DropDownListItem Text="SI" Value="S" />
                            <telerik:DropDownListItem Text="NO" Value="N" />
                        </Items>
                    </telerik:RadDropDownList>
                </div>
                <div class="w3-col s12 m3">
                    <asp:Panel runat="server" ID="pnlNoExpediente" Visible="false">
                        <label>No Expediente</label>
                        <telerik:RadDropDownList ID="TxtNoExpediente" runat="server" Width="100%"  >
                            <Items>
                                <telerik:DropDownListItem Text="Seleccione" />
                                <telerik:DropDownListItem Text="Expediente 1" value="Expediente 1"/>
                                <telerik:DropDownListItem Text="Expediente 2" value="Expediente 2"/>
                                <telerik:DropDownListItem Text="Expediente 3" value="Expediente 3"/>

                            </Items>
                        </telerik:RadDropDownList>
                    </asp:Panel>
                </div>
            </div>
            <div class="w3-row-padding">
                <div class="w3-col s12 m3">
                    <asp:Panel runat="server" ID="pnlJuzgado" Visible="false">
                        <label>Juzgado</label>
                        <telerik:RadDropDownList ID="TxtJuzgado" runat="server" Width="100%" >
                             <Items>
                                <telerik:DropDownListItem Text="Seleccione" />

                                <telerik:DropDownListItem Text="Juzgado 1" value="Juzgado 1"/>
                                <telerik:DropDownListItem Text="Juzgado 2" value="Juzgado 2"/>
                                <telerik:DropDownListItem Text="Juzgado 3" value="Juzgado 3"/>

                            </Items>
                        </telerik:RadDropDownList>
                    </asp:Panel>
                </div>
                <div class="w3-col s12 m3">
                    <asp:Panel runat="server" ID="PanelCancela" Visible="false">
                    <label>Motivo de Cancelación</label>
                    <telerik:RadTextBox runat="server" ID="TxtCancelacion" TextMode="MultiLine" Resize="Vertical"  Enabled="false"  Width="100%" CssClass="w3-input"></telerik:RadTextBox> 
                        </asp:Panel>
                </div>
                <div class="w3-col s12 m3"></div>
                <div class="w3-col s12 m3"></div>
            </div>
            <div class="w3-col s12 m12 l6">
            </div>
            <br />
            <div class="w3-block w3-center w3-margin">
                <telerik:RadButton ID="BtnGuardar" runat="server" Text="Guardar" CssClass="w3-btn w3-green w3-text-white w3-hover-shadow" Skin="">
                </telerik:RadButton>
            </div>
            <br />
        </telerik:RadAjaxPanel>
        <telerik:RadNotification ID="Notificacion" runat="server" Position="Center" Width="330" Height="160"
            Animation="Fade" EnableRoundedCorners="true" EnableShadow="true" Style="z-index: 100000">
        </telerik:RadNotification>
       
    </form>
</body>

</html>
