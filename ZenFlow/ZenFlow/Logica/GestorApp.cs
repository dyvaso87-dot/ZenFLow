using ZenFlow.Modelos;
using ZenFlow.Repositorios;

namespace ZenFlow.Logica
{
    public class GestorApps
    {
        private readonly IAppBloqueadaRepo _repo;

        public GestorApps(IAppBloqueadaRepo repo)
        {
            _repo = repo;
        }

        public List<AppBloqueada> ObtenerTodas() => _repo.ObtenerTodas();

        public List<string> ObtenerProcesosActivos()
        {
            return _repo.ObtenerTodas()
                        .Where(a => a.Activa)
                        .Select(a => a.Proceso)
                        .ToList();
        }

        public void AgregarApp(string nombre, string proceso)
        {
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(proceso))
                throw new ArgumentException("El nombre y el proceso son requeridos.");

            _repo.Guardar(new AppBloqueada
            {
                Nombre = nombre,
                Proceso = proceso.Trim(),
                Activa = true
            });
        }

        public void ToggleActivar(int id)
        {
            var apps = _repo.ObtenerTodas();
            var app = apps.FirstOrDefault(a => a.Id == id);
            if (app == null) return;
            app.Activa = !app.Activa;
            _repo.Guardar(app);
        }

        public void Eliminar(int id) => _repo.Eliminar(id);
    }
}