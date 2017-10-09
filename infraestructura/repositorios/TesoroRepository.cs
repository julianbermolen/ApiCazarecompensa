using System.Collections.Generic;
using System.Linq;
using infraestructura.entidades;
using infraestructura.repositorios.abstracciones;

namespace infraestructura.repositorios
{
	public class TesoroRepository : ITesoroRepository
	{
        private readonly Contexto _contexto;
		public TesoroRepository(Contexto contexto)
		{
            _contexto = contexto;
		}

		public List<Tesoro> ObtenerTodos()
		{
			return _contexto.Tesoro.OrderByDescending(x => x.FechaCarga).ToList();
		}
		public Tesoro ObtenerPorId(int idTesoro)
		{
			return _contexto.Tesoro.FirstOrDefault(x => x.IdTesoro == idTesoro);
		}

        public List<Tesoro> ObtenerPorIdCategoria(int idCategoriaTesoro)
        {
            return _contexto.Tesoro
            .Where(x => x.IdCategoria == idCategoriaTesoro)
            .OrderByDescending(x => x.FechaCarga)
            .ToList();
        }
		public void Guardar(Tesoro tesoro)
		{
			_contexto.Tesoro.Add(tesoro);
            _contexto.SaveChanges();
		}
	}
}