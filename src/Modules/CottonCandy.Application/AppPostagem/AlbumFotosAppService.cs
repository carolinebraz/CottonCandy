using CottonCandy.Application.AppPostagem.Interfaces;
using CottonCandy.Domain.Core.Interfaces;
using CottonCandy.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CottonCandy.Application.AppPostagem
{
    public class AlbumFotosAppService : IAlbumFotosAppService
    {
        private readonly IPostagemRepository _postagemRepository;
        private readonly ILogado _logado;
        public AlbumFotosAppService(IPostagemRepository postagemRepository, ILogado logado)
        {
            _postagemRepository = postagemRepository;
            _logado = logado;
        }

        public async Task<List<String>> GetByUserIdOnlyPhotoAsync()
        {
            var usuarioId = _logado.GetUsuarioLogadoId();

            var fotos = await _postagemRepository
                                    .GetByUserIdOnlyPhotosAsync(usuarioId)
                                    .ConfigureAwait(false);
            return fotos;
        }
    }
}