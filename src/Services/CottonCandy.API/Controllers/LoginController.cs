using CottonCandy.API.Comum;
using CottonCandy.Application.AppUsuario.Input;
using CottonCandy.Application.AppUsuario.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CottonCandy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginAppService _loginAppService;
        private readonly IConfiguration _configuration;
        public LoginController(ILoginAppService loginAppService,
                                IConfiguration configuration)
        {
            _loginAppService = loginAppService;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost] 
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<object> Post([FromBody] LoginInput input) // metodo post com os dados para o usuario se autenticar
        {
            try
            {
                var logado = await _loginAppService
                                    .LoginAsync(input.Email, input.Senha) //informações do LoginInput
                                    .ConfigureAwait(false);

                if (logado != null) // caso esteja usuario  e senha ok
                {
                    var token = TokenService.GenerateToken(logado, _configuration.GetSection("Secrets").Value); // gera o token chamando a TokenService

                    return new
                    {
                        authenticated = true,
                        accessToken = token, // retorno do acessToken
                        message = "OK"
                    };
                }

                return Unauthorized("Sem permissão"); //quer dizer que algum dado esta errado: email ou senha
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + " " + ex.InnerException); // retorno de usuario nao encontrado
            }
        }
    }
}
