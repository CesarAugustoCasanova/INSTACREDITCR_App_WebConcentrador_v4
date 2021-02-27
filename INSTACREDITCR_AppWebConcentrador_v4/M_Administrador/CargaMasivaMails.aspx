<%@ Page Title="" Language="VB" MasterPageFile="./MasterPage.master" AutoEventWireup="false" CodeFile="CargaMasivaMails.aspx.vb" Inherits="CargaMasivaMails" Async="True" %>


<asp:Content ID="Content2" ContentPlaceHolderID="CPHMaster" runat="Server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" ></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="UPNLGeneral" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <table width="100%" align="Center">
            <tr>
                <td class="Titulos">
                    <asp:Label ID="LblTitulo" runat="server" Text="Carga Masiva Emails"></asp:Label>

                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>

            <tr>
                <td>&nbsp;
                        
                           
                          

                    <telerik:RadLabel runat="server" ID="lbl" Text="Archivo"></telerik:RadLabel>
                    <telerik:RadAsyncUpload ID="RadAsyncUpload1" runat="server" AllowedFileExtensions=".CSV,.txt">
                    </telerik:RadAsyncUpload>

                    <br />
                    <asp:Label ID="lblMesg" runat="server" Text=""></asp:Label>



                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LblTipo" runat="server" Text="Plantilla:" CssClass="LblDesc"></asp:Label>
                    <asp:DropDownList ID="DdlPlantilla" runat="server" CssClass="DdlDesc">
                    </asp:DropDownList>&nbsp;
                        &nbsp;
                        <asp:Label ID="LblDelimitador" runat="server" Text="Delimitador" CssClass="LblDesc"></asp:Label>&nbsp;
                        <asp:DropDownList ID="DdlDelimitador" runat="server" CssClass="DdlDesc">
                        </asp:DropDownList></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Correo De Salida" CssClass="LblDesc"></asp:Label>&nbsp;
                        <asp:DropDownList ID="DdlCorreoSalida" runat="server" CssClass="DdlDesc">
                        </asp:DropDownList>
                    &nbsp;
                        <telerik:RadButton ID="BtnCargar" runat="server" CssClass="Botones" Text="Cargar"  />

                </td>
            </tr>
            <tr>
                <td>
                    <telerik:RadToolTip ID="RadToolTip1" runat="server" RelativeTo="Element"  TargetControlID="BtnCargar" Animation="Resize" HideEvent="LeaveTargetAndToolTip">
                        <table width="100%">
                            <tr align="justify">
                                <td class="titulos" colspan="2">Layout Carga Mails Masivos</td>
                            </tr>
                            <tr align="center">
                                <td>
                                    <strong>Campo</strong></td>
                                <td>
                                    <strong>Observaciones</strong></td>
                            </tr>
                            <tr align="center">
                                <td colspan="2">Archivo Con Encabezado</td>
                            </tr>
                            <tr align="justify">
                                <td>Credito
                                </td>
                                <td>Hasta 25 Caracteres</td>
                            </tr>
                            <tr align="justify">
                                <td>Correo
                                </td>
                                <td>Hasta 50 Caracteres</td>
                            </tr>
                        </table>
                    </telerik:RadToolTip>
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                        <asp:Label ID="LblMensaje" runat="server" Text="" CssClass="LblDesc"></asp:Label>
                </td>

            </tr>
            <tr>
                <td align="Center">
                    <telerik:RadGrid ID="GvCargaMail" runat="server"  Width="251px" Visible="false">
                    </telerik:RadGrid>

                    <telerik:RadButton ID="LnkLog" runat="server" Visible="false" Text="Archivo Log" ButtonType="LinkButton"  />
                    <br />
                    <telerik:RadButton ID="LnkBad" runat="server" Visible="false" Text="Archivo Bad" ButtonType="LinkButton"  />
                    <br />
                </td>
            </tr>

        </table>

    </telerik:RadAjaxPanel>
    <asp:HiddenField ID="HidenUrs" runat="server" />


    <asp:HiddenField ID="HCat_Pe_id" runat="server" />
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:Button ID="BtnModalMsj" runat="server" Style="visibility: hidden" />
    <telerik:RadWindowManager ID="RadAviso" runat="server" >
        <Localization OK="Aceptar" />
    </telerik:RadWindowManager>
</asp:Content>

