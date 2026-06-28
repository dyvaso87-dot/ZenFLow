using Microsoft.AspNetCore.Mvc;
using ZenFlow.Logica;
using ZenFlow.Modelos;

namespace ZenFlow.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HabitosController : ControllerBase
    {
        private readonly IGestorHabitos _gestor;  // ← interfaz

        public HabitosController(IGestorHabitos gestor)
        {
            _gestor = gestor;
        }

        [HttpGet]
        public ActionResult<List<Habito>> GetTodos()
        {
            try { return Ok(_gestor.ObtenerTodos()); }
            catch (UnauthorizedAccessException) { return Unauthorized("Token inválido."); }
        }

        [HttpPost]
        public ActionResult Crear([FromBody] CrearHabitoRequest request)
        {
            try
            {
                _gestor.AgregarHabito(request.Nombre);
                return Created("/api/habitos", null);
            }
            catch (UnauthorizedAccessException) { return Unauthorized("Token inválido."); }
        }

        [HttpPut("{id}/cumplir")]
        public ActionResult Cumplir(int id)
        {
            try { _gestor.RegistrarCumplimiento(id); return NoContent(); }
            catch (UnauthorizedAccessException) { return Unauthorized("Token inválido."); }
        }

        [HttpDelete("{id}")]
        public ActionResult Eliminar(int id)
        {
            try { _gestor.EliminarHabito(id); return NoContent(); }
            catch (UnauthorizedAccessException) { return Unauthorized("Token inválido."); }
        }
    }

    public record CrearHabitoRequest(string Nombre);
}