<%@ Page Title="" Language="VB" MasterPageFile="MasterPage.master" AutoEventWireup="false" CodeFile="ExcepcionDeMedios.aspx.vb" Inherits="ExcepcionDeMedios" %>



<asp:Content ID="Content1" ContentPlaceHolderID="CPHMaster" runat="Server">
    <link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css" />


    <%--<div class="Pagina">--%>
    <telerik:RadAjaxLoadingPanel ID="Radpanelcarga" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="Pnlgen" runat="server" HorizontalAlign="NotSet" LoadingPanelID="Radpanelcarga">
        <table class="Table">
            <tr>
                <td class="Titulos" colspan="2">
                    <asp:Label ID="LblTitulo" runat="server" Text="Excepción Campañas De Contactación"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                            <tr class="LblDesc">
                   <td >
                       <telerik:RadLabel runat="server" ID="RLblElemento" Text="Seleccione"></telerik:RadLabel>
                       <telerik:RadDropDownList runat="server" ID="RdElemento" SelectedText="RdTipo" AutoPostBack="true">
                           <Items>
                               <telerik:DropDownListItem runat="server" Value="Seleccione" Selected="True" Text="Seleccione"/>
                               <telerik:DropDownListItem runat="server" Value="Credito" Text="Crédito"/>
                               <telerik:DropDownListItem runat="server" Value="Correo" Text="Correo"/>
                               <telerik:DropDownListItem runat="server" Value="Rol" Text="Rol"/>
                               <telerik:DropDownListItem runat="server" Value="Telefono" Text="Teléfono" />
                           </Items>
                       </telerik:RadDropDownList>
                   </td>
                   <td>
                       <telerik:RadLabel runat="server" ID="RLblTipo" Text="Seleccione" Visible="false"></telerik:RadLabel>
                        <telerik:RadDropDownList runat="server" ID="RDTipo" SelectedText="RdTipo" Visible="false" AutoPostBack="true">
                           <Items>
                               <telerik:DropDownListItem runat="server" Value="Seleccione" Selected="True" Text="Seleccione"/>
                               <telerik:DropDownListItem runat="server" Value="Bloqueo" Text="Bloqueo"/>
                               <telerik:DropDownListItem runat="server" Value="Desbloqueo" Text="Desbloqueo"/>
                           </Items>
                       </telerik:RadDropDownList>
                   </td>
               </tr>
            <tr> 
                <td colspan="2">
             <asp:panel runat="server" id="PnlElementos" Visible="false" >
                 <table>
                     <tr class="LblDesc">
                         <td>
                             <telerik:RadLabel runat="server" ID="RLblTipoS"></telerik:RadLabel>
                         </td>
                         <td>
                             <telerik:RadTextBox runat="server" ID="RTxtValor" Width="250px"></telerik:RadTextBox>
                         </td>
                         <td>
                             <telerik:RadButton runat="server" ID="RBtnAceptar" Text="Aplicar"></telerik:RadButton>
                         </td>
                     </tr>
                 </table>
             </asp:panel>

                </td>
            </tr>
                    </table>
                </td>
            </tr>
        </table>



    </telerik:RadAjaxPanel>
    <%--</div>--%>
    <asp:HiddenField ID="HidenUrs" runat="server" />
    <telerik:RadWindowManager ID="WinMsj" runat="server"></telerik:RadWindowManager>
</asp:Content>

