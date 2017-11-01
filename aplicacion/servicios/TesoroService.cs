using System;
using System.Collections.Generic;
using aplicacion.servicios.abstracciones;
using infraestructura.entidades;
using infraestructura.repositorios.abstracciones;
using SixLabors.ImageSharp;

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
			GuardarImagenesEnDisco(tesoro);
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

		private void GuardarImagenesEnDisco(Tesoro tesoro)
        {
            if(!string.IsNullOrEmpty(tesoro.Imagen1))
            {
                var imagen = tesoro.Imagen1.Replace("data:image/jpeg;base64,", string.Empty);

                using (Image<Rgba32> image = Image.Load<Rgba32>(Convert.FromBase64String(imagen)))
                {
                    image.Save("imagen1.jpg"); // el 1 podría ser el id del tesoro
                    tesoro.Imagen1 = "imagen1.jpg"; // piso el encoding con el path de la imagen ya generada
                }
            }
            if(!string.IsNullOrEmpty(tesoro.Imagen2))
            {
                var imagen = tesoro.Imagen2.Replace("data:image/jpeg;base64,", string.Empty);

                using (Image<Rgba32> image = Image.Load<Rgba32>(Convert.FromBase64String(imagen)))
                {
                    image.Save("imagen2.jpg"); // el 1 podría ser el id del tesoro
                    tesoro.Imagen2 = "imagen2.jpg"; // piso el encoding con el path de la imagen ya generada
                }
            }
            if(!string.IsNullOrEmpty(tesoro.Imagen3))
            {
                var imagen = tesoro.Imagen3.Replace("data:image/jpeg;base64,", string.Empty);

                using (Image<Rgba32> image = Image.Load<Rgba32>(Convert.FromBase64String(imagen)))
                {
                    image.Save("imagen3.jpg"); // el 1 podría ser el id del tesoro
                    tesoro.Imagen3 = "imagen3.jpg"; // piso el encoding con el path de la imagen ya generada
                }
            }
        }

		public int ObtenerIdPublicacionPorIdTesoro(int id)
		{
			return _tesoroRepository.ObtenerIdPublicacionPorIdTesoro(id);
		}
	}
}