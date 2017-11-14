using System.ComponentModel.DataAnnotations;

namespace infraestructura.entidades
{
    public class PeticionRecompensa
    {
        [Key]
        public int IdPeticionRecompensa {get; set;}
        public int IdUsuario {get; set;}
        public int IdTesoro {get; set;}
        public int Estado {get; set;}
    }
}