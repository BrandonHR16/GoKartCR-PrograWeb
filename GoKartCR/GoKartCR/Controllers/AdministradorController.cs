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
        UsuarioModel usuariomodel = new UsuarioModel();
        PistasModel pistasmodel = new PistasModel();



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


        public IActionResult Clientes()
        {
            return View(usuariomodel.getUsuarios());
        }


        public IActionResult Pistas()
        {

            return View(pistasmodel.obtenerPistas());
        }

        public IActionResult AgregarPista(Pista psita)
        {
            try
            {
                if (psita.file != null)
                {
                    psita.Imagen = new byte[psita.file.Length];
                    psita.file.OpenReadStream().Read(psita.Imagen, 0, (int)psita.file.Length);
                }

                var res = pistasmodel.registrarPista(psita);
                if (res.idCodigo == 0)
                {
                    TempData["Mensaje"] = "Se agrego la pista con exito.success";
                    return RedirectToAction("Pistas", "Administrador");
                }
                else if (res.idCodigo == 1)
                {
                    TempData["Mensaje"] = "Error al registrar la pista.error";
                    return RedirectToAction("Pistas", "Administrador");
                }
                else
                {
                    TempData["Mensaje"] = "Error al conectar con el servidor.error";
                    return RedirectToAction("Pistas", "Administrador");
                }
                return RedirectToAction("Pistas", "Administrador");
            }
            catch (Exception)
            {
                TempData["Mensaje"] = "Error al conectar con el servidor.error";
                return RedirectToAction("Pistas", "Administrador");
            }
        }

        [HttpPost]
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
                    TempData["Mensaje"] = "Se actualizó el paquete con exito.success";
                    return RedirectToAction("Paquetes", "Administrador");
                }
                else
                {
                    TempData["Mensaje"] = "Error al actualizar el paquete.error";
                    return RedirectToAction("Paquetes", "Administrador");
                }
            }
            catch (Exception)
            {

                throw;
            }

        }


        public async Task<IActionResult> DeletePaquete(int idPaquete)
        {
            try
            {
                var res = paquetemodel.deletePaquete(idPaquete);
                if (res.idCodigo == 0)
                {
                    TempData["Mensaje"] = "Se agrego el paquete con exito.success";
                    return RedirectToAction("Paquetes", "Administrador");
                }
                else if (res.idCodigo == 1)
                {
                    TempData["Mensaje"] = "No de pudo eliminar el paquete.error";
                    return RedirectToAction("Paquetes", "Administrador");
                }
                else
                {
                    TempData["Mensaje"] = "No de pudo conectar con el servidor.error";
                    return RedirectToAction("Paquetes", "Administrador");
                }
            }
            catch (Exception)
            {

                TempData["Mensaje"] = "No de pudo conectar con el servidor.error";
                return RedirectToAction("Paquetes", "Administrador");
            }
        }

        public async Task<IActionResult> DeletePista(int idPista)
        {
            try
            {
                var res = pistasmodel.deletePista(idPista);
                if (res.idCodigo == 0)
                {
                    TempData["Mensaje"] = "Se agrego el paquete con exito.success";
                    return RedirectToAction("Pistas", "Administrador");
                }
                else if (res.idCodigo == 1)
                {
                    TempData["Mensaje"] = "No de pudo eliminar el paquete.error";
                    return RedirectToAction("Pistas", "Administrador");
                }
                else
                {
                    TempData["Mensaje"] = "No de pudo conectar con el servidor.error";
                    return RedirectToAction("Pistas", "Administrador");
                }
            }
            catch (Exception)
            {

                TempData["Mensaje"] = "No de pudo conectar con el servidor.error";
                return RedirectToAction("Pistas", "Administrador");
            }
        }
    }
}
