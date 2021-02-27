<%@ Page Title="" Language="VB" MasterPageFile="./MasterPage.master" AutoEventWireup="false" CodeFile="CargaBlockCuentas.aspx.vb" Inherits="CargaBlockCuentas" %>



<asp:Content ID="Content1" ContentPlaceHolderID="CPHMaster" runat="Server">

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" ></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="UPNLGeneral" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <table class="Table">
            <tr>
                <td class="Titulos">Bloqueo Y Desbloqueo De Creditos</td>

            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>

                                <telerik:RadLabel runat="server" ID="lbl" Text="Archivo"></telerik:RadLabel>
                                <telerik:RadAsyncUpload ID="RadAsyncUpload1" runat="server" AllowedFileExtensions=".CSV,.txt"></telerik:RadAsyncUpload>

                            </td>
                            <td>

                                <asp:Label ID="LblTipo" runat="server" Text="Seleccione:" CssClass="LblDesc"></asp:Label>
                            </td>
                            <td>

                                <asp:DropDownList ID="DdlTipo" runat="server">
                                    <asp:ListItem Selected="True">Seleccione</asp:ListItem>
                                    <asp:ListItem Value="Bloquear">Bloquear Creditos</asp:ListItem>
                                    <asp:ListItem Value="DesBloquear">DesBloquear Creditos</asp:ListItem>
                                </asp:DropDownList>

                            </td>
                            <td>

                                <asp:Label ID="LblDelimitador" runat="server" Text="Delimitador" CssClass="LblDesc" ></asp:Label>

                            </td>
                            <td>

                                <asp:DropDownList ID="DdlDelimitador" runat="server" CssClass="DdlDesc" >
                                </asp:DropDownList>

                            </td>
                            <td>

                                <telerik:RadButton ID="BtnCargar" runat="server" CssClass="Botones" Text="Cargar"  />

                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <telerik:RadToolTip ID="RadToolTip1" runat="server" RelativeTo="Element"  TargetControlID="BtnCargar" Animation="Resize" HideEvent="LeaveTargetAndToolTip">
                                    <table class="Table" style="color: black">
                                        <tr align="justify">
                                            <td class="Titulos" colspan="2">Layout Cuentas Bloqueadas</td>
                                        </tr>
                                        <tr class="Izquierda">
                                            <td><strong>Campo</strong> </td>
                                            <td><strong>Observaciones</strong> </td>
                                        </tr>
                                        <tr class="Izquierda">
                                            <td>Número de Cuenta:</td>
                                            <td>Hasta 25 Caracteres</td>
                                        </tr>
                                        <tr class="Izquierda">
                                            <td>&nbsp;</td>
                                            <td style="color: red">Archivo En Formato en CSV Con Encabezado</td>
                                        </tr>
                                    </table>
                                </telerik:RadToolTip>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">

                                <asp:Label ID="LblMensaje" runat="server" Text="" CssClass="LblDesc"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">

                                <telerik:RadGrid ID="GvCargaCta" runat="server"  Width="251px" Visible="false">
                                </telerik:RadGrid>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6"></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </telerik:RadAjaxPanel>
    <asp:HiddenField ID="HidenUrs" runat="server" />
    <telerik:RadWindowManager ID="RadAviso" runat="server" >
        <Localization OK="Aceptar" />
    </telerik:RadWindowManager>
</asp:Content>

