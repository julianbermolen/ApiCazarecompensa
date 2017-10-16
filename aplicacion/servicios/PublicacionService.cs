using System.Collections.Generic;
using aplicacion.servicios.abstracciones;
using infraestructura.entidades;
using infraestructura.repositorios.abstracciones;

namespace aplicacion.servicios
{
	public class PublicacionService : IPublicacionService
	{
        private readonly IPublicacionRepository _publicacionRepository;
        public PublicacionService(IPublicacionRepository publicacionRepository)
        {
            _publicacionRepository = publicacionRepository;
        }
        public List<Publicacion> ObtenerPorIdCategoria(int idCategoria)
		{
			return _publicacionRepository.ObtenerPorIdCategoria(idCategoria);
		}

		public List<Publicacion> ObtenerTodas()
		{
			return _publicacionRepository.ObtenerTodas();
		}
		public void Eliminar(int id)
		{
			_publicacionRepository.Eliminar(id);
		}

		public void Guardar(Publicacion publicacion)
		{
			_publicacionRepository.Guardar(publicacion);
		}

		public Publicacion ObtenerPorId(int id)
		{
			return _publicacionRepository.ObtenerPorId(id);
		}

	}
}