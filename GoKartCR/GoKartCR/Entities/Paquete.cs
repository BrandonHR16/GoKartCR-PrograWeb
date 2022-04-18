﻿namespace GoKartCR.Entities
{
    public class Paquete
    {
        public int idPista { get; set; } = 0;
        public string nombre { get; set; } = String.Empty;
        public string descripcion { get; set; } = String.Empty;
        public int costo { get; set; } = 0;
        public TimeSpan tiempoOfrecido { get; set; } 
        public int cantidadUsuarios { get; set; } = 0;
        public byte[] imagen { get; set; }
        public string nombrePista { get; set; } = String.Empty;
    }

    public class PaqueteRespuesta
    {

        public int idCodigo { get; set; }
        public string mensaje { get; set; }
        public List<Paquete> listaDePaquetes { get; set; }

    }
}