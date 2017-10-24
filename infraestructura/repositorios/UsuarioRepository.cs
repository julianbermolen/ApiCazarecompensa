using System.Collections.Generic;
using System;
using System.Linq;
using infraestructura.entidades;
using infraestructura.repositorios.abstracciones;

namespace infraestructura.repositorios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly Contexto _contexto;
        public UsuarioRepository(Contexto contexto)
        {
            _contexto = contexto;
        }
        public List<Usuario> ObtenerUsuarios()
        {
            return _contexto.Usuario.ToList();
        }
        public void RegistrarUsuario(Usuario usuario)
        {
            usuario.FechaCarga = DateTime.Now;
            _contexto.Usuario.Add(usuario);
            _contexto.SaveChanges();
        }
        public void EliminarUsuario(int id)
        {
            var usuario = ObtenerUsuarios().FirstOrDefault(x => x.IdUsuario == id);

            if(usuario == null)
            {
                throw new Exception("El usuario no existe en el sistema");
            }
            
            _contexto.Usuario.Remove(usuario);
            _contexto.SaveChanges();
        }
        public int getUserId(long idFacebook){
            var usuario = ObtenerUsuarios().FirstOrDefault(x => x.IdFacebook == idFacebook);
            return usuario.IdUsuario;
        }

        public void Guardar(Usuario usuario)
        {
            _contexto.Usuario.Add(usuario);
            _contexto.SaveChanges();
        }
    }
}