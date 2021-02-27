<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Editar.ascx.vb" Inherits="Editar" %>

<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" ></telerik:RadAjaxLoadingPanel>
<%--<telerik:RadAjaxPanel ID="UPNLGen" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" BackColor="#999999">--%>

    <table>
        <tr>

            <td>
                <telerik:RadLabel ID="LblCat_Ag_Nombre" runat="server" CssClass="LblMsj" Text="Nombre" ></telerik:RadLabel>

            </td>
            <td>
                <telerik:RadTextBox ID="TxtCat_Ag_Nombre" runat="server" CssClass="fuenteTxt" Text='<%# Bind( "Nombre") %>' MaxLength="99" Width="500px" ReadOnly="true" ReadOnlyStyle-BackColor="LightGray" ></telerik:RadTextBox>
            </td>
            <td>
                <telerik:RadLabel ID="LblCat_Ag_Usuario" runat="server" CssClass="LblMsj" Text="Usuario" ></telerik:RadLabel>
            </td>
            <td>
                <telerik:RadTextBox ID="TxtCat_Ag_Usuario" runat="server" CssClass="fuenteTxt" MaxLength="25" Width="150px" ReadOnly="true" ReadOnlyStyle-BackColor="LightGray" Text='<%# Bind("Usuario") %>'></telerik:RadTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadLabel ID="LblCat_Ag_Estatus" runat="server" CssClass="LblMsj" Text="Estatus"  Visible='<%# Not (TypeOf DataItem Is Telerik.Web.UI.GridInsertionObject) %>'></telerik:RadLabel>
            </td>
            <td>
                <telerik:RadDropDownList ID="DdlCat_Ag_Estatus" runat="server"  SelectedValue='<%# DataBinder.Eval(Container, "DataItem.Estatus") %>' AutoPostBack="true" Visible='<%# Not (TypeOf DataItem Is Telerik.Web.UI.GridInsertionObject) %>'>
                    <Items>
                        <telerik:DropDownListItem Value="Seleccione" Text="Seleccione"/>
                        <telerik:DropDownListItem Value="Activa" Text="Activa" />
                        <telerik:DropDownListItem Value="Baja" Text="Baja" />
                        <telerik:DropDownListItem Value="Inactiva" Text="Inactiva" />
                    </Items>
                </telerik:RadDropDownList>

            </td>

            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <telerik:RadLabel ID="LblCat_Ag_Motivo" runat="server" CssClass="LblMsj" Text="Motivo" Visible="False" ></telerik:RadLabel>
            </td>
            <td>
                <telerik:RadTextBox ID="TxtCat_Ag_Motivo" runat="server" CssClass="fuenteTxt" Height="58px" MaxLength="200" TextMode="MultiLine" Visible="False" Width="394px"  Text='<%# Bind("Motivo") %>'></telerik:RadTextBox>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>

                <telerik:RadButton ID="BtnAccion" runat="server"  Text="Actualizar" CommandName="Update" Visible='<%# Not (TypeOf DataItem Is Telerik.Web.UI.GridInsertionObject) %>'></telerik:RadButton>
                 <telerik:RadButton  ID="btnInsert" Text="INSERTAR" runat="server" CommandName="PerformInsert"
                            Visible='<%# (TypeOf DataItem Is Telerik.Web.UI.GridInsertionObject) %>'>
                        </telerik:RadButton>
            </td>
            <td>

                <telerik:RadButton ID="Btncancelar" runat="server" Text="Cancelar"  CommandName="Cancel" CausesValidation="false"></telerik:RadButton>
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>

    
    
<%--</telerik:RadAjaxPanel>--%>
