using System.Collections.Generic;
using infraestructura.entidades;

namespace infraestructura.repositorios.abstracciones
{
    public interface IPublicacionRepository
    {
         List<Publicacion> ObtenerTodas();
         Publicacion ObtenerPorId(int id);
         List<Publicacion> ObtenerPorIdCategoria(int idCategoria);
         void Guardar(Publicacion publicacion);
         void Eliminar(int id);
    }
}