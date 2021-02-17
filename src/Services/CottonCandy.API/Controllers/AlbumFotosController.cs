using CottonCandy.Application.AppPostagem.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CottonCandy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumFotosController : ControllerBase
    {
        private readonly IAlbumFotosAppService _albumFotosAppService;
        public AlbumFotosController(IAlbumFotosAppService albumFotosAppService)
        {
            _albumFotosAppService = albumFotosAppService;
        }

        [Authorize]
        [HttpGet]
        [Route("{id}/AlbumFotos")]
        public async Task<IActionResult> GetFotos([FromRoute] int id)
        {
            try
            {
                var fotos = await _albumFotosAppService
                                       .GetByUserIdOnlyPhotoAsync(idUsuario)
                                       .ConfigureAwait(false);

                return Ok(fotos);
            }
            catch (ArgumentException arg)
            {
                return BadRequest(arg.Message);
            }
        }
    }
}