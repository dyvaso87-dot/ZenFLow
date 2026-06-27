using ZenFlow.Modelos;
using ZenFlow.Repositorios;

namespace ZenFlow.Logica
{
    public class GestorTareas : IGestorTareas  // ← agrega esto
    {
        private readonly ITareaRepo _repo;

        public GestorTareas(ITareaRepo repo)
        {
            _repo = repo;
        }

        public void AgregarTarea(string titulo, DateTime fechaLimite)
        {
            if (string.IsNullOrWhiteSpace(titulo))
                throw new ArgumentException("El título no puede estar vacío.");

            _repo.Guardar(new Tarea
            {
                Titulo = titulo,
                FechaLimite = fechaLimite,
                Completada = false
            });
        }

        public List<Tarea> ObtenerTareasPendientes()
        {
            return _repo.ObtenerTodas()
                        .Where(t => !t.Completada)
                        .ToList();
        }

        public void CompletarTarea(int id)
        {
            var tareas = _repo.ObtenerTodas();
            var tarea = tareas.FirstOrDefault(t => t.Id == id);
            if (tarea != null)
            {
                tarea.Completada = true;
                _repo.Eliminar(id);
                _repo.Guardar(tarea);
            }
        }

        public void EliminarTarea(int id)
        {
            _repo.Eliminar(id);
        }
    }
}