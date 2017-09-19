using aplicacion.servicios;
using aplicacion.servicios.abstracciones;
using infraestructura.repositorios;
using infraestructura.repositorios.abstracciones;
using Microsoft.Extensions.DependencyInjection;

namespace api.ioc
{
    public class Bindings
    {
        private IServiceCollection _services;

        public Bindings(IServiceCollection services)
        {
            _services = services;
        }
        public void CrearBindings()
        {
            _services.AddTransient<IUsuarioService, UsuarioService>();
            _services.AddTransient<IUsuarioRepository, UsuarioRepository>();
        }
    }
}