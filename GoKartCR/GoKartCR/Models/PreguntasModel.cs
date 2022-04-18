using GoKartCR.Entities;
using Newtonsoft.Json;
using System.Text;

namespace GoKartCR.Models
{
    public class PreguntasModel
    {

        public async Task<PreguntaRespuesta> RegistarPregunta(Pregunta pregunta)
        {
            using (var http = new HttpClient())
            {
                var response =  http.PostAsync("https://localhost:7169/api/Pregunta/registrarPregunta", new StringContent(JsonConvert.SerializeObject(pregunta), Encoding.UTF8, "application/json")).Result;

                var preguntaRegistrada = JsonConvert.DeserializeObject<PreguntaRespuesta>(response.Content.ReadAsStringAsync().Result);

                return preguntaRegistrada;

            }
        }
    }
}
