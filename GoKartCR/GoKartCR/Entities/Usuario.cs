namespace GoKartCR.Entities
{
    public class Usuario
    {

        public int idUsuario { get; set; } = 0;
        public string cedulaUsuario { get; set; } = String.Empty;
        public string primerNombre { get; set; } = String.Empty;
        public string primerApellido { get; set; } = String.Empty;
        public string? segundoApellido { get; set; }
        public string correoElectronico { get; set; } = String.Empty;
        public string contrasennia { get; set; } = String.Empty;
        public string telefono { get; set; } = String.Empty;
        public string direccionResidencia { get; set; } = String.Empty;
        public string? tokenRecuperacion { get; set; }
        public int idRol { get; set; } = 0;

    } //Fin.

    public class UsuarioRespuesta
    {

        public int idCodigo { get; set; }
        public string mensaje { get; set; } = string.Empty;
        public List<Usuario> listaUsuarios { get; set; } = new List<Usuario>();

    }
}
