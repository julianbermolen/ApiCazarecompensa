using aplicacion.servicios.abstracciones;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService UsuarioService)
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