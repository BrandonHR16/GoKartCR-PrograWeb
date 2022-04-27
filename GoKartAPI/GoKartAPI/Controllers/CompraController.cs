using GoKartAPI.Entities;
using GoKartAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoKartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraController : ControllerBase
    {

        private readonly IConfiguration configuracion;
        private CompraModel compraoModel = new CompraModel();
        private EventoModel eventoModel = new EventoModel();

        public CompraController(IConfiguration configuracionP)
        {

            configuracion = configuracionP;

        }

        [HttpPost]
        [Route("realizarCompra")]
        public ActionResult<CompraRespuesta> realizarCompra(List<Compra> listaCompras)
        {

            try
            {

                if (listaCompras.Any())
                {

                    int estadoCompra = compraoModel.realizarCompra(configuracion, listaCompras);

                    if (estadoCompra!=0)
                    {

                        eventoModel.registrarEvento("Compra", "Se ha realizado una compra.", configuracion);
                        return compraoModel.armarRespuesta(0, "Compra realizada", listaCompras);

                    }
                    else
                    {

                        eventoModel.registrarEvento("Compra", "No se ha podido realizar la compra.", configuracion);
                        return compraoModel.armarRespuesta(1, "Compra no realizada", listaCompras);

                    }

                }
                else
                {

                    return compraoModel.armarRespuesta(2, "No hay articulos por comprar", listaCompras);

                }

            }
            catch (Exception excepcion)
            {

                eventoModel.registrarEvento("Usuario", "Error: " + excepcion.Message, configuracion);
                return compraoModel.armarRespuesta(99, "Error: " + excepcion.Message, new List<Compra>());

            } //Fin.

        } //Fin.

    } //Fin. 

} //Fin.
