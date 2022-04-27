using GoKartCR.Entities;
using Newtonsoft.Json;
using System.Text;

namespace GoKartCR.Models
{
    public class RegistrarAdminModel
    {
        public async Task<UsuarioRespuesta> Getusuarios()
        {
            using (var http = new HttpClient())
            {
                var response = http.GetAsync("https://localhost:7169/api/RegistrarAdmin/getUsuarios").Result;

                var res = JsonConvert.DeserializeObject<UsuarioRespuesta>(response.Content.ReadAsStringAsync().Result);

                return res;

            }

        } //Fin



        public UsuarioRespuesta ActualizarRol(int idUsuario)
        {
            using (var http = new HttpClient())
            {
                var response = http.GetAsync("https://localhost:7169/api/RegistrarAdmin/ActualizarRol?idCodigo=" + idUsuario).Result;

                var res = JsonConvert.DeserializeObject<UsuarioRespuesta>(response.Content.ReadAsStringAsync().Result);

                return res;

            }


        }
    }
}
