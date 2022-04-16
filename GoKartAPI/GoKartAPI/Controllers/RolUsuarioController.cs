using GoKartAPI.Entities;
using GoKartAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoKartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolUsuarioController : ControllerBase
    {

        private readonly IConfiguration configuracion;
        private RolUsuarioModel rolUsuarioModel = new RolUsuarioModel();
        private EventoModel eventoModel = new EventoModel();

        public RolUsuarioController(IConfiguration configuracionP)
        {

            configuracion = configuracionP;

        }

        [HttpGet]
        [Route("getRolesUsuario")]
        public ActionResult<RolUsuarioRespuesta> selectRolesUsuario()
        {

            try
            {

                var rolesDeUsaurio = rolUsuarioModel.selectRolesUsuario(configuracion);

                if (rolesDeUsaurio.Any())
                {

                    return rolUsuarioModel.armarRespuesta(0, "OK!", rolesDeUsaurio);

                }
                else
                {

                    eventoModel.registrarEvento("RolUsuario", "No hay roles de usuario registrados.", configuracion);
                    return rolUsuarioModel.
                        armarRespuesta(1, "No hay roles de usuario registrados.", new List<RolUsuario>());

                }

            }
            catch (Exception excepcion)
            {

                eventoModel.registrarEvento("RolUsuario", "Error: " + excepcion.Message, configuracion);
                return rolUsuarioModel.armarRespuesta(99, "Error: " + excepcion.Message, new List<RolUsuario>());

            }

        } //Fin.

    } //Fin.

} //Fin. 
