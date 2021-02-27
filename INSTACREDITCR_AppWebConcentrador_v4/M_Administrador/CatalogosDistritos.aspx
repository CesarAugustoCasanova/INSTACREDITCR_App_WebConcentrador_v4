<%@ Page Title="" Language="VB" MasterPageFile="~/M_Administrador/MasterPage.master" AutoEventWireup="false" CodeFile="CatalogosDistritos.aspx.vb" Inherits="M_Administrador_CatalogosDistritos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMaster" runat="Server">
    <div class="w3-container Titulos w3-center w3-margin-bottom">Catalogo de Distritos</div>
    <div class="w3-container w3-center w3-padding">
        <%--<telerik:RadAjaxLoadingPanel ID="Radpanelcarga" runat="server"></telerik:RadAjaxLoadingPanel>
        <telerik:RadAjaxPanel runat="server" LoadingPanelID="Radpanelcarga" CssClass="w3-panel">--%>
        <telerik:RadGrid runat="server" ID="RGDistritos" AutoGenerateColumns="False" ClientSettings-EnableRowHoverStyle="true" Width="1200px"  AllowPaging="True" PageSize="25" AllowFilteringByColumn="true">
            <ExportSettings ExportOnlyData="true" FileName="Usuarios" IgnorePaging="true" OpenInNewWindow="true">
                <Csv ColumnDelimiter="Comma" EncloseDataWithQuotes="true" />
                <Excel DefaultCellAlignment="Left" AutoFitImages="true" />
            </ExportSettings>
            <MasterTableView CommandItemDisplay="Top"  EditMode="PopUp" DataKeyNames="ID" EditFormSettings-PopUpSettings-KeepInScreenBounds="true">
                <CommandItemSettings AddNewRecordText="Añadir Distrito" RefreshText="Recargar" />
                <Columns>
                    <telerik:GridEditCommandColumn ItemStyle-Width="5px" ButtonType="FontIconButton" ></telerik:GridEditCommandColumn>
                    <telerik:GridButtonColumn ItemStyle-Width="5px" HeaderText="Eliminar" CommandName="Delete" ConfirmDialogType="RadWindow" ConfirmTextFormatString="¿Desea elminiar '{0}'?" ConfirmTextFields="Plaza" ConfirmTitle="Confirmar"></telerik:GridButtonColumn>
                    <telerik:GridBoundColumn UniqueName="ID" HeaderText="ID" DataField="ID" AllowSorting="true" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"></telerik:GridBoundColumn>
                   <%-- <telerik:GridBoundColumn UniqueName="GERENTE" HeaderText="Gerente" DataField="GERENTE" AllowSorting="true"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="SUPERVISOR" HeaderText="Supervisor" DataField="SUPERVISOR" AllowSorting="true"></telerik:GridBoundColumn>--%>
                   
                    <telerik:GridBoundColumn UniqueName="Plaza" HeaderText="Nombre Plaza" DataField="PLAZA" AllowSorting="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"></telerik:GridBoundColumn>
                     <telerik:GridBoundColumn UniqueName="REGION" HeaderText="Region" DataField="REGION" AllowSorting="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="REGIONAL" HeaderText="Jefe Regional" DataField="REGIONAL" AllowSorting="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="JEFEPLAZA" HeaderText="Jefe de Plaza" DataField="JEFEPLAZA" AllowSorting="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="GESTOR" HeaderText="Gestor" DataField="GESTOR" AllowSorting="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="AUXILIAR" HeaderText="Auxiliar" DataField="AUXILIAR" AllowSorting="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="NumPlaza" HeaderText="Numero Plaza" DataField="NUMPLAZA" AllowSorting="true" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"></telerik:GridBoundColumn>
                   
                   <%-- <telerik:GridBoundColumn UniqueName="DISTRITO" HeaderText="Distrito" DataField="DISTRITO" AllowSorting="true"></telerik:GridBoundColumn>--%>
                   <telerik:GridBoundColumn UniqueName="USUARIO" HeaderText="Usuario" DataField="USUARIO" AllowSorting="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="Zona" HeaderText="Zona" DataField="ZONA" AllowSorting="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"></telerik:GridBoundColumn>
                   
                    <telerik:GridBoundColumn ItemStyle-Width="500px" ItemStyle-HorizontalAlign="Left" UniqueName="CodigoI" HeaderText="Codigos Postales" DataField="CPI" AllowSorting="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"></telerik:GridBoundColumn>
                    
                </Columns>
                <EditFormSettings  CaptionDataField="Plaza" CaptionFormatString="Editando {0}" UserControlName="./CodigosPostales.ascx" EditFormType="WebUserControl">
                    <EditColumn UniqueName="EditCommandColumn1">
                    </EditColumn>
                    <PopUpSettings Modal="true" KeepInScreenBounds="true" ShowCaptionInEditForm="true" OverflowPosition="Center" Height="90%" Width="100%" CloseButtonToolTip="Cerrar" ScrollBars="Auto"/>
                </EditFormSettings>
            </MasterTableView>
        </telerik:RadGrid>
        <telerik:RadNotification RenderMode="Lightweight" ID="RadNotification1" runat="server" Height="140px" Animation="Fade" EnableRoundedCorners="true" EnableShadow="true" AutoCloseDelay="3500" Position="Center" ShowCloseButton="true" VisibleOnPageLoad="false" LoadContentOn="EveryShow" KeepOnMouseOver="false">
        </telerik:RadNotification>
        <%--  </telerik:RadAjaxPanel>--%>
    </div>
</asp:Content>

