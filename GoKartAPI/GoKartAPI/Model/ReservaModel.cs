using Dapper;
using GoKartAPI.Entities;
using System.Data;
using System.Data.SqlClient;

namespace GoKartAPI.Model
{
    public class ReservaModel
    {

        //reserva por su id
        public Reserva BuscarReserva(int id, IConfiguration configuracionP)
        {

            var conexion = new SqlConnection(configuracionP.GetSection("ConnectionStrings:baseDeDatos").Value);

            conexion.Open();

            var reserva = conexion.Query<Reserva>("selectReservaPorID", new { id }, commandType: CommandType.StoredProcedure).FirstOrDefault();

            conexion.Close();

            return reserva;

        } //Fin

        //get all reservas
        public List<Reserva> Reservas(IConfiguration configuracionP)
        {

            var conexion = new SqlConnection(configuracionP.GetSection("ConnectionStrings:baseDeDatos").Value);

            conexion.Open();

            var listaReservas = conexion.Query<Reserva>("selectReserva", new { }, commandType: CommandType.StoredProcedure).ToList();

            conexion.Close();

            return listaReservas;

        } //Fin

        //registrar reserva
        public void AgregarReserva(Reserva nuevaReserva, IConfiguration configuracionP)
        {

            var conexion = new SqlConnection(configuracionP.GetSection("ConnectionStrings:baseDeDatos").Value);

            conexion.Open();

            conexion.Execute("agregarReserva", new { nuevaReserva.idUsuario, nuevaReserva.fecha, nuevaReserva.idPaquete }, commandType: CommandType.StoredProcedure);

            conexion.Close();

        } //Fin

        //selectReservaPorFecha
        public List<Reserva> ReservasPorFecha(string fecha, IConfiguration configuracionP)
        {

            var conexion = new SqlConnection(configuracionP.GetSection("ConnectionStrings:baseDeDatos").Value);

            conexion.Open();

            var listaReservas = conexion.Query<Reserva>("selectReservaPorFecha", new { fecha }, commandType: CommandType.StoredProcedure).ToList();

            conexion.Close();

            return listaReservas;

        } //Fin

    }
}
