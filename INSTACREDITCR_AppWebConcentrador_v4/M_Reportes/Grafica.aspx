<%@ Page Title="" Language="VB" MasterPageFile="MasterPage.master" AutoEventWireup="false" CodeFile="Grafica.aspx.vb" Inherits="Grafica" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHMaster" runat="Server">
    <telerik:RadFormDecorator ID="FormDecorator1" runat="server" DecoratedControls="all" DecorationZoneID="optionsDecoration"></telerik:RadFormDecorator>
    <telerik:RadAjaxPanel runat="server" CssClass="container" LoadingPanelID="RadAjaxLoadingPanel1">
        <div class="container" id="optionsDecoration">

            <h3>Configuraci&oacute;n de reporte</h3>

            <fieldset class="my-1">

                <legend>Selecciona una gr&aacute;fica</legend>

                <div class="row">

                    <div class="col-md-4 my-1">
                        <h6>
                            <telerik:RadButton runat="server" ButtonType="ToggleButton" ID="btnAreaChart" Text="Gráfica de área" ToggleType="Radio" GroupName="ChooseChart" AutoPostBack="false" Width="100%"></telerik:RadButton>
                        </h6>
                        <telerik:RadHtmlChart runat="server" ID="RadHtmlChart1" Width="100%" Height="150px">
                            <PlotArea>
                                <Series>
                                    <telerik:AreaSeries Name="Ejemplo">
                                        <Appearance>
                                            <FillStyle BackgroundColor="Blue"></FillStyle>
                                        </Appearance>
                                        <LabelsAppearance Visible="false"></LabelsAppearance>
                                        <LineAppearance Width="1" LineStyle="Smooth"></LineAppearance>
                                        <TooltipsAppearance Color="White"></TooltipsAppearance>
                                        <SeriesItems>
                                            <telerik:CategorySeriesItem Y="400"></telerik:CategorySeriesItem>
                                            <telerik:CategorySeriesItem Y="720"></telerik:CategorySeriesItem>
                                            <telerik:CategorySeriesItem Y="1300"></telerik:CategorySeriesItem>
                                            <telerik:CategorySeriesItem Y="450"></telerik:CategorySeriesItem>
                                            <telerik:CategorySeriesItem />
                                            <telerik:CategorySeriesItem Y="600"></telerik:CategorySeriesItem>
                                            <telerik:CategorySeriesItem Y="900"></telerik:CategorySeriesItem>
                                        </SeriesItems>
                                    </telerik:AreaSeries>
                                </Series>
                                <Appearance>
                                    <FillStyle BackgroundColor="Transparent"></FillStyle>
                                </Appearance>
                                <YAxis Visible="false"></YAxis>
                            </PlotArea>
                            <Appearance>
                                <FillStyle BackgroundColor="Transparent"></FillStyle>
                            </Appearance>
                        </telerik:RadHtmlChart>
                    </div>

                    <div class="col-md-4 my-1">
                        <h6>
                            <telerik:RadButton runat="server" ButtonType="ToggleButton" ID="btnLineChart" Text="Gráfica linear" ToggleType="Radio" GroupName="ChooseChart" AutoPostBack="false" Width="100%"></telerik:RadButton>
                        </h6>
                        <telerik:RadHtmlChart runat="server" ID="RadHtmlChart4" Width="100%" Height="150px" Transitions="true">
                            <PlotArea>
                                <Series>
                                    <telerik:LineSeries Name="Ejemplo" MissingValues="Zero">
                                        <Appearance>
                                            <FillStyle BackgroundColor="Blue"></FillStyle>
                                        </Appearance>
                                        <LabelsAppearance Visible="false"></LabelsAppearance>
                                        <TooltipsAppearance Color="White"></TooltipsAppearance>
                                        <SeriesItems>
                                            <telerik:CategorySeriesItem Y="400"></telerik:CategorySeriesItem>
                                            <telerik:CategorySeriesItem Y="720"></telerik:CategorySeriesItem>
                                            <telerik:CategorySeriesItem Y="1300"></telerik:CategorySeriesItem>
                                            <telerik:CategorySeriesItem Y="450"></telerik:CategorySeriesItem>
                                            <telerik:CategorySeriesItem />
                                            <telerik:CategorySeriesItem Y="600"></telerik:CategorySeriesItem>
                                            <telerik:CategorySeriesItem Y="900"></telerik:CategorySeriesItem>
                                        </SeriesItems>
                                    </telerik:LineSeries>
                                </Series>
                                <Appearance>
                                    <FillStyle BackgroundColor="Transparent"></FillStyle>
                                </Appearance>
                                <XAxis Visible="false">
                                </XAxis>
                                <YAxis Visible="false">
                                </YAxis>
                            </PlotArea>
                            <Appearance>
                                <FillStyle BackgroundColor="Transparent"></FillStyle>
                            </Appearance>
                        </telerik:RadHtmlChart>
                    </div>

                    <div class="col-md-4 my-1">
                        <h6>
                            <telerik:RadButton runat="server" ButtonType="ToggleButton" ID="btnColumnChart" Text="Gráfica de columnas" ToggleType="Radio" GroupName="ChooseChart" AutoPostBack="false" Width="100%"></telerik:RadButton>
                        </h6>
                        <telerik:RadHtmlChart runat="server" ID="RadHtmlChart6" Width="100%" Height="150px" Transitions="true">
                            <PlotArea>
                                <Series>
                                    <telerik:ColumnSeries Name="Ejemplo">
                                        <Appearance>
                                            <FillStyle BackgroundColor="Blue"></FillStyle>
                                        </Appearance>
                                        <TooltipsAppearance Color="White"></TooltipsAppearance>
                                        <LabelsAppearance Visible="false"></LabelsAppearance>
                                        <SeriesItems>
                                            <telerik:CategorySeriesItem Y="400"></telerik:CategorySeriesItem>
                                            <telerik:CategorySeriesItem Y="720"></telerik:CategorySeriesItem>
                                            <telerik:CategorySeriesItem Y="1300"></telerik:CategorySeriesItem>
                                            <telerik:CategorySeriesItem Y="450"></telerik:CategorySeriesItem>
                                            <telerik:CategorySeriesItem />
                                            <telerik:CategorySeriesItem Y="600"></telerik:CategorySeriesItem>
                                            <telerik:CategorySeriesItem Y="900"></telerik:CategorySeriesItem>
                                        </SeriesItems>
                                    </telerik:ColumnSeries>
                                </Series>
                                <Appearance>
                                    <FillStyle BackgroundColor="Transparent"></FillStyle>
                                </Appearance>
                                <XAxis Visible="false">
                                </XAxis>
                                <YAxis Visible="false">
                                </YAxis>
                            </PlotArea>
                            <Appearance>
                                <FillStyle BackgroundColor="Transparent"></FillStyle>
                            </Appearance>
                        </telerik:RadHtmlChart>
                    </div>

                </div>

                <div class="row">

                    <div class="col-md-4 my-1">
                        <h6>
                            <telerik:RadButton runat="server" ButtonType="ToggleButton" ID="btnBarChart" Text="Gráfica de barras" ToggleType="Radio" GroupName="ChooseChart" AutoPostBack="false" Width="100%"></telerik:RadButton>
                        </h6>
                        <telerik:RadHtmlChart runat="server" ID="RadHtmlChart2" Width="100%" Height="150px" Transitions="true">
                            <PlotArea>
                                <Series>
                                    <telerik:BarSeries Name="Ejemplo" Stacked="false">
                                        <Appearance>
                                            <FillStyle BackgroundColor="Blue"></FillStyle>
                                        </Appearance>
                                        <TooltipsAppearance Color="White"></TooltipsAppearance>
                                        <LabelsAppearance Visible="false"></LabelsAppearance>
                                        <SeriesItems>
                                            <telerik:CategorySeriesItem Y="400"></telerik:CategorySeriesItem>
                                            <telerik:CategorySeriesItem Y="720"></telerik:CategorySeriesItem>
                                            <telerik:CategorySeriesItem Y="1300"></telerik:CategorySeriesItem>
                                            <telerik:CategorySeriesItem Y="450"></telerik:CategorySeriesItem>
                                            <telerik:CategorySeriesItem />
                                            <telerik:CategorySeriesItem Y="600"></telerik:CategorySeriesItem>
                                            <telerik:CategorySeriesItem Y="900"></telerik:CategorySeriesItem>
                                        </SeriesItems>
                                    </telerik:BarSeries>
                                </Series>
                                <Appearance>
                                    <FillStyle BackgroundColor="Transparent"></FillStyle>
                                </Appearance>
                                <XAxis Visible="false">
                                </XAxis>
                                <YAxis Visible="false">
                                </YAxis>
                            </PlotArea>
                            <Appearance>
                                <FillStyle BackgroundColor="Transparent"></FillStyle>
                            </Appearance>
                        </telerik:RadHtmlChart>
                    </div>

                    <div class="col-md-4 my-1">
                        <h6>
                            <telerik:RadButton runat="server" ButtonType="ToggleButton" ID="btnDonutChart" Text="Gráfica de dona" ToggleType="Radio" GroupName="ChooseChart" AutoPostBack="false" Width="100%"></telerik:RadButton>
                        </h6>
                        <telerik:RadHtmlChart runat="server" ID="RadHtmlChart3" Width="100%" Height="150px" Transitions="true">
                            <PlotArea>
                                <Series>
                                    <telerik:DonutSeries Name="Ejemplo">
                                        <Appearance>
                                            <FillStyle BackgroundColor="Blue"></FillStyle>
                                        </Appearance>
                                        <TooltipsAppearance Color="White"></TooltipsAppearance>
                                        <LabelsAppearance Visible="false"></LabelsAppearance>
                                        <SeriesItems>
                                            <telerik:PieSeriesItem Y="400"></telerik:PieSeriesItem>
                                            <telerik:PieSeriesItem Y="720"></telerik:PieSeriesItem>
                                            <telerik:PieSeriesItem Y="1300"></telerik:PieSeriesItem>
                                            <telerik:PieSeriesItem Y="450"></telerik:PieSeriesItem>
                                            <telerik:PieSeriesItem />
                                            <telerik:PieSeriesItem Y="600"></telerik:PieSeriesItem>
                                            <telerik:PieSeriesItem Y="900"></telerik:PieSeriesItem>
                                        </SeriesItems>
                                    </telerik:DonutSeries>
                                </Series>
                                <Appearance>
                                    <FillStyle BackgroundColor="Transparent"></FillStyle>
                                </Appearance>
                                <XAxis Visible="false">
                                </XAxis>
                                <YAxis Visible="false">
                                </YAxis>
                            </PlotArea>
                            <Appearance>
                                <FillStyle BackgroundColor="Transparent"></FillStyle>
                            </Appearance>
                        </telerik:RadHtmlChart>
                    </div>

                    <div class="col-md-4 my-1">
                        <h6>
                            <telerik:RadButton runat="server" ButtonType="ToggleButton" ID="btnPieChart" Text="Gráfica de pastel" ToggleType="Radio" GroupName="ChooseChart" AutoPostBack="false" Width="100%"></telerik:RadButton>
                        </h6>
                        <telerik:RadHtmlChart runat="server" ID="RadHtmlChart5" Width="100%" Height="150px" Transitions="true">
                            <PlotArea>
                                <Series>
                                    <telerik:PieSeries Name="Ejemplo">
                                        <Appearance>
                                            <FillStyle BackgroundColor="Blue"></FillStyle>
                                        </Appearance>
                                        <TooltipsAppearance Color="White"></TooltipsAppearance>
                                        <LabelsAppearance Visible="false"></LabelsAppearance>
                                        <SeriesItems>
                                            <telerik:PieSeriesItem Y="400"></telerik:PieSeriesItem>
                                            <telerik:PieSeriesItem Y="720"></telerik:PieSeriesItem>
                                            <telerik:PieSeriesItem Y="1300"></telerik:PieSeriesItem>
                                            <telerik:PieSeriesItem Y="450"></telerik:PieSeriesItem>
                                            <telerik:PieSeriesItem />
                                            <telerik:PieSeriesItem Y="600"></telerik:PieSeriesItem>
                                            <telerik:PieSeriesItem Y="900"></telerik:PieSeriesItem>
                                        </SeriesItems>
                                    </telerik:PieSeries>
                                </Series>
                                <Appearance>
                                    <FillStyle BackgroundColor="Transparent"></FillStyle>
                                </Appearance>
                                <XAxis Visible="false">
                                </XAxis>
                                <YAxis Visible="false">
                                </YAxis>
                            </PlotArea>
                            <Appearance>
                                <FillStyle BackgroundColor="Transparent"></FillStyle>
                            </Appearance>
                        </telerik:RadHtmlChart>
                    </div>

                </div>

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
                                    <telerik:RadButtonToggleState value="Count" Text="Contar"></telerik:RadButtonToggleState>
                                </ToggleStates>
                            </telerik:RadButton>
                        </label>
                        <telerik:RadDropDownList runat="server" ID="ddlSumarContar" DefaultMessage="Seleccione" Width="100%"></telerik:RadDropDownList>
                    </div>
                </div>
            </fieldset>
            <div class="container-fluid my-4">
                <div class="text-right">
                    <telerik:RadButton runat="server" ID="btnTerminar" CssClass="bg-success text-white border-0" Text="Finalizar" Width="150px"></telerik:RadButton>
                </div>
            </div>
        </div>
        <telerik:RadNotification ID="RadNotification1" runat="server" EnableRoundedCorners="true" EnableShadow="true" Width="300" Height="100" AutoCloseDelay="5100">
        </telerik:RadNotification>
    </telerik:RadAjaxPanel>
</asp:Content>