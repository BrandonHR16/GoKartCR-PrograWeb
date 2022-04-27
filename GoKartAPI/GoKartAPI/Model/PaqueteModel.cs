using Dapper;
using GoKartAPI.Entities;
using System.Data;
using System.Data.SqlClient;

namespace GoKartAPI.Model
{
    public class PaqueteModel
    {

        public List<Paquete> selectPaquetes(IConfiguration configuracionP)
        {

            var conexion = new SqlConnection(configuracionP.GetSection("ConnectionStrings:baseDeDatos").Value);

            conexion.Open();

            var listaPaquete = conexion.Query<Paquete>("selectPaquetes", new { }, commandType: CommandType.StoredProcedure).ToList();

            conexion.Close();

            return listaPaquete;
             
        } //Fin.


        public PaqueteRespuesta armarRespuesta(int idCodigo, string mensaje, List<Paquete> listaDePistas)
        {

            PaqueteRespuesta respuesta = new PaqueteRespuesta();

            respuesta.idCodigo = idCodigo;
            respuesta.mensaje = mensaje;
            respuesta.listaDePaquetes = listaDePistas;

            return respuesta;

        }

    }
}