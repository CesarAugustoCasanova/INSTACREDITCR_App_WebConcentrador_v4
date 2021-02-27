<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Acuerdos.ascx.vb" Inherits="Acuerdos" %>
<table>
    <tr>
        <td>
            <telerik:RadLabel ID="LblNombre" runat="server" CssClass="LblDesc" Text="Nombre acuerdo" >
            </telerik:RadLabel>
        </td>
        <td>
            <telerik:RadTextBox ID="TxtNombre" runat="server" >
            </telerik:RadTextBox>             
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadLabel ID="LblIndicadorImpulso" runat="server" CssClass="LblDesc" Text="Indicador impulso" >
            </telerik:RadLabel>
        </td>
        <td>
            <telerik:RadDropDownList ID="DdlIndicadorimpulso" runat="server"  AutoPostBack="false">
                <Items>
                    <telerik:DropDownListItem Value="1" Text="SI"/>
                    <telerik:DropDownListItem Value="0" Text="NO" />
                </Items>
            </telerik:RadDropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadLabel ID="LblCambiaEtapa" runat="server" CssClass="LblDesc" Text="Cambia etapa" >
            </telerik:RadLabel>
        </td>
        <td>
            <telerik:RadDropDownList ID="DdlCambiaEtapa" runat="server"  AutoPostBack="true">
                <Items>
                    <telerik:DropDownListItem Value="1" Text="SI"/>
                    <telerik:DropDownListItem Value="0" Text="NO" />
                </Items>
            </telerik:RadDropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadLabel ID="LblEtapaDestino" runat="server" CssClass="LblDesc" Text="Etapa destino" >
            </telerik:RadLabel>
        </td>
        <td>
            <telerik:RadDropDownList ID="DdlEtapaDestino" runat="server"  AutoPostBack="false">
                </telerik:RadDropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadLabel ID="LblPagoHonorarios" runat="server" CssClass="LblDesc" Text="Pago de Avance" >
            </telerik:RadLabel>
        </td>
        <td>
            <telerik:RadDropDownList ID="DdlPagoHonorarios" runat="server"  AutoPostBack="true">
                <Items>
                    <telerik:DropDownListItem Value="1" Text="SI"/>
                    <telerik:DropDownListItem Value="0" Text="NO" />
                </Items>
            </telerik:RadDropDownList>
        </td>
    </tr>
     <tr>
        <td>
            <telerik:RadLabel ID="LblConcepto" runat="server" CssClass="LblDesc" Text="Concepto Pago" >
            </telerik:RadLabel>
        </td>
        <td>
            <telerik:RadTextBox runat="server" ID="txtConcepto"></telerik:RadTextBox>
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadLabel ID="LblMontoPagar" runat="server" CssClass="LblDesc" Text="Monto a pagar" ></telerik:RadLabel>
        </td>
        <td>
            <telerik:RadnumericTextBox ID="NtxtMontoPagar" runat="server" CssClass="fuenteTxt" MinValue="0" Type="Number"  ></telerik:RadnumericTextBox>        
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadLabel ID="LblPagoExterno" runat="server" CssClass="LblDesc" Text="Pago externo" >
            </telerik:RadLabel>
        </td>
        <td>
            <telerik:RadDropDownList ID="DdlPagoExterno" runat="server"  AutoPostBack="false">
                <Items>
                    <telerik:DropDownListItem Value="1" Text="SI"/>
                    <telerik:DropDownListItem Value="0" Text="NO" />
                </Items>
            </telerik:RadDropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadLabel ID="LblIncluyeIVA" runat="server" CssClass="LblDesc" Text="Incluye IVA" >
            </telerik:RadLabel>
        </td>
        <td>
            <telerik:RadDropDownList ID="DdlIncluyeIVA" runat="server"  AutoPostBack="false">
                <Items>
                    <telerik:DropDownListItem Value="1" Text="SI"/>
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