namespace GoKartCR.Entities
{
    public class Pregunta
    {
        public int idPregunta { get; set; } = 0;
        public string nombreUsuario { get; set; } = String.Empty;
        public string correo { get; set; } = String.Empty;
        public string mensaje { get; set; } = String.Empty;
    }

    public class PreguntaRespuesta
    {
        public int idCodigo { get; set; } = 0;
        public string mensaje { get; set; } = String.Empty;
        public List<Pregunta> listaDePreguntas { get; set; } = new List<Pregunta>();
    }
}