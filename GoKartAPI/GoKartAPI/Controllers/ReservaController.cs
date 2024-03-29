﻿using GoKartAPI.Entities;
using GoKartAPI.Model;
using Microsoft.AspNetCore.Mvc;
using static GoKartAPI.Entities.Reserva;

namespace GoKartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : Controller
    {
        private readonly IConfiguration configuracion;
        ReservaModel reservamodel = new ReservaModel();

        public ReservaController(IConfiguration configuracion)
        {
            this.configuracion = configuracion;
        }

        //reserva por id
        [HttpGet]
        [Route("ReservaporId")]
        public ReservaRespuesta ReservaporId(int id)
        {
            try
            {
                ReservaRespuesta respuesta = new ReservaRespuesta();
                respuesta.idRespuesta = 0;
                respuesta.mensaje = "!OK";
                respuesta.listaDeReservas = new List<Reserva>();
                respuesta.listaDeReservas.Add(reservamodel.BuscarReserva(id, configuracion));
                return respuesta;
            }
            catch (System.Exception e)
            {
                ReservaRespuesta respuesta = new ReservaRespuesta();
                respuesta.idRespuesta = 99;
                respuesta.mensaje = "Reserva no encontrada";
                respuesta.listaDeReservas = new List<Reserva>();
                return respuesta;
            }

        }

        //obtener todas las reservas
        [HttpGet]
        [Route("Reservas")]
        public ReservaRespuesta Reservas()
        {
            try
            {
                var res = reservamodel.Reservas(configuracion);
                ReservaRespuesta respuesta = new ReservaRespuesta();
                respuesta.idRespuesta = 0;
                respuesta.mensaje = "!OK";
                respuesta.listaDeReservas = new List<Reserva>();
                respuesta.listaDeReservas.AddRange(res);
                return respuesta;
            }
            catch (System.Exception)
            {
                ReservaRespuesta respuesta = new ReservaRespuesta();
                respuesta.idRespuesta = 99;
                respuesta.mensaje = "!ERROR";
                respuesta.listaDeReservas = new List<Reserva>();
                return respuesta;
            }


        }


        //registrar reserva
        [HttpPost]
        [Route("AgregarReserva")]
        public ReservaRespuesta AgregarReserva(Reserva reserva)
        {
            try
            {
                reservamodel.AgregarReserva(reserva, configuracion);
                ReservaRespuesta respuesta = new ReservaRespuesta();
                respuesta.idRespuesta = 0;
                respuesta.mensaje = "!OK";
                respuesta.listaDeReservas = new List<Reserva>();
                return respuesta;
            }
            catch (System.Exception e)
            {
                ReservaRespuesta respuesta = new ReservaRespuesta();
                respuesta.idRespuesta = 99;
                respuesta.mensaje = "!ERROR";
                respuesta.listaDeReservas = new List<Reserva>();
                return respuesta;
            }

        }
    
    
     //selectReservaPorFecha
        [HttpGet]
        [Route("ReservaporFecha")]
        public ReservaRespuesta ReservaporFecha(string fecha, int id)
        {
            try
            {
                var res = reservamodel.ReservasPorFecha(fecha, id, configuracion);
                ReservaRespuesta respuesta = new ReservaRespuesta();
                respuesta.idRespuesta = 0;
                respuesta.mensaje = "!OK";
                respuesta.listaDeReservas = new List<Reserva>();
                respuesta.listaDeReservas.AddRange(res);
                return respuesta;
            }
            catch (System.Exception e)
            {
                ReservaRespuesta respuesta = new ReservaRespuesta();
                respuesta.idRespuesta = 99;
                respuesta.mensaje = "!ERROR";
                respuesta.listaDeReservas = new List<Reserva>();
                return respuesta;
            }

        }

        [HttpGet]
        [Route("ReservasAPagar")]
        public ReservaAPagarRespuesta getReservasAPagar(int idUsuario)
        {
            try
            {
                var res = reservamodel.getReservasaPagar(configuracion, idUsuario);

                if (res.Any())
                {

                    return reservamodel.armarRespuestaReservaAPagar(0, "Ok!", res);

                }
                else
                {

                    return reservamodel.armarRespuestaReservaAPagar(1, "No hay reservas por pagar.", res);

                }
            }
            catch (System.Exception e)
            {

                return reservamodel.armarRespuestaReservaAPagar(99, e.Message, new List<ReservaAPagar>());

            }

        }

    }
}
