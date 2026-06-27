using ZenFlow.Modelos;
using ZenFlow.Repositorios;

namespace ZenFlow.Logica
{
    public class GestorHabitos
    {
        private readonly IHabitoRepo _repo;

        public GestorHabitos(IHabitoRepo repo)
        {
            _repo = repo;
        }

        public void AgregarHabito(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede estar vacío.");

            _repo.Guardar(new Habito
            {
                Nombre = nombre,
                RachaDias = 0,
                UltimoCumplimiento = DateTime.MinValue
            });
        }

        public void RegistrarCumplimiento(int id)
        {
            var habitos = _repo.ObtenerTodos();
            var habito = habitos.FirstOrDefault(h => h.Id == id);
            if (habito == null) return;

            var hoy = DateTime.Today;

            // Si ya lo cumplió hoy, no hace nada
            if (habito.UltimoCumplimiento.Date == hoy) return;

            // Calcula la racha
            if (habito.UltimoCumplimiento.Date == hoy.AddDays(-1))
                habito.RachaDias++;
            else
                habito.RachaDias = 1;

            habito.UltimoCumplimiento = hoy;

            // Solo actualiza, no elimina ni vuelve a crear
            _repo.Guardar(habito);
        }

        public List<Habito> ObtenerTodos() => _repo.ObtenerTodos();

        public void EliminarHabito(int id)
        {
            _repo.Eliminar(id);
        }
    }
}