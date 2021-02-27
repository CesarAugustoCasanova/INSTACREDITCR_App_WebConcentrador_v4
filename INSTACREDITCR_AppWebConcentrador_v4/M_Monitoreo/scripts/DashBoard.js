var poly;
var map, heatmap;
var markers = [];
var Calor = [];
var Creditos = [];
var Colores = [];
var CreditosAsigna = [];
var markersSelected = [];
var listener1;
var cliked0 = true;
var cliked3 = true;
var markerCluster;
var Info;
var mapstatus = 0;

function SelectMarkers(Bounds) {
    CreditosAsigna.length = 0;
    for (var i = 0; i < markers.length; i++) {
        marker = markers[i];
        if (Bounds.contains(marker.getPosition()) == true) {
            marker.setIcon(imageJobSelected)
            if (marker.get('id') == 0)
            { cliked0 = false };
            if (marker.get('id') == 3)
            { cliked3 = false };
            CreditosAsigna.push(Creditos[i]);
        }
    }
};


var Baseiconos = 'https://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=|';
var Icono = {
    1: {
        icon: Baseiconos + 'ImgDotGreen_sm.png',
        color: Baseiconos + "229954"
    },
    2: {
        icon: Baseiconos + 'ImgDotBlue_sm.png',
        color: Baseiconos + "003399"
    },
    3: {
        icon: Baseiconos + 'ImgDotOrange_sm.png',
        color: Baseiconos + "FF8000"
    },
    4: {
        icon: Baseiconos + 'ImgDotRed_sm.png',
        color: Baseiconos + "FF4000"
    },
    5: {
        icon: Baseiconos + 'ImgDotYellow_sm.png',
        color: Baseiconos + "ffee51"
    },
    6: {
        icon: Baseiconos + 'ImgDotGrey_sm.png',
        color: Baseiconos + "BDBDBD"
    },
    7: {
        icon: Baseiconos + 'ImgDotBlack_sm.png',
        color: Baseiconos + "000000"
    },
    8: {
        icon: Baseiconos + 'ImgDotPurple_sm.png',
        color: Baseiconos + "8c2e7f"
    },
    9: {
        icon: Baseiconos + 'ImgDotPurple_sm.png',
        color: Baseiconos + "f49ac2"
    }


};


var imageJobSelected = Icono[9].color;
function initMap() {
    mapstatus = 1;
    map = new google.maps.Map(document.getElementById('map'), {
        zoom: 5,
        center: { lat: 19.508194, lng: -99.143493 },
        mapTypeId: google.maps.MapTypeId.ROADMAP
    });
    map.enableKeyDragZoom();

    var menu = new contextMenuMio({ map: map });
    menu.addItem('Asignar', function (map, latLng) {
        if (CreditosAsigna.length != 0) {
            $('#MdlAsignar').modal({ show: true });
        }
        else {
            ShowPopup("No Ha Seleccionado Ningun Crédito Para Asignar");
        };
    });
    menu.addSep();
    menu.addItem('Limpiar Selección', function (map, latLng) {
        $('#MdlEspere').modal({ backdrop: 'static', keyboard: false, show: true });
        Obtener()
        $('#MdlEspere').modal('hide');
        ShowPopup("Se Ha Eliminado La SEleccion De Los Créditos");
    });
   
            
};

function Asignar(Credito) {
    $('#MdlEspere').modal({ backdrop: 'static', keyboard: false, show: true });

    if (document.getElementById('Usuario' + Credito).value == null) {
        ShowPopup("Escriba El Id Del Usuario Al Que Se Asignara La Cuenta")
    }
    else {
        $.ajax({
            type: "POST",
            url: "DashBoardVisitas.aspx/AsignarManual",
            data: "{'V_Usuario':'" + document.getElementById('Usuario' + Credito).value + "','V_Credito':'" + Credito + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {

                $('#MdlEspere').modal('hide')
                EnviarMSJPush("Se Ha Asignado El Crédito " + Credito, document.getElementById('Usuario' + Credito).value);
                if (msg.d == "Credito Asignado") {
                    document.getElementById('Usuario' + Credito).value = ""
                };
            },
            failure: function (msg) {
                $('#MdlEspere').modal('hide')
                ShowPopup(msg.d);
                document.getElementById('Tablas').innerHTML = "";
            },
            error: function (msg) {
                $('#MdlEspere').modal('hide')
                ShowPopup(msg.d);
                document.getElementById('Tablas').innerHTML = "";
            }
        });

    };
};
var cuantosSi = 0;
var cuantosNo = 0;
function AsignarMasivo() {
    $('#MdlAsignar').modal('hide');
    $('#MdlEspere').modal({ backdrop: 'static', keyboard: false, show: true });
    for (var i = 0; i < CreditosAsigna.length; i++) {
        $.ajax({
            type: "POST",
            url: "DashBoardVisitas.aspx/AsignarManual",
            data: "{'V_Usuario':'" + document.getElementById('RcbCat_lo_usuario').value + "','V_Credito':'" + CreditosAsigna[i] + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                cuantosSi = cuantosSi++;
            },
            failure: function (msg) {
                cuantosNo = cuantosNo++;
            },
            error: function (msg) {
                cuantosNo = cuantosNo++;
            }
        });
    };
    $('#MdlEspere').modal('hide');
  
    EnviarMSJPush("Se Han Asignado " + CreditosAsigna.length + " Créditos", document.getElementById('RcbCat_lo_usuario').value);
};


