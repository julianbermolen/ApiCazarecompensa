using infraestructura.entidades;
using Microsoft.EntityFrameworkCore;

namespace infraestructura
{
    public class Contexto : DbContext
    {
        private string _connectionString;
        public DbSet<Usuario> Usuario { get; set; }

        public Contexto(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseMySql(_connectionString);
        
    }
}