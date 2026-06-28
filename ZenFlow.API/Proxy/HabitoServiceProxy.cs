using ZenFlow.Logica;
using ZenFlow.Modelos;

namespace ZenFlow.API.Proxy
{
    public class HabitoServiceProxy : IGestorHabitos
    {
        private readonly GestorHabitos _gestor;
        private readonly IHttpContextAccessor _http;
        private readonly string _tokenEsperado = "zenflow-token-2025";
        private readonly string _bitacora = "bitacora.log";

        public HabitoServiceProxy(GestorHabitos gestor, IHttpContextAccessor http)
        {
            _gestor = gestor;
            _http = http;
        }

        private void Verificar(string operacion)
        {
            var token = _http.HttpContext?.Request.Headers["Authorization"]
                        .ToString().Replace("Bearer ", "");

            if (token != _tokenEsperado)
                throw new UnauthorizedAccessException("Token inválido.");

            File.AppendAllText(_bitacora,
                $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {operacion}\n");
        }

        public void AgregarHabito(string nombre)
        {
            Verificar($"AgregarHabito: {nombre}");
            _gestor.AgregarHabito(nombre);
        }

        public void RegistrarCumplimiento(int id)
        {
            Verificar($"RegistrarCumplimiento: {id}");
            _gestor.RegistrarCumplimiento(id);
        }

        public List<Habito> ObtenerTodos()
        {
            Verificar("ObtenerHabitos");
            return _gestor.ObtenerTodos();
        }

        public void EliminarHabito(int id)
        {
            Verificar($"EliminarHabito: {id}");
            _gestor.EliminarHabito(id);
        }
    }
}