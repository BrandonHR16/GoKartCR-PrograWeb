using Dapper;
using GoKartAPI.Entities;
using System.Data;
using System.Data.SqlClient;

namespace GoKartAPI.Model
{
    public class RegistroAdminModel
    {
        public void CambiarRol(int idUsuario, IConfiguration configuracionP)
        {

            var conexion = new SqlConnection(configuracionP.GetSection("ConnectionStrings:baseDeDatos").Value);

            conexion.Open();

            var usuario = conexion.Execute("ActualizarRol", new { idUsuario }, commandType: CommandType.StoredProcedure);

            conexion.Close();

  

        } //Fin
        public List<Usuario> GetUsuarios(IConfiguration configuracionP)
        {

            var conexion = new SqlConnection(configuracionP.GetSection("ConnectionStrings:baseDeDatos").Value);

            conexion.Open();

            var listaEventos = conexion.Query<Usuario>("Usuarios", new { }, commandType: CommandType.StoredProcedure).ToList();

            conexion.Close();

            return listaEventos;

        } //Fin.

        public UsuarioRespuesta armarRespuesta(int idCodigo, string mensaje, List<Usuario> listaUsuarios)
        {

            UsuarioRespuesta respuesta = new UsuarioRespuesta();

            respuesta.idCodigo = idCodigo;
            respuesta.mensaje = mensaje;
            respuesta.listaUsuarios = listaUsuarios;

            return respuesta;

        }

    }
}