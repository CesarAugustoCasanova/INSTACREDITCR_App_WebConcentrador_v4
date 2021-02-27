<%@ Page Title="" Language="VB" MasterPageFile="./MasterPage.master" AutoEventWireup="false" CodeFile="ConfigurarCorreo.aspx.vb" Inherits="ConfigurarCorreo" %>



<asp:Content ID="Content1" ContentPlaceHolderID="CPHMaster" runat="Server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" ></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <table class="Table">
            <tr class="Titulos">
                <td>Configurar correo de salida
                </td>
            </tr>
             </table>
        <telerik:RadGrid ID="RGVCorreoSalida" runat="server"  RenderMode="Lightweight" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true"
            ClientSettings-EnableRowHoverStyle="true" OnNeedDataSource="RGVCorreoSalida_NeedDataSource" Culture="es-MX">

            <MasterTableView Width="100%" CommandItemDisplay="Top" EditFormSettings-PopUpSettings-KeepInScreenBounds="true" DataKeyNames="ID">

                <Columns>

                    <telerik:GridEditCommandColumn ItemStyle-Width="5px" UniqueName="Expandible">
                    </telerik:GridEditCommandColumn>
                     <telerik:GridBoundColumn UniqueName="ID" HeaderText="ID" DataField="ID">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="Usuario" HeaderText="Usuario" DataField="Usuario">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="Password" HeaderText="Password" DataField="Password">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="Host" HeaderText="Host" DataField="Host">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="Puerto" HeaderText="Puerto" DataField="Puerto">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="SSL" HeaderText="SSL" DataField="SSL">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="Responsable" HeaderText="Responsable" DataField="Responsable">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="SalidaGestion" HeaderText="Salida Gestion" DataField="Salida Gestion">
                    </telerik:GridBoundColumn>
                    <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete" HeaderText="Eliminar" >
                            </telerik:GridButtonColumn>
                </Columns>
                <EditFormSettings UserControlName="CorreoSalida.ascx" EditFormType="WebUserControl">
                    <EditColumn UniqueName="EditCommandColumn1">
                    </EditColumn>
                </EditFormSettings>
            </MasterTableView>
        </telerik:RadGrid>
    </telerik:RadAjaxPanel>
    <telerik:RadWindowManager ID="RadAviso" runat="server" >
        <Localization OK="Aceptar" />
    </telerik:RadWindowManager>
    <asp:Button runat="server" ID="BtnAceptarConfirmacion" Visible="true" Height="1px" Style="display: none;" />
    <asp:HiddenField ID="HidenUrs" runat="server" />
</asp:Content>

