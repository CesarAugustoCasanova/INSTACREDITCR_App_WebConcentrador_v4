<%@ Page Title="" Language="VB" MasterPageFile="./MasterPage.master" AutoEventWireup="false" CodeFile="Ponderacion.aspx.vb" Inherits="Ponderacion" %>


<asp:Content ID="Content1" ContentPlaceHolderID="CPHMaster" runat="Server">
    <%--<asp:UpdatePanel ID="UpnlPrincipal" runat="server">
        <ContentTemplate>--%>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" ></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="UPNLGeneral" runat="server" UpdatePanelCssClass="w3-center" CssClass="w3-center" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
            <table class="Table">
                <tr class="Titulos">
                    <td>Ponderación</td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="LblProducto" runat="server" CssClass="LblDesc" Text="Producto"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DdlProducto" runat="server" CssClass="DdlDesc" AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LblAccion" runat="server" CssClass="LblDesc" Text="Acción" Visible="false"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DdlAccion" runat="server" AutoPostBack="true" CssClass="DdlDesc" Visible="false">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td class="auto-style2">
                                    <table>
                                        <tr>
                                            <td style="vertical-align: top; " colspan="3">
                                                <div class="auto-style4">
                                                    <asp:GridView ID="GvPonderacionDesde" runat="server" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false" CssClass="auto-style1" Font-Names="Tahoma" Font-Size="Small" PagerStyle-CssClass="pgr" Style="font-size: x-small" Width="394px">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <table style="border-style: hidden;">
                                                                        <tr style="border-style: hidden;">
                                                                            <td style="width: 14px; position: static; vertical-align: top; border-style: hidden;">
                                                                                <asp:CheckBox ID="Cbx1" runat="server" AutoPostBack="true" Font-Size="5px" ForeColor="White" Height="0px" OnCheckedChanged="Cbx_" Width="0px" />
                                                                            </td>
                                                                            <td style="position: static; vertical-align: top; border-style: hidden;">
                                                                                <asp:Button ID="Button1" runat="server" CssClass="w3-button w3-indigo w3-hover-blue w3-round" Height="25px" Width="350px" OnClick="Bnt_clik_Desde" Text='<%# Bind("1") %>' />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>

                                            </td>
                                            <td style="vertical-align: top; width: 10%"></td>
                                            <td style="vertical-align: top; width: 15%">
                                                <div class="ScrollPondera">
                                                    <asp:GridView ID="GvPonderacionHasta" runat="server" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false" CssClass="mGrid" Font-Names="Tahoma" Font-Size="Small" PagerStyle-CssClass="pgr" Style="font-size: x-small">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:Button ID="Button1" runat="server" CssClass="w3-button w3-indigo w3-hover-blue w3-round" Height="25px" Width="350px" OnClick="Bnt_clik_Hasta" Text='<%# Bind("1") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; width: 15%">
                                                <asp:ImageButton ID="ImgAbajo" runat="server" ImageUrl="~/M_Administrador/Imagenes/ImgAbajo.png" Visible="false" />
                                            </td>
                                            <td style="vertical-align: top; width: 15%">
                                                <asp:ImageButton ID="ImgArriba" runat="server" ImageUrl="~/M_Administrador/Imagenes/ImgArriba.png" Visible="false" />
                                            </td>
                                            <td style="vertical-align: top; " class="auto-style3">
                                                <asp:Button ID="BtnPonderarDesde" runat="server" CssClass="Botones" Text="Ponderar" Visible="false" />
                                            </td>
                                            <td style="vertical-align: top; width: 15%">&nbsp;</td>
                                            <td style="vertical-align: top; width: 15%">
                                                <asp:Button ID="BtnPonderarHasta" runat="server" CssClass="Botones" Text="Ponderar" Visible="false" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr align="center">
                    <td>&nbsp;</td>
                </tr>
            </table>
            <asp:HiddenField ID="HidenUrs" runat="server" />
            <asp:Button ID="BtnModalMsj" runat="server" Style="visibility: hidden" />
            <asp:Panel ID="PnlModalMsj" runat="server" CssClass="ModalMsj"  Style="display: none">
                <div class="heading">
                    Aviso
                </div>
                <div class="CuerpoMsj">
                    <table class="Table">
                        <tr align="center">
                            <td>&nbsp;</td>
                        </tr>
                        <tr align="center">
                            <td>
                                <asp:Label ID="LblMsj" runat="server" CssClass="LblDesc"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="BtnCancelarMsj" runat="server" CssClass="button green close" Text="Aceptar" />
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
       </telerik:RadAjaxPanel>
    <telerik:RadWindowManager ID="RadAviso" runat="server" ></telerik:RadWindowManager>
</asp:Content>

<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .auto-style1 {
            background-color: #FFFFFF;
            margin: 5px 0 10px 0;
            border: solid 1px #525252;
            border-collapse: collapse;
            -o-user-select: none;
            -webkit-user-select: none;
            -moz-user-select: -moz-none;
            -khtml-user-select: none;
            -ms-user-select: none;
            -user-select: none;
            font-size: large;
        }
        .auto-style2 {
            width: 882px;
        }
        .auto-style3 {
            width: 12%;
        }
        .auto-style4 {
            margin: 0px;
            background-color: White;
            color: #000000;
            border: 1px solid buttonshadow;
            cursor: 'default';
            overflow: auto;
            height: auto;
            max-height: 350px;
            width: 420px;
            list-style-type: none;
            font-size: medium;
        }
    </style>
</asp:Content>


