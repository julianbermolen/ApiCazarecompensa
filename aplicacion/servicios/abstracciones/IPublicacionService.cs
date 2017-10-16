using System.Collections.Generic;
using infraestructura.entidades;

namespace aplicacion.servicios.abstracciones
{
    public interface IPublicacionService
    {
         List<Publicacion> ObtenerTodas();
         Publicacion ObtenerPorId(int id);
         List<Publicacion> ObtenerPorIdCategoria(int idCategoria);
         void Guardar(Publicacion publicacion);
         void Eliminar(int id);
    }
}