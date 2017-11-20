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
		private readonly IPublicacionRepository _publicacionRepository;

		public ComentarioRepository(Contexto contexto, IUsuarioRepository usuarioRepository, IPublicacionRepository publicacionRepository)
		{
            _contexto = contexto;
			_usuarioRepository = usuarioRepository;
			_publicacionRepository = publicacionRepository;
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

        public List<Dictionary<int, List<Comentario>>> ObtenerBandejaEntrada(int idUsuario)
		{

			// Se obtienen todas las publicaciones del usuario
			int[] publicaciones = _contexto.Publicacion 
				.Include(t => t.Tesoro)
				.Where(x => x.Tesoro.IdUsuario == idUsuario)
				.Select(x => x.IdPublicacion)
				.ToArray();

			if(publicaciones.Length == 0)
			{
				return new List<Dictionary<int, List<Comentario>>>();
			}

			var conversaciones = _contexto.Comentario
			.Where(x => publicaciones.Contains(x.IdPublicacion))
			.OrderByDescending(x => x.FechaCarga)
			.OrderBy(x => x.NumeroConversacion)
			.ToList();

			if(conversaciones.Count() == 0)
			{
				return new List<Dictionary<int, List<Comentario>>>();
			}

			List<Dictionary<int, List<Comentario>>> lista = new List<Dictionary<int, List<Comentario>>>();
			var listaConversaciones = new List<Comentario>();

			var conversacionAnterior = conversaciones.First() ?? new Comentario();

			foreach(var conversacion in conversaciones)
			{
				if(conversacionAnterior.NumeroConversacion == conversacion.NumeroConversacion)
				{
					listaConversaciones.Add(conversacion);

				}
				else
				{
					var diccionario = new Dictionary<int, List<Comentario>>();
					List<Comentario> listaComversacionesTemp = new List<Comentario>();
					listaComversacionesTemp.AddRange(listaConversaciones);
					diccionario.Add(conversacionAnterior.NumeroConversacion, listaComversacionesTemp);
					lista.Add(diccionario);

					conversacionAnterior = conversacion;
					listaConversaciones.Clear();
					listaConversaciones.Add(conversacion);
				}

			}

			var dic = new Dictionary<int, List<Comentario>>();
			dic.Add(conversacionAnterior.NumeroConversacion, listaConversaciones);
			lista.Add(dic);

			return lista;

		}

        public void Guardar(Comentario comentario)
		{
			comentario.FechaCarga = DateTime.Now;

			ObtenerNumeroConversacion(comentario);

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

		private void ObtenerNumeroConversacion(Comentario comentario)
		{
			var  numeroConversacion = _contexto
				.Comentario.Where(x => x.IdPublicacion == comentario.IdPublicacion 
				&& (x.IdUsuarioEmisor == comentario.IdUsuarioEmisor && x.IdUsuarioReceptor == comentario.IdUsuarioReceptor)  ||
				 (x.IdUsuarioEmisor == comentario.IdUsuarioReceptor && x.IdUsuarioReceptor == comentario.IdUsuarioEmisor))
				.FirstOrDefault();

			if (numeroConversacion == null)
			{
				var num = _contexto.Comentario.OrderByDescending(x => x.NumeroConversacion).FirstOrDefault() == null ? 1 : _contexto.Comentario.OrderByDescending(x => x.NumeroConversacion).First().NumeroConversacion;

				if(num == 1)
				{
					comentario.NumeroConversacion = 1;
				}
				else
				{
					comentario.NumeroConversacion =  num++;
				}
			}
			else
			{
				comentario.NumeroConversacion = numeroConversacion.NumeroConversacion;
			}
			
		}
	}
}