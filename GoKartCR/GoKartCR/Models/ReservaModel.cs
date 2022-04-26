using GoKartCR.Entities;
using Newtonsoft.Json;
using System.Text;
using static GoKartCR.Entities.Reserva;

namespace GoKartCR.Models
{
    public class ReservaModel
    {

        //reserva por su id
        public Reserva BuscarReserva(int id)
        {
            using (var http = new HttpClient())
            {
                var response = http.PostAsync("https://localhost:7169/api/Reserva/ReservaporId", new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json")).Result;

                var preguntaRegistrada = JsonConvert.DeserializeObject<Reserva>(response.Content.ReadAsStringAsync().Result);

                return preguntaRegistrada;
            }

        } //Fin

        //get all reservas
        public List<Reserva> Reservas()
        {
            using (var http = new HttpClient())
            {
                var response = http.GetAsync("https://localhost:7169/api/Reserva/Reservas").Result;

                var res = JsonConvert.DeserializeObject<List<Reserva>>(response.Content.ReadAsStringAsync().Result);

                return res;
            }

        } //Fin

        //registrar reserva
        public void AgregarReserva(Reserva nuevaReserva)
        {

            using (var http = new HttpClient())
            {
                var response = http.PostAsync("https://localhost:7169/api/Reserva/AgregarReserva", new StringContent(JsonConvert.SerializeObject(nuevaReserva), Encoding.UTF8, "application/json")).Result;

                var preguntaRegistrada = JsonConvert.DeserializeObject<PreguntaRespuesta>(response.Content.ReadAsStringAsync().Result);

            }

        } //Fin

        //selectReservaPorFecha
        public List<Reserva> ReservasPorFecha(string fecha)
        {

            using (var http = new HttpClient())
            {

                var response = http.GetAsync("https://localhost:7169/api/Reserva/ReservaporFecha?fecha=" + fecha).Result;

                var res = JsonConvert.DeserializeObject<ReservaRespuesta>(response.Content.ReadAsStringAsync().Result);

                //si la hora de la reserva es entre las 9am y las 12md se le asigna true a la vasriable mannana
                //si la hora de la reserva es entre las 12md y las 3pm se le asigna true a la vasriable tarde
                //si la hora de la reserva es entre las 3pm y las 6pm se le asigna true a la vasriable noche
                if (res.listaDeReservas == null)
                {
                    return res.listaDeReservas;
                }

                if(res.listaDeReservas.Count > 0) {                 
                    foreach (var item in res.listaDeReservas)
                {
                    if (item.fecha.Hour >= 9 && item.fecha.Hour <= 12)
                    {
                        item.mannana = true;
                    }
                    else
                    {
                        item.mannana = false;
                    }

                    if (item.fecha.Hour >= 13 && item.fecha.Hour <= 15)
                    {
                        item.tarde = true;
                    }
                    else
                    {
                        item.tarde = false;
                    }

                    if (item.fecha.Hour >= 16 && item.fecha.Hour <= 18)
                    {
                        item.noche = true;
                    }
                    else
                    {
                        item.noche = false;
                    }
                }
                }

                return res.listaDeReservas;
            }


        } //Fin

    }
}
