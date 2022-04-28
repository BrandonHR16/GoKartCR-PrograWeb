using GoKartAPI.Entities;
using GoKartAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace GoKartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablaEventosController : Controller
    {
        private readonly IConfiguration configuracion;
        Tabla_EventosModel eventomodel = new Tabla_EventosModel();
        private EventoModel eventoModel = new EventoModel();

        public TablaEventosController(IConfiguration configuracion)
        {
            this.configuracion = configuracion;
        }

        [HttpGet]
        [Route("getEventos")]
        public ActionResult<TablaRespuesta> EventosSemana()
        {

            try
            {

                var listaEventos = eventomodel.EventosSemana(configuracion);

                    return eventomodel.armarRespuesta(0, "OK!", listaEventos);

            }
            catch (Exception excepcion)
            {

                eventoModel.registrarEvento("Paquete", "Error: " + excepcion.Message, configuracion);
                return eventomodel.armarRespuesta(99, "Error: " + excepcion.Message, new List<Tabla_Eventos>());

            }

        } //Fin.
    }
}
