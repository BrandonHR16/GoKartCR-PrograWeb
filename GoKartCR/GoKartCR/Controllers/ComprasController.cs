using GoKartCR.Entities;
using GoKartCR.Models;
using Microsoft.AspNetCore.Mvc;

namespace GoKartCR.Controllers
{
    public class ComprasController : Controller
    {

        private readonly ReservaModel reservaModel = new ReservaModel();
        private readonly ComprasModel compraModel = new ComprasModel();
        private static List<ReservaAPagar> listaDeCompras = new List<ReservaAPagar>();
        private int idUsuario = 0;

        public ComprasController()
        {



        }

        public IActionResult CarritoCompra()
        {

            try
            {

                idUsuario = Int32.Parse(Request.Cookies["IdUser"]);

                ReservaAPagarRespuesta reservasAPagarRespuesta = reservaModel.getReservasaPagar(idUsuario);

                if (reservasAPagarRespuesta.idCodigo==0)
                {
                    
                    listaDeCompras = reservasAPagarRespuesta.reserverAPagar;

                }

                return View(reservasAPagarRespuesta);

            }
            catch (Exception)
            {

                return View(null);
            }

        } //Fin.

        [HttpPost]
        public IActionResult RealizarCompra()
        {

            try
            {

                CompraRespuesta respuestaCompra = compraModel.realizarCompra(listaDeCompras);

                listaDeCompras.Clear();

                return View(respuestaCompra);

            }
            catch (Exception)
            {

                listaDeCompras.Clear();

                return View(null);

            }

        } //Fin.

    } //Fin.

} //Fin.
