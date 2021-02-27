<%@ Control Language="VB" AutoEventWireup="false" CodeFile="TramitesJudiciales.ascx.vb" Inherits="TramitesJudiciales" %>
<table> 
     <tr>
        <td>
            <telerik:RadLabel ID="LblTipoTramite" runat="server" CssClass="LblDesc" Text="Tipo tramite" >
            </telerik:RadLabel>
        </td>
        <td>
            <telerik:RadDropDownList ID="DdlTipoTramite" runat="server"  AutoPostBack="false">
            </telerik:RadDropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadLabel ID="LblTipoInscripcion" runat="server" CssClass="LblDesc" Text="Tipo inscripcion" >
            </telerik:RadLabel>
        </td>
        <td>
            <telerik:RadDropDownList ID="DdlTipoInscripcion" runat="server"  AutoPostBack="false">
                <Items>
                    <telerik:DropDownListItem Value="1" Text="SI"/>
                    <telerik:DropDownListItem Value="0" Text="NO" />
                </Items>
            </telerik:RadDropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadLabel ID="LblBienEmbargado" runat="server" CssClass="LblDesc" Text="Bien embargado" >
            </telerik:RadLabel>
        </td>
        <td>
            <telerik:RadDropDownList ID="DdlBienEmbargado" runat="server"  AutoPostBack="false">
                <Items>
                    <telerik:DropDownListItem Value="1" Text="SI"/>
                    <telerik:DropDownListItem Value="0" Text="NO" />
                </Items>
            </telerik:RadDropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadLabel ID="LblFolioInmobiliario" runat="server" CssClass="LblDesc" Text="Folio Inmobiliario" >
            </telerik:RadLabel>
        </td>
        <td>
            <telerik:RadDropDownList ID="DdlFolioInmobiliario" runat="server"  AutoPostBack="false">
                <Items>
                    <telerik:DropDownListItem Value="1" Text="SI"/>
                    <telerik:DropDownListItem Value="0" Text="NO" />
                </Items>
            </telerik:RadDropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadLabel ID="LblFechaEvento" runat="server" CssClass="LblDesc" Text="Fecha Evento" >
            </telerik:RadLabel>
        </td>
        <td>
            <telerik:RadDropDownList ID="DdlFechaEvento" runat="server"  AutoPostBack="false">
                <Items>
                    <telerik:DropDownListItem Value="1" Text="SI"/>
                    <telerik:DropDownListItem Value="0" Text="NO" />
                </Items>
            </telerik:RadDropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadLabel ID="LblHoraEvento" runat="server" CssClass="LblDesc" Text="Hora Evento" >
            </telerik:RadLabel>
        </td>
        <td>
            <telerik:RadDropDownList ID="DdlHoraEvento" runat="server"  AutoPostBack="false">
                <Items>
                    <telerik:DropDownListItem Value="1" Text="SI"/>
                    <telerik:DropDownListItem Value="0" Text="NO" />
                </Items>
            </telerik:RadDropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadLabel ID="LblPosicion" runat="server" CssClass="LblDesc" Text="Posicion" >
            </telerik:RadLabel>
        </td>
        <td>
            <telerik:RadDropDownList ID="DdlPosicion" runat="server"  AutoPostBack="false">
                <Items>
                    <telerik:DropDownListItem Value="1" Text="SI"/>
                    <telerik:DropDownListItem Value="0" Text="NO" />
                </Items>
            </telerik:RadDropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadLabel ID="LblObservaciones" runat="server" CssClass="LblDesc" Text="Observaciones" >
            </telerik:RadLabel>
        </td>
        <td>
            <telerik:RadDropDownList ID="DdlObservaciones" runat="server"  AutoPostBack="false">
                <Items>
                    <telerik:DropDownListItem Value="1" Text="SI"/>
                    <telerik:DropDownListItem Value="0" Text="NO" />
                </Items>
            </telerik:RadDropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadLabel ID="LblFechaAvaluo" runat="server" CssClass="LblDesc" Text="Fecha Avaluo" >
            </telerik:RadLabel>
        </td>
        <td>
            <telerik:RadDropDownList ID="DdlFechaAvaluo" runat="server"  AutoPostBack="false">
                <Items>
                    <telerik:DropDownListItem Value="1" Text="SI"/>
                    <telerik:DropDownListItem Value="0" Text="NO" />
                </Items>
            </telerik:RadDropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadLabel ID="LblValorComercial" runat="server" CssClass="LblDesc" Text="Valor Comercial" >
            </telerik:RadLabel>
        </td>
        <td>
            <telerik:RadDropDownList ID="DdlValorComercial" runat="server"  AutoPostBack="false">
                <Items>
                    <telerik:DropDownListItem Value="1" Text="SI"/>
                    <telerik:DropDownListItem Value="0" Text="NO" />
                </Items>
            </telerik:RadDropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadLabel ID="LblGarantia" runat="server" CssClass="LblDesc" Text="Garantia" >
            </telerik:RadLabel>
        </td>
        <td>
            <telerik:RadDropDownList ID="DdlGarantia" runat="server"  AutoPostBack="false">
                <Items>
                    <telerik:DropDownListItem Value="1" Text="SI"/>
                    <telerik:DropDownListItem Value="0" Text="NO" />
                </Items>
            </telerik:RadDropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadLabel ID="LblOrigenGarantia" runat="server" CssClass="LblDesc" Text="Origen Garantia" >
            </telerik:RadLabel>
        </td>
        <td>
            <telerik:RadDropDownList ID="DdlOrigenGarantia" runat="server"  AutoPostBack="false">
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