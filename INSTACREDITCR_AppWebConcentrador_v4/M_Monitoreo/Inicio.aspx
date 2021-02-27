<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Inicio.aspx.vb" MasterPageFile="~/M_Monitoreo/MasterPage.master" Inherits="Inicio" %>


<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>BO:: Inicio</title>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadScriptBlock runat="server" >
        <script>

            Avisos = msg => {
                if (msg.d == 'No') {
                    document.getElementById('Rows').innerHTML = "<h4>Avisos</h4>" + "Sin Avisos por el momento" + "</BR></BR></BR></BR>"
                }
                else {
                    var mensajes2 = ' ';
                    var mensajes = JSON.parse(msg.d);
                    for (x = 0; x < mensajes.length; x++) {
                        mensajes2 = mensajes2 + "<p>" + mensajes[x].mensaje + "</p>"
                    }
                    document.getElementById('Rows').innerHTML = "<h4>Avisos</h4>" + mensajes2 + "</BR></BR></BR></BR>"
                }
            }

            Alertas = msg => {
                if (msg.d == 'No') {
                    document.getElementById('RowsAlertas').innerHTML =  "<h4>Alertas</h4><div class='table-responsive'><table class='table table-sm'><thead class='thead-inverse'><tr><th>Usuario</th><th>Mensaje</th><th>Fecha</th></thead>"+ "<tr><td>" + "Sin Alertas" + "</td></tr>";
                }
                else {
                    var mensajesa2 = "<h4>Alertas</h4><div class='table-responsive'><table class='table table-sm'><thead class='thead-inverse'><tr><th>Usuario</th><th>Mensaje</th><th>Fecha</th></thead>";
                    var mensajesa = JSON.parse(msg.d);
                    for (x = 0; x < mensajesa.length; x++) {
                        mensajesa2 = mensajesa2 + "<tr><td>" + mensajesa[x].USUARIO + "</td><td>" + mensajesa[x].EVENTO + "</td><td>" + mensajesa[x].FECHA + "</td><td> <a href='MonitoreoUsuarios.aspx?" + mensajesa[x].USUARIO + "'><img border='0' alt='Auditar Ubicación' src='Images/ImgAudit.png' /></a></td></tr>"
                    }
                    document.getElementById('RowsAlertas').innerHTML = mensajesa2 + "</table>"
                }
            }

            Asistencia = msg => {
                if (msg.d == 'No') {
                    document.getElementById('Ingreso').innerHTML = "<p>" + " " + "</p>"
                }
                else {
                    var mensajesa2 = "<h4>Asistencia</h4><div class='table-responsive'><table class='table table-sm'><thead class='thead-inverse'><tr><th>Usuario</th><th>Entrada</th><th>Salida</th></thead>";
                    var mensajesa = JSON.parse(msg.d);
                    for (x = 0; x < mensajesa.length; x++) {
                        mensajesa2 = mensajesa2 + "<tr><td>" + mensajesa[x].USUARIO + "</td><td>" + mensajesa[x].ENTRADA + "</td><td>" + mensajesa[x].SALIDA + "</td></tr>"
                    }
                    document.getElementById('Ingreso').innerHTML = mensajesa2 + "</table>"
                }
            }

            SendAjax = (bandera, funcion) => {
                $.ajax({
                    type: "POST",
                    url: "Inicio.aspx/Cargar",
                    data: "{'V_Bandera':'" + bandera + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    success: function (msg) { funcion(msg) }
                });
            }

            llenar = () => {
                SendAjax('0', Avisos)
                SendAjax('1', Alertas)
                SendAjax('5', Asistencia)
            };

            llenar();

            setTimeout(llenar(), 10000);

        </script>
    </telerik:RadScriptBlock>
    <div class="container h-100">
        <asp:Panel ID="PnlInfo" runat="server" Visible="false">

            <div class="w3-row-padding">
                <div class="w3-col m3 w3-hide-small">
                    <asp:Image runat="server" AlternateText="Cliente" ImageUrl="~/Imagenes/ImgLogo_Cliente.png" class="img-fluid" Style="max-height: 50px;" />
                </div>
                <div class="w3-col s12 m3">
                    <label>Desde:</label><br />
                    <telerik:RadDatePicker ID="TxtFechaI" runat="server" Width="100%"></telerik:RadDatePicker>
                </div>
                <div class="w3-col s12 m3">
                    <label>Hasta:</label><br />
                    <telerik:RadDatePicker ID="TxtFechaF" runat="server" Width="100%"></telerik:RadDatePicker>
                </div>
                <div class="w3-col s12 m3">
                    <telerik:RadButton ID="RadBtnGenerar" runat="server" Text="Obtener Información" CssClass="bg-primary border-0 text-white mt-2"></telerik:RadButton>
                </div>
            </div>
        </asp:Panel>
        <br />
        <div style="max-width: 100%; overflow: auto">
            <span id="Rows"></span>
            <span id="RowsAlertas"></span>
            <span id="Ingreso"></span>
        </div>
    </div>
    <telerik:RadWindowManager ID="RamiWa" runat="server" EnableShadow="true"
        Animation="Resize" Modal="True"
        VisibleTitlebar="False" ShowContentDuringLoad="false">
        <Localization OK="Aceptar" />
    </telerik:RadWindowManager>
    <asp:HiddenField ID="HidenUrs" runat="server" />
</asp:Content>

