using GoKartCR.Entities;
using Newtonsoft.Json;
using System.Text;
using static GoKartCR.Entities.Correo;

namespace GoKartCR.Models
{
    public class CorreoModel
    {

        public CorreoRespuesta Enviar(Correo correo)
        {
            using (var http = new HttpClient())
            {
                var response = http.PostAsync("https://localhost:7169/api/Correo/EnviarCorreo", new StringContent(JsonConvert.SerializeObject(correo), Encoding.UTF8, "application/json")).Result;

                var correores = JsonConvert.DeserializeObject<CorreoRespuesta>(response.Content.ReadAsStringAsync().Result);

                return correores;

            }

        }

    }
}
