<%@ Page Language="VB" MasterPageFile="./MasterPage.master" AutoEventWireup="false"
    CodeFile="EnvioMasivo.aspx.vb" Inherits="EnvioMasivo" Title=":: Master Collection :: Envio Masivo De SMS" %>


<asp:Content ID="Content2" ContentPlaceHolderID="CPHMaster" runat="Server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" ></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="UPNLGeneral" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <table style="width: 100%">
            <tr>
                <td class="Titulos" colspan="5">Envio SMS</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td colspan="3">

                    <asp:Label ID="LblSaldo" runat="server" CssClass="LblError"></asp:Label>
                </td>
            </tr>

            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td colspan="3">&nbsp;</td>
            </tr>

            <tr>
                <td>

                    <telerik:RadLabel runat="server" ID="lbl" Text="Archivo"></telerik:RadLabel>
                    <telerik:RadAsyncUpload ID="RadAsyncUpload1" runat="server" AllowedFileExtensions=".CSV,.txt">
                    </telerik:RadAsyncUpload>
                </td>
                <td>
                    <asp:Label ID="lBLdELIMITADOR" runat="server" Text="Delimitador:" CssClass="LblDesc"></asp:Label>
                </td>
                <td>



                    <asp:DropDownList ID="DdlDelimitador" runat="server" CssClass="DdlDesc">
                        <asp:ListItem Selected="True">Seleccione</asp:ListItem>
                        <asp:ListItem>,</asp:ListItem>
                        <asp:ListItem>|</asp:ListItem>
                        <asp:ListItem>;</asp:ListItem>
                    </asp:DropDownList>


                </td>
                <td>


                    <telerik:RadButton ID="BtnPre" runat="server" CssClass="Btn_Aceptar" Text="PreView"  />

                </td>
                <td style="text-align: right">

                    <telerik:RadButton ID="BtnCargar" runat="server" CssClass="Btn_Aceptar" Text="Cargar"  Visible="false" />

                </td>
            </tr>

            <tr>
                <td style="text-align: center; font-weight: 700; font-size: medium; color: #003399; font-family: 'Arial Black'">

                    <asp:Label ID="Label1" runat="server" Font-Names="Arial Black" Font-Size="Medium"
                        ForeColor="#0033CC" Text=""></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td colspan="2">
                    <telerik:RadToolTip ID="RadToolTip1" runat="server" RelativeTo="Element"  TargetControlID="BtnPre" Animation="Resize" HideEvent="LeaveTargetAndToolTip">
                        <table width="100%">
                            <tr align="justify">
                                <td class="titulos" colspan="2">Layout SMS Masivo</td>
                            </tr>
                            <tr align="center">
                                <td class="auto-style1"><strong>Campo</strong></td>
                                <td class="auto-style1"><strong>Observaciones</strong></td>
                            </tr>
                            <tr align="center">
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr align="justify">
                                <td>Número De Cuenta: </td>
                                <td>Hasta 25 Caracteres</td>
                            </tr>
                            <tr align="justify">
                                <td>Teléfono</td>
                                <td>10 Caracteres </td>
                            </tr>
                            <tr align="justify">
                                <td>Nombre De La Plantilla&nbsp; </td>
                                <td>Nombre De La Plantilla Creada En El Menu Catálogos/Plantillas SMS</td>
                            </tr>
                            <tr align="justify">
                                <td colspan="2">Archivo En Formato csv o txt Con Encabezado</td>
                            </tr>
                        </table>
                    </telerik:RadToolTip>

                </td>
            </tr>
            <tr>
                <td colspan="2">


                    <telerik:RadGrid ID="Grid_RESULTADO" runat="server"  Width="251px" Visible="false">
                    </telerik:RadGrid>
                </td>
                <td>

                    <asp:HiddenField ID="HidenUrs" runat="server" />
                </td>
                <td colspan="2">&nbsp;</td>
            </tr>
        </table>
    </telerik:RadAjaxPanel>

    <telerik:RadWindowManager ID="RadAviso" runat="server" >
        <Localization OK="Aceptar" />
    </telerik:RadWindowManager>
</asp:Content>
