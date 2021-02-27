<%@ Page Title="" Language="VB" MasterPageFile="~/M_Monitoreo/MasterPage.master" AutoEventWireup="false" CodeFile="RP_SolicitudInvCampo.aspx.vb" Inherits="M_Monitoreo_RP_SolicitudInvCampo" UICulture="es-MX"  Culture="es-MX"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta name="viewport" content="width = device-width, initial-scale = 1.0, minimum-scale = 1.0, maximum-scale = 1.0, user-scalable = no" />
    <%--<meta charset="utf-8" />--%>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-15" />
    <%--<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>--%>
    <title>Reporte Visitas</title>
    <script src="scripts/jquery.min.js"></script>
    <script src="scripts/Bootstapcdn.js"></script>
    <link href="styles/Boot.css" rel="stylesheet" />
    <link href="styles/General.css" rel="stylesheet" />
    <link href="styles/ObjHtmlNoS.css" rel="stylesheet" />
    <link href="../M_Gestion/Estilos/w3.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css?family=Bitter" rel="stylesheet"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="w3-margin-top s12">
        <div>
            <div class="w3-container w3-center w3-blue">
                <b>Reporte investigaciones de campo</b>
            </div>
            <div class="w3-container w3-center w3-white">
                <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" Runat="server" Skin="Default" >
                        </telerik:RadAjaxLoadingPanel>
                <telerik:RadAjaxPanel ID="RadAjaxPanelGral" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
                    <div class="w3-container w3-center w3-white">
                <telerik:RadRadioButtonList ID="RblReporte" Runat="server" OnSelectedIndexChanged="RblReporte_SelectedIndexChanged" Direction="Horizontal">
                    <Items>
                        <telerik:ButtonListItem Text="Solicitudes" ToolTip="Solicitudes de Investigaciones" Value="Solicitudes" />
                        <telerik:ButtonListItem Text="Domicilio" ToolTip="Investigaciones Domicilio" Value="Domicilio" />
                        <telerik:ButtonListItem Text="Ingresos" ToolTip="Investigaciones de Ingresos" Value="Ingresos" />
                    </Items>
                </telerik:RadRadioButtonList>
                  </div>

                    <div class="w3-container w3-center w3-white">
                 <telerik:RadAjaxPanel ID="RadAjaxPanelFiltros" runat="server" visible="false">
                                        <div>
                                            <telerik:RadComboBox ID="RcbEstatus" runat="server" AutoPostBack="True" CheckBoxes="true" DropDownAutoWidth="Enabled" EnableCheckAllItemsCheckBox="true" Localization-AllItemsCheckedString="Todos los elementos seleccionados" Localization-CheckAllString="Todos" Label="Estatus">
                                            </telerik:RadComboBox>
                                            <telerik:RadComboBox ID="RcbTipo" runat="server" AutoPostBack="True" CheckBoxes="true" DropDownAutoWidth="Enabled" EnableCheckAllItemsCheckBox="true" Localization-AllItemsCheckedString="Todos los elementos seleccionados" Localization-CheckAllString="Todos" Visible="False" Label="Tipo">
                                            </telerik:RadComboBox>
                                            <telerik:RadComboBox ID="RcbUsuario" runat="server" CheckBoxes="true" DropDownAutoWidth="Enabled" EnableCheckAllItemsCheckBox="true" Localization-AllItemsCheckedString="Todos los elementos seleccionados" Localization-CheckAllString="Todos" Visible="False" Label="Usuario">
                                            </telerik:RadComboBox>
                                            <telerik:RadDatePicker ID="TxtFechaI" runat="server" Visible="False">
                                            </telerik:RadDatePicker>
                                            <telerik:RadDatePicker ID="TxtFechaF" runat="server" Visible="False">
                                            </telerik:RadDatePicker>
                                            <telerik:RadButton ID="BtnGenerar" runat="server" SingleClick="true" SingleClickText="Procesando" text="Generar" Visible="False">
                                            </telerik:RadButton>
                                           
                                                                                   </div>
                      <div>
                           <telerik:RadComboBox ID="RcbExportar" runat="server" AutoPostBack="True" CheckBoxes="True" DropDownAutoWidth="Enabled" EnableCheckAllItemsCheckBox="True" Localization-AllItemsCheckedString="Todos los elementos seleccionados" Localization-CheckAllString="Todos" Visible="False" Culture="es-ES">
                               <Items>
                                   <telerik:RadComboBoxItem runat="server" Text="Solicitudes" Value="Solicitudes" />
                                   <telerik:RadComboBoxItem runat="server" Text="Domicilio" Value="Domicilio" />
                                   <telerik:RadComboBoxItem runat="server" Text="Ingresos" Value="Ingresos" />
                               </Items>
                               <Localization AllItemsCheckedString="Todos los elementos seleccionados" CheckAllString="Todos" />
                           </telerik:RadComboBox>
                           <telerik:RadButton ID="BtnExportar" runat="server" text="Exportar" Visible="False">
                          </telerik:RadButton>

                         
                          </div>
                                        <div class="w3-row w3-panel" style="overflow: auto">
                                            <telerik:RadGrid RenderMode="Lightweight" ID="GVSolInvCampo" runat="server" AllowPaging="True" AllowSorting="True" OnNeedDataSource="GVSolInvCampo_NeedDataSource" PageSize="50" Visible="false" HeaderStyle-HorizontalAlign="Center">
                                                <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                                <ClientSettings>
                                                    <Scrolling AllowScroll="True" FrozenColumnsCount="1" SaveScrollPosition="true" UseStaticHeaders="True" />
                                                </ClientSettings>
                                                <MasterTableView >
                                                    <PagerStyle PageSizeControlType="RadDropDownList" />
                                                </MasterTableView>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <PagerStyle Mode="NextPrevAndNumeric" PageSizeControlType="RadDropDownList" />
                                                <FilterMenu RenderMode="Lightweight">
                                                </FilterMenu>
                                                <HeaderContextMenu RenderMode="Lightweight">
                                                </HeaderContextMenu>
                                            </telerik:RadGrid>
                                        </div>
                                    </telerik:RadAjaxPanel>
            </div>
                </telerik:RadAjaxPanel>
            </div>



            </div>
        
        
    </div>
      <telerik:RadWindowManager ID="RamiWa" runat="server" EnableShadow="true" 
            Animation="Resize" Modal="True" RenderMode="Lightweight"
            VisibleTitlebar="False" ShowContentDuringLoad="false">
            <Localization OK="Aceptar" />
      </telerik:RadWindowManager>
</asp:Content>
