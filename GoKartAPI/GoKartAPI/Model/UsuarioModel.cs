using Dapper;
using GoKartAPI.Entities;
using System.Data;
using System.Data.SqlClient;

namespace GoKartAPI.Model
{
    public class UsuarioModel
    {

        public List<Usuario> selectUsuarios(IConfiguration configuracionP)
        {

            var conexion = new SqlConnection(configuracionP.GetSection("ConnectionStrings:baseDeDatos").Value);

            conexion.Open();

            var usuarios = conexion.Query<Usuario>("selectUsuarios", new { }, commandType: CommandType.StoredProcedure).ToList();

            conexion.Close();

            return usuarios;

        } //Fin.

        public Usuario selectUsuarioPorCorreo(string correoUsuario, IConfiguration configuracionP)
        {

            var conexion = new SqlConnection(configuracionP.GetSection("ConnectionStrings:baseDeDatos").Value);

            conexion.Open();    

            var usuario = conexion.Query<Usuario>("selectUsuarioPorCorreo", new {correoUsuario}, commandType: CommandType.StoredProcedure).FirstOrDefault();

            conexion.Close();

            return usuario;

        } //Fin.

        public Usuario selectUsuarioPorCedula(string cedulaUsuario, IConfiguration configuracionP)
        {

            var conexion = new SqlConnection(configuracionP.GetSection("ConnectionStrings:baseDeDatos").Value);

            conexion.Open();

            var usuario = conexion.Query<Usuario>("selectUsuarioPorCedula", new { cedulaUsuario }, commandType: CommandType.StoredProcedure).FirstOrDefault();

            conexion.Close();

            return usuario;

        } //Fin.

        public Usuario iniciarSesionUsuario(string correoUsuario, string contrasennia, IConfiguration configuracionP)
        {

            var conexion = new SqlConnection(configuracionP.GetSection("ConnectionStrings:baseDeDatos").Value);

            conexion.Open();

            var usuario = conexion.Query<Usuario>("iniciarSesion", new { correoUsuario, contrasennia }, commandType: CommandType.StoredProcedure).FirstOrDefault();

            conexion.Close();

            return usuario;

        } //Fin.

        public void registrarUsuario(Usuario nuevoUsuario, IConfiguration configuracionP)
        {

            var conexion = new SqlConnection(configuracionP.GetSection("ConnectionStrings:baseDeDatos").Value);

            conexion.Open();

            conexion.Execute("registrarUsuario", 
                new 
                { 
                    
                    nuevoUsuario.cedulaUsuario, 
                    nuevoUsuario.primerNombre,
                    nuevoUsuario.primerApellido,
                    nuevoUsuario.segundoApellido,
                    nuevoUsuario.correoElectronico,
                    nuevoUsuario.contrasennia,
                    nuevoUsuario.telefono,
                    nuevoUsuario.direccionResidencia,
                    nuevoUsuario.idRol
                
                }, 
                commandType: CommandType.StoredProcedure);

            conexion.Close();

        } //Fin de registarUsuario.

        public void actualizarUsuario(Usuario nuevoUsuario, IConfiguration configuracionP)
        {

            var conexion = new SqlConnection(configuracionP.GetSection("ConnectionStrings:baseDeDatos").Value);

            conexion.Open();

            conexion.Execute("actualizarUsuario",
                new
                {

                    nuevoUsuario.cedulaUsuario,
                    nuevoUsuario.correoElectronico,
                    nuevoUsuario.contrasennia,
                    nuevoUsuario.telefono,
                    nuevoUsuario.direccionResidencia,
                    nuevoUsuario.idRol

                },
                commandType: CommandType.StoredProcedure);

            conexion.Close();

        } //Fin de registarUsuario.

        public UsuarioRespuesta armarRespuesta(int idCodigo, string mensaje, List<Usuario> listaUsuarios)
        {

            UsuarioRespuesta respuesta = new UsuarioRespuesta();

            respuesta.idCodigo = idCodigo;
            respuesta.mensaje = mensaje;    
            respuesta.listaUsuarios = listaUsuarios; 

            return respuesta;

        } //Fin.

    } //Fin de la clase. 

} //Fin.