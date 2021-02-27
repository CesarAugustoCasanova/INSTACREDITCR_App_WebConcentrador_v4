<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Promociones.ascx.vb" Inherits="Promociones" %>
<table> 
     <tr>
        <td>
            <telerik:RadLabel ID="LblNombre" runat="server" CssClass="LblDesc" Text="Nombre promocion" >
            </telerik:RadLabel>
        </td>
        <td>
            <telerik:RadTextBox ID="TxtNombre" runat="server" >
            </telerik:RadTextBox>             
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadLabel ID="LblComodin" runat="server" CssClass="LblDesc" Text="Comodin" >
            </telerik:RadLabel>
        </td>
        <td>
            <telerik:RadDropDownList ID="DdlComodin" runat="server"  AutoPostBack="false">
                <Items>
                    <telerik:DropDownListItem Value="1" Text="SI"/>
                    <telerik:DropDownListItem Value="0" Text="NO" />
                </Items>
            </telerik:RadDropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadLabel ID="LblCambiaEtapa" runat="server" CssClass="LblDesc" Text="Impulsa etapa" >
            </telerik:RadLabel>
        </td>
        <td>
            <telerik:RadDropDownList ID="DdlCambiaEtapa" runat="server"  AutoPostBack="false">
                <Items>
                    <telerik:DropDownListItem Value="1" Text="SI"/>
                    <telerik:DropDownListItem Value="0" Text="NO" />
                </Items>
            </telerik:RadDropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadLabel ID="LblDiasRespuesta" runat="server" CssClass="LblDesc" Text="Dias Respuesta" ></telerik:RadLabel>
        </td>
        <td>
            <telerik:RadnumericTextBox ID="NtxtDiasRespuesta" runat="server" CssClass="fuenteTxt" MinValue="0" Type="Number" NumberFormat-DecimalDigits="0"  ></telerik:RadnumericTextBox>        
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadLabel ID="LblTipoDiasRes" runat="server" CssClass="LblDesc" Text="Tipo dias respuesta" >
            </telerik:RadLabel>
        </td>
        <td>
            <telerik:RadDropDownList ID="DdlTipoDiasRes" runat="server"  AutoPostBack="false">
                <Items>
                    <telerik:DropDownListItem Value="0" Text="Habil"/>
                    <telerik:DropDownListItem Value="1" Text="Inhabil" />
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