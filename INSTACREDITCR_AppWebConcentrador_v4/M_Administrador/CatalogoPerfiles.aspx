<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="./MasterPage.master" CodeFile="CatalogoPerfiles.aspx.vb" Inherits="CatalogoPerfiles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHMaster" runat="Server">
    <asp:HiddenField ID="HidenUrs" runat="server" />
    <telerik:RadAjaxPanel ID="UPNLPerfiles" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <table>
            <tr class="Titulos">
                <td>Perfiles</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <telerik:RadGrid ID="RGVPerfiles" runat="server"  RenderMode="Lightweight" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true"
                        ClientSettings-EnableRowHoverStyle="true" OnNeedDataSource="RGVPerfiles_NeedDataSource" Culture="es-MX">

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
                </td>
            </tr>
        </table>

    </telerik:RadAjaxPanel>
    <telerik:RadAjaxPanel ID="RDPanelPermisos" runat="server" Visible="false">
        <table class="Table">
            <tr class="Titulos">
                <td>Permisos por perfil</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr align="center">
                <td>
                    <table style="width: 100%">
                        <tr style="vertical-align: top; text-align: left;">
                            <td style="width: 33%">
                                <telerik:RadAjaxPanel ID="UPNLGestion" runat="server">
                                    <telerik:RadComboBox ID="ddcPermisosGestion" runat="server" AutoPostBack="true" CheckBoxes="true" DropDownAutoWidth="Enabled" EmptyMessage="Seleccione Permisos" EnableCheckAllItemsCheckBox="true" Font-Size="Small" Label="Permisos Gestion"  Width="250px">
                                        <Localization AllItemsCheckedString="Todos Los Elementos Seleccionados" CheckAllString="Todos" />
                                    </telerik:RadComboBox>
                                </telerik:RadAjaxPanel>
                            </td>

                            <td style="width: 33%">
                                <telerik:RadAjaxPanel ID="UPNLPantallasGestion" runat="server">

                                    <telerik:RadComboBox ID="ddcPermisosPantallasGestion" runat="server" AutoPostBack="true" CheckBoxes="true" DropDownAutoWidth="Enabled" EmptyMessage="Seleccione Permisos" EnableCheckAllItemsCheckBox="true" Font-Size="Small" Label="Pantallas Gestion"  Width="250px">
                                        <Localization AllItemsCheckedString="Todos Los Elementos Seleccionados" CheckAllString="Todos" />
                                    </telerik:RadComboBox>
                                </telerik:RadAjaxPanel>
                            </td>
                            <td style="width: 33%">
                                <telerik:RadAjaxPanel ID="UPNLPermisosAdmin" runat="server">
                                    <telerik:RadComboBox ID="ddcPermisosAdministrador" runat="server" AutoPostBack="true" CheckBoxes="true" DropDownAutoWidth="Enabled" EmptyMessage="Seleccione Permisos" EnableCheckAllItemsCheckBox="true" Font-Size="Small" Label="Administrador"  Width="250px">
                                        <Localization AllItemsCheckedString="Todos Los Elementos Seleccionados" CheckAllString="Todos" />
                                    </telerik:RadComboBox>
                                </telerik:RadAjaxPanel>
                            </td>
                        </tr>
                        <tr class="Izquierda">
                            <td style="vertical-align: top" colspan="4" class="Izquierda">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 33%">
                                <telerik:RadAjaxPanel ID="UPNLMenuBackOffice" runat="server">
                                    <telerik:RadComboBox ID="ddcPermisosBackOffice" runat="server" AutoPostBack="true" CheckBoxes="true" DropDownAutoWidth="Enabled" EmptyMessage="Seleccione Permisos" EnableCheckAllItemsCheckBox="true" Font-Size="Small" Label="Backoffice"  Width="250px">
                                        <Localization AllItemsCheckedString="Todos Los Elementos Seleccionados" CheckAllString="Todos" />
                                    </telerik:RadComboBox>
                                </telerik:RadAjaxPanel>
                            </td>
                            <td style="width: 33%">
                                <telerik:RadAjaxPanel ID="UPNLMenuMovil" runat="server">
                                    <telerik:RadComboBox ID="ddcPermisosMovil" runat="server" AutoPostBack="true" CheckBoxes="true" DropDownAutoWidth="Enabled" EmptyMessage="Seleccione Permisos" EnableCheckAllItemsCheckBox="true" Font-Size="Small" Label="Móvil"  Width="250px">
                                        <Localization AllItemsCheckedString="Todos Los Elementos Seleccionados" CheckAllString="Todos" />
                                    </telerik:RadComboBox>
                                </telerik:RadAjaxPanel>
                            </td>
                            <td style="width: 33%">
                                <telerik:RadAjaxPanel ID="UPNLMenuJudicial" runat="server">
                                    <telerik:RadComboBox ID="ddcPermisosJudicial" runat="server" AutoPostBack="true" CheckBoxes="true" DropDownAutoWidth="Enabled" EmptyMessage="Seleccione Permisos" EnableCheckAllItemsCheckBox="true" Font-Size="Small" Label="Judicial"  Width="250px">
                                        <Localization AllItemsCheckedString="Todos Los Elementos Seleccionados" CheckAllString="Todos" />
                                    </telerik:RadComboBox>
                                </telerik:RadAjaxPanel>
                            </td>
                        </tr>
                        <tr class="Izquierda">
                            <td style="vertical-align: top" colspan="4" class="Izquierda">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 33%">
                                <telerik:RadAjaxPanel ID="UPNLMenuIndicadores" runat="server">
                                    <telerik:RadComboBox ID="ddcPermisosIndicadores" runat="server" AutoPostBack="true" CheckBoxes="true" DropDownAutoWidth="Enabled" EmptyMessage="Seleccione Permisos" EnableCheckAllItemsCheckBox="true" Font-Size="Small" Label="Reportes"  Width="250px">
                                        <Localization AllItemsCheckedString="Todos Los Elementos Seleccionados" CheckAllString="Todos" />
                                    </telerik:RadComboBox>
                                </telerik:RadAjaxPanel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

        <table class="Table">
            <tr>
                <td class="Derecha">
                    <asp:Button ID="BtnGuardar" runat="server" CssClass="Botones" />
                </td>
                <td>
                    <asp:Button ID="BtnCancelar" runat="server" Text="Cancelar" CssClass="Botones" />
                </td>
            </tr>
        </table>
    </telerik:RadAjaxPanel>
    <asp:Button ID="BtnModalMsj" runat="server" Style="visibility: hidden" />
    <asp:Panel ID="PnlModalMsj" runat="server" CssClass="ModalMsj" Style="display: none">
        <div class="heading">
            Aviso
        </div>
        <div class="CuerpoMsj">
            <table class="Table">
                <tr align="center">
                    <td>&nbsp;</td>
                </tr>
                <tr align="center">
                    <td>
                        <asp:Label ID="LblMsj" runat="server" CssClass="LblDesc"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="BtnCancelarMsj" runat="server" CssClass="button green close" Text="Aceptar" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <telerik:RadWindowManager ID="RadAviso" runat="server"></telerik:RadWindowManager>

</asp:Content>
