using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace GoKartAPI.Model
{
    public class KartModel
    {

        public Kart selectKartPorID(int idGoKart, IConfiguration configuracionP)
        {

            var conexion = new SqlConnection(configuracionP.GetSection("ConnectionStrings:baseDeDatos").Value);

            conexion.Open();

            var kart = conexion.Query<Kart>("selectKartPorID", new { idGoKart }, commandType: CommandType.StoredProcedure).FirstOrDefault();

            conexion.Close();

            return kart;

        } //Fin.

        public Kart selectKartPorNombre(string nombreKart, IConfiguration configuracionP)
        {

            var conexion = new SqlConnection(configuracionP.GetSection("ConnectionStrings:baseDeDatos").Value);

            conexion.Open();

            var kart = conexion.Query<Kart>("selectKartPorNombre", new { nombreKart }, commandType: CommandType.StoredProcedure).FirstOrDefault();

            conexion.Close();

            return kart;

        } //Fin.

        public List<Kart> selectKarts(IConfiguration configuracionP)
        {

            var conexion = new SqlConnection(configuracionP.GetSection("ConnectionStrings:baseDeDatos").Value);

            conexion.Open();

            var listaKarts = conexion.Query<Kart>("selectKarts", new { }, commandType: CommandType.StoredProcedure).ToList();

            conexion.Close();

            return listaKarts;

        } //Fin.

        public void registrarKart(Kart nuevaKart, IConfiguration configuracionP)
        {

            var conexion = new SqlConnection(configuracionP.GetSection("ConnectionStrings:baseDeDatos").Value);

            conexion.Open();

            conexion.Execute("registrarKart",
                new
                {

                    nuevaKart.nombreKart,


                },
                commandType: CommandType.StoredProcedure);

            conexion.Close();

        } //Fin de registarUsuario.

        public void actualizarKart(Kart actualizarKart, IConfiguration configuracionP)
        {

            var conexion = new SqlConnection(configuracionP.GetSection("ConnectionStrings:baseDeDatos").Value);

            conexion.Open();

            conexion.Execute("actualizarKart",
                new
                {

                    actualizarKart.idGoKart,
                    actualizarKart.nombreKart

                },
                commandType: CommandType.StoredProcedure);

            conexion.Close();

        } //Fin de registarUsuario.

        public KartRespuesta armarRespuesta(int idCodigo, string mensaje, List<Kart> listaDePistas)
        {

            KartRespuesta respuesta = new KartRespuesta();

            respuesta.idCodigo = idCodigo;
            respuesta.mensaje = mensaje;
            respuesta.listaDeKarts = listaDePistas;

            return respuesta;

        } //Fin.


    } //Fin.

} //Fin

