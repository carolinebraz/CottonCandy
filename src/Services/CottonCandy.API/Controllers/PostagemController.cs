﻿using CottonCandy.Application.AppPostagem.Input;
using CottonCandy.Application.AppPostagem.Interfaces;
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
    public class PostagemController : ControllerBase
    {
        private readonly IPostagemAppService _postagemAppService;
        private readonly ICurtidasAppService _curtidasAppService;
        public PostagemController(IPostagemAppService postagemAppService,
                                  ICurtidasAppService curtidasAppService)
        {
            _postagemAppService = postagemAppService;
            _curtidasAppService = curtidasAppService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostagemInput postagemInput)
        {
            try
            {
                var postagem = await _postagemAppService
                                    .InsertAsync(postagemInput)
                                    .ConfigureAwait(false);

                return Created("", postagem);
            }
            catch (ArgumentException arg)
            {
                return BadRequest(arg.Message);
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var postagem = await _postagemAppService
                                    .GetByUserIdAsync()
                                    .ConfigureAwait(false);

            if (postagem is null)
                return NoContent();

            return Ok(postagem);
        }

       
        [Authorize]
        [HttpPost]
        [Route("{id}/Curtidas")]
        public async Task<IActionResult> PostCurtidas([FromRoute] int id)
        {
            try
            {
                await _curtidasAppService
                            .InsertAsync(id)
                            .ConfigureAwait(false);

                return Created("", "");
            }
            catch (ArgumentException arg)
            {
                return BadRequest(arg.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("{id}/Curtidas/Total")]
        public async Task<IActionResult> GetCurtidas([FromRoute] int id)
        {
            try
            {
                var quantity = await _curtidasAppService.GetQtdeCurtidasByPostagemIdAsync(id)
                                        .ConfigureAwait(false);

                return Ok(quantity);
            }
            catch (ArgumentException arg)
            {
                return BadRequest(arg.Message);
            }
        }
    }
}
