<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Resultados.ascx.vb" Inherits="Resultados" %>
<table> 
     <tr>
        <td>
            <telerik:RadLabel ID="LblNombre" runat="server" CssClass="LblDesc" Text="Nombre resultado" >
            </telerik:RadLabel>
        </td>
        <td>
            <telerik:RadTextBox ID="TxtNombre" runat="server" >
            </telerik:RadTextBox>             
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadLabel ID="LblCambiaEtapa" runat="server" CssClass="LblDesc" Text="Cambia etapa" >
            </telerik:RadLabel>
        </td>
        <td>
            <telerik:RadDropDownList ID="DdlCambiaEtapa" runat="server"  AutoPostBack="false">       
                <Items>
                    <telerik:DropDownListItem Value="1" Text="SI" />
                    <telerik:DropDownListItem Value="0" Text="NO" />
                </Items>         
            </telerik:RadDropDownList>
        </td>
    </tr>      
    <tr>
        <td>
            <telerik:RadLabel ID="LblDisminucion" runat="server" CssClass="LblDesc" Text="Disminucion contador" ></telerik:RadLabel>
        </td>
        <td>
            <telerik:RadDropDownList ID="DdlDisminucion" runat="server"  AutoPostBack="false">       
                <Items>
                    <telerik:DropDownListItem Value="1" Text="SI" />
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