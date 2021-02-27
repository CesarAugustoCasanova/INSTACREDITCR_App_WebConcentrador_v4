<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/M_Administrador/MasterPage.master" CodeFile="Usuarios.aspx.vb" Inherits="M_Administrador_Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHMaster" runat="Server">
        <div class="w3-container Titulos w3-center w3-margin-bottom">Usuarios Del Sistema</div>

    <telerik:RadAjaxLoadingPanel ID="Radpanelcarga" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel runat="server" LoadingPanelID="Radpanelcarga" CssClass="w3-panel">

        <telerik:RadGrid runat="server" ID="gridUsuarios" cssclass="w3-small" AllowFilteringByColumn="True">
            <ClientSettings>
                <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="3">
                </Scrolling>
            </ClientSettings>
            <ExportSettings ExportOnlyData="true" FileName="Usuarios" IgnorePaging="true" OpenInNewWindow="true">
                <Csv ColumnDelimiter="Comma" EncloseDataWithQuotes="true" />
                <Excel DefaultCellAlignment="Left" AutoFitImages="true"/>
            </ExportSettings>
            <GroupingSettings CaseSensitive="false"></GroupingSettings>
            <MasterTableView EditMode="PopUp" CommandItemDisplay="Top" AutoGenerateColumns="false" AllowSorting="true">
                <CommandItemSettings AddNewRecordText="Nuevo Usuario" ExportToExcelText="Excel" ExportToCsvText="CSV" RefreshText="Actualizar" ShowExportToCsvButton="true" ShowExportToExcelButton="true"/>                
                <Columns>
                    <telerik:GridButtonColumn ButtonType="ImageButton" ImageUrl="~/Imagenes/user-expired.png" HeaderText="Expirar" CommandName="Expirar" ConfirmDialogType="RadWindow" ConfirmTextFormatString="¿Desea expirar el usuario {0}?" ConfirmTextFields="USUARIO" ConfirmTitle="Expirar usuario" ItemStyle-Width="20px"></telerik:GridButtonColumn>
                    <telerik:GridButtonColumn ButtonType="ImageButton" ImageUrl="~/Imagenes/Usr-Cancelar.png" HeaderText="Cancelar" CommandName="Cancelar" ConfirmDialogType="RadWindow" ConfirmTextFormatString="¿Desea cancelar el usuario {0}?" ConfirmTextFields="USUARIO" ConfirmTitle="Cancelar usuario" ItemStyle-Width="20px"></telerik:GridButtonColumn>
                    <telerik:GridEditCommandColumn ButtonType="FontIconButton" HeaderText="Editar"></telerik:GridEditCommandColumn>

                    <telerik:GridBoundColumn UniqueName="Nombre" HeaderText="Nombre" DataField="Nombre" AllowSorting="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="Usuario" HeaderText="Usuario" DataField="Usuario" AllowSorting="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"></telerik:GridBoundColumn>
                     <telerik:GridBoundColumn UniqueName="Contrasena" HeaderText="Contraseña" DataField="Contrasena" AllowSorting="true" AllowFiltering="false" ></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="Agencia" HeaderText="Agencia" DataField="Agencia" AllowSorting="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"></telerik:GridBoundColumn>
                     <telerik:GridBoundColumn UniqueName="Estatus" HeaderText="Estatus" DataField="Estatus" AllowSorting="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"></telerik:GridBoundColumn>
                     <telerik:GridBoundColumn UniqueName="Puesto" HeaderText="Puesto" DataField="Puesto" AllowSorting="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"></telerik:GridBoundColumn>
                     <telerik:GridBoundColumn UniqueName="Supervisor" HeaderText="Supervisor" DataField="Supervisor" AllowSorting="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"></telerik:GridBoundColumn>
                     <telerik:GridBoundColumn UniqueName="FechaAlta" HeaderText="Fecha Alta" DataField="Fecha Alta" AllowSorting="true" AllowFiltering="false" ></telerik:GridBoundColumn>
                     <telerik:GridBoundColumn UniqueName="Motivo" HeaderText="Motivo" DataField="Motivo" AllowSorting="true" AllowFiltering="false"></telerik:GridBoundColumn>
                     <telerik:GridBoundColumn UniqueName="HorarioEntrada" HeaderText="Horario Entrada" DataField="Horario Entrada" AllowSorting="true" AllowFiltering="false" ></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="HorarioSalida" HeaderText="Horario Salida" DataField="Horario Salida" AllowSorting="true" AllowFiltering="false" ></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="VerCuentas" HeaderText="Ver Cuentas" DataField="Ver Cuentas" AllowSorting="true" AllowFiltering="false" ></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="UltimaModificación" HeaderText="Última modificación" DataField="Última modificación" AllowFiltering="false"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="FechaModificacion" HeaderText="Fecha modificación" DataField="Fecha modificación" AllowSorting="true" AllowFiltering="false" ></telerik:GridBoundColumn>

                </Columns>
                <EditFormSettings CaptionDataField="USUARIO" CaptionFormatString="Editando {0}" EditFormType="WebUserControl" UserControlName="./Usuarios.ascx">
                    <EditColumn UniqueName="EditCommandColumn1">
                    </EditColumn>
                    <PopUpSettings Modal="true" KeepInScreenBounds="true" ShowCaptionInEditForm="true" OverflowPosition="Center" Height="90%" Width="90%" CloseButtonToolTip="Cerrar" ScrollBars="Auto"/>
                </EditFormSettings>
                <NoRecordsTemplate>
                    Sin usuarios.
                </NoRecordsTemplate>
            </MasterTableView>
        </telerik:RadGrid>
        <telerik:RadNotification RenderMode="Lightweight" ID="RadNotification1" runat="server" Height="140px" Animation="Fade" EnableRoundedCorners="true" EnableShadow="true" AutoCloseDelay="3500" Position="Center" ShowCloseButton="true" VisibleOnPageLoad="false" LoadContentOn="EveryShow" KeepOnMouseOver="false">
        </telerik:RadNotification>
    </telerik:RadAjaxPanel>
    <telerik:RadWindowManager ID="RadAviso" runat="server"></telerik:RadWindowManager>

</asp:Content>