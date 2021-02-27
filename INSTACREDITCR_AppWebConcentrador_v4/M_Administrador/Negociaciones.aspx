<%@ Page Title="" Language="VB" MasterPageFile="./MasterPage.master" AutoEventWireup="false" CodeFile="Negociaciones.aspx.vb" Inherits="MAdministrador_Negociaciones" %>



<asp:Content ID="Content1" ContentPlaceHolderID="CPHMaster" runat="Server">
    <%-- <script type="text/javascript">
            function RadioCheck(rb) {
                var gv = document.getElementById("<%=GvNEGOCIACIONES.ClientID%>");
                    var rbs = gv.getElementsByTagName("input");
                    var row = rb.parentNode.parentNode;
                    for (var i = 0; i < rbs.length; i++) {
                        if (rbs[i].type == "radio") {
                            if (rbs[i].checked && rbs[i] != rb) {
                                rbs[i].checked = false;
                                break;
                            }
                        }
                    }
                }
        </script>--%>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="UpnGral" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">

        <table class="Table">
          
            <tr class="Titulos">
                <td>Negociaciones
                </td>
            </tr>
              <tr>
                <td>
                    <telerik:RadDropDownList runat="server" ID="DdlNivel" AutoPostBack="true">
                        <Items>
                            <telerik:DropDownListItem Text="Seleccione" Value="0" />
                            <telerik:DropDownListItem Text="1" Value="1" />
                            <telerik:DropDownListItem Text="2" Value="2" />
                            <telerik:DropDownListItem Text="3" Value="3" />
                        </Items>
                    </telerik:RadDropDownList>
                </td>
                  <td>
                    <telerik:RadButton runat="server" ID="rBtnEjecutarAsig" Enabled="false" Text="Ejecutar Quitas" CssClass="alert-primary"></telerik:RadButton>
                </td>
                <td>
                    <telerik:RadButton runat="server" ID="BtnDescargar" Enabled="false" Text="Descargar Reglas" CssClass="alert-primary"></telerik:RadButton>
                </td>
            </tr>
           
        </table>
        <br />


        <telerik:RadGrid ID="RGVNegociaciones" runat="server" RenderMode="Lightweight" AllowPaging="True" AllowSorting="True" Visible="false" AutoGenerateColumns="False" ShowStatusBar="true"
            ClientSettings-EnableRowHoverStyle="true" OnNeedDataSource="RGVNegociaciones_NeedDataSource" Culture="es-MX">

            <MasterTableView Width="100%" CommandItemDisplay="Top" EditFormSettings-PopUpSettings-KeepInScreenBounds="true" DataKeyNames="Negociacion">

                <Columns>

                    <telerik:GridEditCommandColumn ItemStyle-Width="5px" UniqueName="Expandible">
                    </telerik:GridEditCommandColumn>
                    <telerik:GridBoundColumn UniqueName="Negociacion" HeaderText="Negociacion" DataField="Negociacion">
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn UniqueName="Estatus" HeaderText="Estatus" DataField="Estatus">
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn UniqueName="ID" HeaderText="ID" DataField="ID">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="NIVEL" HeaderText="NIVEL" DataField="Nivel">
                    </telerik:GridBoundColumn>
                    <telerik:GridButtonColumn UniqueName="Simular" HeaderText="Simular" Text="Simular" CommandName="Simular" ButtonType="PushButton"></telerik:GridButtonColumn>
                    <telerik:GridButtonColumn UniqueName="Descargar" HeaderText="Descargar" Text="Descargar" CommandName="Descargar" HeaderTooltip="Descargar Simulacion" ButtonType="PushButton"></telerik:GridButtonColumn>
                    <telerik:GridButtonColumn UniqueName="Habilitar" HeaderText="Habilitar" Text="Habilitar" CommandName="Habilitar" ButtonType="ImageButton" ImageUrl="~/M_Administrador/Imagenes/ImgOk.png"></telerik:GridButtonColumn>
                    <telerik:GridButtonColumn UniqueName="Deshabilitar" HeaderText="Deshabilitar" Text="Deshabilitar" CommandName="Deshabilitar" ButtonType="ImageButton" ImageUrl="~/M_Administrador/Imagenes/ImgBorrar.png"></telerik:GridButtonColumn>
                    <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete" HeaderText="Eliminar">
                    </telerik:GridButtonColumn>
                </Columns>
                <EditFormSettings UserControlName="Negociaciones.ascx" EditFormType="WebUserControl">
                    <EditColumn UniqueName="EditCommandColumn1">
                    </EditColumn>
                </EditFormSettings>
            </MasterTableView>
        </telerik:RadGrid>

        <telerik:RadWindow runat="server" ID="WinSim" Behaviors="Close" Width="850px" Height="550px" EnableViewState="false" VisibleStatusbar="false">
            <ContentTemplate>
                <telerik:RadLabel runat="server" ID="lblCuantos"></telerik:RadLabel>
                <telerik:RadGrid runat="server" ID="GvSimulacion" AutoGenerateColumns="true" Height="500px" Width="100%">
                    <%--  <MasterTableView CommandItemDisplay="Top" AllowSorting="true">
                            <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToCsvButton="true" ShowExportToExcelButton="true" ShowRefreshButton="false" />
                            <Columns>
                                <telerik:GridBoundColumn DataField="CREDITO" ></telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    <ClientSettings>
                            <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                            <Virtualization EnableVirtualization="true" LoadingPanelID="RadAjaxLoadingPanel1" ItemsPerView="50" InitiallyCachedItemsCount="50" />
                        </ClientSettings>--%>
                </telerik:RadGrid>
            </ContentTemplate>
        </telerik:RadWindow>

        <asp:HiddenField ID="HidenUrs" runat="server" />

        <asp:Button runat="server" ID="BtnAceptarConfirmacion" Visible="true" Height="1px" Style="display: none;" />


        <telerik:RadWindowManager ID="RadAviso" runat="server">
            <Localization OK="Aceptar" />
        </telerik:RadWindowManager>
    </telerik:RadAjaxPanel>
</asp:Content>

