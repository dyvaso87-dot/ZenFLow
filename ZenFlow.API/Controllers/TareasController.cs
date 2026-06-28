using Microsoft.AspNetCore.Mvc;
using ZenFlow.Logica;
using ZenFlow.Modelos;

namespace ZenFlow.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TareasController : ControllerBase
    {
        private readonly IGestorTareas _gestor;  // ← interfaz, no clase

        public TareasController(IGestorTareas gestor)
        {
            _gestor = gestor;
        }

        [HttpGet]
        public ActionResult<List<Tarea>> GetTodas()
        {
            try { return Ok(_gestor.ObtenerTareasPendientes()); }
            catch (UnauthorizedAccessException) { return Unauthorized("Token inválido."); }
        }

        [HttpPost]
        public ActionResult Crear([FromBody] CrearTareaRequest request)
        {
            try
            {
                _gestor.AgregarTarea(request.Titulo, request.FechaLimite);
                return Created("/api/tareas", null);
            }
            catch (UnauthorizedAccessException) { return Unauthorized("Token inválido."); }
        }

        [HttpDelete("{id}")]
        public ActionResult Eliminar(int id)
        {
            try { _gestor.EliminarTarea(id); return NoContent(); }
            catch (UnauthorizedAccessException) { return Unauthorized("Token inválido."); }
        }
    }

    public record CrearTareaRequest(string Titulo, DateTime FechaLimite);
}