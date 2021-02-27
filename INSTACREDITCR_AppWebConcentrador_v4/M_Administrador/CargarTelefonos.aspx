<%@ Page Title="" Language="VB" MasterPageFile="./MasterPage.master" AutoEventWireup="false" CodeFile="CargarTelefonos.aspx.vb" Inherits="CargarTelefonos" %>



<asp:Content ID="Content1" ContentPlaceHolderID="CPHMaster" runat="Server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" ></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="UPNLGeneral" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <table class="Table" align="Center">
            <tr>
                <td class="Titulos">
                    <asp:Label ID="LblTitulo" runat="server" Text="Cargar Telefonos"></asp:Label>
                    <%--<asp:Label ID="LblTitulo" runat="server" Text="Actualización de cartera"></asp:Label>--%>
                </td>
            </tr>
            <tr align="Center">
                <td>
                    <asp:Label ID="LblMenInfor" runat="server" Text="" CssClass="LblDesc" Visible="false" ForeColor="Red"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTipo" runat="server" Text="Tipo: " CssClass="LblDesc"></asp:Label>
                    <asp:DropDownList ID="DdlTipo" runat="server" AutoPostBack="true" CssClass="DdlDesc">
                        <asp:ListItem Selected="True">Seleccione</asp:ListItem>
                        <asp:ListItem>Adicionales</asp:ListItem>
                        <asp:ListItem Value="Quejas">Quejas/AOC (3 Columns)</asp:ListItem>
                        <asp:ListItem Value="AOC 4 Colums">AOC (4 Columns)</asp:ListItem>
                        <asp:ListItem Value="BTTC 7 NARANJA">BTTC NARANJA (7 Columns)</asp:ListItem>
                        <asp:ListItem Value="BTTC 8 AZUL">BTTC AZUL (8 Columns)</asp:ListItem>
                        <asp:ListItem Value="BTTC 8 ROJO">BTTC ROJO (8 Columns)</asp:ListItem>
                        <asp:ListItem Value="NARANJA CON LEGADOS (5 Columns)">NARANJA CON LEGADOS (5 Columns)</asp:ListItem>

                    </asp:DropDownList>
                </td>
            </tr>


            <tr>
                <td>&nbsp;
                <telerik:RadLabel runat="server" ID="lbl" Text="Archivo"></telerik:RadLabel>
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
                    <telerik:RadButton ID="BtnCargar" runat="server" CssClass="Botones" Text="Cargar"  />


                </td>
            </tr>
            <tr>
                <td></td>
            </tr>

            <tr>
                <td align="Center">
                    <telerik:RadGrid ID="GvCargaAsignacion" runat="server"  Width="251px" Visible="false">
                    </telerik:RadGrid>
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td align="left">

                    <asp:Panel runat="server" Visible="false" ID="PnlDetalleCartera">
                    </asp:Panel>
                </td>
            </tr>

        </table>
    </telerik:RadAjaxPanel>

    <asp:HiddenField ID="HidenUrs" runat="server" />
    <telerik:RadWindowManager ID="RadAviso" runat="server" >
        <Localization OK="Aceptar" />
    </telerik:RadWindowManager>

</asp:Content>
