using System.Collections.Generic;
using System;
using System.Linq;
using infraestructura.entidades;
using infraestructura.repositorios.abstracciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using SixLabors.ImageSharp;
using System.IO;
using Microsoft.AspNetCore.Http;

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
			Dictionary<string, string> encodings = new  Dictionary<string, string>();

			tesoro.FechaCarga = DateTime.Now;
			tesoro.IdTesoroEstado = (int)TesoroEstadoEnum.Activo;

			GuardarEncodings(encodings, tesoro);

			_contexto.Tesoro.Add(tesoro);
            _contexto.SaveChanges();

			GuardarImagenesEnDisco(encodings, tesoro);
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

		private void GuardarEncodings(Dictionary<string, string> encodings, Tesoro tesoro)
		{
			encodings.Add("imagen1", tesoro.Imagen1);
			tesoro.Imagen1 = string.Empty;
			encodings.Add("imagen2", tesoro.Imagen2);
			tesoro.Imagen2 = string.Empty;
			encodings.Add("imagen3", tesoro.Imagen3);
			tesoro.Imagen3 = string.Empty;
		}

		private void GuardarImagenesEnDisco(Dictionary<string, string> encodings, Tesoro tesoro)
		{
			var webRoot = _env.WebRootPath;
			var PathWithFolderName = System.IO.Path.Combine(webRoot, "tesoros");


			if (!Directory.Exists(PathWithFolderName))
			{
				DirectoryInfo di = Directory.CreateDirectory(PathWithFolderName);
			}

			foreach(var encoding in encodings)
			{
				if(!string.IsNullOrEmpty(encoding.Value))
				{
					var imagen = encoding.Value
						.Replace("data:image/jpeg;base64,", string.Empty)
						.Replace("data:image/png;base64,", string.Empty)
						.Replace('-', '+')
						.Replace('_', '/');

					using (Image<Rgba32> image = Image.Load<Rgba32>(Convert.FromBase64String(imagen)))
					{
						image.Save(string.Format("{0}/{1}-{2}.jpg", PathWithFolderName, tesoro.IdTesoro, encoding.Key));
					}
					tesoro.Imagen1 = GenerarPath(tesoro.IdTesoro + "-" + encoding.Key + ".jpg");
				}
			}
		}

		public void CambiarEstadoTesoro(int idTesoro, int idEstado)
		{
			var tesoro = _contexto.Tesoro.FirstOrDefault(x => x.IdTesoro == idTesoro);

			if(tesoro == null) throw new Exception("El tesoro no existe en la bd");

			tesoro.IdTesoroEstado = idEstado;

			_contexto.Entry(tesoro).State =  EntityState.Modified;
			_contexto.SaveChanges();

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