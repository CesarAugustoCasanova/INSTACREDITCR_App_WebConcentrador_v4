<%@ Control Language="VB" AutoEventWireup="false" CodeFile="DiasInhabiles.ascx.vb" Inherits="DiasInhabiles" %>
<table> 
     <tr>
        <td>
            <telerik:RadLabel ID="LblNombre" runat="server" CssClass="LblDesc" Text="Nombre Periodo" >
            </telerik:RadLabel>
        </td>
        <td>
            <telerik:RadTextBox ID="TxtNombre" runat="server" >
            </telerik:RadTextBox>             
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadLabel ID="LblFechainicio" runat="server" CssClass="LblDesc" Text="Fecha inicio" >
            </telerik:RadLabel>
        </td>
        <td>
            <telerik:RadDatePicker ID="RDPFechaInicio" runat="server" >
            <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True" Culture="es-MX" FastNavigationNextText="&amp;lt;&amp;lt;"  runat="server"></Calendar>
            <DateInput DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" LabelWidth="40%" runat="server">
                <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                <FocusedStyle Resize="None"></FocusedStyle>
                <DisabledStyle Resize="None"></DisabledStyle>
                <InvalidStyle Resize="None"></InvalidStyle>
                <HoveredStyle Resize="None"></HoveredStyle>
                <EnabledStyle Resize="None"></EnabledStyle>
            </DateInput>
            </telerik:RadDatePicker>            
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadLabel ID="LblFechafin" runat="server" CssClass="LblDesc" Text="Fecha fin" >
            </telerik:RadLabel>
        </td>
        <td>
            <telerik:RadDatePicker ID="RDPFechaFin" runat="server" >
            <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True" Culture="es-MX" FastNavigationNextText="&amp;lt;&amp;lt;"  runat="server"></Calendar>
            <DateInput DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" LabelWidth="40%" runat="server">
                <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                <FocusedStyle Resize="None"></FocusedStyle>
                <DisabledStyle Resize="None"></DisabledStyle>
                <InvalidStyle Resize="None"></InvalidStyle>
                <HoveredStyle Resize="None"></HoveredStyle>
                <EnabledStyle Resize="None"></EnabledStyle>
            </DateInput>
            </telerik:RadDatePicker>            
        </td>
    </tr>    
    <tr>
        <td>
            <telerik:RadLabel ID="LblEstado" runat="server" CssClass="LblDesc" Text="Estado" >
            </telerik:RadLabel>
        </td>
        <td>
            <telerik:radcombobox ID="DdlEstado" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" Localization-AllItemsCheckedString="Aplica para todos" Localization-CheckAllString="Aplica para todos"  >
            </telerik:radcombobox>
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