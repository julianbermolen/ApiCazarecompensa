using System;

namespace infraestructura.entidades
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
        public int NumeroConversacion { get; set; }
        public DateTime FechaCarga { get; set; }
        public  Publicacion Publicacion { get; set; }
        public  Usuario UsuarioEmisor { get; set; }
        public  Usuario UsuarioReceptor { get; set; }
    }
}