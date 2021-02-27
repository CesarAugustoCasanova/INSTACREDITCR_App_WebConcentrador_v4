<%@ Page Title="" Language="VB" MasterPageFile="./MasterPage.master" AutoEventWireup="false" CodeFile="CargaEtiquetaDom.aspx.vb" Inherits="MAdministrador_CargaEtiquetaDom" %>


<asp:Content ID="Content1" ContentPlaceHolderID="CPHMaster" runat="Server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" ></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="UPNLGeneral" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <table class="Table" align="Center" >
            <tr>
                <td class="Titulos" colspan="2">
                    <asp:Label ID="LblTitulo" runat="server" Text="Carga Etiquetas Domiciliación"></asp:Label>
                </td>
            </tr>
           
            <tr>
                <td colspan="2">&nbsp; </td>
            </tr>

            <tr>
                <td >&nbsp;
                        <telerik:RadLabel runat="server" ID="lbl" Text="Archivo"></telerik:RadLabel>
                    <telerik:RadAsyncUpload ID="RadAsyncUpload1" runat="server" AllowedFileExtensions=".CSV,.txt">
                    </telerik:RadAsyncUpload>
                    &nbsp;

                </td>
                <td>                               
                        &nbsp;
                        <asp:Label ID="LblDelimitador" runat="server" Text="Delimitador" CssClass="LblDesc"></asp:Label>&nbsp;
                <telerik:RadDropDownList ID="DdlDelimitador" runat="server"  DefaultMessage="Seleccione" AutoPostBack="false">
                     <Items>
                        <telerik:DropDownListItem Text="Seleccione" Value="0" Selected="true" />
                        <telerik:DropDownListItem Text="Tabulador" Value="1" />
                        <telerik:DropDownListItem Text="Coma" Value="2" />
                    </Items>
                </telerik:RadDropDownList>&nbsp;&nbsp;
                       
                        <telerik:RadButton ID="BtnCargar" runat="server" CssClass="Botones" Text="Cargar"  />
                </td>
            </tr>
            <tr>
            
                <td colspan="2"><div class="w-100">
                Layout para carga de cartera. El archivo debe ser CSV o TXT con encabezados.
            </div>
            <div class="table-responsive">
                <table class="table" runat="server">
                    <tr>
                        <th>Campo</th>
                        <td>Crédito</td>
                        <td>Etiqueta</td>
                        <td>Fecha Inicio</td>
                        <td>Fecha Fin</td>
                        <td>Cuota</td>
                    </tr>
                    <tr>
                        <th>Descripcion</th>
                        <td>Hasta 25 Caracteres</td>
                        <td>Hasta 50 Caracteres</td>
                        <td>Fecha (dd/mm/yyyy)</td>
                        <td>Fecha (dd/mm/yyyy)</td>
                        <td>Numerico Hasta 5</td>
                    </tr>
                </table>
                </div>
                </td>
            </tr>
            <%--<tr>
                <td>&nbsp;
                </td>
            </tr>--%>
           <%-- <tr>
                <td align="Center">
                    <telerik:RadGrid ID="GvCargaAsignacion" runat="server"  Width="251px" Visible="false">
                    </telerik:RadGrid>

                    <telerik:RadButton ID="LnkLog" runat="server" Visible="false" Text="Archivo Log" ButtonType="LinkButton"  />
                    <br />
                    <telerik:RadButton ID="LnkBad" runat="server" Visible="false" Text="Archivo Bad" ButtonType="LinkButton"  />
                    <br />
                </td>
            </tr>--%>

        </table>
        <div class="d-flex justify-content-center mt-2">
            
                <asp:Label ID="LblMensaje" runat="server" Text="" CssClass="LblDesc"></asp:Label>
        </div>
    </telerik:RadAjaxPanel>

    <asp:HiddenField ID="HidenUrs" runat="server" />

    <telerik:RadWindowManager ID="RadAviso" runat="server" >
        <Localization OK="Aceptar" />
    </telerik:RadWindowManager>
</asp:Content>

