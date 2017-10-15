using aplicacion.servicios.abstracciones;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System;
using api.viewmodels;
using infraestructura.entidades;

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

        [HttpPost("guardar")]
        public JsonResult Guardar(Usuario usuario)
        {
            try
            {
                _usuarioService.Guardar(usuario);
                return Json( new Respuesta { Exito = true, Mensaje = "Usuario guardado con éxito"});
            }
            catch(Exception e)
            {
                return Json( new Respuesta { Exito = false, Mensaje = e.Message});
            }
        }

        [HttpDelete("eliminarporid/{id}")]
        public JsonResult EliminarUsuarioPorId(int id)
        {
            try
            {
                _usuarioService.EliminarUsuario(id);
                return Json( new Respuesta { Exito = true, Mensaje = "Usuario eliminado con éxito"});

            }
            catch(Exception e)
            {
                return Json( new Respuesta { Exito = true, Mensaje = e.Message});
            }
        }
        
    }
}