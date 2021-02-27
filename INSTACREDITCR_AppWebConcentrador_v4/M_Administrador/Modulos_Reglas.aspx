<%@ Page Title="" Language="VB" MasterPageFile="~/M_Administrador/MasterPage.master" AutoEventWireup="false" CodeFile="Modulos_Reglas.aspx.vb" Inherits="Modulos_Reglas" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPHMaster" runat="Server">
    <div class="container">
        <div class="text-center">
            <h2>Reglas de parametrizacion</h2>
            <div class="text-center mb-2">Selecciona una regla para continuar. Tambien puedes editar, crear o eliminar las reglas que consideres.</div>
        </div>
        <telerik:RadGrid runat="server" ID="gridReglas" Width="100%" AllowSorting="true" AutoGenerateColumns="true" ClientSettings-Selecting-AllowRowSelect="true" AllowMultiRowSelection="false">
            <MasterTableView CommandItemDisplay="Top" AllowPaging="true" PageSize="10" EditMode="PopUp">
                <Columns>
                    <telerik:GridButtonColumn Text="Seleccionar" CommandName="Select" ButtonType="LinkButton">
                    </telerik:GridButtonColumn>
                    <telerik:GridEditCommandColumn ButtonType="FontIconButton" HeaderText="Editar" HeaderStyle-Width="50px"></telerik:GridEditCommandColumn>
                    <telerik:GridButtonColumn ButtonType="FontIconButton" HeaderText="Eliminar" CommandName="Delete" HeaderStyle-Width="50px" ConfirmText="Esta acción no se puede deshacer" ConfirmTitle="¿Eliminar?" ConfirmDialogType="RadWindow" ConfirmDialogWidth="600px"></telerik:GridButtonColumn>
                </Columns>
                <CommandItemSettings AddNewRecordText="Nueva regla" RefreshText="Actualizar" />
                <PagerStyle FirstPageText="Inicio" LastPageText="Fin" NextPageText="Siguiente" Mode="NextPrevAndNumeric" Wrap="true" />
                <EditFormSettings UserControlName="./Modulos_Reglas_Grid.ascx" EditFormType="WebUserControl" >
                    <PopUpSettings CloseButtonToolTip="Cerrar" Modal="true" KeepInScreenBounds="true" Width="90%" OverflowPosition="Center" Height="90%" ScrollBars="Auto"/>
                    <EditColumn UniqueName="EditCommandColumn1">
                    </EditColumn>
                </EditFormSettings>
                <NoRecordsTemplate>
                    <p class="text-center">
                        Sin reglas definidas. Por Favor, agregue una regla para continuar.
                    </p>
                </NoRecordsTemplate>
            </MasterTableView>
        </telerik:RadGrid>
        <telerik:RadWindowManager runat="server"></telerik:RadWindowManager>
        <telerik:RadNotification runat="server" ID="not1" Position="Center" KeepOnMouseOver="true" AutoCloseDelay="10000" ShowCloseButton="true"></telerik:RadNotification>
    </div>
</asp:Content>

