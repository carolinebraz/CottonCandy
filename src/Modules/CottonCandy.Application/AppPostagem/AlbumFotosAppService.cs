﻿using CottonCandy.Application.AppPostagem.Interfaces;
using CottonCandy.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CottonCandy.Application.AppPostagem
{
    public class AlbumFotosAppService : IAlbumFotosAppService
    {
        private readonly IPostagemRepository _postagemRepository;
        public AlbumFotosAppService(IPostagemRepository postagemRepository)
        {
            _postagemRepository = postagemRepository;
        }

        public async Task<List<string>> GetByUserIdOnlyPhotoAsync(int idUsuario)
        {
            var fotos = await _postagemRepository
                                    .GetByUserIdOnlyPhotosAsync(idUsuario)
                                    .ConfigureAwait(false);
            return fotos;
        }
    }
}