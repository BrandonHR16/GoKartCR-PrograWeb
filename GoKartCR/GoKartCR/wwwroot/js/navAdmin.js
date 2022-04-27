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


    $(".btnEditarPaquete").click(function (e) {
        e.preventDefault();
        let row = $(this).closest('tr');
        let id = row.find('td:eq(0)').text();
        let nombre = row.find('td:eq(1)').text();
        let desc = row.find('td:eq(2)').text();
        let costo = row.find('td:eq(3)').text();
        let to = row.find('td:eq(4)').text();
        let cantusers = row.find('td:eq(5)').text();
        let pista = row.find('td:eq(7)').text();
        let gk = row.find('td:eq(8)').text();


        var txtid = document.getElementById('idpaqueteEditar')
        txtid.value = id;
        var txtnombre = document.getElementById('nombreEditar')
        txtnombre.value = nombre;
        var txtdesc = document.getElementById('descripcionEditar')
        txtdesc.value = desc;
        var txtcosto = document.getElementById('costoEditar')
        txtcosto.value = costo;
        var txtto = document.getElementById('tiempoOfrecidoEditar')
        txtto.value = to;
        var txtcantusers = document.getElementById('cantidadUsuariosEditar')
        txtcantusers.value = cantusers;
        var txtpista = document.getElementById('idPistaEditar')
        txtpista.value = pista;
        var txtgk = document.getElementById('idGoKartEditar')
        txtgk.value = gk;


        var myModal = new bootstrap.Modal(document.getElementById('modalEditarPaquete'))
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


    $("#TClientes").DataTable({

        language: {
            url: 'https://cdn.datatables.net/plug-ins/1.10.25/i18n/Spanish.json'
        },
        pageLength: 8,
        "order": [[0, "desc"]],
        dom: 'Bfrtip',

    });


});
  
       
