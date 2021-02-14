
using CottonCandy.Application.AppUser.Interfaces;
using CottonCandy.Application.AppUsuario;
using CottonCandy.Application.AppUsuario.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CottonCandy.Repositories.IoC.Application
{
   internal class ApplicationBootstraper
    {
        internal void ChildServiceRegister(IServiceCollection services)
        {
            services.AddScoped<IUsuarioAppService, UsuarioAppService>();
            services.AddScoped<ILoginAppService, LoginAppService>();
        }
    }
}
