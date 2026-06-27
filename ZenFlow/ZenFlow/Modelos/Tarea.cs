namespace ZenFlow.Modelos
{
    public class Tarea
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = "";
        public DateTime FechaLimite { get; set; }
        public bool Completada { get; set; }
    }
}