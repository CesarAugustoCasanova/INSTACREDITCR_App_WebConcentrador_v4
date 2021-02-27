<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Gastos.ascx.vb" Inherits="Gastos" %>
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
            <Telerik:RadLabel ID="LblCAT_GA_ID" runat="server" CssClass="LblDesc"   Visible="False" Text='<%# Bind("Id") %>'></Telerik:RadLabel>
        </td>
        <td class="auto-style1">
            <Telerik:RadLabel ID="LblCAT_GA_GASTO0" runat="server" CssClass="LblDesc" Visible="False"  ></Telerik:RadLabel>
        </td>
    </tr>
    <tr>
        <td>
            <Telerik:RadLabel ID="LblCAT_GA_GASTO" runat="server" CssClass="LblDesc" Text="Tipo De Gasto"  >
            </Telerik:RadLabel>
        </td>
        <td>
            <Telerik:RadTextBox ID="TxtCAT_GA_GASTO" runat="server" CssClass="TxtDesc"   Text='<%# Bind("Tipo") %>'></Telerik:RadTextBox>
        </td>
    </tr>
    <tr>
        <td>
            <Telerik:RadLabel ID="LblCAT_GA_MONTO" runat="server" CssClass="LblDesc" Text="Monto Del Gasto"  ></Telerik:RadLabel>
        </td>
        <td>
            <Telerik:RadNumericTextBox ID="TxtCAT_GA_MONTO" runat="server" CssClass="TxtDesc" MinValue="1" ShowSpinButtons="True" Text='<%# Bind("Monto") %>'   Culture="es-MX" DbValueFactor="1" LabelWidth="64px" Type="Currency" Width="160px" >
<NegativeStyle Resize="None"></NegativeStyle>

<NumberFormat ZeroPattern="$n"></NumberFormat>

<EmptyMessageStyle Resize="None"></EmptyMessageStyle>

<ReadOnlyStyle Resize="None"></ReadOnlyStyle>

<FocusedStyle Resize="None"></FocusedStyle>

<DisabledStyle Resize="None"></DisabledStyle>

<InvalidStyle Resize="None"></InvalidStyle>

<HoveredStyle Resize="None"></HoveredStyle>

<EnabledStyle Resize="None"></EnabledStyle>
            </Telerik:RadNumericTextBox>
           
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
