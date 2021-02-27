<%@ Control Language="VB" AutoEventWireup="false" CodeFile="TipoJuicio.ascx.vb" Inherits="TipoJuicio" %>
<style type="text/css">
    .auto-style1 {
        height: 24px;
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
        <td class="auto-style1">
            <Telerik:RadLabel ID="LblCAT_TJ_ID" runat="server" CssClass="LblDesc"   Visible="False" Text='<%# Bind("Id") %>'></Telerik:RadLabel>
        </td>
        <td class="auto-style1">
            <Telerik:RadLabel ID="LblCAT_TJ_TIPO0" runat="server" CssClass="LblDesc" Visible="False"   Text='<%# Bind("Tipo") %>'></Telerik:RadLabel>
        </td>
    </tr>
    <tr>
        <td>
            <Telerik:RadLabel ID="LblCAT_GA_GASTO" runat="server" CssClass="LblDesc" Text="Tipo De Gasto"  >
            </Telerik:RadLabel>
        </td>
        <td>
            <Telerik:RadTextBox ID="TxtCAT_TJ_TIPO" runat="server" CssClass="TxtDesc"   Text='<%# Bind("Tipo") %>'></Telerik:RadTextBox>
        </td>
    </tr>
    
    <tr class="Centro">
        <td>
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
    <telerik:RadWindowManager ID="RadAviso" runat="server" >
            <Localization OK="Aceptar" />
        </telerik:RadWindowManager>
</table>