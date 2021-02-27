<%@ Page Title="" Language="VB" MasterPageFile="./MasterPage.master" AutoEventWireup="false" CodeFile="CatalogoAvisos.aspx.vb" Inherits="CatalogoAvisos" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMaster" runat="Server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel runat="server" ID="pnlLoading"></telerik:RadAjaxLoadingPanel>
    <div class="w3-panel Titulos">Configuracion Plantillas Avisos</div>
    <%--<telerik:RadAjaxPanel runat="server" CssClass="w3-row-padding" LoadingPanelID="pnlLoading" ShowLoadingPanelForPostBackControls="true">--%>
        <div class="w3-col m12 l5">
            <div class="w3-panel Titulos">Etiquetas</div>
            <telerik:RadGrid runat="server" ID="gridEtiquetas" AllowPaging="true" CssClass="w3-small">
                <ClientSettings>
                    <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="2"></Scrolling>
                </ClientSettings>
                <MasterTableView CommandItemDisplay="Top" AutoGenerateColumns="true">
                    <CommandItemSettings AddNewRecordText="Nueva Etiqueta" RefreshText="Recargar" />
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="FontIconButton" HeaderText="Editar"></telerik:GridEditCommandColumn>
                        <telerik:GridButtonColumn ConfirmText="¿Eliminar etiqueta?" ConfirmDialogType="RadWindow"
                            ConfirmTitle="Eliminar" ButtonType="FontIconButton" CommandName="Delete" HeaderText="Eliminar" />
                    </Columns>
                    <EditFormSettings CaptionDataField="Campana" CaptionFormatString="Editando {0}" EditFormType="WebUserControl" UserControlName="CatalogoAvisosEtiquetas.ascx">
                        <EditColumn UniqueName="EditCommandColumn1">
                        </EditColumn>
                    </EditFormSettings>
                    <NoRecordsTemplate>
                        Agrega una etiqueta para comenzar.
                    </NoRecordsTemplate>
                </MasterTableView>
            </telerik:RadGrid>
        </div>
        <div class="w3-col m12 l7">
            <div class="w3-panel Titulos">Plantillas</div>
            <telerik:RadGrid runat="server" ID="gridPlantillas" AllowPaging="true" CssClass="w3-small">
                <ClientSettings>
                    <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="2"></Scrolling>
                </ClientSettings>
                <MasterTableView EditMode="PopUp" CommandItemDisplay="Top" AutoGenerateColumns="true">
                    <CommandItemSettings AddNewRecordText="Nueva Plantilla" RefreshText="Recargar" />
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="FontIconButton" HeaderText="Editar"></telerik:GridEditCommandColumn>
                        <telerik:GridButtonColumn ConfirmText="¿Eliminar plantilla?" ConfirmDialogType="RadWindow"
                            ConfirmTitle="Eliminar" ButtonType="FontIconButton" CommandName="Delete" HeaderText="Eliminar" />
                    </Columns>
                    <EditFormSettings CaptionDataField="Nombre" CaptionFormatString="Editando {0}" EditFormType="WebUserControl" UserControlName="CatalogoAvisosPlantillas.ascx">
                        <EditColumn UniqueName="EditCommandColumn1">
                        </EditColumn>
                        <PopUpSettings Modal="true" KeepInScreenBounds="true" ShowCaptionInEditForm="true" OverflowPosition="Center" Height="90%" Width="90%" CloseButtonToolTip="Cerrar" ScrollBars="Auto" />
                    </EditFormSettings>
                    <NoRecordsTemplate>
                        Agrega una plantilla para comenzar.
                    </NoRecordsTemplate>
                </MasterTableView>
            </telerik:RadGrid>
        </div>
<%--    </telerik:RadAjaxPanel>--%>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Animation="Resize" EnableShadow="true" Modal="True" RenderMode="Lightweight" ShowContentDuringLoad="false" VisibleTitlebar="False">
                    <Localization OK="Aceptar" />
                </telerik:RadWindowManager>
</asp:Content>