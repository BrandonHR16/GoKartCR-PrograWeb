namespace GoKartCR.Entities
{
    public class Pista
    {
        public int idPista { get; set; } = 0;
        public string nombrePista { get; set; } = String.Empty;
        public decimal? distanciaMetros { get; set; }
        public int capacidadUsuarios { get; set; } = 0;
        public IFormFile file { get; set; }
        public byte[] Imagen { get; set; }
    }

    public class PistaRespuesta
    {
        public int idCodigo { get; set; }
        public string mensaje { get; set; }
        public List<Pista> listaDePistas { get; set; }

    }

}
