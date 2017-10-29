using System.Collections.Generic;
using aplicacion.servicios.abstracciones;
using infraestructura.entidades;
using infraestructura.repositorios.abstracciones;

namespace aplicacion.servicios
{
	public class ComentarioService : IComentarioService
	{
        private readonly IComentarioRepository _comentarioRepository;
        public ComentarioService(IComentarioRepository comentarioRepository)
        {
            _comentarioRepository = comentarioRepository;
        }
		public void CambiarAComentarioLeido(int idComentario)
		{
			_comentarioRepository.CambiarAComentarioLeido(idComentario);
		}

		public void Guardar(Comentario comentario)
		{
			_comentarioRepository.Guardar(comentario);
		}

		public List<Comentario> ObtenerBandejaEntrada(int idUsuario)
		{
			return _comentarioRepository.ObtenerBandejaEntrada(idUsuario);
		}

		public List<Comentario> ObtenerComentariosPorIdPublicacion(int idPublicacion)
		{
			return _comentarioRepository.ObtenerComentariosPorIdPublicacion(idPublicacion);
		}

		public Comentario ObtenerPorIdComentario(int id)
		{
			return _comentarioRepository.ObtenerPorIdComentario(id);
		}

		public List<Comentario> ObtenerTodos()
		{
			return _comentarioRepository.ObtenerTodos();
		}
	}
}