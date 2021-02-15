using CottonCandy.Application.AppPostagem.Input;
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
        private readonly IComentarioAppService _comentarioAppService;

        public PostagemController(IPostagemAppService postagemAppService,
                                  ICurtidasAppService curtidasAppService,
                                  IComentarioAppService comentarioAppService)
        {
            _postagemAppService = postagemAppService;
            _curtidasAppService = curtidasAppService;
            _comentarioAppService = comentarioAppService;
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
        [Route("{idPostagem}/Curtidas")]
        public async Task<IActionResult> PostCurtidas([FromRoute] int idPostagem)
        {
            try
            {
                await _curtidasAppService
                            .InsertAsync(idPostagem)
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
        [Route("{idPostagem}/Curtidas/Total")]
        public async Task<IActionResult> GetCurtidas([FromRoute] int idPostagem)
        {
            try
            {
                var quantity = await _curtidasAppService
                                        .GetQtdeCurtidasByPostagemIdAsync(idPostagem)
                                        .ConfigureAwait(false);

                return Ok(quantity);
            }
            catch (ArgumentException arg)
            {
                return BadRequest(arg.Message);
            }
        }



        [Authorize]
        [HttpPost]
        [Route("{idPostagem}/Comentarios")]
        public async Task<IActionResult> PostComentarios([FromRoute] int idPostagem, [FromBody] ComentarioInput comentarioInput)
        {
            try
            {
                var user = await _comentarioAppService
                                    .InserirAsync(idPostagem, comentarioInput)
                                    .ConfigureAwait(false);

                return Created("", user);  //traduzir?
            }
            catch (ArgumentException arg)
            {
                return BadRequest(arg.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("{idPostagem}/Comentarios")]
        public async Task<IActionResult> GetComments([FromRoute] int idPostagem)
        {
            var comments = await _comentarioAppService
                                    .PegarComentariosPorIdPostagemAsync(idPostagem)
                                    .ConfigureAwait(false);

            if (comments is null)
                return NoContent();

            return Ok(comments);
        }
    }
}
