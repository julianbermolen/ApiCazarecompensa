using System;
using System.Collections.Generic;
using System.IO;
using aplicacion.servicios.abstracciones;
using infraestructura.entidades;
using infraestructura.repositorios.abstracciones;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace aplicacion.servicios
{
	public class TesoroService : ITesoroService
	{
		private readonly ITesoroRepository _tesoroRepository;
		
		public TesoroService(ITesoroRepository tesoroRepository)
		{
			_tesoroRepository = tesoroRepository;
		}

		public List<Tesoro> ObtenerTodos()
		{
			return _tesoroRepository.ObtenerTodos();
		}

        public Tesoro ObtenerPorId(int id)
		{
			return _tesoroRepository.ObtenerPorId(id);
		}
        public Tesoro Guardar(Tesoro tesoro)
		{
			return  _tesoroRepository.Guardar(tesoro);
		}

		public List<TesoroCategoria> ObtenerCategoria()
		{
			return _tesoroRepository.ObtenerCategoria();
		}
		
		public List<Tesoro> ObtenerPorIdCategoria(int idCategoria)
		{
			return _tesoroRepository.ObtenerPorIdCategoria(idCategoria);
		}
		public void Eliminar(int id)
		{
			if(ExisteTesoro(id))
			{
				_tesoroRepository.Eliminar(id);
			}
			else
			{
				throw new Exception("El tesoro no existe");
			}
		}

		private bool ExisteTesoro(int id)
		{
			return _tesoroRepository.ObtenerPorId(id) != null;
		}

		public int ObtenerIdPublicacionPorIdTesoro(int id)
		{
			return _tesoroRepository.ObtenerIdPublicacionPorIdTesoro(id);
		}

		public void CambiarEstadoTesoro(int idTesoro, int idEstado)
		{ 
			_tesoroRepository.CambiarEstadoTesoro(idTesoro, idEstado);
		}

	}
}