<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Productos.ascx.vb" Inherits="Productos" %>
<table>
    <tr>
        <td>
            <telerik:RadLabel ID="LblMercado" runat="server" CssClass="LblDesc" Text="Mercado" ></telerik:RadLabel>
        </td>
        <td>
            <telerik:RadDropDownList ID="DdlMercado" runat="server" >
            </telerik:RadDropDownList>
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
