using Dapper;
using GoKartAPI.Entities;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json;

namespace GoKartAPI.Model
{
    public class CompraModel
    {

        public int realizarCompra(IConfiguration configuracionP, List<Compra> listaCompras)
        {

            string JSONIDReservas = JsonSerializer.Serialize(listaCompras);

            var conexion = new SqlConnection(configuracionP.GetSection("ConnectionStrings:baseDeDatos").Value);

            conexion.Open();

            var compraRealizada = conexion.Execute("realizarCompra", new { JSONIDReservas }, commandType: CommandType.StoredProcedure);

            conexion.Close();

            return compraRealizada;

        } //Fin.

        public CompraRespuesta armarRespuesta(int idCodigo, string mensaje, List<Compra> compras)
        {

            CompraRespuesta respuesta = new CompraRespuesta();

            respuesta.idCodigo = idCodigo;
            respuesta.mensaje = mensaje;
            respuesta.compras = compras;

            return respuesta;

        } //Fin.

    } //Fin.

} //Fin.
