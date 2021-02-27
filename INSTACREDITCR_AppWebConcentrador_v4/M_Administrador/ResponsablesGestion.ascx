<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ResponsablesGestion.ascx.vb" Inherits="M_Administrador_ResponsablesGestion" %>
<telerik:RadAjaxLoadingPanel runat="server" ID="pnlLoading"></telerik:RadAjaxLoadingPanel>
<telerik:RadAjaxPanel runat="server" ID="P1" LoadingPanelID="pnlLoading">
    <table>
        <tr>
            <td>
                <telerik:RadLabel ID="LblNombre" runat="server" CssClass="LblDesc" Text="Nombre responsable">
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
                <telerik:RadLabel ID="LblUsuarioResp" runat="server" CssClass="LblDesc" Text="Usuario responsable de gestión">
                </telerik:RadLabel>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="RCBUsuarioResp" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
            </td>
            <td>
                <telerik:RadComboBox ID="RCBUsuarioResp" runat="server" CheckBoxes="false" DropDownAutoWidth="Enabled" EmptyMessage="Seleccione usuario responsable" EnableCheckAllItemsCheckBox="true" AutoPostBack="true">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadLabel ID="LblEmail" runat="server" CssClass="LblDesc" Text="Email"></telerik:RadLabel>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="RTBEmailResp" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
            </td>
            <td>
                <telerik:RadTextBox ID="RTBEmailResp" runat="server" CssClass="fuenteTxt"></telerik:RadTextBox>
            </td>
        </tr>
    </table>
</telerik:RadAjaxPanel>
<telerik:RadButton ID="BtnAccion" runat="server" Text="Actualizar" CommandName="Update" Visible='<%# Not (TypeOf DataItem Is Telerik.Web.UI.GridInsertionObject) %>' AutoPostBack="true"></telerik:RadButton>
<telerik:RadButton ID="btnInsert" Text="Insertar" runat="server" CommandName="PerformInsert"
    Visible='<%# (TypeOf DataItem Is Telerik.Web.UI.GridInsertionObject) %>' AutoPostBack="true">
</telerik:RadButton>
<telerik:RadButton ID="RBtnCacelar" runat="server" Text="Cancelar" CommandName="Cancel" CausesValidation="false"></telerik:RadButton>


