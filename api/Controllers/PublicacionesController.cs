using System;
using System.Collections.Generic;
using api.viewmodels;
using aplicacion.servicios.abstracciones;
using infraestructura.entidades;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    public class PublicacionesController : Controller
    {
        private readonly IPublicacionService _publicacionesService;
		public  PublicacionesController(IPublicacionService publicacionesService)
		{
            _publicacionesService = publicacionesService;
		}

		[HttpGet("obtener")]
        public JsonResult Get()
        {
            return Json(_publicacionesService.ObtenerTodas());
        }

		[HttpGet("obtener/{id}")]
        public JsonResult ObtenerPorIdPublicacion(int id)
        {
            return Json(_publicacionesService.ObtenerPorId(id));
        }

        [HttpPost("guardar")]
        public JsonResult GuardarPublicacion(Publicacion publicacion)
        {   
            try
            {
                _publicacionesService.Guardar(publicacion);
                return Json( new Respuesta { Exito = true, Mensaje = "Publicación guardada con éxito"});
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
                _publicacionesService.Eliminar(id);
                return Json( new Respuesta { Exito = true, Mensaje = "Publicación eliminada con éxito"});
            }
            catch(Exception e)
            {
                return Json( new Respuesta { Exito = false, Mensaje = e.Message});
            }
        }

    }
}