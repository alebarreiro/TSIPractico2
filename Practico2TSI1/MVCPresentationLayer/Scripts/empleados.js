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

function Login() {
    var datos = {
        "mail": document.getElementById("user").value,
        "password": document.getElementById("pass").value
    };

    $.ajax({
        type: "POST",
        url: "/Account/Login",
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            //var pd = $.parseJSON(data);
            alert("asd");
            if (parseInt(data["ErrorCode"]) == -1) {
                //usuario o pass incorrecto
            } else if (data["success"] == "Valid") {
                alert(data["ErrorMessage"]);
            }
            
        },
        error: function (data) {

        }
    });

    var datos2 ={
                    
                };
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
                    $("#resultadoPartTime").hide();
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