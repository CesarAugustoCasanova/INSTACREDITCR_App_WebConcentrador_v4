
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

