<%@ Control Language="VB" AutoEventWireup="false" CodeFile="CatJudiciales.ascx.vb" Inherits="CatJudiciales" %>
<table class="Table">
    <%--  <tr class="Titulos">
        <td>
            <telerik:RadLabel ID="LblTipo" runat="server"></telerik:RadLabel>
        </td>
    </tr>--%>
    <tr align="center" style="background-color: aliceblue">
        <td>
            <table>
                <tr>
                    <td colspan="3">
                        <telerik:RadTextBox ID="LblHLabel" runat="server" Style="visibility: hidden" Text='<%# Bind("ID") %>'></telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadLabel ID="LblCat_Descripcion" runat="server" Text="Nombre" ></telerik:RadLabel>
                    </td>
                    <td colspan="2">
                        <telerik:RadTextBox ID="TxtCat_Descripcion" runat="server" Text='<%# Bind("NOMBRE") %>' ></telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadLabel ID="LblComodin" runat="server" Text="¿Comodin?" Visible="false" ></telerik:RadLabel>
                    </td>
                    <td colspan="2">
                        <telerik:RadCheckBox ID="ChkbxComodin" runat="server" Visible="false"   AutoPostBack="false"/>
                    </td>
                </tr>

                <tr>
                    <td>&nbsp;</td>
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadButton ID="BtnAccion" runat="server"  Text="Actualizar" CommandName="Update" Visible='<%# Not (TypeOf DataItem Is Telerik.Web.UI.GridInsertionObject) %>'></telerik:RadButton>
            <telerik:RadButton  ID="btnInsert" Text="Insertar" runat="server" CommandName="PerformInsert"
                Visible='<%# (TypeOf DataItem Is Telerik.Web.UI.GridInsertionObject) %>'>
            </telerik:RadButton>
                    </td>
                    <td colspan="2">
                        <telerik:RadButton ID="RBtnCacelar" runat="server"  Text="Cancelar" CommandName="Cancel"></telerik:RadButton>
                    </td>

                </tr>
            </table>
        </td>
    </tr>
</table>

