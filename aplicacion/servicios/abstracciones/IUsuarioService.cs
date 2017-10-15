using System.Collections.Generic;
using infraestructura.entidades;

namespace aplicacion.servicios.abstracciones
{
    public interface IUsuarioService
    {
         List<Usuario> ObtenerUsuarios();
         void RegistrarUsuario(Usuario usuario);
         void EliminarUsuario(int id);
         void Guardar(Usuario usuario);
    }
}