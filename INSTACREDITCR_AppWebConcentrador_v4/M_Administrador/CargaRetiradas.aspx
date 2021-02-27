<%@ Page Title="" Language="VB" MasterPageFile="./MasterPage.master" AutoEventWireup="false" CodeFile="CargaRetiradas.aspx.vb" Inherits="CargaRetiradas" %>




<asp:Content ID="Content1" ContentPlaceHolderID="CPHMaster" runat="Server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" ></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="UPNLGeneral" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <table class="Table">
            <tr>
                <td class="Titulos">Retiro De Creditos
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>1.- 
            <asp:Label ID="LblTipo" runat="server" Text="Tipo" CssClass="LblDesc"></asp:Label>
                                <asp:DropDownList ID="DdlTipo" runat="server" CssClass="DdlDesc" AutoPostBack="true">
                                    <asp:ListItem Selected="True">Seleccione</asp:ListItem>
                                    <asp:ListItem Value="AZUL">Azul</asp:ListItem>
                                    <asp:ListItem Value="NARANJA">Naranja</asp:ListItem>
                                    <asp:ListItem Value="ROJO">Rojo</asp:ListItem>
                                </asp:DropDownList>

                            </td>
                        </tr>
                        <tr>
                            <td>

                                <telerik:RadLabel runat="server" ID="lbl" Text="Archivo"></telerik:RadLabel>
                                <telerik:RadAsyncUpload ID="RadAsyncUpload1" runat="server" AllowedFileExtensions=".CSV,.txt">
                                </telerik:RadAsyncUpload>
                            </td>
                            <td></td>
                            <td>

                                <telerik:RadButton ID="BtnCargar" runat="server" CssClass="Botones" Text="Cargar"  />

                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Label ID="lbllayout" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <telerik:RadToolTip ID="RadToolTip1" runat="server" RelativeTo="Element"  TargetControlID="BtnCargar" Animation="Resize" HideEvent="LeaveTargetAndToolTip">
                                    <table class="Table" style="color: black">
                                        <tr align="justify">
                                            <td class="Titulos" colspan="2">Layout Retiro Cuentas</td>
                                        </tr>
                                        <tr class="Izquierda">
                                            <td><strong>Campo</strong> </td>
                                            <td><strong>Observaciones</strong> </td>
                                        </tr>
                                        <tr class="Izquierda">
                                            <td>Número De Crédito:</td>
                                            <td>Hasta 25 Caracteres</td>
                                        </tr>
                                        <tr class="Izquierda">
                                            <td>Fecha Retiro:</td>
                                            <td>Hasta 25 Caracteres</td>
                                        </tr>
                                        <tr class="Izquierda">
                                            <td>Fecha Asignacion:</td>
                                            <td>Hasta 25 Caracteres</td>
                                        </tr>
                                        <tr class="Izquierda">
                                            <td>Motivo Retiro:</td>
                                            <td>Hasta 25 Caracteres</td>
                                        </tr>
                                        <tr class="Izquierda">
                                            <td>Agencia:</td>
                                            <td>Hasta 25 Caracteres</td>
                                        </tr>
                                        <tr class="Izquierda">
                                            <td>Lineas:</td>
                                            <td>Hasta 25 Caracteres</td>
                                        </tr>
                                        <tr class="Izquierda">
                                            <td>Bucket Agencia:</td>
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
                            <td colspan="3">

                                <asp:Label ID="LblMensaje" runat="server" Text="" CssClass="LblDesc"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">

                                <telerik:RadGrid ID="GvCargaRetiradas" runat="server"  Width="251px" Visible="false">
                                </telerik:RadGrid>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3"></td>
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

