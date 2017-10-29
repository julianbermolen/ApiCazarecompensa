using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace infraestructura.entidades
{
    public class Comentario
    {
        [Key]
        public int IdComentario { get; set; }
        public int IdRespuestaComentario { get; set; }
        [ForeignKey("Publicacion")]
        public int IdPublicacion { get; set; }
        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        public string Detalle { get; set; }
        public string Imagen { get; set; }
        public bool MensajeLeido { get; set; }
        public DateTime FechaCarga { get; set; }
        public virtual Publicacion Publicacion { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}