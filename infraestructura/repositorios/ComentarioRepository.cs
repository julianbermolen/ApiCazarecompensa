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
		private readonly IUsuarioRepository _usuarioRepository;

		public ComentarioRepository(Contexto contexto, IUsuarioRepository usuarioRepository)
		{
            _contexto = contexto;
			_usuarioRepository = usuarioRepository;
		}

        public List<Comentario> ObtenerTodos()
		{
			return _contexto.Comentario
                .Include(x => x.Publicacion)
                .OrderByDescending(x => x.FechaCarga)
                .ToList();
		}

		public List<Comentario> ObtenerComentariosPorIdPublicacion(int idPublicacion)
		{
			return _contexto.Comentario
                .Where(x => x.IdPublicacion == idPublicacion)
                .OrderByDescending(x => x.FechaCarga)
                .ToList();
		}

		public Comentario ObtenerPorIdComentario(int idComentario)
		{
			return _contexto.Comentario
                .Include(x => x.Publicacion)
            	.FirstOrDefault(x => x.IdComentario == idComentario);
		}
        public List<Comentario> ObtenerBandejaEntrada(int idUsuarioReceptor)
		{
				return  _contexto.Comentario
				.Where(x => x.IdUsuarioReceptor == idUsuarioReceptor)
                .Include(x => x.Publicacion)
					.ThenInclude(pub => pub.Tesoro)
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