function EnviarMSJPush(V_mensaje, V_usr) {
    $.ajax({
        type: "POST",
        url: "DashBoardVisitas.aspx/PushUsr",
        data: "{'V_usuario':'" + V_usr + "','V_Msj':'" + V_mensaje + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            ShowPopup("Créditos Asignados");
        },
        failure: function (msg) {
            ShowPopup(msg.d);
        },
        error: function (msg) {
            ShowPopup(msg.d);
        }
    });
};


function Obtener() {
    $('#MdlEspere').modal({ backdrop: 'static', keyboard: false, show: true });
    LimpiarMarcadores(map);
    var ListaS = ObtenSucursal();
    var ListaP = ObtenProducto();
    $.ajax({
        type: "POST",
        url: "DashBoardVisitas.aspx/ObtenerInfo",
        data: "{'V_Sucursal':'" + ListaS + "','V_Producto':'" + ListaP + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            initMap();
            Info = JSON.parse(msg.d);
            OnSuccess(Info)
        },
        failure: function (msg) {
            $('#MdlEspere').modal('hide')
            ShowPopup(msg.d);
        },
        error: function (msg) {
            $('#MdlEspere').modal('hide')
            ShowPopup(msg.d);
        }
    });
};
var V_Ubicacion;
var content;

function OnSuccess(Info) {
    try {
        for (x = 0; x < Info.length; x++) {
            var ColorM;
            if (document.getElementById('ddcTipo').value == "Montos") {
                if (Info[x].Saldo2 <= 20000) {
                    ColorM = 1;
                }
                else if (Info[x].Saldo2 > 20000 && Info[x].Saldo < 50000) {
                    ColorM = 5;
                }
                else if (Info[x].Saldo2 >= 50000 && Info[x].Saldo < 100000) {
                    ColorM = 4;
                }
                else if (Info[x].Saldo2 >= 100000 && Info[x].Saldo < 250000) {
                    ColorM = 7;
                }
                else if (Info[x].Saldo2 >= 250000) {
                    ColorM = 8;
                }
            }
            else if (document.getElementById('ddcTipo').value == "Visitado/No Visitado") {
                if (Info[x].Estatus == 0) {
                    //Verde
                    ColorM = 1;
                }
                else if (Info[x].Estatus == 1) {
                    //Amarillo
                    ColorM = 5;
                }
                else if (Info[x].Estatus == 2) {
                    //Rojo
                    ColorM = 4;
                }
            }


            var latLng = new google.maps.LatLng(Info[x].Latitud, Info[x].Longitud);

            var marker = new google.maps.Marker({
                position: latLng,
                animation: google.maps.Animation.DROP,
                map: map,
                icon: Icono[ColorM].color,
            });

            Calor.push(latLng);
            content = "<table><tr><td class='Titulo' colspan='2'>Información Del Credito</td></tr><tr><td>Credito</td><td>" + Info[x].Credito + "</td></tr><tr><td>Producto</td><td>" + Info[x].Producto + "</td></tr><tr><td>Saldo</td><td>" + Info[x].Saldo + "</td></tr><tr><td colspan='2' class='Titulo'>Información Visita</td></tr><tr><td>Visitador Asignado</td><td>" + Info[x].VisitadorAsig + "</td></tr><tr><td>Visitador</td><td>" + Info[x].Visitador + "</td></tr><tr><td>Fecha Visita</td><td>" + Info[x].FechaVisita + "</td></tr><tr><td>Resultado</td><td>" + Info[x].Resultado + "</td></tr><tr><td></td><td></td></tr><tr><td></td><td></td></tr><tr><td><input type='button' value='Asignar Crédito'   onclick='Asignar(\"" + Info[x].Credito + "\")'></td><td><input type='text' id='Usuario" + Info[x].Credito + "'></td></tr></table>";
            var infowindow = new google.maps.InfoWindow()
            google.maps.event.addListener(marker, 'click', (function (marker, content, infowindow) {
                return function () {
                    infowindow.setContent(content);
                    infowindow.open(map, marker);
                };
            })(marker, content, infowindow));
            markers.push(marker);
            Creditos.push(Info[x].Credito);
            Colores.push(ColorM)
        }
        markerCluster = new MarkerClusterer(map, markers, { imagePath: 'Images/m' });
        $('#MdlEspere').modal('hide')
    }
    catch (err) {
        alert(err.message);
    }
};
function OnSuccess2(Info) {
    try {
        for (x = 0; x < Info.length; x++) {
            var ColorM;
            if (document.getElementById('ddcTipo').value == "Montos") {
                if (Info[x].Saldo2 <= 20000) {
                    ColorM = 1;
                }
                else if (Info[x].Saldo2 > 20000 && Info[x].Saldo < 50000) {
                    ColorM = 5;
                }
                else if (Info[x].Saldo2 >= 50000 && Info[x].Saldo < 100000) {
                    ColorM = 4;
                }
                else if (Info[x].Saldo2 >= 100000 && Info[x].Saldo < 250000) {
                    ColorM = 7;
                }
                else if (Info[x].Saldo2 >= 250000) {
                    ColorM = 8;
                }
            }
            else if (document.getElementById('ddcTipo').value == "Visitado/No Visitado") {
                if (Info[x].Estatus == 0) {
                    //Verde
                    ColorM = 1;
                }
                else if (Info[x].Estatus == 1) {
                    //Amarillo
                    ColorM = 5;
                }
                else if (Info[x].Estatus == 2) {
                    //Rojo
                    ColorM = 4;
                }
            }


            var latLng = new google.maps.LatLng(Info[x].Latitud, Info[x].Longitud);

            var marker = new google.maps.Marker({
                position: latLng,
                animation: google.maps.Animation.DROP,
                map: map,
                icon: Icono[ColorM].color,
            });

            Calor.push(latLng);
            content = "<table><tr><td class='Titulo' colspan='2'>Información Del Credito</td></tr><tr><td>Credito</td><td>" + Info[x].Credito + "</td></tr><tr><td>Producto</td><td>" + Info[x].Producto + "</td></tr><tr><td>Saldo</td><td>" + Info[x].Saldo + "</td></tr><tr><td colspan='2' class='Titulo'>Información Visita</td></tr><tr><td>Visitador Asignado</td><td>" + Info[x].VisitadorAsig + "</td></tr><tr><td>Visitador</td><td>" + Info[x].Visitador + "</td></tr><tr><td>Fecha Visita</td><td>" + Info[x].FechaVisita + "</td></tr><tr><td>Resultado</td><td>" + Info[x].Resultado + "</td></tr><tr><td></td><td></td></tr><tr><td></td><td></td></tr><tr><td><input type='button' value='Asignar Crédito'   onclick='Asignar(\"" + Info[x].Credito + "\")'></td><td><input type='text' id='Usuario" + Info[x].Credito + "'></td></tr></table>";
            var infowindow = new google.maps.InfoWindow()
            google.maps.event.addListener(marker, 'click', (function (marker, content, infowindow) {
                return function () {
                    infowindow.setContent(content);
                    infowindow.open(map, marker);
                };
            })(marker, content, infowindow));
            markers.push(marker);
            Creditos.push(Info[x].Credito);
            Colores.push(ColorM)
        }
             
        $('#MdlEspere').modal('hide')
    }
    catch (err) {
        alert(err.message);
    }
};
function LimpiarMarcadores(map) {
    for (var i = 0; i < markers.length; i++) {
        markers[i].setMap(null);
    }
    markers.length = 0
    Creditos.length = 0
};

