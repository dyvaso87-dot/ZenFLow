using System.IO;
using System.Text.Json;
using ZenFlow.Modelos;
using ZenFlow.Repositorios;

namespace ZenFlow.Datos
{
    public class HabitoRepoJson : IHabitoRepo
    {
        // ── SINGLETON ──────────────────────────────────────
        private static HabitoRepoJson? _instancia;
        private static readonly object _lock = new object();

        private HabitoRepoJson() { }

        public static HabitoRepoJson Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    lock (_lock)
                    {
                        if (_instancia == null)
                            _instancia = new HabitoRepoJson();
                    }
                }
                return _instancia;
            }
        }
        // ───────────────────────────────────────────────────

        private readonly string _ruta = "habitos.json";

        public List<Habito> ObtenerTodos()
        {
            lock (_lock)
            {
                if (!File.Exists(_ruta)) return new List<Habito>();
                var json = File.ReadAllText(_ruta);
                return JsonSerializer.Deserialize<List<Habito>>(json)
                       ?? new List<Habito>();
            }
        }

        public void Guardar(Habito habito)
        {
            lock (_lock)
            {
                var habitos = ObtenerTodosSinLock();
                var existente = habitos.FirstOrDefault(h => h.Id == habito.Id);
                if (existente != null)
                {
                    habitos.Remove(existente);
                    habitos.Add(habito);
                }
                else
                {
                    habito.Id = habitos.Count > 0 ? habitos.Max(h => h.Id) + 1 : 1;
                    habitos.Add(habito);
                }
                File.WriteAllText(_ruta, JsonSerializer.Serialize(habitos));
            }
        }

        public void Eliminar(int id)
        {
            lock (_lock)
            {
                var habitos = ObtenerTodosSinLock();
                habitos.RemoveAll(h => h.Id == id);
                File.WriteAllText(_ruta, JsonSerializer.Serialize(habitos));
            }
        }

        private List<Habito> ObtenerTodosSinLock()
        {
            if (!File.Exists(_ruta)) return new List<Habito>();
            var json = File.ReadAllText(_ruta);
            return JsonSerializer.Deserialize<List<Habito>>(json)
                   ?? new List<Habito>();
        }
    }
}