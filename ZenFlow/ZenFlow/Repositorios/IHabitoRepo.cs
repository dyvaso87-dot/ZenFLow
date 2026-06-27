using ZenFlow.Modelos;

namespace ZenFlow.Repositorios
{
    public interface IHabitoRepo
    {
        void Guardar(Habito habito);
        List<Habito> ObtenerTodos();
        void Eliminar(int id);
    }
}