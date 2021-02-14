using System;
using System.Threading.Tasks;
using CottonCandy.Application.AppPostagem.Input;
using CottonCandy.Application.AppPostagem.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CottonCandy.API.Controllers
{
    public class PostagemController : ControllerBase
    {
        private readonly IPostagemAppService _postagemAppService;
        private readonly IComentarioAppService _comentarioAppService;
        private readonly ICurtidasAppService _curtidasAppService;
        public PostageController(IPostagemAppService postagemAppService,
                              IComentarioAppService comentarioAppService,
                              ICurtidasAppService curtidasAppService)
        {
            _postagemAppService = postagemAppService;
            _comentarioAppService = comentarioAppService;
            _curtidasAppService = curtidasAppService;
        }


        [Authorize]
        [HttpPost]
        [Route("{id}/Comentarios")]
        public async Task<IActionResult> PostComentarios([FromRoute] int id, [FromBody] ComentarioInput comentarioInput)
        {
            try
            {
                var user = await _comentarioAppService
                                    .InserirAsync(id, comentarioInput)
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
        [Route("{id}/Comentarios")]
        public async Task<IActionResult> GetComments([FromRoute] int id)
        {
            var comments = await _comentarioAppService
                                    .PegarComentariosPorIdPostagemAsync(id)
                                    .ConfigureAwait(false);

            if (comments is null)
                return NoContent();

            return Ok(comments);
        }


    }
}
