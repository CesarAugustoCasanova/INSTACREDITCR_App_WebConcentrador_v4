<%@ Page Title="" Language="VB" MasterPageFile="~/M_Monitoreo/MasterPage.master" AutoEventWireup="false" CodeFile="VisualizarAsignacion.aspx.vb" Inherits="M_Monitoreo_VisualizarAsignacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Monitoreo Usuarios</title>
    <link href="styles/General.css" rel="stylesheet" />
    <script type='text/javascript' src='https://maps.googleapis.com/maps/api/js?key=AIzaSyB4XOxMMENmanihA9hOqfPVAcWPR3eUW3I'></script>
    <script type="text/javascript" src="scripts/TelerikCustomFunctions.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>
        var map;
        var markers = [];
        var markersC = [];
        var V_Ubicacion;
        var V_Destino;
        var Colores = [];
        var CreditosAsigna = [];
        var indice = 0;
        var borra = 0;
        function initMap() {
            map = new google.maps.Map(document.getElementById('map'), {
                zoom: 5,
                center: { lat: 19.508194, lng: -99.143493 }
            });
            //markers.push(marker);
        };
        var Baseiconos = 'https://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=|';
        var Icono = {
            0: {
                icon: Baseiconos + 'ImgDotGreen_sm.png',
                color: Baseiconos + "229954"
            },
            1: {
                icon: Baseiconos + 'ImgDotYellow_sm.png',
                color: Baseiconos + "ffee51"
            },
            2: {
                icon: Baseiconos + 'ImgDotRed_sm.png',
                color: Baseiconos + "FF4000"
            },
            3: {
                icon: Baseiconos + 'ImgDotGray_sm.png',
                color: Baseiconos + "afafaf"
            }
        };

        var combo
        var tipo
        var filtro
        var tope
        function Obtener2() {
            try {
                console.log("Obtener2");
                initMap();
                filtro = RcbCat_lo_usuario.get_selectedItem().get_value();
                console.log('ddd ' + filtro)
                combo = ' '
                tope = 'Usuario';
                $('#MdlEspere').modal({ backdrop: 'static', keyboard: false, show: false });
                $.ajax({
                    type: "POST",
                    url: "VisualizarAsignacion.aspx/ObtenerCartera",
                    data: "{'V_Agrupacion':'" + combo + "','V_Tipo':'" + "1" + "','v_Filtro':'" + filtro + "','v_Tipotope':'" + "0" + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        OnSuccess2(msg.d)
                    },
                    failure: function (msg) {
                        $('#MdlEspere').modal('hide')
                        ShowPopup(msg);
                        document.getElementById('Tablas').innerHTML = "";
                    },
                    error: function (msg) {
                        $('#MdlEspere').modal('hide')
                        ShowPopup(msg);
                        document.getElementById('Tablas').innerHTML = "";
                    }
                });
            }
            catch (err) {
                alert(err.message);
            }
        };

        function addMarker2(Info) {
            var ColorM;
            for (x = 0; x < Info.length; x++) {

                if (Info[x].Bloque == 'RA') {
                    //Verde
                    ColorM = './Images/Alto.png';
                }
                else if (Info[x].Bloque == 'RB') {
                    //Amarillo
                    ColorM = './Images/Bajo.png';
                }



                var loc = new google.maps.LatLng(Info[x].Latitud, Info[x].Longitud);
                var marker = new google.maps.Marker({
                    position: loc,
                    map: map,
                    content: content,
                    icon: ColorM
                });
                try {
                    markers.push(marker);
                    //map.setCenter(loc);
                }
                catch (err) {
                    alert(err.message);
                }
                var infowindow = new google.maps.InfoWindow()
                var formatData = (title, value) => {
                    return `<tr><td>${title}</td><td>${value}</td></tr>`
                }
                var content = "<table><tr><td class='Titulo' colspan='2'>CUENTA " + Info[x].Cuenta + " </td></tr>"
                content += formatData("Municipio", Info[x].Resultado)
                content += formatData("Gestor Asignado", Info[x].Gestor)
                content += formatData("CP", Info[x].Asignacion)
                content += "</table >"
                google.maps.event.addListener(marker, 'click', (function (marker, content, infowindow) {
                    return function () {
                        infowindow.setContent(content);
                        infowindow.open(map, marker);
                    };
                })(marker, content, infowindow));
                marker.setMap(map);
            }
        };
        function OnSuccess2(Informacion) {
            if (Informacion == "No se encontraron cuentas") {
                document.getElementById('Tablas').innerHTML = "";
                $('#MdlEspere').modal('hide')
                ShowPopup(Informacion);
            }
            else {
                var Info = JSON.parse(Informacion);
                var cuerpo = " ";
                var v_tiempo = " ";
                LimpiarMarcadores(map);
                addMarker2(Info);
                $('#MdlEspere').modal('hide')
            }
        };
        function LimpiarMarcadores(map) {
            for (var i = 0; i < markers.length; i++) {
                markers[i].setMap(null);
            }
            markers = [];
        };
        function ShowPopup(message) {
            $('#MdlMsj').modal({ show: true });
            document.getElementById('LblMsj').innerHTML = message;
        };
    </script>
    <telerik:RadFormDecorator ID="FormDecorator1" runat="server" DecoratedControls="all" DecorationZoneID="optionsDecoration"></telerik:RadFormDecorator>
    <div class="container" id="optionsDecoration">
        <h3>Visualizacion de Asignacion</h3>
        <fieldset>
            <legend>Configuraci&#243;n</legend>
            
            <div class="row justify-content-center">
                <div class="col-md-6">
                    <asp:Label runat="server" Text="Usuario"></asp:Label><br />
                    <telerik:RadDropDownList DropDownAutoWidth="Enabled" CheckBoxes="false" ID="RcbCat_lo_usuario" runat="server" EmptyMessage="Seleccione Un Usuario" Width="100%" EnableCheckAllItemsCheckBox="false" OnClientItemChecked="comboCheckedChanged">
                    </telerik:RadDropDownList>
                </div>
            </div>

            <div id="PnlBuscar" class="d-flex justify-content-center mt-2">
                <telerik:RadButton ID="RadBtnObtener" runat="server" Text="Mostrar Asignacion" AutoPostBack="false" Width="175px" OnClientClicked="Obtener2" CssClass="bg-success border-0 text-white mx-2">
                </telerik:RadButton>
            </div>

        </fieldset>

    </div>
    <div class="container w3-hide mt-2" id="Monitoreo">
        <div class="row">
            <div class="col-md-4 invisible" id="InfoUser" style="max-height: 600px; overflow: auto">
            </div>
            <div class="col-md-12" id="DivMapa">
                <div id="map" style="height: 600px;" class="w-100 shadow"></div>
            </div>
        </div>

    </div>
    <div class="modal fade" id="MdlEspere" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header w3-center">
                    <h4 class="modal-title">Procesando...</h4>
                </div>
                <div style="text-align: center">
                    <div class="spinner-border text-warning"></div>
                </div>
            </div>
        </div>
    </div>
    <div id="MdlMsj" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Aviso</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <p><span id="LblMsj"></span></p>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="HidenUrs" runat="server" />
    <telerik:RadScriptBlock runat="server" ID="jsBlock">
        <script>
            var RcbCat_lo_usuario
            var dtPicker
            var btnObtener
            var btnRuta

            Sys.Application.add_load(loadPage)

            function loadPage() {
                RcbCat_lo_usuario = $find("<%=RcbCat_lo_usuario.ClientID %>");
                btnObtener = $find("<%=RadBtnObtener.ClientID %>");
            }

        </script>
    </telerik:RadScriptBlock>
    <script type='text/javascript' src='./scripts/MonitoreoUsuarios.min.js?V=1.4'></script>

</asp:Content>
