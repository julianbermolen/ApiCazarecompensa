using System.Collections.Generic;
using infraestructura.entidades;

namespace aplicacion.servicios.abstracciones
{
    public interface IPeticionRecompensaService
    {
         PeticionRecompensa ObtenerPorIdUsuario(int idUsuario);
         void Guardar(PeticionRecompensa peticionRecompensa);
         void ActualizarEstado(int idUsuario, int idTesoro, int estado);
         List<PeticionRecompensa> ObtenerTodas();
    }
}