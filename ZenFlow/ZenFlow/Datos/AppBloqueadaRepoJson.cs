using System.IO;
using System.Text.Json;
using ZenFlow.Modelos;
using ZenFlow.Repositorios;

namespace ZenFlow.Datos
{
    public class AppBloqueadaRepoJson : IAppBloqueadaRepo
    {
        // SINGLETON
        private static AppBloqueadaRepoJson? _instancia;
        private static readonly object _lock = new object();

        private AppBloqueadaRepoJson() { }

        public static AppBloqueadaRepoJson Instancia
        {
            get
            {
                if (_instancia == null)
                    lock (_lock)
                        if (_instancia == null)
                            _instancia = new AppBloqueadaRepoJson();
                return _instancia;
            }
        }

        private readonly string _ruta = "apps.json";

        public List<AppBloqueada> ObtenerTodas()
        {
            lock (_lock)
            {
                if (!File.Exists(_ruta)) return Defaults();
                var json = File.ReadAllText(_ruta);
                return JsonSerializer.Deserialize<List<AppBloqueada>>(json)
                       ?? Defaults();
            }
        }

        public void Guardar(AppBloqueada app)
        {
            lock (_lock)
            {
                var apps = ObtenerTodasSinLock();
                var existente = apps.FirstOrDefault(a => a.Id == app.Id);
                if (existente != null)
                {
                    apps.Remove(existente);
                    apps.Add(app);
                }
                else
                {
                    app.Id = apps.Count > 0 ? apps.Max(a => a.Id) + 1 : 1;
                    apps.Add(app);
                }
                File.WriteAllText(_ruta, JsonSerializer.Serialize(apps));
            }
        }

        public void Eliminar(int id)
        {
            lock (_lock)
            {
                var apps = ObtenerTodasSinLock();
                apps.RemoveAll(a => a.Id == id);
                File.WriteAllText(_ruta, JsonSerializer.Serialize(apps));
            }
        }

        private List<AppBloqueada> ObtenerTodasSinLock()
        {
            if (!File.Exists(_ruta)) return Defaults();
            var json = File.ReadAllText(_ruta);
            return JsonSerializer.Deserialize<List<AppBloqueada>>(json) ?? Defaults();
        }

        // Apps preconfiguradas con nombres de proceso correctos
        private List<AppBloqueada> Defaults() => new()
        {
            new() { Id=1, Nombre="Google Chrome",  Proceso="chrome",   Activa=true  },
            new() { Id=2, Nombre="Steam",           Proceso="steam",    Activa=true  },
            new() { Id=3, Nombre="Spotify",         Proceso="Spotify",  Activa=false },
            new() { Id=4, Nombre="Discord",         Proceso="Discord",  Activa=false },
            new() { Id=5, Nombre="Microsoft Edge",  Proceso="msedge",   Activa=false },
            new() { Id=6, Nombre="WhatsApp",        Proceso="WhatsApp", Activa=false },
        };
    }
}