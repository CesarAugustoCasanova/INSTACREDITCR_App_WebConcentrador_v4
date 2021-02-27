<%@ Page Language="VB" MasterPageFile="./MasterPage.master" AutoEventWireup="false" CodeFile="CatalogoCEspeciales.aspx.vb" Inherits="CatalogoCEspeciales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHMaster" runat="Server">
    <script languaje="javascript" type="text/javascript">

        function confirmCallbackFn(arg) {
            if (arg) {

                __doPostBack('ctl00$CPHMaster$BtnAceptarConfirmacion', '')

            }
        }
    </script>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="UPNLGeneral" runat="server" UpdatePanelCssClass="w3-center" CssClass="w3-center" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1" Style="overflow: auto;">
        <div class="w3-container Titulos">
            Campañas Especiales
        </div>
        <div class="w3-container" style="margin: auto">
            <telerik:RadButton runat="server" ID="BtnExcel" Text="Carga desde csv(Excel)" CssClass="w3-margin"></telerik:RadButton>
        </div>
        <table class="Table">
            <tr align="center">
                <td style="font-size: 10px">
                    <telerik:RadGrid runat="server" ID="RGEspeciales" AutoGenerateColumns="False" ClientSettings-EnableRowHoverStyle="true" Width="1200px" AllowPaging="True" PageSize="5">
                        <MasterTableView CommandItemDisplay="Top" EditFormSettings-PopUpSettings-KeepInScreenBounds="true" DataKeyNames="Nombre">
                            <CommandItemSettings AddNewRecordText="Añadir Campaña" RefreshText="Recargar" />
                            <Columns>
                                <telerik:GridEditCommandColumn ItemStyle-Width="5px" ButtonType="FontIconButton"></telerik:GridEditCommandColumn>
                                <telerik:GridButtonColumn ItemStyle-Width="5px" HeaderText="Eliminar" CommandName="Delete" ConfirmDialogType="RadWindow" ConfirmTextFormatString="¿Desea elminiar '{0}'?" ConfirmTextFields="Nombre" ConfirmTitle="Confirmar"></telerik:GridButtonColumn>
                             <%--   <telerik:GridButtonColumn ButtonType="ImageButton" HeaderText="Asignacion Manual" CommandName="Select" ImageUrl="./Imagenes/ImgOk.png" ItemStyle-Width="16px"></telerik:GridButtonColumn>--%>
                                <telerik:GridBoundColumn UniqueName="Nombre" HeaderText="Nombre" DataField="NOMBRE"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Vigencia" HeaderText="Fecha vigencia" DataField="VIGENCIA"></telerik:GridBoundColumn>
                                <%--<telerik:GridBoundColumn UniqueName="Instancia" HeaderText="Instancia" DataField="INSTANCIA"></telerik:GridBoundColumn>--%>
                                <telerik:GridBoundColumn UniqueName="Saldo" HeaderText="Saldo" DataField="SALDO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Descripcion" HeaderText="Descripción" DataField="DESCRIPCION"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Bonificacion" HeaderText="% Condonacion comisión" DataField="BONIFICACION" DataFormatString="{0}%"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="CondonacionM" HeaderText="% Condonacion Moratorio" DataField="CONDONACIONM" DataFormatString="{0}%"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="CondonacionN" HeaderText="% Condonacion Normal" DataField="CONDONACIONN" DataFormatString="{0}%"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="CondonacionC" HeaderText="% Condonacion Capital" DataField="CONDONACIONC" DataFormatString="{0}%"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Externo" HeaderText="% Despachos Externos" DataField="EXTERNO" DataFormatString="{0}%"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="MontoE" HeaderText="Monto Campaña" DataField="MONTOE" DataFormatString="${0}"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Recuperacion" HeaderText="% Recuperacion" DataField="RECUPERACION" DataFormatString="{0}%"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="MontoR" HeaderText="Monto Real Recuperacion" DataField="MONTOR" DataFormatString="${0}"></telerik:GridBoundColumn>
                            </Columns>
                            <EditFormSettings UserControlName="./Especiales.ascx" EditFormType="WebUserControl">
                                <EditColumn UniqueName="EditCommandColumn1">
                                </EditColumn>
                            </EditFormSettings>
                            <PagerStyle PageSizeLabelText="Elementos por página:" />
                        </MasterTableView>
                    </telerik:RadGrid>
                </td>
            </tr>
        </table>
        <telerik:RadWindow runat="server" ID="winManual" Behaviors="Close" Height="600px" Width="850px" Title="Asignacion Manual">
            <ContentTemplate>
                <telerik:RadAjaxPanel runat="server" ID="PnlWin" LoadingPanelID="RadAjaxLoadingPanel1">
                    <table>
                        <tr align="Center">
                            <td style="align-content: center;" colspan="8">
                                <telerik:RadLabel runat="server" ID="LblNombreCampaña"></telerik:RadLabel>
                            </td>
                        </tr>
                        <tr>
                            <td rowspan="4">
                                <telerik:RadListBox runat="server" ID="LBCreditos" CheckBoxes="true" ShowCheckAll="true" Localization-CheckAll="Todos los Creditos" Height="450px"></telerik:RadListBox>
                                <br />
                                <telerik:RadButton runat="server" ID="BtnCargaManual" Skin="Web20" Text="Asignar Créditos"></telerik:RadButton>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <telerik:RadListBox runat="server" ID="LBFiltro" CheckBoxes="true" AllowDelete="true" ButtonSettings-ShowDelete="true"></telerik:RadListBox>
                            </td>
                            <td>
                                <telerik:RadButton runat="server" ID="BtnAplicar" Text="Aplicar Filtro"></telerik:RadButton>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <telerik:RadLabel runat="server" ID="LblCampo" Text="Campo"></telerik:RadLabel>
                                <br />
                                <telerik:RadDropDownList runat="server" ID="DDLCampo" AutoPostBack="true"></telerik:RadDropDownList>
                            </td>
                            <td>
                                <telerik:RadLabel runat="server" ID="LblOperador" Text="Operador" Visible="false"></telerik:RadLabel>
                                <br />
                                <telerik:RadDropDownList runat="server" ID="DDLOperador" AutoPostBack="true" Visible="false">
                                </telerik:RadDropDownList>
                            </td>
                            <td>
                                <telerik:RadLabel runat="server" ID="LblValores" Text="Valores" Visible="false"></telerik:RadLabel>
                                <br />
                                <telerik:RadTextBox runat="server" ID="TxtValores" Visible="false"></telerik:RadTextBox>
                                <telerik:RadComboBox runat="server" ID="CBVAlores" Visible="false" CheckBoxes="true"></telerik:RadComboBox>
                                <telerik:RadDatePicker runat="server" ID="DPValores" Visible="false"></telerik:RadDatePicker>
                                <telerik:RadTextBox runat="server" ID="TxtValores2" Visible="false" Label=" Y "></telerik:RadTextBox>
                                <telerik:RadDatePicker runat="server" ID="DPValores2" Visible="false"></telerik:RadDatePicker>
                            </td>
                            <td>
                                <telerik:RadLabel runat="server" ID="LblConector" Text="Conector" Visible="false"></telerik:RadLabel>
                                <br />
                                <telerik:RadDropDownList runat="server" ID="DDLConector" AutoPostBack="true" Visible="false">
                                    <Items>
                                        <telerik:DropDownListItem Text="Seleccione" Value="0" />
                                        <telerik:DropDownListItem Text="Y" Value="and" />
                                        <telerik:DropDownListItem Text="O" Value="or" />
                                    </Items>
                                </telerik:RadDropDownList>
                            </td>
                        </tr>

                    </table>
                </telerik:RadAjaxPanel>
            </ContentTemplate>
        </telerik:RadWindow>

        <telerik:RadWindow runat="server" ID="WinExcel" Behaviors="Close" Height="400px" Width="650px" Title="Asignacion Excel">
            <ContentTemplate>
                <telerik:RadAjaxPanel runat="server" ID="RadAjaxPanel1" LoadingPanelID="RadAjaxLoadingPanel1">
                    <br />
                    <telerik:RadLabel runat="server" ID="Lblce" Text="Selecciona archivo a cargar"></telerik:RadLabel>
                    <telerik:RadAsyncUpload runat="server" ID="AUCarga" AllowedFileExtensions="csv,txt" MultipleFileSelection="Disabled"></telerik:RadAsyncUpload>
                    <telerik:RadButton runat="server" ID="BtnCarga" Text="Carga"></telerik:RadButton>
                    <br />
                    <br />
                    <telerik:RadGrid runat="server" ID="GVResultado"></telerik:RadGrid>
                </telerik:RadAjaxPanel>
            </ContentTemplate>
        </telerik:RadWindow>

        <asp:HiddenField ID="HidenUrs" runat="server" />
        <asp:Button runat="server" ID="BtnAceptarConfirmacion" Visible="true" Height="1px" Style="display: none;" />
        <telerik:RadToolTip runat="server" ID="TTCarga" ShowEvent="OnMouseOver" TargetControlID="BtnCarga">
            <table>
                <tr>
                    <td>Layout</td>
                </tr>
                <tr>
                    <td>Crédito,</td>
                    <td>Campaña</td>
                    <td>(Archivo csv delimitado por comas)</td>
                </tr>
            </table>
        </telerik:RadToolTip>

        <telerik:RadWindowManager ID="RadAviso" runat="server">
            <Localization OK="Aceptar" />
        </telerik:RadWindowManager>

    </telerik:RadAjaxPanel>
</asp:Content>
