<%@ Page Title="" Language="VB" MasterPageFile="./MasterPage.master" AutoEventWireup="false" CodeFile="CatalogoGastos.aspx.vb" Inherits="CatalogoGastos" %>


<asp:Content ID="Content2" ContentPlaceHolderID="CPHMaster" runat="Server">
    <script languaje="javascript" type="text/javascript">

        function confirmCallbackFn(arg) {
            if (arg) {

                __doPostBack('ctl00$CPHMaster$RGVGastos$ctl00$ctl13$EditFormControl$BtnAceptarConfirmacion', '')

            }
        }
    </script>
    <link href="Styles/Estilos/HTML.css" rel="stylesheet" />
    <link href="Styles/Estilos/Modal.css" rel="stylesheet" />
    <link href="Styles/Estilos/ObjAjax.css" rel="stylesheet" />
    <link href="Styles/Estilos/ObjHtmlNoS.css" rel="stylesheet" />
    <link href="Styles/Estilos/ObjHtmlS.css" rel="stylesheet" />

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" ></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="UPNLGeneral" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <table class="Table">
            <tr class="Titulos">
                <td>Gastos
                </td>
            </tr>
             </table>
                                    <telerik:RadGrid ID="RGVGastos" runat="server"  RenderMode="Lightweight" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true"
                                        ClientSettings-EnableRowHoverStyle="true" OnNeedDataSource="RGVGastos_NeedDataSource" Culture="es-MX">

                                        <MasterTableView Width="100%" CommandItemDisplay="Top" EditFormSettings-PopUpSettings-KeepInScreenBounds="true" DataKeyNames="Id">

                                            <Columns>

                                                <telerik:GridEditCommandColumn ItemStyle-Width="5px" UniqueName="Expandible">
                                                </telerik:GridEditCommandColumn>
                                                <telerik:GridBoundColumn UniqueName="Id" HeaderText="Id" DataField="Id">
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn UniqueName="Tipo" HeaderText="Tipo" DataField="Tipo">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="Monto" HeaderText="Monto" DataField="Monto">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete" HeaderText="Eliminar" >
                            </telerik:GridButtonColumn>
                                            </Columns>
                                            <EditFormSettings UserControlName="Gastos.ascx" EditFormType="WebUserControl">
                                                <EditColumn UniqueName="EditCommandColumn1">
                                                </EditColumn>
                                            </EditFormSettings>
                                        </MasterTableView>
                                    </telerik:RadGrid>
        &nbsp;
        &nbsp;
        &nbsp;
        &nbsp;
        &nbsp;
        &nbsp;
                                
        <telerik:RadWindowManager ID="RadAviso" runat="server" >
            <Localization OK="Aceptar" />
        </telerik:RadWindowManager>
        <asp:Button runat="server" ID="BtnAceptarConfirmacion" Visible="true" Height="1px" Style="display: none;" />
    </telerik:RadAjaxPanel>
</asp:Content>
