
<%@ Page Title="" Language="VB" MasterPageFile="~/M_Administrador/MasterPage.master"
    AutoEventWireup="false" CodeFile="CatalogoAgencias.aspx.vb" Inherits="Administrador_CatalogoAgencias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHMaster" runat="Server">
    <script languaje="javascript" type="text/javascript">

        function confirmCallbackFn(arg) {
            if (arg) {

                __doPostBack('ctl00$CPHMaster$BtnAceptarConfirmacion', '')

            }
        }
    </script> 

        <table style="width:100%">
            <tr class="Titulos">
                <td>Agencias
                </td>
            </tr>
            <tr align="center">
                <td>
                    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" ></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="UPNLGeneral" runat="server" UpdatePanelCssClass="w3-center" CssClass="w3-center" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">

                    <telerik:RadGrid ID="RGVAgencias" runat="server"  RenderMode="Lightweight" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true"
                        ClientSettings-EnableRowHoverStyle="true" OnNeedDataSource="RGVAgencias_NeedDataSource" Culture="es-MX" AllowFilteringByColumn="true" Style="overflow: visible;" HeaderStyle-HorizontalAlign="Center" >

                        <MasterTableView Width="100%" CommandItemDisplay="Top" EditFormSettings-PopUpSettings-KeepInScreenBounds="true" DataKeyNames="USUARIO" Caption="">
                            <CommandItemSettings ShowAddNewRecordButton="true" RefreshText="Refrescar" AddNewRecordText="Nuevo"/>
                            <Columns>

                                <telerik:GridEditCommandColumn ItemStyle-Width="5px" UniqueName="Expandible">
                                </telerik:GridEditCommandColumn>

                                <telerik:GridBoundColumn UniqueName="Usuario" HeaderText="Usuario" DataField="Usuario">
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn UniqueName="NOMBRE" HeaderText="Nombre" DataField="NOMBRE">
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn UniqueName="Estatus" HeaderText="Estatus" DataField="Estatus">
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn UniqueName="Motivo" HeaderText="Motivo" DataField="Motivo">
                                </telerik:GridBoundColumn>

                                <%--<telerik:GridBoundColumn UniqueName="Tipo_despacho" HeaderText="Tipo despacho" DataField="Tipo despacho">
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn UniqueName="Instancia" HeaderText="Instancia" DataField="Instancia">
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn UniqueName="Tipo_de_persona" HeaderText="Tipo de persona" DataField="Tipo de persona">
                                </telerik:GridBoundColumn>--%>

                                <telerik:GridBoundColumn UniqueName="Supervisor" HeaderText="Supervisor" DataField="Supervisor">
                                </telerik:GridBoundColumn>
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

                </telerik:RadAjaxPanel>
                </td>
            </tr>
        </table>
</asp:Content>
