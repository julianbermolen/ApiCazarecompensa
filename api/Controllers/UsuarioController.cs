using aplicacion.servicios.abstracciones;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System;
using api.viewmodels;

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

        [HttpDelete("eliminarporid/{id}")]
        public JsonResult EliminarUsuarioPorId(int id)
        {
            try
            {
                _usuarioService.EliminarUsuario(id);
                return Json( new Respuesta { Exito = true, Mensaje = "Tesoro guardado con éxito"});

            }
            catch(Exception e)
            {
                return Json( new Respuesta { Exito = true, Mensaje = e.Message});
            }
        }
        
    }
}