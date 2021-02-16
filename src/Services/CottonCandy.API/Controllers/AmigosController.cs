using CottonCandy.Application.AppUsuario.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CottonCandy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmigosController : ControllerBase
    {
        private readonly IAmigosAppService _amigosAppService;

        public AmigosController(IAmigosAppService amigosAppService)
        {
            _amigosAppService = amigosAppService;
        }

        [Authorize]
        [HttpPost]
        [Route("seguir/{idSeguido}")]
        public async Task<IActionResult> SeguirUsuario([FromRoute] int idSeguido)
        {
            try
            {
                var idRelacionamento = await _amigosAppService.SeguirAsync(idSeguido)
                                                        .ConfigureAwait(false);

                return Created("", idRelacionamento);
            }
            catch (ArgumentException arg)
            {
                return BadRequest(arg.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("amigos")]
        public async Task<IActionResult> Get()
        {
            var amigos = await _amigosAppService.GetListaAmigos()
                                .ConfigureAwait(false);

            if (amigos is null)
                return NotFound();

            return Ok(amigos);

        }
    }
}
