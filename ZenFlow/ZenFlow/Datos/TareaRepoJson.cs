using System.IO;
using System.Text.Json;
using ZenFlow.Modelos;
using ZenFlow.Repositorios;

namespace ZenFlow.Datos
{
    public class TareaRepoJson : ITareaRepo
    {
        // ── SINGLETON ──────────────────────────────────────
        private static TareaRepoJson? _instancia;
        private static readonly object _lock = new object();

        private TareaRepoJson() { }  // constructor privado

        public static TareaRepoJson Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    lock (_lock)
                    {
                        if (_instancia == null)
                            _instancia = new TareaRepoJson();
                    }
                }
                return _instancia;
            }
        }
        // ───────────────────────────────────────────────────

        private readonly string _ruta = "tareas.json";

        public List<Tarea> ObtenerTodas()
        {
            lock (_lock)
            {
                if (!File.Exists(_ruta)) return new List<Tarea>();
                var json = File.ReadAllText(_ruta);
                return JsonSerializer.Deserialize<List<Tarea>>(json)
                       ?? new List<Tarea>();
            }
        }

        public void Guardar(Tarea tarea)
        {
            lock (_lock)
            {
                var tareas = ObtenerTodasSinLock();
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
        }

        public void Eliminar(int id)
        {
            lock (_lock)
            {
                var tareas = ObtenerTodasSinLock();
                tareas.RemoveAll(t => t.Id == id);
                File.WriteAllText(_ruta, JsonSerializer.Serialize(tareas));
            }
        }

        // Versión interna sin lock (para llamar desde dentro del lock)
        private List<Tarea> ObtenerTodasSinLock()
        {
            if (!File.Exists(_ruta)) return new List<Tarea>();
            var json = File.ReadAllText(_ruta);
            return JsonSerializer.Deserialize<List<Tarea>>(json)
                   ?? new List<Tarea>();
        }
    }
}