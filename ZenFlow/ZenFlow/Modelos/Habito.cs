namespace ZenFlow.Modelos
{
    public class Habito
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public int RachaDias { get; set; }
        public DateTime UltimoCumplimiento { get; set; }
    }
}