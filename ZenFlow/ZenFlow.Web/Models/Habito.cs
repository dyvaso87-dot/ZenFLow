namespace ZenFlow.Web.Models;

public class Habito
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int RachaDias { get; set; }
    public bool CumplidoHoy { get; set; }
}