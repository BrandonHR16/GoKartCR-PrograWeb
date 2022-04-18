using Dapper;
using GoKartAPI.Entities;
using System.Data;
using System.Data.SqlClient;

namespace GoKartAPI.Model
{
    public class PreguntaModel
    {



        //Seleccionar pista por Id
        public Pregunta selectPreguntaPorId(int idPregunta, IConfiguration configuracionP)
        {

            var conexion = new SqlConnection(configuracionP.GetSection("ConnectionStrings:baseDeDatos").Value);

            conexion.Open();

            var pregunta = conexion.Query<Pregunta>("selectPreguntas", new { idPregunta }, commandType: CommandType.StoredProcedure).FirstOrDefault();

            conexion.Close();

            return pregunta;

        } //Fin
        //Selecciona todas las preguntas de selectPreguntas
        public List<Pregunta> selectPreguntas(IConfiguration configuracionP)
        {

            var conexion = new SqlConnection(configuracionP.GetSection("ConnectionStrings:baseDeDatos").Value);

            conexion.Open();

            var listaPreguntas = conexion.Query<Pregunta>("selectPreguntas", new { }, commandType: CommandType.StoredProcedure).ToList();

            conexion.Close();

            return listaPreguntas;

        } //Fin

        //Registra pregunta
        public void registrarPregunta(Pregunta nuevaPregunta, IConfiguration configuracionP)
        {

            var conexion = new SqlConnection(configuracionP.GetSection("ConnectionStrings:baseDeDatos").Value);

            conexion.Open();

            conexion.Execute("registrarPregunta", new { nuevaPregunta.nombreUsuario, nuevaPregunta.correo, nuevaPregunta.mensaje }, commandType: CommandType.StoredProcedure);

            conexion.Close();

        } //Fin

        public PreguntaRespuesta armarRespuesta(int idCodigo, string mensaje, List<Pregunta> listaDePreguntas)
        {

            PreguntaRespuesta respuesta = new PreguntaRespuesta();

            respuesta.idCodigo = idCodigo;
            respuesta.mensaje = mensaje;
            respuesta.listaDePreguntas = listaDePreguntas;

            return respuesta;

        } //Fin.
    }
}