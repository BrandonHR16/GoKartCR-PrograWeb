namespace GoKartCR.Entities
{
    public class Reserva
    {

        public int idReserva { get; set; }
        public DateTime fecha { get; set; }
        public int idUsuario { get; set; }
        public int idPaquete { get; set; }
        public Boolean mannana { get; set; } = true;
        public Boolean tarde { get; set; } = true;
        public Boolean noche { get; set; } = true;

        public class ReservaRespuesta
        {
            public int idRespuesta { get; set; }
            public string mensaje { get; set; }
            public List<Reserva> listaDeReservas { get; set; }
        }

    }
}
