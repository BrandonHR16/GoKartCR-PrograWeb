﻿using GoKartCR.Entities;
using GoKartCR.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using Microsoft.JSInterop;
using CurrieTechnologies.Razor.SweetAlert2;
using System.Dynamic;

namespace GoKartCR.Controllers
{
    public class HomeController : Controller
    {
        UsuarioModel usuariomodel = new UsuarioModel();
        PistasModel pistasModel = new PistasModel();
        PaquetesModel paqueteModel = new PaquetesModel();
        PreguntasModel preguntaModel = new PreguntasModel();

        dynamic mymodel = new ExpandoObject();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IJSRuntime js)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            mymodel.Pistas = GetPistas();
            mymodel.Paquetes = GetPaquetes();
            return View(mymodel);
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
        [HttpGet]
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

        [HttpPost]
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
        [HttpGet]
        public List<Pista> GetPistas()
        {
            PistaRespuesta res = pistasModel.obtenerPistas();
            return res.listaDePistas;

        }

        [HttpGet]
        public List<Paquete> GetPaquetes()
        {
            PaqueteRespuesta res = paqueteModel.obtenerPaquete();
            return res.listaDePaquetes;

        }

        //Preguntas
        [HttpPost]
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

    }
}