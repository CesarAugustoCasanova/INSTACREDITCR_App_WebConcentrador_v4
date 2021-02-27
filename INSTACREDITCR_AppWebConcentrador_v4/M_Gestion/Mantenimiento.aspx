<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Mantenimiento.aspx.vb" Inherits="Mantenimiento" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mc :: Modulo Gestión</title>
    <script src="JS/iFrameMejorado.js?v=2" type="text/javascript"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
    <link rel="stylesheet" href="Estilos/w3.css" />
</head>
<body style="font-size: .7em" class="scroll w3-padding">
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
        <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="DrlFila">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="GridFilas" />
                        <telerik:AjaxUpdatedControl ControlID="BtnCrear" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="BtnCrear">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="Notificacion" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <!-- Filas de Trabajo -->
        <asp:Panel runat="server" ID="pnlFilaTrabajo" Visible='<%# tmpPermisos("MANTENIMIENTO_FILAS_TRABAJO") %>'>
        <div class="w3-container w3-center w3-blue">
            <b>Filas de Trabajo</b>
        </div>
        <div class="w3-container w3-center">
            <label>Crear fila de Trabajo por:</label>
            <telerik:RadDropDownList ID="DrlFila" runat="server" AutoPostBack="true">
                <Items>
                    <telerik:DropDownListItem Text="Seleccione" />
                    <telerik:DropDownListItem Text="Código Resultado" />
                    <telerik:DropDownListItem Text="Código Resultado Ponderado" />
                    <telerik:DropDownListItem Text="Semaforo" />
                    <telerik:DropDownListItem Text="Promesas de Pago" />
                    <telerik:DropDownListItem Text="Contactar Hoy" />
                </Items>
            </telerik:RadDropDownList>
        </div>
        <div class="w3-container w3-center" style="overflow: auto">
            <telerik:RadGrid RenderMode="Lightweight" ID="GridFilas" HeaderStyle-HorizontalAlign="Center" runat="server" OnNeedDataSource="GridFilas_NeedDataSource" AllowMultiRowSelection="True" Visible="true" Style="overflow: visible;">
                <HeaderStyle HorizontalAlign="Center" />
                <MasterTableView>
                    <Columns>
                        <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn">
                        </telerik:GridClientSelectColumn>
                    </Columns>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="true">
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </div>
            </asp:Panel>
        <br />
        <div class="w3-center">
            <telerik:RadButton Visible="false" ID="BtnCrear" runat="server" Text="Crear Fila" />
        <br />
        <telerik:RadLabel ID="LblMsjFilas" runat="server" CssClass="LblMsj" />
        <br />
        </div>
        <!-- Avance -->
        <div class="w3-container w3-center w3-blue">
            <b>Avance</b>
        </div>

        <div class="w3-container w3-blue w3-margin">
            <asp:Label ID="LblAsignadas" runat="server"></asp:Label>
        </div>
        <div class="w3-row-padding">
            <div class="w3-half w3-card">
                <div class="w3-container w3-blue w3-center">
                    <b>Resultados en el día</b>
                </div>
                <div class="w3-row-padding w3-margin-top">
                    <div class="w3-half">
                        <div class="w3-center w3-container">
                            Cuentras trabajadas: 
                            <asp:Label ID="LblTrabajadas" runat="server"></asp:Label>
                        </div>
                        <br />
                        <div class="w3-container w3-center">
                            <asp:Label ID="LblPPPD" runat="server" CssClass="w3-text-blue"></asp:Label>
                        </div>
                        <br />
                        <div class="w3-container w3-center">
                            <b>
                                <asp:Label ID="LblMtoPPPD" runat="server"></asp:Label></b>
                        </div>
                        <br />
                    </div>
                    <div class="w3-half">
                        <div class="w3-container w3-center" style="overflow: auto">
                            <telerik:RadGrid ID="GvRDia" HeaderStyle-HorizontalAlign="Center" runat="server" OnNeedDataSource="GvRDia_NeedDataSource" Visible="true"></telerik:RadGrid>
                        </div>
                        <br />
                    </div>
                </div>
            </div>
            <div class="w3-half w3-card">
                <div class="w3-container w3-blue w3-center">
                    <b>Resultados en el mes</b>
                </div>
                <div class="w3-row-padding w3-margin-top">
                    <div class="w3-half">
                        <div class="w3-center w3-container">
                            Cuentras trabajadas: 
                            <asp:Label ID="LblTrabajadasM" runat="server"></asp:Label>
                        </div>
                        <br />
                        <div class="w3-center w3-container">
                            <asp:Label ID="LblPPPM" runat="server" CssClass="w3-text-blue"></asp:Label>
                        </div>
                        <br />
                        <div class="w3-center w3-container">
                            <b><asp:Label ID="LblMtoPPPM" runat="server"></asp:Label></b>
                        </div>
                        <br />
                    </div>
                    <div class="w3-half">
                        <div class="w3-container  w3-center" style="overflow: auto">
                            <telerik:RadGrid ID="GvRmes" HeaderStyle-HorizontalAlign="Center" runat="server" OnNeedDataSource="GvRmes_NeedDataSource" Visible="true"></telerik:RadGrid>
                        </div>
                        <br />

                    </div>
                </div>
            </div>
        </div>
        <br />
        <!-- Mi asignación -->
        <div class="w3-container w3-center w3-blue">
            <b>Mi Asignación</b>
        </div>
        <div class="w3-container">
            <telerik:RadGrid RenderMode="Lightweight" ID="GvAsignacion" HeaderStyle-HorizontalAlign="Center" runat="server" OnNeedDataSource="GvAsignacion_NeedDataSource" Visible="true" Style="overflow: visible; font-size: small">
            </telerik:RadGrid>
        </div>
        <telerik:RadNotification ID="Notificacion" runat="server" Position="Center" Width="330" Height="160" Animation="Fade" EnableRoundedCorners="true" EnableShadow="true" Style="z-index: 100000">
        </telerik:RadNotification>
    </form>
</body>
</html>
