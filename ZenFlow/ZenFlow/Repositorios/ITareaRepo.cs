using ZenFlow.Modelos;

namespace ZenFlow.Repositorios
{
    public interface ITareaRepo
    {
        void Guardar(Tarea tarea);
        List<Tarea> ObtenerTodas();
        void Eliminar(int id);
    }
}