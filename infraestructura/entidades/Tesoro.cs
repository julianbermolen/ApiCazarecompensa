using System;
using System.ComponentModel.DataAnnotations;

namespace infraestructura.entidades
{
    public class Tesoro
    {
        [Key]
        public int IdTesoro { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdCategoria { get; set; }
        public int IdUsuario { get; set; }
        public decimal Recompensa { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public string Imagen1 { get; set; }
        public string Imagen2 { get; set; }
        public string Imagen3 { get; set; }
        public int? IdEstadoTesoro { get; set; }
        public DateTime? FechaCarga { get; set; }
    }
}