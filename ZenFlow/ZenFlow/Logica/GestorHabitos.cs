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
            if (habito.UltimoCumplimiento.Date == hoy.AddDays(-1))
                habito.RachaDias++;
            else if (habito.UltimoCumplimiento.Date != hoy)
                habito.RachaDias = 1;

            habito.UltimoCumplimiento = hoy;
            _repo.Eliminar(id);
            _repo.Guardar(habito);
        }

        public List<Habito> ObtenerTodos() => _repo.ObtenerTodos();
    }
}