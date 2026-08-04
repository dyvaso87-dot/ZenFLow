namespace ZenFlow.Web.Models;

public class AppBloqueada
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Proceso { get; set; } = string.Empty;
    public bool Activa { get; set; } = true;
}