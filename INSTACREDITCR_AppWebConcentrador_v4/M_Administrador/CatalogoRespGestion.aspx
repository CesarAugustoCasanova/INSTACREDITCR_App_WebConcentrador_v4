<%@ Page Language="VB" MasterPageFile="MasterPage.master" AutoEventWireup="false" CodeFile="CatalogoRespGestion.aspx.vb" Inherits="M_Administrador_CatalogoRespGestion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHMaster" runat="Server">
    <script languaje="javascript" type="text/javascript">

        function confirmCallbackFn(arg) {
            if (arg) {

                __doPostBack('ctl00$CPHMaster$BtnAceptarConfirmacion', '')

            }
        }
    </script>
    <link href="Estilos/ObjAjax.css" rel="stylesheet" />
    <script src="Scripts/scripts.js" type="text/javascript"></script>
    <telerik:RadAjaxLoadingPanel ID="Radpanelcarga" runat="server"></telerik:RadAjaxLoadingPanel>
    
    <telerik:RadAjaxPanel ID="Pnlgen" runat="server" LoadingPanelID="Radpanelcarga">
        <table class="Table">
            <tr class="Titulos">
                <td>Responsables de gestión
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="PnlNuevo" runat="server" Visible="true">
                        <table class="Table">
                            <tr align="center">
                                <td>                                    
                                        <telerik:RadGrid ID="gridRespGestion" runat="server" RenderMode="Lightweight" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true"
                                            ClientSettings-EnableRowHoverStyle="true" OnNeedDataSource="gridRespGestion_NeedDataSource" Culture="es-MX">
                                            <MasterTableView Width="100%" CommandItemDisplay="Top" EditFormSettings-PopUpSettings-KeepInScreenBounds="true" DataKeyNames="Id">
                                                <CommandItemSettings CancelChangesText="Cancelar" RefreshText="Actualizar" SaveChangesText="Guardar" ShowAddNewRecordButton="false"/>
                                                <Columns>
                                                    <telerik:GridEditCommandColumn ItemStyle-Width="5px" UniqueName="Expandible">
                                                    </telerik:GridEditCommandColumn>
                                                    <telerik:GridBoundColumn UniqueName="Id" HeaderText="Id" DataField="Id">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn UniqueName="NombreResponsable" HeaderText="Nombre responsable" DataField="NombreResponsable">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn UniqueName="UsuarioResponsable" HeaderText="Usuario responsable" DataField="UsuarioResponsable">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn UniqueName="Email" HeaderText="Email" DataField="Email">
                                                    </telerik:GridBoundColumn>                                                    
                                                </Columns>
                                                <EditFormSettings UserControlName="ResponsablesGestion.ascx" EditFormType="WebUserControl">
                                                    <EditColumn UniqueName="EditCommandColumn1">
                                                    </EditColumn>
                                                </EditFormSettings>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        <telerik:RadWindowManager ID="RadAviso" runat="server">
            <Localization OK="Aceptar" />
        </telerik:RadWindowManager>
        <asp:Button runat="server" ID="BtnAceptarConfirmacion" Visible="true" Height="1px" Style="display: none;" />
    </telerik:RadAjaxPanel>
    <asp:HiddenField ID="HidenUrs" runat="server" />
    <asp:Label runat="server" ID="LblUsuario" Visible="false"></asp:Label>
    <telerik:RadNotification ID="Notificacion" runat="server" Position="Center" Width="330" Height="160" Animation="Fade" EnableRoundedCorners="true" EnableShadow="true" Style="z-index: 100000">
        </telerik:RadNotification>
</asp:Content>