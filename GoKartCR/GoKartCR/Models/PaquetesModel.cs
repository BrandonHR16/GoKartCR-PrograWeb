using GoKartCR.Entities;
using Newtonsoft.Json;

namespace GoKartCR.Models
{
    public class PaquetesModel
    {
        public PaqueteRespuesta obtenerPaquete()
        {
            using (var http = new HttpClient())
            {
                var response = http.GetAsync("https://localhost:7169/api/Paquete/getPaquetes").Result;

                var paquete = JsonConvert.DeserializeObject<PaqueteRespuesta>(response.Content.ReadAsStringAsync().Result);

                return paquete;

            }

        }

    }
}
