using GoKartCR.Entities;
using Newtonsoft.Json;

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

    }
}
