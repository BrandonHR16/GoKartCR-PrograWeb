namespace GoKartAPI.Entities
{
    public class UsuarioPistaRecord
    {

        public int idUsuario { get; set; } = 0;
        public int idPista { get; set; } = 0;
        public DateTime fecha { get; set; }
        public TimeSpan tiempo { get; set; }

    }

    public class UsuarioPistaRecordRespuesta
    {

        public int idCodigo { get; set; }
        public string mensaje { get; set; } = string.Empty;
        public List<UsuarioPistaRecord> listaRecords { get; set; } = new List<UsuarioPistaRecord>();

    }

}
