using System.Collections.Generic;
using System;
using System.Linq;
using infraestructura.entidades;
using infraestructura.repositorios.abstracciones;
using Microsoft.EntityFrameworkCore;

namespace infraestructura.repositorios
{
	public class ComentarioRepository : IComentarioRepository
	{
        private readonly Contexto _contexto;
		public ComentarioRepository(Contexto contexto)
		{
            _contexto = contexto;
		}

        public List<Comentario> ObtenerTodos()
		{
			return _contexto.Comentario
                .Include(x => x.Usuario)
                .Include(x => x.Publicacion)
                .OrderByDescending(x => x.FechaCarga)
                .ToList();
		}

		public List<Comentario> ObtenerComentariosPorIdPublicacion(int idPublicacion)
		{
			return _contexto.Comentario
                .Where(x => x.IdPublicacion == idPublicacion)
                .Include(x => x.Usuario)
                .OrderByDescending(x => x.FechaCarga)
                .ToList();
		}

		public Comentario ObtenerPorIdComentario(int idComentario)
		{
			return _contexto.Comentario
			    .Include(x => x.Usuario)
                .Include(x => x.Publicacion)
            	.FirstOrDefault(x => x.IdComentario == idComentario);
		}
        public List<Comentario> ObtenerBandejaEntrada(int idUsuario)
		{
			return _contexto.Comentario
                .Include(x => x.Usuario)
                .Include(x => x.Publicacion)
				.OrderByDescending(x => x.MensajeLeido)
                .OrderByDescending(x => x.FechaCarga)
                .ToList();
		}
        public void Guardar(Comentario comentario)
		{
			comentario.FechaCarga = DateTime.Now;
			_contexto.Comentario.Add(comentario);
            _contexto.SaveChanges();
		}

		public void CambiarAComentarioLeido(int idComentario)
		{
            var comentario = ObtenerPorIdComentario(idComentario);
			comentario.MensajeLeido = true;
			_contexto.Entry(comentario).State = EntityState.Modified;
			_contexto.SaveChanges();
		}
	}
}