<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Etiquetas_whatsapp.ascx.vb" Inherits="Etiquetas_whatsapp" %>
<table>
    <tr>
        <td>
            <telerik:RadLabel ID="LblTabla" runat="server" CssClass="LblDesc" Text="Tabla" ></telerik:RadLabel>  
            <telerik:RadLabel ID="LblId" runat="server"   Visible="false" Text='<%# Bind("Id") %>'></telerik:RadLabel>
            <telerik:RadLabel ID="LblCampoval" runat="server"   Visible="false" Text='<%# Bind("Campo") %>'></telerik:RadLabel>
            <telerik:RadLabel ID="LblCamporeal" runat="server"   Visible="false" Text='<%# Bind("CampoReal") %>'></telerik:RadLabel>
        
        </td>
       
        <td>
            <telerik:RadDropDownList ID="DdlTabla" runat="server" AutoPostBack="True"  SelectedValue='<%# DataBinder.Eval(Container, "DataItem.Tabla") %>' DefaultMessage="Seleccione" >
            </telerik:RadDropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadLabel ID="LblCampo" runat="server" CssClass="LblDesc" Text="Campo" ></telerik:RadLabel>
        </td>
        <td>
            <telerik:RadDropDownList ID="DdlCampo" runat="server" DefaultMessage="Seleccione" AutoPostBack="true">
            </telerik:RadDropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadLabel ID="LblCat_Sm_Descripcion" runat="server" CssClass="LblDesc" Text="Etiqueta" ></telerik:RadLabel>
        </td>
        <td>
            <telerik:RadTextBox ID="TxtCat_Sm_Descripcion" runat="server" CssClass="textbox" MaxLength="21" Width="250px"  Text='<%# Bind("Etiqueta") %>'></telerik:RadTextBox>
        </td>
    </tr>
    <tr>
        <td></td>
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
