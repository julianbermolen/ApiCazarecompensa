using System.ComponentModel.DataAnnotations;

namespace infraestructura.entidades
{
    public class TesoroCategoria
    {
        [Key]
        public int IdTesoroCategoria { get; set; }
        public string Nombre { get; set; }
    }
}