<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Castigo.ascx.vb" Inherits="Castigo" %>
<table>
    <tr>
        <td>
            <telerik:RadLabel ID="LblInstancia" runat="server" CssClass="LblDesc" Text="Instancia" >
            </telerik:RadLabel>
        </td>
        <td>
            <telerik:RadDropDownList ID="DdlInstancia" runat="server"  AutoPostBack="true" DefaultMessage="Seleccione">                
            </telerik:RadDropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadLabel ID="LblUsuario" runat="server" CssClass="LblDesc" Text="Usuario"  Visible="false">
            </telerik:RadLabel>
        </td>
        <td>
            <telerik:RadDropDownList ID="DdlUsuario" runat="server"  AutoPostBack="false" Visible="false">
            </telerik:RadDropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadLabel ID="LblOrden" runat="server" CssClass="LblDesc" Text="Orden" >
            </telerik:RadLabel>
        </td>
        <td>
            <telerik:RadDropDownList ID="DdlOrden" runat="server"  AutoPostBack="false">
                <Items>
                    <telerik:DropDownListItem Value="1" Text="1"/>
                    <telerik:DropDownListItem Value="2" Text="2"/>
                    <telerik:DropDownListItem Value="3" Text="3"/>
                    <telerik:DropDownListItem Value="4" Text="4"/>
                </Items>
            </telerik:RadDropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadLabel ID="LblCorreos" runat="server" CssClass="LblDesc" Text="Correos" >
            </telerik:RadLabel>
        </td>
        <td>
            <telerik:RadTextBox ID="RTBCorreos" runat="server" ></telerik:RadTextBox>
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadButton ID="BtnAccion" runat="server"  Text="Actualizar" CommandName="Update" Visible='<%# Not (TypeOf DataItem Is Telerik.Web.UI.GridInsertionObject) %>'></telerik:RadButton>
            <telerik:RadButton  ID="btnInsert" Text="Insertar" runat="server" CommandName="PerformInsert"
                Visible='<%# (TypeOf DataItem Is Telerik.Web.UI.GridInsertionObject) %>'>
            </telerik:RadButton>
        </td>
        <td>
            <telerik:RadButton ID="RBtnCacelar" runat="server"  Text="Cancelar" CommandName="Cancel" ></telerik:RadButton>
        </td>
    </tr>
</table>