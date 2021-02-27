<%@ Page Title="" Language="VB" MasterPageFile="./MasterPage.master" AutoEventWireup="false" CodeFile="Catalogo_Mail_v2.aspx.vb" Inherits="Catalogo_Mail_v2" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMaster" runat="Server">


    <%--<telerik:RadSkinManager ID="RadSkinManager1" runat="server"  />--%>
    <telerik:RadAjaxPanel runat="server">
        <div class="w3-container w3-margin-bottom Titulos w3-center">
            Configuración de correos
        </div>
        <div class="w3-row-padding">
            <div class="w3-col s12 m6">
                <div class="w3-container w3-margin-bottom Titulos w3-center">
                    Etiquetas
                </div>
                <telerik:RadGrid runat="server" ID="gridEtiquetas" AllowPaging="true" CssClass="w3-small">
                    <MasterTableView CommandItemDisplay="Top" AutoGenerateColumns="true">
                        <CommandItemSettings AddNewRecordText="Nueva Etiqueta" RefreshText="Recargar" />
                        <Columns>
                            <telerik:GridEditCommandColumn ButtonType="FontIconButton" HeaderText="Editar"></telerik:GridEditCommandColumn>
                            <telerik:GridButtonColumn ConfirmText="¿Eliminar etiqueta?" ConfirmDialogType="RadWindow"
                                ConfirmTitle="Eliminar" ButtonType="FontIconButton" CommandName="Delete" HeaderText="Eliminar" />
                        </Columns>
                        <EditFormSettings CaptionDataField="Campana" CaptionFormatString="Editando {0}" EditFormType="WebUserControl" UserControlName="gridEtiquetasCorreo.ascx">
                            <EditColumn UniqueName="EditCommandColumn1">
                            </EditColumn>
                        </EditFormSettings>
                        <NoRecordsTemplate>
                            Agrega una etiqueta para comenzar.
                        </NoRecordsTemplate>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
            <div class="w3-col s12 m6">
                <div class="w3-container w3-margin-bottom Titulos w3-center">
                    Plantillas
                </div>
                <telerik:RadGrid runat="server" ID="gridPlantillas" AllowPaging="true" CssClass="w3-small">
                    <MasterTableView EditMode="PopUp" CommandItemDisplay="Top" AutoGenerateColumns="true">
                        <CommandItemSettings AddNewRecordText="Nueva Plantilla" RefreshText="Recargar" />
                        <Columns>
                            <telerik:GridEditCommandColumn ButtonType="FontIconButton" HeaderText="Editar"></telerik:GridEditCommandColumn>
                            <telerik:GridButtonColumn ConfirmText="¿Eliminar plantilla?" ConfirmDialogType="RadWindow"
                                ConfirmTitle="Eliminar" ButtonType="FontIconButton" CommandName="Delete" HeaderText="Eliminar" />
                        </Columns>
                        <EditFormSettings CaptionDataField="Nombre" CaptionFormatString="Editando {0}" EditFormType="WebUserControl" UserControlName="gridPlantillasCorreo.ascx">
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
        </div>
    </telerik:RadAjaxPanel>

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Animation="Resize" EnableShadow="true" Modal="True" RenderMode="Lightweight" ShowContentDuringLoad="false" VisibleTitlebar="False">
        <Localization OK="Aceptar" />
    </telerik:RadWindowManager>
    <asp:Button ID="Button1" runat="server" Style="visibility: hidden" Text="" />
    <telerik:RadLabel ID="HCat_Pe_id" runat="server" Style="visibility: hidden">
    </telerik:RadLabel>

    <asp:HiddenField ID="HidenUrs" runat="server" />
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <script>
        function pasteAtCursorPos() {
            var data = "Etiqueta 1";//get the desired content
            var editor = $find("ctl00_CPHMaster_editor");
            if (currentRange) {
                currentRange.select(); //restore the selection
            }
            editor.pasteHtml(data); //paste content
        }

        var currentRange = null;

        function OnClientSelectionChange(sender, args) {
            currentRange = sender.getDomRange(); //store current range/cursor position
        }
    </script>
</asp:Content>

