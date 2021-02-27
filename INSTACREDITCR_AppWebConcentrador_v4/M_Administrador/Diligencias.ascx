<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Diligencias.ascx.vb" Inherits="Productos" %>
<table>
    <tr>
        <td>
            <telerik:RadLabel runat="server" Text="Tipo de Diligencia"></telerik:RadLabel>
        </td>
        <td>
            <telerik:RadDropDownList ID="DdlDiligencia" runat="server">
                <Items>
                    <telerik:DropDownListItem Text="EMPLAZAMIENTO Y EMBARGO" Value="EMPLAZAMIENTO Y EMBARGO" />
                    <telerik:DropDownListItem Text="CAMBIO DE DEPOSITARIO" Value="CAMBIO DE DEPOSITARIO"/>
                    <telerik:DropDownListItem Text="EMPLAZAMIENTO" Value="EMPLAZAMIENTO" />
                    <telerik:DropDownListItem Text="EJECUCIÓN" Value="EJECUCIÓN" />
                    <telerik:DropDownListItem Text="DESAHOGO DE PRUEBAS" Value="DESAHOGO DE PRUEBAS" />
                    <telerik:DropDownListItem Text="REMATE" Value="REMATE"/>
                    <telerik:DropDownListItem Text="Seleccione" Selected="true" />
                </Items>
            </telerik:RadDropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadLabel runat="server" text="Tipo de Resultado"></telerik:RadLabel>
        </td>
        <td>
            <telerik:RadDropDownList ID="DdlResultado" runat="server">
                <Items>
                    <telerik:DropDownListItem Text="ÉXITOSO" Value="ÉXITOSO" />
                    <telerik:DropDownListItem Text="PARCIALMENTE EXITOSO" Value="PARCIALMENTE EXITOSO"/>
                    <telerik:DropDownListItem Text="NO ÉXITOSO" Value="NO ÉXITOSO"/>
                    <telerik:DropDownListItem Text="NO EJECUTADA" Value="NO EJECUTADA" />
                    <telerik:DropDownListItem Text="Seleccione" Selected="true" />
                </Items>
            </telerik:RadDropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadLabel runat="server" Text="Sub Resultado"></telerik:RadLabel>
        </td>
        <td>
            <telerik:RadDropDownList ID="DdlSubresultado" runat="server">
                <Items>
                    <telerik:DropDownListItem Text="Seleccione" Selected="true" />
                </Items>
            </telerik:RadDropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadLabel runat="server" Text="Marcar Participante"></telerik:RadLabel>
        </td>
        <td>
            <telerik:RadDropDownList ID="DdlMarcarParticipante" runat="server">
                <Items>
                    <telerik:DropDownListItem Text="SI" value="SI" />
                    <telerik:DropDownListItem Text="NO" value="NO" />
                    <telerik:DropDownListItem Text="NA" value="NA"/>
                    <telerik:DropDownListItem Text="Seleccione" Selected="true" />
                </Items>
            </telerik:RadDropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadLabel runat="server" Text="Registro Embargo Mueble"></telerik:RadLabel>
        </td>
        <td>
            <telerik:RadDropDownList ID="DdlRembueble" runat="server">
                <Items>
                   <telerik:DropDownListItem Text="SI" value="SI" />
                    <telerik:DropDownListItem Text="NO" value="NO" />
                    <telerik:DropDownListItem Text="NA" value="NA"/>
                    <telerik:DropDownListItem Text="Seleccione" Selected="true" />
                </Items>
            </telerik:RadDropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadLabel runat="server" Text="Registro Embargo Inmueble"></telerik:RadLabel>
        </td>
        <td>
            <telerik:RadDropDownList ID="DdlReinmueble" runat="server">
                <Items>
                    <telerik:DropDownListItem Text="SI" value="SI" />
                    <telerik:DropDownListItem Text="NO" value="NO" />
                    <telerik:DropDownListItem Text="NA" value="NA"/>
                    <telerik:DropDownListItem Text="Seleccione" Selected="true" />
                </Items>
            </telerik:RadDropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadLabel runat="server" Text="Registro Cambio de Depositario"></telerik:RadLabel>
        </td>
        <td>
            <telerik:RadDropDownList ID="DdlRcdepositario" runat="server">
                <Items>
                   <telerik:DropDownListItem Text="SI" value="SI" />
                    <telerik:DropDownListItem Text="NO" value="NO" />
                    <telerik:DropDownListItem Text="NA" value="NA"/>
                    <telerik:DropDownListItem Text="Seleccione" Selected="true" />
                </Items>
            </telerik:RadDropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadLabel runat="server" Text="Registro Impulso Procesal"></telerik:RadLabel>
        </td>
        <td>
            <telerik:RadDropDownList ID="DdlRiprocesal" runat="server">
                <Items>
                   <telerik:DropDownListItem Text="SI" value="SI" />
                    <telerik:DropDownListItem Text="NO" value="NO" />
                    <telerik:DropDownListItem Text="NA" value="NA"/>
                    <telerik:DropDownListItem Text="Seleccione" Selected="true" />
                </Items>
            </telerik:RadDropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadLabel runat="server" Text="Nombre de la Promoción"></telerik:RadLabel>
        </td>
        <td>
            <telerik:RadDropDownList ID="DdlPromocion" runat="server">
                <Items>
                    <telerik:DropDownListItem Text="Seleccione" Selected="true" />
                </Items>
            </telerik:RadDropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadLabel runat="server" Text="Notificación a Supervisor por Correo"></telerik:RadLabel>
        </td>
        <td>
            <telerik:RadDropDownList ID="DdlNotifCorreo" runat="server">
                <Items>
                   <telerik:DropDownListItem Text="SI" value="SI" />
                    <telerik:DropDownListItem Text="NO" value="NO" />
                    <telerik:DropDownListItem Text="NA" value="NA"/>
                    <telerik:DropDownListItem Text="Seleccione" Selected="true" />
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
