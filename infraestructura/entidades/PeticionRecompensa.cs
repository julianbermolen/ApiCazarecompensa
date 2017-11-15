using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace infraestructura.entidades
{
    public class PeticionRecompensa
    {
        [Key]
        public int IdPeticionRecompensa {get; set;}
        public int IdUsuario {get; set;}
        [ForeignKey("Tesoro")]
        public int IdTesoro {get; set;}
        public int Estado {get; set;}
        public virtual Tesoro Tesoro { get; set; }
    }
}