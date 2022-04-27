using GoKartAPI.Entities;
using GoKartAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoKartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly IConfiguration configuracion;
        private UsuarioModel usuarioModel = new UsuarioModel();
        private EventoModel eventoModel = new EventoModel();

        public UsuarioController(IConfiguration configuracionP)
        {

            configuracion = configuracionP;

        }

        [HttpGet]
        [Route("getUsuarios")]
        public ActionResult<UsuarioRespuesta> selectUsuarios()
        {

            try
            {

                var listaUsuarios = usuarioModel.selectUsuarios(configuracion);

                if (listaUsuarios.Any())
                {

                    return usuarioModel.armarRespuesta(0, "OK!", listaUsuarios);

                }
                else
                {

                    eventoModel.registrarEvento("Usuario", "No hay usuarios registrados.", configuracion);
                    return usuarioModel.
                        armarRespuesta(1, "No hay usuarios registrados.", new List<Usuario>());

                }

            }
            catch (Exception excepcion)
            {

                eventoModel.registrarEvento("Usuario", "Error: " + excepcion.Message, configuracion);
                return usuarioModel.armarRespuesta(99, "Error: " + excepcion.Message, new List<Usuario>());

            }

        } //Fin.

        [HttpGet]
        [Route("getUsuario")]
        public ActionResult<UsuarioRespuesta> selectUsuario(string cedulaUsuario)
        {

            try
            {

                var usuario = usuarioModel.selectUsuarioPorCedula(cedulaUsuario, configuracion);

                if (usuario != null)
                {

                    List<Usuario> listaUsuarios = new List<Usuario>();
                    listaUsuarios.Add(usuario);

                    return usuarioModel.armarRespuesta(0, "OK!", listaUsuarios);

                }
                else
                {

                    eventoModel.registrarEvento("Usuario", "No se encontró el usuario con la cédula: " + cedulaUsuario, configuracion);
                    return usuarioModel.
                        armarRespuesta(1, "No se encontró el usuario con la cédula: " + cedulaUsuario, new List<Usuario>());

                }

            }
            catch (Exception excepcion)
            {

                eventoModel.registrarEvento("Usuario", "Error: " + excepcion.Message, configuracion);
                return usuarioModel.armarRespuesta(99, "Error: " + excepcion.Message, new List<Usuario>());

            }

        } //Fin.

        [HttpGet]
        [Route("iniciarSesion")]
        public ActionResult<UsuarioRespuesta> iniciarSesionUsuario(string correoUsuario, string contrasennia)
        {

            try
            {

                var usuario = usuarioModel.selectUsuarioPorCorreo(correoUsuario, configuracion);

                if (usuario != null)
                {

                    usuario = usuarioModel.iniciarSesionUsuario(correoUsuario, contrasennia, configuracion);

                    if (usuario != null)
                    {

                        List<Usuario> listaUsuarios = new List<Usuario>();
                        listaUsuarios.Add(usuario);

                        return usuarioModel.armarRespuesta(0, "OK!", listaUsuarios);

                    }
                    else
                    {

                        eventoModel.registrarEvento("Usuario",
                        "No se ha autenticado el usuario con el correo: " + correoUsuario + ". Verificar contraseña.", configuracion);
                        return usuarioModel.
                            armarRespuesta(1, "No se ha autenticado el usuario con el correo: " + correoUsuario + ". Verificar contraseña.", new List<Usuario>());

                    }

                }
                else
                {

                    eventoModel.registrarEvento("Usuario",
                        "No está registrado el usuario con el correo: " + correoUsuario + ".", configuracion);
                    return usuarioModel.
                        armarRespuesta(2, "No está registrado el usuario con el correo: " + correoUsuario + ".", new List<Usuario>());

                }

            }
            catch (Exception excepcion)
            {

                eventoModel.registrarEvento("Usuario", "Error: " + excepcion.Message, configuracion);
                return usuarioModel.armarRespuesta(99, "Error: " + excepcion.Message, new List<Usuario>());

            }

        } //Fin.

        [HttpPost]
        [Route("registrarUsuario")]
        public ActionResult<UsuarioRespuesta> registrarUsuario(Usuario nuevoUsuario)
        {

            try
            {

                var usuario = usuarioModel.selectUsuarioPorCedula(nuevoUsuario.cedulaUsuario, configuracion);

                if (usuario == null)
                {

                    usuarioModel.registrarUsuario(nuevoUsuario, configuracion);
                    usuario = usuarioModel.selectUsuarioPorCedula(nuevoUsuario.cedulaUsuario, configuracion);

                    if (usuario != null)
                    {

                        List<Usuario> listaUsuarios = new List<Usuario>();
                        listaUsuarios.Add(usuario);

                        eventoModel.registrarEvento("Usuario", "Se ha registrado el usuario: \n\n" +
                            "Nombre: " + usuario.primerNombre + " " + usuario.primerApellido + " " + usuario.segundoApellido +
                            "\n Correo: " + usuario.correoElectronico +
                            "\n Cédula: " + usuario.cedulaUsuario,
                            configuracion);
                        return usuarioModel.armarRespuesta(0, "Usuario Registrado", listaUsuarios);

                    }
                    else
                    {

                        eventoModel.registrarEvento("Usuario", "No se ha podido registrado el usuario: \n\n" +
                            "Nombre: " + nuevoUsuario.primerNombre + " " + nuevoUsuario.primerApellido + " " + nuevoUsuario.segundoApellido +
                            "\nCorreo: " + nuevoUsuario.correoElectronico +
                            "\nCédula: " + nuevoUsuario.cedulaUsuario, configuracion);

                        return usuarioModel.
                            armarRespuesta(1, "No se ha podido registrado el usuario: \n\n" +
                            "Nombre: " + nuevoUsuario.primerNombre + " " + nuevoUsuario.primerApellido + " " + nuevoUsuario.segundoApellido +
                            "\n Correo: " + nuevoUsuario.correoElectronico +
                            "\n Cédula: " + nuevoUsuario.cedulaUsuario,
                            new List<Usuario>());

                    } //Fin del else. 

                }
                else
                {

                    List<Usuario> listaUsuarios = new List<Usuario>();
                    listaUsuarios.Add(usuario);

                    eventoModel.registrarEvento("Usuario", "Cédula ya registrada: " + usuario.cedulaUsuario, configuracion);
                    return usuarioModel.
                        armarRespuesta(3, "Cédula ya registrada: " + usuario.cedulaUsuario, listaUsuarios);

                } //Fin del else. 

            }
            catch (Exception excepcion)
            {

                eventoModel.registrarEvento("Usuario", "Error: " + excepcion.Message, configuracion);
                return usuarioModel.armarRespuesta(99, "Error: " + excepcion.Message, new List<Usuario>());

            } //Fin.

        } //Fin.

        [HttpPut]
        [Route("actualirUsuario")]
        public ActionResult<UsuarioRespuesta> actualirUsuario(Usuario usuarioAActualizar)
        {

            try
            {

                var usuario = usuarioModel.selectUsuarioPorCedula(usuarioAActualizar.cedulaUsuario, configuracion);

                if (usuario != null)
                {

                    usuarioModel.actualizarUsuario(usuarioAActualizar, configuracion);
                    usuario = usuarioModel.selectUsuarioPorCedula(usuarioAActualizar.cedulaUsuario, configuracion);

                    if (usuario != null)
                    {

                        List<Usuario> listaUsuarios = new List<Usuario>();
                        listaUsuarios.Add(usuario);

                        eventoModel.registrarEvento("Usuario", "Se ha actualizado el usuario: \n\n" +
                            "Nombre: " + usuario.primerNombre + " " + usuario.primerApellido + " " + usuario.segundoApellido +
                            "\n Correo: " + usuario.correoElectronico +
                            "\n Cédula: " + usuario.cedulaUsuario,
                            configuracion);
                        return usuarioModel.armarRespuesta(0, "Usuario actualizado.", listaUsuarios);

                    }
                    else
                    {

                        eventoModel.registrarEvento("Usuario", "No se ha podido actualizar el usuario: \n\n" +
                            "Nombre: " + usuarioAActualizar.primerNombre + " " + usuarioAActualizar.primerApellido + " " + usuarioAActualizar.segundoApellido +
                            "\nCorreo: " + usuarioAActualizar.correoElectronico +
                            "\nCédula: " + usuarioAActualizar.cedulaUsuario, configuracion);

                        return usuarioModel.
                            armarRespuesta(1, "No se ha podido actualizar el usuario: \n\n" +
                            "Nombre: " + usuarioAActualizar.primerNombre + " " + usuarioAActualizar.primerApellido + " " + usuarioAActualizar.segundoApellido +
                            "\n Correo: " + usuarioAActualizar.correoElectronico +
                            "\n Cédula: " + usuarioAActualizar.cedulaUsuario,
                            new List<Usuario>());

                    } //Fin del else. 

                }
                else
                {

                    List<Usuario> listaUsuarios = new List<Usuario>();
                    listaUsuarios.Add(usuario);

                    eventoModel.registrarEvento("Usuario", "No se ha encontrado el usuario con el correo: " + usuario.correoElectronico, configuracion);
                    return usuarioModel.
                        armarRespuesta(3, "No se ha encontrado el usuario con el correo: " + usuario.correoElectronico, listaUsuarios);

                } //Fin del else. 

            }
            catch (Exception excepcion)
            {

                eventoModel.registrarEvento("Usuario", "Error: " + excepcion.Message, configuracion);
                return usuarioModel.armarRespuesta(99, "Error: " + excepcion.Message, new List<Usuario>());

            } //Fin.

        } //Fin.

    } //Fin. 

} //Fin.