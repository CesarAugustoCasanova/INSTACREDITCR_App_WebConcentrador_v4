<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Etapas.ascx.vb" Inherits="Etapas" %>
<telerik:RadAjaxLoadingPanel runat="server" ID="pnlLoading"></telerik:RadAjaxLoadingPanel>
<telerik:RadAjaxPanel runat="server" ID="ajaxPanel" LoadingPanelID="pnlLoading">
    <table>
        <tr>
            <td>
                <telerik:RadLabel ID="LblNombre" runat="server" CssClass="LblDesc" Text="Nombre etapa">
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
                <telerik:RadLabel ID="LblTipoJuicio" runat="server" CssClass="LblDesc" Text="Tipo Juicio">
                </telerik:RadLabel>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DdlTipoJuicio" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
            </td>
            <td>
                <telerik:RadComboBox ID="DdlTipoJuicio" runat="server" AutoPostBack="true" CheckBoxes="true" EmptyMessage="Seleccione juicios">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadLabel ID="LblDescripcion" runat="server" CssClass="LblDesc" Text="Descripcion">
                </telerik:RadLabel>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtDescripcion" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
            </td>
            <td>
                <telerik:RadTextBox ID="TxtDescripcion" runat="server">
                </telerik:RadTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadLabel ID="LblImporte" runat="server" CssClass="LblDesc" Text="Importe"></telerik:RadLabel>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="NtxtImporte" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
            </td>
            <td>
                <telerik:RadNumericTextBox ID="NtxtImporte" runat="server" CssClass="fuenteTxt" MinValue="0" Type="Number"></telerik:RadNumericTextBox>
            </td>
        </tr>
    </table>
</telerik:RadAjaxPanel>
<telerik:RadButton ID="BtnAccion" runat="server" Text="Actualizar" CommandName="Update" Visible='<%# Not (TypeOf DataItem Is Telerik.Web.UI.GridInsertionObject) %>'></telerik:RadButton>
<telerik:RadButton ID="btnInsert" Text="Insertar" runat="server" CommandName="PerformInsert"
    Visible='<%# (TypeOf DataItem Is Telerik.Web.UI.GridInsertionObject) %>'>
</telerik:RadButton>

<telerik:RadButton ID="RBtnCacelar" runat="server" Text="Cancelar" CommandName="Cancel" CausesValidation="false"></telerik:RadButton>
