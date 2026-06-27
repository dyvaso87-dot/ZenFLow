using System.IO;
using System.Text.Json;
using ZenFlow.Modelos;
using ZenFlow.Repositorios;

namespace ZenFlow.Datos
{
    public class HabitoRepoJson : IHabitoRepo
    {
        private readonly string _ruta = "habitos.json";

        public List<Habito> ObtenerTodos()
        {
            if (!File.Exists(_ruta)) return new List<Habito>();
            var json = File.ReadAllText(_ruta);
            return JsonSerializer.Deserialize<List<Habito>>(json) ?? new List<Habito>();
        }

        public void Guardar(Habito habito)
        {
            var habitos = ObtenerTodos();
            habito.Id = habitos.Count + 1;
            habitos.Add(habito);
            File.WriteAllText(_ruta, JsonSerializer.Serialize(habitos));
        }

        public void Eliminar(int id)
        {
            var habitos = ObtenerTodos();
            habitos.RemoveAll(h => h.Id == id);
            File.WriteAllText(_ruta, JsonSerializer.Serialize(habitos));
        }
    }
}