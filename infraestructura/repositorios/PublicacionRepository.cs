using System.Collections.Generic;
using System.Linq;
using infraestructura.entidades;
using infraestructura.repositorios.abstracciones;
using Microsoft.EntityFrameworkCore;

namespace infraestructura.repositorios
{
	public class PublicacionRepository : IPublicacionRepository
	{
        private readonly Contexto _contexto;
        public PublicacionRepository(Contexto contexto)
        {
            _contexto = contexto;
        }
        public List<Publicacion> ObtenerTodas()
		{
			return _contexto.Publicacion
                .Include(x => x.Tesoro)
                .ToList();
		}
		public List<Publicacion> ObtenerPorIdCategoria(int idCategoria)
		{
			return _contexto.Publicacion
                .Include(x => x.Tesoro)
                .Where(x => x.Tesoro.IdTesoroCategoria == idCategoria)
                .ToList();
		}

        public Publicacion ObtenerPorId(int id)
		{
			return _contexto.Publicacion
                .Include(x => x.Tesoro)
                .FirstOrDefault(x => x.IdPublicacion == id);
		}
		public void Eliminar(int id)
		{
            var publicacion = ObtenerPorId(id);
			_contexto.Publicacion.Remove(publicacion);
			_contexto.SaveChanges();
		}

		public void Guardar(Publicacion publicacion)
		{
			_contexto.Publicacion.Add(publicacion);
            _contexto.SaveChanges();
		}


	}
}