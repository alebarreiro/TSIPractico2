var seleccionado = -1;

function seleccionar(idEmpleado) {
    seleccionado = idEmpleado;
}

function agregarEmpleado() {
    var datos = {
        "nombre": document.getElementById("name").value,
        "mail": document.getElementById("email").value ,
        "tipoEmpleado": $('input[name=tipoEmpleado]:checked').val(),
        "salario": document.getElementById("salarioVal").value
    };

    $.ajax({
        type: "POST",
        url: "/Employees/AgregarEmpleado",
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: respuestaAgregarEmpleado,
        error: errorAgregarEmpleado
    });
}

function modificarEmpleado() {
    var datos = {
         "nombre" : document.getElementById("nombreModif").value,
         "mail": document.getElementById("mailModif").value,
         "tipoEmpleado": $('input[name=tipoEmpleadoModif]:checked').val(),
         "salario" : document.getElementById("salarioModif").value
    };

    $.ajax({
        type: "POST",
        url: "/Employees/ModificarEmpleado",
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: respuestaModificarEmpleado,
        error: errorModificarEmpleado
    });
}

function respuestaModificarEmpleado() {
    document.getElementById("bodyNotificaciones").innerHTML = "Se ha modificado al Empleado correctamente";
    $("#notificaciones").modal();
}

function errorModificarEmpleado() {
    document.getElementById("bodyNotificaciones").innerHTML = "Error al modificar al Empleado";
    $("#notificaciones").modal();
}

function respuestaAgregarEmpleado() {
    document.getElementById("bodyNotificaciones").innerHTML = "Se ha agregado al Empleado correctamente";
    $("#notificaciones").modal();
}

function errorAgregarEmpleado() {
    document.getElementById("bodyNotificaciones").innerHTML = "Error al agregar al Empleado";
    $("#notificaciones").modal();
}

function chequearSeleccionModificar() {
    if (seleccionado == -1) {
        document.getElementById("bodyNotificaciones").innerHTML = "Debe seleccionar un Empleado para poder Modificarlo";
        $("#notificaciones").modal();
    }
    else {
        $("#modificarEmpleado").modal();
    }
}

function chequearSeleccionBorrar() {
    if (seleccionado == -1) {
        document.getElementById("bodyNotificaciones").innerHTML = "Debe seleccionar un Empleado para poder Borrarlo";
        $("#notificaciones").modal();
    }
    else {
        $("#borrarEmpleado").modal();
    }
}

function borrarEmpleado() {
    var datos = {
         "idEmpleado" : seleccionado 
    };

    $.ajax({
        type: "POST",
        url: "/Employees/BorrarEmpleado",
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: respuestaBorrarEmpleado,
        error: errorBorrarEmpleado
    });
}

function respuestaBorrarEmpleado() {
    document.getElementById("bodyNotificaciones").innerHTML = "Se ha borrado al Empleado correctamente";
    $("#notificaciones").modal();
}

function errorBorrarEmpleado() {
    document.getElementById("bodyNotificaciones").innerHTML = "Error al borrar al Empleado";
    $("#notificaciones").modal();
}