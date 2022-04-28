using GoKartCR.Entities;
using Newtonsoft.Json;
using System.Text;

namespace GoKartCR.Models
{
    public class PistasModel
    {

        public PistaRespuesta obtenerPistas()
        {
            using (var http = new HttpClient())
            {
                var response = http.GetAsync("https://localhost:7169/api/Pista/getPistas").Result;

                var pistas = JsonConvert.DeserializeObject<PistaRespuesta>(response.Content.ReadAsStringAsync().Result);

                return pistas;

            }

        }
        //registrarpista
        public PistaRespuesta registrarPista(Pista pista)
        {
            using (var http = new HttpClient())
            {

                var response = http.PostAsync("https://localhost:7169/api/Pista/registrarPista", new StringContent(JsonConvert.SerializeObject(pista), Encoding.UTF8, "application/json")).Result;
                var pistas = JsonConvert.DeserializeObject<PistaRespuesta>(response.Content.ReadAsStringAsync().Result);

                return pistas;

            }

        }

        public PaqueteRespuesta deletePista(int id)
        {
            //DELETE
            using (var http = new HttpClient())
            {


                var response = http.DeleteAsync("https://localhost:7169/api/Pista/eliminarPista?id=" + id).Result;

                var paquetes = JsonConvert.DeserializeObject<PaqueteRespuesta>(response.Content.ReadAsStringAsync().Result);

                return paquetes;

            }

        }
    }
}
