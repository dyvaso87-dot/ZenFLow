using ZenFlow.Modelos;

namespace ZenFlow.Logica
{
    public interface IGestorHabitos
    {
        void AgregarHabito(string nombre);
        void RegistrarCumplimiento(int id);
        List<Habito> ObtenerTodos();
        void EliminarHabito(int id);
    }
}