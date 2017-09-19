using System.Collections.Generic;
using infraestructura.entidades;

namespace aplicacion.servicios.abstracciones
{
    public interface IUsuarioService
    {
         List<Usuario> ObtenerUsuarios();
    }
}