using aplicacion.servicios.abstracciones;
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

        [HttpGet]
        public JsonResult Get()
        {
            return Json(_usuarioService.ObtenerUsuarios());
        }
        
    }
}