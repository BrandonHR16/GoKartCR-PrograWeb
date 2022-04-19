using GoKartCR.Entities;
using GoKartCR.Models;
using Microsoft.AspNetCore.Mvc;

namespace GoKartCR.Controllers
{
    public class AdministradorController : Controller
    {
        PreguntasModel preguntasModel = new PreguntasModel();
        
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
    }
}
