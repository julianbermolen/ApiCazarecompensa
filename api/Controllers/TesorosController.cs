using System;
using api.viewmodels;
using aplicacion.servicios.abstracciones;
using infraestructura.entidades;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;

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

            GuardarImagenesEnDisco(tesoro);
            
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

        private void GuardarImagenesEnDisco(Tesoro tesoro)
        {
            if(!string.IsNullOrEmpty(tesoro.Imagen1))
            {
                var imagen = tesoro.Imagen1.Replace("data:image/jpeg;base64,", string.Empty);

                using (Image<Rgba32> image = Image.Load<Rgba32>(Convert.FromBase64String(imagen)))
                {
                    image.Save("/imagenes/tesoros/1/imagen1.jpg"); // el 1 podría ser el id del tesoro
                    tesoro.Imagen1 = "/imagenes/tesoros/1/imagen1.jpg"; // piso el encoding con el path de la imagen ya generada
                }
            }
            if(!string.IsNullOrEmpty(tesoro.Imagen2))
            {
                var imagen = tesoro.Imagen2.Replace("data:image/jpeg;base64,", string.Empty);

                using (Image<Rgba32> image = Image.Load<Rgba32>(Convert.FromBase64String(imagen)))
                {
                    image.Save("/imagenes/tesoros/1/imagen2.jpg"); // el 1 podría ser el id del tesoro
                    tesoro.Imagen2 = "/imagenes/tesoros/1/imagen2.jpg"; // piso el encoding con el path de la imagen ya generada
                }
            }
            if(!string.IsNullOrEmpty(tesoro.Imagen3))
            {
                var imagen = tesoro.Imagen3.Replace("data:image/jpeg;base64,", string.Empty);

                using (Image<Rgba32> image = Image.Load<Rgba32>(Convert.FromBase64String(imagen)))
                {
                    image.Save("/imagenes/tesoros/1/imagen3.jpg"); // el 1 podría ser el id del tesoro
                    tesoro.Imagen3 = "/imagenes/tesoros/1/imagen3.jpg"; // piso el encoding con el path de la imagen ya generada
                }
            }
        }

    }
}