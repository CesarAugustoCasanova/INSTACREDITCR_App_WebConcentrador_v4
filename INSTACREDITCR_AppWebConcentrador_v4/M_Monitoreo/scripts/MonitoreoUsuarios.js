//-----------------------------------------------------------------------
//--------------------------------- OJO ---------------------------------
// CUALQUIER CAMBIO EN ESTE ARCHIVO NO SE VERÁ EN LA PAGINA
// HAY QUE MINIFICAR ESTE ARCHIVO Y PONERLO EN MONITOREOUSUARIOS.MIN.JS
//-----------------------------------------------------------------------

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
    $("#Monitoreo").removeClass("w3-hide");
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
var timerMonitoreo = 0;
var RutaDeUsuario

var symbolThree = {
    path: 'M-2,2 L0,-8 L2,2',
    strokeColor: '#000',
    strokeWeight: 0.5
};
function DibujaRuta(Info) {
    let puntos = [];
    let bounds = new google.maps.LatLngBounds();
    Info.forEach(punto => {
        let lat = parseFloat(punto.Latitud), lng = parseFloat(punto.Longitud);
        puntos.push({ lat: lat, lng: lng })
        bounds.extend(new google.maps.LatLng(lat, lng));
    });
    try { RutaDeUsuario.setMap(null); } catch (err) { }
    RutaDeUsuario = new google.maps.Polyline({
        path: puntos,
        icons: [{
            icon: symbolThree,
            offset: '50%'
        }],
        strokeWeight: 1,
        map: map
    });
    map.fitBounds(bounds);
}

function Obtener() {
    try {
        if (typeof map === 'undefined') {
            initMap();
        }
        try { RutaDeUsuario.setMap(null); } catch (err) { }
        hideInfoUser()
        //alert("Hola1")
        //alert(lista)
        var usr = document.getElementById("ContentPlaceHolder1_HidenUrs").value
        //alert("usr" + usr)
        $.ajax({
            type: "POST",
            url: "MonitoreoUsuarios.aspx/ObtenerMonitoreo",
            data: "{'V_Usuario':'" + RcbCat_lo_usuario.Get_CheckedValues() + "','V_solicitante':'" + usr + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                OnSuccess(msg.d)
            },
            failure: function (msg) {
                ShowPopup(msg);

            },
            error: function (msg) {
                ShowPopup(msg);

            }
        });
    }
    catch (err) {
        //alert( err.message);
    }
}

function Obtener3() {
    try {
        if (typeof map === 'undefined') {
            initMap();
        }
        if (RcbCat_lo_usuario.get_checkedItems.length > 1) {
            ShowPopup("Las rutas solo se pueden ver para un usuario a la vez");
        }
        hideInfoUser()
        $.ajax({
            type: "POST",
            url: "MonitoreoUsuarios.aspx/ObtenerRuta",
            data: "{'V_Usuario':'" + RcbCat_lo_usuario.Get_CheckedValues() + "','V_Fecha':'" + dtPicker.FormattedDate() + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                OnSuccess3(msg.d)
            },
            failure: function (msg) {
                ShowPopup(msg);

            },
            error: function (msg) {
                ShowPopup(msg);

            }
        });
    }
    catch (err) {
        //alert( err.message);
    }
}

