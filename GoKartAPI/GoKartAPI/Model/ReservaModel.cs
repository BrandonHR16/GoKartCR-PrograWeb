﻿using Dapper;
using GoKartAPI.Entities;
using System.Data;
using System.Data.SqlClient;

namespace GoKartAPI.Model
{
    public class ReservaModel
    {

        //reserva por su id
        public Reserva BuscarReserva(int idReserva, IConfiguration configuracionP)
        {

            var conexion = new SqlConnection(configuracionP.GetSection("ConnectionStrings:baseDeDatos").Value);

            conexion.Open();

            var reserva = conexion.Query<Reserva>("selectReservaPorID", new { idReserva }, commandType: CommandType.StoredProcedure).FirstOrDefault();

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
        public List<Reserva> ReservasPorFecha(string fecha,int id, IConfiguration configuracionP)
        {

            var conexion = new SqlConnection(configuracionP.GetSection("ConnectionStrings:baseDeDatos").Value);

            conexion.Open();

            var listaReservas = conexion.Query<Reserva>("selectReservaPorFecha", new { fecha,id }, commandType: CommandType.StoredProcedure).ToList();

            conexion.Close();

            return listaReservas;

        } //Fin

        public List<ReservaAPagar> getReservasaPagar(IConfiguration configuracionP, int idUsuario)
        {

            var conexion = new SqlConnection(configuracionP.GetSection("ConnectionStrings:baseDeDatos").Value);

            conexion.Open();

            var listaReservasAPagar = conexion.Query<ReservaAPagar>("getReservasaPagar", new { idUsuario }, commandType: CommandType.StoredProcedure).ToList();

            conexion.Close();

            return listaReservasAPagar;

        } //Fin

        public ReservaAPagarRespuesta armarRespuestaReservaAPagar(int idCodigo, string mensaje, List<ReservaAPagar> reservasAPagar)
        {

            ReservaAPagarRespuesta respuesta = new ReservaAPagarRespuesta();

            respuesta.idCodigo = idCodigo;
            respuesta.mensaje = mensaje;
            respuesta.reserverAPagar = reservasAPagar;

            return respuesta;

        } //Fin.

    }
}
