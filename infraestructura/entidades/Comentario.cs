using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace infraestructura.entidades
{
    public class Comentario
    {
        [Key]
        public int IdComentario { get; set; }
        [ForeignKey("Usuario")]
        public int IdUsuarioEmisor { get; set; }
        [ForeignKey("Usuario")]
        public int IdUsuarioReceptor { get; set; }
        [ForeignKey("Publicacion")]
        public int IdPublicacion { get; set; }
        public string Detalle { get; set; }
        public string Imagen { get; set; }
        public bool MensajeLeido { get; set; }
        public int NumeroConversacion { get; set; }
        public DateTime FechaCarga { get; set; }
        public virtual Publicacion Publicacion { get; set; }
    }
}