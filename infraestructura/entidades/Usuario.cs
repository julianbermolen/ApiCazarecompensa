using System;
using System.ComponentModel.DataAnnotations;

namespace infraestructura.entidades
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string UrlFoto { get; set; }
        public string Email { get; set; }
        public int? IdFacebook { get; set; }
        public DateTime FechaCarga { get; set; }
    }
}