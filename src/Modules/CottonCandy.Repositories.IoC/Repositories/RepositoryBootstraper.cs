using CottonCandy.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CottonCandy.Repositories.IoC.Repositories
{
    internal class RepositoryBootstraper
    {
        internal void ChildServiceRegister(IServiceCollection services)
        {
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IGeneroRepository, GeneroRepository>();

        }
    }
}
