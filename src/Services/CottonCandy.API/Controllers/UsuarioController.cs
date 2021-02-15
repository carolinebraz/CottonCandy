
using CottonCandy.Application.AppUser.Interfaces;
using CottonCandy.Application.AppUsuario.Input;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CottonCandy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioAppService _usuarioAppService;

        public UsuarioController(IUsuarioAppService usuarioAppService)
        {
            _usuarioAppService = usuarioAppService;
        }
        [AllowAnonymous] // atributo para permitir o acesso por usuários não autenticados a ações individuais
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UsuarioInput usuarioInput)
        {
            try {
                var usuario = await _usuarioAppService.InsertAsync(usuarioInput).ConfigureAwait(false);

                return Created("", usuario);
            }
            catch(ArgumentException arg)
            {
                return BadRequest(arg.Message);
            }
  
        }

        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var usuario = await _usuarioAppService
                                .GetByIdAsync(id)
                                 //Método Get para usuários e postagens
                                .ConfigureAwait(false);

            if (usuario is null)
                return NotFound();

            return Ok(usuario);

        }

        [HttpGet]
        [Route("perfil/{id}")]
        public async Task<IActionResult> GetUsuarioPerfil([FromRoute] int id)
        {
            var perfil = await _usuarioAppService
                                .ObterInformacoesPorIdAsync(id)
                                //Método Get para usuários e postagens
                                .ConfigureAwait(false);

            if (perfil is null)
                return NotFound();

            return Ok(perfil);

        }
    }
}
