using System;
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
		public TesorosController(ITesoroService tesoroService)
		{
            _tesoroService = tesoroService;
		}

        [HttpGet("obtener")]
        public JsonResult Get()
        {
            return Json(_tesoroService.ObtenerTodos());
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
                _tesoroService.Guardar(tesoro);
                return Json( new Respuesta { Exito = true, Mensaje = "Tesoro guardado con éxito"});
            }
            catch(Exception e)
            {
                return Json( new Respuesta { Exito = false, Mensaje = e.Message});
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
    }
}