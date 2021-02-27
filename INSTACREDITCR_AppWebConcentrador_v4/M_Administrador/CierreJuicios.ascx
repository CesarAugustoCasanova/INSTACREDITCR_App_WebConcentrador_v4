<%@ Control Language="VB" AutoEventWireup="false" CodeFile="CierreJuicios.ascx.vb" Inherits="CierreJuicios" %>
<table> 
     <tr>
        <td>
            <telerik:RadLabel ID="LblNombre" runat="server" CssClass="LblDesc" Text="Nombre cierre" >
            </telerik:RadLabel>
        </td>
        <td>
            <telerik:RadTextBox ID="TxtNombre" runat="server" >
            </telerik:RadTextBox>             
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadLabel ID="LblValidaProm" runat="server" CssClass="LblDesc" Text="Valida promocion" >
            </telerik:RadLabel>
        </td>
        <td>
            <telerik:RadDropDownList ID="DdlValidaProm" runat="server"  AutoPostBack="true">
                <Items>
                    <telerik:DropDownListItem Value="1" Text="SI"/>
                    <telerik:DropDownListItem Value="0" Text="NO" Selected="true"/>
                </Items>
            </telerik:RadDropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadLabel ID="LblNombreProm" runat="server" CssClass="LblDesc" Text="Nombre promocion"  Visible="false">
            </telerik:RadLabel>
        </td>
        <td>
            <telerik:RadDropDownList ID="DdlNombreProm" runat="server"  AutoPostBack="false" Visible="false">              
            </telerik:RadDropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadLabel ID="LblValidaDiasMora" runat="server" CssClass="LblDesc" Text="Valida dias mora" >
            </telerik:RadLabel>
        </td>
        <td>
            <telerik:RadDropDownList ID="DdlValidaDiasMora" runat="server"  AutoPostBack="false">
                <Items>
                    <telerik:DropDownListItem Value="1" Text="SI"/>
                    <telerik:DropDownListItem Value="0" Text="NO" />
                </Items>
            </telerik:RadDropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadLabel ID="LblSuperior" runat="server" CssClass="LblDesc" Text="Validación por superior" >
            </telerik:RadLabel>
        </td>
        <td>
            <telerik:RadDropDownList ID="DDLSuperior" runat="server"  AutoPostBack="false">
                <Items>
                    <telerik:DropDownListItem Value="1" Text="SI"/>
                    <telerik:DropDownListItem Value="0" Text="NO" />
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