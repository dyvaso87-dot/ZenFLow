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

            // Si ya existe (tiene Id), actualiza — si no, crea uno nuevo
            var existente = habitos.FirstOrDefault(h => h.Id == habito.Id);
            if (existente != null)
            {
                habitos.Remove(existente);
                habitos.Add(habito);
            }
            else
            {
                // Genera un Id único basado en el máximo existente
                habito.Id = habitos.Count > 0 ? habitos.Max(h => h.Id) + 1 : 1;
                habitos.Add(habito);
            }

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