var accion;
function addMarker(Info) {
    var bounds = new google.maps.LatLngBounds();
    var ColorM;
    var lenInfo = Info.length;
    var content = ""
    Info.forEach(item => {
        switch (item.Accion) {
            case 0:
                //Verde
                ColorM = './Images/Esperando_gestion.png';
                accion = 'Esperando Gestion';
                break;
            case 1:
                //Amarillo
                ColorM = './Images/Salida.png';
                accion = "Salida";
                break;
            case 2:
                //Amarillo
                ColorM = './Images/Comida.png';
                accion = "Pausa Comida";
                break;
            case 3:
                //Rojo
                ColorM = './Images/Banio.png';
                accion = "Pausa Descanso";
                break;
            case 4:
                //Rojo
                ColorM = './Images/LLuvia.png';
                accion = "Pausa Lluvia";
                break;
            case 5:
                //Rojo
                ColorM = './Images/Gasolina.png';
                accion = "Pausa Gasolina";
                break;
            case 6:
                //Rojo
                ColorM = './Images/Falla_mecanica.png';
                accion = "Pausa Falla mecanica";
                break;
            case -1:
                //Rojo
                ColorM = './Images/Rumbo_gestionar.png';
                accion = "Rumbo a gestionar";
                break;
            case -2:
                //Rojo
                ColorM = './Images/Gestionando.png';
                accion = "Gestionando";
                break;

        }
        var loc = new google.maps.LatLng(item.Latitud, item.Longitud);
        //alert(loc)
        var showName = ($("#switch1:checked").val() == "on") ? true : false;

        if (!showName) {

            var marker = new google.maps.Marker({
                position: loc,
                map: map,
                icon: ColorM
            });
        }
        else {

            var marker = new google.maps.Marker({
                position: loc,
                map: map,
                icon: ColorM,
                label: {
                    text: item.Usuario +
                        " (Bateria: " + item.Bateria + ")",
                    fontFamily: "custom-label"
                }
            });
        }
        try {
            markers.push(marker);
            bounds.extend(marker.position);
            //map.setCenter(loc);
        }
        catch (err) {
            alert(err.message);
        }
        var credito = item.Credito.split("-")
        //alert(item.Nombre)
        content = "<table class='table table-hover table-bordered shadow'>" +
            `<tr class='bg-primary text-white'><th colspan='2' class='text-center'>Información de ${item.Usuario}</th></tr>` +
            `<tr><td>Último estado</td><td>${accion}</td></tr>` +
            `<tr><td>Velocidad de movimiento</td><td>${item.Velocidad}</td></tr>` +
            `<tr><td>Bateria</td><td>${item.Bateria}</td></tr>` +
            `<tr><td colspan='2' class='text-center bg-info text-white'><b>Última gestión</b></td></tr>` +
            `<tr><td>Fecha</td><td>${item.Fecha}</td></tr>` +
            `<tr><td>Credito</td><td>${credito[0]}</td></tr>` +
            `<tr><td>Resultado</td><td>${item.Resultado}</td></tr>` +
            "</table > "

        google.maps.event.addListener(marker, 'click', (function (marker, content, Usuario) {
            return function () {
                showInfoUser()
                $("#InfoUser").html(content);
                let lastPosition = marker.getPosition();
                map.panTo(lastPosition);
                map.setZoom(18);
                RcbCat_lo_usuario.get_checkedItems().forEach(item => item.set_checked(false))
                RcbCat_lo_usuario.findItemByText(Usuario).set_checked(true);
                dtPicker.set_enabled(true)
            };
        })(marker, content, item.Usuario));
        marker.setMap(map);
    })
    map.fitBounds(bounds);
    //map.setZoom(18);
    if (lenInfo == 1) {
        showInfoUser()
        $("#InfoUser").html(content)
    };

};

