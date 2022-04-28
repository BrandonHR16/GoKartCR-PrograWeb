using GoKartAPI.Entities;
using GoKartAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace GoKartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrarAdminController : Controller
    {
        private readonly IConfiguration configuracion;
        RegistroAdminModel adminModel = new RegistroAdminModel();

        public RegistrarAdminController(IConfiguration configuracion)
        {
            this.configuracion = configuracion;
        }
        [HttpGet]
        [Route("ActualizarRol")]
        public UsuarioRespuesta ActualizarRol(int idCodigo)
        {
            try
            {
                UsuarioRespuesta respuesta = new UsuarioRespuesta();
                respuesta.idCodigo = 0;
                respuesta.mensaje = "!OK";
                respuesta.listaUsuarios = new List<Usuario>();
                adminModel.CambiarRol(idCodigo, configuracion);
                return respuesta;
            }
            catch (System.Exception)
            {
                UsuarioRespuesta respuesta = new UsuarioRespuesta();
                respuesta.idCodigo = 99;
                respuesta.mensaje = "Usuario no encontrada";
                respuesta.listaUsuarios = new List<Usuario>();
                return respuesta;
            }

        }
        [HttpGet]
        [Route("getUsuarios")]
        public ActionResult<UsuarioRespuesta> Usuarios()
        {

            try
            {
                var listaUsuarios = adminModel.GetUsuarios(configuracion);

                return adminModel.armarRespuesta(0, "OK!", listaUsuarios);

            }
            catch (Exception excepcion)
            {

          
                return adminModel.armarRespuesta(99, "Error: " + excepcion.Message, new List<Usuario>());

            }

        } //Fin.
    }
}
