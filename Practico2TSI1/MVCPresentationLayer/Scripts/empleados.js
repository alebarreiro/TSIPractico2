var seleccionado = -1;

function seleccionar(idEmpleado) {
    if (seleccionado != -1) {
        $("#" + seleccionado).removeClass("seleccionado");
    }
    $("#" + idEmpleado).removeClass("table-hover");
    $("#" + idEmpleado).addClass("seleccionado");
    seleccionado = idEmpleado;
}

function agregarEmpleado() {
    var datos = 
        {   nombre : $("#nombre").val() ,
            mail : $("#mail").val() ,
            password : $("#pass").val(),
            tipoEmpleado : $("#tipoEmpleado").val() ,
            salario : $("#salario").val()
        };
    //var parametros = "nombre=" + document.getElementById("nombre").value
    //        + "&mail=" + document.getElementById("mail").value
    //        + "&password=" + document.getElementById("pass").value
    //        + "&tipoEmpleado=" + document.getElementById("tipoEmpleado").value
    //        + "&salario=" + document.getElementById("salario").value;

    $.ajax({
        type: "POST",
        url: "/Employees/AgregarEmpleado",
        data: JSON.stringify(datos),
        async : true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: respuestaAgregarEmpleado,
        error: errorAgregarEmpleado
    });
    $("#agregarEmpleado").modal("hide");
}

function modificarEmpleado() {
    var datos =
        {
            nombre: $("#nombreModif").val(),
            mail: $("#mailModif").val(),
            //password: $("#passModif").val(),
            tipoEmpleado: $("#tipoEmpleadoModif").val(),
            salario: $("#salarioModif").val()
        };

    $.ajax({
        type: "POST",
        url: "~/Employees/ModificarEmpleado",
        data: JSON.stringify(datos),
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: respuestaModificarEmpleado,
        error: errorModificarEmpleado
    });
    $("#modificarEmpleado").modal("hide");
}

function respuestaModificarEmpleado(jqXHR, textStatus, errorThrown) {
    document.getElementById("bodyNotificaciones").innerHTML = "Se ha modificado al Empleado correctamente";
    $("#cerrarNotificaciones").hide();
    $("#notificaciones").modal();
    setTimeout(function (){
            $("#notificaciones").modal("hide");
            location.reload();
    }, 2000);
}

function errorModificarEmpleado(jqXHR, textStatus, errorThrown) {
    document.getElementById("bodyNotificaciones").innerHTML = "Error al modificar al Empleado";
    $("#notificaciones").modal();
}

function respuestaAgregarEmpleado(jqXHR, textStatus, errorThrown) {
    document.getElementById("bodyNotificaciones").innerHTML = "Se ha agregado al Empleado correctamente";
    $("#cerrarNotificaciones").hide();
    $("#notificaciones").modal();
    setTimeout(function () {
        $("#notificaciones").modal("hide");
        location.reload();
    }, 2000);
}

function errorAgregarEmpleado(jqXHR, textStatus, errorThrown) {
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
    var datos = 
        { idEmpleado : seleccionado };

    $.ajax({
        type: "POST",
        url: "~/Employees/BorrarEmpleado",
        data: JSON.stringify(datos),
        async : true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: respuestaBorrarEmpleado,
        error: errorBorrarEmpleado
    });
    $("#borrarEmpleado").modal("hide");
}

function respuestaBorrarEmpleado(jqXHR, textStatus, errorThrown) {
    document.getElementById("bodyNotificaciones").innerHTML = "Se ha borrado al Empleado correctamente";
    $("#cerrarNotificaciones").hide();
    $("#notificaciones").modal();
    setTimeout(function () {
        $("#notificaciones").modal("hide");
        location.reload();
    }, 2000);
}

function errorBorrarEmpleado(jqXHR, textStatus, errorThrown) {
    document.getElementById("bodyNotificaciones").innerHTML = "Error al borrar al Empleado";
    $("#notificaciones").modal();
}