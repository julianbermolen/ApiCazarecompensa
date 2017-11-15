using System.Collections.Generic;
using infraestructura.entidades;

namespace aplicacion.servicios.abstracciones
{
    public interface IUsuarioService
    {
         List<Usuario> ObtenerUsuarios();
         void RegistrarUsuario(Usuario usuario);
         void EliminarUsuario(int id);
         int getUserId(string idFacebook);
         void Guardar(Usuario usuario);
         Usuario ObtenerUsuarioPorIdUsuario(int idUsuario);
    }
}