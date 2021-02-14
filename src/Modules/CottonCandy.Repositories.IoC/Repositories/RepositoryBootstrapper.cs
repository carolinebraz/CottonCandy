using CottonCandy.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CottonCandy.Repositories.IoC.Repositories
{
    internal class RepositoryBootstrapper
    {
        internal void ChildServiceRegister(IServiceCollection services)
        {
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IGeneroRepository, GeneroRepository>();
        }
    }
}