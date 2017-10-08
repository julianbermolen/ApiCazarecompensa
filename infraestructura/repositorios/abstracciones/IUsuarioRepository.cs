using System.Collections.Generic;
using infraestructura.entidades;

namespace infraestructura.repositorios.abstracciones
{
    public interface IUsuarioRepository
    {
        List<Usuario> ObtenerUsuarios();
        void RegistrarUsuario(Usuario usuario);
    }
}