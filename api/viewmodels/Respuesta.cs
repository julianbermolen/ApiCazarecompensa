namespace api.viewmodels
{
    public class Respuesta
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; }
        public int Valor{get;set;}
        public string StackTrace { get; set; }
        public string  InnerException { get; set; }
    }
}