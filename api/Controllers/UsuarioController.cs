using aplicacion.servicios.abstracciones;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    public class UsuariosController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService UsuarioService)
        {
            _usuarioService = UsuarioService;
        }

        [HttpGet("obtener")]
        public JsonResult Get()
        {
            return Json(_usuarioService.ObtenerUsuarios());
        }

        [HttpGet("obtenerporid/{id}")]
        public JsonResult ObtenerUsuarioPorIdUsuario(int id)
        {
            return Json(_usuarioService.ObtenerUsuarios().First(x => x.IdUsuario == id));
        }
        
    }
}