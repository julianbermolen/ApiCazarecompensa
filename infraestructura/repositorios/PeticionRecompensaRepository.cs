using System.Collections.Generic;
using System.Linq;
using infraestructura.entidades;
using infraestructura.repositorios.abstracciones;
using Microsoft.EntityFrameworkCore;

namespace infraestructura.repositorios
{
    public class PeticionRecompensaRepository : IPeticionRecompensaRepository
    {
        private readonly Contexto _contexto;
        public PeticionRecompensaRepository(Contexto contexto)
        {
            _contexto = contexto;
        }

		public void ActualizarEstado(int idUsuario, int idTesoro, int estado)
		{
            var peticion = _contexto.PeticionRecompensa
                .FirstOrDefault(x => x.IdUsuario == idUsuario && x.IdTesoro == idTesoro);

			peticion.Estado = estado;
			_contexto.Entry(peticion).State = EntityState.Modified;
			_contexto.SaveChanges();
		}

		public void Guardar(PeticionRecompensa peticionRecompensa)
		{
			_contexto.PeticionRecompensa.Add(peticionRecompensa);
            _contexto.SaveChanges();
		}

		public PeticionRecompensa ObtenerPorIdUsuario(int id)
		{
			return _contexto.PeticionRecompensa.FirstOrDefault(x => x.IdUsuario == id);
		}

		public List<PeticionRecompensa> ObtenerTodas()
		{
			return _contexto.PeticionRecompensa.ToList();
		}
	}
}