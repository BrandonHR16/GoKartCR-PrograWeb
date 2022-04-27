// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
var idpaquete;

//cuando la pagian carga
$(document).ready(function () {
    $('#Fecha').datepicker();


    //uncheck
    var Cbox = document.getElementById("mannana")  
    var Cbox2 = document.getElementById("tarde")
    var Cbox3 = document.getElementById("noche")
    Cbox.checked = false;
    Cbox2.checked = false;
    Cbox3.checked = false;
    



    $('#Fecha').datepicker();
    $('.paqueteid').click(function (e) {
        //obtener el id del boton
        idpaquete = $('.paqueteid').attr('id');
    })


    $('.btn-evio').click(function () {
        
        var fecha = $('#Fecha').val();

        $.ajax({
            url: '/Home/consultarReservas',
            type: 'GET',
            data: {
                time: fecha,
                id: idpaquete
            },
            success: function (data) {
                $("#mannana").prop('disabled', false);
                $("#tarde").prop('disabled', false);
                $("#noche").prop('disabled', false);
                var Cbox = document.getElementById("mannana")  
                var Cbox2 = document.getElementById("tarde")
                var Cbox3 = document.getElementById("noche")
                Cbox.checked = false;
                Cbox2.checked = false;
                Cbox3.checked = false;

                for (var i = 0; i < data.length; i++) {
                    if (data[i].mannana) {
                        $("#mannana").prop('disabled', true);
                    }
                    if (data[i].tarde) {
                        $("#tarde").prop('disabled', true);
                    }
                    if (data[i].noche) {
                        $("#noche").prop('disabled', true);
                    }
                }
            }
        });
    });

    $('.btnreservar').click(function () {
        // int idPaquete, int idUsuario, string fecha, bool mannana, bool tarde, bool noche
        var fecha = $('#Fecha').val();
        var mannana = $('#mannana').is(':checked');
        var tarde = $('#tarde').is(':checked');
        var noche = $('#noche').is(':checked');
        idpaquete = $('.paqueteid').attr('id');

        $.ajax({
            url: '/Home/CreaReserva',
            type: 'GET',
            data: {
                idPaquete: idpaquete,
                idUsuario: 1,
                fecha: fecha,
                mannana: mannana,
                tarde: tarde,
                noche: noche
            },
            success: function (data) {
                alert('aaaaa');
            }
        });
    }
    );

});
