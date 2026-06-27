using System.IO;
using System.Text.Json;
using ZenFlow.Modelos;
using ZenFlow.Repositorios;

namespace ZenFlow.Datos
{
    public class TareaRepoJson : ITareaRepo
    {
        private readonly string _ruta = "tareas.json";

        public List<Tarea> ObtenerTodas()
        {
            if (!File.Exists(_ruta)) return new List<Tarea>();
            var json = File.ReadAllText(_ruta);
            return JsonSerializer.Deserialize<List<Tarea>>(json) ?? new List<Tarea>();
        }

        public void Guardar(Tarea tarea)
        {
            var tareas = ObtenerTodas();
            tarea.Id = tareas.Count + 1;
            tareas.Add(tarea);
            File.WriteAllText(_ruta, JsonSerializer.Serialize(tareas));
        }

        public void Eliminar(int id)
        {
            var tareas = ObtenerTodas();
            tareas.RemoveAll(t => t.Id == id);
            File.WriteAllText(_ruta, JsonSerializer.Serialize(tareas));
        }
    }
}