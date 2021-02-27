<%@ Page Title="" Language="VB" MasterPageFile="~/M_Administrador/MasterPage.master" AutoEventWireup="false" CodeFile="Modulos_Acciones.aspx.vb" Inherits="Modulos_Acciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMaster" runat="Server">
    <h2 class="text-center">Acciones</h2>
    <asp:Panel runat="server" ID="pnlAcciones" CssClass="container">
        <%--<div class="card-deck my-1">
            <telerik:RadButton runat="server" ID="PT1" CssClass="card text-center border bg-light" OnClick="Change_UserControl_Click">
                <ContentTemplate>
                    <div class="card-body">
                        <h5 class="card-title text-center">Generar Reporte/Archivo</h5>
                        <p class="card-text">Generar un reporte o alg&uacute;n archivo con la regla seleccionada</p>
                    </div>
                </ContentTemplate>
            </telerik:RadButton>
            <telerik:RadButton runat="server" ID="PT2" CssClass="card text-center border bg-light" OnClick="Change_UserControl_Click">
                <ContentTemplate>
                    <div class="card-body">
                        <h5 class="card-title text-center">Asignar</h5>
                        <p class="card-text">Generar una asignacion a usuarios con la regla seleccionada</p>
                    </div>
                </ContentTemplate>
            </telerik:RadButton>
            <telerik:RadButton runat="server" ID="PT3" CssClass="card text-center border bg-light" OnClick="Change_UserControl_Click">
                <ContentTemplate>
                    <div class="card-body">
                        <h5 class="card-title text-center">Generar Etiqueta</h5>
                        <p class="card-text">Generar etiquetas con la regla seleccionada</p>
                    </div>
                </ContentTemplate>
            </telerik:RadButton>
        </div>--%>
        <div class="card-deck my-1">
            <%-- <telerik:RadButton runat="server" ID="PT4" CssClass="card text-center border bg-light" OnClick="Change_UserControl_Click">
                <ContentTemplate>
                    <div class="card-body">
                        <h5 class="card-title text-center">Generar Script</h5>
                        <p class="card-text">Generar script con la regla seleccionada</p>
                    </div>
                </ContentTemplate>
            </telerik:RadButton>--%>
            <telerik:RadButton runat="server" ID="PT5" CssClass="card text-center border bg-light" OnClick="Change_UserControl_Click">
                <ContentTemplate>
                    <div class="card-body">
                        <h5 class="card-title text-center">Campaña</h5>
                        <p class="card-text">Generar una campaña con la regla seleccionada</p>
                    </div>
                </ContentTemplate>
            </telerik:RadButton>
            <%--;telerik:RadButton runat="server" ID="PT6" CssClass="card text-center border bg-light" OnClick="Change_UserControl_Click">
                <ContentTemplate>
                    <div class="card-body">
                        <h5 class="card-title text-center">Colores Para Cuentas</h5>
                        <p class="card-text">Asignar un color a las cuentas con la regla seleccionada</p>
                    </div>
                </ContentTemplate>
            </telerik:RadButton>--%>
        </div>
    </asp:Panel>
</asp:Content>

