using System.Collections.Generic;
using aplicacion.servicios.abstracciones;
using infraestructura.entidades;
using infraestructura.repositorios.abstracciones;

namespace aplicacion.servicios
{
    public class PeticionRecompensaService : IPeticionRecompensaService
    {
        private readonly IPeticionRecompensaRepository _peticionRecompensaRepository;
        
        public PeticionRecompensaService(IPeticionRecompensaRepository peticionRecompensaRepository)
        {
            _peticionRecompensaRepository = peticionRecompensaRepository;
        }

		public void ActualizarEstado(int idUsuario, int idTesoro, int estado)
		{
			_peticionRecompensaRepository.ActualizarEstado(idUsuario, idTesoro, estado);
		}

		public void Guardar(PeticionRecompensa peticionRecompensa)
		{
			_peticionRecompensaRepository.Guardar(peticionRecompensa);
		}

		public PeticionRecompensa ObtenerPorIdUsuario(int id)
		{
			return _peticionRecompensaRepository.ObtenerPorIdUsuario(id);
		}

		public List<PeticionRecompensa> ObtenerTodas()
		{
			return _peticionRecompensaRepository.ObtenerTodas();
		}
	}
}