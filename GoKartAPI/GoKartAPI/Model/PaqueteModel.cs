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

        //actualizarpaquete
        public Paquete actualizarPaquete(Paquete paquete, IConfiguration configuracionP)
        {

            var conexion = new SqlConnection(configuracionP.GetSection("ConnectionStrings:baseDeDatos").Value);

            conexion.Open();

            var paqueteActualizado = conexion.QueryFirstOrDefault<Paquete>("actualizarPaquete", new { paquete.idPaquete, paquete.nombre, paquete.descripcion, paquete.costo, paquete.tiempoOfrecido, paquete.cantidadUsuarios, paquete.imagen, paquete.idPista}, commandType: CommandType.StoredProcedure);

            conexion.Close();

            return paqueteActualizado;

        } //Fin.

        //agregarpaquete
        public void agregarPaquete(Paquete paquete, IConfiguration configuracionP)
        {

            var conexion = new SqlConnection(configuracionP.GetSection("ConnectionStrings:baseDeDatos").Value);

            conexion.Open();

            var paqueteAgregado = conexion.QueryFirstOrDefault<Paquete>("agregarPaquete", new { paquete.nombre, paquete.descripcion, paquete.costo, paquete.tiempoOfrecido, paquete.cantidadUsuarios, paquete.imagen, paquete.idPista, paquete.idGoKart }, commandType: CommandType.StoredProcedure);

            conexion.Close();


        } //Fin.


        //DeletePorIDPaquete
        public Paquete deletePaquete(Paquete paquete, IConfiguration configuracionP)
        {

            var conexion = new SqlConnection(configuracionP.GetSection("ConnectionStrings:baseDeDatos").Value);

            conexion.Open();

            var paqueteEliminado = conexion.QueryFirstOrDefault<Paquete>("deletePaquete", new { paquete.idPaquete }, commandType: CommandType.StoredProcedure);

            conexion.Close();

            return paqueteEliminado;

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