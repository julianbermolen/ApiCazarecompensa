using System.Linq;
using System.Collections.Generic;
using aplicacion.servicios.abstracciones;
using infraestructura;
using infraestructura.entidades;
using infraestructura.repositorios.abstracciones;

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
            _usuarioRepository.RegistrarUsuario(usuario);
        }

        public void EliminarUsuario(int id)
        {
            _usuarioRepository.EliminarUsuario(id);
        }
    }
}