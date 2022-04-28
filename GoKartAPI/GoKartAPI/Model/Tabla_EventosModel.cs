using Dapper;
using GoKartAPI.Entities;

using System.Data;
using System.Data.SqlClient;

namespace GoKartAPI.Model
{

   
    public class Tabla_EventosModel
    {
       

        public List<Tabla_Eventos> EventosSemana(IConfiguration configuracionP)
        {

            var conexion = new SqlConnection(configuracionP.GetSection("ConnectionStrings:baseDeDatos").Value);

            conexion.Open();

            var listaEventos = conexion.Query<Tabla_Eventos>("Evento_Semanal", new { }, commandType: CommandType.StoredProcedure).ToList();

            conexion.Close();

            return listaEventos;

        } //Fin.


        public TablaRespuesta armarRespuesta(int idCodigo, string mensaje, List<Tabla_Eventos> listaEventos)
        {

            TablaRespuesta respuesta = new TablaRespuesta();

            respuesta.idCodigo = idCodigo;
            respuesta.mensaje = mensaje;
            respuesta.listaEventos = listaEventos;

            return respuesta;

        }

    }
}