using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace infraestructura.entidades
{
    public class Tesoro
    {
        [Key]
        public int IdTesoro { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        [ForeignKey("TesoroCategoria")]
        public long IdTesoroCategoria { get; set; }
        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        public decimal Recompensa { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public string Imagen1 { get; set; }
        public string Imagen2 { get; set; }
        public string Imagen3 { get; set; }
        [ForeignKey("TesoroEstado")]
        public int? IdTesoroEstado { get; set; }
        public DateTime? FechaCarga { get; set; }
        public virtual TesoroCategoria TesoroCategoria { get; set; }
        public virtual TesoroEstado TesoroEstado { get; set; }
    }
}