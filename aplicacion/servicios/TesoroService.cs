using System;
using System.Collections.Generic;
using System.IO;
using aplicacion.servicios.abstracciones;
using infraestructura.entidades;
using infraestructura.repositorios.abstracciones;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;

namespace aplicacion.servicios
{
	public class TesoroService : ITesoroService
	{
		private readonly ITesoroRepository _tesoroRepository;
		private readonly IHostingEnvironment _env; 
		public TesoroService(ITesoroRepository tesoroRepository, IHostingEnvironment env)
		{
			_tesoroRepository = tesoroRepository;
			_env = env;
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
            AddFolderAndImage(tesoro);
        }

		public int ObtenerIdPublicacionPorIdTesoro(int id)
		{
			return _tesoroRepository.ObtenerIdPublicacionPorIdTesoro(id);
		}

		private void AddFolderAndImage(Tesoro tesoro)
		{
			var webRoot = _env.WebRootPath;
			var PathWithFolderName = System.IO.Path.Combine(webRoot, "tesoros");


			if (!Directory.Exists(PathWithFolderName))
			{
				DirectoryInfo di = Directory.CreateDirectory(PathWithFolderName);
			}

			if(!string.IsNullOrEmpty(tesoro.Imagen1))
			{
				var imagen = tesoro.Imagen1.Replace("data:image/jpeg;base64,", string.Empty);

				using (Image<Rgba32> image = Image.Load<Rgba32>(Convert.FromBase64String(imagen)))
				{
					image.Save(PathWithFolderName + "/" +  tesoro.IdTesoro + "imagen1.jpg"); // el 1 podría ser el id del tesoro
					tesoro.Imagen1 = GenerarPath(tesoro.IdTesoro + "-imagen1.jpg");
				}
			}
			if(!string.IsNullOrEmpty(tesoro.Imagen2))
			{
				var imagen = tesoro.Imagen2.Replace("data:image/jpeg;base64,", string.Empty);

				using (Image<Rgba32> image = Image.Load<Rgba32>(Convert.FromBase64String(imagen)))
				{
					image.Save(PathWithFolderName + "/" + tesoro.IdTesoro + "imagen2.jpg"); // el 1 podría ser el id del tesoro
					tesoro.Imagen2 = GenerarPath(tesoro.IdTesoro + "-imagen2.jpg");
				}
			}
			if(!string.IsNullOrEmpty(tesoro.Imagen3))
			{
				var imagen = tesoro.Imagen3.Replace("data:image/jpeg;base64,", string.Empty);

				using (Image<Rgba32> image = Image.Load<Rgba32>(Convert.FromBase64String(imagen)))
				{
					image.Save(PathWithFolderName + "/" + tesoro.IdTesoro + "imagen3.jpg"); // el 1 podría ser el id del tesoro
					tesoro.Imagen3 = GenerarPath(tesoro.IdTesoro + "-imagen3.jpg");
				}
			}
		}

		private string GenerarPath(string nombreImagen)
		{
				return string.Format("http://li1166-116.members.linode.com/tesoros/{0}", nombreImagen);
		}
	}
}