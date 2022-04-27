namespace GoKartCR.Entities
{
    public class ReservaAPagar
    {

        public int idReserva { get; set; }
        public DateTime fecha { get; set; }
        public string nombrePaquete { get; set; }
        public TimeSpan tiempoOfrecidoPaquete { get; set; }
        public string descripcionPaquete { get; set; }
        public int cantidadUsuariosPaquete { get; set; }
        public decimal costoPaqueteSinIVA { get; set; }
        public decimal costoPaqueteConIVA { get; set; }

    } //Fin.

    public class ReservaAPagarRespuesta
    {

        public int idCodigo { get; set; }
        public string mensaje { get; set; }
        public List<ReservaAPagar> reserverAPagar { get; set; } = new List<ReservaAPagar>();

    }


} //Fin
