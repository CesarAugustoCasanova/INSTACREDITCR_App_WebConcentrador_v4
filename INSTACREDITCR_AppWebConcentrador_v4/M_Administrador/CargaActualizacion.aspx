<%@ Page Title="" Language="VB" MasterPageFile="MasterPage.master" AutoEventWireup="false" CodeFile="CargaActualizacion.aspx.vb" Inherits="M_Administrador_CargaActualizacion" %>


<asp:Content ID="Content1" ContentPlaceHolderID="CPHMaster" runat="Server">
    <link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css" />


    <%--<div class="Pagina">--%>
    <telerik:RadAjaxLoadingPanel ID="Radpanelcarga" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="Pnlgen" runat="server" HorizontalAlign="NotSet" LoadingPanelID="Radpanelcarga">
        <table class="Table">
            <tr>
                <td class="Titulos">
                    <asp:Label ID="LblTitulo" runat="server" Text="Carga Actulización"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="table">
                        <%-- <tr >
                                                            <td colspan="2">Layout Actulización</td>
                                                        </tr>--%>
                        <tr>
                            <th>Campo</th>
                            <td>Número De Crédito:</td>
                            <td>Motivo</td>
                            <td>Submotivo</td>
                            <td>Aplica</td>
                        </tr>
                        <tr>
                            <th>Observaciones</th>
                            <td>Hasta 25 Caracteres</td>
                            <td>Campo Estatus Cobranza</td>
                            <td>Campo Sub Estatus Cobranza</td>
                            <td>Campo Respuesta Domiciliacion</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td style="color: red">Archivo En Formato en CSV Con Encabezado</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="PnlActualizacion" runat="server">
                        <table align="left" class="Table">
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <telerik:RadLabel ID="RLVigencia" runat="server" Text="Archivo">
                                                </telerik:RadLabel>
                                            </td>
                                            <td>
                                                <telerik:RadAsyncUpload ID="RAUActualizacion" runat="server" AllowedFileExtensions=".CSV,.txt" MaxFileInputsCount="1" RenderMode="Lightweight" Width="500px">
                                                </telerik:RadAsyncUpload>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>
                                                <telerik:RadButton ID="RBCargarActualizacion" runat="server" CssClass="Botones" Text="Cargar" />
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="3"></td>

                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                     <%--<<asp:Label ID="LblMensaje" runat="server" Text="" CssClass="LblDesc"></asp:Label>--%>
                    <telerik:RadLabel ID="LblMensaje" runat="server" Text="">
                    </telerik:RadLabel>
                </td>
            </tr>
            <tr>
                <td align="Left">
                    <telerik:RadGrid ID="GvCargaAsignacion2" runat="server" Width="600px" Visible="false">
                    </telerik:RadGrid>
                    <br />
                </td>
            </tr>

        </table>



    </telerik:RadAjaxPanel>
    <%-- </div>--%>
    <asp:HiddenField ID="HidenUrs" runat="server" />
    <telerik:RadWindowManager ID="WinMsj" runat="server"></telerik:RadWindowManager>
    <telerik:RadWindowManager ID="RadAviso" runat="server">
        <Localization OK="Aceptar" />
    </telerik:RadWindowManager>

</asp:Content>


