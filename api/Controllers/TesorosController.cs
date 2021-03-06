using System;
using System.Linq;
using api.viewmodels;
using aplicacion.servicios.abstracciones;
using infraestructura.entidades;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    public class TesorosController : Controller
    {
        private readonly ITesoroService _tesoroService;
        private readonly IPublicacionService _publicacionService;
		public TesorosController(ITesoroService tesoroService, IPublicacionService publicacionService)
		{
            _tesoroService = tesoroService;
            _publicacionService = publicacionService;
		}

        [HttpGet("obtener")]
        public JsonResult Get()
        {
            return Json(_tesoroService.ObtenerTodos().Where(t => t.IdTesoroEstado == 1));
        }

        [HttpGet("Obtener/{id}")]
        public JsonResult ObtenerPorId(int id)
        {
            return Json(_tesoroService.ObtenerPorId(id));
        }

        [HttpGet("ObtenerPorIdCategoria/{id}")]
        public JsonResult ObtenerPorIdCategoria(int id)
        {
            return Json(_tesoroService.ObtenerPorIdCategoria(id));
        }

        [HttpGet("ObtenerCategoria")]
        public JsonResult ObtenerCategoria()
        {
            return Json(_tesoroService.ObtenerCategoria());
        }


        [HttpPost("guardar")]
        public JsonResult GuardarTesoro(Tesoro tesoro)
        {
            try
            {
                var tesoroGenerado = _tesoroService.Guardar(tesoro);
                _publicacionService.Guardar(new Publicacion { IdTesoro = tesoroGenerado.IdTesoro } );

                return Json( new Respuesta { Exito = true, Mensaje = "Tesoro guardado  y publicación generada con éxito"});
            }
            catch(Exception e)
            {
                return Json( new Respuesta { Exito = false, Mensaje = string.Format("MESSAGE: {0} STACKTRACE: {1} INNER EXCEPTION: {2}", e.Message, e.StackTrace, e.InnerException) });
            }
        }

        [HttpDelete("eliminar/{id}")]
        public JsonResult Eliminar(int id)
        {
            try
            {
                _tesoroService.Eliminar(id);
                return Json( new Respuesta { Exito = true, Mensaje = "Tesoro eliminado con éxito"});
            }
            catch(Exception e)
            {
                return Json( new Respuesta { Exito = false, Mensaje = e.Message});
            }
        }

        [HttpGet("ObtenerIdPublicacionPorIdTesoro/{id}")]
        public JsonResult ObtenerIdPublicacionPorIdTesoro(int id)
        {
            try {
                return Json(new {IdPublicacion = _tesoroService.ObtenerIdPublicacionPorIdTesoro(id)});
            }
            catch(Exception e) {
                return Json( new Respuesta { Exito = false, Mensaje = "Ocurrio un error. Puede que el IdTesoro no exista"});
            }
        }

        [HttpPost("CambiarEstadoTesoro")]
        public JsonResult CambiarEstadoTesoro(int idTesoro, int idEstado)
        {
            try
            {
                _tesoroService.CambiarEstadoTesoro(idTesoro, idEstado);
                return Json( new Respuesta { Exito = true, Mensaje = "Tesoro cambiado de estado con éxito"});
            }
            catch(Exception e)
            {
                return Json( new Respuesta { Exito = false, Mensaje = e.Message});
            }
        }
    }
}