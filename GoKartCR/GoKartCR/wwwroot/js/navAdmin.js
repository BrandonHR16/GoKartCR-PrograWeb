$(document).ready(function(){
   

    $(".btnEnviar").click(function (e) {
        e.preventDefault();
        let row = $(this).closest('tr');
        let nombre = row.find('td:eq(1)').text();
        let correo = row.find('td:eq(2)').text();


        var txtCorreo = document.getElementById('correoModal')
        txtCorreo.value = correo;
        var txtNombre = document.getElementById('MensajeModal')
        txtNombre.value = 'Estimado ' + nombre+',';
        $("#nombreModal").val(nombre);
        var myModal = new bootstrap.Modal(document.getElementById('modalMensaje'))
        myModal.show()

    });

    $("#TablaPreguntas").DataTable({

        language: {
            url: 'https://cdn.datatables.net/plug-ins/1.10.25/i18n/Spanish.json'
        },
        pageLength: 8,
        "order": [[0, "desc"]],
        dom: 'Bfrtip',
        buttons: [

        ]
    });


});
  
       
