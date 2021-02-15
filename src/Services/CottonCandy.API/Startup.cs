using CottonCandy.Repositories.IoC;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CottonCandy.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           //services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();

            services.AddControllers();

            var key = Encoding.ASCII.GetBytes(Configuration.GetSection("Secrets").Value); // pega a chave criptografada do appsettings e transforma em Bytes

            services.AddAuthentication(x => //Avisa a API que vai utilizar um esquema de autenticação, no caso JWT
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x => //configuracoes para gerar o token de acordo com os parametros esperados
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters // parametros para validar o token
                {
                    ValidateIssuerSigningKey = true, //validar a assinatura com a chave da assinatura que esta no appsettings
                    IssuerSigningKey = new SymmetricSecurityKey(key), //pega o Bytes e gera uma chave, e todo token gerado pela API é validado com essa chave
                    ValidateLifetime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddSwaggerGen(c =>
            {

                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "CottonCandy",
                        Version = "v1",
                        Description = "Api CottonCandy",
                        Contact = new OpenApiContact
                        {
                            Name = "CottonCandy",
                            Url = new Uri("https://github.com/carolinebraz/CottonCandy")
                        }
                    });
            });

            RegisterServices(services);
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CottonCandy");
            });
        }

        void RegisterServices(IServiceCollection services)
        {
            new RootBootstrapper().RootRegisterServices(services);
        }
    }
}