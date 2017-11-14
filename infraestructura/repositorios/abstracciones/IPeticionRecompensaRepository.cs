using System.Collections.Generic;
using infraestructura.entidades;

namespace infraestructura.repositorios.abstracciones
{
    public interface IPeticionRecompensaRepository
    {
        void ActualizarEstado(int idUsuario, int idTesoro, int estado);
		void Guardar(PeticionRecompensa peticionRecompensa);
		PeticionRecompensa ObtenerPorIdUsuario(int id);
        List<PeticionRecompensa> ObtenerTodas();
    }
}