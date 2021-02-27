<%@ Page Title="" Language="VB" MasterPageFile="MasterPage.master" AutoEventWireup="false" CodeFile="CargaExclusiones.aspx.vb" Inherits="M_Administrador_CargaExclusiones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHMaster" runat="Server">
    <link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css" />


    <%--<div class="Pagina">--%>
    <telerik:RadAjaxLoadingPanel ID="Radpanelcarga" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="Pnlgen" runat="server" HorizontalAlign="NotSet" LoadingPanelID="Radpanelcarga">
        <table class="Table">
            <tr>
                <td class="Titulos" colspan="2">
                    <asp:Label ID="LblTitulo" runat="server" Text="Carga Exclusiones"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="LblDesc">
                    <telerik:RadLabel runat="server" ID="RadLabel1" Text="Seleccione una opción de exclusión"></telerik:RadLabel>
                </td>
                <td class="LblDesc">
                    <telerik:RadLabel runat="server" ID="RadLabel2" Text="Seleccione la acción de la exclusión"></telerik:RadLabel>
                </td>
            </tr>
            <tr>
                <td class="LblDesc">
                    <telerik:RadComboBox ID="DdltipoAsig" runat="server" AutoPostBack="True" Culture="es-ES" EmptyMessage="Selecciona un tipo de exclusión" Width="400px">
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Selected="True" Text="Seleccione" Value="Seleccione" />
                            <telerik:RadComboBoxItem runat="server" Text="Dependencia" Value="Dependencia" />
                            <telerik:RadComboBoxItem runat="server" Text="Crédito" Value="Credito" />
                            <telerik:RadComboBoxItem runat="server" Text="No Domiciliar" Value="NoDomiciliar" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
                <td class="LblDesc">
                    <telerik:RadComboBox ID="RCBAccion" runat="server" AutoPostBack="True" Culture="es-ES" EmptyMessage="Selecciona una acción" Width="400px">
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Selected="True" Text="Dar de alta" Value="1" />
                            <telerik:RadComboBoxItem runat="server" Text="Dar de baja" Value="0" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="PnlVigencia" runat="server" Visible="false">
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
                                                <telerik:RadAsyncUpload ID="RAUVigencia" runat="server" AllowedFileExtensions=".CSV,.txt" MaxFileInputsCount="1" RenderMode="Lightweight" Width="500px">
                                                </telerik:RadAsyncUpload>
                                            </td>
                                            <td>
                                                <telerik:RadButton ID="RBCargarVigencia" runat="server" CssClass="Botones" Text="Cargar" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">

                                                <table class="table">

                                                    <%--<tr >
                                                            <td colspan="2">Dependencias</td>
                                                            <td colspan="2">Créditos</td>
                                                            <td colspan="2">No Domiciliar</td>
                                                        </tr>--%>
                                                    <tr runat="server" id="DepeC">
                                                        <th>Campo</th>
                                                        <td>Créditos:</td>
                                                        <td>Convenio</td>
                                                    </tr>
                                                    <tr runat="server" id="DepeD">
                                                        <th>Descripcion</th>
                                                        <td>Número de crédito o Todos los creditos</td>
                                                        <td>Convenio de la exclusión</td>
                                                    </tr>


                                                    <tr runat="server" id="CrediC">
                                                        <th>Campo</th>
                                                        <td>Créditos:</td>
                                                    </tr>
                                                    <tr runat="server" id="CrediD">
                                                        <th>Descripcion</th>
                                                        <td>Número de crédito o Todos los creditos</td>
                                                    </tr>


                                                    <tr runat="server" id="NoDomC">
                                                        <th>Campo</th>
                                                        <td>Créditos:</td>
                                                        <td>Convenio/Cliente</td>
                                                    </tr>
                                                    <tr runat="server" id="NoDomD">
                                                        <th>Descripcion</th>
                                                        <td>Número de crédito o Todos los creditos</td>
                                                        <td>Convenio o nombre del cliente</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td style="color: red">Archivo En Formato en CSV Con Encabezado</td>
                                                    </tr>
                                                </table>

                                            </td>

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


