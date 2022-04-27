namespace GoKartCR.Entities
{
    public class Compra
    {
        public int idReserva { get; set; }
        public decimal precioFinal { get; set; }

    } //Fin.

    public class CompraRespuesta
    {

        public int idCodigo { get; set; }
        public string mensaje { get; set; }
        public List<Compra> compras { get; set; } = new List<Compra>();

    }

} //Fin.
