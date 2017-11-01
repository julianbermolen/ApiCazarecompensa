using System.Collections.Generic;
using infraestructura.entidades;

namespace aplicacion.servicios.abstracciones
{
    public interface ITesoroService
    {
         List<Tesoro> ObtenerTodos();
         Tesoro ObtenerPorId(int id);
         List<Tesoro> ObtenerPorIdCategoria(int idCategoria);
         List<TesoroCategoria> ObtenerCategoria();
         Tesoro Guardar(Tesoro tesoro);
         void Eliminar(int id);
         int ObtenerIdPublicacionPorIdTesoro(int id);
         
    }
}