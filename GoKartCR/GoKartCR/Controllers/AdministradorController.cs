using GoKartCR.Entities;
using GoKartCR.Models;
using Microsoft.AspNetCore.Mvc;

namespace GoKartCR.Controllers
{
    public class AdministradorController : Controller
    {
        PreguntasModel preguntasModel = new PreguntasModel();
        CorreoModel correomodel = new CorreoModel();


        public IActionResult preguntasFrecuentes()
        {

            return View(GetPreguntas());
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
    }
}
