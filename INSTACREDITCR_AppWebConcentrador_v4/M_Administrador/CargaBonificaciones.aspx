<%@ Page Title="" Language="VB" MasterPageFile="./MasterPage.master" AutoEventWireup="false" CodeFile="CargaBonificaciones.aspx.vb" Inherits="CargaBonificaciones" %>


<asp:Content ID="Content1" ContentPlaceHolderID="CPHMaster" runat="Server">

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" ></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="UPNLGeneral" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <table class="Table" align="Center">
            <tr>
                <td class="Titulos">
                    <asp:Label ID="LblTitulo" runat="server" Text="Carga Bonificaciones"></asp:Label>
                    <%--<asp:Label ID="LblTitulo" runat="server" Text="Actualización de cartera"></asp:Label>--%>
                </td>
            </tr>
            <tr align="Center">
                <td>
                    <telerik:RadLabel runat="server" ID="lbl" Text="Archivo"></telerik:RadLabel>
                    <asp:Label ID="LblMenInfor" runat="server" Text="" CssClass="LblDesc" Visible="false" ForeColor="Red"></asp:Label></td>
            </tr>

            <tr>
                <td>&nbsp;
                     <telerik:RadAsyncUpload ID="RadAsyncUpload1" runat="server" AllowedFileExtensions=".CSV,.txt">
                     </telerik:RadAsyncUpload>
                    &nbsp;
                        

                        &nbsp;
                        &nbsp;
                        
                </td>
            </tr>
            <tr>
                <td>

                    <asp:Label ID="LblDelimitador" runat="server" Text="Delimitador" CssClass="LblDesc"></asp:Label>&nbsp;
                <asp:DropDownList ID="DdlDelimitador" runat="server" CssClass="DdlDesc">
                </asp:DropDownList>&nbsp;
            &nbsp;
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                        <asp:Label ID="LblMensaje" runat="server" Text="" CssClass="LblDesc"></asp:Label>
                </td>
            </tr>
            <tr aling="Center">
                <td>
                    <asp:Label ID="LblLayOut" runat="server" Text="" Font-Size="XX-Small" CssClass="LblDesc"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="BtnCargar" runat="server" CssClass="Btn_Aceptar" Text="Cargar" />
                    <telerik:RadToolTip ID="RadToolTip1" runat="server" RelativeTo="Element"  TargetControlID="BtnCargar" Animation="Resize" HideEvent="LeaveTargetAndToolTip">
                        <table width="100%">
                            <tr align="center">
                                <td class="Titulo" colspan="14">Layout actualización saldos</td>
                            </tr>
                            <tr align="center">

                                <td>
                                    <strong>Campos:</strong>
                                </td>
                                <td>Número De Crédito</td>
                                <td>Estatus</td>
                                <td>Saldo total</td>
                                <td>Saldo vencido</td>
                                <td>Late Fee</td>
                                <td>Número LateFee</td>
                                <td>SV + LF</td>
                                <td>Bucket</td>
                                <td>Fondos de contingencia</td>
                                <td>Saldo total + Fondos</td>
                                <td>Fecha de ultimo pago</td>
                                <td>Monto de ultimo pago</td>
                                <td>Producto</td>
                                <td>Campaña</td>
                            </tr>
                            <tr align="center">
                                <td>
                                    <strong>Observaciones:</strong>
                                </td>
                                <td>25 Caracteres</td>
                                <td>1 Caracteres</td>
                                <td>22 Caracteres</td>
                                <td>22 Caracteres</td>
                                <td>22 Caracteres</td>
                                <td>22 Caracteres</td>
                                <td>22 Caracteres</td>
                                <td>15 Caracteres</td>
                                <td>22 Caracteres</td>
                                <td>22 Caracteres</td>
                                <td>25 Caracteres</td>
                                <td>22 Caracteres</td>
                                <td>25 Caracteres</td>
                                <td>25 Caracteres</td>

                            </tr>

                        </table>
                    </telerik:RadToolTip>

                </td>
            </tr>
            <tr>
                <td></td>
            </tr>

            <tr>
                <td align="Center">

                    <telerik:RadGrid ID="GvCargaAsignacion" runat="server"  Width="251px" Visible="false">
                    </telerik:RadGrid>
                </td>
            </tr>

        </table>
    </telerik:RadAjaxPanel>

    <asp:HiddenField ID="HidenUrs" runat="server" />

    <telerik:RadWindowManager ID="RadAviso" runat="server" >
        <Localization OK="Aceptar" />
    </telerik:RadWindowManager>

</asp:Content>