function ShowPopup(message) {
    $('#MdlMsj').modal({ show: true });
    document.getElementById('LblMsj').innerHTML = message;
};


function ObtenSucursal() {
    var combo = $find("<% ddcSucursal.ClientID %>");
    var selectedItemsValues = "";
    var items = combo.get_items();
    var itemsCount = items.get_count();

    for (var itemIndex = 0; itemIndex < itemsCount; itemIndex++) {
        var item = items.getItem(itemIndex);
        var checkbox = getItemCheckBox(item);
        if (checkbox.checked) {
            selectedItemsValues += item.get_value() + ",";
        }
    }
    selectedItemsValues = selectedItemsValues.substring(0, selectedItemsValues.length - 1);
    console.log('after loop' + selectedItemsValues);
    if (selectedItemsValues == '') {
        selectedItemsValues = '-1';
    }
    return selectedItemsValues;
};
function ObtenProducto() {
    var combo = $find("<%= ddcProducto.ClientID %>");
    var selectedItemsValues = "";
    var items = combo.get_items();
    var itemsCount = items.get_count();

    for (var itemIndex = 0; itemIndex < itemsCount; itemIndex++) {
        var item = items.getItem(itemIndex);
        var checkbox = getItemCheckBox(item);
        if (checkbox.checked) {
            selectedItemsValues += item.get_value() + ",";
        }
    }
    selectedItemsValues = selectedItemsValues.substring(0, selectedItemsValues.length - 1);
    console.log('after loop' + selectedItemsValues);
    if (selectedItemsValues == '') {
        selectedItemsValues = '-1';
    }
    return selectedItemsValues;
};
function getItemCheckBox(item) {
    var itemDiv = item.get_element();
    var inputs = itemDiv.getElementsByTagName("input");
    for (var inputIndex = 0; inputIndex < inputs.length; inputIndex++) {
        var input = inputs[inputIndex];
        if (input.type == "checkbox") {
            return input;
        }
    }
};
