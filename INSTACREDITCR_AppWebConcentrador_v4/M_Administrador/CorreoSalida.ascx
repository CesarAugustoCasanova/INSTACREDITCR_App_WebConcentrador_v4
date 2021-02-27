<%@ Control Language="VB" AutoEventWireup="false" CodeFile="CorreoSalida.ascx.vb" Inherits="CorreoSalida" %>
<table width="100%">
    <tr align="center">
        <td colspan="3">
            <telerik:RadLabel ID="lblmod" runat="server" BackColor="#3a4f63" Font-Italic="True" ForeColor="White"
                Style="font-size: small" Width="100%">
            </telerik:RadLabel>
        </td>
    </tr>
    <tr>
        <td align="right">&nbsp;
        </td>
        <td colspan="2">
            <telerik:RadLabel runat="server" ID="LblId" Text='<%# Bind("Usuario") %>' ></telerik:RadLabel>
        </td>
    </tr>
    <tr>
        <td align="right">
            <telerik:RadLabel ID="Label1" runat="server" CssClass="LblDesc" Text="Usuario" ></telerik:RadLabel>
        </td>
        <td colspan="2">
            <telerik:RadTextBox ID="TxtCAT_CONF_USER" runat="server" MaxLength="50" Text='<%# Bind("Usuario") %>' ></telerik:RadTextBox>
        </td>
    </tr>
    <tr>
        <td align="right">
            <telerik:RadLabel ID="Label2" runat="server" CssClass="LblDesc" Text="Password" ></telerik:RadLabel>
        </td>
        <td colspan="2">
            <telerik:RadTextBox ID="TxtCAT_CONF_PWD" runat="server" MaxLength="50" Text='<%# Bind("Password") %>' ></telerik:RadTextBox>
        </td>
    </tr>
    <tr>
        <td align="right">
            <telerik:RadLabel ID="Label3" runat="server" CssClass="LblDesc" Text="Host" ></telerik:RadLabel>
        </td>
        <td colspan="2">
            <telerik:RadTextBox ID="TxtCAT_CONF_HOST" runat="server" MaxLength="50" Text='<%# Bind("Host") %>' ></telerik:RadTextBox>
        </td>
    </tr>
    <tr>
        <td align="right">
            <telerik:RadLabel ID="Label4" runat="server" CssClass="LblDesc" Text="Puerto" ></telerik:RadLabel>
        </td>
        <td colspan="2">
            <telerik:RadTextBox ID="TxtCAT_CONF_PUERTO" runat="server" MaxLength="50" Text='<%# Bind("Puerto") %>' ></telerik:RadTextBox>
        </td>
    </tr>
    <tr>
        <td align="right">
            <telerik:RadLabel ID="Label5" runat="server" CssClass="LblDesc" Text="SSL" ></telerik:RadLabel>
        </td>
        <td colspan="2">
            <telerik:RadDropDownList ID="DdlCAT_CONF_SSL" runat="server" CssClass="DdlDesc" SelectedValue='<%# DataBinder.Eval(Container, "DataItem.SSL") %>' >
                <Items>
                    <telerik:DropDownListItem Value="-1" Text="Seleccione..." />
                    <telerik:DropDownListItem Value="0" Text="NO" />
                    <telerik:DropDownListItem Value="1" Text="SI" />
                </Items>
            </telerik:RadDropDownList>
        </td>
    </tr>
    <tr>
        <td align="right">
            <telerik:RadLabel ID="Label6" runat="server" CssClass="LblDesc" Text="Responsable" ></telerik:RadLabel>
        </td>
        <td colspan="2">
            <telerik:RadTextBox ID="TxtCAT_CONF_RESPONSABLE" runat="server" MaxLength="100" Text='<%# Bind("Responsable") %>' ></telerik:RadTextBox>
        </td>
    </tr>
    <tr>
        <td align="right">
            <telerik:RadLabel ID="Label9" runat="server" CssClass="LblDesc" Text="Salida Gestion" ></telerik:RadLabel>
        </td>
        <td colspan="2">
            <telerik:RadDropDownList ID="DdlSalidaGestion" runat="server" CssClass="DdlDesc" SelectedValue='<%# DataBinder.Eval(Container, "DataItem.Salida Gestion") %>' >
                <Items>
                    <telerik:DropDownListItem Value="-1" Text="Seleccione..." />
                    <telerik:DropDownListItem Value="0" Text="NO" />
                    <telerik:DropDownListItem Value="1" Text="SI" />
                </Items>

            </telerik:RadDropDownList>
        </td>
    </tr>
    <tr>
        <td></td>
    </tr>
    <tr>
        <td align="right">
            <telerik:RadButton ID="BtnAccion" runat="server"  Text="Actualizar" CommandName="Update" Visible='<%# Not (TypeOf DataItem Is Telerik.Web.UI.GridInsertionObject) %>'></telerik:RadButton>
            <telerik:RadButton  ID="btnInsert" Text="INSERTAR" runat="server" CommandName="PerformInsert"
                Visible='<%# (TypeOf DataItem Is Telerik.Web.UI.GridInsertionObject) %>'>
            </telerik:RadButton>
        </td>
        <td>
            
        </td>
        <td>
            <telerik:RadButton ID="BtnCancelar" runat="server" CssClass="Botones" Text="Cancelar" Visible="True"  CommandName="Cancel" />
        </td>
    </tr>
    <tr>
        <td align="right">&nbsp;
        </td>
        <td colspan="2">&nbsp;
        </td>
    </tr>
    <telerik:RadWindowManager ID="RadAviso" runat="server" >
        <Localization OK="Aceptar" />
    </telerik:RadWindowManager>
</table>
