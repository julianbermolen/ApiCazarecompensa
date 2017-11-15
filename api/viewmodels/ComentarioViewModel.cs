using System;
using infraestructura.entidades;

namespace api.viewmodels
{
    public class ComentarioViewModel
    {
        public int IdComentario { get; set; }
        public int IdUsuarioEmisor { get; set; }
        public int IdUsuarioReceptor { get; set; }
        public int IdPublicacion { get; set; }
        public string Detalle { get; set; }
        public string Imagen { get; set; }
        public bool MensajeLeido { get; set; }
        public DateTime FechaCarga { get; set; }
        public virtual Publicacion Publicacion { get; set; }
        public virtual Usuario UsuarioEmisor { get; set; }
    }
}