using CottonCandy.Application.AppPostagem;
using CottonCandy.Application.AppPostagem.Interfaces;
using CottonCandy.Application.AppUsuario;
using CottonCandy.Application.AppUsuario.Interfaces;
using CottonCandy.Domain.Core;
using CottonCandy.Domain.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CottonCandy.Repositories.IoC.Application
{
    internal class ApplicationBootstrapper
    {
        internal void ChildServiceRegister(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ILogado, Logado>();
            services.AddScoped<IStorageHelper, StorageHelper>();

            services.AddScoped<IUsuarioAppService, UsuarioAppService>();
            services.AddScoped<IPostagemAppService, PostagemAppService>();
            services.AddScoped<ICurtidasAppService, CurtidasAppService>();
            services.AddScoped<IAlbumFotosAppService, AlbumFotosAppService>();
            services.AddScoped<ILoginAppService, LoginAppService>();
            services.AddScoped<IComentarioAppService, ComentarioAppService>();
            services.AddScoped<IAmigosAppService, AmigosAppService>();
        }
    }
}