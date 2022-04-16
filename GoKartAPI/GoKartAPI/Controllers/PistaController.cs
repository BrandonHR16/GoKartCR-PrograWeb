using GoKartAPI.Entities;
using GoKartAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoKartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PistaController : ControllerBase
    {

        private readonly IConfiguration configuracion;
        private PistaModel pistaModel = new PistaModel();
        private EventoModel eventoModel = new EventoModel();

        public PistaController(IConfiguration configuracionP)
        {

            configuracion = configuracionP;

        }

        [HttpGet]
        [Route("getPistaPorID")]
        public ActionResult<PistaRespuesta> selectPistaPorID(int idPista)
        {

            try
            {

                var usuario = pistaModel.selectPistaPorID(idPista, configuracion);

                if (usuario != null)
                {

                    List<Pista> listaPistas = new List<Pista>();
                    listaPistas.Add(usuario);

                    return pistaModel.armarRespuesta(0, "OK!", listaPistas);

                }
                else
                {

                    eventoModel.registrarEvento("Pista", "No se encontró la pista con el id: " + idPista, configuracion);
                    return pistaModel.
                        armarRespuesta(1, "No se encontró la pista con el id: " + idPista, new List<Pista>());

                }

            }
            catch (Exception excepcion)
            {

                eventoModel.registrarEvento("Pista", "Error: " + excepcion.Message, configuracion);
                return pistaModel.armarRespuesta(99, "Error: " + excepcion.Message, new List<Pista>());

            }

        } //Fin.

        [HttpGet]
        [Route("getPistaPorNombre")]
        public ActionResult<PistaRespuesta> getPistaPorNombre(string nombrePista)
        {

            try
            {

                var usuario = pistaModel.selectPistaPorNombre(nombrePista, configuracion);

                if (usuario != null)
                {

                    List<Pista> listaPistas = new List<Pista>();
                    listaPistas.Add(usuario);

                    return pistaModel.armarRespuesta(0, "OK!", listaPistas);

                }
                else
                {

                    eventoModel.registrarEvento("Pista", "No se encontró la pista con el nombre: " + nombrePista, configuracion);
                    return pistaModel.
                        armarRespuesta(1, "No se encontró la pista con el nombre: " + nombrePista, new List<Pista>());

                }

            }
            catch (Exception excepcion)
            {

                eventoModel.registrarEvento("Pista", "Error: " + excepcion.Message, configuracion);
                return pistaModel.armarRespuesta(99, "Error: " + excepcion.Message, new List<Pista>());

            }

        } //Fin.

        [HttpGet]
        [Route("getPistas")]
        public ActionResult<PistaRespuesta> selectPistas()
        {

            try
            {

                var listaPistas = pistaModel.selectPistas(configuracion);

                if (listaPistas.Any())
                {


                    return pistaModel.armarRespuesta(0, "OK!", listaPistas);

                }
                else
                {

                    eventoModel.registrarEvento("Pista", "No hay pistas registradas.", configuracion);
                    return pistaModel.
                        armarRespuesta(1, "No hay pistas registradas.", new List<Pista>());

                }

            }
            catch (Exception excepcion)
            {

                eventoModel.registrarEvento("Pista", "Error: " + excepcion.Message, configuracion);
                return pistaModel.armarRespuesta(99, "Error: " + excepcion.Message, new List<Pista>());

            }

        } //Fin.

        [HttpPost]
        [Route("registrarPista")]
        public ActionResult<PistaRespuesta> registrarPista(Pista nuevaPista)
        {

            try
            {

                var pista = pistaModel.selectPistaPorNombre(nuevaPista.nombrePista, configuracion);

                if (pista == null)
                {

                    pistaModel.registrarPista(nuevaPista, configuracion);
                    pista = pistaModel.selectPistaPorNombre(nuevaPista.nombrePista, configuracion);

                    if (pista != null)
                    {

                        List<Pista> listaPistas = new List<Pista>();
                        listaPistas.Add(pista);

                        eventoModel.registrarEvento("Pista", "Se ha registrado la pista: " + pista.nombrePista,
                            configuracion);
                        return pistaModel.armarRespuesta(0, "Pista Registrada", listaPistas);

                    }
                    else
                    {

                        eventoModel.registrarEvento("Pista", "No se ha podido registrado la pista: " + nuevaPista.nombrePista, configuracion);

                        return pistaModel.
                            armarRespuesta(1, "No se ha podido registrado la pista: " + nuevaPista.nombrePista,
                            new List<Pista>());

                    } //Fin del else. 

                }
                else
                {

                    List<Pista> listaPistas = new List<Pista>();
                    listaPistas.Add(pista);

                    eventoModel.registrarEvento("Pista", "Pista ya registrada: " + nuevaPista.nombrePista, configuracion);
                    return pistaModel.
                        armarRespuesta(3, "Pista ya registrada: " + nuevaPista.nombrePista, listaPistas);

                } //Fin del else. 

            }
            catch (Exception excepcion)
            {

                eventoModel.registrarEvento("Pista", "Error: " + excepcion.Message, configuracion);
                return pistaModel.armarRespuesta(99, "Error: " + excepcion.Message, new List<Pista>());

            } //Fin.

        } //Fin.

        [HttpPut]
        [Route("actualirPista")]
        public ActionResult<PistaRespuesta> actualirPista(Pista pistaAActualizar)
        {

            try
            {

                var pista = pistaModel.selectPistaPorID(pistaAActualizar.idPista, configuracion);

                if (pista != null)
                {

                    string nombreAntiguo = pista.nombrePista;

                    pistaModel.actualizarPista(pistaAActualizar, configuracion);
                    pista = pistaModel.selectPistaPorID(pistaAActualizar.idPista, configuracion);

                    if (pista != null)
                    {

                        List<Pista> listaPistas = new List<Pista>();
                        listaPistas.Add(pista);

                        eventoModel.registrarEvento("Pista", "Se ha actualizado la pista: " + nombreAntiguo + " a " + pistaAActualizar.nombrePista,
                            configuracion);
                        return pistaModel.armarRespuesta(0, "Pista actualizado", listaPistas);

                    }
                    else
                    {

                        eventoModel.registrarEvento("Pista", "No se ha podido actualizar la pista: " + pistaAActualizar.nombrePista, configuracion);

                        return pistaModel.
                            armarRespuesta(1, "No se ha podido actualizar la pista: " + pistaAActualizar.nombrePista,
                            new List<Pista>());

                    } //Fin del else. 

                }
                else
                {

                    List<Pista> listaPistas = new List<Pista>();
                    listaPistas.Add(pista);

                    eventoModel.registrarEvento("Pista", "Pista no encontrada: " + pistaAActualizar.nombrePista, configuracion);
                    return pistaModel.
                        armarRespuesta(3, "Pista no encontrada: " + pistaAActualizar.nombrePista, listaPistas);

                } //Fin del else. 

            }
            catch (Exception excepcion)
            {

                eventoModel.registrarEvento("Pista", "Error: " + excepcion.Message, configuracion);
                return pistaModel.armarRespuesta(99, "Error: " + excepcion.Message, new List<Pista>());

            } //Fin.

        } //Fin.

    } //Fin.

} //Fin.
