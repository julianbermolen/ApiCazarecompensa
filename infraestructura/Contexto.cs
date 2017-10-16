using infraestructura.entidades;
using Microsoft.EntityFrameworkCore;

namespace infraestructura
{
    public class Contexto : DbContext
    {
        private string _connectionString;
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Tesoro> Tesoro { get; set; }
        public DbSet<Publicacion> Publicacion { get; set; }
        public DbSet<TesoroCategoria> TesoroCategoria { get; set; }
        public DbSet<TesoroEstado> TesoroEstado { get; set; }


        public Contexto(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseMySql(_connectionString);
        
    }
}