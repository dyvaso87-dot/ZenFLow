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

            var existente = tareas.FirstOrDefault(t => t.Id == tarea.Id);
            if (existente != null)
            {
                tareas.Remove(existente);
                tareas.Add(tarea);
            }
            else
            {
                tarea.Id = tareas.Count > 0 ? tareas.Max(t => t.Id) + 1 : 1;
                tareas.Add(tarea);
            }

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