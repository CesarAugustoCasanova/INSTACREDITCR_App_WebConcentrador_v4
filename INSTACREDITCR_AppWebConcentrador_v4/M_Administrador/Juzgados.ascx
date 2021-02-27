<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Juzgados.ascx.vb" Inherits="Juzgados" %>
<table> 
     <tr>
        <td>
            <telerik:RadLabel ID="LblNombre" runat="server" CssClass="LblDesc" Text="Nombre Juzgado" >
            </telerik:RadLabel>
        </td>
        <td>
            <telerik:RadTextBox ID="TxtNombre" runat="server" >
            </telerik:RadTextBox>
        </td>
    </tr>
    <tr>
    <tr>
        <td>
            <telerik:RadLabel ID="LblEstado" runat="server" CssClass="LblDesc" Text="Estado" ></telerik:RadLabel>
        </td>
        <td>
                  
            <telerik:RadDropDownList ID="DdlEstado" runat="server"  AutoPostBack="true">                
            </telerik:RadDropDownList>      
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadLabel ID="LblMunicipio" runat="server" CssClass="LblDesc" Text="Municipio" ></telerik:RadLabel>
        </td>
        <td>
                  
            <telerik:RadDropDownList ID="DdlMunicipio" runat="server" >                
            </telerik:RadDropDownList>      
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadLabel ID="LblEstatus" runat="server" CssClass="LblDesc" Text="Estatus" ></telerik:RadLabel>
        </td>
        <td>
                  
            <telerik:RadDropDownList ID="DdlEstatus" runat="server" >
                <Items>
                    <telerik:DropDownListItem Value="0" text="Inactivo/Desactivado" />
                    <telerik:DropDownListItem Value="1" text="Activo" />
                </Items>              
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