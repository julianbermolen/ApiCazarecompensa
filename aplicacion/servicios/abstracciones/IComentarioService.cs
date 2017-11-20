using System.Collections.Generic;
using infraestructura.entidades;

namespace aplicacion.servicios.abstracciones
{
    public interface IComentarioService
    {
         List<Comentario> ObtenerTodos();
         Comentario ObtenerPorIdComentario(int id);
         List<Comentario> ObtenerComentariosPorIdPublicacion(int idPublicacion);
         void Guardar(Comentario comentario);
        List<Dictionary<int, List<Comentario>>> ObtenerBandejaEntrada(int idUsuario);
        void CambiarAComentarioLeido(int idComentario);
    }
}