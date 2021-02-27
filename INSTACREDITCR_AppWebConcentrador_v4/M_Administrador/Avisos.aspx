<%@ Page Title="" Language="VB" MasterPageFile="MasterPage.master"
    AutoEventWireup="false" CodeFile="Avisos.aspx.vb" Inherits="Administrador_Avisos" %>


<asp:Content ID="CAvisos" ContentPlaceHolderID="CPHMaster" runat="Server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="UPNLGeneral" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <table class="Table">
            <tr>
                <td>
                    <asp:HiddenField ID="HidenUrs" runat="server" />
                </td>
            </tr>
            <thead>
                <tr>
                    <th class="Titulos">Avisos </th>
                </tr>
            </thead>
            <tr>
                <td>
                    <table style="margin-left: 120px;">
                        <tr>
                            <td colspan="6">

                                <asp:Label ID="LblPreAvisos" runat="server" CssClass="LblDesc" Text="Avisos Previos" Visible="False"></asp:Label>

                                <telerik:RadButton ID="RBtnAvisosPrevios" runat="server" Text="Ver Avisos Previos" Visible="true"></telerik:RadButton>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="6">&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <table>
                                    <tr>
                                        <td rowspan="2">
                                            <asp:Label ID="LblUsrSucursales" runat="server" CssClass="LblDesc" Text="Seleccionar Agencia(s)"></asp:Label>
                                            <asp:ImageButton ID="BtnSucursales" runat="server" ImageUrl="~/M_Administrador/Imagenes/ImgAgencias.png" />
                                            <%-- <asp:PopupControlExtender ID="BtnSucursales_PopupControlExtender" runat="server" Enabled="True" PopupControlID="PnlSucursales" Position="Bottom" TargetControlID="BtnSucursales">
                                                </asp:PopupControlExtender>--%>
                                            
                                        </td>
                                        <td rowspan="2" style="padding-left: 25px;">
                                            <asp:Label ID="LblUsr" runat="server" CssClass="LblDesc" Text="Seleccionar Usuario" Visible="false"></asp:Label>
                                            <asp:Image ID="BtnUsrs" runat="server" ImageUrl="~/M_Administrador/Imagenes/ImgUsuario.png" Visible="false" />
                                            <%-- <asp:PopupControlExtender ID="BtnUsr_PopupControlExtender" runat="server" Enabled="True"
                                                    PopupControlID="pNLuSR" Position="Bottom" TargetControlID="BtnUsrs">
                                                </asp:PopupControlExtender>--%>
                                        </td>
                                        <td class="auto-style1">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                                        <td class="auto-style1">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                                        <td>
                                            <asp:Label ID="LblHist_Av_Prioridad" runat="server" CssClass="LblDesc" Text="Prioridad" Visible="false"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="LblHist_Av_Dteexpira" runat="server" CssClass="LblDesc" Text="Fecha Expiración" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style1">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                                        <td class="auto-style1">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                                        <td>
                                            <telerik:RadDropDownList ID="DdlHist_Av_Prioridad" runat="server" CssClass="DdlDesc" AutoPostBack="true" Visible="false">
                                                <Items>
                                                    <telerik:DropDownListItem Selected="True" Text="Seleccione" />
                                                    <telerik:DropDownListItem Value="1" Text="Alta" />
                                                    <telerik:DropDownListItem Value="2" Text="Media" />
                                                    <telerik:DropDownListItem Value="3" Text="Baja" />
                                                </Items>
                                            </telerik:RadDropDownList>
                                        </td>
                                        <td>

                                            <telerik:RadDatePicker ID="RDPHist_Av_Dteexpira" runat="server" Visible="false" AutoPostBack="true">
                                                <Calendar runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True" Culture="es-ES" FastNavigationNextText="&amp;lt;&amp;lt;" Visible="true"></Calendar>

                                                <DateInput runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" LabelWidth="40%">
                                                    <EmptyMessageStyle Resize="None"></EmptyMessageStyle>

                                                    <ReadOnlyStyle Resize="None"></ReadOnlyStyle>

                                                    <FocusedStyle Resize="None"></FocusedStyle>

                                                    <DisabledStyle Resize="None"></DisabledStyle>

                                                    <InvalidStyle Resize="None"></InvalidStyle>

                                                    <HoveredStyle Resize="None"></HoveredStyle>

                                                    <EnabledStyle Resize="None"></EnabledStyle>
                                                </DateInput>
                                            </telerik:RadDatePicker>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td runat="server" id="TdGVAgencias" visible="false" class="w3-center" style="padding-left: 40px;">

                                            <asp:GridView ID="GVAgencias" runat="server" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="true" CssClass="mGrid" Font-Names="Tahoma" Font-Size="Small" PagerStyle-CssClass="pgr" Style="font-size: x-small">
                                                <AlternatingRowStyle CssClass="alt" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="CheckAll">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelectAll_CheckedChangedA" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelect_CheckedChangedA" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle CssClass="pgr" />
                                            </asp:GridView>
                                        </td>
                                        <td runat="server" id="TdGVUsers" visible="false" style="padding-left: 40px;">
                                            <div style="max-height: 500px; overflow: auto">

                                                <asp:GridView ID="GridViewUsrs" runat="server" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="true" CssClass="mGrid" Font-Names="Tahoma" Font-Size="Small" Visible="true" PagerStyle-CssClass="pgr" Style="font-size: x-small; padding-top: 0px;">
                                                    <AlternatingRowStyle CssClass="alt" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="CheckAll">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelectAll_CheckedChangedU" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true" OnCheckedChanged="GridViewUsrs_SelectedIndexChanged" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="pgr" />
                                                </asp:GridView>
                                            </div>
                                        </td>
                                        <td class="auto-style1">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                                        <td class="auto-style1">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                                        <td colspan="2">
                                            <asp:Label ID="LblMsjAvisos" runat="server" CssClass="LblDesc" Text="Aviso" Visible="false"></asp:Label>
                                            <br />

                                            <telerik:RadTextBox ID="RTxtHist_Av_Mensaje" runat="server" TextMode="MultiLine" Resize="Both" Height="43px" Width="441px" EmptyMessage="Escriba el aviso aqui" Visible="false"></telerik:RadTextBox>
                                            <br />
                                            <br />
                                            <telerik:RadButton ID="RBtnCrear" runat="server" Text="Crear Aviso" Visible="false"></telerik:RadButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>


                    </table>
                </td>
            </tr>
        </table>

        <telerik:RadWindowManager ID="RadAviso" runat="server">
            <Localization OK="Aceptar" />
        </telerik:RadWindowManager>
        <telerik:RadWindow RenderMode="Lightweight" runat="server" ID="WindowMostrar" RestrictionZoneID="ContentTemplateZone" Modal="false" Width="750px" Height="701px" Behaviors="None">
            <ContentTemplate>
                <telerik:RadAjaxPanel ID="PnlModalMostrar" runat="server" CssClass="ModalAcciones" LoadingPanelID="RadAjaxLoadingPanel1">

                    <%-- Style="display: none">--%>
                    <div class="heading">
                        Avisos Previos
                    </div>
                    <div class="CuerpoAcciones">
                        <table class="Table">
                            <tr align="center">
                                <td colspan="2">&nbsp;<telerik:RadDropDownList ID="DdlAvisos" runat="server" AutoPostBack="True" CssClass="DdlDesc" Visible="False">
                                </telerik:RadDropDownList>
                                </td>
                            </tr>
                            <tr align="center">
                                <td colspan="2">

                                    <asp:GridView ID="GvAvisos" runat="server" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false" CssClass="mGrid" Font-Names="Tahoma" Font-Size="Small" PagerStyle-CssClass="pgr" Style="font-size: x-small" Visible="False">
                                        <AlternatingRowStyle CssClass="alt" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="CheckAll">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkSelectAllA" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelectAllA_CheckedChanged" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelectA" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="AVISOS" HeaderText="AVISOS" SortExpression="AVISOS" />
                                            <asp:BoundField DataField="ENVIADO" HeaderText="ENVIADO" SortExpression="ENVIADO" />
                                            <asp:BoundField DataField="EXPIRA" HeaderText="EXPIRA" SortExpression="EXPIRA" />
                                            <asp:BoundField DataField="LEIDO" HeaderText="LEIDO" SortExpression="LEIDO" />
                                            <asp:BoundField DataField="ESTATUS" HeaderText="ESTATUS" SortExpression="ESTATUS" />
                                            <asp:BoundField DataField="PRIORIDAD" HeaderText="PRIORIDAD" SortExpression="PRIORIDAD" />
                                            <asp:BoundField DataField="ENVIADO_POR" HeaderText="ENVIADO_POR" SortExpression="ENVIADO_POR" />
                                            <asp:BoundField DataField="USUARIO" HeaderText="USUARIO" SortExpression="USUARIO" />
                                        </Columns>
                                        <PagerStyle CssClass="pgr" />
                                    </asp:GridView>

                                </td>
                            </tr>
                            <tr align="center">
                                <td colspan="2">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="LblAccion" runat="server" CssClass="LblDesc" Text="Acción" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadDropDownList ID="DdlAccion" runat="server" AutoPostBack="true" CssClass="DdlDesc" Visible="False">
                                                    <Items>
                                                        <telerik:DropDownListItem Text="Seleccione" Selected="true" />
                                                        <telerik:DropDownListItem Text="Reactivar" />
                                                        <telerik:DropDownListItem Text="Expirar" />
                                                        <telerik:DropDownListItem Text="Eliminar" />
                                                    </Items>
                                                </telerik:RadDropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="LblHist_Av_Dteexpira2" runat="server" CssClass="LblDesc" Text="Fecha Expiración" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker ID="RDPHist_AvDteexpira2" runat="server" Visible="false" AutoPostBack="true">
                                                    <Calendar runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True" Culture="es-ES" FastNavigationNextText="&amp;lt;&amp;lt;" Visible="true"></Calendar>

                                                    <DateInput runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" LabelWidth="40%">
                                                        <EmptyMessageStyle Resize="None"></EmptyMessageStyle>

                                                        <ReadOnlyStyle Resize="None"></ReadOnlyStyle>

                                                        <FocusedStyle Resize="None"></FocusedStyle>

                                                        <DisabledStyle Resize="None"></DisabledStyle>

                                                        <InvalidStyle Resize="None"></InvalidStyle>

                                                        <HoveredStyle Resize="None"></HoveredStyle>

                                                        <EnabledStyle Resize="None"></EnabledStyle>
                                                    </DateInput>
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td>&nbsp;</td>

                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadButton ID="BtnAceptar" runat="server" Text="                                                                                  Aceptar" Visible="False" CssClass="w3-block" />
                                </td>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadButton ID="BtnCancelar" runat="server" Text="                                                                                  Cancelar" Visible="true" CssClass="w3-block" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </telerik:RadAjaxPanel>
            </ContentTemplate>
        </telerik:RadWindow>
    </telerik:RadAjaxPanel>
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .auto-style1 {
            width: 74px;
        }
    </style>
</asp:Content>

