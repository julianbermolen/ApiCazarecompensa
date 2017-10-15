using System.Linq;
using System.Collections.Generic;
using aplicacion.servicios.abstracciones;
using infraestructura;
using infraestructura.entidades;
using infraestructura.repositorios.abstracciones;
using System;

namespace aplicacion.servicios
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        
        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public List<Usuario> ObtenerUsuarios()
        {
            return _usuarioRepository.ObtenerUsuarios();
        }

        public void RegistrarUsuario(Usuario usuario)
        {
            if(!EsUsuarioRepetido(usuario))
            {
                _usuarioRepository.RegistrarUsuario(usuario);
            }
            else 
            {
                throw new Exception("El usuario ya se encuentra registrado en el sistema");
            }
        }

        public void EliminarUsuario(int id)
        {
            _usuarioRepository.EliminarUsuario(id);
        }
		public void Guardar(Usuario usuario)
		{
			if(!EsUsuarioRepetido(usuario))
            {
                _usuarioRepository.Guardar(usuario);
            }
            else 
            {
                throw new Exception("El usuario ya se encuentra registrado en el sistema");
            }
        }

        private bool EsUsuarioRepetido(Usuario usuario)
        {
            return _usuarioRepository.ObtenerUsuarios().Any(x => x.IdFacebook  == usuario.IdFacebook);
        }
	}
}