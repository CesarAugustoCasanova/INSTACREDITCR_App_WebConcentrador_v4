<%@ Page Language="VB" AutoEventWireup="false"  CodeFile="InformacionGeneral.aspx.vb" Inherits="M_Gestion_InformacionGeneral" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="JS/iFrameMejorado.js?v=2" type="text/javascript"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
    <link rel="stylesheet" href="Estilos/w3.css" />
    <title></title>
</head>
<body style="font-size: .7em" class="scroll">
    <form id="form1" runat="server" onmousemove="window.parent.movement();">
        <noscript>
            <div class="w3-modal" style="display: block">
                <div class="w3-modal-content">
                    <div class="w3-container w3-red w3-center w3-padding-24 w3-jumbo">
                        JavaScript deshabilitado
                    </div>
                    <div class="w3-container w3-center w3-xlarge">
                        Javascript está deshabilitado en su navegador web. Por favor, para ver correctamente este sitio, <b><i>habilite javascript</i></b>.<br />
                        <br />
                        Para ver las instrucciones para habilitar javascript en su navegador, haga click <a href="http://www.enable-javascript.com/es/">aquí</a>.
                    </div>
                </div>
            </div>
        </noscript>
        <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
        <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1"></telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel runat="server" ID="loadingPnl"></telerik:RadAjaxLoadingPanel>
        <telerik:RadLabel ID="LblCat_Lo_Usuario" runat="server" Visible="false"></telerik:RadLabel>
        <!-- Informacion Sistema -->
        <div class="w3-margin-top s12">
            <div class="w3-container w3-center w3-blue">
                <b>Información de Gestión</b>
            </div>
            <div class="w3-row-padding">
                <div class="w3-col w3-left s12 m12 l4">
                    <label>Estado de la Gestión</label>
                    <asp:Image ID="ImgSemaforo" runat="server" Width="75px" />

                </div>
                <div class="w3-col w3-center s12 m12 l4">
                    <telerik:RadLabel ID="LblVi_Dias_Semaforo_Gestion" runat="server" CssClass="w3-border w3-round" Width="100%"></telerik:RadLabel>
                </div>
                <div class="w3-col w3-right-align s12 m12 l4">
                    <label>Deudor Contactado</label>
                    <asp:Image runat="server" ID="ImgNoContacto" Width="40px"></asp:Image>
                </div>
            </div>
            <div class="w3-row-padding  w3-margin-top">
                <div class="w3-col s12 m12 l3">
                    <label>Primera Gestión</label>
                    <telerik:RadLabel ID="TxtPr_Mc_Primeragestion" runat="server" CssClass="w3-border w3-round" Width="100%" ReadOnly="true"></telerik:RadLabel>
                </div>
                <div class="w3-col s12 m12 l3">
                    <label>Fecha Primera Gestión</label>
                    <telerik:RadLabel ID="TxtPr_Mc_Dteprimeragestion" runat="server" CssClass="w3-border w3-round" Width="100%" ReadOnly="true"></telerik:RadLabel>
                </div>

                <div class="w3-col s12 m12 l3">
                    <label>Mejor Gestión</label>
                    <telerik:RadLabel ID="TxtPr_Mc_Resultadorelev" runat="server" CssClass="w3-border w3-round" Width="100%" ReadOnly="true"></telerik:RadLabel>
                </div>
                <div class="w3-col s12 m12 l3">
                    <label>Fecha Mejor Gestión</label>
                    <telerik:RadLabel ID="TxtPr_Mc_Dteresultadorelev" runat="server" CssClass="w3-border w3-round" Width="100%" ReadOnly="true"></telerik:RadLabel>
                </div>
            </div>
            <div class="w3-row-padding w3-margin-top">
                <div class="w3-col s12 m12 l3">
                    <label>Gestor Trabajo</label>
                    <telerik:RadLabel ID="TxtPr_Mc_Usuario" runat="server" CssClass="w3-border w3-round" Width="100%" ReadOnly="true"></telerik:RadLabel>
                </div>
                <div class="w3-col s12 m12 l3">
                    <label>Gestor Asignado</label>
                    <telerik:RadLabel ID="TxtPr_Mc_Uasignado" runat="server" CssClass="w3-border w3-round" Width="100%" ReadOnly="true"></telerik:RadLabel>
                </div>
                <div class="w3-col s12 m12 l3">
                    <label>Ultima Gestión</label>
                    <telerik:RadLabel ID="TxtPr_Mc_Resultado" runat="server" CssClass="w3-border w3-round" Width="100%" ReadOnly="true"></telerik:RadLabel>
                </div>
                <div class="w3-col s12 m12 l3">
                    <label>Fecha Ultima Gestión</label>
                    <telerik:RadLabel ID="TxtPr_Mc_Dtegestion" runat="server" CssClass="w3-border w3-round" Width="100%" ReadOnly="true"></telerik:RadLabel>
                </div>
            </div>
            <div class="w3-row-padding w3-margin-top">
                <div class="w3-col s12 m12 l2">
                    <label>Usuario Extrajudicial</label>
                    <br />
                    <telerik:RadLabel ID="TxtUsrExtrajudicial" runat="server" CssClass="w3-border w3-round" Width="100%" ReadOnly="true"></telerik:RadLabel>
                </div>
                <div class="w3-col s12 m12 l2">
                    <label>Promesas Generadas</label>
                    <br />
                    <telerik:RadLabel ID="TxtProm_PG" runat="server" CssClass="w3-border w3-round" Width="100%" ReadOnly="true"></telerik:RadLabel>
                </div>                
                <div class="w3-col s12 m12 l2">
                    <label>Promesas Cumplidas</label>
                    <telerik:RadLabel ID="TxtProm_PC" runat="server" CssClass="w3-border w3-round" Width="100%" ReadOnly="true"></telerik:RadLabel>
                </div>
                <%--<div class="w3-col w3-center s12 m12 l2">
                    <label>Fichas Cumplidas</label>
                    <br />
                    <telerik:RadLabel ID="TxtProm_NC" runat="server" CssClass="w3-border w3-round" Width="100%" ReadOnly="true"></telerik:RadLabel>
                </div>--%>
                <div class="w3-col s12 m12 l2">
                    <label>Promesas Incumplidas</label>
                    <telerik:RadLabel ID="TxtProm_PI" runat="server" CssClass="w3-border w3-round" Width="100%" ReadOnly="true"></telerik:RadLabel>
                </div>
                               <%-- <div class="w3-col w3-center s12 m12 l2">
                    <label>Fichas Incumplidas</label>
                    <br />
                    <telerik:RadLabel ID="TxtProm_NI" runat="server" CssClass="w3-border w3-round" Width="100%" ReadOnly="true"></telerik:RadLabel>
                </div>--%>
            </div>
        </div>
        <telerik:RadTabStrip runat="server" ID="RadTabStrip1" MultiPageID="RadMultiPage1" SelectedIndex="0">
            <Tabs>
                <telerik:RadTab Text="Inicio" Selected="True"></telerik:RadTab>
                <telerik:RadTab Text="General"></telerik:RadTab>
                <telerik:RadTab Text="Financiera"></telerik:RadTab>
                <telerik:RadTab Text="Avales" visible="false"></telerik:RadTab>
                <telerik:RadTab Text="Juicios" Visible="false"></telerik:RadTab>
                <telerik:RadTab Text="Refs de pago" ></telerik:RadTab>
                <telerik:RadTab Text="Actividad" Visible="false"></telerik:RadTab>
                <telerik:RadTab Text="Condonaciones" Visible="false"></telerik:RadTab>
                <telerik:RadTab Text="Condonación de gastos" Visible="false"></telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0" Width="100%">
            <telerik:RadPageView runat="server">
                <telerik:RadAjaxPanel runat="server" ID="pnlInfoBasica">
                    <div class="w3-container w3-center w3-blue">
                        <b>Información Básica de Crédito</b>
                    </div>
                </telerik:RadAjaxPanel>
            </telerik:RadPageView>
            <telerik:RadPageView runat="server">
                <telerik:RadAjaxPanel runat="server" ID="pnlInfoGeneral">
                    <div class="w3-container w3-blue w3-center w3-text-white">
                        <b>Información General</b>
                    </div>
                </telerik:RadAjaxPanel>
            </telerik:RadPageView>
            <telerik:RadPageView runat="server">
                <telerik:RadAjaxPanel runat="server" ID="pnlInfoFinan">
                    <div class="w3-container w3-center w3-blue">
                        <b>Información Financiera para Gestión</b>
                    </div>
                </telerik:RadAjaxPanel>
            </telerik:RadPageView>
            <telerik:RadPageView runat="server">
                <telerik:RadAjaxPanel runat="server" ID="pnlAvales" Visible="false">
                    <div class="w3-container w3-blue w3-center w3-text-white">
                        <b>Avales</b>
                    </div>
                    <div class="w3-container w3-blue w3-center w3-text-white">
                        <telerik:RadGrid runat="server" ID="rGvAvales">
                            <MasterTableView>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
                </telerik:RadAjaxPanel>
            </telerik:RadPageView>
            <telerik:RadPageView runat="server">
                <telerik:RadAjaxPanel runat="server" ID="pnlJuicio">
                    <div class="w3-container w3-blue w3-center w3-text-white">
                        <b>Informacion de Juicio</b>
                    </div>
                </telerik:RadAjaxPanel>
            </telerik:RadPageView>
            <telerik:RadPageView runat="server">
                <telerik:RadAjaxPanel runat="server" ID="pnlCamapanasEspeciales">
                    <div class="w3-container w3-blue w3-center w3-text-white">
                        <b>Campañas especiales</b>
                    </div>
                </telerik:RadAjaxPanel>
            </telerik:RadPageView>
            <telerik:RadPageView runat="server">
                <div class="w3-row w3-panel" style="overflow: auto">
                    <telerik:RadGrid RenderMode="Lightweight" ID="GvHistActResumido" runat="server" Visible="false" HeaderStyle-HorizontalAlign="Center">
                    </telerik:RadGrid>
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView runat="server">
                <telerik:RadAjaxPanel runat="server" ID="pnlCondonaciones">
                    <div class="w3-container w3-blue w3-center w3-text-white">
                        <b>Condonaciones</b>
                    </div>
                </telerik:RadAjaxPanel>
            </telerik:RadPageView>
            <telerik:RadPageView runat="server">
                <div class="w3-container w3-blue w3-center w3-text-white">
                    <b>Condonación de gastos</b>
                </div>
                <telerik:RadLabel runat="server" ID="RLblMsj"></telerik:RadLabel>
                <telerik:radajaxPanel runat="server" ID="PnlGastos" Visible="false" LoadingPanelID="loadingPnl">
                    <div class="w3-container">
                        <telerik:RadButton ID="RBSolicitar" Text="Solicitar" runat="server"></telerik:RadButton>
                    </div>
                    <div class="w3-container">
                        <telerik:RadTextBox ID="RtxtComentario" runat="server" MaxLength="200" Width="600px" TextMode="MultiLine" Height="150px"></telerik:RadTextBox>
                    </div>
                    <div class="w3-container">
                        <telerik:RadGrid ID="RGGastos" OnNeedDataSource="RGGastos_NeedDataSource" HeaderStyle-HorizontalAlign="Center"
                            AllowMultiRowSelection="true" runat="server" AutoGenerateColumns="False"
                            GridLines="None" Font-Size="Small">
                            <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                            <ClientSettings>
                                <Scrolling AllowScroll="True" FrozenColumnsCount="2" SaveScrollPosition="true" UseStaticHeaders="True" />
                            </ClientSettings>
                            <MasterTableView AllowMultiColumnSorting="false">
                                <PagerStyle AlwaysVisible="true" />
                                <CommandItemSettings ShowAddNewRecordButton="false" />
                                <Columns>
                                    <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn">
                                    </telerik:GridClientSelectColumn>
                                    <telerik:GridBoundColumn UniqueName="ID" HeaderText="ID" DataField="ID"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="NombreGasto" HeaderText="Nombre Gasto" DataField="NombreGasto"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="Monto" HeaderText="Monto" DataField="Monto"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="Registro" HeaderText="Registro" DataField="Registro"></telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                            <ClientSettings EnableRowHoverStyle="true">
                                <Selecting AllowRowSelect="True" UseClientSelectColumnOnly="true"></Selecting>
                            </ClientSettings>
                        </telerik:RadGrid>
                    </div>
                </telerik:radajaxPanel>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
        <telerik:RadWindowManager ID="RadAviso" runat="server">
            <Localization OK="Aceptar" />
        </telerik:RadWindowManager>
    </form>
</body>
</html>
