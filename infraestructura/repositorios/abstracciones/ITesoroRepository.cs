using System.Collections.Generic;
using infraestructura.entidades;

namespace infraestructura.repositorios.abstracciones
{
    public interface ITesoroRepository
    {
        List<Tesoro> ObtenerTodos();
        Tesoro ObtenerPorId(int idTesoro);
        List<Tesoro> ObtenerPorIdCategoria(int idCategoriaTesoro);
        void Guardar(Tesoro tesoro);
    }
}