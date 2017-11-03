using System.Collections.Generic;
using System;
using System.Linq;
using infraestructura.entidades;
using infraestructura.repositorios.abstracciones;
using Microsoft.EntityFrameworkCore;

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
			return _contexto.Tesoro
			.Include(x => x.usuario)
			.Include(x => x.TesoroEstado)
			.Include(x => x.TesoroCategoria)
			.OrderByDescending(x => x.FechaCarga).ToList();
		}
		public Tesoro ObtenerPorId(int idTesoro)
		{
			return _contexto.Tesoro
				.Include(x => x.TesoroEstado)
				.Include(x => x.TesoroCategoria)
				.FirstOrDefault(x => x.IdTesoro == idTesoro);
		}

		public List<TesoroCategoria> ObtenerCategoria(){
			return _contexto.TesoroCategoria
			.ToList();
		}

        public List<Tesoro> ObtenerPorIdCategoria(int idCategoriaTesoro)
        {
            return _contexto.Tesoro
            .Where(x => x.IdTesoroCategoria == idCategoriaTesoro)
			.Include(x => x.TesoroEstado)
			.Include(x => x.TesoroCategoria)
            .OrderByDescending(x => x.FechaCarga)
            .ToList();
        }
		public Tesoro Guardar(Tesoro tesoro)
		{
			tesoro.FechaCarga = DateTime.Now;
			_contexto.Tesoro.Add(tesoro);
            _contexto.SaveChanges();
			return tesoro;
		}

		public void Eliminar(int id)
		{
			var tesoro = ObtenerPorId(id);
			_contexto.Tesoro.Remove(tesoro);
			_contexto.SaveChanges();
		}

		public int ObtenerIdPublicacionPorIdTesoro(int id)
		{
			return _contexto.Publicacion.FirstOrDefault(x => x.IdTesoro == id).IdPublicacion;
		}
	}
}