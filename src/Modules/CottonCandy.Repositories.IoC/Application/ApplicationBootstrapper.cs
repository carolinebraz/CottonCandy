
using CottonCandy.Application.AppUser.Interfaces;
using CottonCandy.Application.AppUsuario;
using Microsoft.Extensions.DependencyInjection;

namespace CottonCandy.Repositories.IoC.Application
{
    internal class ApplicationBootstrapper
    {
        internal void ChildServiceRegister(IServiceCollection services)
        {
            services.AddScoped<IUsuarioAppService, UsuarioAppService>();
        }
    }
}