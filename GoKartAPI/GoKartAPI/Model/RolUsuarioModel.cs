using Dapper;
using GoKartAPI.Entities;
using System.Data;
using System.Data.SqlClient;

namespace GoKartAPI.Model
{
    public class RolUsuarioModel
    {

        public List<RolUsuario> selectRolesUsuario(IConfiguration configuracionP)
        {

            var conexion = new SqlConnection(configuracionP.GetSection("ConnectionStrings:baseDeDatos").Value);

            conexion.Open();

            List<RolUsuario> rolesUsuario = conexion.Query<RolUsuario>("selectRolesUsuario", new {  }, commandType: CommandType.StoredProcedure).ToList();

            conexion.Close();

            return rolesUsuario;

        } //Fin.

        public RolUsuarioRespuesta armarRespuesta(int idCodigo, string mensaje, List<RolUsuario> listaUsuarios)
        {

            RolUsuarioRespuesta respuesta = new RolUsuarioRespuesta();

            respuesta.idCodigo = idCodigo;
            respuesta.mensaje = mensaje;
            respuesta.listaRolesDeUsuario = listaUsuarios;

            return respuesta;

        } //Fin.


    } //Fin.

} //Fin.