function addMarker3(Info) {
    var ColorM;
    var colorL;
    var couenta = 0;
    for (x = 0; x < Info.length; x++) {
        if (Info[x].Resultado != ' ') {

            if (Info[x].Efectivo == 'EFECTIVO') {
                //Verde
                ColorM = './Images/BanderaVerde.png';
                colorL = '#2CCC27';
            }
            else if (Info[x].Efectivo == 'NO EFECTIVO') {
                //Rosa
                ColorM = './Images/BanderaRosa.png';
                colorL = '#C74790';
            }
            else if (Info[x].Efectivo == 'PUNTO') {
                //Rosa
                ColorM = './Images/imgObjetivo.png';
                colorL = '#222222';
            }

            var loc = new google.maps.LatLng(Info[x].Latitud, Info[x].Longitud);
            var marker = new google.maps.Marker({
                position: loc,
                map: map,
                icon: {
                    url: ColorM,
                    labelOrigin: new google.maps.Point(40, 52),
                },
                label: {
                    text: couenta + ' | ' + Info[x].Resultado,
                    color: colorL,

                }
            });
            try {
                markers.push(marker);
            }
            catch (err) {
                alert(err.message);
            }

            marker.setMap(map);
            couenta++;
        }

    }

};
function addMarker4(Info, indices) {
    var bounds = new google.maps.LatLngBounds();
    try {
        var M = (info[x].Latitud - info[x + 1].Latitud) / (info[x].Longitud - info[x + 1].Longitud);
        var teta = Math.atan(M);
    }
    catch (ex) { }
    var icono = { // car icon
        path: 'M29.395,0H17.636c-3.117,0-5.643,3.467-5.643,6.584v34.804c0,3.116,2.526,5.644,5.643,5.644h11.759   c3.116,0,5.644-2.527,5.644-5.644V6.584C35.037,3.467,32.511,0,29.395,0z M34.05,14.188v11.665l-2.729,0.351v-4.806L34.05,14.188z    M32.618,10.773c-1.016,3.9-2.219,8.51-2.219,8.51H16.631l-2.222-8.51C14.41,10.773,23.293,7.755,32.618,10.773z M15.741,21.713   v4.492l-2.73-0.349V14.502L15.741,21.713z M13.011,37.938V27.579l2.73,0.343v8.196L13.011,37.938z M14.568,40.882l2.218-3.336   h13.771l2.219,3.336H14.568z M31.321,35.805v-7.872l2.729-0.355v10.048L31.321,35.805',
        scale: 0.4,
        fillColor: "#427af4",
        fillOpacity: 1,
        strokeWeight: 1,
        anchor: new google.maps.Point(0, 5),
        rotation: teta //<-- Car angle
    };//https://codepen.io/andrewmmc/pen/jaNbYV?editors=0010

    var loc = new google.maps.LatLng(Info[indice].Latitud, Info[indice].Longitud);
    var marker = new google.maps.Marker({
        position: loc,
        map: map,
        icon: icono
    });
    try {
        markersC.push(marker);

    }
    catch (err) {
        alert(err.message);
    }
    if (indice < Info.length) {
        marker.setMap(map);
        console.log('Esta ' + indice + ' es ' + (markers.length - 1))
        if (indice == 0) {
            map.setCenter(loc);
        }
        indice++;
    }
    else {
        indice = 0
    }
}
;
function restMarker() {
    var ultimo = markersC[markersC.length - 1];
    ultimo.setMap(null);
    console.log('Se fue ' + (markers.length - 1))
}
function OnSuccess(Informacion) {
    if (Informacion == "0") {
        ShowPopup("No Se Localizo El Dispositivo");
    }
    else {
        var Info = JSON.parse(Informacion);
        LimpiarMarcadores(map);
        addMarker(Info);
        $("[style*='custom-label']").addClass("badge badge-info")
        timerMonitoreo = setTimeout(Obtener, 25000);

    }
};
function OnSuccess2(Informacion) {
    if (Informacion == "No se encontraron cuentas") {
        $('#MdlEspere').modal('hide');
        ShowPopup(Informacion);
    }
    else {
        var Info = JSON.parse(Informacion);
        LimpiarMarcadores(map);
        addMarker2(Info);
    }
};
function OnSuccess3(Informacion) {
    try {
        clearTimeout(timerMonitoreo)
        timerMonitoreo = 0;
    } catch (err) { }
    if (Informacion == "0") {
        ShowPopup("No se encontró monitoreo");
    }
    else {

        var Info = JSON.parse(Informacion);
        LimpiarMarcadores(map);
        DibujaRuta(Info);
        addMarker3(Info);
        indice = 0;
        markersC = [];
        /*var isChecked = ($("#CBSimular:checked").val() == "on") ? true : false;

    if (isChecked) {
        addMarker4(Info, indice);
        setInterval(restMarker, 1000);
        setInterval(addMarker4, 1020, Info, indice);
    } */

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

hideInfoUser = () => {
    if (!$("#InfoUser").hasClass("invisible")) $("#InfoUser").addClass("invisible")

    if ($("#DivMapa").hasClass("col-md-8")) {
        $("#DivMapa").removeClass("col-md-8")
        $("#DivMapa").addClass("col-md-12")
    }
    $("#InfoUser").html("");
}

showInfoUser = () => {
    if ($("#InfoUser").hasClass("invisible")) $("#InfoUser").removeClass("invisible")

    if ($("#DivMapa").hasClass("col-md-12")) {
        $("#DivMapa").removeClass("col-md-12")
        $("#DivMapa").addClass("col-md-8")
    }
}
//-------------------
//EVENTOS
//-------------------

comboCheckedChanged = (sender, args) => {
    let seleccionados = RcbCat_lo_usuario.get_checkedItems().length
    if (seleccionados == 0) {
        /*No se puede hacer monitoreo ni mostrar ruta*/
        dtPicker.set_enabled(false)
        btnObtener.set_enabled(false)
    }
    else if (seleccionados == 1) {
        /*Se puede hacer monitoreo o mostrar ruta*/
        dtPicker.set_enabled(true)
        btnObtener.set_enabled(true)
    }
    else {
        /*Se puede hacer monitoreo pero no mostrar ruta*/
        btnObtener.set_enabled(true)
        dtPicker.set_enabled(false)
        btnRuta.set_enabled(false)
    }
}

selectedDate = (sender, args) => {
    if (dtPicker.isEmpty()) btnRuta.set_enabled(false)
    else btnRuta.set_enabled(true)
}