using CottonCandy.Application.AppPostagem.Input;
using CottonCandy.Application.AppPostagem.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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
        public async Task<IActionResult> InserirPostagem([FromBody] PostagemInput postagemInput)
        {
            try
            {
                var postagem = await _postagemAppService
                                        .InserirPostagem(postagemInput)
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
        public async Task<IActionResult> ObterPostagens()
        {
            var postagem = await _postagemAppService
                                    .ObterPostagens()
                                    .ConfigureAwait(false);

            if (postagem is null)
                return NoContent();

            return Ok(postagem);
        }

        [Authorize]
        [HttpPost]
        [Route("{idPostagem}/Curtidas")]
        public async Task<IActionResult> CurtirPostagem([FromRoute] int idPostagem)
        {
            try
            {
                var curtidas = await _curtidasAppService
                                        .Curtir(idPostagem)
                                        .ConfigureAwait(false);

                return Created("", curtidas);
            }
            catch (ArgumentException arg)
            {
                return BadRequest(arg.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("{idPostagem}/Curtidas/Total")]
        public async Task<IActionResult> ObterCurtidas([FromRoute] int idPostagem)
        {
            try
            {
                var quantity = await _curtidasAppService
                                        .ObterCurtidas(idPostagem)
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
        public async Task<IActionResult> ComentarPostagem([FromRoute] int idPostagem, 
                                                         [FromBody] ComentarioInput comentarioInput)
        {
            try
            {
                var user = await _comentarioAppService
                                    .InserirComentario(idPostagem, comentarioInput)
                                    .ConfigureAwait(false);

                return Created("", user); 
            }
            catch (ArgumentException arg)
            {
                return BadRequest(arg.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("{idPostagem}/Comentarios")]
        public async Task<IActionResult> ObterComentarios([FromRoute] int idPostagem)
        {
            var comments = await _comentarioAppService
                                    .ObterComentarios(idPostagem)
                                    .ConfigureAwait(false);

            if (comments is null)
                return NoContent();

            return Ok(comments);
        }

        [Authorize]
        [HttpGet]
        [Route("linhaDoTempo")]
        public async Task<IActionResult> ObterLinhaDoTempo()
        {
            var linhaDoTempo = await _postagemAppService
                                .ObterLinhaDoTempo()
                                .ConfigureAwait(false);

            return Ok(linhaDoTempo);
        }
    }
}