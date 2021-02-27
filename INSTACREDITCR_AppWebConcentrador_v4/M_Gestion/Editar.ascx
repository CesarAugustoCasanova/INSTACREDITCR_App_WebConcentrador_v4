<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Editar.ascx.vb" Inherits="Editar" %>

<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" ></telerik:RadAjaxLoadingPanel>
<%--<telerik:RadAjaxPanel ID="UPNLGen" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" BackColor="#999999">--%>

    <table>
        <tr>

            <td>
                <telerik:RadLabel ID="LblCUENTAID0" runat="server" CssClass="LblMsj" Text="No." ></telerik:RadLabel>

            </td>
            <td>
                <telerik:RadTextBox ID="TxtNUMERO" runat="server" CssClass="fuenteTxt" Text='<%# Bind("NUMERO") %>' MaxLength="99" Width="150px" ReadOnly="true" ReadOnlyStyle-BackColor="LightGray" ></telerik:RadTextBox>
            </td>
            <td>
                <telerik:RadLabel ID="LblCUENTAID" runat="server" CssClass="LblMsj" Text="Cuenta ID" ></telerik:RadLabel>

            </td>
         
            <td>
                <telerik:RadTextBox ID="TxtCUENTAID" runat="server" CssClass="fuenteTxt" Text='<%# Bind("CUENTAID") %>' MaxLength="99" Width="150px" ReadOnly="true" ReadOnlyStyle-BackColor="LightGray" ></telerik:RadTextBox>

            </td>
         
            <td>
                <telerik:RadLabel ID="LblPRODUCTO" runat="server" CssClass="LblMsj" Text="Rol" ></telerik:RadLabel>

            </td>
         
            <td>
                <telerik:RadTextBox ID="TxtPRODUCTO" runat="server" CssClass="fuenteTxt" MaxLength="25" Width="150px" ReadOnly="true" ReadOnlyStyle-BackColor="LightGray" Text='<%# Bind("PRODUCTO") %>'></telerik:RadTextBox>

            </td>
         
        </tr>
        <tr>

            <td>
                 <telerik:RadLabel ID="RadLabel1" runat="server" CssClass="LblMsj" Text="Saldo Disponible" ></telerik:RadLabel>
            </td>
            <td>
                <telerik:RadNumericTextBox ID="TxtSALDO_DISP" runat="server" MaxLength="10" ReadOnly="true" ReadOnlyStyle-BackColor="LightGray"
                    Width="150px" Text='<%# Bind("SALDO_DISP") %>'></telerik:RadNumericTextBox>
            </td>
            <td>
                 <telerik:RadLabel ID="RadLabel2" runat="server" CssClass="LblMsj" Text="Saldo a disponer" ></telerik:RadLabel>
            </td>
           
            <td>
                <telerik:RadNumericTextBox ID="TxtSALDO_DISPUESTO" runat="server" MaxLength="10" MinValue="0" CssClass="w3-input"
                    Width="150px" Text='<%# Bind("SALDO_DISPUESTO") %>'></telerik:RadNumericTextBox>
            </td>
           
            <td>
                &nbsp;</td>
           
            <td>
                &nbsp;</td>
           
        </tr>
        <tr>
            <td>

                 <telerik:RadButton  ID="btnInsert" Text="INSERTAR" runat="server" CommandName="PerformInsert"
                            Visible='<%# (TypeOf DataItem Is Telerik.Web.UI.GridInsertionObject) %>'>
                        </telerik:RadButton>
            </td>
            <td>

                &nbsp;</td>
            <td>

                <telerik:RadButton ID="BtnAccion" runat="server"  Text="Actualizar" CommandName="Update" Visible='<%# Not (TypeOf DataItem Is Telerik.Web.UI.GridInsertionObject) %>'></telerik:RadButton>
                 </td>
            <td>

                <telerik:RadButton ID="Btncancelar" runat="server" Text="Cancelar"  CommandName="Cancel" CausesValidation="false"></telerik:RadButton>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>

    
    
<%--</telerik:RadAjaxPanel>--%>
