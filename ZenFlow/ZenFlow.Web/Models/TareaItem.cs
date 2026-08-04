namespace ZenFlow.Web.Models;

public class TareaItem
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public DateTime FechaLimite { get; set; } = DateTime.Today;
    public bool Completada { get; set; }
}