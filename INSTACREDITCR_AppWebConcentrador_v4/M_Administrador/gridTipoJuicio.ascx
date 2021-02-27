<%@ Control Language="VB" AutoEventWireup="false" CodeFile="gridTipoJuicio.ascx.vb" Inherits="M_Administrador_gridTipoJuicio" %>
<telerik:RadAjaxLoadingPanel runat="server" ID="pnlLoading"></telerik:RadAjaxLoadingPanel>
<telerik:RadAjaxPanel runat="server" ID="P1" LoadingPanelID="pnlLoading">
    <table>
        <tr>
            <td>
                <telerik:RadLabel ID="LblNombre" runat="server" CssClass="LblDesc" Text="Nombre juicio">
                </telerik:RadLabel>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator0" runat="server" ControlToValidate="TxtNombre" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
            </td>
            <td>
                <telerik:RadTextBox ID="TxtNombre" runat="server">
                </telerik:RadTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadLabel ID="LblDescripcion" runat="server" CssClass="LblDesc" Text="Descripcion">
                </telerik:RadLabel>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtDescripcion" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true" ></asp:RequiredFieldValidator>
            </td>
            <td>
                <telerik:RadTextBox ID="TxtDescripcion" runat="server">
                </telerik:RadTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadLabel ID="LblTipoDocumentos" runat="server" CssClass="LblDesc" Text="Tipo documentos">
                </telerik:RadLabel>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="RCBTipoDocumentos" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
            </td>
            <td>
                <telerik:RadComboBox ID="RCBTipoDocumentos" runat="server" CheckBoxes="true" DropDownAutoWidth="Enabled" EmptyMessage="Seleccione tipos de documentos" EnableCheckAllItemsCheckBox="true" AutoPostBack="true">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadLabel ID="LblDiasCaducidad" runat="server" CssClass="LblDesc" Text="Caducidad juicio (Días)"></telerik:RadLabel>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="NtxtDiasCaducidad" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
            </td>
            <td>
                <telerik:RadNumericTextBox ID="NtxtDiasCaducidad" runat="server" CssClass="fuenteTxt" MinValue="0" Type="Number"></telerik:RadNumericTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadLabel ID="LblTipoDiasCad" runat="server" CssClass="LblDesc" Text="Tipo Dias Caducidad">
                </telerik:RadLabel>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="DdlTipoDiasCad" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
            </td>
            <td>
                <telerik:RadDropDownList ID="DdlTipoDiasCad" runat="server" AutoPostBack="false">
                    <Items>
                        <telerik:DropDownListItem Value="0" Text="Habil" />
                        <telerik:DropDownListItem Value="1" Text="Inhabil" />
                    </Items>
                </telerik:RadDropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadLabel ID="LblDiasPrescrpcion" runat="server" CssClass="LblDesc" Text="Prescripcion de sentencia (Días)"></telerik:RadLabel>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="NtxtDiasPrescripcion" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
            </td>
            <td>
                <telerik:RadNumericTextBox ID="NtxtDiasPrescripcion" runat="server" CssClass="fuenteTxt" MinValue="0" Type="Number"></telerik:RadNumericTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadLabel ID="LblTipoDiasPres" runat="server" CssClass="LblDesc" Text="Tipo dias prescripcion">
                </telerik:RadLabel>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="DdlTipoDiasPres" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
            </td>
            <td>
                <telerik:RadDropDownList ID="DdlTipoDiasPres" runat="server" AutoPostBack="false">
                    <Items>
                        <telerik:DropDownListItem Value="0" Text="Habil" />
                        <telerik:DropDownListItem Value="1" Text="Inhabil" />
                    </Items>
                </telerik:RadDropDownList>
            </td>
        </tr>
    </table>
</telerik:RadAjaxPanel>
<telerik:RadButton ID="BtnAccion" runat="server" Text="Actualizar" CommandName="Update" Visible='<%# Not (TypeOf DataItem Is Telerik.Web.UI.GridInsertionObject) %>' AutoPostBack="true"></telerik:RadButton>
<telerik:RadButton ID="btnInsert" Text="Insertar" runat="server" CommandName="PerformInsert"
    Visible='<%# (TypeOf DataItem Is Telerik.Web.UI.GridInsertionObject) %>' AutoPostBack="true">
</telerik:RadButton>
<telerik:RadButton ID="RBtnCacelar" runat="server" Text="Cancelar" CommandName="Cancel" CausesValidation="false"></telerik:RadButton>


