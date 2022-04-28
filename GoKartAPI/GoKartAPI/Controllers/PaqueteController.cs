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


        //actualizarPaquete
        [HttpPost]
        [Route("actualizarPaquete")]
        public ActionResult<PaqueteRespuesta> actualizarPaquete(Paquete paquete)
        {

            try
            {

                paquetemodel.actualizarPaquete(paquete, configuracion);
                var paqueteActualizado = paquetemodel.selectPaquete(configuracion, paquete.idPaquete);

                if (paqueteActualizado.Any())
                {

                    eventoModel.registrarEvento("Paquete", "Paquete actualizado correctamente.", configuracion);
                    return paquetemodel.armarRespuesta(0, "OK!", paqueteActualizado);

                }
                else
                {

                    eventoModel.registrarEvento("Paquete", "No se pudo actualizar el Paquete.", configuracion);
                    return paquetemodel.
                        armarRespuesta(1, "No se pudo actualizar el Paquete", new List<Paquete>());

                }

            }
            catch (Exception excepcion)
            {

                eventoModel.registrarEvento("Paquete", "Error: " + excepcion.Message, configuracion);
                return paquetemodel.armarRespuesta(99, "Error: " + excepcion.Message, new List<Paquete>());

            }

        } //Fin.

        //agregarPaquete
        [HttpPost]
        [Route("agregarPaquete")]
        public ActionResult<PaqueteRespuesta> agregarPaquete(Paquete paquete)
        {

            try
            {

                paquetemodel.agregarPaquete(paquete, configuracion);

                eventoModel.registrarEvento("Paquete", "Paquete agregado correctamente.", configuracion);
                return paquetemodel.armarRespuesta(0, "OK!", new List<Paquete>());

            }
            catch (Exception excepcion)
            {

                eventoModel.registrarEvento("Paquete", "Error: " + excepcion.Message, configuracion);
                return paquetemodel.armarRespuesta(99, "Error: " + excepcion.Message, new List<Paquete>());

            }

        } //Fin.

        //deletePaquete
        [HttpDelete]
        [Route("deletePaquete")]
        public ActionResult<PaqueteRespuesta> deletePaquete(int idpaquete)
        {

            try
            {

                var paqueteEliminado = paquetemodel.deletePaquete(idpaquete, configuracion);

                if (paqueteEliminado != null)
                {

                    eventoModel.registrarEvento("Paquete", "Paquete eliminado correctamente.", configuracion);
                    return paquetemodel.armarRespuesta(0, "OK!", new List<Paquete>());

                }
                else
                {

                    eventoModel.registrarEvento("Paquete", "No se pudo eliminar el Paquete.", configuracion);
                    return paquetemodel.
                        armarRespuesta(1, "No se pudo eliminar el Paquete", new List<Paquete>());

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
