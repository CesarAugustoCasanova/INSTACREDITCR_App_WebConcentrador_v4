<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Sistema.ascx.vb" Inherits="Perfiles" %>
<style type="text/css">
    .auto-style1 {
        left: 113px;
        top: 4px;
    }
</style>
<script languaje="javascript" type="text/javascript">

    function confirmCallbackFn(arg) {
        if (arg) {

            __doPostBack('ctl00$CPHMaster$BtnAceptarConfirmacion', '')

        }
    }
</script>
<table>
    <tr>
        <td>
            <telerik:RadLabel ID="LblId" runat="server" CssClass="LblMsj" Text='<%# Bind("ID") %>'  Visible="false"></telerik:RadLabel>
            <telerik:RadLabel ID="LblDescripcion" runat="server" CssClass="LblMsj" Text='<%# Session("TEXTO") %>' ></telerik:RadLabel>
        </td>
        <td>
            <telerik:RadTextBox ID="TxtDescrpcion" runat="server" CssClass="fuenteTxt" Width="394px"  Text='<%# Bind("Descripcion") %>'></telerik:RadTextBox>
        </td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>

            <telerik:RadButton ID="Btnconfirmar" runat="server"  Text="Actualizar" CommandName="Update" Visible='<%# Not (TypeOf DataItem Is Telerik.Web.UI.GridInsertionObject) %>'></telerik:RadButton>
            <telerik:RadButton ID="BtnNuevo" runat="server"  Text="Agregar" CommandName="PerformInsert"
                Visible='<%# (TypeOf DataItem Is Telerik.Web.UI.GridInsertionObject) %>'>
            </telerik:RadButton>

        </td>
        <td>
            <telerik:RadButton ID="BtnAceptarConfirmacion" runat="server"  Text="Eliminar" CommandName="Delete" CssClass="auto-style1" Visible='<%# Not (TypeOf DataItem Is Telerik.Web.UI.GridInsertionObject) %>'></telerik:RadButton>
        </td>
        <td>
            <telerik:RadButton ID="Btncancelar" runat="server" Text="Cancelar"  CommandName="Cancel" CausesValidation="false"></telerik:RadButton>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>
</table>
<telerik:RadWindowManager ID="RadAviso" runat="server" >
    <Localization OK="Aceptar" />
</telerik:RadWindowManager>
