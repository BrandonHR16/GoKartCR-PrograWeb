using GoKartCR.Entities;
using GoKartCR.Models;
using Microsoft.AspNetCore.Mvc;

namespace GoKartCR.Controllers
{
    public class AdministradorController : Controller
    {
        PreguntasModel preguntasModel = new PreguntasModel();
        CorreoModel correomodel = new CorreoModel();
        RegistrarAdminModel registAdminModel = new RegistrarAdminModel();
        PaquetesModel paquetemodel = new PaquetesModel();




        public IActionResult preguntasFrecuentes()
        {

            return View(GetPreguntas());
        }
        public IActionResult AgregarAdministrador()
        {

            return View(GetUsuarios());
        }

        [HttpGet]
        public PreguntaRespuesta GetPreguntas()
        {
            try
            {
            var data = preguntasModel.GetPreguntas().Result;
            return (data);
            }
            catch (Exception)
            {

                throw;
            }

        }
        [HttpGet]
        public UsuarioRespuesta GetUsuarios()
        {
            try
            {
                var data = registAdminModel.Getusuarios().Result;
                return (data);
            }
            catch (Exception)
            {

                throw;
            }

        }


        public async Task<IActionResult> envioCorreo(string correo, string Asunto, string mensaje) 
        {
            try
            {
                Correo correos = new Correo();
                correos.Destinatario = correo;
                correos.Asunto = Asunto;
                correos.Mensaje = mensaje;
                var res = correomodel.Enviar(correos);
                if (res.idCodigo == 0)
                {
                    TempData["Mensaje"] = "Se envio el correo con exito.success";
                    return RedirectToAction("preguntasFrecuentes", "Administrador");
                }
                else
                {
                    TempData["Mensaje"] = "Error al enviar el correo.error";
                    return RedirectToAction("preguntasFrecuentes", "Administrador");
                }

            }
            catch (Exception e)
            {

                TempData["Mensaje"] = "Error al conectar con el servidor.error";
                return RedirectToAction("preguntasFrecuentes", "Administrador");
            }

        }
        public async Task<IActionResult> ActualizarRol(int idUsuario)
        {
            try
            {
                

                var res = registAdminModel.ActualizarRol(idUsuario);
            
                    TempData["Mensaje"] = "Se cambió el Rol con exito.success";
                    return RedirectToAction("AgregarAdministrador", "Administrador");
              
            

            }
            catch (Exception e)
            {

                TempData["Mensaje"] = "Error al conectar con el servidor.error";
                return RedirectToAction("preguntasFrecuentes", "Administrador");
            }

        }
    
    
        //modelPaquetes
        //obtenerPaquete
        public async Task<IActionResult> Paquetes()
        {
            try
            {
                var data = paquetemodel.obtenerPaquete();
                return View(data);
            }
            catch (Exception)
            {

                throw;
            }

        }
        //public int idPaquete { get; set; } = 0;
        //public string nombre { get; set; } = String.Empty;
        //public string descripcion { get; set; } = String.Empty;
        //public int costo { get; set; } = 0;
        //public TimeSpan tiempoOfrecido { get; set; }
        //public int cantidadUsuarios { get; set; } = 0;
        //public byte[] imagen { get; set; }
        //public int idPista { get; set; } = 0;
        //public string nombrePista { get; set; } = String.Empty;

        //crre una tabla con los datos de los paquetes
        //agregar paquete
        [HttpPost]
        //obtener el input de la imagen
      
        public async Task<IActionResult> AgregarPaquete(Paquete paquete)
        {
            try
            {
                if (paquete.file != null)
                {
                    paquete.imagen = new byte[paquete.file.Length];
                    paquete.file.OpenReadStream().Read(paquete.imagen, 0, (int)paquete.file.Length);
                }
                var data = paquetemodel.agregarPaquete(paquete);
                if (data.idCodigo == 0)
                {
                    TempData["Mensaje"] = "Se agrego el paquete con exito.success";
                    return RedirectToAction("Paquetes", "Administrador");
                }
                else
                {
                    TempData["Mensaje"] = "Error al agregar el paquete.error";
                    return RedirectToAction("Paquetes", "Administrador");
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost]
        public async Task<IActionResult> EditarPaquete(Paquete paquete)
        {
            try
            {
                if (paquete.file != null)
                {
                    paquete.imagen = new byte[paquete.file.Length];
                    paquete.file.OpenReadStream().Read(paquete.imagen, 0, (int)paquete.file.Length);
                }
                var data = paquetemodel.actualizarPaquete(paquete);
                if (data.idCodigo == 0)
                {
                    TempData["Mensaje"] = "Se agrego el paquete con exito.success";
                    return RedirectToAction("Paquetes", "Administrador");
                }
                else
                {
                    TempData["Mensaje"] = "Error al agregar el paquete.error";
                    return RedirectToAction("Paquetes", "Administrador");
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        //delte
        [HttpPost]
        //por id
        public async Task<IActionResult> EliminarPaquete(int id)
        {
            try
            {
                Paquete paquete = new Paquete();
                paquete.idPaquete = id;
                var data = paquetemodel.deletePaquete(paquete);
                if (data.idCodigo == 0)
                {
                    TempData["Mensaje"] = "Se elimino el paquete con exito.success";
                    return RedirectToAction("Paquetes", "Administrador");
                }
                else
                {
                    TempData["Mensaje"] = "Error al eliminar el paquete.error";
                    return RedirectToAction("Paquetes", "Administrador");
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
