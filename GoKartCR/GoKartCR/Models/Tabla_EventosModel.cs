using GoKartCR.Entities;
using Newtonsoft.Json;

namespace GoKartCR.Models
{
    public class Tabla_EventosModel
    {
            public TablaRespuesta obtenerEvento()
            {
                using (var http = new HttpClient())
                {
                    var response = http.GetAsync("https://localhost:7169/api/TablaEventos/getEventos").Result;

                    var eventos = JsonConvert.DeserializeObject<TablaRespuesta>(response.Content.ReadAsStringAsync().Result);

                    return eventos;

                }

            }
    }
}
