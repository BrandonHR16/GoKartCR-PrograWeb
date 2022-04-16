namespace GoKartAPI.Model
{
    public class Kart
    {

        public int idGoKart { get; set; } = 0;
        public string nombreKart { get; set; } = String.Empty;

    } //Fin. 

    public class KartRespuesta
    {

        public int idCodigo { get; set; }
        public string mensaje { get; set; } = string.Empty;
        public List<Kart> listaDeKarts { get; set; } = new List<Kart>();

    } //Fin. 

} //Fin.
