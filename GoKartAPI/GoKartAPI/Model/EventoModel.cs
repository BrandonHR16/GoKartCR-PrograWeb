using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace GoKartAPI.Model
{
    public class EventoModel
    {

        public void registrarEvento(string categoriaDelEvento, string mensaje, IConfiguration configuracionP)
        {

            var conexion = new SqlConnection(configuracionP.GetSection("ConnectionStrings:baseDeDatos").Value);

            conexion.Open();

            conexion.Execute("registrarEvento", new{ categoriaDelEvento, mensaje}, commandType: CommandType.StoredProcedure);

            conexion.Close();

        } //Fin.

    } //Fin de la clase. 

} //Fin del namespace. 
