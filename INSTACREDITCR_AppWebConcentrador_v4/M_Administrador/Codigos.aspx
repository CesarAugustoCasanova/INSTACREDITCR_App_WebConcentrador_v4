<%@ Page Language="VB" MasterPageFile="~/M_Administrador/MasterPage.master" AutoEventWireup="false"
    CodeFile="Codigos.aspx.vb" Inherits="Codigos" Title="MC :: Administración de Códigos" StylesheetTheme="" Theme="" %>


<asp:Content ID="Content2" ContentPlaceHolderID="CPHMaster" runat="Server">
    <script languaje="javascript" type="text/javascript">
        function confirmCallbackFn(arg) {
            if (arg) {
                __doPostBack('ctl00$CPHMaster$BtnAceptarConfirmacion', '')
            }
        }
        function openWinContentTemplate() {
            var ventana = document.getElementsByName('RadWindowWrapper_ctl00_CPHMaster_RadWindow_ContentTemplate')
            ventana.show();
        }
    </script>
    <script>
        focusCrearAsociacion = () => {
            $([document.documentElement, document.body]).animate({
                scrollTop: $("#CrearAsociacion").offset().top
            }, 1000);
        }
    </script>

    <telerik:RadFormDecorator runat="server" DecoratedControls="all" DecorationZoneID="optionsPanel"></telerik:RadFormDecorator>

    <telerik:RadAjaxLoadingPanel ID="loadingPanel" runat="server"></telerik:RadAjaxLoadingPanel>
    <div class="text-center mb-3">
        <h2>Configuración de Códigos</h2>
        <p>Configure códigos de acción, resultado, no pago y cree asociaciones entre ellos.</p>
    </div>
    <div class="container">
        <telerik:RadTabStrip runat="server" ID="RadTabStrip1" MultiPageID="RadMultiPage1" SelectedIndex="0" Width="100%">
            <Tabs>
                <telerik:RadTab Text="Códigos Acción"></telerik:RadTab>
                <telerik:RadTab Text="Códigos Resultado"></telerik:RadTab>
                <telerik:RadTab Text="Códigos No Pago"></telerik:RadTab>
                <telerik:RadTab Text="Asociaciones"></telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
    </div>
    <br />
    <div class="container-fluid mb-3">
        <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0">
            <telerik:RadPageView runat="server">
                <telerik:RadAjaxPanel runat="server" LoadingPanelID="loadingPanel">
                    <telerik:RadNotification runat="server" ID="AccionNotify"></telerik:RadNotification>
                    <telerik:RadGrid runat="server" ID="gridCodAccion" AutoGenerateColumns="false" AllowPaging="true" PageSize="5" AllowSorting="true" AllowFilteringByColumn="true" Width="100%" FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" OnFilterCheckListItemsRequested="grid_FilterCheckListItemsRequested">
                        <ExportSettings FileName="Códigos Acción" HideNonDataBoundColumns="true" HideStructureColumns="true">
                            <Excel Format="Xlsx" />
                        </ExportSettings>
                        <MasterTableView CommandItemDisplay="Top" EditMode="PopUp" AllowFilteringByColumn="true" >
                            <CommandItemSettings AddNewRecordText="Agregar" RefreshText="Recargar" ShowExportToExcelButton="true" />
                            <NoRecordsTemplate>Sin códigos de acción.</NoRecordsTemplate>
                            <Columns>
                                <telerik:GridTemplateColumn Exportable="false" AllowSorting="false" HeaderStyle-Width="120px" AllowFiltering="false" EnableHeaderContextMenu="false">
                                    <ItemTemplate>
                                        <telerik:RadButton runat="server" ToolTip="Editar" CommandName="Edit" CssClass="border-0 bg-transparent">
                                            <ContentTemplate>
                                                <span class="material-icons">edit
                                                </span>
                                            </ContentTemplate>
                                        </telerik:RadButton>
                                        <telerik:RadButton runat="server" ToolTip="Editar" CommandName="Delete" CssClass="border-0 bg-transparent">
                                            <ConfirmSettings ConfirmText="Al eliminar este codigo se eliminarán todas sus asociaciones creadas. <br/> ¿Continuar?" UseRadConfirm="true" />
                                            <ContentTemplate>
                                                <span class="material-icons">delete
                                                </span>
                                            </ContentTemplate>
                                        </telerik:RadButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="id" Display="false" ></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AllowFiltering="true" AllowSorting="true" HeaderText="Código" DataField="Codigo" AutoPostBackOnFilter="true" FilterCheckListEnableLoadOnDemand="true"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AllowFiltering="true" AllowSorting="true" HeaderText="Descripcion" DataField="Descripcion" AutoPostBackOnFilter="true"></telerik:GridBoundColumn>
                                <%--<telerik:GridBoundColumn AllowFiltering="true" AllowSorting="true" HeaderText="Alias" DataField="AliasCodigo" AutoPostBackOnFilter="true"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AllowFiltering="true" AllowSorting="true" HeaderText="Descripcion Alias" DataField="AliasDescripcion" AutoPostBackOnFilter="true"></telerik:GridBoundColumn>--%>
                            </Columns>
                            <EditFormSettings EditFormType="WebUserControl" UserControlName="./grids/Codigos/Accion.ascx">
                                <PopUpSettings CloseButtonToolTip="Cerrar" KeepInScreenBounds="true" Modal="true" Height="100%" ScrollBars="Auto" Width="90%" OverflowPosition="Center" />
                            </EditFormSettings>
                        </MasterTableView>
                    </telerik:RadGrid>
                </telerik:RadAjaxPanel>
            </telerik:RadPageView>
            <telerik:RadPageView runat="server">
                <telerik:RadAjaxPanel runat="server" LoadingPanelID="loadingPanel">
                    <telerik:RadNotification runat="server" ID="ResultadosNotify"></telerik:RadNotification>
                    <telerik:RadGrid runat="server" ID="gridResultados" AutoGenerateColumns="false" AllowPaging="true" PageSize="5" AllowSorting="true" AllowFilteringByColumn="true" Width="100%" FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" OnFilterCheckListItemsRequested="grid_FilterCheckListItemsRequested">
                        <ExportSettings FileName="Códigos Resultado" HideNonDataBoundColumns="true" HideStructureColumns="true">
                            <Excel Format="Xlsx" />
                        </ExportSettings>
                        <MasterTableView CommandItemDisplay="Top" EditMode="PopUp">
                            <CommandItemSettings AddNewRecordText="Agregar" RefreshText="Recargar" ShowExportToExcelButton="true" />
                            <NoRecordsTemplate>Sin códigos de resultado.</NoRecordsTemplate>
                            <Columns>
                                <telerik:GridTemplateColumn Exportable="false" AllowSorting="false" HeaderStyle-Width="120px" AllowFiltering="false" EnableHeaderContextMenu="false">
                                    <ItemTemplate>
                                        <telerik:RadButton runat="server" ToolTip="Editar" CommandName="Edit" CssClass="border-0 bg-transparent">
                                            <ContentTemplate>
                                                <span class="material-icons">edit
                                                </span>
                                            </ContentTemplate>
                                        </telerik:RadButton>
                                        <telerik:RadButton runat="server" ToolTip="Editar" CommandName="Delete" CssClass="border-0 bg-transparent">
                                            <ConfirmSettings ConfirmText="Al eliminar este codigo se eliminarán todas sus asociaciones creadas. <br/> ¿Continuar?" UseRadConfirm="true" />
                                            <ContentTemplate>
                                                <span class="material-icons">delete
                                                </span>
                                            </ContentTemplate>
                                        </telerik:RadButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="id" Display="false" ></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AllowFiltering="true" AllowSorting="true" HeaderText="Código" DataField="Codigo" AutoPostBackOnFilter="true" FilterCheckListEnableLoadOnDemand="true"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AllowFiltering="true" AllowSorting="true" HeaderText="Descripcion" DataField="Descripcion" AutoPostBackOnFilter="true"></telerik:GridBoundColumn>
                               <%-- <telerik:GridBoundColumn AllowFiltering="true" AllowSorting="true" HeaderText="Alias" DataField="AliasCodigo" AutoPostBackOnFilter="true"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AllowFiltering="true" AllowSorting="true" HeaderText="Descripcion Alias" DataField="AliasDescripcion" AutoPostBackOnFilter="true"></telerik:GridBoundColumn>--%>
                            </Columns>
                            <EditFormSettings EditFormType="WebUserControl" UserControlName="./grids/Codigos/Resultado.ascx">
                                <PopUpSettings CloseButtonToolTip="Cerrar" KeepInScreenBounds="true" Modal="true" Height="100%" ScrollBars="Auto" Width="90%" OverflowPosition="Center" />
                            </EditFormSettings>
                        </MasterTableView>
                    </telerik:RadGrid>
                </telerik:RadAjaxPanel>
            </telerik:RadPageView>
            <telerik:RadPageView runat="server">
                <telerik:RadAjaxPanel runat="server" LoadingPanelID="loadingPanel">
                    <telerik:RadNotification runat="server" ID="NoPagoNotify"></telerik:RadNotification>
                    <telerik:RadGrid runat="server" ID="gridNoPago" AutoGenerateColumns="false" AllowPaging="true" PageSize="5" AllowSorting="true" AllowFilteringByColumn="true" Width="100%" FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" OnFilterCheckListItemsRequested="grid_FilterCheckListItemsRequested">
                        <ExportSettings FileName="Códigos No Pago" HideNonDataBoundColumns="true" HideStructureColumns="true">
                            <Excel Format="Xlsx" />
                        </ExportSettings>
                        <MasterTableView CommandItemDisplay="Top" EditMode="PopUp">
                            <CommandItemSettings AddNewRecordText="Agregar" RefreshText="Recargar" ShowExportToExcelButton="true" />
                            <NoRecordsTemplate>Sin códigos de no pago.</NoRecordsTemplate>
                            <Columns>
                                <telerik:GridTemplateColumn Exportable="false" AllowSorting="false" HeaderStyle-Width="120px" AllowFiltering="false" EnableHeaderContextMenu="false">
                                    <ItemTemplate>
                                        <telerik:RadButton runat="server" ToolTip="Editar" CommandName="Edit" CssClass="border-0 bg-transparent">
                                            <ContentTemplate>
                                                <span class="material-icons">edit
                                                </span>
                                            </ContentTemplate>
                                        </telerik:RadButton>
                                        <telerik:RadButton runat="server" ToolTip="Editar" CommandName="Delete" CssClass="border-0 bg-transparent">
                                            <ConfirmSettings ConfirmText="Al eliminar este codigo se eliminarán todas sus asociaciones creadas. <br/> ¿Continuar?" UseRadConfirm="true" />
                                            <ContentTemplate>
                                                <span class="material-icons">delete
                                                </span>
                                            </ContentTemplate>
                                        </telerik:RadButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="id" Display="false" ></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AllowFiltering="true" AllowSorting="true" HeaderText="Código" DataField="Codigo" AutoPostBackOnFilter="true" FilterCheckListEnableLoadOnDemand="true"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AllowFiltering="true" AllowSorting="true" HeaderText="Descripcion" DataField="Descripcion" AutoPostBackOnFilter="true"></telerik:GridBoundColumn>
                                <%--<telerik:GridBoundColumn AllowFiltering="true" AllowSorting="true" HeaderText="Alias" DataField="AliasCodigo" AutoPostBackOnFilter="true"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AllowFiltering="true" AllowSorting="true" HeaderText="Descripcion Alias" DataField="AliasDescripcion" AutoPostBackOnFilter="true"></telerik:GridBoundColumn>--%>
                            </Columns>
                            <EditFormSettings EditFormType="WebUserControl" UserControlName="./grids/Codigos/NoPago.ascx">
                                <PopUpSettings CloseButtonToolTip="Cerrar" KeepInScreenBounds="true" Modal="true" Height="100%" ScrollBars="Auto" Width="90%" OverflowPosition="Center" />
                            </EditFormSettings>
                        </MasterTableView>
                    </telerik:RadGrid>
                </telerik:RadAjaxPanel>
            </telerik:RadPageView>
            <telerik:RadPageView runat="server">
                <telerik:RadAjaxPanel runat="server" LoadingPanelID="loadingPanel">
                    <telerik:RadNotification runat="server" ID="AsociacionNotify"></telerik:RadNotification>
                    <telerik:RadGrid runat="server" ID="gridAsociacion" AutoGenerateColumns="false" AllowPaging="true" PageSize="5" AllowSorting="true" AllowFilteringByColumn="true" Width="100%" FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" OnFilterCheckListItemsRequested="grid_FilterCheckListItemsRequested">
                        <ExportSettings FileName="Asociaciones" HideNonDataBoundColumns="true" HideStructureColumns="true">
                            <Excel Format="Xlsx" />
                        </ExportSettings>
                        <MasterTableView CommandItemDisplay="Top" EditMode="PopUp">
                            <CommandItemSettings AddNewRecordText="Agregar" RefreshText="Recargar" ShowExportToExcelButton="true" />
                            <NoRecordsTemplate>Sin asociaciones.</NoRecordsTemplate>
                            <Columns>
                                <telerik:GridTemplateColumn Exportable="false" AllowSorting="false" HeaderStyle-Width="120px" AllowFiltering="false" EnableHeaderContextMenu="false">
                                    <ItemTemplate>
                                        <telerik:RadButton runat="server" ToolTip="Editar" CommandName="Edit" CssClass="border-0 bg-transparent">
                                            <ContentTemplate>
                                                <span class="material-icons">edit
                                                </span>
                                            </ContentTemplate>
                                        </telerik:RadButton>
                                        <telerik:RadButton runat="server" ToolTip="Editar" CommandName="Delete" CssClass="border-0 bg-transparent">
                                            <ConfirmSettings ConfirmText="Esta acción no se puede deshacer. <br/> ¿Continuar?" UseRadConfirm="true" />
                                            <ContentTemplate>
                                                <span class="material-icons">delete
                                                </span>
                                            </ContentTemplate>
                                        </telerik:RadButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn AllowFiltering="true" AllowSorting="true" HeaderText="Id" DataField="Id" AutoPostBackOnFilter="true"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AllowFiltering="true" AllowSorting="true" HeaderText="Acción" DataField="Accion" AutoPostBackOnFilter="true"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AllowFiltering="true" AllowSorting="true" HeaderText="Resultado" DataField="Resultado" AutoPostBackOnFilter="true"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AllowFiltering="true" AllowSorting="true" HeaderText="Significativo" DataField="Significativo" AutoPostBackOnFilter="true"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AllowFiltering="true" AllowSorting="true" HeaderText="Promesa" DataField="Promesa" AutoPostBackOnFilter="true"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AllowFiltering="true" AllowSorting="true" HeaderText="Semaforo Verde" DataField="SemVerde" AutoPostBackOnFilter="true"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AllowFiltering="true" AllowSorting="true" HeaderText="Semaforo Amarillo" DataField="SemAmarillo" AutoPostBackOnFilter="true"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AllowFiltering="true" AllowSorting="true" HeaderText="Tipo" DataField="Tipo" AutoPostBackOnFilter="true"></telerik:GridBoundColumn>
                            </Columns>
                            <EditFormSettings EditFormType="WebUserControl" UserControlName="./grids/Codigos/Asociaciones.ascx">
                                <PopUpSettings CloseButtonToolTip="Cerrar" KeepInScreenBounds="true" Modal="true" Height="100%" ScrollBars="Auto" Width="90%" OverflowPosition="Center" />
                            </EditFormSettings>
                        </MasterTableView>
                    </telerik:RadGrid>
                </telerik:RadAjaxPanel>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </div>
    <telerik:RadWindowManager runat="server"></telerik:RadWindowManager>
   
        <asp:HiddenField ID="HidenUrs" runat="server" />
        <asp:HiddenField ID="HidenUrs2" runat="server" />
</asp:Content>