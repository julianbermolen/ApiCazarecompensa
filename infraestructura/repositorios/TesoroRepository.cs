using System.Collections.Generic;
using System;
using System.Linq;
using infraestructura.entidades;
using infraestructura.repositorios.abstracciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using SixLabors.ImageSharp;
using System.IO;

namespace infraestructura.repositorios
{
	public class TesoroRepository : ITesoroRepository
	{
        private readonly Contexto _contexto;
		private readonly IHostingEnvironment _env; 
		public TesoroRepository(Contexto contexto, IHostingEnvironment env)
		{
            _contexto = contexto;
			_env = env;
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
			tesoro.IdTesoroEstado = (int)TesoroEstadoEnum.Activo;
			_contexto.Tesoro.Add(tesoro);
            _contexto.SaveChanges();

			GuardarImagenesEnDisco(tesoro);
			_contexto.Entry(tesoro).State = EntityState.Modified;
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

		private void GuardarImagenesEnDisco(Tesoro tesoro)
        {
            AddFolderAndImage(tesoro);
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
					image.Save(PathWithFolderName + "/" +  tesoro.IdTesoro + "-imagen1.jpg");
				}
				tesoro.Imagen1 = GenerarPath(tesoro.IdTesoro + "-imagen1.jpg");
			}
			if(!string.IsNullOrEmpty(tesoro.Imagen2))
			{
				var imagen = tesoro.Imagen2.Replace("data:image/jpeg;base64,", string.Empty);

				using (Image<Rgba32> image = Image.Load<Rgba32>(Convert.FromBase64String(imagen)))
				{
					image.Save(PathWithFolderName + "/" + tesoro.IdTesoro + "-imagen2.jpg");
				}
				tesoro.Imagen2 = GenerarPath(tesoro.IdTesoro + "-imagen2.jpg");
			}
			if(!string.IsNullOrEmpty(tesoro.Imagen3))
			{
				var imagen = tesoro.Imagen3.Replace("data:image/jpeg;base64,", string.Empty);

				using (Image<Rgba32> image = Image.Load<Rgba32>(Convert.FromBase64String(imagen)))
				{
					image.Save(PathWithFolderName + "/" + tesoro.IdTesoro + "-imagen3.jpg");
				}
				tesoro.Imagen3 = GenerarPath(tesoro.IdTesoro + "-imagen3.jpg");
			}
		}

		private string GenerarPath(string nombreImagen)
		{
			return string.Format("http://li1166-116.members.linode.com/tesoros/{0}", nombreImagen);
		}
	}

	enum TesoroEstadoEnum {
		Activo = 1,
		Finalizado = 2,
		Cancelado = 3	
	}
}