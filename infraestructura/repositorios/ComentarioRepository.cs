using System.Collections.Generic;
using System;
using System.Linq;
using infraestructura.entidades;
using infraestructura.repositorios.abstracciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using SixLabors.ImageSharp;

namespace infraestructura.repositorios
{
	public class ComentarioRepository : IComentarioRepository
	{
        private readonly Contexto _contexto;
		private readonly IUsuarioRepository _usuarioRepository;
		private readonly IHostingEnvironment _env;
		private readonly IPublicacionRepository _publicacionRepository;

		public ComentarioRepository(Contexto contexto, IUsuarioRepository usuarioRepository, IPublicacionRepository publicacionRepository,
		IHostingEnvironment env)
		{
            _contexto = contexto;
			_usuarioRepository = usuarioRepository;
			_publicacionRepository = publicacionRepository;
			_env = env;
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

        public List<Dictionary<int, List<ComentarioViewModel>>> ObtenerBandejaEntrada(int idUsuario)
		{

			// Se obtienen todas las publicaciones del usuario
			int[] publicaciones = _contexto.Publicacion 
				.Include(t => t.Tesoro)
				.Where(x => x.Tesoro.IdUsuario == idUsuario)
				.Select(x => x.IdPublicacion)
				.ToArray();

			if(publicaciones.Length == 0)
			{
				return new List<Dictionary<int, List<ComentarioViewModel>>>();
			}

			// esto son los todos los comentarios que se hicieron en las publicaciones del
			// usuario en cuestion
			var conversaciones = _contexto.Comentario
			.Where(x => publicaciones.Contains(x.IdPublicacion))
			.OrderByDescending(x => x.FechaCarga)
			.OrderBy(x => x.NumeroConversacion)
			.ToList();

			if(conversaciones.Count() == 0)
			{
				return new List<Dictionary<int, List<ComentarioViewModel>>>();
			}

			List<Dictionary<int, List<Comentario>>> resultado = new List<Dictionary<int, List<Comentario>>>();
			var listaConversaciones = new List<Comentario>();

			var conversacionAnterior = conversaciones.First() ?? new Comentario();

			
			foreach(var conversacion in conversaciones) // por cada comentario relacionado a una publicacion del usuario ....
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
					resultado.Add(diccionario);

					conversacionAnterior = conversacion;
					listaConversaciones.Clear();
					listaConversaciones.Add(conversacion);
				}

			}

			var dic = new Dictionary<int, List<Comentario>>();
			dic.Add(conversacionAnterior.NumeroConversacion, listaConversaciones);
			resultado.Add(dic);

			return AgregarUsuarioEmisoresYReceptores(resultado);
		}

        public void Guardar(Comentario comentario)
		{
			var encodingImagen = comentario.Imagen;
			comentario.Imagen = null;

			comentario.FechaCarga = DateTime.Now;

			ObtenerNumeroConversacion(comentario);

			_contexto.Comentario.Add(comentario);

			_contexto.SaveChanges();

			GuardarImagenEnDisco(comentario, encodingImagen);

            _contexto.Entry(comentario).State = EntityState.Modified;
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
			var numeroConversacion = _contexto
				.Comentario.Where(
					x => x.IdPublicacion == comentario.IdPublicacion &&
					(x.IdUsuarioEmisor == comentario.IdUsuarioEmisor && x.IdUsuarioReceptor == comentario.IdUsuarioReceptor)  ||
					(x.IdUsuarioEmisor == comentario.IdUsuarioReceptor && x.IdUsuarioReceptor == comentario.IdUsuarioEmisor)
				 )
				.FirstOrDefault();

			if (numeroConversacion == null)
			{
				var num = _contexto.Comentario.OrderByDescending(x => x.NumeroConversacion).FirstOrDefault() == null ? 1 : _contexto.Comentario.OrderByDescending(x => x.NumeroConversacion).First().NumeroConversacion +1;
				comentario.NumeroConversacion = num;
			}
			else
			{
				comentario.NumeroConversacion = numeroConversacion.NumeroConversacion;
			}
			
		}

		private List<Dictionary<int, List<ComentarioViewModel>>> AgregarUsuarioEmisoresYReceptores(List<Dictionary<int, List<Comentario>>> lista)
		{
			var listadoViewModel = new List<Dictionary<int, List<ComentarioViewModel>>>();
			var itemViewModel = new Dictionary<int, List<ComentarioViewModel>>();

			foreach(var diccionario in lista)
			{
				itemViewModel.Clear();
				foreach(var item in diccionario)
				{
					itemViewModel.Add(item.Key, MapearComentario(item.Value));	
				};

				Dictionary<int, List<ComentarioViewModel>> temp = new Dictionary<int, List<ComentarioViewModel>>(itemViewModel);
				listadoViewModel.Add(temp);
			}

			return listadoViewModel;
		
		}


		private List<ComentarioViewModel> MapearComentario(List<Comentario> listadoComentarios)
		{
			List<ComentarioViewModel> listadoNuevo = new List<ComentarioViewModel>();

			foreach (var x in listadoComentarios)
			{
				var comentarioViewModel = new ComentarioViewModel
				{
					IdComentario = x.IdComentario,
					IdUsuarioEmisor = x.IdUsuarioEmisor,
					IdUsuarioReceptor = x.IdUsuarioReceptor,
					IdPublicacion = x.IdPublicacion,
					Detalle = x.Detalle,
					Imagen = x.Imagen,
					MensajeLeido = x.MensajeLeido,
					NumeroConversacion = x.NumeroConversacion,
					FechaCarga = x.FechaCarga,
					UsuarioEmisor = _usuarioRepository.ObtenerUsuarioPorIdUsuario(x.IdUsuarioEmisor),
					UsuarioReceptor = _usuarioRepository.ObtenerUsuarioPorIdUsuario(x.IdUsuarioReceptor)
				};

				listadoNuevo.Add(comentarioViewModel);
			}

			return listadoNuevo;
		} 

		private void GuardarImagenEnDisco(Comentario comentario, string encodingImagen)
		{
			var webRoot = _env.WebRootPath;
			var PathWithFolderName = System.IO.Path.Combine(webRoot, "comentarios");


			if (!Directory.Exists(PathWithFolderName))
			{
				DirectoryInfo di = Directory.CreateDirectory(PathWithFolderName);
			}

			if(!string.IsNullOrEmpty(encodingImagen))
			{
				var imagen = encodingImagen
					.Replace("data:image/jpeg;base64,", string.Empty)
					.Replace("data:image/png;base64,", string.Empty)
					.Replace('-', '+')
					.Replace('_', '/');

				using (Image<Rgba32> image = Image.Load<Rgba32>(Convert.FromBase64String(imagen)))
				{
					image.Save(string.Format("{0}/{1}.jpg", PathWithFolderName, comentario.IdComentario));
				}
				comentario.Imagen = GenerarPath(comentario.IdComentario + ".jpg");
			}
		}

		private string GenerarPath(string nombreImagen)
		{
			return string.Format("http://li1166-116.members.linode.com/comentarios/{0}", nombreImagen);
		}

	}
}