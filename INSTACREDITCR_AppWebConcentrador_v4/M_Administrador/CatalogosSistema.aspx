<%@ Page Title="" Language="VB" MasterPageFile="MasterPage.master" AutoEventWireup="false" CodeFile="CatalogosSistema.aspx.vb" Inherits="CatalogosSistema" %>

<asp:Content ID="CCatalogos" ContentPlaceHolderID="CPHMaster" runat="Server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" ></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="UPNLGeneral" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" CssClass="w3-container w3-centered" Width="100%">
        <table class="Table">
            <tr class="Titulos">
                <td>Catálogos De Sistema
                </td>
            </tr>
            <tr>
                <td>

                    <asp:Panel ID="PnlNuevo" runat="server" Visible="true">
                        <table class="Table">
                            <tr>
                                <td>

                                    <asp:Label ID="LblCatalogo" runat="server" Text="Catálogo:" CssClass="LblDesc"></asp:Label>
                                    <asp:DropDownList ID="DdlTipo" runat="server" CssClass="DdlDesc" AutoPostBack="true">
                                        <asp:ListItem>Seleccione</asp:ListItem>
                                        <asp:ListItem Value="CAT_CAPACIDAD">Capacidad cuentas usuarios y agencia</asp:ListItem>
                                        <asp:ListItem Value="CAT_VARIABLES">Holguras</asp:ListItem>
                                        <asp:ListItem Value="CAT_INSTANCIAS">Instancias</asp:ListItem>
                                        <asp:ListItem Value="CAT_DOMINIOS_SEG">Dominios seguros</asp:ListItem>
                                        <asp:ListItem Value="CAT_LUGAR_PAGO">Lugar De Pago</asp:ListItem>
                                         <asp:ListItem Value="CAT_PARENTESCO">Parentesco</asp:ListItem>
                                       
                                    </asp:DropDownList>

                                </td>
                            </tr>
                            <tr>
                                <td>                                  
                                </td>
                            </tr>
                            <tr align="center">
                                <td align="right">
                                     <telerik:RadAjaxPanel ID="UPNLPagos" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
                                        <telerik:RadGrid ID="RGVLugarPago" runat="server" RenderMode="Lightweight" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true"
                                            ClientSettings-EnableRowHoverStyle="true" OnNeedDataSource="RGVLugarPago_NeedDataSource" >

                                            <MasterTableView Width="100%" CommandItemDisplay="Top" EditFormSettings-PopUpSettings-KeepInScreenBounds="true" DataKeyNames="Id">

                                                <Columns>

                                                    <telerik:GridEditCommandColumn ItemStyle-Width="5px" UniqueName="Expandible">
                                                    </telerik:GridEditCommandColumn>
                                                    <telerik:GridBoundColumn UniqueName="Id" HeaderText="Id" DataField="Id">
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn UniqueName="Descripcion" HeaderText="Descripcion" DataField="Descripcion">
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                                <EditFormSettings UserControlName="Sistema.ascx" EditFormType="WebUserControl">
                                                    <EditColumn UniqueName="EditCommandColumn1">
                                                    </EditColumn>
                                                </EditFormSettings>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </telerik:RadAjaxPanel>
                                    
                                    <telerik:RadAjaxPanel ID="UPNLCapacidad" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
                                        <br />
                                        <br />
                                         &nbsp;&nbsp;&nbsp;&nbsp;
                                        <telerik:RadLabel ID="RadLabel1" runat="server" Text="Capacidad maxima USUARIO: "></telerik:RadLabel>
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <telerik:RadNumericTextBox ID="TxtMaxUsr" runat="server" MinValue="0" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox><br /><br />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <telerik:RadLabel ID="RadLabel2" runat="server" Text="Capacidad maxima AGENCIA: "></telerik:RadLabel>
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <telerik:RadNumericTextBox ID="TxtMaxAGN" runat="server" MinValue="0" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox><br /><br />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <telerik:RadButton ID="BtnGrdCapacidad" runat="server" Text="Guardar"></telerik:RadButton>
                                    </telerik:RadAjaxPanel>

                                   <telerik:RadAjaxPanel ID="UPNLHolguras" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
                                        <br />
                                        <br />
                                         &nbsp;&nbsp;&nbsp;&nbsp;
                                        <telerik:RadLabel ID="RadLabel3" runat="server" Text="Holgura Fecha: "></telerik:RadLabel>
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <telerik:RadNumericTextBox ID="TxtHolguraFecha" runat="server" MinValue="0" NumberFormat-DecimalDigits="0" ShowSpinButtons="true"></telerik:RadNumericTextBox><label> Días</label><br /><br />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <telerik:RadLabel ID="RadLabel4" runat="server" Text="Holgura Monto: "></telerik:RadLabel>
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <telerik:RadNumericTextBox ID="TxtHolguraMonto" runat="server" MinValue="0" MaxValue="100" Type="Percent" ShowSpinButtons="true" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox><br /><br />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <telerik:RadButton ID="BtnGrdHolguras" runat="server" Text="Guardar"></telerik:RadButton>
                                    </telerik:RadAjaxPanel>
                                    
                                    <telerik:RadAjaxPanel ID="UPNLInstancia" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
                                        <telerik:RadGrid ID="RadGInsEx" runat="server" RenderMode="Lightweight" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true"
                                            ClientSettings-EnableRowHoverStyle="true" OnNeedDataSource="RadGInsEx_NeedDataSource">

                                            <MasterTableView Width="100%" CommandItemDisplay="Top" EditFormSettings-PopUpSettings-KeepInScreenBounds="true" DataKeyNames="Id">
                                                <CommandItemSettings  ShowAddNewRecordButton="false" ShowRefreshButton="true" />
                                                <Columns>

                                                    <telerik:GridEditCommandColumn ItemStyle-Width="5px" UniqueName="Expandible">
                                                    </telerik:GridEditCommandColumn>
                                                    <telerik:GridBoundColumn UniqueName="Id" HeaderText="Id" DataField="Id">
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn UniqueName="Descripcion" HeaderText="Descripcion" DataField="Descripcion">
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                                <EditFormSettings UserControlName="Sistema.ascx" EditFormType="WebUserControl">
                                                    <EditColumn UniqueName="EditCommandColumn1">
                                                    </EditColumn>
                                                </EditFormSettings>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </telerik:RadAjaxPanel>

                                    <telerik:RadAjaxPanel ID="UPNLDominios" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
                                        <telerik:RadGrid ID="RGDominiosEx" runat="server" RenderMode="Lightweight" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true" MasterTableView-AllowPaging="false" ClientSettings-Scrolling-AllowScroll="true" ClientSettings-Scrolling-ScrollHeight="300"
                                            ClientSettings-EnableRowHoverStyle="true" OnNeedDataSource="RGDominiosEx_NeedDataSource" >

                                            <MasterTableView Width="100%" CommandItemDisplay="Top" EditFormSettings-PopUpSettings-KeepInScreenBounds="true" DataKeyNames="Id">

                                                <Columns>

                                                    <telerik:GridEditCommandColumn ItemStyle-Width="5px" UniqueName="Expandible">
                                                    </telerik:GridEditCommandColumn>
                                                    <telerik:GridBoundColumn UniqueName="Id" HeaderText="Id" DataField="Id">
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn UniqueName="Descripcion" HeaderText="Descripcion" DataField="Descripcion">
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                                <EditFormSettings UserControlName="Sistema.ascx" EditFormType="WebUserControl">
                                                    <EditColumn UniqueName="EditCommandColumn1">
                                                    </EditColumn>
                                                </EditFormSettings>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </telerik:RadAjaxPanel>
                                    
                                      <telerik:RadAjaxPanel ID="UPNLParentesco" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
                                        <telerik:RadGrid ID="RGParentesco" runat="server" RenderMode="Lightweight" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true" MasterTableView-AllowPaging="false" ClientSettings-Scrolling-AllowScroll="true" ClientSettings-Scrolling-ScrollHeight="300"
                                            ClientSettings-EnableRowHoverStyle="true" OnNeedDataSource="RGParentesco_NeedDataSource" >

                                            <MasterTableView Width="100%" CommandItemDisplay="Top" EditFormSettings-PopUpSettings-KeepInScreenBounds="true" DataKeyNames="Id">

                                                <Columns>

                                                    <telerik:GridEditCommandColumn ItemStyle-Width="5px" UniqueName="Expandible">
                                                    </telerik:GridEditCommandColumn>
                                                    <telerik:GridBoundColumn UniqueName="Id" HeaderText="Id" DataField="Id">
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn UniqueName="Descripcion" HeaderText="Descripcion" DataField="Descripcion">
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                                <EditFormSettings UserControlName="Sistema.ascx" EditFormType="WebUserControl">
                                                    <EditColumn UniqueName="EditCommandColumn1">
                                                    </EditColumn>
                                                </EditFormSettings>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </telerik:RadAjaxPanel>
                                  
                                </td>                                                      
                            </tr>
                            <tr align="center">
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>

        </table>
        <asp:Button runat="server" ID="BtnAceptarConfirmacion" Visible="true" Height="1px" Style="display: none;" />


        <telerik:RadWindowManager ID="RadAviso" runat="server" >
            <Localization OK="Aceptar" />
        </telerik:RadWindowManager>
    </telerik:RadAjaxPanel>
    <asp:HiddenField ID="HidenUrs" runat="server" />



</asp:Content>

