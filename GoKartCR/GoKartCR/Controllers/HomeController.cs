
﻿using GoKartCR.Entities;
using GoKartCR.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using Microsoft.JSInterop;
using System.Dynamic;
using System.Globalization;

namespace GoKartCR.Controllers
{
    public class HomeController : Controller
    {
        UsuarioModel usuariomodel = new UsuarioModel();
        PistasModel pistasModel = new PistasModel();
        Tabla_EventosModel eventosModel = new Tabla_EventosModel();
        PaquetesModel paqueteModel = new PaquetesModel();
        PreguntasModel preguntaModel = new PreguntasModel();
        ReservaModel reservasModel = new ReservaModel();
        RegistrarAdminModel regisAdminModel = new RegistrarAdminModel();
     

        dynamic mymodel = new ExpandoObject();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IJSRuntime js)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            mymodel.Pistas = GetPistas();
            mymodel.Eventos = GetEvento();
            mymodel.Paquetes = GetPaquetes();
            mymodel.Reserva = new Reserva();
            return View(mymodel);
        }

                public IActionResult CarritoCompra()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //Reagistro de Usuarios e Iniciar sesion 
        public async Task<IActionResult> IniciarSession(string correoElectronico, string contrasennia)
        {
            try
            {
                if (correoElectronico != "" && contrasennia != "")
                {
                    using (var http = new HttpClient())
                    {
                        UsuarioRespuesta res = usuariomodel.IniciarSession(correoElectronico, contrasennia);

                        if (res.mensaje == "OK!")
                        {
                            Response.Cookies.Append("Rol", res.listaUsuarios[0].idRol.ToString(), new Microsoft.AspNetCore.Http.CookieOptions { Expires = DateTime.Now.AddMinutes(10) });
                            Response.Cookies.Append("Nombre", res.listaUsuarios[0].primerNombre, new Microsoft.AspNetCore.Http.CookieOptions { Expires = DateTime.Now.AddMinutes(10) });
                            Response.Cookies.Append("IdUser", res.listaUsuarios[0].idUsuario.ToString(), new Microsoft.AspNetCore.Http.CookieOptions { Expires = DateTime.Now.AddMinutes(10) });
                            TempData["Mensaje"] = "Inicio sesion correctamente.success";
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {

                            TempData["Mensaje"] = "Error al iniciar sesion.error";

                        }
                        return RedirectToAction("Index", "Home");
                    }
                    TempData["Mensaje"] = "Error al establecer conexion con el servidor.error";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Mensaje"] = "Debe de completar todos los campos para iniciar sesion.info";
                    return RedirectToAction("Index", "Home");
                }


            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Error al establecer conexion con el servidor.error";
                return RedirectToAction("Index", "Home");
            }
        }


        public async Task<IActionResult> Registrarse(string Cedula, string Nombre, string PrimerApellido, string SegundoApellido, string correoElectronico, string contrasennia, string Telefono, string Direccion)
        {
            try
            {
                //validar que todos los campos esten llenos
                if (Cedula != "" && Nombre != "" && PrimerApellido != "" && SegundoApellido != "" && correoElectronico != "" && contrasennia != "" && Telefono != "" && Direccion != "")
                {
                    using (var http = new HttpClient())
                    {
                        Usuario usuario = new Usuario();
                        usuario.cedulaUsuario = Cedula;
                        usuario.primerNombre = Nombre;
                        usuario.primerApellido = PrimerApellido;
                        usuario.segundoApellido = SegundoApellido;
                        usuario.correoElectronico = correoElectronico;
                        usuario.contrasennia = contrasennia;
                        usuario.telefono = Telefono;
                        usuario.direccionResidencia = Direccion;
                        usuario.idRol = 2;

                        UsuarioRespuesta res = usuariomodel.RegistrarUsuario(usuario);

                        if (res.mensaje == "Usuario Registrado")
                        {
                            TempData["Mensaje"] = "Registro correcto.success";
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            TempData["Mensaje"] = "Error al registrarse.error";
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
                else
                {
                    TempData["Mensaje"] = "Debe de completar todos los campos para registrarse.info";
                    return RedirectToAction("Index", "Home");
                }



            }
            catch (Exception e)
            {
                TempData["Mensaje"] = "Error al establecer conexion con el servidor.error";
                return RedirectToAction("Index", "Home");
            }

        }


        //Informacion de Paquetes y Pistas

        public List<Pista> GetPistas()
        {
            PistaRespuesta res = pistasModel.obtenerPistas();
            return res.listaDePistas;

        }


        public List<Paquete> GetPaquetes()
        {
            PaqueteRespuesta res = paqueteModel.obtenerPaquete();
            return res.listaDePaquetes;

        }


        public List<Tabla_Eventos> GetEvento()
        {
            TablaRespuesta res = eventosModel.obtenerEvento();
            return res.listaEventos;

        }


    


        //Preguntas

        public async Task<IActionResult> enviarPregunta(string Nombre, string Correo, string Mensaje)
        {
            try
            {
                Pregunta pregunta = new Pregunta();
                pregunta.nombreUsuario = Nombre;
                pregunta.correo = Correo;
                pregunta.mensaje = Mensaje;
                var res = await preguntaModel.RegistarPregunta(pregunta);
                if (res.mensaje == "OK!")
                {
                    TempData["Mensaje"] = "Mesaje enviado exitosamente.success";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Mensaje"] = "Error al enviar el mensaje.error";
                    return RedirectToAction("Index", "Home");
                }

            }
            catch (Exception)
            {

                TempData["Mensaje"] = "Error al establecer conexion con el servidor.error";
                return RedirectToAction("Index", "Home");
            }
        }


        public JsonResult consultarReservas(string time){
            //dar formato de DataTime a time 2022-04-22
            
            var res = reservasModel.ReservasPorFecha(time);
            return Json(res);
        }
        public JsonResult consultarReservas(string time, int id){
            //dar formato de DataTime a time 2022-04-22
            try
            {
            var res = reservasModel.ReservasPorFecha(time, id);
            return Json(res);
            }
            catch (Exception)
            {

                throw;
            }

        }

         DateTime fechaReserva = DateTime.ParseExact(fecha, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            try
            {
                if (mannana == true)
                {
                    //establecer add 9 am 
                    fechaReserva = fechaReserva.AddHours(9);
                }
                else if (tarde == true)
                {

                   fechaReserva = fechaReserva.AddHours(13);
                }
                else if (noche == true)
                {

                    fechaReserva = fechaReserva.AddHours(20);
                }
                string idUsuarioweb = Request.Cookies["IdUser"];
                idUsuario = Int32.Parse(idUsuarioweb);
                Reserva res = new Reserva();
                res.idPaquete = idPaquete;
                res.idUsuario = idUsuario;
                res.fecha = fechaReserva;
                res.mannana = mannana;
                res.tarde = tarde;
                res.noche = noche;
                reservasModel.AgregarReserva(res);

                }
            catch (Exception e)
            {

                throw;
            }

        }

    }
}
