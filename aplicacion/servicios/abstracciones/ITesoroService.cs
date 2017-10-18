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
         void Guardar(Tesoro tesoro);
         void Eliminar(int id);
         
    }
}