var seleccionado = -1,
    partTimeSeleccionado;

function seleccionar(idEmpleado) {
    var rowEmpleado;
    if (seleccionado != -1) {
        rowEmpleado = ".row-empleado-" + seleccionado;
        $(rowEmpleado).css({ "background-color": "transparent" });
    } else if (seleccionado == idEmpleado) {
        seleccionado = -1;
        rowEmpleado = ".row-empleado-" + seleccionado;
        $(rowEmpleado).css({ "background-color": "transparent" });
    }
    seleccionado = idEmpleado;
    rowEmpleado = ".row-empleado-" + idEmpleado;
    $(rowEmpleado).css({"background-color" : "#dff0d8"});
}

function agregarEmpleado() {
    var datos = {
        "nombre": $("#name").val(),
        "mail": $("#email").val() ,
        "tipoEmpleado": $('input[name=tipoEmpleado]:checked').val(),
        "salario": $("#salarioVal").val()
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
         "nombre" : $("#nombreModif").val(),
         "mail": $("#mailModif").val(),
         "id" : seleccionado,
         "tipoEmpleado": $('input[name=tipoEmpleadoModif]:checked').val(),
         "salario" : $("#salarioModif").val()
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
    $("#modificarEmpleado").modal("hide");
    $("#notificaciones").modal();
    setTimeout(function () {
        $("#notificaciones").modal("hide");
        document.location.reload();
    }, 2000);
}

function errorModificarEmpleado() {
    document.getElementById("bodyNotificaciones").innerHTML = "Error al modificar al Empleado";
    $("#notificaciones").modal();
}

function respuestaAgregarEmpleado() {
    document.getElementById("bodyNotificaciones").innerHTML = "Se ha agregado al Empleado correctamente";
    $("#agregarEmpleado").modal("hide");
    $("#notificaciones").modal();
    setTimeout(function () {
        $("#notificaciones").modal("hide");
        document.location.reload();
    }, 2000);
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
    $("#borrarEmpleado").modal("hide");
    $("#notificaciones").modal();
    setTimeout(function () {
        $("#notificaciones").modal("hide");
        document.location.reload();
    }, 2000);
}

function errorBorrarEmpleado() {
    document.getElementById("bodyNotificaciones").innerHTML = "Error al borrar al Empleado";
    $("#notificaciones").modal();
}

function Login() {
    var datos = {
        "mail": $("#mail").val(),
        "password": $("#pass").val()
    };

    $.ajax({
        type: "POST",
        url: "/Account/Login",
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
                respuestaLoginFail();     
        },
        error: function (data) {
            document.location.reload();
        }
    });
}

function respuestaLoginFail() {
    document.getElementById("bodyNotificaciones").innerHTML = "Usuario o contraseña incorrectos";
    $("#Login").modal("hide");
    $("#notificaciones").modal();
    setTimeout(function () {
        $("#notificaciones").modal("hide");
        $("#Login").modal();
        //document.location.reload();
    }, 2000);
}

function respuestaLoginOk() {
    $("#Login").modal("hide");
    setTimeout(function () {
        document.location.reload();
    }, 1000);
}


function chequearSeleccionSalario() {
    if (seleccionado != -1) {
        console.log(seleccionado);
        $.ajax({
            type: "GET",
            url: "/Employees/CalcPartTime/"+ seleccionado,
            success: function(response) {
                if (response.partTime) {
                    console.log(response.hourlyRate);
                    partTimeSeleccionado = response.hourlyRate;
                    $("#resultadoPartTime").modal("hide");
                    document.getElementById("horas").value = "";
                    $("#CalcPartTime").modal();
                }
                else {
                    document.getElementById("bodyNotificaciones").innerHTML = "El empleado seleccionado no es Part Time";
                    $("#notificaciones").modal();
                }
            },
            error: function (error) {
                document.getElementById("bodyNotificaciones").innerHTML = "No se pudo recuperar el empleado.";
                $("#notificaciones").modal();
            }
        });
    }
}

function calcularSalario() {
    var horasTrabajadas = document.getElementById("horas").value;
    var salario = horasTrabajadas * partTimeSeleccionado;
    $("#resultadoPartTime").html("Salario: " + salario);
    $("#resultadoPartTime").show();
}

function Logout() {
    $.ajax({
        type: "POST",
        url: "/Account/Logout",
        success: function () {
            setTimeout(function () {
                document.location.reload();
            }, 1000);
        },
        error: function (error) {
        }
    });

}