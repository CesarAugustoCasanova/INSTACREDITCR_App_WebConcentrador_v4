<%@ Control Language="VB" AutoEventWireup="false" CodeFile="DocumentosBase.ascx.vb" Inherits="DocumentosBase" %>
<table> 
     <tr>
        <td>
            <telerik:RadLabel ID="LblNombre" runat="server" CssClass="LblDesc" Text="Nombre Documento" >
            </telerik:RadLabel>
        </td>
        <td>
            <telerik:RadTextBox ID="TxtNombre" runat="server" >
            </telerik:RadTextBox>             
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadLabel ID="LblDescripcion" runat="server" CssClass="LblDesc" Text="Descripcion" >
            </telerik:RadLabel>
        </td>
        <td>
            <telerik:RadTextBox ID="TxtDescripcion" runat="server" >
            </telerik:RadTextBox>             
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadLabel ID="LblFechainicio" runat="server" CssClass="LblDesc" Text="Fecha del evento para inicio del conteo" >
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
            <telerik:RadLabel ID="LblDias" runat="server" CssClass="LblDesc" Text="Tiempo en dias" ></telerik:RadLabel>
        </td>
        <td>
            <telerik:RadnumericTextBox ID="NtxtDias" runat="server" CssClass="fuenteTxt" MinValue="0" Type="Number" NumberFormat-DecimalDigits="0"  ></telerik:RadnumericTextBox>        
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadLabel ID="LblTipoDias" runat="server" CssClass="LblDesc" Text="Tipo Dias" >
            </telerik:RadLabel>
        </td>
        <td>
            <telerik:RadDropDownList ID="DdlTipo" runat="server" >
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