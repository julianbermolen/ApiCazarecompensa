using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace infraestructura.entidades
{
    public class Publicacion
    {
        [Key]
        public int IdPublicacion { get; set; }
        [ForeignKey("Tesoro")]
        public int IdTesoro { get; set; }
        public virtual Tesoro Tesoro { get; set; }
    }
}