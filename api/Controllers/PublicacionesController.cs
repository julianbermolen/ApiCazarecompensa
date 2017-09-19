using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    public class PublicacionesController : Controller
    {
        [HttpGet]
        public JsonResult Get()
        {
            return Json(new { hola = 1 });
        }
        
    }
}