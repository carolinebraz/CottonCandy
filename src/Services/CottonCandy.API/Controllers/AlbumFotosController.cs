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
        public async Task<IActionResult> GetFotos()
        {
            try
            {
                var fotos = await _albumFotosAppService
                                       .GetByUserIdOnlyPhotoAsync()
                                       .ConfigureAwait(false);

                return Ok(fotos);
            }
            catch (Exception arg)
            {
                return BadRequest(arg.Message);
            }
        }
    }
}