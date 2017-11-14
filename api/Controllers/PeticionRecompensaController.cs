using System;
using System.Linq;
using System.Collections.Generic;
using api.viewmodels;
using aplicacion.servicios.abstracciones;
using infraestructura.entidades;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    public class PeticionRecompensaController : Controller
    {
        private readonly IPeticionRecompensaService _peticionRecompensaService;
    private readonly ITesoroService _tesoroService;

        public PeticionRecompensaController(IPeticionRecompensaService peticionRecompensaService, ITesoroService tesoroService)
        {
            _peticionRecompensaService = peticionRecompensaService;
            _tesoroService = tesoroService;
        }

		[HttpGet("obtenerPorIdUsuario/{id}")]
        public JsonResult ObtenerPorIdUsuario(int id)
        {
            var resultado = new List<PeticionRecompensa>();

            var tesoros = _tesoroService.ObtenerTodos().Where(x => x.IdUsuario == id);

            var peticiones  = _peticionRecompensaService.ObtenerTodas();

            if(tesoros != null && tesoros.Count() > 0)
            {
                foreach (PeticionRecompensa peticion in peticiones)
                {
                    if(tesoros.Any(t => t.IdTesoro == peticion.IdTesoro)) {
                        resultado.Add(peticion);
                    }
                }
            }

            return Json(resultado);
            
        }

		[HttpPost("guardar")]
        public JsonResult Guardar(int idUsuario, int idTesoro, int estado)
        {
            var peticion = new PeticionRecompensa
            {
                IdUsuario = idUsuario,
                IdTesoro = idTesoro,
                Estado = estado
            };

            try
            {
                _peticionRecompensaService.Guardar(peticion);
                return Json(new Respuesta{});
            }
            catch(Exception e)
            {
                return Json(new Respuesta{});
            }
        }
        
		[HttpPost("actualizarEstado")]
        public JsonResult Actualizar(int idUsuario, int idTesoro, int estado)
        {
            try
            {
                _peticionRecompensaService.ActualizarEstado(idUsuario, idTesoro, estado);
                return Json(new Respuesta{});
            }
            catch(Exception e)
            {
                return Json(new Respuesta{});
            }
        }
    }
}