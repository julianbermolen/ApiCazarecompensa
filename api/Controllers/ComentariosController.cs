using System;
using api.viewmodels;
using aplicacion.servicios.abstracciones;
using infraestructura.entidades;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    public class ComentariosController : Controller
    {
        private readonly IComentarioService _comentarioService;
        public ComentariosController(IComentarioService comentarioService)
        {
            _comentarioService = comentarioService;
        }

        [HttpGet("obtener")]
        public JsonResult Obtener()
        {
            return Json(_comentarioService.ObtenerTodos());
        }

		[HttpGet("obtener/{id}")]
        public JsonResult ObtenerPorIdComentario(int id)
        {
            return Json(_comentarioService.ObtenerPorIdComentario(id));
        }

		[HttpGet("obtener/publicacion/{id}")]
        public JsonResult ObtenerPorIdPublicacion(int id)
        {
            return Json(_comentarioService.ObtenerComentariosPorIdPublicacion(id));
        }

		[HttpGet("obtener/bandejaEntrada/{id}")]
        public JsonResult ObtenerBandejaEntrada(int id)
        {
            return Json(_comentarioService.ObtenerBandejaEntrada(id));
        }

        [HttpPost("cambiarEstadoALeido")]
        public JsonResult CambiarAComentarioLeido(int id)
        {
            try
            {
                _comentarioService.CambiarAComentarioLeido(id);
                return Json( new Respuesta { Exito = true, Mensaje = "Comentario modificado a leido con éxito"});
            }
            catch(Exception e)
            {
                return Json( new Respuesta { Exito = false, Mensaje = e.Message});
            }

        }

        [HttpPost("guardar")]
        public JsonResult GuardarPublicacion(Comentario comentario)
        {
            try
            {
                _comentarioService.Guardar(comentario);
                return Json( new Respuesta { Exito = true, Mensaje = "Comentario guardado con éxito"});
            }
            catch(Exception e)
            {
                return Json( new Respuesta { Exito = false, Mensaje = e.Message});
            }
        }
    }
}