// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


//usando jquery 
//Cuando el boton cosulta es precionado
//Recoja el variable del campo fecha
$('.btn-evio').click(function(){
    //js para buscar el valor del campo fecha
    var fecha = $('#Fecha').val();
    //Hacer una ajax consulta a al contolador home con el metodo get y el parametro fecha 
    $.ajax({
        url:  '/Home/consultarReservas',
        type: 'GET',
        data: {time: fecha},
        success: function(data){
            //deshabilitar
            //si data.mannna es true 
            //habilitar todos los botones
            $("#mannana").prop('disabled', false);
            $("#tarde").prop('disabled', false);
            $("#noche").prop('disabled', false);

            if(!data[0].mannana){
                //habilitar
                $('#mannana').prop('disabled', false);
            }
            else{
                //deshabilitar
                $('#mannana').prop('disabled', true);
            }
            //si data.tarde es true
            if(!data[1].tarde){
                //habilitar
                $('#tarde').prop('disabled', false);
            }
            else{
                //habilitar
                $('#tarde').prop('disabled', true);
            }
            //si data.noche es true
            if(!data[0].noche){
                //habilitar
                $('#noche').prop('disabled', false);
            }else{
                //habilitar
                $('#noche').prop('disabled', true);
            }
        }
    });
});



