namespace GoKartCR.Entities
{
    public class Tabla_Eventos
    {
        public string primerNombre { get; set; } = string.Empty;
        public string dia { get; set; } = string.Empty;
        public DateTime hora { get; set; }
    }


    public class TablaRespuesta
    {
     

        public int idCodigo { get; set; }
        public string mensaje { get; set; } = string.Empty;
        public List<Tabla_Eventos> listaEventos { get; set; }

    }


}
