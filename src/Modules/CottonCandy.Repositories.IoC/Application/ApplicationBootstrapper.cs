
using CottonCandy.Application;
using CottonCandy.Application.AppPostagem;
using CottonCandy.Application.AppPostagem.Interfaces;
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
            services.AddScoped<IPostagemAppService, PostagemAppService>();
            services.AddScoped<ICurtidasAppService, CurtidasAppService>();
            services.AddScoped<IAlbumFotosAppService, AlbumFotosAppService>();
        }
    }
}
