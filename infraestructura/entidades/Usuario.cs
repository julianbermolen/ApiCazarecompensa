using System.ComponentModel.DataAnnotations;

namespace infraestructura.entidades
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
    }
}