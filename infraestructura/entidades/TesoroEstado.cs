using System.ComponentModel.DataAnnotations;

namespace infraestructura.entidades
{
    public class TesoroEstado
    {
        [Key]
        public int IdTesoroEstado { get; set; }

        public string Nombre { get; set; }
    }
}