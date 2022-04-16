using GoKartAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoKartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KartController : ControllerBase
    {

        private readonly IConfiguration configuracion;
        private KartModel kartModel = new KartModel();
        private EventoModel eventoModel = new EventoModel();

        public KartController(IConfiguration configuracionP)
        {

            configuracion = configuracionP;

        }

        [HttpGet]
        [Route("getKartPorID")]
        public ActionResult<KartRespuesta> selectKartPorID(int idGoKart)
        {

            try
            {

                var usuario = kartModel.selectKartPorID(idGoKart, configuracion);

                if (usuario != null)
                {

                    List<Kart> listaDeKarts = new List<Kart>();
                    listaDeKarts.Add(usuario);

                    return kartModel.armarRespuesta(0, "OK!", listaDeKarts);

                }
                else
                {

                    eventoModel.registrarEvento("Kart", "No se encontró el kart con el id: " + idGoKart, configuracion);
                    return kartModel.
                        armarRespuesta(1, "No se encontró el kart con el id: " + idGoKart, new List<Kart>());

                }

            }
            catch (Exception excepcion)
            {

                eventoModel.registrarEvento("Kart", "Error: " + excepcion.Message, configuracion);
                return kartModel.armarRespuesta(99, "Error: " + excepcion.Message, new List<Kart>());

            }

        } //Fin.

        [HttpGet]
        [Route("getKartPorNombre")]
        public ActionResult<KartRespuesta> getKartPorNombre(string nombreKart)
        {

            try
            {

                var usuario = kartModel.selectKartPorNombre(nombreKart, configuracion);

                if (usuario != null)
                {

                    List<Kart> listaKarts = new List<Kart>();
                    listaKarts.Add(usuario);

                    return kartModel.armarRespuesta(0, "OK!", listaKarts);

                }
                else
                {

                    eventoModel.registrarEvento("Kart", "No se encontró el kart con el nombre: " + nombreKart, configuracion);
                    return kartModel.
                        armarRespuesta(1, "No se encontró el kart con el nombre: " + nombreKart, new List<Kart>());

                }

            }
            catch (Exception excepcion)
            {

                eventoModel.registrarEvento("Kart", "Error: " + excepcion.Message, configuracion);
                return kartModel.armarRespuesta(99, "Error: " + excepcion.Message, new List<Kart>());

            }

        } //Fin.

        [HttpGet]
        [Route("getKarts")]
        public ActionResult<KartRespuesta> selectKarts()
        {

            try
            {

                var listaDeKarts = kartModel.selectKarts(configuracion);

                if (listaDeKarts.Any())
                {


                    return kartModel.armarRespuesta(0, "OK!", listaDeKarts);

                }
                else
                {

                    eventoModel.registrarEvento("Kart", "No hay karts registradas.", configuracion);
                    return kartModel.
                        armarRespuesta(1, "No hay karts registradas.", new List<Kart>());

                }

            }
            catch (Exception excepcion)
            {

                eventoModel.registrarEvento("Kart", "Error: " + excepcion.Message, configuracion);
                return kartModel.armarRespuesta(99, "Error: " + excepcion.Message, new List<Kart>());

            }

        } //Fin.

        [HttpPost]
        [Route("registrarKart")]
        public ActionResult<KartRespuesta> registrarKart(Kart nuevoKart)
        {

            try
            {

                var kart = kartModel.selectKartPorNombre(nuevoKart.nombreKart, configuracion);

                if (kart == null)
                {

                    kartModel.registrarKart(nuevoKart, configuracion);
                    kart = kartModel.selectKartPorNombre(nuevoKart.nombreKart, configuracion);

                    if (kart != null)
                    {

                        List<Kart> listaKarts = new List<Kart>();
                        listaKarts.Add(kart);

                        eventoModel.registrarEvento("Kart", "Se ha registrado kart: " + kart.nombreKart,
                            configuracion);
                        return kartModel.armarRespuesta(0, "Kart Registrado", listaKarts);

                    }
                    else
                    {

                        eventoModel.registrarEvento("Kart", "No se ha podido registrado el kart: " + nuevoKart.nombreKart, configuracion);

                        return kartModel.
                            armarRespuesta(1, "No se ha podido registrado el kart: " + nuevoKart.nombreKart,
                            new List<Kart>());

                    } //Fin del else. 

                }
                else
                {

                    List<Kart> listaKarts = new List<Kart>();
                    listaKarts.Add(kart);

                    eventoModel.registrarEvento("Kart", "Kart ya registrada: " + nuevoKart.nombreKart, configuracion);
                    return kartModel.
                        armarRespuesta(3, "Kart ya registrada: " + nuevoKart.nombreKart, listaKarts);

                } //Fin del else. 

            }
            catch (Exception excepcion)
            {

                eventoModel.registrarEvento("Kart", "Error: " + excepcion.Message, configuracion);
                return kartModel.armarRespuesta(99, "Error: " + excepcion.Message, new List<Kart>());

            } //Fin.

        } //Fin.

        [HttpPut]
        [Route("actualirKart")]
        public ActionResult<KartRespuesta> actualirKart(Kart kartAActualizar)
        {

            try
            {

                var kart = kartModel.selectKartPorID(kartAActualizar.idGoKart, configuracion);

                if (kart != null)
                {

                    string nombreAntiguo = kart.nombreKart;

                    kartModel.actualizarKart(kartAActualizar, configuracion);
                    kart = kartModel.selectKartPorID(kartAActualizar.idGoKart, configuracion);

                    if (kart != null)
                    {

                        List<Kart> listaKarts = new List<Kart>();
                        listaKarts.Add(kart);

                        eventoModel.registrarEvento("Kart", "Se ha actualizado el kart: " + nombreAntiguo + " a " +  kart.nombreKart,
                            configuracion);
                        return kartModel.armarRespuesta(0, "Kart actualizado", listaKarts);

                    }
                    else
                    {

                        eventoModel.registrarEvento("Kart", "No se ha podido actualizar el kart: " + kartAActualizar.nombreKart, configuracion);

                        return kartModel.
                            armarRespuesta(1, "No se ha podido actualizar el kart: " + kartAActualizar.nombreKart,
                            new List<Kart>());

                    } //Fin del else. 

                }
                else
                {

                    List<Kart> listaKarts = new List<Kart>();
                    listaKarts.Add(kart);

                    eventoModel.registrarEvento("Kart", "Kart no encontrada: " + kartAActualizar.nombreKart, configuracion);
                    return kartModel.
                        armarRespuesta(3, "Kart no encontrada: " + kartAActualizar.nombreKart, listaKarts);

                } //Fin del else. 

            }
            catch (Exception excepcion)
            {

                eventoModel.registrarEvento("Kart", "Error: " + excepcion.Message, configuracion);
                return kartModel.armarRespuesta(99, "Error: " + excepcion.Message, new List<Kart>());

            } //Fin.

        } //Fin.


    } //Fin.

} //Fin.
