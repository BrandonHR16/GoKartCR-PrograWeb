﻿using GoKartCR.Entities;
using GoKartCR.Models;
using Microsoft.AspNetCore.Mvc;

namespace GoKartCR.Controllers
{
    public class AdministradorController : Controller
    {
        PreguntasModel preguntasModel = new PreguntasModel();
        PaquetesModel paquetemodel = new PaquetesModel();
        CorreoModel correomodel = new CorreoModel();
        UsuarioModel usuariomodel = new UsuarioModel();
        PistasModel pistasmodel = new PistasModel();


        public IActionResult preguntasFrecuentes()
        {

            return View(GetPreguntas());
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

                var res =  pistasmodel.registrarPista(psita);
                if (res.idCodigo==0)
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
