<%@ Page Title="" Language="VB" MasterPageFile="MasterPage.master" AutoEventWireup="false" CodeFile="DashBoard.aspx.vb" Inherits="DashBoard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="CPHMaster" runat="Server">
    <telerik:RadScriptBlock runat="server">
        <script type="text/javascript">
            var timerAutoejecutar = 0
            autoejecutar = () => {
                timerAutoejecutar = setInterval(() => {
                    var value = document.getElementById("<%= CbxEjecutar.ClientID%>").checked
                    if (value == true) document.getElementById("<%= BtnGenerar.ClientID%>").click();
                }, 20000)
            }

            (function (global, undefined) {
                global.onSeriesClick = function (args) {
                    $("#pnlInfoChart").removeClass("w3-hide")
                    $find('<%=lblGroupBy.ClientID%>').set_value($find('<%=ddlCampoAgrupar.ClientID%>').get_selectedItem().get_text())
                    $find('<%=lblGroupValue.ClientID%>').set_value(args.dataItem.descripcion)
                    gotoEndPage
                }
            })(window);

            function exportRadHtmlChart() {
                let $ = $telerik.$;
                $find('<%=RadClientExportManager1.ClientID%>').exportPDF($(".RadHtmlChart")[1]);
            }

            gotoEndPage = () => setTimeout(() => {
                document.getElementById('endPage').scrollIntoView(true);
            }, 1000)

        </script>
        <script type="text/javascript">
            function clickOnce(btn, msg) {
                btn.value = msg;
                btn.disabled = true;
                return true;
            }
        </script>
    </telerik:RadScriptBlock>
    <telerik:RadFormDecorator ID="FormDecorator1" runat="server" DecoratedControls="all" DecorationZoneID="optionsDecoration"></telerik:RadFormDecorator>
    <telerik:RadAjaxPanel runat="server" CssClass="container" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadClientExportManager runat="server" ID="RadClientExportManager1"></telerik:RadClientExportManager>
        <div class="container" id="optionsDecoration">
            <h3>Generaci&oacute;n de reporte "<i><asp:Label ID="LblReporte" runat="server" /></i>"</h3>

            <fieldset class="my-1">
                <legend>Configuraci&oacute;n de condiciones</legend>
                <telerik:RadAjaxPanel runat="server" LoadingPanelID="pnlLoading">
                    <p>Configura tus reglas o sube un archivo con los créditos necesarios necesarios para el reporte</p>
                    <div class="mb-3">
                        <telerik:RadButton ID="btnCondiciones" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="FilterType" Checked="true">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="Configurar condiciones" CssClass="bg-primary text-white border-0"></telerik:RadButtonToggleState>
                                <telerik:RadButtonToggleState Text="Configurar condiciones" CssClass="bg-light border"></telerik:RadButtonToggleState>
                            </ToggleStates>
                        </telerik:RadButton>
                        <label>ó </label>
                        <telerik:RadButton ID="btnArchivo" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="FilterType">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="Subir archivo" CssClass="bg-primary text-white border-0"></telerik:RadButtonToggleState>
                                <telerik:RadButtonToggleState Text="Subir archivo" CssClass="bg-light border"></telerik:RadButtonToggleState>
                            </ToggleStates>
                        </telerik:RadButton>
                    </div>
                    <asp:Panel runat="server" ID="pnlCondiciones">
                        <telerik:RadGrid runat="server" ID="gridCondiciones" PageSize="10" AllowPaging="true" Width="100%">
                            <MasterTableView AutoGenerateColumns="false" CommandItemDisplay="Top">
                                <CommandItemSettings AddNewRecordText="Agregar condición" ShowRefreshButton="false" />
                                <Columns>
                                    <telerik:GridEditCommandColumn HeaderStyle-Width="40px" ButtonType="FontIconButton"></telerik:GridEditCommandColumn>
                                    <telerik:GridButtonColumn HeaderStyle-Width="40px" ButtonType="FontIconButton" CommandName="Delete"></telerik:GridButtonColumn>
                                    <telerik:GridBoundColumn UniqueName="CONSECUTIVO" HeaderText="ID" DataField="CONSECUTIVO"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="Campo" HeaderText="Campo" DataField="DESCRIPCIONCAMPO">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="Condicion" HeaderText="Condicion" DataField="DESCRIPCIONOPERADOR">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="Valor" HeaderText="Valor" DataField="Valor">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="Conector" HeaderText="Conector" DataField="DESCRIPCIONCONECTOR">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Inicio Grupo">
                                        <ItemTemplate>
                                            <telerik:RadCheckBox runat="server" Checked='<%# IIf(Eval("AGRUPADOR") = 1, True, False) %>'></telerik:RadCheckBox>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Fin Grupo">
                                        <ItemTemplate>
                                            <telerik:RadCheckBox runat="server" Checked='<%# IIf(Eval("AGRUPADOR") = 2, True, False) %>'></telerik:RadCheckBox>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <EditFormSettings UserControlName="./Reglas.ascx" EditFormType="WebUserControl">
                                    <EditColumn UniqueName="EditCommandColumn1"></EditColumn>
                                </EditFormSettings>
                            </MasterTableView>
                        </telerik:RadGrid>
                        <div>
                            <telerik:RadLabel runat="server" Text="Preview Condicion"></telerik:RadLabel>
                            <telerik:RadLabel runat="server" ID="LblCondicion"></telerik:RadLabel>
                            <br />
                            <telerik:RadLabel runat="server" ID="LblCondicionChida" Visible="false"></telerik:RadLabel>
                        </div>
                        <div class="d-flex justify-content-center mt-2">
                            <telerik:RadButton ID="BtnGuardar" runat="server" CssClass="text-white bg-success border-0 mx-1" Text="Guardar condiciones" />
                            <telerik:RadButton ID="BtnLimpiar" runat="server" CssClass="text-white bg-danger border-0 mx-1" Text="Limpiar condiciones" />
                        </div>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlArchivo" Visible="false">
                        <div class="m-2 border rounded" style="height: 200px; text-align: center; background-color: lightgray" id="divDropZone">
                            Selecciona tu archivo
                            <telerik:RadAsyncUpload runat="server" ID="auCreditos" AllowedFileExtensions="txt,csv" Culture="es-ES" MaxFileInputsCount="1" DropZones="#divDropZone" PostbackTriggers="btnProcesarArchivo"></telerik:RadAsyncUpload>
                            <p>
                                ó arrastra tu archivo
                            </p>
                            <br />
                            <p class="text-muted ">
                                El archivo debe ser txt o csv y solo debe contener <u><em>1 credito por renglón</em></u>
                            </p>
                        </div>
                        <div class="text-center">
                            <telerik:radButton runat="server" ID="btnProcesarArchivo" Text="Procesar Archivo" CssClass="bg-success text-white border-0" SingleClick="true" SingleClickText="Procesando" />
                            <asp:HiddenField runat="server" ID="hfCreditos" Value="Error: Da clic en 'Procesar Archivo' " />
                        </div>
                    </asp:Panel>
                    <telerik:RadNotification ID="RadNotification2" runat="server" EnableRoundedCorners="true" Position="Center" EnableShadow="true" Width="300" Height="100" AutoCloseDelay="5100"></telerik:RadNotification>
                </telerik:RadAjaxPanel>


            </fieldset>

            <fieldset class="my-1">
                <legend>Configuraci&oacute;n de campos condicionales</legend>
                <div class="row">
                    <div class="col-md-6 my-1">
                        <label>Campo para agrupar:</label>
                        <telerik:RadDropDownList runat="server" ID="ddlCampoAgrupar" DefaultMessage="Seleccione" Width="100%"></telerik:RadDropDownList>
                    </div>
                    <div class="col-md-6 my-1">
                        <label>
                            Seleccione una campo para 
                            <telerik:RadButton runat="server" ID="btnSumarContar" ButtonType="ToggleButton" ToggleType="CustomToggle" AutoPostBack="false" Checked="true">
                                <ToggleStates>
                                    <telerik:RadButtonToggleState Value="Sum" Text="Sumar" Selected="true"></telerik:RadButtonToggleState>
                                    <telerik:RadButtonToggleState Value="Count" Text="Contar"></telerik:RadButtonToggleState>
                                </ToggleStates>
                            </telerik:RadButton>
                        </label>
                        <telerik:RadDropDownList runat="server" ID="ddlSumarContar" DefaultMessage="Seleccione" Width="100%"></telerik:RadDropDownList>
                    </div>
                </div>
            </fieldset>
            <%-- Seleccion de Grafica --%>
            <fieldset class="my-1">
                <telerik:RadAjaxPanel runat="server" LoadingPanelID="pnlLoading">
                    <legend>Selecciona una gr&aacute;fica</legend>
                    <div class="row">
                        <div class="col-2">
                            <telerik:RadRadioButtonList runat="server" ID="RBLGrafica" Columns="1">
                                <Items>
                                    <telerik:ButtonListItem Text="Área" Value="1" Selected="true" />
                                    <telerik:ButtonListItem Text="Linear" Value="2" />
                                    <telerik:ButtonListItem Text="Columnas" Value="3" />
                                    <telerik:ButtonListItem Text="Barras" Value="4" />
                                    <telerik:ButtonListItem Text="Dona" Value="5" />
                                    <telerik:ButtonListItem Text="Pastel" Value="6" />
                                </Items>
                            </telerik:RadRadioButtonList>
                        </div>
                        <div class="col-10">
                            <telerik:RadHtmlChart runat="server" ID="ChartExample" Width="100%" Height="200px">
                                <ClientEvents OnSeriesClick="onSeriesClick" />
                            </telerik:RadHtmlChart>
                        </div>
                    </div>
                </telerik:RadAjaxPanel>
            </fieldset>

            <fieldset class="my-1">
                <div class="d-flex justify-content-center">
                    <div class="custom-control custom-switch w3-hide">
                        <input runat="server" type="checkbox" class="custom-control-input" id="switchAutoExe" />
                        <label class="custom-control-label" for="CPHMaster_switchAutoExe">¿Reporte autom&aacute;tico?</label>
                    </div>
                    <asp:CheckBox ID="CbxEjecutar" runat="server" CssClass="CbxDesc" Text="AutoEjecutar" AutoPostBack="true" Visible="false" />
                    <telerik:radButton ID="BtnGenerar" runat="server" CssClass="bg-primary text-white rounded border-0 mx-1" Text="Generar" SingleClick="true" SingleClickText="Generando" ></telerik:radButton>
                </div>
            </fieldset>
            <telerik:RadAjaxPanel runat="server" ID="pnlGrafica" Visible="false">
                <fieldset>
                    <legend>Resultado</legend>
                    <telerik:RadButton ID="BtnDetalles" runat="server" Text="Mostrar detalles" CssClass="bg-info text-white border-0 mx-1" SingleClick="true" SingleClickText="Generando" />
                    <telerik:RadButton ID="BtnToCsv" runat="server" Text="Descargar CSV" CssClass="bg-danger text-white border-0 mx-1" />
                    <telerik:RadButton ID="BtnToXlsx" runat="server" Text="Descargar XLSX" CssClass="bg-danger text-white border-0 mx-1" />
                    <telerik:RadButton runat="server" ID="btnImprimir" CssClass="bg-danger text-white border-0 mx-1" Text="Descargar PDF" OnClientClicked="exportRadHtmlChart" AutoPostBack="false"  SingleClick="true" SingleClickText="Generando"/>
                    <asp:Button ID="BtnDescargar" runat="server" Height="0px" OnClick="ButtonDescargar_Click" Style="visibility: hidden" Text="" Width="0px" />

                    <div class="row mb-2">
                        <telerik:RadHtmlChart runat="server" ID="chartPrint" Width="100%" Height="400px">
                            <Zoom Enabled="true">
                                <MouseWheel Enabled="true" Lock="Y" />
                                <Selection Enabled="true" Lock="Y" ModifierKey="Shift" />
                            </Zoom>
                            <Pan Enabled="true" Lock="Y" ModifierKey="None" />
                            <ClientEvents OnSeriesClick="onSeriesClick" />
                        </telerik:RadHtmlChart>
                    </div>
                    <div id="pnlInfoChart" class="w3-hide">
                        <div class="d-flex justify-content-center mb-2">
                            <div>
                                Ver mas informacion de 
                                <b>
                                    <telerik:RadTextBox runat="server" Enabled="false" ID="lblGroupBy"></telerik:RadTextBox>
                                    =
                                    <telerik:RadTextBox runat="server" Enabled="false" ID="lblGroupValue"></telerik:RadTextBox>
                                </b>
                            </div>
                            <telerik:RadButton runat="server" ID="btnInfoChart" Text="Ir" CssClass=" ml-2 bg-success text-white border-0"></telerik:RadButton>
                        </div>
                    </div>

                    <telerik:RadGrid runat="server" ID="gridDetalles" PageSize="10" AllowPaging="true" AutoGenerateColumns="true" Width="100%" Visible="false">
                        <MasterTableView CommandItemDisplay="Top" AllowSorting="true">
                            <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToCsvButton="true" ShowExportToExcelButton="true" ShowRefreshButton="false" />
                            <Columns>
                                <telerik:GridBoundColumn DataField="INDICE" Visible="false"></telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <ExportSettings IgnorePaging="true" FileName="Reporte Automatico MC Collect" OpenInNewWindow="true" ExportOnlyData="true"></ExportSettings>
                        <ClientSettings>
                            <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                            <Virtualization EnableVirtualization="true" LoadingPanelID="pnlLoading" ItemsPerView="50" InitiallyCachedItemsCount="50" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </fieldset>
            </telerik:RadAjaxPanel>
        </div>
        <telerik:RadNotification ID="RadNotification1" runat="server" EnableRoundedCorners="true" Position="Center" EnableShadow="true" Width="300" Height="100" AutoCloseDelay="5100">
        </telerik:RadNotification>
        <asp:Label ID="LblTipo" runat="server" Visible="false"></asp:Label>
        <telerik:RadAjaxLoadingPanel runat="server" ID="pnlLoading"></telerik:RadAjaxLoadingPanel>
    </telerik:RadAjaxPanel>
    <div id="endPage"></div>

    <asp:HiddenField ID="HidenUrs" runat="server" />
</asp:Content>
