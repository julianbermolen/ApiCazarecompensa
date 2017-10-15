using System.Collections.Generic;
using aplicacion.servicios.abstracciones;
using infraestructura.entidades;
using infraestructura.repositorios.abstracciones;

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
        public void Guardar(Tesoro tesoro)
		{
			_tesoroRepository.Guardar(tesoro);
		}

		public List<Tesoro> ObtenerPorIdCategoria(int idCategoria)
		{
			return _tesoroRepository.ObtenerPorIdCategoria(idCategoria);
		}
	}
}