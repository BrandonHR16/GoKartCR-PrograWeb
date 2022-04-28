using GoKartCR.Entities;
using Newtonsoft.Json;
using System.Text;

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
        //actualizarPaquete
        public PaqueteRespuesta actualizarPaquete(Paquete paquete)
        {
            using (var http = new HttpClient())
            {
                paquete.file = null;

                var response = http.PostAsync("https://localhost:7169/api/Paquete/actualizarPaquete", new StringContent(JsonConvert.SerializeObject(paquete), Encoding.UTF8, "application/json")).Result;

                var paquetes = JsonConvert.DeserializeObject<PaqueteRespuesta>(response.Content.ReadAsStringAsync().Result);

                return paquetes;

            }

        }
        //agregarPaquete
        public PaqueteRespuesta agregarPaquete(Paquete paquete)
        {
            //post
            using (var http = new HttpClient())
            {

                paquete.file = null;

                var response = http.PostAsync("https://localhost:7169/api/Paquete/agregarPaquete", new StringContent(JsonConvert.SerializeObject(paquete), Encoding.UTF8, "application/json")).Result;

                var paquetes = JsonConvert.DeserializeObject<PaqueteRespuesta>(response.Content.ReadAsStringAsync().Result);

                return paquetes;

            }

        }

        //deletePaquete
        public PaqueteRespuesta deletePaquete(int id)
        {
            //DELETE
            using (var http = new HttpClient())
            {


                var response = http.DeleteAsync("https://localhost:7169/api/Paquete/deletePaquete?idPaquete=" + id).Result;

                var paquetes = JsonConvert.DeserializeObject<PaqueteRespuesta>(response.Content.ReadAsStringAsync().Result);

                return paquetes;

            }

        }

    }
}
