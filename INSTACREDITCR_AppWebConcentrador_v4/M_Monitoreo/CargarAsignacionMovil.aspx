<%@ Page Title="" Language="VB" MasterPageFile="~/M_Monitoreo/MasterPage.master" AutoEventWireup="false" CodeFile="CargarAsignacionMovil.aspx.vb" Inherits="M_Monitoreo_CargarAsignacionMovil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Asignacion Móvil</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<div class="Pagina">--%>
    <telerik:RadAjaxLoadingPanel ID="Radpanelcarga" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="Pnlgen" runat="server" HorizontalAlign="NotSet" LoadingPanelID="Radpanelcarga">
        <div class="container w3-white">
            <h4>
                <asp:Label ID="LblTitulo" runat="server" Text="Carga Asignación Movil"></asp:Label>
            </h4>
            <div class="w-100 mt-3">
                Layout para carga de cartera. El archivo debe ser CSV o TXT con encabezados.
            </div>
            <div class="table-responsive">
                <table class="table">
                    <tr>
                        <th>Campo</th>
                        <td>Credito</td>
                        <td>Usuario</td>
                    </tr>
                    <tr>
                        <th>Observaciones</th>
                        <td>Hasta 50 caracteres</td>
                        <td>Hasta 50 caracteres</td>
                    </tr>
                </table>
            </div>
            <div class="w3-row-padding mt-2">
                <div class="w3-third">
                    <telerik:RadLabel runat="server" ID="RadLabel1" Text="Tipo de Asignación:"></telerik:RadLabel>
                    <telerik:RadComboBox ID="DdltipoAsig" runat="server" AutoPostBack="True" Culture="es-ES" EmptyMessage="Selecciona un tipo de asignación" Width="100%">
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Text="Archivo - Cobranza" Value="1" />
                        </Items>
                    </telerik:RadComboBox>
                </div>
                <div class="w3-third">
                    <label>Seleccionar archivo:</label>
                    <telerik:RadAsyncUpload ID="RadAsyncUpload1" runat="server" AllowedFileExtensions=".CSV,.txt" MaxFileInputsCount="1" Width="100%">
                        <Localization Select="Seleccionar"/>
                    </telerik:RadAsyncUpload>
                </div>
                <div class="w3-third">
                    <telerik:RadButton ID="BtnCargar" runat="server" CssClass="w3-block bg-success border-0 text-white" Text="Cargar" Enabled="False" />
                </div><br /><br />
                <div class="d-flex justify-content-center my-2">
                    <telerik:RadLabel ID="LblMensaje" runat="server" Text=""></telerik:RadLabel>
                </div>
                <div class="d-flex justify-content-center my-2">
                    <telerik:RadGrid ID="GvCargaAsignacion2" runat="server" Width="600px" Visible="false"></telerik:RadGrid>
                </div>
            </div>
        </div>
    </telerik:RadAjaxPanel>
    <asp:HiddenField ID="HidenUrs" runat="server" />
    <telerik:RadWindowManager ID="WinMsj" runat="server"></telerik:RadWindowManager>

</asp:Content>
