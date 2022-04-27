using Dapper;
using GoKartAPI.Entities;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GoKartAPI.Model
{
    public class PistaModel
    {

        public Pista selectPistaPorID(int idPista, IConfiguration configuracionP)
        {

            var conexion = new SqlConnection(configuracionP.GetSection("ConnectionStrings:baseDeDatos").Value);

            conexion.Open();

            var pista = conexion.Query<Pista>("selectPistaPorID", new { idPista }, commandType: CommandType.StoredProcedure).FirstOrDefault();

            conexion.Close();

            return pista;

        } //Fin.

        public Pista selectPistaPorNombre(string nombrePista, IConfiguration configuracionP)
        {

            var conexion = new SqlConnection(configuracionP.GetSection("ConnectionStrings:baseDeDatos").Value);

            conexion.Open();

            var pista = conexion.Query<Pista>("selectPistaPorNombre", new { nombrePista }, commandType: CommandType.StoredProcedure).FirstOrDefault();

            conexion.Close();

            return pista;

        } //Fin.

        public List<Pista> selectPistas(IConfiguration configuracionP)
        {

            var conexion = new SqlConnection(configuracionP.GetSection("ConnectionStrings:baseDeDatos").Value);

            conexion.Open();

            var listaPistas = conexion.Query<Pista>("selectPistas", new {  }, commandType: CommandType.StoredProcedure).ToList();

            conexion.Close();

            return listaPistas;

        } //Fin.

        public void registrarPista(Pista nuevaPista, IConfiguration configuracionP)
        {

            var conexion = new SqlConnection(configuracionP.GetSection("ConnectionStrings:baseDeDatos").Value);

            conexion.Open();

            conexion.Execute("registrarPista",
                new
                {

                    nuevaPista.nombrePista,
                    nuevaPista.distanciaMetros,
                    nuevaPista.capacidadUsuarios,
                    nuevaPista.Imagen
                  

                },
                commandType: CommandType.StoredProcedure);

            conexion.Close();

        } //Fin de registarUsuario.

        public void actualizarPista(Pista actualizarPista, IConfiguration configuracionP)
        {

            var conexion = new SqlConnection(configuracionP.GetSection("ConnectionStrings:baseDeDatos").Value);

            conexion.Open();

            conexion.Execute("actualizarPista",
                new
                {

                    actualizarPista.idPista,
                    actualizarPista.nombrePista,
                    actualizarPista.distanciaMetros,
                    actualizarPista.capacidadUsuarios

                },
                commandType: CommandType.StoredProcedure);

            conexion.Close();

        } //Fin de registarUsuario.

        public PistaRespuesta armarRespuesta(int idCodigo, string mensaje, List<Pista> listaDePistas)
        {

            PistaRespuesta respuesta = new PistaRespuesta();

            respuesta.idCodigo = idCodigo;
            respuesta.mensaje = mensaje;
            respuesta.listaDePistas = listaDePistas;

            return respuesta;

        } //Fin.

    } //Fin.

} //Fin.
