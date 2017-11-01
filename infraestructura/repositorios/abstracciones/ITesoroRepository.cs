using System.Collections.Generic;
using infraestructura.entidades;

namespace infraestructura.repositorios.abstracciones
{
    public interface ITesoroRepository
    {
        List<Tesoro> ObtenerTodos();
        Tesoro ObtenerPorId(int idTesoro);
        List<Tesoro> ObtenerPorIdCategoria(int idCategoriaTesoro);
        List<TesoroCategoria> ObtenerCategoria();
        Tesoro Guardar(Tesoro tesoro);
        void Eliminar(int id);
        int ObtenerIdPublicacionPorIdTesoro(int id);
    }
}