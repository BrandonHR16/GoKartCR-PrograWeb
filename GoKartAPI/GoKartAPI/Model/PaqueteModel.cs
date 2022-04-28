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

        public List<Paquete> selectPaquete(IConfiguration configuracionP, int idPaquete)
        {

            var conexion = new SqlConnection(configuracionP.GetSection("ConnectionStrings:baseDeDatos").Value);

            conexion.Open();

            var listaPaquete = conexion.Query<Paquete>("selectPaquete", new { idPaquete }, commandType: CommandType.StoredProcedure).ToList();

            conexion.Close();

            return listaPaquete;

        } //Fin.

        //actualizarpaquete
        public int actualizarPaquete(Paquete paquete, IConfiguration configuracionP)
        {

            var conexion = new SqlConnection(configuracionP.GetSection("ConnectionStrings:baseDeDatos").Value);

            conexion.Open();

            int estadoActualizado = conexion.Execute("actualizarPaquete", new { paquete.idPaquete, paquete.nombre, paquete.descripcion, paquete.costo, paquete.tiempoOfrecido, paquete.cantidadUsuarios, paquete.imagen, paquete.idPista}, commandType: CommandType.StoredProcedure);

            conexion.Close();

            return estadoActualizado;

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
        public int deletePaquete(int idPaquete, IConfiguration configuracionP)
        {

            var conexion = new SqlConnection(configuracionP.GetSection("ConnectionStrings:baseDeDatos").Value);

            conexion.Open();

            var paqueteEliminado = conexion.Execute("DeletePorIDPaquete", new { idPaquete }, commandType: CommandType.StoredProcedure);

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