using GoKartCR.Entities;
using Newtonsoft.Json;
using System.Text;

namespace GoKartCR.Models
{
    public class UsuarioModel
    {

        public  UsuarioRespuesta IniciarSession(string correoElectronico, string contrasennia)
        {
            using (var http = new HttpClient())
            {
                var response = http.GetAsync("https://localhost:7169/api/Usuario/iniciarSesion?correoUsuario=" + correoElectronico + "&contrasennia=" + contrasennia).Result;

                var usuario = JsonConvert.DeserializeObject<UsuarioRespuesta>(response.Content.ReadAsStringAsync().Result);

                return usuario;

            }
        }
    
        public  UsuarioRespuesta RegistrarUsuario(Usuario usuario)
        {
            using (var http = new HttpClient())
            {
                var response = http.PostAsync("https://localhost:7169/api/Usuario/registrarUsuario", new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json")).Result;

                var usuarioRegistrado = JsonConvert.DeserializeObject<UsuarioRespuesta>(response.Content.ReadAsStringAsync().Result);

                return usuarioRegistrado;

            }
        }

        public UsuarioRespuesta getUsuarios()
        {
            using (var http = new HttpClient())
            {
                var response = http.GetAsync("https://localhost:7169/api/Usuario/getUsuarios").Result;

                var usuarioRegistrado = JsonConvert.DeserializeObject<UsuarioRespuesta>(response.Content.ReadAsStringAsync().Result);

                return usuarioRegistrado;

            }
        }



    }
}
