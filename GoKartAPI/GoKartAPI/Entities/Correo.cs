namespace GoKartAPI.Entities
{
    public class Correo
    {
        public string Remitente { get; set; } = String.Empty;
        public string Destinatario { get; set; } = String.Empty;
        public string Asunto { get; set; } = String.Empty;
        public string Mensaje { get; set; } = String.Empty;


        public class CorreoRespuesta
        {

            public int idCodigo { get; set; }
            public string mensaje { get; set; } = string.Empty;

        } //Fin. 

    }
}
