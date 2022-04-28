using GoKartCR.Entities;
using Newtonsoft.Json;
using System.Text;

namespace GoKartCR.Models
{
    public class ComprasModel
    {

        public CompraRespuesta realizarCompra(List<ReservaAPagar> listaDeCompras)
        {

            List<Compra> listaFinalDeCompra = new List<Compra>();

            foreach (ReservaAPagar compraAPagar in listaDeCompras)
            {

                Compra compra = new Compra();

                compra.idReserva = compraAPagar.idReserva;
                compra.precioFinal = compraAPagar.costoPaqueteConIVA;

                listaFinalDeCompra.Add(compra);

            }

            using (var http = new HttpClient())
            {
                var response = http.PostAsync("https://localhost:7169/api/Compra/realizarCompra", new StringContent(JsonConvert.SerializeObject(listaFinalDeCompra), Encoding.UTF8, "application/json")).Result;

                if (response.IsSuccessStatusCode)
                {

                    var res = JsonConvert.DeserializeObject<CompraRespuesta>(response.Content.ReadAsStringAsync().Result);

                    return res;

                }
                else
                {

                    return null;

                }

            } //Fin del using.

        } //Fin de getReservasaPagar

    } //Fin.

} //Fin.
