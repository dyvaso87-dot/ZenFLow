using System.Net.Http.Headers;
using ZenFlow.Logica;
using ZenFlow.Modelos;

namespace ZenFlow.API.Proxy
{
    public class TareaServiceProxy : IGestorTareas
    {
        private readonly GestorTareas _gestor;
        private readonly IHttpContextAccessor _http;
        private readonly string _tokenEsperado = "zenflow-token-2025";
        private readonly string _bitacora = "bitacora.log";

        public TareaServiceProxy(GestorTareas gestor, IHttpContextAccessor http)
        {
            _gestor = gestor;
            _http = http;
        }

        private void Verificar(string operacion)
        {
            // 1. Leer token del header Authorization
            var token = _http.HttpContext?.Request.Headers["Authorization"]
                        .ToString().Replace("Bearer ", "");

            if (token != _tokenEsperado)
                throw new UnauthorizedAccessException("Token inválido.");

            // 2. Escribir en bitácora
            File.AppendAllText(_bitacora,
                $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {operacion}\n");
        }

        public void AgregarTarea(string titulo, DateTime fechaLimite)
        {
            Verificar($"AgregarTarea: {titulo}");
            _gestor.AgregarTarea(titulo, fechaLimite);
        }

        public List<Tarea> ObtenerTareasPendientes()
        {
            Verificar("ObtenerTareasPendientes");
            return _gestor.ObtenerTareasPendientes();
        }

        public void CompletarTarea(int id)
        {
            Verificar($"CompletarTarea: {id}");
            _gestor.CompletarTarea(id);
        }

        public void EliminarTarea(int id)
        {
            Verificar($"EliminarTarea: {id}");
            _gestor.EliminarTarea(id);
        }
    }
}