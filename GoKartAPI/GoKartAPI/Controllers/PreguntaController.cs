using GoKartAPI.Entities;
using GoKartAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace GoKartAPI.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class PreguntaController : Controller
    {

        private readonly IConfiguration configuracion;
        PreguntaModel preguntaModel = new PreguntaModel();
        private EventoModel eventoModel = new EventoModel();


        public PreguntaController(IConfiguration configuracion)
        {
            this.configuracion = configuracion;
        }

        [HttpPost]
        [Route("registrarPregunta")]
            public ActionResult<PreguntaRespuesta> registrarPregunta(Pregunta pregunta)
            {
                try
                {
                  
                preguntaModel.registrarPregunta(pregunta, configuracion);
                return preguntaModel.armarRespuesta(0, "OK!", new List<Pregunta>());

                }
                catch (Exception excepcion)
                {
                    eventoModel.registrarEvento("Pregunta", "Error: " + excepcion.Message, configuracion);
                    return preguntaModel.armarRespuesta(99, "Error: " + excepcion.Message, new List<Pregunta>());
                }
            }

            //selectPreguntas
            [HttpGet]
            [Route("selectPreguntas")]
            public ActionResult<PreguntaRespuesta> selectPreguntas()
            {
                try
                {

                    var preguntas = preguntaModel.selectPreguntas(configuracion);

                    if (preguntas != null)
                    {

                        return preguntaModel.armarRespuesta(0, "OK!", preguntas);

                    }
                    else
                    {

                        eventoModel.registrarEvento("Pregunta", "No se encontraron preguntas", configuracion);
                        return preguntaModel.
                            armarRespuesta(1, "No se encontraron preguntas", new List<Pregunta>());

                    }

                }
                catch (Exception excepcion)
                {

                    eventoModel.registrarEvento("Pregunta", "Error: " + excepcion.Message, configuracion);
                    return preguntaModel.armarRespuesta(99, "Error: " + excepcion.Message, new List<Pregunta>());

                }

            }

            //selectPregunta 
            [HttpGet]
            [Route("selectPregunta")]
            public ActionResult<PreguntaRespuesta> selectPregunta(int idPregunta)
            {
                try
                {

                    var pregunta = preguntaModel.selectPreguntaPorId(idPregunta, configuracion);

                    if (pregunta != null)
                    {

                        return preguntaModel.armarRespuesta(0, "OK!", new List<Pregunta>());

                    }
                    else
                    {

                        eventoModel.registrarEvento("Pregunta", "No se encontró la pregunta con el id: " + idPregunta, configuracion);
                        return preguntaModel.
                            armarRespuesta(1, "No se encontró la pregunta con el id: " + idPregunta, new List<Pregunta>());

                    }

                }
                catch (Exception excepcion)
                {

                    eventoModel.registrarEvento("Pregunta", "Error: " + excepcion.Message, configuracion);
                    return preguntaModel.armarRespuesta(99, "Error: " + excepcion.Message, new List<Pregunta>());

                }

            }
    }
}
