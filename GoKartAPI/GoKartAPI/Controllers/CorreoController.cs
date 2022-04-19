using GoKartAPI.Entities;
using GoKartAPI.Model;
using Microsoft.AspNetCore.Mvc;
using static GoKartAPI.Entities.Correo;

namespace GoKartAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class CorreoController : Controller
    {
        private readonly IConfiguration configuracion;

        public CorreoController(IConfiguration configuracion)
        {
            this.configuracion = configuracion;
        }

        CorreoModelo correomodel = new CorreoModelo();

        [HttpPost]
        [Route("EnviarCorreo")]
        public CorreoRespuesta enviarCorreo(Correo correo)
        {
            try
            {
                correomodel.enviarCorreo(correo, configuracion);
                return correomodel.armarRespuesta(0, "Ok!");
            }
            catch (Exception)
            {
                return correomodel.armarRespuesta(99, "Error al enviar el correo");

            }

        }

    }
}
