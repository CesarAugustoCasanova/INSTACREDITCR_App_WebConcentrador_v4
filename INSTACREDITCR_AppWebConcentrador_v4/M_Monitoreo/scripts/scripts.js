
function Valida(sender, args) {
    var texto = document.getElementById("RdbtnValida").value;
    var checklist = document.getElementById("checklistWindow").control;
    var comentario = document.getElementById("comentarioWindow").control;
    if (texto == "Datos correctos") {
        comentario.hide();
        checklist.show();
    } else {
        comentario.show();
    }
};

function Aprueba(sender, args) {  
    var checklist = document.getElementById("checklistWindow").control;
    checklist.show();
};

function Rechaza(sender, args) {
    var comentario = document.getElementById("comentarioWindow").control;
    comentario.show();
};

function Sube(sender, args) {
    document.getElementById("Imagenes_BtnUpload").click();
}

//window.addEventListener("click", function () {
//    alert((event.target).name);
//});

function AlphabetNumbers(sender, eventArgs) {
    var c = eventArgs.get_keyCode();
    //alert(c);
    if ((c < 48) || (c > 57 && c < 65) || (c > 90 && c < 97) || (c > 122))
        eventArgs.set_cancel(true);
};

function Alphabet(sender, eventArgs) {
    var c = eventArgs.get_keyCode();
    //alert(c);
    if ((c < 32) || (c > 32 && c < 48) || (c > 57 && c < 65) || (c > 90 && c < 97) || (c > 122))
        eventArgs.set_cancel(true);
};
function NumbersOnly(sender, eventArgs) {
    var c = eventArgs.get_keyCode();
    //alert(c);
    if ((c < 48) || (c > 57))
        eventArgs.set_cancel(true);
};
