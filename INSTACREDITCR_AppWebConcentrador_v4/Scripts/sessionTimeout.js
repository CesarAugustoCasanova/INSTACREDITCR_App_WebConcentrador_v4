window.history.forward();

var sesionTimeout = 600;
var sesionTiempo = 600;

$(document).mousemove(() => {
    if (window.sesionTiempo <= 30) $("#ModalSession").modal("hide")
    window.sesionTiempo = window.sesionTimeout;
});

//Cuando mueven el mouse se resetea el tiempo de la sesion
//y si el modal de cuenta atras está abierto, se cierra

setInterval(() => {
    var count = window.sesionTiempo;
    count--;
    if (count == 0) window.location.href = "../SesionExpirada.aspx";
    else if (count <= 30)
        try {
            $('.count').html(count);
            $("#ModalSession").modal();
        } catch (e) { }

    window.sesionTiempo = count
}, 1000)