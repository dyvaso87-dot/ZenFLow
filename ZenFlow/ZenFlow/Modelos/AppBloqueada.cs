namespace ZenFlow.Modelos
{
    public class AppBloqueada
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";      // nombre visible: "Spotify"
        public string Proceso { get; set; } = "";     // nombre real: "Spotify"
        public bool Activa { get; set; } = true;      // si se bloquea o no
    }
}