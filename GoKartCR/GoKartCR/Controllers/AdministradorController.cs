using GoKartCR.Entities;
using GoKartCR.Models;
using Microsoft.AspNetCore.Mvc;

namespace GoKartCR.Controllers
{
    public class AdministradorController : Controller
    {
        PreguntasModel preguntasModel = new PreguntasModel();
        CorreoModel correomodel = new CorreoModel();
        RegistrarAdminModel registAdminModel = new RegistrarAdminModel();


        public IActionResult preguntasFrecuentes()
        {

            return View(GetPreguntas());
        }
        public IActionResult AgregarAdministrador()
        {

            return View(GetUsuarios());
        }

        [HttpGet]
        public PreguntaRespuesta GetPreguntas()
        {
            try
            {
            var data = preguntasModel.GetPreguntas().Result;
            return (data);
            }
            catch (Exception)
            {

                throw;
            }

        }
        [HttpGet]
        public UsuarioRespuesta GetUsuarios()
        {
            try
            {
                var data = registAdminModel.Getusuarios().Result;
                return (data);
            }
            catch (Exception)
            {

                throw;
            }

        }


        public async Task<IActionResult> envioCorreo(string correo, string Asunto, string mensaje) 
        {
            try
            {
                Correo correos = new Correo();
                correos.Destinatario = correo;
                correos.Asunto = Asunto;
                correos.Mensaje = mensaje;
                var res = correomodel.Enviar(correos);
                if (res.idCodigo == 0)
                {
                    TempData["Mensaje"] = "Se envio el correo con exito.success";
                    return RedirectToAction("preguntasFrecuentes", "Administrador");
                }
                else
                {
                    TempData["Mensaje"] = "Error al enviar el correo.error";
                    return RedirectToAction("preguntasFrecuentes", "Administrador");
                }

            }
            catch (Exception e)
            {

                TempData["Mensaje"] = "Error al conectar con el servidor.error";
                return RedirectToAction("preguntasFrecuentes", "Administrador");
            }

        }
        public async Task<IActionResult> ActualizarRol(int idUsuario)
        {
            try
            {
                

                var res = registAdminModel.ActualizarRol(idUsuario);
            
                    TempData["Mensaje"] = "Se cambió el Rol con exito.success";
                    return RedirectToAction("AgregarAdministrador", "Administrador");
              
            

            }
            catch (Exception e)
            {

                TempData["Mensaje"] = "Error al conectar con el servidor.error";
                return RedirectToAction("preguntasFrecuentes", "Administrador");
            }

        }
    }
}
