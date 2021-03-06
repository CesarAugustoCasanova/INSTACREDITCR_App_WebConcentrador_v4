﻿<%@ Page Title="" Language="VB" MasterPageFile="./MasterPage.master" AutoEventWireup="false" CodeFile="CatalogoEtapas.aspx.vb" Inherits="CatalogoEtapas" %>


<asp:Content ID="Content2" ContentPlaceHolderID="CPHMaster" runat="Server">
    <link href="Styles/Estilos/HTML.css" rel="stylesheet" />
    <link href="Styles/Estilos/Modal.css" rel="stylesheet" />
    <link href="Styles/Estilos/ObjAjax.css" rel="stylesheet" />
    <link href="Styles/Estilos/ObjHtmlNoS.css" rel="stylesheet" />
    <link href="Styles/Estilos/ObjHtmlS.css" rel="stylesheet" />
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" ></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <table class="Table">
            <tr class="Titulos">
                <td>Etapas Judiciales
                </td>
            </tr>
             </table>
                        
                                <telerik:RadGrid ID="RGVEtapas" runat="server"  RenderMode="Lightweight" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true"
                                    ClientSettings-EnableRowHoverStyle="true" OnNeedDataSource="RGVEtapas_NeedDataSource" Culture="es-MX">

                                    <MasterTableView Width="100%" CommandItemDisplay="Top" EditFormSettings-PopUpSettings-KeepInScreenBounds="true" DataKeyNames="Id">

                                        <Columns>

                                            <telerik:GridEditCommandColumn ItemStyle-Width="5px" UniqueName="Expandible">
                                            </telerik:GridEditCommandColumn>
                                            <telerik:GridBoundColumn UniqueName="Id" HeaderText="Id" DataField="Id">
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn UniqueName="Tipo" HeaderText="Tipo" DataField="Tipo">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete" HeaderText="Eliminar" >
                            </telerik:GridButtonColumn>

                                        </Columns>
                                        <EditFormSettings UserControlName="Etapas.ascx" EditFormType="WebUserControl">
                                            <EditColumn UniqueName="EditCommandColumn1">
                                            </EditColumn>
                                        </EditFormSettings>
                                    </MasterTableView>
                                </telerik:RadGrid>
        &nbsp;
        &nbsp;
        &nbsp;
        &nbsp;
        &nbsp;
        &nbsp;
                    
            
           <telerik:RadWindowManager ID="RadAviso" runat="server" >
            <Localization OK="Aceptar" />
        </telerik:RadWindowManager>
        <asp:Button runat="server" ID="BtnAceptarConfirmacion" Visible="true" Height="1px" Style="display: none;" />
    </telerik:RadAjaxPanel>
</asp:Content>
