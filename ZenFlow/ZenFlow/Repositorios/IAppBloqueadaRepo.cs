using ZenFlow.Modelos;

namespace ZenFlow.Repositorios
{
    public interface IAppBloqueadaRepo
    {
        void Guardar(AppBloqueada app);
        List<AppBloqueada> ObtenerTodas();
        void Eliminar(int id);
    }
}