using ZenFlow.Modelos;

namespace ZenFlow.Logica
{
    public interface IGestorTareas
    {
        void AgregarTarea(string titulo, DateTime fechaLimite);
        List<Tarea> ObtenerTareasPendientes();
        void CompletarTarea(int id);
        void EliminarTarea(int id);
    }
}