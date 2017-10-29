using System.Collections.Generic;
using infraestructura.entidades;

namespace infraestructura.repositorios.abstracciones
{
    public interface IComentarioRepository
    {
         List<Comentario> ObtenerTodos();
         Comentario ObtenerPorIdComentario(int id);
         List<Comentario> ObtenerComentariosPorIdPublicacion(int idPublicacion);
         void Guardar(Comentario comentario);
         List<Comentario> ObtenerBandejaEntrada(int idUsuario);
        void CambiarAComentarioLeido(int idComentario);
    }
}