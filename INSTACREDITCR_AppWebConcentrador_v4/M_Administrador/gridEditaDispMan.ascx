<%@ Control Language="VB" AutoEventWireup="false" CodeFile="gridEditaDispMan.ascx.vb" Inherits="M_Administrador_gridEditaDispMan" %>
<telerik:RadAjaxLoadingPanel runat="server" ID="pnlLoading"></telerik:RadAjaxLoadingPanel>
<telerik:RadAjaxPanel runat="server" ID="ajaxPanel" LoadingPanelID="pnlLoading">
    <table>
        <tr>
            <td>
                <telerik:RadLabel ID="LblUsuario" runat="server" CssClass="LblDesc" Text="Usuario nuevo">
                </telerik:RadLabel>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DdlUsuario" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
            </td>
            <td>
                <telerik:RadComboBox ID="DdlUsuario" runat="server" AutoPostBack="false" CheckBoxes="false" EmptyMessage="Seleccione nuevo usuario">
                </telerik:RadComboBox>
            </td>
        </tr>
    </table>
</telerik:RadAjaxPanel>
<telerik:RadButton ID="BtnAccion" runat="server" Text="Actualizar" CommandName="Update" Visible='<%# Not (TypeOf DataItem Is Telerik.Web.UI.GridInsertionObject) %>'></telerik:RadButton>
<telerik:RadButton ID="btnInsert" Text="Insertar" runat="server" CommandName="PerformInsert"
    Visible='<%# (TypeOf DataItem Is Telerik.Web.UI.GridInsertionObject) %>'>
</telerik:RadButton>

<telerik:RadButton ID="RBtnCacelar" runat="server" Text="Cancelar" CommandName="Cancel" CausesValidation="false"></telerik:RadButton>
