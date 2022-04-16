namespace GoKartAPI.Entities
{
    public class RolUsuario
    {

        public int idRol { get; set; } = 0;
        public string nombreRol { get; set; } = String.Empty;
        public string descripcionRol { get; set; } = String.Empty; 

    }

    public class RolUsuarioRespuesta
    {

        public int idCodigo { get; set; }
        public string mensaje { get; set; }
        public List<RolUsuario> listaRolesDeUsuario { get; set; }


    }

}

