using GoKartAPI.Entities;
using GoKartAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace GoKartAPI.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]

    public class PaqueteController : Controller
    {
     
        private readonly IConfiguration configuracion;
        PaqueteModel paquetemodel = new PaqueteModel();
        private EventoModel eventoModel = new EventoModel();

        public PaqueteController(IConfiguration configuracion)
        {
            this.configuracion = configuracion;
        }

        [HttpGet]
        [Route("getPaquetes")]
        public ActionResult<PaqueteRespuesta> selectPaquete()
        {

            try
            {

                var listaDePaquetes = paquetemodel.selectPaquetes(configuracion);

                if (listaDePaquetes.Any())
                {


                    return paquetemodel.armarRespuesta(0, "OK!", listaDePaquetes);

                }
                else
                {

                    eventoModel.registrarEvento("Paquete", "No hay Paquetes registrados.", configuracion);
                    return paquetemodel.
                        armarRespuesta(1, "No hay Paquetes registrados", new List<Paquete>());

                }

            }
            catch (Exception excepcion)
            {

                eventoModel.registrarEvento("Paquete", "Error: " + excepcion.Message, configuracion);
                return paquetemodel.armarRespuesta(99, "Error: " + excepcion.Message, new List<Paquete>());

            }

        } //Fin.
    }
}
