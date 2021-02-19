using CottonCandy.Application.AppUsuario.Input;
using CottonCandy.Application.AppUsuario.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CadastrarUsuario([FromBody] UsuarioInput usuarioInput)
        {
            try
            {
                var usuario = await _usuarioAppService
                                        .InserirUsuario(usuarioInput)
                                        .ConfigureAwait(false);

                return Created("", usuario);
            }
            catch (ArgumentException arg)
            {
                return BadRequest(arg.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> ObterUsuario([FromRoute] int id)
        {
            var usuario = await _usuarioAppService
                                    .ObterUsuario(id)
                                    .ConfigureAwait(false);

            if (usuario is null)
                return NotFound();

            return Ok(usuario);
        }

        [Authorize]
        [HttpGet]
        [Route("{id}/Perfil")]
        public async Task<IActionResult> ObterPerfil([FromRoute] int id)
        {
            var perfil = await _usuarioAppService
                                    .ObterPerfil(id)
                                    .ConfigureAwait(false);

            if (perfil is null)
                return NotFound();

            return Ok(perfil);
        }
    }
}